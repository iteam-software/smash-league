using Microsoft.Data.Entity;
using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class ImageManager : IImageManager
    {
        private readonly SmashLeagueDbContext _db;

        public ImageManager(SmashLeagueDbContext db)
        {
            _db = db;
        }

        public async Task<Image> GetDefaultImageAsync(string image)
        {
            var defaultImage = await _db.DefaultImages
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Name == image);

            return defaultImage != null ? defaultImage.Image : null;
        }
    }
}
