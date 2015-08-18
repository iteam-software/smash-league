using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmashLeague.Data
{
    public class Image
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileImageId { get; set; }

        [Required]
        public string MimeType { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
