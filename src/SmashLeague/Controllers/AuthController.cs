using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using SmashLeague.Authentication.Battlenet;
using SmashLeague.Data;
using System.Threading.Tasks;
using System.Security.Claims;
using SmashLeague.Models;
using Microsoft.AspNet.Authorization;

namespace SmashLeague.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILoggerFactory loggerFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = loggerFactory.CreateLogger(nameof(AuthController));
        }

        [HttpPost]
        [Route("signout", Name = "Auth:SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return Content(string.Empty);
        }

        // GET /auth/signin-with-battlenet External signin for Battlenet
        [HttpGet]
        [Route("signin-with-battlenet", Name = "Auth:SigninWithBattlenet")]
        public IActionResult SigninWithBattlenet(string returnUrl = null)
        {
            var redirectUrl = Url.Action("SigninExternalCallback", "Auth", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(BattlenetAuthenticationDefaults.AuthenticationScheme, redirectUrl);
            return new ChallengeResult(BattlenetAuthenticationDefaults.AuthenticationScheme, properties);
        }

        // External signin callback
        [HttpGet]
        [Route("signin-external-callback", Name = "Auth:SigninExternalCallback")]
        public async Task<IActionResult> SigninExternalCallback(string returnUrl = null)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                _logger.LogWarning("Failed to get external login info.");
                return HttpBadRequest();
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"User login complete for user with key {info.ProviderKey}.");
                return RedirectToAction(nameof(ExternalSigninSuccess));
            }
            else if (result.IsLockedOut)
            {
                _logger.LogInformation($"User login failed for user with key {info.ProviderKey}. User account is locked.");
                return View("AccountLocked");
            }
            else if (result.IsNotAllowed)
            {
                _logger.LogInformation($"User login failed for user with key {info.ProviderKey}. User is not allowed to login.");
                return View("NotAllowed");
            }
            else if (result.RequiresTwoFactor)
            {
                _logger.LogInformation($"User login failed for user with key {info.ProviderKey}. User requires two factor authentication, redirecting.");
                return RedirectToAction(nameof(SendCode), new { returnUrl = returnUrl });
            }
            else
            {
                // User is not registered.
                ViewData["LoginProvider"] = info.LoginProvider;
                ViewData["ReturnUrl"] = returnUrl;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel());
            }
        }

        [HttpGet]
        [Route("send-code", Name = "Auth:SendCode")]
        public async Task<IActionResult> SendCode(string returlUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            // TODO: load user factors and build view model
            return View();
        }

        [HttpGet]
        [Route("signin-external-success")]
        public IActionResult ExternalSigninSuccess(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("complete-external-registration", Name = "Auth:ExternalLoginConfirmation")]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            if (User.IsSignedIn())
            {
                return View("ExternalSigninSuccess");
            }

            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }

                var user = new ApplicationUser
                {
                    UserName = info.ExternalPrincipal.FindFirstValue(BattlenetAuthenticationDefaults.BattletagClaimType),
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var innerResult = await _userManager.AddLoginAsync(user, info);
                    if (innerResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction(nameof(ExternalSigninSuccess), new { ReturnUrl = returnUrl });
                    }
                    AddErrors(innerResult);
                }
                AddErrors(result);
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [HttpGet]
        [Route("authenticate", Name = "Auth:Authenticate")]
        public IActionResult Authenticate()
        {
            var payload = new {
                Authenticated = User.IsSignedIn(),
                Battletag = User.GetBattletag()
            };

            return Json(payload);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }
    }
}
