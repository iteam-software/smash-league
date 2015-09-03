using Microsoft.AspNet.Http;
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

            return await _db.Users.Include(x => x.ProfileImage).Include(x => x.HeaderImage).FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public override async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var profileImage = user.ProfileImage;
            var headerImage = user.HeaderImage;

            var result = await base.UpdateAsync(user);            

            if (result.Succeeded && profileImage != null && profileImage != user.ProfileImage)
            {
                user.ProfileImage = profileImage;
                _db.Images.Attach(profileImage);
                _db.Users.Attach(user);

                user.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Update(user);

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    result = IdentityResult.Failed(new IdentityError { Code = nameof(e), Description = e.Message });
                }
            }

            if (result.Succeeded && headerImage != null && headerImage != user.HeaderImage)
            {
                user.HeaderImage = headerImage;
                _db.Images.Attach(headerImage);
                _db.Users.Attach(user);

                user.ConcurrencyStamp = Guid.NewGuid().ToString();

                _db.Update(user);

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    result = IdentityResult.Failed(new IdentityError { Code = nameof(e), Description = e.Message });
                }
            }

            return result;
        }
    }
}
