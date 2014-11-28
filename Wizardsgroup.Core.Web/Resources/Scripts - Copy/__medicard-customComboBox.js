var gcType = "";
var gcArgs = "";
var gTargetCombobox = "";
//var gComboboxIdOnChange = "";


////Triggered only on change
function onKendoComboBoxChange(e) {
    debugger;
    //gComboboxIdOnChange = $(e.sender.element).attr("id");
}

//function onKendoComboBoxChange(e) {
//    debugger;
//    gComboboxIdOnChange = $(e.sender.element).attr("id");
//}

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

function _initKendoComboBox(dialog) {

    //----------------------[(for kendoAutoCompleteForComboBox)]----------------------------------
    //Used by comboxbox for debug data
    dialog.find("input[controltype=kendoAutoCompleteForComboBox][serverFiltering=False]").each(function () {
        ////debugger;
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
        ////debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        ////debugger;
        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            dataTextField: "text",             //name or description
            dataValueField: "value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url
                }
            },
            select: function (e) {
                //debugger;
                var targetid = this.element.attr("targetid");
                if (targetid) {
                    var dataItem = this.dataItem(e.item.index());
                    dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.value);
                }
            },
            change: onKendoComboBoxChange
        });

        //Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
        $(this).data("kendoComboBox").input.bind("keyup", function (e) {
            //debugger;
            var comboboxval = $(this).val();
            if (e.keyCode != 9 && comboboxval.length <= 0) {
                var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBox]").attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value("");
                    dialog.find("#" + targetid).focus();
                }
            }
        });
    });

    //------------------END: [(for CustomComboBox)]--------------------------------------

    //----------------------[(for CustomComboBoxWithCascade)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxWithCascade][serverFiltering=False]").each(function () {
        ////debugger;
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDown?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        var urlCascade = $(this).attr('urlCascade');
        ////debugger;
        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            dataTextField: "text",             //name or description
            dataValueField: "value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url
                }
            },
            select: function (e) {
                //debugger;
                var targetid = this.element.attr("targetid");
                if (targetid) {
                    var dataItem = this.dataItem(e.item.index());
                    dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.value);
                    customCascadeFromComboBox(urlCascade, dataItem.value);
                }
            },
            change: onKendoComboBoxChange
        });

        //Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
        $(this).data("kendoComboBox").input.bind("keyup", function (e) {
            //debugger;
            var comboboxval = $(this).val();
            if (e.keyCode != 9 && comboboxval.length <= 0) {
                var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBoxWithCascade]").attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value("");
                    dialog.find("#" + targetid).focus();
                }
            }
        });
    });

    //------------------END: [(for CustomComboBoxWithCascade)]--------------------------------------

    ////----------------------[(for CustomComboBoxCascade)]----------------------------------
    //dialog.find("input[controltype=kendoComboBoxCascade][serverFiltering=False]").each(function () {
    //    //debugger;
    //    var thisId = $(this).attr('id');
    //    var cascadeFrom = $(this).attr('cascadeFrom');
    //    //Note: initialize the global gComboboxIdOnChange variable to store the parent combobox;
    //    gComboboxIdOnChange = cascadeFrom;

    //    //Note: add new attribute to Parent combobox that points to child combobox;
    //    dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
    //    var service = $(this).attr('service');
    //    var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service);
    //    var placeHolderText = $(this).attr('placeHolder');
    //    var serverFiltering = $(this).attr('serverFiltering');
    //    debugger;
    //    $(this).kendoComboBox({
    //        index: -1,
    //        placeholder: "Select " + placeHolderText,
    //        autoBind: false,
    //        dataTextField: "text",             //name or description
    //        dataValueField: "value",           //id
    //        filter: "startswith",
    //        dataSource: {
    //            type: "json",
    //            transport: {
    //                read: url,
    //                parameterMap: function (options) {
    //                    //debugger;
    //                    return {
    //                        cascadeFromValue: dialog.find('#' + gComboboxIdOnChange).val()
    //                    };
    //                }
    //            }
    //        },
    //        select: function (e) {
    //            ////debugger;
    //            var targetid = this.element.attr("targetid");
    //            if (targetid) {
    //                var dataItem = this.dataItem(e.item.index());
    //                dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.value);
    //            }
    //        },
    //        change: onKendoComboBoxChange
    //    });

    //    //Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
    //    dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("change", function (e) {
    //        //debugger;

    //        //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
    //        var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
    //        dialog.find('#' + cascadeToId).data("kendoComboBox").value("");
    //        var cascadeToTargetId = dialog.find('#' + cascadeToId).attr("targetid");
    //        dialog.find("#" + cascadeToTargetId).data("kendoAutoComplete").value("");

    //        dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
    //    });

    //    //Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
    //    dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("dataBound", function (e) {
    //        //debugger;

    //        //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
    //        var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
    //        dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
    //    });


    //    //Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
    //    $(this).data("kendoComboBox").input.bind("keyup", function (e) {
    //        //debugger;
    //        var comboboxval = $(this).val();
    //        if (e.keyCode != 9 && comboboxval.length <= 0) {
    //            var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBoxCascade]").attr("targetid");
    //            if (targetid) {
    //                dialog.find("#" + targetid).data("kendoAutoComplete").value("");
    //            }
    //        }
    //    });


    //});
    ////------------------END: [(for CustomComboBoxCascade)]--------------------------------------


    //----------------------[(for CustomComboBoxLinked)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxLinked][serverFiltering=False]").each(function () {
        debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');
        //Note: initialize the global gComboboxIdOnChange variable to store the parent combobox;
        var gComboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        debugger;
        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            autoBind: false,
            dataTextField: "text",             //name or description
            dataValueField: "value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    parameterMap: function (options) {
                        //debugger;
                        return {
                            cascadeFromValue: dialog.find('#' + gComboboxIdOnChange).val()
                        };
                    }
                }
            },
            select: function (e) {
                debugger;
                //this is for debugger textbox
                var dataItem = this.dataItem(e.item.index());
                var targetid = this.element.attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.value);
                }
            },
            change: onKendoComboBoxChange
        });

        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").unbind("change", changeCallback);
        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("change", changeCallback);

        function changeCallback() {
            debugger;
            alert("ParentToChildEvent of " + gComboboxIdOnChange);
            var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
            var comboboxChild = dialog.find('#' + cascadeToId).data("kendoComboBox");
            comboboxChild.value("");
            comboboxChild.dataSource.read();

            //Textbox
            var cascadeToTargetId = comboboxChild.attr("targetid");
            dialog.find("#" + cascadeToTargetId).data("kendoAutoComplete").value("");
        }

        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").unbind("dataBound", databoundCallback);
        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("dataBound", databoundCallback);
        function databoundCallback() {
            debugger;
            alert("DataboundParent of " + gComboboxIdOnChange);
            //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
            var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
            dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
        };


        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        //dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("change", function (e) {
        //    debugger;
        //    //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
        //    alert("ParentToChildEvent");
        //    var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
        //    var comboboxChild = dialog.find('#' + cascadeToId).data("kendoComboBox");
        //    comboboxChild.value("");
        //    comboboxChild.dataSource.read();

        //    //Textbox
        //    var cascadeToTargetId = comboboxChild.attr("targetid");
        //    dialog.find("#" + cascadeToTargetId).data("kendoAutoComplete").value("");
        //});
        
        ////Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
        //dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("dataBound", function (e) {
        //    debugger;
        //    alert("DataboundParent");
        //    //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
        //    var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
        //    dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
        //});


        //////Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
        //$(this).data("kendoComboBox").input.bind("keyup", function (e) {
        //    //debugger;
        //    var comboboxval = $(this).val();
        //    if (e.keyCode != 9 && comboboxval.length <= 0) {
        //        var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBoxLinked]").attr("targetid");
        //        if (targetid) {
        //            dialog.find("#" + targetid).data("kendoAutoComplete").value("");
        //        }
        //    }
        //});

    });
    //------------------END: [(for CustomComboBoxLinked)]--------------------------------------

    //----------------------[(for CustomComboBoxLinkedWithCascade)]----------------------------------
    dialog.find("input[controltype=kendoComboBoxLinkedWithCascade][serverFiltering=False]").each(function () {
        //debugger;
        var thisId = $(this).attr('id');
        var cascadeFrom = $(this).attr('cascadeFrom');
        var urlCascade = $(this).attr('urlCascade');
        //Note: initialize the global gComboboxIdOnChange variable to store the parent combobox;
        var gComboboxIdOnChange = cascadeFrom;

        //Note: add new attribute to Parent combobox that points to child combobox;
        dialog.find('#' + cascadeFrom).attr('cascadeTo', thisId);
        var service = $(this).attr('service');
        var url = _director_fixUrl(_g_common_Virtualpath + '/Common/ComboBox/GetDataForDropDownCascade?Service=' + service);
        var placeHolderText = $(this).attr('placeHolder');
        var serverFiltering = $(this).attr('serverFiltering');
        debugger;
        $(this).kendoComboBox({
            index: -1,
            placeholder: "Select " + placeHolderText,
            autoBind: false,
            dataTextField: "text",             //name or description
            dataValueField: "value",           //id
            filter: "startswith",
            dataSource: {
                type: "json",
                transport: {
                    read: url,
                    parameterMap: function (options) {
                        //debugger;
                        return {
                            cascadeFromValue: dialog.find('#' + gComboboxIdOnChange).val()
                        };
                    }
                }
            },
            select: function (e) {
                ////debugger;
                var dataItem = this.dataItem(e.item.index());
                customCascadeFromComboBox(urlCascade, dataItem.value);

                var targetid = this.element.attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.value);
                }
            },
            change: onKendoComboBoxChange
        });

        //Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("change", function (e) {
            //debugger;

            //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
            var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
            dialog.find('#' + cascadeToId).data("kendoComboBox").value("");
            var cascadeToTargetId = dialog.find('#' + cascadeToId).attr("targetid");
            dialog.find("#" + cascadeToTargetId).data("kendoAutoComplete").value("");

            dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
        });

        //Note: this is to attach dataBound event to parent combobox, so that when parent data loads, child control will refresh;
        dialog.find('#' + gComboboxIdOnChange).data("kendoComboBox").bind("dataBound", function (e) {
            //debugger;

            //Used to clear and update the cascadeTo controls: Combobox and Autocomplete Textbox;
            var cascadeToId = dialog.find('#' + gComboboxIdOnChange).attr("cascadeTo");
            dialog.find('#' + cascadeToId).data("kendoComboBox").dataSource.read();
        });

        //Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
        $(this).data("kendoComboBox").input.bind("keyup", function (e) {
            //debugger;
            var comboboxval = $(this).val();
            if (e.keyCode != 9 && comboboxval.length <= 0) {
                var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBoxLinkedWithCascade]").attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value("");
                }
            }
        });

    });
    //------------------END: [(for CustomComboBoxLinkedWithCascade)]--------------------------------------

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
            placeholder: "Select " + placeHolderText,
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
            select: function (e) {
                //debugger;
                var dataItem = this.dataItem(e.item.index());

                var targetid = this.element.attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value(dataItem.Value);
                }
            },
            change: onKendoComboBoxChange
        });

        //Note: this is to clear the textbox when selected dropdown is empty (9 = tab key)
        $(this).data("kendoComboBox").input.bind("keyup", function (e) {
            //debugger;
            var comboboxval = $(this).val();
            if (e.keyCode != 9 && comboboxval.length <= 0) {
                var targetid = $(e.target).parent().parent().find("input[controltype=kendoComboBox]").attr("targetid");
                if (targetid) {
                    dialog.find("#" + targetid).data("kendoAutoComplete").value("");
                    dialog.find("#" + targetid).focus();
                }
            }
        });
    });
    //------------------END: (CustomComboBoxForCustomData)----------------------------------------

}