using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmashLeague.Data;

namespace SmashLeague.Services
{
    public interface INotificationManager
    {
        Task<Notification[]> GetForUserAsync(string username);
        Task<Notification[]> GetUnreadForUserAsync(string username);
        Task<Notification[]> GetReadForUserAsync(string username);
        Task<Notification> ReadAsync(DataTransferObjects.Notification note, string username);
        Task DeleteAsync(int id);
        Task NotifyTeamInvite(Team team);
    }
}
