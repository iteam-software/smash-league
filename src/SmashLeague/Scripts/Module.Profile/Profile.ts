
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
            template: '<p>Feed</p>'
          }
        }
      });

      stateProvider.state('Profile.Teams', {
        url: '/teams',
        views: {
          'ProfileContent': {
            template: '<p>Teams</p>'
          }
        }
      });

      stateProvider.state('Profile.Stats', {
        url: '/stats',
        views: {
          'ProfileContent': {
            template: '<p>Stats</p>'
          }
        }
      });

      stateProvider.state('Profile.History', {
        url: '/history',
        views: {
          'ProfileContent': {
            template: '<p>History</p>'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Profile', ['ui.router', 'SmashLeague.Common']);

  Application.Module.config(Application.Config);
}