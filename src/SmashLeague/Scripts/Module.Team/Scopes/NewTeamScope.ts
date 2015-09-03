
module SmashLeague.Teams {
  'use strict';

  export interface INewTeamScope extends ng.IScope {
    Name: string;
    Captain: any;
    SelectedPlayer: any;
    SelectedPlayerUsername: string;
    Roster: any[];
    Suggestions: any[];
    SearchResults: any[];
    Errors: any[];

    AddToRoster: (player: any) => void;
    RemoveFromRoster: (Players: any) => void;
    FindPlayers: (partial: string) => ng.IHttpPromise<any[]>;
    SelectPlayer: (player: any) => void;
    CreateTeam: (createTeamForm: ng.IFormController) => void;
  }
}