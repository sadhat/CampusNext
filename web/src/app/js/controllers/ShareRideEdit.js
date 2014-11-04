campusNextApp.controller("ShareRideEditCtrl", [
    '$scope', '$http', '$location','$routeParams', 'TokenService', 'EnvConfig', 'ProfileService',
    function ($scope, $http, $location, $routeParams, tokenService, envConfig, profileService) {
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

        $http.get(envConfig.get('apiroot') + "api/ShareRide/" + $routeParams.id, config)
            .success(function (shareRide) {
                $scope.id = $routeParams.id;
                $scope.fromLocation = shareRide.fromLocation;
                $scope.toLocation = shareRide.toLocation;
                $scope.isRoundtrip = shareRide.isRoundTrip;
                $scope.startDateTime = shareRide.startDateTime;
                $scope.returnDateTime = shareRide.returnDateTime;
                $scope.additionalInfo = shareRide.additionalInfo;
            });

        

        $scope.submitForm = function() {
            $scope.isSaving = true;
            var shareRide = {
                Id: $scope.id,
                CampusCode: profileService.profile.campusCode,
                FromLocation: $scope.fromLocation,
                ToLocation: $scope.toLocation,
                IsRoundTrip: $scope.isRoundtrip,
                StartDateTime: $scope.startDateTime,
                ReturnDateTime: $scope.returnDateTime,
                AdditionalInfo: $scope.additionalInfo
            };

            tokenService.setAuthorizationHeader();

            var responsePromise = $http.put(envConfig.get('apiroot') + "api/ShareRide/" + $scope.id, shareRide, config);

            responsePromise.success(function() {
                toastr.success('Your Share Ride edited successfully', 'Congratulations!');
                $location.path('/auth/shareride/');
            });

            responsePromise.error(function(dataFromServer) {
                toastr.error(dataFromServer, "Error!");
            });
        }
    }
]);
