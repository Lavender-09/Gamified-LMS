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
    public partial class studentdetails : System.Web.UI.Page
    {
        private string _conString =
WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            int stuId = Convert.ToInt32(Request.QueryString["stu_id"]);
            LoadStudentResults(stuId);

            if (Session["userid"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (!TutorHasAccessToStudent(stuId))
                {
                    Response.Redirect("~/tutordashboard.aspx"); // block viewing students not enrolled in any of this tutor's subjects
                    return;
                }
                LoadStudentDetails(stuId);
            }
        }
        private bool TutorHasAccessToStudent(int stuId)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM tblEnrolledSubjects es, tblTutorSubjects ts, tblTutors t " +
                               "WHERE es.TS_Id = ts.TS_Id AND ts.Tut_Id = t.Tut_Id " +
                               "AND t.U_Id = @userId AND es.Stu_Id = @stuId";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@stuId", stuId);
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count > 0;
        }

        private void LoadStudentDetails(int stuId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT u.F_Name, u.L_Name, u.Username, u.Email, u.Phone, u.ProfilePic, " +
                               "u.Last_Login, g.Grade_Name, d.Dis_Name " +
                               "FROM tblStudents s, tblUser u, tblGrade g, tblDistricts d " +
                               "WHERE s.U_Id = u.U_Id AND s.Grade_Id = g.Grade_Id AND u.Dis_Id = d.Dis_Id " +
                               "AND s.Stu_Id = @stuId";
            cmd.Parameters.AddWithValue("@stuId", stuId);

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    lblStuName.Text = rdr["F_Name"] + " " + rdr["L_Name"];
                    lblStuUsername.Text = "@" + rdr["Username"];
                    lblStuEmail.Text = rdr["Email"].ToString();
                    lblStuPhone.Text = rdr["Phone"].ToString();
                    lblStuGrade.Text = rdr["Grade_Name"].ToString();
                    lblStuDistrict.Text = rdr["Dis_Name"].ToString();
                    lblStuLastLogin.Text = rdr["Last_Login"] != DBNull.Value
                        ? Convert.ToDateTime(rdr["Last_Login"]).ToString("dd MMM yyyy, HH:mm")
                        : "Never";

                    string pic = rdr["ProfilePic"] != DBNull.Value ? rdr["ProfilePic"].ToString() : "reviwer1.jpg";
                    imgStuProfile.ImageUrl = "~/images/" + pic;
                }
            }
            con.Close();
        }

        private void LoadStudentResults(int stuId)
        {
            SqlConnection con = new SqlConnection(_conString);
            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Res_File_Name, Res_Upload FROM tblResults WHERE Stu_Id = @stuId ORDER BY Res_File_Name DESC";
            cmd.Parameters.AddWithValue("@stuId", stuId);
            SqlDataReader reader;
            con.Open();
            reader = cmd.ExecuteReader();
            rptResults.DataSource = reader;
            rptResults.DataBind();
            con.Close();
        }
    }
}