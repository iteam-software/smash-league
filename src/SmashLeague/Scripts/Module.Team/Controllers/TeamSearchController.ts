
module SmashLeague.Teams {
  'use strict';

  export class TeamSearchController {

    private _teamService: TeamService;
    private _scope: ITeamScope;

    public static $inject = [
      'TeamService',
      'query',
      '$scope'
    ];

    constructor(
      teamService,
      query,
      scope) {

      this._teamService = teamService;
      this._scope = scope;
      this._scope.Query = query;

      this._teamService.SearchForTeams(query)
        .success((teams) => this._scope.SearchResults = teams);
    }
  }

  Application.Module.controller('TeamSearchController', TeamSearchController);
}