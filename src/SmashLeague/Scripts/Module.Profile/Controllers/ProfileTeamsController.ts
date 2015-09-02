
module SmashLeague.Profile {
  'use strict';

  export class ProfileTeamsController {

    private _teamService: Teams.TeamService;
    private _scope: IProfileTeamScope;

    public static $inject = [
      'TeamService',
      '$scope'
    ];

    constructor(
      teamsService,
      scope) {

      this._teamService = teamsService;
      this._scope = scope;

      if (this._scope.Profile) {
        this.GetTeams(this._scope.Profile.Username);
      }

      this._scope.$watch('Profile', (newValue: any) => {
        if (newValue) {
          this.GetTeams(newValue.Username);
        }
      });
    }

    private GetTeams(
      username: string) {

      this._teamService.GetTeamsForPlayer(this._scope.Profile.Username)
        .success((teams) => this._scope.Teams = teams);      
    }
  }

  Application.Module.controller('ProfileTeamsController', ProfileTeamsController);
}