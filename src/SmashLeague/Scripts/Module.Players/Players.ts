
module SmashLeague.Players {
  'use strict';

  export class Application {

    public static Module: ng.IModule;

    public static Config(
      stateProvider: ng.ui.IStateProvider) {

      stateProvider.state('Players', {
        url: '/players',
        views: {
          'Banner': {
            template: '<div class="banner banner-red"></div>'
          }
        }
      });
    }

    public static Run(
      playersService: PlayersService) {

      playersService.LoadPlayers();
    }
  }

  Application.Config.$inject = ['$stateProvider'];
  Application.Run.$inject = ['PlayersService'];

  Application.Module = angular.module('SmashLeague.Players', ['ui.router']);

  Application.Module.config(Application.Config);
  Application.Module.run(Application.Run);
}