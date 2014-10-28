campusNextApp.directive("formMessage", [
    function() {
        return {
            restrict: 'E',
            replace: true,
            template: '<div style="height:60px;" class="row">'
                + '<div class="medium-2 small-1 end columns">&nbsp;</div>'
                + '<div class="medium-10 small-11 columns"> Keep typing. Once all fields are filled correctly, send button will apear to submit your message.</div>'
                + '</div>'
        }
    }
]);