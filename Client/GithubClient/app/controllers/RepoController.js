(function() {
	var app = angular.module('GithubViewer');

	var RepoController = function($scope, $routeParams, github) {
		function repoDetailsSuccess(data) {
			$scope.repo = data;
		}

		function repoDetailsError(reason) {
			$scope.error = reason;
		}

		$scope.username = $routeParams.username;
		$scope.repoName = $routeParams.repo;

		github.getRepoDetails($scope.username, $scope.repoName)
				.then(repoDetailsSuccess, repoDetailsError)
	};

	app.controller('RepoController', ['$scope', '$routeParams', 'github', RepoController]);
}());