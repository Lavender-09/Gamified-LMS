using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace MoProf_bse25aft1_250429135
{
    public partial class moprofmaster : System.Web.UI.MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //verify if session username is not null
            if (Session["username"] != null)
            {
                hydefault.Visible = false;

                string typeId = Session["typeid"].ToString();

                if (typeId == "2")
                {
                    pnlTutors.Visible = true;
                    hytutdash.Visible = true;
                }
                else if (typeId == "3")
                {
                    pnlStudents.Visible = true;
                    hydash.Visible = true;
                } 
                else
                {
                    hyregister.Visible = false;
                    hydash.Visible = false;
                    lgregis.CssClass = "nav navbar-nav navbar-right";
                    lbllgged.CssClass = "btn btn-outline-success";
                    lbllgged.Text = "Welcome" + Session["adminuname"];
                    btnlgout.Visible = true;
                    pnlmanagemov.Visible = true;
                    pnlprofile.Style.Add("visibility", "hidden");
                    Page.Controls.Remove(pnlprofile);
                    pnllog.Style.Add("visibility", "hidden");
                    Page.Controls.Remove(pnllog);
                }

                pnllog.Style.Add("visibility", "hidden");
                Page.Controls.Remove(pnllog);
                lgregis.CssClass = "nav navbar-nav navbar-right";
                lbllgged.CssClass = "btn btn-outline-success";
                //add the session name
                lbllgged.Text = "Welcome " + Session["username"];
                btnlgout.Visible = true;
                pnlprofile.Visible = true;
                hyregister.Visible = false;
            } 
            else
            {
                btnlgout.Visible = false;
                lbllgged.Text = "";
                hyregister.Visible = true;
                pnlprofile.Visible = false;
                pnlStudents.Visible = false;
                pnlTutors.Visible = false;
            }

            if (Session["adminuname"] != null)
            {
                hyregister.Visible = false;
                hydash.Visible = false;
                lgregis.CssClass = "nav navbar-nav navbar-right";
                lbllgged.CssClass = "btn btn-outline-success";
                lbllgged.Text = "Welcome" + Session["adminuname"];
                btnlgout.Visible = true;
                pnlmanagemov.Visible = true;
                pnlprofile.Style.Add("visibility", "hidden");
                Page.Controls.Remove(pnlprofile);
                pnllog.Style.Add("visibility", "hidden");
                Page.Controls.Remove(pnllog);
            } 
        }
        protected void btnlgout_Click(object sender, EventArgs e)
        {
            lgout();
        }
        void lgout()
        {
            if (Session["username"] != null || Session["adminuname"] != null)
            {
                //Remove all session
                Session.RemoveAll();
                //Destroy all Session objects
                Session.Abandon();
                //Redirect to homepage or login page
                Response.Redirect("~/default");
            }
        }

        protected void lnkdeac_Click(object sender, EventArgs e)
        {
            if (Session["userid"] == null)
            {
                return;
            }
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand ucmd = con.CreateCommand();
                ucmd.CommandType = CommandType.Text;
                ucmd.CommandText = "update tblUser set is_frozen = '1' Where u_id = @uid";
                ucmd.Parameters.AddWithValue("@uid", Session["userid"]);
                ucmd.ExecuteNonQuery();
            }
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect("~/default");
        }
    }
}