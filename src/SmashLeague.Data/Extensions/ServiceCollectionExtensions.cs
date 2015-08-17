using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;
using SmashLeague.Data.Extensions;
using System;

namespace SmashLeague.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmashLeagueData(
            this IServiceCollection services,
            Action<SmashLeagueDataOptions> setup)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setup == null)
            {
                throw new ArgumentNullException(nameof(setup));
            }

            var dataOptions = new SmashLeagueDataOptions();
            setup(dataOptions);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<SmashLeagueDbContext>(options =>
                {
                    options.UseSqlServer(dataOptions.ConnectionString);
                });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SmashLeagueDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ApplicationUserManager>();

            return services;
        }
    }
}
