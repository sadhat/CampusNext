campusNextApp.controller("ProfileEditCtrl", [
    '$scope', '$http', 'TokenService','$location', 'EnvConfig', 'ProfileService', function($scope, $http, tokenService,$location, envConfig, profileService) {
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        tokenService.setAuthorizationHeader();

        $scope.id = 0;
        $scope.email = "";
        $scope.userId = "";
        $scope.status = 1;
        $scope.facebookName = "";

        $http.get(envConfig.get('apiroot') + "api/Profile", config)
            .success(function (profile) {
                if (profile) {
                $scope.id = profile.id;
                $scope.email = profile.email;
                $scope.userId = profile.userId;
                $scope.status = profile.status;
                $scope.facebookName = profile.facebookName;
                $scope.campusCode = profile.campusCode;
            }
            });

        $scope.submitForm = function(item, event) {
            $scope.isSaving = true;
            var profile = {
                Id: $scope.id,
                Email: $scope.email,
                CampusCode: $scope.campusCode,
                Status: $scope.status,
                FacebookName: $scope.facebookName
            };

            var responsePromise = $http.post(envConfig.get('apiroot') + "api/Profile", profile, config);

            responsePromise.success(function() {
                toastr.success('Your profile edited successfully', 'Congratulations!');
                profileService.load();
                $location.path('/dashboard');
            });

            responsePromise.error(function(dataFromServer, status) {
                toastr.error(status);
            });
        }

    }
]);