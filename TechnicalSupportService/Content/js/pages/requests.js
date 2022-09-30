

$(function () {


    var columns = [
        { "data": "Request_Id", "name": "1", "autoWidth": false, "visible": false },
        { "data": "User_Name", "name": "2", "autoWidth": true, "sortable": false },
        { "data": "Request_Priority_Name", "name": "3", "autoWidth": true },
        { "data": "RequestCategory_Name", "name": "4", "autoWidth": true },
        {
            "data": null, "name": "5", "autoWidth": false, "render": function (data, type, row) {

                var value = data["Request_Subject"];
                /*return data["Rate"] == null ? "" : data + " / 5";*/
                if (value.length > 20) {
                    return value.substring(0, 19);
                }
                else {
                    return value;
                }
            }
        },
        { "data": "Department_Name", "name": "6", "autoWidth": true },
        { "data": "Responsible_Name", "name": "7", "autoWidth": true },
        { "data": "Request_Undertaken", "name": "8", "autoWidth": true, "sortable": false },
        {
            "data": "Create_Date", "name": "9", "autoWidth": true, "render": function (data, type, row) {
                return ConvertJsonDateString(data);
            }
        },
        { 
            "data": null, "name": "10", "autoWidth": false, "render": function (data, type, row) {

                var value = data["Request_Undertaken_Date"];
                /*return data["Rate"] == null ? "" : data + " / 5";*/
                if (value != null) {
                    return ConvertJsonDateString(value);
                }
                else {
                    return "Üstlenilmemiş";
                }
            }

           
        },
        {
            "data": "End_Date", "name": "11", "autoWidth": true, "render": function (data, type, row) {
                return ConvertJsonDateString(data);
            }
        },
        {
            "data": null, "name": "12", "autoWidth": false, "sortable": false, "render": function (data, type, row) {

                var value = data["Request_Status"];
                /*return data["Rate"] == null ? "" : data + " / 5";*/
                if (value != null) {
                    return value;
                }
                else {
                    return "Bekliyor";
                }
            }

        },
        {
            "data": null, "name": "13", "autoWidth": true, "render": function (data, type, row) {

                var value = "<a class=\"waves-effect waves-light btn modal-trigger\" comment href=\"#modal1\">" + data["Rate"] + "/5" + "</a>";
                /*return data["Rate"] == null ? "" : data + " / 5";*/
                if (data["Rate"] != null) {
                    return value;
                }
                else {
                    return "Puanlanmamış";
                }
            }
        },

        

    ];



    var RequestType = $("input[name=RequestType]").val();

    var table = $('#data-table-simple').DataTable({
        "responsive": true,
        "columns": columns,
        "lengthChange": false,
        "searching": true,
        "scrollY": true,
        "ajax": {
            "url": "/Request/GetRequests?Type=" + RequestType,
            "type": "GET",
            "datatype": "json"
        },
        "createdRow": function (row, data, dataIndex) {
            $(row).attr('data-id', data["Request_Id"]);

        }
    });



    

   

    $(document).delegate("#request-list-undertaken", "change", function () {

        if ($(this).val()=="Yes") {
    
       
            table
                .columns(7)
                .search("true")
                .draw();

        } else if($(this).val() == "No") {
            table
                .columns(7)
                .search("false")
                .draw();
        } else {
            table
                .search('')
                .columns().search('')
                .draw();
        }

    });
    $(document).delegate("#request-list-priority", "change", function () {
           

        if ($(this).val()=="High") {
    
            table
                .columns(2)
                .search("Yüksek")
                .draw();

        } else if($(this).val() == "Medium") {
            table
                .columns(2)
                .search("Orta")
                .draw();
        }
        else if($(this).val() == "Min") {
            table
                .columns(2)
                .search("Düşük")
                .draw();
        } else {
            table
                .search('')
                .columns().search('')
                .draw();
        }

    });
    $(document).delegate("#request-list-category", "change", function () {
           
        table
            .columns(3)
            .search($(this).val())
            .draw();

    });
    $(document).delegate("#request-list-status", "change", function () {
           

        if ($(this).val()=="Active") {
    
       
            table
                .columns(11)
                .search("true")
                .draw();

        } else if($(this).val() == "Close") {
            table
                .columns(11)
                .search("false")
                .draw();
        } else {
            table
                .search('')
                .columns().search('')
                .draw();
        }

    });

    $(document).delegate("#data-table-simple tbody tr", "dblclick", function () {

        var a = document.createElement('a');
        a.target = '_blank';

        let id = $(this).attr('data-id')



        a.href = '/Message/MessagesByRequest/' + id;


        a.click();
    })
    $('#data-table-simple').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $(document).delegate("[comment]", "click", function () {
        let id = $(this).parent().parent().attr('data-id');
        console.log(id)
        $("#commentpartial").load("/FAQ/CommentPartial?id=" + id, function (responseTxt, statusTxt, xhr) {
            if (statusTxt == "success")
                $('.modal').modal('open');
            if (statusTxt == "error")
                alert("Error: " + xhr.status + ": " + xhr.statusText);
        });
    })

});



function ConvertJsonDateString(jsonDate) {
    var shortDate = null;
    if (jsonDate) {
        var regex = /-?\d+/;
        var matches = regex.exec(jsonDate);
        var dt = new Date(parseInt(matches[0]));
        var month = dt.getMonth() + 1;
        var monthString = month > 9 ? month : '0' + month;
        var day = dt.getDate();
        var dayString = day > 9 ? day : '0' + day;
        var year = dt.getFullYear();
        shortDate = dayString + '.' + monthString + '.' + year;
    }
    return shortDate;
};