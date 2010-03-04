using Sarabi.Data;

namespace Sarabi.ApplicationServices.Contributors
{
    public class DbFinder : ICelebFinderContributor
    {
        public void Execute(CelebFinderContext context)
        {
            var celebrityRepository = new CelebrityRepository();

            var celebrity = celebrityRepository.GetCelebrityByName(context.Name);
        }
    }
}


