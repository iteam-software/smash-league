
module SmashLeague.Common {
  'use strict';

  export interface IAuthenticationService {
    IsAuthenticated: boolean;
    Username: string;
    UnauthorizedResponseCallback(): void;
    ValidateAuthState(): void;
  }

  export class AuthenticationService implements IAuthenticationService {

    private _isAuthenticated: boolean;
    private _username: string;
    private _http: ng.IHttpService;
    private _root: ng.IRootScopeService;
    private _location: ng.ILocationService;

    public get IsAuthenticated() { return this._isAuthenticated; }
    public set IsAuthenticated(value: boolean) {
      this._isAuthenticated = value;
    }
    public get Username() { return this._username; }

    public static $inject = [
      '$http',
      '$rootScope',
      '$location'
    ];

    constructor(
      http,
      rootScope,
      location) {

      this._http = http;
      this._root = rootScope;
      this._location = location;

      this.ValidateAuthState();
    }

    public UnauthorizedResponseCallback() {

      this.ValidateAuthState();
    }

    public ValidateAuthState(
      ) {

      this._http.get('/auth/authenticate')
        .success((data: any) => this.SetAuthState(true, data))
        .error(() => this.SetAuthState(false));
    }

    private SetAuthState(
      success: boolean,
      data: any = undefined) {

      if (success) {
        this.IsAuthenticated = data.Authenticated;
        this._username = data.Username;

        if (!data.Authenticated) {
          var returnUrl = this._location.path();

          if (returnUrl && returnUrl != '' && returnUrl != '/login') {
            this._location.url('/login').search('returnUrl', returnUrl);
          }
          else {
            this._location.url('/login');
          }
        }
      }
      else {
        this.IsAuthenticated = false;
        this._username = undefined;
      }

      this._root.$broadcast(Common.Events.AuthStateChange, this.IsAuthenticated, this.Username);
    }
  }
}