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

        public static implicit operator Profile(Data.ApplicationUser user)
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
    }
}
