
module SmashLeague.Teams {
  'use strict';

  export function Not() {
    return (array: any[], player: any) => {
      var list = [];

      if (!array) {
        return list;
      }

      if (!player) {
        return array;
      }

      array.forEach((o) => {
        if (o.Username != player.Username) {
          list.push(o);
        }
      });

      return list;
    }
  }

  Application.Module.filter('not', Not);
}