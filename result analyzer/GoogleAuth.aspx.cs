using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace result_analyzer
{
    public partial class GoogleAuth : System.Web.UI.Page
    {
        string contextid;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);

        //your client id  
        string clientid = "125926661032-ocuup25g2qvbpc8het2f5m3r2vu72kqp.apps.googleusercontent.com";
            //your client secret  
            string clientsecret = "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";
            //your redirection url  
            string redirection_url = "https://localhost:44341/GoogleAuth";
            string url = "https://accounts.google.com/o/oauth2/token";
        
            public class Tokenclass
            {
                public string access_token
                {
                    get;
                    set;
                }
                public string token_type
                {
                    get;
                    set;
                }
                public int expires_in
                {
                    get;
                    set;
                }
                public string refresh_token
                {
                    get;
                    set;
                }
            }
            public class Userclass
            {
                public string id
                {
                    get;
                    set;
                }
                public string name
                {
                    get;
                    set;
                }
               
            public string email
            {
                get;
                set;
            }
            
               
              
            
  
            }
            protected void Page_Load(object sender, EventArgs e)
            {

                if (!IsPostBack)
                {
               

                if (Request.QueryString["code"] != null)
                    {
                        GetToken(Request.QueryString["code"].ToString());
                    }
                }
            }
            public void GetToken(string code)
            {
               
                string poststring = "grant_type=authorization_code&code=" + code + "&client_id=" + clientid + "&client_secret=" + clientsecret + "&redirect_uri=" + redirection_url + "&scope =https://www.googleapis.com/auth/userinfo.email";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                UTF8Encoding utfenc = new UTF8Encoding();
                byte[] bytes = utfenc.GetBytes(poststring);
                Stream outputstream = null;
                try
                {
                    request.ContentLength = bytes.Length;
                    outputstream = request.GetRequestStream();
                    outputstream.Write(bytes, 0, bytes.Length);
                }
                catch { }
                var response = (HttpWebResponse)request.GetResponse();
                var streamReader = new StreamReader(response.GetResponseStream());
                string responseFromServer = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                Tokenclass obj = js.Deserialize<Tokenclass>(responseFromServer);
                GetuserProfile(obj.access_token);
            }
        
        public void GetuserProfile(string accesstoken)
            {

                string url = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + accesstoken ;
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = (HttpWebResponse) request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
            dataStream.Close();
            response.Close();
            JavaScriptSerializer js = new JavaScriptSerializer();
            Userclass userinfo = js.Deserialize<Userclass>(responseFromServer);
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
             SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from users where useremail='" + userinfo.email + "'";
            Int32 result = Convert.ToInt32(cmd.ExecuteScalar());
            if (result==0)
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
                   contextid= encryptdata.ToString();
                cmd.CommandText = "Insert into users(useremail,password,contextid) values('" + userinfo.email + "','" + userinfo.id + "','"+contextid+"')";
                cmd.ExecuteNonQuery();
            }
                cmd.CommandText = "select userid,contextid from users where useremail='" + userinfo.email + "'";
            SqlDataReader reader1 = cmd.ExecuteReader();
            int uid=0;
            while (reader1.Read())
            {
                 uid =Convert.ToInt32(reader1["userid"]);
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
        }
    
}