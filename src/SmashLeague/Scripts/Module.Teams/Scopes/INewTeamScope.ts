
module SmashLeague.Teams {
  'use strict';

  export interface INewTeamScope extends ng.IScope {
    Name: string;
    Member1: any;
    Member2: any;
    Member3: any;
    Member4: any;
    Players: any[];
  }
}