

$(function () {


    var columns = [
        { "data": "Request_Id", "name": "1", "autoWidth": false, "visible": false },
        { "data": "User_Name", "name": "2", "autoWidth": true, "sortable": false },
        { "data": "Request_Priority_Name", "name": "3", "autoWidth": true },
        { "data": "RequestCategory_Name", "name": "4", "autoWidth": true },
        { "data": "Request_Subject", "name": "5", "autoWidth": true, "sortable": false },
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

    $('#data-table-sim').DataTable({
        "responsive": true,
        "columns": columns,
        "lengthChange": false,
        "searching": false,
        "scrollY": true,
        "ajax": {
            "url": "/User/RequestsByUser?id=" + UID,
            "type": "GET",
            "datatype": "json"
        },
        "createdRow": function (row, data, dataIndex) {
            $(row).attr('data-id', data["Request_Id"]);

        }
    });

    $(document).delegate("#data-table-sim tbody tr", "dblclick", function () {

        var a = document.createElement('a');
        a.target = '_blank';

        let id = $(this).attr('data-id')
        alert(id)


        a.href = '/Message/MessagesByRequest/' + id;


        a.click();
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