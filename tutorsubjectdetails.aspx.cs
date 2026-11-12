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
    public partial class tutorsubjectdetails : System.Web.UI.Page
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

            bool isTutor = IsTutorView();
            pnlPostAnnouncement.Visible = isTutor;
            pnlUploadMaterial.Visible = isTutor;

            if (!IsPostBack)
            {
                int tsId = GetCurrentTsId();
                BindAnnouncements(tsId);
                BindMaterials(tsId);
                BindTimetable(tsId);
                BindStudents(tsId);
                BindLessons(tsId);
            }
        }

        private int GetCurrentTsId()
        {
            int tsId = Convert.ToInt32(Request.QueryString["ts_id"]);
            int userId = Convert.ToInt32(Session["userid"]);
            bool isTutor = IsTutorView();

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            if (isTutor)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM tblTutorSubjects ts, tblTutors t " +
                                   "WHERE ts.Tut_Id = t.Tut_Id AND t.U_Id = @userId AND ts.TS_Id = @tsId";
            }
            else
            {
                cmd.CommandText = "SELECT COUNT(*) FROM tblEnrolledSubjects es, tblStudents s " +
                                   "WHERE es.Stu_Id = s.Stu_Id AND s.U_Id = @userId AND es.TS_Id = @tsId";
            }
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@tsId", tsId);
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            if (count == 0)
            {
                Response.Redirect(isTutor ? "~/tutordashboard.aspx" : "~/dashboard.aspx");
            }
            return tsId;
        }

        protected void btnPostAnn_Click(object sender, EventArgs e)
        {
            int tsId = GetCurrentTsId();

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO tblAnnouncements (Ann_Date, Ann_Title, Content, TS_Id) " +
                               "VALUES (@date, @title, @content, @tsid)";
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@title", txtAnnTitle.Text.Trim());
            cmd.Parameters.AddWithValue("@content", txtAnnContent.Text.Trim());
            cmd.Parameters.AddWithValue("@tsid", tsId);
            cmd.ExecuteNonQuery();
            con.Close();

            txtAnnTitle.Text = "";
            txtAnnContent.Text = "";
            BindAnnouncements(tsId);
        }
        private void BindAnnouncements(int tsId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Ann_Id, Ann_Date, Ann_Title, Content FROM tblAnnouncements " +
                               "WHERE TS_Id = @tsid ORDER BY Ann_Date DESC";
            cmd.Parameters.AddWithValue("@tsid", tsId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptAnnouncements.DataSource = dt;
            rptAnnouncements.DataBind();
        }

        protected void btnUploadMaterial_Click(object sender, EventArgs e)
        {
            if (!fupMaterial.HasFile)
            {
                return;
            }

            int tsId = GetCurrentTsId();
            string username = Session["username"].ToString();
            string fileName = Path.GetFileName(fupMaterial.FileName);

            string folderPath = Server.MapPath("~/UploadedMaterials/" + username + "/subject_" + tsId + "_uploads/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // only creates it the first time; safe to call every upload
            }

            fupMaterial.SaveAs(Path.Combine(folderPath, fileName));

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO tblMaterial (TS_Id, Mat_File_Name, Mat_Upload) VALUES (@tsid, @filename, @uploadDate)";
            cmd.Parameters.AddWithValue("@tsid", tsId);
            cmd.Parameters.AddWithValue("@filename", fileName);
            cmd.Parameters.AddWithValue("@uploadDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            con.Close();

            BindMaterials(tsId);
        }
        protected string GetMaterialUrl(object fileName)
        {
            int tsId = GetCurrentTsId();
            string tutorUsername = GetTutorUsernameForTs(tsId);
            return ResolveUrl("~/UploadedMaterials/" + tutorUsername + "/subject_" + tsId + "_uploads/" + fileName.ToString());
        }

        private string GetTutorUsernameForTs(int tsId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT u.Username FROM tblTutorSubjects ts, tblTutors t, tblUser u " +
                                   "WHERE ts.Tut_Id = t.Tut_Id AND t.U_Id = u.U_Id AND ts.TS_Id = @tsid";
                cmd.Parameters.AddWithValue("@tsid", tsId);
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
        }
        private void BindMaterials(int tsId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Mat_Id, Mat_File_Name, Mat_Upload FROM tblMaterial " +
                               "WHERE TS_Id = @tsid ORDER BY Mat_Upload DESC";
            cmd.Parameters.AddWithValue("@tsid", tsId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptMaterials.DataSource = dt;
            rptMaterials.DataBind();
        }
        private void BindTimetable(int tsId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT Timetable FROM tblTutorSubjects WHERE TS_Id = @tsid";
            cmd.Parameters.AddWithValue("@tsid", tsId);
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result != null && result != DBNull.Value && !string.IsNullOrEmpty(result.ToString()))
            {
                imgTimetable.ImageUrl = "~/Timetables/" + result.ToString();
                imgTimetable.Visible = true;
                lblNoTimetable.Visible = false;
            }
            else
            {
                imgTimetable.Visible = false;
                lblNoTimetable.Visible = true;
            }
        }
        private void BindStudents(int tsId)
        {
            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT s.Stu_Id, u.F_Name, u.L_Name, u.Last_Login, u.ProfilePic " +
                               "FROM tblEnrolledSubjects es, tblStudents s, tblUser u " +
                               "WHERE es.Stu_Id = s.Stu_Id AND s.U_Id = u.U_Id AND es.TS_Id = @tsid";
            cmd.Parameters.AddWithValue("@tsid", tsId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptEnrolledStudents.DataSource = dt;
            rptEnrolledStudents.DataBind();

        }
        protected void rptEnrolledStudents_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "viewStudent")
            {
                int stuId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("~/studentdetails.aspx?stu_id=" + stuId);
            }
        }
        protected bool IsTutorView()
        {
            return Session["typeid"] != null && Session["typeid"].ToString() == "2";
        }

        protected void rptEnrolledStudents_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton lnkRow = (LinkButton)e.Item.FindControl("lnkStudentRow");
                bool isTutor = IsTutorView();
                lnkRow.Enabled = isTutor;

                if (!isTutor)
                {
                    lnkRow.CssClass += " pe-none";
                }
            }
        }

        protected void btnCreateLesson_Click(object sender, EventArgs e)
        {
            int tsId = GetCurrentTsId();

            DateTime lessonDateTime;
            if (!DateTime.TryParse(txtLessonDateTime.Text, out lessonDateTime))
            {
                lblLessonMsg.Text = "Please enter a valid date and time.";
                lblLessonMsg.CssClass = "text-danger";
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                // Max_Seats is derived from the subject's Max_Enroll, not typed manually
                SqlCommand maxCmd = con.CreateCommand();
                maxCmd.CommandText = "SELECT Max_Enroll FROM tblTutorSubjects WHERE TS_Id = @tsid";
                maxCmd.Parameters.AddWithValue("@tsid", tsId);
                int maxSeats = Convert.ToInt32(maxCmd.ExecuteScalar());

                SqlCommand insCmd = con.CreateCommand();
                insCmd.CommandText = "INSERT INTO tblLessons (TS_Id, Less_Date, Max_Seats, Curr_Seats, Is_Available) " +
                                      "VALUES (@tsid, @dt, @maxseats, 0, 1)";
                insCmd.Parameters.AddWithValue("@tsid", tsId);
                insCmd.Parameters.AddWithValue("@dt", lessonDateTime);
                insCmd.Parameters.AddWithValue("@maxseats", maxSeats);
                insCmd.ExecuteNonQuery();
            }

            lblLessonMsg.Text = "Lesson scheduled!";
            lblLessonMsg.CssClass = "text-success";
            txtLessonDateTime.Text = "";
            BindLessons(tsId);
        }

        protected void rptLessons_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int lessId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "bookSeat")
            {
                BookSeat(lessId);
            }
            else if (e.CommandName == "cancelSeat")
            {
                CancelSeat(lessId);
            }
            else if (e.CommandName == "pay")
            {
                Response.Redirect("~/payment.aspx?less_id=" + lessId);
            }
        }
        private void BindLessons(int tsId)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT l.Less_Id, l.Less_Date, l.Curr_Seats, l.Max_Seats, l.Is_Available, " +
                       "(SELECT TOP 1 b.Book_Status FROM tblBookings b, tblStudents s " +
                       " WHERE b.Less_Id = l.Less_Id AND b.Stu_Id = s.Stu_Id " +
                       " AND s.U_Id = @userId AND b.book_Status IN ('Pending','Booked') ORDER BY b.Book_Id DESC) AS MyBookingStatus, " +
                       "(SELECT TOP 1 b.Pmt_Id FROM tblBookings b, tblStudents s " +
                       " WHERE b.Less_Id = l.Less_Id AND b.Stu_Id = s.Stu_Id " +
                       " AND s.U_Id = @userId AND b.book_Status IN ('Pending','Booked') ORDER BY b.Book_Id DESC) AS MyPaymentId " +
                       "FROM tblLessons l " +
                       "WHERE l.TS_Id = @tsid " +
                       "ORDER BY l.Less_Date";
            cmd.Parameters.AddWithValue("@tsid", tsId);
            cmd.Parameters.AddWithValue("@userId", userId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptLessons.DataSource = dt;
            rptLessons.DataBind();
        }
        private void BookSeat(int lessId)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand stuCmd = con.CreateCommand();
                stuCmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                stuCmd.Parameters.AddWithValue("@uid", userId);
                int stuId = Convert.ToInt32(stuCmd.ExecuteScalar());

                SqlCommand checkCmd = con.CreateCommand();
                checkCmd.CommandText = "SELECT Curr_Seats, Max_Seats FROM tblLessons WHERE Less_Id = @lessid";
                checkCmd.Parameters.AddWithValue("@lessid", lessId);

                int currSeats, maxSeats;
                using (SqlDataReader rdr = checkCmd.ExecuteReader())
                {
                    if (!rdr.Read())
                    {
                        return;
                    }
                    currSeats = Convert.ToInt32(rdr["Curr_Seats"]);
                    maxSeats = Convert.ToInt32(rdr["Max_Seats"]);
                }

                if (currSeats >= maxSeats)
                {
                    return; // lesson is full — do nothing
                }

                SqlCommand bookCmd = con.CreateCommand();
                bookCmd.CommandText = "INSERT INTO tblBookings (Less_Id, Pmt_Id, Book_Status, Stu_Id) " +
                                       "VALUES (@lessid, NULL, 'Pending', @stuid)";
                bookCmd.Parameters.AddWithValue("@lessid", lessId);
                bookCmd.Parameters.AddWithValue("@stuid", stuId);
                bookCmd.ExecuteNonQuery();

                SqlCommand updCmd = con.CreateCommand();
                updCmd.CommandText = "UPDATE tblLessons SET Curr_Seats = Curr_Seats + 1, " +
                                      "Is_Available = CASE WHEN Curr_Seats + 1 >= Max_Seats THEN 0 ELSE 1 END " +
                                      "WHERE Less_Id = @lessid";
                updCmd.Parameters.AddWithValue("@lessid", lessId);
                updCmd.ExecuteNonQuery();

                XPHelper.AwardXP(con, stuId, "JoinClass", "Joined a new subject");
            }

            int tsId = GetCurrentTsId();
            BindLessons(tsId);
        }

        private void CancelSeat(int lessId)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand stuCmd = con.CreateCommand();
                stuCmd.CommandText = "SELECT Stu_Id FROM tblStudents WHERE U_Id = @uid";
                stuCmd.Parameters.AddWithValue("@uid", userId);
                int stuId = Convert.ToInt32(stuCmd.ExecuteScalar());

                SqlCommand cancelCmd = con.CreateCommand();
                cancelCmd.CommandText = "UPDATE tblBookings SET Book_Status = 'Cancelled' " +
                                         "WHERE Less_Id = @lessid AND Stu_Id = @stuid AND Book_Status = 'Booked'";
                cancelCmd.Parameters.AddWithValue("@lessid", lessId);
                cancelCmd.Parameters.AddWithValue("@stuid", stuId);
                int rowsAffected = cancelCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    SqlCommand updCmd = con.CreateCommand();
                    updCmd.CommandText = "UPDATE tblLessons SET Curr_Seats = CASE WHEN Curr_Seats > 0 THEN Curr_Seats - 1 ELSE 0 END, " +
                                          "Is_Available = 1 WHERE Less_Id = @lessid";
                    updCmd.Parameters.AddWithValue("@lessid", lessId);
                    updCmd.ExecuteNonQuery();
                }
            }

            int tsId = GetCurrentTsId();
            BindLessons(tsId);
        }

        private void DecideBooking(int lessId, int stuId, bool accept)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand updBooking = con.CreateCommand();
                updBooking.CommandText = "UPDATE tblBookings SET Book_Status = @status " +
                                          "WHERE Less_Id = @lessid AND Stu_Id = @stuid AND Book_Status = 'Pending'";
                updBooking.Parameters.AddWithValue("@status", accept ? "Booked" : "Cancelled");
                updBooking.Parameters.AddWithValue("@lessid", lessId);
                updBooking.Parameters.AddWithValue("@stuid", stuId);
                int rows = updBooking.ExecuteNonQuery();

                if (rows > 0 && !accept)
                {
                    // declined -> free the seat back up
                    SqlCommand updLesson = con.CreateCommand();
                    updLesson.CommandText = "UPDATE tblLessons SET Curr_Seats = CASE WHEN Curr_Seats > 0 THEN Curr_Seats - 1 ELSE 0 END, " +
                                             "Is_Available = 1 WHERE Less_Id = @lessid";
                    updLesson.Parameters.AddWithValue("@lessid", lessId);
                    updLesson.ExecuteNonQuery();
                }
                SendBookingEmail(stuId, accept);
            }

            int tsId = GetCurrentTsId();
            BindLessons(tsId);
        }

        private void SendBookingEmail(int stuId, bool accept)
        {
            string email, name;
            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT u.Email, u.F_Name FROM tblStudents s, tblUser u " +
                                   "WHERE s.U_Id = u.U_Id AND s.Stu_Id = @stuid";
                cmd.Parameters.AddWithValue("@stuid", stuId);
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return;
                    email = rdr["Email"].ToString();
                    name = rdr["F_Name"].ToString();
                }
            }

            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            using (MailMessage m = new MailMessage(smtpSection.From, email))
            {
                SmtpClient sc = new SmtpClient();
                try
                {
                    m.Subject = "Seat Booking Update";
                    m.IsBodyHtml = true;
                    m.Body = accept
                        ? "Dear " + name + ", your seat has been successfully booked. You may now proceed with payment."
                        : "Dear " + name + ", sorry, your seat booking has been cancelled.";
                    sc.Host = smtpSection.Network.Host;
                    sc.EnableSsl = smtpSection.Network.EnableSsl;
                    sc.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                    sc.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                    sc.Port = smtpSection.Network.Port;
                    sc.Send(m);
                }
                catch (Exception ex)
                {
                    lblLessonMsg.Text = "Email failed: " + ex.Message;
                    lblLessonMsg.CssClass = "text-danger";
                }
            }
        }

        protected void rptLessons_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            if (!IsTutorView()) return;

            int lessId = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Less_Id"));
            Repeater rptPending = (Repeater)e.Item.FindControl("rptPendingRequests");

            SqlConnection con = new SqlConnection(_conString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT s.Stu_Id, u.F_Name, u.L_Name " +
                               "FROM tblBookings b, tblStudents s, tblUser u " +
                               "WHERE b.Stu_Id = s.Stu_Id AND s.U_Id = u.U_Id " +
                               "AND b.Less_Id = @lessid AND b.Book_Status = 'Pending'";
            cmd.Parameters.AddWithValue("@lessid", lessId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            rptPending.DataSource = dt;
            rptPending.DataBind();
        }
        protected string GetBookingStatus(object status)
        {
            return status == DBNull.Value ? "" : status.ToString();
        }

        protected void rptPendingRequests_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem lessonItem = (RepeaterItem)((Repeater)source).NamingContainer;
            
            int stuId = Convert.ToInt32(e.CommandArgument);
            int lessId = Convert.ToInt32(DataBinder.Eval(lessonItem.DataItem, "Less_Id"));

            DecideBooking(lessId, stuId, e.CommandName == "accept");
        }
    }
}