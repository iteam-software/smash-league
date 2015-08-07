using SmashLeague.DataTransferObjects;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IMatchManager
    {
        Task<MatchDto[]> GetMatchDtoAsync(bool? completed = null);
    }
}
