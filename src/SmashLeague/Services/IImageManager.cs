using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IImageManager
    {
        Task<Image> GetDefaultImageAsync(string image);
        Task<Image> CreateImage(Image image);
    }
}
