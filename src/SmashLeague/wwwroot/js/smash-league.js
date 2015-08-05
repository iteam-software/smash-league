var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var Config = (function () {
        function Config(auth) {
            auth.AddUnauthorizedResponseCallback();
        }
        Config.$inject = [
            '!AuthenticationServiceProvider'
        ];
        return Config;
    })();
    SmashLeague.Config = Config;
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var Application = (function () {
        function Application() {
        }
        return Application;
    })();
    SmashLeague.Application = Application;
    Application.Module = angular.module('SmashLeague', []);
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var AuthController = (function () {
        function AuthController(window, scope, interval, auth) {
            this._windowService = window;
            this._scope = scope;
            this._interval = interval;
            this._authenticationService = auth;
            this._scope.SignIn = $.proxy(this.SignIn, this);
            this._scope.Service = this._authenticationService;
        }
        AuthController.prototype.SignIn = function (provider) {
            var _this = this;
            var oauth = this._windowService.open('/auth/signin-with-' + provider, '', 'top=50,left=50,status=0,width=800,height=600');
            var checkPopup = this._interval(function () {
                try {
                    if (oauth['SmashLeague:OAuth:Complete']) {
                        _this._interval.cancel(checkPopup);
                        oauth.close();
                    }
                }
                catch (err) {
                }
            }, 1000);
        };
        AuthController.$inject = [
            '$window',
            '$scope',
            '$interval',
            '!AuthenticationService'
        ];
        return AuthController;
    })();
    SmashLeague.AuthController = AuthController;
    SmashLeague.Application.Module.controller('AuthController', AuthController);
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var DropdownKeepOpen = (function () {
        function DropdownKeepOpen() {
            this.link = $.proxy(this.Link, this);
            this.restrict = 'A';
        }
        DropdownKeepOpen.prototype.Link = function (scope, element, attrs) {
            if (!element.hasClass('dropdown')) {
                throw 'Invalid directive usage: dropdown-keep-open must be applied to a .dropdown';
            }
            element.on({
                "shown.bs.dropdown": function () { return element.data('closable', false); },
                "hide.bs.dropdown": function () { return element.data('closable'); }
            });
            element.find('a').on('click', function () { return element.data('closable', true); });
            element.find('button').on('click', function () { return element.data('closable', true); });
        };
        Object.defineProperty(DropdownKeepOpen, "Factory", {
            get: function () {
                var directive = function () {
                    return new DropdownKeepOpen();
                };
                directive.$inject = DropdownKeepOpen.$inject;
                return directive;
            },
            enumerable: true,
            configurable: true
        });
        return DropdownKeepOpen;
    })();
    SmashLeague.DropdownKeepOpen = DropdownKeepOpen;
    SmashLeague.Application.Module.directive('dropdownKeepOpen', DropdownKeepOpen.Factory);
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var AuthenticationServiceProvider = (function () {
        function AuthenticationServiceProvider(httpProvider) {
            this._httpProvider = httpProvider;
            this.$get.$inject = SmashLeague.AuthenticationService.$inject;
        }
        AuthenticationServiceProvider.prototype.AddUnauthorizedResponseCallback = function () {
            var _this = this;
            var interceptor = function () {
                return {
                    'response': $.proxy(_this.HandleUnauthorized, _this)
                };
            };
            this._httpProvider.interceptors.push(interceptor);
        };
        AuthenticationServiceProvider.prototype.HandleUnauthorized = function (response) {
            if (this._service !== undefined && response.status == 401) {
                this._service.UnauthorizedResponseCallback();
            }
        };
        AuthenticationServiceProvider.prototype.$get = function (http) {
            this._service = new SmashLeague.AuthenticationService(http);
            return this._service;
        };
        AuthenticationServiceProvider.$inject = [
            '$httpProvider'
        ];
        return AuthenticationServiceProvider;
    })();
    SmashLeague.AuthenticationServiceProvider = AuthenticationServiceProvider;
    SmashLeague.Application.Module.provider('!AuthenticationService', AuthenticationServiceProvider);
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var AuthenticationService = (function () {
        function AuthenticationService(http) {
            this._http = http;
            this.ValidateAuthState();
        }
        Object.defineProperty(AuthenticationService.prototype, "IsAuthenticated", {
            get: function () { return this._isAuthenticated; },
            enumerable: true,
            configurable: true
        });
        AuthenticationService.prototype.UnauthorizedResponseCallback = function () {
            this.ValidateAuthState();
        };
        AuthenticationService.prototype.ValidateAuthState = function () {
            var _this = this;
            this._http.get('/auth/validate')
                .success(function () { return _this._isAuthenticated = true; })
                .error(function () { return _this._isAuthenticated = false; });
        };
        AuthenticationService.$inject = [
            '$http'
        ];
        return AuthenticationService;
    })();
    SmashLeague.AuthenticationService = AuthenticationService;
})(SmashLeague || (SmashLeague = {}));
//# sourceMappingURL=smash-league.js.map