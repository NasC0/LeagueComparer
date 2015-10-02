(function() {
	var module = angular.module('LeagueComparer');
	module.config(function($provide) {
		$provide.decorator('$interpolate', function($delegate, $injector) {
			var serviceWrapper = function() {
				var bindingFunction = $delegate.apply(this, arguments);
				if (angular.isFunction(bindingFunction) && arguments[0]) {
					return bindingWrapper(bindingFunction, arguments[0].trim());
				}

				return bindingFunction;
			};

			var bindingWrapper = function(bindingFunction, bindingExpression) {
				return function() {
					var result = bindingFunction.apply(this, arguments)
					var trimmedResult = result.trim();
					var loggingService = $injector.get('$log');
					var log = trimmedResult ? loggingService.info : loggingService.warn;
					log.call(loggingService, bindingExpression + " = " + trimmedResult);

					return result;
				};
			};

			angular.extend(serviceWrapper, $delegate);
			return serviceWrapper;
		});
	});
}());