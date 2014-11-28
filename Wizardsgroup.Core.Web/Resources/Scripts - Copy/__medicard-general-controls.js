/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />

var stateEnabledKey = 'STATEENABLED';

//function getChildrenOfElementExcludingButtons(selector) {
//    var children = $(selector).not(':submit,:button');
//    return children;
//}

function _general_controls_enableControlsExcludingButtons(selector) {
    //alert('hrwizard-general-controls; _general_controls_enableControlsExcludingButtons(); caller is ' + arguments.callee.caller.name);
    $(selector).not(':submit,:button').each(function () {
        $(this).attr('disabled', false);
    });
    //$($(getChildrenOfElementExcludingButtons())).each(function () {
    //    $(this).attr('disabled', false);
    //});
}

function enableDisableControl(element) {
    //alert('hrwizard-general-controls; enableDisableControl(); caller is ' + arguments.callee.caller.name);
    $(element).not(':submit,:button').each(function () {
        //alert($(this).html());
        //alert($(this).data(stateEnabledKey));
        if ($(this).data(stateEnabledKey) === undefined
            || $(this).data(stateEnabledKey) === true) {
            if (hasAttribute(this, 'disabled')) {
                $(this).removeAttr('disabled');
            }
            $(this).attr('disabled', true);
            $(this).data(stateEnabledKey, false);
        }
        else {
            $(this).removeAttr('disabled');
            $(this).data(stateEnabledKey, true);
        }
    });
}

//enable/disable control
//function enableControl(controlId, state) {
//    if (!state) {
//        if (hasAttribute(controlId, 'disabled'))
//            $(controlId).attr('disabled', true);
//    }
//    else {
//        $(controlId).removeAttr('disabled');
//    }
//}