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
    public partial class searchuser : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetDistrictlist();
                GetUserType();

                ddlDistrict.Items.Insert(0, new ListItem("Choose a District", "-1"));
                ddlType.Items.Insert(0, new ListItem("Choose a User Role", "-1"));
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUsers();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetUsers();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtapproval.Text = "";
            txtfname.Text = "";
            txtfrozen.Text = "";
            txtlname.Text = "";
            txtreg.Text = "";
            txtuid.Text = "";
            txtuname.Text = "";
            ddlDistrict.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
        }

        protected void gvs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs.PageIndex = e.NewPageIndex;
            GetUsers();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetUsers();
        }
        private void GetDistrictlist()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "sp_ViewDistricts";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        // Bind the data to the DropDownList
                        ddlDistrict.DataSource = reader;
                        ddlDistrict.DataTextField = "Dis_Name";
                        ddlDistrict.DataValueField = "Dis_Id";
                        ddlDistrict.DataBind();
                    }
                }
            }
        }
        private void GetUserType()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "sp_ViewUserTypes";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        // Bind the data to the DropDownList
                        ddlType.DataSource = reader;
                        ddlType.DataTextField = "Type_Name";
                        ddlType.DataValueField = "Type_Id";
                        ddlType.DataBind();
                    }
                }
            }
        }
        private void GetUsers()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                // Build dynamic query
                string query = @"
                    SELECT 
                        u.u_id, u.f_name, u.l_name, u.reg_date, u.is_frozen, u.approval_Status, u.username, t.type_Name, d.dis_name, t.type_id, d.dis_id
                    FROM 
                        tblUser u
                    LEFT JOIN 
                        tblDistricts d ON u.Dis_Id = d.Dis_Id
                    LEFT JOIN 
                        tblUserType t ON u.Type_Id = t.Type_Id
                    WHERE 
                        1 = 1";

                if (!string.IsNullOrEmpty(txtuname.Text.Trim()))
                {
                    query += " AND u.username LIKE @uname";
                }

                if (!string.IsNullOrEmpty(txtfname.Text.Trim()))
                {
                    query += " AND u.f_name LIKE @fname";
                }

                if (!string.IsNullOrEmpty(txtlname.Text.Trim()))
                {
                    query += " AND u.l_name LIKE @lname";
                }

                if (!string.IsNullOrEmpty(txtreg.Text.Trim()))
                {
                    query += " AND u.reg_date = @reg";
                }

                if (!string.IsNullOrEmpty(txtfrozen.Text.Trim()))
                {
                    query += " AND u.is_frozen = @frozen";
                }

                if (!string.IsNullOrEmpty(txtuid.Text.Trim()))
                {
                    query += " AND u.u_id = @uid";
                }

                if (ddlType.SelectedIndex > 0 && ddlType.SelectedValue != "-1")
                {
                    query += " AND u.type_id = @typeid";
                }

                if (ddlDistrict.SelectedIndex > 0 && ddlDistrict.SelectedValue != "-1")
                {
                    query += " AND u.dis_id = @disid";
                }

                query += " ORDER BY u.username";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                if (!string.IsNullOrEmpty(txtuname.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@uname", "%" + txtuname.Text.Trim() + "%");
                }

                if (!string.IsNullOrEmpty(txtfname.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@fname", "%" + txtfname.Text.Trim() + "%");
                }

                if (!string.IsNullOrEmpty(txtlname.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@lname", "%" + txtlname.Text.Trim() + "%");
                }

                if (!string.IsNullOrEmpty(txtreg.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@reg", txtreg.Text);
                }

                if (!string.IsNullOrEmpty(txtfrozen.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@frozen", txtfrozen.Text.Trim());
                }

                if (!string.IsNullOrEmpty(txtuid.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@uid", txtuid.Text.Trim());
                }

                if (ddlType.SelectedIndex > 0 && ddlType.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@typeid", ddlType.SelectedValue);
                }

                if (ddlDistrict.SelectedIndex > 0 && ddlDistrict.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@disid", ddlDistrict.SelectedValue);
                }

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvs.DataSource = dt;
                gvs.DataBind();

            }
        }

        protected void txtuid_TextChanged(object sender, EventArgs e)
        {
            //Add the following codes in a TextChanged event
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //create the movieid parameter
            //assign the parameter to the sql statement
            cmd.CommandText = "sp_ViewUsersAndDistricts where u_id = @uid";

            cmd.Parameters.AddWithValue("@uid", txtuid.Text.Trim());


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataTable
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable

                da.Fill(dt);
            }
            //Set the DataTable as the DataSource
            gvs.DataSource = dt;
            gvs.DataBind();
        }
    }
}