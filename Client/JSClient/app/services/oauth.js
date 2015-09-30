(function() {
	var oauth = function($http, $rootScope, formEncode, currentUser, apiUrls) {
		var baseApiAddress = apiUrls.sslApiBase;

		var login = function(username, password) {
			var config = {};
			config = formEncode.getHeader(config);

			var data = formEncode.encodeFormData({
				grant_type: 'password',
				username: username,
				password: password
			});

			var url = baseApiAddress + apiUrls.tokenLoginEndpoint;
			return $http.post(url, data, config)
						.then(function(response) {
							currentUser.setProfile(username, response.data.access_token, true);
							$rootScope.$broadcast('user-authentication');
							return username;
						});
		};

		var logout = function() {
			var url = baseApiAddress + apiUrls.accountEndpoint + '/Logout';
			return $http.post(url)
						.then(function(response) {
							currentUser.logout();
							$rootScope.$broadcast('user-authentication');
							return response.data;
						});
		}

		var register = function(username, password, confirmPassword) {
			var url = baseApiAddress + apiUrls.accountEndpoint + '/Register';
			var data = {
				username: username,
				email: username,
				password: password,
				confirmPassword: confirmPassword
			};

			return $http.post(url, data)
					   	.then(function(response) {
					   		return response.data;
					   	});
		};

		return {
			login: login,
			logout: logout,
			register: register
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('oauth', ['$http', '$rootScope', 'formEncode', 'currentUser', 'apiUrls', oauth]);
}());