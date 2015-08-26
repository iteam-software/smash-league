
module SmashLeague.Teams {
  'use strict';

  export class TeamController {

    public static $inject = [
      '$state'
    ];

    constructor(
      state: ng.ui.IStateService) {
      
    }
  }

  Application.Module.controller('TeamController', TeamController);
}