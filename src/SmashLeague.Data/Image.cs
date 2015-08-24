using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

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

        // Helpers
        public static Image FromDataUri(string uri)
        {
            // TODO: come up with a regex for base64 string
            var pureDataUriRx = new Regex("^data:image/[a-z]{3,5};base64,.*$");
            var urlWrappedDateUriRx = new Regex(@"^url\(data:image/[a-z]{3,5};base64,.*\)$");

            if (pureDataUriRx.IsMatch(uri))
            {
                // should give us ["data:image/{type};base64","{data}"]
                var parts = uri.Split(',');

                return BuildImageFromDataUriParts(parts); ;
            }
            else if (urlWrappedDateUriRx.IsMatch(uri))
            {
                var start = uri.IndexOf('(') + 1;
                var data = uri.Substring(start, uri.Length - 1 - start);
                var parts = data.Split(',');

                return BuildImageFromDataUriParts(parts);
            }
            else
            {
                return null;
            }
        }

        private static Image BuildImageFromDataUriParts(string[] parts)
        {
            var image = new Image();

            // Find beginning of the mimetype
            var mimeBegin = parts[0].IndexOf("image");

            // Set image values
            image.MimeType = parts[0].Substring(mimeBegin, parts[0].IndexOf(';') - mimeBegin);
            image.Data = parts[1];

            return image;
        }
    }
}
