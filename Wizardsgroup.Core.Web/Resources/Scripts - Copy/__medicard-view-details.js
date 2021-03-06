﻿/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />
/// <reference path="~/Resources/Scripts/__medicard-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__medicard-popup.js" />
/// <reference path="~/Resources/Scripts/__medicard-error.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />
/// <reference path="~/Resources/Scripts/jquery-1.9.1.js" />


function _details_assignViewDetailsLinkEventHandler(grid) {
    //for the DETAILS link
    var className = _g_common_Name_LinkDetailsObject;
    grid.find("tbody tr ." + className).click(
        function (evt) {
            var linkObject = evt.target;

            evt.preventDefault();
            var url = $(linkObject).attr("url");
            var targetLevel = $(linkObject).attr("targetLevel");
            var primaryId = $(linkObject).attr("primaryid");

            //alert('in grid js; ' + primaryId);
            //comment today
            //_common_assignClientId(linkObject, evt, primaryId);

            //Hight Light Row Grid
            //grid.find("tbody tr").removeClass('k-state-selected');
            //$(this).closest("tr").addClass('k-state-selected');

            var data = { id: primaryId };
            url = _director_fixUrl(url);
            _common_launchJqueryDetails(url, data, evt, targetLevel);
            return false;
        }
    );
}