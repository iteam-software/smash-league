using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Runtime;
using SmashLeague.Authentication.Battlenet;

namespace SmashLeague
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddUserSecrets()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Congfigure logging
            services.AddLogging();

            // Configure battlenet authentication
            services.ConfigureBattlenetAuthentication(options =>
            {
                options.CallbackPath = new PathString("/auth/signin-battlenet");
                options.ClientId = Configuration["Battlenet:ClientId"];
                options.ClientSecret = Configuration["Battlenet:ClientSecret"];
            });

            // Add Mvc services
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Using battlenet authentication
            app.UseBattlenetAuthentication();

            // Setup logging
            loggerFactory.AddConsole();

            app.UseMvc();
        }
    }
}
