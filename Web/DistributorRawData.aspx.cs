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

public partial class DistributorRawData : System.Web.UI.Page
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
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GET_DISTRIBUTOR_List_Detail";
                    cmd.Parameters.AddWithValue("@Div", divCode);
                    cmd.Parameters.AddWithValue("@SF", Sf_code);
                    cmd.Parameters.AddWithValue("@Alpha", alpha);
                    cmd.Parameters.AddWithValue("@SFind", sfind);
                    cmd.Parameters.AddWithValue("@Sub_Div_Code", subdivcode);                    
                    cmd.Connection = con;
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);

                if (ds.Tables.Count > 0)
                    {
                        StringBuilder stb = new StringBuilder();
                        stb.AppendLine("Distributor_Code,Distributor_Name,ContactPerson,Address,Mobile,FieldForce_Name,Territory");
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

                        string filname = "DistributorRawData.csv";

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