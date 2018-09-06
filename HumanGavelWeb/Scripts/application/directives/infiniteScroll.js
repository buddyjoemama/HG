(function () {
    angular.module("humanGavelUser.app")
        .directive("infiniteScroll", infiniteScroll);

    infiniteScroll.$inject = ["$rootScope", "casesService"];
    function infiniteScroll($rootScope, casesService) {

        function compile(element, attrs) {
            var parentElement = element.parent();
            var itemsPerRow = parseInt(attrs.count);
            var currentIndex = 0;
            var row = $('<div class="row"></div>');

            parentElement.append(row);

            return function ($scope, $element, $att, $ctrl, $transclude) {               

                // Handle filter changes
                $scope.$watch("$items", function (collection, evt, $scope) {

                    if (!collection)
                        return;

                    // Create the items.
                    for ($scope.$currentIndex; $scope.$currentIndex < collection.length; $scope.$currentIndex++) {

                        // Create a row if we need to.
                        if ($scope.$currentIndex > 0 && ($scope.$currentIndex % itemsPerRow) == 0) {
                            var space = $("<div class='case-md-space'>&nbsp;</div>");
                            row.append(space);
                        }

                        // Add the element to the row, while updating the parent to be a col-sm-4.
                        $transclude(function (clone, scope) {
                            scope.item = collection[$scope.$currentIndex];
                            clone.addClass("col-sm-4");
                            row.append(clone);
                        });
                    }
                });
            }
        }

        function controller($scope) {
            $scope.$currentIndex = 0;
            $scope.$items = [];
            $scope.$currentFilter = "trending";

            // Update the data when the user changes the filter.
            $scope.$on("filterChanged", function (event, key) {

                if ($scope.$currentFilter != key) {
                    console.log("Changing filter to: " + key);

                    $scope.$currentFilter = key;                    
                    casesService.getSampleData(key, function (res) {

                    });
                }
            });

            // Get the initial set of cases.
            $scope.init = function () {
                //casesService.getSampleData($scope.$currentFilter, function (res) {
                //    $scope.$items = res.data;
                //});
                $scope.$items = casesService.getSampleData();
            }

            $scope.init();
        }

        return {
            compile: compile,
            scope: true,
            multiElement: true,
            priority: 1000,
            transclude: 'element',
            controller: controller
        }
    }
})();