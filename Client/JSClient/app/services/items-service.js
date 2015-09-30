(function() {
	var items = function($http, apiUrls) {
		var apiEndpoint = apiUrls.sslApiBase + apiUrls.itemsEndpoint;

		var getItem = function(itemId) {
			var promise = $http.get(apiEndpoint + '/' + itemId)
							.then(function(response) {
								return response.data;
							});
			return promise;
		};

		var getAllItems = function() {
			var promise = $http.get(apiEndpoint)
							.then(function(response) {
								return response.data;
							});

			return promise;
		}

		return {
			getItem: getItem,
			getAllItems: getAllItems
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('items', ['$http', 'apiUrls', items]);
}());