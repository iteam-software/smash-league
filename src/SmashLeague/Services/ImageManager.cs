using Microsoft.Data.Entity;
using SmashLeague.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNet.FileProviders;
using System.IO;
using System.Text;

namespace SmashLeague.Services
{
    public class ImageManager : IImageManager
    {
        private readonly SmashLeagueDbContext _db;
        private readonly IFileProvider _fileProvider;

        public ImageManager(SmashLeagueDbContext db, IFileProvider fileProvider)
        {
            _db = db;
            _fileProvider = fileProvider;
        }

        public async Task<Image> CreateImage(Image image)
        {
            _db.Images.Add(image);
            await _db.SaveChangesAsync();

            return image;
        }

        public async Task<Image> GetDefaultImageAsync(string image)
        {
            var defaultImage = await _db.DefaultImages
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Name == image);

            return defaultImage != null ? defaultImage.Image : null;
        }

        public async Task UpdateBannerImageAsync(ApplicationUser user, string data)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            var type = GetImageType(data);
            var bytes = GetImageBytes(data);
            var encodedTime = GetEncodedTime();
            
            var relativePath = $"/media/users/{user.UserName}.h-{encodedTime}.{type}";
            var file = _fileProvider.GetFileInfo(relativePath);
            using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }

            if (user.HeaderImage != null)
            {
                var oldRelativePath = user.HeaderImage.Source;

                user.HeaderImage.Source = relativePath;
                _db.Attach(user.HeaderImage);
                await _db.SaveChangesAsync();

                // Delete the old image
                var oldFile = _fileProvider.GetFileInfo(oldRelativePath);
                if (oldFile.Exists)
                {
                    File.Delete(oldFile.PhysicalPath);
                }
            }
            else
            {
                var image = new Image { Source = relativePath };
                _db.Images.Add(image);
                await _db.SaveChangesAsync();

                user.HeaderImage = image;
            }
        }

        public async Task UpdateProfileImageAsync(ApplicationUser user, string data)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }

            var type = GetImageType(data);
            var bytes = GetImageBytes(data);
            var encodedTime = GetEncodedTime();

            var relativePath = $"/media/users/{user.UserName}.p-{encodedTime}.{type}";
            var file = _fileProvider.GetFileInfo(relativePath);
            using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }

            if (user.ProfileImage != null && user.ProfileImage != await GetDefaultImageAsync(Defaults.ProfileImage))
            {
                var oldRelativePath = user.ProfileImage.Source;

                user.ProfileImage.Source = relativePath;
                _db.Attach(user.ProfileImage);
                await _db.SaveChangesAsync();

                // Delete the old image
                var oldFile = _fileProvider.GetFileInfo(oldRelativePath);
                if (oldFile.Exists)
                {
                    File.Delete(oldFile.PhysicalPath);
                }
            }
            else
            {
                var image = new Image { Source = relativePath };
                _db.Images.Add(image);
                await _db.SaveChangesAsync();

                user.ProfileImage = image;
            }
        }

        private string GetEncodedTime()
        {
            return Convert.ToBase64String(BitConverter.GetBytes(DateTime.Now.Ticks))
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private string GetImageType(string data)
        {
            return Image.GetTypeFromDataUri(data);
        }

        private byte[] GetImageBytes(string data)
        {
            return Convert.FromBase64String(Image.GetBase64StringFromDataUri(data));
        }

        public async Task CreateDefaultImageForTeamAsync(Team team)
        {
            var image = await GetDefaultImageAsync(Defaults.TeamImage);
            team.TeamImage = image;

            _db.Update(team);

            await _db.SaveChangesAsync();
        }
    }
}
