var campusNextApp = angular.module('campusNextApp', ['ngRoute']);

campusNextApp.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/', {
		templateUrl: './views/home.html',
		controller: 'HomeCtrl'
	});
}]);

campusNextApp.controller('HomeCtrl', ['$scope', function ($scope) {
    $scope.category = {
        items: [
            { title: "textbook search", totalItems:456 },
            { title: "find tutor", totalItems: 50 },
            { title: "room for rent", totalItems: 78 },
            { title: "campus life events", totalItems: 10 },
            { title: "share a ride", totalItems: 8 },
            { title: "find study group", totalItems: 59 }
        ]
    };
}]);

campusNextApp.controller("CampusChooserCtrl", [
    '$scope', function($scope) {
        $scope.campus = {
            name: "NDSU",
            logoUrl: "NDSU.jpg"
        };
    }
]);