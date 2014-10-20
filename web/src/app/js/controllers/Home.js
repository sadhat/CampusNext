campusNextApp.controller('HomeCtrl', ['$scope', '$http', function ($scope, $http) {
    $http.get('src/app/data/categories/catagories.json').success(function (data) {
        $scope.categories = data;
    });
}]);