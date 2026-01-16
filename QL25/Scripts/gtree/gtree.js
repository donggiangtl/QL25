$(".caret").on("click", function () {
    var $first_nested = $(this)
        .toggleClass("caret-down") // Toggle caret-down class on the clicked element
        .parent().parent()
        .find(".nested:first"); // Find the first nested element inside the parent

    $first_nested.toggleClass("opened"); // Toggle opened class
    var $first_nested_i = $(this).find("i");
    if ($first_nested.hasClass("opened"))
        $first_nested_i.removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");
    else
        $first_nested_i.removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");
});
$(".cd").on("click", function () {
    $(".dv").removeClass("selected");
    $(".cd").removeClass("selected");
    $(this).addClass("selected");
});
$(".dv").on("click", function () {
    $(".dv").removeClass("selected");
    $(".cd").removeClass("selected");
    $(this).addClass("selected");
});



// Click elsewhere to hide menu
// Hide menu when clicking elsewhere
$(document).on("click", function () {
    $("#add_cd_contextMenu").hide();
    $("#show_cd_contextMenu").hide();
});

//// Menu actions
//$("#add_cd").on("click", function () {
//    alert($(this).text());
//});


$(document).ready(function () {
    console.log("document.ready");
    var $nested = $(".nested");
    $nested.each(function () {
        //alert($(this).text());
        var $nested_i = $(this).parent().find("i");
        if ($(this).hasClass("opened"))
            $nested_i.removeClass("fa-solid fa-plus").addClass("fa-solid fa-minus");
        else
            $nested_i.removeClass("fa-solid fa-minus").addClass("fa-solid fa-plus");
    });
});

