(function () {
    angular
        .module("humanGavelUser.app")
        .directive("hgCase", ngCase);

    function ngCase($location, $rootScope, $location, $uibModal, $compile) {

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
                $uibModal.open({
                    component: 'hgCaseDetails',
                    resolve: {
                        case: function () {
                            return caseObject;
                        }
                    }
                });
            });
        }

        return {
            link: link,
            scope: true,
            templateUrl: 'templates/case.html'
        }
    }
})(); 