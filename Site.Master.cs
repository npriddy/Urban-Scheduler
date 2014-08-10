using System;
using Telerik.Web.UI;
using UrbanSchedulerProject.App.Pages;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

namespace UrbanSchedulerProject
{
    public partial class Site : BaseMasterPage
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var myAccountTab = _rtsMenu.FindTabByValue("MyAccount");
            myAccountTab.Visible = CurrentUserUtilities.GetCuIdSafely() > 0;

            if(Utilities.GetQueryStringSafe("message") != string.Empty)
            {
                WriteFeedBackMaster(Utilities.GetQueryStringSafe("messageType") != string.Empty ? Utilities.GetQueryStringSafe("messageType") : FeedbackType.Success, Utilities.GetQueryStringSafe("message"));
            }
        }

       

        /// <summary>
        /// Handles the TabClick event of the _rtsMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadTabStripEventArgs"/> instance containing the event data.</param>
        protected void _rtsMenu_TabClick(object sender, RadTabStripEventArgs e)
        {
            switch (e.Tab.Value)
            {
                case "Home":
                    {
                        RadAjaxManager1.Redirect("~/Default.aspx");
                        break;
                    }
                case "FindRoom":
                    {
                        RadAjaxManager1.Redirect("~/App/Pages/FindARoom.aspx");
                        break;
                    }
                case "PostRoom":
                    {
                        RadAjaxManager1.Redirect("~/App/Pages/PostARoom.aspx");
                        break;
                    }
                case "MyAccount":
                    {
                        RadAjaxManager1.Redirect("~/App/Pages/MyAccount.aspx");
                        break;
                    }
            }
        }
    }
}