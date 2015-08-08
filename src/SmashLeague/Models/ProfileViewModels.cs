using SmashLeague.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string Location { get; set; }

        public byte[] ProfileImage { get; set; }
        public byte[] HeaderImage { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public DateTime? Birthday { get; set; }

        public static implicit operator Profile(ApplicationUser user)
        {
            return new Profile
            {
                Username = user.UserName,
                Location = user.Location,
                ProfileImage = user.ProfileImage,
                HeaderImage = user.HeaderImage,
                Birthday = user.Birthday,
                First = user.First,
                Last = user.Last
            };
        }
    }
}
