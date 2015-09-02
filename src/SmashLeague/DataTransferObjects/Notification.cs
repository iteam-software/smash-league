using System;

namespace SmashLeague.DataTransferObjects
{
    public class Notification
    {
        public string Message { get; private set; }
        public int NotificationId { get; private set; }
        public bool Read { get; private set; }
        public string Title { get; private set; }
        public string Username { get; private set; }

        public static implicit operator Notification(Data.Notification entity)
        {
            if (entity.User == null)
            {
                throw new ArgumentException(nameof(entity.User));
            }

            return new Notification
            {
                NotificationId = entity.NotificationId,
                Title = entity.Title,
                Message = entity.Message,
                Username = entity.User.UserName,
                Read = entity.Read
            };
        }
    }
}
