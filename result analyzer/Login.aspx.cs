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
    
    public partial class WebForm3 : System.Web.UI.Page
    {
        string contextid;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sign"] != null)
            {
                if ((int)Session["sign"] == 1)
                {
                    check.Attributes.CssStyle.Add("display", "flex");
                    dim.Attributes.CssStyle.Add("display", "block");
                    Session["sign"] = null;
                }
            }

            Session["anim"] = 1;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["res-analyz-uid"]!=null)
                {
                    Session["iddata"] = Request.Cookies["res-analyz-uid"].Value;
                    Response.Redirect("Main.aspx");
                }
            }
            

        }
        protected void googlelogin(object sender, EventArgs e)
        {
            string clientid = "125926661032-ocuup25g2qvbpc8het2f5m3r2vu72kqp.apps.googleusercontent.com";
            string clientsecret = "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";
            string redirection_url = "https://localhost:44341/GoogleAuth";
            string url = "https://accounts.google.com/o/oauth2/v2/auth?scope=email%20profile&include_granted_scopes=true&redirect_uri=" + redirection_url + "&response_type=code&client_id=" + clientid + "";
            Response.Redirect(url);

        }


        protected void loginbtn_click(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string email = Request.Form["email"];
            string password = Request.Form["pass"];
            cmd.CommandText = "Select * from users where useremail=@email";
            cmd.Parameters.AddWithValue("@email", email);
            Int32 num_rows = Convert.ToInt32(cmd.ExecuteScalar());
            if (num_rows > 0)
            {
                cmd.CommandText = "select password from users where useremail='" +email + "'";
                string checkpass = (string)cmd.ExecuteScalar();
                if (String.Equals(password, checkpass))
                {
                    cmd.CommandText = "select userid,contextid from users where useremail='" + email + "'";
                    SqlDataReader reader1 = cmd.ExecuteReader();
                    int uid = 0;
                    while (reader1.Read())
                    {
                        uid = Convert.ToInt32(reader1["userid"]);
                        contextid = reader1["contextid"].ToString();
                        Session["iddata"] = uid;
                    }
                    HttpCookie newCookie = new HttpCookie("res-analyz-uid");
                    newCookie.Value = uid.ToString();
                    HttpCookie newCookie1 = new HttpCookie("res-analyz-cid");
                    newCookie1.Value = contextid;
                    newCookie.Expires = DateTime.Now.AddDays(30);
                    newCookie1.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(newCookie);
                    Response.Cookies.Add(newCookie1);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "window.location.href='Main';", true);
                }
                else
                { 
                wrongpass.Attributes.CssStyle.Add("display", "block");
                    wrongemail.Attributes.CssStyle.Add("display", "none");
                }

            }
            else
            {
                wrongemail.Attributes.CssStyle.Add("display", "block");
                wrongpass.Attributes.CssStyle.Add("display", "none");
            }
            }
        }
    }
