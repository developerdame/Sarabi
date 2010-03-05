using System;
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
   
    public class When_more_than_one_celebrity_name : WikipediaPageFinderContext
    {
        private static MultipleCelebritiesException exceptionThrown;

        private Because of = () =>
                                 {
                                     Name = "James Morrison";
                                     try
                                     {
                                         Execute();
                                     }
                                     catch(MultipleCelebritiesException exception)
                                     {
                                         exceptionThrown = exception;
                                         
                                     }
                                 };

        private It should_throw_too_many_results_exception = () => exceptionThrown.Message.ShouldEqual("Dunno which result to choose, there are 20");

        private It should_return_a_list_of_urls = () => exceptionThrown.CelebrityUrls.Count.ShouldEqual(20);
        
    }
}