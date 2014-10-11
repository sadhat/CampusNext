campusNextApp.service('ProductService', function () {
    this.name = function (category) {
        return "MY";
    }
});

campusNextApp.service('TokenService',['$http', function ($http) {
    this.accessToken = sessionStorage.getItem("accessToken");
    this.setAuthorizationHeader = function() {
        $http.defaults.headers.common.Authorization = "Bearer " + this.accessToken;
    }
}]);