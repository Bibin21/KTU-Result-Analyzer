using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using System.Net.Mail;
using System.Net;

namespace result_analyzer
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

        }
        
        int otp;
        protected void sendotp_click(object sender, EventArgs e)
        {

            string email = smail.Value;
            Session["email"] = email;
            Random rnd = new Random();
            otp = rnd.Next(000000, 999999);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from users where useremail='" + email + "'";
            Int32 num_rows = Convert.ToInt32(cmd.ExecuteScalar());
            if (num_rows > 0)
            {
                noaccount.Attributes.CssStyle.Add("display", "none");
                cmd.CommandText = "update users set otp=" + otp + " where useremail='" + email + "'";
                cmd.ExecuteNonQuery();
                smail.Attributes.CssStyle.Add("display", "none");
                sendotp.Attributes.CssStyle.Add("display", "none");
                otpenter.Attributes.CssStyle.Add("display", "block");
                otpenter.Attributes.Add("required", "true");
                verifybtn.Attributes.CssStyle.Add("display", "block");
                labelotp.Attributes.CssStyle.Add("display", "block");
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = "ryzengamer00@gmail.com";
                NetworkCred.Password = "niqpkmqifnwfmtcg";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                MailMessage mm = new MailMessage();
                mm.Subject = "OTP For Password Reset";
                mm.Body = string.Format("Dear User,<br /><br />OTP for Resetting Your Password is: " + otp + "<br /><br />Thank You.");
                mm.To.Add(email);
                mm.From = new MailAddress("ryzengamer00@gmail.com");
                mm.IsBodyHtml = true;
                try
                {
                    smtp.Send(mm);
                    form.DefaultButton="verifybtn";
                }
                catch {
                    throw;
                }

            }
            else
            {
                noaccount.Attributes.CssStyle.Add("display", "block");

            }
           
        }
        protected void verifybtn_click(object sender, EventArgs e)
        {
            form.DefaultButton = "verifybtn";
            string email = Convert.ToString(Session["email"]);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select otp from users where useremail='" + email + "'";
            int otpdb = Convert.ToInt32(cmd.ExecuteScalar());
            labelotp.Attributes.CssStyle.Add("display", "none");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert(" + email + ");", true);
            string otpinput= otpenter.Value;
            int enteredotp = Convert.ToInt32(otpinput);
            if (enteredotp == otpdb)
            {
              
                otpenter.Attributes.Add("required", "false");
                forgot.Attributes.CssStyle.Add("display", "none");
                forgotlabel.Attributes.CssStyle.Add("display", "none");
                changepass.Attributes.CssStyle.Add("display", "block");
                newpass.Attributes.Add("required", "true");
                retypenewpass.Attributes.Add("required", "true");

            }
            else
            {
              
                wrongotp.Attributes.CssStyle.Add("display", "block");
            }

           
        }
        protected void change_pass(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Password Changed');", true);
            string email = Convert.ToString(Session["email"]);
            string newpassword =newpass.Value;
            string repass = retypenewpass.Value;
            if (String.Equals(newpassword, repass) == true)
            {

                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update users set password='" + newpassword + "' where useremail='" + email + "'";
                cmd1.ExecuteNonQuery();
                wrongrepass.Attributes.CssStyle.Add("display", "none");
                Response.Redirect("login");
                
             
            }
            else
            {
                wrongrepass.Attributes.CssStyle.Add("display", "block");
            }
        }

    }
}