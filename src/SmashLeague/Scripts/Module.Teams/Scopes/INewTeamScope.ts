
module SmashLeague.Teams {
  'use strict';

  export interface INewTeamScope extends ng.IScope {
    Name: string;
    Members: any[];
    AddMember: (member: any) => void;
  }
}