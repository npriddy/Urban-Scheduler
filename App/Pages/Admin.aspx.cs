using System;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI;
using Urban.Data;

namespace UrbanSchedulerProject.App
{
    public partial class Admin : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ////UrbanDataContext dbExpected = null; // TODO: Initialize to an appropriate value
            //DateTime start = new DateTime(); // TODO: Initialize to an appropriate value
            //DateTime end = new DateTime(); // TODO: Initialize to an appropriate value
            ////List<RoomAvailability> expected = null; // TODO: Initialize to an appropriate value
            //List<RoomAvailability> actual;
            //var db = new UrbanDataContext();
            //actual = AppointmentUtilities.TestingAppointmentUtilities.GetByRoomId(ref db, start, end);
            //foreach (var item in actual)
            //{

            //}
        }

        protected void RadGrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            var db = new UrbanDataContext();
            var manager = new UrbanDataManager(db);

            RadGrid1.DataSource = (from u in db.User
                                   select u).ToList();
        }
    }
}