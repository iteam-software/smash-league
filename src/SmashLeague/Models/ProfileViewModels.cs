using SmashLeague.Data;
using System;

namespace SmashLeague.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string ProfileImageSrc { get; set; }
        public string BannerImageSrc { get; set; }
        public string ProfileImageEditData { get; set; }
        public string BannerImageEditData { get; set; }
        public DateTime? Birthday { get; set; }
        public PlayerRoles? PreferredRoles { get; set; }
        public bool LookingForTeam { get; private set; }

        public static implicit operator Profile(ApplicationUser user)
        {
            var profile = new Profile
            {
                Username = user.UserName,
                Location = user.Location,
                Birthday = user.Birthday,
                Name = user.Name
            };

            if (user.HeaderImage != null)
            {
                profile.BannerImageSrc = user.HeaderImage.Source;
            }

            if (user.ProfileImage != null)
            {
                profile.ProfileImageSrc = user.ProfileImage.Source;
            }

            return profile;
        }

        public static implicit operator Profile(Player player)
        {
            if (player.User == null)
            {
                throw new ArgumentException("Player must have a loaded user to implicitly convert to profile");
            }

            Profile profile = player.User;

            profile.PreferredRoles = player.PreferredRole;
            profile.LookingForTeam = player.LookingForTeam;

            return profile;
        }
    }
}
