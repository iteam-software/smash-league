
module SmashLeague {
  'use strict';

  export interface IAuthenticationScope extends ng.IScope {
    SignIn(provider: string): void;
    SignOut(): void;
    Service: IAuthenticationService;
    State: ng.ui.IStateService;
  }
}