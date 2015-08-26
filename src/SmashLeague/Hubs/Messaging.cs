using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;

namespace SmashLeague.Hubs
{
    public class Messaging : Hub
    {
        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).sendMessage(message).Wait();
        }
    }
}
