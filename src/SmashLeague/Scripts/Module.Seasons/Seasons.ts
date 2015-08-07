
module SmashLeague.Seasons {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Seasons', {
        url: '/seasons',
        views: {
          'Banner': {
            template: '<div class="banner banner-gold"></div>'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Seasons', ['ui.router']);
  
  Application.Module.config(Application.Config);
}