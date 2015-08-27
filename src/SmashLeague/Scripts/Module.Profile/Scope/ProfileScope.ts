﻿
module SmashLeague.Profile {

  export interface IProfileScope extends IServiceScope<ProfileService> {
    IsEditing: boolean;
    Edit: any;
    BeginEdit: () => void;
    Save: () => void;
    Cancel: () => void;
  }
}