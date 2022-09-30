
$(document).ready(function () {
    var butonum;

    $("[buttonActivePassive]").click(function () {


        var tiklananID = parseInt($(this).data("id"));

        butonum = $(this);

        var btn = butonum.closest("tr").find("[status-text]");

        $.ajax({
            url: '/FAQ/ActivePassiveQuestion',
            data: { ID: tiklananID },
            type: 'POST',
            success: function (response) {


                if (response == true) {

                    butonum.find(".material-icons").text("do_not_disturb_alt");
                    btn.removeClass("red"); btn.addClass("green");
                    btn.find("span").removeClass("red-text"); btn.find("span").addClass("green-text");
                    btn.find("span").html("Aktif");

                }
                if (response == false) {

                    butonum.find(".material-icons").text("done");
                    btn.removeClass("green"); btn.addClass("red");
                    btn.find("span").removeClass("green-text"); btn.find("span").addClass("red-text");
                    btn.find("span").html("Pasif");

                }
            }
        });
    });
})
