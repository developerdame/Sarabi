using System.Web.Mvc;

namespace Sarabi.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
