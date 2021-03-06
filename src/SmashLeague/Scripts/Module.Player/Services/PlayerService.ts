﻿
module SmashLeague.Players {
  'use strict';

  export class PlayersService {

    private _http: ng.IHttpService;

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {

      this._http = http;
    }

    public GetPlayersAsync(
      ): ng.IHttpPromise<any[]> {

      return this._http.get('/api/player');
    }

    public CreatePlayerWithTagAsync(
      tag: string): ng.IHttpPromise<any> {

      return this._http.post('/api/player/placeholder', { Tag: tag });
    }

    public GetPlayerPartialAsync(
      partial: string): ng.IHttpPromise<any[]> {

      return this._http.get('/api/player/search?partial=' + partial);
    }

    public GetPlayerAsync(
      username: string): ng.IHttpPromise<any> {

      return this._http.get('/api/player/' + username);
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