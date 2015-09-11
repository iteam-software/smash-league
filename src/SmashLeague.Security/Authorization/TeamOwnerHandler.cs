using Microsoft.AspNet.Authorization;
using System;
using System.Linq;
using System.Security.Claims;

namespace SmashLeague.Security.Authorization
{
    public class TeamOwnerRequirement : DelegateRequirement
    {
        public TeamOwnerRequirement()
            : base(DelegateHandler)
        {
        }

        protected static Action<AuthorizationContext, DelegateRequirement> DelegateHandler = (x, req) =>
        {
            var context = x.Resource as Microsoft.AspNet.Mvc.AuthorizationContext;
            var requirement = req as TeamOwnerRequirement;
            if (requirement == null)
            {
                throw new InvalidCastException("Unable to cast requirement to TeamOwnerRequirement");
            }

            object routeData;
            if (x.User != null && context != null && context.RouteData.Values.TryGetValue("normalizedName", out routeData))
            {
                var normalizedName = routeData as string;
                if (string.IsNullOrEmpty(normalizedName))
                {
                    throw new InvalidOperationException("normalizedName not found on the authorized route.");
                }

                // The current user must own a claim to this normal name
                var found = x.User.Claims.Any(c => 
                    string.Equals(c.Type, $"{AuthorizationDefaults.ClaimTypeTeamOwner}:{normalizedName}", StringComparison.OrdinalIgnoreCase) &&
                   !string.IsNullOrEmpty(c.Value) &&
                    string.Equals(c.Value, x.User.GetUserName(), StringComparison.OrdinalIgnoreCase));

                if (found)
                {
                    x.Succeed(requirement);
                }
                else
                {
                    x.Fail();
                }
            }
        };
    }
}
