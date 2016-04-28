// BENEFITS VAR
var benefits = {
    more: function (span) {
        $("li[data-reactid='more-benefits']").each(function () {
            $(this).removeClass("more");
        });
        span.attr("data-bool", "true");
        span.text("Ver menos");

        var glyphicon = $("span[data-target='more-benefits']");
        glyphicon.removeClass("glyphicon-chevron-down");
        glyphicon.addClass("glyphicon-chevron-up");


    },
    less: function (span) {
        $("li[data-reactid='more-benefits']").each(function () {
            $(this).addClass("more");
        });
        span.attr("data-bool", "false");
        span.text("Mais benefícios");

        var glyphicon = $("span[data-target='more-benefits']");
        glyphicon.removeClass("glyphicon-chevron-up");
        glyphicon.addClass("glyphicon-chevron-down");
    }
};

// BENEFITS TRIGGER
$(document).on("click", "span[data-id='more-benefits']", function () {
    var bool = $(this).attr("data-bool");
    if (bool == "false")
        benefits.more($(this));
    else
        benefits.less($(this));
});