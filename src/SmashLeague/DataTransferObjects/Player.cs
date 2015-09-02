using SmashLeague.Data;
using System;

namespace SmashLeague.DataTransferObjects
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string ProfileImageSrc { get; set; }
        public string BannerImageSrc { get; set; }
        public string Username { get; set; }
        public PlayerRoles? PreferredRoles { get; set; }
        public string Tag { get; set; }

        public static implicit operator Player(Data.Player entity)
        {
            if (entity == null)
            {
                return null;
            }

            var player = new Player();
            if (entity.User != null)
            {
                player.Username = entity.User.UserName;
                if (entity.User.ProfileImage != null)
                {
                    player.ProfileImageSrc = entity.User.ProfileImage.Source;
                }

                if (entity.User.HeaderImage != null)
                {
                    player.BannerImageSrc = entity.User.HeaderImage.Source;
                }
            }

            player.PreferredRoles = entity.PreferredRole;
            player.Tag = entity.Tag;
            player.PlayerId = entity.PlayerId;

            return player;
        }

        public static implicit operator Player(Data.TeamOwner entity)
        {
            if (entity == null)
            {
                return null;
            }

            if (entity.Player == null)
            {
                throw new ArgumentNullException(nameof(entity.Player));
            }

            return entity.Player;
        }
    }
}
