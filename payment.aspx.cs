using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class payment : System.Web.UI.Page
    {
        private string _conString =
WebConfigurationManager.ConnectionStrings["LearnCS"].ConnectionString;

        private int LessId => Convert.ToInt32(Request.QueryString["less_id"]);
        private int TsId => Convert.ToInt32(Request.QueryString["ts_id"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Response.Redirect("~/login.aspx");
                return;
            }
        }

        protected void btnPayLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPayUsername.Text) || string.IsNullOrWhiteSpace(txtPayPassword.Text))
            {
                lblLoginMsg.Text = "Please enter both fields.";
                return;
            }

            pnlLogin.Visible = false;
            pnlConfirm.Visible = true;
        }

        protected void btnConfirmYes_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE b SET b.Pmt_Id = 1 " +
                                   "FROM tblBookings b, tblStudents s " +
                                   "WHERE b.Stu_Id = s.Stu_Id AND s.U_Id = @uid " +
                                   "AND b.Less_Id = @lessid AND b.Book_Status = 'Booked'";
                cmd.Parameters.AddWithValue("@uid", userId);
                cmd.Parameters.AddWithValue("@lessid", LessId);
                cmd.ExecuteNonQuery();
            }

            ShowResult(true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["userid"]);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                con.Open();

                SqlCommand cancelCmd = con.CreateCommand();
                cancelCmd.CommandText = "UPDATE b SET b.Book_Status = 'Cancelled' " +
                                         "FROM tblBookings b, tblStudents s " +
                                         "WHERE b.Stu_Id = s.Stu_Id AND s.U_Id = @uid " +
                                         "AND b.Less_Id = @lessid AND b.Book_Status = 'Booked'";
                cancelCmd.Parameters.AddWithValue("@uid", userId);
                cancelCmd.Parameters.AddWithValue("@lessid", LessId);
                int rows = cancelCmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    SqlCommand freeSeatCmd = con.CreateCommand();
                    freeSeatCmd.CommandText = "UPDATE tblLessons SET Curr_Seats = CASE WHEN Curr_Seats > 0 THEN Curr_Seats - 1 ELSE 0 END, " +
                                              "Is_Available = 1 WHERE Less_Id = @lessid";
                    freeSeatCmd.Parameters.AddWithValue("@lessid", LessId);
                    freeSeatCmd.ExecuteNonQuery();
                }
            }

            ShowResult(false);
        }
        private void ShowResult(bool success)
        {
            pnlConfirm.Visible = false;
            pnlResult.Visible = true;

            divResult.InnerText = success
                ? "Payment confirmed! Your seat is booked."
                : "Payment cancelled. Your seat has been released.";
            divResult.Attributes["class"] = "alert " + (success ? "alert-success" : "alert-warning");

            lnkBack.NavigateUrl = "~/tutorsubjectdetails.aspx?ts_id=" + TsId;
        }
    }
}