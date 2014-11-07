campusNextApp.controller("RentalAddCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'ProfileService', 'CampusService',
    function ($scope, $http, $location, tokenService, envConfig, profileService, campusService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        $scope.address = "";
        $scope.rent = "";
        $scope.description = "";
        $scope.userId = "";
        $scope.rooms = "";

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $scope.submitForm = function() {
            $scope.isSaving = true;
            var rental = {
                CampusCode: profileService.profile.campusCode,
                Address: $scope.address,
                Rent: $scope.rent,
                Description: $scope.description,
                Rooms: $scope.rooms
            };

            tokenService.setAuthorizationHeader();

            var responsePromise = $http.post(envConfig.get('apiroot') + "api/Rental", rental, config);

            responsePromise.success(function() {
                toastr.success('Your rental added successfully', 'Congratulations!');
                $location.path('/auth/rental/');
            });

            responsePromise.error(function(dataFromServer) {
                toastr.error(dataFromServer, "Error!");
            });
        }
    }
]);
