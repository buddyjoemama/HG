(function () {
    angular.module("humanGavelUser.app")
        .directive("ngImage", ngImage);

    ngImage.$inject = ["$interpolate", "$compile"];
    function ngImage($interpolate, $compile) {

        function link(scope, element, attr) {
            scope.$watch(attr.ngImage, function (val) {
                element.css("backgroundImage", "url(" + val + ")");
            });
        }

        return {
            link: link
        }
    }
})();