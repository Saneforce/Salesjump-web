using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using DBase_EReport;

/// <summary>
/// Summary description for lisdrdetails
/// </summary>
public class Listdrdetails
{
   
    public DataTable GetCustomFieldsDetailsdetails(string div_code)
    {

        DataTable dsAdmin = new DataTable();

        string strQry = " SELECT Field_Col,CF.FieldGroupId,CF.FGTableName,MFG.FGroupName FROM Trans_Custom_Fields_Details CF (NOLOCK) ";
        strQry += " INNER JOIN Mas_FieldGroupTable MFG (NOLOCK) ON CF.FieldGroupId = MFG.FieldGroupId ";
        strQry += " WHERE CF.ModuleId=3 AND Div_code=@Division_Code ";

        try
        {
            using (var con = new SqlConnection(Globals.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        return dsAdmin;
    }

    string strQry = string.Empty;

    public DataSet ViewListedDr(string drcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsListedDR = new DataSet();

        string strQry = " EXEC Get_Retailer_Details '" + drcode + "' ";

        //string strQry = " SELECT d.ListedDrCode,d.Code,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.ListedDr_Name,d.contactperson Contact_Person_Name,d.Doc_Special_Code,d.Doc_Spec_ShortName, " +
        //        " d.Tin_No,d.Sales_Taxno,d.Territory_Code,d.Credit_Days,d.Doc_ClsCode,d.Doc_Class_ShortName,d.Advance_amount,d.Milk_Potential,d.UOM,d.UOM_Name,  " +
        //        " d.ListedDr_Address1,d.ListedDr_Address2,Retailer_Type,stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
        //        " and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name ,Outstanding,Credit_Limit,Cus_Alter,d.Doc_Cat_Code,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,ListedDr_Email" +
        //        " FROM  Mas_ListedDr d WHERE d.ListedDrCode =  '" + drcode + "'  and d.ListedDr_Active_Flag = 0 ";


        try
        {
            dsListedDR = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsListedDR;
    }

    public int RecordAdd11(string DR_Name, string curentCompitat, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode, string latitude, string longitude, string DFDairyMP, string MonthlyAI, string MCCNFPM, string MCCMilkColDaily, string FrequencyOfVisit, string Breed, string curentCom, string txtmail)
    {
        int iReturn = -1;
        //int jReturn = -1;
        //int Listed_DR_Code;
        if (!sRecordExist(retail_code, DR_Name, Div_code))
        {
            if (!RecordExist(DR_Name, retail_code, Div_code))
            {

                if (!ERPRecordExist(erbCode, Div_code, retail_code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        string Division_Code = "-1";
                        //Listed_DR_Code = -1;

                        //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                        //Listed_DR_Code = db.Exec_Scalar(strQry);

                        strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                        DataSet ds = db.Exec_DataSet(strQry);

                        string sfcode = "";
                        string distname = "";
                        foreach (DataRow dd in ds.Tables[0].Rows)
                        {
                            sfcode = dd["sf_code"].ToString();
                            distname = dd["Dist_Name"].ToString();
                            Division_Code = dd["Division_Code"].ToString();
                        }
                        strQry = "select   'EK" + sfcode + "-'+  replace(convert(varchar, getdate(),101),'/','') + replace(convert(varchar, getdate(),108),':','') as ukey ";
                        string UKey = db.Exec_Scalar_s(strQry).ToString();


                        if ((DR_Name == null || DR_Name == ""))
                        { DR_Name = ""; }

                        if ((Milk_pot == null || Milk_pot == ""))
                        { Milk_pot = ""; }

                        if ((credit_days == null || credit_days == ""))
                        { credit_days = ""; }

                        if ((Mobile_No == null || Mobile_No == ""))
                        { Mobile_No = ""; }

                        if ((erbCode == null || erbCode == ""))
                        { erbCode = ""; }

                        if ((advance_amount == null || advance_amount == ""))
                        { advance_amount = ""; }

                        if ((sales_Tax == null || sales_Tax == ""))
                        { sales_Tax = ""; }

                        if ((Tinno == null || Tinno == ""))
                        { Tinno = ""; }

                        if ((Uom == null || Uom == ""))
                        { Uom = ""; }

                        if ((Uom_Name == null || Uom_Name == ""))
                        { Uom_Name = ""; }

                        if ((DR_Class == null || DR_Class == ""))
                        { DR_Class = "0"; }


                        if ((drcategory == null || drcategory == ""))
                        { drcategory = "0"; }


                        SqlParameter[] parameters = new SqlParameter[]
                        {
                                new SqlParameter("@UKey", Convert.ToString(UKey)),
                                new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                                new SqlParameter("@DFDairyMP", Convert.ToString(DFDairyMP)),
                                new SqlParameter("@MonthlyAI", Convert.ToString(MonthlyAI)),
                                new SqlParameter("@curentCompitat", Convert.ToString(curentCompitat)),
                                new SqlParameter("@MCCNFPM", Convert.ToString(curentCompitat)),
                                new SqlParameter("@MCCMilkColDaily", Convert.ToString(MCCMilkColDaily)),
                                new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                                new SqlParameter("@Milk_pot", Convert.ToString(Milk_pot)),
                                new SqlParameter("@curentCom", Convert.ToString(curentCom)),
                                new SqlParameter("@FrequencyOfVisit", Convert.ToString(FrequencyOfVisit)),
                                new SqlParameter("@sfcode", Convert.ToString(sfcode)),
                                new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                                new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                                new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                                new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                                new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                                new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                                new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                                new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                                new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                                new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                                new SqlParameter("@ad", Convert.ToString(ad)),
                                new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                                new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                                new SqlParameter("@Division_Code", Convert.ToString(Division_Code)),
                                new SqlParameter("@Uom", Convert.ToString(Uom)),
                                new SqlParameter("@Uom_Name", Convert.ToString(Uom_Name)),
                                new SqlParameter("@sf_code", Convert.ToString(sf_code)),
                                new SqlParameter("@distname", Convert.ToString(distname)),
                                new SqlParameter("@re_type", Convert.ToString(re_type)),
                                new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                                new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                                new SqlParameter("@Cus_Alt", Convert.ToString(Cus_Alt)),
                                new SqlParameter("@drcategory", Convert.ToString(drcategory)),
                                new SqlParameter("@drcategoryName", Convert.ToString(drcategoryName)),
                                new SqlParameter("@latitude", Convert.ToString(latitude)),
                                new SqlParameter("@longitude", Convert.ToString(longitude)),
                                new SqlParameter("@Breed", Convert.ToString(Breed)),
                                new SqlParameter("@txtmail", Convert.ToString(txtmail))
                        };

                        iReturn = db.Exec_QueryWithParamNew("Insert_RetailerDetails", CommandType.StoredProcedure, parameters);

                        //strQry = " insert into NewContact_Dr(Ukey, FormarName, DFDairyMP, MonthlyAI, AITCU,MCCNFPM, MCCMilkColDaily, CreatedDate," +
                        //    " ListedDrCode,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)" +
                        //    "VALUES('" + UKey + "','" + DR_Name + "','" + DFDairyMP + "','" + MonthlyAI + "','" + curentCompitat + "','" + MCCNFPM + "'," +
                        //    "'" + MCCMilkColDaily + "',getdate(),'" + Listed_DR_Code + "','" + DR_Spec + "'," +
                        //    "'" + Milk_pot + "','" + curentCom + "','" + FrequencyOfVisit + "')";

                        //jReturn = db.ExecQry(strQry);

                        //strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                        //           " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                        //           " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee, " +
                        //            "Breed,Ukey,NoOfAnimal,Entry_Mode,ListedDr_Email) " +
                        //           " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                        //           " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                        //           "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                        //           "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "'," +
                        //           " '" + Breed + "','" + UKey + "','" + credit_days + "','Web','" + txtmail + "')";                          

                        //iReturn = db.ExecQry(strQry);


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -4;
                }

            }
            else
            {
                iReturn = -2;
            }

        }
        else
        {
            iReturn = -3;
        }

        return iReturn;
    }

    public int RecordAdd(string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode, string latitude, string longitude, string txtmail)
    {
        int iReturn = -1;
        //int Listed_DR_Code = -1;
        if (!sRecordExist(retail_code, DR_Name, Div_code))
        {
            if (!RecordExist(DR_Name, retail_code, Div_code))
            {

                if (!ERPRecordExist(erbCode, Div_code, retail_code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        string Division_Code = "-1";


                        //Listed_DR_Code = -1;

                        //strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                        //Listed_DR_Code = db.Exec_Scalar(strQry);

                        strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                        DataSet ds = db.Exec_DataSet(strQry);

                        string sfcode = "";
                        string distname = "";
                        foreach (DataRow dd in ds.Tables[0].Rows)
                        {
                            sfcode = dd["sf_code"].ToString();
                            distname = dd["Dist_Name"].ToString();
                            Division_Code = dd["Division_Code"].ToString();
                        }

                        if ((DR_Name == null || DR_Name == ""))
                        { DR_Name = ""; }

                        if ((Milk_pot == null || Milk_pot == ""))
                        { Milk_pot = ""; }

                        if ((credit_days == null || credit_days == ""))
                        { credit_days = ""; }

                        if ((Mobile_No == null || Mobile_No == ""))
                        { Mobile_No = ""; }

                        if ((erbCode == null || erbCode == ""))
                        { erbCode = ""; }

                        if ((advance_amount == null || advance_amount == ""))
                        { advance_amount = ""; }

                        if ((sales_Tax == null || sales_Tax == ""))
                        { sales_Tax = ""; }

                        if ((Tinno == null || Tinno == ""))
                        { Tinno = ""; }

                        if ((Uom == null || Uom == ""))
                        { Uom = ""; }

                        if ((Uom_Name == null || Uom_Name == ""))
                        { Uom_Name = ""; }

                        if ((DR_Class == null || DR_Class == ""))
                        { DR_Class = "0"; }


                        if ((drcategory == null || drcategory == ""))
                        { drcategory = "0"; }


                        SqlParameter[] parameters = new SqlParameter[]
                        {
                                new SqlParameter("@UKey", ""),
                                new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                                new SqlParameter("@DFDairyMP", ""),
                                new SqlParameter("@MonthlyAI", ""),
                                new SqlParameter("@curentCompitat", ""),
                                new SqlParameter("@MCCNFPM", ""),
                                new SqlParameter("@MCCMilkColDaily", ""),
                                new SqlParameter("@DR_Spec",  Convert.ToString(DR_Spec)),
                                new SqlParameter("@Milk_pot", Convert.ToString(Milk_pot)),
                                new SqlParameter("@curentCom", ""),
                                new SqlParameter("@FrequencyOfVisit", ""),
                                new SqlParameter("@sfcode", Convert.ToString(sfcode)),
                                new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                                new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                                new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                                new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                                new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                                new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                                new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                                new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                                new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                                new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                                new SqlParameter("@ad", Convert.ToString(ad)),
                                new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                                new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                                new SqlParameter("@Division_Code", Convert.ToString(Division_Code)),
                                new SqlParameter("@Uom", Convert.ToString(Uom)),
                                new SqlParameter("@Uom_Name", Convert.ToString(Uom_Name)),
                                new SqlParameter("@sf_code", Convert.ToString(sf_code)),
                                new SqlParameter("@distname", Convert.ToString(distname)),
                                new SqlParameter("@re_type", Convert.ToString(re_type)),
                                new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                                new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                                new SqlParameter("@Cus_Alt", Convert.ToString(Cus_Alt)),
                                new SqlParameter("@drcategory", Convert.ToString(drcategory)),
                                new SqlParameter("@drcategoryName", Convert.ToString(drcategoryName)),
                                new SqlParameter("@latitude", Convert.ToString(latitude)),
                                new SqlParameter("@longitude", Convert.ToString(longitude)),
                                new SqlParameter("@Breed", ""),
                                new SqlParameter("@txtmail", Convert.ToString(txtmail))
                        };

                        iReturn = db.Exec_QueryWithParamNew("Insert_RetailerDetails", CommandType.StoredProcedure, parameters);

                        //strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                        //           " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                        //           " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee,Entry_Mode,ListedDr_Email) " +
                        //           " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                        //           " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                        //           "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                        //           "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "','Web','" + txtmail + "')";

                        //iReturn = db.ExecQry(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -4;
                }

            }
            else
            {
                iReturn = -2;
            }

        }
        else
        {
            iReturn = -3;
        }

        return iReturn;
    }

    public bool sRecordExist(string retail_code, string DR_Name, string Div_code)
    {

        bool bRecordExist = false;
        try
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "SELECT COUNT(code) FROM Mas_ListedDr WHERE ListedDr_Name='" + DR_Name + "' and Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public bool RecordExist(string Listed_DR_Name, string retail_code, string Div_Code)
    {

        bool bRecordExist = false;
        try
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE Code='" + retail_code + "' AND ListedDr_Name='" + Listed_DR_Name + "' and Division_Code='" + Div_Code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public bool ERPRecordExist(string retail_code, string Div_code, string rtCode)
    {

        bool bRecordExist = false;
        try
        {
            if (retail_code != string.Empty)
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ListedDrCode FROM Mas_ListedDr WHERE  Code='" + retail_code + "' and Division_Code='" + Div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";

                DataSet ds = db.Exec_DataSet(strQry);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == rtCode)
                    {

                    }
                    else
                    {
                        bRecordExist = true;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public DataSet get_RetailerCustomField(string listeddrcode, string columnName, string divcode)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsTerr = null;

        strQry = "EXEC [Get_Retailer_CustomFieldDetails] '" + listeddrcode + "','" + columnName + "','" + divcode + "'";

        //strQry = " SELECT *FROM Trans_Retailer_Custom_Field Where RetailerID = " + listeddrcode + "";

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

    public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        strQry = "EXEC [Get_CustomForms_Fields] '" + divcode + "' ,'" + ModeleId + "' ";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    public DataSet GetCustomFormsFieldsFilesData(string divcode, string ModeleId)
    {

        DataSet dsAdmin = new DataSet();

        string strQry = "SELECT * FROM Trans_Custom_Fields_Details ";
        strQry += " WHERE Div_code = @Division_Code AND ModuleId=@ModuleId AND Fld_Type IN('FSC','FS','FC') ";

        try
        {
            using (var con = new SqlConnection(Global.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(divcode));
                    cmd.Parameters.AddWithValue("@ModuleId", Convert.ToInt32(ModeleId));
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        return dsAdmin;

    }

    public int Recordupdate_detail1(string Dr_Code, string curentCompitat, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude, string DFDairyMP, string MonthlyAI, string MCCNFPM, string MCCMilkColDaily, string FrequencyOfVisit, string Breed, string curentCom, string ukey, string txtmail)
    {
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();
        if (!sRecordExist1(erbCode, Dr_Code, div_code))
        {
            if (!ERPRecordExist(erbCode, div_code, Dr_Code))
            {

                strQry = "select sf_code,Dist_Name,Division_Code,Territory_SName from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                DataSet ds = db.Exec_DataSet(strQry);

                string Division_Code = "-1";
                string sfcode = "";
                string distname = "";
                string terr_code = "";

                foreach (DataRow dd in ds.Tables[0].Rows)
                {
                    sfcode = dd["sf_code"].ToString();
                    distname = dd["Dist_Name"].ToString();
                    Division_Code = dd["Division_Code"].ToString();
                    terr_code = dd["Territory_SName"].ToString();
                }

                //strQry = " update NewContact_Dr" +
                //          " set FormarName='" + DR_Name + "',DFDairyMP='" + DFDairyMP + "', MonthlyAI='" + MonthlyAI + "', AITCU='" + curentCompitat + "', " +
                //          "MCCNFPM ='" + MCCNFPM + "', MCCMilkColDaily ='" + MCCMilkColDaily + "', CreatedDate=getdate()," +
                //  "ListedDrCode='" + Dr_Code + "',CustomerCategory='" + DR_Spec + "',PotentialFSD='" + Milk_Potential + "'," +
                //  "CurrentlyUFSD='" + curentCom + "',FrequencyOfVisit='" + FrequencyOfVisit + "'" +
                //  " where ListedDrCode='" + Dr_Code + "'  and Ukey='" + ukey + "'";

                //jReturn = db.ExecQry(strQry);

                //strQry = " update Mas_ListedDr" +
                //           " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                //           ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                //           ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                //           ",ListedDr_Address2='" + DR_Address2 + "',LastUpdt_Date= getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                //           ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                //           ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "', " +
                //           "NoOfAnimal='" + credit_days + "', Breed='" + Breed + "',ListedDr_Email='" + txtmail + "'" +
                //           " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' and Ukey='" + ukey + "'";

                //iReturn = db.ExecQry(strQry);

                if ((DR_Name == null || DR_Name == ""))
                { DR_Name = ""; }

                if ((Milk_Potential == null || Milk_Potential == ""))
                { Milk_Potential = ""; }

                if ((credit_days == null || credit_days == ""))
                { credit_days = ""; }

                if ((Mobile_No == null || Mobile_No == ""))
                { Mobile_No = ""; }

                if ((erbCode == null || erbCode == ""))
                { erbCode = ""; }

                if ((advance_amount == null || advance_amount == ""))
                { advance_amount = ""; }

                if ((sales_Tax == null || sales_Tax == ""))
                { sales_Tax = ""; }

                if ((Tinno == null || Tinno == ""))
                { Tinno = ""; }

                if ((UOM == null || UOM == ""))
                { UOM = ""; }

                if ((UOM_Name == null || UOM_Name == ""))
                { UOM_Name = ""; }

                if ((DR_Class == null || DR_Class == ""))
                { DR_Class = "0"; }


                if ((catgoryCode == null || catgoryCode == ""))
                { catgoryCode = "0"; }


                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@UKey", Convert.ToString(ukey)),
                    new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                    new SqlParameter("@DFDairyMP", Convert.ToString(DFDairyMP)),
                    new SqlParameter("@MonthlyAI", Convert.ToString(MonthlyAI)),
                    new SqlParameter("@curentCompitat", Convert.ToString(curentCompitat)),
                    new SqlParameter("@MCCNFPM", Convert.ToString(MCCNFPM)),
                    new SqlParameter("@MCCMilkColDaily", Convert.ToString(MCCMilkColDaily)),
                    new SqlParameter("@Dr_Code", Convert.ToString(Dr_Code)),
                    new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                    new SqlParameter("@Milk_Potential", Convert.ToString(Milk_Potential)),
                    new SqlParameter("@curentCom", Convert.ToString(curentCom)),
                    new SqlParameter("@FrequencyOfVisit", Convert.ToString(FrequencyOfVisit)),
                    new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                    new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                    new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                    new SqlParameter("@terr_code", Convert.ToString(terr_code)),
                    new SqlParameter("@distname", Convert.ToString(distname)),
                    new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                    new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                    new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                    new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                    new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                    new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                    new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                    new SqlParameter("@ad", Convert.ToString(ad)),
                    new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                    new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                    new SqlParameter("@Uom", Convert.ToString(UOM)),
                    new SqlParameter("@Uom_Name", Convert.ToString(UOM_Name)),
                    new SqlParameter("@Retailer_Type", Convert.ToString(Retailer_Type)),
                    new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                    new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                    new SqlParameter("@Cus_Alt", Convert.ToString(Cus_alt)),
                    new SqlParameter("@catgoryCode", Convert.ToString(catgoryCode)),
                    new SqlParameter("@catgoryName", Convert.ToString(catgoryName)),
                    new SqlParameter("@latitude", Convert.ToString(latitude)),
                    new SqlParameter("@longitude", Convert.ToString(longitude)),
                    new SqlParameter("@div_code", Convert.ToString(Division_Code)),
                    new SqlParameter("@Breed", Convert.ToString(Breed)),
                    new SqlParameter("@txtmail", Convert.ToString(txtmail))
                };

                iReturn = db.Exec_QueryWithParamNew("Update_RetailerDetails", CommandType.StoredProcedure, parameters);
            }
            else
            {
                iReturn = -4;
            }
        }
        else
        {
            iReturn = -3;
        }
        return iReturn;
    }

    public int Recordupdate_detailCustom(string Dr_Code, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude, string txtmail)
    {
        int iReturn = -1;
        DB_EReporting db = new DB_EReporting();

        strQry = "SELECT ListedDrCode FROM Mas_ListedDr WHERE  ListedDrCode  =" + Dr_Code + " AND Division_Code='" + div_code + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";

        DataSet ds = db.Exec_DataSet(strQry);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                strQry = "select sf_code,Dist_Name,Division_Code,Territory_SName from  Mas_Territory_Creation where Territory_Code ='" + DR_Terr + "'";
                DataSet ds1 = db.Exec_DataSet(strQry);

                string Division_Code = "-1";
                string sfcode = "";
                string distname = "";
                string terr_code = "";

                foreach (DataRow dd in ds1.Tables[0].Rows)
                {
                    sfcode = dd["sf_code"].ToString();
                    distname = dd["Dist_Name"].ToString();
                    Division_Code = dd["Division_Code"].ToString();
                    terr_code = dd["Territory_SName"].ToString();
                }

                //strQry = " update Mas_ListedDr " +
                //       " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                //       ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                //       ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                //       ",ListedDr_Address2='" + DR_Address2 + "',ListedDr_Created_Date=getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                //       ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                //       ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "',ListedDr_Email='" + txtmail + "'" +
                //       " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' ";

                if ((DR_Name == null || DR_Name == ""))
                { DR_Name = ""; }

                if ((Milk_Potential == null || Milk_Potential == ""))
                { Milk_Potential = ""; }

                if ((credit_days == null || credit_days == ""))
                { credit_days = ""; }

                if ((Mobile_No == null || Mobile_No == ""))
                { Mobile_No = ""; }

                if ((erbCode == null || erbCode == ""))
                { erbCode = ""; }

                if ((advance_amount == null || advance_amount == ""))
                { advance_amount = ""; }

                if ((sales_Tax == null || sales_Tax == ""))
                { sales_Tax = ""; }

                if ((Tinno == null || Tinno == ""))
                { Tinno = ""; }

                if ((UOM == null || UOM == ""))
                { UOM = ""; }

                if ((UOM_Name == null || UOM_Name == ""))
                { UOM_Name = ""; }

                if ((DR_Class == null || DR_Class == ""))
                { DR_Class = "0"; }


                if ((catgoryCode == null || catgoryCode == ""))
                { catgoryCode = "0"; }

                if ((credit_limit == null || credit_limit == ""))
                { credit_limit = "0"; }


                SqlParameter[] parameters = new SqlParameter[]
                {
                        new SqlParameter("@UKey", ""),
                        new SqlParameter("@DR_Name", Convert.ToString(DR_Name)),
                        new SqlParameter("@DFDairyMP", ""),
                        new SqlParameter("@MonthlyAI", ""),
                        new SqlParameter("@curentCompitat", ""),
                        new SqlParameter("@MCCNFPM", ""),
                        new SqlParameter("@MCCMilkColDaily", ""),
                        new SqlParameter("@Dr_Code", Convert.ToString(Dr_Code)),
                        new SqlParameter("@DR_Spec", Convert.ToString(DR_Spec)),
                        new SqlParameter("@Milk_Potential", Convert.ToString(Milk_Potential)),
                        new SqlParameter("@curentCom", ""),
                        new SqlParameter("@FrequencyOfVisit", ""),
                        new SqlParameter("@DR_Terr", Convert.ToString(DR_Terr)),
                        new SqlParameter("@DR_Class", Convert.ToString(DR_Class)),
                        new SqlParameter("@erbCode", Convert.ToString(erbCode)),
                        new SqlParameter("@terr_code", Convert.ToString(terr_code)),
                        new SqlParameter("@distname", Convert.ToString(distname)),
                        new SqlParameter("@Mobile_No", Convert.ToString(Mobile_No)),
                        new SqlParameter("@advance_amount", Convert.ToString(advance_amount)),
                        new SqlParameter("@dr_spec_name", Convert.ToString(dr_spec_name)),
                        new SqlParameter("@sales_Tax", Convert.ToString(sales_Tax)),
                        new SqlParameter("@Tinno", Convert.ToString(Tinno)),
                        new SqlParameter("@credit_days", Convert.ToString(credit_days)),
                        new SqlParameter("@dr_class_name", Convert.ToString(dr_class_name)),
                        new SqlParameter("@ad", Convert.ToString(ad)),
                        new SqlParameter("@DR_Address1", Convert.ToString(DR_Address1)),
                        new SqlParameter("@DR_Address2", Convert.ToString(DR_Address2)),
                        new SqlParameter("@Uom", Convert.ToString(UOM)),
                        new SqlParameter("@Uom_Name", Convert.ToString(UOM_Name)),
                        new SqlParameter("@Retailer_Type", Convert.ToString(Retailer_Type)),
                        new SqlParameter("@outstanding", Convert.ToString(outstanding)),
                        new SqlParameter("@credit_limit", Convert.ToString(credit_limit)),
                        new SqlParameter("@Cus_Alt", Convert.ToString(Cus_alt)),
                        new SqlParameter("@catgoryCode", Convert.ToString(catgoryCode)),
                        new SqlParameter("@catgoryName", Convert.ToString(catgoryName)),
                        new SqlParameter("@latitude", Convert.ToString(latitude)),
                        new SqlParameter("@longitude", Convert.ToString(longitude)),
                        new SqlParameter("@div_code", Convert.ToString(Division_Code)),
                        new SqlParameter("@Breed", ""),
                        new SqlParameter("@txtmail", Convert.ToString(txtmail))
               };

                iReturn = db.Exec_QueryWithParamNew("Update_RetailerDetails", CommandType.StoredProcedure, parameters);


                //iReturn = db.ExecQry(strQry);
            }
            else
            { iReturn = -4; }
        }
        else
        { iReturn = -3; }

        return iReturn;
    }

    public bool sRecordExist1(string retail_code, string DR_Code, string Div_code)
    {

        bool bRecordExist = false;
        try
        {
            DB_EReporting db = new DB_EReporting();

            strQry = "  SELECT COUNT(code) FROM Mas_ListedDr WHERE Code='" + retail_code + "' and  ListedDrCode ! =" + DR_Code + " and Division_Code !='" + Div_code + "' and (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
            int iRecordExist = db.Exec_Scalar(strQry);

            if (iRecordExist > 0)
                bRecordExist = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return bRecordExist;
    }

    public DataSet GetCustomFormsFieldsGroupData(string divcode, string ModeleId)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsAdmin = null;

        strQry = "EXEC [Get_CustomForms_FieldsGroups] '" + divcode + "' ,'" + ModeleId + "' ";

        try
        {
            dsAdmin = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsAdmin;
    }

    public DataSet getStatePerDivision(string div_code)
    {
        DataSet dsAdmin = new DataSet();

        string strQry = "SELECT State_Code,Division_Name,Division_SName,Url_Short_Name  FROM Mas_Division ";
        strQry += " Where Division_Code = @Division_Code  GROUP BY State_Code,Division_Name,Division_SName,Url_Short_Name ";

        try
        {
            using (var con = new SqlConnection(Global.ConnString))
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = strQry;
                    cmd.Parameters.AddWithValue("@Division_Code", Convert.ToInt32(div_code));
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter dap = new SqlDataAdapter();
                    dap.SelectCommand = cmd;
                    con.Open();
                    dap.Fill(dsAdmin);
                    con.Close();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
        return dsAdmin;
    }
}