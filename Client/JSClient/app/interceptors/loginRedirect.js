(function() {
	var loginRedirect = function($q, $location) {
		var lastPath = '/';

		var responseError = function(response) {
			if (response.status === 401) {
				lastPath = $location.path();
				$location.path('/login');
			}

			return $q.reject(response);
		};

		var redirectPostLogin = function() {
			$location.path(lastPath);
			lastPath = '/';
		};

		return {
			responseError: responseError,
			redirectPostLogin: redirectPostLogin
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('loginRedirect', ['$q', '$location', loginRedirect]);
	module.config(function($httpProvider) {
		$httpProvider.interceptors.push('loginRedirect');
	});
}());