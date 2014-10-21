campusNextApp.controller('RootCtrl', function ($rootScope, $scope, $location, facebookUser, ProfileService) {
    $rootScope.loggedInUser = {};

    $rootScope.$on('fbLoginSuccess', function () {
        facebookUser.then(function (user) {
            user.api('/me').then(function (response) {
                $rootScope.loggedInUser = response;

                ProfileService.load();
                $location.path('/dashboard');
            });
        });
    });

    $rootScope.$on('fbLogoutSuccess', function () {
        $scope.$apply(function () {
            $rootScope.loggedInUser = {};
        });
        $location.path('/');
    });

    $rootScope.$on('fbLoginFailure', function () {
        $location.path('/');
    });
});