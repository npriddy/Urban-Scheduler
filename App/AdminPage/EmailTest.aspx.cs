using System;
using System.Collections.Generic;
using System.Web.UI;
using Urban.Data;
using UrbanSchedulerProject.Code.Utilities.EmailTemplates;

namespace UrbanSchedulerProject.App.AdminPage
{
    public partial class EmailTest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void _btnEmailTest_Click(object sender, EventArgs e)
        {
            var db = new UrbanDataContext();
            User user = db.Manager.User.GetByKey(1);
            var keys = new List<KeyValuePair<string, string>>();
            keys.Add(new KeyValuePair<string, string>("{!MainTitleHtml}", "Account Created"));
            keys.Add(new KeyValuePair<string, string>("{!Subtitle01Html}", "Action Required"));
            keys.Add(new KeyValuePair<string, string>("{!Subtitle01TextHtml}",
                                                      "Congratulations on creating your account activation is still required. Please follow the links bellow."));
            keys.Add(new KeyValuePair<string, string>("{!LinkHtml}", "http://UrbanScheduler.com"));
            keys.Add(new KeyValuePair<string, string>("{!LinkTextHtml}", "Click Here To Activate Account"));
            keys.Add(new KeyValuePair<string, string>("{!FormPerson}", "UrbanScheduler System"));

            EmailTemplateUtilities.EmailFromFromMaster(keys, "Test email", user, new List<int>());
            //EmailUti1lities.SendEmail("Test Email", "Test Email", "Test Email", "Nate Email", "npriddy@npriddy.com", new List<int>());
        }
    }
}