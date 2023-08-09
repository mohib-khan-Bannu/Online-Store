$(document).ready(function () {
   
});

function ShowCategroy() {

    $.ajax({
        url: '/Clients/GetCategorlist',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
               

            });
            $('#table_data').html(object);

        },
        error: function () {
            alert('Data Cant Get');

        },
    });
