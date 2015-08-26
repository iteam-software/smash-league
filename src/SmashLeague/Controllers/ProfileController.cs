using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.Data.Extensions;
using SmashLeague.Models;
using SmashLeague.Services;
using System;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    // API Actions
    [Route("api/profile")]
    public class ProfileApiController : Controller
    {
        private readonly IImageManager _imageManager;
        private readonly ApplicationUserManager _userManager;

        public ProfileApiController(
            ApplicationUserManager userManager,
            IImageManager imageManager)
        {
            _userManager = userManager;
            _imageManager = imageManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<Profile> Get()
        {
            return await _userManager.FindByNameAsync(User.GetUserName());
        }

        [HttpPut]
        [Authorize]
        public async Task<Profile> Put([FromBody] Profile updating)
        {
            if (updating == null)
            {
                throw new ArgumentException("updating");
            }


            if (updating.Username != User.GetUserName())
            {
                throw new SecurityException("Attempted to update a different user than the currently logged in user.");
            }

            var user = await _userManager.FindByNameAsync(updating.Username);

            // Update fields
            user.Birthday = updating.Birthday;
            user.Location = updating.Location;
            user.Name = updating.Name;

            // Update images
            if (!string.IsNullOrEmpty(updating.ProfileImageEditData))
            {
                // TODO: image validation
                await _imageManager.UpdateProfileImageAsync(user, updating.ProfileImageEditData);
            }

            if (!string.IsNullOrEmpty(updating.BannerImageEditData))
            {
                // TODO: image validation
                await _imageManager.UpdateBannerImageAsync(user, updating.BannerImageEditData);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                throw new InvalidOperationException($"Failed to update the user: {string.Join(",", result.Errors.Select(x => x.Description))}");
            }
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
