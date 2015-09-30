(function() {
	var github = function($http) {
		var getUser = function(username) {
			var promise = $http.get('https://api.github.com/users/' + username)
							.then(function(response) {
								return response.data;
							});
			return promise;
		};

		var getRepos = function(username) {
			var promise = $http.get('https://api.github.com/users/' + username + '/repos')
							.then(function(response) {
								return response.data
							});
			return promise;
		}

		var getRepoDetails = function(username, repo) {
			var promise = $http.get('https://api.github.com/repos/' + username + '/' + repo)
							.then(function(response) {
								return response.data;
							});
			return promise;
		};

		return {
			getUser: getUser,
			getRepos: getRepos,
			getRepoDetails: getRepoDetails
		};
	};

	var module = angular.module('GithubViewer');
	module.factory('github', github);
}());