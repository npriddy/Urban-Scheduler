#region

using System.Web.UI;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.MasterPages;

#endregion

namespace UrbanSchedulerProject.Code.BasePage
{
    /// <summary>
    ///     Inherits user control assists in user control security
    /// </summary>
    public class BaseUserControl : UserControl
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref = "BasePage" /> class.
        /// </summary>
        protected BaseUserControl()
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

        #region FeedBackFunctions

        /// <summary>
        ///     Writes the feed back master detects what master page is used
        /// </summary>
        /// <param name = "type">The type.</param>
        /// <param name = "message">The message.</param>
        /// <author>nate</author>
        /// <datetime>8/13/2011-10:55 AM</datetime>
        protected void WriteFeedBackMaster(string type, string message)
        {
            if (Page.Master is Site)
            {
                var master = (Site) Page.Master;
                master.WriteFeedBackMaster(type, message);
            }
            else if (Page.Master is Popup)
            {
                var master = (Popup) Page.Master;
                master.WriteFeedBackMaster(type, message);
            }
        }

        #endregion FeedBackFunctions
    }
}