
module SmashLeague {
  'use strict';

  export interface IAuthenticationScope extends ng.IScope {
    SignIn(provider: string): void;
    Service: IAuthenticationService;
  }
}