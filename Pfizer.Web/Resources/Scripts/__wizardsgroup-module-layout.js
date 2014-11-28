/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-confirm-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-custom-approval.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-buttonAction.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-delete-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-create-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-edit-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-customaction-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-toggleStatus.js" />

var gGridName_ModuleLayout = "";

$.ajaxSetup({ cache: false });
//$.ajaxSetup({ cache: true });

var gGlobalNotification = "";
var gFormNotification = "";
var gGridNotification = "";
var gControlsNotification = "";

var GlobalStatus = {
    FormSuccess: 1,
    FormFailed: 2,
    CompletedInitialiedControl: 3,
    CompletedInitializedGrid: 4,
    GetGridColumns: 5
};

function initializedNotification() {
    gGlobalNotification = $("#txtGlobalNotification");
    gFormNotification = $("#txtFormNotification");
    gGridNotification = $("#txtGridNotification");
    gControlsNotification = $("#txtControlsNotification");

    gGlobalNotification.bind("change", function () { });
    gFormNotification.bind("change", function () { });
    gGridNotification.bind("change", function () { });
    gControlsNotification.bind("change", function () { });
}

function onControlsNotification(actionResultMessage) {
    gControlsNotification.val("");
    gControlsNotification.val(actionResultMessage);
    gControlsNotification.trigger("change");
}

function onFormNotification(actionResultMessage, form) {
    gFormNotification.val("");
    gFormNotification.val(actionResultMessage);
    $.data(gFormNotification, "FORM", form);
    gFormNotification.trigger("change");

}

function attachNewTabLinkEventHandler() {
    var className = _g_tab_className_LinkObject;
    $(document).on('click', '.' + className,
        function (evt) {
            var obj_linkNewTabClass = this;
            _common_getWindowParent()._call_tab_launchNewTab(obj_linkNewTabClass, evt);
        }
    );
}

function adjustPopupTabstripSize(dialog) {
    dialog.find('.k-tabstrip').find('.k-content').css("height", "80%");
}

function attachLaunchDynamicGridEventHandler() {
    $(document).on('click', '.linkModalDynamicGrid',
        function (evt) {
            var url = $(this).attr("url");
            var modalTitle = $(this).attr("modaltitle");
            var modalwidth = $(this).attr("modalwidth");
            var modalheight = $(this).attr("modalheight");
            var gridname = $(this).attr("gridname");
            var dataId = $(this).attr("transactionId");
            _common_getWindowParent().launchJqueryModalDynamicGrid(url, modalTitle, modalwidth, modalheight, dataId, evt, gridname);
        }
    );
}

function attachLaunchDetailsEventHandler() {
    var className = '.' + _g_common_Name_LinkDetailsObject;
    $(document).on('click', className,
        function (evt) {
            evt.preventDefault();

            var url = $(this).attr("href");
            var targetLevel = $(this).attr("targetLevel");
            launchJqueryDetails(url, null, evt, targetLevel);

            return false;
        }
    );
}

function launchJqueryDetails(url, data, evt, targetLevel) {
    evt.preventDefault();
    var detailsDivId = '#detailsDIV' + targetLevel;
    var detailsDiv = $(detailsDivId);

    if (!detailsDiv || detailsDiv.length <= 0) {
        //create a DIV dynamically;
        if (targetLevel === 1) {
            var masterDiv = $('#masterDIV');
            if (!masterDiv || masterDiv.length <= 0) {
                _director_showWarning('The masterDiv DIV element is not found on the page');
                return false;
            }
            $('#masterDIV').append('<br/><div id=detailsDIV' + targetLevel + '></div>');
        }
        else {
            $('#detailsDIV' + (targetLevel - 1)).append('<br/><div id=detailsDIV' + targetLevel + '></div>');
        }
    }

    $(detailsDivId).dialog("destroy");

    evt.preventDefault();
    //var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');
    var dialog = $(detailsDivId);

    dialog.empty();

    dialog
        .append('Loading...')
        .load(url, data, function () {
            //$(detailsDivId).find("input[type='button']").addClass('nice radius small blue button');
            $(detailsDivId).find('.t-grid').width('99%');
        }).show("drop", { direction: "up" }, 1000);

    return false;
}


function addGlobalAjaxSendSpinnerLoader() {
    $(document).ajaxSend(function (event, xhr, options) {

        //dont include spinner in default ajax calls for combobox or grid loading data.
        if (options.url.indexOf("ComboBox") >= 0 || options.url.indexOf("GetRecords") >= 0) return;

        var spinnerElement = $('<img id="loaderSpinner"></img>')
            .css('width', "30px").css('height', "25px").css("position", "relative").css("z-index", "2")
            .attr("src", _director_fixUrl(_g_common_Virtualpath + "/Resources/Images/loading_circle.gif"));

        var processingSpinnerContainer = $('<div id="divloaderMessage">Processing request, please wait...</div>')
            .append(spinnerElement);

        //get modal dialog to add spinner        
        var modalDialog = getDialogForSpinner();

        //check element if input element
        var targetElement = $(event.currentTarget.activeElement);
        var isTargetOk = targetElement.is('input') || targetElement.is('span');
        if (isTargetOk == undefined || isTargetOk == false)
            targetElement = undefined;

        //This will add loading.gif and hide buttons         
        if (targetElement) {
            setupTargetElementSpinner(targetElement, spinnerElement);
        } else {
            setupModalDialogWithSpinner(modalDialog, processingSpinnerContainer);
        }
    }).ajaxComplete(function () {
        removeSpinnerAndShowButtons();
    });
}

function getDialogForSpinner() {
    var modalDialog = $('#confirmModalDialog');
    //for dataentry dialog
    if (modalDialog) { if (modalDialog.length == 0) { modalDialog = gDialogEntry; } }
    return modalDialog;
}

function setupTargetElementSpinner(targetElement, spinnerElement) {
    //debugger;
    var hasSpinnerInElement = undefined;
    var parentContainerClass = $(targetElement).parent().attr('class');
    if (parentContainerClass != undefined) {
        var parentContainerIsFromComboBox = parentContainerClass.indexOf('k-dropdown-wrap') >= 0;
        if (parentContainerIsFromComboBox) {
            //combobox element
            hasSpinnerInElement = $(targetElement).parent().parent().next('#loaderSpinner');
            if (hasSpinnerInElement.length == 0) { $(targetElement).parent().parent().after(spinnerElement); }
        } else {
            //non combobox element
            hasSpinnerInElement = $(targetElement).parent().next('#loaderSpinner');
            if (hasSpinnerInElement.length == 0) { $(targetElement).parent().after(spinnerElement); }
        }
    }
}

function setupModalDialogWithSpinner(modalDialog, spinnerElement) {
    if (modalDialog) {
        var hasSpinner = modalDialog.find('img[id=loaderSpinner]'); if (hasSpinner.length == 0) { modalDialog.prepend(spinnerElement); }
        var buttons = $(".ui-dialog-buttonset button"); if (buttons) { buttons.hide(); }//hide buttons
    }
}

function removeSpinnerAndShowButtons() {
    //This will remove loading.gif and show buttons 
    var spinner = $('#loaderSpinner'); if (spinner) { spinner.remove(); }
    var processing = $('#divloaderMessage'); if (processing) { processing.remove(); }
    var buttons = $(".ui-dialog-buttonset button"); if (buttons) { buttons.show(); }
}