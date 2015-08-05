
module SmashLeague {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      auth: IAuthenticationServiceProvider,
      stateProvider: ng.ui.IStateProvider) {

      // Enable auth state checking
      auth.AddUnauthorizedResponseCallback();

      stateProvider.state('Anonymous', {
        views: {
          '': {
            templateUrl: '/root'
          },
          'Navigation@Anonymous': {
            templateUrl: '/anonymous/navigation',
            controller: 'AuthController'
          }
        }
      });

      stateProvider.state('Authenticated', {
        views: {
          '': {
            templateUrl: '/root'
          },
          'Navigation@Authenticated': {
            templateUrl: '/authenticated/navigation',
            controller: 'AuthController'
          }
        }
      });
    }

    public static Run(
      scope: IAuthenticationScope,
      authService: IAuthenticationService,
      stateService: ng.ui.IStateService) {

      scope.Service = authService;

      scope.$watch('Service.IsAuthenticated', (newValue: boolean) => {
        stateService.go(newValue ? 'Authenticated' : 'Anonymous');
      });
    }
  }

  Application.Config.$inject = [
    '!AuthenticationServiceProvider',
    '$stateProvider'
  ];

  Application.Run.$inject = [
    '$rootScope',
    '!AuthenticationService',
    '$state'
  ];
  
  // Create the angular module
  Application.Module = angular.module('SmashLeague', ['ui.router']);

  // Configure and run the application
  Application.Module.config(Application.Config);
  Application.Module.run(Application.Run);
}