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

$(function () {


    $(document).ajaxSend(function () {
        //This will prepend loading.gif and hide buttons 
        var spinner = $('<img id="loaderSpinner"></img>');
        spinner.attr("src", _director_fixUrl(_g_common_Virtualpath + "/Resources/Images/loading_circle.gif"));
        var processing = $('<div id="divloaderMessage">Processing request, please wait...</div>');
        processing.append(spinner);
        //for multiselect dialog        
        var modalDialog = $('#confirmModalDialog');
        //for dataentry dialog
        if (modalDialog) {
            if (modalDialog.length == 0)
                modalDialog = gDialogEntry;
        }

        if (modalDialog) {
            var hasSpinner = modalDialog.find('img[id=loaderSpinner]');
            if (hasSpinner.length == 0) {
                modalDialog.prepend(processing);
            }
            var buttons = $(".ui-dialog-buttonset button");
            if (buttons)
                buttons.hide();
        }
    }).ajaxComplete(function () {
        //This will remove loading.gif and show buttons 
        var spinner = $('#loaderSpinner');
        if (spinner) {
            spinner.remove();
        }
        var processing = $('#divloaderMessage');
        if (processing) {
            processing.remove();
        }

        var buttons = $(".ui-dialog-buttonset button");
        if (buttons)
            buttons.show();
    });

    //set the Build;
    gGridName_ModuleLayout = $("div.t-grid").attr('id');

    var divMaster = $(_g_common_selector_DivMaster);
    divMaster.find(".buttonEntryClass").hide();
    //divMaster.find(".buttonDeleteClass").hide();

    //Initialized Notification (Global,Form,Grid,Controls)
    initializedNotification();

    //see: http://api.jquery.com/live/ for replacement to 'live' event
    //attachCreateButtonEventHandler2();
    _create_attachCreateButtonEventHandler(divMaster);

    _delete_attachDeleteButtonEventHandler(divMaster);

    _drop_attachDropButtonEventHandler(divMaster);

    _toggle_attachToogleButtonEventHandler(divMaster);

    _custom_attachCustomButtonEventHandler(divMaster);

    _custom_attachCustomButtonActionEventHandler(divMaster);

    _confirm_attachApprovalButtonEventHandler(divMaster);

    //_post_attachPostButtonEventHandler(divMaster);

    _edit_attachLaunchModalEventHandler();

    //attachConfirmButtonEventHandler();
    _confirm_attachConfirmButtonEventHandler(divMaster);

    attachNewTabLinkEventHandler();

    attachLaunchDynamicGridEventHandler();

    attachLaunchDetailsEventHandler();
});

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

function attachConfirmButtonEventHandler() {
    $(document).on('click', '.buttonConfirmClass',
        function (evt) {
            var url = $(this).attr("url");
            var targetUrl = $(this).attr("targetUrl");
            var modalTitle = $(this).attr("modaltitle");
            var modalwidth = $(this).attr("modalwidth");
            var modalheight = $(this).attr("modalheight");
            var gridname = $(this).attr("gridname");
            var buttonText = $(this).attr("value");

            _common_getWindowParent()._confirm_getSelectedRecords(buttonText, url, targetUrl, gridname, modalTitle, modalwidth, modalheight, evt);
        }
    );
    setButtonStyle($(_g_common_selector_DivMaster).find('.buttonConfirmClass'));
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



