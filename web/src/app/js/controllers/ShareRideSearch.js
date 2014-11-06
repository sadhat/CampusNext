campusNextApp.controller("ShareRideSearchCtrl", ['$scope', '$http', '$location', 'EnvConfig', 'CampusService', function ($scope, $http, $location, envConfig, campusService) {
    $scope.campusName = campusService.getSelectedCampus().name;
    $scope.title = "Share Ride Search";
    $scope.keyword = "";
    $scope.fromLocation = "";
    $scope.toLocation = "";
    $scope.startDateTime = "";
    $scope.returnDateTime = "";

    $scope.loading = false;
    $scope.search = function () {
        var filter = "?keyword=" + $scope.keyword + "&campusName=" + $scope.campusName + "&fromLocation=" + $scope.fromLocation
            + "&toLocation=" + $scope.toLocation + "&startDateTime=" + $scope.startDateTime + "&returnDateTime=" + $scope.returnDateTime;
        $scope.loading = true;
        $scope.searchResults = {};
        $http.get(envConfig.get('apiroot') + 'odata/ShareRideSearch/' + filter).success(function (data) {
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