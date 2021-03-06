﻿$(document).on("click", "tr[data-content='patner-row']", function () {
    patner.select($(this));
});

$(document).on("click", "li[data-id='item-remove']", function () {
    patner.callDeleteConfirm("fade-remove");
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

$(document).on("change", "select[name='Metodo']", function () {
    if ($(this).val() == "Prazo") {
        $("#Parcelas").attr("disabled", false);
        $("#Vencimento").attr("disabled", true);
    } else {
        $("#Parcelas").attr("disabled", true);
        if ($(this).val() == "Boleto") {
            $("#Vencimento").attr("disabled", false);
        } else {
            $("#Vencimento").attr("disabled", true);
        }
    }
});

$(document).on("change", "select[name='Tipo']", function () {
    if ($(this).val() == "Troca") {
        $("#MotivoTroca").attr("disabled", false);
    }else{
        $("#MotivoTroca").attr("disabled", true);
    }

});

$(document).on("click", "label[data-content='checkbox']", function () {
    if ($(this).attr("data-info") == "unchecked") {
        $(this).addClass("active");
        $(this).find("input").attr("checked", true);
        $(this).attr("data-info", "checked");
    } else {
        $(this).removeClass("active");
        $(this).find("input").attr("checked", false);
        $(this).attr("data-info", "unchecked");
    }
});

$(document).on("change", "#UsuarioExterno_Id", function () {
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
    if (select.val() > 0) {
        externalOrderProducts.amountChange($(this));
    } else {
        $(this).val("");
    }
        
});

$(document).on("click", "button[data-id='more-product']", function () {
    externalOrderProductsManager.cloneBox();    
});

$(document).on("click", "button[data-id='remove-product']", function () {
    externalOrderProductsManager.removeBox($(this));
    //externalOrderProducts.calculateTotal();
});

$(document).on("click", "button[data-id='panel-submit']", function (e) {
    e.preventDefault();
    externalOrderProductsManager.listFields();
    $(this).closest("form").submit();
});

$(document).on("change", "#Desconto", function () {
    if ($(this).val() > 100) 
        $(this).val(100);
    
    externalOrderProducts.calculateTotal();
});

$(document).on("click", "button[data-id='filter-btn']", function (e) {
    if ($(this).attr("data-info") == "closed") {
        $(this).attr("data-info", "opened");
        $("#filterCollapse").slideDown();
    } else {
        $(this).attr("data-info", "closed");
        $("#filterCollapse").slideUp();
    }
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
    callDeleteConfirm: function (target) {
        if (patner.checkItemCount() > 0) {           
            var fade = $("div[data-id='"+target+"']");

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
        var name = $("input[name='UsuarioExterno.Nome']");
        var contact = $("input[name='UsuarioExterno.Telefone']");
        var document = $("input[name='UsuarioExterno.Documento']");

        var city = $("input[name='UsuarioExterno.Endereco1.Cidade']");
        var local = $("select[name='UsuarioExterno.Endereco1.Bairro1.Id']");
        var street = $("input[name='UsuarioExterno.Endereco1.Rua']");
        var number = $("input[name='UsuarioExterno.Endereco1.Numero']");

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
            url: '/VendaExterna/AjaxUse_ClientsQuery',
            data: {
                exid: id
            },
            success: function (data) {
                var result = JSON.parse(data);

                inputs[0].val(result.Nome);
                inputs[1].val(result.Telefone);
                inputs[2].val(result.Documento);
                inputs[3].val(result.Endereco1.Cidade);
                inputs[4].val(result.Endereco1.Bairro1.Id);
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
        var orderTotal = $("input[name='Valor']");

        var inputs = [];
        inputs.push(un, amount, price, total, orderTotal);

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
            url: '/VendaExterna/AjaxUse_ProductsQuery',
            data: {
                id: object.val()
            },
            success: function (data) {
                var result = JSON.parse(data);               

                inputs[0].val(result.Unidade + "Un");                
                inputs[1].val("1");
                inputs[2].val("R$ " + result.Preco);
                inputs[3].val("R$ " + result.Preco);
                //inputs[4].val("R$ " + result.Preco);

                externalOrderProducts.calculateTotal();

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
        externalOrderProducts.calculateTotal();
    },
    calculateTotal: function () {        
        var content = $("div[data-id='products']");       
        var total = 0;
        content.find(".field-box").each(function () {
            if ($(this).find("input[name='product-price']").val() != "") {
                var price = parseFloat($(this).find("input[name='product-price']").val().split(" ")[1]);
                var count = parseInt($(this).find("input[name='product-count']").val());
                total += price * count;
            }            
        });
        
        var discount = $("#Desconto").val();
        total -= total * (discount / 100.00);
        var totalInput = $("input[name='Valor']");
        totalInput.val(total.toFixed(2).replace(".", ","));

    }
}


var externalOrderProductsManager = {
    cloneBox: function () {
        var box = $("div[data-id='product-box']");
        var clone = box.clone();

        clone.attr("data-id", false);
        clone.find("button[data-id='more-product']").remove();
        var input = clone.find("input[name='product-un']");
        input.val("");
        var input = clone.find("input[name='product-count']");
        input.val("");
        var input = clone.find("input[name='product-total']");
        input.val("");
        var input = clone.find("input[name='product-price']");
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
