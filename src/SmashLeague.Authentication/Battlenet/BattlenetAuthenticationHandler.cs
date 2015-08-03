using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.WebUtilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Features.Authentication;

namespace SmashLeague.Authentication.Battlenet
{
    internal class BattlenetAuthenticationHandler : OAuthAuthenticationHandler<BattlenetAuthenticationOptions>
    {
        public BattlenetAuthenticationHandler(HttpClient client) 
            : base(client)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            // Load the battlenet user info
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint + "?access_token=" + tokens.AccessToken);

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var notification = new OAuthAuthenticatedContext(Context, Options, Backchannel, tokens, payload)
            {
                Properties = properties,
                Principal = new ClaimsPrincipal(identity)
            };

            var id = BattlenetAuthenticationHelper.GetId(payload);
            if (!string.IsNullOrEmpty(id))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id, ClaimValueTypes.Integer, Options.ClaimsIssuer));
            }

            var battletag = BattlenetAuthenticationHelper.GetBattletag(payload);
            if (!string.IsNullOrEmpty(battletag))
            {
                identity.AddClaim(new Claim(BattlenetAuthenticationDefaults.BattletagClaimType, battletag, ClaimValueTypes.String, Options.ClaimsIssuer));
            }

            await Options.Notifications.Authenticated(notification);

            return new AuthenticationTicket(notification.Principal, notification.Properties, notification.Options.AuthenticationScheme);
        }

        protected override Task HandleSignInAsync(SignInContext context)
        {
            return base.HandleSignInAsync(context);
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var scope = FormatScope();

            var queryStrings = new Dictionary<string, string>(StringComparer.Ordinal);
            queryStrings.Add("client_id", Options.ClientId);
            queryStrings.Add("redirect_uri", redirectUri);
            queryStrings.Add("response_type", "code");

            AddQueryString(queryStrings, properties, "scope", scope);

            var state = Options.StateDataFormat.Protect(properties);
            queryStrings.Add("state", state);

            var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings);
            return authorizationEndpoint;
        }

        protected override string FormatScope()
        {
            return string.Join(" ", Options.Scope);
        }

        private static void AddQueryString(IDictionary<string, string> query, AuthenticationProperties properties, string name, string defaultValue = null)
        {
            string value;
            if (!properties.Items.TryGetValue(name, out value))
            {
                value = defaultValue;
            }
            else
            {
                properties.Items.Remove(name);
            }

            if (value == null)
            {
                return;
            }

            query[name] = value;
        }
    }
}
