// LINK SCROLL
$(document).on("click", "a[data-content='scroll-link']", function () {    
    var id = $(this).attr("data-target");
    var target = $("#" + id);

    $('html, body').stop().animate({
        'scrollTop': target.offset().top
    }, 900, 'swing', function () {
        window.location.hash = target;
    });
});

// BENEFITS VAR
var benefits = {
    scroll: function(){
        var target = $("#benefits");

        $('html, body').stop().animate({
            'scrollTop': target.offset().top
        }, 900, 'swing', function () {
            window.location.hash = target;
        });
    },
    more: function (span) {
        $("li[data-reactid='more-benefits']").each(function () {
            $(this).removeClass("more");
        });
        span.attr("data-bool", "true");
        span.text("Ver menos");

        var glyphicon = $("span[data-target='more-benefits']");
        glyphicon.removeClass("glyphicon-chevron-down");
        glyphicon.addClass("glyphicon-chevron-up");

        benefits.scroll();
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

        benefits.scroll();
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