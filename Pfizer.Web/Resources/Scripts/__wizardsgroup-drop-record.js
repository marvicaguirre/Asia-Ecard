/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />

//for DROP button
function _drop_attachDropButtonEventHandler(divPanel) {
    setButtonStyle(divPanel.find(".buttonDropClass"));
    divPanel.find(".buttonDropClass").each(function () {
        var divDetailsName = 'divDetails';
        var dropButton = $(this);

        dropButton.attr('toDropDiv', divDetailsName + '1');
        //dropButton.attr('id', divDetailsName + 'Button1');
        //hack fix for multiple bind in drop click is to remove classname that is used for event selector
        var className = dropButton.attr('class');
        if (className) {
            dropButton.attr('class', className.replace('buttonDropClass ', ''));
        }

        $(this).closest("div[id^='divDetails']").each(function () {
            var divName = $(this).attr('id');
            if (divName) {
                var orderNumber = parseInt(divName.replace(divDetailsName, '')) + 1;
                dropButton.attr('toDropDiv', divDetailsName + orderNumber);
                //dropButton.attr('id', divDetailsName + 'Button' + orderNumber);
            }
        });

        dropButton.click(function (evt) {
            launchDropModalConfirm($(this), divPanel, evt);
        });
    });
}

function launchDropModalConfirm(element, divPanel, evt) {
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

    launchJqueryDropModalConfirm(element, divPanel, buttonText, url, targetUrl, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function _drop_getSelectedRecords(buttonText, url, targetUrl, gridname, title, width, height, evt) {
    var divId = '#' + gridname + ' input[type=checkbox][name=checkedRecords]:checked';
    var selectedRecords = $(divId);

    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }
    //Caller is coming form _modalLayou.chtml
    _common_getWindowParent().launchJqueryDropModalConfirm(buttonText, url, targetUrl, title, width, height, selectedRecords, evt, gridname);
}

//for DROP CONFIRMATION 
function launchJqueryDropModalConfirm(element, divPanel, buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
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
        buttons[buttonText] = function () { invokeDropAction(element, divPanel, evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
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
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Drop")'), 'ui-icon-trash');
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Cancel")'), 'ui-icon-cancel');
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("' + buttonText + '")'), 'ui-icon-trash');
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

function removeChildDivsWithGridDrop(divPanel) {
    var levelsToRemove = parseInt(divPanel.attr('toDropDiv').replace('divDetails', ''));
    var childControls = divPanel.children("[id^='divDetails']");
    childControls.each(function () {
        if (levelsToRemove == 1) {
            $(this).remove();
        } else {
            var selector = "#divDetails" + levelsToRemove;
            var divToRemove = $(this).find(selector);
            divToRemove.remove();
        }
    });
}

function invokeDropAction(element, divPanel, evt, dialog, url, args, gridname) {
    _director_setGlobalVariable(null, gridname, url);
    //hack fix to remove specific div
    divPanel.attr('toDropDiv', element.attr('toDropDiv'));
    evt.preventDefault();
    $.ajax({
        url: url,
        type: "POST",
        data: { checkedRecords: args },
        datatype: "json",
        traditional: true,
        success: function (data) {
            if (data.hasOwnProperty('ActionStatus')) {
                $('.ui-dialog-content').dialog('destroy').remove();
                _grid_refreshChildWindowGrid(gridname);
                removeChildDivsWithGridDrop(divPanel);

                if (data.ActionStatus === "Success") {
                    //_director_showSuccess('Success!');
                    _director_showSuccess(data.Messages);
                }
                else if (data.ActionStatus === "Warning") {
                    _director_showWarning(data.Messages);
                }
                else {
                    _director_showError(data.Messages);
                }
            }
            else {
                if (data === "Success") {
                    $('.ui-dialog-content').dialog('destroy').remove();
                    _grid_refreshChildWindowGrid(gridname);
                    //_director_showSuccess('Success!');
                    _director_showSuccess(data.Messages);
                    removeChildDivsWithGridDrop(divPanel);
                }
                else {
                    _director_showError('Error! ' + data);
                }
            }
        },
        error: function () {
            _director_showError(top.messageEnum.errorMessage);
        },
        complete: function () {
        }
    });
}
