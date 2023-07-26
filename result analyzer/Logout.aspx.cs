using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace result_analyzer
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["res-analyz-uid"] != null)
            {
                Response.Cookies["res-analyz-uid"].Expires = DateTime.Now.AddDays(-1);
            }

            if (Request.Cookies["res-analyz-cid"] != null)
            {
                Response.Cookies["res-analyz-cid"].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Abandon();
            Response.Redirect("login");

        }
    }
}