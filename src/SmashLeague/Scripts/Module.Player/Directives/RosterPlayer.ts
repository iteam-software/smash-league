
module SmashLeague.Players {
  'use strict';

  export class RosterPlayer implements ng.IDirective {

    public scope: any;
    public require: string;
    public restrict: string;
    public templateUrl: string;
    public transclude: boolean;

    public static $inject = [
    ];

    constructor(
      ) {

      this.scope = {
        Player: '=ngModel'
      }

      this.transclude = true;
      this.require = 'ngModel';
      this.restrict = 'E';
      this.templateUrl = '/player/template/roster-player';
    }

    public static get Factory() {
      
      var factory = () => {
        return new RosterPlayer();
      }

      factory.$inject = RosterPlayer.$inject;

      return factory;
    }
  }

  Application.Module.directive('rosterPlayer', RosterPlayer.Factory);
}