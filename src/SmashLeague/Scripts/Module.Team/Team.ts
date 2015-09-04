
module SmashLeague.Teams {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Team', {
        url: '/team',
        views: {
          'Banner': {
            templateUrl: '/teams/banner'
          },
          'Content': {
            templateUrl: '/teams/content',
            controller: 'TeamController'
          }
        }
      });

      stateProvider.state('Team-Search', {
        url: '/search?q',
        resolve: {
          query: ['$stateParams', (params) => params.q]
        },
        views: {
          'Banner': {
            templateUrl: '/teams/banner',
            controller: 'TeamSearchQueryController'
          },
          'Content': {
            templateUrl: '/teams/search',
            controller: 'TeamSearchController'
          }
        }
      });

      stateProvider.state('Team-New', {
        url: '/team/new',
        resolve: {
          captain: ['ProfileService', (service) => { return service.Profile }]
        },
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

      stateProvider.state('Team-Detail', {
        url: '/team/{normalizedName}',
        resolve: {
          normalizedName: ['$stateParams', (stateParams) => stateParams.normalizedName]
        },
        views: {
          'Banner': {
            template: '<div class="banner banner-blue"></div>'
          },
          'Content': {
            templateUrl: '/teams/detail',
            controller: 'TeamDetailController'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Team', [
    'ui.router',
    'ngDragDrop',
    'SmashLeague.Player',
    'SmashLeague.Profile'
  ]);
  
  Application.Module.config(Application.Config);
}