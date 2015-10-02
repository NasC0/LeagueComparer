(function() {
	var AssociateController = function($scope, $http, currentUser, loginRedirect, oauth, alertingService) {
		$scope.externalLoginInfo = currentUser.getExternalLoginInfo();
		$scope.email = '';
		$scope.associateAccount = function() {
			$scope.externalLoginInfo.email = $scope.email;
			oauth.registerExternal($scope.externalLoginInfo)
				 .then(function(response) {
                    currentUser.setProfile(response['userName'], response['access_token'], $scope.externalLoginInfo.provider);
				 	alertingService.addSuccess('Registered!');
				 	loginRedirect.redirectPostLogin();
				 }, function(error) {
				 	alertingService.addDanger('Failed to register!');
				 });
		};
	};

	var module = angular.module('LeagueComparer');
	module.controller('AssociateController', ['$scope', '$http', 'currentUser', 'loginRedirect', 'oauth', 'alertingService', AssociateController]);
}());