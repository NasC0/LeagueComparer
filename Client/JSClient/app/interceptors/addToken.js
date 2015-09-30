(function() {
	var addToken = function(currentUser, $q) {
		var request = function(config) {
			var currentProfile = currentUser.profile();
			if (currentProfile.loggedIn) {
				config.headers.Authorization = 'Bearer ' + currentProfile.token;
			}

			return $q.when(config);
		};

		return {
			request: request
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('addToken', ['currentUser', '$q', addToken]);
	module.config(function($httpProvider) {
		$httpProvider.interceptors.push('addToken');
	});
}());