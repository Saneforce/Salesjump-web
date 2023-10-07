using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Hospital
    {
        private string strQry = string.Empty;

        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Hospital_Code) FROM Mas_Hospital WHERE Sf_Code = '" + sf_code + "' and Hospital_Active_Flag=0";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public bool RecordExist(string Hospital_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Hospital_Name) FROM Mas_Hospital WHERE Hospital_Name='" + Hospital_Name + "' AND sf_code='" + sf_code + "'";
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
        public bool RecordExist(string Hospital_Code, string Hospital_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Hospital_Name) FROM Mas_Hospital WHERE Hospital_Name = '" + Hospital_Name + "' AND Hospital_Code!='" + Hospital_Code + "' AND sf_code='" + sf_code + "' ";

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

        public int RecordAdd(string Hospital_Name, string Hospital_Address1, string Hospital_Contact, string Hospital_Phone, string Hospital_Terr, string sf_code)
        {
            int iReturn = -1;

            if (!RecordExist(Hospital_Name, sf_code))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Hospital_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(Hospital_Code),0)+1 FROM Mas_Hospital ";
                Hospital_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Hospital (Hospital_Code,Hospital_Name,Hospital_Address1,Hospital_Phone,Hospital_Contact, Territory_Code, " +
                         " Division_Code, Sf_Code, Hospital_Active_Flag, Created_Date) " +
                         " VALUES('" + Hospital_Code + "', '" + Hospital_Name + "', '" + Hospital_Address1 + "', '" + Hospital_Phone + "', " +
                         " '" + Hospital_Contact + "',  '" + Hospital_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate())";


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

        public int DeActivate(string Hospital_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Hospital " +
                            " SET Hospital_Active_Flag=1 , " +
                            " Hospital_deactivate_date = getdate() " +
                            " WHERE Hospital_Code = '" + Hospital_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int ReActivate(string Hospital_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Hospital " +
                            " SET Hospital_Active_Flag=0 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Hospital_Code = '" + Hospital_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int BulkEdit(string str, string Hospital_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Hospital SET " + str + "  Where Hospital_Code='" + Hospital_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getTerritory_Hospital(string Hospital_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = " SELECT Territory_Code FROM  Mas_Hospital " +
                     " WHERE Hospital_Code='" + Hospital_code + "' AND sf_Code= '" + sf_code + "' ";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }

        

        public DataSet getHospitalDetails(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT Hospital_Code, Hospital_Name, Hospital_Address1, Hospital_Contact, Territory_Code, Hospital_Phone, Hospital_Fax, Hospital_EMail, Hospital_Mobile" +
                        " FROM Mas_Hospital " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }
        // Sorting For the HospitalList 
        public DataTable getHospitallist_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dtHospital = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHospital;
        }
        //sorting Reactivation
        public DataTable getHospitallist_DataTable_ReAct(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Active_Flag = 1";
            try
            {
                dtHospital = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHospital;
        }

        public DataTable getHospitallistAlpha_DataTable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtHospital = null;
            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.territory_Name FROM " +
                          "Mas_Hospital d, Mas_Territory_Creation t " +
                          "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                          "and d.Hospital_Active_Flag = 0" +
                          " AND LEFT(d.Hospital_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtHospital = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtHospital;
        }
        public DataSet getHospital(string sfcode, string hosp_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact,d.Hospital_Address1,d.Hospital_Phone, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Code = '" + hosp_code + "' and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }


        public DataSet getHospital(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }
        //Reactivation
        public DataSet getHospital_ReAct(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Active_Flag = 1";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }

        //alphabetical order  

        public DataSet getHospitaldet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;
            strQry = "select '1' val,'All' Hospital_Name " +
                     " union " +
                     " select distinct LEFT(Hospital_Name,1) val, LEFT(Hospital_Name,1) Hospital_Name" +
                     " FROM Mas_Hospital " +
                     " WHERE Hospital_Active_Flag=0 " +
                     " AND Sf_Code =  '" + sfcode + "' " +
                     " ORDER BY 1";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }

        public DataSet getHospitaldet_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;
            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.territory_Name FROM " +
                          "Mas_Hospital d, Mas_Territory_Creation t " +
                          "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                          "and d.Hospital_Active_Flag = 0" +
                          " AND LEFT(d.Hospital_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }


        //
        public int RecordUpdate_Hos(string Hospital_Code, string Hospital_Name, string Hospital_Contact, string Territory_Code, string sf_code)
        {
            int iReturn = -1;
             if (!RecordExist(Hospital_Code,Hospital_Name))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Hospital " +
                         " SET Hospital_Name = '" + Hospital_Name + "', " +
                         " Hospital_Contact = '" + Hospital_Contact + "', " +
                         " Territory_Code = '" + Territory_Code + "'," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Hospital_Code = '" + Hospital_Code + "' ";

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

        public DataSet getEmptyHospital()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = " SELECT TOP 10 '' Hospital_Name,'' Hospital_Address1, '' Hospital_Contact, '' Hospital_Phone " +
                     " FROM  sys.tables ";
            try
            {
                dsChemist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemist;
        }

        //Changes done by Priya

    
        public int getTerr_Hosp_Count(string Territory_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Hospital_Code) from Mas_Hospital  " +
                         " where Hospital_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getHospitalDetails_terr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        public DataSet getHospital_terr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code and d.Territory_Code='" + terr_code + "' " +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }

        //Changes Done by Reshmi
        public DataSet getTerritory(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                           " UNION " +
                       " SELECT Territory_Code,Territory_Name " +
                       " FROM  Mas_Territory_Creation where Sf_Code='" + sfcode + "'" +
                       " and territory_active_flag=0 ";
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


        public DataSet getListedHospforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Name like '" + Name + "%'" +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }


        public DataSet getListedChemforTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "'" +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;

        }
        public DataTable getDTHospital_Nam(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Hospital_Name like '" + Name + "%'" +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;
        }
        public DataTable getDTTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsHospital = null;

            strQry = "SELECT d.Hospital_Code, d.Hospital_Name, d.Hospital_Contact, t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Hospital d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "'" +
                        "and d.Hospital_Active_Flag = 0";
            try
            {
                dsHospital = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHospital;


        }

    }

}
