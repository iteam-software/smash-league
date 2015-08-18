
module SmashLeague.Teams {
  'use strict';

  export function Unselected() {
    return (players: any[], roster: any[]) => {
      var list = [];

      if (!players || !roster) {
        return list;
      }

      players.forEach((player, index) => {
        var match = false;

        roster.forEach((member, jndex) => {
          match = match || (member && member.Username && member.Username == player.Username);
        });

        if (!match) {
          list.push(player);
        }
      });

      return list;
    }
  }

  Application.Module.filter('unselected', Unselected);
}