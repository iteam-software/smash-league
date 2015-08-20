
module SmashLeague.Profile {

  export interface IProfileScope extends IServiceScope<ProfileService> {
    IsEditing: boolean;
    BeginEdit: () => void;
    Save: () => void;
    Cancel: () => void;
  }
}