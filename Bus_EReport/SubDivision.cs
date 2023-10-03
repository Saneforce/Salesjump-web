using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class SubDivision
    {
        private string strQry = string.Empty;

        public DataSet getSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_code,a.subdivision_sname,a.subdivision_name,SubDivision_Active_Flag," +
                     " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "' and d.Product_Active_Flag=0) Sub_Count" +
                     ",(select count(e.subdivision_code) from Mas_Salesforce e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + divcode + ",' and e.SF_Status=0) SubField_Count" +
                    " FROM mas_subdivision a WHERE a.Div_Code= '" + divcode + "'" +
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
        //Giri grid View 13.06.16
        public DataSet AreagetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //strQry = "SELECT Area_code,a.Area_sname,a.Area_name,state," +
            //         " (select count(d.Zone_code) from Mas_Zone d where d.Div_Code ='"+divcode+"') Sub_Coun " +
            //         " FROM Mas_Area a WHERE a.Area_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
            //         " ORDER BY 2";
            strQry = " select a.Area_code,a.Area_sname,a.Area_name,a.state, " +
                     " COUNT(z.Area) as 'Sub_Coun' from mas_zone z full join " +
                     " Mas_Area a on a.Area_name=z.Area and z.Div_Code='"+divcode+"'and z.Zone_Active_Flag=0 WHERE a.Area_Active_Flag=0 and a.Div_Code= '" + divcode + "'group by a.Area_name,a.Area_code,a.Area_sname,a.state ";
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
        // Sorting
        public DataTable getSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;

            strQry = "SELECT subdivision_code,subdivision_sname,subdivision_name, " +
                            " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.subdivision_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Count" +
                            ",(select count(e.subdivision_code) from Mas_Salesforce e where charindex(cast(a.subdivision_code as varchar),e.subdivision_code )> 0 and e.Division_Code ='" + divcode + ",') SubField_Count" +
                           " FROM mas_subdivision a WHERE subdivision_active_flag=0 And Div_Code= '" + divcode + "'" +
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
        //Giri Sort 13.06.16
        public DataTable AreagetSubDivisionlist_DataTable(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtSubDiv = null;


            strQry = "SELECT Area_code,Area_sname,Area_name,state," +
                           " (select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Area_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
                           " FROM Mas_Area a WHERE a.Area_Active_Flag=0 And Div_Code= '" + divcode + "'" +
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

        public DataSet getSubDiv(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT subdivision_sname,subdivision_name " +
                     " FROM mas_subdivision WHERE subdivision_active_flag=0 And subdivision_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
        //Giri Area 13.06.2016
        public DataSet getArea(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Area_sname,Area_name,State " +
                     " FROM Mas_Area WHERE Area_Active_Flag=0 And Area_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
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
        public bool AreaRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Area_code) FROM Mas_Area WHERE Area_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sAreaRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Area_code) FROM Mas_Area WHERE Area_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_name ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool sAreaRecordExist1(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Area_code) FROM Mas_Area WHERE Area_code != '" + subdivision_code + "' AND Area_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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

                strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_sname ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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
        public bool AreaRecordExist1(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Area_code) FROM Mas_Area WHERE Area_code != '" + subdivision_code + "' AND Area_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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

        public int RecordAdd(string divcode, string subdiv_sname, string subdiv_name)
        {
            int iReturn = -1;
            if (!RecordExist(subdiv_sname, divcode))
            {
                if (!sRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(subdivision_code)+1,'1') subdivision_code from mas_subdivision ";
                        int subdivision_code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO mas_subdivision(subdivision_code,div_code,subdivision_sname,subdivision_name,created_Date,LastUpdt_Date,SubDivision_Active_Flag)" +
                                 "values('" + subdivision_code + "','" + divcode + "','" + subdiv_sname.Trim() + "', '" + subdiv_name.Trim() + "',getdate(),getdate(),0) ";

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
        //Giri AreaAdd 13.06.2016
        public int AreaRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string state_name,string state_cd)
        {
            int iReturn = -1;
            if (!AreaRecordExist(subdiv_sname, divcode))
            {
                if (!sAreaRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(Area_code)+1,'1')Area_code from Mas_Area";
                        int Areadivision_code = db.Exec_Scalar(strQry);
                        strQry = "INSERT INTO Mas_Area(Area_code,Div_Code,Area_sname,Area_name,State,Created_Date,LastUpdt_Date,Area_Active_Flag,State_Code)" +
                                 "values('" + Areadivision_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + state_name + "',getdate(),getdate(),0,'"+state_cd+"')";
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


        public int RecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string divcode)
        {
            int iReturn = -1;
            if (!sRecordExist(subdivision_code, subdiv_sname, divcode))
            {
                if (!RecordExist(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();
						strQry = "UPDATE Mas_Product_Category SET Product_Cat_Div_Name = '" + subdiv_name + "' WHERE Product_Cat_Div_Code = '" + subdivision_code + "' ";
                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Product_Brand SET Product_Cat_Div_Name = '" + subdiv_name + "' WHERE Product_Cat_Div_Code = '" + subdivision_code + "' ";
                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE mas_subdivision " +
                                 " SET subdivision_sname = '" + subdiv_sname + "', " +
                                 " subdivision_name = '" + subdiv_name + "' , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE subdivision_code = '" + subdivision_code + "' ";

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
        //Giri AreaUpdate 13.06.16
        public int AreaRecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string state_name, string divcode, string state_cd)
        {
            int iReturn = -1;
            if (!sAreaRecordExist1(subdivision_code,subdiv_sname, divcode))
            {
                if (!AreaRecordExist1(subdivision_code,subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Area " +
                                 " SET Area_sname = '" + subdiv_sname + "', " +
                                 " Area_name = '" + subdiv_name + "' ,  " +
                                 " State = '" + state_name + "' ,  " +
                                 " State_Code = '" + state_cd + "' , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Area_code = '" + subdivision_code + "' ";
                       

                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Zone SET Area = '" + subdiv_name + "' WHERE Area_code = '" + subdivision_code + "' ";
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
        //Giri Area Updated 13.06.16
        public int AreaUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string state_name, string divcode,string state_cd)
        {
            int iReturn = -1;
            if (!sAreaRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!AreaRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Area " +
                                 " SET Area_sname = '" + subdiv_sname + "', " +
                                 " Area_name = '" + subdiv_name + "' ,  " +
                                 " State = '" + state_name + "' ,  " +
                                 " State_Code = '" + state_cd + "' , " +
                                 " LastUpdt_Date = getdate() "+
                                 " WHERE Area_code = '" + subdivision_code + "' ";
                        


                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Zone SET Area = '" + subdiv_name + "' WHERE Area_code = '" + subdivision_code + "' ";
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

                strQry = "DELETE FROM  mas_subdivision WHERE subdivision_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by priya

        public int DeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_subdivision " +
                            " SET subdivision_active_flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE subdivision_code = '" + subdivision_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
		 public int DeActivate1(string sdcode, string stat)
        {
            int iReturn=-1 ;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "exec Subdiv_DeActivate '" + sdcode + "'," + stat + " ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //end
        //done by Giri 14.06.2016 

        public int AreaDeActivate(int subdivision_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Mas_Area" +
                            " SET Area_Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Area_code = '" + subdivision_code + "' ";

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
                      " b.Product_Cat_Name," +
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


        public DataSet get_distributor(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "select * from Mas_Stockist where Territory_Code='" + terr_code + "' and Stockist_Active_Flag='0' order by 2";
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
        public DataSet getMenuType(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT Menu_code,a.Menu_SName,a.Menu_Name " +
                     " FROM Mas_Menu_Type a WHERE a.Active_Flag=0 " +
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

        public DataTable getListedDr_new_tb(string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;


            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
            //        " Mas_ListedDr d " +
            //        " WHERE d.Territory_Code='" + Terr_code + "'" +
            //        " and d.ListedDr_Active_Flag = 0" +
            //        " order by ListedDr_Sl_No";
            strQry = "SELECT d.ListedDrCode,d.ListedDr_Name,REPLACE(d.ListedDr_Address1,  CHAR(10), ',')  as Address, " +
                     " case when isnull(d.ListedDr_Phone, '') = '' then isnull(d.ListedDr_Phone2, '') else isnull(d.ListedDr_Phone, '')   end as Mobile_No, " +
                     " stuff((select ', ' + territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code " +
                     " and charindex(cast(t.Territory_Code as varchar) + ',', d.Territory_Code + ',') > 0 for XML path('')),1,2,'') territory_Name " +
                     " FROM  Mas_ListedDr d  WHERE d.Territory_Code = '" + Terr_code + "' and d.ListedDr_Active_Flag = 0 order by ListedDr_Name";

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

        public DataTable getListedDr_new_tb_ds(string Terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;


            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, d.ListedDr_Sl_No,d.Doc_Cat_ShortName,d.Doc_Spec_ShortName ,isnull(d.ListedDr_Mobile,d.ListedDr_phone) ListedDr_Mobile,d.Doc_Class_ShortName,d.Doc_Qua_Name,d.SDP as Activity_Date," +
            //        " stuff((select ', '+territory_Name from Mas_Territory_Creation t where t.Territory_Code = d.Territory_Code and charindex(cast(t.Territory_Code as varchar)+',',d.Territory_Code+',')>0 for XML path('')),1,2,'') territory_Name  FROM " +
            //        " Mas_ListedDr d " +
            //        " WHERE d.Territory_Code='" + Terr_code + "'" +
            //        " and d.ListedDr_Active_Flag = 0" +
            //        " order by ListedDr_Sl_No";
            strQry = "select isnull((select Stockist_Code from Mas_Stockist d where t.Dist_Name = d.Stockist_Code and  d.Stockist_Active_Flag=0 " +
                    "and charindex(cast(t.Dist_Name as varchar) + ',', d.Stockist_Code + ',')> 0 ),'') Stockist_Code, " +
                    "isnull(stuff((select ', ' + Stockist_Name from Mas_Stockist d where t.Dist_Name = d.Stockist_Code and  d.Stockist_Active_Flag = 0 " +
                    "and charindex(cast(t.Dist_Name as varchar) + ',', d.Stockist_Code + ',') > 0 for XML path('')),1,2,''),'') Stockist_Name, " +
                    "isnull((select Stockist_Address from Mas_Stockist d where t.Dist_Name = d.Stockist_Code and  d.Stockist_Active_Flag = 0 " +
                    "and charindex(cast(t.Dist_Name as varchar) + ',', d.Stockist_Code + ',') > 0),'') Stockist_Address, " +
                    "isnull((select Stockist_Mobile from Mas_Stockist d where t.Dist_Name = d.Stockist_Code and  d.Stockist_Active_Flag = 0 " +
                    "and charindex(cast(t.Dist_Name as varchar) + ',', d.Stockist_Code + ',') > 0),'') Stockist_Mobile " +
                    "from Mas_Territory_Creation t where  Territory_Code = '" + Terr_code + "' order by Stockist_Name";

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
           public DataSet getsubid(string divcode, string scode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select subdivision_code,subdivision_name from  mas_subdivision where subdivision_code='" + scode + "'and Div_Code='" + divcode + "'";
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


    }
}
