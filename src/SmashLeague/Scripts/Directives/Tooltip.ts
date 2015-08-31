
module SmashLeague {

  export class Tooltip implements ng.IDirective {

    public link: ng.IDirectiveLinkFn;
    public restrict: string;

    constructor(
      ) {
      this.restrict = 'A';
      this.link = (scope, element, attrs) => {
        // Activate bootstrap tooltip
        element.tooltip();
      }
    }

    public static get Factory() {

      var factory = () => {
        return new Tooltip();
      }

      return factory;
    }
  }

  Application.Module.directive('tooltip', Tooltip.Factory);
}