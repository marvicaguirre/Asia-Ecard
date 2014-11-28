function DownloadDocument(url, gridName) {
    var result = false;
    var selectedRecords = _grid_getGridSelectedRecords(gridName);

    if (selectedRecords.length > 0) {
        if (selectedRecords.length > 1) {
            _director_showWarning('Select one document to download');
        } else {
            $.getJSON(GetDataUrlDocument(url.replace('Download', 'CopyDownload'), selectedRecords), function (data) {
                if (data.ActionStatus == "Success") {
                    window.open(GetDataUrlDocument(url, selectedRecords), "_self");
                } else {
                    _director_showWarning(data.Messages[0]);
                }
            });
            result = true;
        }
    }
    else {
        _director_showWarning('No document(s) selected for downloading');
    }

    return result;
}

function GetDataUrlDocument(url, selectedRecords) {
    var primaryKeys = generatePrimaryKeys(selectedRecords);
    var qString = "?";

    for (var i = 0; i < primaryKeys.length; i++) {
        qString = qString + "checkedRecords=" + primaryKeys[i] + "&";
    }

    return _director_fixUrl(url) + qString;
}