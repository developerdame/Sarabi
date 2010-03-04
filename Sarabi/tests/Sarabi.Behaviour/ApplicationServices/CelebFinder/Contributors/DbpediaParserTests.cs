using Machine.Specifications;
using Sarabi.ApplicationServices;
using Sarabi.ApplicationServices.Contributors;
using Sarabi.Core;

namespace Sarabi.Behaviour.ApplicationServices.CelebFinder.Contributors
{
    public class DbpediaParserTests
    {
        public class WikipediaParserContext
        {
            private static readonly CelebFinderContext _context = new CelebFinderContext();

            public static string WikipediaUrl
            {
                set 
                {
                    _context.WikipediaUrl = value;
                    Execute();                
                }
            }

            public static Celebrity Celebrity
            {
                get
                {
                    return _context.Celebrity;
                }
            }
            
            public static void Execute()
            {
                new DbpediaParser().Execute(_context);
            }
        }

        public class When_given_a_persons_wikipedia_url : WikipediaParserContext
        {
            Because of = () => WikipediaUrl = "http://en.wikipedia.org/wiki/Britney_Spears";

            It should_get_the_celeb_information = () =>
            {
                Celebrity.Name.ShouldEqual("Britney Spears");
                Celebrity.Type.ShouldEqual("Person");
            };
        }

        public class When_given_a_bands_wikipedia_url : WikipediaParserContext
        {
            Because of = () => WikipediaUrl = "http://en.wikipedia.org/wiki/The_Beatles";

            It should_get_the_band_information = () =>
            {
                Celebrity.Name.ShouldEqual("The Beatles");
                Celebrity.Type.ShouldEqual("Band");
            };
        }   
    }
}