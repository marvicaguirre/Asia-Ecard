/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />



//for DELETE button
function _toggle_attachToogleButtonEventHandler(divPanel) {
    divPanel.find(".buttonToggleClass").each(function () {
        var toggleButton = $(this);

        var className = toggleButton.attr('class');
        if (className) {
            toggleButton.attr('class', className.replace('buttonToggleClass ', ''));
        }

        toggleButton.click(function (evt) {
            launchToggleModalConfirm($(this), evt);
        });
    });
    setButtonStyle(divPanel.find(".buttonToggleClass"));
}

function launchToggleModalConfirm(element, evt) {
    var url = element.attr("url");
    var targetUrl = element.attr("targetUrl");
    var modalTitle = element.attr("modaltitle");
    var modalwidth = element.attr("modalwidth");
    var modalheight = element.attr("modalheight");
    var gridname = element.attr("gridname");
    var buttonText = element.text();

    var selectedRecords = _grid_getGridSelectedRecords(gridname);
    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }

    _common_getWindowParent().launchJqueryModalConfirmToggle(buttonText, url, targetUrl, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function launchJqueryModalConfirmToggle(buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
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
        buttons.Toggle = function () { _common_invokeAction(evt, dialogConfirm, targetUrl, primaryKeys, gridname); };        
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
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Toggle")'), '');
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