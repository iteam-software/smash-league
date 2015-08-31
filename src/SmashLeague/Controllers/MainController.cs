using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace SmashLeague.Controllers
{
    // Main view entry
    [Route("")]
    public class MainController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Initializing")]
        public IActionResult Initialize()
        {
            return View();
        }
    }
}
