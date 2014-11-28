using System;
using Wizardsgroup.Domain.Attributes;

namespace Pfizer.Domain.ViewModels
{
    public abstract class AbstractBaseViewModel
    {
        public AbstractBaseViewModel()
        {
            ReadOnlyView = null;
        }

        /// <summary>
        /// When this is set to True, the View will be rendered in UI as readonly.
        /// </summary>
        [ColumnDescription("When this is set to True, the View will be rendered in UI as readonly.")]
        public Boolean? ReadOnlyView { get; set; }
    }
}
