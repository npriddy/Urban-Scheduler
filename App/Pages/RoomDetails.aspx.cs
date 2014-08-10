#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.SerializableClasses;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// Shows room details for the room
    /// </summary>
    public partial class RoomDetails : BasePage
    {
        #region Variables

        /// <summary>
        ///     Gets or sets the room id.
        /// </summary>
        /// <value>
        ///     The room id.
        /// </value>
        private int RoomId
        {
            get
            {
                if (ViewState["RoomId"] == null)
                    return -1;
                return (int) ViewState["RoomId"];
            }
            set { ViewState["RoomId"] = value; }
        }

        /// <summary>
        ///     Gets or sets the last rating.
        /// </summary>
        /// <value>
        ///     The last rating.
        /// </value>
        private decimal LastRating
        {
            get
            {
                if (ViewState["LastRating"] == null)
                    ViewState["LastRating"] = 0;
                return (decimal) ViewState["LastRating"];
            }
            set { ViewState["LastRating"] = value; }
        }

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
                //Gets the first and last day for the month
                ViewState["Appointments"] = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId"));
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
            InitPage("Room Details", MainTabs.FindARoom);

            if (CurrentUserUtilities.GetCuIdSafely() <= 0)
            {
                RadToolTip1.Enabled = false;
                _rRating.ReadOnly = true;
                _rsReservations.ReadOnly = true;
            }

            if (Page.IsPostBack) return;

            RoomId = Utilities.GetQueryStringInt("RoomId");
            if (RoomId <= 0)
                RadAjaxManager.GetCurrent(Page).Redirect("~/Default.aspx?message=Invalid room");
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);
            if (room == null)
                RadAjaxManager.GetCurrent(Page).Redirect("~/Default.aspx?message=Invalid room");

            LoadPageFields(room);
        }

        /// <summary>
        ///     Loads the page fields.
        /// </summary>
        /// <param name = "room">The room.</param>
        private void LoadPageFields(Room room)
        {
            //Room Information
            _txtRoomTitle.Text = room.Title;
            _txtRoomNumber.Text = room.Number;
            _txtMaxOccupancy.DbValue = room.MaxOccupancy;
            _txtRoomType.Text = room.RoomType.Name;

            //Address Information
            _txtBuildingName.Text = room.Building.Name;
            _txtPrimaryAddress.Text = room.Building.PrimaryAddress;
            _txtSecondaryAddress.Text = room.Building.SecondaryAddress;
            _txtState.Text = room.Building.State;
            _txtCity.Text = room.Building.City;
            _txtZip.Text = room.Building.Zip;

            //Description
            _litDescription.Text = room.Description;

            if (CurrentUserUtilities.GetCuIdSafely() > 0 && room.UserID != Cu.Id)
            {
                _btnReserve.Visible = true;
                _btnReserve.Enabled = true;
            }
            else
            {
                _btnReserve.Visible = false;
                _btnReserve.Enabled = false;
            }
            _rRating.Value = RoomCommentsUtilities.GetRatingForRoomAndComments(RoomId);
            _litComments.Text = RoomCommentsUtilities.BuildCommentsForRoom(RoomId);

            var imageLinks = room.RoomImageLinkList;

            var sb2 = new StringBuilder();

            foreach (var image in imageLinks)
            {
                sb2.Append(String.Format("<a href='{0}' rel='prettyPhoto[pp_gal]' title='{1}'>", "../../files/" + image.FileFiles.ServerFileName, image.ImageDescription == null || image.ImageDescription.Trim() == String.Empty ? "" : image.ImageDescription));
                sb2.Append(String.Format("<img src='{0}' height='100' alt='{1}' />", "../../files/" + image.FileFiles.ServerFileName, image.Title == null || image.Title.Trim() == String.Empty ? "" : image.Title));
                sb2.Append("</a>");
            }

            _litPrettyPhoto.Text = sb2.ToString();
        }

        #region RatingControlFunctions

        /// <summary>
        ///     Handles the Click event of the btnPostComment control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void btnPostComment_Click(object sender, EventArgs e)
        {
            //Will insert comments about the site here.
            if (txtComments.Text != String.Empty)
            {
                if (_litComments.Text == "Your Comments")
                {
                    _litComments.Text = string.Empty;
                }
                _litComments.Text = _litComments.Text + "Rating: <strong>" + LastRating + "</strong> <p>" + txtComments.Text + "</p><br/>";

                //Create Comment Object
                var roomRating = new RoomComments
                                     {
                                         Comments = txtComments.Text,
                                         RoomID = RoomId,
                                         UserID = Cu.Id,
                                         Score = LastRating,
                                         DatePosted = DateTime.Now
                                     };
                var db = new UrbanDataContext();
                db.RoomComments.InsertOnSubmit(roomRating);
                db.SubmitChanges();
                //Redirect to same page with comment success
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("RoomDetails.aspx?roomId={0}&message={1}", RoomId, "Comment successfully submitted."));
            }

            //Close tooltip

            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add("CloseToolTip1();");
        }

        /// <summary>
        ///     Handles the Rate event of the RadRating1 control. Sets the variable LastRating to help deal with comment inserting.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void RadRating1_Rate(object sender, EventArgs e)
        {
            LastRating = _rRating.Value;
        }

        #endregion RatingControlFunctions

        #region CalendarControlObjects

        /// <summary>
        ///     Handles the OnNavigationComplete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerNavigationCompleteEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;
            Appointments = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId")).ToList();
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

        /// <summary>
        ///     Handles the Click event of the _btnExportSchedule control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnExportSchedule_Click(object sender, EventArgs e)
        {
            _rsReservations.ExportToPdf();
        }

        #endregion CalendarControlObjects

        #region ViewLocationMap

        /// <summary>
        ///     Handles the Click event of the _btnViewLocation control. <br />
        ///     Turns the address into json object and passes to javascript function on page.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnViewLocation_Click(object sender, EventArgs e)
        {
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);
            var building = new BuildingSerialObj(room);
            var address = building.ToJson();
            RadAjaxManager.GetCurrent(Page).ResponseScripts.Add(String.Format("return ShowAddressLocation('{0}');", address));
        }

        #endregion ViewLocationMap

        #region Action

        /// <summary>
        /// Handles the Click event of the _btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);


            if(CurrentUserUtilities.GetCuIdSafely() > 0 && room.UserID == Cu.Id)
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("MyAccount.aspx"));
            else RadAjaxManager.GetCurrent(Page).Redirect("FindARoom.aspx");
        }

        /// <summary>
        /// Handles the Click event of the _btnReserve control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected  void _btnReserve_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("ReserveRoom.aspx?roomId={0}", RoomId));
        }

        #endregion Action
    }
}