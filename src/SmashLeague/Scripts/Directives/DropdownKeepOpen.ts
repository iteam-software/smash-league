
module SmashLeague {
  'use strict';

  export class DropdownKeepOpen {

    public link: ng.IDirectiveLinkFn;
    public restrict: string;

    constructor(
      ) {
     
      this.link = $.proxy(this.Link, this); 
      this.restrict = 'A'

    }

    private Link(
      scope: ng.IScope,
      element: ng.IAugmentedJQuery,
      attrs: ng.IAttributes) {

      if (!element.hasClass('dropdown')) {
        throw 'Invalid directive usage: dropdown-keep-open must be applied to a .dropdown';
      }

      element.on({
        "shown.bs.dropdown": () => element.data('closable', false),
        "hide.bs.dropdown": () => { return element.data('closable'); }
      });

      // Attach handlers to links and buttons
      element.find('a').on('click', () => element.data('closable', true));
      element.find('button').on('click', () => element.data('closable', true));
    }

    public static get Factory() {

      var directive = () => {
        return new DropdownKeepOpen();
      }

      directive.$inject = DropdownKeepOpen.$inject;

      return directive;
    }
  }
}