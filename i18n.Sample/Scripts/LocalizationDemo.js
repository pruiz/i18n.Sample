!function ($) {

$(document).ready(function () {
    $("#ScriptTest").on("click", function () {
        alert(_("Esto es un mensaje sin localizar."));
    });
});

}(window.jQuery);