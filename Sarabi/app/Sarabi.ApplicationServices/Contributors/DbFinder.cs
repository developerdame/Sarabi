using Sarabi.Data;

namespace Sarabi.ApplicationServices.Contributors
{
    public class DbFinder : ICelebFinderContributor
    {
        private readonly ICelebrityRepository _celebrityRepository;

        public DbFinder(ICelebrityRepository celebrityRepository)
        {
            _celebrityRepository = celebrityRepository;
        }

        public void Execute(CelebFinderContext context)
        {
            context.Celebrity = _celebrityRepository.GetCelebrityByName(context.Name);
        }
    }
}


