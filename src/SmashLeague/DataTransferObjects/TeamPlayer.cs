namespace SmashLeague.DataTransferObjects
{
    public class TeamPlayer : Player
    {
        public TeamPlayer(Player player)
        {
            if (player != null)
            {
                PlayerId = player.PlayerId;
                Username = player.Username;
                PreferredRoles = player.PreferredRoles;
                ProfileImageSrc = player.ProfileImageSrc;
                BannerImageSrc = player.BannerImageSrc;
                Tag = player.Tag;
            }
        }

        public bool Invitee { get; set; }
    }
}
