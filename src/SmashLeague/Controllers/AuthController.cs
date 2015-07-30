using Microsoft.AspNet.Mvc;
using SmashLeague.Authentication.Battlenet;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        // GET: /<controller>/
        [Route("signin", Name = "Auth:Signin")]
        public IActionResult Signin()
        {
            return View();
        }

        [Route("signin-with-battlenet", Name = "Auth:SigninWithBattlenet")]
        public async Task SigninWithBattlenet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                await Context.Authentication.ChallengeAsync(BattlenetAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
