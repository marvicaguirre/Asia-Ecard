/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />

//for CREATE button
function _create_attachCreateButtonEventHandler(divPanel) {
    divPanel.find(".buttonEntryClass").each(function () {

        setButtonStyle(this);
        
        $(this)
        //.attr("style", getButtonStyle())
        //.button({ icons: { primary: 'ui-icon-circle-plus' } })
        .click(function (evt) {
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
