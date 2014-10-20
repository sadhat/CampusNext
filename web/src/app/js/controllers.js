﻿campusNextApp.controller('RootCtrl', function ($rootScope, $scope, facebookUser) {
    $rootScope.loggedInUser = {};

    $rootScope.$on('fbLoginSuccess', function (name, response) {
        facebookUser.then(function (user) {
            user.api('/me').then(function (response) {
                $rootScope.loggedInUser = response;
            });
        });
    });

    $rootScope.$on('fbLogoutSuccess', function () {
        $scope.$apply(function () {
            $rootScope.loggedInUser = {};
        });
    });
}); 

campusNextApp.controller('HomeCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get('src/app/data/categories/catagories.json').success(function (data) {
        $scope.categories = data;
    });
}]);

campusNextApp.controller("CampusChooserCtrl", ['$scope', '$http', function ($scope, $http) {
    $http.get('src/app/data/campuses/viewing_campus.json').success(function (data) {
        $scope.campus = data;
    });
}]);

campusNextApp.controller("FindTextbookCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.title = "find textbook";
}]);

campusNextApp.controller("TextbookAddCtrl", ['$scope', '$http', '$location', 'TokenService', 'EnvConfig', function ($scope, $http, $location, tokenService, envConfig) {
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

    $scope.submitForm = function (item, event) {
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

        responsePromise.success(function (dataFromServer, status, headers, config) {
            toastr.success('Your book added successfully', 'Congratulations!');
            $location.path('/auth/textbook/');
        });

        responsePromise.error(function (dataFromServer, status, headers, config) {
            alert(status);
        });
    }

    $scope.addAnother = function (item, event) {
        $scope.isSaving = false;
    }
}]);

campusNextApp.controller("TextbookEditCtrl", ['$scope', '$http', '$location', '$routeParams', 'TokenService', 'EnvConfig'
    , function ($scope, $http, $location, routeParams, tokenService, envConfig) {
    var config = {
        headers: {
            'accessToken': tokenService.accessToken
        }
    };
    tokenService.setAuthorizationHeader();

    $http.get(envConfig.get('apiroot') + "api/Textbook/" + routeParams.id, config)
        .success(function(textbook) {
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

campusNextApp.controller("TextbookSearchCtrl", ['$scope', '$http', 'EnvConfig', function ($scope, $http, envConfig) {
    $scope.title = "Textbook Search";
    $scope.keyword = "";
    $scope.campusName = "NDSU";
    $scope.search = function () {
        var filter = "?keyword=" + $scope.keyword + "&campusName=" + $scope.campusName;
        $http.get(envConfig.get('apiroot') + 'odata/TextbookSearch/' + filter).success(function (data) {
            $scope.searchResults = data.value;
        });
    }
}]);

campusNextApp.controller("AuthorTextbookCtrl", [
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', 'facebookUser', function ($scope, $http, $location, tokenService, envConfig, facebookUser) {
        tokenService.setAuthorizationHeader();
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };

        $http.get(envConfig.get('apiroot') + 'api/Textbook', config)
            .success(function(data) {
                $scope.searchResults = data;
            })
            .error(function(data) {
                if (data.exceptionType == "Facebook.FacebookOAuthException") {
                    toastr.error('Your session expired. Please login again');
                    facebookUser.then(function (user) {
                        user.logout();
                    });
                    $location.path('/');
                }
                
            });
        
        $scope.delete = function(index) {
            var result = confirm("Are you sure you want to delete? ");
            var book = $scope.searchResults[index];
            if (result) {
                $http.delete(envConfig.get('apiroot') + 'api/Textbook/' + book.id, config);
                $scope.searchResults.splice(index, 1);
            }
        };

        $scope.add = function() {
            $location.path('/textbookadd');
        };

        $scope.edit = function (index) {
            var book = $scope.searchResults[index];
            $location.path('/textbookedit/' + book.id);
        }
    }
]);