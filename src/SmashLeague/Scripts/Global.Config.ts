
module SmashLeague {
  'use strict';

  export class Config {

    public static $inject = [
      '!AuthenticationServiceProvider'
    ];

    constructor(
      auth: IAuthenticationServiceProvider) {

      // Enable auth state checking
      auth.AddUnauthorizedResponseCallback();
    }
  }
}