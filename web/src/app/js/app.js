var campusNextApp = angular.module('campusNextApp', ['facebookUtils', 'ngRoute'])
    .constant('facebookConfigSettings', {
        'routingEnabled': true,
        'channelFile': 'channel.html',
        'appID': '739926446075188',
        'permissions': 'email'
    });

campusNextApp.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/', {
		templateUrl: './views/home.html',
		controller: 'HomeCtrl'
	});

	$routeProvider.when('/dashboard', {
	    templateUrl: './views/dashboard.html',
	    controller: 'HomeCtrl'
	});

    $routeProvider.when('/auth/textbook/', {
        templateUrl: './views/authoring/textbook_all.html'
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
