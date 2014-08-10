#region

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// Used for dispalying user account information
    /// </summary>
    public partial class MyAccount : BasePage
    {
        #region Variables
        /// <summary>
        /// PDF Export bool
        /// </summary>
        private bool _isPdfExport;


        /// <summary>
        ///     Gets the appointments.
        /// </summary>
        private List<AppointmentObj> Appointments
        {
            get
            {
                var sessApts = ViewState["Appointments"] as List<AppointmentObj>;
                return sessApts;
            }

            set { ViewState["Appointments"] = value; }
        }

        #endregion Variables

        /// <summary>
        ///     Raises the <see cref = "E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name = "e">An <see cref = "T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!IsPostBack)
            {
                var db = new UrbanDataContext();

                ViewState["Appointments"] = AppointmentUtilities.GetAllReservedObjectsByDateRangeAndUserId(ref db, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), Cu.Id);
            }

            _rsReservations.DataSource = Appointments;
        }

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("My Account", MainTabs.MyAccount);
        }

        /// <summary>
        ///     Processes my data item.
        /// </summary>
        /// <param name = "myValue">My value.</param>
        /// <returns></returns>
        protected static string ProcessMyDataItem(object myValue)
        {
            return myValue == null ? "Pending" : myValue.ToString();
        }

        #region MyRooms

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgMyRooms control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgMyRooms_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            _rgMyRooms.DataSource = db.Manager.Room.GetByUserId(Cu.Id);
        }

        /// <summary>
        ///     Handles the ItemCommand event of the _rgMyRooms control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgMyRooms_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.RebindGridCommandName:
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
                case "EditDetails":
                    RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/EditRoomDetails.aspx?roomId=" + e.CommandArgument);
                    break;
                case "ViewDetails":
                    RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/RoomDetails.aspx?roomId=" + e.CommandArgument);
                    break;
            }
        }


        /// <summary>
        ///     Handles the ItemCreated event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void SharedFunction_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (_isPdfExport)
                Utilities.GridFunctions.ReportExport.FormatGridItem(e.Item);
        }


        /// <summary>
        ///     Handles the ItemCreated event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgMyRooms_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (!_isPdfExport) return;
            Utilities.GridFunctions.ReportExport.FormatGridItem(e.Item);
            if (e.Item is GridDataItem)
            {
                ((GridDataItem) e.Item)["description"].Text = Utilities.StripHtmlTextAndTags(((GridDataItem) e.Item)["description"].Text);
            }
        }

        #endregion MyRooms

        #region MyBuildings

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgMyBuildings control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgMyBuildings_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var list = db.Manager.Building.GetByUserID(Cu.Id).ToList();

            _rgMyBuildings.DataSource = list;
        }

        /// <summary>
        ///     Handles the ItemCommand event of the _rgMyBuildings control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgMyBuildings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.RebindGridCommandName:
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
                case "ViewDetails":
                    RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/BuildingEdit.aspx?buildingId=" + e.CommandArgument);
                    break;
                case RadGrid.InitInsertCommandName:
                    {
                        RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/BuildingEdit.aspx?buildingId=" + 0);
                        e.Canceled = true;
                    }
                    break;
            }
        }

        #endregion MyBuildings

        #region Reservations

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgMyBuildings control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgReservations_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var list = db.Manager.RoomReservation.GetAllForSearch(Cu.Id, _rTabReservations.SelectedTab.Value == "Open" ? true : false);
            _rgReservations.DataSource = list;
        }

        /// <summary>
        ///     Handles the ItemCommand event of the _rgMyBuildings control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgReservations_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.RebindGridCommandName:
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
                case "ViewDetails":
                    RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/RoomReservationDetails.aspx?RoomReservationID=" + e.CommandArgument);
                    break;
            }
        }

        /// <summary>
        ///     Handles the TabClick event of the _rTabReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.RadTabStripEventArgs" /> instance containing the event data.</param>
        protected void _rTabReservations_TabClick(object sender, RadTabStripEventArgs e)
        {
            _rgReservations.Rebind();
        }

        #endregion Reservations

        #region Messages

        /// <summary>
        ///     Handles the ItemCommand event of the _rgMyBuildings control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgMessages_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.RebindGridCommandName:
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
            }
        }

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgMessages control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgMessages_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();

            //Room Comments
            var cList = db.Manager.RoomComments.GetByRoomUserId(Cu.Id);
            var mList = (from c in cList where c.DatePosted != null select new MyAccountMessageClass(c.Comments, (DateTime) c.DatePosted, c.Room.Number, c.Room.Building.Name, "Room Comments")).ToList();

            //Room Reservation Comments
            var resList = db.Manager.RoomReservationComments.GetAllForSearch(Cu.Id);
            mList.AddRange(resList.Select(rc => new MyAccountMessageClass(rc.Comments, rc.DateSent, rc.RoomReservation.Room.Number, rc.RoomReservation.Room.Building.Name, "Room Reservation Comments")));

            _rgMessages.DataSource = mList;
        }

        #endregion Messages

        #region UpCommingEvents

        /// <summary>
        ///     Handles the OnNavigationComplete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerNavigationCompleteEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;
            Appointments = AppointmentUtilities.GetAllReservedObjectsByDateRangeAndUserId(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Cu.Id).ToList();
            _rsReservations.DataSource = Appointments;
            _rsReservations.Rebind();
        }


        /// <summary>
        ///     Handles the AppointmentDataBound event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            if (((AppointmentObj) e.Appointment.DataItem).Busy == true)
            {
                e.Appointment.CssClass = "rsCategoryRed";
            }
        }

        #endregion UpCommingEvents
    }
}