/*const { data } = require("jquery");*/

$(document).ready(function () {

    /*$(".hide").hide()*/
    $("[preview]").mousedown(function () {
        $("#hideimg").removeClass("hide"); 
    })
    //$("#editbtn").mouseenter(function () {
    //    $(".hide").hide("fast")
    //})

    var butonum;

    $("[buttonActivePassive]").click(function () {

   
        var tiklananID = parseInt($(this).data("id"));

        butonum = $(this);

        var btn = butonum.closest("tr").find("[status-text]");

        $.ajax({
            url: '/FAQ/ActivePassive',
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


    function validateCategoryControlForm() {

        $('form').each(function () {
            $(this).validate({

                rules: {
                    Category_Name: {
                        minlength: 2,
                        maxlength: 50,
                    },
                    Category_Description: {
                        minlength: 2,
                        maxlength: 100,
                    }
                },
                messages: {
                    Category_Name: {
                        required: "Please enter the category name",
                        minlength: "Name at least 2 characters",
                        maxlength: "Name must be less than 50 characters"
                    },
                    Category_Description: {
                        required: "Please enter the category description",
                        minlength: "Description at least 2 characters",
                        maxlength: "Description must be less than 100 characters"
                    },
                },
                submitHandler: function (form) {
                    form.submit();
                }
            })
        });
        //$("#edit_frm").validate({
        //    rules: {
        //        Category_Name: {
        //            minlength: 2,
        //            maxlength: 5,
        //        },
        //        Category_Description: {
        //            minlength: 2,
        //            maxlength: 10,
        //        }
        //    },
        //    messages: {
        //        Category_Name: {
        //            required: "Please enter the category name",
        //            minlength: "Name at least 2 characters",
        //            maxlength: "Name must be less than 50 characters"
        //        },
        //        Category_Description: {
        //            required: "Please enter the category description",
        //            minlength: "Description at least 2 characters",
        //            maxlength: "Description must be less than 100 characters"
        //        },
        //    },
        //    submitHandler: function (form) {
        //        form.submit();
        //    }
        //})
    }
    validateCategoryControlForm(); //Bu silinmeyecek, add category kısmında bu çalışıyor.

    $(document).delegate("#editCategory", "click", function () {
        validateCategoryControlForm();
    });
    $("[edit-button]").click(function () {

        var id = $(this).data("id");
        alert(id)
        $.ajax({
            url: '/FAQ/OnClickedCategory/'+id,
            data: { ID: id },
            type: 'GET',
            dataType: 'json',
            success: function (response) {                
                console.log(response)
                if (response) {
                    $('#EditCategoryTitle').html("<b>Mevcut Kategori Düzenle</b>");
                    $('#Category_Id').val(response.Category_ID);
                    $('#Category_Name').val(response.Category_Name);
                    $('#Category_Description').val(response.Category_Description);
                    $('#Category_Parent_ID').val(response.Category_Parent_ID).formSelect();
                    $("#hideimg").removeClass("hide"); 
                    $("#targetImag").attr("src", response.Img_Url);
                }
                //    //butonum.find(".material-icons").text("do_not_disturb_alt");
                //    //btn.removeClass("red"); btn.addClass("green");
                //    //btn.find("span").removeClass("red-text"); btn.find("span").addClass("green-text");
                //    //btn.find("span").html("Aktif");

                //}
                //if (response == false) {
                //    alert(id+id)
                //    //butonum.find(".material-icons").text("done");
                //    //btn.removeClass("green"); btn.addClass("red");
                //    //btn.find("span").removeClass("green-text"); btn.find("span").addClass("red-text");
                //    //btn.find("span").html("Pasif");

                //}
            }
        });


        //$("#guncellemepartial").load("/FAQ/partialcagir?id=" + id, function (responseTxt, statusTxt, xhr) {
        //    if (statusTxt == "success") {
        //        $('#modaledit').modal();
               
        //    }
        //    if (statusTxt == "error")
        //        alert("Error: " + xhr.status + ":" + xhr.statusText);
        //});
    })
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