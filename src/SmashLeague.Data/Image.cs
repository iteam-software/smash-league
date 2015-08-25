using System;
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
        public string Source { get; set; }

        // Helpers
        private static readonly Regex PureDataUriRx = new Regex("^data:image/[a-z]{3,5};base64,.*$");

        public static string GetTypeFromDataUri(string uri)
        {
            uri = StripDataColon(uri);

            var parts = uri.Split(';');
            var mimeType = parts[0];

            return mimeType.Split('/')[1];
        }

        public static string GetBase64StringFromDataUri(string uri)
        {
            uri = StripDataColon(uri);

            var parts = uri.Split(';');

            return parts[1].Split(',')[1];
        }

        private static string StripDataColon(string uri)
        {
            EnsureValidDataUri(uri);

            return uri.Substring(uri.IndexOf(':') + 1, uri.Length - 5);
        }

        private static void EnsureValidDataUri(string uri)
        {
            if (!PureDataUriRx.IsMatch(uri))
            {
                throw new InvalidOperationException("uri is not an image data uri");
            }
        }

        private static Image BuildImageFromDataUriParts(string[] parts)
        {
            var image = new Image();

            // Find beginning of the mimetype
            var mimeBegin = parts[0].IndexOf("image");

            // Set image values
            image.Source = parts[1];

            return image;
        }
    }
}
