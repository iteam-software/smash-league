
module SmashLeague {
  'use strict';

  export interface IAuthenticationScope extends ng.IScope {
    SignIn(provider: string): void;
    SignOut(): void;
    Service: Common.IAuthenticationService;
    State: ng.ui.IStateService;
  }
}