using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Services;
using Urban.Data;

namespace UrbanSchedulerProject
{
    /// <summary>
    /// Summary description for GoogleMapsService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
        // [System.Web.Script.Services.ScriptService]
    public class GoogleMapsService : WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string ListUsers()
        {
            var db = new UrbanDataContext();
            string UserListString = "";
            List<User> usersList = (from u in db.User select u).ToList();
            foreach (User user in usersList)
            {
                UserListString += user.Id + ";";
            }
            return UserListString;
        }
    }
}