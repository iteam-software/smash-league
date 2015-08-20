using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.Data.Extensions;
using SmashLeague.Models;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    // API Actions
    [Route("api/profile")]
    public class ProfileApiController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public ProfileApiController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            Profile user = await _userManager.FindByNameAsync(User.GetUserName());
            return Json(user);
        }
    }

    [Authorize]
    [Route("profile")]
    public class ProfileController : Controller
    {
        [Route("content")]
        public IActionResult Content()
        {
            return View();
        }
    }
}
