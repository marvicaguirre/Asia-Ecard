//===========================================================
//Notes:
//  - known client of this js file:
//          - _Mover.cshtml
//  - the 2 variables below will be replaced by the corresponding CSHTML file that uses the _Mover.cshtml:
//          -> CompanySubmodule/CompanyModuleAssignment.cshtml
//          -> RoleFunction/RoleFunctionAssignment.cshtml
//          -> UserFunction/OfUser.cshtml
//

var assignUrl;

function setAssignButton(id) {
    var dialog = $("#divEntry");
    var buttonArray = {};
    buttonArray["Assign"] = function () {
        var data = getData(id);
        postAssignment(data);
    };
    dialog.dialog({ buttons: buttonArray });
}

function getData(id) {    
    var selectednumbers = new Array();
    $('#AssignedRecords option').each(function (i, selected) {
        var value = $(selected).val();        
        selectednumbers[i] = value;
    });

    return { parentId: id, ids: selectednumbers };
}

function postAssignment(postdata) {

    var posUrl = _director_fixUrl(assignUrl);
    var request = $.ajax({
        url: posUrl,
        data: postdata,
        type: 'POST',
        traditional: true
    });
    var targetContainerDiv = $("#divEntry");
    request.done(function (data) {
        if (data == "Success") {
            var targetGrid = gDialogEntry.attr("targetgrid");
            targetContainerDiv.dialog("close");            
            _grid_refreshChildWindowGrid(targetGrid);
            _director_showSuccess('Success!');
        }
        else {
            _director_showError('Error!');
        }
    });
    request.fail(function (jqXHR, textStatus) {
        //topMostWindow().showJBarMessage('error', textStatus);
        targetContainerDiv.html(jqXHR.responseText);
    });
}



