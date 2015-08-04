using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Player
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        public ICollection<Rank> Rankings { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}
