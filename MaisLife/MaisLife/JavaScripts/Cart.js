$(document).on("click", "button[data-id='update-cart']", function () {      
    cart.update();
});

var cart = {
    update: function () {
        var i = 1;

        var form = $("form[data-id='update-cart']");

        $("input[data-content='rel-qtd']").each(function () {
            $(this).attr("name", "qtd-" + i);
            var id = $(this).attr("data-id");
            var hidden = $("input[name='" + id + "']");
            hidden.attr("name", "hidden-" + i);
            i++;
        });

        form.children("input[name='amount']").remove();
        var input = $("<input>");
        input.attr("name", "amount");
        input.attr("type", "hidden");
        input.val(i - 1);

        form.prepend(input);
        form.submit();

    }
};

$(document).on("change", "input[data-content='address-type']", function () {    
    shipping.enable($(this));
});

var shipping = {
    enable: function (radio) {
        var id = radio.val();
        var select = $("select[data-id='local-select']");
        var text = $("input[data-id='local-text']");
        if (id == '0') {
            select.prop("disabled", false);
            text.prop("disabled", true);
        } else if (id == '1') {
            text.prop("disabled", false);
            select.prop("disabled", true);
        }
    }
};

