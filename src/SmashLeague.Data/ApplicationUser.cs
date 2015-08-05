using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace SmashLeague.Data
{
    public class ApplicationUser : IdentityUser
    {
        public byte[] HeaderImage { get; set; }
        public byte[] ProfileImage { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Location { get; set; }
        public DateTime? Birthday { get; set; }
    }
}
