#region

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// Allows user to reserve room
    /// </summary>
    public partial class RoomReservationPage : BasePage
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

        /// <summary>
        /// Gets or sets the reserved appt.
        /// </summary>
        /// <value>
        /// The reserved appt.
        /// </value>
        private List<ReserveRoomTempObject> ReservedAppt
        {
            get
            {
                if(ViewState["ReservedAppt"] == null)
                    ViewState["ReservedAppt"] = new List<ReserveRoomTempObject>();
                return (List<ReserveRoomTempObject>) ViewState["ReservedAppt"];
            }
            set { ViewState["ReservedAppt"] = value; }
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
            Title = "Reserve Room";

            RoomId = Utilities.GetQueryStringInt("roomId");
            if (RoomId <= -1)
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid room", FeedbackType.Warning));
                return;
            }
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);
            if (room == null)
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid room", FeedbackType.Warning));
                return;
            }

            _lblRoomNumber.Text = room.Number;
        }

        /// <summary>
        ///     Handles the Click event of the _btnSubmitReservationRequest control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnSubmitReservationRequest_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var result = RoomReservationUtilities.CreateRoomReservation(Cu.Id, RoomId, _rContent.Content,ReservedAppt);
            if (result > 0)
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("/default.aspx?message={0}&messageType={1}", "Room reservation request sent room poster has been notified", FeedbackType.Success));
            }
            else
            {
                WriteFeedBackMaster(FeedbackType.Error, "Please select at least one reserved date.");
            }
        }

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
            Appointments = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId")).ToList();
            _rsReservations.DataSource = Appointments;
            _rsReservations.Rebind();
        }

        /// <summary>
        ///     Handles the AppointmentInsert event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerCancelEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentInsert(object sender, SchedulerCancelEventArgs e)
        {
            Appointments.Add(new AppointmentObj(e.Appointment));
        }

        /// <summary>
        ///     Handles the AppointmentUpdate event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.AppointmentUpdateEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            var ai = FindById(e.ModifiedAppointment.ID);
            ai.CopyInfo(e.ModifiedAppointment);
        }

        /// <summary>
        ///     Handles the AppointmentDelete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerCancelEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentDelete(object sender, SchedulerCancelEventArgs e)
        {
            Appointments.Remove(FindById(e.Appointment.ID));
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
        ///     Finds the by id.
        /// </summary>
        /// <param name = "id">The id.</param>
        /// <returns></returns>
        private AppointmentObj FindById(object id)
        {
            return Appointments.FirstOrDefault(ai => ai.Id.Equals(id));
        }

        #endregion CalendarCode

        /// <summary>
        /// Handles the Click event of the _btnAddDates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnAddDates_Click(object sender, EventArgs e)
        {
            var date = (DateTime)_rdpDate.DbSelectedDate;

            var startTime = ((DateTime)_rtpStartTime.DbSelectedDate).TimeOfDay;
            var endTime = ((DateTime)_rtpEndTime.DbSelectedDate).TimeOfDay;

            var appt = new AppointmentObj
                           {
                               Busy = true,
                               
                               Days = String.Empty,
                               End = date.Add(endTime),
                               Start = date.Add(startTime),
                           };

            var isVald = GetIsValdAddDates(startTime, endTime);

            if (isVald == false)
                return;

            foreach(var d in ReservedAppt)
            {
                if((d.ToAppointmentObj().Start == appt.Start && d.ToAppointmentObj().End == appt.End) || (d.ToAppointmentObj().Start.Date == date && !appt.AllowedToAdd(d.ToAppointmentObj())))
                {
                    WriteToBothFeedback(FeedbackType.Error, "Date conflicts with existing reserved date");
                    return;
                }
            }

            var db = new UrbanDataContext();
            var currentAvailabilityDates = RoomAvailabilityUtilities.GetAppointmentObjsWithRecurring(ref db, DateTime.MinValue, DateTime.MaxValue, RoomId).Where(t => t.Start.Date == date && t.End.Date == date).ToList();
            var validToAdd = false;
           foreach(var c in currentAvailabilityDates)
           {
               if(c.Busy == false && appt.AllowedToAdd(c))
               {
                   validToAdd = true;
               }
               else if(c.Busy == true && appt.OverlapsWith(c))
               {
                   validToAdd = false;
                   break;
               }
           }

            if (validToAdd)
            {
                WriteToBothFeedback(FeedbackType.Success, "Date Added");
                ReservedAppt.Add(new ReserveRoomTempObject{Date = date,End = endTime,Start = startTime});
            }
            else
            {
                WriteFeedBackMaster(FeedbackType.Error,"Date is not valid or overlaps with another date");
            }


            _rgAvailableTimes.Rebind();
        }

        /// <summary>
        /// Gets the is vald add dates.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        private bool GetIsValdAddDates(TimeSpan startTime, TimeSpan endTime)
        {
            var isVald = true;
            if (startTime > endTime)
            {
                WriteToBothFeedback(FeedbackType.Error, "Start time is greater then end date");
                isVald = false;
            }

            if (endTime < startTime)
            {
                WriteToBothFeedback(FeedbackType.Error, "End time is less then start date");
                isVald = false;
            }
            return isVald;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<param name="message"></param>
        private void WriteToBothFeedback(string type, string message)
        {
            ucDateFeedBack.InsertFeedBack(type,message);
            WriteFeedBackMaster(type,message);
        }
            
        #region AvailabilityFunctions

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rgAvailableTimes.DataSource = ReservedAppt;
        }

        /// <summary>
        ///     Handles the OnItemDataBound event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!(e.Item is GridDataItem)) return;
            //Get the instance of the right type
            var r = e.Item as GridDataItem;
            if (r["Start"].Text != String.Empty)
            {
                DateTime startTime;
                if (DateTime.TryParse(r["Start"].Text, out startTime))
                {
                    r["Start"].Text = startTime.ToShortTimeString();
                }
            }
            if (r["End"].Text == String.Empty) return;
            DateTime endTime;
            if (DateTime.TryParse(r["End"].Text, out endTime))
            {
                r["End"].Text = endTime.ToShortTimeString();
            }
        }

        private bool _isPdfExport;
        /// <summary>
        ///     Handles the ItemCommand event of the _rgMyRooms control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.DeleteCommandName:
                    {
                        var g = (GridDataItem)e.Item;

                        var item = ReservedAppt.Find(t => t.Id == new Guid(g["ID"].Text));
                        if (item != null)
                            ReservedAppt.Remove(item);
                        WriteToBothFeedback(FeedbackType.Success,"Date removed.");
                        break;
                    }
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;

            }
        }


        #endregion

        /// <summary>
        /// Handles the Click event of the _btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnBack_Click(object sender, EventArgs e)
        {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("MyAccount.aspx"));
        }
    
    }
}