using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmashLeague.Data
{
    public class RankedUser : ApplicationUser
    {
        [Required]
        public Rank Rank { get; set; }
    }
}
