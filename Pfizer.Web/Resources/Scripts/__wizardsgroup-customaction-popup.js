/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-general-controls.js" />

function _custom_attachCustomButtonEventHandler(divPanel) {
    divPanel.find(".buttonModalClass").each(function () {        
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
            var confirm = $(this).attr("withConfirm");
            _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, { id: parentId }, evt, gridname, autoClose, confirm);
        });
    });
}
