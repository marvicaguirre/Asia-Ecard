/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />
/// <reference path="~/Resources/Scripts/__medicard-dynamicTab.js" />
/// <reference path="~/Resources/Scripts/__medicard-popup.js" />
/// <reference path="~/Resources/Scripts/__medicard-error.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />
/// <reference path="~/Resources/Scripts/__medicard-view-details.js" />
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
        sortable: true,
        columns: columnsData,
        //toolbar: kendo.template($("#template").html()),
        dataBound: onGridDataBound,
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
            alert(obj_linkNewTabClass);            
            _common_getWindowParent()._call_tab_launchNewTabFromGrid(obj_linkNewTabClass, evt);
            
            }
        );
    }

    //grid.find("tbody tr ." + className).click(
    //        function (evt) {
    //            var obj_linkNewTabClass = this;
    //            _common_getWindowParent()._call_tab_launchNewTab(obj_linkNewTabClass, evt);
    //        }
    //    );

    //for the EDIT link
    //grid.find("tbody tr .linkModalClass").click(function (evt) {
    //    linkModalClassLaunchEntry($(this), evt);
    //    grid.find("tbody tr").removeClass('k-state-selected');
    //    $(this).closest("tr").addClass('k-state-selected');
    //    return false;

    //});


    //grid.find("tbody tr .linkModalDynamicGrid").live('click',
    //        function (evt) {
    //            var url = $(this).attr("href");
    //            var modalTitle = $(this).attr("modaltitle");
    //            var modalwidth = $(this).attr("modalwidth");
    //            var modalheight = $(this).attr("modalheight");
    //            var gridname = $(this).attr("gridname");
    //            var dataId = $(this).attr("primaryid");
    //            _common_getWindowParent().launchJqueryModalDynamicGrid(url, modalTitle, modalwidth, modalheight, dataId, evt, gridname);
    //        }
    //    );

    //for the DETAILS link
    _details_assignViewDetailsLinkEventHandler(grid);

    //className = _g_common_Name_LinkDetailsObject;
    //grid.find("tbody tr ." + className).click(
    //    function (evt) {
    //        var linkObject = evt.target;

    //        evt.preventDefault();
    //        var url = $(linkObject).attr("url");
    //        var targetLevel = $(linkObject).attr("targetLevel");
    //        var primaryId = $(linkObject).attr("primaryid");

    //        //alert('in grid js; ' + primaryId);
    //        _common_assignClientId(linkObject, e, primaryId);

    //        //Hight Light Row Grid
    //        //grid.find("tbody tr").removeClass('k-state-selected');
    //        //$(this).closest("tr").addClass('k-state-selected');

    //        data = { id: primaryId };
    //        url = _director_fixUrl(url);
    //        _common_launchJqueryDetails(url, data, evt, targetLevel);
    //        return false;
    //    }
    //);

    ////Add Checkbox Header
    ////Comment today    
    //var thfirst = grid.find('thead th:first');
    //thfirst.find('input:checkbox').remove();

    //if (grid.find('tbody tr').length > 0) {        
    //    if (thfirst.text().trim() === '') {
    //        thfirst.append("<input type='checkbox' class='selectAll' />");
    //        thfirst.find('input:checkbox').click(
    //        function () {                
    //            var checkbox = $(this);
    //            //grid.find("tr").find("td:first input").attr("checked", checkbox.is(":checked"));
    //            grid.find("tr").find("td:first input[type=checkbox]").attr("checked", checkbox.is(":checked"));
    //        });
    //    }
    //}    

    //var thfirst = grid.find('thead th:first');
    //thfirst.find('input:checkbox').remove();

    //if (grid.find('tbody tr').length > 0) {        
    //    if (thfirst.text().trim() === '') {
    //        thfirst.append("<input type='checkbox' class='selectAll' />");
    //        thfirst.find('input:checkbox').click(
    //        function () {
    //            debugger;
    //            var checkbox = $(this);
    //            var gridName = grid.attr("id");
    //            var checkboxes = '#' + gridName + ' input[type=checkbox][name=checkedRecords]';
    //            $(checkboxes).attr("checked", checkbox.is(":checked"));
    //        });
    //    }
    //}   

}

function _grid_refreshChildWindowGrid(gridName) {
    // use the grid object - call its ajaxRequest method
    var grid = getGridFromChildWindow();
    if (!grid && gridName) {
        //missing grid but passed a gridName
        grid = getGridObject(gridName);
    }

    if (grid) {
        grid.dataSource.read();
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
        //grid = theActiveWindow.$("#" + theGridName).data(_g_grid_divObject_ControlType);
        grid = theActiveWindow.getGridObject(theGridName);
    }

    return grid;
}

function getGridObject(gridName) {
    return $("#" + gridName).data(_g_grid_divObject_ControlType);
}
