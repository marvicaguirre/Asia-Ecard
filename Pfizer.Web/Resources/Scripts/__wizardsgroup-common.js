/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />

var _g_common_Applicationpath = "";
var _g_common_Virtualpath = "";
var _g_common_SelectedRecords = [];
var _g_common_Url = "";
var _g_common_Name_LinkDetailsObject = "linkDetailsClass";
var _g_common_Name_LinkActionsObject = "linkActionClass";
var _g_common_name_DivMaster = 'divMaster';
var _g_common_selector_DivMaster = '#' + _g_common_name_DivMaster;

$(function () {

    //alert('getServerSoftware(): ' + getServerSoftware());
});



//Sample url : /HRAdministration/AddedIncome/GetReferenceType
//function fixUrl(myurl) {
//    var url = window._g_common_Applicationpath + myurl;
//    return url;
//}

// Grid Filter Input Parameter
function composeJsonParameter(parameter) {
    if (parameter === undefined)
        parameter = "";

    var paramArray = parameter.split(',');
    var jsonParam = [];
    for (var item in paramArray) {
        jsonParam.push({ FieldName: paramArray[item], FieldValue: $('#' + paramArray[item]).val() });
    }
    return kendo.stringify(jsonParam);
}

function getLoadingImage() {
    var img = $("<img></img>");
    img.attr("src", _director_fixUrl(_g_common_Virtualpath + "/Resources/Images/loading_circle.gif"));
    return img;
}

function _common_getWindowParent() {
    return window.parent;
}

function resizeiFrame(frameId, heightToAdd) {       
    var contentSize = $('#divMaster')[0].scrollHeight + 320;//parent.document.getElementById(frameId).contentWindow.document.body.scrollHeight; //+ 50;
    //console.log('contentSize : ' + contentSize);
    if (contentSize > 850) {
        if (heightToAdd == undefined)
            heightToAdd = 0;

        //console.log('height to add : ' + heightToAdd);
        //var currentFrameHeight= parent.document.getElementById(frameId).height;
        //console.log('current IFrame size : ' + currentFrameHeight);
        parent.document.getElementById(frameId).height = contentSize + heightToAdd;
        //get element outside iframe
        var container = $("div[id^='medicard-tabstrip-']", window.parent.document);
        //console.log('current container : ' + container.height());
        container.css('height', contentSize + heightToAdd);
        //console.log('new container : ' + container.height());
        //console.log('new IFrame size : ' + parent.document.getElementById(frameId).height);
    }
}

function _common_topMostWindow() {
    //todo: see if there is a simpler way to do this;
    var theWindow = window;
    //while (theWindow.location.href.toLowerCase().indexOf("home/index") < 0
    //    && theWindow.parent.frames.length > 0) {
    //    theWindow = theWindow.parent;
    //}
    return theWindow;
}

function getButtonStyle() {
    //return "height:24px;font-size:11px";
    return "inherit;";
}

function setButtonStyle(buttonSelector, icon) {
    var theIcon = '';
    if (icon) {
        theIcon = icon;
    }
    //alert(buttonSelector);
    $(buttonSelector).each(function () {
        //alert('hello!' + $(this));
        $(this)
            .attr("style", getButtonStyle())
            .removeClass("btn btn-info")
            .addClass("btn btn-info")
        //.button({ icons: { primary: theIcon } })
        ;
    });
}

//function _common_filterAndReloadGrid(e, gridId, keyName, url) {
//function _common_filterAndReloadGrid(e, keyName, url, gridId) {
//    //if ("kendoConsole" in window) {
//    //kendoConsole.log("event :: select (" + dataItem.Text + " : " + dataItem.Value + ")");
//    //}
//    var index = -1;
//    if (e.item) {
//        index = e.item.index();
//    } else {
//        index = e.sender.selectedIndex;
//    }

//    //var dataValue = 0;
//    //if (index) {
//    //    var dataItem = this.dataItem(index);
//    //    //var dataValue = dataItem.Value;
//    //    dataValue = dataItem.Value;
//    //}
//    var dataValue = getSelectedValue(index, this);

//    //initialize parameters to the controller function
//    var dataValues = {};
//    dataValues[keyName] = dataValue;

//    $.ajax({
//        url: url,
//        data: dataValues,
//        success: function (data) {
//            if (gridId) {
//                refreshGrid(gridId);
//            }
//        }
//    });
//}

function _common_filterAndReloadGrid(e, keyName, url, gridId) {
    var dataValue = getSelectedValueFromDropdown(this, e);

    //initialize parameters to the controller function
    var dataValues = {};
    dataValues[keyName] = dataValue;

    _assignId(e, keyName, url, dataValue, (function () { if (gridId) { refreshGrid(gridId); } }));
}

function _common_assignClientId(sender, e, clientId) {
    var keyName = 'clientId';
    //var url = _g_common_Virtualpath + "/Common/RouteValue/SetClientIdFilter";

    var url = _scriptContainer_getpath_SetClientIdFilter();
    _assignId(e, keyName, url, clientId, defaultSuccessCallback);
}

function _assignId(e, keyName, url, dataValue, successCallback) {
    //if ("kendoConsole" in window) {
    //kendoConsole.log("event :: select (" + dataItem.Text + " : " + dataItem.Value + ")");
    //}

    //initialize parameters to the controller function
    var dataValues = {};
    dataValues[keyName] = dataValue;

    $.ajax({
        url: url,
        data: dataValues,
        success: successCallback
    });
}

function defaultSuccessCallback() {
    //alert('in common.js; defaultSuccessCallback; success!');
}

function getSelectedValueFromDropdown(sender, e) {
    var that = sender;
    var index = -1;
    if (e.item) {
        index = e.item.index();
    } else {
        index = e.sender.selectedIndex;
    }

    var dataValue = getSelectedValue(index, that);
    return dataValue;
}

function getSelectedValue(index, sender) {
    var that = sender;
    var dataValue = 0;
    if (index > -1) {
        var dataItem = that.dataItem(index);
        //var dataValue = dataItem.Value;
        dataValue = dataItem.Value;
    }
    return dataValue;
}

function refreshGrid(gridId) {
    if (!gridId) {
        _director_showError("Missing gridId");
        return;
    }
    $(gridId).data(_g_grid_divObject_ControlType).dataSource.read();
}

function getFormEntity() {
    if ($('form').length === 0)
        return "";

    var formAction = $('form').attr("action");

    if (_g_common_Virtualpath.length > 1) {
        formAction = formAction.replace(_g_common_Virtualpath, '');
    }
    var splitUrl = formAction.split("/");
    if (splitUrl.length > 1) {
        return splitUrl[2];
    }
    return "";
}

function _common_invokeAction(evt, dialog, url, args, gridname) {
    _director_setGlobalVariable(null, gridname, url);


    evt.preventDefault();
    $.ajax({
        url: url,
        type: "POST",
        data: { checkedRecords: args },
        datatype: "json",
        traditional: true,
        success: function (data) {
            if (data.hasOwnProperty('ActionStatus')) {
                if (data.ActionStatus === "Success") {
                    $('.ui-dialog-content').dialog('destroy').remove();
                    _grid_refreshChildWindowGrid(gridname);

                    _director_showSuccess(data.Messages);
                }
                else if (data.ActionStatus === "Warning") {
                    _director_showWarning(data.Messages);
                }
                else {
                    _director_showError('Error! ' + data);
                }
            }
            else {
                if (data === "Success") {
                    $('.ui-dialog-content').dialog('destroy').remove();
                    _grid_refreshChildWindowGrid(gridname);

                    if (!data.hasOwnProperty('Messages')) {
                        _director_showSuccess("Success");
                    } else {
                        _director_showSuccess(data.Messages);
                    }
                }
                else if (data === "Warning") {
                    _director_showWarning("Warning: A problem was encountered during the operation.");
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

// Entry point for creating new KendoGrid, grid location (targetLevel) will be dynamically determined using
// the "evt" parameter as basis. 
function _common_launchJqueryDetails(url, data, evt, targetLevel, overrideTarget) {
    evt.preventDefault();
    
    // do we need to modify the "targetLevel"?
    if (overrideTarget == undefined || overrideTarget.length <= 0
        || targetLevel == undefined || targetLevel.length <= 0) {
        // check the parent of the element that trigger the event
        var evtTarget = evt.target;
        var parentdivDetails = $(evtTarget).parents('div[id^="divDetails"]');
        // do we have a valid source source divDetails?
        if (!parentdivDetails || parentdivDetails.length <= 0) {
            // then targetLevel = 1
            targetLevel = 1;
        } else {
            // +1 to current parentdivDetails count
            targetLevel = $(parentdivDetails).length + 1;
        }
    }

    var detailsDivId = '#divDetails' + targetLevel;
    var detailsDiv = $(detailsDivId);

    //check if DIV is manually created
    if (!detailsDiv || detailsDiv.length <= 0) {
        if (!createDynamicDiv(targetLevel)) return false;
    }

    //$(detailsDivId).dialog("destroy");
    $('.ui-dialog-content').dialog('destroy').remove();

    evt.preventDefault();
    var dialog = $(detailsDivId);
    dialog.empty();
    dialog
        .append('Loading...').append(getLoadingImage())
        .load(url, data, function () {
            $(detailsDivId).find('.t-grid').width('92%');
            //attachCreateButtonEvent($(detailsDivId));
            //attachDeleteButtonEvent($(detailsDivId));
            var frameId = window.frameElement.id;
            frameId && resizeiFrame(window.frameElement.id);
        }).show("drop", { direction: "up" }, 1000);
    return false;
}


function _common_launchJqueryActions(url, data, evt) {
    evt.preventDefault();
    var result = false;


    //window.open(url + "?id=" + data);
    $.ajax({
        url: url,
        type: "POST",
        data: { id: data },
        datatype: "json",
        traditional: true,
        success: function (callback) {
            if (callback.hasOwnProperty('ActionStatus')) {
                if (callback.ActionStatus === "Success") {
                    _director_showSuccess('Success!');
                }
                else {
                    _director_showWarning(callback.Messages);
                }
            }
            else {
                window.open(url + "?id=" + data, "_self");
            }
        },
        error: function () {
            //_director_showError(top.messageEnum.errorMessage);
        },
        complete: function () {
        }
    });

    result = true;

    return result;
}


function createDynamicDiv(targetLevel) {
    if (!targetLevel) {
        _director_showWarning('Missing targetLevel parameter!');
        return false;
    }

    var htmlToInsert = '<br/><div id=divDetails' + targetLevel + '></div>';

    //create a DIV dynamically;
    if (targetLevel == 1) {
        var masterDiv = $(_g_common_selector_DivMaster);
        if (!masterDiv || masterDiv.length <= 0) {
            _director_showWarning('The ' + _g_common_name_DivMaster + ' DIV element is not found on the page');
            return false;
        }
        $(_g_common_selector_DivMaster).append(htmlToInsert);
    }
    else {
        $('#divDetails' + (targetLevel - 1)).append(htmlToInsert);
    }
    return true;
}

function hasAttribute(obj, attribute) {
    //Checks element attribute
    var attr = $(obj).attr(attribute);
    if (typeof attr !== undefined && attr !== false) {
        return true;
    }
    return false;
}

function _common_resetValidations() {
    $('.field-validation-error')
    .removeClass('field-validation-error')
    .addClass('field-validation-valid');

    $('.input-validation-error')
    .removeClass('input-validation-error')
    .addClass('valid');

    // Reset Validation Sumary
    $(".validation-summary-errors")
    .removeClass("validation-summary-errors")
    .addClass("validation-summary-valid");
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

function _customButtonValidationAction(data, controller, validationAction, mode) {
    debugger;
    var primaryKeys = generatePrimaryKeys(data);
    var validationResult = mode == 'Multiple' ?
            _buttonValidationForMultipleRecord(primaryKeys, controller, validationAction)
            : _buttonValidationForSingleRecord(primaryKeys, controller, validationAction);
    return validationResult;
}

function _buttonValidationForSingleRecord(primaryKeys, controller, validationAction) {
    var validationResult = false;
    var singleRequest = $.ajax({
        type: "GET",
        url: _director_fixUrl(window._g_common_Virtualpath + '/Common/Button/ValidationActionForSingle'),
        traditional: true,
        async: false,
        data: { checkedRecord: primaryKeys, controllerName: controller, actionToExecute: validationAction }
    });
    singleRequest.done(function (result) {
        if (result.Success) {
            validationResult = true;
        } else {
            _director_showWarning(result.Message);
            validationResult = false;
        }
    });
    return validationResult;
}

function _buttonValidationForMultipleRecord(primaryKeys, controller, validationAction) {
    var validationResult = false;
    var multipleRequest = $.ajax({
        type: "GET",
        url: _director_fixUrl(window._g_common_Virtualpath + '/Common/Button/ValidationActionForMultiple'),
        traditional: true,
        async: false,
        data: { checkedRecords: primaryKeys, controllerName: controller, actionToExecute: validationAction }
    });
    multipleRequest.done(function (result) {
        if (result.Success) {
            validationResult = true;
        } else {
            _director_showWarning(result.Message);
            validationResult = false;
        }
    });
    return validationResult;
}