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
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.EmailTemplates;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// Used for viewing rooms that have already been reserved and the details
    /// </summary>
    public partial class RoomReservationDetails : BasePage
    {
        #region Variables

        /// <summary>
        ///     Gets or sets the room reservation id.
        /// </summary>
        /// <value>
        ///     The room reservation id.
        /// </value>
        private int RoomReservationId
        {
            get
            {
                if (ViewState["RoomReservationId"] == null)
                    return -1;
                return (int) ViewState["RoomReservationId"];
            }
            set { ViewState["RoomReservationId"] = value; }
        }

        /// <summary>
        ///     Gets or sets the room poster mode.
        /// </summary>
        /// <value>
        ///     The room poster mode.
        /// </value>
        private int RoomPosterMode
        {
            get
            {
                if (ViewState["RoomPosterMode"] == null)
                    ViewState["RoomPosterMode"] = 0;
                return (int) ViewState["RoomPosterMode"];
            }
            set { ViewState["RoomPosterMode"] = value; }
        }

        /// <summary>
        /// Gets or sets the reviewer user id.
        /// </summary>
        /// <value>
        /// The reviewer user id.
        /// </value>
        private int ReviewerUserId
        {
            get
            {
                if (ViewState["ReviewerUserId"] == null)
                    return -1;
                return (int) ViewState["ReviewerUserId"];
            }
            set { ViewState["ReviewerUserId"] = value; }
        }

        private bool _isPdfExport;

        /// <summary>
        ///     Gets or sets the appointments.
        /// </summary>
        /// <value>
        ///     The appointments.
        /// </value>
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
                ViewState["Appointments"] = AppointmentUtilities.GetAllReservedObjectsByReservationId(ref db, Utilities.GetQueryStringInt("RoomReservationID"));
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
            InitPage("Room Reservation Details", MainTabs.MyAccount);

            if (Page.IsPostBack) return;

            var roomReservationId = Utilities.GetQueryStringInt("RoomReservationID");
            if (roomReservationId <= -1)
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/default.aspx?message={0}&messageType={1}", "Invalid Room Reservation", FeedbackType.Error));
                return;
            }



            RoomReservationId = roomReservationId;

            var db = new UrbanDataContext();
            //Load reservation information
            var reservation = db.Manager.RoomReservation.GetByKey(RoomReservationId);

            _litComments.Text = RoomReservationUtilities.ConvertCommentsToString(ref reservation);
            _txtRoomNumber.Text = reservation.Room.Number;
            _txtBuildingName.Text = reservation.Room.Building.Name;
            _txtReservationStatus.Text = reservation.Approved == null ? "Pending" : reservation.Approved.ToString();
            _rdpRequestedDate.DbSelectedDate = reservation.RequestedDate;

            ReviewerUserId = reservation.ReserverUserID;

            //Set reservered user information
            if (reservation.ReserverUserID == Cu.Id)
            {
                RoomPosterMode = (int) PosterModeEnum.RoomRequestor;
                _btnApprove.Visible = false;
                _btnApprove.Enabled = false;
                _btnDeny.Visible = false;
                _btnDeny.Enabled = false;
            }

            if (reservation.Room.UserID == Cu.Id)
            {
                RoomPosterMode = (int) PosterModeEnum.RoomPoster;
              
            }

            if (reservation.Approved == null) return;

            //if approval already set then disable buttons.
            _btnSubmitComments.Enabled = false;
            _btnSubmitComments.Visible = false;
            _btnApprove.Enabled = false;
            _btnApprove.Visible = false;
            _btnDeny.Enabled = false;
            _btnDeny.Visible = false;
            _roomCommentsHeader.Visible = false;
            _roomCommentsHeader.Visible = false;
            _actionHeader.Visible = false;
            _action.Visible = false;

           
            _rsReservations.Rebind();
            _rgRequestedTimes.Rebind();
        }
      
        #region RequestedFunctions

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgRequestedTimes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var results = db.Manager.RoomReservationDates.GetByRoomReservationID(RoomReservationId).ToList();
            _rgRequestedTimes.DataSource = results;
        }

        /// <summary>
        ///     Handles the OnItemDataBound event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgRequestedTimes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!(e.Item is GridDataItem)) return;
            //Get the instance of the right type
            var r = e.Item as GridDataItem;
            if (r["StartTime"].Text != String.Empty)
            {
                DateTime startTime;
                if (DateTime.TryParse(r["StartTime"].Text, out startTime))
                {
                    r["StartTime"].Text = startTime.ToShortTimeString();
                }
            }
            if (r["EndTime"].Text == String.Empty) return;
            DateTime endTime;
            if (DateTime.TryParse(r["EndTime"].Text, out endTime))
            {
                r["EndTime"].Text = endTime.ToShortTimeString();
            }
        }


        /// <summary>
        /// Handles the ItemCommand event of the _rgSearchResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void _rgRequestedTimes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
               
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;
                case RadGrid.RebindGridCommandName:
                    _rgRequestedTimes.Rebind();
                    break;
            }
        }

        /// <summary>
        ///     Handles the ItemCreated event of the _rgSearchResults control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgRequestedTimes_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (_isPdfExport)
                Utilities.GridFunctions.ReportExport.FormatGridItem(e.Item);
        }

        #endregion RequestedFunctions

        #region Actions

        /// <summary>
        ///     Handles the Click event of the _btnSubmitComments control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnSubmitComments_Click(object sender, EventArgs e)
        {
            if (_rContent.Content.Trim() == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Info, "Please enter comments to send");
                return;
            }

            //Create new Reservation Comments
            var db = new UrbanDataContext();
            var revComments = new RoomReservationComments
                                  {
                                      Comments = _rContent.Content.Trim(),
                                      DateSent = DateTime.Now,
                                      RoomReservationID = RoomReservationId,
                                      UserID = Cu.Id
                                  };
            db.RoomReservationComments.InsertOnSubmit(revComments);
            db.SubmitChanges();

            var roomReservation = db.Manager.RoomReservation.GetByKey(RoomReservationId);
            var user = db.Manager.User.GetByKey(Cu.Id);

            //Emails based on who is the poster
            switch (RoomPosterMode)
            {
                case (int) PosterModeEnum.RoomPoster:
                    RoomReservationEmailUtilities.RoomReservationCommentsSentRoomPoster(roomReservation.Room, user, roomReservation.Id, _rContent.Content);
                    break;
                case (int) PosterModeEnum.RoomRequestor:
                    RoomReservationEmailUtilities.RoomReservationCommentsSentRoomRequestor(roomReservation.Room, user, roomReservation.Id, _rContent.Content);
                    break;
            }
            _rContent.Content = String.Empty;
            WriteFeedBackMaster(FeedbackType.Success, "Comments sent");
        }

        /// <summary>
        ///     Handles the Click event of the _btnApprove control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnApprove_Click(object sender, EventArgs e)
        {
            if (RoomPosterMode == (int) PosterModeEnum.RoomPoster)
            {
                var db = new UrbanDataContext();
                var reserver = db.Manager.RoomReservation.GetByKey(RoomReservationId);
                var user = db.Manager.User.GetByKey(reserver.ReserverUserID);
                reserver.Approved = true;
                db.SubmitChanges();
                //Set status to approved and email and redirect.
                RoomReservationEmailUtilities.RoomReservationApproved(reserver.Room, user, RoomReservationId, _rContent.Content);
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("MyAccount.aspx?message={0}&messageType={1}", "Room Request Approved, Email Sent", FeedbackType.Success));
            }
            else RedirectDefaultInvalid();
        }

        /// <summary>
        ///     Handles the Click event of the _btnDeny control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnDeny_Click(object sender, EventArgs e)
        {
            if (RoomPosterMode == (int) PosterModeEnum.RoomPoster)
            {
                var db = new UrbanDataContext();
                var reserver = db.Manager.RoomReservation.GetByKey(RoomReservationId);
                var user = db.Manager.User.GetByKey(reserver.ReserverUserID);
                reserver.Approved = true;
                db.SubmitChanges();
                //Set status to approved and email and redirect.
                RoomReservationEmailUtilities.RoomReservationDenied(reserver.Room, user, RoomReservationId, _rContent.Content);
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("MyAccount.aspx?message={0}&messageType={1}", "Room Request Denied, Email Sent", FeedbackType.Success));
            }
            else RedirectDefaultInvalid();
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

        #region Misc

        /// <summary>
        ///     Redirects the default invalid.
        /// </summary>
        private void RedirectDefaultInvalid()
        {
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/default.aspx?message={0}&messageType={1}", "Invalid Room Reservation", FeedbackType.Error));
        }

        #endregion Misc

        #region CalendarCode

        /// <summary>
        ///     Handles the OnNavigationComplete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerNavigationCompleteEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;
            ViewState["Appointments"] = AppointmentUtilities.GetAllReservedObjectsByReservationId(ref db, RoomReservationId);
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
            if (((AppointmentObj)e.Appointment.DataItem).Busy == true)
            {
                e.Appointment.CssClass = "rsCategoryRed";
            }
        }

        #endregion CalendarCode
    }
}