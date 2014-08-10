using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace UrbanSchedulerProject.App
{
    public partial class SiteTest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fun = ConfigurationManager.ConnectionStrings["UrbanConnectionString"].ConnectionString;
            Response.Write(fun);
            var sqlConnection1 =
                new SqlConnection(ConfigurationManager.ConnectionStrings["UrbanConnectionString"].ConnectionString);
            var cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select * from dbo.[User]";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Response.Write(reader[0] + Environment.NewLine);
            }
            // Data is accessible through the DataReader object here.

            sqlConnection1.Close();
        }
    }
}