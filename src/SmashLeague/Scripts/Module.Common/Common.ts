
module SmashLeague.Common {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Login', {
        url: '/login',
        views: {
          'Content': {
            templateUrl: '/auth/login',
            controller: 'AuthController'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Common', ['ui.router']);
  Application.Module.config(Application.Config);
}