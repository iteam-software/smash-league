using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Services;
using System.Threading.Tasks;


namespace SmashLeague.Controllers
{
    [Route("api/match")]
    public class MatchController : Controller
    {
        private readonly IMatchManager _matches;

        public MatchController(
            IMatchManager matches)
        {
            _matches = matches;
        }

        // Get all matches matches
        [HttpGet]
        public async Task<MatchDto[]> Get()
        {
            return await _matches.GetMatchDtoAsync();
        }

        [HttpGet("completed")]
        public async Task<MatchDto[]> GetCompleted()
        {
            return await _matches.GetMatchDtoAsync(true);
        }

        [HttpGet("not-completed")]
        public async Task<MatchDto[]> GetNotCompleted()
        {
            return await _matches.GetMatchDtoAsync(false);
        }
    }
}
