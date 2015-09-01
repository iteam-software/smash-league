using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System;

namespace SmashLeague.Services
{
    public static class EmailServiceExtensions
    {
        public static IServiceCollection AddEmailService(this IServiceCollection collection, Action<EmailServiceOptions> config = null)
        {
            var option = new EmailServiceOptions();

            if (config != null)
            {
                config(option);
            }

            collection.AddTransient<IEmailService, EmailService>(x => new EmailService(option, x.GetRequiredService<ILoggerFactory>()));

            return collection;
        }
    }
}
