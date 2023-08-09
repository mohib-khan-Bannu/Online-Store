$(document).ready(function () {
    ShowItemsData();
});
function ShowItemsData() {


    $.ajax({
        url: '/Item/ItemList',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (results) {
            var object = '';
            $.each(results, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.itemId + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.price + '</td>';
                object += '<td>' + item.quantity + '</td>';
                object += '<td>' + item.itemInStock + '</td>';
                object += '<td>' + item.expireDate + '</td>';
                object += '<td>' + item.categoryName + '</td>';
                object += '<td><img style="width: 50px; hieght: 50px;" src="' + item.profileImage + '" /></td>';
                object += '<td><a href="#" class="btn btn-primary "onclick="Edit(' + item.itemId + '); ">Edit</a> ||<a href="#" class="btn btn-danger" onclick="Delete(' + item.itemId + ');">Delete</td>';
                object += '</tr>';

            })
            $('#ItemDataTable').html(object);

        },
        error: function () {
            alert('Data Cant Get');

        },
    });

}
$('#additems').click(function () {

    $('#ItemsModal').modal('show');



});

function AddItems() {

    debugger

    var formdata = new FormData();
    formdata.append('Name', $('#name').val());
    formdata.append('Price', $('#price').val());
    formdata.append('Quantity', $('#quantity').val());
    formdata.append('ItemStoack', $('#itemInStock').val());
    formdata.append('ExpireDate', $('#expireDate').val());
    formdata.append('CategoryId', $('#categoryId').val());
    formdata.append('ProfileImage', document.getElementById("fileUpload").files[0]);

    $.ajax({
        url: '/Item/AddItems',
        type: 'POST',
        data: formdata,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function () {
            ClearFuntion();
            HideModalPopup();
            alert('Data is Saved');

        },
        error: function () {
            alert('Data Cant Saved');

        }
    });

}
function Edit(id) {

    $.ajax({
        url: '/Item/Edit?id=' + id,
        type: 'GET',

        contentType: 'application/json;charset=utf-8;',
        dataType: 'json',
        success: function (response) {

            $('#ItemID').val(response.itemId),
                $('#name').val(response.name),
                $('#price').val(response.price);
            $('#quantity').val(response.quantity);
            $('#itemInStock').val(response.itemInStock);
            $('#categoryId').val(response.categoryId);
            $('#imgpt').attr('src', response.profileImage);

            const date = new Date(response.expireDate);

            document.getElementById('date').valueAsDate = date;

            $('#additemdata').hide();
            $('#updateItem').show();
            $('#ItemsModal').modal('show');

        },
        error: function () {
            alert('Data not Founs');
        },

    });
}
function Delete(id) {
    debugger
    if (confirm('Are You Sure You Want to Delete')) {
        $.ajax({
            url: '/Item/Delete?id='  + id,
            type: 'GET',
            dataType: 'json',
            success: function () {
                ShowItemsData();
                alert('Data Delete')
            },
            error: function () {
                alert('Data Cant Delete');

            }
            

        })
    }

}
function UpdateItem() {
    var formdata = new FormData();
    formdata.append('itemId', $('#ItemID').val());
    formdata.append('Name', $('#name').val());
    formdata.append('Price', $('#price').val());
    formdata.append('Quantity', $('#quantity').val());
    formdata.append('ItemInStock', $('#itemInStock').val());
    formdata.append('ExpireDate', $('#date').val());
    formdata.append('CategoryId', $('#categoryId').val());
    formdata.append('ProfileImage', document.getElementById("fileUpload").files[0]);



    $.ajax({
        url: '/Item/UpdateItem',
        type: 'Post',
        data: formdata,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function () {
            ShowItemsData();
            HideModalPopup();
            ClearFuntion();
            alert('Data Update');


        },
        error: function () {
            alert('REquest Filed');
        }

    });
}
function HideModalPopup() {
    $('#ItemsModal').modal('hide');

}
function ClearFuntion() {
    $('#name').val(''),
        $('#price').val('');
    $('#quantity').val('');
    $('#itemInStock').val('');
    $('#ExpireDate').val(''),
        $('#categoryId').val('');

}


function readitemsURL(input) {

    var url = input.value;
    var ext = url.substring(url.lastIndexOf('.') + 1).toLowerCase();
    if (input.files && input.files[0] && (ext == "gif" || ext == "png" || ext == "jpeg" || ext == "jpg")) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgpt').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
    else {
        $('#imgpt').attr('src', '~/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg');
    }
}
$(function () {
    $(".btn-defult").click(function () {
        $("#ItemsModal").modal("hide");
    });
});

function AddtoCart(item) {
    var itemId = $(item).attr("itemid");
    alert("Ok");
}