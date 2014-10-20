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

	$routeProvider.when('/textbookedit/:id', {
	    templateUrl: './views/products/textbook_edit.html',
	    controller: 'TextbookEditCtrl'
	});
}])
.factory('authHttpResponseInterceptor', ['$q', '$location','facebookUser', function ($q, $location, facebookUser) {
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
            facebookUser.then(function (user) {
                user.logout();
            });
        }
        if (rejection.exceptionType == "Facebook.FacebookOAuthException") {
            toastr.error('Your session expired. Please login again');
            facebookUser.then(function(user) {
                user.logout();
            });
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
