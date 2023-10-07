using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_New_SupplierCreation : System.Web.UI.Page
{
    public string sf_dept = string.Empty;
    public string sf_code = string.Empty;
    public string sf_status = string.Empty;
    public string stockist_code = string.Empty;
    public static string divcode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
         sf_code = Session["sf_code"].ToString();
         sf_dept = "";
         sf_status = "";
        stockist_code = Request.QueryString["sup_code"];
        divcode = Convert.ToString(Session["div_code"]);
    }
    [WebMethod(EnableSession = true)]
    public static string getstate()
    {

        DataSet ds = new DataSet();
        ds = get_state();
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet get_state()
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = "select State_Code,StateName+' ( '+ShortName+' )' as statename from Mas_State where State_Active_Flag='0' ";

        try
        {
            dsStockist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsStockist;
    }
    [WebMethod(EnableSession = true)]
    public static string getFOName( string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = FOName(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet FOName(string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsStockist = null;
        string sub = "";
        string sf = "";
        string strQry = "Exec HyrSFList_All '" + Div_Code + "','" + sub+"','"+ sf + "'";

        try
        {
            dsStockist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsStockist;

    }
    [WebMethod(EnableSession = true)]
    public static string getusername(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = getDivisname(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getDivisname(string divcode)
    {
        DB_EReporting db = new DB_EReporting();
        DataSet ds = null;
        string strQry = "select upper(Division_SName+'SS')Division_SName,(select cast(COUNT(*)as varchar) from Supplier_Master where charindex(','+CAST(" + divcode + " as varchar)+',',','+Division_Code+',')>0)+1 uname,LEN(Division_SName)ulen from Mas_Division where division_code=" + divcode + "";
        try
        {
            ds = db.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string getdivision(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = getsubdivision(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getsubdivision(string Div_Code)
    {
        DataSet ds = null;
        DB_EReporting db_ER = new DB_EReporting();

        string strQry = "SELECT subdivision_code,a.subdivision_sname,a.subdivision_name, " +
                        " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + Div_Code + "' and d.Product_Active_Flag=0) Sub_Count" +
                        ",(select count(e.subdivision_code) from Mas_Salesforce e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + Div_Code + ",' and e.SF_Status=0) SubField_Count" +
                        " FROM mas_subdivision a WHERE a.subdivision_active_flag=0 And a.Div_Code= '" + Div_Code + "'" +
                        " ORDER BY 2";
        try
        {
            ds = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }
    [WebMethod(EnableSession = true)]
    public static string save_supplier(string Div_Code, string stockist_code ,string stockist_name, string contant_person, string mobile, string erp_code, string username, string pass, string field_name,
        string field_code, string state_code, string state_name, string Gst,string address,string sub_div ,string dis_type)
    {
        string result = "";
        if (stockist_code == "" || stockist_code == "null")
        {
            string stkname = "";
            DataSet ds = new DataSet();
            if (username != "")
                stkname = chkRecord(Div_Code, stockist_code, username, "create");
            else
                stkname = "";

            int iReturn = -1;
            if (stkname == "")
            {
                iReturn = RecordAdd(Div_Code,  stockist_name,  contant_person,  mobile,  erp_code,  username,  pass,  field_name,
                          field_code,  state_code,  state_name,  Gst,  address,  sub_div,dis_type);
            }
            else
            {
                iReturn = -2;

            }
            if (iReturn > 0)
                result = "Created_Successfully";
            else if (iReturn == -2)
                result = "Already username Exists";
            else
                result = "something went wrong";
        }
        else
        {
            string stkname = "";
            int iReturn = 0;
            if (username != "")
                stkname = chkRecord(Div_Code, stockist_code, username, "edit");
            else
                stkname = "";
            if (stkname == "")
            {
                iReturn = RecordUpdate(Div_Code, stockist_code, stockist_name, contant_person, mobile, erp_code, username, pass, field_name,
                       field_code, state_code, state_name, Gst, address, sub_div,dis_type);
            }
            else
            {
                iReturn = -2;

            }
            
            if (iReturn > 0)
            {
                result = "Updated_Successfully";
            }
            else if (iReturn == -2)
            {
                result = "Already Exist with the Same Distributor Name";
            }
            else if (iReturn == -3)
            {
                result = "Already Exist";
            }


        }
        return result;
    }
    public static string chkRecord(string divcode, string SF_Name, string Username, string type)
    {
        string iReturn = "";
        //if (!NRecordExist(Territor_Code, stockist_name, divcode))
        //{
        try
        {
            string stockist_code = "";
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;
            DataTable dt = new DataTable();
            if (type == "edit")
            {
                strQry = "select S_No from supplier_master  where division_code ='" + divcode + "' and S_No <> '" + SF_Name + "' and (UsrDfd_UserName='" + Username + "' OR ISNULL( UsrDfd_UserName,'')='" + Username + "')";
            }
            else
            {
                strQry = "select S_No from supplier_master  where division_code='" + divcode + "'  and (UsrDfd_UserName='" + Username + "' OR ISNULL( UsrDfd_UserName,'')='" + Username + "') ";

            }
            dt = db.Exec_DataTable(strQry);
            if (dt.Rows.Count > 0)
                stockist_code = db.Exec_DataTable(strQry).Rows[0][0].ToString();
            else
                stockist_code = "";


            if (stockist_code != "")
            {
                iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }

        return iReturn;

    }
    public static int RecordAdd(string Div_Code, string sup_name, string contant_person, string mobile, string erp_code, string username, string pass, string field_name,
                          string field_code, string state_code, string state_name, string  Gst, string address, string sub_div,string dis_type)
    {
        int iReturn = -1;
        //if (!NRecordExist(Territor_Code, stockist_name, divcode))
        //{
        try
        {
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;
            strQry = "exec [insertnew_superstockist] '" + divcode + "','" + sup_name + "','" + contant_person + "','" + mobile + "','" + erp_code + "','" + username + "','" + pass + "','" + field_name + "','" + field_code + "','" + state_code + "','" + Gst + "','" + address + "','" + sub_div + "','"+dis_type+"' ";
            iReturn = db.ExecQry(strQry);
            if (iReturn > 0)
            {
                
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
        //}
        //else
        //{
        //iReturn = -2; 
        //}
        return iReturn;

    }
    public static int RecordUpdate(string divcode, string sup_code, string sup_name, string contant_person, string mobile, string erp_code, string username, string pass, string field_name,
                          string field_code, string state_code, string state_name, string Gst, string address, string sub_div,string dis_type)
    {
        int iReturn = -1;
        try
        {
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;

            
            strQry = "UPDATE supplier_master " +
                     " SET S_Name = '" + sup_name + "' , " +
                     " Contact_Person = '" + contant_person + "', " +
                     " Mobile = '" + mobile + "' , " +
                     " Erp_Code = '" + erp_code + "' , " +
                     " sf_code = '" + field_code + "'  ," +
                     " sf_name = '" + field_name + "', " +
                     " UsrDfd_UserName = '" + username + "' , " +
                     " sf_password = '" + pass + "' , " +
                     " State_Code ='" + state_code + "', " +
                     " GST_NO ='" + Gst + "', " +
                     " subdivision_code = '" + sub_div + "', " +
                     " Addr = '" + address + "' , "+
                     " Type = '" + dis_type + "'  "+
                     "where S_No='"+ sup_code + "'";
            iReturn = db.ExecQry(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }
    [WebMethod(EnableSession = true)]
    public static string getdistdetail(string Div_Code, string stockist_code)
    {

        DataSet ds = new DataSet();
        ds = getStockist_Create(Div_Code, stockist_code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getStockist_Create(string divcode, string stockist_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = string.Empty;
        strQry = "SELECT S_No ,S_Name,Contact_Person,Mobile,ERP_Code,UsrDfd_UserName,sf_password,"+
                "sf_code +'-'+ sf_name Field_Code, sf_name Field_Name,State_Code,division_code,subdivision_code,Addr,GST_NO,isnull(Type,'Stoctkist') Type " +
                " FROM Supplier_Master" +
                " where S_No = '" + stockist_code + "'";// ms.Division_Code = '" + divcode + "' and


        try
        {
            dsStockist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsStockist;
    }
}