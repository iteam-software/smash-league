
module SmashLeague.Teams {
  'use strict';

  export class NewTeamController {

    private _scope: INewTeamScope;
    private _players: any[];
    private _roster: any[];

    private _captain: any;
    private _member1: any;
    private _member2: any;
    private _member3: any;
    private _member4: any;

    public static $inject = [
      '$scope',
      'ProfileService',
      'PlayersService'
    ];

    constructor(
      scope,
      profileService: Profile.ProfileService,
      playersService: Players.PlayersService) {


      this._scope = scope;
      this._scope.ProfileService = profileService;

      if (profileService.Profile) {

        this.SetCaptain(profileService.Profile.Username, playersService);
      }

      this._scope.$watch('ProfileService.Profile', () => {
        if (profileService.Profile) {
          this.SetCaptain(profileService.Profile.Username, playersService);
        }
        else {
          this._scope.Captain = undefined;
        }
      });

      this._scope.$on('SmashLeague:Event:AuthStateChange', (event, authenticated: boolean, username: string) => {

          if (authenticated) {
            this.SetCaptain(username, playersService);
          }
          else {
            this._scope.Captain = undefined;
            this._scope.Member1 = undefined;
            this._scope.Member2 = undefined;
            this._scope.Member3 = undefined;
            this._scope.Member4 = undefined;
          }
      });
    }

    private SetCaptain(
      username: string,
      playersService: Players.PlayersService) {

    }

    public get Roster() { return this._roster }
  }

  Application.Module.controller('NewTeamController', NewTeamController);
}