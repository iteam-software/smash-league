using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using SmashLeague.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Services
{
    public class NotificationManager : INotificationManager
    {
        private const string TeamInviteTitle = "Team Invite!";
        private const string TeamInviteMessage = "You have been invited to {0}, please visit your team page to accept or decline the invite!";

        private readonly SmashLeagueDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public NotificationManager(
            SmashLeagueDbContext db,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService,
            ILoggerFactory loggerFactory)
        {
            _db = db;
            _userManager = userManager;
            _emailService = emailService;
            _logger = loggerFactory.CreateLogger(nameof(NotificationManager));
        }

        public async Task DeleteAsync(int id)
        {
            var note = _db.Notifications
                .SingleOrDefault(x => x.NotificationId == id);

            if (note != null)
            {
                _db.Remove(note);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Notification[]> GetForUserAsync(string username)
        {
            var user = await EnsureGetUserAsync(username);

            return await _db.Notifications
                .Include(x => x.User == user)
                .ToArrayAsync();
        }

        public async Task<Notification[]> GetReadForUserAsync(string username)
        {
            var user = await EnsureGetUserAsync(username);

            return await _db.Notifications
                .Include(x => x.User == user)
                .Where(x => x.Read)
                .ToArrayAsync();
        }

        public async Task<Notification[]> GetUnreadForUserAsync(string username)
        {
            var user = await EnsureGetUserAsync(username);

            return await _db.Notifications
                .Include(x => x.User == user)
                .Where(x => !x.Read)
                .ToArrayAsync();
        }

        public async Task NotifyTeamInvite(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team));
            }
            if (team.Invitees == null)
            {
                throw new ArgumentNullException(nameof(team.Invitees));
            }

            foreach (var invitee in team.Invitees)
            {
                if (invitee.Player != null && invitee.Player.User != null)
                {
                    var notification = new Notification
                    {
                        Message = string.Format(TeamInviteMessage, team.Name),
                        Title = TeamInviteTitle,
                        User = invitee.Player.User
                    };

                    try
                    {
                        _db.Add(notification);
                        await _db.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        _logger.LogWarning($"Failed to notify {invitee.Player.User.UserName} of team invite to {team.Name}", e);
                    }

                    if (invitee.Player.User.EmailNotifications)
                    {
                        await _emailService.SendAsync(invitee.Player.User.Email, TeamInviteMessage);
                    }
                }
            }
        }

        public async Task<Notification> ReadAsync(DataTransferObjects.Notification note, string username)
        {
            var user = await EnsureGetUserAsync(username);
            var entity = await _db.Notifications.SingleOrDefaultAsync(x => x.NotificationId == note.NotificationId);
            if (entity != null)
            {
                entity.Read = true;
                _db.Update(entity);
                await _db.SaveChangesAsync();
            }

            return entity;
        }

        private async Task<ApplicationUser> EnsureGetUserAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new InvalidOperationException($"User not found for username {username}");
            }

            return user;
        }
    }
}
