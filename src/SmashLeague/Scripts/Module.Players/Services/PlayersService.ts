
module SmashLeague.Players {
  'use strict';

  export class PlayersService {

    private _http: ng.IHttpService;
    private _players: any[];

    public static $inject = [
      '$http'
    ];

    public get Players() { return this._players }

    constructor(
      http) {

      this._http = http;
    }

    public LoadPlayers() {

      this._http.get('/api/player')
        .success((players: any[]) => this._players = players);
    }

    public static get Factory() {
      
      var factory = (http) => {
        return new PlayersService(http);
      }

      factory.$inject = PlayersService.$inject;

      return factory;
    }
  }

  Application.Module.factory('PlayersService', PlayersService.Factory);
}