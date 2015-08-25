
module SmashLeague {
  'use strict';

  export class InitializeController {

    private _totalLoadingCount: number;
    private _currentLoadingCount: number;
    private _returnUrl: string;
    private _location: ng.ILocationService;
    private _scope: IInitializeScope;
    private _root: ng.IRootScopeService;
    private _interval: ng.IIntervalService;
    private _intervalPromise: ng.IPromise<any>;
    private _authenticationService: Common.IAuthenticationService;

    public static $inject = [
      'returnUrl',
      'PlayersService',
      '$scope',
      '$rootScope',
      '$location',
      '$interval',
      'AuthenticationService'
    ];

    constructor(
      returnUrl: string,
      players: Players.PlayersService,
      scope: IInitializeScope,
      root,
      locationService,
      interval,
      auth) {

      this._totalLoadingCount = 1;
      this._currentLoadingCount = 0;
      this._returnUrl = returnUrl;
      this._location = locationService;
      this._scope = scope;
      this._root = root;
      this._interval = interval;
      this._authenticationService = auth;

      this._scope.Completion = 0;
      this.EnableSmoothLoading();

      // begin load operations

      // load players
      players.LoadPlayers($.proxy(this.LoadCompleteCallback, this));
    }

    private LoadCompleteCallback(
      arg: any) {

      this._scope.Completion = ++this._currentLoadingCount / this._totalLoadingCount;

      if (this._scope.Completion == 1) {

        this._root['Initialized'] = true;
        this._authenticationService.ValidateAuthState();
        this._location.url(this._returnUrl);
      }
    }

    private EnableSmoothLoading() {

      this._intervalPromise = this._interval(() => {

        var nextLoadRatio = (this._currentLoadingCount + 1) / this._totalLoadingCount;

        // Update completion
        this._scope.Completion += this.Clamp((nextLoadRatio - this._scope.Completion) / 8);

        // We need to cancel if we've hit 1.
        if (this._scope.Completion == 1) {
          this._interval.cancel(this._intervalPromise);
        }
        
      }, 200);
    }

    private Clamp(
      completion: number) {
      
      return completion >= 1 ? 1 : completion;
    }
  }

  Application.Module.controller('InitializeController', InitializeController);
}