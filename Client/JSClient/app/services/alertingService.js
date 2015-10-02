(function() {
	var alertingService = function($timeout) {
		var WARNING_TYPE = 'warning';
		var DANGER_TYPE = 'danger';
		var INFO_TYPE = 'info';
		var SUCCESS_TYPE = 'success';

		var currentAlerts = [];
		var alertTypes = [
			WARNING_TYPE,
			DANGER_TYPE,
			INFO_TYPE,
			SUCCESS_TYPE
		];

		var addWarning = function(message) {
			addAlert(WARNING_TYPE, message);
		};

		var addDanger = function(message) {
			addAlert(DANGER_TYPE, message);
		};

		var addInfo = function(message) {
			addAlert(INFO_TYPE, message);
		};

		var addSuccess = function(message) {
			addAlert(SUCCESS_TYPE, message);
		};

		var addAlert = function(type, message) {
			var alert = {
				type: type,
				message: message
			};

			currentAlerts.push(alert);

			$timeout(function() {
				removeAlert(alert);
			}, 10000)
		};

		var removeAlert = function(alert) {
			for (var i = 0; i < currentAlerts.length; i++) {
				if (currentAlerts[i] === alert) {
					currentAlerts.splice(i, 1);
					break;
				}
			}
		};

		var errorHandler = function(description) {
			return function() {
				addDanger(description);
			};
		};

		return {
			addAlert: addAlert,
			addWarning: addWarning,
			addDanger: addDanger,
			addInfo: addInfo,
			addSuccess: addSuccess,
			currentAlerts: currentAlerts,
			alertTypes: alertTypes,
			removeAlert: removeAlert,
			errorHandler: errorHandler
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('alertingService', ['$timeout', alertingService]);
}());