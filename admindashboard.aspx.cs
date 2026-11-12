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
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class admindashboard : System.Web.UI.Page
    {
        private string _conString =
WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string pieChartData = GetPieChartData();
            ClientScript.RegisterStartupScript(this.GetType(), "piechartScript", $"renderPieChart({pieChartData});", true);

            string barChartData = GetBarChartData();
            ClientScript.RegisterStartupScript(this.GetType(), "barchartScript", $"renderBarChart({barChartData});", true);

            string barChartData2 = GetBarChartData2();
            ClientScript.RegisterStartupScript(this.GetType(), "barchart2Script", $"renderBarChart2({barChartData2});", true);

            getTutorApprovalStatus();
            if (!IsPostBack)
            {
                BindGrades();
                BindCategories();
                BindDistricts();
            }
        }

        protected void lnkdeny_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grvRow = (GridViewRow)lnk.NamingContainer;
            int user_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ucmd = con.CreateCommand();
            ucmd.CommandType = CommandType.Text;
            ucmd.CommandText = "update tblUser set approval_status='0' Where u_id ='" + user_id + "'";
            ucmd.ExecuteNonQuery();
            con.Close();
            getTutorApprovalStatus();
        }

        protected void lnkgrant_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grvRow = (GridViewRow)lnk.NamingContainer;
            int user_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ucmd = con.CreateCommand();
            ucmd.CommandType = CommandType.Text;
            ucmd.CommandText = "update tblUser set approval_status='1' Where u_id ='" + user_id + "'";
            ucmd.ExecuteNonQuery();

            SqlCommand scmd = con.CreateCommand();
            scmd.CommandType = CommandType.Text;
            scmd.CommandText = "SELECT username, email, f_name, l_name FROM tbluser WHERE u_id = @uid";
            scmd.Parameters.AddWithValue("@uid", user_id);

            string uname = null, email = null, fname = null;
            using (SqlDataReader rdr = scmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    uname = rdr["username"].ToString();
                    email = rdr["email"].ToString();
                    fname = rdr["f_name"].ToString();
                }
            }
            if (!string.IsNullOrEmpty(email))
            {
                sendApprovalEmail(email, fname);
            }
            con.Close();
            getTutorApprovalStatus();
        }

        protected void gvs_PreRender(object sender, EventArgs e)
        {
            if (gvs.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        void getTutorApprovalStatus()
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand ccmd = con.CreateCommand();
            ccmd.CommandType = CommandType.Text;
            ccmd.CommandText = "Select tu.u_id as U_Id, ";
            ccmd.CommandText += "tu.f_name as F_Name, ";
            ccmd.CommandText += "tu.l_name as L_Name, ";
            ccmd.CommandText += "tu.username as UserName, ";
            ccmd.CommandText += "tu.profilepic as ProfilePic, ";
            ccmd.CommandText += "tu.approval_status as Approval_Status, ";
            ccmd.CommandText += "tut.tut_id as Tut_Id, ";
            ccmd.CommandText += "tu.reg_date as Reg_Date ";
            ccmd.CommandText += "from tblUser tu, tblTutors tut ";
            ccmd.CommandText += "where tu.u_id = tut.u_id ";
            //ccmd.CommandText += "and tu.approval_status = '0' ";
            SqlDataAdapter sqlda = new SqlDataAdapter(ccmd.CommandText, con);
            DataTable dta = new DataTable();
            sqlda.Fill(dta);
            con.Close();
            gvs.DataSource = dta;
            gvs.DataBind();
        }
        private void sendApprovalEmail(string toEmail, string name)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            using (MailMessage m = new MailMessage(smtpSection.From, toEmail))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Your Tutor Application Has Been Approved";
                    m.IsBodyHtml = true;
                    StringBuilder msgBody = new StringBuilder();
                    msgBody.Append("Dear " + name + ", your tutor account has been approved. You may click the link below to start using your account immediately");
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

        protected void gvSubjects_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editSubject")
            {
                int subId = Convert.ToInt32(e.CommandArgument);
                LoadSubjectForEdit(subId);
            }
        }

        protected void btnAddSubject_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedGradeId"] == null)
            {
                return;
            }
            hidSubId.Value = "";
            txtSubName.Text = "";
            txtSubDesc.Text = "";
            BindCategoryDropdown();
            pnlSubjectForm.Visible = true;
        }

        protected void btnSaveSubject_Click(object sender, EventArgs e)
        {
            int gradeId = Convert.ToInt32(ViewState["SelectedGradeId"]);
            bool isEdit = !string.IsNullOrEmpty(hidSubId.Value);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            if (isEdit)
            {
                cmd.CommandText = "UPDATE tblSubjects SET Sub_Name=@name, Sub_Desc=@desc, Cat_Id=@cat " +
                                   "WHERE Sub_Id=@id";
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hidSubId.Value));
            }
            else
            {
                cmd.CommandText = "INSERT INTO tblSubjects (Sub_Name, Sub_Desc, Cat_Id, Grade_Id) " +
                                   "VALUES (@name, @desc, @cat, @gradeId)";
                cmd.Parameters.AddWithValue("@gradeId", gradeId);
            }

            cmd.Parameters.AddWithValue("@name", txtSubName.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", txtSubDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@cat", ddlCategory.SelectedValue);
            cmd.ExecuteNonQuery();
            con.Close();

            pnlSubjectForm.Visible = false;
            BindSubjects(gradeId);
        }

        protected void btnCancelSubject_Click(object sender, EventArgs e)
        {
            pnlSubjectForm.Visible = false;
        }

        protected void rptGrades_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "selectGrade")
            {
                int gradeId = Convert.ToInt32(e.CommandArgument);
                ViewState["SelectedGradeId"] = gradeId;
                pnlSubjectForm.Visible = false;
                BindSubjects(gradeId);
            }
        }
        private void BindGrades()
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Grade_Id, Grade_Name FROM tblGrade ORDER BY Grade_Id";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptGrades.DataSource = dt;
            rptGrades.DataBind();
        }
        private void BindCategoryDropdown()
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Cat_Id, Cat_Name FROM tblCategory ORDER BY Cat_Name";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            ddlCategory.DataSource = dt;
            ddlCategory.DataBind();
        }
        private void LoadSubjectForEdit(int subId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Sub_Name, Sub_Desc, Cat_Id FROM tblSubjects WHERE Sub_Id = @subId";
            cmd.Parameters.AddWithValue("@subId", subId);

            BindCategoryDropdown(); // populate ddlCategory first

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    hidSubId.Value = subId.ToString();
                    txtSubName.Text = rdr["Sub_Name"].ToString();
                    txtSubDesc.Text = rdr["Sub_Desc"].ToString();
                    ddlCategory.SelectedValue = rdr["Cat_Id"].ToString();
                }
            }
            con.Close();

            pnlSubjectForm.Visible = true;
        }
        private void BindSubjects(int gradeId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT s.Sub_Id, s.Sub_Name, s.Sub_Desc, c.Cat_Name " +
                               "FROM tblSubjects s, tblCategory c " +
                               "WHERE s.cat_id = c.Cat_Id AND s.grade_id = @gradeId";
            cmd.Parameters.AddWithValue("@gradeId", gradeId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            gvSubjects.DataSource = dt;
            gvSubjects.DataBind();

            lblSelectedGrade.Text = "";
        }

        protected void btnDeleteSubject_Click(object sender, EventArgs e)
        {
            int gradeId = Convert.ToInt32(ViewState["SelectedGradeId"]);
            bool hasSelectedValue = !string.IsNullOrEmpty(hidSubId.Value);

            if (!hasSelectedValue)
            {
                return;
            }

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = "DELETE FROM tblSubjects WHERE Sub_Id=@id";
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hidSubId.Value));
            cmd.ExecuteNonQuery();
            con.Close();

            pnlSubjectForm.Visible = false;
            BindSubjects(gradeId);
        }

        protected void rptCat_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "selectCategory")
            {
                int catId = Convert.ToInt32(e.CommandArgument);
                ViewState["SelectedCategoryId"] = catId;
                LoadCategoryForEdit(catId);
            }
        }

        protected void btnAddCategory_Click(object sender, EventArgs e)
        {
            hidCatId.Value = "";
            txtcatname.Text = "";
            txtcatdesc.Text = "";
            pnlCategory.Visible = true;
        }

        protected void btnSaveCat_Click(object sender, EventArgs e)
        {
            int catId = Convert.ToInt32(ViewState["SelectedCategoryId"]);
            bool isEdit = !string.IsNullOrEmpty(hidCatId.Value);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            if (isEdit)
            {
                cmd.CommandText = "UPDATE tblcategory SET Cat_Name=@name, Cat_Desc=@desc WHERE Cat_Id=@catid";
                cmd.Parameters.AddWithValue("@catid", Convert.ToInt32(hidCatId.Value));
            }
            else
            {
                cmd.CommandText = "INSERT INTO tblcategory (Cat_Name, Cat_Desc) VALUES (@name, @desc)";
                cmd.Parameters.AddWithValue("@catid", catId);
            }

            cmd.Parameters.AddWithValue("@name", txtcatname.Text.Trim());
            cmd.Parameters.AddWithValue("@desc", txtcatdesc.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

            pnlCategory.Visible = false;
            BindCategories();
        }

        protected void btnCancelCat_Click(object sender, EventArgs e)
        {
            pnlCategory.Visible = false;
        }

        protected void btnDeleteCat_Click(object sender, EventArgs e)
        {
            int catId = Convert.ToInt32(ViewState["SelectedCategoryId"]);
            bool hasSelectedValue = !string.IsNullOrEmpty(hidCatId.Value);

            if (!hasSelectedValue)
            {
                return;
            }

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = "DELETE FROM tblCategory WHERE Cat_Id=@id";
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hidCatId.Value));
            cmd.ExecuteNonQuery();
            con.Close();

            pnlCategory.Visible = false;
            BindCategories();
        }
        private void BindCategories()
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT cat_id, cat_name FROM tblCategory ORDER BY cat_name";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptCat.DataSource = dt;
            rptCat.DataBind();

            lblCategory.Text = "";
        }
        private void LoadCategoryForEdit(int catId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM tblCategory WHERE Cat_Id = @CatId";
            cmd.Parameters.AddWithValue("@CatId", catId);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    hidCatId.Value = catId.ToString();
                    txtcatname.Text = rdr["Cat_Name"].ToString();
                    txtcatdesc.Text = rdr["Cat_Desc"].ToString();
                }
            }
            con.Close();

            pnlCategory.Visible = true;
        }

        protected void rptDis_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "selectDistrict")
            {
                int disId = Convert.ToInt32(e.CommandArgument);
                ViewState["SelectedDistrictId"] = disId;
                LoadDistrictForEdit(disId);
            }
        }

        protected void btnAddDistrict_Click(object sender, EventArgs e)
        {
            hidDisId.Value = "";
            txtdisname.Text = "";
            pnlDistrict.Visible = true;
        }

        protected void btnSaveDis_Click(object sender, EventArgs e)
        {
            int disId = Convert.ToInt32(ViewState["SelectedDistrictId"]);
            bool isEdit = !string.IsNullOrEmpty(hidDisId.Value);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            if (isEdit)
            {
                cmd.CommandText = "UPDATE tbldistricts SET dis_name = @name WHERE dis_id = @disid";
                cmd.Parameters.AddWithValue("@disid", Convert.ToInt32(hidDisId.Value));
            }
            else
            {
                cmd.CommandText = "INSERT INTO tbldistricts (dis_name) VALUES (@name)";
                cmd.Parameters.AddWithValue("@name", txtdisname.Text.Trim());
            }

            cmd.Parameters.AddWithValue("@name", txtdisname.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();

            pnlDistrict.Visible = false;
            BindDistricts();
        }

        protected void btnCancelDis_Click(object sender, EventArgs e)
        {
            pnlDistrict.Visible = false;
        }

        protected void btnDeleteDis_Click(object sender, EventArgs e)
        {
            int disId = Convert.ToInt32(ViewState["SelectedDistrictId"]);
            bool hasSelectedValue = !string.IsNullOrEmpty(hidDisId.Value);

            if (!hasSelectedValue)
            {
                return;
            }

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = "DELETE FROM tbldistricts WHERE dis_id = @id";
            cmd.Parameters.AddWithValue("@id", Convert.ToInt32(hidDisId.Value));
            cmd.ExecuteNonQuery();
            con.Close();

            pnlDistrict.Visible = false;
            BindDistricts();
        }
        private void LoadDistrictForEdit(int disId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM tblDistricts WHERE Dis_Id = @DisId";
            cmd.Parameters.AddWithValue("@DisId", disId);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    hidDisId.Value = disId.ToString();
                    txtdisname.Text = rdr["Dis_Name"].ToString();
                }
            }
            con.Close();

            pnlDistrict.Visible = true;
        }
        private void BindDistricts()
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT dis_Id, dis_name FROM tbldistricts ORDER BY dis_name";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptDis.DataSource = dt;
            rptDis.DataBind();

            lblDistrict.Text = "";
        }
        private string GetPieChartData()
        {
            string query = "SELECT t.type_name, COUNT(u.u_id) as user_count " +
                   "FROM tblUserType t, tblUser u " +
                   "WHERE u.type_id = t.type_id " +
                   "GROUP BY t.type_name";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open(); using (SqlDataReader reader = cmd.ExecuteReader())
                    { //Populate DataTable
                        dt.Load(reader);
                    }
                }
            }
            // Convert data to JSON format
            var chartData = new
            {
                labels = dt.AsEnumerable().Select(row => row["type_name"].ToString()).ToArray(),
                datasets = new[] {
                    new
                    {
                        data = dt.AsEnumerable().Select(row => Convert.ToInt64(row["user_count"])).ToArray(), backgroundColor = new[]
                        {
                            "rgba(255, 99, 132, 0.6)", "rgba(54, 162, 235, 0.6)", "rgba(255, 206, 86, 0.6)"
                        }
                        , borderColor = new[]
                        {
                            "rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)"
                        }
                        , borderWidth = 1
        }
    }
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer(); return serializer.Serialize(chartData);
        }

        private string GetBarChartData()
        {
            string query = "SELECT c.cat_name, COUNT(s.sub_id) as subject_count " +
                   "FROM tblCategory c, tblSubjects s " +
                   "WHERE c.cat_id = s.cat_id " +
                   "GROUP BY c.cat_name";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    { //Populate Datatable
                        dt.Load(reader);
                    }
                }
            }
            var chartData = new
            {
                labels = dt.AsEnumerable().Select(row => row["cat_name"].ToString()).ToArray(),
                datasets = new[]
                {
                                new
                            {
                                    label = "Subjects Classified by Categories", data = dt.AsEnumerable().Select(row => Convert.ToInt64(row["subject_count"])).ToArray(), backgroundColor = new[]
                                    {
                                        "rgba(255, 99, 132, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(255, 206, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(153, 102, 255, 0.2)"
                                    }
                                    , borderColor = new[]
                                    {
                                        "rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(75, 192, 192, 1)", "rgba(153, 102, 255, 1)"
                                    }
                                    , borderWidth = 1
                                }
                            }
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer(); return serializer.Serialize(chartData);
        }
        private string GetBarChartData2()
        {
            string query = "SELECT g.grade_name, COUNT(s.stu_id) as student_count " +
                   "FROM tblStudents s, tblGrade g " +
                   "WHERE g.grade_id = s.grade_id " +
                   "GROUP BY g.grade_name";
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    { //Populate Datatable
                        dt.Load(reader);
                    }
                }
            }
            var chartData = new
            {
                labels = dt.AsEnumerable().Select(row => row["grade_name"].ToString()).ToArray(),
                datasets = new[]
                {
                                new
                            {
                                    label = "Number of Students Classified by Grade", data = dt.AsEnumerable().Select(row => Convert.ToInt64(row["student_count"])).ToArray(), backgroundColor = new[]
                                    {
                                        "rgba(255, 99, 132, 0.2)", "rgba(54, 162, 235, 0.2)", "rgba(255, 206, 86, 0.2)", "rgba(75, 192, 192, 0.2)", "rgba(153, 102, 255, 0.2)"
                                    }
                                    , borderColor = new[]
                                    {
                                        "rgba(255, 99, 132, 1)", "rgba(54, 162, 235, 1)", "rgba(255, 206, 86, 1)", "rgba(75, 192, 192, 1)", "rgba(153, 102, 255, 1)"
                                    }
                                    , borderWidth = 1
                                }
                            }
            };
            JavaScriptSerializer serializer = new JavaScriptSerializer(); return serializer.Serialize(chartData);
        }
    }
}