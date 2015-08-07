using Microsoft.AspNet.Mvc;

namespace SmashLeague.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        [Route("banner")]
        public IActionResult Banner()
        {
            return View();
        }

        [Route("content")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
