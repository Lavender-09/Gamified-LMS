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
    public partial class categories : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getCategorylist();
            }
        }
        private void getCategorylist()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "sp_ViewCategories";
            cmd.Connection = con;
            // Create DataAdapter (Refer to slide 8)

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            GrdView1.DataSource = dt;
            GrdView1.DataBind();
        }

        protected void GrdView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdView1.PageIndex = e.NewPageIndex;
            getCategorylist();
        }
    }
}