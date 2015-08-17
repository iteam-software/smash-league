using SmashLeague.DataTransferObjects;
using System;

namespace SmashLeague.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string Location { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public DateTime? Birthday { get; set; }
        public Image Image { get; set; }
        public Image Banner { get; set; }

        public static implicit operator Profile(Data.ApplicationUser user)
        {
            var profile = new Profile
            {
                Username = user.UserName,
                Location = user.Location,
                Birthday = user.Birthday,
                First = user.First,
                Last = user.Last
            };

            if (user.ProfileImage != null)
            {

                profile.Image = new Image
                {
                    Src = user.ProfileImage.MimeType == "text/url"
                        ? user.ProfileImage.Data
                        : $"data:{user.ProfileImage.MimeType};base64,{user.ProfileImage.Data}"
                };
            }

            if (user.HeaderImage != null)
            {
                profile.Banner = new Image
                {
                    Src = user.HeaderImage.MimeType == "text/url"
                        ? user.HeaderImage.Data
                        : $"data:{user.HeaderImage.MimeType};base64,{user.HeaderImage.Data}"
                };
            }

            return profile;
        }
    }
}
