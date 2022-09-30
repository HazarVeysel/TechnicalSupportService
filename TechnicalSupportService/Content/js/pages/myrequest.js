$(document).ready(function () {
    //$(document).delegate("#data-table-myreq tbody tr", "click", function () {
    //    alert("a");
    //    console.log((this).b.attr('style', 'background-color: yellow'));
    //        /*parents('tr').attr('style', 'background-color: yellow')*/
    //});


    $(function () {


        var columns = [
            { "data": "Request_Id", "name": "1", "autoWidth": false, "visible": false },
            { "data": "User_Name", "name": "2", "autoWidth": true, "sortable": false, "visible": false },
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
                "data": "Request_Undertaken_Date", "name": "10", "autoWidth": true, "render": function (data, type, row) {
                    return ConvertJsonDateString(data);
                }
            },
            {
                "data": "End_Date", "name": "11", "autoWidth": true, "render": function (data, type, row) {
                    return ConvertJsonDateString(data);
                }
            },
            { "data": "Request_Status", "name": "12", "autoWidth": true, "sortable": false }

        ];

        var UID = $("input[name=User_Id]").val();
        var table = $('#data-table-myreq').DataTable({
            "responsive": true,
            "columns": columns,
            "lengthChange": false,
            "searching": false,
            "scrollY": true,
            "ajax": {
                "url": "/FAQ/MyRequest?id=" + UID,
                "type": "GET",
                "datatype": "json"
            },
            "createdRow": function (row, data, dataIndex) {
                $(row).attr('data-id', data["Request_Id"]);

            }
        });

        $(document).delegate("#data-table-myreq tbody tr", "dblclick", function () {

            var a = document.createElement('a');
            a.target = '_blank';

            let id = $(this).attr('data-id')



            a.href = '/FAQ/MyRequestDetail/' + id;


            a.click();
        })


        $('#data-table-myreq').on('click', 'tr', function () {
            if ($(this).hasClass('selected')) {
                $(this).removeClass('selected');
            } else {
                table.$('tr.selected').removeClass('selected');
                $(this).addClass('selected');
            }
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
