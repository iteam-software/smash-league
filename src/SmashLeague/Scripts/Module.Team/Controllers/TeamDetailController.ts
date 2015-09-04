
module SmashLeague.Teams {
  'use strict';

  export class TeamDetailController {

    private _teamService: TeamService;
    private _scope: ITeamDetailScope;

    public static $inject = [
      'normalizedName',
      'TeamService',
      '$scope'
    ];

    constructor(
      name,
      teamService,
      scope) {

      this._scope = scope;
      this._teamService = teamService;

      this._teamService.GetTeam(name)
        .success((team) => this._scope.Team = team);
    }

  }

  Application.Module.controller('TeamDetailController', TeamDetailController);
}