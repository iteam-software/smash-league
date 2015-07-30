using Microsoft.AspNet.Builder;
using Microsoft.Framework.OptionsModel;
using System;

namespace SmashLeague.OAuth.Battlenet
{
    public static class BattlenetAppBuilderExtensions
    {
        public static IApplicationBuilder UseBattlenetAuthentication(
            this IApplicationBuilder app,
            Action<BattlenetAuthenticationOptions> config = null,
            string optionsName = "")
        {
            return app.UseMiddleware<BattlenetAuthenticationMiddleware>(
                new ConfigureOptions<BattlenetAuthenticationOptions>(config ?? (o => { }))
                {
                    Name = optionsName
                });
        }
    }
}
