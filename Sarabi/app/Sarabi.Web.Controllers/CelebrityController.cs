using System.Web.Mvc;
using Sarabi.Web.Controllers.ViewModels.Celebrity;

namespace Sarabi.Web.Controllers
{
    public class CelebrityController : Controller
    {
        public ActionResult Seen(string name)
        {
            return View(new SeenCelebrityViewModel {SearchedName = name});
        }
    }
}