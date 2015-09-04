using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using SmashLeague.Data;
using SmashLeague.Data.Extensions;
using SmashLeague.Security.Battlenet;
using SmashLeague.Services;

namespace SmashLeague
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddJsonFile("initial-data.json")
                .AddUserSecrets()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment Environment { get; set; }

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

            // Add Mvc services
            services.AddMvc();

            // Add email services
            services.AddEmailService();

            // Add application services
            services.AddSmashLeagueServices(Environment);
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

            // Use SignalR
            //app.UseSignalR();

            // Using Mvc
            app.UseMvc();

            // Data initialization
            app.UseSmashLeagueData(Configuration).Wait();
        }
    }
}
