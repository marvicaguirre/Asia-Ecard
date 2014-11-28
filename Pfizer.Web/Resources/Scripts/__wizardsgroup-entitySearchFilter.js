
function SearchAccountName(url) {
    var filterText = $("#AccountNameFilter").val();
    SearchAccount(url, filterText);
}
function ClearGridAndText(url) {
    var filterText = "";
    $("#AccountNameFilter").val("");
    SearchAccount(url, filterText);
}

function SearchAccount(url, filterText) {
    var request = $.ajax({
        type: "POST",
        url: url,
        traditional: true,
        async: false,
        data: { filterText: filterText }
    });

    request.done(function () {
        deleteChildGrids();
        var accountFranchiseGrid = $('#AccountFranchiseGrid').data('kendoGrid');
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