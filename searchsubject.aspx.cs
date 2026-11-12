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
    public partial class searchsubject : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCategorylist();
                GetGradelist();

                ddlgrade.Items.Insert(0, new ListItem("Choose a Grade Level", "-1"));
                ddlcategory.Items.Insert(0, new ListItem("Choose a Category", "-1"));
            }
        }

        protected void txtsubid_TextChanged(object sender, EventArgs e)
        {
            //Add the following codes in a TextChanged event
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //create the movieid parameter
            //assign the parameter to the sql statement
            cmd.CommandText = "sp_ViewSubjectsandGrade where sub_id=@subid";

            cmd.Parameters.AddWithValue("@subid", txtsubid.Text.Trim());


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
        private void GetCategorylist()
        {
            using (SqlConnection conn = new SqlConnection(_conString))
            {
                using (SqlCommand scmd = new SqlCommand())
                {
                    scmd.Connection = conn;
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "sp_ViewCategories";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        // Bind the data to the DropDownList
                        ddlcategory.DataSource = reader;
                        ddlcategory.DataTextField = "Cat_Name";
                        ddlcategory.DataValueField = "Cat_Id";
                        ddlcategory.DataBind();
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
                    scmd.CommandType = CommandType.Text;
                    scmd.CommandText = "sp_ViewGradeNames";
                    conn.Open();
                    using (SqlDataReader reader = scmd.ExecuteReader())
                    {
                        // Bind the data to the DropDownList
                        ddlgrade.DataSource = reader;
                        ddlgrade.DataTextField = "Grade_Name";
                        ddlgrade.DataValueField = "Grade_Id";
                        ddlgrade.DataBind();
                    }
                }
            }
        }

        protected void ddlcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubjects();
        }

        protected void ddlgrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubjects();
        }

        private void GetSubjects()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                // Build dynamic query
                string query = @"
                    SELECT 
                        ts.TS_Id,
                s.Sub_Name,
                s.Sub_Desc,
                g.Grade_Name,
                c.Cat_Name,
                ts.Fees,
                ts.Curr_Enroll,
                ts.Max_Enroll,
                u.F_Name + ' ' + u.L_Name AS TutorName,
                CASE WHEN es.Enroll_Id IS NULL THEN 0 ELSE 1 END AS IsEnrolled
            FROM tblTutorSubjects ts
            INNER JOIN tblSubjects s ON ts.Sub_Id = s.Sub_Id
            LEFT JOIN tblGrade g ON s.Grade_Id = g.Grade_Id
            LEFT JOIN tblCategory c ON s.Cat_Id = c.Cat_Id
            INNER JOIN tblTutors t ON ts.Tut_Id = t.Tut_Id
            INNER JOIN tblUser u ON t.U_Id = u.U_Id
            LEFT JOIN tblStudents st ON st.U_Id = @studentUserId
            LEFT JOIN tblEnrolledSubjects es ON es.TS_Id = ts.TS_Id AND es.Stu_Id = st.Stu_Id
            WHERE ts.TS_Availability = 'True' AND ts.is_Active = 1";
                if (!string.IsNullOrEmpty(txttutorname.Text.Trim()))
                {
                    query += " AND u.F_Name LIKE @tutorname OR u.L_Name LIKE @tutorname";
                }

                if (!string.IsNullOrEmpty(txtsubname.Text.Trim()))
                {
                    query += " AND s.Sub_Name LIKE @subname";
                }

                if (!string.IsNullOrEmpty(txtsubid.Text.Trim()))
                {
                    query += " AND s.Sub_Id = @subid";
                }

                if (ddlgrade.SelectedIndex > 0 && ddlgrade.SelectedValue != "-1")
                {
                    query += " AND s.Grade_Id = @gradeid";
                }

                if (ddlcategory.SelectedIndex > 0 && ddlcategory.SelectedValue != "-1")
                {
                    query += " AND s.Cat_Id = @categoryid";
                }

                query += " ORDER BY s.Sub_Name";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@studentUserId", Session["userid"]);
                if (!string.IsNullOrEmpty(txttutorname.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@tutorname", "%" + txttutorname.Text.Trim() + "%");
                }

                if (!string.IsNullOrEmpty(txtsubname.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@subname", "%" + txtsubname.Text.Trim() + "%");
                }

                if (!string.IsNullOrEmpty(txtsubid.Text.Trim()))
                {
                    cmd.Parameters.AddWithValue("@subid", txtsubid.Text.Trim());
                }

                if (ddlgrade.SelectedIndex > 0 && ddlgrade.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@gradeid", ddlgrade.SelectedValue);
                }

                if (ddlcategory.SelectedIndex > 0 && ddlcategory.SelectedValue != "-1")
                {
                    cmd.Parameters.AddWithValue("@categoryid", ddlcategory.SelectedValue);
                }

                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvs.DataSource = dt;
                gvs.DataBind();

            }
        }
        protected void gvs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs.PageIndex = e.NewPageIndex;
            GetSubjects();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetSubjects();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtsubname.Text = "";
            txtsubid.Text = "";
            txttutorname.Text = "";
            ddlgrade.SelectedIndex = 0;
            ddlcategory.SelectedIndex = 0;
        }

        protected void gvs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "joinClass")
            {
                int tsId = Convert.ToInt32(e.CommandArgument);
                JoinClass(tsId);
            }
        }
        private void JoinClass(int tsId)
        {
            if (Session["userid"] == null || Session["typeid"] == null || Session["typeid"].ToString() != "3")
            {
                lblmsg.Text = "Only students can join classes.";
                lblmsg.CssClass = "text-danger";
                return;
            }

            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand stuCmd = con.CreateCommand();
                stuCmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                stuCmd.Parameters.AddWithValue("@uid", userId);
                int stuId = Convert.ToInt32(stuCmd.ExecuteScalar());

                SqlCommand checkCmd = con.CreateCommand();
                checkCmd.CommandText = "SELECT ts.Curr_Enroll, ts.Max_Enroll, " +
                                        "(SELECT COUNT(*) FROM tblEnrolledSubjects WHERE Stu_Id = @stuId AND TS_Id = @tsId) AS AlreadyEnrolled " +
                                        "FROM tblTutorSubjects ts WHERE ts.TS_Id = @tsId";
                checkCmd.Parameters.AddWithValue("@stuId", stuId);
                checkCmd.Parameters.AddWithValue("@tsId", tsId);

                int currEnroll, maxEnroll, alreadyEnrolled;
                using (SqlDataReader rdr = checkCmd.ExecuteReader())
                {
                    if (!rdr.Read())
                    {
                        lblmsg.Text = "Class not found.";
                        lblmsg.CssClass = "text-danger";
                        return;
                    }
                    currEnroll = Convert.ToInt32(rdr["Curr_Enroll"]);
                    maxEnroll = Convert.ToInt32(rdr["Max_Enroll"]);
                    alreadyEnrolled = Convert.ToInt32(rdr["AlreadyEnrolled"]);
                }

                if (alreadyEnrolled > 0)
                {
                    lblmsg.Text = "You're already enrolled in this class.";
                    lblmsg.CssClass = "text-warning";
                    return;
                }

                if (currEnroll >= maxEnroll)
                {
                    lblmsg.Text = "This class is full.";
                    lblmsg.CssClass = "text-danger";
                    return;
                }

                SqlCommand insCmd = con.CreateCommand();
                insCmd.CommandText = "INSERT INTO tblEnrolledSubjects (Stu_Id, TS_Id) VALUES (@stuId, @tsId)";
                insCmd.Parameters.AddWithValue("@stuId", stuId);
                insCmd.Parameters.AddWithValue("@tsId", tsId);
                insCmd.ExecuteNonQuery();

                SqlCommand updCmd = con.CreateCommand();
                updCmd.CommandText = "UPDATE tblTutorSubjects SET Curr_Enroll = Curr_Enroll + 1 WHERE TS_Id = @tsId";
                updCmd.Parameters.AddWithValue("@tsId", tsId);
                updCmd.ExecuteNonQuery();

                XPHelper.AwardXP(con, stuId, "JoinClass", "Joined a new subject");
            }

            lblmsg.Text = "You've successfully joined the class!";
            lblmsg.CssClass = "text-success";
            GetSubjects();
        }
    }
}