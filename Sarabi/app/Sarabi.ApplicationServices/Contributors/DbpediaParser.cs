using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using Sarabi.Core;

namespace Sarabi.ApplicationServices.Contributors
{
    public class DbpediaParser : ICelebFinderContributor
    {
        private Dictionary<string, string > _validEntityTypes = new Dictionary<string, string>
        {
            { "http://xmlns.com/foaf/0.1/Person", "Person" },
            { "http://dbpedia.org/ontology/Band", "Band" }
        };

        public void Execute(CelebFinderContext context)
        {
            var dbpediaUrl = string.Format("http://dbpedia.org/page/") +
                context.WikipediaUrl.Substring(context.WikipediaUrl.LastIndexOf('/') + 1);
            var doc = new HtmlWeb().Load(dbpediaUrl);
            
            var type = GetType(doc);
            
            if (string.IsNullOrEmpty(type))
                throw new InvalidCelebrityException(string.Format("The celeb {0} is not a valid type", context.Name));

            context.Celebrity = new Celebrity {Type = type, Name = GetName(doc)};
        }

        private static string GetName(HtmlDocument doc)
        {
            return doc.DocumentNode.SelectSingleNode("//span[@property='dbpprop:name']").InnerText;
        }

        private string GetType(HtmlDocument doc)
        {
            return (from type in _validEntityTypes
                    let href = string.Format("//a[@href='{0}']", type.Key)
                    where doc.DocumentNode.SelectSingleNode(href) != null
                    select type.Value).FirstOrDefault();
        }
    }
}