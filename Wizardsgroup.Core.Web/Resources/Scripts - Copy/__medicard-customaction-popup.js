/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />
/// <reference path="~/Resources/Scripts/__medicard-layout.js" />
/// <reference path="~/Resources/Scripts/__medicard-grid.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />
/// <reference path="~/Resources/Scripts/__medicard-datepicker.js" />
/// <reference path="~/Resources/Scripts/__medicard-general-controls.js" />

function _custom_attachCustomButtonEventHandler(divPanel) {
    divPanel.find(".buttonModalClass").each(function () {        
        debugger;
        setButtonStyle(this);
        $(this).click(function (evt) {
            var url = $(this).attr("url");
            var modalTitle = $(this).attr("modaltitle");
            var modalwidth = $(this).attr("modalwidth");
            var modalheight = $(this).attr("modalheight");
            var gridname = $(this).attr("gridname");
            var autoClose = $(this).attr("autoClose");
            var actionName = $(this).attr("actionName");
            var parentId = $(this).attr("parentId");
            var isModal = false;
            _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, { id: parentId }, evt, gridname, autoClose, isModal);
        });
    });
}
