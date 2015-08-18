
module SmashLeague.Teams {
  'use strict';

  export interface INewTeamScope extends ng.IScope {
    Name: string;
    Captain: any;
    Member1: any;
    Member2: any;
    Member3: any;
    Member4: any;
    Roster: any[];
    AddToRoster: (event: ng.IAngularEvent, player: any) => void;
    ProfileService: Profile.ProfileService;
  }
}