using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DocumentFormat.OpenXml.Drawing;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;
using System.Security.Cryptography;

public partial class MasterFiles_newtp : System.Web.UI.Page
{
    DataTable dsProd1 = null;
    DataSet dsProd = null;
    DataSet dsProduct = null;
    DataSet dsDivision = null;
    DataSet dsState = null;
    string divcode = string.Empty;
    public string sf_type = string.Empty;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            GetMR();

        }
    }

    public class sfMR
    {
        public string sfname { get; set; }
        public string sfcode { get; set; }
    }

    public class dist
    {
        public string disname { get; set; }
        public string discode { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static List<ListItem> GetMR()
    {
        SalesForce dsf = new SalesForce();
        string divcode = HttpContext.Current.Session["div_code"].ToString();
        string sf_code = HttpContext.Current.Session["Sf_Code"].ToString();
        string sftype = HttpContext.Current.Session["sf_type"].ToString();

        List<ListItem> fldname = new List<ListItem>();
        DataSet ds = new DataSet();

        if (sftype == "2")
        { 
            //ds = dsf.UserList_getMGR(divcode, sf_code);
            ds = UserList_getMGR(divcode, sf_code);
        }
        else 
        {
            //ds = dsf.UserList_getMR(divcode);
            ds = UserList_getMR(divcode);
        }

        List<sfMR> sf = new List<sfMR>();
        foreach (DataRow rows in ds.Tables[0].Rows)
        {
            fldname.Add(new ListItem
            {
                Value = rows["SF_Code"].ToString(),
                Text = rows["Sf_Name"].ToString(),

            });
        }
        return fldname;
    }

    public static DataSet UserList_getMGR(string divcode, string sf_code)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;
        //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

        //strQry = "SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
        //               " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
        //               " FROM mas_salesforce a " +
        //               " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
        //               " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
        //               " and a.Reporting_To_SF ='" + sf_code + "' " +
        //               " order by 2 ";


        strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, ";
        strQry += " ( SELECT sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password ";
        strQry += " FROM mas_salesforce a ";
        //strQry += " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or ";
        strQry += " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like @divcode + ',' + '%' or ";
        //strQry += " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";
        strQry += " a.Division_Code like '%' + @divcode+ '%') ";
        strQry += " and a.Reporting_To_SF = @sf_code ";
        strQry += " order by 2 ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));
                    cmd.Parameters.AddWithValue("@sf_code", Convert.ToString(sf_code));

                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    public static DataSet UserList_getMR(string divcode)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;
        //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

        //strQry = "SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
        //            " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
        //            " FROM mas_salesforce a " +
        //            " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
        //            " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
        //            " and a.sf_code !='admin' and a.sf_type=1 " +
        //            " order by 2 ";

        strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,  ";
        strQry += "  ( SELECT sf_name from Mas_Salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password  ";
        strQry += "  FROM Mas_Salesforce a  ";
        //strQry += " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or ";
        strQry += "   WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like @divcode + ',' + '%'  or ";
        //strQry += " a.Division_Code like '%" + ',' + divcode + ',' + "%') ";
        strQry += "   a.Division_Code like '%' + @divcode+ '%') ";
        strQry += "  and a.sf_code != 'admin' and a.sf_type=1  ";
        strQry += " order by 2  ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));                   

                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }


    [WebMethod(EnableSession = true)]
    public static List<ListItem> distname(string fieldforcecode)
    {
        //SalesForce dsf = new SalesForce();
        List<ListItem> disname = new List<ListItem>();
        DataSet ds = new DataSet();

        //ds = dsf.getdisttributor(fieldforcecode);

        ds = getdisttributor(fieldforcecode);

        List<dist> sf = new List<dist>();
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                disname.Add(new ListItem
                {
                    Value = rows["Stockist_Code"].ToString(),
                    Text = rows["Stockist_Name"].ToString(),

                });
            }
        }
        return disname;
    }

    public static DataSet getdisttributor(string fieldforcecode)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;

        //strQry = "select Stockist_Code,Stockist_Name from mas_stockist where CHARINDEX('" + fieldforcecode + "',field_code)>0 and Stockist_Active_Flag=0";
        strQry = " SELECT Stockist_Code,Stockist_Name FROM Mas_Stockist ";
        strQry += " WHERE CHARINDEX(@fieldforcecode,field_code)>0 and Stockist_Active_Flag=0  ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@fieldforcecode", Convert.ToString(fieldforcecode));
                    
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }

    public class bindyear
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    [WebMethod(EnableSession = true)]
    public static bindyear[] BindDate(string divcode)
    {
        //TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        //dsTP = tp.Get_TP_Edit_Year(divcode);
        dsTP = Get_TP_Edit_Year(divcode);
        List<bindyear> sf = new List<bindyear>();
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                bindyear rt = new bindyear();
                rt.value = k.ToString();
                rt.text = k.ToString();
                sf.Add(rt);
            }
        }
        return sf.ToArray();
    }

    public static DataSet Get_TP_Edit_Year(string divcode)
    {
        //DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = new DataSet();
        string strQry = String.Empty;

        //strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
        strQry = " SELECT max([Year]-1) as Year FROM Mas_Division ";
        strQry += " WHERE Division_Code = @divcode ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@divcode", Convert.ToString(divcode));                    
                    cmd.CommandType = CommandType.Text;

                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
            //dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;

    }

    [WebMethod(EnableSession = true)]
    public static string FillProd(string ffc)
    {
        Product dv = new Product();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //string subdiv_code = HttpContext.Current.Session["subdivision_code"].ToString();

        DataSet dsProd = dv.get_pro_all(ffc, div_code);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string clsdata(string ffc, string emonth, string eyear, string distname)
    {
        Product dv = new Product();
        string divcode = HttpContext.Current.Session["div_code"].ToString();

        DataSet dsProd = dv.getclosProdall(ffc, emonth, eyear, divcode, distname);
        return JsonConvert.SerializeObject(dsProd.Tables[0]);
    }

    [WebMethod(EnableSession = true)]
    public static string Savestock(string Data, string fieldforcecode, string distname, string emonth, string eyear)
    {
        var items = JsonConvert.DeserializeObject<List<proddtl>>(Data);
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        Product prd = new Product();

        DataSet ds_EReport = new DataSet();
        string sxml = "<ROOT>";
        for (int k = 0; k < items.Count; k++)
        {
            if (items[k].oqtys != "" || items[k].ofrees != "" || items[k].pqtys != "" || items[k].pfrees != "" || items[k].sqtys != "" || items[k].sfrees != "" || items[k].cqtys != "" || items[k].cfrees != "" || items[k].rqtys != "" || items[k].rfrees != "")
            {
                sxml += "<ASSD PCode=\"" + items[k].pCodes + "\" oqty=\"" + items[k].oqtys + "\" ofree=\"" + items[k].ofrees + "\" pqty=\"" + items[k].pqtys + "\"  pfree=\"" + items[k].pfrees + "\" sqty=\"" + items[k].sqtys + "\" sfree=\"" + items[k].sfrees + "\" cqty=\"" + items[k].cqtys + "\" cfree=\"" + items[k].cfrees + "\" rqty=\"" + items[k].rqtys + "\" rfree=\"" + items[k].rfrees + "\"/>";
            }
        }
        sxml += "</ROOT>";
        string consString = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(consString))
        {
            using (SqlCommand cmd = con.CreateCommand())
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertMasstock";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@sxml", sxml),
                    new SqlParameter("@ffcode", fieldforcecode),
                    new SqlParameter("@dtcode", distname),
                    new SqlParameter("@emonths", emonth),
                    new SqlParameter("@eyears", eyear),
                    new SqlParameter("@divcode", div_code),
                };
                cmd.Parameters.AddRange(parameters);

                try
                {
                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
        return "Success";
    }

    public class proddtl
    {
        public string pCodes { get; set; }
        public string oqtys { get; set; }
        public string ofrees { get; set; }
        public string pqtys { get; set; }
        public string pfrees { get; set; }
        public string sqtys { get; set; }
        public string sfrees { get; set; }
        public string cqtys { get; set; }
        public string cfrees { get; set; }
        public string rqtys { get; set; }
        public string rfrees { get; set; }
    }
}