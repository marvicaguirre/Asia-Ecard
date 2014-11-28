/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-grid.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />



//for DELETE button
function _delete_attachDeleteButtonEventHandler(divPanel) {
	divPanel.find(".buttonDeleteClass").each(function () {
		$(this)
			//.attr("style", getButtonStyle())
			//.button({ icons: { primary: 'ui-icon-trash' } })
			.click(function (evt) {
				launchDeleteModalConfirm($(this), evt);
			});
	});

	//setButtonStyle(divPanel.find(".buttonDeleteClass"), 'ui-icon-trash');
	//RESTORE!!!
	//setButtonStyle(".buttonDeleteClass", 'ui-icon-trash');
	//setButtonStyle(".buttonDeleteClass");
	setButtonStyle(divPanel.find(".buttonDeleteClass"));
}

function launchDeleteModalConfirm(element, evt) {
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

	_common_getWindowParent().launchJqueryModalConfirm(buttonText, url, targetUrl, modalTitle, modalwidth, modalheight, selectedRecords, evt, gridname);
}

function _delete_getSelectedRecords(buttonText, url, targetUrl, gridname, title, width, height, evt) {
	var divId = '#' + gridname + ' input[type=checkbox][name=checkedRecords]:checked';
	var selectedRecords = $(divId);

	if (selectedRecords.length < 1) {
		_director_showWarning('No record(s) has been selected');
		return;
	}
	//Caller is coming form _modalLayou.chtml
	_common_getWindowParent().launchJqueryModalConfirm(buttonText, url, targetUrl, title, width, height, selectedRecords, evt, gridname);
}

//for DELETE CONFIRMATION 
function launchJqueryModalConfirm(buttonText, url, targetUrl, title, width, height, data, evt, gridname) {
	var dialogConfirm = $("<div></div>");

	evt.preventDefault();

	//var primaryKeys = [];
	//data.each(function (index) {
	//	primaryKeys[index] = $(this).attr('primaryid');
	//});

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
		buttons[buttonText] = function () { _common_invokeAction(evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
		//buttons["Yes"] = function () { _common_invokeAction(evt, dialogConfirm, targetUrl, primaryKeys, gridname); };
		buttons.Cancel = function () { $('.ui-dialog-content').dialog('destroy').remove(); };

		//gDialogEntry.empty();
		dialogConfirm
			 .html(tableKendo);

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
				//$(this).parent().find('.ui-dialog-buttonpane button:contains("Delete")').button({
				//    icons: { primary: 'ui-icon-trash' }
				//});
				//$(this).parent().find('.ui-dialog-buttonpane button:contains("Cancel")').button({
				//    icons: { primary: 'ui-icon-cancel' }
				//});

				setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Delete")'), 'ui-icon-trash');
				setButtonStyle($(this).parent().find('.ui-dialog-buttonpane button:contains("Cancel")'), 'ui-icon-cancel');

				//setButtonStyle('.ui-dialog-buttonpane');

				//$(this).closest('.ui-dialog').find('.ui-dialog-titlebar-close').hide();
			}
		});

		//assign button style after creating them!
		//setButtonStyle('.ui-dialog-buttonpane button');
		//setButtonStyle('.ui-dialog-buttonpane button:contains("Delete")', 'ui-icon-trash');
		//setButtonStyle('.ui-dialog-buttonpane button:contains("Cancel")', 'ui-icon-cancel');

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