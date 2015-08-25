
module SmashLeague.Players {
  'use strict';

  export class PlayersService {

    private _http: ng.IHttpService;
    private _players: any[];

    public static $inject = [
      '$http'
    ];

    public get Players() { return this._players }
    public set Players(value: any[]) {
      value.forEach(player => {
        if (player.Banner) {
          player.Banner.SrcUrl = 'url(' + player.Banner.Src + ')';
        }
      });

      this._players = value;
    }

    constructor(
      http) {

      this._http = http;
    }

    public LoadPlayers(
      callback?: (Players: any[]) => void) {

      this._http.get('/api/player')
        .success((players: any[]) => {
          this.Players = players;
          if (callback !== undefined) {
            callback(players);
          }
        });
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