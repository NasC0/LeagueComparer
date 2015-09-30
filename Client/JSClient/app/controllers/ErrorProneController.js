(function() {
	var ErrorProneController = function($scope, alertingService) {
		$scope.alertType = '';
		$scope.alertMessage = '';
		$scope.alertTypes = alertingService.alertTypes;

		$scope.addAlert = function() {
			alertingService.addAlert($scope.alertType, $scope.alertMessage);
		};
	};

	var module = angular.module('LeagueComparer');
	module.controller('ErrorProneController', ['$scope', 'alertingService', ErrorProneController]);
}());