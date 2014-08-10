#region

using System;
using System.Linq;
using Telerik.Web.UI;
using UrbanSchedulerProject.Code.BasePage;
using UrbanSchedulerProject.Code.Enum;
using UrbanSchedulerProject.Code.Utilities;
using UrbanSchedulerProject.Code.Utilities.DB;
using UrbanSchedulerProject.Code.Utilities.TypeUtilities;

#endregion

namespace UrbanSchedulerProject
{
    /// <summary>
    /// Main Login / Default home page the user lands on
    /// </summary>
    public partial class Default : BasePage
    {
        /// <summary>
        ///     Handles the Load event of the Page control.
        /// </summary>
        /// <param name = "sender">The source of the event.</param>
        /// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage("UrbanScheduler.com", MainTabs.Home);

            if (Page.IsPostBack) return;
            ProcessActivationKey();

            //If user has a room reservation id auto direct to it
            int roomReservationId;
            if (Utilities.GetQueryStringSafe("RoomReservationKey") != String.Empty && int.TryParse(Utilities.GetQueryStringSafe("RoomReservationKey"), out roomReservationId))
            {
                RadAjaxManager.GetCurrent(Page).Redirect(String.Format("~/App/Pages/RoomReservationDetails.aspx?RoomReservationID={0}", Utilities.GetQueryStringSafe("RoomReservationKey")));
            }


            var db = new Urban.Data.UrbanDataContext();
            string reservedRooms = db.Manager.RoomReservation.GetAllCount().ToString();
            string numberRooms = db.Manager.Room.GetCountForAll().ToString();
            _lblRoomsAvail.Text = reservedRooms;
            _lblReservedRooms.Text = numberRooms;
        }

        /// <summary>
        ///     Processes the activation key for user account creation.
        /// </summary>
        private void ProcessActivationKey()
        {
            var key = Utilities.GetQueryStringGuid("activationKey");
            if (key == null) return;
            switch (UserUtilities.CheckActivateUser((Guid) key))
            {
                case -1:
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "Invalid activation link.");
                    }
                    break;
                case 0:
                    {
                        WriteFeedBackMaster(FeedbackType.Warning, "User is already activate.");
                    }
                    break;
                case 1:
                    {
                        WriteFeedBackMaster(FeedbackType.Success, "Account activated successfully you may now login.");
                    }
                    break;
            }
        }
    }
}