var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var Application = (function () {
        function Application() {
        }
        Application.Config = function (auth, stateProvider) {
            auth.AddUnauthorizedResponseCallback();
            stateProvider.state('!', {
                url: '/',
                abstract: true,
                templateUrl: '/root'
            });
            stateProvider.state('!.Anonymous', {
                abstract: true,
                views: {
                    'Navigation': {
                        templateUrl: '/anonymous/navigation',
                        controller: 'AuthController'
                    },
                    'Content': {
                        templateUrl: '/content'
                    }
                }
            });
            stateProvider.state('!.Authenticated', {
                abstract: true,
                views: {
                    'Navigation': {
                        templateUrl: '/authenticated/navigation',
                        controller: 'AuthController'
                    },
                    'Content': {
                        templateUrl: '/content'
                    }
                }
            });
        };
        Application.Run = function (scope, authService, stateService, location) {
            scope.Service = authService;
            scope.State = stateService;
            scope.$watch('Service.IsAuthenticated', function (newValue) {
                stateService.go(newValue ? '!.Authenticated.Home' : '!.Anonymous.Home');
            });
            location.path('/home');
        };
        return Application;
    })();
    SmashLeague.Application = Application;
    Application.Config.$inject = [
        '!AuthenticationServiceProvider',
        '$stateProvider'
    ];
    Application.Run.$inject = [
        '$rootScope',
        '!AuthenticationService',
        '$state',
        '$location'
    ];
    Application.Module = angular.module('SmashLeague', [
        'ui.router',
        'SmashLeague.Home',
        'SmashLeague.Players',
        'SmashLeague.Teams',
        'SmashLeague.Seasons',
    ]);
    Application.Module.config(Application.Config);
    Application.Module.run(Application.Run);
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var AuthController = (function () {
        function AuthController(window, scope, interval, http, auth) {
            this._windowService = window;
            this._scope = scope;
            this._interval = interval;
            this._http = http;
            this._authenticationService = auth;
            this._scope.SignIn = $.proxy(this.SignIn, this);
            this._scope.SignOut = $.proxy(this.SignOut, this);
            this._scope.Service = this._authenticationService;
        }
        AuthController.prototype.SignOut = function () {
            var _this = this;
            this._http.post('/auth/signout', null)
                .then(function () { return _this._authenticationService.ValidateAuthState(); });
        };
        AuthController.prototype.SignIn = function (provider) {
            var _this = this;
            var oauth = this._windowService.open('/auth/signin-with-' + provider, '', 'top=50,left=50,status=0,width=800,height=600');
            var checkPopup = this._interval(function () {
                try {
                    if (!oauth || oauth.closed || oauth['SmashLeague:OAuth:Complete']) {
                        _this._interval.cancel(checkPopup);
                        _this._authenticationService.ValidateAuthState();
                        if (oauth && !oauth.closed) {
                            oauth.close();
                        }
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
            '$http',
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
    var Home;
    (function (Home) {
        'use strict';
        var Application = (function () {
            function Application() {
            }
            Application.Config = function (stateProvider) {
                stateProvider.state('!.Anonymous.Home', {
                    url: 'home',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-default"></div>'
                        }
                    }
                });
            };
            return Application;
        })();
        Home.Application = Application;
        Application.Config.$inject = ['$stateProvider'];
        Application.Module = angular.module('SmashLeague.Home', ['ui.router']);
        Application.Module.config(Application.Config);
    })(Home = SmashLeague.Home || (SmashLeague.Home = {}));
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    var Players;
    (function (Players) {
        'use strict';
        var Application = (function () {
            function Application() {
            }
            Application.Config = function (stateProvider) {
                stateProvider.state('!.Anonymous.Players', {
                    url: 'players',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-red"></div>'
                        }
                    }
                });
            };
            return Application;
        })();
        Players.Application = Application;
        Application.Config.$inject = ['$stateProvider'];
        Application.Module = angular.module('SmashLeague.Players', ['ui.router']);
        Application.Module.config(Application.Config);
    })(Players = SmashLeague.Players || (SmashLeague.Players = {}));
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    var Seasons;
    (function (Seasons) {
        'use strict';
        var Application = (function () {
            function Application() {
            }
            Application.Config = function (stateProvider) {
                stateProvider.state('!.Anonymous.Seasons', {
                    url: 'seasons',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-gold"></div>'
                        }
                    }
                });
            };
            return Application;
        })();
        Seasons.Application = Application;
        Application.Config.$inject = ['$stateProvider'];
        Application.Module = angular.module('SmashLeague.Seasons', ['ui.router']);
        Application.Module.config(Application.Config);
    })(Seasons = SmashLeague.Seasons || (SmashLeague.Seasons = {}));
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    var Teams;
    (function (Teams) {
        'use strict';
        var Application = (function () {
            function Application() {
            }
            Application.Config = function (stateProvider) {
                stateProvider.state('!.Anonymous.Teams', {
                    url: 'teams',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-blue"></div>'
                        }
                    }
                });
            };
            return Application;
        })();
        Teams.Application = Application;
        Application.Config.$inject = ['$stateProvider'];
        Application.Module = angular.module('SmashLeague.Teams', ['ui.router']);
        Application.Module.config(Application.Config);
    })(Teams = SmashLeague.Teams || (SmashLeague.Teams = {}));
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
                    response: $.proxy(_this.HandleUnauthorized, _this)
                };
            };
            this._httpProvider.interceptors.push(interceptor);
        };
        AuthenticationServiceProvider.prototype.HandleUnauthorized = function (response) {
            if (this._service !== undefined && response.status == 401) {
                this._service.UnauthorizedResponseCallback();
            }
            return response;
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
        Object.defineProperty(AuthenticationService.prototype, "Battletag", {
            get: function () { return this._battletag; },
            enumerable: true,
            configurable: true
        });
        AuthenticationService.prototype.UnauthorizedResponseCallback = function () {
            this.ValidateAuthState();
        };
        AuthenticationService.prototype.ValidateAuthState = function () {
            var _this = this;
            this._http.get('/auth/authenticate')
                .success(function (data) { return _this.SetAuthState(true, data); })
                .error(function () { return _this.SetAuthState(false); });
        };
        AuthenticationService.prototype.SetAuthState = function (authenticated, battletag) {
            if (battletag === void 0) { battletag = undefined; }
            if (authenticated) {
                this._http.get('/api/profile');
            }
            this._isAuthenticated = authenticated;
            this._battletag = battletag;
        };
        AuthenticationService.$inject = [
            '$http'
        ];
        return AuthenticationService;
    })();
    SmashLeague.AuthenticationService = AuthenticationService;
})(SmashLeague || (SmashLeague = {}));
