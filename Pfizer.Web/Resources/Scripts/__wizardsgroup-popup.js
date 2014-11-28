/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-module-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-general-controls.js" />

var gDialogEntry = "";
var _g_popup_DialogEntryNameSelector = "";

$(function () {    
    _g_popup_DialogEntryNameSelector = "#divEntry";
    gDialogEntry = $(_g_popup_DialogEntryNameSelector);
});

function getDialogButton(buttonName) {
    return $(".ui-dialog-buttonpane button:contains('" + buttonName + "')");
}

function _popup_getModalCancelButton() {
    return getDialogButton('Cancel');
}

function _popup_getModalSaveButton() {
    return getDialogButton('Save');
}

function _popup_getModalModifyButton() {
    return getDialogButton('Save');
}

function getModalCreateButton() {
    return getDialogButton('Save');
}

/*******************JQUERY MODAL POPUP****************/
function _popup_launchJqueryEntry(url, actionName, title, width, height, paramdata, evt, gridname, autoClose, confirm) {
    _director_setGlobalVariable(paramdata, gridname, url);
    //gDialogEntry.attr("targetgrid", gridname);
    if (typeof (evt) != 'undefined') {
        evt.preventDefault();
    }
    //var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');

    var dialog = gDialogEntry;
    dialog.attr("targetgrid", gridname);

    dialog.empty();

    dialog.attr("autoClose", autoClose);

    //$(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
    //1. call the page using ajax
    var request = $.ajax({
        type: "GET",
        url: url,
        data: paramdata
    });

    //2a. assign event handler for successful call
    request.done(function (data) {
        //4. called if successful operation
        createButtons(dialog, actionName, confirm);

        //4a. check if data contains the hidden tag for making the dialog readonly
        debugger;
        var dataForViewingOnly = $(data);
        // just incase we have more than 1 input#DynamicReadOnlyView
        dataForViewingOnly.filter("input[id=DynamicReadOnlyView]").each(function () {
            var val = $(this).val().toLowerCase();
            if (!(val == undefined || val == '' || val.length <= 0 || val == null) && val == "true") {
                // marked the following elements as "disabled"
                dataForViewingOnly.find("input[type=text], input[type=checkbox], textarea, select").each(function () {
                    if ($(this).is("textarea"))
                        $(this).prop("readonly", true);
                    else
                        $(this).attr("disabled", "disabled");
                });
                // remove buttons, if any
                dataForViewingOnly.find("a.btn").remove();
                // replace buttons with "Close" only
                createButtons(dialog, "View", confirm);
                //Change title of dialog box
                debugger;
                // we have found one true, exit each
                return false;
            }
        });
        // return resulting data
        data = dataForViewingOnly;

        dialog.html(data);
        $("#divEntryData").data("ORIGINAL_DATA_KEY", { content: dialog.html() });
        _layout_initControls();
        _initKendoControls(dialog);
    });

    //2b. assign event handler for failed call
    request.fail(function (jqXHR, textStatus) {
        //5b. called if failed operation
        //var trimmedMsg = jqXHR.responseText.substring(0, 500).replace("<html>", "").replace("</html>", "");
        //alert("Request failed: " + textStatus + "; " + trimmedMsg);
        gDialogEntry.html(jqXHR.responseText);
    });

    //3. initialize the popup window and open it; see 4 above (they should be called afterwards;
    _popup_openDialog(dialog, title, width, height);
}
function _attachConfirmOnCloseButtonDialog(dialog) {
    var closeButton = dialog.closest('.ui-dialog').find('.ui-dialog-titlebar-close');
    closeButton.unbind('click').bind('click', function (e) {
        e.preventDefault();
        closeModalDialogConfirm(dialog);
    });
}

function _removeConfirmOnCloseButtonDialog(dialog) {
    var closeButton = dialog.closest('.ui-dialog').find('.ui-dialog-titlebar-close');
    closeButton.unbind('click').bind('click', function (e) {
        _create_attachCreateButtonEventHandler($("#divMaster"));
        dialog.dialog('close');
    });
}

function confirmDialogDecider(confirm) {
    if (confirm == undefined || confirm == '') {
        confirm = 'yes';
    }

    (confirm == 'yes') ? closeModalDialogWithConfirmation() : closeModalDialog();
}

//Create buttons inside the popup window
function createButtons(dialog, actionName, confirm) {
    if (confirm == undefined || confirm == '') {
        //No default buttons
    }

    //alert(actionName);

    //For views that have their own buttons, reset buttonArray = {};
    var buttonArray = {};
    if (actionName == "CreateAccount") {
        var autoClose = gDialogEntry.attr("autoClose");
        if (autoClose === "False") {
            buttonArray.Save = function () { $('form[name="CreateAccountForm"]').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
            dialog.dialog({ buttons: buttonArray });

            //replace Create text with Create and Continue;
            var createAndContinue = $("span.ui-button-text");
            createAndContinue.text("Create and Continue");
        }
        else {
            buttonArray.Create = function () { $('form[name="CreateAccountForm"]').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider('yes'); };
            dialog.dialog({ buttons: buttonArray });
        }
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);

    } else if (actionName == "Wizard") {
        buttonArray.Back = function () { submitWizardForm('Back'); };
        buttonArray.Continue = function () { submitWizardForm('Continue'); };
        buttonArray.Save = function () { submitWizardForm('Save'); };
        buttonArray.Cancel = function () { confirmDialogDecider('yes'); };
        buttonArray.AddDependent = function () { submitWizardForm('AddDependent'); };
        buttonArray.Finish = function () { submitWizardForm('Finish'); };
        dialog.dialog({ buttons: buttonArray });


    } else if (actionName == "ResetPasswordForm" || actionName == "ResetPassword") {
        buttonArray.Create = function () { $('form[name="ResetPasswordForm"]').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider('yes'); };
        dialog.dialog({ buttons: buttonArray });
        var createAndContinue = $("span.ui-button-text");
        createAndContinue.text("Create and Continue");

    } else if (actionName == "ForgotPassword") {

        var autoClose = gDialogEntry.attr("autoClose");
        if (autoClose === "False") {
            buttonArray.Save = function () { $('form[name="ForgotPasswordForm"]').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
            dialog.dialog({ buttons: buttonArray });

            //replace Create text with Create and Continue;
            var createAndContinue = $("span.ui-button-text");
            createAndContinue.text("Create and Continue");
        }
        else {
            buttonArray.Create = function () { $('form[name="ForgotPasswordForm"]').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider('yes'); };
            dialog.dialog({ buttons: buttonArray });
        }

        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);

    } else if (actionName === "Create") {
        var autoClose = gDialogEntry.attr("autoClose");
        if (autoClose === "False") {
            buttonArray.Save = function () { $('form').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
            dialog.dialog({ buttons: buttonArray });

            //replace Create text with Create and Continue;
            var createAndContinue = $("span.ui-button-text");
            createAndContinue.text("Create and Continue");
        }
        else {
            buttonArray.Create = function () { $('form').submit(); };
            buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
            dialog.dialog({ buttons: buttonArray });
        }

        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "Edit") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({
            buttons: buttonArray
			, open: function (event) {
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Delete")'), 'ui-icon-trash');
			    setButtonStyle($(this).parent().find(' button:contains("Cancel")'), 'ui-icon-cancel');
			}
        });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "Upload" ||
        actionName === "EditUpload" ||
        actionName === "Contest") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Close = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        $(".ui-dialog-titlebar-close").hide();
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "View") {
        buttonArray.Close = function () { dialog.dialog('close'); };
        dialog.dialog({ buttons: buttonArray });
        //$(".ui-dialog-titlebar-close").hide();
    } else if (actionName === "Rearrange") {
        //TODO check document for rearrange with confirm
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "Approval") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "AddExisting") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { closeModalDialog(); };
        dialog.dialog({ buttons: buttonArray });
    } else if (actionName === "AssignAPEHospital" ||
        actionName === "AssignProposalAPEHospital") {
        buttonArray.Save = function () {
            $('form').submit();
            RefreshHospitalsDualListBox();
        };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "Assign") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "AssignExclusion") {
        buttonArray.Save = function () {
            $('#SelectedExclusionId option').attr('selected', 'selected');
            $('form').submit();
            var targetGrid = gDialogEntry.attr("targetgrid");
            _grid_refreshChildWindowGrid(targetGrid);
            dialog.dialog('close');
        };
        buttonArray.Cancel = function () { closeModalDialogConfirm(dialog); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "Copy") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "WizardCreate" ||
        actionName === "WizardEdit" ||
        actionName === "WizEdit") {
        dialog.dialog({ buttons: {} });
    } else if (actionName === "AccountTransfer") {
        buttonArray.Save = function () { $('form').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });
        confirm == 'yes' && _attachConfirmOnCloseButtonDialog(dialog);
    } else if (actionName === "UploadEmployee") {
        buttonArray.Save = function () { $('form[name="UploadEmployee"]').submit(); };
        buttonArray.Cancel = function () { confirmDialogDecider(confirm); };
        dialog.dialog({ buttons: buttonArray });

        //replace Create text with Create and Continue;
        var createAndContinue = $("span.ui-button-text");
        createAndContinue.text("Submit");
    }

    //assign the style of all buttons after creating them!
    //CSS: all elements with ui-button class which are children of ui-dialog class
    //setButtonStyle(".ui-dialog .ui-button", null, 'popup-createButtons');
    setButtonStyle(".ui-dialog-buttonset button");
}

function _popup_enableDisableControls() {
    //var actionName = getEditActionName();
    //if (actionName === "EDIT") {
    //{
    //	enableDisableControl(editorFieldElementSelector);
    //	hideDateTimePicker();
    //}
}

function _popup_initDialogProperties(dialog, title, width, height) {
    if (width == 0 && height == 0) {
        width = $(window).width() * 0.6;
        height = $(window).height() * 0.8;
    }

    dialog.dialog({
        autoOpen: false,
        draggable: true,
        title: title,
        width: width,
        modal: true,
        height: height,
        minHeight: 200,
        show: 'drop',
        hide: { effect: 'drop', direction: 'right' },
        //close: $(this).dialog("destroy").remove(),
        position: 'center',
        open: function (event) {
            $('.ui-widget-overlay').css("height", "150%");
        }
    });
}

function launchJqueryModalDynamicGrid(url, title, width, height, data, evt, gridname) {
    $(_g_popup_DialogEntryNameSelector).dialog("destroy");
    _director_setGlobalVariable(data, gridname, url);

    evt.preventDefault();

    var request = $.ajax({
        type: "GET",
        url: url,
        traditional: true,
        data: { id: data }
    });

    request.done(function (callbackdata) {
        //Dynamically create html table on Json data
        var htmlTable = createTableView(callbackdata, true);

        var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');
        var dialog = $(_g_popup_DialogEntryNameSelector);

        var buttonArray = {};
        buttonArray.Close = function () { dialog.dialog('close'); };

        dialog.empty();
        dialog.append(loading).html(htmlTable);

        _popup_openDialog(dialog, title, width, height, null, buttonArray);
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}

//function disableControls() {
//    alert('_medicard-layout; disableControls()');
//    $(editorFieldElementSelector).not(':submit,:button').each(function () {
//        $(this).attr('disabled', true);
//    });
//}

//Open the dialog with the optional buttons and data
function _popup_openDialog(dialog, title, width, height, data, buttons) {
    _popup_initDialogProperties(dialog, title, width, height);

    if (buttons) {
        dialog.dialog({ buttons: buttons });
    }
    dialog.bind('close', function (event) {
        //this will do nothing if _g_grid_GridNameMultiple in grid.js has no value
        var grids = getGridNameMultiple();
        if (grids) {
            _grid_refreshChildWindowGridMultiple();
        }
    });
    dialog.dialog('open');

    if (data) {
        dialog.html(data);
    }
}

function launchJqueryModalConfirm_Old(buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
    _director_setGlobalVariable(data, gridname, url);

    evt.preventDefault();

    var myArray = [];
    data.each(function (index) {
        myArray[index] = $(this).val();
    });

    var request = $.ajax({
        type: "GET",
        url: url,
        traditional: true,
        data: { checkedRecords: myArray }
    });

    request.done(function (callbackdata) {
        //Dynamically create HTML table on JSON data
        var htmlTable = createTableView(callbackdata, true);

        var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');
        var dialog = $(_g_popup_DialogEntryNameSelector);

        var buttonArray = {};
        buttonArray[buttonText] = function () { hrPostAction(targetUrl); };
        buttonArray.Cancel = function () { dialog.dialog('close'); };

        dialog.empty();
        dialog.append(loading).html(htmlTable);

        _popup_openDialog(dialog, title, width, height, null, buttonArray);
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}
function saveModalDialogWithConfirmation(message) {
    var dialog = $(_g_popup_DialogEntryNameSelector);
    SaveModalDialogConfirmation(dialog, message);
}
function SaveModalDialogConfirmation(dialog, message) {
    var confirm = $('<div id="dialog-confirm" title="Confirm Message"><p>' + message + '</p></div>');
    $(".validation-summary-errors").hide();
    confirm.dialog({
        autoOpen: true,
        height: 300,
        width: 400,
        modal: true,
        resizable: false,
        hide: { effect: 'drop', direction: 'right' },
        buttons: [
            { text: "Yes", click: function () { $(this).dialog('close'); $("#ConfirmedFlag").val('1'); $('form').submit(); }, class: "btn btn-info", style: "display: inline-block;" },
            { text: "No", click: function () { $(this).dialog('close'); }, class: "btn btn-info", style: "display: inline-block;" }
        ],
        open: function (event) {
            $('.ui-widget-overlay').css("height", "150%");
        }
    });
}

function closeModalDialog() {
    var dialog = $(_g_popup_DialogEntryNameSelector);
    dialog.dialog('close');
}

function closeModalDialogWithConfirmation() {
    var dialog = $(_g_popup_DialogEntryNameSelector);
    closeModalDialogConfirm(dialog);

    $('.ui-dialog-buttonpane').show();
}

function closeModalDialogConfirm(dialog) {
    var confirm = $('<div id="dialog-confirm" title="Confirm Message"><p>Are you sure you want to exit without saving?</p></div>');
    confirm.dialog({
        autoOpen: true,
        height: 200,
        width: 400,
        modal: true,
        resizable: false,
        hide: { effect: 'drop', direction: 'right' },
        buttons: [
            { text: "Yes", click: function () { $(this).dialog('close'); dialog.dialog('close'); }, class: "btn btn-info", style: "display: inline-block;" },
            { text: "No", click: function () { $(this).dialog('close'); return false; }, class: "btn btn-info", style: "display: inline-block;" }
        ],
        open: function (event) {
            $('.ui-widget-overlay').css("height", "150%");
        }
    });
}

function resetModalDialogConfirm(btnReset, action) {
    var confirm = $('<div id="dialog-confirm" title="Confirm Message"><p>Are you sure you want to reset?</p></div>');
    confirm.dialog({
        autoOpen: true,
        height: 200,
        width: 400,
        modal: true,
        resizable: false,
        hide: { effect: 'drop', direction: 'right' },
        buttons: [
            { text: "Yes", click: function () { $(this).dialog('close'); populatePartialView(action); }, class: "btn btn-info", style: "display: inline-block;" },
            { text: "No", click: function () { $(this).dialog('close'); return false; }, class: "btn btn-info", style: "display: inline-block;" }
        ],
        open: function (event) {
            $('.ui-widget-overlay').css("height", "150%");
        }
    });
}



function populatePartialView(action) {
    $.ajax({
        type: "POST",
        url: action,
        data: $('form').serialize(),
        success: function (partialView) {
            $("#divEntry").html(partialView);
        }
    });
}

function refreshModalDialogConfirmOnCreate(btnReset, action, guidId) {
    $.ajax({
        type: "POST",
        url: action,
        data: $('form').serialize(),
        success: function (partialView) {
            $("#divEntry").html(partialView);
        }
    });
}
/*******************JQUERY MODAL POPUP****************/


/*********************DATA ENTRY************************/

var confirmOnSubmitOverrideFxMessage = function (message, noCallBack, yesCallBack) {
    if (message == '' || message == undefined) return true;
    var confirm = $('<div id="dialog-confirm" title="Confirm Message"><p>' + message + '<br> Are you sure you want to continue?</p></div>');
    confirm.dialog({
        autoOpen: true,
        height: 300,
        width: 400,
        modal: true,
        resizable: false,
        hide: { effect: 'drop', direction: 'right' },
        buttons: [
            {
                text: "Yes", click: function () {
                    $(this).dialog('close');
                    var confirmElement = $('#IsConfirm');
                    if (confirmElement)
                        $('#IsConfirm').val(true);
                    confirmOnSubmitOverrideFx = function () { return true; }; $('form').submit();
                    if (yesCallBack)
                        yesCallBack();
                }, class: "btn btn-info", style: "display: inline-block;"
            },
            {
                text: "No", click: function () {
                    if (noCallBack)
                        noCallBack();
                    $(this).dialog('close');
                }, class: "btn btn-info", style: "display: inline-block;"
            }
        ],
        open: function (event) {
            $('.ui-widget-overlay').css("height", "150%");
        }
    });
    return false;
};

var confirmOnSubmitOverrideFx = function () { return true; };

function onFormSubmit(e) {
    if ($(this).attr('name') == 'LoginForm') {
        return true;
    }
    e.preventDefault();
    if (!confirmOnSubmitOverrideFx()) {
        return false;
    }
    var request = $.ajax({
        type: "POST",
        url: $(this).attr("action"),
        datatype: "json",
        data: $(this).serialize()
    });

    var targetContainerDiv = $(_g_popup_DialogEntryNameSelector);

    //finished submission of form:
    request.done(function (data) {
        var targetGrid = gDialogEntry.attr("targetgrid");
        var grids = getGridNameMultiple();

        if (data.ActionStatus === "Success") {
            //_common_getWindowParent().
            _common_resetValidations();
            _grid_refreshChildWindowGrid(targetGrid, _grid_refreshCallback);

            if (grids) {
                _grid_refreshChildWindowGridMultiple();
            }
            _director_showSuccess(data.Messages);
            gDialogEntry.dialog("close");
        }
        else if (data.ActionStatus === "Warning") {
            //_common_getWindowParent().
            _common_resetValidations();
            _grid_refreshChildWindowGrid(targetGrid);

            if (grids) {
                _grid_refreshChildWindowGridMultiple();
            }
            _director_showWarning(data.Messages);
            gDialogEntry.dialog("close");
        }
        else if (data.ActionStatus === "Save") {

            if (targetGrid != null && targetGrid != '') {
                _grid_refreshChildWindowGrid(targetGrid);

                if (grids) {
                    _grid_refreshChildWindowGridMultiple();
                }
            }
            _common_resetValidations();

            _director_showSuccess(data.Messages);
            _layout_initLabelTextControls();

            if (data.hasOwnProperty("Id"))
                $('.pKey').val(data.Id);
            if (data.hasOwnProperty("MemberId"))
                $('#MemberId').val(data.MemberId);

            $('.validation-summary-valid').html('');
        }
        else if (data.ActionStatus === "Finish") {
            _director_showSuccess(data.Messages);
            gDialogEntry.dialog("close");
        }
        else {
            //failed to ADD or UPDATE record;
            //comment today
            //restoreContent(targetContainerDiv);
            targetContainerDiv.html(data);
            //comment today
            //_director_showError('Error!');
            _layout_initLabelTextControls();
            attachControlsEvent();
        }
    });

    request.fail(function (jqXHR, textStatus) {
        //_director_showError(textStatus);
        targetContainerDiv.html(jqXHR.responseText);
        _layout_initLabelTextControls();
        attachControlsEvent();
    });

    return false;
}

function restoreContent(targetContainerDiv) {
    var data = $("#divEntryData").data("ORIGINAL_DATA_KEY");
    if (data) {
        var content = data.content;
        targetContainerDiv.html(content);

        //var htmlData = $("#divEntryData").data("ORIGINAL_DATA_KEY").content;
        //targetContainerDiv.html(htmlData);
    }
}

function modifyEntry() {
    _popup_enableAllControls();
    _popup_getModalCancelButton().show();
    _popup_getModalSaveButton().show();
    _popup_getModalModifyButton().hide();
    //_popup_getModalModifyButton().bind('click', customModifyEvent);
}

function cancelEntry() {
    //var data = jQuery.data($('#divEntryData')[0], "ORIGINAL_DATA_KEY");
    var data = $("#divEntryData").data("ORIGINAL_DATA_KEY");
    if (data) {
        var content = data.content;
        $(_g_popup_DialogEntryNameSelector).html(content);
    }

    _layout_initControls();
    _layout_initLabelTextControls();
    _popup_getModalModifyButton().show();
}

//var editorFieldElementSelector = '.editor-field input,select,textarea,img';
//var editorFieldElementSelector = '.controls .k-datetimepicker span.k-picker-wrap span.k-select span.k-i-calendar';
//var editorFieldElementSelector = '.controls .k-datetimepicker span.k-picker-wrap';

//==============================================================
//Note: there should be a DIV whose class is called 'controls' (see Edit.cshtml of Client); this is normally a class from the Bootstrap CSS Framework
//==============================================================
//var editorFieldElementSelector = '.controls input,select,textarea,img';
var editorFieldElementSelector = '.control-group input,select,textarea,img';

function _popup_enableAllControls() {
    //alert('hrwizard-popup; _popup_enableAllControls()');
    //$(editorFieldElementSelector).not(':submit,:button').each(function () {
    //    $(this).attr('disabled', false);
    //});
    _general_controls_enableControlsExcludingButtons(editorFieldElementSelector);

    _datePicker_attachDatePickerEvent();
    _datePicker_showDateTimePicker();
}