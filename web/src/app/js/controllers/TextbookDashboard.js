campusNextApp.controller("TextbookDashboardCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'facebookUser', function ($scope, $http, $location, tokenService, envConfig, facebookUser) {
        tokenService.setAuthorizationHeader();
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $http.get(envConfig.get('apiroot') + 'api/Textbook', config)
            .success(function (data) {
                $scope.searchResults = data;
            })
            .error(function (data) {
                if (data.exceptionType == "Facebook.FacebookOAuthException") {
                    toastr.error('Your session expired. Please login again');
                    facebookUser.then(function (user) {
                        user.logout();
                    });
                    $location.path('/');
                }

            });

        $scope.delete = function (index) {
            var result = confirm("Are you sure you want to delete? ");
            var book = $scope.searchResults[index];
            if (result) {
                $http.delete(envConfig.get('apiroot') + 'api/Textbook/' + book.id, config);
                $scope.searchResults.splice(index, 1);
            }
        };

        $scope.add = function () {
            $location.path('/textbookadd');
        };

        $scope.edit = function (index) {
            var book = $scope.searchResults[index];
            $location.path('/textbookedit/' + book.id);
        }
    }
]);