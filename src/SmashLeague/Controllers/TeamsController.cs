using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Security.Authorization;
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
        private readonly IImageManager _imageManager;

        public TeamsApiController(
            ITeamManager teamManager,
            IImageManager imageManager,
            INotificationManager notificationManager)
        {
            _teamManager = teamManager;
            _imageManager = imageManager;
            _notificationManager = notificationManager;
        }

        // TODO: move this to player API?
        [HttpPost("suggest")]
        public async Task<Player[]> Suggest([FromBody] Player[] players)
        {
            var suggeestions = await _teamManager.SuggestAsync(players);
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

        [HttpGet("top/{number}")]
        public async Task<Team[]> GetTopTeams(int number)
        {
            var teams = await _teamManager.GetTopTeamsAsync(number);

            return teams.Select(x => (Team)x).ToArray();
        }

        [HttpGet("search")]
        public async Task<Team[]> SearchForTeam([FromQuery] string q)
        {
            var teams = await _teamManager.SearchForTeamsAsync(q);

            return teams.Select(x => (Team)x).ToArray();
        }

        [HttpGet("{normalizedName}")]
        public async Task<Team> Get(string normalizedName)
        {
            return await _teamManager.FindTeamByNormalizedNameAsync(normalizedName);
        }

        [HttpGet("{username}/teams")]
        public async Task<Team[]> GetTeamsForPlayer(string username)
        {
            var teams = await _teamManager.GetTeamsForPlayerAsync(username);

            return teams.Select(x => (Team)x).ToArray();
        }

        [HttpPut("{normalizedName}/owner")]
        [Authorize(AuthorizationDefaults.PolicyTeamOwner)]
        public async Task<Team> ChangeOwner(string normalizedName, [FromBody] string newOwner)
        {
            var team = await _teamManager.FindTeamByNormalizedNameAsync(normalizedName);
            team = await _teamManager.UpdateTeamOwner(team, newOwner);

            // TODO notify the new owner

            return team;
        }

        [HttpPut("{normalizedName}")]
        [Authorize(AuthorizationDefaults.PolicyTeamOwner)]
        public async Task<Team> UpdateTeam([FromBody] Team team)
        {
            var updated = await _teamManager.UpdateTeam(team);

            if (!string.IsNullOrEmpty(team.TeamImageEditSrc))
            {
                await _imageManager.UpdateProfileImageAsync(updated, team.TeamImageEditSrc);
            }

            if (!string.IsNullOrEmpty(team.HeaderImageEditSrc))
            {
                await _imageManager.UpdateBannerImageAsync(updated, team.HeaderImageEditSrc);
            }

            return updated;
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
        public IActionResult New()
        {
            return View();
        }

        [Route("Search")]
        public IActionResult Search()
        {
            return View();
        }

        [Route("Detail")]
        public IActionResult Detail()
        {
            return View();
        }

        [Route("template/team-listing")]
        public IActionResult Team()
        {
            return View();
        }
    }
}
