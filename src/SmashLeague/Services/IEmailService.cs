using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public interface IEmailService
    {
        Task<EmailResult> Send(string sendTo, string message);
    }
}
