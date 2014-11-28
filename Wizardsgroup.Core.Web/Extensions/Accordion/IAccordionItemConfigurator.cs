namespace Wizardsgroup.Core.Web.Extensions
{
    public interface IAccordionItemConfigurator
    {
        /// <summary>
        /// Items for.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>IAccordionItemContentConfigurator.</returns>
        IAccordionItemContentConfigurator ItemFor(string title);
    }
}