using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class dashboard : System.Web.UI.Page
    {
        private string _conString =
WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }
            if (!IsPostBack)
            {
                BindMySubjects();
                BindArchSubjects();
                BindXpProgress();
            }
        }
        private void BindMySubjects()
        {
            int userId = Convert.ToInt32(Session["userid"]);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ts.TS_Id, sub.Sub_Name, sub.Sub_Desc, g.Grade_Name, " +
                               "u.F_Name + ' ' + u.L_Name AS TutorName " +
                               "FROM tblEnrolledSubjects es, tblStudents st, tblTutorSubjects ts, tblSubjects sub, tblGrade g, tblTutors t, tblUser u " +
                               "WHERE es.TS_Id = ts.TS_Id AND es.Stu_Id = st.Stu_Id AND ts.Sub_Id = sub.Sub_Id " +
                               "AND g.Grade_Id = sub.Grade_Id AND ts.Tut_Id = t.Tut_Id AND t.U_Id = u.U_Id " +
                               "AND ts.is_Active = 1 AND st.U_Id = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            lvMySubjects.DataSource = dt;
            lvMySubjects.DataBind();
        }

        private void BindArchSubjects()
        {
            int userId = Convert.ToInt32(Session["userid"]);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ts.TS_Id, sub.Sub_Name, sub.Sub_Desc, g.Grade_Name, " +
                               "u.F_Name + ' ' + u.L_Name AS TutorName " +
                               "FROM tblEnrolledSubjects es, tblStudents st, tblTutorSubjects ts, tblSubjects sub, tblGrade g, tblTutors t, tblUser u " +
                               "WHERE es.TS_Id = ts.TS_Id AND es.Stu_Id = st.Stu_Id AND ts.Sub_Id = sub.Sub_Id " +
                               "AND g.Grade_Id = sub.Grade_Id AND ts.Tut_Id = t.Tut_Id AND t.U_Id = u.U_Id " +
                               "AND ts.is_Active = 0 AND st.U_Id = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            lvArchSubjects.DataSource = dt;
            lvArchSubjects.DataBind();
        }
        private void BindXpProgress()
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand stuCmd = con.CreateCommand();
                stuCmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                stuCmd.Parameters.AddWithValue("@uid", userId);
                object stuIdObj = stuCmd.ExecuteScalar();

                if (stuIdObj == null)
                {
                    return; // shouldn't happen for a logged-in student, but safe guard
                }
                int stuId = Convert.ToInt32(stuIdObj);

                SqlCommand progCmd = con.CreateCommand();
                progCmd.CommandText = "SELECT xp.Total_XP, lvl.XL_Name, lvl.XP_Required " +
                                       "FROM tblXPProgress xp, tblXPLevel lvl " +
                                       "WHERE xp.XL_Id = lvl.XL_Id AND xp.Stu_Id = @stuid";
                progCmd.Parameters.AddWithValue("@stuid", stuId);

                int totalXp = 0;
                string levelName = "Newcomer";
                int currentThreshold = 0;

                using (SqlDataReader rdr = progCmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        totalXp = Convert.ToInt32(rdr["Total_XP"]);
                        levelName = rdr["XL_Name"].ToString();
                        currentThreshold = Convert.ToInt32(rdr["XP_Required"]);
                    }
                }

                // Find the next level's threshold (the smallest XP_Required greater than current total)
                SqlCommand nextCmd = con.CreateCommand();
                nextCmd.CommandText = "SELECT TOP 1 XP_Required FROM tblXPLevel WHERE XP_Required > @xp ORDER BY XP_Required ASC";
                nextCmd.Parameters.AddWithValue("@xp", totalXp);
                object nextThresholdObj = nextCmd.ExecuteScalar();

                lblLevelName.Text = levelName;

                if (nextThresholdObj == null)
                {
                    // already at the max level
                    lblXpText.Text = totalXp + " XP (Max Level)";
                    litXpBar.Text = "<div class=\"progress-bar bg-success\" style=\"width:100%\">100%</div>";
                }
                else
                {
                    int nextThreshold = Convert.ToInt32(nextThresholdObj);
                    int xpIntoLevel = totalXp - currentThreshold;
                    int xpNeededForLevel = nextThreshold - currentThreshold;
                    int percent = xpNeededForLevel > 0 ? (int)((double)xpIntoLevel / xpNeededForLevel * 100) : 0;

                    lblXpText.Text = totalXp + " / " + nextThreshold + " XP";
                    litXpBar.Text = "<div class=\"progress-bar bg-success\" style=\"width:" + percent + "%\">" + percent + "%</div>";
                }
            }
        }
        protected void lvMySubjects_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "leaveClass")
            {
                int tsId = Convert.ToInt32(e.CommandArgument);
                LeaveClass(tsId);
            }
        }
        private void LeaveClass(int tsId)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand stuCmd = con.CreateCommand();
                stuCmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                stuCmd.Parameters.AddWithValue("@uid", userId);
                int stuId = Convert.ToInt32(stuCmd.ExecuteScalar());

                SqlCommand delCmd = con.CreateCommand();
                delCmd.CommandText = "DELETE FROM tblEnrolledSubjects WHERE Stu_Id = @stuId AND TS_Id = @tsId";
                delCmd.Parameters.AddWithValue("@stuId", stuId);
                delCmd.Parameters.AddWithValue("@tsId", tsId);
                int rowsAffected = delCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    SqlCommand updCmd = con.CreateCommand();
                    updCmd.CommandText = "UPDATE tblTutorSubjects SET Curr_Enroll = CASE WHEN Curr_Enroll > 0 THEN Curr_Enroll - 1 ELSE 0 END WHERE TS_Id = @tsId";
                    updCmd.Parameters.AddWithValue("@tsId", tsId);
                    updCmd.ExecuteNonQuery();
                }
            }

            BindMySubjects();
            BindArchSubjects();
        }
    }
}