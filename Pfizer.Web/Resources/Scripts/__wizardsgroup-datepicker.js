/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />

//var dateTimePickerSelector = '.ui-datepicker-trigger';  
var dateTimePickerContainerSelector = 'span.k-select';
//we hide the calendar and the clock
var dateTimePickerSelectorForDisabling = 'span.k-select span.k-i-calendar,span.k-i-clock';
//we show only the calendar
var dateTimePickerSelectorForEnabling = 'span.k-select span.k-i-calendar';


function getCalendarGif() {
    return _director_fixUrl(_g_common_Virtualpath + '/Content/images/calendar.gif');
}

function _datePicker_attachDatePickerEvent() {
    var imgsrc = getCalendarGif();
    $(":input[data-datepicker]").datepicker({
        dateFormat: 'mm/dd/yy',
        duration: 0,
        showOn: 'button',
        buttonImage: imgsrc,
        buttonImageOnly: true
    });
}


function hideDateTimePicker() {
    showHideDateTimePicker(false);
}

function _datePicker_showDateTimePicker() {
    showHideDateTimePicker(true);
}

function showHideDateTimePicker(show) {
    var showAttr = show ? 'visible' : 'hidden';
    //alert(show);
    if (show) {
        $(dateTimePickerSelectorForEnabling).each(function () {
            $(this).attr('style', 'visibility:' + showAttr);
        });
    }
    else {
        $(dateTimePickerSelectorForDisabling).each(function () {
            $(this).attr('style', 'visibility:' + showAttr);
        });
    }

    //var attr2 = show ? 'visible' : '';
    //$(dateTimePickerSelector).attr('style', 'visibility:' + attr2);
}
