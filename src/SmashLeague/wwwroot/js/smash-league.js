var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var Application = (function () {
        function Application() {
        }
        Application.Config = function (auth, stateProvider) {
            auth.AddUnauthorizedResponseCallback();
        };
        Application.Run = function (scope, authService, stateService, location) {
            scope.Service = authService;
            scope.State = stateService;
            if (location.path() === '' || location.path() === '/') {
                location.path('/home');
            }
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
            var oauth = this._windowService.open('/auth/signin-with-' + provider, '', 'top=50,left=50,status=0,width=800,height=680');
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
    'use strict';
    var SearchNav = (function () {
        function SearchNav() {
            this.restrict = 'E';
            this.scope = { entity: '=entity' };
            this.templateUrl = '/partial/search-nav';
        }
        Object.defineProperty(SearchNav, "Factory", {
            get: function () {
                var directive = function () {
                    return new SearchNav();
                };
                directive.$inject = SearchNav.$inject;
                return directive;
            },
            enumerable: true,
            configurable: true
        });
        return SearchNav;
    })();
    SmashLeague.SearchNav = SearchNav;
    SmashLeague.Application.Module.directive('searchNav', SearchNav.Factory);
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
                stateProvider.state('Home', {
                    url: '/home',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-default"></div>'
                        },
                        'Content': {
                            templateUrl: '/home/content',
                            controller: 'HomeController'
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
    var Home;
    (function (Home) {
        'use strict';
        var HomeController = (function () {
            function HomeController(http, scope) {
                this._http = http;
                this._scope = scope;
                this.LoadMatches(0, 10);
            }
            HomeController.prototype.LoadMatches = function (start, top) {
                var _this = this;
                this._http.get('/api/match?' + $.param({ start: start, top: top }))
                    .success(function (matches) { return _this._scope.Matches = matches; });
            };
            HomeController.$inject = [
                '$http',
                '$scope'
            ];
            return HomeController;
        })();
        Home.HomeController = HomeController;
        Home.Application.Module.controller('HomeController', HomeController);
    })(Home = SmashLeague.Home || (SmashLeague.Home = {}));
})(SmashLeague || (SmashLeague = {}));
var SmashLeague;
(function (SmashLeague) {
    var Home;
    (function (Home) {
        'use strict';
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
                stateProvider.state('Players', {
                    url: '/players',
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
                stateProvider.state('Seasons', {
                    url: '/seasons',
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
                stateProvider.state('Teams', {
                    url: '/teams',
                    views: {
                        'Banner': {
                            template: '<div class="banner banner-blue"></div>'
                        },
                        'Content': {
                            templateUrl: '/teams/content'
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
        AuthenticationService.prototype.SetAuthState = function (success, data) {
            if (data === void 0) { data = undefined; }
            if (success) {
                this._isAuthenticated = data.Authenticated;
                this._battletag = data.Battletag;
            }
            else {
                this._isAuthenticated = false;
                this._battletag = undefined;
            }
        };
        AuthenticationService.$inject = [
            '$http'
        ];
        return AuthenticationService;
    })();
    SmashLeague.AuthenticationService = AuthenticationService;
})(SmashLeague || (SmashLeague = {}));
