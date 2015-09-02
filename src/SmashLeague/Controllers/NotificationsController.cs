using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using SmashLeague.DataTransferObjects;
using SmashLeague.Services;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SmashLeague.Controllers
{
    [Authorize]
    [Route("api/notification")]
    public class NotificationsController : Controller
    {
        private readonly INotificationManager _notificationManager;

        public NotificationsController(
            INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;
        }

        [HttpGet]
        public async Task<Notification[]> Get()
        {
            var notifications = await _notificationManager.GetForUserAsync(User.GetUserName());

            return notifications.Select(x => (Notification)x).ToArray();
        }

        [HttpGet("unread")]
        public async Task<Notification[]> GetUnread()
        {
            var notifications = await _notificationManager.GetUnreadForUserAsync(User.GetUserName());

            return notifications.Select(x => (Notification)x).ToArray();
        }

        [HttpGet("read")]
        public async Task<Notification[]> GetRead()
        {
            var notifications = await _notificationManager.GetReadForUserAsync(User.GetUserName());

            return notifications.Select(x => (Notification)x).ToArray();
        }

        [HttpPut("read")]
        public async Task<Notification> Read(Notification note)
        {
            return await _notificationManager.ReadAsync(note, User.GetUserName());
        }

        [HttpDelete("{id")]
        public async Task Delete([FromQuery] int id)
        {
            await _notificationManager.DeleteAsync(id);
        }
    }
}
