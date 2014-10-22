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
		controller: 'CategoryCatalogCtrl'
	});

	$routeProvider.when('/dashboard', {
	    templateUrl: './views/authoring/dashboard.html',
	    controller: 'HomeCtrl'
	});

    $routeProvider.when('/auth/textbook/', {
        templateUrl: './views/authoring/textbook/dashboard_textbook.html',
        controller: 'TextbookDashboardCtrl'
    });

	$routeProvider.when('/textbooksearch', {
	    templateUrl: './views/search/textbook/textbook_search.html',
	    controller: 'TextbookSearchCtrl'
	});

	$routeProvider.when('/textbookadd', {
	    templateUrl: './views/authoring/textbook/textbook_add.html',
	    controller: 'TextbookAddCtrl'
	});

	$routeProvider.when('/textbookedit/:id', {
	    templateUrl: './views/authoring/textbook/textbook_edit.html',
	    controller: 'TextbookEditCtrl'
	});

	$routeProvider.when('/profile', {
	    templateUrl: './views/authoring/profile/profile_edit.html',
	    controller: 'ProfileEditCtrl'
	});

	$routeProvider.when('/changeCampus', {
	    templateUrl: './views/authoring/profile/campus_selected.html',
	    controller: 'CampusCtrl'
	});

}])
.factory('authHttpResponseInterceptor', ['$q', '$location', function ($q, $location) {
return {
    response: function (response) {
        if (response.status === 401) {
            console.log("Response 401");
        }
        return response || $q.when(response);
    },
    responseError: function (rejection) {
        if (rejection.status === 401) {
            console.log("Response Error 401", rejection);
            toastr.error("Unauthorized access. Please login again. Remember to close all your browser!");
        }
        if (rejection.exceptionType == "Facebook.FacebookOAuthException") {
            toastr.error('Your session expired. Please login again');
            $location.path('/');
        }
        return $q.reject(rejection);
    }
}
}])
.config(['$httpProvider', function ($httpProvider) {
    //Http Intercpetor to check auth failures for xhr requests
    $httpProvider.interceptors.push('authHttpResponseInterceptor');
}]);;
