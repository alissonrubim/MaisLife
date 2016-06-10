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
    report: function () {
        /*
        var report = $("div[data-id='delivery-report']");
        var same = false;
        var locals = [];
        $("select[data-id='delivery-local']").each(function () {            
            console.log("fdsfsd");
            for (var i = 0; i < locals.length; i++) {
                if ($(this).val() == locals[i] && $(this).val() != "0")
                    same = true;
                else
                    locals.push($(this).val());
            }

            if (same) {
                report.text("Existem dois bairros iguais!");
                report.show();
            }               

        });
        */
    },
    listFields: function () {
        var content = $("div[data-id='deliverys']");
        var i = 1;
        content.find(".field-box").each(function () {
            var select = $(this).find("select");
            if ( select.val() != "0" ){
                select.attr("name", "delivery-local-" + i);                
                $(this).find("input").attr("name", "delivery-tax-" + i);
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