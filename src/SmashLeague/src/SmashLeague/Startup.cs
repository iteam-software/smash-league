using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using SmashLeague.OAuth.Battlenet;

namespace SmashLeague
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            // TODO: configure user secrets and set the client id and client secret
            app.UseBattlenetAuthentication();
        }
    }
}
