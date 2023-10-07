using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class Zone
    {
        private string strQry = string.Empty;

       
        //Giri grid View 13.06.16
        public DataSet ZonegetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //strQry = "SELECT Zone_code,a.Zone_sname,a.Zone_name,Area," +
            //         "(select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Zone_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //         " FROM Mas_Zone a WHERE a.Zone_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
            //         "ORDER BY 2";
            strQry = " select Z.Zone_code,Z.Zone_sname,Z.Zone_name,Z.Area,COUNT(T.Zone) as 'Sub_Coun'  from Mas_Territory T "+
                     " right outer join  Mas_Zone Z on Z.Zone_code=T.Zone_code and T.Territory_Active_Flag=0 " +
                     " where Z.Zone_Active_Flag=0 and Z.Div_Code='"+divcode+"' group by Z.Zone_name,Z.Zone_code,Z.Zone_sname,Z.Area "; 
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
        public DataTable ZonegetSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;


            //strQry = "SELECT Zone_code,Zone_sname,Zone_name,Area," +
            //               " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Zone_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //               " FROM Mas_Zone a WHERE a.Zone_Active_Flag=0 And Div_Code= '" + divcode + "'" +
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

       
        //Giri Zone 13.06.2016
        public DataSet getZone(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Zone_sname,Zone_name,Area " +
                     " FROM Mas_Zone WHERE Zone_Active_Flag=0 And Zone_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
        public bool sZoneRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool ZoneRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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

        
        //Giri ZoneAdd 13.06.2016
        public int ZoneRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Area_name,string Area_cd)
        {
            int iReturn = -1;
            if (!sZoneRecordExist(subdiv_sname, divcode))
            {
                if (!ZoneRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Zone_code)+1,'1')Zone_code from Mas_Zone";
                        int Zonedivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_Zone(Zone_code,Div_Code,Zone_sname,Zone_name,Area,Area_code,Created_Date,LastUpdt_Date,Zone_Active_Flag)" +
                                 "values('" + Zonedivision_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + Area_name + "','" + Area_cd + "',getdate(),getdate(),0)";
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

        public bool sZoneRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "' AND Zone_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool ZoneRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "' AND Zone_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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
       
        //Giri ZoneUpdate 13.06.16
        public int ZoneRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Area_name, string divcode,string Area_code)
        {
            int iReturn = -1;
            if (!sZoneRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!ZoneRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Zone " +
                                 " SET Zone_sname = '" + subdiv_sname + "', " +
                                 " Zone_name = '" + subdiv_name + "' ,  " +
                                 " Area= '"+Area_name+ "', " +
                                 " Area_code= '" + Area_code + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Zone_code = '" + subdivision_code + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Territory SET Zone = '" + subdiv_name + "' WHERE Zone_code = '" + subdivision_code + "' ";
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
        //Giri Zone Updated 13.06.16
        public int ZoneUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Area_name, string divcode, string Area_cd)
        {
            int iReturn = -1;
            if (!sZoneRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!ZoneRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Zone " +
                                 " SET Zone_sname = '" + subdiv_sname + "', " +
                                 " Zone_name = '" + subdiv_name + "' ,  " +
                                 " Area = '" + Area_name + "' ,  " +
                                 " Area_code ='" + Area_cd + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Zone_code = '" + subdivision_code + "' ";

                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Territory SET Zone = '" + subdiv_name + "' WHERE Zone_code = '" + subdivision_code + "' ";
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

                strQry = "DELETE FROM Mas_Zone WHERE subdivision_code = '" + subdivision_code + "' ";

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

        public int ZoneDeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_Zone" +
                            " SET Zone_Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Zone_code = '" + subdivision_code + "' ";

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
            strQry = "select Div_Code from Mas_Area";
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
            strQry="SELECT 0 as Area_code,'--Select--' as Areaname,'' as shortname "+ 
                         " UNION " + 
                         " SELECT Area_code,Area_name,Area_sname " + 
                         " FROM Mas_Area " +
                         " WHERE Div_Code='"+state_code+"' and Area_Active_Flag=0 " +
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
        public DataSet area_code(string Area_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Area_code from Mas_Area where Area_name='" + Area_name + "' ";
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
        public DataSet Menuget(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //strQry = "SELECT Zone_code,a.Zone_sname,a.Zone_name,Area," +
            //         "(select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Zone_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //         " FROM Mas_Zone a WHERE a.Zone_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
            //         "ORDER BY 2";
            strQry = " select a.Menu_ID,a.Menu_Name,a.Parent_ID " +
                     " from Mas_Menu_Details a " +
                     " WHERE a.Active_Flag=0 group by a.Menu_ID,a.Menu_Name,a.Parent_ID ";
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
        public DataSet getMenu(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Menu_ID,Menu_Name,Parent_ID,HRef " +
                     " FROM Mas_Menu_Details WHERE Active_Flag=0 And Menu_ID= '" + subdivcode + "'" +
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
        public bool sMenuRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "' AND Zone_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool MenuRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "' AND Zone_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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
        public bool sMenuRecordExist(string subdivision_code, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "'  and Div_Code= '" + divcode + "' ";

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
        public bool MenuRecordExist(string subdivision_code, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Zone_code) FROM Mas_Zone WHERE Zone_code != '" + subdivision_code + "'  and Div_Code= '" + divcode + "' ";

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
        public int MenuRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Area_name, string Area_cd, string Icon)
        {
            int iReturn = -1;
            if (!sMenuRecordExist(subdiv_sname, divcode))
            {
                if (!MenuRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        if (Area_name == "- Main Menu -") Area_name = "null";
                        strQry = "SELECT isnull(count(Menu_ID)+1,'1')Menu_ID from Mas_Menu_Details";
                        int Zonedivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_Menu_Details(Menu_ID,Menu_Name,Parent_ID,Active_Flag,HRef,Menu_Icon)" +
                                 "values('" + Zonedivision_code + "','" + subdiv_name + "'," + Area_name + ",0,'" + Area_cd + "','" + Icon + "')";
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
       
        public int MenuRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Area_name, string divcode, string Area_code, string Icon)
        {
            int iReturn = -1;
            if (!sMenuRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!MenuRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Menu_Details " +
                                 " SET Menu_Name = '" + subdiv_name + "', " +
                            //" Zone_name = '" + subdiv_name + "' ,  " +
                                 " Parent_ID= '" + Area_name + "', " +
                                 " HRef= '" + Area_code + "', " +
                                 " Menu_Icon = '" + Icon + "' " +
                                 " WHERE Menu_ID = '" + subdiv_sname + "' ";

                        iReturn = db.ExecQry(strQry);

                        //strQry = "UPDATE Mas_Territory SET Zone = '" + subdiv_name + "' WHERE Zone_code = '" + subdivision_code + "' ";

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
        public int MenuDeActivate(string subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_Menu_Details" +
                            " SET Active_Flag=1  " +
                            " WHERE Menu_ID = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet getMenuP(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT 0 as state_code,'ALL' as statename,'' as shortname " +
            //         " UNION " +
            //         " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
            //         " ORDER BY 2";
            strQry = "SELECT '0' as Menu_ID,'--Select--' as Menu_Name " +
                         " UNION " +
                         " SELECT Menu_ID,Menu_Name " +
                         " FROM Mas_Menu_Details " +
                         " WHERE Active_Flag=0 " +
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
