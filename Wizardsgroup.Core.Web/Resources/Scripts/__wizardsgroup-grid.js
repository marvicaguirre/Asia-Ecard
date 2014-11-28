/// <reference path="~/Resources/Scripts/__wizardsgroup-common.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-notification.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-popup.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-error.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-director.js" />
/// <reference path="~/Resources/Scripts/__wizardsgroup-view-details.js" />
/// <reference path="~/Resources/Scripts/jquery-1.9.1.js" />

var _g_grid_GridName = "";
var _g_grid_divObject_ControlType = "kendoGrid";

var loadingKey = 'Loading...';
$(function () {
    var divMaster = $(_g_common_selector_DivMaster);
    createCustomKendoGrid(divMaster);

});

function getGridName() {
    return _g_grid_GridName;
}

function _grid_setGlobalVariable(gridName) {
    _g_grid_GridName = gridName;
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

            //1. load the columns structure of the grid
            var requestForColumnSchema = $.ajax({
                type: "POST",
                url: columnsStructureUrl,
                traditional: true,
                data: { gridName: gridName }
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
    var gridParameter = grid.attr('parameter');
    var gridName = grid.attr('id');
    var gridWidth = grid.attr('gridwidth');
    var gridHeight = grid.attr('gridheight');
    var serverPaging = grid.attr('serverPaging');
    var serverFiltering = grid.attr('serverFiltering');
    grid.attr('created', 'True');
    var autoBind = true;

    //var groupable = true;
    //var printgrid = true;

    //if (grid.attr('autoBind') == "False") autoBind = false;
    //if (grid.attr('groupable') == "False") groupable = false;
    //if (grid.attr('printGrid') == "False") printgrid = false;
    //debugger;
    grid.kendoGrid({
        dataSource: {
            type: "json",
            //type: "aspnetmvc-ajax",
            serverPaging: serverPaging,
            serverSorting: serverFiltering,
            pageSize: 10,
            //pageSize: 10,
            transport: {
                read: gridUrl,
                parameterMap: function (options, operation) {
                    if (operation === "read") {
                        return { args: composeJsonParameter(gridParameter) };
                    }
                    return options;
                }
            },
            //batch: true,
            schema: {
                parse: function (response) {
                    //if (groupable == true) {
                    //    return onCustomKendoGridSchema(response);
                    //}
                    //else {
                    return response;
                    //}
                }
            }
        },
        autoBind: autoBind,
        width: gridWidth,
        height: gridHeight,
        filterable: true,
        //groupable: groupable,        
        sortable: {
            mode: "single",
            allowUnsort: false
        },
        columns: columnsData,
        //toolbar: kendo.template($("#template").html()),
        dataBound: onGridDataBound,
        autoSync: true,
        pageable:
        {
            //pageSize: 10,
            pageSize: 10,
            input: true,
            pageSizes: [10, 20, 30],
            refresh: true
        }
        //,
        //                    schema: {
        //                        data: "Data",
        //                        total: "Total"
        //                    }
    });

    //attachToParentGridDataboundEvent(grid);
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
    _grid_checkAll(grid);
}

function _grid_checkAll(grid) {
    //RemoveAndAdd Checkbox Header
    var thfirst = grid.find('thead th:first');
    thfirst.find('input:checkbox').remove();

    if (grid.find('tbody tr').length > 0) {
        if (thfirst.text().trim() === '') {
            var headerInputButton = $("<input type='checkbox' class='selectAll' />");
            thfirst.append(headerInputButton);
            thfirst.find('input:checkbox').bind('click', function () {
                var checkbox = $(this);
                var inputButtons = $('#' + grid.attr('id') + ' input[type=checkbox][name=checkedRecords]');
                //alert('parent ' + checkbox.is(":checked"));
                inputButtons.each(function () {
                    var checkedRecord = $(this);
                    //alert(checkedRecord.attr('primaryid') + ' ' + checkedRecord.is(":checked"));
                    checkedRecord.prop('checked', checkbox.is(":checked"));
                });

            });
        }
    }
}

function _grid_refreshChildWindowGrid(gridName) {
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
    var targetGridChildrens = activeTabContentWindow.$('#' + gridName).children();
    $.each(targetGridChildrens, function () {
        //[0] solves the problem in click for a href element.
        //not using the [0] does not trigger the click event for a href via jscript. weird..
        var kGridRefreshButton = $(this).find('.k-pager-refresh.k-link')[0];
        if (kGridRefreshButton) {
            $(kGridRefreshButton)[0].click();
        }
    });
}