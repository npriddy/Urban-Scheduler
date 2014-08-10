using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities;

namespace UrbanSchedulerProject.App.Popup
{

    
  
      

    /// <summary>
    /// Used to help with printing
    /// </summary>
    public partial class SchedulePrint : BasePage
    {



        ///<summary>
        ///</summary>
        private static int Type
        {
            get
            {
                switch (Utilities.GetQueryStringSafe("type").ToLower())
                {
                    case "roomdetails":
                        {
                            return 1;
                        }
                    case "myrooms":
                        {
                            return 2;
                        }
                    case "myroomreservation":
                        {
                            return 3;
                        }
                }
                return -1;
            }
        }

        private IEnumerable<AppointmentObj> Appointments
        {
            get
            {
                var sessApts = ViewState["Appointments"] as List<AppointmentObj>;
                return sessApts;
            }
            set { ViewState["Appointments"] = value; }
        }



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
                switch(Type)
                {
                    case 1:
                        {
                            ViewState["Appointments"] = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId"));
                            Page.Title = "Room Details";
                            break;
                        }
                    case 2:
                        {
                            ViewState["Appointments"] = AppointmentUtilities.GetAllReservedObjectsByDateRangeAndUserId(ref db, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1), Cu.Id);
                            Page.Title = "Up Comming Events";
                            break;
                        }
                    case 3:
                        {
                            ViewState["Appointments"] = AppointmentUtilities.GetAllReservedObjectsByReservationId(ref db, Utilities.GetQueryStringInt("RoomReservationID"));
                            Page.Title = "Reservation";
                            break;
                        }
                }
            }
            _rsReservations.DataSource = Appointments;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
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


        /// <summary>
        ///     Handles the OnNavigationComplete event of the _rsReservations control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerNavigationCompleteEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_OnNavigationComplete(object sender, SchedulerNavigationCompleteEventArgs e)
        {
            var db = new UrbanDataContext();
            var selectedDate = _rsReservations.SelectedDate;

            switch (Type)
            {
                case 1:
                    {
                        Appointments = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Utilities.GetQueryStringInt("roomId"));
                        break;
                    }
                case 2:
                    {
                        Appointments = AppointmentUtilities.GetAllReservedObjectsByDateRangeAndUserId(ref db, new DateTime(selectedDate.Year, selectedDate.Month, 1), new DateTime(selectedDate.Year, selectedDate.Month, 1).AddMonths(1).AddDays(-1), Cu.Id);
                        break;
                    }
                case 3:
                    {
                        Appointments = AppointmentUtilities.GetAllReservedObjectsByReservationId(ref db, Utilities.GetQueryStringInt("RoomReservationID"));
                        break;
                    }
            }

            _rsReservations.DataSource = Appointments;
            _rsReservations.Rebind();
        }
    }
}