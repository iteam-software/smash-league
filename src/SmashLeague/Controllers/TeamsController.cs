using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Authorize]
    [Route("Teams")]
    public class TeamsController : Controller
    {
        [Route("Content")]
        public IActionResult Content()
        {
            return View();
        }

        [Route("Banner")]
        public IActionResult Banner()
        {
            return View();
        }

        [Route("New")]
        [Authorize]
        public IActionResult New()
        {
            return View();
        }
    }
}
