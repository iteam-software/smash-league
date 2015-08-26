
module SmashLeague.Players {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Player', {
        url: '/player',
        views: {
          'Banner': {
            template: '<div class="banner banner-red"></div>'
          }
        }
      });
    }
  }

  Application.Config.$inject = ['$stateProvider'];

  Application.Module = angular.module('SmashLeague.Player', ['ui.router']);

  Application.Module.config(Application.Config);
}