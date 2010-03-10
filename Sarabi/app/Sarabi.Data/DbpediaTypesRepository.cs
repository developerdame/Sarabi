using System.Collections.Generic;
using System.IO;
using log4net;
using Sarabi.Core.DataInterfaces;

namespace Sarabi.Data
{
    public class DbpediaTypesRepository : IDbpediaTypesRepository
    {
        private ILog _log = LogManager.GetLogger(typeof (DbpediaTypesRepository));

        private Dictionary<string, string> _resources;

        private const int Resource = 0, Type = 2;

        public DbpediaTypesRepository()
            //:this(new FileStream("Types.csv", FileMode.Open))
        {
        }

        public DbpediaTypesRepository(Stream csv)
        {
            _resources = GetResources(csv);
        }

        private Dictionary<string ,string> GetResources(Stream csv)
        {
            if (_resources != null)
                return _resources;

            _log.Debug("Loading dbpedia resource types");
            
            _resources = new Dictionary<string, string>();

            foreach(var row in new StreamReader(csv).ReadToEnd().Split('\n'))
            {
                if(string.IsNullOrEmpty(row))
                    continue;

                var cols = row.Split('\t');
                var resource = cols[Resource].ToLower();
                var type = cols[Type].ToLower();

                type = type.Substring(type.LastIndexOf('/') + 1);

                if(_resources.ContainsKey(cols[Resource]))
                {
                    _log.Warn("The resource " + cols[Resource] + " is found in the list of types more than once");
                    continue;
                }

                _resources[resource] = type;
            }

            return _resources;
        }

        public string GetResourceType(string resource)
        {
            resource = resource.ToLower();

            if(_resources.ContainsKey(resource))
                return _resources[resource];

            return null;
        }
    }
}