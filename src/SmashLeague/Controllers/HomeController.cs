using Microsoft.AspNet.Mvc;

namespace SmashLeague.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        public IActionResult Banner()
        {
            return View();
        }

        [Route("Content")]
        public IActionResult Content()
        {
            return View();
        }
    }
}
