using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class District
    {
        private string strQry = string.Empty;


        //Giri grid View 13.06.16
        public DataSet DistgetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

           
            strQry = " select a.Dist_code,a.Dist_sname,a.Dist_name,a.State, " +
                     " COUNT(z.Dist) as 'Sub_Coun' from Mas_Town z full join " +
                     " Mas_District a on a.Dist_name=z.Dist and z.Town_Active_Flag=0 and z.Div_Code='" + divcode + "' "+
                     " WHERE a.Dist_Active_Flag=0 and a.Div_Code= '" + divcode + "'group by a.Dist_name,a.Dist_code,a.Dist_sname,a.State  order by a.Dist_name";
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
        public DataTable DistgetSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;


            strQry = "SELECT Dist_code,Dist_sname,Dist_name,State," +
                           " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Dist_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
                           " FROM Mas_District a WHERE a.Dist_Active_Flag=0 And Div_Code= '" + divcode + "'" +
                          " ORDER BY 2";

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


        //Giri Dist 13.06.2016
        public DataSet getDist(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Dist_sname,Dist_name,State " +
                     " FROM Mas_District WHERE Dist_Active_Flag=0 And Dist_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
        public bool DistRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Dist_code) FROM Mas_District WHERE Dist_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sDistRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Dist_code) FROM Mas_District WHERE Dist_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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


        //Giri DistAdd 13.06.2016
        public int DistRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string State_name,string State_code)
        {
            int iReturn = -1;
            if (!DistRecordExist(subdiv_sname, divcode))
            {
                if (!sDistRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Dist_code)+1,'1')Dist_code from Mas_District";
                        int Distdivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_District(Dist_code,Div_Code,Dist_sname,Dist_name,State,Created_Date,LastUpdt_Date,Dist_Active_Flag,State_Code)" +
                                 "values('" + Distdivision_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + State_name + "',getdate(),getdate(),0,'"+State_code+"')";
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
        public bool sDistRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Dist_code) FROM Mas_District WHERE Dist_code != '" + subdivision_code + "' AND Dist_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool DistRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Dist_code) FROM Mas_District WHERE Dist_code != '" + subdivision_code + "' AND Dist_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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



        //Giri DistUpdate 13.06.16
        public int DistRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string State_name, string divcode,string State_code)
        {
            int iReturn = -1;
            if (!DistRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!sDistRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_District " +
                                 " SET Dist_sname = '" + subdiv_sname + "', " +
                                 " Dist_name = '" + subdiv_name + "' ,  " +
                                 " State = '" + State_name + "' ,  " +
                                 " State_Code = '" + State_code + "' ,  " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Dist_code = '" + subdivision_code + "' ";

                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Town SET Dist = '" + subdiv_name + "' WHERE Dist_code = '" + subdivision_code + "' ";
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
        //Giri Dist Updated 13.06.16
        public int DistUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string State_name, string divcode)
        {
            int iReturn = -1;
            if (!DistRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!sDistRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_District " +
                                 " SET Dist_sname = '" + subdiv_sname + "', " +
                                 " Dist_name = '" + subdiv_name + "' ,  " +
                                 " State = '" + State_name + "' ,  " +

                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Dist_code = '" + subdivision_code + "' ";

                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Town SET Town = '" + subdiv_name + "' WHERE Town_code = '" + subdivision_code + "' ";
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

                strQry = "DELETE FROM Mas_District WHERE subdivision_code = '" + subdivision_code + "' ";

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

        public int DistDeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_District" +
                            " SET Dist_Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Dist_code = '" + subdivision_code + "' ";

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
            strQry = "select Div_Code from Mas_State";
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
            strQry = "SELECT 0 as State_code,'--Select--' as Statename,'' as shortname " +
                         " UNION " +
                         " SELECT State_code,State_name,State_sname " +
                         " FROM Mas_State " +
                         " WHERE Div_Code='" + state_code + "' and State_Active_Flag=0 " +
                         " ORDER BY 2";
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
       

    }
}
