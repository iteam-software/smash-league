﻿using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Services;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNet.Authorization;
using System;

namespace SmashLeague.Controllers
{
    [Authorize]
    [Route("api/player")]
    public class PlayerApiController : Controller
    {
        private readonly IPlayerManager _playerManager;

        public PlayerApiController(
            IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        [HttpPost("placeholder")]
        public async Task<Player> CreatePlaceholder([FromBody] Player dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            Player player = await _playerManager.CreatePlayerWithTagAsync(dto.Tag);

            return player;
        }

        [HttpGet("search")]
        public async Task<Player[]> GetPartial([FromQuery] string partial)
        {
            var players = await _playerManager.GetPlayersByPartialNameAsync(partial);

            return players.Select(x => (Player)x).ToArray();
        }

        [HttpGet("{username}")]
        public async Task<Player> Get(string username)
        {
            Player player = await _playerManager.GetPlayerByUserNameAsync(username);

            return player;
        }

        [HttpGet]
        public async Task<Player[]> Get()
        {
            var players = await _playerManager.GetPlayersAsync();

            return players
                .Select(x => (Player)x)
                .ToArray();
        }
    }

    [Authorize]
    [Route("player")]
    public class PlayerController : Controller
    {
        [Route("template/roster-player")]
        public IActionResult RosterPlayer()
        {
            return View();
        }

        [Route("content")]
        public IActionResult Content()
        {
            return View();
        }

        [Route("banner")]
        public IActionResult Banner()
        {
            return View();
        }
    }
}
