
module SmashLeague {
  'use strict';

  export interface IAuthenticationService {
    IsAuthenticated: boolean;
    Battletag: string;
    UnauthorizedResponseCallback(): void;
    ValidateAuthState(): void;
  }

  export class AuthenticationService implements IAuthenticationService {

    private _isAuthenticated: boolean;
    private _battletag: string;
    private _http: ng.IHttpService;

    public get IsAuthenticated() { return this._isAuthenticated; }
    public get Battletag() { return this._battletag; }

    public static $inject = [
      '$http'
    ];

    constructor(
      http) {

      this._http = http;
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
        this._isAuthenticated = data.Authenticated;
        this._battletag = data.Battletag;
      }
      else {
        this._isAuthenticated = false;
        this._battletag = undefined;
      }
    }
  }
}