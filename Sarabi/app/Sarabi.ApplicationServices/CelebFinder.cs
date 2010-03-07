using Sarabi.ApplicationServices.Contributors;
using Sarabi.Core;
using Sarabi.Data;

namespace Sarabi.ApplicationServices
{
    public class CelebFinder
    {
        public Celebrity Find(string name)
        {
            var context = new CelebFinderContext {Name = name};
            var workers = new ICelebFinderContributor[] {new DbFinder(new CelebrityRepository()), new WikipediaPageFinder(), new DbpediaParser() };

            foreach (var worker in workers)
            {
                worker.Execute(context);

                if(context.Celebrity != null)
                    break;
            }

            return context.Celebrity;
        }
    }
}
