using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using log4net;
using NHibernate;
using Neoworks.Utils.CommandLine;
using Sarabi.Core;

namespace Sarabi.CommandTool.Commands
{
    [Command("Import", Help = "Import celebrity information from dbpedia files")]
    public class ImportFromDbpedia : Command
    {
        private const int Resource = 0, Type = 2;
        private static readonly ILog Log = LogManager.GetLogger(typeof (ImportFromDbpedia));
        private Dictionary<string, Celebrity> _celebrities;

        protected override void Execute(ISession session)
        {
            var types = GetFile("ResourceTypes.csv");

            _celebrities = GetCelebrities(types);

            Log.DebugFormat("Imported {0} celebrities/bands", _celebrities.Count);

            var resetEvents = new[] {new ManualResetEvent(false), new ManualResetEvent(false), new ManualResetEvent(false), new ManualResetEvent(false)};

            DoInNewThread(() => Import("Title", (a, cols) => a.Name = cols[2], resetEvents[0]));
            DoInNewThread(() => Import("Abstract", (a, cols) => a.Abstract = cols[2], resetEvents[1]));
            DoInNewThread(() => Import("Homepage", (a, cols) => a.HomepageUrl = cols[2], resetEvents[2]));
            DoInNewThread(() => Import("Image", (a, cols) => a.Images.Add(new Image(cols[1], cols[2])), resetEvents[3]));

            WaitHandle.WaitAll(resetEvents);

            Log.DebugFormat("Saving to disk");

            ToXml(_celebrities.Values.ToArray());
        }

        public void ToXml(object entity)
        {
            var serializer = new XmlSerializer(entity.GetType());
            using(var stream = new FileStream("Celebrities.xml", FileMode.CreateNew))
                serializer.Serialize(stream, entity);
        }

        private static void DoInNewThread(Action action)
        {
            ThreadPool.QueueUserWorkItem(o => action());
        }

        public void Import(string contentType, Action<Celebrity, string[]> set, ManualResetEvent @event)
        {
            var line = string.Empty;
            var reader = new StreamReader(GetFile(string.Format("{0}s.csv", contentType)));

            while (null != (line = reader.ReadLine()))
            {
                var cols = line.Split('\t');
                var resource = cols[0].ToLower();

                if(false == _celebrities.ContainsKey(resource))
                    continue;

                Log.DebugFormat("Found an {0} for {1}", contentType, resource);

                set(_celebrities[resource], cols);
            }

            Log.DebugFormat("Finished importing the " + contentType + "s");

            @event.Set();
        }

        private static Dictionary<string, Celebrity> GetCelebrities(Stream csv)
        {
            Log.Debug("Loading dbpedia resource types");

            var resources = new Dictionary<string, Celebrity>();
            foreach (var row in new StreamReader(csv).ReadToEnd().Split('\n'))
            {
                if (string.IsNullOrEmpty(row))
                    continue;

                var cols = row.Split('\t');
                var resource = cols[Resource].ToLower();
                var type = cols[Type].ToLower();

                type = type.Substring(type.LastIndexOf('/') + 1);

                if (resources.ContainsKey(cols[Resource]))
                {
                    Log.Warn("The resource " + cols[Resource] + " is found in the list of types more than once");
                    continue;
                }

                resources[resource] = new Celebrity {DbpediaResourceId = resource, Type = type, WikipediaUrl = "http://en.wikipedia.org/" + resource};
            }

            return resources;
        }


        private static Stream GetFile(string filePath)
        {
            filePath = Path.Combine(@"D:\dev\personal\Sarabi\Sarabi\app\Sarabi.CommandTool\Data\", filePath);

            if(false == File.Exists(filePath))
                throw new FileNotFoundException(filePath + " is required");

            return new FileStream(filePath, FileMode.Open);
        }
    }
}