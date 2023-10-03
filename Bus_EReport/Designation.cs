using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class Designation
    {
        private string strQry = string.Empty;

        public DataSet getDesignation()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            strQry = "SELECT Designation_Code,Designation_Short_Name,Designation_Name,Type " +
                     " FROM Mas_SF_Designation order by Manager_SNo,Baselevel_SNo ";
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        // Sorting
        public DataTable getDesignation_DataTable(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDesignation = null;
            strQry = " SELECT D.Designation_Code,D.Designation_Short_Name,D.Designation_Name,D.Type, " +
                       " d.Baselevel_SNo,d.Manager_SNo, (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code ) as sf_count,D.Division_Code " +
                       " from Mas_SF_Designation D where Division_Code ='" + div_code + "' and Designation_Active_Flag = 0" +
                       " order by d.Designation_Name ";
            try
            {
                dsDesignation = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        public DataSet getDesignationEd(string Designation_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            
            DataSet dsDesignation = null;
            strQry = "Select Designation_Short_Name, Designation_Name, Desig_Color, Type, Division_Code," +
                      " (select COUNT(s.Designation_Code) from Mas_Salesforce S where Designation_Code=S.Designation_Code and Designation_Code='" + Designation_Code + "' ) as sf_count " +
                      " From Mas_SF_Designation" +
                      " where Designation_Code='" + Designation_Code + "' and Designation_Active_Flag = 0 and Division_Code='" + div_code + "'" +
                      "ORDER BY 2";
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }
        public bool RecordExist(string Designation_Short_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Designation_Code) FROM Mas_SF_Designation WHERE Designation_Short_Name='" + Designation_Short_Name + "' and Division_Code = '" + div_code + "' ";
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
        public bool dnRecordExist(string Designation_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Designation_Code) FROM Mas_SF_Designation WHERE Designation_Name='" + Designation_Name + "' and Division_Code = '" + div_code + "' ";
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

        public bool RecordExist(int Designation_Code, string Designation_Short_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Designation_Code) FROM Mas_SF_Designation WHERE Designation_Code != '" + Designation_Code + "' AND Designation_Short_Name='" + Designation_Short_Name + "' and Division_Code = '" + div_code + "' ";

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
        public int RecordUpdate(int Designation_Code, string Designation_Short_Name, string Designation_Name, string Desig_Color, string type, string div_code)
        {
            int iReturn = -1;
            if (!RecordExist(Designation_Code, Designation_Short_Name, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();
                    strQry = "UPDATE Mas_Salesforce " +
                          " SET sf_Designation_Short_Name = '" + Designation_Short_Name + "' " +
                          " WHERE Designation_Code = '" + Designation_Code + "' and Division_Code = '"+div_code+"'";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_SF_Designation " +
                             " SET Designation_Short_Name = '" + Designation_Short_Name + "', " +
                             " Designation_Name = '" + Designation_Name + "' , Desig_Color ='" + Desig_Color + "', type ='" + type + "', Division_Code = '" + div_code + "'," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Designation_Code = '" + Designation_Code + "' and Designation_Active_Flag = 0";

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
        public int RecordDelete(int Designation_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_SF_Designation WHERE Designation_Code = '" + Designation_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getDesign()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,''Designation_Short_Name,'---Select---'Designation_Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name,Designation_Name" +
                     " FROM Mas_SF_Designation ";
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
        public int RecordAdd(string Designation_Short_Name, string Designation_Name, string Desig_Color, string type, string div_code)
        {
            int iReturn = -1;

            if (!RecordExist(Designation_Short_Name, div_code))
            {
                if (!dnRecordExist(Designation_Name, div_code))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Designation_Code)+1,'1') Designation_Code from Mas_SF_Designation ";
                        int Designation_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_SF_Designation(Designation_Code,Designation_Short_Name,Designation_Name,Desig_Color,Created_Date,LastUpdt_Date,Type,Designation_Active_Flag,Division_Code)" +
                                 "values('" + Designation_Code + "','" + Designation_Short_Name + "', '" + Designation_Name + "','" + Desig_Color + "',getdate(),getdate(),'" + type + "',0, '" + div_code + "' ) ";

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

        public DataSet getDesign_Baselevel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Type" +
                     " FROM Mas_SF_Designation where Type= 1 and Designation_Active_Flag = 0 and Division_Code = '" + div_code + "' order by Baselevel_SNo";
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
        public DataSet getDesign_Managerlevel(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Type" +
                     " FROM Mas_SF_Designation where Type= 2 and Designation_Active_Flag = 0 and Division_Code = '" + div_code + "' order by Manager_SNo";
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
        public int Update_BaselevelSno(string Designation_Code, string Sno, string div_code)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_SF_Designation " +
                         " SET Baselevel_SNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Designation_Code = '" + Designation_Code + "' and Designation_Active_Flag = 0 and Division_Code = '" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Update_ManagerSno(string Designation_Code, string Sno, string div_code)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_SF_Designation " +
                         " SET Manager_SNo = '" + Sno + "', " +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Designation_Code = '" + Designation_Code + "' and Designation_Active_Flag = 0 and Division_Code = '" + div_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
public DataSet getDesig_SF(string Designation_Short_Name,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            strQry = "Select Designation_Code, Designation_Short_Name " +
                     " From Mas_SF_Designation" +
                     " where Designation_Short_Name='" + Designation_Short_Name + "' and Division_Code = '" + div_code + "'";
                   
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }


        public DataSet getDesignation_count(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            strQry = " SELECT D.Designation_Code,D.Designation_Short_Name,D.Designation_Name,D.Type, " +
                     " d.Baselevel_SNo,d.Manager_SNo, (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code and SF_Status='0') as sf_count,D.Division_Code,isnull(D.Des_Rights,'') Menuid   " +
                     " from Mas_SF_Designation D where D.Designation_Active_Flag = 0 and D.Division_Code ='"+div_code+"' " +
                     " order by d.Manager_SNo,d.Baselevel_SNo ";
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }
        // Changes done by Priya
        public int RecordUpdate_Inline(int Designation_Code, string Designation_Short_Name, string Designation_Name, string div_code)
        {
            int iReturn = -1;
            if (!RecordExist(Designation_Code, Designation_Short_Name, div_code))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_Salesforce " +
                           " SET sf_Designation_Short_Name = '" + Designation_Short_Name + "' " +
                           " WHERE Designation_Code = '" + Designation_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "UPDATE Mas_SF_Designation " +
                             " SET Designation_Short_Name = '" + Designation_Short_Name + "', " +
                             " Designation_Name = '" + Designation_Name + "' ," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Designation_Code = '" + Designation_Code + "' and Designation_Active_Flag = 0 ";

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

        public DataSet getDesignation_get_cnt()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            strQry = "  select st.State_Code,StateName,ShortName,b.Designation_Short_Name,b.Cnt, " +
                      " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(st.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name " +
                      "  from Mas_State ST, vwSFDesgCnt b where b.State_Code=ST.State_Code ";
                 
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        //Changes done by Saravanan
        public DataSet getDesignationMGR(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            //strQry = " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Type,'Yes' as Yes,'No' as No,'0' as TYes,'1' as TNo " +
            //         " FROM Mas_SF_Designation where [Type]=2  ";

            strQry = " SELECT Designation_Short_Name,Designation_Code,TP_Approval_Sys " +
                     " FROM Mas_SF_Designation where [Type]=2 and Division_Code='" + Division_Code + "' ";

            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }
        public DataSet getDesignation_Sys_Approval(string StrDesgination, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            if (StrDesgination == "")
            {
                strQry = " select Designation_Short_Name,tp_approval_Sys from Mas_SF_Designation where Type=1 and Division_Code='" + div_code + "' ";
            }
            else
            {
                strQry = " select Designation_Short_Name,tp_approval_Sys from Mas_SF_Designation where Designation_Short_Name in ('" + StrDesgination + "') and Division_Code='" + div_code + "' ";
            }

            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        //Changes done by Saravanan
        public DataSet getDesignationMR(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;

            strQry = " SELECT Designation_Short_Name,Designation_Code,TP_Approval_Sys " +
                     " FROM Mas_SF_Designation where [Type]=1 and Division_Code='" + Division_Code + "'   ";

            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }
        //Changes done by Reshmi
        public int DeActivate(int Designation_Code, string Division_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();
                if (Division_Code.Contains(','))
                {
                    Division_Code = Division_Code.Remove(Division_Code.Length - 1);
                }
                strQry = "Update Mas_SF_Designation " +
                          "SET Designation_Active_Flag=1 , " +
                          "LastUpdt_Date = getdate() " +
                          "WHERE Designation_Code = '" + Designation_Code + "' and Division_Code='" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int Reactivate(int Designation_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_SF_Designation " +
                          "SET Designation_Active_Flag=0 , " +
                          "LastUpdt_Date = getdate() " +
                          "WHERE Designation_Code = '" + Designation_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet Designation_React(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;
            strQry = " SELECT D.Designation_Code,D.Designation_Short_Name,D.Designation_Name,D.Type, " +
                     " d.Baselevel_SNo,d.Manager_SNo, (select COUNT(s.Designation_Code) from Mas_Salesforce S where D.Designation_Code=S.Designation_Code ) as sf_count " +
                     " from Mas_SF_Designation D " +
                     " WHERE Designation_Active_Flag=1 and Division_Code='" + Div_Code + "' " +
                     " order by d.Manager_SNo,d.Baselevel_SNo ";
            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        public DataSet getDesinationCode(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesig = null;
            strQry = "select Designation_Code from Mas_SF_Designation where division_code='" + div_code + "' and Type='" + ddltype + "'";
            try
            {
                dsDesig = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesig;
        }

        public DataSet getDesignationAddChkBox(string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesig = null;
            strQry = " SELECT Designation_Code,Designation_Short_Name " +
                     " FROM Mas_SF_Designation " +
                     " WHERE Designation_Code in (" + Designation_Code + ") and Designation_Active_Flag=0" +
                     " union select '' Designation_Code,'ALL' Designation_Short_Name ORDER BY 2";
            try
            {
                dsDesig = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesig;
        }

        public DataSet getDesig_analysis(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDesignation = null;

            strQry = " select Designation_Code,Designation_Short_Name,Designation_Name,type,Report_Level from Mas_SF_Designation " +
                     " where Division_Code='" + Division_Code + "' and type='2' order by Manager_SNo ";


            try
            {
                dsDesignation = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }

        public DataTable getDesigcodenew(string Division_Code, string desig_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDesignation = new DataTable();

            strQry = " select Designation_Code,Designation_Short_Name from Mas_SF_Designation where designation_name='" + desig_name + "' and Division_Code=" + Division_Code + " ";

            try
            {
                dsDesignation = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDesignation;
        }




     }                     

}
