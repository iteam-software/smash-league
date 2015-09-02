using System;
using System.Collections.Generic;

namespace SmashLeague.DataTransferObjects
{
    public class Team
    {
        public TeamPlayer[] Roster { get; set; }
        public Player Owner { get; set; }
        public string Name { get; set; }

        public static implicit operator Team(Data.Team entity)
        {
            if (entity == null)
            {
                return null;
            }

            if (entity.Members == null)
            {
                throw new ArgumentNullException(nameof(entity.Members));
            }

            if (entity.Invitees == null)
            {
                throw new ArgumentNullException(nameof(entity.Members));
            }

            var roster = new List<TeamPlayer>();
            var team = new Team { Name = entity.Name, Owner = entity.Owner };
            foreach (var member in entity.Members)
            {
                roster.Add(new TeamPlayer(member.Player) { Invitee = false });
            }
            foreach (var invitee in entity.Invitees)
            {
                roster.Add(new TeamPlayer(invitee.Player) { Invitee = true });
            }

            team.Roster = roster.ToArray();

            return team;
        }
    }
}
