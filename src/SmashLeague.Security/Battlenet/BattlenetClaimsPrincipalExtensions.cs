using System.Security.Claims;

namespace SmashLeague.Security.Battlenet
{
    public static class BattlenetClaimsPrincipalExtensions
    {
        public static string GetBattletag(this ClaimsPrincipal principal)
        {
            return principal.HasClaim(claim => claim.Type == BattlenetAuthenticationDefaults.BattletagClaimType) 
                ? principal.FindFirst(BattlenetAuthenticationDefaults.BattletagClaimType).Value 
                : null;
        }
    }
}
