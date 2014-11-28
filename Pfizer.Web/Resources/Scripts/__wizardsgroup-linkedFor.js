
function _initKendoLinkedForControl(dialog) {
    
    //------------------------[CustomTextBoxLinkedFor]-----------------------------------Start
    dialog.find('input[controltype=kendoAutoCompleteTextBoxLinkedFor]').each(function () {
        $(this).kendoAutoComplete();
        var thisId = $(this).attr('id');
        var parentId = $(this).attr('parent');
        var model = $(this).attr('model');
        var addAttributeToParent = 'linkedToTextBox' + thisId;        
        var parent = dialog.find('#' + parentId);
        //Note: add new attribute to Parent combobox that points to child textbox;
        parent.attr(addAttributeToParent, thisId);               
        
        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        parent.data("kendoComboBox").unbind("change", textboxCallback);
        parent.data("kendoComboBox").bind("change", textboxCallback);
        function textboxCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoAutoComplete'), thisId, parent.val());
        }              
    });
    //------------------------[CustomTextBoxLinkedFor]-----------------------------------End
    
    //------------------------[CustomNumberOnlyTextBoxLinkedFor]-----------------------------------Start
    dialog.find('input[controltype=kendoNumericTextBoxNumberOnlyLinkedFor]').each(function () {

        //-----set attributes for kendo numeric textbox------------
        var minValue = $(this).attr('min');
        var formatValue = $(this).attr('format');
        var decimalsValue = $(this).attr('decimals');
        var typeValue = $(this).attr('type');
        $(this).kendoNumericTextBox({ min: minValue, format: formatValue, decimals: decimalsValue, type: typeValue });
        //----------------------------------------------------------
        
        var thisId = $(this).attr('id');
        var parentId = $(this).attr('parent');
        var model = $(this).attr('model');
        var addAttributeToParent = 'linkedToTexboxNumberOnly' + thisId;
        var parent = dialog.find('#' + parentId);
        //Note: add new attribute to Parent combobox that points to child textbox;
        parent.attr(addAttributeToParent, thisId);

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        parent.data("kendoComboBox").unbind("change", numberOnlyCallback);
        parent.data("kendoComboBox").bind("change", numberOnlyCallback);
        function numberOnlyCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoNumericTextBox'), thisId, parent.val());
        }               
    });
    //------------------------[CustomNumberOnlyTextBoxLinkedFor]-----------------------------------End
    
    //------------------------[CustomNumericTextBoxLinkedFor]-----------------------------------Start
    dialog.find('input[controltype=kendoNumericTextBoxLinkedFor]').each(function () {        
        //-----set attributes for kendo numeric textbox------------
        var minValue = $(this).attr('min');
        $(this).kendoNumericTextBox({ min: minValue });
        //----------------------------------------------------------

        var thisId = $(this).attr('id');
        var parentId = $(this).attr('parent');
        var model = $(this).attr('model');
        var addAttributeToParent = 'linkedToTexboxNumeric' + thisId;
        var parent = dialog.find('#' + parentId);
        //Note: add new attribute to Parent combobox that points to child textbox;
        parent.attr(addAttributeToParent, thisId);

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        parent.data("kendoComboBox").unbind("change", numericCallback);
        parent.data("kendoComboBox").bind("change", numericCallback);
        function numericCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoNumericTextBox'), thisId, parent.val());
        }
    });
    //------------------------[CustomNumericTextBoxLinkedFor]-----------------------------------End
    
    //------------------------[CustomReadOnlyTextBoxLinkedFor]-----------------------------------Start
    dialog.find('input[controltype=kendoAutoCompleteReadOnlyLinkedFor]').each(function () {
        
        $(this).kendoAutoComplete();
        $(this).data("kendoAutoComplete").enable(false);
        //----------------------------------------------------------

        var thisId = $(this).attr('id');
        var parentId = $(this).attr('parent');
        var model = $(this).attr('model');
        var addAttributeToParent = 'linkedToReadOnlyTextBox' + thisId;
        var parent = dialog.find('#' + parentId);
        //Note: add new attribute to Parent combobox that points to child textbox;
        parent.attr(addAttributeToParent, thisId);

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        parent.data("kendoComboBox").unbind("change", readonlyTextboxCallback);
        parent.data("kendoComboBox").bind("change", readonlyTextboxCallback);
        function readonlyTextboxCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoAutoComplete'), thisId, parent.val());
        }
        
        parent.data("kendoComboBox").unbind("dataBound", databoundReadonlyTextBoxCallback);
        parent.data("kendoComboBox").bind("dataBound", databoundReadonlyTextBoxCallback);
        function databoundReadonlyTextBoxCallback() {            
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoAutoComplete'), thisId, parent.val());
        };
    });
    //------------------------[CustomReadOnlyTextBoxLinkedFor]-----------------------------------End

    //------------------------[CustomReadOnlyDateTextBoxLinkedFor]-----------------------------------Start
    dialog.find('input[controltype=kendoDatePickerReadOnlyLinkedFor]').each(function () {

        //-----set attributes for kendo numeric textbox------------
        var dateformat = $(this).attr('dateFormat');
        $(this).kendoDatePicker({ format: dateformat });
        $(this).data("kendoDatePicker").enable(false);
        //----------------------------------------------------------

        var thisId = $(this).attr('id');
        var parentId = $(this).attr('parent');
        var model = $(this).attr('model');
        var addAttributeToParent = 'linkedToReadOnlyTextBox' + thisId;
        var parent = dialog.find('#' + parentId);
        //Note: add new attribute to Parent combobox that points to child textbox;
        parent.attr(addAttributeToParent, thisId);

        ////Note: this is to attach change event to parent combobox, so that when parent data changes, it will cascade to the child;
        parent.data("kendoComboBox").unbind("change", readonlyDateTextboxCallback);
        parent.data("kendoComboBox").bind("change", readonlyDateTextboxCallback);
        function readonlyDateTextboxCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoDatePicker'), thisId, parent.val());
        }

        parent.data("kendoComboBox").unbind("dataBound", databoundReadonlyDateTextBoxCallback);
        parent.data("kendoComboBox").bind("dataBound", databoundReadonlyDateTextBoxCallback);
        function databoundReadonlyDateTextBoxCallback() {
            getDataLinkedFromParent(model, $('#' + thisId).data('kendoDatePicker'), thisId, parent.val());
        };
    });
    //------------------------[CustomReadOnlyDateTextBoxLinkedFor]-----------------------------------End

    //Assign value to target field
    function getDataLinkedFromParent(model, control,key, id) {
            var url = _director_fixUrl(_g_common_Virtualpath + '/Common/TextBox/GetValueFromLinkedControl');
            $.getJSON(url, { model: model, controlProperty: key, parentId: id },
            function (data) {
                //alert(key + ' ' + data);
                control.value(data);
            }) ;    
    }
}

