using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;

namespace Bus_EReport
{
    public class Territorys
    {
        private string strQry = string.Empty;


        //Giri grid View 16.06.16
        public DataSet TerritorygetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Territory_code,a.Territory_sname,a.Territory_name,Zone" +
                      ",(select count(d.Territory_code) from Mas_Salesforce d where charindex(CAST(a.Territory_code AS varchar),d.Territory_code  )> 0) FO_Coun" +
                     " FROM Mas_Territory a WHERE a.Territory_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
                     "ORDER BY 3";
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

	 public DataSet TerritorygetSF_Code(string divcode,string sf_cdoe)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "exec [Get_Territory_HQ] '"+sf_cdoe+"','"+divcode+"'";
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
        public DataTable TerritorygetSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;


            strQry = "SELECT Territory_code,Territory_sname,Territory_name,Zone" +
                           //" (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Territory_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
                           " FROM Mas_Territory a WHERE a.Territory_Active_Flag=0 And Div_Code= '" + divcode + "'" +
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


        //Giri Territory 13.06.2016
        public DataSet getTerritory(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Territory_sname,Territory_name,Zone " +
                     " FROM Mas_Territory WHERE Territory_Active_Flag=0 And Territory_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
public DataSet get_Territory(string sf_code, string terr_Route_Code, string Division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Route_Code, Territory_Name,Target,Min_Prod,Population,SF_Code " +
                     " FROM  Mas_Territory_Creation " +
                     " where territory_active_flag=0 and Territory_Code= '" + terr_Route_Code + "' and Dist_Name='" + sf_code + "' AND Division_Code= '" + Division_code + "'";
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
        public bool TerritoryRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_code) FROM Mas_Territory WHERE Territory_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sTerritoryRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_code) FROM Mas_Territory WHERE Territory_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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


        //Giri TerritoryAdd 13.06.2016
        public int TerritoryRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Zone_name, string Zone_cd)
        {
            int iReturn = -1;
            if (!TerritoryRecordExist(subdiv_sname, divcode))
            {
                if (!sTerritoryRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Territory_code)+1,'1')Territory_code from Mas_Territory";
                        int Territorydivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_Territory(Territory_code,Div_Code,Territory_sname,Territory_name,Zone,Zone_code,Created_Date,LastUpdt_Date,Territory_Active_Flag)" +
                                 "values('" + Territorydivision_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + Zone_name + "','" + Zone_cd + "',getdate(),getdate(),0)";
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

        public bool sTerritoryRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_code) FROM Mas_Territory WHERE Territory_code != '" + subdivision_code + "' AND Territory_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool TerritoryRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_code) FROM Mas_Territory WHERE Territory_code != '" + subdivision_code + "' AND Territory_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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

        //Giri TerritoryUpdate 13.06.16
        public int TerritoryRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Zone_name, string divcode)
        {
            int iReturn = -1;
            if (!TerritoryRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!sTerritoryRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Territory " +
                                 " SET Territory_sname = '" + subdiv_sname + "', " +
                                 " Territory_name = '" + subdiv_name + "' ,  " +
                                 " Zone ='" + Zone_name + "'," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Territory_code = '" + subdivision_code + "' ";

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
        //Giri Territory Updated 13.06.16
        public int TerritoryUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string Zone_name, string divcode, string Zone_cd)
        {
            int iReturn = -1;
            if (!TerritoryRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!sTerritoryRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Territory " +
                                 " SET Territory_sname = '" + subdiv_sname + "', " +
                                 " Territory_name = '" + subdiv_name + "' ,  " +
                                 " Zone = '" + Zone_name + "' ,  " +
                                 " Zone_code ='" + Zone_cd + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Territory_code = '" + subdivision_code + "' ";

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

                strQry = "DELETE FROM Mas_Territory WHERE subdivision_code = '" + subdivision_code + "' ";

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

        public int TerritoryDeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_Territory" +
                            " SET Territory_Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Territory_code = '" + subdivision_code + "' ";

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
            strQry = "select Div_Code from Mas_Zone";
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
            strQry = "SELECT 0 as Zone_code,'--Select--' as Zonename,'' as shortname " +
                         " UNION " +
                         " SELECT Zone_code,Zone_name,Zone_sname " +
                         " FROM Mas_Zone " +
                         " WHERE Div_Code='" + state_code + "' and Zone_Active_Flag=0 " +
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
        public DataSet Zone_code(string Zone_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Zone_code from Mas_Zone where Zone_name='" + Zone_name + "' ";
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
		 public int Distance_add(string divcode, string terr_hq, string from_code, string to_code, decimal distance, string Place_Type)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                string sf_code="";
                DataTable dt = new DataTable();
                strQry = "select sf_Code+',' from mas_salesforce where  Territory_code="+terr_hq+" for xml path('')";
                dt = db.Exec_DataTable(strQry);

                sf_code = dt.Rows[0][0].ToString();
                strQry = "exec [Insert_distance] '" + divcode + "'," + terr_hq + ",'" + from_code + "','" + to_code + "'," + distance + ",'" + Place_Type + "','" + sf_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        public DataSet Get_Distance_Values(string division_code,string terr_hq )
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
           
            strQry = "select * from Mas_Distance_Entry where Division_Code='"+ division_code+"' and Territory_Ho='"+terr_hq+"'";
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

        public DataSet getSF_Code_Route(string div_Route_Code,string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "select Territory_Code,Territory_Name from Mas_Territory_Creation where  Territory_Active_Flag='0' and  charindex(','+cast('" + sf_code + "' as varchar)+',',','+SF_Code+',')>0  and Division_Code='" + div_Route_Code + "'ORDER BY 2";

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

        public DataSet getSF_Code_Wtyp(string div_Route_Code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "exec [GetWorkTypes_App] '" + sf_code + "'";

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

        public DataSet getSF_Code_MR(string sf_code, string div)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
            strQry = "exec [getHyrSFList_MR] '" + sf_code + "','"+div+"'";

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

        public DataSet getRoutesBySF(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC GetTerritoryByID '" + sf_code + "'";

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

        public DataSet getRoutesByManager(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC GetTerritoryByManagerID '" + sf_code + "'";

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

    }
}
