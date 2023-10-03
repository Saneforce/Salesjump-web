using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Distributor_Prirate_fixation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    public static DB_EReporting  db_ER = new DB_EReporting();
    [WebMethod]
    public static string save_ratedetails(string div_code,string sfcode, string dt,string proddetails)
    {

        DataSet ds = new DataSet();
        var items = JsonConvert.DeserializeObject<List<prod_details>>(proddetails);
        string sxml = "<ROOT>";
        for (int k = 0; k < items.Count; k++)
        {
            if (items[k].Distcode != "" && items[k].prodcode != "" && items[k].mrp != "0" && items[k].pts != "0")
            {
                sxml += "<Prod Distcode=\"" + items[k].Distcode + "\" prodcode=\"" + items[k].prodcode + "\" mrp=\"" + items[k].mrp + "\" pts=\"" + items[k].pts + "\"  />";
            }
        }
        sxml += "</ROOT>";
        string strQry = "exec sp_save_Pri_DistRate '" + div_code + "','" + sfcode + "','" + dt + "','" + sxml + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }


    [WebMethod]
    public static string getDistRate(string sfcode)
    {
        
        DataSet ds = new DataSet();
        string strQry = "exec sp_get_distRate '" + sfcode + "'";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return JsonConvert.SerializeObject(ds.Tables[0]);

    }

    [WebMethod]
    public static Distributordata[] getdata()
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //DistributorSetup adm = new DistributorSetup();
        List<Distributordata> DistList = new List<Distributordata>();

        //using (DataSet dsEmployee = adm.GetDistData(div_code.TrimEnd(','), sf_code))
        using (DataSet dsEmployee = GetDistData(div_code.TrimEnd(',')))
        {
            foreach (DataRow row in dsEmployee.Tables[0].Rows)
            {

                Distributordata DisList = new Distributordata
                {
                    Distcode = row["Stockist_Code"].ToString(),
                    Distname = row["Stockist_Name"].ToString()
                };
                DistList.Add(DisList);
            }
        }

        return DistList.ToArray();
    }
    public static DataSet GetDistData(string div_code)
    {
       
        DataSet dsDist = null;
        string strQry = "select MS.Stockist_Code,MS.Stockist_Name from  Mas_Stockist MS where MS.Division_Code='" + div_code + "' and  Stockist_Active_Flag=0";
        //strQry = "SELECT Stockist_Code,Stockist_Name,Price_type FROM Mas_Stockist WHERE  Stockist_Active_Flag=0 and Division_Code=c ";
        try
        {
            dsDist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsDist;
    }
    public class Distributordata
    {

        public string Distcode { get; set; }
        public string Distname { get; set; }

    }
    public class prod_details
    {
        public string Distcode { get; set; }
        public string prodcode { get; set; }
        public string mrp { get; set; }
        public string pts { get; set; }
       
    }
}