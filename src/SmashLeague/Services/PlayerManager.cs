using SmashLeague.Data;
using Microsoft.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using SmashLeague.Data.Extensions;
using System;
using System.Collections.Generic;
using SmashLeague.DataTransferObjects;
using Player = SmashLeague.Data.Player;

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

        public async Task<Player> CreatePlayerWithTagAsync(string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentNullException(nameof(tag));
            }

            Player player;

            // See if this player already exists
            player = await _db.Players
                .SingleOrDefaultAsync(x => tag.Equals(x.Tag, StringComparison.OrdinalIgnoreCase));
            if (player == null)
            {
                player = new Player { Tag = tag };
                _db.Add(player);
                await _db.SaveChangesAsync();
            }

            return player;
        }

        public async Task<Player> GetPlayerByUserNameAsync(string username)
        {
            return await _db.Players
                .Include(x => x.User).ThenInclude(x => x.HeaderImage)
                .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                .SingleOrDefaultAsync(x => x.User.UserName == username);
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
            return await _db.Players
                .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                .Where(x => x.User.UserName.StartsWith(partial, StringComparison.OrdinalIgnoreCase))
                .ToArrayAsync();
        }

        public async Task<Player> UpdatePlayerAsync(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException(nameof(profile));
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
