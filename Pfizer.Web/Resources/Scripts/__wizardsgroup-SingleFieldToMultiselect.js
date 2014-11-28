function _configureSingleFieldToMultiselectFor(dialog) {
    //TODO refactor this code
    dialog.find("select[controltype=singleFieldMultiselect]").each(function () {
        var multiselect = $(this);
        var parentContainer = $(multiselect).parent();
        var hiddenValueField = parentContainer.children("input[id*='_Value']");
        var delimeter = $(multiselect).attr('delimeter');
        var filterable = $(multiselect).attr('filterable');
        var placeHolder = $(multiselect).attr('placeholder');
        var isSelectAll = $(multiselect).attr('selectall');
        var widthPx = $(multiselect).attr('width');
        var textValue = '';
        
        $(multiselect).keydown(function (e) {
            if (e == 32) {
                e.preventDefault();
            }
        }).multipleSelect({
            width: widthPx,
            filter: filterable,
            selectAll: isSelectAll,
            placeholder: placeHolder,
            onClick: function (view) {
                var value = $(multiselect).multipleSelect("getSelects", "value");
                hiddenValueField.val(value);
                value = hiddenValueField.val().replace(/,/g, delimeter);
                hiddenValueField.val(value);
                
                textValue = $(multiselect).multipleSelect("getSelects", "text");
                parentContainer.find('.ms-parent').attr('title', textValue);
            },
            onCheckAll: function () {
                var value = $(multiselect).multipleSelect("getSelects", "value");
                hiddenValueField.val(value);                
                value = hiddenValueField.val().replace(/,/g, delimeter);
                hiddenValueField.val(value);
                
                textValue = $(multiselect).multipleSelect("getSelects", "text");
                parentContainer.find('.ms-parent').attr('title', textValue);
            },
            onUncheckAll: function () {
                hiddenValueField.val('');                
                parentContainer.find('div .ms-parent').attr('title', '');
            }
        });
        
        var multiSelectParent = parentContainer.find('.ms-parent');        
        multiSelectParent.tooltip();
        textValue = $(multiselect).multipleSelect("getSelects", "text");        
        multiSelectParent.attr('title', textValue);
    });
    
}