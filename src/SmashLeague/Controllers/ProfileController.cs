using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using SmashLeague.Data;
using SmashLeague.Authentication.Battlenet;
using System.Threading.Tasks;
using SmashLeague.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    // API Actions
    [Route("api/profile")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // MVC Actions


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Profile user = await _userManager.FindByNameAsync(User.GetBattletag());
            return Json(user);
        }
    }

    [Route("profile")]
    public class ProfileMvcController : Controller { }
}
