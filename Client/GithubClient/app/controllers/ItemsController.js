(function() {
    var simpleApp = angular.module('GithubViewer');

    var ItemsController = function($scope, github, $routeParams) {
    	var itemsData;

        function displayRepos(data) {
            $scope.repos = data;
        }

    	function displayItems(data) {
            $scope.user = data;

            github.getRepos($scope.username)
                    .then(displayRepos, errorOnFetch);
    	}

    	function errorOnFetch(reason) {
    		$scope.error = 'Could not fetch data';
    	}

        $scope.username = $routeParams.username;
        $scope.sortOrder = '-stargazers_count';

        github.getUser($scope.username)
            .then(displayItems, errorOnFetch);
    };

    simpleApp.controller('ItemsController', ['$scope', 'github', '$routeParams', ItemsController]);
}());