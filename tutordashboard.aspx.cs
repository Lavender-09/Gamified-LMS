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
    public partial class tutordashboard : System.Web.UI.Page
    {
        private string _conString =
WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMySubjects();
                BindArchSubjects();
            }
        }
        private void BindMySubjects()
        {
            int userId = Convert.ToInt32(Session["userid"]);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT ts.TS_Id, s.Sub_Name, s.Sub_Desc, ts.Fees, " +
                               "ts.TS_Availability, ts.Curr_Enroll, ts.Max_Enroll, ts.is_Active, g.grade_name " +
                               "FROM tblTutorSubjects ts, tblSubjects s, tblTutors t, tblGrade g " +
                               "WHERE ts.Sub_Id = s.Sub_Id AND ts.Tut_Id = t.Tut_Id AND ts.is_active = 1 AND g.grade_id = s.grade_id AND t.U_Id = @userId";
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
            cmd.CommandText = "SELECT ts.TS_Id, s.Sub_Name, s.Sub_Desc, ts.Fees, " +
                               "ts.TS_Availability, ts.Curr_Enroll, ts.Max_Enroll, ts.is_Active, g.grade_name " +
                               "FROM tblTutorSubjects ts, tblSubjects s, tblTutors t, tblGrade g " +
                               "WHERE ts.Sub_Id = s.Sub_Id AND ts.Tut_Id = t.Tut_Id AND ts.is_active = 0 AND g.grade_id = s.grade_id AND t.U_Id = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            lvArchSubjects.DataSource = dt;
            lvArchSubjects.DataBind();
        }
        protected void lnkarchive_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                LinkButton lnk = (LinkButton)sender;
                int tsId = Convert.ToInt32((sender as LinkButton).CommandArgument);

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ts_id FROM tblTutorSubjects WHERE ts_id = @tsid";
                cmd.Parameters.AddWithValue("@tsid", tsId);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close(); 

                    SqlCommand ucmd = con.CreateCommand();
                    ucmd.CommandType = CommandType.Text;
                    ucmd.CommandText = "UPDATE tblTutorSubjects SET is_Active = 0 WHERE TS_Id = @tsid";
                    ucmd.Parameters.AddWithValue("@tsid", tsId);
                    ucmd.ExecuteNonQuery();
                }
            }
        }

        protected void lnkunarchive_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                LinkButton lnk = (LinkButton)sender;
                int tsId = Convert.ToInt32((sender as LinkButton).CommandArgument);

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT ts_id FROM tblTutorSubjects WHERE ts_id = @tsid";
                cmd.Parameters.AddWithValue("@tsid", tsId);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();

                    SqlCommand ucmd = con.CreateCommand();
                    ucmd.CommandType = CommandType.Text;
                    ucmd.CommandText = "UPDATE tblTutorSubjects SET is_Active = 1 WHERE TS_Id = @tsid";
                    ucmd.Parameters.AddWithValue("@tsid", tsId);
                    ucmd.ExecuteNonQuery();
                }
            }
        }
    }
}