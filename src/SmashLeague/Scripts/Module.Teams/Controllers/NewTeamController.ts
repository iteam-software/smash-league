
module SmashLeague.Teams {
  'use strict';

  export class NewTeamController {

    private _scope: INewTeamScope;
    private _players: any[];

    public static $inject = [
      '$scope'
    ];

    constructor(
      scope) {

      this._players = [
        { Name: 'retrofunk' },
        { Name: 'Nats' },
        { Name: 'Randy' },
        { Name: 'Archer' },
        { Name: 'Cheesechips' }
      ];

      this._scope = scope;
      this._scope.Players = this.Players;
    }

    public get Players() { return this._players }
  }

  Application.Module.controller('NewTeamController', NewTeamController);
}