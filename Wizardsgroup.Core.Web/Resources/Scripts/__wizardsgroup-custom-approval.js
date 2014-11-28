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
    var buttonText = element.text();

    var selectedRecords = _grid_getGridSelectedRecords(gridname);
    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }

    _common_getWindowParent().launchJqueryModalApproval(buttonText, url, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function launchJqueryModalApproval(buttonText, url, title, width, height, data, evt, gridname) {
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
        createApprovalButtons(dialog);
        dialog.html(callBackData);
        _layout_initHrControls();
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

function createApprovalButtons(dialog) {
    var buttonArray = {};
    buttonArray.Submit = function () { $('form').submit(); };
    buttonArray.Cancel = function () { closeModalDialog(); };
    dialog.dialog({ buttons: buttonArray });
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