(function() {
	var ErrorProneController = function($scope, alertingService, $http, apiUrls) {
		$scope.alertType = '';
		$scope.alertMessage = '';
		$scope.alertTypes = alertingService.alertTypes;

		$scope.addAlert = function() {
			alertingService.addAlert($scope.alertType, $scope.alertMessage);
		};

		$scope.createException = function() {
			throw new Error("Something has gone terribly wrong.");
		};

		var url = apiUrls.sslApiBase + '/api/Slow/';
		$http.get(url)
			 .then(function(response) {
			 	return response.data;
			 })
			 .catch(alertingService.errorHandler('Failed to load data!'));
	};

	var module = angular.module('LeagueComparer');
	module.controller('ErrorProneController', ['$scope', 'alertingService', '$http', 'apiUrls', ErrorProneController]);
}());