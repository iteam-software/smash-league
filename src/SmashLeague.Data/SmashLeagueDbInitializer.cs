using Microsoft.AspNet.Identity;
using Microsoft.Framework.Configuration;
using SmashLeague.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmashLeague.Data
{
    public class SmashLeagueDbInitializer
    {
        private readonly ApplicationUserManager _userManager;
        private readonly SmashLeagueDbContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SmashLeagueDbInitializer(
            SmashLeagueDbContext db,
            ApplicationUserManager userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateDefaultSeason(string seasonName)
        {
            if (string.IsNullOrEmpty(seasonName))
            {
                throw new ArgumentNullException(nameof(seasonName));
            }

            if (!await _db.DefaultSeasons.AnyAsync(x => x.Name == Defaults.DefaultSeason))
            {
                var season = new Season
                {
                    Name = seasonName,
                    Brackets = new RankBracket[]
                    {
                        new RankBracket { Type = RankBrackets.NeverLucky, Name = "Never Lucky", MinimumMMR = 0, MaximumMMR = 1599 },
                        new RankBracket { Type = RankBrackets.Common, Name = "Common", MinimumMMR = 1600, MaximumMMR = 2199 },
                        new RankBracket { Type = RankBrackets.Rare, Name = "Rare", MinimumMMR = 2199, MaximumMMR = 2599 },
                        new RankBracket { Type = RankBrackets.Legendary, Name = "Legendary", MinimumMMR = 2600, MaximumMMR = 9999 }
                    }
                };

                foreach (var bracket in season.Brackets)
                {
                    bracket.Season = season;
                    _db.Add(bracket);
                }

                _db.Add(season);
                _db.Add(new DefaultSeason { Name = Defaults.DefaultSeason, Season = season });

                await _db.SaveChangesAsync();
            }
        }

        public async Task CreateDefaultImage(string name, string source)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (string.IsNullOrEmpty(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!await _db.DefaultImages.AnyAsync(x => x.Name == name))
            {
                var image = new Image
                {
                    Source = source
                };

                _db.Add(image);
                _db.Add(new DefaultImages { Image = image, Name = name });

                await _db.SaveChangesAsync();
            }
        }

        public async Task CreateAdministrator(string username, string email, string battletag, string battlenetUserId)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            if (string.IsNullOrEmpty(battletag))
            {
                throw new ArgumentNullException(nameof(battletag));
            }
            if (string.IsNullOrEmpty(battlenetUserId))
            {
                throw new ArgumentNullException(nameof(battlenetUserId));
            }

            // Make sure the admin role exists
            if (await _roleManager.FindByNameAsync("Admin") == null)
            {
                var r = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                if (!r.Succeeded)
                {
                    throw new InvalidProgramException("Failed to create admin role");
                }
            }

            // first look for the user
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                await CreatePlayerForUserAsync(user);
                var logins = await _userManager.GetLoginsAsync(user);
                if (!logins.Any(x => x.LoginProvider == "Battlenet" && x.ProviderKey == battlenetUserId))
                {
                    var r = await _userManager.AddLoginAsync(user, new UserLoginInfo("Battlenet", battlenetUserId, null));

                    if (!r.Succeeded)
                    {
                        throw new InvalidProgramException("Failed to associate login with existing admin user.");
                    }
                }

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return;
                }
                else
                {
                    var r = await _userManager.AddToRoleAsync(user, "Admin");
                    if (r.Succeeded)
                    {
                        return;
                    }
                    else
                    {
                        throw new InvalidProgramException("Failed to add user to admin role");
                    }
                }
            }

            var defaultImage = await _db.DefaultImages
                .Include(x => x.Image)
                .SingleOrDefaultAsync(x => x.Name == Defaults.ProfileImage);
            var image = defaultImage != null ? defaultImage.Image : null;

            user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                Battletag = battletag,
                ProfileImage = image
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await CreatePlayerForUserAsync(user);
                result = await _userManager.AddLoginAsync(user, new UserLoginInfo("Battlenet", battlenetUserId, null));
            }
            else
            {
                throw new InvalidProgramException("Failed to create administrator");
            }

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                throw new InvalidProgramException("Failed to add login to user");
            }

            if (result.Succeeded)
            {
                return;
            }
            else
            {
                throw new InvalidProgramException("Failed to add user to admin role");
            }
        }

        private async Task CreatePlayerForUserAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Make sure the user has a player entity
            var player = await _db.Players
                .Include(x => x.User)
                .SingleOrDefaultAsync(x => x.User.Id == user.Id);
            if (player == null)
            {
                var rating = new Rating { MatchMakingRating = 1500 };
                _db.Add(rating);

                // Setup default rank
                var rank = new Rank { Rating = rating };
                var defaultSeason = await _db.DefaultSeasons
                    .Include(x => x.Season)
                    .FirstOrDefaultAsync();
                if (defaultSeason != null)
                {
                    var season = await _db.Seasons
                        .Include(x => x.Brackets)
                        .SingleOrDefaultAsync(x => x.SeasonId == defaultSeason.Season.SeasonId);
                    if (season != null && season.Brackets.Any(x => x.Type == RankBrackets.NeverLucky))
                    {
                        rank.RankBracket = season.Brackets.Single(x => x.Type == RankBrackets.NeverLucky);
                    }
                }

                _db.Add(rank);

                player = new Player
                {
                    User = user,
                    Rank = rank
                };

                _db.Add(player);
                await _db.SaveChangesAsync();
            }
        }
    }
}
