
module SmashLeague {
  'use strict';

  var smashLeague = angular.module('SmashLeague', []);

  // Add directives
  smashLeague.directive('dropdownKeepOpen', DropdownKeepOpen.Factory);
}