/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />

//for EDIT link
function _edit_attachLaunchModalEventHandler() {
    $(document).on('click', '.linkModalClass',
        function (evt) {
            var url = $(this).attr("href");

            var modalTitle = $(this).attr("modaltitle");
            var modalwidth = $(this).attr("modalwidth");
            var modalheight = $(this).attr("modalheight");
            var gridname = $(this).attr("gridname");
            var autoClose = $(this).attr("autoClose");
            var actionName = $(this).attr("actionName");
            //var parentId = $(this).attr("parentId");
            //var isModal = false;
            _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, null, evt, gridname, autoClose);

            return false;
        }
    );
}