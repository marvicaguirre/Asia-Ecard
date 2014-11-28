/// <reference path="~/Resources/Scripts/__medicard-grid.js" />
/// <reference path="~/Resources/Scripts/__medicard-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />

var _g_common_Applicationpath = "";
var _g_common_Virtualpath = "";
var _g_common_SelectedRecords = [];
var _g_common_Url = "";
var _g_common_Name_LinkDetailsObject = "linkDetailsClass";
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

function resizeiFrame(FrameID) {
    var contentSize = parent.document.getElementById(FrameID).contentWindow.document.body.scrollHeight + 50;

    if (contentSize > 850) {
        parent.document.getElementById(FrameID).height = contentSize;
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
    return "inherit";
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

    _assignId(e, keyName, url, dataValue, (function () {if (gridId) {refreshGrid(gridId);}}));
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
            if (data === "Success") {
                //dialog.dialog("destroy").remove();
                $('.ui-dialog-content').dialog('destroy').remove();
                _grid_refreshChildWindowGrid(gridname);
                _director_showSuccess('Success!');
            }
            else {
                _director_showError('Error! ' + data);
            }
        },
        error: function () {
            _director_showError(top.messageEnum.errorMessage);
        },
        complete: function () {
        }
    });
}

function _common_launchJqueryDetails(url, data, evt, targetLevel) {
    evt.preventDefault();
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
        }).show("drop", { direction: "up" }, 1000);
    return false;
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
