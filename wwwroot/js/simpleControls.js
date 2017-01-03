// simpleControls.js
(function () {
    "use strict";

    angular.module("simpleControls", [])
        .directive("waitCursor", waitCursor);

    function waitCursor() {
        return {
            scope: {
                // the object that we're binding our wait cursor to
                show: "=displayWhen"
            },
            restrict: "E",  // restrict to being an element
            templateUrl: "/views/waitCursor.html"
        };
    }
})();