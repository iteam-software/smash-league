
module SmashLeague.Teams {
  'use strict';

  export class TeamListing {

    public templateUrl: string;
    public require: string;
    public restrict: string;
    public scope: any;

    constructor(
      ) {

      this.restrict = 'E';
      this.require = 'ngModel';
      this.scope = {
        Team: '=ngModel'
      }
      this.templateUrl = '/teams/template/team-listing';
    }

    public static get Factory() {
      
      var factory = () => new TeamListing();

      factory.$inject = TeamListing.$inject;

      return factory;
    }
  }

  Application.Module.directive('teamListing', TeamListing.Factory);
}