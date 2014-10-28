campusNextApp.controller("EmailCtrl", [
    '$scope', '$http', '$location', '$routeParams', 'EnvConfig'
    , function ($scope, $http, $location, $routeParams, envConfig) {
        $scope.subject = "";
        $scope.fromEmail = "";
        $scope.message = "";
        $scope.userId = "";

        $scope.submitForm = function() {
            $scope.isSaving = true;

            var email = {
                Subject: $scope.subject,
                FromEmail: $scope.fromEmail,
                Message: $scope.message,
                UserId: $scope.userId,
                CategoryName : $routeParams.category,
                ItemId : $routeParams.id
            };
            console.log(email);
            var responsePromise = $http.post(envConfig.get('apiroot') + "api/Email", email);

            responsePromise.success(function() {
                toastr.success('Your email has been sent successfully', 'Congratulations!');

            });

            responsePromise.error(function(dataFromServer, status) {
                toastr.error(dataFromServer, 'Error!');
            });
        }
    }
]);