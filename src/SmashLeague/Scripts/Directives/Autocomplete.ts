
module SmashLeague {
  'use strict';

  export class Autocomplete implements ng.IDirective {

    public restrict: string;
    public transclude: boolean;
    public scope: any;
    public link: ng.IDirectiveLinkFn;

    private _http: ng.IHttpService;

    constructor(
      ) {

      this.restrict = 'A';
      this.transclude = true;
      this.scope = { Results: '=results', Load: '=load' }
      this.link = $.proxy(this.Link, this);
    }

    public Link(
      scope: IAutocompleteScope,
      element: ng.IAugmentedJQuery,
      attrs: ng.IAttributes) {

      // TODO: validate scope.Results && scope.Load
      // Validate we are attached to an input
      
      var state = {};

      // Bind to events
      element.keyup((e) => this.HandleKeyUp(e, scope, $.extend(state, { Value: element.val() })));
    }

    private HandleKeyUp(
      e: JQueryKeyEventObject,
      scope: IAutocompleteScope,
      state: any) {

      if (!state.Loading && state.Value && state.Value !== '') {
        state.Loading = true;
        var promise = scope.Load(state.Value);
        promise.success((result) => {
          // Clear results and push new ones on
          this.ClearResults(scope);

          result.forEach((o) => scope.Results.push(o));

          this.EnsureApplied(scope);
        });
        promise.then(() => state.Loading = false);
      }

      if (state.Value === '') {
        this.ClearResults(scope, true);
      }
    }

    private EnsureApplied(
      scope: ng.IScope) {

      if (!scope.$$phase && !scope.$root.$$phase) {
        scope.$apply();
      }
    }

    private ClearResults(
      scope: IAutocompleteScope,
      apply?: boolean) {

      scope.Results.splice(0, scope.Results.length);

      if (apply) {
        this.EnsureApplied(scope);
      }
    }

    public static get Factory() {
      var factory = () => {
        return new Autocomplete()
      }

      factory.$inject = Autocomplete.$inject;

      return factory;
    }
  }

  Application.Module.directive('autoComplete', Autocomplete.Factory);
}