$(document).ready(function () {
    $("[data-toggle='dropdown']").click(function (e) {
        $(this).parents(".dropdown").toggleClass("open");
    });

    $("html").click(function () {
        $(".open").removeClass("open");
    });
});