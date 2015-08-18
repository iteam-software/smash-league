
namespace SmashLeague.DataTransferObjects
{
    public class Player
    {
        public Image Image { get; set; }
        public Image Banner { get; set; }
        public string Username { get; set; }

        public static implicit operator Player(Data.Player entity)
        {
            if (entity == null)
            {
                return null;
            }

            var player = new Player { Username = entity.User.UserName };

            if (entity.User.ProfileImage != null)
            {

                player.Image = new Image
                {
                    Src = entity.User.ProfileImage.MimeType == "text/url"
                        ? entity.User.ProfileImage.Data
                        : $"data:{entity.User.ProfileImage.MimeType};base64,{entity.User.ProfileImage.Data}"
                };
            }

            if (entity.User.HeaderImage != null)
            {
                player.Banner = new Image
                {
                    Src = entity.User.HeaderImage.MimeType == "text/url"
                        ? entity.User.HeaderImage.Data
                        : $"data:{entity.User.HeaderImage.MimeType};base64,{entity.User.HeaderImage.Data}"
                };
            }

            return player;
        }
    }
}
