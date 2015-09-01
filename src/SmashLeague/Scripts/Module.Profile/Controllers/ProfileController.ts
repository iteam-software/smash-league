
module SmashLeague.Profile {
  'use strict';

  export class ProfileController {

    private _scope: IProfileScope;
    private _service: ProfileService;
    private _auth: Common.IAuthenticationService;

    public static $inject = [
      '$scope',
      'ProfileService',
      'AuthenticationService'
    ];

    constructor(
      scope,
      profileService,
      auth) {

      this._scope = scope;
      this._service = profileService;
      this._auth = auth;

      this._scope.Service = profileService;
      this._scope.IsEditing = false;
      this._scope.Edit = this.InitializeEditObject({});

      this._scope.BeginEdit = $.proxy(this.BeginEdit, this);
      this._scope.Save = $.proxy(this.Save, this);
      this._scope.Cancel = $.proxy(this.Cancel, this);
      this._scope.ToggleRole = $.proxy(this.ToggleRole, this);

      this._service.GetProfile()
        .success((profile) => this.SetProfile(profile));
    }

    public BeginEdit() {
      this._scope.IsEditing = true;
    }

    public Save() {

      this._scope.Service.UpdateProfile($.extend(this._scope.Profile, {
        ProfileImageEditData: this._scope.Edit.Image.Src,
        BannerImageEditData: this._scope.Edit.Banner.Src,
        PreferredRoles: this._scope.Edit.Role
      })).success((profile) => this.SetProfile(profile));

      this._scope.Edit = this.InitializeEditObject(this._scope.Edit);
      this._scope.IsEditing = false;
    }

    public Cancel() {

      this._scope.Edit = this.InitializeEditObject(this._scope.Edit);
      this._scope.IsEditing = false;
    }

    public ToggleRole(
      role: string) {

      switch (role.toLowerCase()) {
        case 'tank':
          this._scope.Edit.Role = this.ToggleBit(this._scope.Edit.Role, 0x1);
          break;
        case 'assassin':
          this._scope.Edit.Role = this.ToggleBit(this._scope.Edit.Role, 0x2);
          break;
        case 'support':
          this._scope.Edit.Role = this.ToggleBit(this._scope.Edit.Role, 0x4);
          break;
        case 'specialist':
          this._scope.Edit.Role = this.ToggleBit(this._scope.Edit.Role, 0x8);
          break;
      }
    }

    private SetProfile(
      profile: any) {

      this._scope.Profile = profile;
      this._scope.Edit.Role = profile.PreferredRoles;
    }

    private ToggleBit(
      field: number,
      bit: number) {
      return field ^ bit;
    }

    private InitializeEditObject(
      edit: any) {

      if (edit.Banner) { delete edit.Banner }
      if (edit.Image) { delete edit.Image }

      edit.Banner = {};
      edit.Image = {};
      edit.Role = 0;

      return edit;
    }
  }

  Application.Module.controller('ProfileController', ProfileController);
}