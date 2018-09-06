(function () {

    angular.module('humanGavelUser.app')
        .directive("navMenu", navMenu);

    navMenu.$inject = ['$rootScope', '$filter'];
    function navMenu($rootScope, $filter) {

        var rootScope = $rootScope;

        function link(scope, element, attrs) {
            scope.$currentFilter = "trending";

            $(element).find("li").each(function (e) {
                $(this).click(function (d) {
                    
                    // Update the selected.
                    scope.$currentFilter = d.currentTarget.getAttribute("id");
                    scope.$apply();

                    // Broadcast
                    rootScope.$broadcast("filterChanged", scope.$currentFilter);

                    return false;
                });
            });

            // Silly way to update the other instances of the directive. Might be a better way to do it.
            scope.$on("filterChanged", function (event, key) {
                scope.$currentFilter = key;
            });
        }

        return {
            link: link,
            scope: {},
            templateUrl: 'templates/navMenu.html'
        }
    }
})();