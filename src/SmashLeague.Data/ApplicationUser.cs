﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Image ProfileImage { get; set; }
        public Image HeaderImage { get; set; }

        [Required]
        public string Battletag { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime? Birthday { get; set; }
        public bool EmailNotifications { get; set; }

        public ICollection<Notification> Notifications { get; set; }
    }
}
