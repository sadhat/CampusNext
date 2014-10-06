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

campusNextApp.controller("TextbookAddCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.title = "";
    $scope.isbn = "";
    $scope.price = 0;
    $scope.description = "";

    $scope.submitForm = function (item, event) {
        $scope.isSaving = true;
        var textbook = {
            CampusName: "NDSU",
            Title: $scope.title,
            Isbn: $scope.isbn,
            Price: $scope.price,
            Description: $scope.description
        };
        var responsePromise = $http.post("http://campusnextservices.azurewebsites.net/odata/TextbookSearch", textbook, {});

        responsePromise.success(function (dataFromServer, status, headers, config) {
            toastr.success('Your book added successfully', 'Congratulations!');
        });

        responsePromise.error(function (dataFromServer, status, headers, config) {
            alert(status);
        });
    }

    $scope.addAnother = function (item, event) {
        $scope.isSaving = false;
    }
}]);

campusNextApp.controller("TextbookSearchCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.title = "Textbook Search";
    $scope.keyword = "";
    $scope.campusName = "NDSU";
    $scope.search = function () {
        var filter = "?keyword=" + $scope.keyword + "&campusName=" + $scope.campusName;
        $http.get('http://campusnextservices.azurewebsites.net/odata/TextbookSearch/' + filter).success(function (data) {
            $scope.searchResults = data.value;
        });
    }
}]);