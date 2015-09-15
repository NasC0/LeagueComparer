$(document).ready(function() {
	var loginScriptsLocation = 'app/logins/';
	var popupParameters = 'width=300,height=400,scrollbars=no,resizable=no,location=no,menubar=no,toolbar=no';
	var baseHost = 'https://localhost:44301';
	var facebookLoginButton = $('#facebook-login');
	var loginRequestsElement = $('#login-requests');

	facebookLoginButton.click(function() {
		var requestEndpoint = '/api/Account/ExternalLogins?returnUrl=%2F&generateState=true';
		var fullEndpoint = baseHost + requestEndpoint;

		$.get(fullEndpoint, function(response) {
		})
		.done(function(response) {
			console.dir(response);
			var btnTemplate = $('<button />')
								.addClass('login-button');
			var currentUrl;
			var currentProvider;
			var currentButton;
			response.forEach(function(item, index) {

				currentUrl = item['Url'];
				currentButton = btnTemplate.clone();
				currentButton.text(item['Name']);

				currentButton.click(function() {
					var currentPopup = window.open(loginScriptsLocation + item['Name'].toLowerCase() + '-login.html', 'login', popupParameters);
					var locationUrl = baseHost + currentUrl;

					setTimeout(function() {
						console.log(localStorage.getItem('access_token'));
					}, 10000);

					currentPopup.locationToGo = locationUrl;
				});

				loginRequestsElement.append(currentButton);
			})
		})
		.fail(function(response) {
			console.dir(response);
		});
	});
});