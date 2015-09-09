namespace SmashLeague.Security.Authorization
{
    public static class AuthorizationDefaults
    {
        // Authorization Policies
        public const string PolicyTeamOwner = "policy:smashleague:teamowner";

        // Claim Types
        public static string ClaimTypeTeamOwner { get; } = "urn:smashleague:teamowner";

        public static string[] RequiredSchemes { get; } = new[]
        {
            Battlenet.BattlenetAuthenticationDefaults.AuthenticationScheme
        };
    }
}
