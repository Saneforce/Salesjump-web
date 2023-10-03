using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Chemist
    {
        private string strQry = string.Empty;

        public int RecordAdd(string Chemists_Name, string Chemists_Address1, string Chemists_Contact, string Chemists_Phone, string Chemists_Terr,string Chemists_Cat, string sf_code)
        {
            int iReturn = -1;
            
            if (!sRecordExist(Chemists_Name, sf_code))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int chemists_code = -1;

                Chemists_Name = Chemists_Name.Replace("  ", " ");

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

             //   strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists "; 
                chemists_code = db.Exec_Scalar(strQry);

                strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                         " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code) " +
                         " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                         " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "')";


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

        public bool RecordExist(string Chemists_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name='" + Chemists_Name + "' ";
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
 
        public int DeActivate(string chem_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Chemists_Active_Flag=1 , " +
                            " chemists_deactivate_date = getdate() " +
                            " WHERE Chemists_Code = '" + chem_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int ReActivate(string chem_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Chemists" +
                         " SET Chemists_Active_Flag=0, " +
                         " LastUpdt_Date = getdate() " +
                         " where Chemists_Code = '" + chem_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int BulkEdit(string str, string chem_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "UPDATE Mas_Chemists SET " + str + "  Where Chemists_Code='" + chem_Code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getTerritory_Chemists(string chem_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = " SELECT Territory_Code FROM  Mas_Chemists " +
                     " WHERE Chemists_Code='" + chem_code + "' AND sf_Code= '" + sf_code + "' ";
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

        public DataSet getChemistsDetails(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT Chemists_Code, Chemists_Name, Chemists_Address1, Chemists_Contact, Territory_Code, Chemists_Phone, Chemists_Fax, Chemists_EMail, Chemists_Mobile" +
                        " FROM Mas_Chemists " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND Chemists_Active_Flag = 0";
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

        public DataSet getChemists(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0";
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
        
        // Sorting For ChemistsList 
        public DataTable getChemistslist_DataTable(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         " WHERE d.Sf_Code = '" + sfcode + "' and d.Territory_Code = t.Territory_Code " +
                         " and d.Chemists_Active_Flag = 0";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        //sorting Reactivation

        public DataTable getChemistslist_DataTable_ReAct(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         " WHERE d.Sf_Code = '" + sfcode + "' and d.Territory_Code = t.Territory_Code " +
                         " and d.Chemists_Active_Flag = 1";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        public DataTable getChemistsAlpha_DataTable(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;
            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Chemists_Active_Flag = 0" +
                     " AND LEFT(Chemists_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }
        public int RecordCount(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Code) FROM Mas_Chemists WHERE Sf_Code = '" + sf_code + "' and Chemists_Active_Flag = 0";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getChemists(string sfcode,string chemists_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Code = '" + chemists_code + "' and d.Chemists_Active_Flag = 0";
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

        //alphabetical order

        public DataSet getChemist_Alphabet(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
           
            DataSet dsChemists = null;
            strQry = "select '1' val,'All' Chemists_Name " +
                     " union " +
                     " select distinct LEFT(Chemists_Name,1) val, LEFT(Chemists_Name,1) Chemists_Name" +
                     " FROM Mas_Chemists " +
                     " WHERE Chemists_Active_Flag=0 " +
                     " AND Sf_Code =  '" + sfcode + "' " +
                     " ORDER BY 1";
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

        public DataSet getChemist_Alphabet(string sfcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;
            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                         "Mas_Chemists d, Mas_Territory_Creation t " +
                         "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                         "and d.Chemists_Active_Flag = 0"+
                     " AND LEFT(Chemists_Name,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";
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


        //
        public int RecordUpdate_Chem(string Chemists_Code, string Chemists_Name, string Chemists_Contact, string Territory_Code, string sf_code)
        {
            int iReturn = -1; 
              if (!RecordExist(Chemists_Code,Chemists_Name, sf_code))
            {
            try
            {

                DB_EReporting db = new DB_EReporting();
                Chemists_Name = Chemists_Name.Replace("  ", " ");

                strQry = "UPDATE Mas_Chemists " +
                         " SET Chemists_Name = '" + Chemists_Name + "', " +
                         " Chemists_Contact = '" + Chemists_Contact + "', " +
                         " Territory_Code = '" + Territory_Code + "'," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Chemists_Code = '" + Chemists_Code + "'  and  sf_code = '" + sf_code + "'";

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
        public DataSet get_Territory(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                           " UNION " +
                       " SELECT Territory_Code,Territory_Name " +
                       " FROM  Mas_Territory_Creation where Sf_Code='" + sfcode + "'"+
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

        public DataSet getEmptyChemist()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = " SELECT TOP 10 '' Chemists_Name,'' Chemists_Address1, '' Chemists_Contact, '' Chemists_Phone " +
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

        //Chemists Reactivation
        public DataSet getChemists_React(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 1";
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

        //Changes done by Priya

        public int getTerr_Chem_Count(string Territory_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(Chemists_Code) from Mas_Chemists  " +
                         " where Chemists_Active_Flag=0 and Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getChemist_terr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Address1, d.Chemists_Contact, " +
                     " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
                     " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
                     " FROM Mas_Chemists d, Mas_Territory_Creation t  WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code = '" + terr_code + "'" +
                     " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0 and t.Territory_Active_Flag=0 ";
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
        public DataSet getChemistsDetails_terr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;
            strQry = " SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Address1, d.Chemists_Contact, " +
                               " d.Territory_Code, d.Chemists_Phone, d.Chemists_Fax, " +
                               " d.Chemists_EMail, d.Chemists_Mobile,t.territory_Name " +
                               " FROM Mas_Chemists d, Mas_Territory_Creation t  WHERE d.Sf_Code =  '" + sfcode + "'" +
                               " and d.Territory_Code =t.Territory_Code AND d.Chemists_Active_Flag = 0  and t.Territory_Active_Flag=0";
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
        public int GetChemistCode()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Chemists_Code)+1,'1') Chemists_Code from Mas_Chemists";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        //Changes done by Priya

        public DataSet getChemists_transfer(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code='" + terr_code + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0";
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
        public DataTable getChem_transfer(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name,'' color FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code='" + terr_code + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dtChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemists;
        }

        //Changes done by Priya
        public int Transfer_Chemists(string Chemists_Code, string terr_code, string sf_code, string trans_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "Update Mas_Chemists " +
                         " Set Territory_Code ='" + terr_code + "', sf_code = '" + trans_Code + "', Transfer_MR_Chemist = getdate()" +
                         " WHERE Chemists_Code='" + Chemists_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        // Changes Done by Reshmi
        public DataSet getListedChemforName(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Name like '" + Name + "%'" +
                        "and d.Chemists_Active_Flag = 0";
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


        public DataSet getListedChemforTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "' and d.Chemists_Active_Flag = 0";
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
        public DataTable getDTChemist_Nam(string sfcode, string Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone,t.Territory_Code, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Chemists_Name like '" + Name + "%'" +
                        "and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        public DataTable getDTTerr(string sfcode, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsChemists = null;

            strQry = "SELECT d.Chemists_Code, d.Chemists_Name, d.Chemists_Contact,d.Chemists_Address1,d.Chemists_Phone, t.territory_Name FROM " +
                        "Mas_Chemists d, Mas_Territory_Creation t " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and d.Territory_Code  = t.Territory_Code " +
                        "and d.Territory_Code = '" + terr_code + "' and d.Chemists_Active_Flag = 0";
            try
            {
                dsChemists = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemists;
        }
        //Added by Sri - For DCR - New Chemists
        public int RecordAdd_DcrChem(string Chemists_Name, string Chemists_Address1, string Chemists_Contact, string Chemists_Phone, string Chemists_Terr, string Chemists_Cat, string sf_code,string dcr_date,string created_by)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            if (!sRecordExist(Chemists_Name, sf_code))
            {
                try
                {

                    

                    int Division_Code = -1;
                    int chemists_code = -1;

                    strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    Division_Code = db.Exec_Scalar(strQry);

                    // strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                    strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                    chemists_code = db.Exec_Scalar(strQry);

                    strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                             " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,dcr_date,created_by) " +
                             " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                             " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "','" + dcr_date  + "','" + created_by  + "')";


                    iReturn = db.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        iReturn = chemists_code;
                    }

                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                strQry = "SELECT Chemists_Code FROM Mas_Chemists WHERE Chemists_Name ='" + Chemists_Name + "' and sf_code = '" + sf_code + "'  ";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                {
                    //strQry = "delete from Mas_Chemists where sf_code = '" + sf_code + "' and  Chemists_Code = " + iRecordExist;
                    //iReturn = db.ExecQry(strQry);

                    //int Division_Code = -1;
                    //int chemists_code = -1;

                    //strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                    //Division_Code = db.Exec_Scalar(strQry);

                    //// strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists WHERE Division_Code = '" + Division_Code + "' ";
                    //strQry = "SELECT ISNULL(MAX(Chemists_Code),0)+1 FROM Mas_Chemists ";
                    //chemists_code = db.Exec_Scalar(strQry);

                    //strQry = "insert into Mas_Chemists (Chemists_Code,Chemists_Name,Chemists_Address1,Chemists_Phone,Chemists_Contact, Territory_Code, " +
                    //         " Division_Code, Sf_Code, Chemists_Active_Flag, Created_Date,Cat_Code,dcr_date,created_by) " +
                    //         " VALUES('" + chemists_code + "', '" + Chemists_Name + "', '" + Chemists_Address1 + "', '" + Chemists_Phone + "', " +
                    //         " '" + Chemists_Contact + "',  '" + Chemists_Terr + "', '" + Division_Code + "','" + sf_code + "', 0, getdate(),'" + Chemists_Cat + "','" + dcr_date + "','" + created_by + "')";


                    //iReturn = db.ExecQry(strQry);
                    //if (iReturn > 0)
                    //{
                          iReturn = iRecordExist;
                    }


                //}
                //else
                //{
                //    iReturn = -2;
                //}
                
            }
            return iReturn;
        }
        //changes done by reshmi
        public DataSet getChemist_Allow_Admin(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemist = null;

            strQry = "Select No_Of_Sl_ChemistsAllowed from  Admin_Setups where Division_Code='" + div_code + "' ";

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

        public DataSet getChemist_Count(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsChemist = null;

            strQry = "SELECT COUNT(Chemists_Code) from Mas_Chemists WHERE Sf_Code = '" + sf_code + "' and Division_Code='" + div_code + "' and Chemists_Active_Flag= 0 ";

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
        public bool RecordExist(string Chemists_Code, string Chemists_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name = '" + Chemists_Name + "' AND Chemists_Code!='" + Chemists_Code + "' AND Sf_Code ='" + sf_code + "' ";

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

        public bool sRecordExist(string Chemists_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chemists_Name) FROM Mas_Chemists WHERE Chemists_Name='" + Chemists_Name + "' AND Sf_Code='" + sf_code + "'";
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

        //start for chemist category
        public DataSet getChemCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;

            strQry = " SELECT Cat_Code,c.Chem_Cat_SName,c.Chem_Cat_Name, " +
                     " (select count(d.Cat_Code) from Mas_Chemists d where d.Cat_Code = c.Cat_Code and Division_Code='" + divcode + "') as Cat_Count" +
                     "  FROM  Mas_Chemist_Category c" +
                     " WHERE c.Chem_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                     " ORDER BY c.Chem_Cat_Sl_No";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public int RecordUpdate_Chem_code(int Chem_Cat_Code, string Chem_Cat_SName, string Chem_Cat_Name, string divcode)
        {
            int iReturn = -1;
            if (!RecordExistS(Chem_Cat_Code, Chem_Cat_SName, divcode))
            {
                if (!sRecordExistN(Chem_Cat_Code, Chem_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        //strQry = "UPDATE Mas_Chemists " +
                        //      "SET Chem_Cat_ShortName = '" + Chem_Cat_SName + "'" +
                        //      "WHERE Chem_Cat_Code = '" + Chem_Cat_Code + "' AND Division_Code='" + divcode + "' ";

                        //iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Chemist_Category " +
                                 " SET Chem_Cat_SName = '" + Chem_Cat_SName + "', " +
                                 " Chem_Cat_Name = '" + Chem_Cat_Name + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public DataSet getChemCat_Re(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;

            strQry = " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=1 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY Chem_Cat_Sl_No";
            try
            {
                dsDocCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCat;
        }

        public int ReActivate_Chemcat(int Chem_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                            " SET Chem_Cat_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getChemCat_trans(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;
            strQry = "SELECT 0 as Cat_Code,'--Select--' as Chem_Cat_SName,'' as Chem_Cat_Name " +
                     " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName, Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public DataSet getChemCat_Transfer(string divcode, string Chem_Cat_SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCls = null;

            strQry = "SELECT 0 as Cat_Code,'--Select--' as Chem_Cat_SName,'' as Chem_Cat_Name " +
                     " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Chem_Cat_Active_Flag=0 AND Division_Code=  '" + divcode + "' and Chem_Cat_SName!='" + Chem_Cat_SName + "'  " +
                     " ORDER BY 2";
            try
            {
                dsChemCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCls;
        }

        public DataSet getChemCat_count(string Chem_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(Cat_Code) as Cat_Code from Mas_Chemists  where Cat_Code=" + Chem_Cat_Code + " and Chemists_Active_Flag =0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }

        public int Update_ChemCat_Chemists(string Chem_Cat_from, string Chem_cat_to, string chkdel)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                         " SET Cat_Code = '" + Chem_cat_to + "',  " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Cat_Code = '" + Chem_Cat_from + "'  ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Chemist_Category " +
                        " SET Chem_Cat_Active_Flag = '" + chkdel + "' " +
                        " WHERE Cat_Code = '" + Chem_Cat_from + "' and Chem_Cat_Active_Flag=0 ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable getChemistCategorylist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtChemCat = null;

            strQry = " SELECT Cat_Code,c.Chem_Cat_SName,c.Chem_Cat_Name, " +
                    " (select count(d.Cat_Code) from Mas_Chemists d where d.Cat_Code = c.Cat_Code) as Cat_Count" +
                    "  FROM  Mas_Chemist_Category c" +
                    " WHERE c.Chem_Cat_Active_Flag=0 AND c.Division_Code= '" + divcode + "' " +
                    " ORDER BY c.Chem_Cat_Sl_No";
            try
            {
                dtChemCat = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtChemCat;
        }

        public int Update_ChemCatSno(string Chem_Cat_Code, string Sno)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                         " SET Chem_Cat_Sl_No = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getChemCat_code(string divcode, string Chem_Cat_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsChemCat = null;

            strQry = " SELECT Chem_Cat_SName,Chem_Cat_Name FROM  Mas_Chemist_Category " +
                     " WHERE Cat_Code='" + Chem_Cat_Code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
            try
            {
                dsChemCat = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsChemCat;
        }

        public int RecordAdd_chem(string divcode, string Chem_Cat_SName, string Chem_Cat_Name)
        {
            int iReturn = -1;
            int Chem_Cat_Code = -1;
            if (!RecordExistche(Chem_Cat_SName, divcode))
            {
                if (!sRecordExistname(Chem_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT ISNULL(COUNT(Cat_Code),0)+1 FROM Mas_Chemist_Category ";
                        Chem_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Chemist_Category(Cat_Code,Division_Code,Chem_Cat_SName,Chem_Cat_Name,Chem_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Chem_Cat_Code + "','" + divcode + "','" + Chem_Cat_SName + "', '" + Chem_Cat_Name + "',0,getdate(),getdate())";


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
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;
        }

        public bool RecordExistche(string Chem_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_SName) FROM Mas_Chemist_Category WHERE Chem_Cat_SName='" + Chem_Cat_SName + "' AND Division_Code='" + divcode + "' ";
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

        public bool sRecordExistname(string Chem_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_Name) FROM Mas_Chemist_Category WHERE Chem_Cat_Name='" + Chem_Cat_Name + "' AND Division_Code='" + divcode + "' ";
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
        public int RecordDeleteChem(int Chem_Cat_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Chemist_Category WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int DeActivateChem(int Chem_Cat_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemist_Category " +
                            " SET Chem_Cat_Active_Flag=1 ," +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Cat_Code = '" + Chem_Cat_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet FetchCategory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int div_code = -1;

            DataSet dsTerr = null;

            strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
            div_code = db_ER.Exec_Scalar(strQry);

            strQry = " SELECT 0 as Cat_Code,'---Select---' as Chem_Cat_SName, '' as Chem_Cat_Name " +
                         " UNION " +
                     " SELECT Cat_Code,Chem_Cat_SName,Chem_Cat_Name " +
                     " FROM  Mas_Chemist_Category where division_Code = '" + div_code + "' AND Chem_Cat_Active_Flag=0 ";
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

        public bool RecordExistS(int Chem_Cat_Code, string Chem_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_SName) FROM Mas_Chemist_Category WHERE Chem_Cat_SName = '" + Chem_Cat_SName + "' AND Cat_Code!='" + Chem_Cat_Code + "' AND Division_Code='" + divcode + "' ";

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

        public bool sRecordExistN(int Chem_Cat_Code, string Chem_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Chem_Cat_Name) FROM Mas_Chemist_Category WHERE Chem_Cat_Name = '" + Chem_Cat_Name + "' AND Cat_Code!='" + Chem_Cat_Code + "'AND Division_Code='" + divcode + "' ";

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
