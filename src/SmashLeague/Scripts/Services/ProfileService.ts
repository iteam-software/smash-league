
module SmashLeague {
  'use strict';

  export class ProfileService {

    private _profile: any;
    private _http: ng.IHttpService;

    public get Profile() { return this._profile }

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {
      this._http = http;
    }

    public LoadProfile() {
      this._http.get('/api/profile')
        .success(profile => this._profile = profile);
    }

    public DestroyProfile() {
      delete this._profile;
      this._profile = undefined;
    }

    public static get Factory() {
      
      var factory = (http) => {
        return new ProfileService(http);
      }

      factory.$inject = ProfileService.$inject;

      return factory;
    }
  }

  SmashLeague.Application.Module.factory("ProfileService", ProfileService.Factory);
}