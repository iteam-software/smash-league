using Microsoft.AspNet.Mvc;

namespace SmashLeague.Controllers
{
    [Route("Partial")]
    public class PartialController : Controller
    {
        [HttpGet("Search-Nav")]
        public IActionResult SearchNav()
        {
            return PartialView();
        }
    }
}
