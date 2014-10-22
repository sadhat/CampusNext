campusNextApp.controller("CampusCtrl", ['$scope', '$http','$window','$interval', 'CampusService', function ($scope, $http, $window, $interval, campusService) {
    $http.get('src/app/data/campuses/campuses.json').success(function (data) {
        var arr = new Array();
        for (var i = 0; i < data.length; i++) {
            if (campusService.getSelectedCampus()) {
                if (data[i].name === campusService.getSelectedCampus().name) {
                    $scope.campus = data[i];
                }
            }
            arr.push(data[i].name);
        }

        $scope.campusCodes = arr;
        $scope.campuses = data;
        
        $scope.submitForm = function () {
            $scope.isSaving = true;
            campusService.setSelectedCampus($scope.campus);
            $scope.isSaving = false;

            toastr.success('Your campus updated successfully. We will refresh the page shortly.', 'Congratulations!');
            $interval(function() {
                $window.location.href = "/";
            }, 1000);           
        }
    });
}]);