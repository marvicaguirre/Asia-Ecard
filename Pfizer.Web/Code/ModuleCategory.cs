using System;
using System.Collections.Generic;
using System.Linq;
using Wizardsgroup.Utilities.Helpers;

namespace Pfizer.Web.Code
{
    /// <summary>
    /// This is for grouping controllers into sets. Then this information will be used to determined
    /// if the set is to be displayed as readonly if viewed on other set.
    /// </summary>
    public sealed class ModuleCategory
    {
        #region Constructor

        private ModuleCategory()
        {
            //// prosal shared conroller
            //List<String> proposalAddThisController = new List<string>() { "DocumentController" };
            //proposalAddThisController.AddRange(
            //    GetControllerList(Enum.GetName(typeof(ModuleSetArea), ModuleSetArea.Proposal)));

            //// franchising shared controoler
            //List<String> franchiseAddThisController = new List<string>() { "DocumentController" };
            //franchiseAddThisController.AddRange(
            //    GetControllerList(Enum.GetName(typeof(ModuleSetArea), ModuleSetArea.Franchising)));

            //ModuleSet = new List<ControllerSet>()
            //{
            //    // This is for Franchise
            //    new ControllerSet()
            //    {
            //        Area = ModuleSetArea.Franchising,
            //        TabTextList = new List<string>() {"Franchise", "Franchise Contest", "Broker"},
            //        ControllerList = franchiseAddThisController
            //    },

            //    // This is for Proposal
            //    new ControllerSet()
            //    {
            //        Area = ModuleSetArea.Proposal,
            //        TabTextList = new List<string>() {"Proposal Request", "Package Builder", "Proposal Tagged As Closed"},
            //        ControllerList = proposalAddThisController
            //    }

            //    // This is for Contract
            //    ,
            //    new ControllerSet()
            //    {
            //        Area = ModuleSetArea.Contract,
            //        TabTextList = new List<string>() {"Contract Request", "Templates"},
            //        ControllerList = GetControllerList(Enum.GetName(typeof (ModuleSetArea), ModuleSetArea.Contract))
            //    }
            //};
        }

        #endregion
        #region Public methods & functions

        /// <summary>
        /// Module set information
        /// </summary>
        private readonly List<ControllerSet> ModuleSet = new List<ControllerSet>();

        /// <summary>
        /// This will verify if the current controller is listed within the ModuleSet of the current TabTextStrip
        /// </summary>
        /// <param name="activeTabStripText"></param>
        /// <param name="currentControllerName"></param>
        /// <returns></returns>
        public Boolean IsActiveTab(String activeTabStripText, String currentControllerName)
        {
            Boolean result = true;

            // check if currentControllerName is defined within ModuleSet, we just want process
            // controllers that are defined. not defined controllers are untouched
            if (currentControllerName != string.Empty && (from controller in ModuleSet 
                                                          where controller.ControllerList.Contains(currentControllerName)
                                                              select controller).Any())
            {
                result = (from moduleSet in ModuleSet
                          where moduleSet.TabTextList.Contains(activeTabStripText)
                          let checkcontroller = moduleSet.ControllerList
                          where checkcontroller.Exists(o => o.ToLower().Equals((currentControllerName).ToLower()))
                          select moduleSet).Any();
            }
            return result;
        }

        /// <summary>
        /// Singleton
        /// </summary>
        public static ModuleCategory Instance
        {
            get { return Singleton<ModuleCategory>.Instance; }
        }
        #endregion


        #region members

        /// <summary>
        /// Retrieve the controllers under "area"
        /// </summary>
        /// <param name="areaname">The area name of the controller</param>
        /// <returns></returns>
        private List<String> GetControllerList(String areaname)
        {
            return (from gcontroller in MvcApplication.Global_ControllerList
                    let gcontrollerarray = gcontroller.Split('.')
                    let garea = gcontrollerarray[1]
                    let gname = gcontrollerarray.Last().Split('`').First()
                    where garea.Equals(areaname)
                    select gname).ToList();
        }

        /// <summary>
        /// Definition of set of modules / controllers. This definition will then be used to identify if the view of the controller
        /// will be displayed as viewing only, overriding the specific BU rules for each. 
        /// </summary>
        protected internal class ControllerSet
        {
            internal ControllerSet() { }

            /// <summary>
            /// This is the Area for the set
            /// </summary>
            //internal ModuleSetArea Area { get; set; }

            /// <summary>
            /// The expected "tabText" for this set. We can specify several expected values here
            /// Note: This is just an interim solution until an appropriate one is materialized
            /// </summary>
            internal List<String> TabTextList { get; set; }

            /// <summary>
            /// The all controller that is expected to be within the set.
            /// For shared controller, manually add them to the list within the constructor of this class
            /// </summary>
            internal List<String> ControllerList { get; set; }
        }

        #endregion
    }
}