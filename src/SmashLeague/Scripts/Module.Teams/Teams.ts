
module SmashLeague.Teams {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Teams', {
        url: '/teams',
        views: {
          'Banner': {
            template: '<div class="banner banner-blue"></div>'
          },
          'Content': {
            templateUrl: '/teams/content'
          }
        }
      });

      stateProvider.state('Teams-New', {
        url: '/teams/new',
        views: {
          'Banner': {
            template: '<div class="banner banner-blue"></div>'
          },
          'Content': {
            templateUrl: '/teams/new',
            controller: 'NewTeamController'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Teams', ['ui.router']);
  
  Application.Module.config(Application.Config);
}