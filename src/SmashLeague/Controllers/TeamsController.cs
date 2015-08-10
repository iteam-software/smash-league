using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("Teams")]
    public class TeamsController : Controller
    {
        [Route("Content")]
        public IActionResult Content()
        {
            return View();
        }

        [Route("New")]
        public IActionResult New()
        {
            return View();
        }
    }
}
