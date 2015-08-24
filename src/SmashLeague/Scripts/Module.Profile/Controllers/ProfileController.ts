
module SmashLeague.Profile {
  'use strict';

  export class ProfileController {

    private _scope: IProfileScope;
    private _service: ProfileService;

    public static $inject = [
      '$scope',
      'ProfileService'
    ];

    constructor(
      scope,
      profileService) {

      this._scope = scope;
      this._service = profileService;

      this._scope.Service = profileService;
      this._scope.IsEditing = false;
      this._scope.Edit = this.InitializeEditObject({});
      this._scope.BeginEdit = $.proxy(this.BeginEdit, this);
      this._scope.Save = $.proxy(this.Save, this);
      this._scope.Cancel = $.proxy(this.Cancel, this);
    }

    public BeginEdit() {
      this._scope.IsEditing = true;
    }

    public Save() {

      this._scope.Service.UpdateProfile($.extend(this._scope.Service.Profile, this._scope.Edit));
      this._scope.IsEditing = false;
    }

    public Cancel() {

      this._scope.Edit = this.InitializeEditObject(this._scope.Edit);
      this._scope.IsEditing = false;
    }

    private InitializeEditObject(
      edit: any) {

      if (edit.Banner) { delete edit.Banner }
      if (edit.Image) { delete edit.Image }

      edit.Banner = {};
      edit.Image = {};

      return edit;
    }
  }

  Application.Module.controller('ProfileController', ProfileController);
}