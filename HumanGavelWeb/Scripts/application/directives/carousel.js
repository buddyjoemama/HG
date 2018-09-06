(function() {
    'use strict';

    angular
        .module('humanGavelUser.app')
        .directive('carousel', carousel);

    function carousel ($window, casesService) {
        // Usage:
        //     <carousel></carousel>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'E',
            templateUrl: 'templates/carousel.html',
            transclude: true
        };
        return directive;

        function link(scope, element, attrs) {

            scope.slides = casesService.getSampleData();
        }
    }

})();