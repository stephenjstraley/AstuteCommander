// Write your Javascript code.
(function () {

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#menuToggle i.fa");

    $("#menuToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("display-sidebar");
        if ($sidebarAndWrapper.hasClass("display-sidebar")) {
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right")
        } else {
            $icon.removeClass("fa-angle-right");
            $icon.addClass("fa-angle-left")
        }
    });

})();