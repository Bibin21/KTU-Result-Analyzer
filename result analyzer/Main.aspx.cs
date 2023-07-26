using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.Configuration;
using Microsoft.Office.Interop.Excel;
using Microsoft.CSharp;
using System.Drawing;
using DataTable = System.Data.DataTable;
using Button = System.Web.UI.WebControls.Button;
using System.Windows.Forms;

namespace result_analyzer
{

    public partial class WebForm5 : System.Web.UI.Page
    {
        int id, rescountint = 0;
        string sem;
        int semno;
        protected void Page_Load(object sender, EventArgs e)
        {
            string contextid;
            SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
            if (!IsPostBack)
            {
                if (Session["anim"] !=null)
                {
                    if ((int)Session["anim"] == 1)
                    {
                        check.Attributes.CssStyle.Add("display", "flex");
                        dim.Attributes.CssStyle.Add("display", "block");

                        Session["anim"] = null;
                    }
                }
            }
            if (IsPostBack)
            {
                check.Attributes.CssStyle.Add("display", "none !important");
                dim.Attributes.CssStyle.Add("display", "none !important");
            }
          

            if (Session["iddata"] != null)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                id = Convert.ToInt32(Session["iddata"]);
                if (Request.Cookies["res-analyz-cid"] != null)
                {
                    contextid = Request.Cookies["res-analyz-cid"].Value;
                    cmd.CommandText = "Select contextid from users where userid='" + id + "'";
                    string cid = cmd.ExecuteScalar().ToString();
                    if (string.Equals(cid, contextid) == false)
                    {
                        Response.Redirect("Error.aspx");
                    }
                    else
                    {




                        cmd.CommandText = "Select max(sem) from results where Id='" + id + "'";
                        object userNameObj = cmd.ExecuteScalar();
                        if (userNameObj != null)
                        {
                            string rescount = userNameObj.ToString();
                            bool isParsable = Int32.TryParse(rescount, out rescountint);
                            con.Close();
                            if (String.Equals(rescount, "NULL") == false)
                            {
                                if (rescountint >= 1)
                                {

                                    s1.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                { 
                                    s1.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 2)
                                {

                                    s2.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                { 
                                    s2.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 3)
                                {
                                    s3.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                { 
                                    s3.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 4)
                                {
                                    s4.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                { 
                                    s4.Attributes.CssStyle.Add("background-color", "#FF6969!important");

                                }
                                if (rescountint >= 5)
                                {
                                    s5.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else { 
                                    s5.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 6)
                                {
                                    s6.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                { 
                                    s6.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 7)
                                {
                                    s7.Attributes.CssStyle.Add("background-color", "#0CD66B !important");

                                }
                                else
                                {
                                    s7.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }
                                if (rescountint >= 8)
                                {
                                    s8.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                                }
                                else
                                {
                                    s8.Attributes.CssStyle.Add("background-color", "#FF6969!important");
                                }

                            }
                        }

                        else
                        {

                            Response.Redirect("Error");
                        }
                    }
                }
                else
                {
                    Response.Redirect("Error");
                }

            }
        }

        int allpasscount = 0, failcount = 0, totalcount = 0, k;


        protected void generate_report(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(1500);
            check.Attributes.CssStyle.Add("display", "none");
            dim.Attributes.CssStyle.Add("display", "none");
            topdiv.Attributes.CssStyle.Add("display", "none");
            gridcontainer.Attributes.CssStyle.Add(" overflow-x", "scroll");
            percentpass.Attributes.CssStyle.Add("display", "block");
            exportbtn.Attributes.CssStyle.Add("display", "block");

            string q1 = "", q2 = "", q3 = "", q4 = "", q5 = "", q6 = "", q7 = "", q8 = "", qt = "";
            if (rescountint != 0)
            {
                string query = "select * from results";
                qt = "(select  Ktuid, Sum(SupplyCount) as [Total Arrears] from results where Id = '" + id + "' and Sem<= '" + rescountint + "' group by Ktuid) as I on A.Ktuid = I.Ktuid";
                if (rescountint >= 1)
                {
                    q1 = "(select Ktuid, Name, FailedSubjects as [Arrears in S1],Credit,Sgpa from results where Id = '" + id + "' and Sem = 1 ) as A";
                    query = "select  A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Total Arrears] from(" + q1 + " left join " + qt + " )";
                    k = 5;
                }
                if (rescountint >= 2)
                {
                    q2 = "(select Ktuid, FailedSubjects as [Arrears in S2], Credit as [Credits in S2], CumilativeCredits as [Credits Upto S2], Cgpa from results where Id = '" + id + "' and Sem = 2) as B on A.Ktuid = B.Ktuid";
                    query = "select  A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as Cgpa,[Total Arrears]from(" + q1 + " left join " + q2 + " left join " + qt + " )";
                    k = 9;
                }
                if (rescountint >= 3)
                {
                    q3 = "(select Ktuid, FailedSubjects as [Arrears in S3], Credit as [Credits in S3], CumilativeCredits as [Credits Upto S3], Cgpa from results where Id = '" + id + "' and Sem = 3) as C on A.Ktuid = C.Ktuid";
                    query = "select  A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + "  left join " + qt + ")";
                    k = 13;
                }
                if (rescountint >= 4)
                {
                    q4 = "(select Ktuid, FailedSubjects as [Arrears in S4], Credit as [Credits in S4], CumilativeCredits as [Credits Upto S4], Cgpa from results where Id = '" + id + "' and Sem = 4) as D on A.Ktuid = D.Ktuid";
                    query = "select  A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Arrears in S4],[Credits in S4],[Credits Upto S4],D.Cgpa as [Cgpa upto S4],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + " left join " + q4 + "  left join " + qt + ")";
                    k = 17;
                }
                if (rescountint >= 5)
                {
                    q5 = "(select Ktuid, FailedSubjects as [Arrears in S5], Credit as [Credits in S5], CumilativeCredits as [Credits Upto S5], Cgpa from results where Id = '" + id + "' and Sem = 5) as E on A.Ktuid = E.Ktuid";
                    query = "select A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Arrears in S4],[Credits in S4],[Credits Upto S4],D.Cgpa as [Cgpa upto S4],[Arrears in S5],[Credits in S5],[Credits Upto S5],E.Cgpa as [Cgpa upto S5],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + " left join " + q4 + "  left join " + q5 + " left join " + qt + ")";
                    k = 21;
                }
                if (rescountint >= 6)
                {
                    q6 = "(select Ktuid, FailedSubjects as [Arrears in S6], Credit as [Credits in S6], CumilativeCredits as [Credits Upto S6], Cgpa from results where Id = '" + id + "' and Sem = 6) as F on A.Ktuid = F.Ktuid";
                    query = "select A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Arrears in S4],[Credits in S4],[Credits Upto S4],D.Cgpa as [Cgpa upto S4],[Arrears in S5],[Credits in S5],[Credits Upto S5],E.Cgpa as [Cgpa upto S5],[Arrears in S6],[Credits in S6],[Credits Upto S6],F.Cgpa as [Cgpa upto S6],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + " left join " + q4 + "  left join " + q5 + " left join " + q6 + " left join " + qt + ")";
                    k = 25;
                }
                if (rescountint >= 7)
                {
                    q7 = "(select Ktuid, FailedSubjects as [Arrears in S7], Credit as [Credits in S7], CumilativeCredits as [Credits Upto S7], Cgpa from results where Id = '" + id + "' and Sem = 7) as G on A.Ktuid = G.Ktuid";
                    query = "select A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Arrears in S4],[Credits in S4],[Credits Upto S4],D.Cgpa as [Cgpa upto S4],[Arrears in S5],[Credits in S5],[Credits Upto S5],E.Cgpa as [Cgpa upto S5],[Arrears in S6],[Credits in S6],[Credits Upto S6],F.Cgpa as [Cgpa upto S6],[Arrears in S7],[Credits in S7],[Credits Upto S7],F.Cgpa as [Cgpa upto S7],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + " left join " + q4 + "  left join " + q5 + " left join " + q6 + " left join " + q7 + " left join " + qt + ")";
                    k = 29;
                }
                if (rescountint >= 8)
                {
                    q8 = "(select Ktuid, FailedSubjects as [Arrears in S8], Credit as [Credits in S8], CumilativeCredits as [Credits Upto S8], Cgpa from results where Id = '" + id + "' and Sem = 8) as H on A.Ktuid = H.Ktuid";
                    query = "select A.Ktuid,A.Name as [Name of Student],[Arrears in S1],A.Credit as Credits,A.Sgpa as Sgpa,[Arrears in S2],[Credits in S2],[Credits Upto S2],B.Cgpa as [Cgpa upto S2],[Arrears in S3],[Credits in S3],[Credits Upto S3],C.Cgpa as [Cgpa upto S3],[Arrears in S4],[Credits in S4],[Credits Upto S4],D.Cgpa as [Cgpa upto S4],[Arrears in S5],[Credits in S5],[Credits Upto S5],E.Cgpa as [Cgpa upto S5],[Arrears in S6],[Credits in S6],[Credits Upto S6],F.Cgpa as [Cgpa upto S6],[Arrears in S7],[Credits in S7],[Credits Upto S7],F.Cgpa as [Cgpa upto S7],[Arrears in S8],[Credits in S8],[Credits Upto S8],F.Cgpa as [Cgpa upto S8],[Total Arrears] from(" + q1 + " left join " + q2 + " left join " + q3 + " left join " + q4 + "  left join " + q5 + " left join " + q6 + " left join " + q7 + " left join " + q8 + " left join " + qt + ")";
                    k = 33;
                }

                string constr = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query+" order by Ktuid asc"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = con;
                            sda.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                gridview.Visible = true;
                                sda.Fill(dt);
                                gridview.DataSource = dt;
                                gridview.DataBind();

                            }
                        }
                    }

                }

                int i = 0;

                foreach (GridViewRow row in gridview.Rows)
                {

                    if (rescountint >= 1)
                    {
                        i = 2;
                        for (; i <= 4; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.LightGray;
                            row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                        }


                    }
                    if (rescountint >= 2)
                    {
                        for (; i <= 8; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.BurlyWood;
                            row.Cells[i].BackColor = System.Drawing.Color.BurlyWood;
                        }

                    }
                    if (rescountint >= 3)
                    {
                        for (; i <= 12; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.Khaki;
                            row.Cells[i].BackColor = System.Drawing.Color.Khaki;
                        }

                    }
                    if (rescountint >= 4)
                    {
                        for (; i <= 16; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.LightBlue;
                            row.Cells[i].BackColor = System.Drawing.Color.LightBlue;
                        }

                    }
                    if (rescountint >= 5)
                    {
                        for (; i <= 20; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.MediumAquamarine;
                            row.Cells[i].BackColor = System.Drawing.Color.MediumAquamarine;
                        }

                    }
                    if (rescountint >= 6)
                    {
                        for (; i <= 24; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.LightCoral;
                            row.Cells[i].BackColor = System.Drawing.Color.LightCoral;
                        }

                    }
                    if (rescountint >= 7)
                    {
                        for (; i <= 28; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                            row.Cells[i].BackColor = System.Drawing.Color.CadetBlue;
                        }

                    }
                    if (rescountint >= 8)
                    {
                        for (; i <= 32; i++)
                        {
                            gridview.HeaderRow.Cells[i].BackColor = System.Drawing.Color.LightPink;
                            row.Cells[i].BackColor = System.Drawing.Color.LightPink;
                        }

                    }


                }

                

                foreach (GridViewRow row in gridview.Rows)
                {
                    int suppcount;
                    totalcount++;
                    int.TryParse(row.Cells[k].Text, out suppcount);
                    if (suppcount == 0)
                    {
                        allpasscount++;

                    }

                    float percent = (float)allpasscount * 100 / totalcount;
                    percentpass.InnerText = "Pass Percentage: " + percent;
                }
            }
            reportgen.Attributes.CssStyle.Add("display", "none");
            closereport.Attributes.CssStyle.Add("display", "block");
        }


        int colcount, rowcount;
        string[] head;
        string temp1, temp;

        protected void upload_sheet(object sender, EventArgs e)
        {
            FileUpload fileuploadid = FileUpload1;
            Button button = (Button)sender;
            string buttonId = button.ID;
            sem = buttonId.Remove(2);
            sem = sem.TrimStart('s');
            semno = Convert.ToInt32(sem);
            int prevsem = semno - 1;
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from results where Id='" + id + "' and Sem='" + prevsem + "'";
            Int32 rescount = (Int32)cmd.ExecuteScalar();
            conn.Close();
            if (semno == 1)
            {
                fileuploadid = FileUpload1;
            }
            else
            {
                if (semno == 2)
                {
                    fileuploadid = FileUpload2;
                }
                else if (semno == 3)
                {
                    fileuploadid = FileUpload3;
                }
                else if (semno == 4)
                {
                    fileuploadid = FileUpload4;

                }
                else if (semno == 5)
                {
                    fileuploadid = FileUpload5;
                }
                else if (semno == 6)
                {
                    fileuploadid = FileUpload6;
                }
                else if (semno == 7)
                {
                    fileuploadid = FileUpload7;
                }
                else if (semno == 8)
                {
                    fileuploadid = FileUpload8;
                }
            }
            if (String.Equals(Path.GetFileName(fileuploadid.PostedFile.FileName), "") == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File Not Selected');", true);
            }
            else
            {
                if ((rescount > 0) || (semno == 1))
                {
                    string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(fileuploadid.PostedFile.FileName);
                    string extension = Path.GetExtension(fileuploadid.PostedFile.FileName);
                    fileuploadid.SaveAs(excelPath);
                    Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook excelworkbook = excelapp.Workbooks.Open(excelPath);
                    foreach (Worksheet sheet in excelworkbook.Worksheets)
                    {
                        rowcount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                        System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                        Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                                        false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;


                        colcount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                                       System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                                       Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                                             false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
                        head = new string[colcount + 1];
                        for (int i = 1; i <= colcount; i++)
                        {
                            head[i] = sheet.Cells[2, i].Text;
                        }
                        sheet.Rows[1].Delete();

                    }
                    excelworkbook.Save();
                    excelworkbook.Close(true);
                    excelapp.Application.Quit();
                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls":
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx":
                            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                            break;
                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("select *," + id + " as [Constid]," + semno + " as [Semester] from [" + sheet1 + "] where Student IS NOT NULL", excel_con))
                        {

                            oda.Fill(dtExcelData);
                            dtExcelData.Columns.Add("Supplycount", typeof(int));
                            dtExcelData.Columns.Add("Supplysubs", typeof(string));
                            excel_con.Close();
                            foreach (DataRow dr in dtExcelData.Rows)
                            {
                                int oldval, newval, flag = 0;
                                string oldsub = "", newsub;
                                for (int i = 2; i <= colcount; i++)
                                {
                                    if (i == 2)
                                        temp = (string)dr[2];

                                    if ((string.Equals(dr[i], "F") == true) || (string.Equals(dr[i], "AB") == true))
                                    {
                                        flag = flag + 1;
                                        if (flag == 1)
                                            oldval = 0;
                                        else
                                            oldval = (int)dr["Supplycount"];
                                        newval = oldval + 1;
                                        dr["Supplycount"] = newval;
                                        if (flag == 1)
                                            oldsub = "";
                                        else
                                            oldsub = "," + (string)dr["Supplysubs"];
                                        newsub = (string)head[i - 1];
                                        dr["Supplysubs"] = newsub + oldsub;
                                    }

                                }
                            }


                        }



                        string consString = ConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString;
                        using (SqlConnection con = new SqlConnection(consString))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                con.Open();
                                sqlBulkCopy.DestinationTableName = "dbo.results";
                                sqlBulkCopy.ColumnMappings.Add("Constid", "Id");
                                sqlBulkCopy.ColumnMappings.Add("Semester", "Sem");
                                sqlBulkCopy.ColumnMappings.Add("Student", "Ktuid");
                                sqlBulkCopy.ColumnMappings.Add("student", "Name");
                                sqlBulkCopy.ColumnMappings.Add("Supplycount", "SupplyCount");
                                sqlBulkCopy.ColumnMappings.Add("Supplysubs", "FailedSubjects");
                                sqlBulkCopy.ColumnMappings.Add("Earned Credits", "Credit");
                                sqlBulkCopy.ColumnMappings.Add("Cumilative Credits", "CumilativeCredits");
                                sqlBulkCopy.ColumnMappings.Add("SGPA", "Sgpa");
                                sqlBulkCopy.ColumnMappings.Add("CGPA", "Cgpa");
                                sqlBulkCopy.WriteToServer(dtExcelData);
                                con.Close();
                            }
                        }
                    }
                    SqlConnection con1 = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
                    SqlCommand cmd1 = con1.CreateCommand();
                    con1.Open();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "update results set Ktuid = LEFT(Ktuid, CHARINDEX('-', Ktuid) -1) where Sem='" + sem + "' and Id='" + id + "'";
                    cmd1.ExecuteNonQuery();
                    cmd1.CommandText = "update results set Name = RIGHT(Name, CHARINDEX('-', reverse(Name))-1) where Sem='" + sem + "' and Id='" + id + "'";
                    cmd1.ExecuteNonQuery();
                    con1.Close();
                    if (semno == 1)
                    {
                        s1.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                    }
                    else
                    {
                        if (semno == 2)
                        {
                            s2.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }
                        if (semno == 3)
                        {
                            s3.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }


                        if (semno == 4)
                        {
                            s4.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }
                        if (semno == 5)
                        {
                            s5.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }
                        if (semno == 6)
                        {
                      
                            s6.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }
                        if (semno == 7)
                        {
                         
                            s7.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }
                            if (semno == 8)
                            {
                            s8.Attributes.CssStyle.Add("background-color", "#0CD66B !important");
                        }

                    }
                    close_report(closereport, e);
                }
                else
                { 
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Upload Previous Semesters before Uploading a Particular Semester');", true);
                } 
            } }
        protected void update_sheet(object sender, EventArgs e)
        {
            int x, index,index1;
            string ktuid, name,fsubs;
            FileUpload fileuploadid = FileUpload1;
            Button button = (Button)sender;
            string buttonId = button.ID;
            sem = buttonId.Remove(2);
            sem = sem.TrimStart('s');
            semno = Convert.ToInt32(sem);
            int prevsem = semno - 1;
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select count(*) from results where Id='" + id + "' and Sem='" + semno + "'";
            Int32 rescount = (Int32)cmd.ExecuteScalar();
            conn.Close();
            if (semno == 1)
            {
                fileuploadid = FileUpload1;
            }
            else
            {
                if (semno == 2)
                {
                    fileuploadid = FileUpload2;
                }
                else if (semno == 3)
                {
                    fileuploadid = FileUpload3;
                }
                else if (semno == 4)
                {
                    fileuploadid = FileUpload4;

                }
                else if (semno == 5)
                {
                    fileuploadid = FileUpload5;
                }
                else if (semno == 6)
                {
                    fileuploadid = FileUpload6;
                }
                else if (semno == 7)
                {
                    fileuploadid = FileUpload7;
                }
                else if (semno == 8)
                {
                    fileuploadid = FileUpload8;
                }
            }
            if (String.Equals(Path.GetFileName(fileuploadid.PostedFile.FileName), "") == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('File Not Selected');", true);
            }
            else
            {
                if ((rescount > 0) || (semno == 1))
                {

                    string excelPath = Server.MapPath("~/Files/") + Path.GetFileName(fileuploadid.PostedFile.FileName);
                    string extension = Path.GetExtension(fileuploadid.PostedFile.FileName);
                    fileuploadid.SaveAs(excelPath);
                    Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook excelworkbook = excelapp.Workbooks.Open(excelPath);
                    foreach (Worksheet sheet in excelworkbook.Worksheets)
                    {
                        rowcount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                        System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                        Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                                        false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;


                        colcount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                                       System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                                       Microsoft.Office.Interop.Excel.XlSearchOrder.xlByColumns, Microsoft.Office.Interop.Excel.XlSearchDirection.xlPrevious,
                                             false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;
                        head = new string[colcount + 1];
                        for (int i = 1; i <= colcount; i++)
                        {
                            head[i] = sheet.Cells[2, i].Text;
                        }
                        sheet.Rows[1].Delete();

                    }
                    excelworkbook.Save();
                    excelworkbook.Close(true);
                    excelapp.Application.Quit();
                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls":
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx":
                            conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                            break;
                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();
                        using (OleDbDataAdapter oda = new OleDbDataAdapter("select *," + id + " as [Constid]," + semno + " as [Semester] from [" + sheet1 + "] where Student IS NOT NULL", excel_con))
                        {

                            oda.Fill(dtExcelData);
                            excel_con.Close();

                            foreach (DataRow dr in dtExcelData.Rows)
                            {
                                fsubs = "";
                                // int oldval, newval, flag = 0;
                                // string oldsub = "", newsub;

                                name = dr["Student"].ToString();
                                if (name.Contains("-") == true)
                                {
                                    index = name.IndexOf("-");
                                    ktuid = name.Substring(0, index);
                                    string studname = name.Substring(index + 1);
                                    SqlCommand cmd1 = conn.CreateCommand();
                                    conn.Open();
                                    cmd1.CommandType = CommandType.Text;
                                    cmd1.CommandText = "select * from results where Id=" + id + " and sem=" + semno + " and Ktuid='" + ktuid + "'";
                                    Int32 ispresent = 0;
                                   ispresent = Convert.ToInt32(cmd1.ExecuteScalar());
                                    if (ispresent>0)
                                    {
                                        cmd1.CommandText = "select FailedSubjects from results where Id='" + id + "' and sem=" + semno + " and Ktuid='" + ktuid + "'";

                                        object queryres =cmd1.ExecuteNonQuery();
                                        if (queryres != null)
                                        {
                                            SqlDataReader myreader = cmd1.ExecuteReader();

                                            while (myreader.Read())
                                            {
                                                fsubs = myreader[0].ToString();

                                            }
                                            myreader.Close();

                                            for (x = 2; x <= colcount; x++)
                                            {
                                                cmd1.CommandText = "update results set Sgpa='" + dr["SGPA"] + "',Cgpa='" + dr["CGPA"] + "' where Id='" + id + "' and sem=" + semno + " and Ktuid='" + ktuid + "'";
                                                cmd1.ExecuteNonQuery();
                                                if (fsubs.Contains(head[x - 1]) == true)
                                                {

                                                    if ((string.Equals(dr[head[x - 1]], "F") == false) && (string.Equals(dr[head[x - 1]], "AB") == false) && (dr.IsNull(head[x - 1]) == false))
                                                    {

                                                        //index1 = fsubs.IndexOf(head[x-1]);

                                                        // string removesubs = fsubs.Substring(0, index);//
                                                        if (fsubs.Contains("," + head[x - 1]) == true)
                                                            fsubs = fsubs = fsubs.Replace("," + head[x - 1], "");
                                                        else if (fsubs.Contains(head[x - 1] + ",") == true)
                                                            fsubs = fsubs = fsubs.Replace(head[x - 1] + ",", "");
                                                        else
                                                            fsubs = fsubs.Replace(head[x - 1], "");
                                                        cmd1.CommandText = "update results set FailedSubjects='" + fsubs + "',SupplyCount=SupplyCount-1 where Id='" + id + "' and sem=" + semno + " and Ktuid='" + ktuid + "'";
                                                        cmd1.ExecuteNonQuery();

                                                    }
                                                }
                                            }


                                            //  for (int i = 1; i <= colcount; i++)
                                            // {
                                            //
                                            //
                                            //
                                            //     if (i == 1)
                                            //         temp = (string)dr[2];
                                            //
                                            //     if ((string.Equals(dr[i], "F") == true) || (string.Equals(dr[i], "AB") == true))
                                            //     {
                                            //         flag = flag + 1;
                                            //         if (flag == 1)
                                            //             oldval = 0;
                                            //         else
                                            //             oldval = (int)dr["Supplycount"];
                                            //         newval = oldval + 1;
                                            //         dr["Supplycount"] = newval;
                                            //         if (flag == 1)
                                            //             oldsub = "";
                                            //         else
                                            //             oldsub = "," + (string)dr["Supplysubs"];
                                            //         newsub = (string)head[i - 1];
                                            //         dr["Supplysubs"] = newsub + oldsub;
                                            //     }
                                            //
                                            //    }
                                        }
                                    }
                                    else
                                    {
                                        SqlCommand cmd2 = conn.CreateCommand();
                                        cmd2.CommandType = CommandType.Text;
                                        cmd2.CommandText = "insert into results(Id,Sem,Ktuid,Name) values("+id+","+semno+",'"+ktuid+"','"+studname+"')";
                                        cmd2.ExecuteNonQuery();
                                    }
                                }
                                conn.Close();
                            }
                        }

                    }

                        }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot Update Without Uploading Result');", true);
                }

                }
        }
        protected void close_report(object sender, EventArgs e)
        {
            topdiv.Attributes.CssStyle.Add("display", "flex");
            gridcontainer.Attributes.CssStyle.Add(" overflow-x", "hidden");
            percentpass.Attributes.CssStyle.Add("display", "none");
            gridview.Visible = false;
            exportbtn.Attributes.CssStyle.Add("display", "none");
            reportgen.Attributes.CssStyle.Add("display", "block");
            closereport.Attributes.CssStyle.Add("display", "none");

        }

        protected void grid_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            int supcount;
            if (e.Row.RowIndex >= 0)
            {

                int.TryParse(e.Row.Cells[k].Text, out supcount);
                if (supcount == 0)
                {
                    e.Row.Cells[k].BackColor = System.Drawing.Color.LawnGreen;
                }
                if (supcount >= 5)
                {
                    e.Row.Cells[k].BackColor = System.Drawing.Color.Red;
                }


                else if (supcount >= 1 && supcount < 5)
                {
                    e.Row.Cells[k].BackColor = System.Drawing.Color.Orange;
                }

            }

        }

        protected void grid_sort(object sender, GridViewSortEventArgs e)
        {
        
        }
        public override void VerifyRenderingInServerForm(System.Web.UI.Control control)
        {
            return;
        }
        protected void export_grid(object sender, EventArgs e)
        {

     SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["myconnectionstring"].ConnectionString);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from users where userid=" + id + " and name IS NOT NULL and college IS NOT NULL ";
             Int32 quercount= Convert.ToInt32(cmd.ExecuteScalar());
            if (quercount > 0)
            {
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Student Report.xls");
                Response.ContentType = "File/Data.xls";
                StringWriter StringWriter = new System.IO.StringWriter();
                HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
                gridview.RenderControl(HtmlTextWriter);
                cmd.CommandText = "Select college from users where userid='" + id + "' and name IS NOT NULL";
                Response.Write("<center><h3> " + cmd.ExecuteScalar().ToString()+ "</h3></center>");
                cmd.CommandText = "Select name from users where userid='" + id + "' and name IS NOT NULL";
                Response.Write("<center><h3> Result Analysis Prepared By " + cmd.ExecuteScalar().ToString() + "</h3></center>");
                Response.Write(StringWriter.ToString());
                Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('First Update your Profile before Exporting..');", true);
            }
        
        }
      
    }
   
}
    


