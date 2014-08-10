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
    /// Used for adding dates to schedule control
    /// </summary>
    public partial class RoomDetailsAddDates : BasePage
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
                return (int)ViewState["RoomId"];
            }
            set { ViewState["RoomId"] = value; }
        }

        /// <summary>
        /// Gets or sets the appointments.
        /// </summary>
        /// <value>
        /// The appointments.
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
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                var db = new UrbanDataContext();
                ViewState["Appointments"] = RoomAvailabilityUtilities.GetAppointmentObjsWithRecurring(ref db, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("RoomId"));
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
            if (Page.IsPostBack) return;

            RoomId = Utilities.GetQueryStringInt("RoomId");
            if (RoomId <= 0)
                RadAjaxManager.GetCurrent(Page).Redirect("~/Default.aspx?message=Invalid room");
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);

           

            if (CurrentUserUtilities.GetCuIdSafely() <= 0 || room == null || room.UserID != Cu.Id)
            {
                RadAjaxManager.GetCurrent(Page).Redirect("~/Default.aspx?message=Invalid room");
                return;
            }

            _txtRoomNumber.Text = room.Number;
            _txtBuildingName.Text = room.Building.Name;
        }

        #region AvailabilitiesGrid

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var roomList = db.Manager.RoomAvailability.GetByRoomID(RoomId).OrderByDescending(t => t.StartDate).ToList();
            _rgAvailableTimes.DataSource = roomList;
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
        /// Handles the ItemCommand event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.GridCommandEventArgs"/> instance containing the event data.</param>
        protected void _rgAvailableTimes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    {
                        var r = e.Item as GridDataItem;
                        int itemId;

                        if(r != null && int.TryParse(r["Id"].Text, out itemId))
                        {
                            var db = new UrbanDataContext();
                            var date = db.Manager.RoomAvailability.GetByKey(itemId);

                            if(date != null)
                            {
                                db.RoomAvailability.DeleteOnSubmit(date);
                                db.SubmitChanges();
                                WriteFeedBackMaster(FeedbackType.Success,"Date deleted");
                                RebindGridAndScheduler();
                            }
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Handles the Click event of the _btnAddDates control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnAddDates_Click(object sender, EventArgs e)
        {
            var startDate = (DateTime)_rdpStartDate.DbSelectedDate;
            var startTime = ((DateTime)_rtpStartTime.DbSelectedDate).TimeOfDay;
            var endDate = _rdpEndDate.DbSelectedDate;
            var endTime = ((DateTime)_rtpEndTime.DbSelectedDate).TimeOfDay;

            var days = _rcbDays.CheckedItems.Aggregate(String.Empty, (current, item) => current + (item.Value + ",")).TrimEnd(',');

            var isVald = GetIsValdAddDates(days, endDate, startDate, startTime, endTime);

            if (isVald == false)
                return;

            //Check for any overlap here
            var appointMentToAdd = new AppointmentTemporaryObj
            {
                AllDay = false,
                Days = days,
                EndDate = (DateTime?)endDate,
                EndTime = endTime,
                RoomId = RoomId,
                StartDate = startDate,
                StartTime = startTime
            };

            var db = new UrbanDataContext();
            var currentAvilList = db.Manager.RoomAvailability.GetByRoomID(RoomId).OrderByDescending(t => t.StartDate).ToList();

            //Check if date to add is valid over existing range of dates.
            if (AppointmentTemporaryObj.DoesDateOverLap(appointMentToAdd, currentAvilList.ToAppointmentTempObject()))
            {
                WriteFeedBackMaster(FeedbackType.Error, "Date overlaps");
                return;
            }
            var newAppointment = new RoomAvailability
                                     {
                                         AllDay = appointMentToAdd.AllDay,
                                         Days = appointMentToAdd.Days == String.Empty ? null : appointMentToAdd.Days,
                                         EndDate = appointMentToAdd.EndDate,
                                         EndTime = appointMentToAdd.EndTime,
                                         RoomID = RoomId,
                                         StartDate = appointMentToAdd.StartDate,
                                         StartTime = appointMentToAdd.StartTime
                                     };
            db.RoomAvailability.InsertOnSubmit(newAppointment);
            db.SubmitChanges();
            RebindGridAndScheduler();

        }

        #endregion Availabilities

        #region CalendarControlFunctions

        /// <summary>
        /// Handles the AppointmentInsert event of the RadScheduler1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerCancelEventArgs"/> instance containing the event data.</param>
        protected void RadScheduler1_AppointmentInsert(object sender, SchedulerCancelEventArgs e)
        {
            Appointments.Add(new AppointmentObj(e.Appointment));
        }

        /// <summary>
        /// Handles the AppointmentUpdate event of the RadScheduler1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.AppointmentUpdateEventArgs"/> instance containing the event data.</param>
        protected void RadScheduler1_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            var ai = FindById(e.ModifiedAppointment.ID);
            ai.CopyInfo(e.ModifiedAppointment);
        }

        /// <summary>
        /// Handles the AppointmentDelete event of the RadScheduler1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerCancelEventArgs"/> instance containing the event data.</param>
        protected void RadScheduler1_AppointmentDelete(object sender, SchedulerCancelEventArgs e)
        {
            Appointments.Remove(FindById(e.Appointment.ID));
        }

        /// <summary>
        /// Handles the AppointmentDataBound event of the _rsReservations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.SchedulerEventArgs"/> instance containing the event data.</param>
        protected void _rsReservations_AppointmentDataBound(object sender, SchedulerEventArgs e)
        {
            if (((AppointmentObj)e.Appointment.DataItem).Busy == true)
            {
                e.Appointment.CssClass = "rsCategoryRed";
            }
        }

        /// <summary>
        ///     Handles the OnNavigationComplete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerNavigationCompleteEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;
            Appointments = RoomAvailabilityUtilities.GetAppointmentObjsWithRecurring(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId")).ToList();
            _rsReservations.DataSource = Appointments;
            _rsReservations.Rebind();
        }


        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        private AppointmentObj FindById(object id)
        {
            return Appointments.FirstOrDefault(ai => ai.Id.Equals(id));
        }

        #endregion CalendarControlFunctions

        /// <summary>
        /// Rebinds the grid and scheduler.
        /// </summary>
        private void RebindGridAndScheduler()
        {
            _rgAvailableTimes.Rebind();
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;
            Appointments = RoomAvailabilityUtilities.GetAppointmentObjsWithRecurring(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId")).ToList();
            _rsReservations.DataSource = Appointments;

        }

        /// <summary>
        ///     Checks if dates to be entered are valid
        /// </summary>
        /// <param name = "days">The days.</param>
        /// <param name = "endDate">The end date.</param>
        /// <param name = "startDate">The start date.</param>
        /// <param name = "startTime">The start time.</param>
        /// <param name = "endTime">The end time.</param>
        /// <returns></returns>
        private bool GetIsValdAddDates(string days, object endDate, DateTime startDate, TimeSpan startTime, TimeSpan endTime)
        {
            var isVald = true;
            if (endDate == null && days == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Error, "Please select days if end date is empty for recurring dates");
                isVald = false;
            }

            if (endDate != null && startDate > (DateTime)endDate)
            {
                WriteFeedBackMaster(FeedbackType.Error, "Start date is greater then end date");
                isVald = false;
            }

            if (endDate != null && (DateTime)endDate < startDate)
            {
                WriteFeedBackMaster(FeedbackType.Error, "End date is less then start date");
                isVald = false;
            }

            if (startTime > endTime)
            {
                WriteFeedBackMaster(FeedbackType.Error, "Start time is greater then end date");
                isVald = false;
            }

            if (endTime < startTime)
            {
                WriteFeedBackMaster(FeedbackType.Error, "End time is less then start date");
                isVald = false;
            }
            return isVald;
        }

        /// <summary>
        /// Handles the Click event of the _btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("EditRoomDetails.aspx?roomId={0}",RoomId));
        }
    }
}