using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class adminlogin : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            string username = userLogin.Username;
            string password = userLogin.Password;
            //string type_id = ;
            bool chk = userLogin.Chk;
            //string type_id = ;
            using (SqlConnection con = new SqlConnection(_conString))
            {
                // Create Command
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                //searching for a record containing matching username
                cmd.CommandText = "SELECT u.u_id, u.u_password, u.type_id, a.a_id FROM tbluser u, tbladmin a WHERE a.u_id = u.u_id AND username = @un";
                cmd.Parameters.AddWithValue("@un", username);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                // use the Read() to check if the DataReader contains a record
                if (dr.Read())
                {
                    string storedHash = dr["u_password"].ToString();
                    int typeId = Convert.ToInt32(dr["type_id"]);
                    if (typeId == 1 && PasswordHelper.VerifyPassword(password, storedHash))
                    {

                        Response.Cookies["username"].Value = username;
                        Response.Cookies["u_password"].Value = password;
                        //Response.Cookies["type_id"].Value = 

                        if (chk)
                        {
                            //if checkbox is checked, make cookies persistent
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(100);
                            Response.Cookies["u_password"].Expires = DateTime.Now.AddDays(100);
                        }
                        else
                        {
                            //delete the cookies if checkbox is unchecked
                            //delete content of password field
                            Response.Cookies["username"].Expires = DateTime.Now.AddDays(-100);
                            Response.Cookies["u_password"].Expires = DateTime.Now.AddDays(-100);
                        }


                        Session["userid"] = dr["u_id"];
                        Session["username"] = username;
                        Session["adminuname"] = dr["a_id"];
                        Session["typeid"] = dr["type_id"];
                        Response.Redirect("~/admindashboard.aspx");
                    }
                    else
                    {
                        lblmsg.Text = "Invalid Login!";
                        return;
                    }
                }
                else
                {
                    lblmsg.Text = "Invalid username or password!";
                }
            }
        }
    }
}