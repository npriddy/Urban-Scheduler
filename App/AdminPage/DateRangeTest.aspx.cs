using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UrbanSchedulerProject.Code.Classes;
using UrbanSchedulerProject.Code.Utilities;

namespace UrbanSchedulerProject.App.AdminPage
{
    public partial class DateRangeTest : System.Web.UI.Page
    {
        private List<AppointmentObj> apptList;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid1.DataSource = apptList;
        }

        protected void _btnRun_Click(object sender, EventArgs e)
        {
            var db = new Urban.Data.UrbanDataContext();
            var startDate = _rdpStart.SelectedDate;
            var endDate = _rdpEnd.SelectedDate;

            if (startDate != null)
                startDate = ((DateTime) startDate).AddHours(0);

            if (endDate != null)
                endDate = ((DateTime) endDate).AddHours(23);

            apptList = AppointmentUtilities.GetAppointmentObjectsByDateRangeAndRoomId(ref db, (DateTime) startDate, (DateTime) endDate, 1).ToList();
            RadGrid1.Rebind();
        }
    }
}