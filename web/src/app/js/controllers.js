campusNextApp.controller('RootCtrl', function ($rootScope, $scope, facebookUser) {
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

campusNextApp.controller("TextbookAddCtrl", ['$scope', '$http', '$location', 'TokenService', function ($scope, $http, $location, tokenService) {
    $scope.title = "";
    $scope.isbn = "";
    $scope.price = 0;
    $scope.description = "";
    $scope.userId = 123;

    var config = {
        headers: {
            'accessToken': tokenService.accessToken
        }
    };


    $scope.submitForm = function (item, event) {
        $scope.isSaving = true;
        var textbook = {
            CampusName: "NDSU",
            Name: $scope.title,
            Isbn: $scope.isbn,
            Price: $scope.price,
            Description: $scope.description,
            UserId: $scope.userId
        };

        tokenService.setAuthorizationHeader();

        var responsePromise = $http.post("http://localhost:50000/api/Textbook", textbook, config);

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
    '$scope', '$http', '$location', 'TokenService', 'EnvConfig', function ($scope, $http, $location, tokenService, envConfig) {
        tokenService.setAuthorizationHeader();
        var config = {
            headers: {
                'accessToken': tokenService.accessToken
            }
        };
        $http.get(envConfig.get('apiroot') + 'api/Textbook', config)
            .success(function (data) {
            $scope.searchResults = data;
        });

        $scope.delete = function(index) {
            var result = confirm("Are you sure you want to delete? ");
            var book = $scope.searchResults[index];
            if (result) {
                $http.delete(envConfig.get('apiroot') + 'api/Textbook/' + book.id, config);
                $scope.searchResults.splice(index, 1);
            }
        };

        $scope.add = function () {
            $location.path('/textbookadd');
        }
    }
]);