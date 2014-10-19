campusNextApp.service('ProductService', function () {
    this.name = function (category) {
        return "MY";
    }
});

campusNextApp.service('TokenService',['$http', function ($http) {
    this.accessToken = sessionStorage.getItem("accessToken");
    this.setAuthorizationHeader = function () {
        if(this.accessToken)
            $http.defaults.headers.common.Authorization = "Bearer " + this.accessToken;
    }
}]);

campusNextApp.service('EnvConfig', function() {
        /**
     * You can have as many environments as you like in here
     * just make sure the host matches up to your hostname including port
     */
        var _environments = {
                local: {
                    host: 'localhost:40000',
                    config: {
                        /**
                     * Add any config properties you want in here for this environment
                     */
                        apiroot: 'http://localhost:50000/'
                    }
                },
                prod: {
                    host: 'campusnext.azurewebsites.net',
                    config: {
                        /**
                     * Add any config properties you want in here for this environment
                     */
                        apiroot: 'http://campusnextservices.azurewebsites.net/'
                    }
                }
            },
            _environment;

        return {
            getEnvironment: function() {
                var host = window.location.host;

                if (_environment) {
                    return _environment;
                }

                for (var environment in _environments) {
                    if (typeof _environments[environment].host && _environments[environment].host == host) {
                        _environment = environment;
                        return _environment;
                    }
                }

                return null;
            },
            get: function(property) {
                return _environments[this.getEnvironment()].config[property];
            }
        }
    }
);