using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace result_analyzer
{
    
    public partial class WebForm2 : System.Web.UI.Page
    {
        string contextid;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["anim"] = 1;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (!IsPostBack)
            {
                if (Request.Cookies["res-analyz-uid"] != null)
                {
                   
                    Session["iddata"] = int.Parse( Request.Cookies["res-analyz-uid"].Value);
                    Response.Redirect("Main.aspx");
                }
            }
        }
        protected void Singupbtn_click(object sender, EventArgs e)
        {
           
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            
        SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string email = Request.Form["emails"];
            string password = Request.Form["pass"];
            cmd.CommandText = "Select * from users where useremail='" + email+"'";
            Int32 num_rows = Convert.ToInt32(cmd.ExecuteScalar());
            if (num_rows > 0)
            {
                alreadyexist.Attributes.CssStyle.Add("display", "block");
                samepass.Attributes.CssStyle.Add("display", "none");
            }
            else
            {
                string retypepass = Request.Form["repass"];
                if (password == retypepass)
                {
                    Random ran = new Random();
                    string a = ran.Next(999999).ToString();
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] encrypt;
                    UTF8Encoding encode = new UTF8Encoding();
                    encrypt = md5.ComputeHash(encode.GetBytes(a));
                    StringBuilder encryptdata = new StringBuilder();
                    for (int i = 0; i < encrypt.Length; i++)
                    {
                        encryptdata.Append(encrypt[i].ToString());
                    }
                    contextid = encryptdata.ToString();
                    cmd.CommandText = "Insert into users(useremail,password,contextid) values('" + email + "','" + password + "','"+contextid+"')";
                    cmd.ExecuteNonQuery();
                    Session["sign"] = 1;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Signup Success Redirecting To Login Page');window.location.href='Login';", true);
                
                }
                else { 
                    alreadyexist.Attributes.CssStyle.Add("display", "none");
                    samepass.Attributes.CssStyle.Add("display", "block");
                  
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
        
        protected void closebtn_click(object sender, EventArgs e)
        { 
        
        }





    }



}