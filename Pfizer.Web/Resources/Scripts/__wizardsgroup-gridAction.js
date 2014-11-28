function _details_assignActionLinkEventHandler(grid) {
    //for the DETAILS link
    var className = _g_common_Name_LinkActionsObject;
    grid.find("tbody tr ." + className).click(
        function (evt) {
            var linkObject = evt.target;

            evt.preventDefault();
            var url = $(linkObject).attr("url");
            var primaryId = $(linkObject).attr("primaryid");


            url = _director_fixUrl(url);
            _common_launchJqueryActions(url, primaryId, evt);
            return false;
        }
    );
}