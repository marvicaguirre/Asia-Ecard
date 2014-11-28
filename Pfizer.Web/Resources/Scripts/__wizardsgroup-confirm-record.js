/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />



//for CONFIRM button
function _confirm_attachConfirmButtonEventHandler(divPanel) {
    divPanel.find(".buttonConfirmClass").each(function () {
        var confirmButton = $(this);

        var className = confirmButton.attr('class');
        if (className) {
            confirmButton.attr('class', className.replace('buttonConfirmClass ', ''));
        }

        confirmButton.click(function (evt) {
            launchUpdateModalConfirm($(this), evt);
        });
    });
    setButtonStyle(divPanel.find(".buttonConfirmClass"));
}

function launchUpdateModalConfirm(element, evt) {
    var url = element.attr("url");
    var targetUrl = element.attr("targetUrl");
    var modalTitle = element.attr("modaltitle");
    var modalwidth = element.attr("modalwidth");
    var modalheight = element.attr("modalheight");
    var gridname = element.attr("gridname");
    var selectionMode = element.attr('selectionMode');
    var validationAction = element.attr('validationAction');
    var controller = element.attr('controller');
    var buttonText = element.text();

    var selectedRecords = _grid_getGridSelectedRecords(gridname);
    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }

    if (selectionMode == 'Single' && selectedRecords.length > 1) {
        _director_showWarning('Only one record should be selected');
        return;
    }

    if (validationAction && validationAction != '') {
        var result = !_customButtonValidationAction(selectedRecords, controller, validationAction, selectionMode);
        if (result) return;
    }

    _common_getWindowParent().launchJqueryModalConfirm(buttonText, url, targetUrl, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function launchJqueryModalConfirm(buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
    var dialogConfirm = $("<div id='confirmModalDialog'></div>");

    evt.preventDefault();
    var primaryKeys = generatePrimaryKeys(data);

    var request = $.ajax({
        type: "GET",
        url: url,
        traditional: true,
        data: { checkedRecords: primaryKeys }
    });

    request.done(function (callbackdata) {
        if (callbackdata.hasOwnProperty('ActionStatus')) {
            if (callbackdata.ActionStatus === "Warning") {
                _director_showWarning(callbackdata.Messages);
                return;
            }
        }

        var tableKendo = $("<div></div>");
        tableKendo.kendoGrid({
            dataSource: {
                data: callbackdata
            }
            //height: 150
        });

        ////Get table Title From Source Grid (BugId :249)
        tableKendo.find("table thead th").each(function () {
            $(this).text($(this).text().replace(/([a-z])([A-Z])/g, "$1 $2"));
            var fieldName = $(this).attr("data-field");
            var athead = $("#" + gridname + " table thead th[data-field=" + fieldName + "]");
            if (athead.lenth !== 0) {
                if (athead.attr("data-title") !== "") {
                    $(this).text(athead.attr("data-title"));
                }
            }

        });

        var buttons = {};
        if (buttonText.toLowerCase() == "for franchising" ||
            buttonText == "for affiliate company approval" ||
            buttonText == "Contest Extension") {
            buttons.Process = function() { _common_invokeAction(evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
        } else {
            buttons.Confirm = function () { _common_invokeAction(evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
        }
        buttons.Cancel = function () { $('.ui-dialog-content').dialog('destroy').remove(); };

        //gDialogEntry.empty();
        dialogConfirm
			 .html(tableKendo);

        if (width == 0 && height == 0) {
            width = $(window).width() * 0.5;
            height = $(window).height() * 0.5;
        }

        dialogConfirm.dialog({
            draggable: true
			, title: title
			, width: width
			, height: height
			, minHeight: 200
			, modal: true
			, show: 'fade'
			, hide: { effect: 'fade', direction: 'right' }
			, close: $('.ui-dialog-content').dialog('destroy').remove()
			, buttons: buttons
			, position: ["middle", 100]
			, open: function (event) {
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Confirm")'), '');
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Process")'), '');
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Cancel")'), 'ui-icon-cancel');
			}
        });
        dialogConfirm.dialog('open');
    });

    request.fail(function (jqXHR, textStatus) {
        alert("Request failed: " + textStatus);
    });
}

function generatePrimaryKeys(source) {
    var myArray = extractArrayOfAttributeValues(source, 'primaryid');
    return myArray;
}

function extractArrayOfAttributeValues(source, attributeName) {
    var myArray = [];
    source.each(function (index) {
        myArray[index] = $(this).attr(attributeName);
    });

    return myArray;
}