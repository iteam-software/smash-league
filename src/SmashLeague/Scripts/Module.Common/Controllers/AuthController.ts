
module SmashLeague.Common {
  'use strict';

  export class AuthController {

    private _windowService: ng.IWindowService;
    private _scope: IAuthenticationScope;
    private _interval: ng.IIntervalService;
    private _http: ng.IHttpService;
    private _authenticationService: IAuthenticationService;

    public static $inject = [
      '$window',
      '$scope',
      '$interval',
      '$http',
      'AuthenticationService'
    ];

    constructor(
      window,
      scope,
      interval,
      http,
      auth) {

      this._windowService = window;
      this._scope = scope;
      this._interval = interval;
      this._http = http;
      this._authenticationService = auth;

      this._scope.SignIn = $.proxy(this.SignIn, this);
      this._scope.SignOut = $.proxy(this.SignOut, this);
      this._scope.Service = this._authenticationService;
      this._scope.$on(Common.Events.AuthStateChange, () => { });
    }

    public SignOut() {
      this._http.post('/auth/signout', null)
        .then(() => this._authenticationService.ValidateAuthState());
    }

    public SignIn(
      provider: string) {

      var oauth = this._windowService.open('/auth/signin-with-' + provider, '', 'top=50,left=50,status=0,width=800,height=680');

      var checkPopup = this._interval(() => {
        try {
          if (!oauth || oauth.closed || oauth['SmashLeague:OAuth:Complete']) {
            this._interval.cancel(checkPopup);

            // Validate the login
            this._authenticationService.ValidateAuthState();

            // Close popup
            if (oauth && !oauth.closed) {
              oauth.close();
            }
          }
        }
        catch (err) {
        }
      }, 1000);
    }
  }

  Application.Module.controller('AuthController', AuthController);
}