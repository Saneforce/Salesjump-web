using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class Territory
    {
        private string strQry = string.Empty;

        public DataSet getEmptyTerritory()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT TOP 10 '' Territory_Name,'' Territory_SName " +
            //         " FROM  sys.tables ";
            strQry = "SELECT TOP 10 ''Route_Code,'' Territory_Name,'' Dist_Name,'' Target,'' Min_Prod " +
                     "FROM  sys.tables ";
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

        public int DeActivate(string terr_Route_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                            " SET territory_active_flag=1  " +
                            " WHERE Territory_Code = '" + terr_Route_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public bool cRecordExist1(string Territory_Code, string Sf_Code, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Route_Code) FROM Mas_Territory_Creation WHERE Route_Code ! ='" + Territory_Code + "'and Sf_Code = '" + Sf_Code + "'and Division_Code ='" + div_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist < 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool RecordExist1(string Territory_Name, string Sf_Code, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Name) FROM Mas_Territory_Creation WHERE Territory_Name ! ='" + Territory_Name + "' and Sf_Code = '" + Sf_Code + "'and Division_Code ='" + div_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist < 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int RecordUpdate(string Terr_code, string Route_Code, string Territory_Name, string Territory_Type, string sf_code, string Target, string min, string Div_code)
        {
            int iReturn = -1;
            if (!cRecordExist1(Route_Code, sf_code, Div_code))
            {
                if (!RecordExist1(Territory_Name, sf_code, Div_code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Territory_Creation " +
                                     " SET Route_Code ='" + Route_Code + "'," +
                                    " Territory_Name = '" + Territory_Name + "', " +
                                      " Dist_Name = '" + Territory_Type + "', " +
                                      " SF_Code ='" + sf_code + "', " +
                                      " Target = '" + Target + "', LastUpdt_Date= getdate(), " +
                                       " Min_Prod ='" + min + "' " +
                                        " WHERE Territory_Code = '" + Terr_code + "' ";

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


        public int TransferTerritory(string Territory_Code, string Target_Territory)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Chemists " +
                            " SET Territory_Code = '" + Target_Territory + "'  " +
                            " WHERE Territory_Code = '" + Territory_Code + "'";

                iReturn = db.ExecQry(strQry);

                if (iReturn != -1)
                {
                    strQry = "UPDATE Mas_ListedDr " +
                                " SET Territory_Code = '" + Target_Territory + "'  " +
                                " WHERE Territory_Code = '" + Territory_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    //if (iReturn != -1)
                    //{
                    //    strQry = "UPDATE Mas_UnListedDr " +
                    //                " SET Territory_Code = '" + Target_Territory + "'  " +
                    //                " WHERE Territory_Code = '" + Territory_Code + "' ";

                    //    iReturn = db.ExecQry(strQry);

                    //    if (iReturn != -1)
                    //    {
                    //        strQry = "UPDATE Mas_Hospital " +
                    //                    " SET Territory_Code = '" + Target_Territory + "'  " +
                    //                    " WHERE Territory_Code = '" + Territory_Code + "' ";

                    //        iReturn = db.ExecQry(strQry);

                    if (iReturn != -1)
                    {
                        strQry = "UPDATE Mas_Territory_Creation " +
                                    " SET territory_active_flag=1, Territory_Deactive_Date=getdate()  " +
                                    " WHERE Territory_Code = '" + Territory_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }


                }

            }




            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet get_Territory(string sf_code, string terr_Route_Code, string Division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Route_Code, Territory_Name,Target,Min_Prod " +
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


        public DataSet getTerritory(string Sf_Code, string terr_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_Code = '" + Sf_Code + "' " +
                     " AND Territory_Code != '" + terr_Route_Code + "' AND territory_active_flag=0 ";
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

        public DataSet getTerritory(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code,a.Route_Code,a.Territory_Name, a.Dist_Name,a.Target,a.Min_Prod," +


                     " (select COUNT(b.ListedDrCode) " +
                     " from Mas_ListedDr b where b.Territory_Code like '%,'+cast(a.Territory_Code as varchar) or b.Territory_Code like cast(a.Territory_Code as varchar)+',%' " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Dist_Name) from Mas_Territory_Creation b where b.Territory_Code=a.Territory_Code and " +
                     "  b.Territory_Active_Flag=0) Chemists_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "'  and a.Territory_Active_Flag=0" +
                     " order by Territory_SNo";
            //strQry = " SELECT Territory_Code,Route_Code,Territory_Name,Dist_Name,Target,Min_Prod " +
            //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "'  and a.Territory_Active_Flag=0 " +
            //         " order by Territory_Code";
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

        // Sorting For Territory Creation

        public DataTable getTerritory_DataTable(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Route_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Route_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "' AND a.territory_active_flag=0 ";

            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        // Sorting For Territory List 
        public DataTable getTerritorylist_DataTable(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where convert(varchar(10),a.Territory_Code)=b.Territory_Code " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Route_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Route_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "' AND a.territory_active_flag=0 ";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }
        //Sorting for G.No
        public DataTable getTerr_DtTable(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtTerr = null;

            //strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
            //         " case when Territory_Cat=1 then 'HQ' " +
            //         " else case when Territory_Cat=2 then 'EX' " +
            //         " else case when Territory_Cat=3 then 'OS' " +
            //         " else 'OS-EX' " +
            //         " end end end as Territory_Cat " +               
            //         " FROM  Mas_Territory_Creation where Sf_Code = '" + Sf_Code + "' AND territory_active_flag=0 ";

            strQry = " SELECT Territory_Code,Route_Code,Territory_Name " +
                      " FROM  Mas_Territory_Creation where Sf_Code = '" + Sf_Code + "' AND territory_active_flag=0 " +
                      " ORDER BY Territory_SNo";
            try
            {
                dtTerr = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtTerr;
        }

        //End

        public DataSet getTerritory_Det(string Sf_Code, string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
                     " case when Territory_Cat=1 then 'HQ' " +
                     " else case when Territory_Cat=2 then 'EX' " +
                     " else case when Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,Alias_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + Sf_Code + "' AND Territory_Code='" + Territory_Code + "' and territory_active_flag=0 ";
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
        public int getTerritory_Al(string Territory_Code, string Alias_Name, string Territory_SName)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                             " SET Alias_Name='" + Alias_Name + "', Territory_SName='" + Territory_SName + "' " +
                             " WHERE Territory_Code = '" + Territory_Code + "' and territory_active_flag=0  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Terr_SlNO(string Territory_Code, string Sno)
        {
            int iReturn = -1;
            //if (!RecordExist(Product_Cat_Route_Code, Product_Cat_SName))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET Territory_SNo = '" + Sno + "', " +
                        " LastUpdt_Date = getdate() " +
                         " WHERE Territory_Code = '" + Territory_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getTerritory_Slno(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT Territory_Code, Territory_Name, Territory_SName, " +
            //         " case when Territory_Cat=1 then 'HQ' " +
            //         " else case when Territory_Cat=2 then 'EX' " +
            //         " else case when Territory_Cat=3 then 'OS' " +
            //         " else 'OS-EX' " +
            //         " end end end as Territory_Cat " +                   
            //         " FROM  Mas_Territory_Creation where Sf_Code = '" + Sf_Code + "' AND territory_active_flag=0 "+
            //         " ORDER BY Territory_SNo"; ;
            strQry = " SELECT Territory_Code,Route_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + Sf_Code + "' AND territory_active_flag=0 " +
                     " ORDER BY Territory_SNo";
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

        //Changes done by Saravana     

        //public DataSet getWorkAreaName()
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = "select wrk_area_Name from Admin_Setups";
        //    try
        //    {
        //        dsProCat = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsProCat;
        //}
        //End
         public bool cRecordExist(string Territory_Code, string Territory_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Route_Code) FROM Mas_Territory_Creation WHERE Route_Code ='" + Territory_Code + "'and Territory_Name != '" + Territory_Name + "' and Division_Code='"+ div_code +"'";
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
        public bool RecordExist(string Territory_Name, string Territorys_Route_Code,string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Name) FROM Mas_Territory_Creation WHERE Territory_Name='" + Territory_Name + "' and Route_Code ! = '" + Territorys_Route_Code + "' and Division_Code='"+div_code+"'";
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

        public int RecordAdd(string Territorys_Route_Code, string Territory_Name, string Territory_Type, string Sf_Code, string Target, string min_prod, string Div_code)
        {
            int iReturn = -1;

             if (!cRecordExist(Territorys_Route_Code, Territory_Name,Div_code))
            {
                if (!RecordExist(Territory_Name, Territorys_Route_Code,Div_code))
                {

                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        int division_Code = -1;
                        int Territory_Code = -1;
                        int Town_code = -1;
                        strQry = "select div_Code from Mas_DSM where DSM_code = '" + Sf_Code + "' ";
                        division_Code = db.Exec_Scalar(strQry);
                        strQry = "select Town_Code from Mas_DSM where DSM_code = '" + Sf_Code + "'";
                        Town_code = db.Exec_Scalar(strQry);

                        //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE division_Code = '" + division_Code + "' ";
                        strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation ";
                        Territory_Code = db.Exec_Scalar(strQry);

                        strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Dist_Name,division_Code, " +
                                 " Sf_Code,Territory_Active_Flag,Created_date,Target,Min_Prod,Route_Code,Town_Code) " +
                                 " VALUES('" + Territory_Code + "', '" + Territory_Name + "', '" + Territory_Type + "',  " +
                                 " '" + division_Code + "', '" + Sf_Code + "', 0, getdate(),'" + Target + "','" + min_prod + "','" + Territorys_Route_Code + "','" + Town_code + "')";

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
        public int RecordCount(string Sf_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Code) FROM Mas_Territory_Creation WHERE Sf_Code = '" + Sf_Code + "' and Territory_Active_Flag = 0";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Saravanan
        public DataSet getTerrritoryView(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select DSM_name as sfName from Mas_DSM where Distributor_Code='" + Sf_Code + "'";

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
        //Changes done by Saravana     
        public DataSet getWorkAreaName(string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            if (div_Route_Code.Contains(','))
            {
                div_Route_Code = div_Route_Code.Remove(div_Route_Code.Length - 1);
                strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where division_Code in(" + div_Route_Code + ")";
            }
            else
            {

                strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups where division_Code='" + div_Route_Code + "'";
            }

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

        //public DataSet getWorkAreaName()
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = "select wrk_area_Name,No_of_TP_View,wrk_area_SName from Admin_Setups ";

        //    try
        //    {
        //        dsProCat = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsProCat;
        //}
        //End
        public DataSet getSF_Code(string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT '' as Sf_Code, '---Select---' as Sf_Name " +
            //         " UNION " +
            //         " select Sf_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name from Mas_Salesforce where (division_Code like '" + div_Route_Code + ',' + "%'  or " +
            //          " division_Code like '%" + ',' + div_Route_Code + ',' + "%') " +
            //         " and sf_type=1 and sf_status = 0 ";
            strQry = "SELECT '' as DSM_code, '---Select---' as DSM_name " +
                     " UNION " +
                     " select DSM_code,DSM_name from Mas_DSM where DSM_Active_Flag=0 and Distributor_Code ='" + div_Route_Code + "'";
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

        public DataSet getSF_Code_distributor(string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Stockist_Code, '---Select---' as stockist_Name " +
                     " UNION " +
                " select Stockist_Code,stockist_Name from Mas_Stockist  where Division_Code='" + div_Route_Code + "'";
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
        public DataSet getSF_Code_Route(string div_Route_Code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;
			strQry = " SELECT '0' as Territory_Code, '---Select---' as Territory_Name " +
                     " UNION " +
                     "select [Territory_Code],[Territory_Name] from Mas_Territory_Creation where  Dist_Name='" + distributor + "' and Division_Code='" + div_Route_Code + "'";
            
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
        public DataSet getHQ_Dist(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                       " case when a.Territory_Cat=1 then 'HQ' " +
                       " else case when a.Territory_Cat=2 then 'EX' " +
                       " else case when a.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Territory_Cat, " +
                       " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                       " and b.ListedDr_Active_Flag=0) ListedDR_Count  " +
                       " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "' and  Territory_Cat=1  and a.Territory_Active_Flag=0" +

                       " order by Territory_Name";
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
        public DataSet getOS_Dist(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.OS_Distance,c.Sf_HQ " +
                //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + Sf_Code + "' and Territory_Cat=3  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
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
        public DataSet getEX_Dist(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name,  " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat,a.Ex_Distance, c.Sf_HQ" +
                //" (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code= CAST(a.Territory_Code as varchar) " +
                //" and b.ListedDr_Active_Flag=0) ListedDR_Count " +
                     " FROM  Mas_Territory_Creation a, Mas_Salesforce c where a.Sf_Code = '" + Sf_Code + "' and Territory_Cat=2  and a.Territory_Active_Flag=0" +
                      " and a.Sf_Code=c.Sf_Code " +
                     " order by Territory_Name";
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

        public DataSet getOSEX_Dist(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = " SELECT * FROM( SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName,case when a.Territory_Cat=1 then 'HQ' "+ 
            //         " else case when a.Territory_Cat=2 then 'EX'  else case when a.Territory_Cat=3 then 'OS' "+
            //          " else 'OSEX'  end end end as Territory_Cat,  (select COUNT(b.ListedDrCode) "+                      
            //           " from Mas_ListedDr b where a.Territory_Code=b.Territory_Code  and "+                       
            //           " b.ListedDr_Active_Flag=0) ListedDR_Count   FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "'" +                
            //           " and  (Territory_Cat=3 or Territory_Cat=4) and a.Territory_Active_Flag=0 ) as a PIVOT ( max(a.Territory_Name)  FOR [Territory_Cat] IN (OS,OSEX)) AS pivot1";

            strQry = ";with terr as (select * from Mas_Territory_Creation where Territory_Cat=3 and Territory_Active_Flag=0),terr1 as" +
                   " (select * from Mas_Territory_Creation where Territory_Cat=4 and Territory_Active_Flag=0) select f.Sf_Code,f.Territory_Name as Terr_From,e.Territory_Name as Terr_To,e.OSEX_Distance from terr1 e, " +
                   " terr f where f.Sf_Code='" + Sf_Code + "' and e.Sf_Code='" + Sf_Code + "'  ";


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

        public int get_ExKms(string Sf_Code, string EX_Distance, string Territory_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET EX_Distance = '" + EX_Distance + "' " +
                         " WHERE Sf_Code = '" + Sf_Code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsKms(string Sf_Code, string OS_Distance, string Territory_Name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OS_Distance = '" + OS_Distance + "' " +
                         " WHERE Sf_Code = '" + Sf_Code + "' and Territory_Name='" + Territory_Name + "'  ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int get_OsExKms(string Sf_Code, string OSEX_Distance)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OSEX_Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + Sf_Code + "'   ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Changes done by Priya

        public DataSet getSfname_Desig(string Sf_Code, string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select a.sf_name,a.Sf_HQ,a.sf_emp_id,b.Designation_Name,c.Division_Name " +
                     " from Mas_Salesforce a,Mas_SF_Designation b, Mas_Division c where a.Sf_Code= '" + Sf_Code + "' and c.division_Code = '" + div_Route_Code + "'" +
                     " and a.Designation_Route_Code=b.Designation_Route_Code ";

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
        // Changes done by Saravanan 20/03/15
        public int WrkTypeMGR_Expense_Update(string Worktype_Name, string Expense_Type, string Div_Route_Code, string Type, string Work_Type_Route_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();

                strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "' and TYPE='" + Type + "'";

                dsData = db.Exec_DataSet(strQry);

                if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    strQry = "Insert into Worktype_Allowance_Setup (Work_Type_Name,Type,division_Code,Allow_Type,Work_Type_Route_Code)values('" + Worktype_Name + "','" + Type + "','" + Div_Route_Code + "','" + Expense_Type + "','" + Work_Type_Route_Code + "')";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Update Worktype_Allowance_Setup set Allow_type='" + Expense_Type + "',Work_Type_Route_Code='" + Work_Type_Route_Code + "' where Work_Type_Name='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "' and TYPE='" + Type + "'";

                    iReturn = db.ExecQry(strQry);
                }

                strQry = "Update Mas_WorkType_Mgr set Expense_Type='" + Expense_Type + "' where Worktype_Name_M='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {

            }
            return iReturn;
        }

        public int ExpenseParameter_Insert(string Exp_Parameter_Name, string Exp_Type, string Div_Route_Code, string txtFixedAmount, string Level_Value)
        {
            int iReturn = -1;

            if (!ExpParameter_RecordExist(Exp_Parameter_Name, Div_Route_Code, Level_Value))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    int Parameter_Route_Code = 0;

                    strQry = "select isnull(max(Expense_Parameter_Route_Code),0)+1 Expense_Parameter_Route_Code from Fixed_Variable_Expense_Setup ";
                    Parameter_Route_Code = db.Exec_Scalar(strQry);

                    //strQry = "select  count(Expense_Parameter_Route_Code) as Expense_Parameter_Route_Code from Fixed_Variable_Expense_Setup where division_Code='" + Div_Route_Code + "'";
                    //dsCount = db.Exec_DataSet(strQry);


                    strQry = "Insert into Fixed_Variable_Expense_Setup (Expense_Parameter_Route_Code,Type," +
                                "Expense_Parameter_Name,division_Code,Param_type,Fixed_Amount) values('" + Parameter_Route_Code + "','" + Level_Value + "'," +
                                "'" + Exp_Parameter_Name + "','" + Div_Route_Code + "','" + Exp_Type + "','" + txtFixedAmount + "')";


                    iReturn = db.ExecQry(strQry);

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                iReturn = -2;
            }
            return iReturn;

        }

        public DataSet getExp_Parameter()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Route_Code,Expense_Parameter_Name,case when Expense_Type=1 then 'Fixed' " +
                     "when Expense_Type=2  then 'Variable' " +
                     "end Expense_Type from Mas_Expense_Parameter";

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

        public bool ExpParameter_RecordExist(string Exp_Parameter_Name, string Div_Route_Code, string Level_Value)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Expense_Parameter_Name) Expense_Parameter_Name from Fixed_Variable_Expense_Setup where Expense_Parameter_Name='" + Exp_Parameter_Name + "' and division_Code='" + Div_Route_Code + "' and TYPE='" + Level_Value + "' ";

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

        public int ExpRecordUpdate(string Exp_Parameter_Route_Code, string ExpParameter_Name, string Exp_Type, string Fixed_Amount)
        {
            int iReturn = -1;
            int InsertColumn = 0;
            //if (!RecordExist(ExpParameter_Name))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Fixed_Variable_Expense_Setup set Expense_Parameter_Name='" + ExpParameter_Name + "',Param_type='" + Exp_Type + "',Fixed_Amount='" + Fixed_Amount + "' where Expense_Parameter_Route_Code='" + Exp_Parameter_Route_Code + "'";

                iReturn = db.ExecQry(strQry);


                //DataSet dsTerr = null;
                //DataTable dt = null;
                //strQry = "SELECT REPLACE(LTRIM(RTRIM(Expense_Parameter_Name)), SPACE(1), '_' ) Expense_Parameter_Name " +
                //          " FROM Mas_Expense_Parameter where Expense_Type=1 and Expense_Parameter_Route_Code='" + Exp_Parameter_Route_Code + "'";
                //dsTerr = db.Exec_DataSet(strQry);                   

                //dt = dsTerr.Tables[0];

                //for (int i = 0; i < dsTerr.Tables[0].Rows.Count; i++)
                //{
                //    string strColumn = dt.Rows[i].Field<string>(0);

                //    //strQry = "ALTER TABLE Mas_Allowance_Fixation DROP COLUMN" + strColumn + "";
                //    //InsertColumn = db.ExecQry(strQry);

                //    strQry = "Alter table Mas_Allowance_Fixation add " + strColumn + " float";
                //    InsertColumn = db.ExecQry(strQry);

                //}


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

        public int Exp_Parameter_RecordDelete(int Expense_Parameter_Route_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  Mas_Expense_Parameter WHERE Expense_Parameter_Route_Code = '" + Expense_Parameter_Route_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public DataSet getExp_ParameterBL(int BL_workRoute_Code, string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Route_Code,Expense_Parameter_Name,case when Param_type='F' then 'Fixed' " +
                     "when Param_type='V'  then 'Variable' when Param_type='L'  then 'Variable with Limit'  " +
                     "end Param_type,Fixed_Amount,Param_type from Fixed_Variable_Expense_Setup where Type='" + BL_workRoute_Code + "' and division_Code='" + div_Route_Code + "'";

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
        public DataSet getExp_ParameterMGR(int BL_workRoute_Code, string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Expense_Parameter_Route_Code,Expense_Parameter_Name,case when Type=1 then 'Fixed' " +
                      "when Type=2  then 'Variable' " +
                      "end Param_type,Fixed_Amount from Fixed_Variable_Expense_Setup where Type='" + BL_workRoute_Code + "' and division_Code='" + div_Route_Code + "'";

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

        public DataSet getExp_Managers(string div_Route_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select a.Sf_Code,a.Sf_HQ+' - '+Sf_Name+' - '+b.Designation_Short_Name Sf_Name from mas_salesforce a," +
                     " Mas_SF_Designation b" +
                     " where a.Designation_Route_Code=b.Designation_Route_Code and sf_TP_Active_Flag in (0,2) " +
                     " and Sf_Code in  (select TP_Reporting_SF from Mas_Salesforce where Sf_Code != 'admin' and  " +
                     " (division_Code like '" + div_Route_Code + ',' + "%'  or  division_Code like '%" + ',' + div_Route_Code + ',' + "%' ))";



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

        public DataSet getExp_FixedType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;

            strQry = "select count(Expense_Parameter_Name) from Mas_Expense_Parameter where Expense_Type=1";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Expense_Parameter_Name) " +
                         " from Mas_Expense_Parameter where Expense_Type=1 " +
                         " group by Expense_Parameter_Name, Expense_Parameter_Route_Code " +
                         " order by Expense_Parameter_Route_Code " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select Expense_Parameter_Route_Code, Expense_Parameter_Name from Mas_Expense_Parameter ) x " +
                         "pivot " +
                         "(max(Expense_Parameter_Route_Code) " +
                         "for Expense_Parameter_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";
            }

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
        public DataSet getExp_FixedType1(string division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            int iCount = 0;
            // strQry = "select division_Code from Mas_Salesforce_AM where Sf_Code = '" + Sf_Code + "' ";
            //int division_Code = db_ER.Exec_Scalar(strQry);
            strQry = "select  count(Expense_Parameter_Name) from Fixed_Variable_Expense_Setup where division_Code=" + division_Code + " and param_type='F'";
            iCount = db_ER.Exec_Scalar(strQry);

            if (iCount != 0)
            {
                strQry = "DECLARE @cols AS NVARCHAR(MAX),    @query  AS NVARCHAR(MAX) select @cols = STUFF((SELECT ',' + QUOTENAME(Expense_Parameter_Name) " +
                         " from Fixed_Variable_Expense_Setup where division_Code=" + division_Code + " and param_type='F' " +
                         " group by Expense_Parameter_Name, Expense_Parameter_Route_Code " +
                         " order by Expense_Parameter_Route_Code " +
                         " FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') " +
                         "set @query = N'SELECT ' + @cols + N' from (select Expense_Parameter_Route_Code, Expense_Parameter_Name from Fixed_Variable_Expense_Setup ) x " +
                         "pivot " +
                         "(max(Expense_Parameter_Route_Code) " +
                         "for Expense_Parameter_Name in (' + @cols + N') ) p ' " +
                         "exec sp_executesql @query;";

                //strQry = "select Expense_Parameter_Name from Mas_Expense_Parameter where Expense_Type=1";

            }
            else
            {
                strQry = "select Expense_Parameter_Name from Fixed_Variable_Expense_Setup where division_Code=" + division_Code + " and param_type='F'";
            }

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
        public int WrkTypeBase_Expense_Update(string Worktype_Name, string Expense_Type, string Div_Route_Code, string Type, string Work_Type_Route_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();

                strQry = "select COUNT(*) as [Value] from Worktype_Allowance_Setup where work_type_Name='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "' and TYPE='" + Type + "'";

                dsData = db.Exec_DataSet(strQry);

                if (dsData.Tables[0].Rows[0]["Value"].ToString() == "0")
                {
                    strQry = "Insert into Worktype_Allowance_Setup (Work_Type_Name,Type,division_Code,Allow_Type,Work_Type_Route_Code)values('" + Worktype_Name + "','" + Type + "','" + Div_Route_Code + "','" + Expense_Type + "','" + Work_Type_Route_Code + "')";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Update Worktype_Allowance_Setup set Allow_type='" + Expense_Type + "',Work_Type_Route_Code='" + Work_Type_Route_Code + "' where Work_Type_Name='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "' and TYPE='" + Type + "'";

                    iReturn = db.ExecQry(strQry);
                }

                strQry = "Update Mas_WorkType_BaseLevel set Expense_Type='" + Expense_Type + "' where Worktype_Name_B='" + Worktype_Name + "' and division_Code='" + Div_Route_Code + "'";

                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {

            }
            return iReturn;

        }
        //Giri Expanse_Insert 04.08.2016
        public int WrkType_Expense_Update(string lblDCR_Date, string lblWorktype_Name, string Place, string Place_no, string div_code, string txtAlw, string txtDis, string txtFare, string lblTotal, string sfname, string sfcode)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select DCR_Date from Trans_FM_Expense_Detail where DCR_Date='" + lblDCR_Date + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_FM_Expense_Detail set Expense_wtype_Name='" + lblWorktype_Name + "',Place_of_Work='" + Place + "',Expense_Place_No='" + Place_no + "',Expense_Allowance='" + txtAlw + "',Expense_Distance='" + txtDis + "',Expense_Fare='" + txtFare + "',Expense_Total='" + lblTotal + "',Sf_Name='" + sfname + "',Sf_Code='" + sfcode + "',LastUpdt_Date=getdate() where DCR_Date='" + lblDCR_Date + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Insert into Trans_FM_Expense_Detail(DCR_Date,Expense_wtype_Name,Place_of_Work,Expense_Place_No,Division_code,Expense_Allowance,Expense_Distance,Expense_Fare,Expense_Total,Created_Date,LastUpdt_Date,Sf_Name,Sf_Code)values('" + lblDCR_Date + "','" + lblWorktype_Name + "','" + Place + "','" + Place_no + "','" + div_code + "','" + txtAlw + "','" + txtDis + "','" + txtFare + "','" + lblTotal + "',getdate(),getdate(),'" + sfname + "','" + sfcode + "')";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }
        

        public int Expense_Amount_Update(string sf_name, string sfCode, string Month, string Year, string Tot_allow, string Tot_dis, string tot_Fare, string Tot_exp, string Tot_Additial, string GrandTotal, string div_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select Sf_Code from Trans_Expense_Amount_Detail where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_Expense_Amount_Detail set Month='" + Month + "',Year='" + Year + "',Total_Allowance='" + Tot_allow + "',Total_Distance='" + Tot_dis + "',Total_Fare='" + tot_Fare + "',Total_Expense='" + Tot_exp + "',Total_Additional_Amt='" + Tot_Additial + "',Grand_Total='" + GrandTotal + "' where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = "Insert into Trans_Expense_Amount_Detail(Sf_Name,Sf_Code,Month,Year,Total_Allowance,Total_Distance,Total_Fare,Total_Expense,Total_Additional_Amt,Grand_Total,Division_code)values('" + sf_name + "','" + sfCode + "','" + Month + "','" + Year + "','" + Tot_allow + "','" + Tot_dis + "','" + tot_Fare + "','" + Tot_exp + "','" + Tot_Additial + "','" + GrandTotal + "','" + div_code + "')";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }

        public int get_OsExKms(string Sf_Code, string OSEX_Distance, string Terr_To)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Territory_Creation " +
                         " SET OSEX_Distance = '" + OSEX_Distance + "' " +
                         " WHERE Sf_Code = '" + Sf_Code + "' and Territory_Name='" + Terr_To + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //Changes done by Priya
        public DataSet getTerritory_Transfer(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT '-1' Territory_Code, '---Select---' Territory_Name " +
                     " UNION " +
                     " SELECT Territory_Code, Territory_Name " +
                     " FROM  Mas_Territory_Creation " +
                     " where Sf_Code = '" + Sf_Code + "' and Territory_Active_Flag = 0";

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

        //Changes done by Priya

        public int Getterr_Code()
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Territory_Code)+1,'1') Territory_Code from Mas_Territory_Creation";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getTerritory_Total(string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code, a.Territory_Name, a.Territory_SName, " +
                     " case when a.Territory_Cat=1 then 'HQ' " +
                     " else case when a.Territory_Cat=2 then 'EX' " +
                     " else case when a.Territory_Cat=3 then 'OS' " +
                     " else 'OS-EX' " +
                     " end end end as Territory_Cat, " +
                //   " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where a.Territory_Code=b.Territory_Code " +
                     " (select COUNT(b.ListedDrCode) from Mas_ListedDr b where b.Territory_Code = cast(a.Territory_Code as varchar ) " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Chemists_Route_Code) from Mas_Chemists b where a.Territory_Code=b.Territory_Code " +
                     " and b.Chemists_Active_Flag=0) Chemists_Count, " +
                     " (select COUNT(b.Hospital_Route_Code) from Mas_Hospital b where a.Territory_Code=b.Territory_Code " +
                     " and b.Hospital_Active_Flag =0) Hospital_Count, " +
                     " (select COUNT(b.UnListedDrCode) from Mas_UnListedDr b where a.Territory_Code=b.Territory_Code " +
                     " and b.UnListedDr_Active_Flag=0) UnListedDR_Count " +
                     " FROM  Mas_Territory_Creation a where a.Sf_Code in(" + Sf_Code + ")  and a.Territory_Active_Flag=0" +
                     " order by Territory_SNo";
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
        public DataSet getTerritory_dm(string sfname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //strQry = "select Stockist_Name from Mas_Stockist  where  sf_code LIKE '%,"+sfname+",%' or sf_code LIKE '%,"+sfname+"' or sf_code LIKE '"+sfname+",%' or sf_code='"+sfname+"'";
            strQry = "select Distributor_Name,Distributor_Code from Mas_DSM where Distributor_Code='" + sfname + "'";
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
        public DataSet getTerritory_dc(string sfRoute_Code1)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name from Mas_Salesforce where Sf_Code='" + sfRoute_Code1 + "' ";
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
		public DataSet getSF_dis(string sf_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT '' as Sf_Code, '---Select---' as Sf_Name " +
            //         " UNION " +
            //         " select Sf_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name from Mas_Salesforce where (division_Code like '" + div_Route_Code + ',' + "%'  or " +
            //          " division_Code like '%" + ',' + div_Route_Code + ',' + "%') " +
            //         " and sf_type=1 and sf_status = 0 ";
            strQry = "select '' as DSM_code, '---Select---' as DSM_name UNION select  DSM_code,DSM_name from Mas_DSM where Distributor_Name='"+sf_name+"' and  DSM_Active_Flag=0";
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
 		public DataSet getSF_Code_dis_DIS(string sf_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Distributor_Code, '---Select---' as stockist_Name " +
                     " UNION " +
                " select Distributor_Code,stockist_Name from Mas_Stockist  where stockist_Name='" + sf_name + "'";
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
 		public DataSet getTerrritoryViewDIS(string sf_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = "select DSM_name as sfName from Mas_DSM where Distributor_Name='" + sf_Name + "'";

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
 		public DataSet getTerritorydiv(string Sf_Code, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code,a.Route_Code,a.Territory_Name, a.Dist_Name,a.Target,a.Min_Prod," +


                     " (select COUNT(b.ListedDrCode) " +
                     " from Mas_ListedDr b where b.Territory_Code like '%,'+cast(a.Territory_Code as varchar) or b.Territory_Code like cast(a.Territory_Code as varchar)+',%' " +
                     " and b.ListedDr_Active_Flag=0) ListedDR_Count, " +
                     " (select COUNT(b.Dist_Name) from Mas_Territory_Creation b where b.Territory_Code=a.Territory_Code and " +
                     "  b.Territory_Active_Flag=0) Chemists_Count " +
                     " FROM  Mas_Territory_Creation a where a.Dist_Name = '" + Sf_Code + "'  and a.Territory_Active_Flag=0 and a.Division_Code ='" + Div_code + "' " +
                     " order by Territory_SNo";
            //strQry = " SELECT Territory_Code,Route_Code,Territory_Name,Dist_Name,Target,Min_Prod " +
            //         " FROM  Mas_Territory_Creation a where a.Sf_Code = '" + Sf_Code + "'  and a.Territory_Active_Flag=0 " +
            //         " order by Territory_Code";
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
		public int RecordCountdiv(string Sf_Code, string Div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Territory_Code) FROM Mas_Territory_Creation WHERE Sf_Code = '" + Sf_Code + "' and Territory_Active_Flag = 0 and Division_Code='" + Div_code + "'";

                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

    }
}
