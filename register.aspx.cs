using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class register : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtfname.Focus();

                GetDistrictlist();

                ListItem lidefault = new ListItem("Select District", "-1");
                ddlDistrict.Items.Insert(0, lidefault);

                GetGradelist();

                ListItem lidefault2 = new ListItem("Select Grade", "-1");
                ddlGrade.Items.Insert(0, lidefault2);
            }

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string filen = "avatar.jpg";
            //Check whether the fileupload contains a file
            if (fupprofilepic.HasFile)
            {
                if (CheckFileType(fupprofilepic.FileName))
                {
                    filen = Path.GetFileName(fupprofilepic.PostedFile.FileName);
                    fupprofilepic.PostedFile.SaveAs(Server.MapPath("~/images/") +
                    filen);
                }
            }

            int studentTypeId = GetStudentType();

            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand scmd = new SqlCommand();
            scmd.CommandType = CommandType.Text;
            scmd.Connection = con;
            //create a parameterized query for the username
            scmd.Parameters.AddWithValue("@uname", txtuname.Text.Trim());
            //search for username from tbluser
            scmd.CommandText = "select * from tbluser where username = @uname";
            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = scmd.ExecuteReader();
            //Check if username already exists in the DB
            if (dr.HasRows)
            {
                lblmsg.Text = "Username Already Exist, Please Choose Another";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                txtuname.Focus();
            }
            else
            {
                dr.Close();
                //Ensure the DataReader is closed
                string gender = "";
                if (radmale.Checked == true)
                {
                    gender = "M";
                }
                else
                {
                    gender = "F";
                }
                string strDate = txtdob.Text;
                //DateTime dt = Convert.ToDateTime(strDate);
                // Create another Command object for the insert statement
                SqlCommand icmd = new SqlCommand();
                icmd.Connection = con;
                icmd.CommandText = "INSERT INTO tblUser(f_name, l_name, gender, dis_id, phone, DOB, profilepic, email, username, u_password, approval_status, type_id, reg_date)" +
                    "VALUES(@firstname, @lastname, @gender, @district, @mob, @dob, @pfp, @email, @username, @pwd, @status, @type, SYSDATETIME())" + "SELECT SCOPE_IDENTITY()";
                icmd.Parameters.AddWithValue("@firstname", txtfname.Text.Trim());
                icmd.Parameters.AddWithValue("@lastname", txtlname.Text.Trim());
                icmd.Parameters.AddWithValue("@gender", gender);
                //retrieve the country dropdown list value
                icmd.Parameters.AddWithValue("@district", ddlDistrict.SelectedValue); //bound the data value
                icmd.Parameters.AddWithValue("@mob", txtmob.Text.Trim());
                icmd.Parameters.AddWithValue("@dob", strDate);
                icmd.Parameters.AddWithValue("@pfp", filen);
                icmd.Parameters.AddWithValue("@email", txtemail.Text.Trim());
                icmd.Parameters.AddWithValue("@username", txtuname.Text.Trim());
                //add a method to hash your password
                icmd.Parameters.AddWithValue("@pwd", PasswordHelper.HashPassword(txtpass.Text.Trim()));
                //set the status to active or inactive
                icmd.Parameters.AddWithValue("@status", 1);
                icmd.Parameters.AddWithValue("@type", studentTypeId);
                icmd.CommandType = CommandType.Text;

                //separate into the different role tables after inserting into user table
                int newUserId = Convert.ToInt32(icmd.ExecuteScalar());
                int gradeId = Convert.ToInt32(ddlGrade.SelectedValue);
                int lvlId = GetLevelIdForGrade(gradeId);

                SqlCommand roleCmd = new SqlCommand();
               
                roleCmd.Connection = con;
                roleCmd.CommandText = "INSERT INTO tblStudents(u_id, grade_id, lvl_id) VALUES(@uid, @gradeid, @lvlid)";
                roleCmd.Parameters.AddWithValue("@uid", newUserId);
                roleCmd.Parameters.AddWithValue("@gradeid", gradeId);
                roleCmd.Parameters.AddWithValue("@lvlid", lvlId);
                roleCmd.ExecuteNonQuery();
                int newStuId = Convert.ToInt32(roleCmd.ExecuteScalar());

                if (upResults.HasFile && CheckDocFileType(upResults.FileName))
                {
                    string resultsFileName = !string.IsNullOrWhiteSpace(txtResultsFilename.Text.Trim())
                        ? txtResultsFilename.Text.Trim() + Path.GetExtension(upResults.FileName)
                        : Path.GetFileName(upResults.FileName);

                    string customfolder = "~/files/" + txtuname.Text.Trim() + "/doc/";
                    string folderPath = Server.MapPath(customfolder);
                    Directory.CreateDirectory(folderPath); // safe to call even if it already exists

                    upResults.SaveAs(Path.Combine(folderPath, resultsFileName));

                    SqlCommand resCmd = new SqlCommand();
                    resCmd.Connection = con;
                    resCmd.CommandText = "INSERT INTO tblresults(res_file_name, res_upload, stu_id) VALUES(@rfname, @upload, @sid)";
                    resCmd.Parameters.AddWithValue("@rfname", resultsFileName);
                    resCmd.Parameters.AddWithValue("@upload", customfolder + resultsFileName);
                    resCmd.Parameters.AddWithValue("@sid", newStuId);
                    resCmd.ExecuteNonQuery();
                }

                //call the sendemail method
                sendemail();
                con.Close();
                Response.Redirect("login.aspx");
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlGrade.SelectedIndex = 0;
            txtfname.Text = "";
            txtlname.Text = "";
            txtdob.Text = "";
            ddlDistrict.SelectedIndex = 0;
            txtmob.Text = "";
            txtemail.Text = "";
            txtuname.Text = "";
            txtpass.Text = "";
            txtcpass.Text = "";
        }
        private void GetDistrictlist()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.StoredProcedure;
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
        private void GetGradelist()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.CommandText = "sp_ViewGradeNames";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        // Bind the data to the DropDownList
                        ddlGrade.DataSource = dt;
                        ddlGrade.DataTextField = "Grade_Name";
                        ddlGrade.DataValueField = "Grade_Id";
                        ddlGrade.DataBind();

                        Dictionary<int, int> gradeToLevel = new Dictionary<int, int>();
                        foreach (DataRow row in dt.Rows)
                        {
                            int gradeId = Convert.ToInt32(row["Grade_Id"]);
                            int lvlId = Convert.ToInt32(row["Lvl_Id"]);
                            if (!gradeToLevel.ContainsKey(gradeId))
                                gradeToLevel.Add(gradeId, lvlId);
                        }
                        ViewState["GradeToLevelMap"] = gradeToLevel;
                    }
                }
            }
        }
        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                case ".webp":
                    return true;
                default:
                    return false;
            }
        }
        private void sendemail()
        {
            string filen = "reviwer2.jpg";
            //Check whether the fileupload contains a file
            if (fupprofilepic.HasFile)
            {
                if (CheckFileType(fupprofilepic.FileName))
                {
                    filen = Path.GetFileName(fupprofilepic.PostedFile.FileName);
                }
            }

            SmtpSection smtpSection =
            (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage m = new MailMessage(smtpSection.From, txtemail.Text.Trim()))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Student Registration Process";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + txtuname.Text + ", your registration is successful, thank you for signing up. You may click the link below to start using your account immediately");
                    //msgBody.Append(txtbody.Text.Trim());
                    m.Attachments.Add(new Attachment(Server.MapPath("~/images/") + filen));
                    msgBody.Append("<a href='https://" +
                    HttpContext.Current.Request.Url.Authority + "login'>Click here to login to...</ a > ");
                    m.Body = msgBody.ToString();
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    NetworkCredential networkCred = new
                    NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = networkCred;
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);
                    Response.Write("Email Sent successfully");
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length < 6 && args.Value.Length > 10)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
        private int GetStudentType()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.CommandText = "sp_GetStudentType";
                    conn.Open();
                    return Convert.ToInt32(scmd.ExecuteScalar());
                }
            }
        }
        private int GetLevelIdForGrade(int gradeId)
        {
            Dictionary<int, int> map = ViewState["GradeToLevelMap"] as Dictionary<int, int>;
            if (map != null && map.ContainsKey(gradeId))
                return map[gradeId];

            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.StoredProcedure;
                    scmd.CommandText = "sp_ViewGradeNames";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["Grade_Id"]) == gradeId)
                                return Convert.ToInt32(reader["Lvl_Id"]);
                        }
                    }
                }
            }
            throw new Exception("Could not resolve Lvl_Id for the selected grade.");
        }
        private bool CheckDocFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return ext == ".doc" || ext == ".docx" || ext == ".pdf";
        }
    }
}
