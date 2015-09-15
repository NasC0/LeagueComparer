$(document).ready(function () {
    var loginRedirect = 'http://localhost:57667/app/logins/authenticate.html';
    var loginScriptsLocation = 'app/logins/';
    var popupParameters = 'width=300,height=400,scrollbars=no,resizable=no,location=no,menubar=no,toolbar=no';
    var baseHost = 'https://localhost:44301';
    var facebookLoginButton = $('#facebook-login');
    var loginRequestsElement = $('#login-requests');
    var requestEndpoint = '/api/Account/ExternalLogins?returnUrl=%2F&generateState=true';
    var fullEndpoint = baseHost + requestEndpoint;

    $.get(fullEndpoint, function (response) {
    })
    .done(function (response) {
        console.dir(response);
        var btnTemplate = $('<button />').addClass('login-button');

        $.each(response, function (index, item) {
            var currentUrl = item['Url'];
            var currentButton = btnTemplate.clone();
            currentButton.text(item['Name']);

            currentButton.click(function () {
                var locationUrl = baseHost + currentUrl + '&redirectUrl=' + loginRedirect;
                var currentPopup = window.open(locationUrl, 'login', popupParameters);

                setTimeout(function () {
                    console.log(localStorage.getItem('access_token'));
                }, 10000);
            });

            loginRequestsElement.append(currentButton);
        });
    })
    .fail(function (response) {
        console.dir(response);
    });
});