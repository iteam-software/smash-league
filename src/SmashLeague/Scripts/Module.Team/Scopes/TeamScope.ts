﻿
module SmashLeague.Teams {

  export interface ITeamScope extends ng.IScope {
    TopTeams: any[];
    SearchResults: any[];
    Query: string;
  }
}