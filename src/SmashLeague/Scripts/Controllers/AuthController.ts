
module SmashLeague {
  'use strict';

  export class AuthController {

    private _windowService: ng.IWindowService;
    private _scope: IAuthenticationScope;
    private _interval: ng.IIntervalService;
    private _authenticationService: IAuthenticationService;

    public static $inject = [
      '$window',
      '$scope',
      '$interval',
      '!AuthenticationService'
    ];

    constructor(
      window,
      scope,
      interval,
      auth) {

      this._windowService = window;
      this._scope = scope;
      this._interval = interval;
      this._authenticationService = auth;

      this._scope.SignIn = $.proxy(this.SignIn, this);
      this._scope.Service = this._authenticationService;
    }

    public SignIn(
      provider: string) {

      var oauth = this._windowService.open('/auth/signin-with-' + provider, '', 'top=50,left=50,status=0,width=800,height=600');

      var checkPopup = this._interval(() => {
        try {
          if (oauth['SmashLeague:OAuth:Complete']) {
            this._interval.cancel(checkPopup);
            oauth.close();
          }
        }
        catch (err) {
        }
      }, 1000);
    }
  }

  Application.Module.controller('AuthController', AuthController);
}