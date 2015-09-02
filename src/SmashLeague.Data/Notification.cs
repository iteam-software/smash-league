using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        public bool Read { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public ApplicationUser User { get; set; }
    }
}
