using Machine.Specifications;
using Sarabi.ApplicationServices;
using Sarabi.ApplicationServices.Contributors;

namespace Sarabi.Behaviour.ApplicationServices.CelebFinder.Contributors
{
    public class WikipediaPageFinderContext
    {
        private static CelebFinderContext _context;

        public WikipediaPageFinderContext()
        {
            _context = new CelebFinderContext();
        }

        public static string Name
        {
            set { _context.Name = value;}
        }
    
        public static string WikipediaUrl
        {
            get { return _context.WikipediaUrl; }
        }

        public static void Execute()
        {
            new WikipediaPageFinder().Execute(_context);   
        }
    }

    public class When_valid_celeb_name : WikipediaPageFinderContext
    {
        Because of = () =>
        {
            Name = "Britney Spears";
            Execute();
        };

        private It should_find_a_wikipedia_url = () => WikipediaUrl.ShouldEqual("http://www.wikipedia.org/wiki/Britney_Spears");
    }
}