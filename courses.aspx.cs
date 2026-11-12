using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class courses : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getSubjectlist();
            }
        }
        private void getSubjectlist()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ViewSubjectsandGrade";
            cmd.Connection = con;
            // Create DataAdapter (Refer to slide 8)

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            GrdView2.DataSource = dt;
            GrdView2.DataBind();
        }
        protected void GrdView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdView2.PageIndex = e.NewPageIndex;
            getSubjectlist();
        }
    }
}