using Microsoft.Framework.DependencyInjection;

namespace SmashLeague.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmashLeagueServices(this IServiceCollection collection)
        {
            collection.AddTransient<IMatchManager, MatchManager>();

            return collection;
        }
    }
}
