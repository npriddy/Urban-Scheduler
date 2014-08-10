#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
    /// User posts a room + data information.
    /// </summary>
    public partial class PostARoom : BasePage
    {
        #region Varaibles

        /// <summary>
        /// </summary>
        private static DbTransaction _transac;

        /// <summary>
        ///     Gets the availability list.
        /// </summary>
        private List<AppointmentTemporaryObj> AvailabilityList
        {
            get
            {
                if (ViewState["AvailabilityList"] == null)
                    ViewState["AvailabilityList"] = new List<AppointmentTemporaryObj>();
                return (List<AppointmentTemporaryObj>) ViewState["AvailabilityList"];
            }
        }

        /// <summary>
        ///     Gets the availability list.
        /// </summary>
        private List<ImagesObj> ImageList
        {
            get
            {
                if (ViewState["ImagesObj"] == null)
                    ViewState["ImagesObj"] = new List<ImagesObj>();
                return (List<ImagesObj>) ViewState["ImagesObj"];
            }
        }

        #endregion Varaibles

        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("Post A Room", MainTabs.PostARoom);

            //If current user is null then only show image instead.
            if (CurrentUserUtilities.GetCuIdSafely() <= 0)
            {
                _pnlPostARoom.Visible = false;
                _pnlPostARoom.Enabled = false;
                _pnlCreateNoAccount.Visible = true;
                _pnlCreateNoAccount.Enabled = true;
                return;
            }

            if (Page.IsPostBack) return;

            if (_cbState.Items.Count > 0) return;

            //Load state information.
            _cbState.LoadXml(Utilities.GetXmlForPath(Utilities.StatePathWithNoneXml));
            _cbState.FindItemByValue("NULL").Selected = true;
            _cbRoomType.Items.AddRange(RoomTypeUtilties.GetAll(true));
            _cbBuilding.Items.AddRange(BuildingUtilities.GetByUserId(Cu.Id, true));
        }

        /// <summary>
        /// Handles the Click event of the _btnUploadImage control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        protected void _btnUploadImage_Click(object sender, EventArgs e)
        {
            if (RadUpload1.UploadedFiles.Count <= 0) return;
            foreach (UploadedFile validFile in RadUpload1.UploadedFiles)
            {
                var guid = Guid.NewGuid();
                var targetFolder = Server.MapPath("~/FilesTemp");
                validFile.SaveAs(Path.Combine(targetFolder, guid.ToString()), true);
                //Save to Files Temp path and then a new ImageObj for temp list
                ImageList.Add(new ImagesObj
                                  {
                                      Description = _txtDescription.Text,
                                      Title = _txtName.Text,
                                      ImageUrl = guid.ToString(),
                                      FileName = validFile.GetNameWithoutExtension(),
                                      Extension = validFile.GetExtension(),
                                  });
                _txtName.Text = "";
                _txtDescription.Text = "";
            }
            _rgImages.Rebind();
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the _cbBuilding control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs" /> instance containing the event data.</param>
        protected void _cbBuilding_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int itemId;
            if (!int.TryParse(e.Value, out itemId) || itemId <= 0) return;
            var db = new UrbanDataContext();
            var building = db.Manager.Building.GetByKey(itemId);
            if (building == null)
                throw new NullReferenceException("building is null with itemId : " + itemId);

            _txtPrimaryAddress.Text = building.PrimaryAddress;
            _txtSecondaryAddress.Text = building.SecondaryAddress;
            _txtCity.Text = building.City;
            _txtZip.Text = building.Zip;
            _txtBuildingName.Text = building.Name;

            var li = _cbState.FindItemByValue(building.State);
            if (li != null) li.Selected = true;
        }

        #region CreateRoomFunctions

        /// <summary>
        ///     Handles the Click event of the _btnCreateRoom control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnCreateRoom_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var db = new UrbanDataContext();

            //Verify All sections from user are valid
            var validationCheck = VerifyRequiredFieldsSelected();
            switch (validationCheck)
            {
                case -1:
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "Please fill in required fields");
                        return;
                    }
                case -2:
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "Building already exists with this address information");
                        return;
                    }
                case -3:
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "Room already exists for this building and room number");
                        return;
                    }
                case 1:
                    {
                        break;
                    }
            }

            try
            {
                db.Connection.Open();
                _transac = db.Connection.BeginTransaction();
                db.Transaction = _transac;

                //Building Creation
                var buildingCreateParams = GetBuildingCreateParams();

                var buidlingResult = BuildingUtilities.ProcessBuildingCreation(ref db, Cu.Id, buildingCreateParams);
                if (buidlingResult != -1)
                {
                    //Room Creation
                    var roomCreateParams = GetRoomCreateParams();
                    var roomResult = RoomUtilities.ProcessRoomCreation(ref db, buidlingResult, Cu.Id, AvailabilityList, roomCreateParams);
                    if (roomResult == -1)
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "Room already exists with this number for this building please select another.");
                        _transac.Rollback();
                    }
                    else
                    {
                        //_transac.Rollback();
                        _transac.Commit();
                        //Move every temp image to full image
                        foreach (var file in ImageList)
                        {
                            var filesTemp = Server.MapPath("~/FilesTemp");
                            var imageToCopy = Path.Combine(filesTemp, file.ImageUrl);
                            var files = Server.MapPath("~/Files");
                            var imagePathToCopy = Path.Combine(files, file.ImageUrl);

                            File.Copy(imageToCopy, imagePathToCopy);

                            var f = new Files
                                        {
                                            Extension = file.Extension,
                                            FilesName = file.FileName,
                                            FileSubType = "Image",
                                            ServerFileName = file.ImageUrl
                                        };
                            db.Files.InsertOnSubmit(f);
                            db.SubmitChanges();
                            var link = new RoomImageLink
                                           {
                                               ImageDescription = file.Description,
                                               Title = file.Title,
                                               RoomID = roomResult,
                                               FileID = f.Id
                                           };
                            db.RoomImageLink.InsertOnSubmit(link);
                            db.SubmitChanges();
                        }


                        RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/App/Pages/MyAccount.aspx?message={0}", ("Room " + _txtRoomNumber.Text + " created")));
                    }
                }
                else
                {
                    WriteFeedBackMaster(FeedbackType.Warning, "This address information already eixsts for a different user. Please modify and resubmit");
                    _transac.Rollback();
                }
            }
            catch (Exception)
            {
                if (_transac != null)
                    _transac.Rollback();
                throw;
            }
            finally
            {
                if (db.Connection.State == ConnectionState.Open)
                    db.Connection.Close();
            }
        }

        /// <summary>
        ///     Verifies the required fields selected.
        /// </summary>
        /// <returns></returns>
        private int VerifyRequiredFieldsSelected()
        {
            //Verify Room Values
            if (_txtRoomNumber.Text.Trim() == String.Empty || _txtRoomTitle.Text.Trim() == String.Empty || _txtMaxOccupancy.Text == String.Empty || _cbRoomType.SelectedValue == "NULL")
                return -1;
            if (_txtPrimaryAddress.Text.Trim() == String.Empty || _txtCity.Text.Trim() == String.Empty || _txtZip.Text.Trim() == String.Empty || _cbState.SelectedValue == "NULL")
                return -1;

            var db = new UrbanDataContext();
            //Building Exists for another user check
            if (BuildingUtilities.DoesBuildingAlreadyExistNotForUser(ref db, _txtPrimaryAddress.Text.Trim(), _txtSecondaryAddress.Text.Trim(), _txtCity.Text.Trim(), _txtZip.Text.Trim(), _cbState.SelectedValue, Cu.Id))
                return -2;

            var existingBuilding = BuildingUtilities.DoesBuildingAlreadyExist(ref db, _txtPrimaryAddress.Text.Trim(), _txtSecondaryAddress.Text.Trim(), _txtCity.Text.Trim(), _txtZip.Text.Trim(), _cbState.SelectedValue, Cu.Id);

            if (existingBuilding != null)
            {
                //return existingBuilding.Id;
                if (RoomUtilities.DoesRoomExistWithNumber(ref db, existingBuilding.Id, _txtRoomNumber.Text.Trim()))
                {
                    return -3;
                }
            }

            return 1;
        }

        #endregion CreateRoomFunctions

        #region ParamFunctionHelpers

        /// <summary>
        ///     Gets the room create params.
        /// </summary>
        /// <returns></returns>
        private RoomUtilities.ProcessRoomCreationParams GetRoomCreateParams()
        {
            return new RoomUtilities.ProcessRoomCreationParams
                       {
                           Description = _rtxtContent.Content,
                           MaxOccupancy = int.Parse(_txtMaxOccupancy.DbValue.ToString()),
                           Name = _txtRoomTitle.Text.Trim(),
                           Number = _txtRoomNumber.Text.Trim(),
                           Type = int.Parse(_cbRoomType.SelectedItem.Value)
                       };
        }

        /// <summary>
        ///     Gets the building create params.
        /// </summary>
        /// <returns></returns>
        private BuildingUtilities.ProcessBuildingCreationParams GetBuildingCreateParams()
        {
            return new BuildingUtilities.ProcessBuildingCreationParams
                       {
                           PrimaryAddress = _txtPrimaryAddress.Text.Trim(),
                           SecondaryAddress = _txtSecondaryAddress.Text.Trim(),
                           City = _txtCity.Text.Trim(),
                           Zip = _txtZip.Text.Trim(),
                           State = _cbState.SelectedValue,
                           Name = _txtBuildingName.Text.Trim()
                       };
        }

        #endregion ParamFunctionHelpers

        #region ImageUploadCode

        /// <summary>
        ///     Handles the NeedDataSource event of the RadGrid1 control.
        /// </summary>
        /// <param name = "source">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgImages_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            //RadGrid1.DataSource = DataSource;
            _rgImages.DataSource = ImageList;
        }
         /// <summary>
        ///     Handles the ItemCommand event of the _rgMyRooms control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridCommandEventArgs" /> instance containing the event data.</param>
        protected void _rgImages_ItemCommand(object sender, GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case RadGrid.DeleteCommandName:
                    {
                        var g = (GridDataItem)e.Item;

                        var item = ImageList.Find(t => t.Id == new Guid(g["ID"].Text));
                        if (item != null)
                            ImageList.Remove(item);
                        WriteToBothFeedback(FeedbackType.Success, "Image removed.");
                        break;
                    }
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;

            }
        }

        #endregion ImageUploadCode

        #region AvailabilityFunctions

        /// <summary>
        ///     Handles the NeedDataSource event of the _rgAvailableTimes control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "Telerik.Web.UI.GridNeedDataSourceEventArgs" /> instance containing the event data.</param>
        protected void _rgAvailableTimes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            _rgAvailableTimes.DataSource = AvailabilityList;
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
        ///     Handles the Click event of the _btnAddDates control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void _btnAddDates_Click(object sender, EventArgs e)
        {
            var startDate = (DateTime) _rdpStartDate.DbSelectedDate;
            var startTime = ((DateTime) _rtpStartTime.DbSelectedDate).TimeOfDay;
            var endDate = _rdpEndDate.DbSelectedDate;
            var endTime = ((DateTime) _rtpEndTime.DbSelectedDate).TimeOfDay;

            var days = _rcbDays.CheckedItems.Aggregate(String.Empty, (current, item) => current + (item.Value + ","));
            days = days.TrimEnd(',');


            var isVald = GetIsValdAddDates(days, endDate, startDate, startTime, endTime);

            if (isVald == false)
                return;

            //Check for any overlap here
            var appointMentToAdd = new AppointmentTemporaryObj
                                       {
                                           AllDay = false,
                                           Days = days,
                                           EndDate = (DateTime?) endDate,
                                           EndTime = endTime,
                                           RoomId = 0,
                                           StartDate = startDate,
                                           StartTime = startTime
                                       };

            if (AppointmentTemporaryObj.DoesDateOverLap(appointMentToAdd, AvailabilityList))
            {
                WriteToBothFeedback(FeedbackType.Error, "Date overlaps");
            }
            else
            {
                AvailabilityList.Add(appointMentToAdd);
            }


            _rgAvailableTimes.Rebind();
        }

        /// <summary>
        ///     Gets the is vald add dates.
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
                WriteToBothFeedback(FeedbackType.Error, "Please select days if end date is empty for recurring dates");
                isVald = false;
            }

            if (endDate != null && startDate > (DateTime) endDate)
            {
                WriteToBothFeedback(FeedbackType.Error, "Start date is greater then end date");
                isVald = false;
            }

            if (endDate != null && (DateTime) endDate < startDate)
            {
                WriteToBothFeedback(FeedbackType.Error, "End date is less then start date");
                isVald = false;
            }

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
            ucDateFeedBack.InsertFeedBack(type, message);
            WriteFeedBackMaster(type, message);
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

                        var item = AvailabilityList.Find(t => t.Id == new Guid(g["ID"].Text));
                        if (item != null)
                            AvailabilityList.Remove(item);
                        WriteToBothFeedback(FeedbackType.Success, "Date removed.");
                        break;
                    }
                case RadGrid.ExportToPdfCommandName:
                    _isPdfExport = true;
                    break;

            }
        }

        #endregion AvailabilityFunctions
    }
}