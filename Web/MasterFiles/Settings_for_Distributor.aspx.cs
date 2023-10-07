using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;

public partial class MasterFiles_Settings_for_Distributor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    public static string updatedata( string distcode, string pricetype)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        //DistributorSetup adm = new DistributorSetup();

        Distributordata DisList = new Distributordata
        {
            Distcode = distcode,
            Pricetype = int.Parse(pricetype),
            div_code = div_code
        };

        //using (DataSet dsEmployee = adm.UpdateDistData(DisList.div_code, DisList.Distcode, DisList.Pricetype.ToString()))
        //{
        //    return dsEmployee.Tables[0].Rows[0]["result"].ToString();
        //}
        using (DataSet dsEmployee = UpdateDistData(DisList.div_code, DisList.Distcode, DisList.Pricetype.ToString()))
        {
            return dsEmployee.Tables[0].Rows[0]["result"].ToString();
        }

    }
    public static DataSet UpdateDistData(string div_code, string distcode, string pricetype)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDist = null;
        string strQry = "Exec SP_UpdateDist_Rate '" + div_code + "' , '" + distcode + "','" + pricetype + "'";
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
    [WebMethod]
    public static Distributordata[] getdata(string div_code, string sf_code)
    {
        //DistributorSetup adm = new DistributorSetup();
        List<Distributordata> DistList = new List<Distributordata>();

        //using (DataSet dsEmployee = adm.GetDistData(div_code.TrimEnd(','), sf_code))
        using (DataSet dsEmployee = GetDistData(div_code.TrimEnd(','), sf_code))
        {
            foreach (DataRow row in dsEmployee.Tables[0].Rows)
            {

                Distributordata DisList = new Distributordata
                {
                    Distcode = row["Stockist_Code"].ToString(),
                    Pricetype = int.Parse(row["price_type"].ToString()),
                    Distname = row["Stockist_Name"].ToString()
                };
                DistList.Add(DisList);
            }
        }

        return DistList.ToArray();
    }
    public  static DataSet GetDistData(string div_code, string sfcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDist = null;
        string strQry = "select MS.Stockist_Code,MS.Stockist_Name,isnull(MR.price_type,5) as price_type from  Mas_Stockist MS left join Mas_Retailer_Rate MR on MR.Div_code = MS.Division_Code  and MS.Stockist_Code =MR.Dist_Code where Division_Code='" + div_code + "' and  Stockist_Active_Flag=0";
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
        public string div_code { get; set; }
        public string Distcode { get; set; }
        public string Distname { get; set; }
        public int Pricetype { get; set; }
    }

}