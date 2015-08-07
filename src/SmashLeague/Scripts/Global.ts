
module SmashLeague {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      auth: IAuthenticationServiceProvider,
      stateProvider: ng.ui.IStateProvider) {

      // Enable auth state checking
      auth.AddUnauthorizedResponseCallback();

      stateProvider.state('!', {
        url: '/',
        abstract: true,
        templateUrl: '/root'
      });

      stateProvider.state('!.Anonymous', {
        abstract: true,
        views: {
          'Navigation': {
            templateUrl: '/anonymous/navigation',
            controller: 'AuthController'
          },
          'Content': {
            templateUrl: '/content'
          }
        }
      });

      stateProvider.state('!.Authenticated', {
        abstract: true,
        views: {
          'Navigation': {
            templateUrl: '/authenticated/navigation',
            controller: 'AuthController'
          },
          'Content': {
            templateUrl: '/content'
          }
        }
      });
    }

    public static Run(
      scope: IAuthenticationScope,
      authService: IAuthenticationService,
      stateService: ng.ui.IStateService,
      location: ng.ILocationService) {

      scope.Service = authService;
      scope.State = stateService;

      scope.$watch('Service.IsAuthenticated', (newValue: boolean) => {
        stateService.go(newValue ? '!.Authenticated.Home' : '!.Anonymous.Home');
      });

      location.path('/home');
    }
  }

  Application.Config.$inject = [
    '!AuthenticationServiceProvider',
    '$stateProvider'
  ];

  Application.Run.$inject = [
    '$rootScope',
    '!AuthenticationService',
    '$state',
    '$location'
  ];
  
  // Create the angular module
  Application.Module = angular.module('SmashLeague', [
    'ui.router',
    'SmashLeague.Home',
    'SmashLeague.Players',
    'SmashLeague.Teams',
    'SmashLeague.Seasons',
  ]);

  // Configure and run the application
  Application.Module.config(Application.Config);
  Application.Module.run(Application.Run);
}