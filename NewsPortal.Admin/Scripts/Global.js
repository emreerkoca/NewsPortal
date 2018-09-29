function AddCategory()
{
    category = new Object();
    category.CategoryName = $("#categoryName").val();
    category.Url = $("#categoryUrl").val();
    category.IsActive = $("#isActiveCategory").is(":checked");
    category.ParentID = $("#ParentID").val();

    $.ajax({
        url: "Insert",
        data: category,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function() {
                    //TODO
                });
            }
        }
    })

}

function DeleteCategory() {
    var deleteID = $("#btnDeleteCategory").attr("data-id");

    $.ajax({
        url: "Delete/" + deleteID,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //TODO
                });
            }
        }
    });
}

function UpdateCategory() {
    category = new Object();
    category.CategoryName = $("#categoryName").val();
    category.Url = $("#categoryUrl").val();
    category.IsActive = $("#isActiveCategory").is(":checked");
    category.ParentID = $("#ParentID").val();
    category.ID = $("#ID").val();

    $.ajax({
        url: "Update",
        data: category,
        type: "POST",
        dataType: "json",
        success: function (response) {
            if (response.Success) {
                bootbox.alert(response.Message, function () {
                    location.reload();
                });
            }
            else {
                bootbox.alert(response.Message, function () {
                    //TODO
                });
            }
        }
    })
}