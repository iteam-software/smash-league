using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using SmashLeague.Data.Extensions;
using SmashLeague.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly IImageManager _imageManager;
        private readonly ApplicationUserManager _userManager;

        public ImageController(
            ApplicationUserManager userManager,
            IImageManager imageManager)
        {
            _userManager = userManager;
            _imageManager = imageManager;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetImageForUser(string username)
        {
            var user =  await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return HttpNotFound();
            }

            var image = user.ProfileImage;
            if (image == null)
            {
                return HttpNotFound();
            }

            return Content(image.Data, image.MimeType);
        }
    }
}
