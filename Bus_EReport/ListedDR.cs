using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class ListedDR
    {
        private string strQry = string.Empty;

        public DataSet getEmptyListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 10 '' ListedDR_Name,'' ListedDR_Address1 ,''ListedDR_Address2,'' Contact_Person,'' Mobile,''Credit_Days " +
                     " FROM  sys.tables ";
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

        //Change done by saravanan
        public DataSet GetDataFromDataBase(string prefixText)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where ListedDr_Name like '" + prefixText + "%';";

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

        public DataSet getTopListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 1 '' ListedDR_Name,'' ListedDR_Address1 " +
                     " FROM  sys.tables ";
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

        public DataSet getListedDr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName,d.SDP as Activity_Date, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        //slno

        public DataSet getListedDr_SlNO(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by ListedDr_Sl_No";
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
        //Reactivation
        public DataSet getListedDr_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, convert(char(11),ListedDr_Deactivate_Date,103) ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name ,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_Qua_Name as Doc_QuaName, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                        " Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 1 ";
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
        public DataSet getListedDr_React_approve(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE  " +
                        " d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 3 and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2)";
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

        //Reject List

        public DataSet getListedDr_Reject(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        "stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.ListedDr_Active_Flag = 4 ";
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

        // Sorting For ListedDR List 
        public DataTable getListedDoctorList_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        //Reactivation Sorting

        public DataTable getListedDoctorList_DataTable_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name ,d.Doc_Class_ShortName as Doc_ClsName ,d.Doc_Qua_Name as Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.ListedDr_Active_Flag = 1";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        //public DataSet getListedDr_MGR(string sfcode, int iVal)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsListedDR = null;

        //    strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ  " +
        //                " from mas_listeddr a, Mas_Salesforce b " +
        //                " where b.Reporting_To_SF = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " ";
        //    try
        //    {
        //        dsListedDR = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsListedDR;
        //}
        public DataSet getListedDr_MGR(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + " and  a.Division_code in('" + div_code + "')";
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
        public DataSet getListedDr_MGRNew(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + "  " +
                        " and a.SLVNo not in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2  and Division_code = '" + div_code + "')  and a.Division_code in('" + div_code + "') ";
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
        public DataSet getListedDr_MGRapp(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name  " +
                        " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c " +
                        " where c.LstDr_AM = '" + sfcode + "' and  a.Sf_Code = b.Sf_Code and b.Sf_Code=c.Sf_Code and a.ListedDr_Active_Flag = " + iVal + "  " +
                        " and a.SLVNo not in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  and Division_code = '" + div_code + "')  and a.Division_code in('" + div_code + "') ";
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
        public DataSet getListedDrforSpl(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.Doc_Qua_Name,d.Doc_Class_ShortName," +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforCat(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforQual(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName,d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforClass(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Qua_Name, d.Doc_Class_ShortName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforTerr(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName, d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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

        //Reactivation Dr Search

        public DataSet getListedDrforSpl_React(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name, d.Doc_Class_ShortName as Doc_ClsName ,d.Doc_Qua_Name as Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforCat_React(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                         " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforQual_React(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforClass_React(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName , " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                         "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 1";
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

        public DataSet getListedDrforTerr_React(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforName_React(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        "stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        "and d.ListedDr_Active_Flag = 1";
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
        //end

        public DataSet getListedDoctorr_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0 Order By 2";
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

        public DataTable get_ListedDoctor_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.Territory_Code, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where  t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        " and d.ListedDr_Active_Flag = 0 Order By 2";
            try
            {
                //dsListedDR = db_ER.Exec_DataSet(strQry);
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataSet getListedDrforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Type,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName, d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctor(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1," +
                         "case isnull(ListedDR_DOB,null)" +
                            " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                         "case isnull(ListedDR_DOW,null)" +
                            " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                         "'' No_of_Visit, ListedDR_Mobile, " +
                                        " ListedDR_Phone, ListedDR_EMail  " +
                                        " FROM Mas_ListedDr " +
                                        " WHERE Sf_Code =  '" + sfcode + "' and Division_Code = '" + div_code + "' " +
                                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        " case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name like '%" + Name + "%'" +
                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforTerr(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND (Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + TerrCode + ',' + "%' or Territory_Code like '" + TerrCode + "') " +
                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforClass(string sfcode, string ClsCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                        "case isnull(ListedDR_DOB,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                        "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103) end ListedDR_DOW," +
                //" convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_ClsCode ='" + ClsCode + "' " +
                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforSpl(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                      "case isnull(ListedDR_DOB,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOB,103)  end ListedDR_DOB," +
                     "case isnull(ListedDR_DOW,null)" +
                        " when '1900-01-01 00:00:00.000' then null  else  convert(char(10),ListedDr_DOW,103)  end ListedDR_DOW," +
                //" convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, "+
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_Special_Code ='" + SplCode + "' " +
                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforCat(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                      " case isnull(ListedDR_DOB,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOB  end ListedDR_DOB," +
                      " case isnull(ListedDR_DOW,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOW  end ListedDR_DOW," +
                // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +                      
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_Cat_Code ='" + CatCode + "' " +
                        " AND ListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctorforQual(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name, ListedDR_Address1, " +
                     "case isnull(ListedDR_DOB,null)" +
                     " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOB  end ListedDR_DOB," +
                     "case isnull(ListedDR_DOW,null)" +
                      " when '1900-01-01 00:00:00.000' then null  else  ListedDR_DOW  end ListedDR_DOW," +
                // " convert(char(10),ListedDR_DOB,103)ListedDR_DOB, convert(char(10),ListedDR_DOW,103)ListedDR_DOW, " +
                        " '' No_of_Visit, ListedDR_Mobile, " +
                        " ListedDR_Phone, ListedDR_EMail  " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_QuaCode ='" + QuaCode + "' " +
                        " AND ListedDr_Active_Flag = 0";
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

        public DataSet getListedDr(string sfcode, string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Qua_Name as Doc_QuaName , d.Doc_Class_ShortName as Doc_ClsName , " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        " and d.ListedDrCode = '" + drcode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet ViewListedDr(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT d.ListedDrCode,d.Code,isnull(d.ListedDr_phone,d.ListedDr_Mobile) ListedDr_Mobile,d.ListedDr_Name,d.contactperson Contact_Person_Name,d.Doc_Special_Code,d.Doc_Spec_ShortName, " +
                    " d.Tin_No,d.Sales_Taxno,d.Territory_Code,d.Credit_Days,d.Doc_ClsCode,d.Doc_Class_ShortName,d.Advance_amount,d.Milk_Potential,d.UOM,d.UOM_Name,  " +
                    " d.ListedDr_Address1,d.ListedDr_Address2,Retailer_Type,stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
                    " and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name ,Outstanding,Credit_Limit,Cus_Alter,d.Doc_Cat_Code,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,ListedDr_Email" +
                    " FROM  Mas_ListedDr d WHERE d.ListedDrCode =  '" + drcode + "'  and d.ListedDr_Active_Flag = 0 ";


            //strQry = " SELECT d.ListedDrCode,d.Sf_Code,d.ListedDr_Name,d.ListedDr_Address1,d.ListedDr_Address2,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, " +
            //         " d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode,d.Code,d.Credit_Days,d.Contact_Person_Name,d.Tin_No,d.Sales_Taxno,d.Advance_amount,d.Milk_Potential,d.UOM,d.UOM_Name," +
            //         " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
            //         " and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
            //         " d.Territory_Code, d.ListedDr_Mobile,d.Code FROM  Mas_ListedDr d WHERE d.ListedDrCode =  '" + drcode + "' " +
            //         " and d.ListedDr_Active_Flag = 0 ";

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
        public DataSet Viewcustomer_Details(string drcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,d.Doc_Cat_ShortName as Doc_Cat_Name,d.Doc_Cat_Code,d.Doc_Spec_ShortName as Doc_Special_Name,d.Doc_Special_Code, d.Doc_Qua_Name as Doc_QuaName,d.Doc_QuaCode,d.Doc_Class_ShortName as Doc_ClsName,d.Doc_ClsCode, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " d.Territory_Code,d.ListedDr_Address2,d.ListedDr_Address3, d.ListedDr_Pin, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " d.Code, convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW,d.State_Code," +
                      " d.ListedDr_Profile,d.ListedDr_Visit_Days,d.ListedDr_IUI,d.ListedDr_Avg_Patients,d.ListedDr_DayTime," +
                      " d.ListedDr_Hospital,d.ListedDr_Class_Patients,d.ListedDr_Consultation_Fee,d.Hospital_Address FROM" + " " +
                      " Mas_ListedDr d " +
                        "WHERE d.ListedDrCode =  '" + drcode + "'  " +

                        "and d.ListedDr_Active_Flag = 0";
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
        public int DeActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = 3 , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Reactivate
        public int ReActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag=0 , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Approve(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Reject(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Reject_Flag = 'DR', Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int ApproveAdd(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int AddVsDeActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select SLVNo from Mas_ListedDr where listeddrcode = '" + dr_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet FetchTerritory(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Division_Code = '" + div_code + "' AND territory_active_flag=0 ";
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

        public DataSet LoadTerritory(string sf_code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
            //             " UNION " +
            //         " SELECT Territory_Code,Territory_Name " +
            //         " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
            //         " AND Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
                     " AND Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " UNION " +
                     " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";

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

        public DataSet LoadTerritory(string sf_code, string Territory_Code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
            //             " UNION " +
            //         " SELECT Territory_Code,Territory_Name " +
            //         " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
            //         " AND Sf_Code = '" + sf_code + "' AND " +
            //         " Territory_Code in (" + terr_code + ") " +
            //         " AND territory_active_flag=0 ";

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code != '" + Territory_Code + "' " +
                     " AND Sf_Code = '" + sf_code + "' AND " +
                     " Territory_Code in (" + terr_code + ") " +
                     " AND territory_active_flag=0 " +
                     " UNION " +
                     " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";
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

        public DataSet FetchTerritoryName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code Territory_Code, " +
                     " (a.Territory_Name +  ' (' + CAST((select COUNT(b.ListedDrCode) from Mas_ListedDr b " +
                     " where a.Territory_Code=b.Territory_Code and b.ListedDr_Active_Flag=0) as CHAR(3)) " +
                     " + ') ' ) Territory_Name " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";
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

        public DataSet FetchCategory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Stockist where Distributor_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_Cat_Code,'---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name " +
                         " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                     " FROM  Mas_Doctor_Category where division_Code = '" + div_code + "' AND doc_cat_active_flag=0 ";
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

        public DataSet FetchSpeciality(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();


            DataSet dsTerr = null;

            strQry = " SELECT 0 as Doc_Special_Code,'---Select---' as Doc_Special_SName, '---Select---' as Doc_Special_Name " +
                         " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + div_code + "' AND doc_special_active_flag=0 ";
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


        public DataSet FetchCatagory(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();


            DataSet dsTerr = null;



            strQry = " SELECT 0 as Doc_Cat_Code,'---Select---' as Doc_Cat_SName, '---Select---' as Doc_Cat_Name " +
                         " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
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
        //public bool LRecordExist(string Listed_DR_Name)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE  ListedDr_Name='" + Listed_DR_Name + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}
        //public bool RecordExist(string ListedDrCode, string Listed_DR_Name, string Sf_Code)
        //{

        //    bool bRecordExist = false;
        //    try
        //    {
        //        DB_EReporting db = new DB_EReporting();

        //        strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE ListedDr_Name = '" + Listed_DR_Name + "' AND ListedDrCode!='" + ListedDrCode + "' AND Sf_Code ='" + Sf_Code + "'  ";

        //        int iRecordExist = db.Exec_Scalar(strQry);

        //        if (iRecordExist > 0)
        //            bRecordExist = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bRecordExist;
        //}

        //public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code)
        //{
        //    int iReturn = -1;

        //    if (!RecordExist(Listed_DR_Name))
        //    {
        //        try
        //        {

        //            DB_EReporting db = new DB_EReporting();

        //            int Division_Code = -1;
        //            int Listed_DR_Code = -1;

        //            Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");
        //            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
        //            Division_Code = db.Exec_Scalar(strQry);

        //            //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
        //            strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
        //            Listed_DR_Code = db.Exec_Scalar(strQry);

        //            //strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
        //            //         " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
        //            //         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
        //            //         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
        //            //         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 2, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
        //            //         " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";

        //            strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
        //                     " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
        //                     " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
        //                     " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
        //                     " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 2, getdate(), '" + Division_Code + "','','','', " +
        //                     " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";


        //            iReturn = db.ExecQry(strQry);

        //            if (iReturn != -1)
        //            {
        //                //Insert a record into LstDoctor_Terr_Map_History table
        //                strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
        //                int SNo = db.Exec_Scalar(strQry);

        //                strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
        //                          " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

        //                iReturn = db.ExecQry(strQry);

        //                if (Listed_DR_Terr.IndexOf(",") != -1)
        //                {
        //                    string[] subterr;
        //                    subterr = Listed_DR_Terr.Split(',');
        //                    foreach (string st in subterr)
        //                    {
        //                        if (st.Trim().Length > 0)
        //                        {
        //                            strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
        //                            int iPlanNo = db.Exec_Scalar(strQry);

        //                            //Insert a record into Call Plan
        //                            strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
        //                                    " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

        //                            iReturn = db.ExecQry(strQry);
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        iReturn = -2;
        //    }
        //    return iReturn;
        //}


        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, string Terr_Name)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                         " Territory_Code,Doc_QuaCode, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "','" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";


                iReturn = db.ExecQry(strQry);

                //Insert a record into LstDoctor_Terr_Map_History table
                strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                int SNo = db.Exec_Scalar(strQry);

                strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                          " '" + Listed_DR_Terr + "',getdate(), '" + Division_Code + "')";

                iReturn = db.ExecQry(strQry);

                //Insert a record into Call Plan

                strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                int iPlanNo = db.Exec_Scalar(strQry);

                strQry = "insert into Call_Plan values('" + sf_code + "', '" + Listed_DR_Terr + "', getdate(), '" + iPlanNo + "', " +
                        " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'" + Terr_Name + "')";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }



        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', 2, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }

//detailed add for tbg
        public int RecordAdd11(string DR_Name,string curentCompitat, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode, string latitude, string longitude, string DFDairyMP, string MonthlyAI, string MCCNFPM, string MCCMilkColDaily, string FrequencyOfVisit, string Breed,string curentCom,string txtmail)
        {
            int iReturn = -1;
            int jReturn = -1;
            int Listed_DR_Code;
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
                            Listed_DR_Code = -1;
                            // UKey = "";

                            // strQry = "select Division_Code from Mas_Stockist where Territory_Code = '" + DR_Terr + "' ";
                            // Division_Code = db.Exec_Scalar(strQry);

                            strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                            Listed_DR_Code = db.Exec_Scalar(strQry);

                           

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

                            strQry = " insert into NewContact_Dr(Ukey, FormarName, DFDairyMP, MonthlyAI, AITCU,MCCNFPM, MCCMilkColDaily, CreatedDate," +
                                " ListedDrCode,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)" +
                                "VALUES('" + UKey + "','" + DR_Name + "','" + DFDairyMP + "','" + MonthlyAI + "','"+ curentCompitat + "','" + MCCNFPM + "'," +
                                "'" + MCCMilkColDaily + "',getdate(),'"+ Listed_DR_Code + "','" + DR_Spec + "'," +
                                "'" + Milk_pot + "','" + curentCom + "','" + FrequencyOfVisit + "')";


                            jReturn = db.ExecQry(strQry);
                            // strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,Contact_Person_Name,Doc_Special_Code,Doc_Spec_ShortName, " +
                            //    " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                            //   " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName) " +
                            //   " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                            //   " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "','" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "','" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "')";



                            strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                                       " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                                       " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee, " +
                                        "Breed,Ukey,NoOfAnimal,Entry_Mode,ListedDr_Email) " +
                                       " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                                       " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                                       "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                                       "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "'," +
                                " '" + Breed + "','"+UKey+"','"+ credit_days + "','Web','"+ txtmail + "')";


                            iReturn = db.ExecQry(strQry);


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
     
        // DetailAdd ListedDr
        public int RecordAdd(string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string Div_code, string Milk_pot, string Uom, string Uom_Name, string re_type, string outstanding, string credit_limit, string Cus_Alt, string drcategory, string drcategoryName, string erbCode,string latitude, string longitude,string txtmail)
        {
            int iReturn = -1;
            int Listed_DR_Code;
            if (!sRecordExist(retail_code, DR_Name, Div_code))
            {
                if (!RecordExist(DR_Name, retail_code, Div_code))
                {

                    //if (!ERPRecordExist(erbCode, Div_code, retail_code))
                    //{
                        try
                        {

                            DB_EReporting db = new DB_EReporting();

                            string Division_Code = "-1";
                            Listed_DR_Code = -1;

                            // strQry = "select Division_Code from Mas_Stockist where Territory_Code = '" + DR_Terr + "' ";
                            // Division_Code = db.Exec_Scalar(strQry);

                            strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr";
                            Listed_DR_Code = db.Exec_Scalar(strQry);

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



                           // strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,Contact_Person_Name,Doc_Special_Code,Doc_Spec_ShortName, " +
                               //    " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                                //   " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName) " +
                                //   " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                                //   " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "','" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "','" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "')";

  						strQry = " insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Mobile,Code,contactperson,Doc_Special_Code,Doc_Spec_ShortName, " +
                                   " Tin_No,sales_Taxno,Territory_Code,Credit_Days,Doc_ClsCode,Doc_Class_ShortName,Advance_amount,ListedDr_Address1,ListedDr_Address2,Division_Code, ListedDr_Active_Flag, ListedDr_Created_Date, " +
                                   " LastUpdt_Date,Milk_Potential,UOM,UOM_Name,TERRCODE,DIST_NAME,Retailer_Type,outstanding,Credit_Limit,Cus_Alter,Doc_Cat_Code,Doc_Cat_ShortName,ListedDr_Class_Patients,ListedDr_Consultation_Fee,Entry_Mode,ListedDr_Email) " +
                                   " VALUES('" + Listed_DR_Code + "', '" + sfcode + "', '" + DR_Name + "', '" + Mobile_No + "', '" + erbCode + "', '" + advance_amount + "', " +
                                   " '" + DR_Spec + "','" + dr_spec_name + "','" + sales_Tax + "','" + Tinno + "','" + DR_Terr + "','" + credit_days + "','" + DR_Class + "','" + dr_class_name + "','" + ad + "','" + DR_Address1 + "'," +
                                   "'" + DR_Address2 + "', '" + Division_Code + "',0,getdate(),getdate(),'" + Milk_pot + "','" + Uom + "','" + Uom_Name + "','" + sf_code + "','" + distname + "','" + re_type + "','" + outstanding + "'," +
                                   "'" + credit_limit + "','" + Cus_Alt + "','" + drcategory + "','" + drcategoryName + "','" + latitude + "','" + longitude + "','Web','"+ txtmail + "')";



                            iReturn = db.ExecQry(strQry);


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    //}
                    //else
                    //{
                    //    iReturn = -4;
                    //}

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
        public bool RecordExist1(int Dr_Code, string DR_Name, string Div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDr_Name) FROM Mas_ListedDr WHERE ListedDrCode!='" + Dr_Code + "' and Division_Code='" + Div_code + "' and ListedDr_Name ='" + DR_Name + "' AND  (ListedDr_Active_Flag=0 or ListedDr_Active_Flag=2) ";
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
  public int Recordupdate_detail1(string Dr_Code,string curentCompitat, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude, string DFDairyMP,string  MonthlyAI,string MCCNFPM,string MCCMilkColDaily,string FrequencyOfVisit,string Breed,string curentCom,string ukey,string txtmail)
        {
            int iReturn = -1;
            int jReturn = -1;
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
                    //strQry = " insert into NewContact_Dr(Ukey, FormarName, DFDairyMP, MonthlyAI, AITCU,MCCNFPM, MCCMilkColDaily, CreatedDate," +
                    //           " ListedDrCode,,CustomerCategory,PotentialFSD,CurrentlyUFSD,FrequencyOfVisit)" +
                    //           "VALUES('" + UKey + "','" + DR_Name + "','" + DFDairyMP + "','" + MonthlyAI + "','" + curentCompitator + "','" + MCCNFPM + "'," +
                    //           "'" + MCCMilkColDaily + "',getdate(),'" + FrequencyOfVisit + "'," +
                    //           "'" + Dr_Code + "','" + DR_Spec + "','" + Milk_pot + "','" + curentCom + "','" + FrequencyOfVisit + "')";

                    strQry = " update NewContact_Dr" +
                              " set FormarName='" + DR_Name + "',DFDairyMP='" + DFDairyMP + "', MonthlyAI='" + MonthlyAI + "', AITCU='" + curentCompitat + "', " +
                              "MCCNFPM ='" + MCCNFPM + "', MCCMilkColDaily ='" + MCCMilkColDaily + "', CreatedDate=getdate()," +
                      "ListedDrCode='" + Dr_Code + "',CustomerCategory='" + DR_Spec + "',PotentialFSD='" + Milk_Potential + "'," +
                      "CurrentlyUFSD='" + curentCom + "',FrequencyOfVisit='" + FrequencyOfVisit + "'" +
                      " where ListedDrCode='" + Dr_Code + "'  and Ukey='"+ ukey + "'";

                    jReturn = db.ExecQry(strQry);

                    strQry = " update Mas_ListedDr" +
                               " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                               ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                               ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                               ",ListedDr_Address2='" + DR_Address2 + "',LastUpdt_Date= getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                               ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                               ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "', "+
                               "NoOfAnimal='" + credit_days + "', Breed='"+ Breed+"',ListedDr_Email='" + txtmail + "'" +
                               " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' and Ukey='" + ukey + "'";

                    iReturn = db.ExecQry(strQry);
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

        public int Recordupdate_detail(string Dr_Code, string DR_Name, string sf_code, string Mobile_No, string retail_code, string advance_amount, string DR_Spec, string dr_spec_name, string sales_Tax, string Tinno, string DR_Terr, string credit_days, string DR_Class, string dr_class_name, string ad, string DR_Address1, string DR_Address2, string div_code, string Milk_Potential, string UOM, string UOM_Name, string Retailer_Type, string outstanding, string credit_limit, string Cus_alt, string catgoryCode, string catgoryName, string erbCode, string latitude, string longitude,string txtmail)
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

                   // strQry = " update Mas_ListedDr" +
                    //           " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                      //         ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',Contact_Person_Name='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "',sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "',ListedDr_Address2='" + DR_Address2 + "',ListedDr_Created_Date=getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "',Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "',Doc_Cat_ShortName='" + catgoryName + "' " +
                         //      " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' ";


                    strQry = " update Mas_ListedDr" +
                               " set ListedDr_Name='" + DR_Name + "',Territory_Code='" + DR_Terr + "', Doc_ClsCode='" + DR_Class + "', Code='" + erbCode + "', terrcode ='" + terr_code + "', dist_name ='" + distname + "'" +
                               ", Doc_Special_Code='" + DR_Spec + "',ListedDr_Mobile='" + Mobile_No + "',contactperson='" + advance_amount + "',Doc_Spec_ShortName='" + dr_spec_name + "',Tin_No='" + sales_Tax + "'" +
                               ",sales_Taxno='" + Tinno + "',Credit_Days='" + credit_days + "',Doc_Class_ShortName='" + dr_class_name + "',Advance_amount='" + ad + "',ListedDr_Address1='" + DR_Address1 + "'" +
                               ",ListedDr_Address2='" + DR_Address2 + "',LastUpdt_Date=getdate(),Milk_Potential='" + Milk_Potential + "',UOM='" + UOM + "',UOM_Name='" + UOM_Name + "'" +
                               ",Retailer_Type='" + Retailer_Type + "' ,outstanding='" + outstanding + "' ,credit_limit='" + credit_limit + "',Cus_Alter='" + Cus_alt + "',Doc_Cat_Code='" + catgoryCode + "'" +
                               ",Doc_Cat_ShortName='" + catgoryName + "',ListedDr_Class_Patients='" + latitude + "',ListedDr_Consultation_Fee='" + longitude + "',ListedDr_Email='"+ txtmail + "'"+
                               " where ListedDrCode='" + Dr_Code + "' and Division_Code='" + div_code + "' ";
                   
                    iReturn = db.ExecQry(strQry);
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


        public int BulkEdit(string str, string Doc_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_ListedDr SET " + str + "  Where ListedDrCode='" + Doc_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int BulkEdit(string str, string Doc_Code, bool bSDP, string Listed_DR_Terr, string sf_code, string Division_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_ListedDr SET " + str + "  Where ListedDrCode='" + Doc_Code + "'";
                iReturn = db.ExecQry(strQry);

                //if (bSDP)
                //{
                //    if (iReturn != -1)
                //    {
                //        if (Listed_DR_Terr.IndexOf(",") != -1)
                //        {
                //            string[] subterr;
                //            subterr = Listed_DR_Terr.Split(',');
                //            foreach (string st in subterr)
                //            {
                //                if (st.Trim().Length > 0)
                //                {
                //                    string sQry = string.Empty;

                //                    sQry = "select count(Territory_Code) from call_plan Where ListedDrCode='" + Doc_Code + "' and " +
                //                               " Territory_Code = '" + st + "' and sf_code='" + sf_code + "' and Division_code='" + Division_Code + "' ";

                //                    int iRecordExist = db.Exec_Scalar(sQry);

                //                    CallPlan cp = new CallPlan();
                //                    //If the DR is not available on Call_Plan then the DR will be loaded in Call_Plan
                //                    if (iRecordExist <= 0)
                //                    {

                //                        iReturn = cp.Copy_WorkPlan(st, Doc_Code, sf_code);
                //                    }
                //                    else
                //                    {
                //                        iReturn = cp.Update_CallPlan(st, Doc_Code, sf_code);
                //                    }



                //                    //strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                //                    //int iPlanNo = db.Exec_Scalar(strQry);

                //                    ////Insert a record into Call Plan
                //                    //strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                //                    //        " '" + Doc_Code + "', '" + Division_Code + "', 0,'')";

                //                    //iReturn = db.ExecQry(strQry);
                //                }
                //            }
                //        }
                //    }

                //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int BulkEdit_CallPlan(string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsCallPlan = null;
                string sTerr = string.Empty;
                string Division_Code = string.Empty;
                string sQry = string.Empty;
                CallPlan cp = new CallPlan();

                strQry = "select Territory_Code,SF_Code,Division_code from Mas_ListedDr Where ListedDrCode='" + Doc_Code + "'";
                dsCallPlan = db.Exec_DataSet(strQry);

                if (dsCallPlan.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dsCallPlan.Tables[0].Rows)
                    {
                        sTerr = dataRow["Territory_Code"].ToString();
                        Division_Code = dataRow["Division_Code"].ToString();


                        sQry = "select count(Territory_Code) from call_plan Where ListedDrCode='" + Doc_Code + "' and " +
                                   " Territory_Code = '" + sTerr + "' and sf_code='" + sf_code + "' and Division_code='" + Division_Code + "' ";

                        int iRecordExist = db.Exec_Scalar(sQry);

                        //If the DR is not available on Call_Plan then the DR will be loaded in Call_Plan
                        if (iRecordExist <= 0)
                        {
                            iReturn = cp.Copy_WorkPlan(sTerr, Doc_Code, sf_code);
                        }
                        else
                        {
                            iReturn = cp.Update_CallPlan(sTerr, Doc_Code, sf_code);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public DataSet getDoctor_Terr_Catg_Spec_Class_Qual(string doc_code, string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Territory_Code,Doc_Cat_Code,Doc_Special_Code,Doc_ClsCode,Doc_QuaCode,SDP FROM  Mas_ListedDr " +
                     " WHERE ListedDrCode='" + doc_code + "' AND sf_Code= '" + sf_code + "' and Division_Code = '" + div_code + "' ";
            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        //alphabetical order
        public DataSet getDoctorlist_Alphabet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select '1' val,'All' stockist_Name union select distinct LEFT(stockist_Name,1) val, LEFT(stockist_Name,1) ListedDr_Name   FROM Mas_Stockist ORDER BY 1";
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



        public DataSet FetchClass(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();


            DataSet dsTerr = null;



            strQry = " SELECT 0 as Doc_ClsCode,'---Select---' as Doc_ClsSName, '---Select---' Doc_ClsName " +
                         " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND doc_cls_activeflag=0 ";
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

        public DataSet FetchQualification(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as  Doc_QuaCode,'---Select---' as Doc_QuaName " +
                         " UNION " +
                     " SELECT Doc_QuaCode,Doc_QuaName " +
                     " FROM  Mas_Doc_Qualification where division_Code = '" + div_code + "' AND doc_qua_activeflag=0 ";
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
        public DataSet getDoctorlist_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " AND LEFT(ListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
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


        public int Div_Code(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            return div_code;
        }
        public int RecordUpdateDoctor(string Listed_DR_Name, string Territory_Code, string Doc_ClsCode, string Doc_Cat_Code, string Doc_Special_Code, string Doc_QuaCode, string ListedDrCode, string CatSName, string SpecSName, string ClsSName, string QuaSName, string sf_code)
        {
            int iReturn = -1;
            if (!RecordExist(ListedDrCode, Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");
                    strQry = " update Mas_ListedDr" +
                        " set ListedDr_Name='" + Listed_DR_Name + "',Territory_Code='" + Territory_Code + "', Doc_ClsCode='" + Doc_ClsCode + "'," +
                        " Doc_Cat_Code='" + Doc_Cat_Code + "', Doc_Special_Code='" + Doc_Special_Code + "', Doc_QuaCode='" + Doc_QuaCode + "', " +
                        " Doc_Cat_ShortName = '" + CatSName + "', Doc_Spec_ShortName = '" + SpecSName + "', Doc_Class_ShortName = '" + ClsSName + "', Doc_Qua_Name = '" + QuaSName + "' " +
                        " where ListedDrCode='" + ListedDrCode + "'  ";

                    //strQry = " SELECT Territory_Code,Doc_Cat_Code,Doc_Special_Code,Doc_ClsCode,Doc_QuaCode FROM  Mas_ListedDr " +
                    //  " WHERE ListedDrCode='" + doc_code + "' AND sf_Code= '" + sf_code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and ListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Priya
        public int RecordCount(string sf_code, string Terr)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and Territory_Code='" + Terr + "' and ListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        public int RecordUpdate(string ListedDrCode, string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
            string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
            string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
            string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
            string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName)
        {
            int iReturn = -1;

            if (!RecordExist(ListedDrCode, Listed_DR_Name, sf_code))
            {

                try
                {

                    DB_EReporting db = new DB_EReporting();
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = " update Mas_ListedDr set ListedDr_Name = '" + Listed_DR_Name + "', ListedDr_Sex ='" + DR_Sex + "', ListedDr_DOB='" + DR_DOB + "',Doc_QuaCode='" + DR_Qual + "', " +
                           " ListedDr_DOW ='" + DR_DOW + "', Doc_Special_Code = '" + DR_Spec + "',ListedDr_RegNo ='" + DR_RegNo + "', Doc_Cat_Code='" + DR_Catg + "', Territory_Code='" + DR_Terr + "', " +
                           " ListedDr_Comm ='" + DR_Comm + "', Doc_ClsCode ='" + DR_Class + "', ListedDr_Address1 = '" + DR_Address1 + "', ListedDr_Address2 = '" + DR_Address2 + "', ListedDr_Address3='" + DR_Address3 + "', " +
                           " State_Code='" + DR_State + "', ListedDr_Pin ='" + DR_Pin + "', ListedDr_Mobile='" + DR_Mobile + "', ListedDr_Phone='" + DR_Phone + "', ListedDr_EMail='" + DR_EMail + "', ListedDr_Profile='" + DR_Profile + "', " +
                           " ListedDr_Visit_Days='" + DR_Visit_Days + "',ListedDr_DayTime='" + DR_DayTime + "', ListedDr_IUI='" + DR_IUI + "', ListedDr_Avg_Patients='" + DR_Avg_Patients + "', ListedDr_Hospital='" + DR_Hospital + "', ListedDr_Class_Patients='" + DR_Class_Patients + "', ListedDr_Consultation_Fee='" + DR_Consultation_Fee + "', " +
                           " ListedDr_Created_Date=getdate(),  ListedDR_Sl_No='" + ListedDrCode + "', LastUpdt_Date=getdate(), Hospital_Address='" + Hospital_Address + "', SLVNo = '" + ListedDrCode + "', Doc_Cat_ShortName = '" + Cat_SName + "', Doc_Spec_ShortName = '" + Spec_SName + "', Doc_Class_ShortName = '" + Cls_SName + "', Doc_Qua_Name = '" + Qua_SName + "' " +
                           " where ListedDr_Active_Flag=0 and ListedDrCode='" + ListedDrCode + "'";


                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }


        public int RecordUpdate_customer(string ListedDrCode, string Listed_DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
          string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
          string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
          string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
          string sf_code, string Hospital_Address, string Cat_SName, string Spec_SName, string Cls_SName, string Qua_SName)
        {
            int iReturn = -1;

            if (!RecordExist(ListedDrCode, Listed_DR_Name, sf_code))
            {

                try
                {

                    DB_EReporting db = new DB_EReporting();
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = " update Mas_ListedDr set ListedDr_Name = '" + Listed_DR_Name + "', ListedDr_Sex ='" + DR_Sex + "', ListedDr_DOB='" + DR_DOB + "',Doc_QuaCode='" + DR_Qual + "', " +
                           " ListedDr_DOW ='" + DR_DOW + "', Doc_Special_Code = '" + DR_Spec + "',ListedDr_RegNo ='" + DR_RegNo + "', Doc_Cat_Code='" + DR_Catg + "', Territory_Code='" + DR_Terr + "', " +
                           " ListedDr_Comm ='" + DR_Comm + "', Doc_ClsCode ='" + DR_Class + "', ListedDr_Address1 = '" + DR_Address1 + "', ListedDr_Address2 = '" + DR_Address2 + "', ListedDr_Address3='" + DR_Address3 + "', " +
                           " State_Code='" + DR_State + "', ListedDr_Pin ='" + DR_Pin + "', ListedDr_Mobile='" + DR_Mobile + "', ListedDr_Phone='" + DR_Phone + "', ListedDr_EMail='" + DR_EMail + "', ListedDr_Profile='" + DR_Profile + "', " +
                           " ListedDr_Visit_Days='" + DR_Visit_Days + "',ListedDr_DayTime='" + DR_DayTime + "', ListedDr_IUI='" + DR_IUI + "', ListedDr_Avg_Patients='" + DR_Avg_Patients + "', ListedDr_Hospital='" + DR_Hospital + "', ListedDr_Class_Patients='" + DR_Class_Patients + "', ListedDr_Consultation_Fee='" + DR_Consultation_Fee + "', " +
                           " ListedDr_Created_Date=getdate(),  ListedDR_Sl_No='" + ListedDrCode + "', LastUpdt_Date=getdate(), Hospital_Address='" + Hospital_Address + "', SLVNo = '" + ListedDrCode + "', Doc_Cat_ShortName = '" + Cat_SName + "', Doc_Spec_ShortName = '" + Spec_SName + "', Doc_Class_ShortName = '" + Cls_SName + "', Doc_Qua_Name = '" + Qua_SName + "' " +
                           " where ListedDr_Active_Flag=0 and ListedDrCode='" + ListedDrCode + "'";


                    iReturn = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }
        //Changes done by Saravanan
        public DataTable getListedDrforTerr_Datatable(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataTable getListedDrforSpl_Datatable(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName , d.Doc_Qua_Name,  " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        public DataTable getListedDrforCat_Datatable(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataTable getListedDrforQual_Datatable(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataTable getListedDrforClass_Datatable(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name , " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public DataTable getListedDrforName_Datatable(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName ,d.Doc_Qua_Name,  " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                          " (select max(distinct convert(varchar(10),b.Activity_Date,103))  Activity_Date  from " +
                        " DCRMain_Trans b, DCRDetail_Lst_Trans c " +
                        " where b.Trans_SlNo = c.Trans_SlNo and d.ListedDrCode=c.Trans_Detail_Info_Code " +
                        " and b.Sf_Code='" + sfcode + "' and Month(b.Activity_Date) = Month(getdate()) and YEAR(b.Activity_Date)=YEAR(GETDATE())) " +
                        " as   Activity_Date   FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        public DataTable getDoctorlistAlphabet_Datatable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName, d.Doc_Class_ShortName ,d.Doc_Qua_Name, d.SDP as Activity_Date, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " AND LEFT(ListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        public DataSet getListeddr_Alphabet(string ddlvar, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = " SELECT '' as Sf_Code, '--Select--' as Sf_Name" +
                     " UNION " +

                     " select Sf_Code,Sf_Name from Mas_Salesforce " +
                     " where    LEFT(Sf_Name,1) = '" + ddlvar + "'and sf_TP_Active_Flag = 0 and Division_Code='" + div_code + "'";


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
        public DataSet getListeddr_Alphabet1(string ddlvar, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            // strQry = " SELECT '' as Stockist_Code, '--Select--' as Stockist_Name" +
            //        " UNION " +

            //       " select Stockist_Code,Stockist_Name from Mas_Stockist " +
            //       " where    LEFT(Stockist_Name,1) = '" + ddlvar + "'and Stockist_Active_Flag = 0 and Division_Code='" + div_code + "'";

            strQry = " SELECT '' as Sf_Code, '--Select--' as Sf_Name" +
                    " UNION " +

                    " select Sf_Code,Sf_Name from Mas_Salesforce " +
                    " where    LEFT(Sf_Name,1) = '" + ddlvar + "'and sf_TP_Active_Flag = 0 and Division_Code='" + div_code + "'";


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
        public DataSet getListedDr_Add_approve(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and t.sf_code = '" + sfcode + "' and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE " +
                        " d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and  d.sf_code = '" + sfcode + "'" +
                        "and d.ListedDr_Active_Flag = 2  and d.SLVNo not in " +
                        " (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 )";
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

        public int Reject_Approve(string sf_code, string dr_code, int iVal, int oVal, string sf_name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Reject_Flag = 'AR', Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and listeddr_active_flag = " + oVal + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getSlNO(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = " SELECT ROW_NUMBER() over (ORDER BY ListedDr_Name) AS SlNO" +
                     "  FROM  Mas_ListedDr where ListedDr_Active_Flag=0 and Sf_Code='" + Sf_Code + "'";

            try
            {
                dsProCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }
        public int Single_Multi_Select_Territory(string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select SingleDr_WithMultiplePlan_Required  from Admin_Setups where division_code = '" + div_code + "'";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet ViewListedDr_DobDow(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                    //" (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name," +
                          " d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                          " WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                          " and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code" +
                          " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                    // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            " and DAY(ListedDr_DOB)='" + Date + "' and DAY(ListedDr_DOW)='" + Date + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code" +
                            " order by sf_name";
            }
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
        public DataSet ViewListedDr_Dow(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                    // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code " +
                            " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                    // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                      " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                      " from Mas_ListedDr d, mas_salesforce s " +
                        "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOW) = '" + ddlmonth + "' and DAY(ListedDr_DOW)='" + Date + "'" +
                        "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code " +
                        " order by sf_name";
            }
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
        public DataSet ViewListedDr_Dob(string Sf_Code, string ddlmonth, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (Date == "")
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name,  " +
                    //  " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                          " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                          " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                          " from Mas_ListedDr d, mas_salesforce s " +
                            "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "'  " +
                            "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code " +
                            " order by sf_name";
            }
            else
            {
                strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Address1,s.sf_name,s.Sf_HQ, s.sf_Designation_Short_Name, " +
                    // " (select Sf_Name from mas_salesforce where sf_code = d.Sf_code) sf_name," +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,d.Territory_Code, d.ListedDr_Phone, d.ListedDr_Mobile, " +
                     " convert(char(11),ListedDr_DOB,106) ListedDr_DOB, convert(char(11),ListedDr_DOW,106)ListedDr_DOW" +
                     " from Mas_ListedDr d, mas_salesforce s " +
                       "WHERE d.Sf_Code =  '" + Sf_Code + "' and MONTH(ListedDr_DOB) = '" + ddlmonth + "' and DAY(ListedDr_DOB)='" + Date + "' " +
                       "and d.ListedDr_Active_Flag = 0 and s.sf_code = d.Sf_code " +
                       " order by sf_name";
            }
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
        public DataSet Speciality_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + div_code + "' AND doc_special_active_flag=0 ";
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

        public DataSet Category_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_Cat_Code,Doc_Cat_SName,Doc_Cat_Name " +
                     " FROM  Mas_Doctor_Category where division_Code = '" + div_code + "' AND doc_cat_active_flag=0 ";
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


        public DataSet Terr_doc(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_code = '" + sf_code + "' " +
                     " AND division_Code = '" + div_code + "' AND territory_active_flag=0 ";
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
        public DataSet Load_Territory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";

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

        public DataSet Load_Territory_catg(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                     " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 " +
                     " UNION " +
                      " SELECT 999 as Territory_Code,'Missed DRs' as Territory_Name ";


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
        // changes done by priya
        public DataSet GetCategory_Special_Code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Stockist_code,Field_Code from Mas_Stockist where Stockist_Code='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "' ";

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
        public DataSet GetProduct_Category_Code(string Doc_Special_Name, string div_code, string Sub_Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //and Product_Cat_Div_Code = '" + Sub_Div + "'
            strQry = " select Product_Cat_Code,Product_Cat_Name from Mas_Product_Category where Product_Cat_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "'and Product_Cat_Active_Flag=0";


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


        public DataSet GetProduct_Brand_Code(string Doc_Special_Name, string div_code, string Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Product_Brd_Code,Product_Brd_Name from Mas_Product_Brand where Product_Brd_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "'and Product_Brd_Active_Flag=0";

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
        public DataSet GetProductBaseUOM(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Move_MailFolder_Id,Move_MailFolder_Name from Mas_Multi_Unit_Entry where Move_MailFolder_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "'and Folder_Act_flag=0";

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
        public DataSet GetProductState(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select State_Code from Mas_Division where Division_Code='" + div_code + "'and Division_Active_Flag=0";

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
        public DataSet GetTerritory_Code(string Territory_Name, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Territory_Code from Mas_Territory_Creation where Territory_Name='" + Territory_Name + "'  and sf_code='" + sf_code + "'";

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

        public DataSet GetDoc_Cat_Code(string Cat_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Product_Detail_Code='" + Cat_Name + "' and Division_Code = '" + div_code + "'";

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
        public int GetListedDrCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Tran_Slno)+1,'1') Tran_Slno from Trans_Stock_Updation_Details";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet GetProduct_Sl_No(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT isnull(max(Prod_Detail_Sl_No)+1,'1') Prod_Detail_Sl_No from Mas_Product_Detail";

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

        public DataSet GetDist_Sl_No(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT isnull(max(cast(Distributor_Code as numeric))+1,'1') Distributor_Code from Mas_Stockist";

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
        public DataSet GetTerr_Sl_No(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT isnull(max(cast(Territory_Code as numeric))+1,'1') Territory_Code from Mas_Territory_Creation";

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
        public DataSet GetRetail_Sl_No(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT isnull(max(cast(ListedDrCode as numeric))+1,'1') ListedDrCode from Mas_ListedDr";

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

        public DataSet GetProduct_Detail_Code(string DivshName, string SuDivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //int Prod_Detail_Sl_No = -1;
            strQry = "SELECT isnull(max(Prod_Detail_Sl_No)+1,'1') Prod_Detail_Sl_No from Mas_Product_Detail";

            dsListedDR = db_ER.Exec_DataSet(strQry);
           // strQry = "SELECT '" + Prod_Detail_Sl_No + "'  from Mas_Product_Detail where Prod_Detail_Sl_No= (SELECT max(Prod_Detail_Sl_No)  from Mas_Product_Detail)";

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

        public DataSet GetDist_Detail_Code(string DivshName, string SuDivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            int Dist_Detail_Sl_No = -1;
            strQry = "SELECT isnull(max(cast(Distributor_Code as numeric))+1,'1') Distributor_Code from Mas_Stockist";

            //Dist_Detail_Sl_No = db_ER.Exec_Scalar(strQry);
            //strQry = "SELECT '" + Dist_Detail_Sl_No + "'  from Mas_Stockist where Distributor_Code= (SELECT max(cast(Distributor_Code as numeric))from Mas_Stockist)";

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
        public DataSet GetTerr_Detail_Code(string DivshName, string SuDivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            int Dist_Detail_Sl_No = -1;
            strQry = "SELECT isnull(max(cast(Territory_Code as numeric))+1,'1') Territory_Code from Mas_Territory_Creation";

            Dist_Detail_Sl_No = db_ER.Exec_Scalar(strQry);
            strQry = "SELECT '" + DivshName + Dist_Detail_Sl_No + "'  from Mas_Territory_Creation where Territory_Code= (SELECT max(cast(Territory_Code as numeric))from Mas_Territory_Creation)";

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
        public DataSet GetRetailer_Detail_Code(string DivshName, string SuDivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            int Dist_Detail_Sl_No = -1;
            strQry = "SELECT isnull(max(cast(ListedDrCode as numeric))+1,'1') ListedDrCode from Mas_ListedDr";

            Dist_Detail_Sl_No = db_ER.Exec_Scalar(strQry);
            strQry = "SELECT '" + Dist_Detail_Sl_No + "'";//  from Mas_ListedDr where ListedDrCode= (SELECT max(cast(ListedDrCode as numeric))from Mas_ListedDr)";

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
        public int IsExistingDoctor(string doctorname)//,int territory,int speciality,int category)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count(ListedDrCode) from Mas_listedDr where ListedDr_Name='" + doctorname + "'";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // Changes done by Priya
        public int Update_LdDoctorSno(string Listed_DR_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET ListedDr_Sl_No = '" + Sno + "', SLVNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + Listed_DR_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Convert_ListedDr(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag=0 , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //Convert Unlist to List --Changes done by Priya
        public int ConvertDoctors(string ListedDrCode, string sf_code)
        {
            int iReturn = -1;


            try
            {

                DB_EReporting db = new DB_EReporting();

                int Listed_DR_Code = -1;

                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
                         " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
                         " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
                         " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " Select '" + Listed_DR_Code + "'  as ListedDrCode, SF_Code, UnListedDr_Name as ListedDr_Name,UnListedDr_Address1 as ListedDr_Address1, " +
                         " UnListedDr_Phone as ListedDr_Phone,UnListedDr_Mobile as ListedDr_Mobile,UnListedDr_Email as ListedDr_Email, " +
                         " UnListedDr_DOB as ListedDr_DOB, UnListedDr_DOW as ListedDr_DOW, a.Doc_Special_Code, " +
                         " a.Doc_Cat_Code, a.Territory_Code, a.Doc_QuaCode as Doc_QuaCode,UnListedDr_Active_Flag as ListedDr_Active_Flag,UnListedDr_Created_Date as ListedDr_Created_Date, " +
                         " UnListedDr_Deactivate_Date as ListedDr_Deactivate_Date, '" + Listed_DR_Code + "' as ListedDr_Sl_No,UnListedDr_Special_No as ListedDr_Special_No,a.Division_Code as Division_Code," +
                         " '" + Listed_DR_Code + "' as SLVNo,a.Doc_ClsCode as Doc_ClsCode,a.LastUpdt_Date,visit_days,Visit_Hours, " +
                         " c.Doc_Cat_SName as Doc_Cat_ShortName, s.Doc_Special_SName as Doc_Spec_ShortName, cl.Doc_ClsSName as Doc_Class_ShortName, q .Doc_QuaName as Doc_Qua_Name" +
                         " from Mas_UnListedDr a, Mas_Doctor_Category c, Mas_Doctor_Speciality s, Mas_Doc_Class cl, Mas_Doc_Qualification q where UnListedDrCode ='" + ListedDrCode + "' and Sf_Code= '" + sf_code + "' " +
                         " and a.Doc_Cat_Code = c.Doc_Cat_Code and a.Doc_Special_Code = s.Doc_Special_Code and " +
                         " a.Doc_ClsCode = cl.Doc_ClsCode and a.Doc_QuaCode = q.Doc_QuaCode ";


                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=1,Transfered_date=getdate() where UnListedDrCode ='" + ListedDrCode + "' and sf_code = '" + sf_code + "'";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getListedDr_map(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                     " d.ListedDr_Sl_No,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName, d.Doc_Class_ShortName as Doc_ClsSName, d.Doc_Qua_Name as Doc_QuaName, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        //Doctor type

        public int Update_doctype(string doc_code, string doc_type)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_Type = '" + doc_type + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + doc_code + "' and ListedDr_Active_Flag = 0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Campaign

        public DataSet getListedDr_Camp(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, d.Doc_SubCatCode, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where Division_code='" + div_code + "' and charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName   FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0 and d.Division_code='" + div_code + "'";


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
        public int Map_Campaign(string Sub_Cat_code, string doc_code, string div_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Doc_SubCatCode = '" + Sub_Cat_code + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE ListedDrCode = '" + doc_code + "' and ListedDr_Active_Flag = 0 ";
                //strQry = "EXEC SaveCampaign '" + Sub_Cat_code + "', '" + doc_code + "','" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet get_Camp(string ListedDrCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select ListedDrCode,ListedDr_Name,Doc_SubCatCode from mas_listeddr " +
                     " where ListedDrCode =  '" + ListedDrCode + "' and Division_code = '" + div_code + "' ";


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

        //Campaign Search

        public DataSet getListedDrforSpl_Camp(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                       " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        " and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforCat_Camp(string sfcode, string CatCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, " +
                       " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name ,s.Doc_Special_SName,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                      " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                      " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforQual_Camp(string sfcode, string QuaCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforClass_Camp(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                      " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                     " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName , " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name,  " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.ListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforTerr_Camp(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.Territory_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      "c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName, dc.Doc_ClsName, dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
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
        public DataSet getListedDrforName_Camp(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name," +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                       " c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName,  " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') +', ' Doc_SubCatName FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "'  " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.ListedDr_Name like '" + Name + "%'" +
                        " and d.ListedDr_Active_Flag = 0";
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

        public DataSet FetchCampName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Doc_SubCatCode , Doc_SubCatName  " +
                     " FROM  Mas_Doc_SubCategory  where Doc_SubCat_ActiveFlag=0 and Division_code='" + div_code + "'";

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
        public DataSet FetchCamp_Name(string Doc_SubCatCode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Doc_SubCatCode , Doc_SubCatName  " +
                     " FROM  Mas_Doc_SubCategory  where Doc_SubCatCode = '" + Doc_SubCatCode + "' and Doc_SubCat_ActiveFlag=0 and Division_code='" + div_code + "'";

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
        //Changes done by Priya

        public DataSet GetClass_Code(string Doc_ClsName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Doc_ClsCode,Doc_ClsSName from Mas_Doc_Class where Doc_ClsName='" + Doc_ClsName + "' and Division_Code = '" + div_code + "'";

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
        public DataSet Class_doc(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " SELECT Doc_ClsCode,Doc_ClsSName,Doc_ClsName " +
                     " FROM  Mas_Doc_Class where division_Code = '" + div_code + "' AND Doc_Cls_ActiveFlag=0 ";
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

        public DataSet Deact_Visitdr(string sf_code, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //strQry ="select distinct c.ListedDr_Name,'' ListedDrCode,'' ListedDr_Sl_No, " +
            //         " ''Doc_Cat_Name,'' Doc_Cat_SName,'' Doc_Special_Name,'' Doc_Special_SName,'' Doc_ClsName,''Doc_ClsSName " +
            //         " ,'' Doc_QuaName,b.Activity_Date,'' territory_Name, a.sf_code,a.Trans_Detail_Info_Type,a.Trans_Detail_Info_Code "+
            ////strQry = "select a.sf_code,a.Trans_Detail_Info_Type,a.Trans_Detail_Info_Code, " +
            //        // " c.ListedDr_Name,b.Activity_Date,'' ListedDrCode 
            //         " from DCRDetail_Lst_Trans a, DCRMain_Trans b,Mas_ListedDr c " +
            //         " where  Month(Activity_Date) = Month(getdate()) and YEAR(Activity_Date)=YEAR(GETDATE()) " +
            //         "  and a.Trans_SlNo = b.Trans_SlNo and c.ListedDrCode = a.Trans_Detail_Info_Code and c.sf_code='" + sf_code + "'  and c.ListedDrCode ='" + ListedDrCode + "'";


            strQry = " select distinct convert(varchar(10),b.Activity_Date,103) Activity_Date  from DCRDetail_Lst_Trans a, " +
                     " DCRMain_Trans b  where  Month(b.Activity_Date) = Month(getdate()) " +
                     " and YEAR(b.Activity_Date)=YEAR(GETDATE())   and a.Trans_SlNo = b.Trans_SlNo and " +
                     " a.sf_code='" + sf_code + "'  and a.Trans_Detail_Info_Code ='" + ListedDrCode + "' ";
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

        //Changes done by Priya

        public DataSet getLi_Deactivate(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName ,dc.Doc_ClsName,dc.Doc_ClsSName ,g.Doc_QuaName, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.Doc_Special_Code = s.Doc_Special_Code " +
                    "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                    " and d.Doc_QuaCode = g.Doc_QuaCode " +
                    "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        public int RecordCount_Transfer(string sf_code, string terr_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "' and Territory_Code ='" + terr_code + "' and ListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //changes done by Priya

        public DataTable getListedDrforTerr_Trans(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, " +
                  " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +
                      "c.Doc_Cat_Name,c.Doc_Cat_SName,s.Doc_Special_Name,s.Doc_Special_SName, dc.Doc_ClsName, dc.Doc_ClsSName ,g.Doc_QuaName, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                         " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'') Doc_SubCatName, '' color FROM " +
                        "Mas_ListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                        " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '" + TerrCode + "') " +
                        "and d.ListedDr_Active_Flag = 0";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        public DataSet GetTerritory_Upload(string Territory_Name, string SF_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Territory_Code from Mas_Territory_Creation where Territory_Name='" + Territory_Name + "' and SF_Code = '" + SF_Code + "' and Division_Code = '" + div_code + "' and Territory_Active_Flag = 0 ";

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
        //Changes done by Priya
        public int Transfer_Doctor(string Doc_Code, string terr_code, string sf_code, string trans_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Update Mas_ListedDr " +
                         " Set Territory_Code ='" + terr_code + "', sf_code = '" + trans_Code + "', Transfer_MR_Listeddr = getdate()" +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet doc_dob(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_doctor_dob '" + sf_code + "', '" + dmonth + "', '" + ddate + "', '" + Div_code + "'  ";
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
        public DataSet doc_dow(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_doctor_dow '" + sf_code + "', '" + dmonth + "', '" + ddate + "' , '" + Div_code + "' ";
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
        public DataSet doc_dob_dow(string sf_code, string dmonth, string ddate, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "EXEC sp_get_Rep_dob_dow '" + sf_code + "', '" + dmonth + "', '" + ddate + "', '" + Div_code + "'  ";
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

        public DataSet getterrcode()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "SELECT isnull(max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

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
        public DataSet GetDoc_Qua_Code(string Doc_QuaName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = "select Doc_QuaCode from Mas_Doc_Qualification where Doc_QuaName='" + Doc_QuaName + "'";

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
        public DataSet GetQua_Upload(string Doc_QuaName, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select Doc_QuaCode,Doc_QuaName from Mas_Doc_Qualification where Doc_QuaName='" + Doc_QuaName + "' and Division_Code = '" + div_code + "' ";

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

        //changes done by Reshmi
        public int RecordAddLDr(string code, string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Address1, string Listed_DR_Spec, string Listed_DR_Class, string sf_code, string DoSpecSName, string DoClaSName, int iflag, string MobileNo, string Contactperson, string Creditdays, string route)
        {
            int iReturn = -1;

            if (!sRecordExist(code, Listed_DR_Name, code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = "select Division_Code from Mas_Stockist where Territory_Code='" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);


                    strQry = "insert into Mas_ListedDr (code,ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Address2,Doc_Special_Code, " +
                             "  ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone, " +
                             " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Spec_ShortName,Doc_Class_ShortName,ListedDr_Mobile,Credit_Days,Contact_Person_Name,Territory_Code) " +
                             " VALUES('" + code + "','" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Address1 + "','" + Listed_DR_Spec + "', " +
                             " '" + iflag + "', getdate(), '" + Division_Code + "','','', " +
                             " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + DoSpecSName + "','" + DoClaSName + "','" + MobileNo + "','" + Creditdays + "','" + Contactperson + "','" + route + "')";


                    iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        //Insert a record into LstDoctor_Terr_Map_History table
                        //strQry = "SELECT isnull(max(SNo)+1,'1') SNo from LstDoctor_Terr_Map_History ";
                        //int SNo = db.Exec_Scalar(strQry);

                        //strQry = "insert into LstDoctor_Terr_Map_History values('" + SNo + "','" + sf_code + "',  '" + Listed_DR_Code + "', " +
                        //          " '" + Listed_DR_Terr + "',getdate(),getdate(), '" + Division_Code + "')";

                        //iReturn = db.ExecQry(strQry);

                        //if (Listed_DR_Terr.IndexOf(",") != -1)
                        //{
                        //    string[] subterr;
                        //    subterr = Listed_DR_Terr.Split(',');
                        //    foreach (string st in subterr)
                        //    {
                        //        if (st.Trim().Length > 0)
                        //        {
                        //            strQry = "SELECT ISNULL(MAX(Plan_No),0)+1 FROM Call_Plan ";
                        //            int iPlanNo = db.Exec_Scalar(strQry);

                        //            strQry = "insert into Call_Plan values('" + sf_code + "', '" + Convert.ToInt32(st) + "', getdate(), '" + iPlanNo + "', " +
                        //                    " '" + Listed_DR_Code + "', '" + Division_Code + "', 0,'')";

                        //            iReturn = db.ExecQry(strQry);
                        //        }
                        //    }
                        //}
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;
        }

        public DataSet getListedDr_new(string sfcode, string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date," +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    " Mas_ListedDr d " +
                    " WHERE d.TERRCODE =  '" + sfcode + "' and d.Territory_Code='" + Terr_code + "'" +
                    " and d.ListedDr_Active_Flag = 0" +
                    " order by d.ListedDr_Name";

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
        //Changes done by priya

        public DataSet getListedDrdeativate(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.Doc_Class_ShortName, d.Doc_Qua_Name,d.SDP as Activity_Date, " +

                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
                    "Mas_ListedDr d " +
                    "WHERE d.Sf_Code =  '" + sfcode + "'" +
                    "and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";

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

        // Sorting For ListedDR List 
        public DataTable getListedDoctorListNew_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,d.Doc_Class_ShortName ,d.Doc_Qua_Name,d.SDP as Activity_Date, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +

                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        //Changes done by Priya

        public DataSet getListedDr_RejectList(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = " select convert(varchar(10),COUNT(ListedDrCode)) LSTCount,Listeddr_App_Mgr, " +
                     " case Reject_Flag when 'AR' then 'Addition' " +
                     " when 'DR' then 'Deactivation' end  mode  from Mas_ListedDr where Reject_Flag = 'AR' and Sf_Code='" + sfcode + "' " +
                     " and ListedDr_Active_Flag = 4 group by Reject_Flag, Listeddr_App_Mgr " +
                     " union " +
                     " select convert(varchar(10),COUNT(ListedDrCode)) LSTCount,Listeddr_App_Mgr, " +
                     " case Reject_Flag " +
                     " when 'AR' then 'Addition'  " +
                     " when 'DR' then 'Deactivation' end  mode from Mas_ListedDr where Reject_Flag = 'DR' and Sf_Code='" + sfcode + "'  " +
                     " and ListedDr_Active_Flag=0 " +

                     " group by Reject_Flag, Listeddr_App_Mgr ";
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


        //changes done by Reshmi
        public DataSet getListDr_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select No_Of_Sl_DoctorsAllowed from Admin_Setups where Division_Code='" + div_code + "'";
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

        public DataSet getListDr_Count(string sf_code, string div_code, string Terr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = strQry = "SELECT COUNT(ListedDrCode) FROM Mas_ListedDr WHERE Sf_Code = '" + sf_code + "'and Territory_Code='" + Terr + "' and Division_Code ='" + div_code + "' and (ListedDr_Active_Flag = 0 or ListedDr_Active_Flag = 2)";


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
        //Changes done by Priya
        public DataSet getListedDr_TerritoryName(string TerritoryName, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select Territory_Code from Mas_Territory_Creation where Territory_Name='" + TerritoryName + "'  and Territory_Active_Flag = 0 and sf_code='" + sf_code + "'";
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

        public DataSet getListDr_allow_app(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select Doc_App_Needed from Admin_Setups where Division_Code='" + div_code + "'";
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

        public DataSet getListedDr_adddeact(string sf_code, int val1, int val2, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct b.Sf_Code,b.Sf_Name, b.Sf_HQ, b.sf_Designation_Short_Name " +
                   " from mas_listeddr a, Mas_Salesforce b,Mas_Salesforce_AM c  " +
                   " where c.LstDr_AM = '" + sf_code + "' and  a.Sf_Code = b.Sf_Code " +
                   " and b.Sf_Code=c.Sf_Code and  a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2  and Division_code = '" + div_code + "') and " +
                   " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  and Division_code = '" + div_code + "' ) and a.Division_code in('" + div_code + "') ";
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

        public DataSet getListedDr_addAgainst(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT  d.ListedDrCode,d.ListedDr_Name, max(d.SLVNo) as SLVNo,d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name, " +
                   " stuff((select ', '+territory_Name from Mas_Territory_Creation t where " +
                   " t.SF_Code = d.Sf_Code and t.sf_code =  '" + sfcode + "' and " +
                   " charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                   " case d.ListedDr_Active_Flag when '2' then 'Addition' when '3' then 'Deactivation' end mode, d.ListedDr_Active_Flag " +
                   " FROM Mas_ListedDr d WHERE d.sf_code =  '" + sfcode + "' and  " +
                   " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2 ) and " +
                   " d.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3 ) and d.ListedDr_Active_Flag !=0 and d.ListedDr_Active_Flag !=1  " +
                   " group by SLVNo,d.ListedDrCode,d.ListedDr_Name,d.Territory_Code, d.Sf_Code,d.ListedDr_Active_Flag, d.Doc_Cat_ShortName, d.Doc_Spec_ShortName,d.Doc_Qua_Name  " +
                   " order by SLVNo, mode ";
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
        public int ApproveAddDeact(string sf_code, string dr_code, int iVal, string sf_name, string slvno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag= " + iVal + " , " +
                            " lastupdt_date = getdate(), Listeddr_App_Mgr = '" + sf_name + "' " +
                            " WHERE sf_code = '" + sf_code + "' and listeddrcode = '" + dr_code + "' and SLVNo = '" + slvno + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getDoc_Deact_Needed(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;

            strQry = "Select Doc_Deact_Needed from Admin_Setups where Division_Code='" + div_code + "'";
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
        //Changes done by Priya & Reshmi
        public int DeActivate_Dr(string dr_code, int iflag)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                            " SET listeddr_active_flag = '" + iflag + "' , " +
                            " listeddr_deactivate_date = getdate() " +
                            " WHERE listeddrcode = '" + dr_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getListDr_CountNew(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsListedDR = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT COUNT(ListedDrCode) as dr_count FROM Mas_ListedDr a inner join Mas_Stockist b on a.Sf_Code=b.SF_Code or b.Field_Code=a.Sf_Code WHERE b.Field_Code = '" + sf_code + "' and  a.Division_Code ='" + div_code + "' and ListedDr_Active_Flag = 0";


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

        public int GetListedDrSlNO()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //Changes done by Siva
        public DataSet getDoctorDetailsByName(string div_code, string sfCode, string DocName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_DCR_ListedDRByName '" + div_code + "','" + sfCode + "','" + DocName + "'";

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
        //Changes done by Nirmal 
        public DataSet GetDoctorBySearch(string prefixText, string sfCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where Sf_Code = '" + sfCode + "' AND ListedDr_Name like '" + prefixText + "%';";

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
        public DataTable getListedDoctorList_DataTable_camp(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Deactivate_Date,d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName,d.Doc_Qua_Name as Doc_QuaName, d.SDP as Activity_Date, " +
                        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name , " +
                        " stuff((select ', '+Doc_SubCatName from Mas_Doc_SubCategory dc where  " +
                        " charindex(cast(dc.Doc_SubCatCode as varchar)+',',d.Doc_SubCatCode+',')>0 for XML path('')),1,2,'')+', ' Doc_SubCatName  FROM " +

                        " Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' " +
                        "and d.ListedDr_Active_Flag = 0" +
                        " order by 2";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }
        //Changes done by Siva
        public DataSet getListedDoctorBySfCode(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT ListedDrCode, ListedDr_Name,ListedDr_Name +'|'+ISNULL(DC.Doc_Cat_Name,'')+'|'+ISNULL(DS.Doc_Special_Name,'')+'|'+ISNULL(MT.Territory_Name,'') AS DoctorDetails " +
                         " FROM Mas_ListedDr  ML" +
                            " LEFT OUTER JOIN Mas_Doctor_Category DC ON DC.Doc_Cat_Code = ML.Doc_Cat_Code" +
                         " LEFT OUTER JOIN Mas_Doctor_Speciality DS ON DS.Doc_Special_Code = ML.Doc_Special_Code" +
                            " LEFT OUTER JOIN (SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation WHERE SF_Code = '" + sfcode + "') MT " +
                         " ON Mt.Territory_Code = ML.Territory_Code WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0";
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
        public int RecordAdd3(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No, string Cat_SName, string Spec_SName, string Cls_SName, string Qual_SName)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Listed_DR_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //  strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_ListedDr (ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,Doc_Special_Code,Doc_QuaCode,Doc_Cat_Code, " +
                         " Territory_Code, ListedDr_Active_Flag, ListedDr_Created_Date, division_code,ListedDr_Phone,ListedDr_Mobile, " +
                         " ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Visit_Hours,visit_days,ListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', '" + Listed_DR_Qual + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', 2, getdate(), '" + Division_Code + "','','','', " +
                         " '','','','','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate(),'" + Cat_SName + "', '" + Spec_SName + "', '" + Cls_SName + "', '" + Qual_SName + "')";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -2;
            //}
            return iReturn;
        }
        public DataSet getListedDr_Product(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No, d.Doc_Cat_ShortName as Doc_Cat_SName,d.Doc_Spec_ShortName as Doc_Special_SName ,d.Doc_Class_ShortName as Doc_ClsSName , d.Doc_Qua_Name as Doc_QuaName, d.Doc_SubCatCode, d.Product_Detail_Code as Product_Code_SlNo, " +
                //" (select t.territory_Name FROM Mas_Territory_Creation t where t.Territory_Code like d.Territory_Code) territory_Name "+
                 " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.SF_Code = d.Sf_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name, " +
                //   " stuff((select ', '+Product_Detail_Name from Mas_Product_Detail dc where  charindex(cast(dc.Product_Code_SlNo as varchar)+',',d.Product_Detail_Code+',')>0 for XML path('')),1,2,'') +', ' Product_Detail_Name   FROM " +
                 " isnull((select ProductName+' ( '+CAST(Product_Priority as varchar)+' ), ' from " +
                 " vw_Doc_Prod where d.ListedDrCode=Listeddr_Code for xml path('')),'')  Product_Detail_Name from " +
                 "Mas_ListedDr d " +
                 "WHERE d.Sf_Code =  '" + sfcode + "'" +
                 "and d.ListedDr_Active_Flag = 0";


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
        public DataSet get_Prod(string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select Listeddr_Code,Doctor_Name,Product_Code as Product_Code_SlNo,Product_Priority from Map_LstDrs_Product " +
                     " where Listeddr_Code =  '" + ListedDrCode + "' ";


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
        public int RecordAdd_ProductMap(string Listeddr_Code, string Product_Code, string Product_Priority, string Product_Name, string Doctor_Name, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                int Sl_No = -1;


                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);



                if (DocProd_RecordExist(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product set Product_Code ='" + Product_Code + "',Product_Name='" + Product_Name + "', Product_Priority = '" + Product_Priority + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Priority,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + Product_Priority + "', '" + Product_Name + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate() )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool DocProd_RecordExist(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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
        public int Delete_ProductMap(string Listeddr_Code, string Product_Code, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                if (DocProd_RecordExist(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "delete from Map_LstDrs_Product  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getListedDrdeativate_MR(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "EXEC Listeddr_Deact '" + sfcode + "' ";

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

        public DataSet getListedDr_for_Mapp(string sfcode, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            //strQry = " SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
            //           " UNION " +
            //        " select ListedDrCode, d.ListedDr_Name + ' - '  + t.Territory_Name from Mas_ListedDr d,Mas_Territory_Creation t " +
            //        " where d.Territory_Code=CONVERT(varchar,t.Territory_Code) and d.Division_Code ='" + div_code + "' and d.Sf_Code='" + sfcode + "' " +
            //        " and ListedDr_Active_Flag =0 ";
            strQry = "SELECT 0 as ListedDrCode,'---Select---' as ListedDr_Name " +
                       " UNION all " +
                     " select d.ListedDrCode, d.ListedDr_Name + ' - '  + t.Territory_Name from Mas_ListedDr d,Mas_Territory_Creation t " +
                     " where  d.Division_Code ='" + div_code + "' and d.Sf_Code='" + sfcode + "' " +
                     " and d.ListedDr_Active_Flag =0 and (d.Territory_Code like '%,'+CONVERT(varchar,t.Territory_Code) or d.Territory_Code like CONVERT(varchar,t.Territory_Code)+',%' or " +
                     " d.Territory_Code like '%,'+CONVERT(varchar,t.Territory_Code)+',%' or  d.Territory_Code =CONVERT(varchar,t.Territory_Code)) ";


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

        public int RecordAdd_ProductMap_New(string Listeddr_Code, string Product_Code, string Doctor_Name, string Sf_Code, string Division_Code)
        {
            int iReturn = -1;
            DataSet dsPrd_Name;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Sl_No = -1;
                //string Product_Name = string.Empty;

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Map_LstDrs_Product ";
                Sl_No = db.Exec_Scalar(strQry);

                strQry = "select Product_Detail_Name  from Mas_Product_Detail where Division_Code='" + Division_Code + "' and Product_Code_SlNo='" + Product_Code + "' ";
                dsPrd_Name = db.Exec_DataSet(strQry);

                if (DocProd_RecordExist_New(Listeddr_Code, Sf_Code, Product_Code))
                {
                    strQry = "update Map_LstDrs_Product set Product_Code ='" + Product_Code + "',Product_Name='" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "'  " +
                        " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' and Product_Priority=1 ";
                }
                else
                {

                    strQry = " insert into Map_LstDrs_Product (Sl_No,Listeddr_Code,Product_Code,Product_Name,Doctor_Name, " +
                       " Sf_Code,Division_Code,Created_Date,Product_Priority) " +
                       " VALUES('" + Sl_No + "','" + Listeddr_Code + "', '" + Product_Code + "', '" + dsPrd_Name.Tables[0].Rows[0][0].ToString() + "', '" + Doctor_Name + "', '" + Sf_Code + "', " +
                       " '" + Division_Code + "',  getdate(),'1' )";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool DocProd_RecordExist_New(string Listeddr_Code, string Sf_Code, string Product_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Map_LstDrs_Product " +
                         " where Listeddr_Code = '" + Listeddr_Code + "' and Sf_Code ='" + Sf_Code + "' and Product_Code ='" + Product_Code + "' ";

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

        public DataSet getListedDrCount_MR(string div_code, string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            strQry = " select  distinct sf_code, " +
                    " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=0  and a.Sf_Code = d.Sf_Code and a.Territory_Code=d.Territory_Code) as doccnt, " +
                     " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=2 and a.Sf_Code = d.Sf_Code and a.Territory_Code=d.Territory_Code ) as docapp, " +
                     " (select count(ListedDrCode) from mas_listeddr a where ListedDr_Active_Flag=3 and a.Sf_Code = d.Sf_Code and a.Territory_Code=d.Territory_Code ) as deaapp, " +
                     " (select count(ListedDrCode) from mas_listeddr a where   a.Sf_Code = d.Sf_Code   and  a.Territory_Code=d.Territory_Code and " +
                     " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 2 ) and " +
                     " a.SLVNo in (select SLVNo from mas_listeddr where ListedDr_Active_Flag = 3  ) )  as addagain " +
                     " from Mas_ListedDr d where d.Division_code = '" + div_code + "' and d.Sf_Code='" + sfcode + "' and d.Territory_Code='" + terr_code + "' ";

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
        /*--------------Listed Doctor AutoFill Data--------------- */

        public DataSet GetListedDoctorAutoFill(string prefixText, string Div_code, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select ListedDr_Name from Mas_ListedDr where ListedDr_Name like '" + prefixText + "%' and Division_Code='" + Div_code + "' and Sf_Code='" + Sf_Code + "'";

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
        public DataTable getListedDoctorListNew_DT(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = " EXEC sp_Listeddr_Deact '" + sfcode + "' ";
            try
            {
                dtListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public int Interchange_MR(string from_sfcode, string to_sfcode, string from_name, string to_name, string div_code)
        {
            int iReturn = -1;

            try
            {
                int Listed_DR_Code = -1;
                int Listed_DR_Code1 = -1;
                int chemists_cd = -1;
                int chemists_cd1 = -1;
                int terr_New = -1;
                int Hospital_Cd = -1;
                int Hospital_Cd1 = -1;
                int UnListedDrCd = -1;

                int UnListedDrCd1 = -1;
                int terr_New1 = -1;
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                terr_New = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                chemists_cd = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital ";
                Hospital_Cd = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                UnListedDrCd = db.Exec_Scalar(strQry);



                strQry = "update Mas_Territory_Creation set Territory_Active_Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Territory_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                            " SF_Code,Territory_Active_Flag,Created_date,HQ_Code, Territory_Intechange) " +
                                            " Select '" + terr_New + "' - 1 + row_number() over (order by (select NULL)) as Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                            " '" + to_sfcode + "' as SF_Code,'0' as Territory_Active_Flag,getdate() as Created_date,HQ_Code, 'TT' as Territory_Intechange from Mas_Territory_Creation where Sf_Code= '" + from_sfcode + "'  and Territory_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);


                strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                terr_New1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                           " SF_Code,Territory_Active_Flag,Created_date,HQ_Code, Territory_Intechange) " +
                                           " Select '" + terr_New1 + "' - 1 + row_number() over (order by (select NULL)) as Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                           " '" + from_sfcode + "' as SF_Code, '0' as Territory_Active_Flag,getdate() as Created_date,HQ_Code, 'TT' as Territory_Intechange from Mas_Territory_Creation where Sf_Code= '" + to_sfcode + "'   and Territory_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Territory_Creation set Territory_Active_Flag=1 where  (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "')  and Territory_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update mas_Distance_Fixation_001 set Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into mas_Distance_Fixation_001 (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,Amount,division_code, " +
                    " Flag,Created_Date,Territory_Name,approve_flg,level1_flg,level2_flg) " +
                    " select '" + to_sfcode + "' as SF_Code,'" + to_sfcode + "' as From_Code," +
                    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                    " Territory_Code= a.To_Code)) as To_Code, " +
                    " Town_Cat,Distance_In_Kms,Amount,division_code, " +
                    " '0' as Flag,getdate() as Created_Date," +
                    " (select Territory_Name from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                    " Territory_Code= a.To_Code)) as Territory_Name " +
                    " ,approve_flg,level1_flg,level2_flg from mas_Distance_Fixation_001 a where Sf_Code= '" + from_sfcode + "' and Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into mas_Distance_Fixation_001 (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,Amount,division_code, " +
                 " Flag,Created_Date,Territory_Name,approve_flg,level1_flg,level2_flg) " +
                 " select '" + from_sfcode + "' as SF_Code,'" + from_sfcode + "' as From_Code," +
                 " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                 " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                 " Territory_Code= a.To_Code)) as To_Code, " +
                 " Town_Cat,Distance_In_Kms,Amount,division_code, " +
                 " '0' as Flag,getdate() as Created_Date," +
                  " (select Territory_Name from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                    " Territory_Code= a.To_Code)) as Territory_Name,approve_flg,level1_flg,level2_flg " +
                  " from mas_Distance_Fixation_001 a where Sf_Code= '" + to_sfcode + "' and Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "update mas_Distance_Fixation_001 set Flag=0 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "') and Flag=6";

                iReturn = db.ExecQry(strQry);


                strQry = "update Mas_ListedDr set ListedDr_Active_Flag=6 where (sf_code = '" + from_sfcode + "' or sf_code = '" + to_sfcode + "')  and ListedDr_Active_Flag=0";

                iReturn = db.ExecQry(strQry);


                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
             " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Interchange_Drs) " +
             " Select '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDrCode,'" + to_sfcode + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code, " +
                    //" select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                    //" Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and Territory_Code= a.territory_code) as territory_code " +
             " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
             " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
             " Territory_Code= a.territory_code)) as Territory_Code, " +

             " Doc_QuaCode,'0' as ListedDr_Active_Flag,getdate() as ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " '" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + Listed_DR_Code + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name, 'DI' Interchange_Drs " +
             " from Mas_ListedDr a where  Sf_Code= '" + from_sfcode + "' and ListedDr_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(ListedDrCode),0)+1 FROM Mas_ListedDr ";
                Listed_DR_Code1 = db.Exec_Scalar(strQry);

                strQry = " Insert into Mas_ListedDr(ListedDrCode,SF_Code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code,Territory_Code, " +
             " Doc_QuaCode,ListedDr_Active_Flag,ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " ListedDr_Sl_No,ListedDr_Special_No,Division_Code,SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name,Interchange_Drs) " +
             " Select '" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as ListedDrCode,'" + from_sfcode + "' as sf_code,ListedDr_Name,ListedDr_Address1,ListedDr_Phone, " +
             " ListedDr_Mobile,ListedDr_Email,ListedDr_DOB,ListedDr_DOW,Doc_Special_Code,Doc_Cat_Code, " +
             " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
             " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
             " Territory_Code= a.territory_code)) as Territory_Code, " +
             " Doc_QuaCode, '0' as ListedDr_Active_Flag,getdate() as ListedDr_Created_Date,ListedDr_Deactivate_Date, " +
             " '" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as ListedDr_Sl_No,ListedDr_Special_No,Division_Code,'" + Listed_DR_Code1 + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,Doc_ClsCode,LastUpdt_Date,visit_days,Visit_Hours,Doc_Cat_ShortName,Doc_Spec_ShortName,Doc_Class_ShortName,Doc_Qua_Name, 'DI' Interchange_Drs " +
             " from Mas_ListedDr a where  Sf_Code= '" + to_sfcode + "' and ListedDr_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_ListedDr set ListedDr_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "')   and ListedDr_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Chemists set Chemists_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Chemists_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
              " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code) " +
              " Select '" + chemists_cd + "' - 1 + row_number() over (order by (select NULL)) as Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact,  " +
              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
              " Territory_Code= a.territory_code)) as Territory_Code, " +
              " Division_Code, '" + to_sfcode + "' as Sf_Code, '0' as Chemists_Active_Flag, getdate() as Created_Date,Cat_Code " +
               " from Mas_Chemists a  where  Sf_Code= '" + from_sfcode + "'  and Chemists_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                chemists_cd1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
              " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code) " +
              " Select '" + chemists_cd1 + "' - 1 + row_number() over (order by (select NULL)) as Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact,  " +
              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
              " Territory_Code= a.territory_code)) as Territory_Code, " +
              " Division_Code, '" + from_sfcode + "' as Sf_Code, '0' as Chemists_Active_Flag, getdate() as Created_Date,Cat_Code " +
               " from Mas_Chemists a  where  Sf_Code= '" + to_sfcode + "'  and Chemists_Active_Flag=6 ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Chemists set Chemists_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Chemists_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Hospital set Hospital_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Hospital_Active_Flag=0";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact, Territory_Code, " +
                    " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                    " Select '" + Hospital_Cd + "' - 1 + row_number() over (order by (select NULL)) as Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact,  " +
                    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                    " Territory_Code= a.territory_code)) as Territory_Code, " +
                    " Division_Code, '" + to_sfcode + "' as Sf_Code, '0' as Hospital_Active_Flag, getdate() as Created_Date from Mas_Hospital a where  Sf_Code= '" + from_sfcode + "' and Hospital_Active_Flag=6 ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital ";
                Hospital_Cd1 = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact, Territory_Code, " +
                           " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                           " Select '" + Hospital_Cd1 + "' - 1 + row_number() over (order by (select NULL)) as Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact,  " +
                           " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                           " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                           " Territory_Code= a.territory_code)) as Territory_Code, " +
                           " Division_Code, '" + from_sfcode + "' as Sf_Code, '0' as Hospital_Active_Flag, getdate() as Created_Date from Mas_Hospital a where  Sf_Code= '" + to_sfcode + "' and Hospital_Active_Flag=6   ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Hospital set Hospital_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and Hospital_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=6 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and UnListedDr_Active_Flag=0";

                iReturn = db.ExecQry(strQry);



                strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                              " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                              " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                              " select '" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as UnListedDrCode,'" + to_sfcode + "' as SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                              " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                              " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                              " Territory_Code= a.territory_code)) as Territory_Code, " +
                              " Doc_QuaCode, '0' as UnListedDr_Active_Flag, getdate() as UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                              " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,'" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as UnListedDr_Sl_No,Doc_ClsCode,'" + UnListedDrCd + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,LastUpdt_Date " +
                              " from Mas_UnListedDr a where  Sf_Code= '" + from_sfcode + "' and  UnListedDr_Active_Flag =6";

                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                UnListedDrCd1 = db.Exec_Scalar(strQry);


                strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                                    " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                                    " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                                    " select '" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as UnListedDrCode,'" + from_sfcode + "' as SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                                    " (select Territory_Code from Mas_Territory_Creation where SF_Code='" + from_sfcode + "' and " +
                                    " Territory_Name= (select Territory_Name from  Mas_Territory_Creation where SF_Code='" + to_sfcode + "' and " +
                                    " Territory_Code= a.territory_code)) as Territory_Code, " +
                                    " Doc_QuaCode, '0' as UnListedDr_Active_Flag, getdate() as UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                                    " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,'" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as UnListedDr_Sl_No,Doc_ClsCode,'" + UnListedDrCd1 + "' - 1 + row_number() over (order by (select NULL)) as SLVNo,LastUpdt_Date " +
                                    " from Mas_UnListedDr a where  Sf_Code= '" + to_sfcode + "' and  UnListedDr_Active_Flag =6";

                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_UnListedDr set UnListedDr_Active_Flag=1 where (sf_code = '" + from_sfcode + "'  or sf_code = '" + to_sfcode + "') and UnListedDr_Active_Flag=6";

                iReturn = db.ExecQry(strQry);

                strQry = "insert into Mas_InterChange_Det (From_Sf_code, To_Sf_code, From_HQ, To_HQ, From_Name, To_Name, Division_code, Created_date)" +
                         " VALUES('" + from_sfcode + "', '" + to_sfcode + "', '', '','" + from_name + "','" + to_name + "','" + div_code + "',getdate() ) ";

                iReturn = db.ExecQry(strQry);


                strQry = "insert into Mas_InterChange_Det (From_Sf_code, To_Sf_code, From_HQ, To_HQ, From_Name, To_Name, Division_code, Created_date)" +
                         " VALUES('" + to_sfcode + "', '" + from_sfcode + "', '', '','" + to_name + "','" + from_name + "','" + div_code + "',getdate() ) ";

                iReturn = db.ExecQry(strQry);

                strQry = " select sf_hq INTO #HQ_Table1 from Mas_Salesforce where Sf_Code='" + from_sfcode + "' " +
                         " select sf_hq INTO #HQ_Table2 from Mas_Salesforce where Sf_Code='" + to_sfcode + "' " +
                         " update Mas_Salesforce set sf_hq=(select * from #HQ_Table2) where Sf_Code='" + from_sfcode + "' " +
                         " update Mas_Salesforce set sf_hq=(select * from #HQ_Table1) where Sf_Code='" + to_sfcode + "'" +
                         " drop table #HQ_Table1 " +
                         " drop table #HQ_Table2 ";

                iReturn = db.ExecQry(strQry);




            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // Dist upload
        public DataSet GetDist_Territory(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Territory_code,Territory_name from Mas_Territory where Territory_name like '" + Doc_Special_Name + "' and Div_Code = '" + div_code + "' ";

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

        public DataSet GetDist_Dis_Code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Dist_code,Dist_name from Mas_District where Dist_name like '%" + Doc_Special_Name + "%' and Div_Code = '" + div_code + "' ";

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

        public DataSet GetDis_Sta(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select State_Code,StateName from Mas_State where StateName like '%" + Doc_Special_Name + "%'";

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

        public DataSet GetDis_Ex_name(string Doc_Special_Name, string div_code, string Territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where Sf_Name like '" + Doc_Special_Name + "' and Territory_Code='" + Territory_code + "' and Division_Code = '" + div_code + ',' + "'and sf_type='1' and SF_Status=0";

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
        //Terr Upload
        public DataSet GetTerr_Dis(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "' ";

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

        public DataSet GetTerr_DSM(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select DSM_code,DSM_name from Mas_DSM where DSM_name='" + Doc_Special_Name + "'and Div_code = '" + div_code + "' ";

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


        public DataSet GetDis_Ex_name_terr(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where Sf_Name like '" + Doc_Special_Name + "' and Division_Code = '" + div_code + ',' + "'and sf_type='1' and SF_Status=0";

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

        public DataSet GetTerr_Town(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Town_Code,Town_Name from Mas_DSM where Distributor_Code='" + Doc_Special_Name + "' and Div_code = '" + div_code + "' ";

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
        public DataSet GetTerr_Terr(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Territory_Code,Territory from Mas_Salesforce where Sf_Code='" + Doc_Special_Name + "'";

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
        //Retailer Upload
        public DataSet GetRetailer_Route(string Doc_Special_Name, string Dist, string div_code, string SubDiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "declare @SubDive varchar(10)='0'set @SubDive='" + SubDiv + "'  select a.Territory_Code,Territory_Name from Mas_Territory_Creation a inner join mas_salesforce s on charindex(','+ cast(s.sf_code as varchar) +',', ','+a.sf_code+',') > 0 where Territory_Name='" + Doc_Special_Name + "'and Territory_SName = '" + Dist + "' and a.Division_Code = '" + div_code + "' and Territory_Active_Flag=0 and (@SubDive='0' or  charindex(','+@SubDive +',', ','+ s.subdivision_code+',') > 0 )";

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

        public DataSet GetRetailer_Hq(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select distinct a.Territory_code,a.Territory_name from  Mas_Territory a inner join Mas_Territory_Creation b on b.Territory_SName=a.Territory_code where a.Territory_name='" + Doc_Special_Name + "' and a.Div_Code='" + div_code + "' and a.Territory_Active_Flag=0";

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


        public DataSet GetRetailer_channel(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Doc_Special_Code,Doc_Special_Name from Mas_Doctor_Speciality where Doc_Special_Name='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "' and Doc_Special_Active_Flag=0";

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
        public DataSet GetRetailer_Class(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Doc_ClsCode,Doc_ClsName from Mas_Doc_Class where Doc_ClsName='" + Doc_Special_Name + "' and Division_Code = '" + div_code + "' and Doc_Cls_ActiveFlag=0";

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
        public DataSet getListedDr_new1(string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,d.ListedDr_Mobile,d.Doc_Qua_Name,d.Sf_Code, " +
                     " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
                     " Mas_ListedDr d " +
                    " WHERE  d.Division_Code='" + Terr_code + "'" +
                     " and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";
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
        public DataSet getCheck(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = "select ISNULL(MAX(ListedDrCode),1) as Num from Mas_ListedDr";
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
        public DataSet GetRetailer_sf(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select sf_code,Dist_Name,Division_Code from  Mas_Territory_Creation where Territory_Code ='" + Doc_Special_Name + "' and Territory_Active_Flag=0";

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

        public int AddLDr_Change(string Div_Code, string Channel1, string channel2)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_ListedDr set Doc_Special_Code='" + channel2 + "' where division_code='" + Div_Code + "' and Doc_Special_Code='" + Channel1 + "' ";
                iReturn = db.ExecQry(strQry);

                strQry = "update Mas_Doctor_Speciality set Doc_Special_Active_Flag='1' where  Division_Code='" + Div_Code + "' and Doc_Special_Code='" + Channel1 + "'";
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet GET_CHANNEL(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            strQry = " SELECT Doc_Special_Code,Doc_Special_SName,Doc_Special_Name " +
                     " FROM  Mas_Doctor_Speciality where division_Code = '" + Div_Code + "' AND doc_special_active_flag=0 ";
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
        public int GetTarget_Check(string sfcode, string prod_code,  string CTarget, string PTarget, string month, string year, string Div)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db_ER = new DB_EReporting();
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = null;
                DataSet dschk = new DataSet();


                strQry = "select * from PRODUCT_PRITARGET_MONTHLY_head  inner join PRODUCT_PRITARGET_MONTHLY_detail b " +
                         " where SF_CODE='" + sfcode + "' and PRODUCT_CODE='" + prod_code + "' and MONTH='" + month + "' and YEAR='" + year + "' and a.Division_Code='" + Div + "'";
                dschk = db.Exec_DataSet(strQry);
                string trans_sl_id = "";
               foreach (DataRow dd in dschk.Tables[0].Rows)
                {
                    trans_sl_id = dd["trans_sl_id"].ToString();
                }

                if (dschk.Tables[0].Rows.Count > 0)
                {
                    strQry = "update PRODUCT_PRITARGET_MONTHLY_head set MONTH='" + month + "',YEAR='" + year + "' where trans_sl_id='" + trans_sl_id + "' and a.Division_Code='" + Div + "'";
                    strQry = "update PRODUCT_PRITARGET_MONTHLY_detail set CASETARGET='"+ CTarget + "',PieceTARGET='" + PTarget + "' where trans_sl_id='" + trans_sl_id + "' and a.Division_Code='" + Div + "'";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    iReturn = -2;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
		public DataSet insert_primtarupload(string sfcode, string prod_code, string CTarget, string PTarget, string month, string year, string Div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "exec insert_primtarupload_excl '" + sfcode + "','" + prod_code + "','" + CTarget + "','" + PTarget + "','" + month + "','" + year + "','" + Div + "'";

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
        public DataSet Gettarget_Sl_No(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT isnull(max(cast(AUTO_ID as numeric))+1,'1') AUTO_ID from PRODUCT_TARGET_MONTHLY";

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
        public DataSet GetSF_code(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce where Sf_Code='" + Doc_Special_Name + "'and  Division_Code = '" + div_code + "," + "'and SF_Status=0";

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
        public DataSet GetProd_detail(string Doc_Special_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Product_Detail_Code='" + Doc_Special_Name + "'and Division_Code = '" + div_code + "'and Product_Active_Flag=0";

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
        public DataTable getListedDr_new1_ex_MGR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            string sub = "";
            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date,e.Stockist_Name,e.Field_Name, " +
            //         " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
            //         " Mas_ListedDr d inner join Mas_Stockist e on e.SF_Code=d.Sf_Code" +
            //        " WHERE  d.Division_Code='" + Terr_code + "'" +
            //         " and d.ListedDr_Active_Flag = 0" +
            //        " order by ListedDr_Sl_No";
            strQry = "exec [GET_Retailer_List_new] '" + divcode + "','" + sub + "','" + sf_code + "'";
            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataTable getListedDr_new1_ex_MGR_excel(string Terr_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            string sub = "";
            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date,e.Stockist_Name,e.Field_Name, " +
            //         " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
            //         " Mas_ListedDr d inner join Mas_Stockist e on e.SF_Code=d.Sf_Code" +
            //        " WHERE  d.Division_Code='" + Terr_code + "'" +
            //         " and d.ListedDr_Active_Flag = 0" +
            //        " order by ListedDr_Sl_No";
            strQry = "exec [GET_Retailer_List_Ex] '" + Terr_code + "','" + sub + "','" + sf_code + "'";
            try
            {
                dsListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsListedDR;
        }
        public DataTable getStockist_Ex_MGR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            string sub = "";
            DataTable dsStockist = null;
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name, " +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join "+
            //         "mas_stockist a on a.Stockist_Code=z.Distributor_Code and z.DSM_Active_Flag=0 where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name";
            //strQry = "SELECT a.User_Entry_Code,a.Stockist_Code,a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code," +
            //         "a.Field_Name,a.Username,a.Password,(select count(x.Territory_Code)as 'Sub_Couns' from Mas_Territory_Creation x where a.Stockist_Code=x.Dist_Name and x.Territory_Active_Flag=0)Sub_Couns," +
            //         "COUNT(z.DSM_name) as 'Sub_Coun' from Mas_DSM z full join mas_stockist a on a.Stockist_Code=z.Distributor_Code " +
            //         "and z.DSM_Active_Flag=0 " +
            //         "where a.Stockist_Active_Flag=0 and a.Division_Code='" + divcode + "' group by a.User_Entry_Code,a.Stockist_Code," +
            //         "a.Stockist_Name,a.Stockist_ContactPerson,a.Stockist_Mobile,a.Territory,a.Distributor_Code,a.Field_Name,a.Username,a.Password";
            strQry = "exec [GET_DISTRIBUTOR_List] '" + divcode + "','" + sub + "','" + sf_code + "'";
            try
            {
                dsStockist = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public DataSet Get_lstdr_Count(string Div_Code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select ms.sf_code, count(distinct ListedDrCode) lrdr_count from Mas_ListedDr Ld inner join mas_salesforce Ms on charindex(','+ms.sf_code+',', ','+ld.sf_code+',')>0  where ld.division_code='" + Div_Code + "'  AND MONTH(ListedDr_Created_Date)='" + fmonth + "'  AND YEAR(ListedDr_Created_Date)='" + fyear + "'  group by ms.sf_code";

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
        public DataTable getRet_EX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsProCat = null;

            strQry = "SELECT d.ListedDrCode as Listed_Customer_code,d.ListedDr_Name as Listed_Customer_Name,d.ListedDr_Mobile as Listed_Customer_Mobile,ListedDr_Address1 as Listed_Customer_Address1,ListedDr_Address2 as Listed_Customer_Address2,Doc_Spec_ShortName as Retailerchannelname,Doc_Class_ShortName as RetailerClassname,s.Sf_Name as EX_Name, " +
                    " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') Route_Name FROM " +
                    " Mas_ListedDr d inner join Mas_Salesforce s on  charindex(','+ cast(s.Sf_Code as varchar)+',',','+cast(d.Sf_Code as varchar)+',')>0 " +
                    " WHERE  d.Division_Code='" + divcode + "'" +
                    " and d.ListedDr_Active_Flag = 0" +
                    " order by ListedDr_Sl_No";
            try
            {
                dsProCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProCat;
        }

        public DataSet Get_Retailer_sal(string Div_Code, string fyear, string SF_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Retailer_sal '" + Div_Code + "','" + fyear + "','" + SF_code + "'";

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


        public DataSet Get_Retailer_salVisted(string Div_Code, string SF_code, string fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Retailer_sal_VISIT '" + Div_Code + "','" + SF_code + "','" + fyear + "'";

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
        public DataSet Get_Retailer_salVisted_months(string Div_Code, string SF_code, string fyear, string Fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Retailer_sal_VISIT_month '" + Div_Code + "','" + SF_code + "','" + fyear + "','" + Fmonth + "'";

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

        public DataSet Get_Retailer(string Div_Code, string subDivCode = "0", string SF_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec GET_Retailer '" + Div_Code + "','" + subDivCode + "','" + SF_code + "'";

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

        public DataSet getIn_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsLocation = null;
            strQry = "SELECT Type_Id,Name FROM Mas_Customer_Type WHERE Division_Code='" + div_code + "' and Active_Flag=0" +
                " order by 2";
            try
            {
                dsLocation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsLocation;
        }
        public DataSet Get_Retailer_sal_Days(string Div_Code, string fyear, string months, string SF_Code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Retailer_sal_days '" + Div_Code + "','" + fyear + "','" + months + "','" + SF_Code + "'";

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

        public DataSet Get_Retailer_sal_ProductWise(string Div_Code, string fyear, string months, string SF_code = "admin")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec [Get_Retailer_sal_ProductWise] '" + Div_Code + "','" + fyear + "','" + SF_code + "','" + months + "'";

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
        public DataSet Get_Retailer_sal_ProductWise_pro(string Div_Code, string fyear, string months, string SF_code = "admin")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec [RetailerWiseProductValues] '" + Div_Code + "','" + SF_code + "','" + fyear + "','" + months + "'";

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



        public DataSet Get_sale_rep1(string Div_Code, string sf_code, string Fyear, string Fmonth, string Tyear, string Tmonth, string P_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "GET_Inv_Pro_Sale_Rep1 '" + Div_Code + "','" + sf_code + "','" + Fyear + "','" + Fmonth + "','" + Tyear + "','" + Tmonth + "','" + P_code + "'";

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
        public DataSet Get_sale_rep(string Div_Code, string sf_code, string Fyear, string Fmonth, string Tyear, string Tmonth, string P_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "GET_Inv_Pro_Sale_Rep '" + Div_Code + "','" + sf_code + "','" + Fyear + "','" + Fmonth + "','" + Tyear + "','" + Tmonth + "','" + P_code + "'";

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
        public DataSet GetRetailers(string div_code)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select ListedDrCode,ListedDr_Name,Sf_Code from mas_listeddr where division_code='" + div_code + "' and ListedDr_Active_Flag='0'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }

        public DataSet getListedDrDashbord(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            string sub = "";
            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName,d.ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date,e.Stockist_Name,e.Field_Name, " +
            //         " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name FROM " +
            //         " Mas_ListedDr d inner join Mas_Stockist e on e.SF_Code=d.Sf_Code" +
            //        " WHERE  d.Division_Code='" + Terr_code + "'" +
            //         " and d.ListedDr_Active_Flag = 0" +
            //        " order by ListedDr_Sl_No";
            strQry = "exec [RetailerDashbord] '" + DivCode + "'";
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

        public DataSet Get_Retailer_Target_vs_Sal(string SF_Code, string FYear, string FMonth, string TYear, string TMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_Retailer_Target_vs_Sal '" + SF_Code + "','" + FYear + "','" + FMonth + "','" + TYear + "','" + TMonth + "'";

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

        public DataSet GetRetailerSFWise(string div_code, string sfCode)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select ListedDrCode,ListedDr_Name,Sf_Code,Doc_Class_ShortName from mas_listeddr where division_code='" + div_code + "' and charindex(','+'" + sfCode + "'+',',','+sf_code+',')>0  and ListedDr_Active_Flag='0' order by Doc_Class_ShortName,ListedDr_Name";
            strQry = "select ListedDrCode,ListedDr_Name,m.Sf_Code,Doc_Class_ShortName,Territory_Name RoName from mas_listeddr m inner join Mas_Territory_Creation t on t.Territory_Code=m.Territory_Code where m.division_code='" + div_code + "' and charindex(','+'" + sfCode + "'+',',','+m.sf_code+',')>0  and ListedDr_Active_Flag='0' order by Territory_Name,Doc_Class_ShortName,ListedDr_Name";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
        public int updatelatlong(string code, string lat, string longi, string ldrcode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update mas_listeddr set code='" + code + "',ListedDr_Class_Patients='" + lat + "',ListedDr_Consultation_Fee='" + longi + "' where ListedDrCode ='" + ldrcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet GetNewRetailers(string div_code, string sfCode, string subDiv, string FYear, string Mode)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec GET_New_Retailer_SF '" + div_code + "','" + subDiv + "','" + sfCode + "','" + FYear + "','" + Mode + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
        public int Retailer_Appprove_Manager(string sf_code, string custCode, string mode, string reason, int flg)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            if (mode == "1")
            {
                strQry = " update Mas_ListedDr set ListedDr_Active_Flag = " + flg + " " +
                         "  where   ListedDrCode = '" + custCode + "'";
            }
            else
            {
                strQry = " update Mas_ListedDr set ListedDr_Active_Flag = 4 ,reject_remarks='" + reason + "'" +
                         "  where  ListedDrCode = '" + custCode + "'";
            }
            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GetGEORetailers(string div_code, string sfCode, string subDiv, string FYear, string FMonth)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec geoRetailers '" + div_code + "','" + sfCode + "','" + FYear + "','" + FMonth + "','" + subDiv + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
        public int untagRetailers_confirm(string custCode)
        {

            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "delete from map_GEO_Customers where Cust_Code='" + custCode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet Get_Retailer_Walk_in_Sequance(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select ListedDrCode,ListedDr_Name,Territory_Code, ListedDr_Sl_No from mas_listeddr where  charindex(','+'" + SF_Code + "'+',',','+Sf_Code+',')>0 order by ListedDr_Sl_No,ListedDr_Name";

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

        public int Retailer_Walk_in_sequance_Update(string cust_code, string sequanceNum)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            strQry = "update mas_listeddr set ListedDr_Sl_No='" + sequanceNum + "' where ListedDrCode='" + cust_code + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getRetailerMGR(string div_code, string sfcode)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = " exec getdistretail '" + sfcode + "','" + div_code + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

 public DataSet get_frequencyofvisit(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();


            DataSet dsTerr = null;



            strQry = "exec get_FrequencyOfVisit '"+ div_code + "' ";
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
  public DataSet get_breed(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            strQry = "exec get_breed '" + div_code + "' ";
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
public DataSet get_curentCompetitor(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            strQry = "exec get_Competitor '" + div_code + "' ";
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
  public DataSet ViewnewcontactDr(string drcode,string div_id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " EXEC ViewnewcontactDr '" + drcode + "','"+ div_id + "'";


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
 public DataSet getFryidforxl(string fzyname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "exec  getFryidforxl '" + fzyname + "'";

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
        public DataSet getCurrentcom(string currentcom)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "exec getCurrentcom '" + currentcom + "'";

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
        public DataSet Get_FieldForce_Target_vs_Sal(string SF_Code, string FYear, string FMonth, string TYear, string TMonth, string div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec Get_FieldForce_Target_vs_Sale '" + SF_Code + "','" + FYear + "','" + FMonth + "','" + TYear + "','" + TMonth + "','" + div + "'";

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
        public DataTable getRetailerBusinessSlab(string div_code, string sfcode)
        {

            DataTable dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = " exec getMarkesRetailerSlab '" + div_code + "','" + sfcode + "'";
            try
            {
                dsSF = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }

        public DataTable getRetailerBusiness(string div_code, string sfcode, string typ)
        {

            DataTable dsSF = null;
            DB_EReporting db = new DB_EReporting();
            if (typ == "M")
            {
                strQry = " exec getMarkesRetailerMarked '" + div_code + "','" + sfcode + "'";
            }
            if (typ == "NM")
            {
                strQry = " exec getMarkesRetailerNotMarked '" + div_code + "','" + sfcode + "'";
            }
            if (typ == "AllM")
            {
                strQry = " exec getMarkesRetailerAllMarked '" + div_code + "','" + sfcode + "'";
            }
            if (typ == "AllNM")
            {
                strQry = " exec getMarkesRetailerAllNotMarked '" + div_code + "','" + sfcode + "'";
            }
            try
            {
                dsSF = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
        }
		
        public DataSet getNew_Retailers(string SF, string Div, string Mn, string Yr)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getNew_Retailers '"+ SF + "','" + Div + "','"+ Mn + "','" + Yr + "'";
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
        public DataSet getStockist_Details(string SF, string Div)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getStockist_Details '" + Div + "','" + SF + "'";
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
        public DataSet getNew_Retailers_Excel(string SF, string Div, string Mn, string Yr)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getNew_Retailers_Excel '" + SF + "','" + Div + "','" + Mn + "','" + Yr + "'";
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
		public DataSet getEdit_Retailers(string SF, string Div, string Mn, string Yr)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getEdited_Retailers '" + SF + "','" + Div + "','" + Mn + "','" + Yr + "'";
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
        public DataSet getEdit_Retailers_Excel(string SF, string Div, string Mn, string Yr)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getEdit_Retailers_Excel '" + SF + "','" + Div + "','" + Mn + "','" + Yr + "'";
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
        public DataSet get_SF_Retailers(string divcode, string SF)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec get_SF_Retailers '"+ SF + "'";

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
        public DataSet tGet_Retailer_sal(string Div_Code, string fyear, string SF_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "exec tGet_Retailer_sal '" + Div_Code + "','" + fyear + "','" + SF_code + "'";

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
        public DataSet getCliamSalb(string divcode, string YR, string SYR)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getRptClaimSlab '" + divcode + "'," + YR + ",'" + SYR + "'";

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
        public DataSet getRptClaim(string SF, string divcode, string YR, string SYR)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getRptClaims '" + SF + "','" + divcode + "'," + YR + ",'" + SYR + "'";

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
        public DataSet getRptClaimDets(string SF, string divcode, string YR, string SYR)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getRptClaimDets '" + SF + "','" + divcode + "'," + YR + ",'" + SYR + "'";

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
		 public DataSet getslab(string yr, string div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "select distinct SlabDesc from mas_retail_business_slab where year(From_dt)='" + yr + "' and division_code='" + div + "'";
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
        public DataTable getMultiSubdivision(string subdivs,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            strQry = "select (select CAST(subdivision_code as varchar)+',' from mas_subdivision where CHARINDEX(','+subdivision_name+',','," + subdivs + ",')>0 and Div_Code='" + div_code + "' group by subdivision_code for xml path(''))as subdiv";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataTable getMultiStates(string statenames)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            strQry = "select (select CAST(State_Code as varchar)+',' from Mas_State where CHARINDEX(','+StateName+',','," + statenames + ",')>0 and State_Active_Flag=0 group by State_Code for xml path(''))as states";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public int GetTarget_Check(string sfcode, string prod_code, string Target, string month, string year, string Div)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db_ER = new DB_EReporting();
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = null;
                DataSet dschk = new DataSet();


                strQry = "select * from PRODUCT_TARGET_MONTHLY where SF_CODE='" + sfcode + "' and PRODUCT_CODE='" + prod_code + "' and MONTH='" + month + "' and YEAR='" + year + "' and Division_Code='" + Div + "'";
                dschk = db.Exec_DataSet(strQry);

                if (dschk.Tables[0].Rows.Count > 0)
                {
                    strQry = "update PRODUCT_TARGET_MONTHLY set TARGET='" + Target + "' where SF_CODE='" + sfcode + "' and PRODUCT_CODE='" + prod_code + "' and MONTH='" + month + "' and YEAR='" + year + "'and Division_Code='" + Div + "'";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    iReturn = -2;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet GetSalesForceDetails(string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Sf_Code,Sf_Name+'  -  '+ Sf_HQ +'  -  '+sf_Designation_Short_Name Sf_Name FROM Mas_SalesForce  ";
            strQry += " WHERE CHARINDEX(','+cast('" + DivCode + "' as varchar)+',',','+Division_Code+',')>0  AND SF_Status<>2 AND Sf_Code<>'admin' ";
            strQry += " GROUP BY Sf_Code,Sf_Name+'  -  '+ Sf_HQ +'  -  '+sf_Designation_Short_Name  ORDER BY Sf_Name+'  -  '+ Sf_HQ +'  -  '+sf_Designation_Short_Name ASC  ";

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
		
		public DataSet GetRetailerDetails(string DivCode, string ListedDrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT ListedDrCode,ListedDr_Name,ListedDr_Address1, ";
            strQry += " Convert(varchar(10),Cast(ListedDr_Created_Date as date),103) ListedDr_Created_Date,IsNull(ListedDr_Mobile,'') ListedDr_Mobile FROM Mas_ListedDr   ";
            strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
            strQry += " AND ListedDrCode = " + ListedDrCode + "  ";            

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


        public DataSet GetRetailerList(string Territory_Code, string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //if (SfCode == "0")
            //{
            //    //strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
            //    //strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
            //    ////strQry += " AND  ( '" + SfCode + "' = '0' OR  CHARINDEX(','+cast('" + SfCode + "'  as varchar)+',',','+Sf_Code+',')>0 ) ";
            //    //strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

            //    strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
            //    strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
            //    strQry += " AND Territory_Code = " + Territory_Code + "  ";
            //    strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";
            //}
            //else
            //{
            //    strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
            //    strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
            //    strQry += " AND   (CHARINDEX(','+cast('" + SfCode + "'  as varchar)+',',','+Sf_Code+',')>0 ) ";
            //    strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

            //}

            strQry = " SELECT ListedDrCode,ListedDr_Name FROM Mas_ListedDr  ";
            strQry += " WHERE ListedDr_Active_Flag = 0 and Division_Code = '" + DivCode + "'  ";
            strQry += " AND Territory_Code = " + Territory_Code + "  ";
            strQry += " GROUP BY ListedDrCode,ListedDr_Name  ORDER BY ListedDr_Name ASC  ";

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


        public string Outletsmerging(string divcode, string sfcode,string SfRoute, string fromretailer, string toretailer)
        {

            string msg = string.Empty;
           
            try
            {
                DB_EReporting db = new DB_EReporting();

                msg = db.Outletsmerging(divcode, sfcode, SfRoute, fromretailer, toretailer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return msg;
        }
		
		public DataSet GetRouteDetails(string sfcode, string DivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT Territory_Code,Territory_Name FROM Mas_Territory_Creation  ";
            strQry += " WHERE Division_Code = " + DivCode + " AND  (CHARINDEX(','+cast('" + sfcode + "'  as varchar)+',',','+SF_Code+',')>0)  ";
            strQry += " GROUP BY Territory_Code,Territory_Name ORDER BY Territory_Name ASC  ";

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

    }
}
