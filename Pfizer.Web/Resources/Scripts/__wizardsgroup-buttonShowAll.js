function ShowAll(url) {
    var request = $.ajax({
        type: "POST",
        url: url,
        traditional: true,
        async: false
    });

    request.done(function () {
        deleteChildGrids();
        var accountFranchiseGrid = $('#MemberRequestGrid').data('kendoGrid');
        accountFranchiseGrid.dataSource.read();
        accountFranchiseGrid.dataSource.page(1);
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Request failed while setting entity search filter: " + textStatus);
    });
}

function deleteChildGrids() {
    $("#divMaster").find("#divDetails1").remove();
}