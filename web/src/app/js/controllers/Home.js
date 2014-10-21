campusNextApp.controller('HomeCtrl', ['$scope', '$http', 'ProfileService', function ($scope, $http, ProfileService) {
    $http.get('src/app/data/categories/catagories.json').success(function (data) {
        $scope.categories = data;
    });
}]);