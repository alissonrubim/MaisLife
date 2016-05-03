$(document).on("click", "div[data-content='address']", function () {
    address.update($(this));
});

address = {
    update: function (div) {
        var tax = div.find("span[data-id='address-tax']");
        var shipping = $("input[data-id='address-shipping']");
        var total = $("input[data-id='address-total']");

        shipping.val("R$ " + tax.text());
        var taxValue = parseFloat(tax.text());        
        var totalValue = parseFloat(total.val().split(" ")[1]);       
        total.val(totalValue + taxValue);
        total.val("R$ " + total.val());

    }
}