using System;
using System.Linq;
using ScottWater.Boss;

namespace Sarabi.ApplicationServices.Contributors
{
    public class WikipediaPageFinder : ICelebFinderContributor
    {
        public void Execute(CelebFinderContext context)
        {
            const string appId = "AnAkWUfV34G9T5uEFFZ6ffBa6wWhoaFdyVFGOWnBVA9L4zW4OsR1aSJVPm8pCDZQEk5774aOmuTz5V53owQ-";

            var query = string.Format("{0} site:http://en.wikipedia.org", context.Name);
            var result = new WebSearch(appId).Query(query).Get();

            if(result.Count > 1)
                throw new Exception("Dunno which result to choose, there are " + result.Count);

            if(result.Count == 0)
                throw new Exception("Could not find anyone with that name");

            context.WikipediaUrl = result.First().Url;
        }
    }
}