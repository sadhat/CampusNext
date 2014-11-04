campusNextApp.controller("ShareRideDashboardCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', function ($scope, $http, $location, tokenService, envConfig) {
        tokenService.setAuthorizationHeader();
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $http.get(envConfig.get('apiroot') + 'api/ShareRide', config)
            .success(function (data) {
                $scope.searchResults = data;
            });

        $scope.delete = function (index) {
            var result = confirm("Are you sure you want to delete? ");
            var shareRide = $scope.searchResults[index];
            if (result) {
                $http.delete(envConfig.get('apiroot') + 'api/ShareRide/' + shareRide.id, config);
                $scope.searchResults.splice(index, 1);
            }
        };

        $scope.add = function () {
            $location.path('/sharerideadd');
        };

        $scope.edit = function (index) {
            var shareRide = $scope.searchResults[index];
            $location.path('/sharerideedit/' + shareRide.id);
        }
    }
]);