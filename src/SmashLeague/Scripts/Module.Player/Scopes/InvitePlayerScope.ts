
module SmashLeague.Players {

  export interface IInvitePlayerScope extends ng.IScope {
    Invite: (model: ng.INgModelController) => void;
    Reset: () => void;
    Invitee: string;
    Invited: any;
    Errors: any;
    IsInviting: boolean;
    IsInviteComplete: boolean;
  }
}