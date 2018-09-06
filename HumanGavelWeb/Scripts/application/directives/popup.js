(function () {
    angular.module("humanGavelUser.app")
        .directive("popup", popup);

    popup.$inject = ["$rootScope"];
    function popup($rootScope) {

        function link(scope, element, attr) {
            scope.hello = "test";
        }

        return {
            link: link,
            scope: true
        }
    }
})()