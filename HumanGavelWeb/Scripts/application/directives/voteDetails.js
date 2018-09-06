(function () {
    angular.module("humanGavelUser.app")
        .directive("voteDetails", voteDetails);

    voteDetails.$inject = ["$rootScope"];
    function voteDetails($rootScope) {

        function link(scope, element, attr) {
            scope.$$choices = [];
            scope.$watch(attr.series, function (val) {
                scope.$$choices = val.voteData;
                scope.$$id = val.caseId;
            });
        }

        return {
            link: link,
            templateUrl: 'templates/voteChoices.html'
        };
    }
})();