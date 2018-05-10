using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcImageStreaming.Controllers
{
    public class FileClientController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Index Page";

            return View();
        }

        public ActionResult Client()
        {
            ViewBag.Title = "Ex Client";

            return View();
        }
    }
}
