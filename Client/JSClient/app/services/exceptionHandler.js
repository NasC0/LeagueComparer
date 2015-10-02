(function() {	
	var module = angular.module('LeagueComparer');
	module.config(function($provide) {
		$provide.decorator('$exceptionHandler', function($delegate, $injector) {
			return function(exception, cause) {
				$delegate(exception, cause);

				var alertingService = $injector.get('alertingService');
				alertingService.addDanger(exception.message);
			};
		});
	});
}());