using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;

public partial class attendanceRawdate : System.Web.UI.Page
{
    string divCode = string.Empty;
    string userName = string.Empty;
    string pwd = string.Empty;
    string date = string.Empty;
     string subdivcode = string.Empty;
    string Sf_code = "admin";
    string constr = Globals.ConnString;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataSet ds = null;
    Boolean chConn = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            userName = Request.QueryString["username"].ToString();
        }
        catch
        {
            Response.Write("Username is required...");
            userName = string.Empty;
        }
        try
        {
            pwd = Request.QueryString["pwd"].ToString();
        }
        catch
        {
            Response.Write("Password is required...");
            pwd = string.Empty;
        }
        try
        {
            date = Request.QueryString["date"].ToString();
        }
        catch
        {
            Response.Write("date is required...");
            date = string.Empty;
        }
        if (userName == string.Empty)
        {
            chConn = false;
        }

        if (pwd == string.Empty)
        {
            chConn = false;
        }

        if (chConn)
        {
            using (con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    string sql = "select * from mas_ho_id_creation where User_Name=@sanuserName and Password=@sanpwd";
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@sanuserName", userName);
                    cmd.Parameters.AddWithValue("@sanpwd", pwd);
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0)
                    {
                        divCode = ds.Tables[0].Rows[0]["Division_Code"].ToString().TrimEnd(',');
                    }
                    else
                    {
                        divCode = null;
                    }
                }
            }
        }
        else
        {
            Response.Write("User Name and Password Not match..!");
        }
        if (divCode != string.Empty)
        {
            Loaddata();
        }
        else
        {
            Response.Write("User Name  and Password Not match..!");
        }
    }
    private void Loaddata()
    {
        using (con = new SqlConnection(constr))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "tgetMyDayPlanVwSFHry_sub";
                cmd.Parameters.AddWithValue("@SF", Sf_code);
                cmd.Parameters.AddWithValue("@Div", divCode);
                cmd.Parameters.AddWithValue("@Dt", date);
                cmd.Parameters.AddWithValue("@Sub_Div_Code", subdivcode);
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(ds.Tables[0], "Daily Attendance List");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Daily_Attendance" + date + ".xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                else
                {
                    Response.Write("No Data Found..!");
                }

            }
        }
    }
}