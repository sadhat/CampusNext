campusNextApp.directive("submit", [function () {
        return {
            restrict: 'E',
            transclude: true,
            template: '<div class="row">'
                + '<div class="medium-12 small-12 columns">'
                + '<a ng-show="!my.$invalid" ng-click="submitForm()" class="button right">Save</a>'
                + '</div>'
                + '</div>'
    }
    }
]);