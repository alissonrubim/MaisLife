$(document).on("click", "div[data-content='order-box']", function () {
    var info = $(this).attr("data-info");
    if (info == "unselected") {
        $(this).addClass("selected");
        $(this).attr("data-info", "selected");
    } else {
        $(this).removeClass("selected");
        $(this).attr("data-info", "unselected");
    }
    
});

$(document).on("click", "button[data-id='map-submit']", function () {
    var box = $("#order-box");
    var form = $("form[name='map-data']");
    form.empty();
    var itens = 1;    
    box.find("div[data-content='order-box']").each(function () {
        var info = $(this).attr("data-info");
        if (info == "selected") {
            var input = $("<input>");
            var id = $(this).attr("data-id");
            input.attr({
                type: "hidden",
                name: "order-" + itens,
                value: id
            });
            form.append(input);
            itens++;
        }      
    });

    var input = $("<input>");
    input.attr({
        type: "hidden",
        name: "orderCount",
        value: itens-1
    });

    form.append(input);

    form.submit();

});