
module SmashLeague {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      auth: Common.IAuthenticationServiceProvider,
      stateProvider: ng.ui.IStateProvider) {

      // Enable auth state checking
      auth.AddUnauthorizedResponseCallback();

      // TODO: global not found page

      stateProvider.state('Initialize', {
        url: '/initialize',
        resolve: {
          returnUrl: ['$location', (location: ng.ILocationService) => {
            var returnUrl = location.search()['returnUrl'];
            return returnUrl = returnUrl !== undefined ? returnUrl : '/home';
          }]
        },
        views: {
          'Content': {
            templateUrl: '/initializing',
            controller: 'InitializeController'
          }
        }
      });
    }

    public static Run(
      scope: IAuthenticationScope,
      authService: Common.IAuthenticationService,
      stateService: ng.ui.IStateService,
      location: ng.ILocationService) {

      scope.Service = authService;
      scope.State = stateService;

      // capture return url
      var returnUrl = location.path();

      // Initialize the application
      location.url('initialize');

      if (returnUrl !== '/initialize' && returnUrl !== '' && returnUrl !== '/') {
        location.search('returnUrl', returnUrl);
      }
    }
  }

  Application.Config.$inject = [
    'AuthenticationServiceProvider',
    '$stateProvider'
  ];

  Application.Run.$inject = [
    '$rootScope',
    'AuthenticationService',
    '$state',
    '$location'
  ];
  
  // Create the angular module
  Application.Module = angular.module('SmashLeague', [
    'ui.router',
    'SmashLeague.Home',
    'SmashLeague.Player',
    'SmashLeague.Profile',
    'SmashLeague.Team',
    'SmashLeague.Tournament',
  ]);

  // Configure and run the application
  Application.Module.config(Application.Config);
  Application.Module.run(Application.Run);
}