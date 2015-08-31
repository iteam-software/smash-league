
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
    Suggestions: any[];
    AddToRoster: (player: any) => void;
    RemoveFromRoster: (Players: any) => void;
    ProfileService: Profile.ProfileService;
  }
}