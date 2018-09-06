(function () {
    angular
        .module("humanGavelUser.app")
        .directive("hgCase", ngCase);

    function ngCase($location, $rootScope, $location, $interval, $filter, $parse, casesService, $compile) {

        function link(scope, element, attrs) {
            var caseObject = scope.$eval(attrs.case);

            // scope properties.
            scope.$$image = caseObject.image;
            scope.$$name = caseObject.name;
            scope.$$caseEndDate = caseObject.caseEndDate;
            scope.$$case = null;//scopeFromSeries(angular.copy(caseObject), $filter);
            scope.$$showCaseData = false;
            scope.$$selected = false;

            // Show the popup.
            element.click(function () {
                var compiledTemplate = $compile($rootScope.popupTemplate)(scope);
                $.blockUI({
                    message: compiledTemplate,
                    onUnblock: function () {
                        scope.$$showCaseData = scope.$$selected = false;
                        scope.$digest();
                    }
                });
                scope.$$showCaseData = scope.$$selected = true;
                scope.$digest();
            });
        }

        return {
            link: link,
            scope: true,
            templateUrl: 'templates/case.html'
        }
    }
})();