// app-trips.js
(function () {
    // CREATE a module for ourselves
    angular.module("app-trips", ["simpleControls", "ngRoute"]) // tells that simpleControls is a dependency
        .config(function($routeProvider) {
            $routeProvider.when("/", {
                controller: "tripsController",
                controllerAs: "vm",
                templateUrl: "/views/tripsView.html"
            });

            // tripname: route parameter
            $routeProvider.when("/editor/:tripname", {
                controller: "tripEditorController",
                controllerAs: "vm",
                templateUrl: "/views/tripEditorView.html"
            });

            $routeProvider.otherwise({
                redirectTo: "/"
            });
        });

})();
