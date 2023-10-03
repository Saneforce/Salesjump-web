using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
// Add this namespace
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class DCR_New
    {
        //Added by sridevi - For Begin,Commit,Rollback
        private string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        //End

        private string strQry = string.Empty;

        public DataSet getWorkType(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select '0' WorkType_Code_M, '---Select---' Worktype_Name_M UNION select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr where active_flag=0 and TP_DCR like '%D%'" +
                     " and division_code = '" + div_code + "'  ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet getWorkType_MRDCR(string div_code,string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%D%'" +
                          " and division_code = '" + div_code + "'  ORDER BY 2";
            }
            else if (sf_type == "2")
            {
                strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_M as WorkType_Code_B,Worktype_Name_M as Worktype_Name_B from Mas_WorkType_Mgr where active_flag=0 and TP_DCR like '%D%'" +
                    " and division_code = '" + div_code + "'  ORDER BY 2";
            }
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public string get_SFName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTerr = null;
            string sReturn = string.Empty;
            strQry = "select Sf_Name from Mas_Salesforce where sf_code = '" + sf_code + "' ";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;

        }

        public DataSet getTerrHQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC sp_get_Rep '" + sf_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getTerrHQ_DCR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_get_Rep_MGRDCR] '" + sf_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet LoadWorkwith(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " Select Sf_Code,'SELF' Sf_Name, 'ZZZZ' Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' " +
                         " UNION" +
                         " Select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // AM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                         " UNION" +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF from Mas_Salesforce " + // RM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                         " UNION " +
                         " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF from Mas_Salesforce " + // ZM Level
                         " where Sf_Code !='admin' and Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                         " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) ) ) order by Reporting_To_SF Desc ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getProducts(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.Prod_Detail_Sl_No as Product_Detail_Code,b.Product_Detail_Name " +
            //         " from Mas_Salesforce a, Mas_Product_Detail b " +
            //         " where a.Sf_Code = '" + sf_code + "' " +
            //         " and cast(b.Division_Code as varchar) = '" + divcode + "' " +
            //         " and b.Product_Active_Flag=0 and " +
            //         "(b.state_code like cast(a.state_code as varchar) +','+'%' or b.state_code like '%'+','+ cast(a.state_code as varchar) +','+'%')" +
            //         " ORDER BY 2";


            strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.product_code_slno as Product_Detail_Code,b.Product_Detail_Name " +
                     " from Mas_Salesforce a, Mas_Product_Detail b " +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and cast(b.Division_Code as varchar) = '" + divcode + "' " +
                     " and b.Product_Active_Flag=0 and " +
                     "(b.state_code like cast(a.state_code as varchar) +','+'%' or b.state_code like '%'+','+ cast(a.state_code as varchar) +','+'%')" +
                    //  " and b.subdivision_code like cast(a.subdivision_code as varchar) " +
                     " ORDER BY 2";
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

        public DataSet getGift(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Gift_Code, '-Select-' Gift_Name UNION select a.Gift_Code,a.Gift_Name " +
                     " from mas_gift a, mas_salesforce b" +
                     " Where cast(a.Division_Code as varchar) = '" + divcode + "' " +
                     " and a.Gift_Active_Flag=0 " +
                     " and b.Sf_Code = '" + sf_code + "' " +
                     " ORDER BY 2";
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

        public DataSet getProducts_MGR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.Product_Detail_Code,b.Product_Detail_Name " +
                     " from Mas_Salesforce a, Mas_Product_Detail b " +
                     " where a.Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
                     " and b.Product_Active_Flag=0 and " +
                       "(b.state_code like cast(a.state_code as varchar) +','+'%' or b.state_code like '%'+','+ cast(a.state_code as varchar) +','+'%')" +
                     " ORDER BY 2";
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
        public DataSet getGift_MGR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select '0' Gift_Code, '-Select-' Gift_Name UNION select a.Gift_Code,a.Gift_Name " +
                     " from mas_gift a, mas_salesforce b" +
                     " where a.Gift_Active_Flag=0 " +
                     " and b.Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
                     " ORDER BY 2";
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

        public int RecordAdd_Header(string SF_Code, string sf_name, string emp_id, string employee_id, string Activity_Date, string Work_Type, string SDP, string SDP_Name, string sRemarks, string vConf1, string dcrdate, bool reentry, bool isdelayrel, string vConf, string Start_Date, string WorkType_name, string sf_type, string IPadd, string EntryMode)
        {
            int iReturn = -1;
            int iReturnmax = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_SlNo = -1;
                string FWI = string.Empty;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE Division_Code = '" + Division_Code + "' and  Sf_Code = '" + SF_Code + "' and Activity_Date ='" + Activity_Date + "' ";
                string slno = string.Empty;
                DataSet dsdup = db.Exec_DataSet(strQry);
                if (dsdup != null)
                {
                    if (dsdup.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drrow in dsdup.Tables[0].Rows)
                        {
                            slno = drrow["Trans_SlNo"].ToString();

                            strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + slno + "' " +
                                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                            iReturn = db.ExecQry(strQry);

                            strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + slno + "' " +
                                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                            iReturn = db.ExecQry(strQry);

                            strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + slno + "' " +
                                    " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                            iReturn = db.ExecQry(strQry);

                            strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + slno + "' " +
                                    " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                            iReturn = db.ExecQry(strQry);
                        }
                    }
                }
                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Main as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_SlNo as bigint)),0)+1 FROM DCRMain_Temp ";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }
                // Added by Sridevi to include FieldWork_Indicator
                if (sf_type == "1")
                {
                    strQry = "SELECT  FieldWork_Indicator  from Mas_WorkType_BaseLevel  where WorkType_Code_B ='" + Work_Type + "' and division_code = '" + Division_Code + "' ";
                }
                else
                {
                    strQry = "SELECT  FieldWork_Indicator  from Mas_WorkType_Mgr  where WorkType_Code_M  ='" + Work_Type + "' and division_code = '" + Division_Code + "' ";
                }
                DataSet dsfw = db.Exec_DataSet(strQry);
                if (dsfw.Tables[0].Rows.Count > 0)
                {
                    FWI = dsfw.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    FWI = "";
                }
                strQry = "insert into DCRMain_Temp (Trans_SlNo, Sf_Code,sf_name,emp_id,employee_id, Activity_Date, Submission_Date, Work_Type,Plan_No,Plan_Name, Division_Code, Remarks, confirmed,Start_Time,End_Time,WorkType_Name,FieldWork_Indicator,Sys_Ip,Entry_Mode) " +
                       " VALUES('" + Trans_SlNo + "', '" + SF_Code + "','" + sf_name + "', '" + emp_id + "','" + employee_id + "','" + Activity_Date + "', getdate(), '" + Work_Type + "', '" + SDP + "', '" + SDP_Name + "', '" + Division_Code + "', '" + sRemarks + "','" + vConf1 + "','" + Start_Date + "', getdate(), '" + WorkType_name + "','" + FWI + "','" + IPadd + "','" + EntryMode + "')";

                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    iReturn = Trans_SlNo; //Inorder to maintain the same sl_no on detail table

                    if (vConf == "1")
                    {
                        if (Trans_SlNo > 1)
                        {
                            strQry = "update DCR_MaxSlNo set Max_Sl_No_Main = '" + Trans_SlNo + "' ";
                            iReturnmax = db.ExecQry(strQry);
                        }
                        else
                        {
                            if (reentry == false)
                            {
                                strQry = "insert into DCR_MaxSlNo  VALUES('" + Division_Code + "','" + Trans_SlNo + "', '0')";
                                iReturnmax = db.ExecQry(strQry);
                            }
                        }
                        //if (isdelayrel == true)
                        //{
                        //    int iReturndel = -1;
                        //    strQry = " Update DCR_Delay_Dtls  set Delayed_Flag =  2 where Sf_Code= '" + SF_Code + "' and Delayed_Date = '" + Activity_Date + "'";

                        //    iReturndel = db.ExecQry(strQry);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordAdd_Detail(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Code, string Gift_Name, string GQty, string SDP, string vConf, string sess_code, string minutes, string seconds, string product_detail_code, string gift_detail_code, string Add_Prod_Detail, string Add_Prod_Code, string Add_Gift_Detail, string Add_Gift_Code, string Trans_Detail_Name, string remarks)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;
                
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Lst_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                 
                // Added by Sridevi - 27 Dec 15 Starts
                string terr_code = string.Empty;
                string terr_name = string.Empty;
                string dcrdate = string.Empty;
                DataSet dsterr = null;
                
                if (SF_Code.IndexOf("MGR") == -1)
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name  from  Mas_Listeddr a, Mas_Territory_Creation b" +
                             " where a.Sf_Code = b.SF_Code and a.ListedDrCode =  '" + Trans_Detail_Info_Code + "' " +
                             " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                             " and a.sf_code= '" + SF_Code + "'";
                }
                else
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name + ' ( ' + c.sf_name + ' ) ' from  Mas_Listeddr a, Mas_Territory_Creation b, Mas_Salesforce c " +
                             " where a.Sf_Code = b.SF_Code and a.ListedDrCode =  '" + Trans_Detail_Info_Code + "' " +
                             " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                             " and a.sf_code = c.sf_code ";
                }

                dsterr = db.Exec_DataSet(strQry);

                if (dsterr.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsterr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    terr_name = dsterr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
                
                // Ends Here
                strQry = "insert into DCRDetail_Lst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                        " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name,SDP_Name,activity_remarks) " +
                        " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                        " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                        " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + terr_code + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "','" + terr_name + "' ,'" + remarks + "')";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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
        public int RecordAdd_Detail_Chem(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                // Added by Sridevi - 27 Dec 15 Starts
                string terr_code = string.Empty;
                string terr_name = string.Empty;
                string dcrdate = string.Empty;
                DataSet dsterr = null;
               
                if (SF_Code.IndexOf("MGR") == -1)
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name  from  Mas_Chemists a, Mas_Territory_Creation b" +
                         " where a.Sf_Code = b.SF_Code and a.Chemists_Code =  '" + Trans_Detail_Info_Code + "' " +
                         " and a.Territory_Code = b.Territory_Code  " +
                         " and a.sf_code= '" + SF_Code + "'";
                }
                else
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name + ' ( ' + c.sf_name + ' ) '  from  Mas_Chemists a, Mas_Territory_Creation b,Mas_salesforce c" +
                             " where a.Sf_Code = b.SF_Code and a.Chemists_Code =  '" + Trans_Detail_Info_Code + "' " +
                             " and a.Territory_Code = b.Territory_Code  " +
                             " and a.sf_code = c.sf_code ";
                }
              

                dsterr = db.Exec_DataSet(strQry);

                if (dsterr.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsterr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    terr_name = dsterr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

                // Ends Here
                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB ," +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name,SDP_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "',  '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + terr_code + "', '" + Division_Code + "', '" + Trans_Detail_Name + "' ,'" + terr_name + "')";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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

        public int RecordAdd_Detail_Stockiest(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB, string Worked_With_Code, string Worked_With_Name, string SDP, string Visit_Type, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }


                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Visit_Type, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + SDP + "',  '" + Visit_Type + "', '" + Division_Code + "', '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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


        public int RecordAdd_Detail_Unlst(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Code, string Gift_Name, string GQty, string SDP, string vConf, string sess_code, string minutes, string seconds, string product_detail_code, string gift_detail_code, string Add_Prod_Detail, string Add_Prod_Code, string Add_Gift_Detail, string Add_Gift_Code, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Unlst_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                // Added by Sridevi - 27 Dec 15 Starts
                string terr_code = string.Empty;
                string terr_name = string.Empty;
                
               
                DataSet dsterr = null;
               
                if (SF_Code.IndexOf("MGR") == -1)
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name  from  Mas_UnListedDr a, Mas_Territory_Creation b" +
                         " where a.Sf_Code = b.SF_Code and a.Unlisteddrcode =  '" + Trans_Detail_Info_Code + "' " +
                         " and a.Territory_Code = b.Territory_Code  " +
                         " and a.sf_code= '" + SF_Code + "'";
                }
                else
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name + ' ( ' + c.sf_name + ' ) '   from  Mas_UnListedDr a, Mas_Territory_Creation b,Mas_salesforce c" +
                             " where a.Sf_Code = b.SF_Code and a.Unlisteddrcode =  '" + Trans_Detail_Info_Code + "' " +
                             " and a.Territory_Code = b.Territory_Code  " +
                             " and a.sf_code = c.sf_code ";
                }
                dsterr = db.Exec_DataSet(strQry);

                if (dsterr.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsterr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    terr_name = dsterr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

                strQry = "insert into DCRDetail_Unlst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                         " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name,SDP_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                         " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + terr_code + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "' ,'" + terr_name  + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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

        public int RecordAdd_Detail_Hosp(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP, string vConf, string Trans_Detail_Name)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                // Added by Sridevi - 27 Dec 15 Starts
                string terr_code = string.Empty;
                string terr_name = string.Empty;
                string dcrdate = string.Empty;

                DataSet dsterr = null;
                DataSet dsMgr = null;

                strQry = "SELECT Activity_Date FROM DCRMain_temp where trans_slno =  '" + Trans_SlNo + "'";
                dsMgr = db.Exec_DataSet(strQry);
                if (dsterr.Tables[0].Rows.Count > 0)
                    dcrdate = dsterr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                if (SF_Code.IndexOf("MGR") != -1)
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name  from  Mas_Hospital a, Mas_Territory_Creation b" +
                         " where a.Sf_Code = b.SF_Code and a.Hospital_Code =  '" + Trans_Detail_Info_Code + "' " +
                         " and a.Territory_Code = b.Territory_Code  " +
                         " and a.sf_code= '" + SF_Code + "'";
                }
                else
                {
                    strQry = " select a.Territory_Code ,b.Territory_Name + ' ( ' + c.sf_name + ' ) ' from  Mas_Hospital a, Mas_Territory_Creation b,Mas_salesforce c" +
                            " where a.Sf_Code = b.SF_Code and a.Hospital_Code =  '" + Trans_Detail_Info_Code + "' " +
                            " and a.Territory_Code = b.Territory_Code  " +
                            " and a.sf_code = c.sf_code and a.Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + SF_Code + "' and  DCR_Date = '" + dcrdate + "')";
                }
               

                dsterr = db.Exec_DataSet(strQry);

                if (dsterr.Tables[0].Rows.Count > 0)
                {
                    terr_code = dsterr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    terr_name = dsterr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }

                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name,SDP_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + terr_code + "', '" + Division_Code + "' ,'" + Trans_Detail_Name + "','" +terr_name + "')";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
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
        public DataSet getChemists(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sf_code + "' " +
                     " ORDER BY 2";
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

        public DataSet getStockiest(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,Stockist_Name " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
                     " and division_code = '" + div_code + "' " +
                     " and Sf_Code like '%" + sf_code + "%' " +
                     " ORDER BY 2";
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

        public DataSet getHospital(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Hospital_Code, '---Select---' Hospital_Name UNION select Hospital_Code,Hospital_Name " +
                     " from Mas_Hospital " +
                     " where Hospital_Active_Flag=0 " +
                     " and Sf_Code like '%" + sf_code + "%' " +
                     " ORDER BY 2";
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

        public DataSet getChemists(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name,Territory_Code " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
                     " ORDER BY 2";
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

        //public DataSet getStockiest(string sf_code, string DCRDate)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsProCat = null;

        //    strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,Stockist_Name " +
        //             " from Mas_Stockist " +
        //             " where Stockist_Active_Flag=0 " +
        //             " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
        //             " ORDER BY 2";
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

        public DataSet getHospital(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Hospital_Code, '---Select---' Hospital_Name UNION select Hospital_Code,Hospital_Name " +
                     " from Mas_Hospital " +
                     " where Hospital_Active_Flag=0 " +
                     " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "')" +
                     " ORDER BY 2";
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

        public DataSet getMR(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " Select '0' Sf_Code, '-Select-' Sf_Name " +
                     " UNION " +
                     " Select a.Sf_Code,a.Sf_Name   from Mas_Salesforce a,DCR_MGR_WorkAreaDtls b" +
                     " where a.SF_Status != 2 and  a.Sf_Code = b.sf_code " +
                     " and b.MGR_Code = '" + sf_code + "' and  b.DCR_Date = '" + DCRDate + "' " +
                     " ORDER BY 2";
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
        public int isTerritoryDoctor(string sf_code, string dr_code, string terr_code)// Modified by Sri - 6 Aug
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                            " where ListedDrCode = '" + dr_code + "' and Sf_Code='" + sf_code + "' and Territory_Code= '" + terr_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataTable getListedDoctor(string sfcode, string SName, string doc_disp)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = "SELECT ListedDrCode, ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                           " FROM Mas_ListedDr " +
                           " WHERE Sf_Code =  '" + sfcode + "' " +
                           " AND ListedDr_Active_Flag = 0 " +
                           " AND ListedDr_Name like + '" + SName + "%' Order By 2";


                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = "SELECT a.ListedDrCode, " +
                        "a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                             " WHERE a.Sf_Code =  '" + sfcode + "' " +
                          " AND a.ListedDr_Active_Flag = 0 " +
                          " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = "SELECT a.ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
                else if (doc_disp == "5")//Class
                {
                    strQry = "SELECT a.ListedDrCode, " +
                               " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "%' Order By 2";

                }
            }
            else
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = "SELECT ListedDrCode, ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                           " FROM Mas_ListedDr " +
                           " WHERE Sf_Code =  '" + sfcode + "' " +
                           " AND ListedDr_Active_Flag = 0 Order By 2";


                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = "SELECT a.ListedDrCode, " +
                        "a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                             " WHERE a.Sf_Code =  '" + sfcode + "' " +
                          " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = "SELECT a.ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
                else if (doc_disp == "5")//Class
                {
                    strQry = "SELECT a.ListedDrCode, " +
                               " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 Order By 2";

                }
            }

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

        //Added by sri - Manager DCR Doc

        //public DataTable getListedDoctorMGR(string sfcode, string SName, string doc_disp, string DCRDate)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataTable dsListedDR = null;

        //    SName = SName.Replace('*', ' ');


        //    if (SName.Trim().Length > 0)
        //    {
        //        if (doc_disp == "1")// DR Name
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                      " UNION SELECT a.ListedDrCode, a.ListedDr_Name , a.Territory_Code  from Mas_ListedDr a," +
        //                       " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //                      " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //                      " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //                      " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' Order By 2";
        //        }
        //        else if (doc_disp == "2")//Slno
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                        " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //                        " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //                        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //                        " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' Order By 2";
        //        }
        //        else if (doc_disp == "3")//Speciality
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                       " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                       " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //                       " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //                       " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //                       " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' Order By 2";

        //        }
        //        else if (doc_disp == "4")//Category
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                      " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                      "  DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //                     " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //                      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //                      " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' Order By 2";
        //        }
        //        else if (doc_disp == "5")//Class
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                      " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                      " Mas_Doc_Class b, DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //                      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //                      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //                      " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' Order By 2";
        //        }
        //    }
        //    else
        //    {
        //        if (doc_disp == "1")// DR Name
        //        {

        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                      " UNION SELECT a.ListedDrCode, a.ListedDr_Name , a.Territory_Code  from Mas_ListedDr a," +
        //                      " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //                      " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //                      " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //                      " AND ListedDr_Active_Flag = 0  Order By Territory_Code";

        //        }
        //        else if (doc_disp == "2")//Slno
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                 " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                 " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //                 " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //                 " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //                 " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";


        //        }
        //        else if (doc_disp == "3")//Speciality
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //              " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //              " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //              " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //              " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //              " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";
        //        }
        //        else if (doc_disp == "4")//Category
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                      " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                      "  DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //                      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //                      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //                      " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";


        //        }
        //        else if (doc_disp == "5")//Class
        //        {
        //            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
        //                        " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                        " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //                        " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //                        " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //                        " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";

        //        }
        //    }

        //    try
        //    {
        //        dsListedDR = db_ER.Exec_DataTable(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsListedDR;
        //}

        public DataTable getListedDoctorMGR(string sfcode, string SName, string doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {
                    strQry = " select ListedDrCode,ListedDr_Name,Territory_Code" +
                           " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                            " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                                " union all " +
                               " select  ListedDrCode, ListedDr_Name,Territory_Code" +
                               " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                               " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //           " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //          " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                    //          " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION ALL " +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";
                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                               " union all " +
                              " select ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";
                    //strQry =  " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //            " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION  ALL" +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //            " order by Territory_Code";
                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' ";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //           " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //           " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //           " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                    //           " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //           " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //              " UNION ALL " +
                    //              " SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //             " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //             " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //             " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //          "  DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //         " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                    //          " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION ALL " +
                    //             " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //             " order by Territory_Code";
                }
                else if (doc_disp == "5")//Class
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                          " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%' " +
                              " union all " +
                             " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'";

                    //strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                    //          " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                    //          " Mas_Doc_Class b, DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                    //          " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +    
                    //          " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                    //          " AND ListedDr_Active_Flag = 0  AND ListedDr_Name like + '" + SName + "%' " +
                    //             " UNION  ALL" +
                    //             " SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                    //            " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //            " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //            " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "%'" +
                    //            " order by Territory_Code";
                }
            }
            else
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                              " UNION SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                              " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                              " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                              " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                              " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                 " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                         " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                         " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                         " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                         " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                         " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL " +
                                  "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";
                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                      " UNION SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                      " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL " +
                                  "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                " order by Territory_Code";
                }
                else if (doc_disp == "4")//Category
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                              " UNION SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                              " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                              " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                              " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                              " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                  "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";


                }
                else if (doc_disp == "5")//Class
                {
                    strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name ,'' Territory_Code " +
                                " UNION SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                                " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                                " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " UNION ALL" +
                                 " SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                                " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                                " AND ListedDr_Active_Flag = 0 " +
                                 " order by Territory_Code";
                }
            }

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
        public DataTable getChe_src(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code = '" + sfcode + "' " +
                   " and Chemists_Name like + '" + SName + "%' Order By 2";
            }

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
        public DataTable getChe_srcMGR(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                   " and Chemists_Name like + '" + SName + "%' Order By 2";
            }

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

        public DataTable getUnDoctor(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "%' Order By 2";
            }

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

        public DataTable getUnDoctor_MGR(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "%' Order By 2";
            }

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
        public DataSet getTerrListedDoctor(string sfcode, int doc_disp, string terr_code)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " select * from (SELECT ListedDrCode,  ListedDr_Name , '#ACFA58' col ,1 sortby" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                                " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                        " UNION ALL" +
                        " SELECT ListedDrCode, ListedDr_Name ,'#FFFFFF' col,2 sortby" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                                " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ) doc" +
                            " order by sortby,ListedDr_Name";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = "select * from (SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, '#ACFA58' col ,1 sortby" +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name ,'#FFFFFF' col,2 sortby" +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ) doc" +
                         " order by sortby,ListedDr_Name";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " select * from (SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, '#ACFA58' col ,1 sortby    " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            "  SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  ,'#FFFFFF' col,2 sortby   " +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ) doc " +
                        " order by sortby,ListedDr_Name";

                //strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                //            " a.ListedDr_Name + ' - ' + b.Doc_Special_SName  AS ListedDr_Name  " +
                //            " FROM Mas_ListedDr a, Mas_Doctor_Speciality b " +
                //            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                //            " AND a.Doc_Special_Code = b.Doc_Special_Code " +
                //            " AND a.ListedDr_Active_Flag = 0 " +
                //            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                //        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                //            " UNION ALL" +
                //            "  SELECT a.ListedDrCode AS ListedDrCode, " +
                //            " a.ListedDr_Name + ' - ' + b.Doc_Special_SName  AS ListedDr_Name  " +
                //            " FROM Mas_ListedDr a, Mas_Doctor_Speciality b " +
                //            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                //            " AND a.Doc_Special_Code = b.Doc_Special_Code " +
                //            " AND a.ListedDr_Active_Flag = 0 " +
                //            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                //        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 4) // Category
            {
                strQry = "select * from (SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name , '#ACFA58' col ,1 sortby" +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  ,'#FFFFFF' col, 2 sortby" +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "')) doc " + 
                " order by sortby,ListedDr_Name";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = "select * from (SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name , '#ACFA58' col ,1 sortby" +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  ,'#FFFFFF' col,2 sortby" +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "')) doc "+
                " order by sortby,ListedDr_Name";
            }
            //else if (doc_disp == 6) //Campaign
            //{
            //    strQry = "Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
            //                " FROM Mas_ListedDr " +
            //                " WHERE Sf_Code =  '" + sfcode + "' " +
            //                " AND ListedDr_Active_Flag = 0 " +
            //                " AND Territory_Code like '%" + terr_code + "%' " +
            //                " UNION " +
            //                " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
            //                " FROM Mas_ListedDr " +
            //                " WHERE Sf_Code =  '" + sfcode + "' " +
            //                " AND ListedDr_Active_Flag = 0 " +
            //                " AND Territory_Code NOT like '%" + terr_code + "%' " +
            //                "  Order By 2";
            //}

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

        //public DataSet getTerrListedDoctor_Mgr(string sfcode, int doc_disp, string DCRDate)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsListedDR = null;

        //    if (doc_disp == 1) // DR Name
        //    {

        //        strQry = " SELECT a.ListedDrCode, a.ListedDr_Name , a.Territory_Code  from Mas_ListedDr a," +
        //               " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //               " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //               " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //               " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";
        //    }
        //    else if (doc_disp == 2) // SLV No
        //    {
        //        strQry = "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //                " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
        //                 " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
        //                " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
        //                " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";
        //    }
        //    else if (doc_disp == 3) //Speciality
        //    {

        //        strQry = "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //              " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //              " and a.Territory_Code  = c.Territory_Code " +
        //              " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //              " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //              " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";

        //    }
        //    else if (doc_disp == 4) // Category
        //    {
        //        strQry = " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //              " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //             " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //              " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //              " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";
        //    }
        //    else if (doc_disp == 5) //Class
        //    {
        //        strQry = "SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, a.Territory_Code  from Mas_ListedDr a," +
        //             " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
        //              " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
        //             " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
        //             " AND ListedDr_Active_Flag = 0 Order By Territory_Code ";
        //    }
        //    try
        //    {
        //        dsListedDR = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsListedDR;
        //}

         public DataSet getTerrListedDoctor_Mgr(string sfcode, int doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = "select * from ( select sf_code, ListedDrCode,ListedDr_Name,Territory_Code ,1 sortby   " +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                " AND ListedDr_Active_Flag = 0" +
                                  " union all " +
                                 " select sf_code, ListedDrCode, ListedDr_Name,Territory_Code ,2 sortby   " +
                                 " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                  " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                 " AND ListedDr_Active_Flag = 0) doc " +
                                 " order by sortby,Territory_Code,ListedDr_Name";


                    //strQry = " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //       " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //       " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                    //       " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //       " AND ListedDr_Active_Flag = 0" +
                    //       " UNION ALL " +
                    //       " SELECT a.ListedDrCode, a.ListedDr_Name , b.Territory_Code  from Mas_ListedDr a," +
                    //       " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                    //       " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                    //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                    //       " AND ListedDr_Active_Flag = 0" +
                    //       " order by Territory_Code";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " select * from ( select sf_code, ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code,1 sortby " +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code,2 sortby " +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0) doc" +
                              " order by sortby,Territory_Code,ListedDr_Name";
                //strQry = "SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0 " +
                //            " UNION  ALL" +
                //        " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                //        " order by Territory_Code";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " select * from ( select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code,1 sortby " +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code,2 sortby " +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0) doc" +
                              " order by sortby,Territory_Code,ListedDr_Name";
                //strQry = "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //      " AND ListedDr_Active_Flag = 0 " +
                //      " order by Territory_Code";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " select * from ( select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code,1 sortby " +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code,2 sortby " +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0) doc" +
                              " order by sortby,Territory_Code,ListedDr_Name";

                //strQry = " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //     " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //      " AND ListedDr_Active_Flag = 0 " +
                //         " UNION  ALL" +
                //         " SELECT a.ListedDrCode, a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                        //" order by Territory_Code";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " select * from ( select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code,1 sortby " +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code,2 sortby " +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0) doc" +
                              " order by sortby,Territory_Code,ListedDr_Name";

                //strQry = "SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //        " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //       " AND ListedDr_Active_Flag = 0  " +
                //         " UNION ALL " +
                //         "SELECT a.ListedDrCode,  a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name, b.Territory_Code  from Mas_ListedDr a," +
                //        " DCR_MGR_WorkAreaDtls b WHERE a.Sf_Code = b.Sf_Code " +
                //        " and (a.Territory_Code NOT like cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' AND a.Territory_Code NOT like cast(b.Territory_Code as varchar))" +
                //        " and b.MGR_Code = '" + sfcode + "' and  b.DCR_Date = '" + DCRDate + "' " +
                //        " AND ListedDr_Active_Flag = 0" +
                //        " order by Territory_Code";
            }
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
        public DataSet getTerrListedDoctor(string sfcode, string sName, int iVal)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION SELECT ListedDrCode, ListedDr_Name " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name like '" + sName + "%' " +
                        " AND ListedDr_Active_Flag = 0 Order By 2";
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
       
        public DataSet getdoctercolor(string sfcode, string DCRDate, string doccode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            //strQry = "select b.Color_code from Mas_ListedDr a ,DCR_MGR_WorkAreaDtls b " +
            //            " where a.ListedDrCode = " + doccode + " " +
            //            " and (a.Territory_Code like cast(b.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(b.Territory_Code as varchar) +','+'%' or  a.Territory_Code like cast(b.Territory_Code as varchar))" +
            //            " AND a.Sf_Code = b.sf_Code " +
            //            " AND DCR_Date =  '" + DCRDate + "' ";

            strQry = "select Color_code from DCR_MGR_WorkAreaDtls Where" +
                      " MGR_Code = '" + sfcode + "' " +
                      " AND DCR_Date =  '" + DCRDate + "' " +
                      " AND Territory_code = (select territory_code from mas_listeddr where ListedDrCode = '" + doccode + "')";

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
        public DataSet getTerrListedDoctor(string sfcode, string Terr_Code, string DR_Name)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "SELECT ListedDrCode " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND ListedDr_Name = '" + DR_Name + "' " +
                        " AND  (Territory_Code like '" + Terr_Code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + Terr_Code + ',' + "%' or Territory_Code like '" + Terr_Code + "') " +
                        " AND ListedDr_Active_Flag = 0";
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

        public DataSet getTerrUnListedDoctor(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sf_code + "' " +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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

        public DataSet getTerrUnListedDoctor_Mgr(string sf_code, int doc_disp, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select '0' UnListedDrCode,'-Select-' UnListedDr_Name UNION SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code = '" + sf_code + "'" +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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
        public DataSet getTerrUnListedDoctor_MgrNew(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name ,Territory_Code" +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sf_code + "' and  DCR_Date = '" + DCRDate + "') " +
                        " AND UnListedDr_Active_Flag = 0 Order By 2";
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

        public DataSet getTerrListedDoctor(string sfcode, string Terr_Code) // Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //Modified the qry to accomplish the territory code data type (varchar)
            // by Sridevi on 12/12/14

            ////strQry = "SELECT ListedDrCode, ListedDr_Name " +
            ////            " FROM Mas_ListedDr " +
            ////            " WHERE Sf_Code =  '" + sfcode + "' " +
            ////            " AND Territory_Code = '" + Terr_Code + "' " +
            ////            " AND ListedDr_Active_Flag = 0 Order By 2";

            strQry = " Select '0' ListedDrCode,'-Select-' ListedDr_Name UNION " +
                        "SELECT ListedDrCode, ListedDr_Name" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND  (Territory_Code like '" + Terr_Code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + Terr_Code + ',' + "%' or Territory_Code like '" + Terr_Code + "') " +
                        " AND ListedDr_Active_Flag = 0";


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

        public DataTable getSF(string sname)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsTP = null;
            strQry = "select sf_code,sf_name from mas_salesforce where sf_name like + '" + sname + "%' " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getSDP(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            //strQry = "select '0' Territory_Code, '---Select---' Territory_Name  " +
            //         " UNION " +
            //         " select distinct a.Territory_Code, a.Territory_Name " +
            //         " from Mas_Territory_Creation a, Mas_ListedDr b " +
            //         " where a.Territory_Active_Flag=0 and a.SF_Code='" + SF_Code + "' " +
            //         " and a.SF_Code = b.Sf_Code and b.ListedDr_Active_Flag=0 " +
            //         " and cast(a.Territory_Code as varchar) like b.Territory_Code " +                      
            //         " ORDER BY 2";

            strQry = "select '0' Territory_Code, '---Select---' Territory_Name  " +
                     " UNION " +
                     " select distinct  Territory_Code,  Territory_Name " +
                     " from Mas_Territory_Creation  " +
                     " where  Territory_Active_Flag=0 and  SF_Code='" + SF_Code + "' " +
                     " ORDER BY 2";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getDCRDate(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Last_Dcr_Date  from Mas_Salesforce " +
                     " where Sf_Code='" + SF_Code + "' and sf_TP_Active_Flag=0 ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getsf_dtls(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT  Sf_Name, sf_emp_id, Employee_Id " +
                     " FROM mas_salesforce " +
                     " WHERE (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " AND sf_code  ='" + sf_code + "' ";
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

        public DataSet getDCREntryDate(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select max(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '1'  and " +
                     " activity_date not in (select activity_date from dcrmain_trans where Sf_Code='" + SF_Code + "')";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet getDCREntryDate_Reject(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '2' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getRejectedDCR(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type,Plan_No,Remarks, ReasonforRejection from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '2'  and  Activity_Date = '" + DCRDate + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getDCREntryDate_trans(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select max(activity_date) from dcrmain_trans " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '1' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet getDCREdit(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select  convert(varchar,Activity_Date,103) Activity_Date,Trans_SlNo,worktype_name_b, Sf_Code " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and Sf_Code='" + SF_Code + "' and MONTH(Activity_Date) = '" + sMonth + "' and YEAR(Activity_Date) = '" + sYear + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int RecordInsertMGRWorkArea(string MGR_Code, string Sf_Code, string WorkArea, string Color_Code, string DCRDate, string Work_Type)
        {
            int iReturn = -1;
            int S_No = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count(S_No) FROM DCR_MGR_WorkAreaDtls WHERE MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' ";
                S_No = db.Exec_Scalar(strQry);

                if (S_No > 0)
                {
                    strQry = "delete from DCR_MGR_WorkAreaDtls where MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                }
                strQry = "insert into DCR_MGR_WorkAreaDtls (MGR_Code,Sf_Code, DCR_Date, Territory_Code,Color_Code,Work_Type) " +
                            " VALUES('" + MGR_Code + "', '" + Sf_Code + "', '" + DCRDate + "', '" + WorkArea + "','" + Color_Code + "','" + Work_Type + "' )";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getMgrWorkAreaDtls(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select DISTINCT Work_Type from DCR_MGR_WorkAreaDtls Where " +
                     " MGR_Code ='" + SF_Code + "' and DCR_Date ='" + DCRDate + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int get_Trans_SlNo(string SF_Code, string work_type)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select Trans_SlNo from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and Work_Type= '" + work_type + "' and confirmed=0 ";
                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Update_DCR_Status(string SF_Code, int Trans_SlNo)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "update DCRMain_Temp set confirmed = 1 where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }




        public int RecordAdd_Detail_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Detail, string SDP)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "delete from DCRDetail_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";


                iReturn = db.ExecQry(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Time, " +
                         " Worked_with_Code, Worked_with_Name, Product_Detail, Gift_Detail, SDP, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "', '" + DCR_Time + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + Prod_Detail + "', '" + Gift_Detail + "', '" + SDP + "',  '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int RecordAdd_Detail_Chem_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB_Value, string Worked_With_Code, string Worked_With_Name, string SDP)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "'  and Sf_Code = '" + SF_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, POB_Value, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "', '" + POB_Value + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RecordAdd_Detail_Stockiest_Submit(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string POB, string Worked_With_Code, string Worked_With_Name, string SDP, string Visit_Type)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_Detail_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_Slno as bigint)),0)+1 FROM DCRDetail_Temp WHERE Division_Code = '" + Division_Code + "'  and Sf_Code = '" + SF_Code + "' ";
                Trans_Detail_SlNo = db.Exec_Scalar(strQry);

                strQry = "insert into DCRDetail_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Visit_Code, Division_Code) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', '" + SDP + "',  '" + Visit_Type + "', '" + Division_Code + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "'";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_DCR_Pending_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name, " +
                        " MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                        " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                        " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                        " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                        " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c      " +
                        " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code  " +
                        " and a.Reporting_To_SF  = '" + sf_code + "' and b.confirmed=1";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public DataSet get_DCR_Pending_Approval_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select a.sf_Code,a.trans_slno,convert(varchar,a.Activity_Date,103) Activity_Date,a.Plan_Name,wor.Worktype_Name_B ,(select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                        " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                        " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                        " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                        " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as unlst_cnt," +
                        " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor, Mas_Salesforce b where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' and Year(a.Activity_date) = '" + year + "' " +
                        " and a.work_type = wor.worktype_code_b and a.sf_code = b.sf_code and b.sf_type = 1 and wor.FieldWork_Indicator != 'L' " +
                        " Union" +
                        " select a.sf_Code,a.trans_slno,convert(varchar,a.Activity_Date,103) Activity_Date,a.Plan_Name,wor.Worktype_Name_m ,(select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                        " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                        " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                        " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                        " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as unlst_cnt," +
                        " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor, Mas_Salesforce b where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' and Year(a.Activity_date) = '" + year + "' " +
                        " and a.work_type = wor.worktype_code_m and a.sf_code = b.sf_code and b.sf_type = 2 and wor.FieldWork_Indicator != 'L' ";
           

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet getDCR_Report(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WType_SName " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear;
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getDCR_Report_Det(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select day(Submission_Date) from DCRMain_Trans " +
                     " Where Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear;
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet getDCR_Report_Det_doc(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(a.Activity_Date) = " + sday +
                     " and MONTH(a.Activity_Date) = " + sMonth + " and YEAR(a.Activity_Date) =  " + sYear;
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_WorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WType_SName, Worktype_Name_B  from Mas_WorkType_BaseLevel ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_dcr_ff_details(int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select distinct a.Sf_Code, b.Sf_Name, " +
                     " (select sf_name from mas_salesforce where sf_code = b.Reporting_To_SF) approved_by " +
                     " from DCRMain_Temp a, Mas_Salesforce b " +
                     " where a.Sf_Code = b.Sf_Code and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_dcr_pending_approval(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Activity_Date) pending_date from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int get_Trans_SlNo_toIns(string SF_Code, string Activity_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "select Trans_SlNo from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and convert(varchar,Activity_Date,103) = '" + Activity_Date + "'";
                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Create_DCRHead_Trans(string SF_Code, int Trans_SlNo)
        {
            SqlConnection _conn = new SqlConnection(strConn);

            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int slno = 0;
            //Added by sridevi - For Begin,Commit,Rollback
            SqlTransaction tran = null;
            //End
            try
            {

                DB_EReporting db = new DB_EReporting();       

                //Added by sridevi - For Begin,Commit,Rollback
                _conn.Open();
                tran = _conn.BeginTransaction();
                //End
                strQry = "Insert into DCRMain_Trans select * from DCRMain_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                //iReturnmain = db.ExecQry(strQry);

                //Modifed by sridevi - For Begin,Commit,Rollback
                iReturnmain = db.ExecQry(strQry, _conn, tran);
                //End

                if (iReturnmain > 0)
                {
                    strQry = "Insert into DCRDetail_Lst_Trans select * from DCRDetail_Lst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                   // iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "Insert into DCRDetail_UnLst_Trans select * from DCRDetail_UnLst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    //iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "Insert into DCRDetail_CSH_Trans select * from DCRDetail_CSH_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                   // iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End
                }
                if (iReturntemp >= 0)
                {
                    strQry = "DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";

                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";

                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End
                    strQry = "DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                 
                    //iReturn = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "DELETE from DCRMain_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRMain_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End
                }
                if (iReturn > 0)
                {
                    //Added by sridevi - For Begin,Commit,Rollback
                    tran.Commit();
                }

            }
            catch (Exception ex)
            {
                // Added  by Sridevi - To RollBack when error occurs     
                if (tran != null)
                    tran.Rollback();
                //Ends

                throw ex;
                // throw ex;
            }
            _conn.Close();
            return iReturn;
        }


        public int Reject_DCR(string SF_Code, int Trans_SlNo, string ReasonforReject)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "update DCRMain_Temp set confirmed = 2, ReasonforRejection= '" + ReasonforReject + "' where Trans_SlNo = '" + Trans_SlNo + "' " +
                         " and Sf_Code = '" + SF_Code + "' ";


                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int isDCR(string div_code, int imonth, int iyear)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(trans_slno)  from DCRMain_Temp " +
                            " where MONTH(activity_date)= " + imonth + " and YEAR(activity_date)= " + iyear + "  and Division_Code = '" + div_code + "' ";
                iReturn = db.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_dcr_dates(string div_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Trans_SlNo , Sf_Code,day(activity_date),Submission_Date,Activity_Date from DCRMain_Temp " +
                     " where MONTH(activity_date)= " + imon + " and YEAR(activity_date)= " + iyear + "  and Division_Code = '" + div_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet get_dcr_date(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_dcr_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time" +
                      " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB" +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_dcr_hos_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Hospital c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5 and b.Trans_Detail_Info_Code=c.hospital_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time " +
                           " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getSfName_HQ(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Name,Sf_HQ FROM  Mas_Salesforce " +
                     " WHERE sf_code= '" + sfcode + "' ";

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
        public DataSet getRemarks(string sfcode, string trans_slno)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " select a.remarks , wor.Worktype_Name_B  from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor " +
                     " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.worktype_code_b ";

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

        public DataSet Catg_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Catg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Catg_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_Catg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Catg_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet Catg_Visit_Report_noofvisit(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet Catg_Visit_Report_noofvisit1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit_self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";
            }
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }


        public DataSet Spec_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Spec_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet Spec_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (mode == 2)
            {

                strQry = "EXEC sp_DCR_Spec_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Spec_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet Class_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Class_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }


        public DataSet Class_Visit_Report1(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == 2)
            {
                strQry = "EXEC sp_DCR_Class_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            else if (mode == 1)
            {
                strQry = "EXEC sp_DCR_Class_VisitCallReport_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
            }
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet Visit_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_VisitDR '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "'  ";

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
        public DataSet Visit_Doc_noofvisit(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode, string novst)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC [sp_DCR_VisitDR_Catg_visit] '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' , '" + novst + "' ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_VisitDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_VisitDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }

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


        public DataSet Visit_Doc(string div_code, string sf_code, int cmon, int cyear, DateTime dtcurrent, string sMode, string vMode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if (sMode == "1")
            {
                strQry = " EXEC sp_DCR_VisitDR_Catg '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "2")
            {
                strQry = " EXEC sp_DCR_VisitDR_Spec '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }
            else if (sMode == "3")
            {
                strQry = " EXEC sp_DCR_VisitDR_Class '" + div_code + "', '" + sf_code + "', '" + cmon + "', '" + cyear + "', '" + dtcurrent + "', '" + vMode + "' ";
            }

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

        public DataSet Visit_Doc(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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

        public DataSet SF_ReportingTo_TourPlan(string div_code, string reporting_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.sf_code, a.Sf_Name, a.SF_Type, a.Sf_HQ, b.Designation_Short_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and a.Designation_Code=b.Designation_Code " +
                     " ORDER BY 2";
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


        public DataSet DCR_Visit_FLDWRK(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_FLDWRK  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_Total_Doc_Visit_Report(string sf_code, string div_code, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Total_Doc_Visit_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_Doc_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_Doc_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet get_worktype(string sf_code, int iMonth, int iDay, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.Work_Type ,b.Worktype_Name_B  " +
                     " FROM DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                     " WHERE a.Work_Type = b.WorkType_Code_B  " +
                     " and a.Sf_Code = '" + sf_code + "' " +
                     " and Day(a.Activity_Date ) = " + iDay +
                     " and Month(a.Activity_Date ) = " + iMonth +
                     " and Year(a.Activity_Date ) = " + iYear +
                     " ORDER BY 2";
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

        public DataSet get_Dcr_Exp(string sf_code, int iMonth, int iDay, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "EXEC FetchDCRDtls_forExpCalc '" + sf_code + "' ," + iDay + "," + iMonth + ", " + iYear + " ";

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

        public DataSet get_ExpAll(string sf_code, string Allowtype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (Allowtype == "HQ")
            {
                strQry = " select HQ_allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
            else if (Allowtype == "EX")
            {
                strQry = " select EX_HQ_Allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
            else if (Allowtype == "OS")
            {
                strQry = " select OS_Allowance from mas_allowance_fixation where sf_code = '" + sf_code + "' ";
            }
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

        public DataSet SF_Self_Report(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.sf_code, a.Sf_Name, a.SF_Type, a.Sf_HQ, b.Designation_Short_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.sf_code = '" + sf_code + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and a.Designation_Code=b.Designation_Code " +
                     " ORDER BY 2";
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

        //Saravana Changes

        public DataSet get_DCRRemarks(string sf_code, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Trans where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' order by Activity_Date";

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

        public DataSet DCR_Visit_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_Days '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_Visit_Leave(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Visit_Leave '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_Visit_TotalDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT(ListedDrCode)ListedDrCode from Mas_ListedDr where Sf_Code='" + sf_code + "' and " +
                     " month(ListedDr_Created_Date)<= '" + iMonth + "' and year(ListedDr_Created_Date)='" + iYear + "'" +
                     " and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalSubDaysQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans where Sf_Code = '" + sf_code + "' and month(Activity_Date)='" + iMonth + "' " +
                     " and YEAR(Activity_Date)='" + iYear + "' and Division_Code='" + div_code + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalFLDWRKQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='F'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalLeaveQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.Button_Access='L'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalChemistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=2 ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalStockistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=3 ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalUnlistDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(UnListedDrCode) from Mas_UnListedDr " +
                     " where Sf_Code = '" + sf_code + "' and UnListedDr_Active_Flag=0 and month(UnListedDr_Created_Date)='" + iMonth + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalStockistDocQuery(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRDetail_CSH_Trans " +
                     " where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=3 ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet DCR_TotalUnlstDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT(UnListedDrCode)ListedDrCode from Mas_UnListedDr where Sf_Code='" + sf_code + "' and " +
                     " month(UnListedDr_Created_Date)<'" + iMonth + "' and year(UnListedDr_Created_Date)='" + iYear + "' " +
                     " and Division_Code='" + div_code + "' and UnListedDr_Active_Flag=0 ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }




        public DataSet Get_Call_Total_Chemist_Visit_Report(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Call_Total_Chemist_Visit_Report  '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }



        public DataSet Get_Call_Total_Stock_Visit_Report(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Call_Total_Stock_Visit_Report  '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }



        public DataSet DCR_Unlst_Doc_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_UnlstDoc_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet get_DCRView_Pending_Approval_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B ,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.worktype_code_b ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        // Changes done by Saravanan
        public DataSet get_dcr_Pending_date(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Pending_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                          " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                          " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time" +
                          " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                          " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                          " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                          " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                          " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Single_dcr_date(string sf_code, int imon, int iyear, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and CONVERT(varchar(10),Activity_Date,103)='" + Date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Pending_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB" +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Pending_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                      " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time " +
                      " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Pending_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Pending_dcr_hos_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Hospital_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Hospital c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=5 and b.Trans_Detail_Info_Code=c.hospital_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet DCR_NonFwkQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select DCR.sf_Code,DCR.trans_slno,convert(varchar,DCR.Submission_Date,103) Submission_Date,day(DCR.Activity_Date) Activity_Date,'Sockiest Work' as Sockiest " +
                     " from DCRMain_TEMP DCR,Mas_WorkType_BaseLevel B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_B and B.FieldWork_Indicator='S'";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet get_dcr_Doctor_Detail_View(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Lst.Sf_Code,Lst.ListedDr_Name,Spc.Doc_Special_Name,cat.Doc_Cat_Name,Terr.Territory_Name, " +
                     " Qul.Doc_QuaName,Class.Doc_ClsName " +
                     " from Mas_ListedDr as Lst,Mas_Doctor_Category as cat,Mas_Doctor_Speciality as Spc, " +
                     " Mas_Territory_Creation as Terr,Mas_Doc_Qualification as Qul,Mas_Doc_Class as Class  where lst.Sf_Code='" + sf_code + "' and Lst.Doc_Cat_Code=cat.Doc_Cat_Code " +
                     " and Lst.Doc_Special_Code=Spc.Doc_Special_Code and Lst.Territory_Code=cast(Terr.Territory_Code as varchar)  " +
                     " and Qul.Doc_QuaCode=Lst.Doc_QuaCode and Lst.Doc_ClsCode=Class.Doc_ClsCode " +
                     " and MONTH(ListedDr_Created_Date)='" + iMonth + "' and YEAR(ListedDr_Created_Date)='" + iYear + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_DCRView_Approved_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B ,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Trans a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.worktype_code_b ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet get_Approved_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                        " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                        " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name  , d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time" +
                        " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                        " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                        " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                        " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                        " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Approved_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB" +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Approved_dcr_stk_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Approved_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                         " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                         " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time " +
                         " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                         " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                         " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                         " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                         " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_DCRHoliday_Name(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_M from DCRMain_Trans a,Mas_WorkType_Mgr b " +
                     " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                     " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                     " and b.WType_SName in('M','L','H','TR','S','IN','SS','CW','IW','CS','CM','SW','PL') ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_DCRPendingList(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select 'Pending Approval' Worktype_Name_M from DCRMain_Temp a,Mas_WorkType_Mgr b " +
                     " where a.Work_Type= b.WorkType_Code_M  and  a.Sf_Code='" + sf_code + "' " +
                     " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                     " and b.FieldWork_Indicator in('F') ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Changes done by Saravanan

        public DataSet get_dcr_DCRPendingdate(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Sridevi - To get all pending approvals
        public DataSet get_dcr_pending_approval_TransSl(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Trans_SlNo  From  DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 " +
                     " UNION " +
                     " select Trans_SlNo  From  DCRMain_Temp a, Mas_Salesforce b" +
                     " where b.sf_code = a.sf_code and b.Reporting_To_SF  = '" + sf_code + "' and a.confirmed=1";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public int Option_EditDCRDates(string sf_code, int Month, int Year, int Trans_Slno, string Edit_Date)
        {
            int iReturn = -1;
            int iretmove = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Option_DCR_Edit_Dates ";
                int Sl_No = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Option_DCR_Edit_Dates VALUES ('"+Sl_No+"' ,'" + sf_code + "' ," + Month + "," + Year + ", " + Trans_Slno + ", '" + Edit_Date + "',getdate(),getdate(),0) ";
                iReturn = db.ExecQry(strQry);

                if (iReturn > 0)
                {
                    // Transfer Trans data to Temp tables.
                    iretmove = Move_Trans_to_Temp(sf_code, Trans_Slno);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Move_Trans_to_Temp(string SF_Code, int Trans_SlNo)
        {
            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int ireturnupdate = -1;
            SqlConnection _conn = new SqlConnection(strConn);

            //Added by sridevi - For Begin,Commit,Rollback
            SqlTransaction tran = null;
            //End
            try
            {

                DB_EReporting db = new DB_EReporting();

                //Added by sridevi - For Begin,Commit,Rollback
                _conn.Open();
                tran = _conn.BeginTransaction();
                //End
                strQry = "Insert into DCRMain_Temp select * from DCRMain_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

              //  iReturnmain = db.ExecQry(strQry);
                //Modifed by sridevi - For Begin,Commit,Rollback
                iReturnmain = db.ExecQry(strQry, _conn, tran);
                //End

                if (iReturnmain > 0)
                {
                    strQry = "Insert into DCRDetail_Lst_Temp select * from DCRDetail_Lst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    //iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End
                    strQry = "Insert into DCRDetail_UnLst_Temp select * from DCRDetail_UnLst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                   // iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End
                    strQry = "Insert into DCRDetail_CSH_Temp select * from DCRDetail_CSH_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                   // iReturntemp = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturntemp = db.ExecQry(strQry, _conn, tran);
                    //End
                }
                if (iReturntemp >= 0)
                {
                    strQry = "DELETE from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    //iReturn = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End
                    strQry = "DELETE from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End
                    strQry = "DELETE from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "DELETE from DCRMain_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    //iReturn = db.ExecQry(strQry);

                    //Modifed by sridevi - For Begin,Commit,Rollback
                    iReturn = db.ExecQry(strQry, _conn, tran);
                    //End

                    strQry = "update DCRMain_Temp set Confirmed = 3  where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " "; // 3 - Edit 

                    //ireturnupdate = db.ExecQry(strQry);
                    //Modifed by sridevi - For Begin,Commit,Rollback
                    ireturnupdate = db.ExecQry(strQry, _conn, tran);
                    //End
                    if (ireturnupdate > 0)
                    {
                        //Added by sridevi - For Begin,Commit,Rollback
                        tran.Commit();
                    }
                }

            }
            catch (Exception ex)
            {
                // Added  by Sridevi - To RollBack when error occurs     
                if (tran != null)
                    tran.Rollback();
                //Ends

                throw ex;
            }
            _conn.Close();
            return iReturn;
        }

        // Added on 6th june - dcr edit - sridevi
        public DataSet getDCREntryDate_Edit(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(activity_date) from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '3' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet getWorkType_MR(string worktype_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel " +
                     " where Worktype_Name_B = '" + worktype_name + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public int RecordAdd_DelayDtls(string sf_code, int Month, int Year, string ddate, DateTime ldate,string div_code) //Modified by Sri - 07-Aug
        {
            int iReturn = -1;
            int Trans_SlNo = 0;

            try
            {
                DB_EReporting db = new DB_EReporting();
                //Delete temp if exists
                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + ddate + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo > 0)
                {
                    strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + sf_code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + sf_code + "'";
                    iReturn = db.ExecQry(strQry);
                }

               
                strQry = "SELECT Trans_SlNo FROM DCRMain_Trans WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + ddate + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);
                if (Trans_SlNo > 0)
                {
                    // Update Last Dcr Date in salesforce

                    DateTime dtDCR;
                    int iReturnsf = -1;
                    string Last_Dcr_Date = string.Empty;
                    // dtDCR = Convert.ToDateTime(ddate);
                    dtDCR = ldate.AddDays(1);
                    Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                    strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE Sf_Code= '" + sf_code + "' ";

                    iReturnsf = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "INSERT INTO DCR_Delay_Dtls(Sf_code,Month,Year,Delayed_Date, Delayed_Flag,Delay_Created_Date,Division_Code) VALUES ( '" + sf_code + "' ," + Month + "," + Year + ", '" + ddate + "',0, getdate(),'" + div_code + "') ";
                    iReturn = db.ExecQry(strQry);

                    if (iReturn > 0)
                    {
                        // Update Last Dcr Date in salesforce

                        DateTime dtDCR;
                        int iReturnsf = -1;
                        string Last_Dcr_Date = string.Empty;
                        // dtDCR = Convert.ToDateTime(ddate);
                        dtDCR = ldate.AddDays(1);
                        Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                        strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Sf_Code= '" + sf_code + "' ";

                        iReturnsf = db.ExecQry(strQry);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet getDCREntryDelay_Release(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(Delayed_Date) from DCR_Delay_Dtls " +
                     " where Sf_Code='" + SF_Code + "' and Delayed_Flag = 1 ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }



        //Changes done by Saravanan

        public DataSet Get_ChkWorkTypeName(string StrChkWorkType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where WorkType_Code_B in(" + StrChkWorkType + ")";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDCR;
        }

        public DataSet Get_CountWorkType(string SF_Code, string div_code, string cmonth, string cyear, string WorkTypeName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select count(Trans_SlNo)  " +
                     "from DCRMain_Trans DCR,Mas_WorkType_BaseLevel B " +
                     "where DCR.Sf_Code = '" + SF_Code + "' and month(DCR.Activity_Date)='" + cmonth + "' " +
                     "and YEAR(DCR.Activity_Date)='" + cyear + "' " +
                     "and DCR.Work_Type =B.WorkType_Code_B and DCR.Division_Code='" + div_code + "' and B.WType_SName='" + WorkTypeName + "' ";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDCR;
        }
        public DataSet Get_LastDCRDate(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select isnull(MAX(convert(varchar(10),activity_date,103)),'') from DCRMain_Trans where Sf_Code='" + sf_code + "' and Division_Code='" + Div_Code + "'";

            DataSet dsDCR = null;

            try
            {
                dsDCR = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsDCR;
        }
        public DataSet DCR_get_WorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet DCR_get_WorkType(string div_code, string work_type_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select FieldWork_Indicator,Button_Access from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' and WorkType_Code_B = '" + work_type_code + "'";
            }
            else if (sf_type == "2")
            {
                strQry = "select FieldWork_Indicator,Button_Access from Mas_WorkType_Mgr Where division_code = '" + div_code + "' and WorkType_Code_M = '" + work_type_code + "'";
            }

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getWorkTypeCode_MR(string worktype_sname, string sf_type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
            {
                strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel " +
                         " where division_code = '" + div_code + "' and  FieldWork_Indicator = '" + worktype_sname + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr " +
                         " where division_code = '" + div_code + "' and  FieldWork_Indicator = '" + worktype_sname + "' ";
            }
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getLeave(string sf_code, string leavedate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Leave_id,Leave_Active_Flag from mas_Leave_Form where sf_code = '" + sf_code + "'  and  '" + leavedate + "' between From_Date and To_Date and Leave_Active_Flag != 1";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Changes done by Priya

        public DataSet getReleaseDate(string SF_Code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select convert(varchar,d.Delayed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name from DCR_Delay_Dtls d, Mas_Salesforce s " +
                     " where d.Sf_Code='" + SF_Code + "' and MONTH(Delayed_Date)='" + sMonth + "' " +
                     " and Year(Delayed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and  Delayed_Flag =0";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_Release_Sf()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                     " union " +
                     " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                     "  from DCR_Delay_Dtls d ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public int Update_Delayed(string sf_code, DateTime ddate)
        {
            int iReturn = -1;
            string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE DCR_Delay_Dtls " +
                            " SET Delayed_Flag = 1 , " +
                            " Delay_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                            " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =0 and Delayed_Date='" + delaydate + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getDCREdit(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type,Plan_No,Remarks from dcrmain_temp " +
                     " where Sf_Code='" + SF_Code + "' and Confirmed = '3'  and  Activity_Date = '" + DCRDate + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int Update_Header(string sf_code, int trans_slno, bool reentry, string dcrdate, string div_code, string Activity_date, bool isdelayrel)
        {
            int iReturn = -1;
            int iReturnmax = -1;
            int iReturnmis = -1;
            int iReturnsf = -1;
            int iReturndel = -1;

            SqlConnection _conn = new SqlConnection(strConn);
            //Added by sridevi - For Begin,Commit,Rollback
            SqlTransaction tran = null;
            //End
            try
            {

                DB_EReporting db = new DB_EReporting();

                

                //Added by sridevi - For Begin,Commit,Rollback
                _conn.Open();
                tran = _conn.BeginTransaction();
                //End

                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1  " +
                            " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo =" + trans_slno + "";

               // iReturn = db.ExecQry(strQry);

                //Modifed by sridevi - For Begin,Commit,Rollback
                iReturn = db.ExecQry(strQry, _conn, tran);
                //End

                if (iReturn > 0)
                {
                    if (reentry == false)
                    {

                        // Modified on 6th June - To update Last Dcr date in Sales force
                        DateTime dtDCR;
                      
                        string Last_Dcr_Date = string.Empty;
                        dtDCR = Convert.ToDateTime(dcrdate);
                        dtDCR = dtDCR.AddDays(1);
                        Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                        strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Sf_Code= '" + sf_code + "' ";

                       // iReturnsf = db.ExecQry(strQry);

                        //Modifed by sridevi - For Begin,Commit,Rollback
                        iReturnsf = db.ExecQry(strQry, _conn, tran);
                        //End

                    }
                       
                    if (isdelayrel == true)
                    {
                      
                        strQry = " Update DCR_Delay_Dtls  set Delayed_Flag =  2 where Sf_Code= '" + sf_code + "' and Delayed_Date = '" + Activity_date + "' and Division_Code = '" + div_code + "' ";

                       // iReturndel = db.ExecQry(strQry);

                        //Modifed by sridevi - For Begin,Commit,Rollback
                        iReturndel = db.ExecQry(strQry, _conn, tran);
                        //End

                        strQry = " Update DCR_MissedDates  set Status =  2 where Sf_Code= '" + sf_code + "' and Dcr_Missed_Date = '" + Activity_date + "' and Division_Code = '" + div_code + "' ";

                      //  iReturnmis = db.ExecQry(strQry);

                        //Modifed by sridevi - For Begin,Commit,Rollback
                        iReturnmis = db.ExecQry(strQry, _conn, tran);
                        //End

                        
                    }
                    if (isdelayrel == true)
                    {
                        if ((iReturndel > 0) || (iReturnmis > 0))
                        {
                            //Added by sridevi - For Begin,Commit,Rollback
                            tran.Commit();
                        }
                    }
                    if ((reentry == false) && (isdelayrel == false))
                    {
                        if (iReturnsf > 0)
                        {
                            //Added by sridevi - For Begin,Commit,Rollback
                            tran.Commit();
                        }
                    }
                    if ((reentry == true) && (isdelayrel == false))
                    {
                        if (iReturn > 0)
                        {
                            //Added by sridevi - For Begin,Commit,Rollback
                            tran.Commit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Added  by Sridevi - To RollBack when error occurs     
                if (tran != null)
                    tran.Rollback();
                //Ends
                throw ex;
            }
            _conn.Close();
            return iReturn;

        }
        public DataSet getMgrWorkAreaDtls_All(string SF_Code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Work_Type, Territory_Code, sf_code from DCR_MGR_WorkAreaDtls Where " +
                     " MGR_Code ='" + SF_Code + "' and  DCR_Date ='" + DCRDate + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // Changes done by Saravanan
        public DataSet get_Approved_dcr_stk_detailsView(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        //Added by sri - 15July
        public DataSet get_dcr_Not_Submitted(string sf_code, int cday, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Activity_Date) pending_date from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear + " " +
                     " and DAY(Activity_Date) = " + cday + " " +
                     " UNION" +
                     " select day(Activity_Date) pending_date from DCRMain_Trans " +
                     " where Sf_Code = '" + sf_code + "' and Confirmed = 1 and MONTH(Activity_Date) = " + imon + " and YEAR(Activity_Date) = " + iyear + " " +
                     " and DAY(Activity_Date) = " + cday;

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        //Changes done by Priya
        public DataSet get_delayed_Status(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select distinct CONVERT(varchar(10),Delay_Created_Date,103) from DCR_Delay_Dtls where Month(Delay_Created_Date)=Month(getdate()) and  Sf_Code='" + sf_code + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        //Changes done by Saravanan

        public DataSet getDCR_Report_MR_Calendar(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select WType_SName " +
                                 " from DCRMain_Trans a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select case confirmed  " +
                                 " when '1' then 'FW(NA)' " +
                                 " When '2' then 'FW(R)' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and b.WType_SName='FW' and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_BaseLevel b " +
                                 " where a.Work_Type = b.WorkType_Code_B " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + "  and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select 'D' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=0 " +
                                 " union " +
                                 " select 'DR' WType_SName  from DCR_Delay_Dtls where Sf_Code='" + SF_Code + "' and DAY(Delayed_Date) = " + sday +
                                 " and [Month] = " + sMonth + " and [Year] = " + sYear + " and Delayed_Flag=1";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_DCR_Rejected_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name,W.Worktype_Name_B,b.ReasonforRejection, " +
                        "  CONVERT(char(10),Activity_Date,103) as DCR_Activity_Date,MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                        " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                        " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                        " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                        " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c,Mas_WorkType_BaseLevel W      " +
                        " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code and b.Work_Type = W.WorkType_Code_B  " +
                        " and a.SF_code  = '" + sf_code + "' and b.confirmed=2";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public bool chkdcrstpchg(string sf_type, string dcrdate, string div_code, string sf_code)
        {

            bool bRecordExist = false;
            DataSet dsdcr = null;
            DataSet dsadm = null;
            DateTime dcrsubdate = DateTime.Today;

            DateTime dcrsetdate = DateTime.Today;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT Submission_Date FROM DCRMain_Temp WHERE Division_Code = '" + div_code + "' and  Sf_Code = '" + sf_code + "' and Activity_Date ='" + dcrdate + "' ";

                dsdcr = db.Exec_DataSet(strQry);
                if (dsdcr.Tables[0].Rows.Count > 0)
                {
                    dcrsubdate = Convert.ToDateTime(dsdcr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (sf_type == "1")
                    {
                        strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups where Division_Code = '" + div_code + "' ";
                    }
                    else if (sf_type == "2")
                    {
                        strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR where Division_Code = '" + div_code + "'";
                    }

                    dsadm = db.Exec_DataSet(strQry);

                    if (dsadm.Tables[0].Rows.Count > 0)
                    {
                        dcrsetdate = Convert.ToDateTime(dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    }
                    if ((dsdcr.Tables[0].Rows.Count > 0) && (dsadm.Tables[0].Rows.Count > 0))
                    {
                        if (dcrsubdate < dcrsetdate) // Setup Change
                        {
                            bRecordExist = true;
                        }
                        else
                        {
                            bRecordExist = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool chkxml(string sf_type, DateTime dcrdate, string div_code, string sf_code)
        {

            bool bRecordExist = false;
            DataSet dsdcr = null;
            DataSet dsadm = null;
            DateTime dcrsubdate = DateTime.Today;

            DateTime dcrsetdate = DateTime.Today;
            try
            {
                DB_EReporting db = new DB_EReporting();


                if (sf_type == "1")
                {
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups";
                }
                else if (sf_type == "2")
                {
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR";
                }

                dsadm = db.Exec_DataSet(strQry);

                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "")
                        dcrsetdate = Convert.ToDateTime(dsadm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                }
                if (dsadm.Tables[0].Rows.Count > 0)
                {
                    if (dcrdate < dcrsetdate) // Setup Change
                    {
                        bRecordExist = true;
                    }
                    else
                    {
                        bRecordExist = false;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int Clear_Header(string SF_Code, string Activity_Date)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int Trans_SlNo = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + SF_Code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE Division_Code = '" + Division_Code + "' and  Sf_Code = '" + SF_Code + "' and Activity_Date ='" + Activity_Date + "' ";
                Trans_SlNo = db.Exec_Scalar(strQry);

                if (Trans_SlNo > 0)
                {
                    strQry = "delete from DCRDetail_Lst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_CSH_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    strQry = "delete from DCRDetail_UnLst_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                   " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);

                    //strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                    // " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    //iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Added - 7-Sep-15
        public DataSet getTerrChemists(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select * from (select Chemists_Code, ltrim(Chemists_Name) Chemists_Name , '#ACFA58' col ,1 sortby   " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code = '" + Terr_Code + "' " +
                     " UNION ALL" +
                     " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name ,'#FFFFFF' col ,2 sortby   " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code != '" + Terr_Code + "') doc" +
                     " order by sortby,Chemists_Name";

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
        //Added - 7-Sep-15
        public DataSet getTerrChemists_color(string sfcode, string Terr_Code) // Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code = '" + Terr_Code + "' ";
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

        public DataSet getTerrUnListedDoctorSrc(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select * from (SELECT UnListedDrCode, UnListedDr_Name , '#ACFA58' col ,1 sortby" +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code = '" + Terr_Code + "' " +
                        " UNION ALL" +
                        " SELECT UnListedDrCode, UnListedDr_Name ,'#FFFFFF' col ,2 sortby" +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code != '" + Terr_Code + "' ) doc " +
                         " order by sortby,UnListedDr_Name";

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
        public DataSet getTerrUnListedDoctorSrc_Color(string sfcode, string Terr_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code = '" + Terr_Code + "' ";
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

        //Added by Sri - To get Activity_date - for SlNo
        public DataSet getDCR_ActivityDate(int slno)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Activity_Date from DCRMain_Trans " +
                     " Where Trans_SlNo =" + slno + " ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // 10-Sep-15 Added to create XML
        public DataSet get_Trans_Head(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select Work_Type,Plan_No,Remarks,Start_Time" +
                     " from DCRMain_Temp " +
                     " where Sf_Code = '" + sf_code + "' " +
                     " and Activity_Date = '" + Activity_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet get_Lst_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Session,a.Session_Code,a.Time,a.Minutes,a.Seconds,a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name, a.Product_Code,a.Product_Detail,a.Additional_Prod_Code,a.Additional_Prod_Dtls,a.Gift_Code,a.Gift_Name,a.Gift_Qty, " +
                        "a.Additional_Gift_Code,a.Additional_Gift_Dtl,a.Activity_Remarks from DCRMain_Temp m, DCRDetail_Lst_Temp  a where " +
                        "a.trans_slno = m.trans_slno and " +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Che_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name,POB,a.SDP from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                        "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 2 and" +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Stk_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                       "a.Worked_with_Name,POB,Visit_Type from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                       "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 3 and" +
                       " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public DataSet get_Hos_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                          "a.Worked_with_Name,POB from DCRMain_Temp m, DCRDetail_CSH_Temp  a where " +
                          "a.trans_slno = m.trans_slno and a.Trans_Detail_Info_Type= 5 and" +
                          " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_UnLst_Trans(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select a.Session,a.Session_Code,a.Time,a.Minutes,a.Seconds,a.Trans_Detail_Info_Code,a.Trans_Detail_Name,a.Worked_with_code, " +
                        "a.Worked_with_Name, a.Product_Code,a.Product_Detail,a.Additional_Prod_Code,a.Additional_Prod_Dtls,a.Gift_Code,a.Gift_Name,a.Gift_Qty, " +
                        "a.Additional_Gift_Code,a.Additional_Gift_Dtl,a.Activity_Remarks,a.SDP from DCRMain_Temp m, DCRDetail_UnLst_Temp  a where " +
                        "a.trans_slno = m.trans_slno and " +
                        " m.Sf_Code = '" + sf_code + "'  and m.Activity_Date = '" + Activity_date + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        //Added a function to fetch Work Type details by Sridevi on 09/16/15
        public DataSet DCR_get_WorkType(string div_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            if (sf_type == "1")
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_B as WorkType_Code from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' ";
            else if (sf_type == "2")
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_M as WorkType_Code from Mas_WorkType_Mgr Where division_code = '" + div_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


        public int RecordDelMGRWorkArea(string MGR_Code, string DCRDate)
        {
            int iReturn = -1;
            int S_No = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT count(S_No) FROM DCR_MGR_WorkAreaDtls WHERE MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' ";
                S_No = db.Exec_Scalar(strQry);

                if (S_No > 0)
                {
                    strQry = "delete from DCR_MGR_WorkAreaDtls where MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' ";

                    iReturn = db.ExecQry(strQry);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        // Added by Sridevi - 8-Dec-15
        public DataTable getListedDoctor_Exist(string sfcode, string SName, string doc_disp)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {

                    strQry = "SELECT ListedDrCode, ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND ListedDr_Name like + '" + SName + "' Order By 2";

                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                           " FROM Mas_ListedDr " +
                           " WHERE Sf_Code =  '" + sfcode + "' " +
                           " AND ListedDr_Active_Flag = 0 " +
                           " AND ListedDr_Name like + '" + SName + "' Order By 2";


                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = "SELECT a.ListedDrCode, " +
                        "a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                             " WHERE a.Sf_Code =  '" + sfcode + "' " +
                          " AND a.ListedDr_Active_Flag = 0 " +
                          " AND a.ListedDr_Name like + '" + SName + "' Order By 2";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = "SELECT a.ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "' Order By 2";

                }
                else if (doc_disp == "5")//Class
                {
                    strQry = "SELECT a.ListedDrCode, " +
                               " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                           " AND a.ListedDr_Active_Flag = 0 " +
                           " AND a.ListedDr_Name like + '" + SName + "' Order By 2";

                }
                try
                {
                    dsListedDR = db_ER.Exec_DataTable(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return dsListedDR;
        }


        public DataTable getListedDoctorMGR_Exist(string sfcode, string SName, string doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                if (doc_disp == "1")// DR Name
                {
                    strQry = " select ListedDrCode,ListedDr_Name,Territory_Code" +
                           " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                            " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'" +
                                " union all " +
                               " select  ListedDrCode, ListedDr_Name,Territory_Code" +
                               " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                               " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'";

                   
                }
                else if (doc_disp == "2")//Slno
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "' " +
                               " union all " +
                              " select ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'";
                }
                else if (doc_disp == "3")//Speciality
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'" +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "' ";

                }
                else if (doc_disp == "4")//Category
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "' " +
                               " union all " +
                              " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'";

                   
                }
                else if (doc_disp == "5")//Class
                {
                    strQry = " select  ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                          " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                            " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "' " +
                              " union all " +
                             " select  ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0 AND ListedDr_Name like + '" + SName + "'";

                    
                }
            
                try
                {
                    dsListedDR = db_ER.Exec_DataTable(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }

        public DataTable getUnDoctor_Exist(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "' Order By 2";
            }

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

        public DataTable getUnDoctor_MGR_Exist(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND UnListedDr_Name like + '" + SName + "' Order By 2";
            }

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

        public DataTable getChe_src_Exist(string sfcode, string SName)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code = '" + sfcode + "' " +
                   " and Chemists_Name like + '" + SName + "' Order By 2";
            }

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
        public DataTable getChe_srcMGR_Exist(string sfcode, string SName, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsListedDR = null;

            SName = SName.Replace('*', ' ');


            if (SName.Trim().Length > 0)
            {
                strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                   " from Mas_Chemists " +
                   " where Chemists_Active_Flag=0 " +
                   " and Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "') " +
                   " and Chemists_Name like + '" + SName + "' Order By 2";
            }

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


        // added by sri
        public DataSet getSfStatus(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "Select sf_TP_Active_Flag from Mas_Salesforce where sf_code = '" + sf_code + "'";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getDocCount(string sf_code, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "Select count(*) from Mas_Listeddr where sf_code = '" + sf_code + "' and " +
                     " ListedDr_Active_Flag = 0 and (Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " Territory_Code like '%" + ',' + TerrCode + ',' + "%' or Territory_Code like '" + TerrCode + "') ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getCheCount(string sf_code, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " Select count(*) from Mas_Chemists where sf_code = '" + sf_code + "' and " +
                     " Chemists_Active_Flag=0 and Territory_Code = '" + TerrCode + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getUnCount(string sf_code, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " Select count(*) from Mas_Unlisteddr where sf_code = '" + sf_code + "' and " +
                    " UnListedDr_Active_Flag=0 and Territory_Code = '" + TerrCode + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public int getdcr_docprod(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsprod = null;

            int iReturn = -1;

            try
            {
                strQry = "select trans_slno,trans_detail_slno, Product_Code ,Product_Detail, division_code from DCRDetail_Lst_Trans" +
                            " Where division_code = '" + div_code + "' and Product_Detail !='' ";

                dsprod = db_ER.Exec_DataSet(strQry);
                if (dsprod.Tables[0].Rows.Count > 0)
                {
                    iReturn = 1;
                    foreach (DataRow drrow in dsprod.Tables[0].Rows)
                    {

                        string prod = drrow["Product_Code"].ToString();
                        string Prod_Code = drrow["Product_Detail"].ToString();
                        string Prod_Name = drrow["Product_Detail"].ToString();
                        string prod_det = drrow["Product_Detail"].ToString();
                        string[] addprod = prod.Split('#');
                        string[] addprod_det = prod_det.Split('#');

                        foreach (string aprod in addprod_det)
                        {
                            //Levox~1$ # LAPP~2$#
                            if (aprod != "")
                            {
                                string product = aprod;
                                string prodname = string.Empty;

                                prodname = product.Substring(0, product.IndexOf("~")); //aprod.EndsWith('~');

                                strQry = " select Prod_Detail_Sl_No,product_code_slno from Mas_Product_Detail where Product_Detail_Name = '" + prodname + "' " +
                                            " and division_code = '" + div_code + "'";

                                dsprod = db_ER.Exec_DataSet(strQry);
                                if (dsprod.Tables[0].Rows.Count > 0)
                                {
                                    string pc = dsprod.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    string pnsl = dsprod.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                                    if (prodname.Trim().Length > 0)
                                    {
                                        //Prod_Name = Prod_Name + prodname + "~" + "$" + "#";
                                        //Prod_Code = Prod_Code + pnsl + "~" +  "$" + "#";

                                        Prod_Code = Prod_Code.Replace(prodname, pnsl);
                                    }

                                }
                            }
                        }

                        strQry = "Update DCRDetail_Lst_Trans SET Product_Code  ='" + Prod_Code + "', Product_Detail = '" + Prod_Name + "'  where trans_detail_slno = '" + drrow["trans_detail_slno"].ToString() + "' ";

                        iReturn = db_ER.ExecQry(strQry);
                    }
                }
                else
                {
                    iReturn = 2;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Added by Sridevi - 7Feb16
        public DataSet getTerrListedDoctor_New(string sfcode, int doc_disp)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                         " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                         " order by ListedDr_Name";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                       " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                       " order by ListedDr_Name";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                      " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                      " order by ListedDr_Name";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                     " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                     " order by ListedDr_Name";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                    " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                    " order by ListedDr_Name";         
            }
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

        public DataSet getTerrListedDoctor_New_DupSet(string sfcode, int doc_disp)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                         " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                         " order by Territory_Code,ListedDr_Name";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                       " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                       " order by Territory_Code,ListedDr_Name";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                      " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                      " order by Territory_Code,ListedDr_Name";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                     " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                     " order by Territory_Code,ListedDr_Name";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName  AS ListedDr_Name, Territory_Code  FROM Mas_ListedDr " +
                    " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                    " order by Territory_Code,ListedDr_Name";
            }
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

        public DataSet getTerrChemists_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name , Territory_Code    " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " order by Chemists_Name";

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

        public DataSet getTerrChemists_New_dup(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name , Territory_Code    " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " order by Territory_Code,Chemists_Name";

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

        public DataSet getTerrUnListedDoctorSrc_New(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name ,Territory_Code" +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " order by UnListedDr_Name";

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


        public DataSet getTerrUnListedDoctorSrc_New_dup(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " SELECT UnListedDrCode, UnListedDr_Name ,Territory_Code" +
                      " FROM Mas_UnListedDr " +
                      " WHERE Sf_Code =  '" + sfcode + "' " +
                      " AND UnListedDr_Active_Flag = 0 " +
                      " order by Territory_Code,UnListedDr_Name";

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

        //Added by Sridevi - 7Feb16
        public DataSet getTerrListedDoctorMgr_New(string sfcode, int doc_disp,string DCRDate)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                 strQry =  " select ListedDrCode,ListedDr_Name,Territory_Code " +
                            " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                             " AND ListedDr_Active_Flag = 0 order by ListedDr_Name";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, Territory_Code " +
                            "from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                             " AND ListedDr_Active_Flag = 0 order by ListedDr_Name";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name, Territory_Code" +
                            "  from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                             " AND ListedDr_Active_Flag = 0 order by ListedDr_Name";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName  AS ListedDr_Name, Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                         " AND ListedDr_Active_Flag = 0 order by ListedDr_Name";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName  AS ListedDr_Name, Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                         " AND ListedDr_Active_Flag = 0 order by ListedDr_Name";
            }
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

        public DataSet getTerrListedDoctor_NewMgr_DupSet(string sfcode, int doc_disp, string DCRDate)// Modified by Sri - 6 Aug
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " select ListedDrCode,ListedDr_Name,Territory_Code " +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                         " AND ListedDr_Active_Flag = 0 order by Territory_Code, ListedDr_Name";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name, Territory_Code " +
                            "from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                             " AND ListedDr_Active_Flag = 0 order by Territory_Code,ListedDr_Name";
            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name, Territory_Code" +
                            "  from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                             " AND ListedDr_Active_Flag = 0 order by Territory_Code, ListedDr_Name";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName  AS ListedDr_Name, Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                         " AND ListedDr_Active_Flag = 0 order by Territory_Code, ListedDr_Name";
            }
            else if (doc_disp == 5) //Class
            {
                strQry = " SELECT ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName  AS ListedDr_Name, Territory_Code" +
                         " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                         " AND ListedDr_Active_Flag = 0 order by Territory_Code, ListedDr_Name";
            }
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

        public DataSet getmgrcolor(string sfcode, string DCRDate, string doccode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select Territory_code,Color_code from DCR_MGR_WorkAreaDtls Where" +
                      " MGR_Code = '" + sfcode + "' " +
                      " AND DCR_Date =  '" + DCRDate + "' ";
                     

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

        public DataSet getTerrListedDoctor_Mgr_New(string sfcode, int doc_disp, string dcr_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Mgr_DCR_Doc_List] '" + sfcode + "' ," + doc_disp + ",'" + dcr_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getTerrUnListedDoctor_MgrNew_proc(string sfcode, string dcr_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Mgr_DCR_UnDoc_List] '" + sfcode + "','" + dcr_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet getChemists_New(string sfcode, string dcr_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Mgr_DCR_Che_List] '" + sfcode + "','" + dcr_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getAllTerr(string sfcode, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select 'ALL' SF_Code, '0' Territory_Code, '---Select---' Territory_Name  " +
                    " UNION " +
                   " select SF_Code,  Territory_Code,  Territory_Name " +
                    " from Mas_Territory_Creation  " +
                    " where  Territory_Active_Flag=0  and " +
                    " SF_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                    " order by SF_Code,Territory_Name";


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
        public DataSet getAllMRTerr(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Mgr_DCR_Terr_List] '" + div_code + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet getDCR_Apps_MissedDate(string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select min(Dcr_Missed_Date) from DCR_MissedDates " +
                     " where Sf_Code='" + SF_Code + "' and Status = 1 ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet getStockist_New(string sfcode, string dcr_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_Mgr_DCR_Stk_List] '" + sfcode + "','" + dcr_date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }


		 public DataSet getRemarksVal(string DivCode, string SFCode,string FYear, string FMonth,string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC GetRemarks '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + SubDivCode + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

         public DataSet getNew_outlet_sale_Val(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode,string modedt)
         {
             DB_EReporting db_ER = new DB_EReporting();

             DataSet dsTP = null;

              modedt = "";

            if (FMonth.Length == 1)
            { FMonth = "0" + FMonth; }

             strQry = "EXEC GET_New_Outlet_Sales_Rep '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + FMonth + "','" + FYear + "','" + modedt + "'";

             try
             {
                 dsTP = db_ER.Exec_DataSet(strQry);
             }
             catch (Exception ex)
             {
                 throw ex;
             }
             return dsTP;
         }

		  public DataSet getRemarksRetailers(string DivCode, string SFCode, string FYear, string FMonth,string Remark, string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC GetRemarksRetailer '" + DivCode + "','" + SFCode + "','" + FYear + "','" + FMonth + "','" + Remark + "','" + SubDivCode + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
          public DataSet getNew_outlet_List(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode, string modedt)
          {
              DB_EReporting db_ER = new DB_EReporting();

              DataSet dsTP = null;

              strQry = "EXEC GET_New_Retailer_List_Today '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + modedt + "'";

              try
              {
                  dsTP = db_ER.Exec_DataSet(strQry);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dsTP;
          }
          public DataSet getNew_outlet_List_no(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode, string modedt)
          {
              DB_EReporting db_ER = new DB_EReporting();

              DataSet dsTP = null;

              strQry = "EXEC GET_New_Outlet_Sales_Rep_No_re '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + FMonth + "','"+FYear+"'";

              try
              {
                  dsTP = db_ER.Exec_DataSet(strQry);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dsTP;
          }
          public DataSet getSec_sale_Val(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode, string modedt, string ToDt)
          {
              DB_EReporting db_ER = new DB_EReporting();

              DataSet dsTP = null;

              strQry = "EXEC GET_sec_order_dtl_Rep '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + FMonth + "','" + FYear + "','" + modedt + "','" + ToDt + "'";

              try
              {
                  dsTP = db_ER.Exec_DataSet(strQry);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dsTP;
          }
          public DataSet getExp_status_rep(string DivCode, string SFCode, string FYear, string FMonth, string SubDivCode, string modedt)
          {
              DB_EReporting db_ER = new DB_EReporting();

              DataSet dsTP = null;

              strQry = "EXEC GET_Exp_Status_Rep '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + FMonth + "','" + FYear + "','" + modedt + "'";

              try
              {
                  dsTP = db_ER.Exec_DataSet(strQry);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dsTP;
          }
          public DataSet GetDCRDeviation(string SFCode, string FYear, string FMonth)
          {
              DB_EReporting db_ER = new DB_EReporting();

              DataSet dsTP = null;

//              strQry = "select sf_code,sf_name,cast(convert(varchar,Activity_Date,101) as datetime) Activity_Date,cast(convert(varchar,Submission_Date,101) as datetime) Submission_Date,Work_Type,WorkType_Name,FieldWork_Indicator,Plan_No,Plan_Name from DCRMain_Trans b where  YEAR(b.Activity_Date)='" + FYear + "' and b.SF_Code='" + SFCode + "' order by Activity_Date ";
              strQry = "select T.sf_code,s.sf_name, cast(convert(varchar,pln_date,101) as datetime) Activity_Date, wtype Work_Type,worked_with_name,(case when sf_type=1 then (select Worktype_Name_B from Mas_WorkType_BaseLevel WT where WT.WorkType_Code_B=t.wtype) else (select Worktype_Name_M from Mas_WorkType_Mgr WT where WT.WorkType_Code_M=t.wtype) End) as  WorkType_Name,FWFlg FieldWork_Indicator,cluster Plan_No,ClstrName Plan_Name,StkName  from TbMyDayPlan T inner join mas_Salesforce s on s.sf_code=t.sf_code where T.sf_code='" + SFCode + "' and YEAR(pln_date)='" + FYear + "'  order by Activity_Date ";

              try
              {
                  dsTP = db_ER.Exec_DataSet(strQry);
              }
              catch (Exception ex)
              {
                  throw ex;
              }
              return dsTP;
          }
		  
		  public DataSet getPrimary_sale_Val(string DivCode, string SFCode, string Fromdate, string ToDate, string SubDivCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC GET_Primary_order_dtl_Rep '" + DivCode + "','" + SubDivCode + "','" + SFCode + "','" + Fromdate + "','" + ToDate + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
    }
}
