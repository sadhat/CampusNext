campusNextApp.controller("TextbookSearchCtrl", ['$scope', '$http', 'EnvConfig', function ($scope, $http, envConfig) {
    $scope.title = "Textbook Search";
    $scope.keyword = "";
    $scope.campusName = "NDSU";
    $scope.loading = false;
    $scope.search = function () {
        var filter = "?keyword=" + $scope.keyword + "&campusName=" + $scope.campusName;
        $scope.loading = true;
        $scope.searchResults = {};
        $http.get(envConfig.get('apiroot') + 'odata/TextbookSearch/' + filter).success(function (data) {
            $scope.searchResults = data.value;
            if ($scope.searchResults.length === 0) {
                toastr.warning("No result found. Please refine your search criteria.");
            }
            $scope.loading = false;
        });
    }
}]);