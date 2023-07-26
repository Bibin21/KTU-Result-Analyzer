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
    public partial class WebForm8 : System.Web.UI.Page
    {
        int id;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (Session["iddata"] != null)
            {
                id = (int)Session["iddata"];
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Select useremail from users where userid=" + id + "";
                email.Value = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "Select name from users where userid='" + id + "' and name IS NOT NULL";
               object querres = cmd.ExecuteScalar();
                if (querres!=null)
                {
                    name.Value = cmd.ExecuteScalar().ToString();
                }
                   
                    cmd.CommandText = "Select college from users where userid='" + id + "' and college IS NOT NULL";
                object querres1 = cmd.ExecuteScalar();
                if (querres1!= null)
                {
                    
                    clg.Value = cmd.ExecuteScalar().ToString();

                }
            }
            else
            {

                Response.Redirect("Error");
            }


            }
        
        protected void profile_save(object sender, EventArgs e)
        {
            string nameval,clgval;
            nameval = name.Value;
            clgval = clg.Value;
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "update users set name='"+nameval+"',college='"+clgval+"' where userid=" + id + "";
            cmd1.ExecuteNonQuery();

        }

    }
}