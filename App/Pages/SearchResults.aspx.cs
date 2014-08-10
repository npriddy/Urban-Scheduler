using System;
using System.Web.UI;
using Telerik.Web.UI;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Enum;

namespace UrbanSchedulerProject.App
{
    public partial class SearchResults : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("Search Results", MainTabs.FindARoom);

            RadAjaxManager manager = RadAjaxManager.GetCurrent(Page);
            manager.AjaxSettings.AddAjaxSetting(_btnSearch, manager);
            manager.AjaxSettings.AddAjaxSetting(_btnSearch, _lblCords);

            manager.AjaxRequest += manager_AjaxRequest;
        }

        protected void manager_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //handle the manager AjaxRequest event here
            if (e.Argument != null)
            {
                _lblCords.Text = e.Argument;
            }
            else _lblCords.Text = "Not Found";
        }

        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            RadAjaxManager manager = RadAjaxManager.GetCurrent(Page);
            manager.ResponseScripts.Add(string.Format("myUserControlClickHandler('{0}');", _txtSearchText.Text));
        }
    }
}