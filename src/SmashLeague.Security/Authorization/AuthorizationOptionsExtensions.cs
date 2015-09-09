using Microsoft.AspNet.Authorization;
using System;

namespace SmashLeague.Security.Authorization
{
    public static class AuthorizationOptionsExtensions
    {
        public static void AddTeamOwnerPolicy(this AuthorizationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            // build team ownership policy
            var ownershipRequirements = new IAuthorizationRequirement[]
            {
                    new DenyAnonymousAuthorizationRequirement(),
                    new TeamOwnerRequirement()
            };

            var ownershipPolicy = new AuthorizationPolicy(ownershipRequirements, new string[0]);

            options.AddPolicy(AuthorizationDefaults.PolicyTeamOwner, ownershipPolicy);
        }
    }
}
