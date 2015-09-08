using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IImageManager
    {
        Task<Image> GetDefaultImageAsync(string image);
        Task<Image> CreateImage(Image image);
        Task CreateDefaultImageForTeamAsync(Team team);
        Task UpdateProfileImageAsync(ApplicationUser user, string data);
        Task UpdateProfileImageAsync(Team team, string data);
        Task UpdateBannerImageAsync(ApplicationUser user, string data);
        Task UpdateBannerImageAsync(Team team, string data);
    }
}
