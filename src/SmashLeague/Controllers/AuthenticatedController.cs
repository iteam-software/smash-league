using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("authenticated")]
    public class AuthenticatedController : Controller
    {
        [Authorize]
        [Route("navigation")]
        public IActionResult Navigation()
        {
            return View();
        }
    }
}
