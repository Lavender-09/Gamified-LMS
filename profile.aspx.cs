using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace MoProf_bse25aft1_250429135
{
    public partial class profile : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    LoadProfile();
                    SetEditMode(false);
                    GetGradelist();
                }
            }
        }

        private void LoadProfile()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM tbluser WHERE username=@uname", con);
            cmd.Parameters.AddWithValue("@uname", Session["username"]);

            DataTable dt = new DataTable();
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                dt.Load(reader); // fully consumes and closes this reader before we query again
            }

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                txtFname.Text = row["f_name"].ToString();
                txtLname.Text = row["l_name"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtdob.Text = row["dob"].ToString();
                txtgender.Text = row["gender"].ToString();
                txtPhone.Text = row["phone"].ToString();
                txtregdate.Text = row["reg_date"].ToString();
                txtuname.Text = row["username"].ToString();

                int typeid = Convert.ToInt32(row["type_id"]);
                int userId = Convert.ToInt32(row["u_id"]);
                ViewState["typeid"] = typeid;

                SqlCommand rolecmd = new SqlCommand();
                rolecmd.Connection = con;
                rolecmd.CommandType = CommandType.Text;

                if (typeid == 2) //tutor
                {
                    radavail.Visible = true;
                    radunavail.Visible = true;
                    lblavail.Visible = true;
                    lblunavail.Visible = true;
                    upDoc.Visible = false;
                    lblresults.Visible = false;
                    txtfilename.Visible = false;
                    btnAdd.Visible = false;
                    lbllvl.Visible = false;
                    lblgrade.Visible = false;
                    lblquals.Visible = true;
                    lblskills.Visible = true;
                    lblexp.Visible = true;
                    lbltitle.Visible = true;
                    ddlTitle.Visible = true;
                    txtlvl.Visible = false;
                    ddlgrade.Visible = false;
                    txtquals.Visible = true;
                    txtskills.Visible = true;
                    txtexp.Visible = true;

                    rfvlvl.Enabled = false;
                    rfvgrade.Enabled = false;
                    rfvtitle.Enabled = true;
                    rfvquals.Enabled = true;
                    rfvskills.Enabled = true;
                    rfvexp.Enabled = true;

                    // Named columns instead of * to avoid SELECT * collision/rename issues
                    rolecmd.CommandText = "select tut_id, title, tut_quals, tut_skills, tut_experience, tut_availability " +
                                          "from tblTutors where u_id = @uid";
                    rolecmd.Parameters.AddWithValue("@uid", userId);
                }
                else if (typeid == 3) //student
                {
                    radavail.Visible = false;
                    radunavail.Visible = false;
                    lblavail.Visible = false;
                    lblunavail.Visible = false;
                    upDoc.Visible = true;
                    lblresults.Visible = true;
                    txtfilename.Visible = true;
                    btnAdd.Visible = false;
                    lbllvl.Visible = true;
                    lblgrade.Visible = true;
                    lblquals.Visible = false;
                    lblskills.Visible = false;
                    lblexp.Visible = false;
                    lbltitle.Visible = false;
                    ddlTitle.Visible = false;
                    txtlvl.Visible = true;
                    ddlgrade.Visible = true;
                    txtquals.Visible = false;
                    txtskills.Visible = false;
                    txtexp.Visible = false;

                    rfvtitle.Enabled = false;
                    rfvquals.Enabled = false;
                    rfvskills.Enabled = false;
                    rfvexp.Enabled = false;
                    rfvlvl.Enabled = true;
                    rfvgrade.Enabled = true;

                    // Named columns - g.grade_id/s.grade_id would otherwise collide under SELECT *
                    rolecmd.CommandText = "select s.stu_id, s.grade_id, g.grade_name, l.lvl_name " +
                                          "from tblstudents s, tblgrade g, tbllevel l " +
                                          "where s.u_id = @uid and l.lvl_id = s.lvl_id and g.grade_id = s.grade_id";
                    rolecmd.Parameters.AddWithValue("@uid", userId);
                }

                DataTable dt2 = new DataTable();
                using (SqlDataReader reader2 = rolecmd.ExecuteReader())
                {
                    dt2.Load(reader2);
                }

                if (dt2.Rows.Count > 0)
                {
                    DataRow row2 = dt2.Rows[0];

                    if (typeid == 2)
                    {
                        ddlTitle.SelectedValue = row2["title"].ToString();
                        txtquals.Text = row2["tut_quals"].ToString();
                        txtskills.Text = row2["tut_skills"].ToString();
                        txtexp.Text = row2["tut_experience"].ToString();

                        string availability = row2["tut_availability"].ToString();
                        radavail.Checked = (availability == "Available");
                        radunavail.Checked = (availability == "Unavailable");
                    }
                    else if (typeid == 3)
                    {
                        ddlgrade.SelectedValue = row2["grade_id"].ToString();
                        txtlvl.Text = row2["lvl_name"].ToString();

                        SqlCommand cmdRes = new SqlCommand("SELECT res_file_name FROM tblresults WHERE stu_id = @sid", con);
                        cmdRes.Parameters.AddWithValue("@sid", row2["stu_id"]);

                        object resResult = cmdRes.ExecuteScalar();
                        txtfilename.Text = (resResult != null) ? resResult.ToString() : "";
                    }
                }

                rptimg.DataSource = dt;
                rptimg.DataBind();
            }

            con.Close();
        }

        private void SetEditMode(bool editing)
        {
            bool readOnly = !editing;
            txtdob.ReadOnly = readOnly;
            txtEmail.ReadOnly = readOnly;
            txtexp.ReadOnly = readOnly;
            txtFname.ReadOnly = readOnly;
            txtgender.ReadOnly = readOnly;
            txtLname.ReadOnly = readOnly;
            txtPhone.ReadOnly = readOnly;
            txtquals.ReadOnly = readOnly;
            txtregdate.ReadOnly = readOnly;
            txtskills.ReadOnly = readOnly;
            txtuname.ReadOnly = readOnly;
            txtfilename.ReadOnly = readOnly;
            // txtlvl is always read-only - it's derived from the grade selection, never typed directly
            radavail.Enabled = !readOnly;
            radunavail.Enabled = !readOnly;
            ddlTitle.Enabled = !readOnly;
            ddlgrade.Enabled = !readOnly;

            btnEdit.Visible = !editing;
            btnCancel.Visible = editing;
            btnSubmit.Visible = editing;
            btnAdd.Visible = editing;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int typeid = ViewState["typeid"] != null ? Convert.ToInt32(ViewState["typeid"]) : 0;
            string availability = radunavail.Checked ? "Unavailable" : "Available";

            string sql = "UPDATE tblUser SET f_name=@fname, l_name=@lname, email=@email, dob=@dob, " +
                         "gender=@gender, phone=@mob, reg_date=@reg WHERE username=@uname";

            if (typeid == 3)
            {
                sql += "; UPDATE s SET s.grade_id=@gid, s.lvl_id=@lid FROM tblstudents s, tbluser u " +
                       "WHERE s.u_id=u.u_id AND username=@uname";
            }
            else if (typeid == 2)
            {
                sql += "; UPDATE t SET t.title=@title, t.tut_quals=@quals, t.tut_skills=@skills, " +
                       "t.tut_experience=@exp, t.tut_availability=@avail FROM tbltutors t, tbluser u " +
                       "WHERE t.u_id=u.u_id AND username=@uname";
            }

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand ucmd = new SqlCommand(sql, con);

            ucmd.Parameters.AddWithValue("@uname", Session["username"]);
            ucmd.Parameters.AddWithValue("@fname", txtFname.Text.Trim());
            ucmd.Parameters.AddWithValue("@lname", txtLname.Text.Trim());
            ucmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            ucmd.Parameters.AddWithValue("@dob", txtdob.Text.Trim());
            ucmd.Parameters.AddWithValue("@gender", txtgender.Text.Trim());
            ucmd.Parameters.AddWithValue("@mob", txtPhone.Text.Trim());
            ucmd.Parameters.AddWithValue("@reg", txtregdate.Text.Trim());

            if (typeid == 3)
            {
                var map = ViewState["GradeToLvl"] as Dictionary<int, int>;
                int gradeId = Convert.ToInt32(ddlgrade.SelectedValue);
                int lvlId = (map != null && map.ContainsKey(gradeId)) ? map[gradeId] : 0;

                ucmd.Parameters.AddWithValue("@gid", gradeId);
                ucmd.Parameters.AddWithValue("@lid", lvlId);
            }
            else if (typeid == 2)
            {
                ucmd.Parameters.AddWithValue("@title", ddlTitle.SelectedValue);
                ucmd.Parameters.AddWithValue("@quals", txtquals.Text.Trim());
                ucmd.Parameters.AddWithValue("@skills", txtskills.Text.Trim());
                ucmd.Parameters.AddWithValue("@exp", txtexp.Text.Trim());
                ucmd.Parameters.AddWithValue("@avail", availability);
            }

            try
            {
                con.Open();
                int updated = ucmd.ExecuteNonQuery();
                lblmsg.Text = updated + " record(s) updated.";
            }
            catch (Exception)
            {
                lblmsg.Text = "Error Updating";
            }
            finally
            {
                con.Close();
            }

            SetEditMode(false);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SetEditMode(true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LoadProfile();
            SetEditMode(false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //check if file upload contains a file
            if (upDoc.HasFile)
            {
                //add the filename as an argument
                if (CheckFileType(upDoc.FileName))
                {
                    //Add a folder bearing the Username in the path below
                    string customfolder = "~/files/" + Session["username"] + "/doc/";
                    string folderPath = Server.MapPath(customfolder);
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    //search database to see if the given filename already exist
                    cmd.CommandText = "SELECT res_file_name FROM tblresults WHERE res_file_name = @rfn";
                    cmd.Parameters.AddWithValue("@rfn", txtfilename.Text);

                    SqlDataReader reader;
                    con.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        lblmsg.Text = "Choose Another Filename";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        reader.Close();

                        int stuId;
                        SqlCommand cmdStu = new SqlCommand();
                        cmdStu.Connection = con;
                        cmdStu.CommandType = CommandType.Text;
                        cmdStu.CommandText = "SELECT stu_id FROM tblstudents, tbluser WHERE tblstudents.u_id = tbluser.u_id AND tbluser.u_id = @uid";
                        cmdStu.Parameters.AddWithValue("@uid", Session["userid"]);

                        object result = cmdStu.ExecuteScalar();
                        if (result == null)
                        {
                            lblmsg.Text = "Student record not found for this account.";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            con.Close();
                            return;
                        }
                        stuId = Convert.ToInt32(result);

                        Directory.CreateDirectory(folderPath);
                        upDoc.SaveAs(folderPath + upDoc.FileName);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.Connection = con;
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "INSERT INTO tblresults(res_file_name, res_upload, stu_id) VALUES(@rfname, @upload, @sid)";
                        cmd1.Parameters.AddWithValue("@rfname", txtfilename.Text);
                        cmd1.Parameters.AddWithValue("@upload", customfolder + upDoc.FileName);
                        cmd1.Parameters.AddWithValue("@sid", stuId);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        lblmsg.Text = "File has been successfully uploaded!";
                    }
                }
            }
            bool CheckFileType(string fileName)
            {
                string ext = Path.GetExtension(fileName);
                switch (ext.ToLower())
                {
                    case ".doc":
                        return true;
                    case ".docx":
                        return true;
                    case ".pdf":
                        return true;
                    case ".jpg":
                        return true;
                    case ".jpeg":
                        return true;
                    case ".png":
                        return true;
                    default:
                        return false;
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
                        ddlgrade.DataSource = dt;
                        ddlgrade.DataTextField = "Grade_Name";
                        ddlgrade.DataValueField = "Grade_Id";
                        ddlgrade.DataBind();

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

        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            var map = ViewState["GradeToLvl"] as Dictionary<int, int>;
            if (map == null) return;

            int gradeId;
            if (int.TryParse(ddlgrade.SelectedValue, out gradeId) && map.ContainsKey(gradeId))
            {
                int lvlId = map[gradeId];

                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand("SELECT lvl_name FROM tbllevel WHERE lvl_id = @lid", con);
                cmd.Parameters.AddWithValue("@lid", lvlId);

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                txtlvl.Text = (result != null) ? result.ToString() : "";
            }
            else
            {
                txtlvl.Text = "";
            }
        }

        protected void btnUploadProfilePic_Click(object sender, EventArgs e)
        {
            if (!fupNewProfilePic.HasFile)
            {
                lblPicMsg.Text = "Please choose an image to upload.";
                lblPicMsg.CssClass = "text-danger";
                return;
            }

            if (!CheckImageFileType(fupNewProfilePic.FileName))
            {
                lblPicMsg.Text = "Only .jpg, .jpeg, .png, .webp, and .gif files are allowed.";
                lblPicMsg.CssClass = "text-danger";
                return;
            }

            int userId = Convert.ToInt32(Session["userid"]);

            // Use the user's ID in the filename to keep it unique and avoid overwriting other users' pictures
            string ext = Path.GetExtension(fupNewProfilePic.FileName);
            string fileName = "user_" + userId + ext;

            string folderPath = Server.MapPath("~/images/");
            fupNewProfilePic.SaveAs(Path.Combine(folderPath, fileName));

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE tblUser SET profilepic = @pic WHERE u_id = @uid";
                cmd.Parameters.AddWithValue("@pic", fileName);
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.ExecuteNonQuery();
            }

            lblPicMsg.Text = "Profile picture updated!";
            lblPicMsg.CssClass = "text-success";

            LoadProfile();
        }
        private bool CheckImageFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            return ext == ".gif" || ext == ".png" || ext == ".jpg" || ext == ".jpeg" || ext == ".webp";
        }
    }
}