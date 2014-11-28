/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-confirm-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-custom-approval.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-buttonAction.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-module-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-delete-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-drop-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-create-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-edit-record.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-customaction-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-toggleStatus.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-selectModal.js" />

$(function () {

    addGlobalAjaxSendSpinnerLoader();
    //set the Build;
    gGridName_ModuleLayout = $("div.t-grid").attr('id');

    var divMaster = $(_g_common_selector_DivMaster);
    divMaster.find(".buttonEntryClass").hide();
    //divMaster.find(".buttonDeleteClass").hide();

    //Initialized Notification (Global,Form,Grid,Controls)

    var divMaster = $(_g_common_selector_DivMaster);
    createCustomKendoGrid(divMaster);

    initializedNotification();

    //see: http://api.jquery.com/live/ for replacement to 'live' event
    _create_attachCreateButtonEventHandler(divMaster);

    _delete_attachDeleteButtonEventHandler(divMaster);

    _drop_attachDropButtonEventHandler(divMaster);

    _toggle_attachToogleButtonEventHandler(divMaster);

    _custom_attachCustomButtonEventHandler(divMaster);

    _custom_attachCustomButtonActionEventHandler(divMaster);

    _confirm_attachApprovalButtonEventHandler(divMaster);

    _edit_attachLaunchModalEventHandler(divMaster);

    _confirm_attachConfirmButtonEventHandler(divMaster);
    
    _select_attachSelectModalButtonEventHandler(divMaster);
    
    attachNewTabLinkEventHandler();

    attachLaunchDynamicGridEventHandler();

    // This is redundant, already handled above @ createCustomKendoGrid(divMaster).
    // Verified the underlying function calls and event handlers ....
    //attachLaunchDetailsEventHandler();
});
