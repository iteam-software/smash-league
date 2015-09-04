
module SmashLeague.Teams {
  'use strict';

  export class TeamSearchQueryController {

    public static $inject = [
      'query',
      '$scope'
    ];

    constructor(
      query: string,
      scope: ITeamScope) {

      scope.Query = query;
    }
  }

  Application.Module.controller('TeamSearchQueryController', TeamSearchQueryController);
}