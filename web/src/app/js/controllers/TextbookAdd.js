campusNextApp.controller("TextbookAddCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'ProfileService',
    function ($scope, $http, $location, tokenService, envConfig, profileService) {
        //Check whether profile is complete before authoring
        profileService.gaurdAuthoring();

        $scope.title = "";
        $scope.isbn = "";
        $scope.price = 0;
        $scope.description = "";
        $scope.userId = 123;
        $scope.authors = "";
        $scope.course = "";

        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $scope.submitForm = function(item, event) {
            $scope.isSaving = true;
            var textbook = {
                CampusCode: "NDSU",
                Name: $scope.title,
                Isbn: $scope.isbn,
                Price: $scope.price,
                Description: $scope.description,
                UserId: $scope.userId,
                Authors: $scope.authors,
                Course: $scope.course
            };

            tokenService.setAuthorizationHeader();

            var responsePromise = $http.post(envConfig.get('apiroot') + "api/Textbook", textbook, config);

            responsePromise.success(function(dataFromServer, status, headers, config) {
                toastr.success('Your book added successfully', 'Congratulations!');
                $location.path('/auth/textbook/');
            });

            responsePromise.error(function(dataFromServer, status, headers, config) {
                alert(status);
            });
        }

        $scope.addAnother = function(item, event) {
            $scope.isSaving = false;
        }
    }
]);
