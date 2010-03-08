using System.Web.Mvc;
using Sarabi.ApplicationServices;

namespace Sarabi.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly ICelebrityFinder _celebrityFinder;

        public HomeController(ICelebrityFinder celebrityFinder)
        {
            _celebrityFinder = celebrityFinder;
        }

        public ActionResult Index()
        {
            var celeb = _celebrityFinder.Find("Britney Spears");
            return View();
        }
    }
}
