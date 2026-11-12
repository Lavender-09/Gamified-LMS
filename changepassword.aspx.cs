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
    public partial class changepassword : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
        }

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT u_password FROM tblUser WHERE u_id = @uid";
                cmd.Parameters.AddWithValue("@uid", userId);
                string storedHash = cmd.ExecuteScalar().ToString();

                if (!PasswordHelper.VerifyPassword(txtOldPass.Text, storedHash))
                {
                    lblMsg.Text = "Current password is incorrect.";
                    lblMsg.CssClass = "text-danger";
                    return;
                }

                string newHash = PasswordHelper.HashPassword(txtNewPass.Text.Trim());

                SqlCommand ucmd = con.CreateCommand();
                ucmd.CommandText = "UPDATE tblUser SET u_password = @newpass WHERE u_id = @uid";
                ucmd.Parameters.AddWithValue("@newpass", newHash);
                ucmd.Parameters.AddWithValue("@uid", userId);
                ucmd.ExecuteNonQuery();
            }

            lblMsg.Text = "Password updated successfully!";
            lblMsg.CssClass = "text-success";
            txtOldPass.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
        }
    }
}