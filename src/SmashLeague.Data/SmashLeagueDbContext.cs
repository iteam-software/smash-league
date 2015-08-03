using Microsoft.AspNet.Identity.EntityFramework;
using SmashLeague.Data.Security;

namespace SmashLeague.Data
{
    public class SmashLeagueDbContext : IdentityDbContext<ApplicationUser>
    {
        public SmashLeagueDbContext()
        {

        }
    }
}
