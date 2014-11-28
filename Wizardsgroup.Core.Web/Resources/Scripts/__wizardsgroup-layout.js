/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />

/********Global Variable use during Modal Popup**********/

var gErrorHtmlData = "";
var gDataDictionaryData = "";

function getSelectedRecords() {
    return _g_common_SelectedRecords;
}

function getUrl() {
    return _g_common_Url;
}

function getErrorHtmlData() {
    return gErrorHtmlData;
}

function getDataDictionaryData() {
    return gDataDictionaryData;
}


/********Global Variable use during Modal Popup**********/

function resetHtmlControls() {
    $('select').val("");
    $('input[type=text]').val("");
}

//function resetValidations() {
//    $('.field-validation-error')
//    .removeClass('field-validation-error')
//    .addClass('field-validation-valid');

//    $('.input-validation-error')
//    .removeClass('input-validation-error')
//    .addClass('valid');

//    // Reset Validation Sumary
//    $(".validation-summary-errors")
//    .removeClass("validation-summary-errors")
//    .addClass("validation-summary-valid");
//}

function onSuccess() {
    $("#divEntry").dialog("close");
}

function hrPostAction(targetUrl) {
    /******_g_common_Url,_g_common_SelectedRecords and _g_grid_GridName (ref: jquery-hrwizards.js)********/
    //var myurl = _g_common_Url + "Confirmed";
    var checkboxes = _g_common_SelectedRecords;
    var gridName = _g_grid_GridName;

    var myArray = [];
    checkboxes.each(function (index) {
        myArray[index] = $(this).val();
    });

    disableModalAllButton(true);
    
    var request = $.ajax({
        type: "POST",
        url: targetUrl,
        traditional: true,
        data: { checkedRecords: myArray }
    });

   
    request.done(function (data) {
        //The "Success" word seems to indicate success here
        $('.ui-dialog-buttonpane').show();
        if (data.toLowerCase() === "success") {
            $("#divEntry").dialog("close");
            _grid_refreshChildWindowGrid(gridName);
            _director_showSuccess('Success!');
        }
        else {
            $("#divEntry").dialog("close");
            var magicWord = "fetchErrorData";
            var errorUrl = '/Error/ErrorPage2?msg=' + magicWord;
            storeErrorData(data);
            _common_getWindowParent()._tab_launchNewTab(errorUrl, "error", "Error");
        }
    });

    request.fail(function (jqXHR, textStatus) {
        //var trimmedMsg = jqXHR.responseText.substring(0, 500).replace("<html>", "").replace("</html>", "");
        //alert("Request failed: " + textStatus + "; " + trimmedMsg);
        $("#divEntry").html(jqXHR.responseText);
        disableModalAllButton(false);
    });
}

/// <summary>
/// Store the error html for use by the ErrorPage2.cshtml. See fetchErrorData() below
/// </summary>
/// <example>
/// </example>
function storeErrorData(data) {
    if (data) {
        gErrorHtmlData = data;
    }
}

/// <summary>
/// Set the error html; ErrorPage2.cshtml page calls this function.
/// </summary>
/// <example>
/// </example>
function fetchErrorData() {
    var theWindow = getLastTabContentWindow();
    if (theWindow && getErrorHtmlData()) {
        var errorDiv = theWindow.$("#errorDiv");
        if (errorDiv) {
            errorDiv.html(getErrorHtmlData());
        }
    }
}

/*********************DATA ENTRY************************/



/******Dynamic HTML table from JSON results******/
function createTableView(objArray, enableHeader) {

    if (enableHeader === undefined) {
        enableHeader = true; //default enable headers
    }

    var array = objArray; //JSON.parse(objArray);

    var str = '<table >';

    // table head
    if (enableHeader) {
        str += '<thead><tr>';
        for (var thindex in array[0]) {
            str += '<th scope="col" class = "t-header">' + thindex.replace(/_/g, '&nbsp;') + '</th>';
        }
        str += '</tr></thead>';
    }

    // table body
    str += '<tbody>';
    for (var i = 0; i < array.length; i++) {
        str += (i % 2 === 0) ? '<tr class="alt">' : '<tr>';
        for (var index in array[i]) {            
            var dataResult = array[i][index];
            if (dataResult === null || dataResult === 0){
                dataResult = "";
            }
            str += '<td>' + dataResult + '</td>';
//          str += '<td>' + array[i][index] + '</td>';
        }
        str += '</tr>';
    }
    str += '</tbody>';
    str += '</table>';
    return str;
}

/*****************************Enable Disable*******************************/


function disableModalButton(button, disableButton) {
         if (disableButton)
           button.attr('disabled', disableButton).addClass('ui-state-disabled');
        else
           button.attr('disabled', disableButton).removeClass('ui-state-disabled');

}

function disableModalAllButton(disable) {
    //Handles double click
    if (disable===true)
        $('.ui-dialog-buttonpane button').attr('disabled', true).addClass('ui-state-disabled');
    else
        $('.ui-dialog-buttonpane button').attr('disabled', false).removeClass('ui-state-disabled');
}

function _layout_initHrControls() {

    //Comment today
    //_popup_getModalCancelButton().hide();
    //_popup_getModalSaveButton().hide();
 
    attachHrControlsEvent();
    //Comment today
    //_popup_enableDisableControls();
}

function attachHrControlsEvent() {
    $('form').submit(onFormSubmit);
    _datePicker_attachDatePickerEvent();
    attachHrDropdownListEvents();
}

function populateTargetDropdown(sourceddl, targetddl, myurl) {
    var url = _director_fixUrl(myurl);

    $("#" + sourceddl).change(function () {
        var ddltarget = "#" + targetddl;
        $.getJSON(url, { iD: $(this).val() }, function (data) {
            $(ddltarget).empty();
            $(ddltarget).append("<option value=''> </option>"); //add empty record
            $.each(data, function (index) {
                $(ddltarget).append("<option value='" + data[index].Value + "'>" + data[index].Text + "</option>");
            });

        });
    });
}


function getEditActionName() {
    var formAction = $('form').attr('action');
    if (formAction) {
        var aname = formAction.split('/');

        for (var i = 0; i < aname.length; i++) {
            if (aname[i].toUpperCase() === "EDIT" || aname[i].toUpperCase() === "EDITCONTRIBUTIONSCHEDULE") {
                return aname[i].toUpperCase();
            }
        }
    }
    
    return "";
}

//Area/Controller/Action
function getActionName(url) {
    //var formAction = $('form').attr("action");
    if (window._g_common_Virtualpath.length > 1) {
        url = url.replace(window._g_common_Virtualpath, '');     
    }           
    var splitUrl = url.split("/");
    if (splitUrl.length > 2) {
        return splitUrl[3];
    }
    return url;
}



/*****************************Enable Disable*******************************/

/*****************Cascading DropdownList************************/

function attachHrDropdownListEvents() {

    $(".ddlCascadingClass").change(
        function (evt) {
            evt.preventDefault();
            var url = $(this).attr("url");
            var id = $(this).val();
            if (id === "") id = 0;

            $.getJSON(url, { id: id },
                function (data) {
                    //key is the fieldName, value is the data (string,date or json)
                    $.each(data, function (key, value) {
                        var element = $("#" + key);
                        if (value.type === "list") {
                            populateDropDownList(element, value.data, true);
                        }
                        else {
                            element.val(value.data);
                        }
                    });
                });
        });
}


function populateDropDownList(dropdownList, data, addEmptyRecord) {
    dropdownList.empty();
    if (addEmptyRecord) {
        dropdownList.append("<option value=''> </option>"); //add empty record
    }
    
    $.each(data, function (index) {
        dropdownList.append("<option value='" + data[index].Value + "'>" + data[index].Text + "</option>");
    });
}


/*****************Cascading DropdownList************************/

/*****************Init  Label Text************************/

function _layout_initLabelTextControls() {
    var data = getDataDictionaryData();
    //debugger;
    if (data) {
        $.each(data, function (index) {
            var fieldName = data[index].FieldName;
            var fieldDisplayText = data[index].FieldDisplayText;
            var labelElement;
            labelElement = "label[for='" + fieldName.toString() + "']";
            //labelElement = ".editor-label label[for='" + fieldName.toString() + "']";
            $(labelElement).text(fieldDisplayText);
        });      
    }
}

function getDataDictionaryByFieldName(field) {
    var data = getDataDictionaryData();
    if (data) {
        $.each(data, function (index) {
            var fieldName = data[index].FieldName;
            if (fieldName === field) {
                return data[index];
            }
        });
    }
    return null;
}

function _initDataDictionaryData() {
    //debugger;
    var formEntity = getFormEntity();
    var url = _director_fixUrl(_g_common_Virtualpath + "/Common/DataDictionary/GetDataDictionary");
    $.getJSON(url, { entityName: formEntity },
          function (data) {
              gDataDictionaryData = data;
              _layout_initLabelTextControls();
              
              onGlobalNotification(GlobalStatus.CompletedInitialiedControl);
          }
     );
}

function _initKendoControls(dialog) {
    //numeric textbox
    dialog.find('input[controltype=kendoNumericTextBox]').each(function () {
        var minValue = $(this).attr('min');
        var decimalsValue = $(this).attr('decimals');
        $(this).kendoNumericTextBox({ min: minValue, decimals: decimalsValue });
    });
    //number only textbox
    dialog.find('input[controltype=kendoNumericTextBoxNumberOnly]').each(function () {
        var minValue = $(this).attr('min');
        var formatValue = $(this).attr('format');
        var decimalsValue = $(this).attr('decimals');
        var typeValue = $(this).attr('type');
        $(this).kendoNumericTextBox({ min: minValue, format: formatValue, decimals: decimalsValue, type: typeValue });
    });
    dialog.find('input[controltype=kendoAutoComplete]').each(function() {
        $(this).capitalizeFirstLetterWord();
    });
    dialog.find('input[controltype=kendoAutoCompleteTextBox]').kendoAutoComplete();
    
    dialog.find('input[controltype=kendoAutoComplete]').kendoAutoComplete();
    
    dialog.find('input[controltype=kendoAutoCompleteReadOnly]').kendoAutoComplete();

    dialog.find('input[controltype=kendoDatePicker]').each(function () {
        var dateformat = $(this).attr("dateFormat");
        $(this).kendoDatePicker({ format: dateformat });
    });

    dialog.find('input[controltype=kendoDatePickerReadOnly]').each(function () {
        var dateformat = $(this).attr("dateFormat");
        $(this).kendoDatePicker({ format: dateformat });
    });

    dialog.find('textarea[controltype=kendoEditor]').kendoEditor({
        tools: ["bold",
                "italic",
                "underline",
                "strikethrough",
                "fontName",
                "fontSize",
                "foreColor",
                "backColor",
                "justifyLeft",
                "justifyCenter",
                "justifyRight",
                "justifyFull",
                "insertUnorderedList",
                "insertOrderedList",
                "indent",
                "outdent",
                "formatBlock",
                "createLink",
                "unlink",
                "insertImage",
                "subscript",
                "superscript",
                "insertHtml"],
        insertHtml: [
            { text: "Text Box", value: "<input value = '' type='text'/>" },
            { text: "Check Box", value: "<input type='checkbox'/>" }
        ]
    });

    _initKendoComboBox(dialog);
    _initKendoLinkedForControl(dialog);
}




