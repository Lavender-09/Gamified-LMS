using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MoProf_bse25aft1_250429135
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnViewCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect("categories.aspx");
        }

        protected void btnViewCourses_Click(object sender, EventArgs e)
        {
            Response.Redirect("courses.aspx");
        }

        protected void btnTopregiser_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }
    }
}