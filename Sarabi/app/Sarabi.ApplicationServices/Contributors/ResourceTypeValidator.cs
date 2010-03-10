using Sarabi.Core;
using Sarabi.Core.DataInterfaces;

namespace Sarabi.ApplicationServices.Contributors
{
    public class ResourceTypeValidator : ICelebFinderContributor
    {
        private readonly IDbpediaTypesRepository _typesRepository;

        public ResourceTypeValidator(IDbpediaTypesRepository typesRepository)
        {
            _typesRepository = typesRepository;
        }

        public void Execute(CelebFinderContext context)
        {
            var resource = context.WikipediaUrl.Substring(context.WikipediaUrl.LastIndexOf('/') + 1);
            var type = _typesRepository.GetResourceType(resource);
            
            if (string.IsNullOrEmpty(type))
                throw new InvalidCelebrityException(string.Format("The celeb {0} is not a valid type", context.Name));

            context.Celebrity = new Celebrity {Type = type};
        }
    }
}