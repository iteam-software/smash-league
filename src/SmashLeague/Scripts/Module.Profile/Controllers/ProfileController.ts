
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

      if (this._auth.IsAuthenticated && (!this._service.Profile || this._service.Profile.Username != this._auth.Username)) {
        this._service.LoadProfile();
      }

      this._scope.$on(Common.Events.AuthStateChange, (event, authenticated: boolean) => {
        if (authenticated) this._service.LoadProfile();
      });
    }

    public BeginEdit() {
      this._scope.IsEditing = true;
    }

    public Save() {

      this._scope.Service.UpdateProfile($.extend(this._scope.Service.Profile, {
        ProfileImageEditData: this._scope.Edit.Image.Src,
        BannerImageEditData: this._scope.Edit.Banner.Src
      }));

      this._scope.Edit = this.InitializeEditObject(this._scope.Edit);
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