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
            user.First = updating.First;
            user.Last = updating.Last;

            // Update images
            if (updating.Banner != null)
            {
                if (!string.IsNullOrEmpty(updating.Banner.Src))
                {
                    if (user.HeaderImage == null)
                    {
                        user.HeaderImage = Data.Image.FromDataUri(updating.Banner.Src);
                        await _imageManager.CreateImage(user.HeaderImage);
                    }
                    else
                    {
                        var image = Data.Image.FromDataUri(updating.Banner.Src);

                        user.HeaderImage.Data = image.Data;
                        user.HeaderImage.MimeType = image.MimeType;
                    }
                }
            }

            if (updating.Image != null)
            {
                if (!string.IsNullOrEmpty(updating.Image.Src))
                {
                    if (user.ProfileImage == null)
                    {
                        user.ProfileImage = Data.Image.FromDataUri(updating.Image.Src);
                        await _imageManager.CreateImage(user.ProfileImage);
                    }
                    else if (user.ProfileImage.ProfileImageId == (await _imageManager.GetDefaultImageAsync(Data.Defaults.ProfileImage)).ProfileImageId)
                    {
                        user.ProfileImage = Data.Image.FromDataUri(updating.Image.Src);
                        await _imageManager.CreateImage(user.ProfileImage);
                    }
                    else
                    {
                        var image = Data.Image.FromDataUri(updating.Image.Src);

                        user.ProfileImage.Data = image.Data;
                        user.ProfileImage.MimeType = image.MimeType;
                    }
                }
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
