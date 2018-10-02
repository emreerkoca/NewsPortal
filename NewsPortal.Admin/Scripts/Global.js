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

//DeleteCategory
$(document).on("click", "#btnDeleteCategory", function () {
    var deleteId = $(this).attr("data-id");
    var deletedRow = $(this).closest("tr");
    $.ajax({
        url: "Category/Delete/" + deleteId,
        type: "POST",
        dataType: "json",
        success: function (response) {
            deletedRow.fadeOut(300, function () {
                deletedRow.remove();
            })
            $.notify(response.Success, "success");
        }
    })
})

