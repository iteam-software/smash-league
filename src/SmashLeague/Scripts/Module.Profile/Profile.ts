
module SmashLeague.Profile {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Profile', {
        url: '/profile',
        views: {
          'Banner': {
            template: '<div class="banner banner-default"></div>'
          },
          'Content': {
            templateUrl: '/profile/content',
            controller: 'ProfileController'
          },
          'ProfileContent@Profile': {
            templateUrl: '/profile/my-feed'
          }
        }
      });

      stateProvider.state('Profile.Teams', {
        url: '/teams',
        views: {
          'ProfileContent': {
            templateUrl: '/profile/my-teams',
            controller: 'ProfileTeamsController'
          }
        }
      });

      stateProvider.state('Profile.Stats', {
        url: '/stats',
        views: {
          'ProfileContent': {
            templateUrl: '/profile/my-stats'
          }
        }
      });

      stateProvider.state('Profile.History', {
        url: '/history',
        views: {
          'ProfileContent': {
            templateUrl: '/profile/my-history'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Profile', ['ui.router', 'SmashLeague.Common', 'SmashLeague.Player', 'SmashLeague.Team']);

  Application.Module.config(Application.Config);
}