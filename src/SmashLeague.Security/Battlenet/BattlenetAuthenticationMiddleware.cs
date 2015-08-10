using Microsoft.AspNet.Authentication;
using Microsoft.AspNet.Authentication.OAuth;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.DataProtection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;
using Microsoft.Framework.WebEncoders;

namespace SmashLeague.Security.Battlenet
{
    public class BattlenetAuthenticationMiddleware : OAuthAuthenticationMiddleware<BattlenetAuthenticationOptions>
    {
        public BattlenetAuthenticationMiddleware(
            RequestDelegate next, 
            IDataProtectionProvider dataProtectionProvider, 
            ILoggerFactory loggerFactory, 
            IUrlEncoder encoder, 
            IOptions<ExternalAuthenticationOptions> externalOptions, 
            IOptions<BattlenetAuthenticationOptions> options,
            ConfigureOptions<BattlenetAuthenticationOptions> configureOptions = null) 
            : base(next, dataProtectionProvider, loggerFactory, encoder, externalOptions, options, configureOptions)
        {
        }

        protected override AuthenticationHandler<BattlenetAuthenticationOptions> CreateHandler()
        {
            return new BattlenetAuthenticationHandler(Backchannel);
        }
    }
}
