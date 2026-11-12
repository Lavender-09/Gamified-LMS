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
    public partial class tutordetails : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                GetTutorList();
                if (!IsPostBack)
                {
                    CheckExistingRecommendation();
                }
            }
        }
        private void GetTutorList()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();

            int qs = Convert.ToInt32(Request.QueryString["id"]);

            cmd.CommandText = "SELECT t.*, u.*, CONVERT(VARCHAR, u.Reg_Date, 103) AS Registration\r\n\tFROM tblUser u, tblTutors t\r\n\tWHERE u.u_id = t.U_Id AND tut_id = " + qs;
            cmd.Connection = con;
            // Create DataAdapter (Refer to slide 8)

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            DetailsView1.DataSource = dt;
            DetailsView1.DataBind();
        }

        private bool IsStudent()
        {
            return Session["typeid"] != null && Session["typeid"].ToString() == "3";
        }

        private int GetTutId()
        {
            return Convert.ToInt32(Request.QueryString["id"]);
        }

        private int GetCurrentStuId()
        {
            int userId = Convert.ToInt32(Session["userid"]);
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                cmd.Parameters.AddWithValue("@uid", userId);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private void CheckExistingRecommendation()
        {
            pnlRecommend.Visible = IsStudent();
            if (!IsStudent()) return;

            int stuId = GetCurrentStuId();
            int tutId = GetTutId();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM tblRecommendations WHERE Stu_Id = @stuid AND Tut_Id = @tutid";
                cmd.Parameters.AddWithValue("@stuid", stuId);
                cmd.Parameters.AddWithValue("@tutid", tutId);
                int count = (int)cmd.ExecuteScalar();

                pnlAlreadyRated.Visible = count > 0;
                pnlRateForm.Visible = count == 0;
            }
        }

        protected void btnSubmitRec_Click(object sender, EventArgs e)
        {
            int rating = Convert.ToInt32(hdnRating.Value);
            if (rating < 1 || rating > 5)
            {
                lblRecMsg.Text = "Please select a valid star rating.";
                lblRecMsg.CssClass = "text-danger";
                return;
            }

            int stuId = GetCurrentStuId();
            int tutId = GetTutId();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand checkCmd = con.CreateCommand();
                checkCmd.CommandText = "SELECT COUNT(*) FROM tblRecommendations WHERE Stu_Id = @stuid AND Tut_Id = @tutid";
                checkCmd.Parameters.AddWithValue("@stuid", stuId);
                checkCmd.Parameters.AddWithValue("@tutid", tutId);
                if ((int)checkCmd.ExecuteScalar() > 0)
                {
                    pnlRateForm.Visible = false;
                    pnlAlreadyRated.Visible = true;
                    return;
                }

                SqlCommand insCmd = con.CreateCommand();
                insCmd.CommandText = "INSERT INTO tblRecommendations (Testimonial, Rating, Rec_Date, Stu_Id, Tut_Id) " +
                                      "VALUES (@testimonial, @rating, @date, @stuid, @tutid)";
                insCmd.Parameters.AddWithValue("@testimonial", txtTestimonial.Text.Trim());
                insCmd.Parameters.AddWithValue("@rating", rating);
                insCmd.Parameters.AddWithValue("@date", DateTime.Now);
                insCmd.Parameters.AddWithValue("@stuid", stuId);
                insCmd.Parameters.AddWithValue("@tutid", tutId);
                insCmd.ExecuteNonQuery();

                XPHelper.AwardXP(con, stuId, "Recommendation", "Recommended a tutor");
            }

            pnlRateForm.Visible = false;
            pnlAlreadyRated.Visible = true;
        }
        private void BindAverageRating()
        {
            int tutId = GetTutId();
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT AVG(CAST(Rating AS FLOAT)) AS AvgRating, COUNT(*) AS RecCount " +
                                   "FROM tblRecommendations WHERE Tut_Id = @tutid";
                cmd.Parameters.AddWithValue("@tutid", tutId);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read() && rdr["AvgRating"] != DBNull.Value)
                    {
                        double avg = Convert.ToDouble(rdr["AvgRating"]);
                        int count = Convert.ToInt32(rdr["RecCount"]);
                        lblAvgStars.Text = BuildStarString(avg);
                        lblAvgRatingText.Text = avg.ToString("0.0") + " out of 5 (" + count + (count == 1 ? " review)" : " reviews)");
                    }
                    else
                    {
                        lblAvgStars.Text = "";
                        lblAvgRatingText.Text = "No ratings yet.";
                    }
                }
            }
        }

        private string BuildStarString(double avg)
        {
            int fullStars = (int)Math.Round(avg); // rounds to nearest whole star for display
            string stars = "";
            for (int i = 1; i <= 5; i++)
            {
                stars += i <= fullStars ? "&#9733;" : "&#9734;";
            }
            return stars;
        }
    }
}