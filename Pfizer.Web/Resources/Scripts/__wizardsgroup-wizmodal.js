/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-module-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-general-controls.js" />

var modalpopup = "";
var _g_popup_modal = "";

$(function () {
    _g_popup_modal = "#divEntry";
    modalpopup = $(_g_popup_modal);
});

function isEmpty(obj) {
    if (typeof obj == 'undefined' || obj == null || obj == '') {
        return true;
    }
    return false;
}

function _popup_modal() {
    var dialog = modalpopup;

    var modal = {
        _setting: {
            dialog: { title: null, width: null, height: null, buttons: [] }
        },
        dialog: function (title, width, height) {
            modal._setting.dialog.title = title;
            modal._setting.dialog.width = width;
            modal._setting.dialog.height = height;
            return modal._buttons;
        },

        _open: {
            open: function (url, paramdata, call_back) {
                dialog = modal._popup.dialog(dialog);
                modal._load.load(url, paramdata, call_back);
                dialog.dialog('open');
            }
        },

        _load: {
            load: function (url, paramdata, call_back) {
                dialog.empty();
                var request = $.ajax({
                    type: "GET",
                    url: _director_fixUrl(_g_common_Virtualpath + url),
                    datatype: "html",
                    data: paramdata
                });

                request.done(function (data) {
                    modal.event.load(data);
                    modal._html.html(data, call_back);
                });

                request.fail(function (jqXHR, textStatus) {
                    dialog.html(jqXHR.responseText);
                });
            }
        },

        _html: {
            html: function (data, call_back) {
                dialog.html(data);
                modal._callback(call_back, data);
                //$(_g_popup_modal + ' form').submit(modal._submit.submit);
                if (!$(_g_popup_modal + ' .k-input').length > 0) {
                    _initKendoControls($(_g_popup_modal));
                }
            }
        },

        _submit: {
            submit: function (url, data, call_back) {
                url = isEmpty(url) ? $('#divEntry form').attr('action') : url;
                data = isEmpty(data) ? $('#divEntry form').serialize() : data;

                var request = $.ajax({
                    type: "POST",
                    url: _director_fixUrl(_g_common_Virtualpath + url),
                    datatype: "json",
                    data: data
                });
                request.done(function (data) {
                    if (data.hasOwnProperty('ActionStatus')) {
                        if (data.ActionStatus == 'Success') {
                            modal._result.success(data);
                        } else if (data.ActionStatus == 'Save') {
                            modal._result.save(data);
                        } else if (data.ActionStatus == 'Warning') {
                            modal._result.warning(data);
                        } else if (data.ActionStatus == 'Failed') {
                            modal._result.failed(data);
                        } else {

                        }
                    } else {
                        modal._html.html(data);
                    }
                    modal.event.submit(data);
                    modal._callback(call_back, data);
                });
            }
        },

        _buttons: {
            buttons: function (array) {
                for (var i = 0; i < array.length; i++) {
                    if (array[i] == 'Cancel') {
                        modal._setting.dialog.buttons.push({
                            text: "Cancel",
                            click: function (e) { e.preventDefault(); modal.close(true); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Create') {
                        modal._setting.dialog.buttons.push({
                            text: "Create",
                            click: function (e) { e.preventDefault(); modal._submit.submit(); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Submit') {
                        modal._setting.dialog.buttons.push({
                            text: "Submit",
                            click: function (e) { e.preventDefault(); modal._submit.submit(); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Save') {
                        modal._setting.dialog.buttons.push({
                            text: 'Save',
                            click: function (e) { e.preventDefault(); modal.event.onsubmit('Save'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Continue') {
                        modal._setting.dialog.buttons.push({
                            text: 'Continue',
                            click: function (e) { e.preventDefault(); modal.event.onsubmit('Continue'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Finish') {
                        modal._setting.dialog.buttons.push({
                            text: 'Finish',
                            click: function (e) { e.preventDefault(); modal.event.onsubmit('Finish'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Add Dependent') {
                        modal._setting.dialog.buttons.push({
                            text: 'Add Dependent',
                            click: function (e) { e.preventDefault(); modal.event.onload('Add Dependent'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Back') {
                        modal._setting.dialog.buttons.push({
                            text: 'Back',
                            click: function (e) { e.preventDefault(); modal.event.onload('Back'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Accept') {
                        modal._setting.dialog.buttons.push({
                            text: 'Accept',
                            click: function (e) { e.preventDefault(); modal._submit.submit('Accept'); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    } else if (array[i] == 'Decline') {
                        modal._setting.dialog.buttons.push({
                            text: 'Decline',
                            click: function (e) { e.preventDefault(); modal.close(false); },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        });
                    }
                };
                return modal._open;
            }
        },

        close: function (bool_confirm, call_back) {
            if (confirm) {
                modal.confirm('Are you sure you want to exit without saving?', function () {
                    dialog.dialog('close');
                });
            } else {
                dialog.dialog('close');
            }
        },

        event: {
            onclick: function (action) { },
            click: function (action) { },
            onload: function () { },
            load: function (data) { },
            onsubmit: function (action) {
                modal._submit.submit();
            },
            submit: function (data) { },
            close: function () { }
        },

        _result: {
            success: function (data) {
                _common_resetValidations();
                _director_showSuccess(data.Messages);
                if (dialog.hasOwnProperty('dialog')) {
                    dialog.dialog('close');
                }
            },
            save: function (data) {
                _common_resetValidations();
                _director_showSuccess(data.Messages);
                _layout_initLabelTextControls();
                if (data.hasOwnProperty('MemberId')) {
                    $('#MemberId').val(data.MemberId);
                }
                if (data.hasOwnProperty('Id')) {
                    $('.pKey').val(data.Id);
                }
                $('.validation-summary-valid').html('');
            },
            warning: function (data) {
                _director_showWarning(data.Messages);
                if (dialog.hasOwnProperty('dialog')) {
                    dialog.dialog('close');
                }
            },
            failed: function (data) {
                _director_showError(data.Messages);
                if (dialog.hasOwnProperty('dialog')) {
                    dialog.dialog('close');
                }
            },
        },

        _callback: function (call_back, param) {
            if (typeof call_back == 'function') {
                call_back(param);
            }
        },

        _popup: {
            dialog: function (dialog) {
                dialog.dialog({
                    title: modal._setting.dialog.title,
                    width: modal._setting.dialog.width,
                    height: modal._setting.dialog.height,
                    autoOpen: false, draggable: true, modal: true, show: 'drop', hide: { effect: 'drop', direction: 'right' },
                    buttons: modal._setting.dialog.buttons,
                    close: function (event) {
                        modal.event.close();
                        $('#divEntry').dialog("destroy");
                    },
                    position: 'center',
                    open: function (event) {
                        $('.ui-widget-overlay').css("height", "150%");
                    }
                });
                return dialog;
            },
            confirm: function (message, call_back, title, width, height) {
                title = isEmpty(title) ? "Confirm Message" : title;
                width = isEmpty(width) ? 400 : width;
                height = isEmpty(height) ? 220 : height;
                var confirm = $('<div id="dialog-confirm" title="' + title + '"><p>' + message + '</p></div>');
                confirm.dialog({
                    width: width, height: height, autoOpen: true, draggable: false, modal: true, resizable: false, hide: { effect: 'drop', direction: 'right' },
                    buttons: [
                        {
                            text: "Yes",
                            click: function () {
                                $(this).dialog('close');
                                modal._callback(call_back);
                            },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        },
                        {
                            text: "No",
                            click: function () {
                                $(this).dialog('close');
                            },
                            class: "btn btn-info",
                            style: "display: inline-block;"
                        }
                    ],
                    close: function (event) {
                        $(this).dialog("destroy");
                    },
                    open: function (event) {
                        $('.ui-widget-overlay').css("height", "150%");
                    }
                });
            }
        },

        confirm: function (message, call_back, title, width, height) {
            modal._popup.confirm(message, call_back, title, width, height);
        }
    };

    return modal;
}