using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Wizardsgroup.Core.Web.Extensions;
using Wizardsgroup.Core.Web.SessionManagement;

namespace Wizardsgroup.Core.Web.Helpers.MenuHelper
{
    public static class MenuGenerator
    {
        private const string _beforeSubMenuItem = "<li class='dropdown-submenu'> "
                                    + "<a href='#' tabindex='-1' class='dropdown-toggle' data-toggle='dropdown'>{0} </a> "
                                    + "<ul class='dropdown-menu'> ";
        private const string _afterSubMenuItem = "</ul> "
                                    + "</li>";
        private const string _beforeMenuItem = "<li>";
        private const string _afterMenuItem = "</li>";
        private const string _separator = "<li class='divider'></li>";
        public static MvcHtmlString GenerateMenu(this WebViewPage page, AbstractMainMenu menuHelper,Func<string,string,string,bool> hasAccess)
        {
            var hasMenuItemBeforeSeparator = false;
            var builder = new StringBuilder();

            var finalMenus = new List<MenuItem>();
            var menus = GetMenuItems(menuHelper);//.OrderBy(o => o.ModuleName);//.OrderBy(o=>o.MenuTitle);

            foreach (var menu in menus)
            {
                if (!menu.IsMenuSeparator)
                {                     
                    if (hasAccess(menu.ModuleName, menu.FunctionName,SessionManager.GetUserId().ToString()))
                    {
                        finalMenus.Add(menu);

                        hasMenuItemBeforeSeparator = true;
                    }
                }
                else
                {
                    if (hasMenuItemBeforeSeparator)
                    {
                        finalMenus.Add(menu);
                        //reset
                        hasMenuItemBeforeSeparator = false;
                    }
                }
            }

            if (finalMenus.Count > 1)
            {
                var lastItem = finalMenus[finalMenus.Count - 1];
                if (lastItem.IsMenuSeparator)
                {
                    finalMenus.RemoveAt(finalMenus.Count - 1);
                }
            }

            string previousMenuTitle = "";

            foreach (var menu in finalMenus)
            {
                if (menu.MenuTitle != previousMenuTitle)
                {
                    if (previousMenuTitle != string.Empty)
                    {
                        builder.Append(_afterSubMenuItem);
                    }

                    previousMenuTitle = menu.MenuTitle;

                    if (menu.MenuTitle != string.Empty)
                    {
                        builder.AppendFormat(_beforeSubMenuItem, menu.MenuTitle);
                    }
                }

                if (menu.IsMenuSeparator)
                {
                    builder.AppendFormat("{0}", _separator);
                }
                else
                {
                    MvcHtmlString htmlString;
                    if (menu.FunctionName.ToLower() == "logoff")
                    {
                        htmlString = page.Html.ActionLink(menu.ModuleItemText, menu.ControllerAction,
                                                                menu.ControllerName,
                                                                new { area = menu.ControllerArea }, menu.TabCaption);
                    }
                    else
                    {
                        htmlString = page.Html.CustomLinkNewTab(menu.ModuleItemText, menu.ControllerAction,
                                                                menu.ControllerName,
                                                                new { area = menu.ControllerArea }, menu.TabCaption);
                    }

                    builder.AppendFormat("{0}{1}{2}", _beforeMenuItem, htmlString, _afterMenuItem);
                }
            }

            if (previousMenuTitle != string.Empty)
            {
                builder.Append(_afterSubMenuItem);
            }

            if (finalMenus.Count <= 0)
            {
                const string blank = "";
                return new MvcHtmlString(blank);
            }
            const string template = "<li class='dropdown'> "
                                    + "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>{0} <b class='caret'></b></a> "
                                    + "<ul class='dropdown-menu'> "
                                    + "  {1}"
                                    + "</ul> "
                                    + "</li>";

            var menuTitle = menuHelper.DisplayName;
            if (menuTitle.ToLower() == "system")
            {
                var virtDir = CommonHelper.Instance.GetNormalizedVirtualDir();
                menuTitle = string.Format("<img src='{0}/Resources/images/gear.jpg' style='width: 24px;'>", virtDir);
            }
            var html = string.Format(template, menuTitle, builder);

            return new MvcHtmlString(html);
        }

        private static List<MenuItem> GetMenuItems(IMenuHelper menuHelper)
        {
            var list = new List<MenuItem>();
            list.AddRange(menuHelper.GetMenuItems());

            return list;
        }
    }
}