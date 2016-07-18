$(document).on("click", "div[data-content='address']", function () {
    address.enable($(this));
});

$(document).on("click", "button[data-id='shipping-calculate']", function () {
    address.calculateShipping($(this));
});

address = {
    enable: function (div) {
        var id = div.attr("data-info");
        var button = $("button[data-id='shipping-calculate']");
        button.attr("disabled", false);
        button.attr("data-info", id);
        var addressInput = $("input[name='address']");
        var addressId = div.attr("data-id");
        addressInput.val(addressId);
    },
    calculateShipping: function(button){
        var id = button.attr("data-info");
        $.ajax({
            type: "POST",
            url: '/Home/AjaxUse_Shipping',
            data: {
                id: id
            },
            success: function (data) {               
                var shippingInput = $("input[data-id='address-shipping']");
                shippingInput.val("R$ " + data.replace(".", ","));
                var totalInput = $("input[data-id='address-total']");
                var total = totalInput.attr("data-content");
                total = parseFloat(total);
                total = total + parseFloat(data);

                totalInput.val("R$ " + total.toFixed(2));

            },
            error: function () {
                alert("Erro com o servidor!");
            }
        });
    },
    form: function () {
        var pay = $("input[data-id='chng']").val();
        var payValueInput = $("input[name='payValue']");

        payValueInput.val(pay);
    }
}

$(document).on("keyup", "input[data-id='chng']", function () {
    var digit = $(this).val();
    digit = digit.trim();
    digit = parseFloat(digit.replace(",", "."));

    var finalButton = $("button[data-id='buy-submit']");
    
    var buyPrice = $("input[data-id='address-total']").attr("data-content");    
    buyPrice = parseFloat(buyPrice.replace(",", "."));

    var buyPriceWShipping = $("input[data-id='address-total']").val();
    buyPriceWShipping = buyPriceWShipping.split(" ")[1];
    buyPriceWShipping = parseFloat(buyPriceWShipping.replace(",", "."));

    var recipe = $("div[data-reactid='chng']"); 

    if (buyPrice == buyPriceWShipping){
        recipe.html("Endereço de entrega não escolhido!");
        finalButton.attr("disabled", true);
    } else if (digit >= buyPriceWShipping) {
        var change = digit - buyPriceWShipping;
        change = change.toFixed(2);
        recipe.html("Troco: R$ <strong>" + change + "</strong>");
        finalButton.attr("disabled", false);

    } else {
        recipe.html("Valor insuficiente");
        finalButton.attr("disabled", true);
    }
        
        

});

$(document).on("click", "button[data-id='buy-submit']", function () {   
    address.form();
});

method = {
    change: function (method) {
        var input = $("input[data-id='chng']");
        var phse = $("div[data-reactid='chng']");
        var methodInput = $("input[name='payMethod']");
        var finalButton = $("button[data-id='buy-submit']");
        var parcels = $("li[data-id='parcel']");

        if (method == "creditcard") {
            input.prop("disabled", true);
            input.val("0");
            phse.text("Troco apenas para pagamento em dinheiro");
            methodInput.val("Prazo");
            finalButton.attr("disabled", false);
            parcels.show();
        }else{
            input.prop("disabled", false);
            input.val("0");
            phse.text("Valor insuficiente");
            methodInput.val("A vista");
            finalButton.attr("disabled", true);
            parcels.hide();
        }
    }
};

$(document).on("click", "label[data-id='method-cash']", function () {
    method.change("cash");
});

$(document).on("click", "label[data-id='method-creditcard']", function () {
    method.change("creditcard");
});