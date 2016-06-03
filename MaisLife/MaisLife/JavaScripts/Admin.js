$(document).on("click", "tr[data-content='patner-row']", function () {
    patner.select($(this));
});

$(document).on("click", "li[data-id='item-remove']", function () {
    patner.callDeleteConfirm();
});

$(document).on("click", "button[data-id='confirm-panel-close']", function () {
    patner.closeDeleteConfirm();
});

$(document).on("click", "button[data-id='confirm-panel-yes']", function () {
    patner.remove();
    patner.closeDeleteConfirm();
});





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
    }
};