var gcType = "";
var gcArgs = "";
var gTargetCombobox = "";
var gComboboxIdOnChange = "";


////Triggered only on change
function onKendoComboBoxChange(e) {
    //debugger;
    gComboboxIdOnChange = $(e.sender.element).attr("id");
}
//Cascade to other elements other than the combobox
function customCascadeFromComboBox(url, id) {
    $.getJSON(url, { id: id },
    function (data) {
        //key is the field name, value is the data (string, date or json)
        $.each(data, function (key, value) {
            var element = $("#" + key);
            element.val(value.data);
        });
    });
}

function attachChildCustomComboBoxChangeEvent(dialog, comboboxIdOnChange) {
    //debugger;
    //updated by Carl 2013-12-19 
    var cascadeToIds = dialog.find('#' + comboboxIdOnChange).attr("cascadeTo");
    while (cascadeToIds != '') {
        cascadeToIds = clearChildCustomComboBoxOnChange(dialog, cascadeToIds.split(';'));
    }
}

function clearChildCustomComboBoxOnChange(dialog, cascadeToId) {
    var ids = '';
    var cascadeToIdLength = cascadeToId.length;

    for (var i = 0; i < cascadeToIdLength; ++i) {
        if (cascadeToId[i] == '' | cascadeToId[i] == 'undefined')
            continue;

        var comboboxChild = dialog.find('#' + cascadeToId[i]).data("kendoComboBox");
        if (comboboxChild != undefined) {
            comboboxChild.value("");
            comboboxChild.dataSource.read();
            ids = ids.concat(dialog.find('#' + cascadeToId[i]).attr("cascadeTo"), ';');
        }
    }
    return ids;
}

function attachChildCustomComboBoxDataboundEvent(dialog, comboboxIdOnChange) {
    //debugger;
    var cascadeToIds = dialog.find('#' + comboboxIdOnChange).attr("cascadeTo");
    if (cascadeToIds != undefined) {
        var cascadeToId = cascadeToIds.split(';');
        var cascadeToIdLength = cascadeToId.length;

        for (var i = 0; i < cascadeToIdLength; ++i) {
            var comboboxChild = dialog.find('#' + cascadeToId[i]).data("kendoComboBox");
            if (comboboxChild != undefined) {
                comboboxChild.dataSource.read();
            }
        }
    }
}

function addChildIdToParent(dialog, cascadeFrom, thisId) {
    var parentCombo = dialog.find('#' + cascadeFrom);
    var currentAttrValue = parentCombo.attr('cascadeTo');
    var newAttrValue = currentAttrValue != undefined ? currentAttrValue + ';' + thisId : thisId;
    parentCombo.attr('cascadeTo', newAttrValue);
}

//This should fix the issue that gets triggered when clicking a combobox and not selecting a
//record then the page focuses on the 1st combobox inside the modal popup.
function hackComboBoxFocus(elem) {
    $(elem).parent().removeClass('k-state-focused');
    $(elem).on('blur', function () {
        $(this).parent().removeClass('k-state-focused');
        $(this).focus().off("blur").on('blur', function () {
            hackComboBoxFocus($(this));
        });
    });
}

function _initKendoComboBox(dialog) {

    //----------------------[(for kendoAutoCompleteForComboBox)]----------------------------------
    //Used by comboxbox for debug data
    dialog.find("input[controltype=kendoAutoCompleteForComboBox][serverFiltering=False]").each(function () {
        //debugger;
        $(this).kendoAutoComplete();
        $(this).keyup(function (e) {
            //debugger;
            if (e.keyCode == 13) {
                var targetid = $(this).attr('targetid');
                var targetcombobox = dialog.find("#" + targetid).data("kendoComboBox");
                targetcombobox.value($(this).val());
                targetcombobox.trigger("change");
            }
        });
    });
    //------------------END: [(for kendoAutoCompleteForComboBox)]--------------------------------------
    //------------------BEGIN: [(for kendoDropDownList)]--------------------------------------------------
    dialog.find("input[controltype=kendoDropDownList][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        //var serverFiltering = $(this).attr('serverFiltering');
        //var self = $(this);
        $(this).kendoDropDownList({
            index: -1,
            autoBind: true,
            serverFiltering: false,
            placeholder: placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: { read: url }
            },
            change: onKendoComboBoxChange
        });

        $(this).closest('.k-dropdown').find('span').on("blur", function (e) {
            $(this).focus().off("blur");
        });
    });
    //------------------END: [(for kendoDropDownList)]----------------------------------------------------

    //----------------------[(for CustomComboBox)]----------------------------------
    dialog.find("input[controltype=kendoComboBox][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var thisId = $(this).attr('id');
        var key = $(this).attr('key');
        var value = $(this).val();
        $(this).val('');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service + '&key=' + key);
        var placeHolderText = $(this).attr('placeHolder');
        $(this).kendoComboBox({
            index: -1,
            autoBind: false,
            minLength: 2,
            placeholder: placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: { read: url },
                serverFiltering: true,
            },
            change: onKendoComboBoxChange
        });
        //$(this).val(value);
        $(this).data("kendoComboBox").value(value);
        $(this).data("kendoComboBox").trigger("change");
        var elemComboBox = $(thisId).closest('.k-combobox').find('span').find('input');
        hackComboBoxFocus(elemComboBox);
    });
    //------------------END: [(for CustomComboBox)]--------------------------------------


    //----------------------[(for CustomReadonlyComboBox)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxReadOnly][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');

        $(this).kendoComboBox({
            index: -1,
            placeholder: placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            enable: false,
            dataSource: {
                type: "json",
                transport: { read: url }
            },
            change: onKendoComboBoxChange
        });
    });

    //------------------END: [(for CustomReadonlyComboBox)]--------------------------------------

    //------------------BEGIN: (CustomComboBoxForCustomData)----------------------------------------
    dialog.find("input[controltype=kendoComboBoxForCustomData]").each(function () {
        //debugger;
        var url = $(this).attr('url');
        var placeHolderText = $(this).attr('placeHolder');
        var parameter = $(this).attr('parameter');

        $(this).kendoComboBox({
            index: -1,
            autoBind: true,
            serverFiltering: false,
            placeholder: placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id            
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    parameterMap: function (options, operation) {
                        if (operation == "read") {
                            return { args: composeJsonParameter(parameter) };
                        }
                        return options;
                    }
                }
            },
            change: onKendoComboBoxChange
        });
    });
    //------------------END: (CustomComboBoxForCustomData)----------------------------------------

    //------------------BEGIN: [(for CustomComboBoxWithCascade)]------------------------------------------
    dialog.find("input[controltype=kendoComboBoxWithCascade][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        //var serverFiltering = $(this).attr('serverFiltering');
        var urlCascade = $(this).attr('urlCascade');

        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: { read: url }
            },
            select: function (e) {
                //debugger;                
                var dataItem = this.dataItem(e.item.index());
                customCascadeFromComboBox(urlCascade, dataItem.value);
            },
            change: onKendoComboBoxChange
        });
    });

    //------------------END: [(for CustomComboBoxWithCascade)]--------------------------------------------

    //------------------BEGIN: [(for CustomComboBoxLinked)]-----------------------------------------------
    dialog.find("input[controltype=kendoComboBoxLinked][serverFiltering=False]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');
        var key = $(this).attr('key');
        var comboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service + '&key=' + key);
        var placeHolderText = $(this).attr('placeHolder');

        //var value = $(this).val();
        //$(this).val('');     
        $(this).kendoComboBox({
            index: -1,
            placeholder: placeHolderText,
            autoBind: false,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    //serverFiltering: true,
                    parameterMap: function (options) {
                        //debugger;
                        return {
                            cascadeFromValue: dialog.find('#' + comboboxIdOnChange).val()
                        };
                    }
                }
            },
            change: onKendoComboBoxChange
        });
        //$(this).data("kendoComboBox").value(null);
        
        //$(this).val(value);
        var elemComboBox = $(thisId).closest('.k-combobox').find('span').find('input');
        hackComboBoxFocus(elemComboBox);

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        dialog.find('#' + cascadeFrom).data("kendoComboBox").unbind("change", changeCallback);
        dialog.find('#' + cascadeFrom).data("kendoComboBox").bind("change", changeCallback);
        function changeCallback() {
            attachChildCustomComboBoxChangeEvent(dialog, cascadeFrom);
        }

        ////Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
        dialog.find('#' + cascadeFrom).data("kendoComboBox").unbind("dataBound", databoundCallback);
        dialog.find('#' + cascadeFrom).data("kendoComboBox").bind("dataBound", databoundCallback);
        function databoundCallback() {
            attachChildCustomComboBoxDataboundEvent(dialog, cascadeFrom);
        };
    });
    //------------------END: [(for CustomComboBoxLinked)]--------------------------------------

    //------------------BEGIN: [(for CustomComboBoxLinkedWithCascade)]------------------------------------
    dialog.find("input[controltype=kendoComboBoxLinkedWithCascade][serverFiltering=False]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');
        var urlCascade = $(this).attr('urlCascade');
        var key = $(this).attr('key');
        //Note: initialize the global gComboboxIdOnChange variable to store the parent combobox;
        var comboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service + '&key=' + key);
        var placeHolderText = $(this).attr('placeHolder');
        //var serverFiltering = $(this).attr('serverFiltering');

        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            autoBind: false,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    parameterMap: function (options) {
                        //debugger;
                        return {
                            cascadeFromValue: dialog.find('#' + comboboxIdOnChange).val()
                        };
                    }
                }
            },
            select: function (e) {
                //debugger;
                var dataItem = this.dataItem(e.item.index());
                customCascadeFromComboBox(urlCascade, dataItem.value);
            },
            change: onKendoComboBoxChange
        });

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").unbind("change", changeCallback);
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").bind("change", changeCallback);
        function changeCallback() {
            attachChildCustomComboBoxChangeEvent(dialog, comboboxIdOnChange);
        }

        ////Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").unbind("dataBound", databoundCallback);
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").bind("dataBound", databoundCallback);
        function databoundCallback() {
            attachChildCustomComboBoxDataboundEvent(dialog, comboboxIdOnChange);
        };
    });
    //------------------END: [(for CustomComboBoxLinkedWithCascade)]--------------------------------------

    //------------------BEGIN: (CustomComboBoxForCustomDataLinkedFor)----------------------------------------
    dialog.find("input[controltype=kendoComboBoxForCustomDataLinkedFor]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var url = $(this).attr('url');
        var placeHolderText = $(this).attr('placeHolder');
        var parameter = $(this).attr('parameter');
        var cascadeFrom = $(this).attr('cascadeFrom');
        var comboboxIdOnChange = cascadeFrom;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);

        $(this).kendoComboBox({
            index: -1,
            autoBind: true,
            serverFiltering: false,
            placeholder: placeHolderText,
            dataTextField: "Text",             //name or description
            dataValueField: "Value",           //id            
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    parameterMap: function (options, operation) {
                        if (operation == "read") {
                            return { args: composeJsonParameter(parameter) };
                        }
                        return options;
                    }
                }
            },
            change: onKendoComboBoxChange
        });

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").unbind("change", changeCallback);
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").bind("change", changeCallback);
        function changeCallback() {
            attachChildCustomComboBoxChangeEvent(dialog, comboboxIdOnChange);
        }

        ////Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").unbind("dataBound", databoundCallback);
        dialog.find('#' + comboboxIdOnChange).data("kendoComboBox").bind("dataBound", databoundCallback);
        function databoundCallback() {
            attachChildCustomComboBoxDataboundEvent(dialog, comboboxIdOnChange);
        };
    });
    //------------------END: (CustomComboBoxForCustomDataLinkedFor)----------------------------------------    
}