campusNextApp.controller("TutorEditCtrl", [
    '$scope', '$http', '$location', '$routeParams', 'TokenService', 'EnvConfig', 'ProfileService', 'CampusService',
     function ($scope, $http, $location, routeParams, tokenService, envConfig, profileService, campusService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        tokenService.setAuthorizationHeader();

        $http.get(envConfig.get('apiroot') + "api/Tutor/" + routeParams.id, config)
            .success(function (tutor) {
                $scope.id = routeParams.id;
                $scope.price = tutor.price;
                $scope.description = tutor.description;
                $scope.userId = tutor.userId;
                $scope.rate = tutor.rate;
                $scope.course = tutor.course;
            });

        $scope.submitForm = function () {
            $scope.isSaving = true;
            var tutor = {
                Id: $scope.id,
                CampusCode: profileService.profile.campusCode,
                Price: $scope.price,
                Description: $scope.description,
                UserId: $scope.userId,
                Rate: $scope.rate,
                Course: $scope.course
            };

            var responsePromise = $http.put(envConfig.get('apiroot') + "api/Tutor/" + $scope.id, tutor, config);

            responsePromise.success(function () {
                toastr.success('Your tutor edited successfully', 'Congratulations!');
                $location.path('/auth/findtutor/');
            });

            responsePromise.error(function (dataFromServer, status) {
                toastr.error(status);
            });
        }
    }]);
