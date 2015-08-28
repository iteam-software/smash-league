using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("tournament")]
    public class TournamentController : Controller
    {
        [Route("content")]
        public IActionResult Content()
        {
            return View();
        }

        [Route("banner")]
        public IActionResult Banner()
        {
            return View();
        }
    }
}
