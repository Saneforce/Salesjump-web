using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class UnListedDR
    {

        private string strQry = string.Empty;

        public DataSet getEmptyListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 10 '' UnListedDR_Name,'' UnListedDR_Address1 " +
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

        public DataSet getTopListedDR()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT TOP 1 '' UnListedDR_Name,'' UnListedDR_Address1 " +
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0" +
                        "order by 2";
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
        public DataSet getListedDr_ReAct(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 1" +
                        "order by 2";
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
        // Sorting For Unlisted DoctorList 
        public DataTable getUnlistedDoctorList_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0";
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
        //sorting Reactivation
        public DataTable getUnlistedDoctorList_DataTable_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 1";
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
        public DataSet getListedDrforSpl(string sfcode, string SplCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName ,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g , Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '%" + TerrCode + "%') " +
                        "and d.UnListedDr_Active_Flag = 0";
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

        public DataSet getListedDrforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g , Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Name like '" + Name + "%'" +
                        "and d.UnListedDr_Active_Flag = 0";
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
        public DataSet getListedDoctor(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103) UnListedDR_DOB, " +
                             " convert(char(10),UnListedDR_DOW,103)UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                             " UnListedDR_Phone, UnListedDR_EMail  " +
                             " FROM Mas_UnListedDr " +
                             " WHERE Sf_Code =  '" + sfcode + "' " +
                             " AND UnListedDr_Active_Flag = 0";
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

        public DataSet getState(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "SELECT State_Code,Division_Code " +
                        " FROM Mas_Salesforce " +
                        " WHERE Sf_Code =  '" + sfcode + "' ";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Name like '%" + Name + "%'" +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND (Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " Territory_Code like '%" + ',' + TerrCode + ',' + "%' or Territory_Code like '%" + TerrCode + "%') " +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_ClsCode ='" + ClsCode + "' " +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_Special_Code ='" + SplCode + "' " +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_Cat_Code ='" + CatCode + "' " +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT UnListedDrCode, UnListedDr_Name, UnListedDR_Address1, convert(char(10),UnListedDR_DOB,103)UnListedDR_DOB,convert(char(10),UnListedDR_DOW,103) UnListedDR_DOW, '' No_of_Visit, UnListedDR_Mobile, " +
                        " UnListedDR_Phone, UnListedDR_EMail  " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Doc_QuaCode ='" + QuaCode + "' " +
                        " AND UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,d.UnListedDr_Address1,c.Doc_Cat_Name,s.Doc_Special_Name,g.Doc_QuaName,dc.Doc_ClsName ,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s, Mas_Doc_Qualification g, Mas_Doc_Class dc,Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code and d.UnListedDrCode = '" + drcode + "' " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,d.UnListedDr_Address1,d.Doc_Cat_Code,c.Doc_Cat_Name,d.Doc_Special_Code,s.Doc_Special_Name,d.Doc_QuaCode,g.Doc_QuaName,d.Doc_ClsCode,dc.Doc_ClsName, " +
                      " d.Territory_Code, t.territory_Name,d.UnListedDr_Address2,d.UnListedDr_Address3, d.UnListedDr_PinCode, d.UnListedDr_Phone, d.UnListedDr_Mobile, " +
                      " UnListedDr_Email, UnListedDr_DOB, UnListedDr_DOW FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Qualification g, Mas_Doc_Class dc,Mas_Territory_Creation t " +
                        "WHERE d.UnListedDrCode =  '" + drcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " + 
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0";
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

        public int RecordUpdate(string Listed_DR_Code, string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

              //  strQry = "update Mas_UnListedDr set UnListedDr_Name = '" + Listed_DR_Name + "', UnListedDr_Address1 = '" + Listed_DR_Address + "', " +
              //           " Doc_Special_Code  = '" + Listed_DR_Spec + "', Doc_Cat_Code = '" + Listed_DR_Catg + "', " +
              //           " Territory_Code = '" + Listed_DR_Terr + "', Doc_ClsCode = '" + Listed_DR_Class + "', Doc_QuaCode = '" + Listed_DR_Qual + "' " +
              //           " Where UnListedDrCode = '" + Listed_DR_Code + "' ";

                strQry = "update Mas_UnListedDr set UnListedDr_Name = '" + Listed_DR_Name + "', UnListedDr_Address1 = '" + Listed_DR_Address + "', " +
                        " Doc_Cat_Code = '" + Listed_DR_Catg + "' ,Doc_Special_Code  = '" + Listed_DR_Spec + "', " +
                        " Doc_QuaCode = '" + Listed_DR_Qual + "' ,Doc_ClsCode = '" + Listed_DR_Class + "' ,Territory_Code = '" + Listed_DR_Terr + "' " +
                        " Where UnListedDrCode = '" + Listed_DR_Code + "' ";  

                
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

        public int DeActivate(string dr_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_UnListedDr " +
                            " SET Unlisteddr_active_flag=1 , " +
                            " Unlisteddr_deactivate_date = getdate() " +
                            " WHERE Unlisteddrcode = '" + dr_code + "' ";

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

                strQry = "select SLVNo from Mas_UnListedDr where Unlisteddrcode = '" + dr_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet FetchTerritory(string sf_code)
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

        
        public DataSet FetchCategory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_Cat_Code,'---Select---' as Doc_Cat_Name " +
                     " UNION " +
                     " SELECT Doc_Cat_Code,Doc_Cat_Name " +
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

        public DataSet FetchSpeciality(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_Special_Code,'---Select---' as Doc_Special_Name " +
                         " UNION " +
                     " SELECT Doc_Special_Code,Doc_Special_Name " +
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

        public bool sRecordExist(string Listed_DR_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UnListedDr_Name) FROM Mas_UnListedDr WHERE UnListedDr_Name='" + Listed_DR_Name + "' AND Sf_Code='" + sf_code + "' ";
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
        public bool RecordExist(string UnListedDrCode, string Listed_DR_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UnListedDr_Name) FROM Mas_UnListedDr WHERE UnListedDr_Name = '" + Listed_DR_Name + "' AND UnListedDrCode!='" + UnListedDrCode + "' AND Sf_Code='" + sf_code + "' ";

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

        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code)
        {
            int iReturn = -1;
            
            if (!sRecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;
                    Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //  strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                             " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
                             " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate())";


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

        public int RecordAdd(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code, int SLV_No)
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

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr WHERE Division_Code = '" + Division_Code + "' ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                         " Territory_Code, Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                         " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
                         " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + SLV_No + "',getdate())";


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


        public int RecordAdd(string DR_Name, string DR_Sex, string DR_DOB, string DR_Qual, string DR_DOW, string DR_Spec, string DR_RegNo,
            string DR_Catg, string DR_Terr, string DR_Comm, string DR_Class, string DR_Address1, string DR_Address2, string DR_Address3,
            string DR_State, string DR_Pin, string DR_Mobile, string DR_Phone, string DR_EMail, string DR_Profile, string DR_Visit_Days,
            string DR_DayTime, string DR_IUI, string DR_Avg_Patients, string DR_Hospital, string DR_Class_Patients, string DR_Consultation_Fee,
            string sf_code)
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

                strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr WHERE Division_Code = '" + Division_Code + "' ";
                Listed_DR_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_UnListedDr (UnListedDrCode, Sf_Code, UnListedDr_Name, UnListedDr_Address1, UnListedDr_Address2, " +
                         " UnListedDr_Address3, UnListedDr_PinCode, UnListedDr_Phone, UnListedDr_Mobile, " +
                         " UnListedDr_Email, UnListedDr_DOB, UnListedDr_DOW, Doc_Special_Code, Doc_Cat_Code, Territory_Code, " +
                         " Doc_QuaCode, visit_days, UnListedDr_Active_Flag, " +
                         " UnListedDr_Created_Date, Division_Code, Doc_ClsCode, Visit_Hours, UnListedDR_Sl_No, LastUpdt_Date) " +
                         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + DR_Name + "', '" + DR_Address1 + "', '" + DR_Address2 + "', " +
                         " '" + DR_Address3 + "', '" + DR_Pin + "', '" + DR_Phone + "', '" + DR_Mobile + "', '" + DR_EMail + "', " +
                         " '" + DR_DOB + "', '" + DR_DOW + "', '" + DR_Spec + "', '" + DR_Catg + "', '" + DR_Terr + "', '" + DR_Qual + "', " +
                         " '" + DR_DayTime + "', 0, getdate(), '" + Division_Code + "','" + DR_Class + "',5,'" + Listed_DR_Code + "', getdate())";


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

        public int BulkEdit(string str, string Doc_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_UnListedDr SET " + str + "  Where UnListedDrCode='" + Doc_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet getDoctor_Terr_Catg_Spec_Class_Qual(string doc_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Territory_Code,Doc_Cat_Code,Doc_Special_Code,Doc_ClsCode, Doc_QuaCode FROM  Mas_UnListedDr " +
                     " WHERE UnListedDrCode='" + doc_code + "' AND sf_Code= '" + sf_code + "' ";
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


        public DataSet FetchClass(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_ClsCode,'---Select---' as Doc_ClsName " +
                         " UNION " +
                     " SELECT Doc_ClsCode,Doc_ClsName " +
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

        //alphabetical order
        public DataSet getDoctorUnlist_Alphabet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUnListedDR = null;
            strQry = "select '1' val,'All' UnListedDr_Name " +
                     " union " +
                     " select distinct LEFT(UnListedDr_Name,1) val, LEFT(UnListedDr_Name,1) UnListedDr_Name" +
                     " FROM Mas_UnListedDr " +
                     " WHERE UnListedDr_Active_Flag=0 " +
                     " AND Sf_Code =  '" + sfcode + "' " +
                     " ORDER BY 1";
            try
            {
                dsUnListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUnListedDR;
        }

        public DataSet getDoctorUnlist_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsUnListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                         "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Doc_Special_Code = s.Doc_Special_Code " +
                         "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                         "and d.Doc_QuaCode = g.Doc_QuaCode " +
                         "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                         "and d.UnListedDr_Active_Flag = 0"+
                        " AND LEFT(d.UnListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
            try
            {
                dsUnListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsUnListedDR;
        }

        //
        public int RecordUpdate_In(string Listed_DR_Name, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string UnListedDrCode, string sf_code)
        {
            int iReturn = -1;
               if (!RecordExist(UnListedDrCode, Listed_DR_Name, sf_code))
            {
           
            try
            {

                DB_EReporting db = new DB_EReporting();
                Listed_DR_Name = Listed_DR_Name.Replace("  ", " ");

                strQry = "update Mas_UnListedDr set UnListedDr_Name = '" + Listed_DR_Name + "', " +
                        " Doc_Cat_Code = '" + Listed_DR_Catg + "' ,Doc_Special_Code  = '" + Listed_DR_Spec + "', " +
                        " Doc_QuaCode = '" + Listed_DR_Qual + "' ,Doc_ClsCode = '" + Listed_DR_Class + "' ,Territory_Code = '" + Listed_DR_Terr + "' " +
                        " Where UnListedDrCode = '" + UnListedDrCode + "' ";


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

        public DataSet FetchQualification(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Doc_QuaCode,'---Select---' as Doc_QuaName " +
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
        public int Div_Code(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            return div_code;
        }
        //change done by saravanan
        public DataTable getDoctorUnlistAlphabet_Datatable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtUnListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                         "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc,Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Doc_Special_Code = s.Doc_Special_Code " +
                         "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                         "and d.Doc_QuaCode = g.Doc_QuaCode " +
                         "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                         "and d.UnListedDr_Active_Flag = 0" +
                        " AND LEFT(d.UnListedDr_Name,1) = '" + sAlpha + "' " +
                    " ORDER BY 2";
            try
            {
                dtUnListedDR = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtUnListedDR;
        }

        public DataTable getListedDrforName_Datatable(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g , Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Name like '" + Name + "%'" +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Special_Code = '" + SplCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode= g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_Cat_Code = '" + CatCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_QuaCode = '" + QuaCode + "' " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.UnListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforClass_Datable(string sfcode, string ClassCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and d.Doc_ClsCode = '" + ClassCode + "' " +
                        "and d.UnListedDr_Active_Flag = 0";
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

        public DataTable getListedDrforTerr_Datatable(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT d.UnListedDrCode,d.UnListedDr_Name,c.Doc_Cat_Name,s.Doc_Special_Name ,dc.Doc_ClsName ,g.Doc_QuaName ,t.territory_Name FROM " +
                        "Mas_UnListedDr d,Mas_Doctor_Category c,Mas_Doctor_Speciality s,Mas_Doc_Class dc, Mas_Doc_Qualification g , Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Doc_Special_Code = s.Doc_Special_Code " +
                        "and d.Doc_ClsCode= dc.Doc_ClsCode " +
                        "and d.Doc_QuaCode = g.Doc_QuaCode " +
                        "and d.Doc_Cat_Code = c.Doc_Cat_Code  " +
                        "and (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '%" + TerrCode + "%') " +
                        "and d.UnListedDr_Active_Flag = 0";
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
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UnListedDrCode) FROM Mas_UnListedDr WHERE Sf_Code = '" + sf_code + "' and UnListedDr_Active_Flag = 0 ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //Changes done by Saravanan
        public DataSet GetNameFromDataBase(string prefixText)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select UnListedDr_Name from Mas_UnListedDr where UnListedDr_Name like '" + prefixText + "%';";

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
        //Added by Sri - For DCR - New Un Listed Doctor
        public int RecordAdd_DcrUn(string Listed_DR_Name, string Listed_DR_Address, string Listed_DR_Catg, string Listed_DR_Spec, string Listed_DR_Qual, string Listed_DR_Class, string Listed_DR_Terr, string sf_code,string dcr_date,string created_by)
        {
            int iReturn = -1;


            DB_EReporting db = new DB_EReporting();

            if (!sRecordExist(Listed_DR_Name, sf_code))
            {
                try
                {

                    int Division_Code = -1;
                    int Listed_DR_Code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    //  strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                    Listed_DR_Code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                             " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                             " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,dcr_date,created_by) " +
                             " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                             " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
                             " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + dcr_date  + "','" + created_by + "')";


                    iReturn = db.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        iReturn = Listed_DR_Code;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                strQry = "SELECT UnListedDrCode FROM Mas_UnListedDr WHERE UnListedDr_Name='" + Listed_DR_Name + "' and sf_code = '" + sf_code + "'   ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                {
                    //strQry = "delete from Mas_UnListedDr where sf_code = '" + sf_code + "' and  UnListedDrCode = " + iRecordExist;
                    //iReturn = db.ExecQry(strQry);

                    //int Division_Code = -1;
                    //int Listed_DR_Code = -1;

                    //strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    //Division_Code = db.Exec_Scalar(strQry);

                    ////  strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr WHERE Division_Code = '" + Division_Code + "' ";
                    //strQry = "SELECT ISNULL(MAX(UnListedDrCode),0)+1 FROM Mas_UnListedDr ";
                    //Listed_DR_Code = db.Exec_Scalar(strQry);

                    //strQry = "insert into Mas_UnListedDr (UnListedDrCode,SF_Code,UnListedDr_Name,UnListedDr_Address1,Doc_Special_Code,Doc_Cat_Code, " +
                    //         " Territory_Code,Doc_QuaCode, UnListedDr_Active_Flag, UnListedDr_Created_Date, division_code,UnListedDr_Phone,UnListedDr_Mobile, " +
                    //         " UnListedDr_Email,UnListedDr_DOB,UnListedDr_DOW,Visit_Hours,visit_days,UnListedDr_Sl_No,Doc_ClsCode,SLVNo,LastUpdt_Date,dcr_date,created_by) " +
                    //         " VALUES('" + Listed_DR_Code + "', '" + sf_code + "', '" + Listed_DR_Name + "', '" + Listed_DR_Address + "', '" + Listed_DR_Spec + "', " +
                    //         " '" + Listed_DR_Catg + "', '" + Listed_DR_Terr + "', '" + Listed_DR_Qual + "', 0, getdate(), '" + Division_Code + "','123','123','TestDR@Test.com', " +
                    //         " '01-01-1900','01-01-1900','12','4','" + Listed_DR_Code + "','" + Listed_DR_Class + "','" + Listed_DR_Code + "', getdate(),'" + dcr_date + "','" + created_by + "')";


                    //iReturn = db.ExecQry(strQry);
                    //if (iReturn > 0)
                    //{
                        iReturn = iRecordExist;
                //    }
                }
                //else
                //{
                //    iReturn = -2;
                //}
            }
            return iReturn;
        }
        public bool RecordExist(string Listed_DR_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UnListedDr_Name) FROM Mas_UnListedDr WHERE UnListedDr_Name='" + Listed_DR_Name + "' ";
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

    }
}
