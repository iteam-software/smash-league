using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using System;

namespace SmashLeague.Security.Battlenet
{
    public static class BattlenetServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureBattlenetAuthentication(
            this IServiceCollection services,
            Action<BattlenetAuthenticationOptions> configure)
        {
            return services.ConfigureBattlenetAuthentication(configure, string.Empty);
        }

        public static IServiceCollection ConfigureBattlenetAuthentication(
            this IServiceCollection services,
            IConfiguration config,
            string optionsName)
        {
            return services.Configure<BattlenetAuthenticationOptions>(config, optionsName);
        }

        public static IServiceCollection ConfigureBattlenetAuthentication(
            this IServiceCollection services,
            Action<BattlenetAuthenticationOptions> configure,
            string optionsName)
        {
            return services.Configure(configure, optionsName);
        }
    }
}
