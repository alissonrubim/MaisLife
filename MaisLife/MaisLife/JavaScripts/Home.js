var benefits = {
    more: function (li) {
        $("li[data-reactid='more-benefits']").each(function () {
            $(this).removeClass("more");
        });
        li.attr("data-bool", "true");

    },
    less: function (li) {
        $("li[data-reactid='more-benefits']").each(function () {
            $(this).addClass("more");
        });
        li.attr("data-bool", "false");
        li.find("span").text("fds");
    }
};

$(document).on("click", "li[data-id='more-benefits']", function () {  
    var bool = $(this).attr("data-bool");
    if (bool == "false")
        benefits.more($(this));
    else
        benefits.less($(this));
});