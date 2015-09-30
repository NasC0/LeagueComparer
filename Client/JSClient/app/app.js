(function() {
	var app = angular.module('LeagueComparer', ['ngRoute', 'ngSanitize', 'ui.bootstrap']);

	app.config(['$routeProvider', function($routeProvider) {
		$routeProvider
			.when('/main', {
				templateUrl: 'app/views/main.html',
				controller: 'MainController'
			})
			.when('/items', {
				templateUrl: 'app/views/items.html',
				controller: 'ItemsController'
			})
			.when('/items/:id', {
				templateUrl: 'app/views/items.html',
				controller: 'ItemsController'
			})
			.when('/login', {
				templateUrl: 'app/views/login-form.html',
				controller: 'LoginController'
			})
			.when('/logout', {
				template: '',
				controller: 'LogoutController'
			})
			.when('/register', {
				templateUrl: 'app/views/register-form.html',
				controller: 'RegisterController'
			})
			.when('/errors', {
				templateUrl: 'app/views/errors.html',
				controller: 'ErrorProneController'
			})
			.otherwise({
				redirectTo: '/main'
			});
	}]);
}());