
module SmashLeague.Teams {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('!.Anonymous.Teams', {
        url: 'teams',
        views: {
          'Banner': {
            template: '<div class="banner banner-blue"></div>'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Teams', ['ui.router']);
  
  Application.Module.config(Application.Config);
}