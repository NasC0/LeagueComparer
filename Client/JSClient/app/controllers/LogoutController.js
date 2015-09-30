(function() {
	var LogoutController = function($location, currentUser, oauth) {
		oauth.logout()
			 .then(function() {
			 	$location.path('/');
			 });
	}

	var module = angular.module('LeagueComparer');
	module.controller('LogoutController', ['$location', 'currentUser', 'oauth', LogoutController]);
}());