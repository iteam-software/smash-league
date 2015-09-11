
module SmashLeague.Teams {
  'use strict';

  export class TeamDetailController {

    private _teamService: TeamService;
    private _scope: ITeamDetailScope;

    public static $inject = [
      'normalizedName',
      'TeamService',
      'AuthenticationService',
      '$scope'
    ];

    constructor(
      name,
      teamService,
      authService,
      scope) {

      this._scope = scope;
      this._teamService = teamService;

      this._scope.AuthService = authService;
      this._scope.BeginEdit = $.proxy(this.BeginEdit, this);
      this._scope.Save = $.proxy(this.Save, this);
      this._scope.Cancel = $.proxy(this.Cancel, this);
      this._scope.Edit = this.InitializeEditObject({});

      this._teamService.GetTeam(name)
        .success((team) => this._scope.Team = team);
    }

    public BeginEdit() {

      this._scope.Edit = this.InitializeEditObject(this._scope.Edit);
      this._scope.IsEditing = true;
    }

    public Save() {

      this._teamService.UpdateTeam($.extend(this._scope.Team, {
        TeamImageEditSrc: this._scope.Edit.Image.Src,
        HeaderImageEditSrc: this._scope.Edit.Banner.Src
      })).success((team) => this._scope.Team = team);

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

  Application.Module.controller('TeamDetailController', TeamDetailController);
}