using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("anonymous")]
    public class AnonymousController : Controller
    {
        [Route("navigation")]
        public IActionResult Navigation()
        {
            return View();
        }
    }
}
