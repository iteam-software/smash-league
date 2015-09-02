
module SmashLeague.Teams {
  'use strict';

  export class TeamController {

    public static $inject = [
      'TeamsService'
    ];

    constructor(
      teamsService) {
      
    }
  }

  Application.Module.controller('TeamController', TeamController);
}