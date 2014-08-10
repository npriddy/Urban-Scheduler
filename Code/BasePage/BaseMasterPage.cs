#region

using System;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;
using UrbanSchedulerProject.App.Pages;
using UrbanSchedulerProject.App.Popup;
using UrbanSchedulerProject.App.UserControl;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.Code.BasePage
{
    /// <summary>
    ///     Used to assit all master pages
    /// </summary>
    public class BaseMasterPage : MasterPage
    {
        protected ucFeedback UcFeedbackBasePage;

        ///<summary>
        ///    Base Master Page Creation
        ///</summary>
        protected BaseMasterPage()
        {
            Init += BasePage_PreInit;
        }

        /// <summary>
        ///     Selects tab from menu based on page and selected tab passed in.
        /// </summary>
        /// <param name = "selectedTab">The selected tab.</param>
        public void SelectTab(MainTabs selectedTab)
        {
            var tabStrip = (RadTabStrip) FindControl("_rtsMenu");
            if (!tabStrip.Visible || !tabStrip.Enabled) return;
            var strTabValue = String.Empty;
            switch (selectedTab)
            {
                case MainTabs.Home:
                    {
                        strTabValue = "Home";
                        break;
                    }
                case MainTabs.FindARoom:
                    {
                        strTabValue = "FindRoom";
                        break;
                    }
                case MainTabs.PostARoom:
                    {
                        strTabValue = "PostRoom";
                        break;
                    }
                case MainTabs.MyAccount:
                    {
                        strTabValue = "MyAccount";
                        break;
                    }
            }
            if (strTabValue != String.Empty)
                tabStrip.FindTabByValue(strTabValue).Selected = true;
        }

        /// <summary>
        ///     Handles the PreInit event of the BasePage control and security
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        private void BasePage_PreInit(object sender, EventArgs e)
        {
            //Checks if the current user has expired and if so then redirect them to login.
            var redirectCheck = false;
            if (HttpContext.Current.Session == null)
                redirectCheck = true;
            else if (CurrentUserUtilities.GetCurrentUser() == null)
                redirectCheck = true;

            if (!redirectCheck) return;

            //If user is on these pages it is not required that they are authenticated.
            if (Page is Default || Page is FindARoom || Page is PostARoom || Page is RoomDetails || Page is pUserCreation || Page is SchedulePrint)
                return;

            Response.Redirect(string.Format("~/Default.aspx?message=Logged out due to inactivity&ReturnUrl={0}", HttpUtility.UrlEncode(Request.Url.AbsolutePath)));
        }


        /// <summary>
        ///     Writes the feed back master.
        ///     Helps with assiting base page to display messages to users on page.
        /// </summary>
        /// <param name = "type">The type.</param>
        /// <param name = "message">The message.</param>
        /// <author>nate</author>
        /// <datetime>6/17/2011-10:42 AM</datetime>
        public void WriteFeedBackMaster(string type, string message)
        {
            if (type != FeedbackType.Warning && type != FeedbackType.Success && type != FeedbackType.Info && type != FeedbackType.Error)
                return;
            UcFeedbackBasePage = (ucFeedback) FindControl("ucFeedbackBasePage");
            if (UcFeedbackBasePage != null)
                UcFeedbackBasePage.InsertFeedBack(type, message);
        }
    }
}