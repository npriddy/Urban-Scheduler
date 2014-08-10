#region

using System;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// </summary>
    public partial class BuildingEdit : BasePage
    {

        #region Variables

        /// <summary>
        ///     Gets or sets the room id.
        /// </summary>
        /// <value>
        ///     The room id.
        /// </value>
        private int BuildingId
        {
            get
            {
                if (ViewState["BuildingId"] == null)
                    return -1;
                return (int) ViewState["BuildingId"];
            }
            set { ViewState["BuildingId"] = value; }
        }

        #endregion Variables

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildingId = Utilities.GetQueryStringInt("BuildingId");

            InitPage(BuildingId > 0 ? "Edit Building Details" : "Create Building", MainTabs.MyAccount);

            if (Page.IsPostBack) return;

            if (BuildingId < 0)
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid Building", FeedbackType.Error));
            var db = new UrbanDataContext();
            var building = db.Manager.Building.GetByKey(BuildingId);

            if (CurrentUserUtilities.GetCuIdSafely() <= 0 || (building != null && building.UserID != Cu.Id))
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid Building", FeedbackType.Error));
            }
            _cbState.LoadXml(Utilities.GetXmlForPath(Utilities.StatePathWithNoneXml));
            LoadPageFields(building);
        }

        /// <summary>
        ///     Loads the page fields.
        /// </summary>
        /// <param name = "b">The b.</param>
        private void LoadPageFields(Building b)
        {
            if (b != null)
            {
                _txtBuildingName.Text = b.Name;
                _txtPrimaryAddress.Text = b.PrimaryAddress;
                _txtSecondaryAddress.Text = b.SecondaryAddress;
                var li = _cbState.FindItemByValue(b.State);
                if (li != null) li.Selected = true;
                _txtCity.Text = b.City;
                _txtZip.Text = b.Zip;
            }
            else
            {
                _btnSave.Text = "Create Building";
            }
        }

        #region Actions

        /// <summary>
        ///     Handles the Click event of the _btnSave control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnSave_Click(object sender, EventArgs e)
        {
            var db = new UrbanDataContext();
            Building building;
            if (BuildingId > 0)
            {
                building = db.Manager.Building.GetByKey(BuildingId);
            }
            else
            {
                building = new Building {UserID = Cu.Id};
                db.Building.InsertOnSubmit(building);
            }

            building.Name = _txtBuildingName.Text.Trim() == String.Empty ? null : _txtBuildingName.Text.Trim();
            building.PrimaryAddress = _txtPrimaryAddress.Text.Trim() == String.Empty ? null : _txtPrimaryAddress.Text.Trim();
            building.SecondaryAddress = _txtSecondaryAddress.Text.Trim() == String.Empty ? null : _txtSecondaryAddress.Text.Trim();
            building.City = _txtCity.Text.Trim() == String.Empty ? null : _txtCity.Text.Trim();
            building.Zip = _txtZip.Text.Trim() == String.Empty ? null : _txtZip.Text.Trim();
            building.State = _cbState.SelectedValue == "NULL" ? null : _cbState.SelectedValue;

            var existingBuilding = BuildingUtilities.DoesBuildingAlreadyExist(ref db, building.Name, building.PrimaryAddress, building.SecondaryAddress, building.City, building.Zip, building.State, Cu.Id);
            if (existingBuilding != null && existingBuilding.Id != building.Id)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Building already exists with this information");
                return;
            }

            if (BuildingUtilities.DoesBuildingAlreadyExistNotForUser(ref db, building.PrimaryAddress, building.SecondaryAddress, building.City, building.Zip, building.State, Cu.Id))
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Building already exists with this information for another user");
                return;
            }

            db.SubmitChanges();

            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/App/Pages/MyAccount.aspx?message={0}", BuildingId > 0 ? "building updated" : "building created"));
        }

        /// <summary>
        ///     Handles the Click event of the _btnCancel control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnCancel_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/MyAccount.aspx");
        }


        /// <summary>
        /// Handles the Click event of the _btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("MyAccount.aspx"));
        }

        #endregion Actions
    }
}