$(document).ready(function() {
	var currentLocation = window.location.href;
	console.log(currentLocation);
	console.log(locationToGo);
	window.location.href = window.locationToGo + '&redirectUrl=' + window.location.href;
});