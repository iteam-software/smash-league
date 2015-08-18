
namespace SmashLeague.Security.Battlenet 
{
	public static class BattlenetAuthenticationDefaults
	{
		public const string AuthenticationScheme = "Battlenet";
		
		public const string AuthorizationEndpoint = "https://us.battle.net/oauth/authorize";
		
		public const string TokenEndpoint = "https://us.battle.net/oauth/token";

        public const string UserInformationEndpoint = "https://us.api.battle.net/account/user";

        public const string BattletagClaimType = "urn:battlenet:battletag";

        public const string BattletagRegex = "^[a-zA-Z]{3,16}#[0-9]{4}$";
    }
}