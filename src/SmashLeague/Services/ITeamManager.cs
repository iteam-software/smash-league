using SmashLeague.Data;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface ITeamManager
    {
        Task<Player[]> Suggest(DataTransferObjects.Player[] players);
    }
}
