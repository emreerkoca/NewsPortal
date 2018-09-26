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
        dataType: 'json',
        success: function (response) {
            debugger;
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