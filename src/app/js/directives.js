campusNextApp.directive("product", function (ProductService) {
    return {
        restrict: 'E',
        transclude: false,
        scope : {},
        controller: function ($scope) {
            $scope.title = ProductService.name()
        },
        templateUrl: '/views/products/textbook_search.html'
    };
});