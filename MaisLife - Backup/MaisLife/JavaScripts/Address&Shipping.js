$(document).on("click", "div[data-content='address']", function () {
    address.update($(this));
});

address = {
    update: function (div) {
        var tax = div.find("span[data-id='address-tax']");
        var shipping = $("input[data-id='address-shipping']");
        var total = $("input[data-id='address-total']");
        var constantTotal = total.attr("data-content");

        var address = $("input[name='address']");

        shipping.val("R$ " + tax.text());
        var taxValue = parseFloat(tax.text().replace(",", "."));
        var totalValue = parseFloat(constantTotal.replace(",", "."));        
        total.val(totalValue + taxValue);
        total.val("R$ " + total.val().replace(".", ","));

        var recipe = $("div[data-reactid='chng']");
        if (recipe.text() == "Endereço de entrega não escolhido!") {
            recipe.text("");
        }
        var change = $("input[data-id='chng']");
        change.val("");

        address.val(div.attr("data-id"));
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
    
    var buyPrice = $("input[data-id='address-total']").attr("data-content");    
    buyPrice = parseFloat(buyPrice.replace(",", "."));

    var buyPriceWShipping = $("input[data-id='address-total']").val();
    buyPriceWShipping = buyPriceWShipping.split(" ")[1];
    buyPriceWShipping = parseFloat(buyPriceWShipping.replace(",", "."));

    var recipe = $("div[data-reactid='chng']"); 

    if (buyPrice == buyPriceWShipping)
        recipe.html("Endereço de entrega não escolhido!");
    else if (digit >= buyPriceWShipping) {
        var change = digit - buyPriceWShipping;
        change = change.toFixed(2);
        recipe.html("Troco: R$ <strong>" + change + "</strong>");
    }else
        recipe.html("Valor insuficiente");
        

});

$(document).on("click", "button[data-id='buy-submit']", function () {   
    address.form();
});

method = {
    change: function (method) {
        var input = $("input[data-id='chng']");
        var phse = $("div[data-reactid='chng']");
        var methodInput = $("input[name='payMethod']");

        if (method == "creditcard") {
            input.prop("disabled", true);
            input.val("0");
            phse.text("Troco apenas para pagamento em dinheiro");
            methodInput.val("Cartão");
        }else{
            input.prop("disabled", false);
            input.val("0");
            phse.text("Valor insuficiente");
            methodInput.val("Dinheiro");
        }
    }
};

$(document).on("click", "label[data-id='method-cash']", function () {
    method.change("cash");
});

$(document).on("click", "label[data-id='method-creditcard']", function () {
    method.change("creditcard");
});