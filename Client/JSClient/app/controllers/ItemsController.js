(function() {
    var simpleApp = angular.module('LeagueComparer');

    var ItemsController = function($scope, items, $routeParams) {
        var itemsData;

        function displayItems(response) {
            $scope.items = [response];
        }

        function errorOnFetch(reason) {
            $scope.error = 'Could not fetch data';
        }

        $scope.id = $routeParams.id;
        items.getItem($scope.id)
            .then(displayItems, errorOnFetch);
    };

    simpleApp.controller('ItemsController', ['$scope', 'items', '$routeParams', ItemsController]);
}());