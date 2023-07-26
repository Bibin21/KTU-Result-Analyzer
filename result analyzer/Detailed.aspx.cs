using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace result_analyzer
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        int id;
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
             id = (int)Session["iddata"];
            SqlCommand com = con.CreateCommand();
            com.CommandType = CommandType.Text;
            com.CommandText = "select * from results where sem=1 and Id=" + id + "";
            Int32 count1 = Convert.ToInt32(com.ExecuteScalar());
            if (count1 == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Results Not Uploaded!!');window.location.href='Main';", true);
            }
            else
            {
                
                SqlCommand com1 = con.CreateCommand();
                com1.CommandType = CommandType.Text;
                com1.CommandText = "select  Sem,sum(SupplyCount) as [Arrears Per Sem] from results where Id=" + id + " group by Sem";
                SqlDataAdapter da = new SqlDataAdapter(com1);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataTable cd = ds.Tables[0];
                    if (cd != null)
                    {

                    string[] label = new string[8]{ "Semester 1", "Semester 2", "Semester 3", "Semester 4", "Semester 5", "Semester 6", "Semester 7", "Semester 8" };

                    
                        String chart = "";
                        chart = "<canvas id=\"bar-chart\"width =\"100%\" height=\"50\"></canvas>";
                        chart += "<script>";
                        chart += "new Chart(document.getElementById(\"bar-chart\"), {  type: 'bar', data: { labels:[";
                          for (int i = 0; i < cd.Rows.Count; i++)
                                  chart +=   "'"+label[i]+"' ,";
                                chart = chart.Substring(0, chart.Length - 1);
                                chart += "],datasets: [{  backgroundColor: '#9254CD', data: [";
                                String value = "";
                                for (int i = 0; i < cd.Rows.Count; i++)
                                    value += cd.Rows[i]["Arrears Per Sem"].ToString() + ",";
                                value = value.Substring(0, value.Length - 1);

                                chart += value;

                                chart += "],label: \"Arrears Per Semester\", borderColor: \"#3e95cd\",fill: true}";
                                chart += "]},options: { maintainAspectRatio: false, responsive: true,title: { display: true,text: 'Arrears Per Semester'}, scales: { x: {grid:{display: false,drawBorder: false} }, y: { grid: {drawBorder:false }}} } "; 
                        chart += "});";
                        chart += "</script>";

                    ltChartData.Text = chart;
                    }

                SqlCommand com2 = con.CreateCommand();
                com2.CommandType = CommandType.Text;
                com2.CommandText = "select COUNT(distinct ktuid) as [count] from results where Ktuid in(select Ktuid from results where Id='"+id+"' group by Ktuid having sum(SupplyCount)>=1 and sum(SupplyCount) <5)and  Id='"+id+"'; ";
                int ltfive=(int)com2.ExecuteScalar();
                com2.CommandText = "select COUNT(distinct ktuid) as [count] from results where Ktuid in(select Ktuid from results where Id='" + id + "' group by Ktuid having sum(SupplyCount) >5)and  Id='" + id + "'; ";
                int gtfive =(int)com2.ExecuteScalar();
                com2.CommandText = "select COUNT(distinct ktuid) as [count] from results where Id='" + id + "'; ";
                int total = (int)com2.ExecuteScalar();
                int allpass = total -(gtfive+ltfive);
                if (total != 0||total!=null)
                {

                    string[] label = new string[8] { "Semester 1", "Semester 2", "Semester 3", "Semester 4", "Semester 5", "Semester 6", "Semester 7", "Semester 8" };

                    percent.InnerText += Math.Round( (float) allpass*100/total, 2);
                    String chart = "";
                    chart = "<canvas id=\"pie-chart\"width =\"100%\" height=\"50\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"pie-chart\"), {  type: 'doughnut', data: { labels:[ 'Students With No Arrear','Students With Less Than 5 Arrear','Students With More Than 5 Arrear'";
                    chart += "],datasets: [{  backgroundColor: [ 'rgb(54, 162, 235)','rgb(255, 205, 86)','rgb(255, 99, 132)'], weight: 1,data: [" + allpass+","+ltfive+","+gtfive+",";
                    chart += "],label: \"Arrears Per Semester\", borderColor: \"rgba(255,255,255,0.3)\",fill: true}";
                    chart += "]},options: { cutout: 90, maintainAspectRatio: false,responsive: true, title: { display: true,text: 'Air Temperature (oC)'}, scales: { x: {grid:{display:false, drawBorder: false} ,display:false }, y: { grid: {display: false,drawBorder:false },display:false}} } ";
                    chart += "});";
                    chart += "</script>";

                    piedata.Text = chart;

                }
                SqlCommand comm = con.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = "select Name as [name],sum(SupplyCount) as [count] from results where SupplyCount>0 and Id=" + id+" group by Ktuid,Name;";
                SqlDataAdapter da1 = new SqlDataAdapter(comm);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                DataTable cd2 = ds1.Tables[0];
                if (cd2 != null)
                {
                    String chart = "";
                    chart = "<canvas id=\"line-chart\"width =\"100%\" height=\"30\"></canvas>";
                    chart += "<script>";
                    chart += "new Chart(document.getElementById(\"line-chart\"), {  type: 'line', data: { labels:[";
                    for (int i = 0; i < cd2.Rows.Count; i++)
                        chart += "'"+cd2.Rows[i]["Name"].ToString() + "'  ,";
                    chart = chart.Substring(0, chart.Length - 1);
                    chart += "],datasets: [{pointStyle: 'circle',pointRadius: 5,pointHoverRadius: 10, data: [";
                    String value = "";
                    for (int i = 0; i < cd2.Rows.Count; i++)
                        value += cd2.Rows[i]["count"].ToString() + ",";
                    value = value.Substring(0, value.Length - 1);

                    chart += value;
                    chart += "],label: \"Arrears Per Semester\",lineTension: 0.5, borderColor: \"#3e95cd\",fill: false}";
                    chart += "]},options: {bezierCurve: true, maintainAspectRatio: false, responsive: true,title: { display: true,text: 'Arrears Per Semester'}, scales: { x: {grid:{display: false,drawBorder: false} }, y: { grid: {drawBorder:false }}} } ";
                    chart += "});";
                    chart += "</script>";
                    linedata.Text = chart;
                }



            }
        }

                private DataTable GetData()
                {
                    string constr = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                using (SqlCommand cmd = new SqlCommand(" SELECT top 5 Name,sum(SupplyCount) as [Total Arrears] FROM results WHERE Id=" + id + " group by Name order by [Total Arrears] desc"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }

                }
                }



            



        }
    }
}