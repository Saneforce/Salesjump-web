using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class Town
    {
        private string strQry = string.Empty;


        //Giri grid View 13.06.16
        public DataSet TowngetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //strQry = "SELECT Town_code,a.Town_sname,a.Town_name,Dist," +
            //         "(select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Town_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //         " FROM Mas_Town a WHERE a.Town_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
            //         "ORDER BY 2";
            strQry = " select a.Town_code,a.Town_sname,a.Town_name,a.Dist,a.Territory_Name " +
                     " From "+
                     " Mas_Town a WHERE a.Town_Active_Flag=0 and a.Div_Code= '" + divcode + "' group by a.Town_name,a.Town_code,a.Town_sname,a.Dist,a.Territory_Name";
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


        //Giri Sort 13.06.16
        public DataTable TowngetSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;


            //strQry = "SELECT Town_code,Town_sname,Town_name,Dist," +
            //               " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Town_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //               " FROM Mas_Town a WHERE a.Town_Active_Flag=0 And Div_Code= '" + divcode + "'" +
            //              " ORDER BY 2";

            try
            {
                dtSubDiv = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtSubDiv;
        }


        //Giri Town 13.06.2016
        public DataSet getTown(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Town_sname,Town_name,Dist,Territory_Name " +
                     " FROM Mas_Town WHERE Town_Active_Flag=0 And Town_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";
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

        public bool RecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool TownRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Town_code) FROM Mas_Town WHERE Town_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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
        public bool sTownRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Town_code) FROM Mas_Town WHERE Town_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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
        public bool RecordExist(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool sRecordExist(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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


        //Giri TownAdd 13.06.2016
        public int TownRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Dist_name, string Dist_cd,string Terr_Name,string Terr_Cd)
        {
            int iReturn = -1;
            if (!TownRecordExist(subdiv_sname, divcode))
            {
                if (!sTownRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Town_code)+1,'1')Town_code from Mas_Town";
                        int Towndivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_Town(Town_code,Div_Code,Town_sname,Town_name,Dist,Dist_code,Created_Date,LastUpdt_Date,Town_Active_Flag,Territory_Code,Territory_Name)" +
                                 "values('" + Towndivision_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + Dist_name + "','" + Dist_cd + "',getdate(),getdate(),0,'"+Terr_Cd+"','"+Terr_Name+"')";
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
        public bool sTownRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Town_code) FROM Mas_Town WHERE Town_code != '" + subdivision_code + "' AND Town_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool TownRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Town_code) FROM Mas_Town WHERE Town_code != '" + subdivision_code + "' AND Town_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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


        //Giri TownUpdate 13.06.16
        public int TownRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Dist_name,string Dist_cd,string Terr_name,string Terr_cd,string divcode)
        {
            int iReturn = -1;
            if (!sTownRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!TownRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Town " +
                                 " SET Town_sname = '" + subdiv_sname + "', " +
                                 " Town_name = '" + subdiv_name + "' ,  " +
                                 " Dist = '" + Dist_name + "' ,  " +
                                 " Dist_code ='" + Dist_cd + "', " +
                                 " Territory_Name = '" + Terr_name + "' ,  " +
                                 " Territory_Code ='" + Terr_cd + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Town_code = '" + subdivision_code + "' ";

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
        //Giri Town Updated 13.06.16
        public int TownUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Dist_name, string divcode, string Dist_cd)
        {
            int iReturn = -1;
            if (!TownRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!sTownRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Town " +
                                 " SET Town_sname = '" + subdiv_sname + "', " +
                                 " Town_name = '" + subdiv_name + "' ,  " +
                                 " Dist = '" + Dist_name + "' ,  " +
                                 " Dist_code ='" + Dist_cd + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Town_code = '" + subdivision_code + "' ";

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

        public int RecordDelete(int subdivision_code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Mas_Town WHERE subdivision_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by priya

        //done by Giri 14.06.2016 

        public int TownDeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_Town" +
                            " SET Town_Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Town_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //end
        public DataSet getSubdivision(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = " SELECT 0 as subdivision_code,'---Select---' as subdivision_name " +
                      " UNION " +
                  " SELECT subdivision_code,subdivision_name " +
                  " FROM  mas_subdivision where Div_Code='" + divcode + "'" +
                  " and subdivision_active_flag=0 ";
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
        public DataSet getSubdiv_Prod(string divcode, string subdivision_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                      " a.Product_Sample_Unit_One,a.Product_Sample_Unit_Two,a.Product_Sample_Unit_Three,b.Product_Cat_Name," +
                       " c.Product_Grp_Name, d.subdivision_code FROM  mas_subdivision d,Mas_Product_Category b, " +
                       " Mas_Product_Group c join Mas_Product_Detail a on a.subdivision_code like '" + subdivision_code + ',' + "%'  or " +
                        " a.subdivision_code like '%" + ',' + subdivision_code + "' or a.subdivision_code like '%" + ',' + subdivision_code + ',' + "%' " +
                        " WHERE a.product_cat_code = b.product_cat_code AND a.Product_Grp_Code = c.product_grp_code" +
                        " AND d.SubDivision_Active_Flag=0 AND  a.Division_Code= '" + divcode + "' and d.subdivision_code ='" + subdivision_code + "'" +
                        "order by Product_Detail_Name";
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
        public DataSet getSubdiv_Sales(string divcode, string subdivision_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.SF_Code, a.Sf_Name, a.Division_Code,a.Sf_HQ,case when a.sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_type,  " +
                " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To,c.Designation_Name " +
                   " FROM Mas_SF_Designation c, mas_subdivision b join  mas_salesforce a on a.subdivision_code like '" + subdivision_code + ',' + "%'  or " +
                    " a.subdivision_code like '%" + ',' + subdivision_code + "' or a.subdivision_code like '%" + ',' + subdivision_code + ',' + "%' " +
                    " WHERE SF_Status=0  AND lower(sf_code) != 'admin' AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                    " and b.SubDivision_Active_Flag=0  and  b.subdivision_code ='" + subdivision_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') and a.Designation_Code=c.Designation_Code ORDER BY 2";
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

        //Changes done by Priya

        public DataSet getSubDiv_Create(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
                     " union select '' subdivision_code,'ALL' subdivision_sname,'ALL' subdivision_name ORDER BY 2";
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

        public DataSet GetSubdiv_Code(string subdivision_name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            strQry = "select subdivision_code from mas_subdivision where subdivision_name='" + subdivision_name + "' and Div_Code = '" + div_code + "'";

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

        public DataSet getSub_sf(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT subdivision_code,Division_Code  " +
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
        public DataSet getStatePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Div_Code from Mas_District";
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
        public DataSet getStateProd(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT 0 as state_code,'ALL' as statename,'' as shortname " +
            //         " UNION " +
            //         " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
            //         " ORDER BY 2";
            strQry = "SELECT 0 as Dist_code,'--Select--' as Distname,'' as shortname " +
                         " UNION " +
                         " SELECT Dist_code,Dist_name,Dist_sname " +
                         " FROM Mas_District " +
                         " WHERE Div_Code='" + state_code + "' and Dist_Active_Flag=0 " +
                         " ORDER BY 2";
            //strQry=" SELECT 0 as Territory_Code,'--Select--' as Territory_Name "+
            //       " UNION "+
            //       " SELECT Territory_Code,Territory_Name "+ 
            //       " FROM Mas_Territory "+
            //       " WHERE Div_Code='" + state_code + "' and Territory_Active_Flag=0 " + 
            //        "ORDER BY 2";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }
        public DataSet getterrProd(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            
            strQry = " SELECT 0 as Territory_Code,'--Select--' as Territory_Name " +
                   " UNION " +
                   " SELECT Territory_Code,Territory_Name " +
                   " FROM Mas_Territory " +
                   " WHERE Div_Code='" + state_code + "' and Territory_Active_Flag=0 " +
                    "ORDER BY 2";
            try
            {
                dsState = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsState;
        }

        public DataSet Dist_code(string Dist_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Dist_code from Mas_District where Dist_name='" + Dist_name + "' ";
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

    }
}
