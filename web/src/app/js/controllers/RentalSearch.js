campusNextApp.controller("RentalSearchCtrl", ['$scope', '$http','$location', 'EnvConfig', 'CampusService', function ($scope, $http, $location, envConfig, campusService) {
    $scope.campusName = campusService.getSelectedCampus().name;
    $scope.title = "Rental Search";
    $scope.additionalInfo = "";
    $scope.rentRangeFrom = "";
    $scope.rentRangeTo = "";
    $scope.rooms = "";

    $scope.loading = false;
    $scope.search = function () {
        var filter = "?additionalInfo=" + $scope.additionalInfo + "&campusName=" + $scope.campusName + "&rentRangeFrom=" + $scope.rentRangeFrom
            + "&rentRangeTo=" + $scope.rentRangeTo + "&rooms=" + $scope.rooms;
        $scope.loading = true;
        $scope.searchResults = {};
        $http.get(envConfig.get('apiroot') + 'odata/RentalSearch/' + filter).success(function (data) {
            $scope.searchResults = data.value;
            if ($scope.searchResults.length === 0) {
                toastr.warning("No result found. Please refine your search criteria.");
            }
            $scope.loading = false;
        });
        $scope.contactSeller = function (category, id) {
            $location.path("/email/" + category + "/" + id);
        }
    }
}]);