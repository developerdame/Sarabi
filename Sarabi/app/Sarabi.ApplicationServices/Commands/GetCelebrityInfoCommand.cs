using Sarabi.Core;
using Sarabi.Data;

namespace Sarabi.ApplicationServices.Commands
{
    public class GetCelebrityInfoCommand
    {
        private readonly ICelebrityRepository _celebrityRepository;

        public GetCelebrityInfoCommand(ICelebrityRepository celebrityRepository)
        {
            _celebrityRepository = celebrityRepository;
        }

        public string CelebrityName { get; set; }

        public Celebrity Execute()
        {
            return _celebrityRepository.GetCelebrityByName(CelebrityName);
        }
    }
}