
module SmashLeague {
  'use strict';

  export interface IAuthenticationServiceProvider extends ng.IServiceProvider {
    AddUnauthorizedResponseCallback(): void;
  }

  export class AuthenticationServiceProvider implements IAuthenticationServiceProvider {

    private _unauthorizedCallback: (path: string) => void;
    private _service: IAuthenticationService;
    private _httpProvider: ng.IHttpProvider;

    public static $inject = [
      '$httpProvider'
    ];

    constructor(
      httpProvider) {

      this._httpProvider = httpProvider;
      this.$get.$inject = AuthenticationService.$inject;
    }

    public AddUnauthorizedResponseCallback() {

      var interceptor = () => {
        return {
          'response': $.proxy(this.HandleUnauthorized, this)
        }
      };

      // Add interceptor
      this._httpProvider.interceptors.push(interceptor);
    }

    private HandleUnauthorized(
      response) {
      if (this._service !== undefined && response.status == 401) {
        this._service.UnauthorizedResponseCallback();
      }
    }

    public $get(http) {

      this._service = new AuthenticationService(http);

      return this._service;
    }
  }

  Application.Module.provider('!AuthenticationService', AuthenticationServiceProvider);
}