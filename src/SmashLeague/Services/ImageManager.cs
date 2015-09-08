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

        public async Task CreateDefaultImageForTeamAsync(Team team)
        {
            var image = await GetDefaultImageAsync(Defaults.TeamImage);
            team.TeamImage = image;

            _db.Update(team);

            await _db.SaveChangesAsync();
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
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var type = GetImageType(data);
            var bytes = GetImageBytes(data);
            var encodedTime = GetEncodedTime();            
            var relativePath = $"/media/users/{user.UserName}.h-{encodedTime}.{type}";

            var image = await UpdateImageAsync(user.HeaderImage, Defaults.HeaderImage, relativePath, bytes);


            if (user.HeaderImage == null)
            {
                user.HeaderImage = image;
                _db.Add(image);
            }

            _db.Attach(user);

            await _db.SaveChangesAsync();
        }

        public async Task UpdateProfileImageAsync(ApplicationUser user, string data)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            var type = GetImageType(data);
            var bytes = GetImageBytes(data);
            var encodedTime = GetEncodedTime();

            var relativePath = $"/media/users/{user.UserName}.p-{encodedTime}.{type}";

            var image = await UpdateImageAsync(user.ProfileImage, Defaults.ProfileImage, relativePath, bytes);
            if (user.ProfileImage == null)
            {
                user.ProfileImage = image;
                _db.Add(image);
            }

            _db.Attach(user);

            await _db.SaveChangesAsync();
        }

        public Task UpdateProfileImageAsync(Team team, string data)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException(nameof(data));
            }

            throw new NotImplementedException();
        }

        public Task UpdateBannerImageAsync(Team team, string data)
        {
            throw new NotImplementedException();
        }

        private async Task<Image> UpdateImageAsync(Image oldImage, string imageDefault, string newImagePath, byte[] bytes)
        {
            // Write the new image to disk
            var file = _fileProvider.GetFileInfo(newImagePath);
            using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Create))
            {
                await fileStream.WriteAsync(bytes, 0, bytes.Length);
            }

            if (oldImage != null && oldImage != await GetDefaultImageAsync(imageDefault))
            {
                var oldRelativePath = oldImage.Source;

                oldImage.Source = newImagePath;

                // Delete the old image
                var oldFile = _fileProvider.GetFileInfo(oldRelativePath);
                if (oldFile.Exists)
                {
                    File.Delete(oldFile.PhysicalPath);
                }

                return oldImage;
            }
            else
            {
                var image = new Image { Source = newImagePath };

                return image;
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
    }
}
