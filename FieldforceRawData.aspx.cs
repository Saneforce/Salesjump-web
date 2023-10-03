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

public partial class FieldforceRawData : System.Web.UI.Page
{
    string divCode = string.Empty;
    string userName = string.Empty;
    string pwd = string.Empty;
    string fromDate = string.Empty;
    string toDate = string.Empty;
    string subdivcode = string.Empty;
    string alpha = string.Empty;
    string sfind = string.Empty;
    string Sf_code = string.Empty;
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
            //Response.Write("CSV File Successfully Downloaded");            
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
                string sql = " SELECT a.Sf_Code Employee_Id, a.Sf_Name as FieldForceName,a.sf_Designation_Short_Name as Designation,(select sf_name from mas_salesforce ms where a.Reporting_To_SF=ms.sf_code) as Reporting_Manager,a.Sf_Joining_Date,a.sf_TP_Active_Dt,Territory,b.StateName,a.SF_Mobile as MobileNo, cast(convert(varchar,SF_DOB,101) as date) DOB,a.UsrDfd_UserName Username , SF_Email as Email,SF_ContactAdd_One +' '+SF_ContactAdd_Two as Address from Mas_Salesforce a join mas_state b on  a.State_Code=b.State_Code WHERE  a.sf_Tp_Active_flag = 0 and a.SF_Status=0 and a.Division_Code like '63,%' or  a.Division_Code like '%,63,%'  and lower(a.sf_code) != 'admin' group by  a.Sf_Code,a.Sf_Name,a.sf_Designation_Short_Name,a.Reporting_To_SF,a.UsrDfd_UserName,a.Sf_Joining_Date,a.sf_TP_Active_Dt,Territory,b.StateName,a.SF_Mobile,cast(convert(varchar,SF_DOB,101) as date),SF_Email,SF_ContactAdd_One +' '+SF_ContactAdd_Two order by 1 ";
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@Div", divCode);
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables.Count > 0)
                {
                    StringBuilder stb = new StringBuilder();
                    stb.AppendLine("Employee_Id,FieldForceName,Designation,Reporting Manager,Joining Date,Reporting Date,Territory,StateName,MobileNo,DOB,UserName,Email,Address");
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //stb.AppendLine((i + 1).ToString());
                        StringBuilder st = new StringBuilder();
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            st.Append(ds.Tables[0].Rows[i][j].ToString().TrimStart('+').TrimStart('-').Replace(",", "_"));
                            st.Append(",");
                        }
                        stb.AppendLine(st.ToString().TrimEnd(','));
                    }

                    string filname = "FieldforceRawData.csv";

                    byte[] bytes = Encoding.ASCII.GetBytes(stb.ToString());
                    Response.Clear();
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Length", bytes.Length.ToString());
                    Response.AddHeader("Content-disposition", "attachment; filename=\"" + filname + "" + "\"");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Response.Write("No Data Found..!");
                }

            }
        }
    }
}