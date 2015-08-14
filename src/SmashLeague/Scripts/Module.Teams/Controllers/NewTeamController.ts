
module SmashLeague.Teams {
  'use strict';

  export class NewTeamController {

    private _scope: INewTeamScope;
    private _members: any[];

    public static $inject = [
      '$scope'
    ];

    constructor(
      scope) {

      this._members = [{
        Id: 'member-1',
        Label: 'Member 1',
        Placeholder: 'Member 1'
      }];

      this._scope = scope;
      this._scope.Members = this._members;
      this._scope.AddMember = $.proxy(this.AddMember, this);
    }

    public get Members() { return this._members }

    public AddMember(
      member: any) {

      var count = this._members.length;

      if (count < 4) {
        this._members.push({
          Id: 'member-' + count + 1,
          Label: 'Member ' + count + 1,
          Placeholder: 'Member ' + count + 1
        });
      }

      member.Complete = true;
    }
  }

  Application.Module.controller('NewTeamController', NewTeamController);
}