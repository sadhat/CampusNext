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

	$routeProvider.when('/tutorsearch', {
	    templateUrl: './views/search/findtutor/findtutor_search.html',
	    controller: 'TutorSearchCtrl'
	});

	$routeProvider.when('/shareridesearch', {
	    templateUrl: './views/search/shareride/shareride_search.html',
	    controller: 'ShareRideSearchCtrl'
	});

	$routeProvider.when('/textbookadd', {
	    templateUrl: './views/authoring/textbook/textbook_add.html',
	    controller: 'TextbookAddCtrl'
	});

	$routeProvider.when('/textbookedit/:id', {
	    templateUrl: './views/authoring/textbook/textbook_edit.html',
	    controller: 'TextbookEditCtrl'
	});

	$routeProvider.when('/auth/findtutor/', {
	    templateUrl: './views/authoring/findtutor/dashboard_findtutor.html',
	    controller: 'FindTutorDashboardCtrl'
	});

	$routeProvider.when('/tutoradd', {
	    templateUrl: './views/authoring/findtutor/findtutor_add.html',
	    controller: 'TutorAddCtrl'
	});

	$routeProvider.when('/tutoredit/:id', {
	    templateUrl: './views/authoring/findtutor/findtutor_edit.html',
	    controller: 'TutorEditCtrl'
	});

	$routeProvider.when('/profile', {
	    templateUrl: './views/authoring/profile/profile_edit.html',
	    controller: 'ProfileEditCtrl'
	});

	$routeProvider.when('/changeCampus', {
	    templateUrl: './views/authoring/profile/campus_selected.html',
	    controller: 'CampusCtrl'
	});

	$routeProvider.when('/email/:category/:id', {
	    templateUrl: './views/authoring/communication/email.html',
	    controller: 'EmailCtrl'
	});

	$routeProvider.when('/auth/shareride/', {
	    templateUrl: './views/authoring/shareride/dashboard_shareride.html',
	    controller: 'ShareRideDashboardCtrl'
	});

	$routeProvider.when('/sharerideadd', {
	    templateUrl: './views/authoring/shareride/shareride_add.html',
	    controller: 'ShareRideAddCtrl'
	});

	$routeProvider.when('/sharerideedit/:id', {
	    templateUrl: './views/authoring/shareride/shareride_edit.html',
	    controller: 'ShareRideEditCtrl'
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
            sessionStorage.removeItem("accessToken");
        }
        if (rejection.exceptionType == "Facebook.FacebookOAuthException") {
            toastr.error('Your session expired. Please login again');
            sessionStorage.removeItem("accessToken");
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
