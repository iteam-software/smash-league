using Microsoft.AspNet.Builder;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace SmashLeague.Data
{
    public class Startup
    {
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        public void Configure(IApplicationBuilder app)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSmashLeagueData(options =>
            {
                options.ConnectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            });
        }
    }
}
