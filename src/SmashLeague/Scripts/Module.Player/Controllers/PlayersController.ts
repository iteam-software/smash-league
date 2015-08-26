
module SmashLeague.Players {
  'use strict';

  export class PlayersController {

    private _scope: IServiceScope<PlayersService>;

    public static $inject = [
      '$scope',
      'PlayersService'
    ];

    constructor(
      scope,
      service) {

      this._scope = scope;
      this._scope.Service = service;
    }
  }

  Application.Module.controller('PlayersController', PlayersController);
}