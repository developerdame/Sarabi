using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        protected override void Execute(ISession session)
        {
            var types = GetFile("Data/ResourceTypes.csv");

            var celebrities = GetCelebrities(types);

            Log.DebugFormat("Imported {0} celebrities/bands", celebrities.Count);

            Import(
                GetFile("Data/Abstracts.csv"),
                celebrities,
                (a, v) => a.Abstract = v
            );
        }


        public void Import(Stream file, Dictionary<string, Celebrity> celebrities, Action<Celebrity, string> set)
        {
            var line = string.Empty;
            var reader = new StreamReader(file);

            while (null != (line = reader.ReadLine()))
            {
                var cols = line.Split('\t');

                var resource = cols[Resource].ToLower();

                if(false == celebrities.ContainsKey(resource))
                    continue;

                Log.DebugFormat("Found an abstract for {0}", resource);

                celebrities[resource].Abstract = cols[2];
            }
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

                resources[resource] = new Celebrity {DbpediaResourceId = resource, Type = type};
            }

            return resources;
        }


        private static Stream GetFile(string filePath)
        {
            if(false == File.Exists(filePath))
                throw new FileNotFoundException(filePath + " is required");

            return new FileStream(filePath, FileMode.Open);
        }
    }
}