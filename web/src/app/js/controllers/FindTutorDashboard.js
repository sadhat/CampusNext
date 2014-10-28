campusNextApp.controller("FindTutorDashboardCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', function ($scope, $http, $location, tokenService, envConfig) {
        tokenService.setAuthorizationHeader();
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $http.get(envConfig.get('apiroot') + 'api/Tutor', config)
            .success(function(data) {
                $scope.searchResults = data;
            });

        $scope.delete = function (index) {
            var result = confirm("Are you sure you want to delete? ");
            var tutor = $scope.searchResults[index];
            if (result) {
                $http.delete(envConfig.get('apiroot') + 'api/Tutor/' + tutor.id, config);
                $scope.searchResults.splice(index, 1);
            }
        };

        $scope.add = function () {
            $location.path('/tutoradd');
        };

        $scope.edit = function (index) {
            var tutor = $scope.searchResults[index];
            $location.path('/tutoredit/' + tutor.id);
        }
    }
]);