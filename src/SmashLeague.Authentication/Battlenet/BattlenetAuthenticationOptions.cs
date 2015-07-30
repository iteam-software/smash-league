using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Http;

namespace SmashLeague.Authentication.Battlenet
{
	public class BattlenetAuthenticationOptions : OAuthAuthenticationOptions
	{
		public BattlenetAuthenticationOptions() 
		{
			AuthenticationScheme = BattlenetAuthenticationDefaults.AuthenticationScheme;
			Caption = AuthenticationScheme;
			CallbackPath = new PathString("/signin-battlenet");
			AuthorizationEndpoint = BattlenetAuthenticationDefaults.AuthorizationEndpoint;
			TokenEndpoint = BattlenetAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = BattlenetAuthenticationDefaults.UserInformationEndpoint;
            SaveTokensAsClaims = true;
		}
	}	
}