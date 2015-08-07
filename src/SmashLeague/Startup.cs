using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using SmashLeague.Authentication.Battlenet;
using SmashLeague.Data;
using SmashLeague.Services;

namespace SmashLeague
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddUserSecrets()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add Smash League data
            services.AddSmashLeagueData(options =>
            {
                options.ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            });

            // Congfigure logging
            services.AddLogging();

            // Configure battlenet authentication
            services.ConfigureBattlenetAuthentication(options =>
            {
                options.CallbackPath = new PathString("/auth/signin-battlenet");
                options.ClientId = Configuration["Battlenet:ClientId"];
                options.ClientSecret = Configuration["Battlenet:ClientSecret"];
            });

            // Configure cookie authentication
            services.ConfigureCookieAuthentication(options =>
            {
                options.LoginPath = null;
            });

            // Configure identity authentication
            services.ConfigureIdentity(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = BattlenetAuthenticationDefaults.BattletagClaimType;
                options.User.UserNameValidationRegex = BattlenetAuthenticationDefaults.BattletagRegex;
            });

            // Add Mvc services
            services.AddMvc();

            // Add application services
            services.AddSmashLeagueServices();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Setup logging
            loggerFactory.AddConsole();

            app.UseStaticFiles();

            // Using Identity
            app.UseIdentity();

            // Using battlenet authentication
            app.UseBattlenetAuthentication();

            app.UseMvc();
        }
    }
}
