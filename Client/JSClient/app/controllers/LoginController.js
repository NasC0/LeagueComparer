(function() {
    var leagueApp = angular.module('LeagueComparer');
    var LoginController = function($scope, $rootScope, $location, $http, $window, oauth, currentUser, loginRedirect, alertingService, apiUrls) {
        var authenticationHandlerUrl = $location.protocol() + '://' + $location.host() + ':' + $location.port() + '/authcomplete.html';
        var popupParameters = 'width=300,height=400,scrollbars=no,resizable=no,location=no,menubar=no,toolbar=no;'

        var model = this;
        model.username = '';
        model.password = '';
        model.user = currentUser.profile();

        $window.$scope = model;

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

        model.externalLogin = function(externalLogin) {
            var externalLoginUrl = apiUrls.sslApiBase + externalLogin.Url;
            $window.open(externalLoginUrl, 'login', popupParameters);
        };

        model.externalLoginComplete = function(fragment) {
            if (fragment['haslocalaccount'] === 'False') {
                currentUser.setExternalLoginInfo(fragment);
                $location.path('/associate')
            } else {

            }
        };

        $rootScope.$on('user-authentication', function() {
            model.user = currentUser.profile();
        });

        var availableLoginsEndpoint = apiUrls.sslApiBase + apiUrls.accountEndpoint + '/ExternalLogins?returnUrl=%2F&generateState=true';
        $http.get(availableLoginsEndpoint)
            .then(function(response) {
                var responseData = response.data;
                angular.forEach(responseData, function(value) {
                    value.Url = value.Url + '&redirectUrl=' + authenticationHandlerUrl;
                });

                model.availableLogins = responseData;
            })
            .catch(alertingService.errorHandler('Could not fetch data!'));
    };

    leagueApp.controller('LoginController', ['$scope', '$rootScope', '$location', '$http', '$window', 'oauth', 'currentUser', 'loginRedirect', 'alertingService', 'apiUrls', LoginController]);
}());