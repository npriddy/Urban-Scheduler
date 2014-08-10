#region

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.SerializableClasses;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// Page for users to search for rooms
    /// </summary>
    public partial class FindARoom : BasePage
    {
        #region Variables

        /// <summary>
        /// Exporting pdf flag
        /// </summary>
        private bool _isPdfExport;

        #endregion Variables

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("Find A Room", MainTabs.FindARoom);
            if (Page.IsPostBack) return;

            _cbState.LoadXml(Utilities.GetXmlForPath(Utilities.StatePathWithNoneXml));
            _cbState.FindItemByValue("NULL").Selected = true;

            //Handle loading cities
            var cList = RoomUtilities.GetDistinctCities().ToList();
            _cbCity.DataSource = cList;
            _cbCity.DataBind();
            if (cList.Count <= 0)
            {
                _btnViewAllCities.Visible = false;
                _btnViewAllCities.Enabled = false;
            }

            _btnViewResultsOnMap.Visible = false;
            _btnViewResultsOnMap.Enabled = false;
        }

        #region SearchFunctions

        /// <summary>
        ///     Handles the Click event of the _btnSearch control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnSearch_Click(object sender, EventArgs e)
        {
            if (_txtZip.Text.Trim() == String.Empty && _cbCity.SelectedValue == "NULL" && _cbState.SelectedValue == "NULL")
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Please select at least one field to search by");
                return;
            }

            var zip = _txtZip.Text.Trim() != String.Empty ? _txtZip.Text.Trim() : String.Empty;
            var city = _cbCity.SelectedValue != "NULL" ? _cbCity.SelectedValue : String.Empty;
            var state = _cbState.SelectedValue != "NULL" ? _cbState.SelectedValue : String.Empty;
            var startDate = (DateTime?)_rdpStart.DbSelectedDate;
            var endDate = (DateTime?)_rdpEnd.DbSelectedDate;

            var db = new UrbanDataContext();
            //Loads all rooms based on search queries
            var list = db.Manager.Room.GetAllForSearch(zip, city, state,startDate,endDate);
            _rgSearchResults.DataSource = list;
            _rgSearchResults.Rebind();

            if (list.Count <= 0) return;
            _btnViewResultsOnMap.Visible = true;
            _btnViewResultsOnMap.Enabled = true;
        }

        /// <summary>
        ///     Handles the Click event of the _viewResultsOnMap control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _viewResultsOnMap_Click(object sender, EventArgs e)
        {
            var results = new Dictionary<Building, List<Room>>();
            var db = new UrbanDataContext();
            foreach (GridDataItem item in _rgSearchResults.Items)
            {
                int itemId;
                if (!int.TryParse(item["Id"].Text, out itemId) || itemId <= 0) continue;

                var r = db.Manager.Room.GetByKey(itemId);
                List<Room> rList;
                if (results.TryGetValue(r.Building, out rList) && rList != null)
                {
                    rList.Add(r);
                    results[r.Building] = rList;
                }
                else
                {
                    results[r.Building] = new List<Room> {r};
                }
            }

            var buldingObj = results.Select(r => new BuildingSerialObj(r.Key, r.Value)).ToList();
            var jsonString = buldingObj.ToJson();
            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(String.Format("return ShowResults('{0}','{1}');", jsonString, true));
        }

        #endregion SearchFunctions

        #region GridFunctions

        /// <summary>
        ///     Handles the ItemDataBound event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgSearchResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
        }

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgSearchResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (_rgSearchResults.DataSource != null) return;

            var db = new UrbanDataContext();

            var zip = _txtZip.Text.Trim() != String.Empty ? _txtZip.Text.Trim() : String.Empty;
            var city = _cbCity.SelectedValue != "NULL" ? _cbCity.SelectedValue : String.Empty;
            var state = _cbState.SelectedValue != "NULL" ? _cbState.SelectedValue : String.Empty;
            var startDate = (DateTime?)_rdpStart.DbSelectedDate;
            var endDate = (DateTime?)_rdpEnd.DbSelectedDate;


            if (zip == String.Empty && city == String.Empty && state == String.Empty)
                _rgSearchResults.DataSource = new List<Room>();
            else
            {
                var resultList = db.Manager.Room.GetAllForSearch(zip, city, state,startDate,endDate).ToList();
                if (resultList.Count <= 0)
                {
                    _btnViewResultsOnMap.Visible = true;
                    _btnViewResultsOnMap.Enabled = true;
                }
                else
                {
                    _btnViewResultsOnMap.Visible = false;
                    _btnViewResultsOnMap.Enabled = false;
                }
                _rgSearchResults.DataSource = resultList;
            }
        }

        /// <summary>
        ///     Handles the ItemCommand event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgSearchResults_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Location":
                    {
                        int itemId;
                        if (int.TryParse(e.CommandArgument.ToString(), out itemId) && itemId > 0)
                        {
                            var db = new UrbanDataContext();
                            var room = db.Manager.Room.GetByKey(itemId);
                            var building = new BuildingSerialObj(room);
                            var address = building.ToJson();
                            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(String.Format("return ShowAddressLocation('{0}','{1}');", address, false));
                        }
                    }
                    break;
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
                case RadGrid.RebindGridCommandName:
                    _rgSearchResults.Rebind();
                    break;
                case RadGrid.FilterCommandName:
                    break;
                case "ViewDetails":
                    {
                        if (e.CommandArgument != null && (string) e.CommandArgument != String.Empty)
                            RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/RoomDetails.aspx?roomId=" + e.CommandArgument);
                    }
                    break;
            }
        }

        /// <summary>
        ///     Handles the Click event of the _btnViewAllCities control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnViewAllCities_Click(object sender, EventArgs e)
        {
            var citiesList = RoomUtilities.GetDistinctCities();
            var cList = citiesList.Select(c => new CitySerialObj(c.Text)).ToList();
            var json = cList.ToJson();
            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(String.Format("return ShowAddressLocation('{0}','{1}');", json, true));
        }

        /// <summary>
        ///     Handles the ItemCreated event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgSearchResults_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (_isPdfExport)
                Utilities.GridFunctions.ReportExport.FormatGridItem(e.Item);
        }

        #endregion GridFunctions

        protected void _cbState_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            var cList = RoomUtilities.GetDistinctCitiesByState(e.Value).ToList();
            _cbCity.DataSource = cList;
            _cbCity.DataBind();
            if (cList.Count <= 0)
            {
                _btnViewAllCities.Visible = false;
                _btnViewAllCities.Enabled = false;
            }

        }
    }
}