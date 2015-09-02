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

        // TODO: move this to player API
        [HttpPost("suggest")]
        public async Task<Player[]> Suggest([FromBody] Player[] players)
        {
            var suggeestions = await _teamManager.Suggest(players);
            return suggeestions.Select(x => (Player)x).ToArray();
        }

        [HttpPost]
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

        [HttpGet("{username}/teams")]
        public async Task<Team[]> GetTeamsForPlayer(string username)
        {
            var teams = await _teamManager.GetTeamsForPlayer(username);

            return teams.Select(x => (Team)x).ToArray();
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
