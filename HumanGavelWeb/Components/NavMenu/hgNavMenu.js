(function () {

    angular.module('humanGavelUser.app')
        .component("hgNavMenu",
        {
            templateUrl: 'Components/NavMenu/hgNavMenu.html',
            controller: function () {
                var $ctrl = this;

                $ctrl.$currentFilter = 'trending';
            }
        });
})();