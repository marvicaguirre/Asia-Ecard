/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />

//for DELETE button
function _delete_attachDeleteButtonEventHandler(divPanel) {
    setButtonStyle(divPanel.find(".buttonDeleteClass"));
    divPanel.find(".buttonDeleteClass").each(function () {
        var divDetailsName = 'divDetails';
        var deleteButton = $(this);

        deleteButton.attr('toDeleteDiv', divDetailsName + '1');
        //deleteButton.attr('id', divDetailsName + 'Button1');
        //hack fix for multiple bind in delete click is to remove classname that is used for event selector
        var className = deleteButton.attr('class');
        if (className) {
            deleteButton.attr('class', className.replace('buttonDeleteClass ', ''));
        }

        $(this).closest("div[id^='divDetails']").each(function () {
            var divName = $(this).attr('id');
            if (divName) {
                var orderNumber = parseInt(divName.replace(divDetailsName, '')) + 1;
                deleteButton.attr('toDeleteDiv', divDetailsName + orderNumber);
                //deleteButton.attr('id', divDetailsName + 'Button' + orderNumber);
            }
        });

        deleteButton.click(function (evt) {
            launchDeleteModalConfirm($(this), divPanel, evt);
        });
    });
}

function launchDeleteModalConfirm(element, divPanel, evt) {
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

    launchJqueryDeleteModalConfirm(element, divPanel, buttonText, url, targetUrl, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function _delete_getSelectedRecords(buttonText, url, targetUrl, gridname, title, width, height, evt) {
    var divId = '#' + gridname + ' input[type=checkbox][name=checkedRecords]:checked';
    var selectedRecords = $(divId);

    if (selectedRecords.length < 1) {
        _director_showWarning('No record(s) has been selected');
        return;
    }
    //Caller is coming form _modalLayou.chtml
    _common_getWindowParent().launchJqueryDeleteModalConfirm(buttonText, url, targetUrl, title, width, height, selectedRecords, evt, gridname);
}

//for DELETE CONFIRMATION 
function launchJqueryDeleteModalConfirm(element, divPanel, buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
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
        buttons[buttonText] = function () { invokeDeleteAction(element, divPanel, evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
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
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Delete")'), 'ui-icon-trash');
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

function removeChildDivsWithGrid(divPanel) {
    var levelsToRemove = parseInt(divPanel.attr('toDeleteDiv').replace('divDetails', ''));
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

function invokeDeleteAction(element, divPanel, evt, dialog, url, args, gridname) {
    _director_setGlobalVariable(null, gridname, url);
    //hack fix to remove specific div
    divPanel.attr('toDeleteDiv', element.attr('toDeleteDiv'));
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
                removeChildDivsWithGrid(divPanel);

                if (data.ActionStatus === "Success") {
                    _director_showSuccess('Success!');
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
                    _director_showSuccess('Success!');
                    removeChildDivsWithGrid(divPanel);
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
