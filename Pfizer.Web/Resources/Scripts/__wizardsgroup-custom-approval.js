/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />

function _confirm_attachApprovalButtonEventHandler(divPanel) {
    divPanel.find(".buttonApprovalClass").each(function () {
        var approvalButton = $(this);

        var className = approvalButton.attr('class');
        if (className) {
            approvalButton.attr('class', className.replace('buttonApprovalClass ', ''));
        }

        approvalButton.click(function (evt) {
            launchApprovalModal($(this), evt);
        });
    });
    setButtonStyle(divPanel.find(".buttonApprovalClass"));
}

function launchApprovalModal(element, evt) {
    var url = element.attr("url");
    var modalTitle = element.attr("modaltitle");
    var modalwidth = element.attr("modalwidth");
    var modalheight = element.attr("modalheight");
    var gridname = element.attr("gridname");
    var confirm = element.attr("withConfirm");
    var singleselect = element.attr("singleselect");
    var selectionMode = element.attr('selectionMode');
    var validationAction = element.attr('validationAction');
    var controller = element.attr('controller');
    var buttonText = element.text();

    var selectedRecords = _grid_getGridSelectedRecords(gridname);
    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }
    //TODO remove this when buttons are cleanedup. this code is deprecated.
    if (singleselect == 'yes' && selectedRecords.length > 1) {
        _director_showWarning('Only one record should be selected');
        return;
    }
    if (selectionMode == 'Single' && selectedRecords.length > 1) {
        _director_showWarning('Only one record should be selected');
        return;
    }
    if (validationAction && validationAction != '') {
        var result = !_customButtonValidationAction(selectedRecords, controller, validationAction, selectionMode);
        if (result) return;
    }
    _common_getWindowParent().launchJqueryModalApproval(buttonText, url, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname, confirm);
}

function launchJqueryModalApproval(buttonText, url, title, width, height, data, evt, gridname,confirm) {
    evt.preventDefault();
    var primaryKeys = generatePrimaryKeys(data);

    var request = $.ajax({
        type: "GET",
        url: url,
        traditional: true,
        data: { checkedRecords: primaryKeys }
    });

    var dialog = gDialogEntry;
    dialog.attr("targetgrid", gridname);

    //2a. assign event handler for successful call
    request.done(function (callBackData) {
        createApprovalButtons(dialog, confirm);
        dialog.html(callBackData);
        _layout_initControls();
        _initKendoControls(dialog);        
    });

    //2b. assign event handler for failed call
    request.fail(function (jqXHR, textStatus) {
        //5b. called if failed operation
        gDialogEntry.html(jqXHR.responseText);
    });

    //3. initialize the popup window and open it; see 4 above (they should be called afterwards;
    _popup_openDialog(dialog, title, width, height);
}

function createApprovalButtons(dialog,confirm) {
    var buttonArray = {};
    buttonArray.Submit = function () { $('form').submit(); };
    buttonArray.Cancel = function () {
        if (confirm == 'yes') 
            closeModalDialogConfirm(dialog);
        else
            closeModalDialog();
    };
    dialog.dialog({ buttons: buttonArray });
    
    if (confirm == 'yes') {
        _attachConfirmOnCloseButtonDialog(dialog);
    } 
    
    setButtonStyle(".ui-dialog-buttonset button");
}


function generatePrimaryKeys(source) {
    var myArray = extractArrayOfAttributeValues(source, 'primaryid');
    return myArray;
}

function extractArrayOfAttributeValues(source, attributeName) {
    var myArray = [];
    source.each(function (index) {
        myArray[index] = $(this).attr(attributeName);
    });

    return myArray;
}