using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmashLeague.Data.Extensions
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly SmashLeagueDbContext _db;

        public ApplicationUserManager(
            IUserStore<ApplicationUser> user,
            IOptions<IdentityOptions> options,
            IPasswordHasher<ApplicationUser> hasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IEnumerable<IUserTokenProvider<ApplicationUser>> tokenProviders,
            ILogger<UserManager<ApplicationUser>> logger,
            IHttpContextAccessor contextAccessor,
            SmashLeagueDbContext db) : base(user, options, hasher, userValidators, passwordValidators, keyNormalizer, errors, tokenProviders, logger, contextAccessor)
        {
            _db = db;
        }

        public override async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }

            return await _db.Users.Include(x => x.ProfileImage).FirstOrDefaultAsync(x => x.UserName == userName);
        }
    }
}
