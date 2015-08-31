using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Services;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SmashLeague.Controllers
{
    [Authorize]
    [Route("api/team")]
    public class TeamsApiController : Controller
    {
        private readonly ITeamManager _teamManager;

        public TeamsApiController(
            ITeamManager teamManager)
        {
            _teamManager = teamManager;
        }

        [HttpPost("suggest")]
        public async Task<Player[]> Suggest([FromBody] Player[] players)
        {
            var suggeestions = await _teamManager.Suggest(players);
            return suggeestions.Select(x => (Player)x).ToArray();
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
