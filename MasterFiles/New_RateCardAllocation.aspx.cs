using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_New_RateCardAllocation : System.Web.UI.Page
{
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
    DataSet dsSf = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["div_code"] != null)
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
        }
    }
    [WebMethod]
    public static string getRate(string divcode)
    {
        string strQry = string.Empty;
        DataTable dt = null;

        strQry = "select mh.Price_list_Sl_No,mh.Price_list_Name from Mas_Product_Wise_Bulk_rate_head mh left join Mas_Product_Wise_Bulk_rate_Period mp on mp.Price_list_Sl_No =mh.Price_list_Sl_No   where Division_Code = '" + divcode + "' and convert(date, mp.Effective_To_Date) >= convert(date,getDATE())  order by Price_list_Name ";

        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }

    //public static string getDivision(string divcode)
    [WebMethod]
    public static string getDivision(string divcode, string ratecard)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        strQry = "select sub_div_code,subdivision_name from Mas_Product_Wise_Bulk_rate_head where Division_Code=" + divcode + " and  Price_list_Sl_No =" + ratecard;
        // strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code=" + divcode + " and SubDivision_Active_Flag=0 order by subdivision_name";

        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getState(string subdivcode, string plant_id)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        strQry = "Exec get_state_ratecard'" + subdivcode + "','" + plant_id + "'";
        dt = execQuery(strQry);

        //strQry = "select mst.State_Code,mst.StateName from Mas_State mst inner join mas_stockist ms on mst.State_Code = ms.State_Code where ms.Stockist_Active_Flag = 0 and charindex(',' + cast('"+ subdivcode + "' as varchar) + ',',',' + subdivision_code + ',')> 0  and ms.Plant_id = '"+ plant_id +"'  group by mst.State_Code,mst.StateName order by 2";
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getDistrict(string state_code)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        strQry = "Exec sp_stateWise_District'" + state_code + "','" + div_code + "'";
        dt = execQuery(strQry);

        //strQry = "select mst.State_Code,mst.StateName from Mas_State mst inner join mas_stockist ms on mst.State_Code = ms.State_Code where ms.Stockist_Active_Flag = 0 and charindex(',' + cast('"+ subdivcode + "' as varchar) + ',',',' + subdivision_code + ',')> 0  and ms.Plant_id = '"+ plant_id +"'  group by mst.State_Code,mst.StateName order by 2";
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    //public static string getDistributor(string subdivcode,string terricode,string plant_id)
    public static string getDistributor(string subdivcode, string terricode)
    {
        string strQry = string.Empty;
        DataTable dt = null;

        //strQry = "Exec get_distibutor_ratecard'" + subdivcode + "','" + terricode + "','" + plant_id + "'";
        strQry = "Exec sp_GetDistrict_Wise_Distributor'" + subdivcode + "','" + terricode + "'";


        //strQry = "select Stockist_Code,Stockist_Name,Division_Code,State_Code,Territory_Code,subdivision_code from Mas_Stockist where Stockist_Active_Flag = 0 and charindex(',' + cast('"+ subdivcode + "' as varchar) + ',',',' + subdivision_code + ',')> 0 and Territory_Code in(select * from SplitString('"+ terricode + "',',')) and Plant_id='"+ plant_id + "'  order by Stockist_Name";
        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }

    [WebMethod]
    public static string getcategory()
    {
        string strQry = string.Empty;
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        strQry = "Exec sp_get_ret_category'" + div_code + "'";
        //strQry = "select Plant_id,State_Code,Territory,Territory_Code from Mas_Stockist where charindex(',' + cast('" + subdivcode + "' as varchar) + ',',',' + subdivision_code + ',')> 0 and State_Code in(select * from SplitString('" + state_code + "', ','))  and Plant_id='" + plant_id + "' group by Plant_id,State_Code,Territory,Territory_Code";

        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    public static string chkratecard(string rate,string mode,string type)
    {
        string strQry = string.Empty;
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();
       
        if (type == "distributor")
        {
            if(mode=="1")
                strQry = "Exec getratecarddetails'" + div_code + "','" + rate + "','dist'";
            else
                strQry = "Exec getratecarddetails'" + div_code + "','" + rate + "','distcat'";
        }
        else if (type == "superstockist")
        {
            if (mode == "1")
                strQry = "Exec getratecarddetails'" + div_code + "','" + rate + "','supstk'";
            else
                strQry = "Exec getratecarddetails'" + div_code + "','" + rate + "','supcat'";
        }
        
        dt = execQuery(strQry);

        return JsonConvert.SerializeObject(dt);
    }
    [WebMethod]
    //public static string saveRate(string DivCode,string SubDivCode, string State, string Territory, string Distributor, string Rate,string Mode,string Plant_id,string Splrate)
    public static string saveRate(string DivCode, string SubDivCode, string State, string Distributor, string Rate, string Mode, string Splrate, string category)
    {
        string strQryDist = string.Empty;
        string strQryRet = string.Empty;
        DataTable dtdist = null;
        DataTable dtret = null;

        //strQryDist = "update Mas_Stockist set Price_List_Name =" + Rate + " where Division_Code=" + DivCode + " and charindex(',"+ SubDivCode + ",', ',' + subdivision_code + ',') > 0 and State_Code in(select * from SplitString('" + State + "', ',')) and Territory_Code in(select * from SplitString('" + Territory + "',',')) and Stockist_Code in(select * from SplitString('" + Distributor + "',',')) and Plant_id='" + Plant_id+"'  select 'success'";
        //strQryRet = "update Mas_Listeddr set Price_List_Name =" + Rate + " where Division_Code=" + DivCode + " and State_Code in(select * from SplitString('" + State + "',',')) and Territory_Code in(select * from SplitString('" + Territory + "',',')) and Dist_name in(select * from SplitString('" + Distributor + "',',')) select 'success'";

        strQryRet = "update Mas_Listeddr set Price_List_Name =" + Rate + " where Division_Code=" + DivCode + " and State_Code in(select * from SplitString('" + State + "',',')) and Dist_name in(select * from SplitString('" + Distributor + "',','))  and Doc_Cat_Code in(select * from SplitString('" + category + "',',')) select 'success'";

        try
        {
            if (Mode == "1")
            {
                Splrate = "true";
                category = "";
                //strQryDist = "Exec save_rate_allocation'" + DivCode + "','" + SubDivCode + "','" + State + "','" + Territory + "','" + Distributor + "','" + Plant_id + "','" + Rate + "','" + Splrate + "'";
                strQryDist = "Exec save_rate_allocation'" + DivCode + "','" + SubDivCode + "','" + State + "','" + Distributor + "','" + Rate + "','" + Splrate + "','" + category + "'";
                dtdist = execQuery(strQryDist);
            }
            else if (Mode == "2")
            {
                Splrate = "false";
                strQryDist = "Exec save_rate_allocation'" + DivCode + "','" + SubDivCode + "','" + State + "','" + Distributor + "','" + Rate + "','" + Splrate + "','" + category + "'";
                 dtret = execQuery(strQryDist);
            }
            else if (Mode == "3")
            {  //update ratecard number in suplier master table price_list_s_no column 
                strQryRet = "update Supplier_Master set Price_list_Sl_No =" + Rate + " where Division_Code=" + DivCode + " and S_No in(select * from SplitString('" + Distributor + "',','))  select 'success'";
                dtret = execQuery(strQryRet);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ("success");
    }

    [WebMethod]
    //public static string saveRate(string DivCode,string SubDivCode, string State, string Territory, string Distributor, string Rate,string Mode,string Plant_id,string Splrate)
    public static string ssaveRate(string DivCode, string SubDivCode, string State, string sstockist, string Rate, string Mode, string Splrate, string category)
    {
        string strQryDist = string.Empty;
        string strQryRet = string.Empty;
        DataTable dtdist = null;
        DataTable dtret = null;

        //strQryDist = "update Mas_Stockist set Price_List_Name =" + Rate + " where Division_Code=" + DivCode + " and charindex(',"+ SubDivCode + ",', ',' + subdivision_code + ',') > 0 and State_Code in(select * from SplitString('" + State + "', ',')) and Territory_Code in(select * from SplitString('" + Territory + "',',')) and Stockist_Code in(select * from SplitString('" + Distributor + "',',')) and Plant_id='" + Plant_id+"'  select 'success'";
        //strQryRet = "update Mas_Listeddr set Price_List_Name =" + Rate + " where Division_Code=" + DivCode + " and State_Code in(select * from SplitString('" + State + "',',')) and Territory_Code in(select * from SplitString('" + Territory + "',',')) and Dist_name in(select * from SplitString('" + Distributor + "',',')) select 'success'";

        try
        {
            if (Mode == "1")
            {
                Splrate = "true";
                category = "";
                //strQryDist = "Exec save_rate_allocation'" + DivCode + "','" + SubDivCode + "','" + State + "','" + Territory + "','" + Distributor + "','" + Plant_id + "','" + Rate + "','" + Splrate + "'";
                strQryDist = "Exec [save_superstk_rate_allocation]'" + DivCode + "','" + SubDivCode + "','" + State + "','" + sstockist + "','" + Rate + "','" + Splrate + "','" + category + "'";
                dtdist = execQuery(strQryDist);
            }
            else if (Mode == "2")
            {
                Splrate = "false";
                strQryDist = "Exec [save_superstk_rate_allocation]'" + DivCode + "','" + SubDivCode + "','" + State + "','" + sstockist + "','" + Rate + "','" + Splrate + "','" + category + "'";
                dtret = execQuery(strQryDist);
            }
            else if (Mode == "3")
            {  //update ratecard number in suplier master table price_list_s_no column 
                strQryRet = "update Supplier_Master set Price_list_Sl_No =" + Rate + " where Division_Code=" + DivCode + " and S_No in(select * from SplitString('" + sstockist + "',','))  select 'success'";
                dtret = execQuery(strQryRet);
            }
            //if (Mode == "1")
            //{
            //    strQryRet = "update Supplier_Master set Price_list_Sl_No =" + Rate + " where Division_Code=" + DivCode + " and S_No in(select * from SplitString('" + sstockist + "',','))  select 'success'";
            //    dtret = execQuery(strQryRet);
            //}
            //else if (Mode == "2")
            //{
            //    Splrate = "false";
            //    strQryDist = "Exec save_rate_allocation'" + DivCode + "','" + SubDivCode + "','" + State + "','" + Distributor + "','" + Rate + "','" + Splrate + "','" + category + "'";
            //    dtret = execQuery(strQryDist);
            //}
            //else if (Mode == "3")
            //{  //update ratecard number in suplier master table price_list_s_no column 
            //    strQryRet = "update Supplier_Master set Price_list_Sl_No =" + Rate + " where Division_Code=" + DivCode + " and S_No in(select * from SplitString('" + Distributor + "',','))  select 'success'";
            //    dtret = execQuery(strQryRet);
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ("success");
    }
    [WebMethod]
    public static string get_SS(string state_code, string subdivcode)
    {
        string strQry = string.Empty;
        DataTable dt = null;

        //strQry = "Exec get_distibutor_ratecard'" + subdivcode + "','" + terricode + "','" + plant_id + "'";
        strQry = "Exec sp_SS_GetDistrict_Wise_Distributor '" + state_code + "','" + subdivcode + "','" + div_code + "'";


        //strQry = "select Stockist_Code,Stockist_Name,Division_Code,State_Code,Territory_Code,subdivision_code from Mas_Stockist where Stockist_Active_Flag = 0 and charindex(',' + cast('"+ subdivcode + "' as varchar) + ',',',' + subdivision_code + ',')> 0 and Territory_Code in(select * from SplitString('"+ terricode + "',',')) and Plant_id='"+ plant_id + "'  order by Stockist_Name";
        dt = execQuery(strQry);
        return JsonConvert.SerializeObject(dt);
    }
    public static DataTable execQuery(string strQry)
    {
        DataTable dt = null;
        DBase_EReport.DB_EReporting db_ER = new DBase_EReport.DB_EReporting();

        try
        {
            dt = db_ER.Exec_DataTable(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}