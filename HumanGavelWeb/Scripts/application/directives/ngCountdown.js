(function () {
    angular.module("humanGavelUser.app")
        .directive("ngCountdown", ngCountdown);

    ngCountdown.$inject = ["$rootScope", "$interval"];
    function ngCountdown($rootScope, $interval) {

        function link(scope, element, attrs) {
            function updateTime() {
                var d2 = moment(scope.endDate, "MM-DD-YYYY");
                var diff = moment.duration(d2.diff(moment()));
                                           
                var months = diff.asMonths();
                var days = Math.floor(diff.asDays());
                var hours = diff.hours();
                var mins = diff.minutes();
                var secs = diff.seconds();
                var remaining = "";
                if(days > 0) {
                    remaining += days + "d ";
                }
                if(diff.asHours() > 0) {
                    remaining += hours + "h ";
                }
                if(diff.asMinutes() > 0) {
                    remaining += mins + "m ";
                }
                if(diff.asSeconds() > 0) {
                    remaining += secs + "s";
                }
                
                if (diff.milliseconds() >= 0) {
                    scope.content = remaining + " remaining";
                } else {

                    // can be moved down.
                    scope.content = "Voting Closed";
                    $interval.cancel(scope.timer);
                }
            }

            element.on("$destroy", function () {
                $interval.cancel(scope.timer);
            });

            scope.timer = $interval(updateTime, 1000);
            updateTime();
        }

        return {
            link: link,
            templateUrl: 'templates/countdown.html',
            scope: {
                endDate: '@'
            }
        };
    }

})();