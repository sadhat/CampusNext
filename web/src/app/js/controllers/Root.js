campusNextApp.controller('RootCtrl', function ($rootScope, $scope, facebookUser) {
    $rootScope.loggedInUser = {};

    $rootScope.$on('fbLoginSuccess', function () {
        facebookUser.then(function (user) {
            user.api('/me').then(function (response) {
                $rootScope.loggedInUser = response;
            });
        });
    });

    $rootScope.$on('fbLogoutSuccess', function () {
        $scope.$apply(function () {
            $rootScope.loggedInUser = {};
        });
    });
});