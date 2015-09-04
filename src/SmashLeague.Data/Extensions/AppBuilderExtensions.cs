using Microsoft.AspNet.Builder;
using Microsoft.Framework.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Data.Extensions
{
    public static class AppBuilderExtensions
    {
        public static async Task<IApplicationBuilder> UseSmashLeagueData(this IApplicationBuilder app, IConfiguration config)
        {
            var initializer = (SmashLeagueDbInitializer)app.ApplicationServices.GetService(typeof(SmashLeagueDbInitializer));

            // Create images
            await initializer.CreateDefaultImage(Defaults.ProfileImage, config["Defaults:Images:Profile:Source"]);
            await initializer.CreateDefaultImage(Defaults.TeamImage, config["Defaults:Images:Team:Source"]);

            // Create default season
            await initializer.CreateDefaultSeason(config["Defaults:Season:SeasonName"]);

            // Create administrator
            await initializer.CreateAdministrator(config["Admin:Username"], config["Admin:Email"], config["Admin:Battletag"], config["Admin:BattlenetUserId"]);

            return app;
        }
    }
}
