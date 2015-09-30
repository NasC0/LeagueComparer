(function() {
	var alerts = function(alerting) {
		return {
			restrict: 'AE',
			templateUrl: "app/views/directives/alerts.html",
			scope: true,
			controller: function($scope) {
				$scope.removeAlert = function(alert) {
					alerting.removeAlert(alert);
				};
			},
			link: function(scope) {
				scope.currentAlerts = alerting.currentAlerts
			}
		};
	};

	var module = angular.module('LeagueComparer');
	module.directive('alerts', ['alertingService', alerts]);
}());