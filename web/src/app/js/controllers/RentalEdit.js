campusNextApp.controller("RentalEditCtrl", [
    '$scope', '$http', '$location', '$routeParams', 'TokenService', 'EnvConfig', 'ProfileService',
     function ($scope, $http, $location, routeParams, tokenService, envConfig, profileService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        tokenService.setAuthorizationHeader();

        $http.get(envConfig.get('apiroot') + "api/Rental/" + routeParams.id, config)
            .success(function (rental) {
                $scope.id = routeParams.id;
                $scope.address = rental.address;
                $scope.description = rental.description;
                $scope.userId = rental.userId;
                $scope.rent = rental.rent;
                $scope.rooms = rental.rooms;
            });

        $scope.submitForm = function () {
            $scope.isSaving = true;
            var rental = {
                Id: $scope.id,
                CampusCode: profileService.profile.campusCode,
                Address: $scope.address,
                Description: $scope.description,
                UserId: $scope.userId,
                Rent: $scope.rent,
                Rooms: $scope.rooms
            };

            var responsePromise = $http.put(envConfig.get('apiroot') + "api/Rental/" + $scope.id, rental, config);

            responsePromise.success(function () {
                toastr.success('Your tutor edited successfully', 'Congratulations!');
                $location.path('/auth/rental/');
            });

            responsePromise.error(function (dataFromServer, status) {
                toastr.error(status);
            });
        }
    }]);
