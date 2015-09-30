(function() {
    var leagueApp = angular.module('LeagueComparer');
    var LoginController = function($scope, $rootScope, $location, oauth, currentUser, loginRedirect) {
        var model = this;
        model.username = '';
        model.password = '';
        model.user = currentUser.profile();

        model.login = function(form) {
            if (form.$valid) {
                oauth.login(model.username, model.password)
                    .then(function() {
                        loginRedirect.redirectPostLogin();
                    });
                // .catch(alerting.errorHandler('Could not login'));

                model.password = model.username = '';
                form.$setUntouched();
            }
        };

        model.logout = function() {
    		$location.path('/logout');
        };

        $rootScope.$on('user-authentication', function() {
            model.user = currentUser.profile();
        });
    };

    leagueApp.controller('LoginController', ['$scope', '$rootScope', '$location', 'oauth', 'currentUser', 'loginRedirect', LoginController]);
}());