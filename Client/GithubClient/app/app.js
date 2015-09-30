(function() {
	var app = angular.module('GithubViewer', ['ngRoute']);

	app.config(['$routeProvider', function($routeProvider) {
		$routeProvider
			.when('/main', {
				templateUrl: 'app/views/main.html',
				controller: 'MainController'
			})
			.when('/users/:username', {
				templateUrl: 'app/views/items.html',
				controller: 'ItemsController'
			})
			.when('/repos/:username/:repo', {
				templateUrl: 'app/views/repo.html',
				controller: 'RepoController'
			})
			.otherwise({
				redirectTo: '/main'
			});
	}]);
}());