

$(document).ready(function () {
    ShowCatData();
});

function ShowCatData() {

    $.ajax({
        url: '/Category/CategoryList',
        type: 'GET',
        dataType: 'json',
        contentType: 'application/json;charset=utf-8;',
        success: function (result) {
            var object = '';
            $.each(result, function (index, item) {
                object += '<tr>';
                object += '<td>' + item.categoryId + '</td>';
                object += '<td>' + item.name + '</td>';
                object += '<td>' + item.date + '</td>';
                object += '<td><img style="width: 50px; hieght: 50px;" src="' + item.profileImage + '" /></td>';
                object += '<td><a href="#" class="btn btn-primary "onclick="Edit(' + item.categoryId + '); ">Edit</a> ||<a href="#" class="btn btn-danger" onclick="Delete(' + item.categoryId + ');">Delete</td>';
                object += '</tr>';

            });
            $('#table_data').html(object);

        },
        error: function () {
            alert('Data Cant Get');

        },
    });

}
$('#addbtncategory').click(function () {
    $('#CategoryModal').modal('show');
    Cleartextbox();


});
function AddCategory() {
    if (validationform()) {

        var formdata = new FormData();

        formdata.append('Name', $('#name').val());
        formdata.append('Date', $('#date').val());
        formdata.append('ProfileImage', document.getElementById("fileUpload").files[0]);

        //formdata.append('InvoiceLogo', $('#invoiceLogo').attr("src"));
        //formdata.append('UploadTopBannerPic', $('#invoiceTopBanner').attr("src"));
        //var objdata = {
        //    Name: $('#name').val(),
        //    Date: $('#date').val()
        //}

        $.ajax({
            url: '/Category/AddCategory',
            type: 'POST',
            data: formdata,
            processData: false,  // tell jQuery not to process the data
            contentType: false,
            success: function () {
                alert('Data is Saved');
                ShowCatData();
                HideModalPopup();
                Cleartextbox();
                $('#addcategory').show();
                $('#btnupdate').hide();

            },
            error: function () {
                alert('Data Cant Saved');

            }
        });

    }
    else {
        alert('Please All textbox fill');
    }
}
function HideModalPopup() {
    $('#CategoryModal').modal('hide');

}
function Cleartextbox() {
    $('#name').val('');
    $('#date').val('');
}

function Delete(id) {
 debugger
    if (confirm("Are you sure you want to delete this?")) {
        $.ajax({
            url: '/Category/Delete?id=' + id,
            type: 'GET',
            dataType: "json",
            success: function (data) {
                ShowCatData();

                alert('Record deleted ');
              

            },
            error: function () {
                alert('Record cant be deleted');

            }
        });
    }
    else {
        return false;
    }


}
function Edit(id) {

    $.ajax({
        url: '/Category/Edit?id=' + id,
        type: 'GET',

        contentType: 'application/json;charset=utf-8;',
        dataType: 'json',
        success: function (response) {

            $('#CatID').val(response.categoryId);
            $('#name').val(response.name);
            $('#imgpt').attr('src', response.profileImage);
            const date = new Date(response.date);
           
            document.getElementById('date').valueAsDate = date;

            $('#CategoryModal').modal('show');
            $('#addcategory').hide();
            $('#btnupdate').show();


        },

        error: function () {
            alert('Data not found');

        },
    });

}
function UpdateCategory() {
    var formdata = new FormData();

    formdata.append('categoryId', $('#CatID').val());
    formdata.append('Name', $('#name').val());
    formdata.append('Date', $('#date').val());
    formdata.append('ProfileImage', document.getElementById("fileUpload").files[0]);
    //var objdata = {
    //    categoryId: $('#CatID').val(),
    //    Name: $('#name').val(),
    //    Date: $('#date').val(),
    //    formdata.append('ProfileImage', document.getElementById("fileUpload").files[0]);

    //}
    $.ajax({
        url: '/Category/UpdateCategory',
        type: 'Post',
        data: formdata,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function () {
            ShowCatData();
            HideModalPopup();
            clearTextBox();
            alert('Data Update');


        },
        error: function () {
            alert('Data Cant Update');

        },

    });
}
function clearTextBox() {

    $('#name').val("");
    $('#date').val("");
    $('#imgpt').attr('src', '~/depositphotos_247872612-stock-illustration-no-image-available-icon-vector.jpg');
}


function readURL(input) {

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
$('#txtsearch').keyup(function () {
    var typeValue = $(this).val();
    $('tbody tr').each(function () {
        if ($(this).text().search(new RegExp(typeValue, "i")) < 0) {
            $(this).fadeOut();

        }
        else {
            $(this).show()
        }
    })

});
function validationform() {
    var isValid = true;

    let name = $('#name').val();
    let date = $('#date').val();

    if (name.trim() == "") {
        $('#name').css("border", "2px solid red");;
        isValid = false;
    }
    else {
        $('#name').css({ border: "" });
    }

    if (name.trim() == "") {
        $('#date').css("border", "2px solid red");;
        isValid = false;
    }
    else {
        $('#date').css({ border: "" });
    }
    return isValid;

}
