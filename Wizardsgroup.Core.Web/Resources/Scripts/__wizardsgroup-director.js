/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/jquery-1.9.1.js"/>


function _director_setGlobalVariable(selectedRecords, grid, url) {
    _director_setGlobalVariable_selectedRecords(selectedRecords);
    _director_setGlobalVariable_url(url);
    _director_setGlobalVariable_grid(grid);
    _tab_setGlobalVariable();
}


function _director_setGlobalVariable_selectedRecords(selectedRecords) {
    if (selectedRecords) {
        window._g_common_SelectedRecords = selectedRecords;
    }
}

function _director_setGlobalVariable_grid(grid) {
    if (grid) {
        _grid_setGlobalVariable(grid);
    }
}

function _director_setGlobalVariable_url(url) {
    if (url) {
        window._g_common_Url = url;
    }
}

function _director_showError(message) {
    _common_getWindowParent()._notification_showJBarMessage_error(message);
}

function _director_showInfo(message) {
    _common_getWindowParent()._notification_showJBarMessage_info(message);
}

function _director_showSuccess(message) {
    _common_getWindowParent()._notification_showJBarMessage_success(message);
}

function _director_showWarning(message) {
    _common_getWindowParent()._notification_showJBarMessage_warning(message);
}

function _director_fixUrl(relativeUrl) {
    //_common_getWindowParent().fixUrl(myurl);
    var url = window._g_common_Applicationpath + relativeUrl.replace("//", "/");
    return url;
}