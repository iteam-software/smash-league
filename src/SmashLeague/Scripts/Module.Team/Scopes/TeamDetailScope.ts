
module SmashLeague.Teams {

  export interface ITeamDetailScope extends ng.IScope {

    Team: any;
    AuthService: Common.IAuthenticationService;
    IsEditing: boolean;
    Edit: any;
    BeginEdit: () => void;
    Cancel: () => void;
    Save: () => void;
  }
}