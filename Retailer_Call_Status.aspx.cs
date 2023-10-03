using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;

public partial class Retailer_Call_Status : System.Web.UI.Page
{
    string divCode = string.Empty;
    string userName = string.Empty;
    string pwd = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    DateTime Fdate;
    DateTime Tdate;
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
            try
            {
                Fdate = DateTime.ParseExact(Request.QueryString["FDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture); //HH:mm:ss
                fromDate = Fdate.ToString("yyyy-MM-dd");
            }
            catch
            {
                Fdate = DateTime.ParseExact(Request.QueryString["FDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); //HH:mm:ss
                fromDate = Fdate.ToString("yyyy-MM-dd");
            }
        }
        catch
        {
            Response.Write("Invalid FDate...");
            fromDate = string.Empty;
        }
        try
        {
            try
            {
                Tdate = DateTime.ParseExact(Request.QueryString["TDate"].ToString(), "dd-MM-yyyy", CultureInfo.InvariantCulture); // HH:mm:ss
                toDate = Tdate.ToString("yyyy-MM-dd");
            }
            catch
            {
                Tdate = DateTime.ParseExact(Request.QueryString["TDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture); // HH:mm:ss
                toDate = Tdate.ToString("yyyy-MM-dd");
            }
        }
        catch
        {
            Response.Write("Invalid TDate...");
            toDate = string.Empty;
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
            if (fromDate != string.Empty && toDate != string.Empty)
            {
                Loaddata();
                //Response.Write("CSV File Successfully Downloaded");
            }
            else
            {
                Response.Write("From & To Date missing..!");
            }
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
                cmd.CommandText = "getRetailerCall";
                cmd.Parameters.AddWithValue("@Div", divCode);
                cmd.Parameters.AddWithValue("@FDT", fromDate);
                cmd.Parameters.AddWithValue("@TDT", toDate);
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0], "Retailer Visit Status");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Retailer Visit Status " + Fdate + "_to_" + Tdate + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }
}