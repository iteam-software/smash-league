using SmashLeague.Data;
using Microsoft.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using SmashLeague.Data.Extensions;
using System;
using System.Collections.Generic;
using SmashLeague.Models;

namespace SmashLeague.Services
{
    public class PlayerManager : IPlayerManager
    {
        SmashLeagueDbContext _db;
        private readonly ApplicationUserManager _userManager;

        public PlayerManager(
            SmashLeagueDbContext db,
            ApplicationUserManager userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Player> CreatePlayerForUserAsync(ApplicationUser user)
        {
            var player = new Player { User = user };

            _db.Players.Add(player);
            await _db.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetPlayerByUserNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var player = await _db.Players
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User == user);

            return player;
        }

        public async Task<Player[]> GetPlayersAsync(int? max = default(int?))
        {
            if (max == null)
            {
                return await _db.Players
                    .Include(x => x.User).ThenInclude(y => y.HeaderImage)
                    .Include(x => x.User).ThenInclude(y => y.ProfileImage)
                    .ToArrayAsync();
            }
            else
            {
                return await _db.Players
                    .Include(x => x.User).ThenInclude(y => y.HeaderImage)
                    .Include(x => x.User).ThenInclude(y => y.ProfileImage)
                    .Take(max.Value)
                    .ToArrayAsync();
            }
        }

        public async Task<Player[]> GetPlayersByPartialNameAsync(string partial)
        {
            // TODO: once EF7 is stable, this needs to use related navigation property from Player dbset
            // to load the players
            var list = new List<Player>();
            var users = await _db.Users
                .Where(x => x.UserName.StartsWith(partial, StringComparison.OrdinalIgnoreCase))
                .ToArrayAsync();

            foreach (var user in users)
            {
                var player = await _db.Players
                    .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                    .Include(x => x.User).ThenInclude(x => x.HeaderImage)
                    .SingleOrDefaultAsync(x => x.User == user);

                if (player != null)
                {
                    list.Add(player);
                }
            }

            return list.ToArray();
        }

        public async Task<Player> UpdatePlayerAsync(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            if (string.IsNullOrEmpty(profile.Username))
            {
                throw new ArgumentException("profile must have a valid username");
            }

            var user = await _userManager.FindByNameAsync(profile.Username);
            if (user == null)
            {
                throw new ArgumentException($"No user found with username {profile.Username}");
            }

            var player = await _db.Players
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User == user);
            if (player == null)
            {
                throw new ArgumentException($"No player found with username {profile.Username}");
            }

            player.PreferredRole = profile.PreferredRoles;
            player.LookingForTeam = profile.LookingForTeam;

            _db.Update(player);
            await _db.SaveChangesAsync();

            return player;
        }
    }
}
