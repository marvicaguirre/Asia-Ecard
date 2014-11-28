/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />

//for EDIT link
function _edit_attachLaunchModalEventHandler(divMaster) {
    $(divMaster).off('click', '.linkModalClass').on('click', '.linkModalClass',
        function (evt) {

            $(this).bind('oncontextmenu', function () {
                return false;
            });
            
            evt.preventDefault();            
            if ($(this).attr("href") != 'javascript:void(0)') {
                $(this).attr("url", $(this).attr("href"));
                $(this).attr("href", 'javascript:void(0)');
                var className = $(this).attr("class");
                $(this).attr('class', className.replace('linkModalClass', ''));
                //bind new event for link click
                $(this).on('click', function (evt) {
                    var url = $(this).attr("url");
                    var modalTitle = $(this).attr("modaltitle");
                    var modalwidth = $(this).attr("modalwidth");
                    var modalheight = $(this).attr("modalheight");
                    var gridname = $(this).attr("gridname");
                    var autoClose = $(this).attr("autoClose");
                    var actionName = $(this).attr("actionName");            
                    _common_getWindowParent()._popup_launchJqueryEntry(url, actionName, modalTitle, modalwidth, modalheight, null, evt, gridname, autoClose);                   
                    return false;                    
                });
                //1st click
                $(this)[0].click();                
            }            
        });

    $(divMaster).on('contextmenu', '.linkModalClass', function (evt) {
        return false;
    });


}