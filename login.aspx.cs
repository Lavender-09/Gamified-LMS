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
    public partial class login : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Verify if uname and pw cookies are not null
                if (Request.Cookies["username"] != null && Request.Cookies["u_password"] != null)
                {
                    //Populate the Username and Password Textboxes by retrieving the values from
                    //the cookies
                    userLogin.Username = Request.Cookies["username"].Value;
                    userLogin.Password = Request.Cookies["u_password"].Value;
                }
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            //get the value of username, password fields and state of checkbox from login
            //form
            string username = userLogin.Username;
            string password = userLogin.Password;
            //string type_id = ;
            bool chk = userLogin.Chk;
            using (SqlConnection con = new SqlConnection(_conString))
            {
                // Create Command
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                //searching for a record containing matching username
                cmd.CommandText = "SELECT * FROM tbluser WHERE username = @un";
                cmd.Parameters.AddWithValue("@un", username);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                // use the Read() to check if the DataReader contains a record
                if (dr.Read())
                {
                    //fetch the password from the database and convert to string
                    string storedHash = dr["u_password"].ToString();
                    //fetch the status from the database and convert to integer
                    int status = Convert.ToInt32(dr["approval_status"]);
                    int frozen = Convert.ToInt32(dr["is_frozen"]);
                    //check status and hashed password
                    int typeId = Convert.ToInt32(dr["type_id"]);

                    if (frozen == 0)
                    {
                        if (status == 1 && PasswordHelper.VerifyPassword(password, storedHash))
                        {
                            //create a memory cookie to store username and pwd
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
                            Session["typeid"] = dr["type_id"];
                            Session["userid"] = dr["u_id"];
                            Session["username"] = username;

                            dr.Close();

                            SqlCommand updCmd = con.CreateCommand();
                            updCmd.CommandText = "UPDATE tblUser SET Last_Login = @now WHERE u_id = @uid";
                            updCmd.Parameters.AddWithValue("@now", DateTime.Now);
                            updCmd.Parameters.AddWithValue("@uid", Session["userid"]);
                            updCmd.ExecuteNonQuery();

                            Response.Redirect("profile.aspx");
                        }
                        else
                        {
                            lblmsg.Text = "Invalid username or password!";
                            return;
                        }
                    }
                    else if(frozen == 1)
                    {
                        Response.Redirect("~/reactivateaccount");
                    }
                }
                else
                {
                    //delete content of password field
                    lblmsg.Text = "Invalid username or password!";
                }
            }
        }
    }
}