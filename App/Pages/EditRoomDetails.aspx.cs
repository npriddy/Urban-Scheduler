#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject.App.Pages
{
    /// <summary>
    /// </summary>
    public partial class EditRoomDetails : BasePage
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

        #endregion Variables

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("Edit Room Details", MainTabs.MyAccount);
            if (Page.IsPostBack) return;

            RoomId = Utilities.GetQueryStringInt("RoomId");
            if (RoomId <= 0)
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid room", FeedbackType.Warning));
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);

            if (CurrentUserUtilities.GetCuIdSafely() <= 0 || room == null || room.UserID != Cu.Id)
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/Default.aspx?message={0}&messageType={1}", "Invalid room", FeedbackType.Warning));
            }
            _cbRoomType.Items.AddRange(RoomTypeUtilties.GetAll(false));
            _cbBuilding.Items.AddRange(BuildingUtilities.GetByUserId(Cu.Id, false));
            LoadPageFields(room);
        }

        /// <summary>
        ///     Loads the page fields.
        /// </summary>
        /// <param name = "room">The room.</param>
        private void LoadPageFields(Room room)
        {
            _txtRoomTitle.Text = room.Title;
            _txtRoomNumber.Text = room.Number;
            _txtMaxOccupancy.DbValue = room.MaxOccupancy;
            var li = _cbRoomType.FindItemByValue(room.RoomTypeID.ToString());
            if (li != null) li.Selected = true;
            _rContent.Content = room.Description;

            var bLi = _cbBuilding.FindItemByValue(room.BuildingID.ToString());
            if (bLi != null) bLi.Selected = true;
        }
        
        #region ActionButtons

        /// <summary>
        ///     Handles the Click event of the _btnBack control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnBack_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect("~/App/Pages/MyAccount.aspx");
        }

        /// <summary>
        ///     Handles the Click event of the _btnSave control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnSaveRoom_Click(object sender, EventArgs e)
        {
            var db = new UrbanDataContext();
            var room = db.Manager.Room.GetByKey(RoomId);
            if (room == null)
                throw new Exception("Room Is Null");

            if (!Page.IsValid || !ValidateInputs()) return;

            room.Title = _txtRoomTitle.Text.Trim();
            room.MaxOccupancy = int.Parse(_txtMaxOccupancy.Text);
            room.RoomTypeID = int.Parse(_cbRoomType.SelectedValue);
            room.Number = _txtRoomNumber.Text;
            room.BuildingID = int.Parse(_cbBuilding.SelectedValue);
            room.Description = _rContent.Content;

            db.SubmitChanges();
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/App/Pages/MyAccount.aspx?message={0}&messageType={1}", "Room saved", FeedbackType.Success));
        }

        /// <summary>
        ///     Handles the Click event of the _btnAddDates control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnAddDates_Click(object sender, EventArgs e)
        {
            RadAjaxManager.GetCurrent(Page).Redirect(String.Format("RoomDetailsAddDates.aspx?roomId={0}",RoomId));
        }

        #endregion ActionButtons

        /// <summary>
        ///     Validates the inputs.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputs()
        {
            var valid = true;
            if (_txtRoomTitle.Text.Trim() == string.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Please enter text for title");
                valid = false;
            }

            if (_txtMaxOccupancy.Text == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Please enter max occupancy");
                valid = false;
            }

            if (_txtRoomNumber.Text == String.Empty)
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Please enter max room number");
                valid = false;
            }

            if (_cbRoomType.SelectedValue == "NULL")
            {
                WriteFeedBackMaster(FeedbackType.Warning, "Please select a room type");
                valid = false;
            }

            return valid;
        }

        #region AvailableTimes

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var roomList = db.Manager.RoomAvailability.GetByRoomID(RoomId).ToList();

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

        #endregion AvailableTimes

        #region CalendarCode

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
        ///     Handles the AppointmentInsert event of the RadScheduler1 control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.SchedulerCancelEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentInsert(object sender, SchedulerCancelEventArgs e)
        {
            Appointments.Add(new AppointmentObj(e.Appointment));
        }

        /// <summary>
        ///     Handles the AppointmentUpdate event of the RadScheduler1 control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.AppointmentUpdateEventArgs" /> instance containing the event data.</param>
        protected void _rsReservations_AppointmentUpdate(object sender, AppointmentUpdateEventArgs e)
        {
            var ai = FindById(e.ModifiedAppointment.ID);
            ai.CopyInfo(e.ModifiedAppointment);
        }

        /// <summary>
        ///     Handles the AppointmentDelete event of the RadScheduler1 control.
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

        /// <summary>
        ///     Handles the Click event of the _btnExportSchedule control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnExportSchedule_Click(object sender, EventArgs e)
        {
            _rsReservations.ExportToPdf();
        }

        #endregion CalendarCode

        #region ImageUploadCode

        /// <summary>
        ///     Handles the NeedDataSource event of the RadGrid1 control.
        /// </summary>
        /// <param name = "source">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgImages_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //RadGrid1.DataSource = DataSource;
            var db = new UrbanDataContext();
            var files = db.Manager.RoomImageLink.GetByRoomID(RoomId).ToList();
            _rgImages.DataSource = files;
        }

        /// <summary>
        ///     Handles the Click event of the _btnUploadImage control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnUploadImage_Click(object sender, EventArgs e)
        {
            if (RadUpload1.UploadedFiles.Count <= 0) return;

            var db = new UrbanDataContext();

            foreach (UploadedFile validFile in RadUpload1.UploadedFiles)
            {
                var guid = Guid.NewGuid();
                var targetFolder = Server.MapPath("~/Files");
                validFile.SaveAs(Path.Combine(targetFolder, guid.ToString()), true);

                var f = new Files
                            {
                                Extension = validFile.GetExtension(),
                                FilesName = validFile.GetNameWithoutExtension(),
                                FileSubType = "Image",
                                ServerFileName = guid.ToString()
                            };
                db.Files.InsertOnSubmit(f);
                db.SubmitChanges();
                var link = new RoomImageLink
                               {
                                   ImageDescription = _txtDescription.Text,
                                   Title = _txtName.Text,
                                   RoomID = RoomId,
                                   FileID = f.Id
                               };
                db.RoomImageLink.InsertOnSubmit(link);
                db.SubmitChanges();

                _txtName.Text = "";
                _txtDescription.Text = "";
            }
            _rgImages.Rebind();
        }

        #endregion ImageUploadCode
    }
}