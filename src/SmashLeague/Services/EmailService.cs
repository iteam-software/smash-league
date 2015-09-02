using Microsoft.Framework.Logging;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger _logger;

        public EmailService(
            EmailServiceOptions options,
            ILoggerFactory factory)
        {
            _logger = factory.CreateLogger(nameof(EmailService));
        }

        public async Task<EmailResult> SendAsync(string sendTo, string message)
        {
            var errorMsg = $"Mail service not fully implemented. Unable to send message to {sendTo}.";

            _logger.LogWarning(errorMsg);

            return await Task.Run(() => EmailResult.Failed(new EmailError[] { new EmailError { Message = errorMsg } }));
        }
    }
}
