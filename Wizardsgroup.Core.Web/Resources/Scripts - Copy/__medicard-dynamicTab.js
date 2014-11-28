/// <reference path="~/Resources/Scripts/__medicard-common.js" />
/// <reference path="~/Resources/Scripts/__medicard-notification.js" />
/// <reference path="~/Resources/Scripts/__medicard-director.js" />
/// <reference path="~/Resources/Scripts/jquery-1.9.1.js"/>

var _g_tab_className_LinkObject = "linkNewTabClass";
var gCurrentTabIndex = 0;
var gTabStripNameSelector = "#medicard-tabstrip";
var gTabStrip = "";

//$(function() {
//    gTabStrip = $(gTabStripNameSelector).data('kendoTabStrip');
//    //return $('#tabstrip.t-widget');
//});
function initTabStrip() {
    gTabStrip = $(gTabStripNameSelector).data('kendoTabStrip');
}

function _tab_setGlobalVariable() {
    gCurrentTabIndex = getCurrentTabIndex();
}

function getTabStrip() {
    return gTabStrip;
}

function _call_tab_launchNewTabFromGrid(obj_linkNewTabClass, evt) {
    debugger;
    var obj = $(obj_linkNewTabClass);
    var url = obj.attr("url");
    url = url +"?id=" + obj.attr("primaryId");
    var tabText = obj.attr("tabText");
    var moduleId = obj.attr("moduleId");

    _tab_launchNewTab(url, tabText, moduleId, evt);
}

function _call_tab_launchNewTab(obj_linkNewTabClass, evt) {    
    var obj = $(obj_linkNewTabClass);
    var url = obj.attr("url");
    var tabText = obj.attr("tabText");
    var moduleId = obj.attr("moduleId");

    _tab_launchNewTab(url, tabText, moduleId, evt);
}

/*******************JQUERY NEW TAB FROM MAIN WINDOW****************/
function _tab_launchNewTab(url, tabText, moduleId, evt) {
    //_director_setGlobalVariable(data, '', url);

    //evt.preventDefault();
    //_tab_loadModule(url, menuText);

    var moduleUrl = url;
    //var moduleId = menuText;// + alink.attr("id");
    if (moduleUrl && tabText && moduleId) {
        _tab_loadModule(moduleUrl, tabText, moduleId);
    }
}
/*******************JQUERY NEW TAB FROM MAIN WINDOW****************/


function _tab_loadModule(myurl, menuText, moduleId) {
    debugger;
    if (!myurl) {
        _director_showError("Missing url");
        return;
    }
    if (!menuText) {
        _director_showError("Missing tabText");
        return;
    }
    if (!moduleId) {
        _director_showError("Missing moduleId");
        return;
    }

    var menuLoadPath = '/Common/Menu/Load?url=';
    //var loaderPath = window._g_common_Applicationpath + menuLoadPath;
    var vDir = window._g_common_Virtualpath;
    var virtualDir = "";
    if (vDir && vDir != "/")
    {
        virtualDir = vDir;
    }

    //var loaderPath = window._g_common_Applicationpath + virtualDir + menuLoadPath;
    var loaderPath = _director_fixUrl(virtualDir + menuLoadPath);
    var url = loaderPath + myurl;
    var frameId = 'theFrame_1_' + moduleId; //randomString();
    var iframeElement = '<iframe id="' + frameId + '" frameborder="0" scrolling="auto" width="100%" height="450px" src="' + url + '" onload="resizeiFrame(\'' + frameId + '\')"></iframe>';

    var extraText = '<span class="ui-icon ui-icon-close dbwizard-inline-block-display"></span>';
    //1.
    initTabStrip();

    //2. check for existing tab
    //var tabFound = false;
    //var index = 0;
    //$(".k-tabstrip-items li.k-item").each(function (ndx, z) {
    //    var item = $(this);
    //    if (item.length > 0) {
    //        var nodeText = item[0].innerText;
    //        //alert(nodeText + "; " + menuText);
    //        if (nodeText && nodeText.toLowerCase() == menuText.toString().toLowerCase()) {
    //            tabFound = true;
    //            index = ndx;
    //            //existing already;
    //            return;
    //        }
    //    }
    //});
    //debugger;
    var index = getIndexOfExistingTabWithText(menuText);
    var index2 = getIndexOfExistingTabWithUrl(url);
    var tabFound = index > -1;
    var tabFound2 = index2 > -1;

    //3. exit if existing already
    if ((tabFound && tabFound2) || menuText.toLowerCase() == "home") {
        var myTab1 = getChildren().eq(index2);
        gTabStrip.select(myTab1);
        return;
    }

    //4. create the tab
    //$(gTabStripNameSelector).data('kendoTabStrip').addTab({ text: menuText, html: iframeElement });
    gTabStrip.append({
        text: menuText + extraText,
        encoded: false, //to prevent encoding of the HTML portion of the text property
        content: iframeElement
        //ui-icon ui-icon-close = from jQuery.ui.theme.css;
        //spriteCssClass: "ui-icon ui-icon-close dbwizard-inline-block-display",
        //imageUrl: "Resources/Content/themes/base/images/ui-icons_222222_256x240.png"
    });

    //5. make the tab active;
    var myTab = getChildren().eq(getLastTabIndex());
    gTabStrip.select(myTab);

    //6. assign a click event handler to the X icon.
    assignClickEventHandler(frameId);
    
    //notes:
    //-active tab selector: $(".k-tabstrip-items li.k-item.k-tab-on-top")
    //-all close icons: $("#hrWizardTabstrip .k-item span.ui-icon-close")
}

function assignClickEventHandler(frameId) {
    if (!frameId) {
        _director_showError("Missing frame Id!");
        return;
    }
    
    var closeTab = function (e) {
        //make sure we have a tabstrip
        initTabStrip();
        

        //var divID = $(this).parent().parent().attr("aria-controls");
        //var tabIndex = getTabIndex(divID);

        gTabStrip.activateTab($(e.target).closest('li'));
        var tab = gTabStrip.select();
        var otherTab = tab.next();
        otherTab = otherTab.length ? otherTab : tab.prev();

        //var tabToRemove = $(e.target).closest('li');
        //gTabStrip.remove(tabToRemove);
        
        gTabStrip.remove(tab);
        gTabStrip.select(otherTab);
        
        //var $tabs = $('#tabs_module').tabs();
        //var index = $("li", $tabs).index($(this).parent());
        //if (index == -1)
        //    return false;

        //$tabs.tabs("remove", index);
    };
    
    //============================================
    //STRUCTURE OF THE TABSTRIP HTML:
    //============================================
    //<div class="k-tabstrip">                                      //==>this is the tabstrip
    //  -   <ul class="k-tabstrip-items">
    //  -   - <li class="k-item" role="tab" aria-controls=[[THE_ID_OF_CONTENT_DIV]]>
    //  -   -   -   <span class="k-link">
    //  -   -   -   -   <span class="ui-icon-close">                //==>this is our clickable X icon
    //  -   <div class="k-content" ID=[[THE_ID_OF_CONTENT_DIV]]>    //==>this is the DIV that has the content of the LI above
    //============================================
    //============================================
    //1. get the id of the container DIV of the iframe
    var parentDivID = $($(gTabStripNameSelector + " div iframe#" + frameId).parent()).attr("id");
    //2. look for the LI with the value of the aria-controls attribute same as the ID of the div;
    //3. then get the span with the ui-icon-close
    var closeIcon = $(gTabStripNameSelector + " li[aria-controls='" + parentDivID + "'] .ui-icon-close");
    //4. attach the click event handler
    $(closeIcon).on("click", closeTab);
}

function doCheck() {
    getCurrentTabIndex();
    getLastTabIndex();
}

function getChildren() {
    return gTabStrip.tabGroup.children("li");
}

function getIndexOfExistingTabWithText(menuText) {
    var index = -1;
    $(".k-tabstrip-items li.k-item").each(function (ndx) {
        var item = $(this);
        if (item.length > 0) {
            var nodeText = item[0].innerText;
            //alert(nodeText + "; " + menuText);
            if (nodeText && nodeText.toLowerCase() == menuText.toString().toLowerCase()) {
                index = ndx;
                //existing already;
                return;
            }
        }
    });

    return index;
}

function getIndexOfExistingTabWithUrl(url) {
    var index = -1;
    $(".k-tabstrip-items li.k-item").each(function (ndx) {
        var item = $(this);
        if (item.length > 0) {
            var theUrl = getTabUrl(ndx);
            //alert("target: " + url + "; current: " + theUrl);
            //debugger;
            if (url && url.toLowerCase() == theUrl.toString().toLowerCase()) {
                index = ndx;
                //existing already;
                return;
            }
        }
    });

    return index;
}

//function randomString() {
//    var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
//    var string_length = 8;
//    var randomstring = '';
//    for (var i = 0; i < string_length; i++) {
//        var rnum = Math.floor(Math.random() * chars.length);
//        randomstring += chars.substring(rnum, rnum + 1);
//    }
//    return randomstring;
//}

/// <summary>
/// Closes a tab.
/// </summary>
/// <param type="string" name="tabText">the text of the tab.</param>
/// <example>
/// closeTab()
/// closeTab('Tab 2')
/// </example>
function closeTab(tabText) {
    var tabStrip = gTabStrip;
    var tabIndex = -1;
    if (tabText) {
        var tab = tabStrip.getTab({ text: tabText });
        if (tab) {
            tabIndex = tab.index();
        }
    }
    else {
        tabIndex = getCurrentTabIndex();
    }
    tabStrip.removeTab(tabIndex);
    return false;
}

function closeAllTabs() {
    var lastIndex = getLastTabIndex();
    if (lastIndex > 0) {
        var answer = confirmCloseTab();
        if (!answer) return;
    }
    for (var i = lastIndex; i > 0; i--) {
        //$(gTabStripNameSelector).data('tTabStrip').removeTab(i);
        gTabStrip.removeTab(i);
    }
}

function confirmCloseTab() {
    var msg = "Are you sure you want to close all the tabs?\n\nAny unsaved changes will be lost.\n\nPress OK to continue or Cancel to stay on the current tab.";
    return confirm(msg);
}

/// <summary>
/// Get the current tab index
/// </summary>
/// <returns>zero-based index<returns>
function getCurrentTabIndex() {
    var index = 0;

    //var activeTabName = $("#hrWizardTabstrip_ts_active").attr("aria-controls");
    var activeTabName = getActiveTabName();
    if (activeTabName) {
        //name of the tab used as href is 1-based; we need to subtract 1;
        //example: #Tabstrip-1 == index -> 0
        index = activeTabName.split('-')[1];
    }
    return index - 1;
}

function getTabIndex(tabName) {
    var index = 0;
    //var activeTabName = $("#hrWizardTabstrip_ts_active").attr("aria-controls");
    if (tabName) {
        //name of the tab used as href is 1-based; we need to subtract 1;
        //example: #Tabstrip-1 == index -> 0
        index = tabName.split('-')[1];
    }
    return index;
}

function getLastTabIndex() {
    var countLength = getChildren().length;
    //var tabstripitems = getTabStrip().find('.k-tabstrip-items');

    //var countLength = tabstripitems.children().length;
    return countLength - 1;
}

function resetMainMenu() {
    //$("#homeLink").parent().removeClass("ui-tabs-selected ui-state-active");
    //$("#hrAdmMain").parent().removeClass("ui-tabs-selected ui-state-active");
    //$("#empSvcMain").parent().removeClass("ui-tabs-selected ui-state-active");
    //$("#mgrSvcMain").parent().removeClass("ui-tabs-selected ui-state-active");
    //$("#sysAdmMain").parent().removeClass("ui-tabs-selected ui-state-active");
    //$("#reportsMain").parent().removeClass("ui-tabs-selected ui-state-active");
}

function hideSubmenu(moduleTabIndex) {
    var elem = '#tabs-' + moduleTabIndex;
    if (elem) {
        $(elem).addClass("ui-tabs-hide");
    }
}

function registerMenuLink(linkId, url, menuText, moduleTabIndex) {
    $("#" + linkId).click(function () {
        _tab_loadModule(url, menuText, linkId);
        if (moduleTabIndex) {
            resetMainMenu();
            hideSubmenu(moduleTabIndex);
        }
    });
}

/// <summary>
/// Gets the window object of the last tab.
/// </summary>
/// <example>
/// </example>
function getLastTabContentWindow() {
    //note: index is zero-based; tab Id is 1-based
    var tabIndex = getLastTabIndex() + 1;
    var iframe = $('#tabstrip-' + tabIndex + ' iframe');
    return iframe.get(0).contentWindow;
}

//function _tab_getActiveTabContentWindow() {
//    //note: index is zero-based; tab Id is 1-based
//    var tabIndex = gCurrentTabIndex + 1;
//    var iframe = $('#tabstrip-' + tabIndex + ' iframe');
//    if (iframe && iframe.get(0)) {
//        return iframe.get(0).contentWindow;
//    }
//    return null;

//    //var selector = "k-item k-state-default k-last k-tab-on-top k-state-active";
//    //var parent = $(selector).closest();
//}

function _tab_getActiveTabContentWindow() {
    var iframeObj = getActiveIFrame();
    if (iframeObj && iframeObj.get(0)) {
        return iframeObj.get(0).contentWindow;
    }
    return null;
}

function getTabUrl(ndx) {
    var iframeObj = getIFrame(ndx);
    if (iframeObj && iframeObj.get(0)) {
        return iframeObj.get(0).src;
    }
    return null;
}

function getIFrame(ndx) {
    var tabName = getTabName(ndx);
    var iframeObj = getIFrameOfTab(tabName);
    return iframeObj;
}

function getActiveIFrame() {
    var tabName = getActiveTabName();
    var iframeObj = getIFrameOfTab(tabName);
    return iframeObj;
}

function getTabName(ndx) {
    var tabItemsSelector = gTabStripNameSelector + " .k-tabstrip-items li";
    var tabItems = $(tabItemsSelector);
    if (tabItems && tabItems.length > 0) {
        var attrIdContainer = "aria-controls";
        //$($(gTabStripNameSelector + " .k-tabstrip-items li")[ndx]).attr(attrIdContainer);
        var tabName = $($(tabItemsSelector)[ndx]).attr(attrIdContainer);
        return tabName;
    }
    return "";
}

function getActiveTabName() {
    //var selector = gTabStripNameSelector + " .k-tabstrip-items li#hrWizardTabstrip_ts_active";
    var selector = gTabStripNameSelector + " .k-tabstrip-items li.k-tab-on-top.k-state-active";
    var attrIdContainer = "aria-controls";
    var tabName = $(selector).attr(attrIdContainer);
    return tabName;
}

function getIFrameOfTab(tabName) {
    var selector = "div#" + tabName + " iframe";
    var iframeObj = $(selector);    
    return iframeObj;
}

$(function () {
    //================================================
    /// Click event is being attached multiple times! Not good! 
    /// SOLUTION: We inline the call to the "closeTab" and "closeAllTabs" function in the link elements;
    /// See: _Layout.cshtml
    //================================================
    // $("#closeTabButton").click(function (e) {
    //    closeTab(e);
    // });

    //$("#closeAllTabsButton").click(function () {
    //    closeAllTabs();
    //});
    //================================================
    
    //$(".k-tabstrip-items span.ui-icon-close").on("click", function (evt) {
    //    evt.preventDefault();
    //    alert('test');
    //    //var $tabs = $('#tabs_module').tabs();
    //    //var index = $("li", $tabs).index($(this).parent());
    //    //if (index == -1)
    //    //    return false;

    //    //$tabs.tabs("remove", index);

    //});
});