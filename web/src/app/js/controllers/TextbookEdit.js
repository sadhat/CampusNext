campusNextApp.controller("TextbookEditCtrl", ['$scope', '$http', '$location', '$routeParams', 'TokenService', 'EnvConfig'
    , function ($scope, $http, $location, routeParams, tokenService, envConfig) {
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        tokenService.setAuthorizationHeader();

        $http.get(envConfig.get('apiroot') + "api/Textbook/" + routeParams.id, config)
            .success(function (textbook) {
                $scope.id = routeParams.id;
                $scope.title = textbook.name;
                $scope.isbn = textbook.isbn;
                $scope.price = textbook.price;
                $scope.description = textbook.description;
                $scope.userId = textbook.userId;
                $scope.authors = textbook.authors;
                $scope.course = textbook.course;

            });

        $scope.submitForm = function (item, event) {
            $scope.isSaving = true;
            var textbook = {
                Id: $scope.id,
                CampusCode: "NDSU",
                Name: $scope.title,
                Isbn: $scope.isbn,
                Price: $scope.price,
                Description: $scope.description,
                UserId: $scope.userId,
                Authors: $scope.authors,
                Course: $scope.course
            };

            var responsePromise = $http.put(envConfig.get('apiroot') + "api/Textbook/" + $scope.id, textbook, config);

            responsePromise.success(function () {
                toastr.success('Your book edited successfully', 'Congratulations!');
                $location.path('/auth/textbook/');
            });

            responsePromise.error(function (dataFromServer, status) {
                toastr.error(status);
            });
        }

        $scope.addAnother = function () {
            $scope.isSaving = false;
        }
    }]);
