/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-grid.js" />





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
            var isModal = false;
            _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, { id: parentId }, evt, gridname, autoClose, isModal);
        });
    });

    //setButtonStyle(".buttonEntryClass");
    //setButtonStyle(divPanel.find(".buttonEntryClass"));
}

//function attachCreateButtonEventHandler2() {
//    $(document).on('click', ".buttonEntryClass",
//        function (evt) {
//            var url = $(this).attr("url");
//            var modalTitle = $(this).attr("modaltitle");
//            var modalwidth = $(this).attr("modalwidth");
//            var modalheight = $(this).attr("modalheight");
//            var gridname = $(this).attr("gridname");
//            var autoClose = $(this).attr("autoClose");
//            var actionName = $(this).attr("actionName");
//            //var parentId = $(this).attr("parentId");
//            //var isModal = false;
//            _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, null, evt, gridname, autoClose);
//        }
//    );
//}
