
module SmashLeague.Home {
  'use strict';

  export class HomeController {

    private _http: ng.IHttpService;
    private _scope: IHomeScope;

    public static $inject = [
      '$http',
      '$scope'
    ];

    constructor(
      http,
      scope) {

      this._http = http;
      this._scope = scope;

      this.LoadMatches(0, 10);
    }

    public LoadMatches(
      start: number,
      top: number) {

      this._http.get('/api/match?' + $.param({ start: start, top: top }))
        .success((matches: any) => this._scope.Matches = matches);
    }
  }

  Application.Module.controller('HomeController', HomeController);
}