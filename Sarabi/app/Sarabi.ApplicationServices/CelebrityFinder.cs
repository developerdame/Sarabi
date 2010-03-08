using Sarabi.ApplicationServices.Contributors;
using Sarabi.Core;
using Sarabi.Data;

namespace Sarabi.ApplicationServices
{
    public interface ICelebrityFinder
    {
        Celebrity Find(string name);
    }

    public class CelebrityFinder : ICelebrityFinder
    {
        private readonly ICelebrityRepository _celebrityRepository;

        public CelebrityFinder(ICelebrityRepository celebrityRepository)
        {
            _celebrityRepository = celebrityRepository;
        }

        public Celebrity Find(string name)
        {
            var context = new CelebFinderContext {Name = name};
            var workers = new ICelebFinderContributor[]
            {
                new DbFinder(_celebrityRepository), 
                new WikipediaPageFinder(), 
                new ResourceTypeValidator(new DbpediaTypesRepository())
            };

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
