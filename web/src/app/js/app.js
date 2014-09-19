var campusNextApp = angular.module('campusNextApp', ['ngRoute']);

campusNextApp.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/', {
		templateUrl: './views/home.html',
		controller: 'HomeCtrl'
	});

	$routeProvider.when('/textbooksearch', {
	    templateUrl: './views/products/textbook_search.html',
	    controller: 'TextbookSearchCtrl'
	});

	$routeProvider.when('/textbookadd', {
	    templateUrl: './views/products/textbook_add.html',
	    controller: 'TextbookAddCtrl'
	});
}]);
