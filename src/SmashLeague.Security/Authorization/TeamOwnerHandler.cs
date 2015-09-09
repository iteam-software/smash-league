using Microsoft.AspNet.Authorization;
using System;
using System.Linq;

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
                    string.Equals(c.Type, AuthorizationDefaults.ClaimTypeTeamOwner, StringComparison.OrdinalIgnoreCase) &&
                   !string.IsNullOrEmpty(c.Value) &&
                    string.Equals(c.Value, normalizedName, StringComparison.OrdinalIgnoreCase));

                if (found)
                {
                    x.Succeed(req);
                }
                else
                {
                    x.Fail();
                }
            }
        };
    }
}
