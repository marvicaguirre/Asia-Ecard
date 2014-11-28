var gcType = "";
var gcArgs = "";
var gTargetCombobox = "";
var gComboboxIdOnChange = "";


////Triggered only on change
function onKendoComboBoxChange(e) {
    //debugger;
    gComboboxIdOnChange = $(e.sender.element).attr("id");
}
//Cascade to other elements other than combobox
function customCascadeFromComboBox(url, id) {
    $.getJSON(url, { id: id },
    function (data) {
        //key is the fieldName, value is the data (string,date or json)
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
    var i;
    var ids = '';
    for (i = 0; i < cascadeToId.length; ++i) {
        if (cascadeToId[i] == '' | cascadeToId[i] == 'undefined')
            continue;
        var comboboxChild = dialog.find('#' + cascadeToId[i]).data("kendoComboBox");
        comboboxChild.value("");
        comboboxChild.dataSource.read();
        ids = ids.concat(dialog.find('#' + cascadeToId[i]).attr("cascadeTo"), ';');
    }
    return ids;
}

function attachChildCustomComboBoxDataboundEvent(dialog, comboboxIdOnChange) {
    var cascadeToIds = dialog.find('#' + comboboxIdOnChange).attr("cascadeTo");
    var cascadeToId = cascadeToIds.split(';');
    var i;
    for (i = 0; i < cascadeToId.length; ++i) {
        var comboboxChild = dialog.find('#' + cascadeToId[i]).data("kendoComboBox");
        comboboxChild.dataSource.read();
    }
}

function addChildIdToParent(dialog, cascadeFrom, thisId) {
    var parentCombo = dialog.find('#' + cascadeFrom);
    var currentAttrValue = parentCombo.attr('cascadeTo');
    var newAttrValue = currentAttrValue != undefined ? currentAttrValue + ';' + thisId : thisId;
    parentCombo.attr('cascadeTo', newAttrValue);
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

    //----------------------[(for CustomComboBox)]----------------------------------
    dialog.find("input[controltype=kendoComboBox][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        //debugger;
        $(this).kendoComboBox({
            index: -1,
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
    });

    //------------------END: [(for CustomComboBox)]--------------------------------------

    //----------------------[(for CustomReadonlyComboBox)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxReadOnly][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        
        //debugger;
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

    //----------------------[(for CustomComboBoxWithCascade)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxWithCascade][serverFiltering=False]").each(function () {
        //debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        var urlCascade = $(this).attr('urlCascade');
        //debugger;
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

    //----------------------[(for CustomComboBoxLinked)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxLinked][serverFiltering=False]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');

        var comboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        //debugger;
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
    //------------------END: [(for CustomComboBoxLinked)]--------------------------------------

    //----------------------[(for CustomComboBoxLinkedWithCascade)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxLinkedWithCascade][serverFiltering=False]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');
        var urlCascade = $(this).attr('urlCascade');
        //Note: initialize the global gComboboxIdOnChange variable to store the parent combobox;
        var comboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        //debugger;
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