using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IEmailService
    {
        Task<EmailResult> SendAsync(string sendTo, string message);
    }
}
