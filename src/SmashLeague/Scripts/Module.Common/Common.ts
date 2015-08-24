
module SmashLeague.Common {
  'use strict';

  export class Application {

    public static Module: ng.IModule;
  }

  Application.Module = angular.module('SmashLeague.Common', []);
}