
module SmashLeague.Players {
  'use strict';

  export class InvitePlayerController {

    private _playersService: PlayersService;
    private _scope: IInvitePlayerScope;

    public static $inject = [
      'PlayersService',
      '$scope'
    ];

    constructor(
      playersService,
      scope) {

      this._playersService = playersService;
      this._scope = scope;

      this._scope.Invite = $.proxy(this.Invite, this);
      this._scope.Reset = $.proxy(this.Reset, this);

      this.Reset();
    }

    public Invite(
      invitee: ng.INgModelController) {

      if (invitee.$valid) {
        this._scope.IsInviting = true;
        this._playersService.CreatePlayerWithTagAsync(invitee.$modelValue)
          .success((player) => {
            this._scope.IsInviting = false;
            this._scope.IsInviteComplete = true;
            this._scope.Invited = player;

            // Assuming this scope is a child scope of NewTeamController, let's try to add the new player to the roster
            if (typeof this._scope['AddToRoster'] === 'function') {
              this._scope['AddToRoster'](player);
            }
          })
          .error((errors) => {
            this._scope.IsInviting = false;
            this._scope.IsInviteComplete = true;
            this._scope.Errors = errors || ['An unknown error occurred'];
          });
      }
    }

    public Reset(
      ) {
      this._scope.IsInviting = false;
      this._scope.IsInviteComplete = false;
    }
  }

  Application.Module.controller('InvitePlayerController', InvitePlayerController);
}