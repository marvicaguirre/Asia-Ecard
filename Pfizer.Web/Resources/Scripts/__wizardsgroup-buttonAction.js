/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-layout.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-grid.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-datepicker.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-general-controls.js" />

function _custom_attachCustomButtonActionEventHandler(divPanel) {
    divPanel.find(".buttonActionClass").each(function () {
        //debugger;
        setButtonStyle(this);

        var className = $(this).attr("class");

        if (className) {
            $(this).attr("class", className.replace("buttonActionClass ", ""));
        }

        $(this).click(function (e) {
            var url = $(this).attr("url");
            var gridName = $(this).attr("gridname");
            var parentId = $(this).attr("parentId");
            var methodName = $(this).attr("methodName");
            var targetLevel = $(this).attr("targetLevel");

            // The target functions' arguments should be in the order the arguments in the following function call.
            // This maps the values correctly to the arguments of the target functions.
            // E.g. if Foo() needs url and gridName, function signature should be Foo(url, gridName)
            // E.g. if Bar() needs e, gridName and parentId, function signature should be Bar(gridName, targetLevel, parentId, e)
            //      --> Note: targetLevel should be included even if it is not used.
            _common_getWindowParent().executeFunctionByName(methodName, window, url, gridName, targetLevel, parentId, e);
        });
    });
}

function executeFunctionByName(functionName, context /*, args */) {
    //debugger;
    var args = Array.prototype.slice.call(arguments, 2);
    var namespaces = functionName.split(".");
    var func = namespaces.pop();
    for (var i = 0; i < namespaces.length; i++) {
        context = context[namespaces[i]];
    }

    return context[func].apply(context, args);
}
