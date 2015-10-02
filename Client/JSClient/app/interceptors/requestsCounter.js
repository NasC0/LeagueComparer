(function() {
	var requestCounter = function($q) {
		var requests = 0;

		var request = function(config) {
			requests++;
			return $q.when(config);
		};

		var requestError = function(error) {
			requests--;
			return $q.reject(error);
		};

		var response = function(response) {
			requests--;
			return $q.when(response);
		};

		var responseError = function(error) {
			requests--;
			return $q.reject(error);
		};

		var getRequestCount = function() {
			return requests;
		};

		return {
			request: request,
			requestError: requestError,
			response: response,
			responseError: responseError,
			getRequestCount: getRequestCount
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('requestCounter', requestCounter);
	module.config(function ($httpProvider) {
		$httpProvider.interceptors.push('requestCounter');
	});
}());