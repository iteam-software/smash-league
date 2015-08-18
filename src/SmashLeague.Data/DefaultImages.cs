using System.ComponentModel.DataAnnotations;

namespace SmashLeague.Data
{
    public class DefaultImages
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public Image Image { get; set; }
    }
}
