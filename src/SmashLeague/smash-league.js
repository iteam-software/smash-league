var SmashLeague;
(function (SmashLeague) {
    'use strict';
    var smashLeague = angular.module('SmashLeague', []);
    smashLeague.directive('dropdownKeepOpen', SmashLeague.DropdownKeepOpen.Factory);
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
})(SmashLeague || (SmashLeague = {}));
//# sourceMappingURL=smash-league.js.map