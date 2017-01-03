(function () {
    //$("#username")
    //    .text("Some Dude");
    //
    //$("#main")
    //   .on("mouseenter", function () {
    //      main.style.backgroundColor = "#888";
    //   })
    //   .on("mouseleave", function () {
    //      main.style.backgroundColor = "";
    //   });

    //$("ul.menu li a")
    //    .on("click", function () {
    //        alert($(this).text());
    //    });

    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function() {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            //$(this).text("Show Sidebar");
            $icon.removeClass("fa-angle-left").addClass("fa-angle-right");
        } else {
            //$(this).text("Hide Sidebar");
            $icon.removeClass("fa-angle-right").addClass("fa-angle-left");
        }
    });

})();