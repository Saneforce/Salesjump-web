using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class RoutePlan
    {
        private string strQry = string.Empty;

        public DataSet FetchTerritoryName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT a.Territory_Code Territory_Code, " +
            //         " (a.Territory_Name +  ' (' + CAST((select COUNT(distinct b.ListedDrCode) from Mas_ListedDr b " +
            //         " where CONVERT(char(3), a.Territory_Code) like  b.Territory_Code and b.ListedDr_Active_Flag=0 and a.sf_code = '" + sf_code + "' ) as CHAR(3)) " +
            //         " + ') ' ) Territory_Name " +
            //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + sf_code + "' AND a.territory_active_flag=0 ";

            strQry = " SELECT a.Territory_Code Territory_Code, a.Territory_Name Territory_Name, a.Territory_Cat " +
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
        public DataSet GetTerritoryName(string sf_code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT  Territory_Code, " +
                     "  Territory_Name " +
                     " FROM  Mas_Territory_Creation  where sf_code = '" + sf_code + "' AND Territory_Code = '" + terr_code + "'";
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

        public DataSet GetTerritoryCode(string sf_code, string Doc_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT  Territory_Code FROM " +
                        "Mas_ListedDr  " +
                        "WHERE Sf_Code =  '" + sf_code + "' and " +
                        " listeddrcode ='" + Doc_Code + "'";

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
       
        public DataTable get_ListedDoctor_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            //Modified to include territory instead of call plan

            //strQry = "SELECT d.ListedDrCode,d.ListedDr_Name, t.territory_name, d.Territory_Code, d.SLVNo, '' color FROM " +
            //            "Mas_ListedDr d, Mas_Territory_Creation t " +
            //            "WHERE d.Sf_Code =  '" + sfcode + "' and " +
            //            " d.Territory_Code = '" + TerrCode + "' and " +
            //            " d.Territory_Code  = t.Territory_Code and " +
            //            " d.ListedDr_Active_Flag = 0 " +
            //            " Order By 2";

            strQry = "SELECT distinct d.ListedDrCode,d.ListedDr_Name, '' territory_name, " + 
                     TerrCode + " as Territory_Code, d.SLVNo, '' color FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sfcode + "' and " +
                        " (d.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " d.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or d.Territory_Code like '%" + TerrCode + "%') and " +
                        " d.ListedDr_Active_Flag = 0 " +
                        " Order By 2";
            
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
        public int getMissedDr(string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count (ListedDrCode) FROM " +
                        "Mas_ListedDr  " +
                        "WHERE Sf_Code =  '" + sf_code + "' and " +
                        " Territory_Code='' and ListedDr_Active_Flag = 0 ";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable get_MissedDoctor(string sf_code)
        {
            int iReturn = -1;
            DataTable dtListedDR = null;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ListedDrCode,ListedDr_Name,'' territory_name, SLVNo, '' color FROM " +
                        "Mas_ListedDr  " +
                        "WHERE Sf_Code =  '" + sf_code + "' and " +
                        " Territory_Code='' ";

                dtListedDR = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public int Std_WorkPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                     " SET Territory_Code = '" + Territory_Code + "' " +
                     " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";


                //strQry = "UPDATE Call_Plan " +
                //         " SET Territory_Code = '" + Territory_Code + "' " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Remove_CallPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "Delete from Call_Plan " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Territory_Code = '" + Territory_Code + "'  ";

                //iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_ListedDr " +
                         " Set Territory_Code ='" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Remove_CallPlan_Single(string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "Delete from Call_Plan " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Territory_Code = '" + Territory_Code + "'  ";

                //iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_ListedDr " +
                         " Set Territory_Code ='' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Update_CallPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Call_Plan " +
                //         " SET Territory_Code = '" + Territory_Code + "' " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                //iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_ListedDr " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Copy_WorkPlan(string Territory_Code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                //Insert a record into Call Plan

                strQry = "SELECT ISNULL(MAX(cast(Plan_No as int)),0)+1 FROM Call_Plan ";
                int iPlanNo = db.Exec_Scalar(strQry);

                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "insert into Call_Plan values('" + sf_code + "', '" + Territory_Code + "', getdate(), '" + iPlanNo + "', " +
                        " '" + Doc_Code + "', '" + Division_Code + "', 0,'')";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int getTerrDR_count(string sf_code, string terr_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT distinct count (ListedDrCode) FROM " +
                        "Mas_ListedDr  " +
                        "WHERE ListedDr_Active_Flag=0 and Sf_Code =  '" + sf_code + "' and " +
                        " (Territory_Code like '" + terr_code + ',' + "%'  or " +
                     "Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '%" + terr_code + "%') ";

                //iReturn = db.ExecQry(strQry);
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Std_WorkPlan_Multiple(string terr_code, string Doc_Code, string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                     " SET Territory_Code = Territory_Code +'" + terr_code + ",' " +
                     " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";


                //strQry = "UPDATE Call_Plan " +
                //         " SET Territory_Code = '" + Territory_Code + "' " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable get_catg_SF(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT distinct a.Doc_Cat_Code, b.Doc_Cat_Name, b.Doc_Cat_SName, isnull(b.No_of_visit,0) No_of_visit " +
                     " FROM Mas_ListedDr a, Mas_Doctor_Category b " +
                        " WHERE a.Sf_Code =  '" + sf_code + "' and " +
                        " a.Doc_Cat_Code = b.Doc_Cat_Code " +
                        " and a.ListedDr_Active_Flag = 0 ";

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

        //public DataTable get_ListedDoctor_Territory_Catg(string sfcode, string TerrCode)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataTable dtListedDR = null;

        //    strQry = "SELECT distinct d.ListedDrCode,d.ListedDr_Name, '' territory_name, " +
        //             TerrCode + " as Territory_Code, d.SLVNo, '' color FROM " +
        //                "Mas_ListedDr d " +
        //                "WHERE d.Sf_Code =  '" + sfcode + "' and " +
        //                " d.Territory_Code like '%" + TerrCode + "%' and " +
        //                " d.ListedDr_Active_Flag = 0 " +
        //                " Order By 2";

        //    try
        //    {
        //        //dsListedDR = db_ER.Exec_DataSet(strQry);
        //        dtListedDR = db_ER.Exec_DataTable(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dtListedDR;
        //}


        public DataTable get_ListedDoctor_Territory_Catg(string sf_code, string catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT distinct d.ListedDrCode,d.ListedDr_Name," +
                      " case when d.Doc_Type=1 then 'HQ' " +
                     " else case when d.Doc_Type=2 then 'EX' " +
                     " else case when d.Doc_Type=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Doc_Type, " +

                        "d.Territory_Code  FROM " +
                        "Mas_ListedDr d " +
                        "WHERE d.Sf_Code =  '" + sf_code + "' and " +
                        " d.Doc_Cat_Code = '" + catg_code + "' and " +
                        
                        " d.ListedDr_Active_Flag = 0 " +
                        " Order By 2";

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

        public DataTable get_ListedDoctor_Territorywise(string sf_code, string terr_code, string doc_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT ListedDr_Name FROM " +
                        "Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sf_code + "' and " +
                        " (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '%" + terr_code + "%') and " +
                        " ListedDrCode = '" + doc_code + "' and  " +
                        " ListedDr_Active_Flag = 0 ";

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

        public DataSet get_ListedDoctor_TerritoryCode(string sf_code, string doc_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dtListedDR = null;

            strQry = "SELECT Territory_Code FROM " +
                        "Mas_ListedDr " +
                        "WHERE Sf_Code =  '" + sf_code + "' and " +
                        " ListedDrCode = '" + doc_code + "' and  " +
                        " ListedDr_Active_Flag = 0 ";

            try
            {
                dtListedDR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtListedDR;
        }

        public int Edit_Lstdoc_Plan(string sf_code, string terr_code, string doc_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_ListedDr " +
                     " SET Territory_Code =  '" + terr_code + "' " +
                     " WHERE ListedDrCode='" + doc_code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataTable view_RoutePlan(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "select count(Territory_Code) cnt, b.Sf_HQ , b.Sf_Code " +
                        " from Mas_Territory_Creation a, Mas_Salesforce b " +
                        "WHERE a.Sf_Code =  '" + sfcode + "' " +
                        " and a.SF_Code = b.Sf_Code and Territory_Active_Flag = 0 " +
                        " group by b.Sf_HQ , b.Sf_Code  ";

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




        public DataTable get_ListedDoctor_RoutePlan(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT distinct lst.ListedDrCode, lst.ListedDr_Name,  " +
                        " mdc.Doc_Cat_Name, mdc.Doc_Cat_SName, mds.Doc_Special_Name , mdcs.Doc_ClsName, mdcs.Doc_ClsSName , " +
                        " mdq.Doc_QuaName,mdq.Doc_QuaSName, mds.Doc_Special_SName  " +
                        " FROM Mas_ListedDr lst, Mas_Doctor_Category mdc, Mas_Doctor_Speciality mds,  " +
                        " Mas_Doc_Class mdcs, Mas_Doc_Qualification mdq " +
                        "WHERE lst.Sf_Code =  '" + sfcode + "' and " +
                        " (lst.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " lst.Territory_Code like '%" + ',' + TerrCode + ',' + "%' or lst.Territory_Code like '%" + TerrCode + "%') and " +
                        " lst.ListedDr_Active_Flag = 0 " +
                        " and lst.Doc_Cat_Code = mdc.Doc_Cat_Code " +
                        " and lst.Doc_Special_Code = mds.Doc_Special_Code " +
                        " and lst.Doc_ClsCode = mdcs.Doc_ClsCode " +
                        " and lst.Doc_QuaCode = mdq.Doc_QuaCode " +
                        " Order By 2";

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

        public int getAllocatedDrCnt(string sf_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count (ListedDrCode) FROM " +
                        "Mas_ListedDr  " +
                        "WHERE Sf_Code =  '" + sf_code + "' and " +
                        " Territory_Code!='' and ListedDr_Active_Flag = 0";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int get_ListedDoctor_TerritoryCatgwise(string sf_code, string terr_code, string catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "SELECT count (a.ListedDrCode)  AS cnt FROM " +
                        "Mas_ListedDr a, Mas_Doctor_Category b" +
                        " WHERE a.Sf_Code =  '" + sf_code + "' and " +
                        " a.Doc_Cat_Code  = b.Doc_Cat_Code  and " +
                        " (a.Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " a.Territory_Code like '%" + ',' + terr_code + ',' + "%' or a.Territory_Code like '%" + terr_code + "%') and " +
                        " b.Doc_Cat_Code   = '" + catg_code  + "' and  " +
                        " a.ListedDr_Active_Flag = 0 ";

            try
            {
                iReturn = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

		public DataSet get_Route_Name(string divcode, string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec get_Route_Name '" + sf_Code + "','" + divcode + "'";
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