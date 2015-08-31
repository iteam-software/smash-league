
module SmashLeague.Teams {
  'use strict';

  export class TeamService {

    private _http: ng.IHttpService;

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {

      this._http = http;
    }

    public GetSuggestionsAsync(
      players?: any[]): ng.IHttpPromise<any[]> {
      return this._http.post('/api/team/suggest', players);
    }

    public static get Factory() {

      var factory = (http) => {
        return new TeamService(http);
      }

      factory.$inject = TeamService.$inject;

      return factory;
    }
  }

  Application.Module.factory('TeamService', TeamService.Factory);
}