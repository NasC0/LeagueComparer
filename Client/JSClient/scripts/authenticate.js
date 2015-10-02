(function() {
    function getUrlParametersAfterHash(url) {
        var hashIndex = url.indexOf('#');
        var allParameters = url.substr(hashIndex + 1);
        return getParameters(allParameters);
    }

    function getParameters(parametersString) {
        var pairDelimeter = '&';
        var parameterDelimeter = '=';
        var parametersSplit = parametersString.split(pairDelimeter);
        var index = 0;
        var parameters = {};

        for (; index < parametersSplit.length; index++) {
            var currentParameterDelimeterIndex = parametersSplit[index].indexOf(parameterDelimeter);
            var currentParameterKey = parametersSplit[index].substring(0, currentParameterDelimeterIndex);
            var currentParameterValue = parametersSplit[index].substring(currentParameterDelimeterIndex + 1, parametersSplit[index].length);
            parameters[currentParameterKey] = currentParameterValue;
        }

        return parameters;
    }

    var currentUrl = window.location.href;
    var parameters = getUrlParametersAfterHash(currentUrl);
    window.opener.$scope.externalLoginComplete(parameters);

    window.close();
}());