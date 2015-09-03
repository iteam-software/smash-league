
module SmashLeague.Teams {
  'use strict';

  export class TeamController {

    private _teamService: TeamService;
    private _http: ng.IHttpService;
    private _scope: ITeamScope;

    public static $inject = [
      'TeamService',
      '$http',
      '$scope'
    ];

    constructor(
      teamService,
      http,
      scope) {
      
      this._teamService = teamService;
      this._http = http;
      this._scope = scope;

      this._teamService.GetTopTeams(5)
        .success((teams) => this._scope.TopTeams = teams);
    }

  }

  Application.Module.controller('TeamController', TeamController);
}