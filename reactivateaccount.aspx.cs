using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class reactivateaccount : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReactivate_Click(object sender, EventArgs e)
        {
            string username = txtRUsername.Text.Trim();
            string password = txtRPassword.Text;

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT u_id, u_password, is_frozen FROM tblUser WHERE username = @un";
                cmd.Parameters.AddWithValue("@un", username);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        string storedHash = dr["u_password"].ToString();
                        int frozen = Convert.ToInt32(dr["is_frozen"]);
                        int userId = Convert.ToInt32(dr["u_id"]);

                        if (frozen == 0)
                        {
                            lblReactivateMsg.Text = "This account is already active. You can log in normally.";
                            lblReactivateMsg.CssClass = "text-warning";
                            return;
                        }

                        if (PasswordHelper.VerifyPassword(password, storedHash))
                        {
                            dr.Close(); 

                            SqlCommand ucmd = con.CreateCommand();
                            ucmd.CommandText = "UPDATE tblUser SET is_frozen = 0 WHERE u_id = @uid";
                            ucmd.Parameters.AddWithValue("@uid", userId);
                            ucmd.ExecuteNonQuery();

                            lblReactivateMsg.Text = "Your account has been reactivated! Redirecting to login...";
                            lblReactivateMsg.CssClass = "text-success";

                            Response.Redirect("~/login.aspx");
                        }
                        else
                        {
                            lblReactivateMsg.Text = "Incorrect username or password.";
                            lblReactivateMsg.CssClass = "text-danger";
                        }
                    }
                    else
                    {
                        lblReactivateMsg.Text = "Incorrect username or password.";
                        lblReactivateMsg.CssClass = "text-danger";
                    }
                }
            }
        }
    }
}