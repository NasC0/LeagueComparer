(function() {
    var simpleApp = angular.module('LeagueComparer');

    var MainController = function($scope, $location, $interval, items) {
        var itemsData;
        var countDownInterval = null;

        function decrementCountdown() {
            $scope.countDown--;
            if ($scope.countDown < 1) {
                $scope.search($scope.id);
            }
        }

        function startCountdown() {
            countDownInterval = $interval(decrementCountdown, 1000, $scope.countdown);
        }

        function errorOnFetch(reason) {
            $scope.error = 'Could not fetch data';
        }

        $scope.search = function(id) {
            if (countDownInterval) {
                $interval.cancel(countDownInterval);
                $scope.countDown = null;
            }

            $location.path('/items/' + id);
        }

        items.getAllItems()
            .then(function(data) {
                $scope.items = data
            }, function(reason) {
                $scope.error = reason;
            });

        // $scope.countDown = 5;
        // startCountdown();
    };

    simpleApp.controller('MainController', ['$scope', '$location', '$interval', 'items', MainController]);
}());