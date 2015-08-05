
module SmashLeague {
  'use strict';

  export interface IAuthenticationService {
    IsAuthenticated: boolean;
    UnauthorizedResponseCallback(): void;
  }

  export class AuthenticationService implements IAuthenticationService {

    private _isAuthenticated: boolean;
    private _http: ng.IHttpService;

    public get IsAuthenticated() { return this._isAuthenticated; }

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

      this._http.get('/auth/validate')
        .success(() => this._isAuthenticated = true)
        .error(() => this._isAuthenticated = false);
    }
  }
}