campusNextApp.controller("CampusChooserCtrl", ['$scope', '$http', function ($scope, $http) {
    $http.get('src/app/data/campuses/viewing_campus.json').success(function (data) {
        $scope.campus = data;
    });
}]);

campusNextApp.controller("CampusCtrl", ['$scope', '$http', function ($scope, $http) {
    $http.get('src/app/data/campuses/campuses.json').success(function (data) {
        $scope.campuses = data;
    });
}]);