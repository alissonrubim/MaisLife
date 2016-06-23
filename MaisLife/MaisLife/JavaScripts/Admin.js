$(document).on("click", "tr[data-content='patner-row']", function () {
    patner.select($(this));
});

$(document).on("click", "li[data-id='item-remove']", function () {
    patner.callDeleteConfirm();
});

$(document).on("click", "li[data-id='item-edit']", function () {
    patner.completeFields();
});

$(document).on("click", "button[data-id='confirm-panel-close']", function () {
    patner.closeDeleteConfirm();
});

$(document).on("click", "button[data-id='confirm-panel-yes']", function () {
    patner.remove();
    patner.closeDeleteConfirm();
});

$(document).on("click", "button[data-id='more-delivery']", function () {
    delivery.cloneBox();
});

$(document).on("click", "button[data-id='remove-delivery']", function () {
    delivery.removeBox($(this));
});

$(document).on("click", "button[data-id='panel-submit']", function () {    
    delivery.listFields();
    $(this).closest("form").submit();
});

$(document).on("change", "select[data-id='client-select']", function () {
    var val = $(this).val();
    if (val > 0)
        externalOrder.addressChange(val);
    else
        externalOrder.resetInputs();
});

$(document).on("change", "select[data-id='product-select']", function () {
    var val = $(this).val();
    if (val > 0)
        externalOrderProducts.productSelect($(this));
    else
        externalOrderProducts.resetInputs($(this));
});

$(document).on("change", "input[name='product-count']", function () {
    var dad = $(this).closest(".field-box");    
    var select = dad.find("select");
    if (select.val() > 0)
        externalOrderProducts.amountChange($(this));
});

$(document).on("click", "button[data-id='more-product']", function () {
    externalOrderProductsManager.cloneBox();
});

$(document).on("click", "button[data-id='remove-product']", function () {
    externalOrderProductsManager.removeBox($(this));
});

$(document).on("click", "button[data-id='panel-submit']", function (e) {
    e.preventDefault();
    externalOrderProductsManager.listFields();
    $(this).closest("form").submit();
});

var delivery = {
    cloneBox: function () {
        var box = $("div[data-id='delivery-box']");
        var clone = box.clone();

        clone.attr("data-id", false);
        clone.find("button[data-id='more-delivery']").remove();
        var input = clone.find("input[data-id='delivery-tax']");
        input.val("");

        var icon = $("<span>");
        icon.addClass("glyphicon glyphicon-remove");

        var button = $("<button>");
        button.attr({
            "data-id": "remove-delivery",
            type: "button"

        });
        button.addClass("btn btn-danger");
        button.append(icon);

        clone.find(".field-more").append(button);
        
        var content = $("div[data-id='delivery-content']");
        content.append(clone);       
    },
    removeBox: function (btn) {
        btn.closest("div[data-content='delivery-field']").remove();       
    },
    listFields: function () {
        var content = $("div[data-id='deliverys']");
        var i = 1;
        content.find(".field-box").each(function () {
            var select = $(this).find("select");
            var input = $(this).find("input");
            if ( select.val() != "0" && input.val() != ""){
                select.attr("name", "delivery-local-" + i);                
                input.attr("name", "delivery-tax-" + i);
                i++;
            }
            
        });
        var amount = $("input[name='delivery-amount']");
        amount.val(i - 1);

    }
}





var patner = {
    select: function (tr) {
        var form = $("form[name='item-remove']");

        if (tr.attr("data-info") == "unselected") {            
            var input = $("<input>");
            input.attr({
                type: "hidden",
                name: "hidden-pre-submit",
                value: tr.attr("data-id")
            });

            form.append(input);

            tr.attr("data-info", "selected");
            tr.addClass("selected");
        } else {
            form.find("input[value='" + tr.attr("data-id") + "']").remove();

            tr.attr("data-info", "unselected");
            tr.removeClass("selected");
        }

        var remove = $("li[data-id='item-remove']");
        var edit = $("li[data-id='item-edit']");
        if (patner.checkItemCount() > 0){
            remove.removeClass("disabled");
            if (patner.checkItemCount() == 1)
                edit.removeClass("disabled");
            else
                edit.addClass("disabled");
        }
        else {
            remove.addClass("disabled");
            edit.addClass("disabled");
        }
            
        

    },
    checkItemCount: function(){       
        var check = 0;
        $("tr[data-content='patner-row']").each(function () {
            if ($(this).attr("data-info") == "selected")
                check++;
        });        
        return check;
    },
    pickSelected: function () {
        var id = 0;
        $("tr[data-content='patner-row']").each(function () {
            if ($(this).attr("data-info") == "selected")
                id = $(this).attr("data-id");
        });        
        return id;
    },
    callDeleteConfirm: function () {
        if (patner.checkItemCount() > 0) {           
            var fade = $(".fade-io");

            fade.show();
        }
       
    },
    closeDeleteConfirm: function () {        
        var fade = $(".fade-io");

        fade.hide();
    },
    remove: function () {
        var form = $("form[name='item-remove']");
        var i = 1;
        form.children("input").each(function () {
            $(this).attr("name", "item-" + i);
            i++;
        });

        var input = $("<input>");
        input.attr({
            type: "hidden",
            name: "count",
            value: i-1
        });

        form.append(input);
        form.submit();
    },
    completeFields: function () {
        var id = patner.pickSelected(); 
        var form = $("form[name='item-edit']");

        var input = $("<input>");
        input.attr({
            type: "hidden",
            name: "item",
            value: id
        });

        form.append(input);
        form.submit();
    }
};


var externalOrder = {
    getInputs: function () {
        var name = $("input[name='client-name']");
        var contact = $("input[name='client-phone']");
        var document = $("input[name='client-doc']");

        var city = $("input[name='client-city']");
        var local = $("input[name='client-local']");
        var street = $("input[name='client-street']");
        var number = $("input[name='client-number']");

        var inputs = [];
        inputs.push(name, contact, document, city, local, street, number);

        return inputs;
    },
    resetInputs: function () {
        var inputs = externalOrder.getInputs();

        inputs.forEach(function (item) {
            item.attr("disabled", false);
            item.val("");
        });
    },
    addressChange: function (id) {
        var inputs = externalOrder.getInputs();
        $.ajax({
            type: "POST",
            url: '../ExternalUsersAjax',
            data: {
                id: id
            },
            success: function (data) {
                var result = JSON.parse(data);

                inputs[0].val(result.Nome);
                inputs[1].val(result.Telefone);
                inputs[2].val(result.Documento);
                inputs[3].val(result.Endereco1.Cidade);
                inputs[4].val(result.Endereco1.Bairro1.Nome);
                inputs[5].val(result.Endereco1.Rua);
                inputs[6].val(result.Endereco1.Numero);

                inputs.forEach(function (item) {
                    item.attr("disabled", true);
                });



            },
            error: function () {
                alert("Erro com o servidor!");
            }
        });
    }
};

var externalOrderProducts = {    
    getInputs: function (object) {
        var dad = object.closest(".field-box");

        var un = dad.find("input[name='product-un']");
        var amount = dad.find("input[name='product-count']");
        var price = dad.find("input[name='product-price']");
        var total = dad.find("input[name='product-total']");

        var inputs = [];
        inputs.push(un, amount, price, total);

        return inputs;        
    },
    resetInputs: function (object) {
        var inputs = externalOrderProducts.getInputs(object);

        inputs.forEach(function (item) {            
            item.val("");
        });
    },
    productSelect: function (object) {
        var inputs = externalOrderProducts.getInputs(object);
        $.ajax({
            type: "POST",
            url: '../ExternalUsersProductsAjax',
            data: {
                id: object.val()
            },
            success: function (data) {
                var result = JSON.parse(data);               

                inputs[0].val(result.Unidade + "Un");                
                inputs[1].val("1");
                inputs[2].val("R$ " + result.Preco);
                inputs[3].val("R$ " + result.Preco);

            },
            error: function () {
                alert("Erro com o servidor!");
            }
        });
    },
    amountChange: function (object) {       
        var inputs = externalOrderProducts.getInputs(object);

        var amount = parseInt(inputs[1].val());       
        var price = parseFloat(inputs[2].val().split(" ")[1]);        
        inputs[3].val("R$ " + amount * price);
    }
}


var externalOrderProductsManager = {
    cloneBox: function () {
        var box = $("div[data-id='product-box']");
        var clone = box.clone();

        clone.attr("data-id", false);
        clone.find("button[data-id='more-product']").remove();
        var input = clone.find("input[name='product-amount']");
        input.val("");

        var icon = $("<span>");
        icon.addClass("glyphicon glyphicon-remove");

        var button = $("<button>");
        button.attr({
            "data-id": "remove-product",
            type: "button"

        });
        button.addClass("btn btn-danger");
        button.append(icon);

        clone.find(".field-more").append(button);

        var content = $("div[data-id='product-content']");
        content.append(clone);
    },
    removeBox: function (btn) {
        btn.closest("div[data-content='product-field']").remove();
    },
    listFields: function () {
        var content = $("div[data-id='products']");
        var i = 1;
        content.find(".field-box").each(function () {
            var select = $(this).find("select");
            var input = $(this).find("input[name='product-count']");
            if (select.val() > 0 && input.val() > 0) {
                select.attr("name", "product-" + i);
                input.attr("name", "product-count-" + i);
                i++;
            }

        });
        var amount = $("input[name='product-amount']");
        amount.val(i - 1);

    }
}
