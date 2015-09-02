using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Services;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Controllers
{
    [Authorize]
    [Route("api/team")]
    public class TeamsApiController : Controller
    {
        private readonly ITeamManager _teamManager;
        private readonly INotificationManager _notificationManager;

        public TeamsApiController(
            ITeamManager teamManager,
            INotificationManager notificationManager)
        {
            _teamManager = teamManager;
            _notificationManager = notificationManager;
        }

        [HttpPost("suggest")]
        public async Task<Player[]> Suggest([FromBody] Player[] players)
        {
            var suggeestions = await _teamManager.Suggest(players);
            return suggeestions.Select(x => (Player)x).ToArray();
        }

        [HttpPost("new")]
        public async Task<Team> Create([FromBody] Team team)
        {
            var result = await _teamManager.CreateTeamAsync(team);
            if (result.Succeeded)
            {
                await _notificationManager.NotifyTeamInvite(result.Team);
            }
            else
            {
                throw ManagerException<TeamError>.Create(result);
            }

            return result.Team;
        }
    }

    [Authorize]
    [Route("Teams")]
    public class TeamsController : Controller
    {
        [Route("Content")]
        public IActionResult Content()
        {
            return View();
        }

        [Route("Banner")]
        public IActionResult Banner()
        {
            return View();
        }

        [Route("New")]
        [Authorize]
        public IActionResult New()
        {
            return View();
        }
    }
}
