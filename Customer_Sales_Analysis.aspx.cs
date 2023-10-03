using System;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Globalization;
using ClosedXML.Excel;

public partial class Customer_Sales_Analysis : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string divcode = string.Empty;
    string FYear = string.Empty;
    string constr = Globals.ConnString;
    SqlConnection con = null;
    SqlCommand cmd = null;
    SqlDataAdapter da = null;
    DataSet ds = null;
    string userName = string.Empty;
    string pwd = string.Empty;

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
                FYear = Request.QueryString["FYear"].ToString();
            }
            catch
            {
                FYear = System.DateTime.Now.Year.ToString();
            }
        }
        catch
        {
            Response.Write("Invalid YEAR...");
            FYear = string.Empty;
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
                        divcode = ds.Tables[0].Rows[0]["Division_Code"].ToString().TrimEnd(',');
                    }
                    else
                    {
                        divcode = null;
                    }
                }
            }
        }
        else
        {
            Response.Write("User Name and Password Not match..!");
        }
        if (divcode != string.Empty)
        {
            if (FYear != string.Empty)
            {
                sfreport();
                //Response.Write("CSV File Successfully Downloaded");
            }
            else
            {
                Response.Write("From Year missing..!");
            }
        }
        else
        {
            Response.Write("User Name  and Password Not match..!");
        }

    }
    public void sfreport()
    {
        RoutePlan rop = new RoutePlan();
        DataSet DsRoute = new DataSet();
        DataSet DsRetailer = new DataSet();
        using (con = new SqlConnection(constr))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_Route_Name_Excel";
                cmd.Parameters.AddWithValue("@SF", "admin");
                cmd.Parameters.AddWithValue("@Div", divcode);
				cmd.Parameters.AddWithValue("@subdiv", "0");
                cmd.Connection = con;
                da = new SqlDataAdapter(cmd);
                da.Fill(DsRoute);

                SqlCommand cnd = new SqlCommand();
                cnd.CommandType = CommandType.StoredProcedure;
                cnd.CommandText = "Get_Retailer_sal_Excel";
                cnd.Parameters.AddWithValue("@Div_Code", divcode);
                cnd.Parameters.AddWithValue("@Years", FYear);
                cnd.Connection = con;
                da = new SqlDataAdapter(cnd);
                da.Fill(DsRetailer);
            }
        }
        ListedDR ldr = new ListedDR();
        DataSet tDsRetailer = ldr.tGet_Retailer_sal(divcode, FYear, sfCode);

        DataTable dsData = new DataTable();
        dsData.Columns.Add("Route", typeof(string));
        dsData.Columns.Add("HQ", typeof(string));
        dsData.Columns.Add("Code", typeof(string));
        dsData.Columns.Add("Name", typeof(string));
        dsData.Columns.Add("Category", typeof(string));
        dsData.Columns.Add("Channel", typeof(string));
        dsData.Columns.Add("Phone", typeof(string));
        dsData.Columns.Add("Address", typeof(string));
        dsData.Columns.Add("Visited", typeof(string));
        dsData.Columns.Add("JAN", typeof(decimal));
        dsData.Columns.Add("FEB", typeof(decimal));
        dsData.Columns.Add("MAR", typeof(decimal));
        dsData.Columns.Add("APR", typeof(decimal));
        dsData.Columns.Add("MAY", typeof(decimal));
        dsData.Columns.Add("JUN", typeof(decimal));
        dsData.Columns.Add("JUL", typeof(decimal));
        dsData.Columns.Add("AUG", typeof(decimal));
        dsData.Columns.Add("SEP", typeof(decimal));
        dsData.Columns.Add("OCT", typeof(decimal));
        dsData.Columns.Add("NOV", typeof(decimal));
        dsData.Columns.Add("DEC", typeof(decimal));
        dsData.Columns.Add("TOTAL", typeof(decimal));
        int i = 1;
        dsData.Rows.Add("", "", "", "Order Given Customers");
        dsData.Rows.Add("", "", "", "Order Total");

        decimal jan_ot = 0;
        decimal feb_ot = 0;
        decimal mar_ot = 0;
        decimal apr_ot = 0;
        decimal may_ot = 0;
        decimal jun_ot = 0;
        decimal jul_ot = 0;
        decimal aug_ot = 0;
        decimal sep_ot = 0;
        decimal oct_ot = 0;
        decimal nov_ot = 0;
        decimal dec_ot = 0;

        int jan_count = 0;
        int feb_count = 0;
        int mar_count = 0;
        int apr_count = 0;
        int may_count = 0;
        int jun_count = 0;
        int jul_count = 0;
        int aug_count = 0;
        int sep_count = 0;
        int oct_count = 0;
        int nov_count = 0;
        int dec_count = 0;

        foreach (DataRow dr in DsRoute.Tables[0].Rows)
        {
            decimal jan_tot = 0;
            decimal feb_tot = 0;
            decimal mar_tot = 0;
            decimal apr_tot = 0;
            decimal may_tot = 0;
            decimal jun_tot = 0;
            decimal jul_tot = 0;
            decimal aug_tot = 0;
            decimal sep_tot = 0;
            decimal oct_tot = 0;
            decimal nov_tot = 0;
            decimal dec_tot = 0;

            DataRow[] drow = DsRetailer.Tables[0].Select("Route = '" + dr["Territory_Code"].ToString() + "'");

            int stCount = drow.Length;
            if (drow.Length > 0)
            {

                foreach (DataRow row in drow)
                {
                    decimal jan_val = row["jan"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jan"]);
                    jan_tot += jan_val;
                    if (jan_val > 0)
                    {
                        jan_count++;
                    }
                    DataRow[] drpj = tDsRetailer.Tables[0].Select("Listeddrcode='" + row["ListedDrCode"].ToString() + "'");
                    decimal feb_val = row["feb"] == DBNull.Value ? 0 : Convert.ToDecimal(row["feb"]);
                    feb_tot += feb_val;
                    if (feb_val > 0)
                    {
                        feb_count++;
                    }

                    decimal mar_val = row["mar"] == DBNull.Value ? 0 : Convert.ToDecimal(row["mar"]);
                    mar_tot += mar_val;
                    if (mar_val > 0)
                    {
                        mar_count++;
                    }

                    decimal apr_val = row["apr"] == DBNull.Value ? 0 : Convert.ToDecimal(row["apr"]);
                    apr_tot += apr_val;
                    if (apr_val > 0)
                    {
                        apr_count++;
                    }

                    decimal may_val = row["may"] == DBNull.Value ? 0 : Convert.ToDecimal(row["may"]);
                    may_tot += may_val;
                    if (may_val > 0)
                    {
                        may_count++;
                    }

                    decimal jun_val = row["jun"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jun"]);
                    jun_tot += jun_val;
                    if (jun_val > 0)
                    {
                        jun_count++;
                    }

                    decimal jul_val = row["jul"] == DBNull.Value ? 0 : Convert.ToDecimal(row["jul"]);
                    jul_tot += jul_val;
                    if (jul_val > 0)
                    {
                        jul_count++;
                    }

                    decimal aug_val = row["aug"] == DBNull.Value ? 0 : Convert.ToDecimal(row["aug"]);
                    aug_tot += aug_val;
                    if (aug_val > 0)
                    {
                        aug_count++;
                    }

                    decimal sep_val = row["sep"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sep"]);
                    sep_tot += sep_val;
                    if (sep_val > 0)
                    {
                        sep_count++;
                    }

                    decimal oct_val = row["oct"] == DBNull.Value ? 0 : Convert.ToDecimal(row["oct"]);
                    oct_tot += oct_val;
                    if (oct_val > 0)
                    {
                        oct_count++;
                    }

                    decimal nov_val = row["nov"] == DBNull.Value ? 0 : Convert.ToDecimal(row["nov"]);
                    nov_tot += nov_val;
                    if (nov_val > 0)
                    {
                        nov_count++;
                    }

                    decimal dec_val = row["dec"] == DBNull.Value ? 0 : Convert.ToDecimal(row["dec"]);
                    dec_tot += dec_val;
                    if (dec_val > 0)
                    {
                        dec_count++;
                    }

                    decimal cur_tot = jan_val + feb_val + mar_val + apr_val + may_val + jun_val + jul_val + aug_val + sep_val + oct_val + nov_val + dec_val;
                    dsData.Rows.Add(dr["Territory_Name"].ToString(), dr["HQ"].ToString(), row["ListedDrCode"].ToString(), row["ListedDr_Name"].ToString(), row["Doc_cat_ShortName"].ToString(), row["Doc_Special_Name"].ToString(), row["phone"].ToString(), row["Address1"].ToString(), "", jan_val, feb_val, mar_val, apr_val, may_val, jun_val, jul_val, aug_val, sep_val, oct_val, nov_val, dec_val, cur_tot);
                }
            }
            jan_ot += jan_tot;
            feb_ot += feb_tot;
            mar_ot += mar_tot;
            apr_ot += apr_tot;
            may_ot += may_tot;
            jun_ot += jun_tot;
            jul_ot += jul_tot;
            aug_ot += aug_tot;
            sep_ot += sep_tot;
            oct_ot += oct_tot;
            nov_ot += nov_tot;
            dec_ot += dec_tot;
            decimal c_tot = jan_tot + feb_tot + mar_tot + apr_tot + may_tot + jun_tot + jul_tot + aug_tot + sep_tot + oct_tot + nov_tot + dec_tot;
            dsData.Rows.Add("", "", "", "Total", "", "", "", "", "", jan_tot, feb_tot, mar_tot, apr_tot, may_tot, jun_tot, jul_tot, aug_tot, sep_tot, oct_tot, nov_tot, dec_tot, c_tot);
        }
        // order count add in first row 
        int cou = 5;
        dsData.Rows[0][cou + 4] = jan_count;
        dsData.Rows[0][cou + 5] = feb_count;
        dsData.Rows[0][cou + 6] = mar_count;
        dsData.Rows[0][cou + 7] = apr_count;
        dsData.Rows[0][cou + 8] = may_count;
        dsData.Rows[0][cou + 9] = jun_count;
        dsData.Rows[0][cou + 10] = jul_count;
        dsData.Rows[0][cou + 11] = aug_count;
        dsData.Rows[0][cou + 12] = sep_count;
        dsData.Rows[0][cou + 13] = oct_count;
        dsData.Rows[0][cou + 14] = nov_count;
        dsData.Rows[0][cou + 15] = dec_count;
        dsData.Rows[0][cou + 16] = jan_count + feb_count + mar_count + apr_count + may_count + jun_count + jul_count + aug_count + sep_count + oct_count + nov_count + dec_count;




        // over all total in second row add 
        dsData.Rows[1][cou + 4] = jan_ot;
        dsData.Rows[1][cou + 5] = feb_ot;
        dsData.Rows[1][cou + 6] = mar_ot;
        dsData.Rows[1][cou + 7] = apr_ot;
        dsData.Rows[1][cou + 8] = may_ot;
        dsData.Rows[1][cou + 9] = jun_ot;
        dsData.Rows[1][cou + 10] = jul_ot;
        dsData.Rows[1][cou + 11] = aug_ot;
        dsData.Rows[1][cou + 12] = sep_ot;
        dsData.Rows[1][cou + 13] = oct_ot;
        dsData.Rows[1][cou + 14] = nov_ot;
        dsData.Rows[1][cou + 15] = dec_ot;
        dsData.Rows[1][cou + 16] = jan_ot + feb_ot + mar_ot + apr_ot + may_ot + jun_ot + jul_ot + aug_ot + sep_ot + oct_ot + nov_ot + dec_ot;

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dsData, "Customer Sales Analysis");
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Customer Sales Analysis_" + FYear + ".xlsx");
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