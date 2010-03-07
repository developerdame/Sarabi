using Machine.Specifications;
using Moq;
using Sarabi.ApplicationServices;
using Sarabi.ApplicationServices.Contributors;
using Sarabi.Core;
using Sarabi.Data;
using It = Machine.Specifications.It;

namespace Sarabi.Behaviour.ApplicationServices.CelebFinder.Contributors
{
    public class DbFinderTests
    {
        public class DbFinderContext
        {
            private static Mock<ICelebrityRepository> _celebrityRepository;
            public static Mock<ICelebrityRepository> CelebrityRepository
            {
                get { return _celebrityRepository ?? (_celebrityRepository = new Mock<ICelebrityRepository>()); }
                set { _celebrityRepository = value; }
            }

            public static Celebrity Celebrity { get; private set; }

            protected static void Tried_to_find_celebrity_in_the_db(string name)
            {
                var celebFinderContext = new CelebFinderContext {Name = name};
                new DbFinder(CelebrityRepository.Object).Execute(celebFinderContext);
                Celebrity = celebFinderContext.Celebrity;
            }
        }

        private class When_the_celebrity_is_in_the_db : DbFinderContext
        {
            private const string _name = "Britney Spears";
            private static readonly Celebrity _celeb = new Celebrity { Name = _name};

            Because of = () =>
            {
                CelebrityRepository.Setup(m => m.GetCelebrityByName(_name))
                    .Returns(_celeb);
                
                Tried_to_find_celebrity_in_the_db(_name);
            };

            It should_return_the_celeb_from_the_db = () =>
                Celebrity.ShouldEqual(_celeb);
        }

        public class When_the_celebrity_is_not_in_the_db : DbFinderContext
        {
            private const string _name = "Britney spears";

            Because of = () =>
            {
                CelebrityRepository.Setup(m => m.GetCelebrityByName(_name))
                    .Returns((Celebrity)null);

                Tried_to_find_celebrity_in_the_db(_name);
            };

            It should_not_return_a_celeb_from_the_db = () =>
                Celebrity.ShouldBeNull();
        }
    }
}