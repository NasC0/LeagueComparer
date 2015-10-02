(function() {
	var spinner = function(requestCounter) {
		return {
			restrict: 'EA',
			transclude: true,
			scope: {},
			template: '<ng-transclude ng-show="requestCount"></ng-transclude>',
			link: function(scope) {
				scope.$watch(function() {
					return requestCounter.getRequestCount();
				}, function(requestCount) {
					scope.requestCount = requestCount;
				});
			}
		}
	};

	var module = angular.module('LeagueComparer');
	module.directive('spinner', ['requestCounter', spinner]);
}());