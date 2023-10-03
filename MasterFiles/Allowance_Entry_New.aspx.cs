using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using Newtonsoft.Json;
using DBase_EReport;


public partial class MasterFiles_Allowance_Entry_New : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    static string Division_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    #endregion
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
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            Division_code = Session["div_code"].ToString();
        }
        else
        {
            Division_code = Session["div_code"].ToString();
        }
    }
	[WebMethod(EnableSession = true)]
    public static string getPrvsDet(string DesgCode)
    {


        DataSet dsAlowtype = null;
        dsAlowtype = getsfcc(" select Sf_name +' - '+ Sf_HQ +' - '+ sf_Designation_Short_Name SfName, iif(Allowance_Name is NULL,Replace(Allowance_Code,'1',''),Allowance_Name)Allowance_Name,Allowance_Value,Convert(varchar,Ma.Created_Date,101)Created_Date,Convert(varchar,Ma.Effective_Date,101)Effective_Date" +
            " from Mas_Allowance_Entry Ma left join Mas_Allowance_Type Mt on CHARINDEX(',' + Convert(varchar, Mt.ID) + ',', ',' + Ma.Allowance_Code + ',') > 0 " +
            " left join Mas_Salesforce Ms on Ms.Sf_Code = Ma.Sf_Code where Destination_Code = " + DesgCode + " and Ma.Division_Code = " + Division_code + " and Effective_Date != '1900-01-01' " +
            " Group by Sf_name, Sf_HQ, sf_Designation_Short_Name, Allowance_Code, Allowance_Name, Allowance_Value, Ma.Created_Date, Effective_Date  Order by Created_Date");
        string res = JsonConvert.SerializeObject(dsAlowtype.Tables[0]);
        dsAlowtype.Dispose();
        return res;
    }
    public class Designation_Details
    {
        public string Designation_Code { get; set; }
        public string Designation_Short_Name { get; set; }
        public string Designation_Name { get; set; }
        public string Name { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static Designation_Details[] Get_Details()
    {

        Notice nt = new Notice();
        List<Designation_Details> Alwd = new List<Designation_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsDetails = null;
        dsDetails = nt.getDesignation_div(Div_code);

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            Designation_Details ald = new Designation_Details();
            ald.Designation_Code = row["Designation_Code"].ToString();
            ald.Designation_Name = row["Designation_Short_Name"].ToString();
            ald.Designation_Short_Name = row["Designation_Name"].ToString();
            ald.Name = row["Name"].ToString();
            Alwd.Add(ald);
        }
        return Alwd.ToArray();
    }

    public class FieldForce_Details
    {
        public string sf_code { get; set; }
        public string sf_name { get; set; }
        public string designation_name { get; set; }
        public string sf_HQ { get; set; }
        public string Designation_Code { get; set; }

    }

    [WebMethod(EnableSession = true)]
    public static FieldForce_Details[] Get_FieldForce()
    {

        alw nt = new alw();
        List<FieldForce_Details> FFD = new List<FieldForce_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();
        DataSet dsDetails = null;
        dsDetails = nt.getSalesForce_Fare(Div_code);

        foreach (DataRow row in dsDetails.Tables[0].Rows)
        {
            FieldForce_Details ffd = new FieldForce_Details();
            ffd.sf_code = row["sf_code"].ToString();
            ffd.sf_name = row["sf_name"].ToString();
            ffd.designation_name = row["designation_name"].ToString();
            ffd.sf_HQ = row["sf_HQ"].ToString();
            ffd.Designation_Code = row["Designation_Code"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }


    public class AllwonceType_Details
    {
        public string ALW_code { get; set; }
        public string ALW_name { get; set; }
        public string ALW_type { get; set; }

    }




    [WebMethod(EnableSession = true)]
    public static AllwonceType_Details[] GetAllType()
    {

        Notice nt = new Notice();
        List<AllwonceType_Details> FFD = new List<AllwonceType_Details>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowtype = null;
        dsAlowtype = nt.get_Allowance_Type_nonenter(Div_code);

        foreach (DataRow row in dsAlowtype.Tables[0].Rows)
        {
            AllwonceType_Details ffd = new AllwonceType_Details();
            ffd.ALW_code = row["ID"].ToString();
            ffd.ALW_name = row["Short_Name"].ToString();
            ffd.ALW_type = row["type"].ToString();
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }



    [WebMethod(EnableSession = true)]
    public static Allowance_Dataff[] GetAllow_Values_FF()
    {

        Expense nt = new Expense();
        List<Allowance_Dataff> FFD = new List<Allowance_Dataff>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowVal = null;
        //dsAlowVal = nt.get_Allowance_Values(Div_code);
        dsAlowVal = getsfcc("exec getAllowance_Values_Designation '" + Div_code + "'");

        foreach (DataRow row in dsAlowVal.Tables[0].Rows)
        {
            Allowance_Dataff ff = new Allowance_Dataff();
            ff.SF_Code = row["Sf_Code"].ToString();
            ff.Des_code = row["Destination_Code"].ToString();
            ff.Alw_code = row["Allowance_Code"].ToString();
            ff.values = row["Allowance_Value"].ToString();
            if (row["Effective_Date"] != DBNull.Value)
            {
                ff.EffDt = Convert.ToDateTime(row["Effective_Date"]).ToString("yyyy-MM-dd");
            }
            else
            {
                ff.EffDt = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            FFD.Add(ff);
        }
        return FFD.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public static Allowance_Data[] GetAllow_Values()
    {

        Expense nt = new Expense();
        List<Allowance_Data> FFD = new List<Allowance_Data>();
        string Div_code = HttpContext.Current.Session["div_code"].ToString();
        string Sf_Code = HttpContext.Current.Session["Sf_Code"].ToString();

        DataSet dsAlowVal = null;
        //dsAlowVal = nt.get_Allowance_Values(Div_code);
        dsAlowVal = getsfcc("exec getAllowance_Designation '" + Div_code + "'");

        foreach (DataRow row in dsAlowVal.Tables[0].Rows)
        {
            Allowance_Data ffd = new Allowance_Data();
            //ffd.SF_Code = row["Sf_Code"].ToString();
            ffd.Des_code = row["Destination_Code"].ToString();
            ffd.Alw_code = row["Allowance_Code"].ToString();
            ffd.values = row["Allowance_Value"].ToString();
            if (row["Effective_Date"] != DBNull.Value)
            {
                ffd.EffDt = Convert.ToDateTime(row["Effective_Date"]).ToString("yyyy-MM-dd");
            }
            else
            {
                ffd.EffDt = System.DateTime.Now.ToString("yyyy-MM-dd");
            }
            FFD.Add(ffd);
        }
        return FFD.ToArray();
    }

    public class Allowance_Data
    {        
        public string values { get; set; }
        public string Des_code { get; set; }
        public string Alw_code { get; set; }
        public string EffDt { get; set; }
    }
    public class Allowance_Dataff
    {
        public string SF_Code { get; set; }
        public string values { get; set; }
        public string Des_code { get; set; }
        public string Alw_code { get; set; }
        public string EffDt { get; set; }
    }

    [WebMethod]
    public static string savedata(string data,string FieldforceData, string date)
    {
        MasterFiles_Allowance_Entry_New mle = new MasterFiles_Allowance_Entry_New();
        return mle.save(data, FieldforceData, date);
    }
    private string save(string data,string FieldforceData, string date)
    {


        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            Division_code = Session["div_code"].ToString();
        }
        else
        {
            Division_code = Session["div_code"].ToString();
        }

	DataTable items = JsonConvert.DeserializeObject<DataTable>(data);
        var itemsFF = JsonConvert.DeserializeObject<List<Allowance_Dataff>>(FieldforceData);
        int co = 0;
        Expense nt = new Expense();        
        string item = "<ROOT>";
        for (int i = 0; i < items.Rows.Count; i++)
        {
            if ((items.Rows[i]["Alw_code"] != null || items.Rows[i]["Alw_code"].ToString() != ""))//items[i].values != string.Empty && 
            {
                item += "<ASSD Div=\"" + Division_code.TrimEnd(',').ToString() + "\"  DesigCode=\"" + items.Rows[i]["Des_code"].ToString() + "\" AlwCode=\"" + items.Rows[i]["Alw_code"].ToString() + "\" Value=\"" + items.Rows[i]["values"].ToString() + "\"/>";               
            }
        }
        item += "</ROOT>";
        string item_FF = "<ROOT>";
        for (int i = 0; i < itemsFF.Count; i++)
        {
            if (itemsFF[i].values != string.Empty && (itemsFF[i].Alw_code != null || itemsFF[i].Alw_code != ""))
            {
                item_FF += "<ASSD Div=\"" + Division_code.TrimEnd(',').ToString() + "\" SFCode=\"" + itemsFF[i].SF_Code.ToString() + "\" AlwCode=\"" + itemsFF[i].Alw_code.ToString() + "\" DesigCode=\"" + itemsFF[i].Des_code.ToString() + "\" Value=\"" + itemsFF[i].values.ToString() + "\"/>";
            }
        }
        item_FF += "</ROOT>";
        //int iReturn = nt.Insert_Allowance_Data(Division_code.TrimEnd(',').ToString(), items[i].SF_Code.ToString(), items[i].Alw_code.ToString(), items[i].Des_code.ToString(), items[i].values.ToString());
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        string strQry = "EXEC Insert_Designation_Allowance_Values '"+ item + "','"+ item_FF + "','" + date + "','" + Division_code.TrimEnd(',').ToString() + "'";
        SqlCommand cmd = new SqlCommand(strQry, con);
        co=cmd.ExecuteNonQuery();
        con.Close();
        if (co > 0)
        {
            return "Sucess";
        }
        else
        {
            return "Error";
        }
    }
    public static DataSet getsfcc(string qrystring)
    {

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSF = null;

        string strQry = qrystring;

        try
        {
            dsSF = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSF;
    }
    public class alw
    {
        public DataSet getSalesForce_Fare(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "SELECT s.sf_code,s.sf_name+' - '+d.Designation_Short_Name sf_name ,isnull(convert(varchar,Sf_Joining_Date,103),'')JDT,isnull(sf_emp_id,'')sf_emp_id,d.designation_name ,s.sf_HQ ,d.Designation_Code" +
                      " FROM mas_salesforce s inner join Mas_SF_Designation d on d.Designation_code=s.designation_code " +

                      " where s.Division_Code like '" + div_code + ",' and s.sf_status=0 order by 2";


            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
    }
}