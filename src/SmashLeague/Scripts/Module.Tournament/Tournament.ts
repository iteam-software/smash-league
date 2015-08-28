
module SmashLeague.Seasons {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Tournament', {
        url: '/tournament',
        views: {
          'Banner': {
            templateUrl: '/tournament/banner'
          },
          'Content': {
            templateUrl: '/tournament/content'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Tournament', ['ui.router']);
  
  Application.Module.config(Application.Config);
}