(function () {

    angular.module('humanGavelUser.app')
        .directive("ngChart", ngChart)
        .directive("ngChartTemplate", ngChartTemplate);

    ngChart.$inject = ['$rootScope', '$filter'];
    function ngChart($rootScope, $filter) {

        function link(scope, element, attrs) {
            scope.$chartData = scope.$eval(attrs.series);
        }

        return {
            link: link,
            scope: true,
            templateUrl: 'templates/chart.html'
        }
    }

    //
    // Chart template directive allows us to crate a chart from a basic template.
    // Pretty much an ng-repeat but this directive does some calculations.
    //
    ngChartTemplate.$inject = ['$rootScope', '$filter'];
    function ngChartTemplate($rootScope, $filter) {

        function link(scope, element, attrs) {
            scope.$watch(attrs.series, function (val) {
                scope.$chartData = scopeFromSeries(angular.copy(val), $filter);
            });
        }

        return {
            link: link,
            scope: true
        }
    }
})();