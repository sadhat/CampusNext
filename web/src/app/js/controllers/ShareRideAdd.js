campusNextApp.controller("ShareRideAddCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'ProfileService',
    function ($scope, $http, $location, tokenService, envConfig, profileService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        $scope.fromLocation = "";
        $scope.toLocation = "";
        $scope.isRoundtrip = false;
        $scope.startDateTime = "";
        $scope.returnDateTime = "";
        $scope.additionalInfo = "";

        $scope.userId = "";

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $scope.submitForm = function() {
            $scope.isSaving = true;
            var tutor = {
                CampusCode: profileService.profile.campusCode,
                FromLocation: $scope.fromLocation,
                ToLocation: $scope.toLocation,
                IsRoundTrip: $scope.isRoundtrip,
                StartDateTime: $scope.startDateTime,
                ReturnDateTime: $scope.returnDateTime,
                AdditionalInfo: $scope.additionalInfo
            };

            tokenService.setAuthorizationHeader();

            var responsePromise = $http.post(envConfig.get('apiroot') + "api/ShareRide", tutor, config);

            responsePromise.success(function() {
                toastr.success('Your Share Ride added successfully', 'Congratulations!');
                $location.path('/auth/shareride/');
            });

            responsePromise.error(function(dataFromServer, status) {
                toastr.error(dataFromServer, "Error!");
            });
        }
    }
]);
