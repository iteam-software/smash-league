using Microsoft.Framework.DependencyInjection;
using System;

namespace SmashLeague.Security.Authorization
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmashLeagueAuthorization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<TeamOwnerRequirement>();

            var provider = services.BuildServiceProvider();

            services.AddAuthorization(x =>
            {
                x.AddTeamOwnerPolicy(provider);
            });

            return services;
        }
    }
}
