campusNextApp.service('ProfileService', ['$http', 'TokenService', 'EnvConfig', '$location', function ($http, tokenService, envConfig, $location) {
    this.load = function() {
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        tokenService.setAuthorizationHeader();
        var that = this;
        $http.get(envConfig.get('apiroot') + "api/Profile", config)
            .success(function(profile) {
                that.profile = profile;
            });
    }

    this.gaurdAuthoring = function() {
        if (!this.profile.campusCode) {
            toastr.warning("You must choose a campus to add a book.");
            $location.path("/profile");
        }
    }
}]);
