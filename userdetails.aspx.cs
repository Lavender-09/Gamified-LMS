using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MoProf_bse25aft1_250429135
{
    public partial class userdetails : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                GetUserList();
            }
        }
        private void GetUserList()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();

            int qs = Convert.ToInt32(Request.QueryString["id"]);

            cmd.CommandText = "SELECT u.*, t.Type_Name, d.Dis_Name,  \r\n\tCONVERT(VARCHAR, u.Reg_Date, 103) AS Registration, \r\n\tCONVERT(VARCHAR, u.dob, 103) AS Birthdate,\r\n\tCONVERT(VARCHAR, u.Last_Login, 103) AS lastlog\r\n\tFROM tblUser u, tblUserType t, tblDistricts d\r\n\tWHERE t.Type_Id = u.Type_Id\r\n\tAND u.Dis_Id = d.Dis_Id and u_id = " + qs;
            cmd.Connection = con;
            // Create DataAdapter (Refer to slide 8)

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            DetailsView1.DataSource = dt;
            DetailsView1.DataBind();
        }
    }
}