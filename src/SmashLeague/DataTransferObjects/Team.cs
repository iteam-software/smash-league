using System;
using System.Collections.Generic;

namespace SmashLeague.DataTransferObjects
{
    public class Team
    {
        public TeamPlayer[] Roster { get; set; }
        public Player Owner { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string TeamImageSrc { get; set; }
        public string HeaderImageSrc { get; private set; }
        public Data.RankBrackets Bracket { get; set; }
        public int Rating { get; set; }

        public static implicit operator Team(Data.Team entity)
        {
            if (entity == null)
            {
                return null;
            }

            var roster = new List<TeamPlayer>();
            var team = new Team { Name = entity.Name, Owner = entity.Owner, NormalizedName = entity.NormalizedName };

            if (entity.TeamImage != null)
            {
                team.TeamImageSrc = entity.TeamImage.Source;
            }

            if (entity.HeaderImage != null)
            {
                team.HeaderImageSrc = entity.HeaderImage.Source;
            }

            if (entity.Rank != null && entity.Rank.RankBracket != null)
            {
                team.Bracket = entity.Rank.RankBracket.Type;

                if (entity.Rank.Rating != null)
                {
                    team.Rating = entity.Rank.Rating.MatchMakingRating;
                }
            }

            if (entity.Members != null)
            {
                foreach (var member in entity.Members)
                {
                    roster.Add(new TeamPlayer(member.Player) { Invitee = false });
                }
            }

            if (entity.Invitees != null)
            {
                foreach (var invitee in entity.Invitees)
                {
                    roster.Add(new TeamPlayer(invitee.Player) { Invitee = true });
                }
            }

            team.Roster = roster.ToArray();

            return team;
        }
    }
}
