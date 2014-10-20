campusNextApp.service('TokenService', ['$http', function ($http) {
    this.accessToken = sessionStorage.getItem("accessToken");
    this.setAuthorizationHeader = function () {
        if (this.accessToken)
            $http.defaults.headers.common.Authorization = "Bearer " + this.accessToken;
    }
}]);
