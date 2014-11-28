/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-general-controls.js" />

var gDialogEntry = "";
var _g_popup_DialogEntryNameSelector = "";

$(function () {

	_g_popup_DialogEntryNameSelector = "#divEntry";
	gDialogEntry = $(_g_popup_DialogEntryNameSelector);

});


function getDialogButton(buttonName) {
	return $(".ui-dialog-buttonpane button:contains('" + buttonName + "')");
}

function _popup_getModalCancelButton() {
	return getDialogButton('Cancel');
}

function _popup_getModalSaveButton() {
	return getDialogButton('Save');
}

function _popup_getModalModifyButton() {
	return getDialogButton('Save');
}

function getModalCreateButton() {
	return getDialogButton('Save');
}

/*******************JQUERY MODAL POPUP****************/
function _popup_launchJqueryEntry(url, actionName, title, width, height, paramdata, evt, gridname, autoClose) {
	_director_setGlobalVariable(paramdata, gridname, url);

	//gDialogEntry.attr("targetgrid", gridname);

	evt.preventDefault();
	//var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');

	var dialog = gDialogEntry;
	dialog.attr("targetgrid", gridname);

	dialog.empty();

	dialog.attr("autoClose", autoClose);

	//$(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
	//1. call the page using ajax
	var request = $.ajax({
		type: "GET",
		url: url,
		data: paramdata
	});

	//2a. assign event handler for successful call
	request.done(function (data) {
		//4. called if successful operation
		createButtons(dialog, actionName);
		dialog.html(data);
		//jQuery.data($('#divEntryData')[0], "ORIGINAL_DATA_KEY", dialog.html());

		$("#divEntryData").data("ORIGINAL_DATA_KEY", { content: dialog.html() });
		//var x = $("#divEntryData").data("ORIGINAL_DATA_KEY").content;
		//alert(x);

        //comment today
		_initDataDictionaryData();
		_layout_initHrControls();
		_initKendoControls(dialog);
	});

	//2b. assign event handler for failed call
	request.fail(function (jqXHR, textStatus) {
		//5b. called if failed operation
		//var trimmedMsg = jqXHR.responseText.substring(0, 500).replace("<html>", "").replace("</html>", "");
		//alert("Request failed: " + textStatus + "; " + trimmedMsg);
		gDialogEntry.html(jqXHR.responseText);
	});

	//3. initialize the popup window and open it; see 4 above (they should be called afterwards;
	_popup_openDialog(dialog, title, width, height);
}

//Create buttons inside the popup window
function createButtons(dialog, actionName) {
	var buttonArray = {};
	if (actionName === "Create") {
		var autoClose = gDialogEntry.attr("autoClose");
		if (autoClose === "False") {
		    buttonArray.Save = function () { $('form').submit(); };
		    buttonArray.Cancel = function () { closeModalDialog(); };
			dialog.dialog({ buttons: buttonArray });

			//replace Create text with Create and Continue;
			var createAndContinue = $("span.ui-button-text");
			createAndContinue.text("Create and Continue");
		}
		else {
		    buttonArray.Create = function () { $('form').submit(); };
		    buttonArray.Cancel = function () { closeModalDialog(); };
			dialog.dialog({ buttons: buttonArray });
		}
	}
	if (actionName === "Edit") {
		buttonArray.Save = function () { $('form').submit(); };
		//buttonArray.Modify = function () { modifyEntry(); };	    
		buttonArray.Cancel = function () { closeModalDialog(); };
		dialog.dialog({
			buttons: buttonArray
			, open: function (event) {
			    //setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Modify")'), 'ui-icon-trash');
			    setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Delete")'), 'ui-icon-trash');
				setButtonStyle($(this).parent().find(' button:contains("Cancel")'), 'ui-icon-cancel');
			}
		});

		//_popup_getModalCancelButton().hide();
		//_popup_getModalSaveButton().hide();
	}
	 
	if (actionName === "Upload") {
		buttonArray.Close = function () { dialog.dialog('close'); };
		dialog.dialog({ buttons: buttonArray });
		$(".ui-dialog-titlebar-close").hide();
	}

	//assign the style of all buttons after creating them!
	//CSS: all elements with ui-button class which are children of ui-dialog class
	//setButtonStyle(".ui-dialog .ui-button", null, 'popup-createButtons');
	setButtonStyle(".ui-dialog-buttonset button");
}

function _popup_enableDisableControls() {
	var actionName = getEditActionName();
	//if (actionName === "EDIT") {
	//{
	//	enableDisableControl(editorFieldElementSelector);
	//	hideDateTimePicker();
	//}
	return false;
}

function _popup_initDialogProperties(dialog, title, width, height) {

    if (width == 0 && height == 0) {
        width = $(window).width() * 0.6;
        height = $(window).height() * 0.8;
    }

	dialog.dialog({
		autoOpen: false
			, draggable: true
			, title: title
			, width: width
			, modal: true
			, height: height
			, minHeight: 200
			, show: 'drop'
			, hide: { effect: 'drop', direction: 'right' }
		    , close: $(this).dialog("destroy").remove()
			, position: 'center'
	});
}

function launchJqueryModalDynamicGrid(url, title, width, height, data, evt, gridname) {
	$(_g_popup_DialogEntryNameSelector).dialog("destroy");
	_director_setGlobalVariable(data, gridname, url);

	evt.preventDefault();

	var request = $.ajax({
		type: "GET",
		url: url,
		traditional: true,
		data: { id: data }
	});

	request.done(function (callbackdata) {
		//Dynamically create html table on Json data
		var htmlTable = createTableView(callbackdata, true);

		var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');
		var dialog = $(_g_popup_DialogEntryNameSelector);

		var buttonArray = {};
		buttonArray.Close = function () { dialog.dialog('close'); };

		dialog.empty();
		dialog
			.append(loading)
			.html(htmlTable);

		_popup_openDialog(dialog, title, width, height, null, buttonArray);
	});

	request.fail(function (jqXHR, textStatus) {
		alert("Request failed: " + textStatus);
	});
}

//function disableControls() {
//    alert('_medicard-layout; disableControls()');
//    $(editorFieldElementSelector).not(':submit,:button').each(function () {
//        $(this).attr('disabled', true);
//    });
//}

//opwn the dialog with the optional buttons and data
function _popup_openDialog(dialog, title, width, height, data, buttons) {
	_popup_initDialogProperties(dialog, title, width, height);

	if(buttons) {
		dialog.dialog({ buttons: buttons });
	}

	dialog.dialog('open');
	
	if (data) {
		dialog.html(data);
	}
}

function launchJqueryModalConfirm_Old(buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
	_director_setGlobalVariable(data, gridname, url);

	evt.preventDefault();

	var myArray = [];
	data.each(function (index) {
		myArray[index] = $(this).val();
	});

	var request = $.ajax({
		type: "GET",
		url: url,
		traditional: true,
		data: { checkedRecords: myArray }
	});


	request.done(function (callbackdata) {
		//Dynamically create html table on Json data
		var htmlTable = createTableView(callbackdata, true);

		var loading = $('<img src="../Content/themes/pepper-grinder/images/ajaxLoader.gif" alt="loading" class="ui-loading-icon">');
		var dialog = $(_g_popup_DialogEntryNameSelector);

		var buttonArray = {};
		buttonArray[buttonText] = function () { hrPostAction(targetUrl); };
		buttonArray.Cancel = function () { dialog.dialog('close'); };

		dialog.empty();
		dialog
			.append(loading)
			.html(htmlTable);

		_popup_openDialog(dialog, title, width, height, null, buttonArray);
	});

	request.fail(function (jqXHR, textStatus) {
		alert("Request failed: " + textStatus);
	});
}

function closeModalDialog() {
	var dialog = $(_g_popup_DialogEntryNameSelector);
	dialog.dialog('close');
}

/*******************JQUERY MODAL POPUP****************/

/*********************DATA ENTRY************************/
function onFormSubmit(e) {
	e.preventDefault();
	var request = $.ajax({
		type: "POST",
		url: $(this).attr("action"),
		datatype: "json",
		data: $(this).serialize()
	});

	var targetContainerDiv = $(_g_popup_DialogEntryNameSelector); 

	//finished submission of form:
	request.done(function (data) {
	    debugger;
	    if (data.ActionStatus === "Success") {
			var targetGrid = gDialogEntry.attr("targetgrid");
	        //_common_getWindowParent().
	        _common_resetValidations();
			_grid_refreshChildWindowGrid(targetGrid);
			gDialogEntry.dialog("close");
			_director_showSuccess(data.Messages);
		}
		else {
	        //failed to ADD or UPDATE record;
	        //comment today
	        //restoreContent(targetContainerDiv);
	        targetContainerDiv.html(data);
            //comment today
			//_director_showError('Error!');
			_layout_initLabelTextControls();
			attachHrControlsEvent();
		}
	});

	request.fail(function (jqXHR, textStatus) {
		//_director_showError(textStatus);
		targetContainerDiv.html(jqXHR.responseText);
		_layout_initLabelTextControls();
		attachHrControlsEvent();
	});

	return false;
}

function restoreContent(targetContainerDiv) {
	var data = $("#divEntryData").data("ORIGINAL_DATA_KEY");
	if (data) {
		var content = data.content;
		targetContainerDiv.html(content);
		
		//var htmlData = $("#divEntryData").data("ORIGINAL_DATA_KEY").content;
		//targetContainerDiv.html(htmlData);
	}
}


function modifyEntry() {
	_popup_enableAllControls();
	_popup_getModalCancelButton().show();
	_popup_getModalSaveButton().show();
	_popup_getModalModifyButton().hide();
	//_popup_getModalModifyButton().bind('click', customModifyEvent);
}

function cancelEntry() {
	//var data = jQuery.data($('#divEntryData')[0], "ORIGINAL_DATA_KEY");
	var data = $("#divEntryData").data("ORIGINAL_DATA_KEY");
	if (data) {
		var content = data.content;
		$(_g_popup_DialogEntryNameSelector).html(content);
	}

	_layout_initHrControls();
	_layout_initLabelTextControls();
	_popup_getModalModifyButton().show();
}


//var editorFieldElementSelector = '.editor-field input,select,textarea,img';
//var editorFieldElementSelector = '.controls .k-datetimepicker span.k-picker-wrap span.k-select span.k-i-calendar';
//var editorFieldElementSelector = '.controls .k-datetimepicker span.k-picker-wrap';

//==============================================================
//Note: there should be a DIV whose class is called 'controls' (see Edit.cshtml of Client); this is normally a class from the Bootstrap CSS Framework
//==============================================================
//var editorFieldElementSelector = '.controls input,select,textarea,img';
var editorFieldElementSelector = '.control-group input,select,textarea,img';

function _popup_enableAllControls() {
	//alert('hrwizard-popup; _popup_enableAllControls()');
	//$(editorFieldElementSelector).not(':submit,:button').each(function () {
	//    $(this).attr('disabled', false);
	//});
	_general_controls_enableControlsExcludingButtons(editorFieldElementSelector);

	_datePicker_attachDatePickerEvent();
	_datePicker_showDateTimePicker();
}