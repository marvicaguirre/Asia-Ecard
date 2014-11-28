/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />

function _select_attachSelectModalButtonEventHandler(divPanel) {
    divPanel.find(".buttonSelectModalClass").each(function () {
        var button = $(this);

        var className = button.attr('class');
        if (className) {
            button.attr('class', className.replace('buttonSelectModalClass ', ''));
        }

        button.click(function (evt) {
            launchApprovalModal($(this), evt);
        });
    });
    setButtonStyle(divPanel.find(".buttonSelectModalClass"));
}

function launchApprovalModal(element, evt) {
    var url = element.attr("url");
    var modalTitle = element.attr("modaltitle");
    var modalwidth = element.attr("modalwidth");
    var modalheight = element.attr("modalheight");
    var gridname = element.attr("gridname");
    var confirm = element.attr("withConfirm");
    var selectionMode = element.attr('selectionMode');
    var validationAction = element.attr('validationAction');
    var controller = element.attr('controller');
    var buttonText = element.text();

    var selectedRecords = _grid_getGridSelectedRecords(gridname);
    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
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
    _common_getWindowParent().launchJquerySelectModalApproval(buttonText, url, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname, confirm, selectionMode);
}

function launchJquerySelectModalApproval(buttonText, url, title, width, height, data, evt, gridname, confirm, selectionMode) {
    evt.preventDefault();
    var primaryKeys = generatePrimaryKeys(data);

    var request;
    if (selectionMode == 'Single') {
        request = $.ajax({
            type: "GET",
            url: url,
            traditional: true,
            data: { checkedRecord: primaryKeys }
        });
    } else {
        request = $.ajax({
            type: "GET",
            url: url,
            traditional: true,
            data: { checkedRecords: primaryKeys }
        });
    }
    

    var dialog = gDialogEntry;
    dialog.attr("targetgrid", gridname);

    //2a. assign event handler for successful call
    request.done(function (callBackData) {
        createSelectModalButtons(dialog, confirm);
        dialog.html(callBackData);
        $('form').submit(onFormSubmitOverride);
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

function createSelectModalButtons(dialog, confirm) {    
    var buttonArray = {};
    buttonArray.Submit = function () { $('form').submit(); };
    buttonArray.Cancel = function () {
        if (confirm == 'yes') 
            closeModalDialogConfirm(dialog);
        else
            closeModalDialog();
    };
    dialog.dialog({ buttons: buttonArray });    
    confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);    
    setButtonStyle(".ui-dialog-buttonset button");
}

function onFormSubmitOverride(e) {
    e.preventDefault();    
    var request = $.ajax({
        type: "POST",
        url: $(this).attr("action"),
        datatype: "json",
        data: $(this).serialize()
    });
    var grids, targetGrid;
    var targetContainerDiv = $(_g_popup_DialogEntryNameSelector);
    request.done(function (data) {
        if (data.ActionStatus === "Success") {
            targetGrid = gDialogEntry.attr("targetgrid");
            _common_resetValidations();
            _grid_refreshChildWindowGrid(targetGrid);
            grids = getGridNameMultiple();
            grids && _grid_refreshChildWindowGridMultiple();
            overrideCustomModalAfterSubmit();
            _director_showSuccess(data.Messages);
            gDialogEntry.dialog("close");
        }
        else if (data.ActionStatus === "Warning") {
            targetGrid = gDialogEntry.attr("targetgrid");
            _common_resetValidations();
            _grid_refreshChildWindowGrid(targetGrid);
            grids = getGridNameMultiple();
            grids && _grid_refreshChildWindowGridMultiple();
            _director_showWarning(data.Messages);
            gDialogEntry.dialog("close");
        }
        else {
            targetContainerDiv.html(data);
            _layout_initLabelTextControls();
            attachControlsEvent();
        }
    });
    
    request.fail(function (jqXHR, textStatus) {
        targetContainerDiv.html(jqXHR.responseText);
        _layout_initLabelTextControls();
        attachControlsEvent();
    });

    return false;
}

function overrideCustomModalAfterSubmit() {
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