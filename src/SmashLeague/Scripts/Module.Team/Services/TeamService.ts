
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

    public CreateTeam(
      team: any) {
      return this._http.post('/api/team', team);
    }

    public GetSuggestionsAsync(
      players?: any[]): ng.IHttpPromise<any[]> {
      return this._http.post('/api/team/suggest', players);
    }

    public GetTeamsForPlayer(
      username: string): ng.IHttpPromise<any[]> {

      return this._http.get('/api/team/' + username + '/teams');
    }

    public GetTopTeams(
      count: number): ng.IHttpPromise<any[]> {

      return this._http.get('/api/team/top/' + count);
    }

    public SearchForTeams(
      q: string): ng.IHttpPromise<any[]> {

      return this._http.get('/api/team/search?q=' + q);
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