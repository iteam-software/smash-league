using SmashLeague.Data;
using System;
using Microsoft.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SmashLeague.Data.Extensions;

namespace SmashLeague.Services
{
    public class TeamManager : ITeamManager
    {
        private readonly SmashLeagueDbContext _db;
        private readonly IImageManager _imageMaanger;
        private readonly IRankManager _rankManager;
        private readonly ApplicationUserManager _userManager;

        public TeamManager(
            SmashLeagueDbContext db,
            ApplicationUserManager userManager,
            IRankManager rankManager,
            IImageManager imageManager)
        {
            _db = db;
            _userManager = userManager;
            _rankManager = rankManager;
            _imageMaanger = imageManager;
        }

        public async Task<TeamResult> CreateTeamAsync(DataTransferObjects.Team team)
        {
            // Validate team
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }
            if (team.Roster == null)
            {
                throw new ArgumentNullException(nameof(team.Roster));
            }
            if (team.Roster.Count() < 5)
            {
                throw new InvalidOperationException("Teams must have at least 5 members.");
            }
            if (team.Owner == null)
            {
                throw new ArgumentNullException(nameof(team.Owner));
            }
            if (string.IsNullOrEmpty(team.Owner.Username))
            {
                throw new InvalidOperationException("Team owner invalid username");
            }
            if (string.IsNullOrEmpty(team.Name))
            {
                throw new ArgumentNullException(nameof(team.Name));
            }
            if (!Team.TeamNameRegex.IsMatch(team.Name))
            {
                throw new InvalidOperationException($"Team name {team.Name} is invalid");
            }

            var entity = new Team { Name = team.Name, NormalizedName = team.Name.ToLower().Replace(' ', '-') };

            // Create team roster
            var roster = new List<TeamInvite>();
            foreach (var invitee in team.Roster)
            {
                // Validate invitee
                if (string.IsNullOrEmpty(invitee.Username) && string.IsNullOrEmpty(invitee.Tag))
                {
                    throw new InvalidOperationException("Team roster invalid");
                }

                if (invitee.Username != team.Owner.Username)
                {
                    Player player = null;

                    // If the player has a username, load the user
                    if (!string.IsNullOrEmpty(invitee.Username))
                    {
                        var user = await _userManager.FindByNameAsync(invitee.Username);
                        if (user == null)
                        {
                            throw new InvalidOperationException($"User not found: {invitee.Username}");
                        }

                        player = _db.Players
                            .Include(x => x.User)
                            .SingleOrDefault(x => x.User == user);
                        if (player == null)
                        {
                            throw new InvalidProgramException($"Player entity for user {invitee.Username} not found.");
                        }
                    }
                    else
                    {
                        player = await _db.Players.FirstOrDefaultAsync(x => x.Tag == invitee.Tag);
                    }

                    if (player == null)
                    {
                        throw new InvalidProgramException($"Unable to find player");
                    }

                    var invite = new TeamInvite { Player = player, Team = entity };
                    roster.Add(invite);
                }
            }

            // Create team owner
            var ownerUser = await _userManager.FindByNameAsync(team.Owner.Username);
            if (ownerUser == null)
            {
                throw new InvalidOperationException($"Team owner not found: {team.Owner.Username}");
            }

            var ownerPlayer = _db.Players
                .Include(x => x.User)
                .SingleOrDefault(x => x.User == ownerUser);
            if (ownerPlayer == null)
            {
                throw new InvalidProgramException($"Player entity for owner {ownerUser.UserName} not found.");
            }

            var teamOwner = new TeamOwner { Player = ownerPlayer, Team = entity };

            entity.Invitees = roster;
            entity.Owner = teamOwner;

            // Save these changes
            _db.Add(teamOwner);
            _db.Add(entity);
            foreach (var invitee in roster)
            {
                _db.Add(invitee);
            }

            try
            {
                await _db.SaveChangesAsync();

                // Setup rank & image
                await _rankManager.CreateNewTeamRankingAsync(entity);
                await _imageMaanger.CreateDefaultImageForTeamAsync(entity);
            }
            catch (Exception e)
            {
                return TeamResult.Failed(e);
            }

            return TeamResult.Success(entity);
        }

        public async Task<Team[]> GetTeamsForPlayerAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new InvalidOperationException($"User not found: {username}");
            }

            var player = await _db.Players
                .Include(x => x.Invites).ThenInclude(x => x.Player)
                .Include(x => x.Invites).ThenInclude(x => x.Team).ThenInclude(x => x.TeamImage)
                .Include(x => x.Invites).ThenInclude(x => x.Team).ThenInclude(x => x.Rank)
                .Include(x => x.Invites).ThenInclude(x => x.Team).ThenInclude(x => x.Rank).ThenInclude(x => x.Rating)
                .Include(x => x.Invites).ThenInclude(x => x.Team).ThenInclude(x => x.Rank).ThenInclude(x => x.RankBracket)
                .Include(x => x.Teams).ThenInclude(x => x.Player)
                .Include(x => x.Teams).ThenInclude(x => x.Team).ThenInclude(x => x.TeamImage)
                .Include(x => x.OwnedTeams).ThenInclude(x => x.Player)
                .Include(x => x.OwnedTeams).ThenInclude(x => x.Team).ThenInclude(x => x.TeamImage)
                .Include(x => x.Rank).ThenInclude(x => x.RankBracket)
                .Include(x => x.Rank).ThenInclude(x => x.Rating)
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User == user);

            if (player == null)
            {
                throw new InvalidProgramException($"Player entity for user {user.UserName} not found.");
            }

            var teams = new List<Team>();
            teams.AddRange(player.Teams.Select(x => x.Team));
            teams.AddRange(player.OwnedTeams.Select(x => x.Team));
            teams.AddRange(player.Invites.Select(x => x.Team));

            return teams.OrderByDescending(x => x.Rank.Rating.MatchMakingRating).ToArray();
        }

        public async Task<Team[]> GetTopTeamsAsync(int number)
        {
            return await _db.Teams
                .Include(x => x.Members).ThenInclude(x => x.Player)
                .Include(x => x.Invitees).ThenInclude(x => x.Player)
                .Include(x => x.Rank).ThenInclude(x => x.RankBracket)
                .Include(x => x.Rank).ThenInclude(x => x.Rating)
                .Include(x => x.Owner).ThenInclude(x => x.Player)
                .Include(x => x.TeamImage)
                .OrderByDescending(x => x.Rank.Rating.MatchMakingRating)
                .Take(number)
                .ToArrayAsync();
        }

        public async Task<Player[]> SuggestAsync(DataTransferObjects.Player[] players)
        {
            var suggestions = await _db.Players
                .Include(x => x.User).ThenInclude(x => x.ProfileImage)
                .Include(x => x.User).ThenInclude(x => x.HeaderImage)
                .Where(x => x.LookingForTeam)
                .ToArrayAsync();

            return suggestions;
        }
    }
}
