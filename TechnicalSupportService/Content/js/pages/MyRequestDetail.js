/*const { data } = require("jquery");*/

$(document).ready(function () {

    $(document).delegate("[rate-button]", "click", function () {
        //Serialize the form datas.
        var valdata = $("#rate_frm").serialize();
        
        var id;
        $.ajax({
            url: "/FAQ/RateRequest",
            type: "POST",
            dataType: 'json',
            success: function (sonuc) {
                if (sonuc == "OK") {
                    id = $("#Request_Id").attr("value");
                    
                    
                    $('#reply-text').html("<span>Geri bildiriminiz için teşekkürler</span>");
                    $('[rate-section]').hide();
                    //$.confirm({
                    //    title: 'Alert!',
                    //    content: 'New blacklist note added',
                    //    autoClose: 'OK|5000',
                    //    buttons: {

                    //        OK: function () {
                    //        }
                    //    }
                    //});
                    //$("#notespartial").load("/BlackList/GetNotesPartial?id=" + id, function (response, status, xhr) {
                    //    if (status != "success") {
                    //        alert("Başarısız");
                    //    }
                    //});
                    //$("#addnote_frm").trigger("reset");
                }
            },
            //contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            data: valdata
        });

    });

})



function LoadImage(input, durum) {


    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {


            if (durum == 0) {
                $("#targetImag").attr("src", e.target.result);
            } else {
                $("#targetImage").attr("src", e.target.result);
            }


        };



        reader.readAsDataURL(input.files[0]);

    }
}