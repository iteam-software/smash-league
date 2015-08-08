
module SmashLeague {
  'use strict';

  export class SearchNav implements ng.IDirective {

    public restrict: string;
    public scope: any;
    public templateUrl: string;

    constructor(
      ) {

      this.restrict = 'E';
      this.scope = { entity: '=entity' }
      this.templateUrl = '/partial/search-nav';
    }

    public static get Factory() {
      var directive = () => {
        return new SearchNav();
      }

      directive.$inject = SearchNav.$inject;

      return directive;
    }
  }

  Application.Module.directive('searchNav', SearchNav.Factory);
}