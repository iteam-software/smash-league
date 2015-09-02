
module SmashLeague.Teams {
  'use strict';

  export class NewTeamController {

    private _scope: INewTeamScope;
    private _location: ng.ILocationService;
    private _playersService: Players.PlayersService;
    private _teamService: TeamService;

    private _roster: any[];

    public static $inject = [
      '$scope',
      '$location',
      'AuthenticationService',
      'PlayersService',
      'TeamService'
    ];

    constructor(
      scope,
      location,
      authService: Common.IAuthenticationService,
      playersService: Players.PlayersService,
      teamService: TeamService) {

      this._roster = [];
      this._scope = scope;
      this._location = location;
      this._playersService = playersService;
      this._teamService = teamService;

      this._scope.Roster = this.Roster;
      this._scope.SearchResults = [];
      this._scope.SelectedPlayer = undefined;
      this._scope.SelectedPlayerUsername = undefined;

      this._scope.AddToRoster = $.proxy(this.AddToRoster, this);
      this._scope.RemoveFromRoster = $.proxy(this.RemoveFromRoster, this);
      this._scope.FindPlayers = $.proxy(this.FindPlayers, this);
      this._scope.SelectPlayer = $.proxy(this.SelectPlayer, this);
      this._scope.CreateTeam = $.proxy(this.CreateTeam, this);

      // Load suggestions
      teamService.GetSuggestionsAsync()
        .success(response => this._scope.Suggestions = response);

      // Load captain
      if (authService.IsAuthenticated) {
        this.SetCaptain(authService.Username);
      }

      this._scope.$on(Common.Events.AuthStateChange, (e, authenticated: boolean, username: string) => {
        if (authenticated) {
          this.SetCaptain(username);
        }
      });
    }

    public AddToRoster(
      player: any) {

      if (this._roster.indexOf(player) == -1) {
        this._roster.push(player);
      }

      if (player == this._scope.SelectedPlayer) {
        this._scope.SelectedPlayer = undefined;
        this._scope.SelectedPlayerUsername = undefined;
      }
    }

    public RemoveFromRoster(
      player: any) {

      var playerIndex = this._roster.indexOf(player);
      if (playerIndex > -1) {
        this._roster.splice(playerIndex, 1);
      }
    }

    // For use with Autocomplete directive
    public FindPlayers(
      partial: string) {

      return this._playersService.GetPlayerPartialAsync(partial);
    }

    public SelectPlayer(
      player: any) {

      this._scope.SelectedPlayer = player;
      this._scope.SelectedPlayerUsername = player.Username;

      if (!this._scope.$$phase && !this._scope.$root.$$phase) {
        this._scope.$apply();
      }
    }

    public CreateTeam(
      createTeamForm: ng.IFormController) {

      if (createTeamForm.$valid) {
        this._teamService.CreateTeam({ Name: this._scope.Name, Owner: this._scope.Captain, Roster: this._scope.Roster })
          .success((team: any) => this._location.url('/team/' + team.NormalizedName))
          .error((errors) => this._scope.Errors = errors);
      }
    }

    private SetCaptain(
      username: string) {

      this._playersService.GetPlayerAsync(username)
        .success(player => {
          this._scope.Captain = player;
          if (this._roster.indexOf(player) == -1) {
            this._roster.push(player);
          }
        });
    }

    public get Roster() { return this._roster }
  }

  Application.Module.controller('NewTeamController', NewTeamController);
}