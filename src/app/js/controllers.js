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

campusNextApp.controller("TextbookSearchCtrl", ['$scope', '$http', function ($scope, $http) {
    $scope.title = "Textbook Search";
}]);