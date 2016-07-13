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

$(document).on("click", "span[data-id='search-submit']", function () {
    var form = $("form[name='search-form']");
    form.submit();
});

$(document).on("click", "button[data-id='map-confirm']", function () {   
    $("div[data-id='fade-confirm-map']").show();
});

$(document).on("click", "button[data-id='confirm-map-yes']", function () {
    var form = $("form[name='confirm-form']");
    form.empty();
    var input = $("<input>");
    var id = $("#Id").val();
    input.attr({
        type: "hidden",
        name: "mapId",
        value: id
    });

    form.append(input);
    form.submit();
});