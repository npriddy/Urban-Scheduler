#region

using System.Web.UI;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;
using UrbanSchedulerProject.MasterPages;

#endregion

namespace UrbanSchedulerProject.Code.BasePage
{
    /// <summary>
    ///     Inheris page and assists with page functions interacting with master pages
    /// </summary>
    public class BasePage : Page
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref = "BasePage" /> class.
        /// </summary>
        protected BasePage()
        {
        }

        /// <summary>
        ///     Gets the cu helps with security
        /// </summary>
        /// <value>
        ///     The cu.
        /// </value>
        protected static CurrentUserObj Cu
        {
            get { return CurrentUserUtilities.GetCurrentUser(); }
        }

        /// <summary>
        ///     Used by pages to pass information about page title and tab to be selected on menu.
        /// </summary>
        /// <param name = "pageTitle">The page title.</param>
        /// <param name = "selectedTab">The selected tab.</param>
        protected void InitPage(string pageTitle, MainTabs selectedTab)
        {
            Page.Title = pageTitle;
            if (Page.Master is Site)
            {
                ((Site) Page.Master).SelectTab(selectedTab);
            }
        }

        #region FeedBackFunctions

        /// <summary>
        ///     Writes the feed back master.
        /// </summary>
        /// <param name = "type">The type.</param>
        /// <param name = "message">The message.</param>
        /// <author>nate</author>
        /// <datetime>8/13/2011-10:55 AM</datetime>
        protected void WriteFeedBackMaster(string type, string message)
        {
            if (type != FeedbackType.Warning && type != FeedbackType.Success && type != FeedbackType.Info && type != FeedbackType.Error)
                return;
            var master = Master as BaseMasterPage;
            if (master != null) master.WriteFeedBackMaster(type, message);
        }

        #endregion FeedBackFunctions
    }
}