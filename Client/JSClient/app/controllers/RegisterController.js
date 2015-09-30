(function() {
	var RegisterController = function($scope, $location, oauth) {
		$scope.register = function() {
			oauth.register($scope.username, $scope.password, $scope.confirmPassword)
			     .then(function(successData) {
			     	$location.path('/login');
			     }, function(errorData) {
			     	console.log(errorData);
			     });
		};
    };

	var module = angular.module('LeagueComparer');
	module.controller('RegisterController', ['$scope', '$location', 'oauth', RegisterController]);
}());