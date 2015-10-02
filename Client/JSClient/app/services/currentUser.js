(function() {
    var currentUser = function(localStorage) {
        var USERKEY = 'utoken';

        var externalLoginInfo = {
            externalAccessToken: '',
            provider: '',
            hasLocalAccount: '',
            externalUserName: ''
        };

        var setProfile = function(username, token, provider) {
            var profile = {};
            profile.username = username;
            profile.token = token;
            profile.provider = provider;

            localStorage.add(USERKEY, profile);
        };

        var logout = function() {
            localStorage.remove(USERKEY);
            // profile = getProfile();
        };

        var getExternalLoginInfo = function() {
            return externalLoginInfo;
        };

        var setExternalLoginInfo = function(loginInfo) {
            externalLoginInfo.externalAccessToken = loginInfo['external_access_token'];
            externalLoginInfo.provider = loginInfo['provider'];
            externalLoginInfo.hasLocalAccount = loginInfo['haslocalaccount'];
            externalLoginInfo.externalUserName = loginInfo['external_user_name'];
        };

        var removeExternalLoginInfo = function(loginInfo) {
            externalLoginInfo.externalAccessToken = '';
            externalLoginInfo.provider = '';
            externalLoginInfo.hasLocalAccount = '';
            externalLoginInfo.externalUserName = '';
        };

        var getProfile = function() {
            var user = {
                username: '',
                token: '',
                provider: '',
                get loggedIn() {
                    return this.token;
                }
            };

            var localUser = localStorage.get(USERKEY);
            if (localUser) {
                user.username = localUser.username;
                user.token = localUser.token;
                user.provider = localUser.provider;
            }

            return user;
        };

        return {
            profile: getProfile,
            setProfile: setProfile,
            logout: logout,
            getExternalLoginInfo: getExternalLoginInfo,
            setExternalLoginInfo: setExternalLoginInfo,
            removeExternalLoginInfo: removeExternalLoginInfo
        };
    };

    var module = angular.module('LeagueComparer');
    module.factory('currentUser', ['localStorage', currentUser]);
}());