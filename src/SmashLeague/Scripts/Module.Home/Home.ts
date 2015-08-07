
module SmashLeague.Home {
  'use strict';

  export class Application {
    
    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('!.Anonymous.Home', {
        url: 'home',
        views: {
          'Banner': {
            template: '<div class="banner banner-default"></div>'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Home', ['ui.router']);
  
  Application.Module.config(Application.Config);
}