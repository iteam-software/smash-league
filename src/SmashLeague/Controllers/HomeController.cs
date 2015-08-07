using Microsoft.AspNet.Mvc;

namespace SmashLeague.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("root")]
        public IActionResult Root()
        {
            return View();
        }
    }
}
