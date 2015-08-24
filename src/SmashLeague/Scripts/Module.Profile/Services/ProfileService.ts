
module SmashLeague.Profile {
  'use strict';

  export class ProfileService {

    private _profile: any;
    private _http: ng.IHttpService;

    public get Profile() { return this._profile }
    public set Profile(value) {

      // Extend Profile.Banner if needed so that it can be rendered in an inline style
      if (value && value.Banner) {
        if (!value.Banner.SrcUrl) {
          value.Banner.SrcUrl = 'url(' + value.Banner.Src + ')';
        }
      }

      this._profile = value;
    }

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {
      this._http = http;
    }

    public LoadProfile() {
      this._http.get('/api/profile')
        .success(profile => this.Profile = profile);
    }

    public DestroyProfile() {
      delete this._profile;
      this._profile = undefined;
    }

    public UpdateProfile(profile) {
      this._http.put('/api/profile', profile)
        .success(updated => this.Profile = updated);
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