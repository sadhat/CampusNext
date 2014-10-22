campusNextApp.service('CampusService', ['$http',function ($http) {
    this.selectedCampus = {
        "campusId": 1,
        "name": "NDSU",
        "logoUrl": "NDSU.jpg",
        "rank": 12
    };
    var key = "selectedCampus";
    this.getSelectedCampus = function () {      
        if (!localStorage.getItem(key) || localStorage.getItem(key) === typeof undefined) {
            localStorage.setItem(key, JSON.stringify(this.selectedCampus));
        }
        return JSON.parse(localStorage.getItem(key));
    }
    this.setSelectedCampus = function (campus) {
        localStorage.setItem(key, JSON.stringify(campus));
    }
}]);