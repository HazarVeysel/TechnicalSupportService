

$(function () {

    


    var columns = [
        { "data": "Id", "name": "1", "autoWidth": false, "visible": false },
        { "data": "UserName", "name": "3", "autoWidth": true },
        { "data": "NameSurname", "name": "4", "autoWidth": true },
        { "data": "Bolum", "name": "5", "autoWidth": true },
        { "data": "Email", "name": "6", "autoWidth": true, "sortable": false },
        { "data": "TelefonNo", "name": "7", "autoWidth": true, "sortable": false },
        { "data": "Status", "name": "8", "autoWidth": true, "sortable": false }

    ];

    $('#data-table-simple').DataTable({
        "responsive": true,
        "columns": columns,
        "lengthChange": false,
        "searching": false,
        "scrollY": true,
        "ajax": {
            "url": "/User/GetAllUsers",
            "type": "GET",
            "datatype": "json"
        },
        "createdRow": function (row, data, dataIndex) {
            $(row).attr('data-id', data["Id"]);

        }
    });

    $(document).delegate("#data-table-simple tbody tr", "dblclick", function () {

      

        var a = document.createElement('a');
        a.target = '_blank';

        let id = $(this).attr('data-id');
        
        a.href = "/User/UsersRequests?id=" + id;
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