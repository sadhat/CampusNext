campusNextApp.directive("preferredcampus", [
    "ProfileService", "CampusService", function (profileService, campusService) {
        
        return {
            restrict: 'E',
            link: function (scope, element, attrs) {
                if (!profileService.profile) profileService.load();
                var profile = profileService.profile;
                if (campusService.getSelectedCampus().name !== profile.campusCode) {
                    element.replaceWith('<div class="selectedCampusMessage">Your preferred campus is ' + profile.campusCode +
                        '. Your post will go under this campus. If you want to change your preferred campus, please update from your profile.</div>');
                }
            }
        }
    }
]);