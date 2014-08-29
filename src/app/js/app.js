var campusNextApp = angular.module('campusNextApp', ['ngRoute']);

campusNextApp.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/', {
		templateUrl: './views/home.html',
		controller: 'HomeCtrl'
	});
}]);
