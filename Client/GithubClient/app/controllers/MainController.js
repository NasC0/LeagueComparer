(function() {
    var simpleApp = angular.module('GithubViewer');

    var MainController = function($scope, $location, $interval) {
    	var itemsData;
    	var countDownInterval = null;

    	function decrementCountdown() {
    		$scope.countDown--;
    		if ($scope.countDown < 1) {
    			$scope.search($scope.id);
    		}
    	}

    	function startCountdown() {
    		countDownInterval = $interval(decrementCountdown, 1000, $scope.countdown);
    	}

    	function errorOnFetch(reason) {
    		$scope.error = 'Could not fetch data';
    	}

    	$scope.search = function(username) {
    		if (countDownInterval) {
    			$interval.cancel(countDownInterval);
    			$scope.countDown = null;
    		}

            $location.path('/users/' + username);
    	}

    	$scope.countDown = 5;
    	startCountdown();
    };

    simpleApp.controller('MainController', ['$scope', '$location', '$interval', MainController]);
}());