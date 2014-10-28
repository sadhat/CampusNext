campusNextApp.controller("TutorAddCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'ProfileService', 'CampusService',
    function ($scope, $http, $location, tokenService, envConfig, profileService, campusService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        $scope.rate = "";
        $scope.description = "";
        $scope.userId = "";
        $scope.course = "";

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $scope.submitForm = function() {
            $scope.isSaving = true;
            var tutor = {
                CampusCode: profileService.profile.campusCode,
                Rate: $scope.rate,
                Description: $scope.description,
                Course: $scope.course
            };

            tokenService.setAuthorizationHeader();

            var responsePromise = $http.post(envConfig.get('apiroot') + "api/Tutor", tutor, config);

            responsePromise.success(function() {
                toastr.success('Your Tutor added successfully', 'Congratulations!');
                $location.path('/auth/findtutor/');
            });

            responsePromise.error(function(dataFromServer, status) {
                toastr.error(dataFromServer, "Error!");
            });
        }
    }
]);
