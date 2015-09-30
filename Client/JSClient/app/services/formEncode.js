(function() {
	var formEncode = function() {
		var encodeFormData = function(data) {
			var pairs = [];
			for (var name in data) {
				pairs.push(encodeURIComponent(name) + '=' + encodeURIComponent(data[name]));
			}

			return pairs.join('&').replace(/%20/g, '+');
		};

		var getHeader = function(config) {
			if (config.headers === undefined) {
				config.headers = {};
			}

			config.headers['Content-Type'] = 'application/x-www-form-urlencoded';
			return config;
		};

		return {
			encodeFormData: encodeFormData,
			getHeader: getHeader
		};
	};

	var module = angular.module('LeagueComparer');
	module.factory('formEncode', [formEncode]);
}());