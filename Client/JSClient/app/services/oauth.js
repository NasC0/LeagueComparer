(function() {
    var oauth = function($http, $rootScope, $q, formEncode, currentUser, apiUrls) {
        var LOCAL_LOGIN_PROVIDER = 'local';
        var baseApiAddress = apiUrls.sslApiBase;

        var login = function(username, password) {
            var config = {};
            config = formEncode.getHeader(config);

            var data = formEncode.encodeFormData({
                grant_type: 'password',
                username: username,
                password: password
            });

            var url = baseApiAddress + apiUrls.tokenLoginEndpoint;
            return $http.post(url, data, config)
                .then(function(response) {
                    currentUser.setProfile(username, response.data.access_token, LOCAL_LOGIN_PROVIDER);
                    $rootScope.$broadcast('user-authentication');
                    return username;
                });
        };

        var logout = function() {
            var url = baseApiAddress + apiUrls.accountEndpoint + '/Logout';
            var deferred = $q.defer();
            var currentProfile = currentUser.getProfile();

            if (currentProfile.provider === LOCAL_LOGIN_PROVIDER) {
                return $http.post(url)
                    .then(function(response) {
                        deferred.resolve(response.data);
                    });
            } else {
            	deferred.resolve();
            }

            deferred.promise
            		.then(function(response) {
                        currentUser.logout();
                        $rootScope.$broadcast('user-authentication');
            		});
        }

        var register = function(username, password, confirmPassword) {
            var url = baseApiAddress + apiUrls.accountEndpoint + '/Register';
            var data = {
                username: username,
                email: username,
                password: password,
                confirmPassword: confirmPassword
            };

            return $http.post(url, data)
                .then(function(response) {
                    // currentUser.setProfile(response.data['userName'], response.data['access_token'])
                    // $rootScope.$broadcast('user-authentication');
                    return response.data;
                });
        };

        var registerExternal = function(data) {
            var url = baseApiAddress + apiUrls.accountEndpoint + '/RegisterExternal';
            var data = {
                email: data.email,
                username: data.email,
                provider: data.provider,
                externalAccessToken: data.externalAccessToken
            };

            return $http.post(url, data)
                .then(function(response) {
                    return response.data;
                });
        };

        return {
            login: login,
            logout: logout,
            register: register,
            registerExternal: registerExternal
        };
    };

    var module = angular.module('LeagueComparer');
    module.factory('oauth', ['$http', '$rootScope', '$q', 'formEncode', 'currentUser', 'apiUrls', oauth]);
}());