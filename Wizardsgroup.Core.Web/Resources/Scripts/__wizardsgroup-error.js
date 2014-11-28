/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />

function _error_launchErrorPage(jqXHR) {
    var text = "Request failed! (_medicard-error.js)";
    if (jqXHR) {
        text = jqXHR.responseText;
    }
    var errorUrl = _director_fixUrl(_g_common_Virtualpath + '/Common/Error/ShowError');
    //alert(errorUrl);
    var requestError = $.ajax({
        type: "GET",
        url: errorUrl,
        traditional: true,
        //data: { errorData: jqXHR.responseText }
        data: { errorData: text }
    });

    requestError.done(function (data) {
        if (data && data.length > 0) {
            var dialog = _common_getWindowParent().gDialogEntry;
            _common_getWindowParent()._popup_openDialog(dialog, "Success", 500, 500, data);
        }
        else {
            _director_showError('Done but failed launching the error page!');
        }
    });

    requestError.fail(function (jqXHR2, textStatus) {
        _director_showError(text); //'Failed launching the error page!');
    });
}