// tripsController.js
(function () {
    "use strict";

    // getting the EXISTING module, then
    // create a controller, and use the function "tripsController"
    // as what the body of the controller will be
    angular.module("app-trips")
        .controller("tripsController", tripsController);

    function tripsController($http) {

        var vm = this;

        // vm.trips = [{
        //     name: "US Trip",
        //     created: new Date()
        // }, {
        //     name: "World Trip",
        //     created: new Date
        // }];
        vm.trips = [];

        vm.newTrip = {};

        vm.errorMessage = "";
        vm.isBusy = true;

        $http.get("/api/trips")
            .then(function(response) {
                // success
                angular.copy(response.data, vm.trips);
            }, function (error) {
                // failure
                vm.errorMessage = "Failed to load data: " + error;
            })
            .finally(function () {
                vm.isBusy = false;
            });

        vm.addTrip = function () {
            // vm.trips.push({
            //     name: vm.newTrip.name,
            //     created: new Date()
            // });
            // vm.newTrip = {};

            vm.isBusy = true;
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function (response) {
                    vm.trips.push(response.data);
                }, function () {
                    vm.errorMessage = "Failed to save new trip.";
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        }
    }

})();
