campusNextApp.controller("CategoryCatalogCtrl", [
    '$scope', '$http', 'EnvConfig', 'CampusService', function($scope, $http, envConfig, campusService) {

        var filter = "?campusName=" + campusService.getSelectedCampus().name;
        $http.get(envConfig.get('apiroot') + 'api/CategoryCatalog/' + filter).success(function(data) {
            $scope.categoryCatalog = data;
        });

    }
]);