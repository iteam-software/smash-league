
module SmashLeague.Profile {
  'use strict';

  export class ProfileController {

    private _scope: IServiceScope<ProfileService>;
    private _service: ProfileService;

    public static $inject = [
      '$scope',
      'ProfileService'
    ];

    constructor(
      scope,
      profileService) {

      this._scope = scope;
      this._service = profileService;

      this._scope.Service = profileService;
    }
  }

  Application.Module.controller('ProfileController', ProfileController);
}