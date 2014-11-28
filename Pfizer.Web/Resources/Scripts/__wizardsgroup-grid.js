/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-error.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-view-details.js" />
/// <reference path="~/Resources/Scripts/jquery-1.10.2.js" />

var _g_grid_GridName = "";
var _g_grid_divObject_ControlType = "kendoGrid";
var _g_grid_GridNameMultiple;

var loadingKey = 'Loading...';

function getGridName() {
    return _g_grid_GridName;
}

function _grid_setGlobalVariable(gridName) {
    _g_grid_GridName = gridName;
}

function getGridNameMultiple() {
    return _g_grid_GridNameMultiple;
}

function _grid_setGlobalVariableMultiple(gridNames) {
    _g_grid_GridNameMultiple = gridNames;
}

function _grid_getGridSelectedRecords(gridname) {
    var element = '#' + gridname + ' input[type=checkbox][name=checkedRecords]:checked';
    var selectedRecords = $(element);
    return selectedRecords;
}

function createCustomKendoGrid(dialog) {
    if (!dialog || dialog.length <= 0) {
        _director_showError("Missing divMaster div object in the page");
        return;
    }
    var gridToSearch = "div[controltype=" + _g_grid_divObject_ControlType + "]";
    dialog.find(gridToSearch).each(function () {
        var grid = $(this);
        var columnsStructureUrl = _director_fixUrl(window._g_common_Virtualpath + '/Common/Grid/GetColumnFormat');
        var gridName = grid.attr('id');

        var isCreated = grid.attr('created');
        if (!isCreated) {
            grid.append(loadingKey).append(getLoadingImage());
            //onGridNotification(GlobalStatus.GetGridColumns, grid);

            //Retrieve parameter
            var parameter = grid.attr('parameter');
            debugger;
            //1. load the columns structure of the grid
            var requestForColumnSchema = $.ajax({
                type: "GET",
                url: columnsStructureUrl,
                traditional: true,
                cache:true,
                data: { gridName: gridName, parameter: parameter }
            });
            
            requestForColumnSchema.done(function (columnsData) {
                //The "Success" word seems to indicate success here
                grid.empty();
                //2. loading of the column structure is done, create the grid now...
                if (columnsData && columnsData.length > 0) {
                    launchGridNow(grid, columnsData);
                }
                else {
                    var magicWord = "fetchErrorData";
                    var errorUrl = window._g_common_Virtualpath + '/Common/Error/ShowError?errorData=' + magicWord;
                    _common_getWindowParent()._tab_launchNewTab(errorUrl, "error", "Error");
                }
            });

            requestForColumnSchema.fail(function (jqXHR, textStatus) {
                var title = "Error";
                var width = 900;
                var height = 500;
                _common_getWindowParent()._popup_openDialog(gDialogEntry, title, width, height, jqXHR.responseText);

                _common_getWindowParent()._error_launchErrorPage(jqXHR);

                //disableModalAllButton(false);
            });
        }
        //grid.remove(loadingKey);
    });
}

function launchGridNow(grid, columnsData) {
    //var gridUrl = _director_fixUrl(grid.attr('url'));
    var gridUrl = grid.attr('url');
    //var gridParameter = grid.attr('parameter');
    var gridName = grid.attr('id');
    //var gridWidth = grid.attr('gridwidth');
    var gridHeight = grid.attr('gridheight');
    //var serverPaging = grid.attr('serverPaging');
    //var serverFiltering = grid.attr('serverFiltering');
    grid.attr('created', 'True');

    var dataSource = new kendo.data.DataSource({
        type: 'aspnetmvc-ajax',
        serverPaging: true,
        serverSorting: true,
        serverFiltering: true,
        pageSize: 10,        
        transport: {
            read: {
                type: "POST",
                url: gridUrl,
                //contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: { gridName: gridName }
            }
        },
        parameterMap: function (options) {
            var jsonResult = JSON.stringify(options);
            return jsonResult;
        },
        schema: { data: "Data",total: "Total" }
    });
    
    grid.kendoGrid({
        dataSource: dataSource,
        height: gridHeight,
        //width: gridWidth,        
        columns: columnsData,
        dataBound: onGridDataBound,
        //selectable: "multiple cell",
        //navigatable: true,
        filterable: true,
        reorderable: true,
        resizable: true,
        groupable: true,
        columnMenu: true,
        sortable: {
            mode: "single",
            allowUnsort: false
        },
        pageable:
        {
            pageSize: 10,
            input: true,
            pageSizes: [10, 20, 30, 40, 50],
            refresh: true
        }
    });
}

function onGridDataBound(e) {
    var divMaster = $(e.sender.element).parent();
    //divMaster.find(".buttonEntryClass").hide();
    divMaster.find(".buttonEntryClass").show();
    divMaster.find(".buttonDeleteClass").show();

    attachGridButtonEvent(e);
}

function attachGridButtonEvent(e) {
    var grid = e.sender.element;
    var className = _g_tab_className_LinkObject;
    var linkNewTabs = grid.find("tbody tr ." + className);
    if (linkNewTabs) {
        linkNewTabs.click(
        function (evt) {
            var obj_linkNewTabClass = this;
            //alert(obj_linkNewTabClass);
            _common_getWindowParent()._call_tab_launchNewTabFromGrid(obj_linkNewTabClass, evt);

        }
        );
    }

    //for the DETAILS link
    _details_assignViewDetailsLinkEventHandler(grid);
    _details_assignActionLinkEventHandler(grid);
    _grid_checkAll(grid);
}

function _grid_checkAll(grid) {
    //RemoveAndAdd Checkbox Header
    var thfirst = grid.find('thead th:first');
    var checkBoxHeaderElement = thfirst.attr("data-title") == ' ';
    checkBoxHeaderElement && thfirst.find(".k-header-column-menu").remove();

    if (grid.find('tbody tr').length > 0) {
        if (thfirst.text().trim() === '') {
            var headerInputButton = $("<input type='checkbox' class='selectAll' />");
            var isCheckboxTemplate = thfirst.attr('data-title') == ' ';
            var isNotAGroupHeader = thfirst.prop('class') != 'k-group-cell k-header';
            isCheckboxTemplate && thfirst.find('input:checkbox').remove();
            isNotAGroupHeader && thfirst.find('a').remove();
            isNotAGroupHeader && isCheckboxTemplate && thfirst.append(headerInputButton);
            thfirst.find('input:checkbox').bind('click', function () {
                var checkbox = $(this);
                var inputButtons = $('#' + grid.attr('id') + ' input[type=checkbox][name=checkedRecords]');
                inputButtons.each(function () {
                    $(this).prop('checked', checkbox.is(":checked"));
                });

            });
        }
    }
}

function _grid_refreshCallback() {
    
}

function _grid_refreshChildWindowGrid(gridName,callback) {
    // use the grid object - call its ajaxRequest method
    var grid = getGridFromChildWindow();
    if (!grid && gridName) {
        //missing grid but passed a Build
        grid = getGridObject(gridName);
    }

    if (grid) {
        grid.dataSource.read();
    }
    else {
        forceRefreshGrid(gridName);
    }
    if (callback) callback();
}

function _grid_refreshChildWindowGridMultiple() {

    // use the grid object - call its ajaxRequest method
    var grids = getGridNameMultiple();
    $.each(grids, function (index, value) {
        debugger;
        var grid = getGridFromChildWindowByGridName(value);
        if (grid) {
            grid.dataSource.read();
        }
        else {
            forceRefreshGrid(value);
        }
    });
}

function getGridFromChildWindowByGridName(gridName) {
    debugger;
    //var theGridNames = getGridNameMultiple();
    if (gridName === "") {
        //default
        gridName = "Grid";
    }

    var grid = null;
    var theActiveWindow = _tab_getActiveTabContentWindow();
    if (theActiveWindow) {
        //grid = theActiveWindow.$("#" + theGridName).data(_g_grid_divObject_ControlType);
        grid = theActiveWindow.getGridObject(gridName);
    }

    return grid;
}

function getGridFromChildWindow() {
    var theGridName = getGridName();
    if (theGridName === "") {
        //default
        theGridName = "Grid";
    }

    var grid = null;
    var theActiveWindow = _tab_getActiveTabContentWindow();
    if (theActiveWindow) {
        grid = theActiveWindow.getGridObject(theGridName);
    }

    return grid;
}

function getGridObject(gridName) {
    return $("#" + gridName).data(_g_grid_divObject_ControlType);
}

function forceRefreshGrid(gridName) {
    //this is the hack fix for refresh of grid that is not reloaded when child is created in divDetail
    //this hack fix was created because of grid not refreshing grid when child grid are created. 
    var activeTabContentWindow = _tab_getActiveTabContentWindow();
    if (activeTabContentWindow) {
        var theGrid1 = activeTabContentWindow.$('#' + gridName);
        if (theGrid1) {
            var targetGridChildrens1 = theGrid1.children();
            $.each(targetGridChildrens1, function () {
                //[0] solves the problem in click for a href element.
                //not using the [0] does not trigger the click event for a href via jscript. weird..
                var kGridRefreshButton = $(this).find('.k-pager-refresh.k-link')[0];
                if (kGridRefreshButton) {
                    $(kGridRefreshButton)[0].click();
                }
            });
        }
    }
    //this is the hack fix for refresh of grid that is not reloaded when child is created in divDetail
    //this hack fix was created because of grid not refreshing grid when child grid are created.
    var theGrid2 = $(_g_common_selector_DivMaster).find('#' + gridName);
    if (theGrid2) {
        var targetGridChildrens2 = theGrid2.children();
        if (targetGridChildrens2) {
            targetGridChildrens2.each(function () {
                //[0] solves the problem in click for a href element.
                //not using the [0] does not trigger the click event for a href via jscript. weird..
                var kGridRefreshButton = $(this).find('.k-pager-refresh.k-link')[0];
                if (kGridRefreshButton) {
                    $(kGridRefreshButton)[0].click();
                }
            });
        }
    }

}