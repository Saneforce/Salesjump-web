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

public partial class Stockist_New_Distributor_creation : System.Web.UI.Page
{
    #region "Declaration"
    public  string stockist_code = string.Empty;
    public string sf_type = string.Empty;
    string Div_Code = string.Empty;
    public static bool transfer = false;
    public static string Field_Code = string.Empty;
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
        else if (sf_type == "5")
        {
            this.MasterPageFile = "~/Master_SS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Div_Code = Convert.ToString(Session["div_code"]);
        stockist_code = Request.QueryString["stockist_code"];
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
    public static string getTerritory(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = getTer_Name(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }

    public static DataSet getTer_Name(string Div_Code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = "select Territory_code,Territory_name from Mas_Territory where Div_Code = '" + Div_Code + "'and Territory_Active_Flag=0 order by 2";

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
    public static string getFOName(string terr, string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = FOName(terr, Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet FOName(string terr, string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        DataSet dsStockist = null;

        string strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where  Division_Code like '" + div_code + ",%' and sf_TP_Active_Flag=0 AND SF_Status!=2 and sf_type!=2 and Territory_Code='" + terr + "' order by 2";

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
    public static string getdist(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = getPool_Name(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getPool_Name(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = " SELECT  Dist_code,Dist_name from Mas_District where Dist_Active_Flag=0 and Div_Code = '" + div_code + "'order by 2";

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
    public static string getTaluk(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = TowngetSubDiv(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet TowngetSubDiv(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsSubDiv = null;


        string strQry = "select a.Town_code,a.Town_name From Mas_Town a WHERE a.Town_Active_Flag=0 and " +
                        " a.Div_Code= '" + divcode + "' order by 2";
        try
        {
            dsSubDiv = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsSubDiv;
    }
    [WebMethod(EnableSession = true)]
    public static string getRatecard(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = getRates(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet getRates(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;
        DataSet dsStockist = null;
        strQry = "select Price_list_Sl_No, Price_list_Name from Mas_Product_Wise_Bulk_rate_head where Division_Code = '" + div_code + "' and Price_Active_flag='0' order by Price_list_Name";
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
    public static string getCatagory(string Div_Code)
    {

        DataSet ds = new DataSet();
        ds = FetchCatagory(Div_Code);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
    public static DataSet FetchCatagory(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();


        DataSet dsTerr = null;



        string strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                        " FROM  Mas_Doctor_Category  where division_Code = '" + div_code + "' AND Doc_Cat_Active_Flag=0 ";
        try
        {
            dsTerr = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsTerr;
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
        string strQry = "select upper(Division_SName+'S')Division_SName,(select cast(COUNT(*)as varchar) from Mas_Stockist where charindex(','+CAST(" + divcode + " as varchar)+',',','+Division_Code+',')>0)uname,LEN(Division_SName)ulen from Mas_Division where division_code=" + divcode + "";
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
    public static string submit_distributor(string Div_Code, string Stock_code, string userentry,string stockist_code, string erp_code, string ContactPerson, string Stockist_Name, string sfusrname,
        string sfterr, string sfterrname, string sfDistrict, string sfDistrictname, string sfddlTaluk, string sfddlTalukname, string sfType, string sfRate,
        string sfAddress, string sfemail, string sfDesignation, string sfMobile, int sfNorm, string sfpwd, string ddlField, string ddlFieldname, string sfHead,
        string sfCategory, string sfGSTN, string stock, string sub_div)
    {
        
        string result = "";
        if (stockist_code == ""||stockist_code=="null")
        {
            string stkname = "";
            DataSet ds = new DataSet();
            if (sfusrname != "")
                stkname = chkRecord(Div_Code, stockist_code, sfusrname,"create");
            else
                stkname = "";

            int iReturn = -1;
            if (stkname == "")
            {
                iReturn = RecordAdd(Div_Code, userentry, Stockist_Name, sfAddress, ContactPerson, sfDesignation, sfMobile, erp_code, sfDistrictname, sfDistrict,
                          sfterr, sfterrname, sfusrname, sfpwd, sub_div, sfNorm, ddlFieldname, ddlField, sfHead, sfType, sfGSTN,
                          sfCategory, sfemail, sfddlTalukname, sfddlTaluk, sfRate);
            }
            else
            {
                iReturn = -2;

            }
            if (iReturn > 0)
                result= "Created_Successfully";
            else if (iReturn == -2)
                result= "Already username Exists";
            else
                result= "something went wrong";
        }
        else
        {
            string stkname = "";
            int iReturn = 0;
            if (sfusrname != "")
                stkname = chkRecord(Div_Code, stockist_code, sfusrname,"edit");
            else
                stkname = "";
            if (stkname == "")
            {
                iReturn = RecordUpdate(Div_Code, stockist_code, userentry, Stockist_Name, sfAddress, ContactPerson, sfDesignation,
                sfMobile, erp_code, sfDistrictname, sfDistrict, sfterr, sfterrname, sfusrname, sfpwd, sfNorm, ddlFieldname, ddlField, sub_div,
                sfHead, sfType, sfGSTN, sfCategory, sfemail, sfddlTalukname, sfddlTaluk, sfRate);
            }
            else
            {
                iReturn = -2;

            }
            if (Div_Code == "52" || Div_Code == "100")
                {
                    if (transfer == true)
                    {
                        string[] myarr = Field_Code.TrimEnd(',').Split(',');
                        for (int i = 0; i < myarr.Length; i++)
                        {
                            int iret = updateMappedDistSF(sfterr, myarr[i], stockist_code);
                        }
                    }
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

    public static string chkRecord(string divcode, string SF_Name, string Username ,string type)
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
            if(type=="edit")
            {
                strQry = "select Stockist_Code from mas_stockist where Division_Code='" + divcode + "'  and Username ='" + Username + "' and Stockist_Code <> '"+ SF_Name + "'";
            }
            else
            {
                strQry = "select Stockist_Code from mas_stockist where Division_Code='" + divcode + "'  and Username ='" + Username + "'";

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

    public static int RecordAdd(string divcode,string SF_Name, string stockist_name, string stockist_Address, string stockist_ContactPerson,
        string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code,
        string Territory_Name, string Username, string Password, string sub_division, int norm, string Fo_Name, string Fo_Code, string head_quaters,
        string type, string gstnNo, string dis_cat_code, string stkemail, string Taluk_Name, string Taluk_code, string Rate)
    {
        int iReturn = -1;
        //if (!NRecordExist(Territor_Code, stockist_name, divcode))
        //{
        try
        {
            int stockist_code = 0;
            string STATE_CD = "";
            DataSet dsstcode = null;
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;

            strQry = "SELECT CASE WHEN COUNT(Distributor_Code)>0 THEN MAX(Distributor_Code) ELSE 0 END FROM mas_stockist";
            stockist_code = db.Exec_Scalar(strQry);
            stockist_code += 1;
            // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
            strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
            // STATE_CD = db.Exec_Scalar(strQry).ToString();

            dsstcode = db.Exec_DataSet(strQry);
            if (dsstcode.Tables[0].Rows.Count > 1)
            {
                return iReturn;
            }
            else
            {
                foreach (DataRow row in dsstcode.Tables[0].Rows)
                {
                    STATE_CD += row["State_code"].ToString() + ",";
                }
                STATE_CD = STATE_CD.TrimEnd(',');
            }



            strQry = " INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory, Created_Date,ERP_Code,Dist_Name,Dist_Code,Username,Password,Territory_Code,Distributor_Code,subdivision_code,Norm_Val,Field_Name,Field_Code,State_Code,User_Entry_Code,Head_Quaters,Type,gstn,Dis_Cat_Code,Stockist_Address1,Taluk_code,Taluk_Name,Price_List_Name) " +
                     " values('" + divcode + "', '" + stockist_code + "', '" + stockist_code + "','" + stockist_name + "', '" + stockist_Address + "', '" + stockist_ContactPerson + "', '" + stockist_Designation + "', '0' ,'" + stockist_mobilno + "','" + Territory_Name + "',getdate(),'" + ERP_Code + "','" + Town_Name + "','" + Town_code + "','" + Username + "','" + Password + "','" + Territor_Code + "','" + stockist_code + "','" + sub_division + "','" + norm + "','" + Fo_Name + "','" + Fo_Code + "','" + STATE_CD + "','" + SF_Name + "','" + head_quaters + "','" + type + "','" + gstnNo + "','" + dis_cat_code + "','" + stkemail + "','" + Taluk_code + "','" + Taluk_Name + "','" + Rate + "')";

            iReturn = db.ExecQry(strQry);
            if (iReturn > 0)
            {
                iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
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
    public static int RecordUpdate(string divcode, string stockist_code, string Stock_code, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code, string Territory_Name, string Username, string Password, int norm, string Fo_Name, string Fo_Code, string sub_division, string headquarters, string type, string gstnNo, string Dis_Cat_Code, string stkemail, string Taluk_Name, string Taluk_code, string Rate)
    {
        int iReturn = -1;
        try
        {
            string STATE_CD = "";
            DataSet dsstcode = null;
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;

            // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
            strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
            // STATE_CD = db.Exec_Scalar(strQry).ToString();

            dsstcode = db.Exec_DataSet(strQry);
            if (dsstcode.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsstcode.Tables[0].Rows)
                {
                    STATE_CD += row["State_code"].ToString() + ",";
                }
                STATE_CD = STATE_CD.TrimEnd(',');
            }
            else
            {
                return iReturn;
            }

            strQry = "UPDATE mas_stockist " +
                     " SET stockist_name = '" + stockist_name + "' , " +
                     " User_Entry_Code = '" + Stock_code + "' , " +
                     " stockist_Address = '" + stockist_Address + "', " +
                     " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                     " stockist_Designation = '" + stockist_Designation + "' , " +
                     " stockist_mobile = '" + stockist_mobilno + "' , " +
                     " Head_Quaters = '" + headquarters + "'  ,   Type = '" + type + "' ," +
                     " Territory = '" + Territory_Name + "', ERP_Code = '" + ERP_Code + "', " +
                     " Dist_Name = '" + Town_Name + "' ,Dist_Code = '" + Town_code + "', " +
                     " Username = '" + Username + "' ,Password = '" + Password + "', " +
                     " Norm_Val ='" + norm + "', " +
                     " Field_Name = '" + Fo_Name + "' ,Field_Code = '" + Fo_Code + "', " +
                     " Territory_Code = '" + Territor_Code + "' , " +
                     " subdivision_code ='" + sub_division + "'," +
                     " State_Code='" + STATE_CD + "'," +
                     " gstn='" + gstnNo + "',Stockist_Address1='" + stkemail + "'," +


                      " Dis_Cat_Code='" + Dis_Cat_Code + "'," +
                      " Taluk_code='" + Taluk_code + "'," +
                      " Taluk_Name='" + Taluk_Name + "'," +
                      " Price_list_Name='" + Rate + "'," +

                     " LastUpdt_Date = getdate() " +
                     " WHERE stockist_code = '" + stockist_code + "' AND Division_Code = '" + divcode + "' ";
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
        strQry = "SELECT User_Entry_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile,SF_Code,ERP_Code,Dist_Code,Username,Password,Territory_Code,subdivision_code,Norm_Val,(select sf_name +',' from Mas_Salesforce s where charindex(','+ s.sf_code +',' ,','+a.field_code +',')>0  for xml path(''))  Field_Name,Field_Code,Head_Quaters,replace(iif(isnull(type,'Stockist')='','Stockist',type),'--Select--','Stockist') Type,gstn,Dis_Cat_Code,isnull(Stockist_Address1,'')Stockist_Email,Price_list_Name" +
                 " FROM mas_stockist a" +
                 " WHERE stockist_active_flag=0 " +
                 " AND Division_Code= '" + divcode + "' " +
                 " AND stockist_code = '" + stockist_code + "'  ";

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
    public static int updateMappedDistSF(string TerritoryCode, string FieldCode, string stkcode)
    {
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        try
        {
            string strQry = "update Mas_territory_creation set Sf_Code=Sf_Code+'," + FieldCode.TrimEnd(',') + "' where Territory_Sname=" + TerritoryCode + " and CHARINDEX('," + stkcode + ",',','+Dist_Name+',')>0 and CHARINDEX('," + FieldCode.TrimEnd(',') + ",',','+SF_Code+',')<1";
            iReturn = db.ExecQry(strQry);
            strQry = "update Mas_ListedDr set Sf_Code=Sf_Code+'," + FieldCode.TrimEnd(',') + "' where TerrCode='" + TerritoryCode + "' and CHARINDEX('," + stkcode + ",',','+Dist_Name+',')>0 and CHARINDEX('," + FieldCode.TrimEnd(',') + ",',','+SF_Code+',')<1";
            iReturn = db.ExecQry(strQry);

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return iReturn;
    }

    [WebMethod(EnableSession = true)]
    public static string getdistnum(string Div_Code)
    {
        DataSet dsStockist = new DataSet();
        dsStockist=getCheck(Div_Code);
        return  JsonConvert.SerializeObject(dsStockist.Tables[0]);
    }
    public static DataSet getCheck(string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = "select isnull(MAX (cast ( Stockist_Code as numeric)), 0) as Num from Mas_Stockist where Stockist_Active_Flag=0 and isnumeric(Stockist_Code)>0";
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