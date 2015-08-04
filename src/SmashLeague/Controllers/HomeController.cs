using Microsoft.AspNet.Mvc;
using System.Security.Claims;

namespace SmashLeague.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [Route("", Name = "Home:Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
