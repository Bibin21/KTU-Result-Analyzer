using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace result_analyzer
{
    public partial class SiteMaster : MasterPage
    {
        public int id;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            if (Session["iddata"] != null)
            {
               id = Convert.ToInt32(Session["iddata"]);
            }
            else
                Response.Redirect("Error");


            }

    }
}
