
module SmashLeague.Common {
  'use strict';

  export class FileSelector implements ng.IDirective {

    public link: ng.IDirectiveLinkFn;
    public restrict: string;
    public scope: any;

    constructor(
      ) {

      this.restrict = 'E';
      this.scope = {
        File: '=selectorFile'
      }
      this.link = this.Link;
    }

    public Link(
      scope: IFileSelectorScope,
      element: ng.IAugmentedJQuery,
      attrs: ng.IAttributes) {

      element.on('click', () => {

        var $file = $('<input type="file" />');

        $file.change((arg: any) => {
          if (arg.target.files && arg.target.files[0]) {

            var dataUrlReader = new FileReader();

            dataUrlReader.readAsDataURL(arg.target.files[0]);
            dataUrlReader.onload = (e: any) => {
              scope.File.Src = e.target.result;

              if (!scope.$$phase) {
                scope.$apply();
              }
            }
          }
        });

        // Click the file input
        $file.click();
      });
    }

    public static get Factory() {

      var factory = () => {
        return new FileSelector();
      }

      factory.$inject = FileSelector.$inject;

      return factory;
    }
  }

  Application.Module.directive('fileSelector', FileSelector.Factory);
}