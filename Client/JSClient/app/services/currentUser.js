(function() {
    var currentUser = function(localStorage) {
        var USERKEY = 'utoken';

        var setProfile = function(username, token, isLoggedIn) {
            var profile = {};
            profile.username = username;
            profile.token = token;

            localStorage.add(USERKEY, profile);
        };

        var logout = function() {
        	localStorage.remove(USERKEY);
            // profile = getProfile();
        };

        var getProfile = function() {
            var user = {
                username: '',
                token: '',
                get loggedIn() {
                    return this.token;
                }
            };

            var localUser = localStorage.get(USERKEY);
            if (localUser) {
            	user.username = localUser.username;
            	user.token = localUser.token
            }

            return user;
        };

        return {
            profile: getProfile,
            setProfile: setProfile,
            logout: logout
        };
    };

    var module = angular.module('LeagueComparer');
    module.factory('currentUser', ['localStorage', currentUser]);
}());