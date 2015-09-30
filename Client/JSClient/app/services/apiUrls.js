(function() {
	var apiUrls = function() {
		var sslApiBase = 'https://localhost:44301';
		var insecureApiBase = 'http://localhost:57908';
		var tokenLoginEndpoint = '/token';
		var accountEndpoint = '/api/Account';
		var itemsEndpoint = '/api/Items';

		return {
			get sslApiBase() {
				return sslApiBase;
			},
			get insecureApiBase() {
				return insecureApiBase;
			},
			get tokenLoginEndpoint() {
				return tokenLoginEndpoint;
			},
			get accountEndpoint() {
				return accountEndpoint;
			},
			get itemsEndpoint() {
				return itemsEndpoint;
			}
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('apiUrls', apiUrls);
}());