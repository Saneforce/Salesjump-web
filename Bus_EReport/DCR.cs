using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class DCR
    {
        private string strQry = string.Empty;


        #region Tsr
        //public DataSet GetTsrSchemeReport(string sf_code, string Zone_code, string Area_code, string div_code, string Fdate, string Tdate)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;


        //    int divcode = 0;

        //    if (div_code == "" || div_code == null)
        //    { divcode = 0; }
        //    else { divcode = Convert.ToInt32(divcode); }

        //    if (Fdate == "" || Fdate == null)
        //    { Fdate = Convert.ToString(DateTime.Now.Date);  }

        //    if (Tdate == "" || Tdate == null)
        //    { Tdate = Convert.ToString(DateTime.Now.Date); }

        //    if (sf_code == null)
        //    { sf_code = "admin"; }

        //    if (Zone_code == null)
        //    { Zone_code = "0"; }

        //    if (Area_code == null)
        //    { Area_code = "0"; }


        //    strQry = "Exec [Get_Tsr_SchemeReport] @SF ='" + sf_code + "',@Zone_code ='" + Zone_code + "',@Area_code ='" + Area_code + "',@Div =" + divcode + ",@fDt ='" + Fdate + "',@tdt = '" + Tdate + "'";
        //    try
        //    {
        //        dsAdmin = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsAdmin;
        //}

        public DataSet GetTsrSchemeReport(string div_code, string sf_code,  string Fdate, string Tdate,string stcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            int divcode = 0;

            if (div_code == "" || div_code == null)
            { divcode = 0; }
            else { divcode = Convert.ToInt32(divcode); }

            if (Fdate == "" || Fdate == null)
            { Fdate = Convert.ToString(DateTime.Now.Date); }

            if (Tdate == "" || Tdate == null)
            { Tdate = Convert.ToString(DateTime.Now.Date); }

            if (sf_code == null)
            { sf_code = "admin"; }

            if (stcode == null)
            { stcode = "0"; }

            strQry = "Exec [Get_Tsr_SchemeReport] @div_code=" + divcode + ", @SF ='" + sf_code + "',@fDt ='" + Fdate + "',@tdt ='" + Tdate + "',@stcode ='" + stcode + "' ";

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

        #endregion

        public DataSet getWorkType(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DB_EReporting db_dcr_product_detaillER = new DB_EReporting();

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


		public DataSet GetTPDayMap_MR(string div_code, string sf_code, string Fdate, string Tdate, string stcode,string distcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "EXEC sp_Get_Tb_My_Day_MR  '" + div_code + "', '" + sf_code + "','" + Fdate + "', '" + Tdate + "' ";
			strQry = "EXEC sp_Get_Tb_My_Day_MR  '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate+ "','" + stcode + "','" + distcode + "'";
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
		
		 public DataSet Get_TsrTPDayMap_MR(string div_code, string sf_code, string Fdate, string Tdate, string stcode, string distcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "EXEC sp_Get_Tb_My_Day_MR  '" + div_code + "', '" + sf_code + "','" + Fdate + "', '" + Tdate + "' ";
            strQry = "EXEC [PR_GET_TSR_GetTbMyDayMR]  '" + div_code + "','" + sf_code + "','" + Fdate + "','" + Tdate + "','" + stcode + "','" + distcode + "'";
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
		
        //public DataSet getWorkType_MRDCR(string div_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsTP = null;
        //    strQry = "select '0' WorkType_Code_B, '---Select---' Worktype_Name_B UNION select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%D%'" +
        //              " and division_code = '" + div_code + "'  ORDER BY 2";
        //    try
        //    {
        //        dsTP = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsTP;
        //}
        public DataSet getWorkType_MRDCR(string div_code, string sf_type)
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
        public DataSet CATEGORY_VIEW_HQ_order(string div_code, string sf_code, string Tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec CATEGORY_VIEW_HQ_order '" + sf_code + "','" + div_code + "','" + Tdate + "','" + subdivcode + "'";
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

        public DataSet getcusproduct(string date, string distcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [getCuswsProdSecDets] '" + date + "', '" +distcode+ "' ";
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

            strQry = " select '0' Product_Detail_Code, '-Product-' Product_Detail_Name UNION select b.Prod_Detail_Sl_No as Product_Detail_Code,b.Product_Detail_Name " +
                     " from Mas_Salesforce a, Mas_Product_Detail b " +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and cast(b.Division_Code as varchar) = '" + divcode + "' " +
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

        public int RecordAdd_Header(string SF_Code, string sf_name, string emp_id, string employee_id, string Activity_Date, string Work_Type, string SDP, string SDP_Name, string sRemarks, string vConf1, string dcrdate, bool reentry, bool isdelayrel, string vConf, string Start_Date, string WorkType_name)
        {
            int iReturn = -1;
            int iReturnmax = -1;

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

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);
                }
                if (vConf == "1")
                {
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Main as bigint)),0)+1 FROM DCR_MaxSlNo";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_SlNo as bigint)),0)+1 FROM DCRMain_Temp";
                    Trans_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRMain_Temp (Trans_SlNo, Sf_Code,sf_name,emp_id,employee_id, Activity_Date, Submission_Date, Work_Type,Plan_No,Plan_Name, Division_Code, Remarks, confirmed,Start_Time,End_Time,WorkType_Name) " +
                       " VALUES('" + Trans_SlNo + "', '" + SF_Code + "','" + sf_name + "', '" + emp_id + "','" + employee_id + "','" + Activity_Date + "', getdate(), '" + Work_Type + "', '" + SDP + "', '" + SDP_Name + "', '" + Division_Code + "', '" + sRemarks + "','" + vConf1 + "','" + Start_Date + "', getdate(), '" + WorkType_name + "')";

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


        public int RecordAdd_Detail(string SF_Code, int Trans_SlNo, int Trans_Detail_Info_Type, string Trans_Detail_Info_Code, string DCR_Session, string DCR_Time, string Worked_With_Code, string Worked_With_Name, string Prod_Detail, string Gift_Code, string Gift_Name, string GQty, string SDP, string vConf, string sess_code, string minutes, string seconds, string product_detail_code, string gift_detail_code, string Add_Prod_Detail, string Add_Prod_Code, string Add_Gift_Detail, string Add_Gift_Code, string Trans_Detail_Name)
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
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Lst_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_Lst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                         " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                         " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + SDP + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
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
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB ," +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "',  '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "', '" + Trans_Detail_Name + "' )";


                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (vConf == "1")
                    {
                        strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
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
        public DataSet ffincentive(string div_code, string sf_code, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select cast(convert(varchar,Order_Date,101)as datetime) Order_Date,ms.Stockist_Code,ms.Stockist_Name,ms.Territory from trans_order_head h " +
                     " inner join trans_order_details d on d.Trans_Sl_No = h.Trans_Sl_No inner join mas_product_detail p on d.Product_Code = p.Product_Detail_Code " +
                     " inner join mas_stockist ms on ms.Stockist_Code = h.Stockist_Code " +
                     " where p.Division_Code ='" + div_code + "' and h.Sf_Code = '" + sf_code + "' and Order_Date between '" + fdate + "' and '" + tdate + "' " +
                     " group by cast(convert(varchar,Order_Date,101)as datetime),ms.Stockist_Code,ms.Stockist_Name,ms.Territory " +
                     " order by cast(convert(varchar,Order_Date,101)as datetime)";
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
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_Unlst_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_Unlst_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, Session, Session_Code,Time,Minutes," +
                         " Seconds,Worked_with_Code, Worked_with_Name, Product_Detail,Product_Code, Additional_Prod_Code,Additional_Prod_Dtls,Gift_Code,Gift_Name,Gift_Qty,Additional_Gift_Code,Additional_Gift_Dtl, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + DCR_Session + "','" + sess_code + "', '" + DCR_Time + "', '" + minutes + "', '" + seconds + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "', " +
                         " '" + Prod_Detail + "', '" + product_detail_code + "','" + Add_Prod_Code + "', '" + Add_Prod_Detail + "','" + Gift_Code + "', '" + Gift_Name + "','" + GQty + "', '" + Add_Gift_Code + "','" + Add_Gift_Detail + "','" + SDP + "',  '" + Division_Code + "',  '" + Trans_Detail_Name + "' )";


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
                    strQry = "SELECT ISNULL(MAX(cast(Max_Sl_No_Detail as bigint)),0)+1 FROM DCR_MaxSlNo ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(cast(Trans_Detail_SlNo as bigint)),0)+1 FROM DCRDetail_CSH_Temp ";
                    Trans_Detail_SlNo = db.Exec_Scalar(strQry);
                }

                strQry = "insert into DCRDetail_CSH_Temp (Trans_SlNo, Trans_Detail_SlNo, Sf_Code, Trans_Detail_Info_Type, Trans_Detail_Info_Code, POB, " +
                         " Worked_with_Code, Worked_with_Name, SDP, Division_Code,Trans_Detail_Name) " +
                         " VALUES('" + Trans_SlNo + "', '" + Trans_Detail_SlNo + "', '" + SF_Code + "', '" + Trans_Detail_Info_Type + "', '" + Trans_Detail_Info_Code + "', " +
                         " '" + POB_Value + "', '" + Worked_With_Code + "', '" + Worked_With_Name + "',  '" + SDP + "', '" + Division_Code + "' ,'" + Trans_Detail_Name + "')";


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

        public DataSet getStockiest(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,ltrim(Stockist_Name) " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
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

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
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

        public DataSet getStockiest(string sf_code, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " select -1 Stockist_Code, '---Select---' Stockist_Name UNION select Stockist_Code,Stockist_Name " +
                     " from Mas_Stockist " +
                     " where Stockist_Active_Flag=0 " +
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
                     " where a.sf_TP_Active_Flag = 0 and  a.Sf_Code = b.sf_code " +
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


                strQry = " SELECT ListedDrCode, ListedDr_Name , 1 as slno " +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                                " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                        " UNION ALL" +
                        " SELECT ListedDrCode, ListedDr_Name , 2 as slno" +
                        " FROM Mas_ListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' AND ListedDr_Active_Flag = 0 " +
                        " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                                " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";
            }
            else if (doc_disp == 2) // SLV No
            {
                strQry = "SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name " +
                            " FROM Mas_ListedDr " +
                            " WHERE Sf_Code =  '" + sfcode + "' " +
                            " AND ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 3) //Speciality
            {
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            "  SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

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
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Cat_ShortName AS ListedDr_Name  " +
                            " FROM Mas_ListedDr a " +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0" +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

            }
            else if (doc_disp == 5) //Class
            {
                strQry = "SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code like '" + terr_code + ',' + "%'  or " +
                        " Territory_Code like '%" + ',' + terr_code + ',' + "%' or Territory_Code like '" + terr_code + "') " +
                            " UNION ALL" +
                            " SELECT a.ListedDrCode AS ListedDrCode, " +
                            " a.ListedDr_Name + ' - ' + a.Doc_Class_ShortName   AS ListedDr_Name  " +
                            " FROM Mas_ListedDr  a" +
                            " WHERE a.Sf_Code =  '" + sfcode + "' " +
                            " AND a.ListedDr_Active_Flag = 0 " +
                            " AND (Territory_Code NOT like '" + terr_code + ',' + "%'  AND " +
                        " Territory_Code NOT like '%" + ',' + terr_code + ',' + "%' AND Territory_Code NOT like '" + terr_code + "') ";

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

        public DataSet getTerrListedDoctor_Mgr(string sfcode, int doc_disp, string DCRDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            if (doc_disp == 1) // DR Name
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name,Territory_Code" +
                             " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                              " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                " AND ListedDr_Active_Flag = 0" +
                                  " union all " +
                                 " select sf_code, ListedDrCode, ListedDr_Name,Territory_Code" +
                                 " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                                  " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                                 " AND ListedDr_Active_Flag = 0";

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
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + SLVNo  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";
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
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Spec_ShortName  AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";
                //strQry = "SELECT a.ListedDrCode,a.ListedDr_Name + ' - ' + a.Doc_Spec_ShortName  AS ListedDr_Name, c.Territory_Code  from Mas_ListedDr a," +
                //      " DCR_MGR_WorkAreaDtls c WHERE a.Sf_Code = c.Sf_Code " +
                //      " and (a.Territory_Code like cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like '%'+','+ cast(c.Territory_Code as varchar) +','+'%' or a.Territory_Code like cast(c.Territory_Code as varchar))" +
                //      " and c.MGR_Code = '" + sfcode + "' and  c.DCR_Date = '" + DCRDate + "' " +
                //      " AND ListedDr_Active_Flag = 0 " +
                //      " order by Territory_Code";
            }
            else if (doc_disp == 4) // Category
            {
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Cat_ShortName AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";

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
                strQry = " select sf_code, ListedDrCode,ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                          " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                           " and Territory_Code in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                             " AND ListedDr_Active_Flag = 0" +
                               " union all " +
                              " select sf_code, ListedDrCode, ListedDr_Name + ' - ' + Doc_Class_ShortName   AS ListedDr_Name,Territory_Code" +
                              " from Mas_ListedDr WHERE Sf_Code in (select distinct sf_code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')" +
                               " and Territory_Code not  in  (select Territory_Code from DCR_MGR_WorkAreaDtls where MGR_Code = '" + sfcode + "' and  DCR_Date = '" + DCRDate + "')  " +
                              " AND ListedDr_Active_Flag = 0";

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

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
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
            strQry = "select  convert(varchar,Activity_Date,103) Activity_Date,Trans_SlNo,worktype_name_b, a.Sf_Code " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b, Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_B and a.sf_code = c.sf_code and c.sf_type = 1 " +
                     " and a.Sf_Code='" + SF_Code + "' and MONTH(Activity_Date) = '" + sMonth + "' and YEAR(Activity_Date) = '" + sYear + "' " +
                     " Union " +
                     " Select  convert(varchar,Activity_Date,103) Activity_Date,Trans_SlNo,Worktype_Name_m, a.Sf_Code " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b , Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_M and a.sf_code = c.sf_code and c.sf_type = 2 " +
                     " and a.Sf_Code='" + SF_Code + "' and MONTH(Activity_Date) = '" + sMonth + "' and YEAR(Activity_Date) = '" + sYear + "' ";

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

                strQry = "SELECT S_No FROM DCR_MGR_WorkAreaDtls WHERE MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' and Territory_Code ='" + WorkArea + "'";
                S_No = db.Exec_Scalar(strQry);

                if (S_No > 0)
                {
                    strQry = "delete from DCR_MGR_WorkAreaDtls where MGR_Code = '" + MGR_Code + "' and  DCR_Date ='" + DCRDate + "' and Sf_Code = '" + Sf_Code + "' and Territory_Code ='" + WorkArea + "'";

                    iReturn = db.ExecQry(strQry);

                }
                strQry = "SELECT isnull(max(S_No)+1,'1') S_No from DCR_MGR_WorkAreaDtls ";
                int Sl_No = db.Exec_Scalar(strQry);
                strQry = "insert into DCR_MGR_WorkAreaDtls (Sl_No,MGR_Code,Sf_Code, DCR_Date, Territory_Code,Color_Code,Work_Type) " +
                            " VALUES('" + Sl_No + "','" + MGR_Code + "', '" + Sf_Code + "', '" + DCRDate + "', '" + WorkArea + "','" + Color_Code + "','" + Work_Type + "' )";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

      public DataSet TODAY_PRI_ORDER_VIEW1(string div_code,  string date )
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec TODAY_PRI_ORDER_VIEW1 '" + div_code + "','" + date + "'";
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

        public DataSet TODAY_PRIOrder_CATEGORY_VIEW(string div_code,  string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec TODAY_PRIOrder_CATEGORY_VIEW '" + div_code + "','" + date + "'";
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
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
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
                    strQry = "update DCR_MaxSlNo set Max_Sl_No_Detail = '" + Trans_Detail_SlNo + "' ";
                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet get_DCR_Pending_Approval(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,a.sf_Designation_Short_Name as Designation_Short_Name, " +
                  " MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                  " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                  " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                  " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                  " from Mas_Salesforce a, DCRMain_Temp b,Mas_Salesforce_AM d    " +
                  " where a.sf_code = b.sf_code " +
                  " and d.DCR_AM = '" + sf_code + "' and   b.Sf_Code=d.Sf_Code and b.confirmed=1" +
                  " and b.Division_code in('" + div_code + "')" +
                  " and (a.sf_type = 1 or a.sf_type = 2) and b.FieldWork_Indicator != 'L' ";
            //" select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name, " +
            //      " MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
            //      " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
            //      " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
            //      " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
            //      " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c ,Mas_Salesforce_AM d, Mas_WorkType_Mgr wor     " +
            //      " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code  " +
            //      " and d.DCR_AM = '" + sf_code + "' and   b.Sf_Code=d.Sf_Code and b.confirmed=1" +
            //      " and b.Division_code in('" + div_code + "')" +
            //      " and b.work_type = wor.worktype_code_m and a.sf_type = 2 and wor.FieldWork_Indicator != 'L' ";
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
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1 " +
                     " Union " +
                     "select WType_SName " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 ";
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

        public DataSet get_dcr_ff_details(int imon, int iyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select distinct a.Sf_Code, b.Sf_Name, " +
                     " (select sf_name from mas_salesforce where sf_code = b.Reporting_To_SF) approved_by " +
                     " from DCRMain_Temp a, Mas_Salesforce b " +
                     " where a.Sf_Code = b.Sf_Code and a.division_code='" + div_code + "' and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;

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
            int iReturnmain = -1;
            int iReturntemp = -1;
            int iReturn = -1;
            int slno = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into DCRMain_Trans select * from DCRMain_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                iReturnmain = db.ExecQry(strQry);

                if (iReturnmain > 0)
                {
                    strQry = "Insert into DCRDetail_Lst_Trans select * from DCRDetail_Lst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);

                    strQry = "Insert into DCRDetail_UnLst_Trans select * from DCRDetail_UnLst_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);

                    strQry = "Insert into DCRDetail_CSH_Trans select * from DCRDetail_CSH_Temp where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);
                }
                if (iReturntemp >= 0)
                {
                    strQry = "DELETE from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);

                    strQry = "DELETE from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);

                    strQry = "DELETE from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);


                    strQry = "DELETE from DCRMain_Temp where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRMain_Trans where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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

                strQry = " select COUNT(a.Sale_Code) from Trans_Secondary_Sales_Details a inner join Mas_Stockist b on b.Stockist_Code=a.Stockist_Code " +
                            " where MONTH(a.date)= '" + imonth + "' and YEAR(a.date)= '" + iyear + "'  and b.Division_Code = '" + div_code + "'";
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
            //strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
            //         " from DCRMain_Trans a" +
            //         " where a.Sf_Code = '" + sf_code + "' " +
            //         " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
            strQry = "select distinct convert(varchar,a.date,103) Activity_Date,convert(varchar,a.date,103)  Submission_Date,a.Stockist_Code " +
                     "from Trans_Secondary_Sales_Details a " +
                     "where a.SfCode = '" + sf_code + "' " +
                     "and MONTH(a.date) =" + imon + "  and YEAR(a.date) = " + iyear + " ";


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

            strQry = " select distinct" +
                     " a.sf_code,b.stockist_code " +
                     " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e  where a.Sf_Code = '" + sf_code + "' " +
                     " and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                     " and  c.Doc_Special_Code = e.Doc_Special_Code  and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' " +
                     " union all " +
                     " select distinct" +
                     " a.sf_code,b.stockist_code " +
                     " from vwActivity_MSL_Details a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e  where a.Worked_with_Code = '" + sf_code + "' " +
                     " and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                     " and  c.Doc_Special_Code = e.Doc_Special_Code  and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'";



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

        public DataSet get_dcr_details_mr(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct" +
                     " a.sf_code,b.stockist_code " +
                     " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e  where a.Sf_Code = '" + sf_code + "' " +
                     " and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                     " and  c.Doc_Special_Code = e.Doc_Special_Code  and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' ";

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

        public DataSet get_dcr_details1(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                   "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                   "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                    "  FOR XML PATH(''), TYPE " +
                    "  ).value('.', 'NVARCHAR(MAX)') " +
                    " ,1,1,'') ) as che_POB_Name ," +
                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.POB_Value,b.net_weight_value,b.Activity_Remarks,b.stockist_code,b.stockist_name,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                    " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,e.Doc_Special_Name,f.Doc_ClsName, a.Plan_No, a.Plan_Name ,b.Session,b.Time,CAST(CONVERT(VARCHAR, b.ModTime, 103) AS DATE) as ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                    " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e,Mas_Doc_Class f " +
                    " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                    " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                    " and  c.Doc_Special_Code = e.Doc_Special_Code and  c.Doc_ClsCode=f.Doc_ClsCode " +
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " Union all " +
                    " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                    "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                   "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                    "  FOR XML PATH(''), TYPE " +
                    "  ).value('.', 'NVARCHAR(MAX)') " +
                    " ,1,1,'') ) as che_POB_Name ," +
                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.ListedDr_Name ,b.POB_Value,b.net_weight_value,b.Activity_Remarks,b.stockist_code,b.stockist_name,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                    " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,e.Doc_Special_Name,f.Doc_ClsName, a.Plan_No, a.Plan_Name ,b.Session,b.Time,CAST(CONVERT(VARCHAR, b.ModTime, 103) AS DATE) as ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                    " from DCRMain_Temp a, DCRDetail_Lst_Temp b, Mas_ListedDr c,Mas_Doctor_Speciality e,Mas_Doc_Class f " +
                    " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                    " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                    " and c.Doc_Special_Code = e.Doc_Special_Code and  c.Doc_ClsCode=f.Doc_ClsCode " +
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'  order by a.Trans_SlNo ";



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
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union " +
                      " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name ,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
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
            //strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
            //         " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         " from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //        "  FOR XML PATH(''), TYPE " +
            //        "  ).value('.', 'NVARCHAR(MAX)') " +
            //        " ,1,1,'') ) as che_POB_Name ,"+
            //         " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name,c.Stockist_Address,b.Activity_Remarks,b.Worked_with_Code,b.Worked_with_Name, " +
            //         " a.Plan_No, a.Plan_Name,b.POB_Value,vstTime,ModTime,Rx,GeoAddrs " +
            //         " from DCRMain_Trans a, vwActivity_CSH_Detail b, Mas_Stockist c " +
            //         " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
            //         " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
            //    // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
            //        " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
            //        " Union " +
            //        " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, "+
            //        "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //        " from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //        "  FOR XML PATH(''), TYPE " +
            //        "  ).value('.', 'NVARCHAR(MAX)') " +
            //        " ,1,1,'') ) as che_POB_Name, " +
            //        " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name,c.Stockist_Address ,b.Activity_Remarks,b.Worked_with_Code,b.Worked_with_Name, " +
            //        " a.Plan_No, a.Plan_Name,b.POB_Value,vstTime,ModTime,Rx,GeoAddrs " +
            //        " from DCRMain_Trans a, vwActivity_CSH_Detail b, Mas_Stockist c " +
            //        " where b.Worked_with_Code = '"+sf_code+"' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type=3 and  b.Trans_Detail_Info_Code=c.stockist_code "+ 
            //        " and "+
            //        " convert(varchar,b.vstTime,103)='"+Activity_date+"'";

            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                      " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                      " from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                   " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name,c.Stockist_Address,b.Activity_Remarks ,b.Worked_with_Code,b.Worked_with_Name, " +
                   " a.Plan_No, a.Plan_Name,isnull(b.POB_Value,0)POB_Value,vstTime,ModTime,Rx,GeoAddrs " +
                   " from DCRMain_Trans a, vwActivity_CSH_Detail b, Mas_Stockist c " +
                   " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                   " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                  " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                  " Union " +
                  " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                  " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                      " from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                  " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name,c.Stockist_Address,b.Activity_Remarks,b.Worked_with_Code,b.Worked_with_Name, " +
                  " a.Plan_No, a.Plan_Name,isnull(b.POB_Value,0)POB_Value,vstTime,ModTime,Rx,GeoAddrs " +
                  " from DCRMain_Trans a, vwActivity_CSH_Detail b, Mas_Stockist c " +
                  " where b.Worked_with_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type=3 and  b.Trans_Detail_Info_Code=c.stockist_code " +
                  " and " +
                  " convert(varchar,b.vstTime,103)='" + Activity_date + "'";

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
        public DataSet get_dcr_stk_detailsx(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " Union " +
                    " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, a.Plan_Name,b.POB,vstTime,ModTime,Rx,GeoAddrs " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Temp b, Mas_Stockist c " +
                     " where b.Worked_with_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type=3 and  b.Trans_Detail_Info_Code=c.stockist_code " +
                     " and " +
                     " convert(varchar,b.vstTime,103)='" + Activity_date + "'";

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
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
                           " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                           " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                           " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                           " Union " +
                           " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                           " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                           " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, a.Plan_Name ,b.Session,b.Time,ModTime,Rx,GeoAddrs,b.SDP_Name " +
                           " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                           " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo " +
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
            //strQry = " select a.remarks , wor.Worktype_Name_B  from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor " +
            //         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.worktype_code_b ";
            if (sfcode.Contains("MR"))
            {
                strQry = " select a.remarks , wor.Worktype_Name_B  from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor " +
                         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.worktype_code_b ";
            }
            else
            {
                strQry = " select a.remarks , wor.Worktype_Name_M  from DCRMain_Temp a ,Mas_WorkType_Mgr wor " +
                         " WHERE a.sf_code= '" + sfcode + "' and a.trans_slno = '" + trans_slno + "'  and a.work_type = wor.WorkType_Code_M ";
            }

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

        public DataSet DCR_Total_Doc_Visit_Report(string sf_code, string div_code, DateTime dtcurrent, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Total_Doc_Visit_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "','" + iMonth + "','" + iYear + "' ";

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

            strQry = " select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Temp where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' " +
                      " union all " +
                      "select convert(char(10),Activity_Date,105) Submission_Date,Remarks from DCRMain_Trans where Sf_Code= '" + sf_code + "' and MONTH(Activity_Date)='" + Month + "' order by Submission_Date ";

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
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=2 and Division_Code='" + div_code + "'";

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
                     "where Sf_Code = '" + sf_code + "' and Trans_Detail_Info_Type=3 and Division_Code='" + div_code + "' ";

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
                     " month(UnListedDr_Created_Date)<='" + iMonth + "' and year(UnListedDr_Created_Date)='" + iYear + "' " +
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
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_B , " +
                     " case Confirmed when '1' then  '' when '2' then 'DisApproved' End as Temp,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=1 or a.confirmed=2) and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.worktype_code_b order by a.Activity_Date ";

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
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";


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
                     " and a.work_type = wor.worktype_code_b order by Activity_Date";

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
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') " +
                           " Union " +
                           " select b.Worktype_Name_M from DCRMain_Temp a,Mas_WorkType_Mgr b " +
                           " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') ";

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

        public DataSet get_dcr_DCRPendingdate(string sf_code, string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "SELECT  convert(varchar,Activity_Date,103) Activity_Date,convert(varchar,Submission_Date,103)  Submission_Date,dcr" +
                   "  from( " +
                   " select  convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,Trans_SlNo as dcr,1 AS priority " +
                   " from DCRMain_Trans a" +
                   " where a.Sf_Code = '" + sf_code + "' " +
                   " and  cast(convert(varchar,a.Activity_Date,101) as datetime) between '" + Fdate + "' and '" + Tdate + "'" +
                   " union all" +
                   " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,Trans_SlNo as dcr,2 AS priority " +
                   " from DCRMain_Temp a" +
                   " where a.Sf_Code = '" + sf_code + "' " +
                  " and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "')u order by convert(datetime, Activity_Date, 103) ASC,priority ASC";

            //strQry = "SELECT  convert(varchar,Activity_Date,103) Activity_Date,convert(varchar,Submission_Date,103)  Submission_Date" +
            //        "  from( " +
            //        " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,1 AS priority " +
            //        " from DCRMain_Trans a" +
            //        " where a.Sf_Code = '" + sf_code + "' " +
            //        " and  a.Activity_Date between '" + Fdate + "' and '" + Tdate + "'" +
            //        " union all" +
            //        " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,2 AS priority " +
            //        " from DCRMain_Temp a" +
            //        " where a.Sf_Code = '" + sf_code + "' " +
            //       " and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "')u order by convert(datetime, Activity_Date, 103) ASC,priority ASC";
            //strQry = "EXEC getDCRVwSFHry  '" + sf_code + "', '" + div_code + "','" + Fdate + "', '" + Tdate + "' ";

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

        public DataSet get_DCRHoliday_Name_MR_chk(string sf_code, string Activity_date, string iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_B from DCRMain_Trans a,Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' and  Trans_SlNo='" + iType + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') " +
                           " Union " +
                           " select b.Worktype_Name_B from DCRMain_Temp a,Mas_WorkType_BaseLevel b " +
                           " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' and  Trans_SlNo='" + iType + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') ";

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

        public DataSet get_DCRHoliday_Name_chk(string sf_code, string Activity_date, string iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_M from DCRMain_Trans a,Mas_WorkType_Mgr b " +
                           " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' and   Trans_SlNo='" + iType + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') " +
                           " Union " +
                           " select b.Worktype_Name_M from DCRMain_Temp a,Mas_WorkType_Mgr b " +
                           " where a.Work_Type= b.WorkType_Code_M and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' and   Trans_SlNo='" + iType + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') ";

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

        public DataSet get_dcr_DCRPendingdate1(string sf_code, string div_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;



            strQry = "SELECT  convert(varchar,Activity_Date,103) Activity_Date,convert(varchar,Submission_Date,103)  Submission_Date,dcr" +
                    "  from( " +
                    " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,Trans_SlNo as dcr,1 AS priority " +
                    " from DCRMain_Trans a" +
                    " where a.Sf_Code = '" + sf_code + "' " +
                    " and  cast(convert(varchar,a.Activity_Date,101) as datetime) between '" + Fdate + "' and '" + Tdate + "'" +
                    " union all" +
                    " select distinct convert(varchar,a.ModTime,103) Activity_Date,convert(varchar,a.ModTime,103)  Submission_Date,Trans_Detail_Slno as dcr,2 AS priority " +
                    " from vwActivity_CSH_Detail a" +
                    " where ( a.sf_code = '" + sf_code + "' or charindex('&&'+'" + sf_code + "'+'&&','&&'+a.Worked_with_Code+'&&')>0  )" +
                    " and cast(convert(varchar, a.ModTime, 101) as datetime) between '" + Fdate + "' and '" + Tdate + "')u order by convert(datetime, Activity_Date, 103) ASC,priority ASC";
            //strQry = "EXEC getDCRVwSFHry  '" + sf_code + "', '" + div_code + "','" + Fdate + "', '" + Tdate + "' ";

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
                     " where Sf_Code = '" + sf_code + "' " +
                     " UNION " +
                     " select Trans_SlNo  From  DCRMain_Temp a, Mas_Salesforce b" +
                     " where b.sf_code = a.sf_code and b.Reporting_To_SF  = '" + sf_code + "'";

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
                strQry = "INSERT INTO Option_DCR_Edit_Dates VALUES ( '" + Sl_No + "','" + sf_code + "' ," + Month + "," + Year + ", " + Trans_Slno + ", '" + Edit_Date + "',getdate(),getdate(),0) ";
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

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into DCRMain_Temp select * from DCRMain_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                iReturnmain = db.ExecQry(strQry);

                if (iReturnmain > 0)
                {
                    strQry = "Insert into DCRDetail_Lst_Temp select * from DCRDetail_Lst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);

                    strQry = "Insert into DCRDetail_UnLst_Temp select * from DCRDetail_UnLst_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);

                    strQry = "Insert into DCRDetail_CSH_Temp select * from DCRDetail_CSH_Trans where Sf_Code ='" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + " ";

                    iReturntemp = db.ExecQry(strQry);
                }
                if (iReturntemp >= 0)
                {
                    strQry = "DELETE from DCRDetail_Lst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRDetail_Lst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);

                    strQry = "DELETE from DCRDetail_UnLst_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_UnLst_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);

                    strQry = "DELETE from DCRDetail_CSH_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                            " (select * from DCRDetail_CSH_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);


                    strQry = "DELETE from DCRMain_Trans where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " and exists " +
                             " (select * from DCRMain_Temp where Sf_Code = '" + SF_Code + "' and Trans_SlNo = " + Trans_SlNo + ")";
                    iReturn = db.ExecQry(strQry);


                    strQry = "update DCRMain_Temp set Confirmed = 3  where Sf_Code = '" + SF_Code + "'  and Trans_SlNo = " + Trans_SlNo + " "; // 3 - Edit 
                    ireturnupdate = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
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


        public int RecordAdd_DelayDtls(string sf_code, int Month, int Year, string ddate, DateTime ldate, string div_code) //Modified by Sri - 07-Aug
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

        public DataSet Get_ChkWorkTypeName(string StrChkWorkType, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Worktype_Name_B in(" + StrChkWorkType + ") and division_code='" + div_Code + "'";

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
            strQry = " select convert(varchar,d.Delayed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( Delayed )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'D' Mode from DCR_Delay_Dtls d, Mas_Salesforce s " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Delayed_Date)='" + sMonth + "' " +
                    " and Year(Delayed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and  Delayed_Flag =0" +
                    " union " +
                    " select convert(varchar,d.Dcr_Missed_Date,103) as Delayed_Date, s.Sf_HQ,s.Sf_Name +' - ' +' ( APP Missing Dates )' as Sf_Name,s.Sf_Code,s.sf_Designation_Short_Name," +
                    " 'A' Mode from DCR_MissedDates d, Mas_Salesforce s " +
                    " where d.Sf_Code='" + SF_Code + "' and MONTH(Dcr_Missed_Date)='" + sMonth + "' " +
                    " and Year(Dcr_Missed_Date)='" + sYear + "' and d.Sf_Code = s.Sf_Code and status=0 ";
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
        public DataSet get_Release_Sf(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_Delay_Dtls d where d.division_code = '" + div_code + "' and d.Delayed_Flag=0" +
                    " union " +
                    " select '' as sf_code, '--Select--' as Sf_Name " +
                    " union " +
                    " select d.sf_code,(select Sf_Name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name from Mas_Salesforce s where d.Sf_Code = s.Sf_Code) as sf_name " +
                    "  from DCR_MissedDates d where d.division_code = '" + div_code + "' and d.status=0";
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
        public int Update_Delayed(string sf_code, DateTime ddate, string Mode)
        {
            int iReturn = -1;
            string delaydate = ddate.Year + "-" + ddate.Month.ToString() + "-" + ddate.Day;
            try
            {

                DB_EReporting db = new DB_EReporting();

                if (Mode == "D")
                {
                    strQry = "UPDATE DCR_Delay_Dtls " +
                                " SET Delayed_Flag = 1 , " +
                                " Delay_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                                " WHERE Sf_Code = '" + sf_code + "' and Delayed_Flag =0 and Delayed_Date='" + delaydate + "'";
                }
                else
                {
                    strQry = "UPDATE DCR_MissedDates " +
                               " SET [status] = 1 , " +
                               " Missed_Release_Date = getdate(), Released_by_Whom = 'admin' " +
                               " WHERE Sf_Code = '" + sf_code + "' and status =0 and Dcr_Missed_Date='" + delaydate + "'";
                }

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

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1  " +
                            " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo =" + trans_slno + "";

                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    if (reentry == false)
                    {

                        // Modified on 6th June - To update Last Dcr date in Sales force
                        DateTime dtDCR;
                        int iReturnsf = -1;
                        string Last_Dcr_Date = string.Empty;
                        dtDCR = Convert.ToDateTime(dcrdate);
                        dtDCR = dtDCR.AddDays(1);
                        Last_Dcr_Date = dtDCR.ToString("MM/dd/yyyy");

                        strQry = " Update Mas_Salesforce  set Last_Dcr_Date = '" + Last_Dcr_Date + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Sf_Code= '" + sf_code + "' ";

                        iReturnsf = db.ExecQry(strQry);
                    }
                    //if (trans_slno > 1)
                    //{
                    //    strQry = "update DCR_MaxSlNo set Max_Sl_No_Main = '" + trans_slno + "' where Division_Code = '" + div_code + "' ";
                    //    iReturnmax = db.ExecQry(strQry);
                    //}
                    if (isdelayrel == true)
                    {
                        int iReturndel = -1;
                        strQry = " Update DCR_Delay_Dtls  set Delayed_Flag =  2 where Sf_Code= '" + sf_code + "' and Delayed_Date = '" + Activity_date + "' and Division_Code = '" + div_code + "' ";

                        iReturndel = db.ExecQry(strQry);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

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
        public DataSet get_delayed_Status(string sf_code, string smonth, string syear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select distinct CONVERT(varchar(10),Delay_Created_Date,103) from DCR_Delay_Dtls " +
            //         " where Month(Delay_Created_Date)=Month(getdate()) and  Sf_Code='" + sf_code + "'";
            strQry = " select distinct CONVERT(varchar(10),delayed_date,103) from DCR_Delay_Dtls " +
                    " where Sf_Code= '" + sf_code + "' and MONTH(delayed_date) = '" + smonth + "' and YEAR(delayed_date) =  '" + syear + "' and Delayed_Flag = 0";
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

            if (sf_code.Contains("MR"))
            {
                strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name,W.Worktype_Name_B,b.ReasonforRejection, " +
                            "  CONVERT(char(10),Activity_Date,103) as DCR_Activity_Date,MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                            " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                            " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                            " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                            " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c,Mas_WorkType_BaseLevel W      " +
                            " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code and b.Work_Type = W.WorkType_Code_B  " +
                            " and a.SF_code  = '" + sf_code + "' and b.confirmed=2";
            }
            else
            {
                strQry = " select distinct a.sf_code, a.sf_name,a.Sf_HQ,c.Designation_Short_Name,Worktype_Name_M as Worktype_Name_B ,b.ReasonforRejection, " +
                            "  CONVERT(char(10),Activity_Date,103) as DCR_Activity_Date,MONTH(Activity_Date) as Mon,YEAR(Activity_Date) as Year, " +
                            " a.sf_code + '-' + cast(MONTH(Activity_Date)  as varchar)+ '-' + cast(YEAR(Activity_Date) as varchar) as key_field, " +
                            " 'Click Here to Approve ' + convert(varchar,MONTH(Activity_Date)) + '-' +convert(varchar, YEAR(Activity_Date)) as Activity_date, " +
                            " 'Click here to Approve '+ convert(char(3),Activity_Date,107) + ' '+ convert(char(4),Activity_Date,111) as Month " +
                            " from Mas_Salesforce a, DCRMain_Temp b, Mas_SF_Designation c,Mas_WorkType_Mgr W      " +
                            " where a.sf_code = b.sf_code and  a.Designation_Code=c.Designation_Code and b.Work_Type = W.WorkType_Code_M  " +
                            " and a.SF_code  = '" + sf_code + "' and b.confirmed=2";
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
                        strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR where Division_Code = '" + div_code + "' ";
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
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups where Division_Code = '" + div_code + "' ";
                }
                else if (sf_type == "2")
                {
                    strQry = "SELECT LastUpdt_DCRStp FROM Admin_Setups_MGR where Division_Code = '" + div_code + "' ";
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

                    strQry = "delete from DCRmain_Temp where Trans_SlNo = '" + Trans_SlNo + "' " +
                     " and Sf_Code = '" + SF_Code + "' and Division_Code = '" + Division_Code + "' ";
                    iReturn = db.ExecQry(strQry);
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

            strQry = " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code = '" + Terr_Code + "' " +
                     " UNION ALL" +
                     " select Chemists_Code, ltrim(Chemists_Name) Chemists_Name " +
                     " from Mas_Chemists " +
                     " where Chemists_Active_Flag=0 " +
                     " and Sf_Code = '" + sfcode + "' " +
                     " AND Territory_Code != '" + Terr_Code + "' ";


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

            strQry = " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code = '" + Terr_Code + "' " +
                        " UNION ALL" +
                        " SELECT UnListedDrCode, UnListedDr_Name " +
                        " FROM Mas_UnListedDr " +
                        " WHERE Sf_Code =  '" + sfcode + "' " +
                        " AND UnListedDr_Active_Flag = 0 " +
                        " AND Territory_Code != '" + Terr_Code + "' ";
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

        public DataSet prodreason(string div_code, string sf_code, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec ProductwiseReason '" + sf_code + "','" + div_code + "','" + fdate + "','" + tdate + "' ";

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

        public DataSet getRemarks(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


           // strQry = " select * from mas_product_remarks where Division_Code=" + div_code + " ";

           strQry = "select Replace(Replace(Replace(Remarks_Content,'Order Given','Availability'),'Balance Remian','Availability'),'Other Source','Availability') Remarks_Content,case when charindex(',' + Remarks_Content + ',',',Order Given,Balance Remian,Other Source,')> 0 then - 1 else Remarks_Id end Remarks_Id from mas_product_remarks where Division_Code=" + div_code + " group by Replace(Replace(Replace(Remarks_Content,'Order Given','Availability'),'Balance Remian','Availability'),'Other Source','Availability') ,case when charindex(',' + Remarks_Content + ',',',Order Given,Balance Remian,Other Source,')> 0 then - 1 else Remarks_Id end";// + "and Remarks_Content not in('Order Given', 'Balance Remian', 'Other Source')";
            
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

        public DataSet getRemarksCount(string div_code, string sf_code, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec ProductwiseReasonCount '" + sf_code + "','" + div_code + "','" + fdate + "','" + tdate + "' ";

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

       public DataSet retailerreason(string div_code,string remarksid,string sfcode, string fdate, string tdate, string pcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec RemarkwiseRetailer '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + remarksid + "','" + pcode + "' ";

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

       public DataTable reasonretailerraw(string div_code, string subdivcode, string sfcode, string fdate, string tdate)
       {
           DB_EReporting db_ER = new DB_EReporting();

           DataTable dsAdmin = null;


           strQry = " exec RetailerReasonRawData '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdivcode + "' ";

           try
           {
               dsAdmin = db_ER.Exec_DataTable(strQry);
           }
           catch (Exception ex)
           {
               throw ex;
           }
           return dsAdmin;
       }

        public DataSet Primaryorderclbal(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " exec PRIMARY_ORDER_VIEW_QTY_51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdivcode + "' ";

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

        public DataSet Primaryorderview(string div_code, string sfcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " exec PRIMARY_ORDER_VIEW_51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdivcode + "' ";

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
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_B from Mas_WorkType_BaseLevel Where division_code = '" + div_code + "' ";
            else if (sf_type == "2")
                strQry = "select FieldWork_Indicator, Button_Access, WorkType_Code_M from Mas_WorkType_Mgr Where division_code = '" + div_code + "' ";

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
        //Changes done by saravanan
        public DataSet DCR_get_WorkType(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Division_Code + "' ";

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

        public DataSet LoadMailWorkwith(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " SELECT  'admin' Sf_Code,'admin-Level1' as sf_mail,Name as sf_name,'admin' Designation_Short_Name, 'ZZZ' Reporting_To_SF, '' Sf_HQ,'' sf_type,'-Level1' as sf_color, '' des_color ,'' Designation_Code from Mas_HO_ID_Creation" +
                      " Where (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%' or Division_Code like '" + div_code + "') " +
                      " UNION " +
                    " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color ,b.Designation_Code" +
                    " from Mas_Salesforce  a, Mas_SF_Designation b    " + // AM Level
                            " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                            " UNION" +
                           " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                           " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
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
        public DataTable LoadMailWorkwithDes(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color ,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " + // AM Level
                            " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                            " UNION" +
                           " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                           " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                            " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                            " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                            " UNION " +
                             " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                     " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
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
            string[] TobeDistinct = { "Designation_Code", "Designation_Short_Name" };
            DataTable DesTable = GetDistinctRecords(dsTP, TobeDistinct);
            return DesTable;
        }

        //Following function will return Distinct records for Name, City and State column.
        public static DataTable GetDistinctRecords(DataSet dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.Tables[0].DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }
        public DataSet get_DCRHoliday_Name_MR(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select b.Worktype_Name_B from DCRMain_Trans a,Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') " +
                           " Union " +
                           " select b.Worktype_Name_B from DCRMain_Temp a,Mas_WorkType_BaseLevel b " +
                     " where a.Work_Type= b.WorkType_Code_B and a.Sf_Code='" + sf_code + "' " +
                           " and convert(varchar,a.Activity_Date,103)='" + Activity_date + "' " +
                           " and b.WType_SName in('M','L','H','TR','T','SS','CW','IW','CM','SW','PL','NFW','WO','AW','DS','DH') ";

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
        public DataSet get_DCRView_Approved_MGR_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_M ,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Trans a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.WorkType_Code_M order by Activity_Date";

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

        public DataSet get_DCRView_Pending_Approval_MGR_All(string sf_code, string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,a.Plan_Name,wor.Worktype_Name_M as Worktype_Name_B ," +
                     " case Confirmed when '1' then  'Approval Pending' when '2' then 'DisApproved' End as Temp,'Stockist Work' as Stockist," +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and Month(a.Activity_date) = '" + mon + "' " +
                     " and Year(a.Activity_date) = '" + year + "'  " +
                     " and a.work_type = wor.WorkType_Code_M ";

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

        public DataSet get_DCR_Status_Delay(string SF_Code, string sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select day(Delayed_Date) as Delay_Created_Date  FROM DCR_Delay_Dtls" +
                     " WHERE DAY(Delayed_Date) = '" + sday + "' and MONTH(Delayed_Date) = " + sMonth + " " +
                     " and YEAR(Delayed_Date) = " + sYear + " AND Sf_Code='" + SF_Code + "' and Delayed_Flag <> 0 ";

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

        public DataSet get_WorkType_DCR_Status(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_B, Worktype_Name_B,WType_SName from Mas_WorkType_BaseLevel where Division_Code='" + Div_Code + "' ";

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

        public DataSet get_Report_DCR_Status(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WType_SName,'0' Activity_Date " +
                     " from DCRMain_Trans a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1  " +
                     " Union " +
                     "select WType_SName,'0' Activity_Date " +
                     " from DCRMain_Trans a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 " +
                     " Union " +
                     "select case WType_SName " +
                     " when 'L' then 'LP' END AS WType_SName,DAY(a.Activity_Date)Activity_Date " +
                     " from DCRMain_Temp a, Mas_WorkType_BaseLevel b ,Mas_Salesforce c" +
                     " where a.Work_Type = b.WorkType_Code_B " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 1  " +
                     " Union " +
                     "select case WType_SName " +
                     " when 'L' then 'LP' END AS WType_SName,DAY(a.Activity_Date)Activity_Date " +
                     " from DCRMain_Temp a, Mas_WorkType_Mgr b,Mas_Salesforce c " +
                     " where a.Work_Type = b.WorkType_Code_M " +
                     " and a.Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                     " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear + " " +
                     " and a.sf_code = c.sf_code and c.sf_type = 2 ";

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

        public DataSet DCR_TotalFLDWRKQuery_MGR(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count(Trans_SlNo) from DCRMain_Trans DCR,Mas_WorkType_Mgr B " +
                     " where DCR.Sf_Code = '" + sf_code + "' and month(DCR.Activity_Date)='" + iMonth + "' and DCR.Division_Code='" + div_code + "' " +
                     " and YEAR(DCR.Activity_Date)='" + iYear + "' and DCR.Work_Type =B.WorkType_Code_M and B.FieldWork_Indicator='F'";

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

        public DataSet DCR_CSH_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_CSH_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Stock_Calls_Seen(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Stock_Calls_Seen  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New(string sf_code, string div_code, int iMonth, int iYear, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC DCR_Status_MR  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC DCR_Status_MGR '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
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
        public DataSet get_DCR_Status_Delay_Cnt(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select case D.Delayed_Flag " +
                     " when '0' then 'D' " +
                     " when '2' then 'E' " +
                     " when '1' then 'DR' end as Delayed_Flag,day(D.Delayed_Date) as Delay_Created_Date  FROM DCR_Delay_Dtls D,Mas_Salesforce S WHERE S.Sf_Code=D.Sf_Code " +
                     " and DAY(D.Delayed_Date) = " + sday + " and MONTH(D.Delayed_Date) = " + sMonth + " " +
                     " and YEAR(D.Delayed_Date) = " + sYear + " AND S.Sf_Code='" + SF_Code + "' ";

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

        public DataSet get_All_dcr_Pending_date_Count(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,'1' as Temp,  " +
                     " case a.FieldWork_Indicator " +
                     " when 'L'  THEN 'LP'  " +
                     " else " +
                     " 'DAP' end FieldWork_Indicator " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name,'' as Temp,  " +
                     " case a.FieldWork_Indicator " +
                     " when 'L'  THEN 'LP'  " +
                     " else " +
                     " 'DAP' end FieldWork_Indicator " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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

        public DataSet get_DCR_Status_Delay_DCRView(string SF_Code, string sday, string Fdate, string FdateFdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select case D.Delayed_Flag " +
                     " when '0' then '( Delayed )' " +
                     " when '1' then '( Delay Relased )' " +
                     " when '2' then '( Delay )' end as Delayed_Flag  FROM DCR_Delay_Dtls D,Mas_Salesforce S WHERE S.Sf_Code=D.Sf_Code " +
                     " and DAY(D.Delayed_Date) = " + sday + " and D.Delayed_Date between '" + Fdate + "' and '" + Fdate + "' AND S.Sf_Code='" + SF_Code + "' ";

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

        public DataSet get_DCRView_Approved_All_Dates(string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " select * from (select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
            //         " day(a.Activity_Date) Activity_Date,convert(varchar,a.Activity_Date,103) Activity_Dat,a.Plan_Name,wor.WorkType_Orderly,wor.Worktype_Name_B,'0' Temp,'Stockist Work' as Stockist, (select sum(b.net_weight_value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as net_weight_value,(select sum(b.POB_Value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as pob_value," +
            //         " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //         "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //          "  FOR XML PATH(''), TYPE " +
            //          "  ).value('.', 'NVARCHAR(MAX)') " +
            //          " ,1,1,'') ) as che_POB_Name ," +
            //         " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //         " (select sum(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //         " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //         " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //         " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //         " a.Remarks from DCRMain_Trans a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and cast(convert(varchar,a.Activity_Date,101) as datetime) between '" + Fdate + "' and '" + Tdate + "'  " +
            //         " and a.work_type = wor.worktype_code_b " +
            //         " union " +
            //         " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
            //         " day(a.Activity_Date) Activity_Date,convert(varchar,a.Activity_Date,103) Activity_Dat,a.Plan_Name,wor.WorkType_Orderly,Worktype_Name_B, case Confirmed " +
            //         " when '1' then  '1' " +
            //         " when '2' then '2' when '3' then '3' End as Temp," +
            //         " 'Stockist Work' as Stockist, (select sum(b.net_weight_value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as net_weight_value,(select sum(b.POB_Value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as pob_value," +
            //         " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
            //         " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
            //         " from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
            //         "  FOR XML PATH(''), TYPE " +
            //         "  ).value('.', 'NVARCHAR(MAX)') " +
            //         " ,1,1,'') ) as che_POB_Name ," +
            //         " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
            //         " (select sum(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
            //         " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
            //         " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
            //         " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
            //         " a.Remarks from DCRMain_Temp a ,Mas_WorkType_BaseLevel wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=2 or a.Confirmed=1 or a.Confirmed=3) and cast(convert(varchar,a.Activity_Date,101) as datetime) between '" + Fdate + "' and '" + Tdate + "' " +
            //         " and a.work_type = wor.worktype_code_b " +
            //         " union " +
            //         " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date, day(D.Delayed_Date) as Activity_Date,'' as Activity_Dat," +
            //         " '' as Plan_Name,'' as WorkType_Orderly,'' as Worktype_Name_B,'0' Temp,'' as Stockist,'' as net_weight_value ,'' as pob_value,'' as doc_cnt,'' as che_cnt,'' as che_POB_Name,'' as che_POB,'' as stk_cnt," +
            //         " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_Delay_Dtls D" +
            //         " WHERE  cast(convert(varchar,D.Delayed_Date,101) as datetime) between '" + Fdate + "' and '" + Tdate + "' AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0') )t ORDER BY " +
            //         "  t.Activity_Date,WorkType_Orderly";

            strQry = " select * from (select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,  day(a.Activity_Date) Activity_Date,convert(varchar,a.Activity_Date,103) Activity_Dat, " +
  " a.Plan_Name,wor.Worktype_Name_B,'0' Temp,'Stockist Work' as Stockist, (select sum(b.net_weight_value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as net_weight_value, " +
  " (select sum(b.order_value) from  vwActivity_MSL_Details b where a.Trans_SlNo = b.Trans_SlNo)as pob_value, (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt, " +
  " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) from DCRDetail_Lst_Trans c " +
   " where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo   FOR XML PATH(''), TYPE   ).value('.', 'NVARCHAR(MAX)')  ,1,1,'') ) as che_POB_Name , " +
   " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt, " +
    " (select sum(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB, " +
    " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt, " +
     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt, " +
     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt, a.Remarks from DCRMain_Trans a ,Mas_WorkType_BaseLevel wor " +
       " where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "'   and a.work_type = wor.worktype_code_b 	" +
        " 	)t ORDER BY  convert (date,t.Submission_Date,103)";

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

        public DataSet get_DCRView_Approved_MGR_All_Dates(string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                     " day(a.Activity_Date) Activity_Date,convert(varchar,a.Activity_Date,103) Activity_Dat,a.Plan_Name,wor.Worktype_Name_M,'0' Temp,'Stockist Work' as Stockist,(select sum(b.net_weight_value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as net_weight_value,(select sum(b.POB_Value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as pob_value, " +
                     " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                     "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                      "  FOR XML PATH(''), TYPE " +
                      "  ).value('.', 'NVARCHAR(MAX)') " +
                      " ,1,1,'') ) as che_POB_Name ," +
                     " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                     " (select COUNT(c.POB) from DCRDetail_CSH_Trans c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                     " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Trans d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                     " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Trans e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                     " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Trans f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                     " a.Remarks from DCRMain_Trans a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "'   " +
                     " and a.work_type = wor.WorkType_Code_M " +
                    " union " +
                    " select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date, " +
                    " day(a.Activity_Date) Activity_Date,convert(varchar,a.Activity_Date,103) Activity_Dat,a.Plan_Name,Worktype_Name_M,case Confirmed " +
                     " when '1' then  '1' " +
                     " when '2' then '2' when '3' then '3' End as Temp," +
                    " 'Stockist Work' as Stockist,(select sum(b.net_weight_value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as net_weight_value,(select sum(b.POB_Value) from  DCRDetail_Lst_Trans b where a.Trans_SlNo = b.Trans_SlNo)as pob_value, " +
                    " (select COUNT(b.Trans_Detail_Slno) from DCRDetail_Lst_Temp b where a.Trans_SlNo = b.Trans_SlNo) as doc_cnt," +
                    " (STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                     " from DCRDetail_Lst_Temp c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                     "  FOR XML PATH(''), TYPE " +
                     "  ).value('.', 'NVARCHAR(MAX)') " +
                     " ,1,1,'') ) as che_POB_Name ," +
                    " (select COUNT(c.Trans_Detail_Slno) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_cnt," +
                    " (select COUNT(c.POB) from DCRDetail_CSH_Temp c where c.Trans_Detail_Info_Type = 2 and  a.Trans_SlNo = c.Trans_SlNo) as che_POB," +
                    " (select COUNT(d.Trans_Detail_Slno) from DCRDetail_CSH_Temp d where d.Trans_Detail_Info_Type = 3 and  a.Trans_SlNo = d.Trans_SlNo) as stk_cnt," +
                    " (select COUNT(e.Trans_Detail_Slno) from DCRDetail_CSH_Temp e where e.Trans_Detail_Info_Type = 5 and  a.Trans_SlNo = e.Trans_SlNo) as hos_cnt," +
                    " (select COUNT(f.Trans_Detail_Slno) from DCRDetail_Unlst_Temp f where a.Trans_SlNo = f.Trans_SlNo) as Undoc_cnt," +
                    " a.Remarks from DCRMain_Temp a ,Mas_WorkType_Mgr wor where a.Sf_Code = '" + sf_code + "' and (a.confirmed=2 or a.Confirmed=1 or a.Confirmed=3) and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "' " +
                    " and a.work_type = wor.WorkType_Code_M " +
                    " union " +
                    " select  D.Sf_Code,'' as trans_slno,'' as Submission_Date, day(D.Delayed_Date) as Activity_Date,'' as Activity_Dat," +
                    " '' as Plan_Name,'' as Worktype_Name_B,'1' Temp,'' as Stockist,'' as net_weight_value ,'' as pob_value,'' as doc_cnt,'' as che_POB_Name, '' as che_cnt,'' as che_POB,'' as stk_cnt," +
                    " '' as hos_cnt,'' as Undoc_cnt,'' as Remarks  FROM DCR_Delay_Dtls D" +
                    " WHERE  D.Delayed_Date between '" + Fdate + "' and '" + Tdate + "' AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0') " +
                    " order by Activity_Date";

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


        public DataSet get_dcr_DCRPendingdate_DCRDetail(string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select  convert(varchar,Activity_Date,103) Activity_Date,convert(varchar,Submission_Date,103)  Submission_Date " +
                    "from( select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date," +
                    "1 AS priority  from DCRMain_Trans a where a.Sf_Code = '" + sf_code + "'  and  a.Activity_Date between '" + Fdate + "' and '" + Tdate + "' " +
                    "union all " +
                    "select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date," +
                    "2 AS priority  from DCRMain_Temp a where a.Sf_Code = '" + sf_code + "'   and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "' " +
                    "Union " +
                    "select convert(char(10),D.Delayed_Date,103) as Activity_Date, '' as Submission_Date,3 AS priority FROM DCR_Delay_Dtls D " +
                    "WHERE  D.Delayed_Date between '" + Fdate + "' and '" + Tdate + "'  AND Sf_Code='" + sf_code + "' and (D.Delayed_Flag='1' or D.Delayed_Flag='0') " +
                    ")u " +
                    "order by priority,convert(datetime, Activity_Date, 103) ASC";

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

        public DataSet get_All_dcr_Sf_Code_date_Count(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Sf_Code " +
                     " from DCRMain_Temp a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct Sf_Code " +
                     " from DCRMain_Trans a" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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
   public DataSet dcr_product_details(string sfcode,string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " Exec dcr_product_details '"+ sfcode + "', '"+ date + "'";

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
          public DataSet get_Temp_and_Approved_dcrLstDOC_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "EXEC get_Temp_and_Approved_dcr '" + sf_code + "' ,'"+ Activity_date + "','"+ iType + "'";
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

        public DataSet get_Temp_and_Approved_dcr_che_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name ,b.POB" +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union " +
                     " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Chemists_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name ,b.POB" +
                     " from DCRMain_Temp a, DCRDetail_CSH_Temp b, Mas_Chemists c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=2 and b.Trans_Detail_Info_Code=c.chemists_code  " +
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

        public DataSet get_Temp_and_Approved_unlst_doc_details(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                         " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                         " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name ,b.Session,b.Time " +
                         " from DCRMain_Trans a, DCRDetail_Unlst_Trans b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                         " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                         " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                         " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                         " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                         " union " +
                         " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                         " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.UnListedDr_Name ,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                         " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name, d.Doc_Cat_Name ,e.Doc_Special_Name, a.Plan_No, b.SDP_Name,b.Session,b.Time " +
                         " from DCRMain_Temp a, DCRDetail_Unlst_Temp b, Mas_UnListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                         " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                         " and b.Trans_Detail_Info_Type=4 and b.Trans_Detail_Info_Code=c.UnListedDrCode  " +
                         " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
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

        public DataSet get_Temp_and_Approved_dcr_stk_detailsView(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name,b.POB " +
                     " from DCRMain_Trans a, DCRDetail_CSH_Trans b, Mas_Stockist c " +
                     " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                     " and b.Trans_Detail_Info_Type=3 and b.Trans_Detail_Info_Code=c.stockist_code  " +
                // " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " union " +
                    " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date, " +
                     " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Stockist_Name ,b.Worked_with_Code,b.Worked_with_Name, " +
                     " a.Plan_No, b.SDP_Name,b.POB " +
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

        public DataSet get_dcr_DCRPendingdate_MR(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a,Mas_WorkType_BaseLevel w" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_B and w.WType_SName='FW'" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     "  from DCRMain_Temp a,Mas_WorkType_BaseLevel w" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_B and w.WType_SName='FW' order by Activity_Date ";

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

        public DataSet get_dcr_DCRPendingdate_MGR(string sf_code, int imon, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Trans a,Mas_WorkType_Mgr w" +
                     " where a.Sf_Code = '" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_M and w.WType_SName='FW'" +
                     " union all" +
                     " select distinct convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,a.Plan_Name " +
                     " from DCRMain_Temp a,Mas_WorkType_Mgr w" +
                      " where a.Sf_Code = '" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + " and a.Work_Type=w.WorkType_Code_M and w.WType_SName='FW' order by Activity_Date ";

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
        public DataSet get_Pending_Single_Temp_Date(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct convert(varchar,a.date,103) Activity_Date,convert(varchar,a.date,103)  Submission_Date,a.Stockist_Code " +
                     " from Trans_Secondary_Sales_Details a" +
                     " where a.SfCode = '" + sf_code + "' " +
                     " and convert(varchar,a.date,103)= '" + Activity_date + "'";

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


        // Added by sridevi
        public DataSet New_DCR_Visit_TotalDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Doc_count from DCRMain_Trans a,DCRDetail_Lst_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;

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

        //New_DCR_TotalChemistQuery

        public DataSet New_DCR_TotalChemistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Che_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 2 " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;

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

        //New_DCR_TotalStockistQuery
        public DataSet New_DCR_TotalStockistQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select count(c.trans_detail_info_code) Stk_count from DCRMain_Trans a,DCRDetail_CSH_Trans  c" +
                    " where a.Trans_SlNo = c.Trans_SlNo " +
                    " and a.Sf_Code = c.sf_code " +
                    " and a.Sf_Code='" + sf_code + "' " +
                    " and c.Trans_Detail_Info_Type= 3 " +
                    " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;

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
        //New_DCR_TotalUnlstDocQuery

        public DataSet New_DCR_TotalUnlstDocQuery(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = strQry = "select count(c.trans_detail_info_code) UnDoc_count from DCRMain_Trans a,DCRDetail_UnLst_Trans  c" +
                     " where a.Trans_SlNo = c.Trans_SlNo " +
                     " and a.Sf_Code = c.sf_code " +
                     " and a.Sf_Code='" + sf_code + "' " +
                     " and MONTH(a.Activity_Date) = " + iMonth + " and YEAR(a.Activity_Date) =  " + iYear;


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
        public DataSet get_All_dcr_Sf_Code_date_Count(int imon, int iyear, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select distinct Sf_Code " +
                     " from DCRMain_Temp a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "" +
                     " union " +
                     "select distinct Sf_Code " +
                     " from DCRMain_Trans a" +
                     " where a.division_code = '" + divcode + "' " +
                     " and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear + "";

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
        public DataSet getDCR_Report_MGR_Calendar(string SF_Code, int sday, int sMonth, int sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select WType_SName " +
                                 " from DCRMain_Trans a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select case confirmed  " +
                                 " when '1' then 'FW(NA)' " +
                                 " When '2' then 'FW(R)' End WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
                                 " and Sf_Code='" + SF_Code + "' and DAY(Activity_Date) = " + sday +
                                 " and MONTH(Activity_Date) = " + sMonth + " and b.WType_SName='FW' and YEAR(Activity_Date) =  " + sYear +
                                 " union " +
                                 " select WType_SName " +
                                 " from DCRMain_Temp a, Mas_WorkType_Mgr b " +
                                 " where a.Work_Type = b.WorkType_Code_M " +
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
        public DataSet getLeave_Mr(string sf_code, DateTime leavedate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            string leave_date = leavedate.Month.ToString() + "-" + leavedate.Day + "-" + leavedate.Year;
            strQry = "select Leave_id,Leave_Active_Flag from mas_Leave_Form where sf_code = '" + sf_code + "'  and  '" + leave_date + "' between From_Date and To_Date and Leave_Active_Flag != 1";
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
        public DataSet getHQ_Mgr(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "EXEC [sp_get_Rep_MgrHQ] '" + sf_code + "' ";

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

        public DataSet getMonth_Count(int imonth, int iyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select day(dateadd(day,-1,dateadd(month,1,CAST(cast('" + imonth + "' as varchar)+'/1/'+CAST('" + iyear + "' as varchar) as DATEtime)))) ";

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

        public DataSet getWorking_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' and FieldWork_Indicator != 'w' " +
                      " and FieldWork_Indicator != 'L' and Division_Code = '" + div_code + "' ";

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

        public DataSet getFieldwork_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator = 'F' and Division_Code = '" + div_code + "' ";

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
        public DataSet getCoverage_anlaysis(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " SELECT 'Total Days in Month', day(dateadd(day,-1,dateadd(month,1,CAST(cast('" + iMonth + "' as varchar)+'/1/'+CAST('" + iYear + "' as varchar) as DATEtime)))) union all " +
                   " SELECT 'Working Days (Excl/Holidays & Sundays )',  count(FieldWork_Indicator) from DCRMain_Trans where  " +
                   " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' and FieldWork_Indicator != 'w'  " +
                   " and FieldWork_Indicator != 'L' and Division_Code = '" + div_code + "'" +
            " union all " +
            " select 'Fieldwork Days', " +
            "  count(FieldWork_Indicator) from DCRMain_Trans where  " +
            " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            " and FieldWork_Indicator = 'F' and Division_Code = '" + div_code + "'" +
            "  union all " +
            " select  'Leave', " +
            " count(FieldWork_Indicator) from DCRMain_Trans where  " +
            " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' " +
                  " union all " +
                  " select 'TP Deviation Days', '' union all " +
                  " select 'No of Listed Drs Met', '' union all " +
                  " select 'No of Listed Drs Seen', '' union all " +
                  " select 'Call Average', ''  ";
            //" select Doc_Cat_SName " +
            //" from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag = 0";

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


        public DataSet getLeave_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' ";

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

        public DataSet getCoverage(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT 'Total Days in Month' union all " +
                     " SELECT 'Working Days (Excl/Holidays & Sundays )'  " +
                     " union all " +
                     " select 'Fieldwork Days' union all " +
                     " select  'Leave' union all " +
                     " select 'TP Deviation Days' union all " +
                     " select 'No of Listed Drs Met' union all " +
                     " select 'No of Listed Drs Seen' union all " +
                     " select 'Call Average' union all " +
                     " select Doc_Cat_SName " +
                     " from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag = 0";

            //" SELECT 'Total Days in Month', " +
            //" day(dateadd(day,-1,dateadd(month,1,CAST(cast('12' as varchar)+'/1/'+CAST('2015' as varchar) as DATEtime)))) union all  " +
            //" SELECT 'Working Days (Excl/Holidays & Sundays )', " +
            //" count(FieldWork_Indicator) from DCRMain_Trans where  " +
            //" MONTH(Activity_Date) = '12'  and YEAR(Activity_Date) = '2015' and Sf_Code='mgr0048' and FieldWork_Indicator != 'w'  " +
            //" and FieldWork_Indicator != 'L' and Division_Code = '7'";


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

        public DataSet DCR_Doc_Met_Self(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet DCR_Doc_Seen_Self(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Seen_Self  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet getDaysWorked(string mgr_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
            //         " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
            //          " and FieldWork_Indicator = 'L' and Division_Code = '" + div_code + "' ";

            strQry = "select COUNT(DISTINCT DCR_Date) from DCR_MGR_WorkAreaDtls where MGR_Code='" + mgr_code + "' and " +
                     " MONTH(DCR_Date)='" + iMonth + "'  and YEAR(DCR_Date)='" + iYear + "' and Sf_Code='" + sf_code + "'";

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

        public DataSet getHQCalls(string sf_code, int iMonth, int iYear, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = " select distinct day(DCR_Date) as dcrdate from DCR_MGR_WorkAreaDtls where Sf_Code='" + sf_code + "' " +
                            " and  MONTH(DCR_Date)='" + iMonth + "'  and YEAR(DCR_Date)='" + iYear + "' and MGR_Code='" + mgr_code + "' ";

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

        public DataSet getHQCalls_Doc(string sf_code, int imon, int iyr, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;


            strQry = "select COUNT(DISTINCT ListedDrCode) from DCRDetail_Lst_Trans a, DCRMain_Trans b, Mas_ListedDr c where " +
                     " a.Trans_Detail_Info_Code=c.ListedDrCode and a.Trans_SlNo=b.Trans_SlNo and c.sf_code = '" + sf_code + "' and  a.sf_code='" + mgr_code + "' " +
                     " and  Month(b.Activity_Date) ='" + imon + "' and Year(b.Activity_Date) = '" + iyr + "'  ";

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
        public DataSet getNonFieldwork_Days(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select count(FieldWork_Indicator) from DCRMain_Trans where  " +
                     " MONTH(Activity_Date) = '" + iMonth + "'  and YEAR(Activity_Date) = '" + iYear + "' and Sf_Code='" + sf_code + "' " +
                      " and FieldWork_Indicator = 'N' and Division_Code = '" + div_code + "' ";

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

        public DataSet DCR_UnDoc_Met(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_UnlstDoc_Met  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet Get_workDays(string sf_code, string div_code, int iMonth, int iYear, string smode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_WorkDays  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", '" + smode + "' ";

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
        public DataSet Get_WorkDaysField(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_WorkDaysField  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        //public DataSet DCR_workwithDay(string mgr_code, string div_code, int iMonth, int iYear, string sf_code)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
        //               " CHARINDEX('" + mgr_code + "',t.Worked_with_Code) > 0 and " +
        //               " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and d.sf_code='" + sf_code + "' ";

        //    try
        //    {
        //        dsAdmin = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsAdmin;
        //}
        public DataSet DCR_workwithDay(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWorkDay '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_workwithDocMet(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWork_DocMet '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_workwithDocSeen(string mgr_code, string div_code, string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_JointWork_DocSeen '" + mgr_code + "', '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet DCR_Doc_Met_Team(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Doc_Met_Team  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet DCR_Total_Call_Doc_Visit_Report(string sf_code, string div_code, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Total_Call_Doc_Visit_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "' ";

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

        public DataSet Special_Visit_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_Specg_VisitCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";
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
        public DataSet DCR_workwithDay_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDate_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithCalls_JW(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(distinct t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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

        public DataSet DCR_workwithCalls_SfName(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct d.Trans_SlNo, t.Worked_with_Code,t.sf_code from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";


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
        public DataSet DCR_workwithDay_dist(string div_code, int iMonth, int iYear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct t.Worked_with_Code as Worked_with_Code   from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";

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

        public DataSet DCR_workwithDay_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count(distinct d.Trans_SlNo) from DCRDetail_Lst_Trans t, DCRMain_Trans d where t.Division_Code = '" + div_code + "' and  " +
                       " CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                       " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithDate_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "'";

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

        public DataSet DCR_workwithCalls_JW_MR(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  COUNT(distinct t.Trans_Detail_Info_Code)  from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
                     " t.Division_Code = '" + div_code + "' and   CHARINDEX('" + sf_code + "',t.Worked_with_Code) > 0 and " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + mgr_code + "' ";

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

        public DataSet DCR_VisitDR_Catg_NoofVisit_Not_Equal(string sf_code, string div_code, int iMonth, int iYear, int catg_code, int no_of_visit)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_VisitDR_Catg_NoofVisit_Not_Equal  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + ", " + no_of_visit + " ";

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

        public DataSet Visit_Doc_workedwith(string doc_code, int cmon, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            strQry = " EXEC sp_DCR_Visit_Count_Workedwith '" + doc_code + "', '" + cmon + "', '" + cyear + "' ";

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

        public DataSet get_dcr_details_Maps(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo,a.Activity_Date,b.Trans_Detail_Name,b.GeoAddrs,b.Rx,b.lati,b.long " +
                      " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c, Mas_Doctor_Category d,Mas_Doctor_Speciality e " +
                      " where a.Sf_Code = '" + sf_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                      " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                      " and c.Doc_Cat_Code = d.Doc_Cat_Code and c.Doc_Special_Code = e.Doc_Special_Code " +
                //" and MONTH(a.Activity_Date) = " + imon + " and YEAR(a.Activity_Date) = " + iyear;
                      " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                      " Union all " +
                      " select a.Trans_SlNo,a.Activity_Date,b.Trans_Detail_Name,b.GeoAddrs,b.Rx,b.lati,b.long " +
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

        public DataSet GetTPDayMap_MR(string div_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_Tb_My_Day_MR  '" + div_code + "', '" + sf_code + "','" + Fdate + "', '" + Tdate + "' ";
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

        public DataSet GetTPDayMap_MGR(string div_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC SP_Get_Tb_My_Day_MGR  '" + div_code + "', '" + sf_code + "','" + Fdate + "', '" + Tdate + "' ";

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

        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_withoutdoccnt(string sf_code, string div_code, int iMonth, int iYear, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC DCR_Status_MR_Nodoc  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC DCR_Status_MGR_Nodoc '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";
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

        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_per_withoutdoccnt(string sf_code, string div_code, string fdate, string tdate, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC [DCR_Status_MR_Date_Nodoc]  '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC [DCR_Status_MGR_Date_Nodoc] '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
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


        //Added by Sridevi - to improve the performance of DCR STatus Report
        public DataSet getDCR_Report_Det_New_Date(string sf_code, string div_code, string fdate, string tdate, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (sf_type == "1")
            {

                strQry = "EXEC [DCR_Status_MR_Date]  '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
            }
            else if (sf_type == "2")
            {
                strQry = "EXEC [DCR_Status_MGR_Date] '" + div_code + "', '" + sf_code + "', '" + fdate + "', '" + tdate + "' ";
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
        public DataSet DCR_work_JW(string div_code, int iMonth, int iYear, string sf_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select  distinct day(d.Activity_Date) as Activity_Date from DCRDetail_Lst_Trans t, DCRMain_Trans d where " +
            //         " t.Division_Code = '" + div_code + "' and   CHARINDEX('MR',t.Worked_with_Code) > 0 and " +
            //         " MONTH(d.Activity_Date)=" + iMonth + " and YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "'";
            strQry = " select  distinct day(d.Activity_Date) as Activity_Date, d.sf_code, t.Worked_with_Code from DCRDetail_Lst_Trans t, DCRMain_Trans d where  " +
                     " t.Division_Code = '" + div_code + "' and  " +
                     " t.Worked_with_Code like '%" + mgr_code + "%'   and  " +
                     " MONTH(d.Activity_Date)=" + iMonth + " and  " +
                     " YEAR(d.Activity_Date)=" + iYear + " and t.Trans_SlNo= d.Trans_SlNo and t.Sf_Code='" + sf_code + "' ";

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
        public DataSet get_details_Maps(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "exec getLocationUpdate '" + sf_code + "','" + Activity_date + "','" + iType + "'";
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
        public DataSet GetTPDayPlan_MR(string div_code, string sf_code, string Date, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select top 1 CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, "+  
            //            "(select Worktype_Name_B from Mas_WorkType_BaseLevel WT where tb.wtype=WT.WorkType_Code_B and sf_code like'MR%') as Worktype_Name_B, "+  
            //            "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+ 
            //            "from TbMyDayPlan TB where Division_Code='" + div_code + "' and sf_code='" + sf_code + "' and CONVERT(char(10), Pln_Date,126)='" + Date + "' " + 
            //            "order by Pln_Date ";
            strQry = "exec [getMyDayPlanVwSFHry_sub] '" + sf_code + "','" + div_code + "','" + Date + "','" + sub_div_code + "'";

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
        public DataSet GetTPDayPlan_MGR(string div_code, string sf_code, string date, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            /*strQry = "select top 1  CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, " +    
                        "(select Worktype_Name_M from Mas_WorkType_Mgr WT where tb.wtype=WT.WorkType_Code_M and sf_code like'MGR%') as Worktype_Name_B, "+ 
                        "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+      
                        "from TbMyDayPlan TB where Division_Code='"+div_code+"' and sf_code='"+sf_code+"' and CONVERT(char(10), Pln_Date,126)='"+date+"' "+     
                        "order by Pln_Date";*/
            strQry = "exec [getMyDayPlanVwSFHry_sub] '" + sf_code + "','" + div_code + "','" + date + "','" + sub_div_code + "'";
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
		
        public DataSet GetTPDayPlan_MGR_Sub(string div_code, string sf_code, string Fdate,string Tdate, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [getMyDayPlanVwSFHry_sub1] '" + sf_code + "','" + div_code + "','" + Fdate + "','"+Tdate+"','" + sub_div_code + "'";
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
        public DataSet Getroutedeviation(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            /*strQry = "select top 1  CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, " +    
                        "(select Worktype_Name_M from Mas_WorkType_Mgr WT where tb.wtype=WT.WorkType_Code_M and sf_code like'MGR%') as Worktype_Name_B, "+ 
                        "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+      
                        "from TbMyDayPlan TB where Division_Code='"+div_code+"' and sf_code='"+sf_code+"' and CONVERT(char(10), Pln_Date,126)='"+date+"' "+     
                        "order by Pln_Date";*/
            strQry = "exec [sp_UserList_getMR] '" + div_code + "','" + sf_code + "'";
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
        public DataSet GetCallReport(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [getCallVwSFHry] '" + sf_code + "','" + div_code + "','" + date + "'";
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

        
        public DataSet GetEventCap_MGR(string div_code, string sf_code, string date, string TDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            int divcode = 0;
            if (div_code == "" || div_code == null)
            {
                divcode = 0;
            }
            else
            {
                divcode = Convert.ToInt32(div_code);
            }

            strQry = "exec [getEventCapRepVwSFHry] " + divcode + ", '" + sf_code + "','" + date + "','" + TDate + "'";
            //strQry = "exec [getEventCapRepVwSFHry] '" + sf_code + "','" + div_code + "','" + date + "','" + TDate + "'";
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
        public DataSet view_total_order_view(string div_code, string sf_code, string date, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_VIEW_DISCOUNT_FREEVAL] '" + sf_code + "','" + div_code + "','" + date + "','" + subdiv + "'";
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
        public DataSet view_total_order_viewyy(string div_code, string sf_code, string Fdate, string Tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_VIEW_sample1] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "','" + subdivcode + "'";
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
 public DataSet view_total_primaryorder_view(string div_code, string sf_code, string Fdate, string Tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_PRIORDER_VIEW_sample1] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "','" + subdivcode + "'";
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




        public DataSet view_MGR_order_viewyy(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [getDCRVwSFHry] '" + sf_code + "','" + div_code + "','" + date + "'";
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

        public DataTable view_MGR_order_viewyy_tbl(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [getDCRVwSFHry] '" + sf_code + "','" + div_code + "','" + date + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataTable view_MGR_order_viewyy_tbl2(string div_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [getDCRVwSFHry1] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataTable view_MGR_order_viewyy_tbl4(string div_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [getDCRVwSFHry_pro] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataTable view_MGR_order_viewyy_tbl3(string div_code, string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [getDCRVwSFHry_pri] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet view_total_order_view1(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "SELECT d.Trans_sl_no,d.SF_Code,(select Sf_Name from Mas_Salesforce g where Sf_Code=D.Sf_Code) Sf_Name,d.Stockist_Code,D.Cust_Code,(select TP.Stockist_Name from mas_stockist TP where Stockist_Code=D.Stockist_Code) Stockist_Name," +
                   "d.Order_value,d.net_weight_value,d.Order_date,d.Order_Flag," +
                   "(select t.Territory_Name from Mas_Territory_creation t where t.Territory_code=d.route and t.Territory_Active_Flag=0) as routename," +
                   "(select l.ListedDr_Name from Mas_Listeddr l where l.ListedDrCode=d.Cust_Code and l.ListedDr_Active_Flag=0) as retailername,Div_ID " +
                   "FROM Trans_Order_Head d " +
                   "where CAST(CONVERT(VARCHAR, D.Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and D.STOCKIST_CODE='" + sf_code + "'" +
                   "group by d.Trans_sl_no,d.SF_Code,d.Stockist_Code,D.Cust_Code,d.Order_value,d.net_weight_value,d.Order_date,d.route,d.Order_Flag,Div_ID " +
                   "order by d.Stockist_Code,D.Cust_Code,d.route,d.Order_date,d.Order_value,d.net_weight_value,d.Order_Flag";
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

        public DataSet view_total_order_view2(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select Product_Name,Quantity,net_weight,value from  Trans_Order_Details a inner join Trans_Order_Head b on " +
                     "a.Trans_sl_no =b.Trans_Sl_No where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '2017-08-01' , 101) AS DATETIME) " +
                     "and STOCKIST_CODE='511' and Cust_Code='26145'";
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

 public DataSet categorywise_Order(string div_code, string sf_code, string Tdate, string subdivcode, string Productcategorycode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [CATEGORY_VIEW_Order] '" + sf_code + "','" + div_code + "','" + Tdate + "','" + subdivcode + "','"+ Productcategorycode + "'";
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
        public DataSet view_total_order_view_categorywise(string div_code, string sf_code, string date, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_CATEGORY_VIEW] '" + sf_code + "','" + div_code + "','" + date + "','" + subdivcode + "'";
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
 public DataSet view_total_order_view_categorywise1(string div_code, string sf_code, string Fdate, string Tdate, string subdivcode,string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_CATEGORY_VIEW1] '" + sf_code + "','" + div_code + "','" + Fdate + "','" + Tdate + "','" + subdivcode + "',"+statecode+"";
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

        //GIRI 04-08-2018
        public DataSet view_total_pri_order_view_categorywise(string div_code, string sf_code, string date, string subdivcode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_PRI_ORDER_CATEGORY_VIEW] '" + sf_code + "','" + div_code + "','" + date + "','" + subdivcode + "'";
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

        public DataSet view_total_pri_order_view_categorywise_month(string div_code, string sf_code, string Years, string Months, string subdivcode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [PRI_ORDER_CATEGORY_MONTH] '" + sf_code + "','" + div_code + "','" + Years + "','" + Months + "','" + subdivcode + "'";
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
		
		
        public DataSet Get_Close_stock_MR(string sf_code, string Div, string Fdate, string Tdate, string stcode = "0", string distcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Tsr_Get_Close_stock_MR '" + sf_code + "','" + Div + "','" + Fdate + "','" + Tdate + "','" + stcode + "','" + distcode + "'";


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

        public DataSet tGet_Close_stock_MR(string sf_code, string Div, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC getDCRVwClosebydate '" + sf_code + "', '" + Div + "', '" + fdate + "', '" + tdate + "'";

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

        public DataSet tGet_Super_stock_MR(string sf_code, string Div, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC getSuperStockListbydate '" + sf_code + "', '" + Div + "', '" + fdate + "', '" + tdate + "'";

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

        public DataSet GetEventCap_MGR(string Div, string sf_code, string Fdate, string Tdate, string stcode = "0" ,string mode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            int DivCode = 0;

            if (Div == "" || Div == null)
            {
                DivCode = 0;
            }
            else
            {
                DivCode = Convert.ToInt32(Div);
            }

            if (DivCode == 156)
            { strQry = "EXEC Tsr_getEventCapRepVwSFHry '" + Div + "','" + Fdate + "','" + Tdate + "','" + sf_code + "', '" + stcode + "', '" + mode + "'"; }
            else
            {
                //strQry = "EXEC getEventCapRepVwSFHry '" + Div + "','" + Fdate + "','" + Tdate + "','" + sf_code + "', '" + stcode + "', '" + mode + "'";
                strQry = "EXEC getEventCapRepVwSFHry '" + sf_code + "'," + DivCode + " ,'" + Fdate + "','" + Tdate + "'";
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
		
		
        //Giri May,2017
        public DataSet Get_Close_stock_MR(string sf_code, string Div, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC getDCRVwClose '" + sf_code + "', '" + Div + "', " + iMonth + ", " + iYear + " ";

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
		
		
		
        public DataSet dcr_Gettransnox(string sfcode, string div_code, string activitydate, string retailer_code, string DistCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

           
            strQry = "EXEC SP_Get_ProQty '" + sfcode + "', '" + div_code + "','" + activitydate + "' ,'" + retailer_code + "','" + DistCode + "'";


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
		
		
        public DataSet get_dcr_details_MGR(string sf_code, string Activity_date, int iType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = " select distinct" +
            //         " b.stockist_code " +
            //         " from DCRMain_Trans a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e  where a.Sf_Code = '" + sf_code + "' " +
            //         " and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
            //         " and  c.Doc_Special_Code = e.Doc_Special_Code  and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' ";
            strQry =" select distinct" +
                    " a.sf_code,b.stockist_code " +
                    " from vwActivity_MSL_Details a, DCRDetail_Lst_Trans b, Mas_ListedDr c,Mas_Doctor_Speciality e  where a.Sf_Code = '" + sf_code + "' " +
                    " and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode " +
                    " and  c.Doc_Special_Code = e.Doc_Special_Code  and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "' ";
                   



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
		
		
		        public DataTable view_MGR_order_viewyy_tbl_Brd(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [getDCRVwSFHry_Cate_sku] '" + sf_code + "','" + div_code + "','" + date + "','" + date + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
		
      //public DataSet tGet_Close_stock_MR(string sf_code, string Div, string fdate, string tdate)
      //  {
      //      DB_EReporting db_ER = new DB_EReporting();

      //      DataSet dsAdmin = null;

      //      strQry = "EXEC getDCRVwClosebydate '" + sf_code + "', '" + Div + "', '" + fdate + "', '" + tdate + "'";

      //      try
      //      {
      //          dsAdmin = db_ER.Exec_DataSet(strQry);
      //      }
      //      catch (Exception ex)
      //      {
      //          throw ex;
      //      }
      //      return dsAdmin;
      //  }
		
		
        public DataSet Get_SalerRT(string sf_code, string Div, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC getDCRSalerRT '" + sf_code + "', '" + Div + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet Get_Close_stock_MGR(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Tsr_Get_Close_stock_MR '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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
        public DataSet dcr_product_detail(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.Product_Code,d.Product_Name,sum(d.Quantity)Quantity,sum(d.value)value from Trans_Order_Head h  inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No where Sf_code='" + sfcode + "' and  CONVERT(VARCHAR(25), Order_Date, 126)  like '" + activitydate + "%' group by d.Product_Code,d.Product_Name ";

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
        public DataSet dcr_Gettransno(string sfcode, string div_code, string activitydate, string retailer_code, string DistCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT Product_Code,cast(sum(Quantity) as varchar) Qty FROM Trans_Order_Head H inner join Trans_Order_Details D on H.Trans_Sl_No=D.Trans_Sl_No where Sf_Code='" + sfcode + "' and Cust_Code='" + retailer_code + "' and Stockist_Code='" + DistCode + "' and cast(convert(varchar,Order_Date,101) as datetime)='" + activitydate + " 00:00:00.000' group by Product_Code";

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
        public DataSet dcr_Gettransno_pro(string sfcode, string div_code, string activitydate, string retailer_code, string DistCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT Product_Code,cast(sum(PromoVal) as varchar) Qty FROM Trans_Order_Head H inner join Trans_Order_Details D on H.Trans_Sl_No=D.Trans_Sl_No where Sf_Code='" + sfcode + "' and Cust_Code='" + retailer_code + "' and Stockist_Code='" + DistCode + "' and cast(convert(varchar,Order_Date,101) as datetime)='" + activitydate + " 00:00:00.000' group by Product_Code";

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
        public DataSet dcr_Gettransno_d(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT Product_Code,cast(sum(Quantity) as varchar) Qty FROM Trans_Order_Head H inner join Trans_Order_Details D on H.Trans_Sl_No=D.Trans_Sl_No where Sf_Code='" + sfcode + "' and cast(convert(varchar,Order_Date,101) as datetime)='" + activitydate + " 00:00:00.000' group by Product_Code";

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
        public DataSet dcr_Gettransno1(string sfcode, string div_code, string activitydate, string retailer_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Trans_Sl_No from Trans_Order_Head  where Sf_code='" + sfcode + "' and  CONVERT(VARCHAR(25), Order_Date, 126)  like '" + activitydate + "%' and Cust_Code='" + retailer_code + "'";
            //strQry = "SELECT Product_Code,cast(sum(Quantity) as varchar) Qty FROM Trans_Order_Head H inner join Trans_Order_Details D on H.Trans_Sl_No=D.Trans_Sl_No where Sf_Code='" + sfcode + "' and Cust_Code='" + retailer_code + "' and Stockist_Code='" + DistCode + "' and cast(convert(varchar,Order_Date,101) as datetime)='" + activitydate + " 00:00:00.000' group by Product_Code";
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
        public DataSet dcr_product_detaill(string Trans_Sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select d.Product_Code,d.Product_Name,d.Quantity,d.free,d.discount_price,d.Rate,d.value,(d.net_weight* d.Quantity)netvalue,d.MfgDt from Trans_Order_Head h  inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No where h.Trans_Sl_No='" + Trans_Sl_no + "'";

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
        public DataSet GetTable(string sfcode, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "select area,Shop_Name,Contact_Person,Phone_Number,a.Remarks,a.address,Convert(varchar(10),CONVERT(date,a.Submission_Date,106),103) as Date " +
                     " from DCRDetail_Distributors_Hunting a " +
                     " where a.DataSF='" + sfcode + "'  and convert(varchar,a.Submission_Date,103)= '" + Activity_date + "'";

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
        public int order_Confirm(string Flag, string date, string cus, string pro)
        {
            int iReturn = 1;


            try
            {

                DB_EReporting db = new DB_EReporting();
                string remark = "";

                strQry = "update Trans_Order_Head set Order_Flag = '" + Flag + "',Remarks='" + remark + "'where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
                strQry = "UPDATE Trans_Order_Details SET Order_Flag='" + 0 + "' FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Details.Product_Name = '" + pro + "' and Trans_Order_Head.Cust_Code='" + cus + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int order_Tranfer(string Flag, string date, string cus, string ord)
        {
            int iReturn = 1;


            try
            {

                DB_EReporting db = new DB_EReporting();


                //// strQry = "update Trans_Order_Head set Order_Flag = '" + Flag + "',Remarks='" + remark + "'where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                //iReturn = db.ExecQry(strQry);
                strQry = "UPDATE Trans_Order_Head SET Stockist_Code='" + cus + "' FROM Trans_Order_Details AS Trans_Order_Details " +
                         "INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE  Trans_Order_Head.Cust_Code='" + ord + "' and Product_Code='" + Flag + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int order_Cancel(string Flag, string date, string cus)
        {
            int iReturn = 1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_Order_Head set Order_Flag ='2',Remarks='" + Flag + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int order_Delete(string Flag, string date, string cus, string ord)
        {
            int iReturn = 1;


            try
            {

                DB_EReporting db = new DB_EReporting();


                //// strQry = "update Trans_Order_Head set Order_Flag = '" + Flag + "',Remarks='" + remark + "'where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                //iReturn = db.ExecQry(strQry);
                strQry = "delete Trans_Order_Details FROM Trans_Order_Details AS Trans_Order_Details " +
                         "INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE  Trans_Order_Head.Cust_Code='" + ord + "' and Product_Code='" + Flag + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //DCR_View
        public DataSet get_dcr_details1(string sf_code, string Activity_date, int iType, string dis_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,b.OrderTyp OrderType, " +
                   "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                   "from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                    "  FOR XML PATH(''), TYPE " +
                    "  ).value('.', 'NVARCHAR(MAX)') " +
                    " ,1,1,'') ) as che_POB_Name , (STUFF((SELECT distinct ',' + QUOTENAME(stockist_name) from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and " +
                    " a.Trans_SlNo = c.Trans_SlNo   FOR XML PATH(''), TYPE   ).value('.', 'NVARCHAR(MAX)')  ,1,1,'') ) as che_POB_DIS , " +
                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Code,c.ListedDr_Name,c.ListedDr_Address1+case when isnull(c.areaname,'') ='' then '' else  ', ' +isnull(c.areaname,'') end+case when isnull(c.PIN_Code,'') ='' then '' else  ', ' + isnull(c.PIN_Code,'') end as ListedDr_Address1,case when isnull(c.Contact_Person_Name,'') ='' then isnull(c.contactperson,'') else isnull(c.contactperson,'') end as Contact_Person_Name,case when isnull(c.ListedDr_Phone,'') ='' then isnull(c.ListedDr_Phone2,'') else isnull(c.ListedDr_Phone,'')   end as ListedDr_Mobile,isnull(b.Order_Value,0) as POB_Value,b.net_weight_value,b.Activity_Remarks,b.stockist_code,b.stockist_name,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                    " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,e.Doc_Special_Name,f.Doc_ClsName, a.Plan_No, a.Plan_Name ,b.Session,b.tm as Time,convert(varchar,b.ModTime,103)as ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                    " from DCRMain_Trans a, vwActivity_MSL_Details b, Mas_ListedDr c,Mas_Doctor_Speciality e,Mas_Doc_Class f " +
                    " where a.Sf_Code = '" + sf_code + "'and stockist_code='" + dis_code + "' and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                    " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                    " and  c.Doc_Special_Code = e.Doc_Special_Code and  c.Doc_ClsCode=f.Doc_ClsCode " +
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'" +
                    " Union all " +
                    " select a.Trans_SlNo, convert(varchar,a.Activity_Date,103) Activity_Date,convert(varchar,a.Submission_Date,103)  Submission_Date,b.OrderTyp OrderType, " +
                    "(STUFF((SELECT distinct ',' + QUOTENAME(SDP_Name) " +
                    " from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  a.Trans_SlNo = c.Trans_SlNo " +
                    "  FOR XML PATH(''), TYPE " +
                    "  ).value('.', 'NVARCHAR(MAX)') " +
                    " ,1,1,'') ) as che_POB_Name ,(STUFF((SELECT distinct ',' + QUOTENAME(stockist_name) from DCRDetail_Lst_Trans c where c.Trans_Detail_Info_Type = 1 and  " +
                    " a.Trans_SlNo = c.Trans_SlNo   FOR XML PATH(''), TYPE   ).value('.', 'NVARCHAR(MAX)')  ,1,1,'') ) as che_POB_DIS , " +
                    " a.Confirmed, b.Trans_Detail_Info_Code, b.Trans_Detail_Info_Type,c.Code,c.ListedDr_Name,c.ListedDr_Address1+case when isnull(c.areaname,'') ='' then '' else  ', ' +isnull(c.areaname,'') end+case when isnull(c.PIN_Code,'') ='' then '' else  ', ' + isnull(c.PIN_Code,'') end as ListedDr_Address1,case when isnull(c.Contact_Person_Name,'') ='' then isnull(c.contactperson,'') else isnull(c.contactperson,'') end as Contact_Person_Name,case when isnull(c.ListedDr_Phone,'') ='' then isnull(c.ListedDr_Phone2,'') else isnull(c.ListedDr_Phone,'')   end as ListedDr_Mobile,isnull(b.Order_Value,0) as POB_Value,b.net_weight_value,b.Activity_Remarks,b.stockist_code,b.stockist_name,b.Worked_with_Code,b.Worked_with_Name ,(b.Product_Detail + Additional_Prod_Dtls) as Product_Detail, " +
                    " b.Gift_Name+'~'+CONVERT(char,b.Gift_Qty) as Gift_Name,e.Doc_Special_Name,f.Doc_ClsName, a.Plan_No, a.Plan_Name ,b.Session,b.tm as Time,convert(varchar,b.ModTime,103)as ModTime,b.GeoAddrs,b.lati,b.long,(select feedback_content from Mas_App_CallFeedback where feedback_id=b.Rx) as Rx,b.SDP_Name " +
                    " from DCRMain_Temp a, vwActivity_MSL_Details b, Mas_ListedDr c,Mas_Doctor_Speciality e,Mas_Doc_Class f " +
                    " where a.Sf_Code = '" + sf_code + "'and stockist_code='" + dis_code + "'and a.Trans_SlNo=b.Trans_SlNo and b.Trans_Detail_Info_Type= " + iType +
                    " and b.Trans_Detail_Info_Type=1 and b.Trans_Detail_Info_Code=c.ListedDrCode  " +
                    " and c.Doc_Special_Code = e.Doc_Special_Code and  c.Doc_ClsCode=f.Doc_ClsCode " +
                    " and convert(varchar,a.Activity_Date,103)= '" + Activity_date + "'  order by a.Trans_SlNo,b.tm ";



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
        public DataSet dcr_Getcount(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select COUNT(b.Trans_Detail_Slno)Total_call_count,count(NULLIF(b.POB_Value,0))as Effictive_call_Count from DCRDetail_Lst_Trans b inner join DCRMain_Trans a on a.Trans_SlNo = b.Trans_SlNo where a.Sf_Code = '" + sfcode + "'and a.Trans_SlNo=b.Trans_SlNo and " +
                     " b.Trans_Detail_Info_Type= 1 and b.Trans_Detail_Info_Type=1   and convert(varchar,a.Activity_Date,103)= '" + activitydate + "'";

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


        public DataSet dcr_Getcount1(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = " select   m.Product_Cat_Name, count(m.Product_Cat_Code)cou from (SELECT t1.Product_Detail_Code,t1.Product_Detail_Name, " +
            //  "t1.Product_Cat_Code,t2.Quantity,t1.Product_Cat_Name FROM(  select c.Product_Detail_Code,c.Product_Detail_Name,c.Product_Cat_Code, " +
            //  "p.Product_Cat_Name from Mas_Product_Detail c inner join Mas_Product_Category p on p.Product_Cat_Code = c.Product_Cat_Code " +
            //  "where c.Division_Code='" + div_code + "'  and c.Product_Active_Flag=0  ) t1  right JOIN  (select c.Product_Detail_Code,c.Product_Detail_Name,d.Quantity from  " +
            //  "Mas_Product_Detail c inner join Trans_Order_Head h  inner join Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No " +
            //  "on  c.Product_Detail_Code=d.Product_Code where convert(varchar,h.Order_Date,103)='" + activitydate + "' and h.Sf_Code='" + sfcode + "' ) t2 " +
            //  "ON t1.Product_Detail_Code = t2.Product_Detail_Code )m " +
            //  "GROUP BY m.Product_Cat_Name ";

            strQry = " select    m.Product_Cat_Name ,count( m.Product_Cat_Code)cou from " +
                   "(SELECT t1.Product_Detail_Code,t1.Product_Detail_Name, t1.Product_Cat_Code,t2.Product_Code,t1.Product_Cat_Name " +
                   " FROM(  select c.Product_Detail_Code,c.Product_Detail_Name,c.Product_Cat_Code, p.Product_Cat_Name " +
                   " from Mas_Product_Detail c inner join Mas_Product_Category p on p.Product_Cat_Code = c.Product_Cat_Code " +
                   "where c.Division_Code='" + div_code + "'  and c.Product_Active_Flag=0 ) t1  right outer JOIN  (select d.Product_Code from  Trans_Order_Head h  " +
                   "inner join " +
                   "Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No  where convert(varchar,h.Order_Date,103)='" + activitydate + "' and  " +
                   "h.Sf_Code='" + sfcode + "'  GROUP BY d.Product_Code ) t2 ON t1.Product_Detail_Code = t2.Product_Code )m " +
                   "GROUP BY m.Product_Cat_Name";

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
        public DataSet dcr_Item_sum(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (div_code == "11" || div_code == "13" || div_code == "8" || div_code == "35" || div_code == "26" || div_code == "32")
            {
                strQry = "select c.Product_Detail_Name,sum(d.Quantity)Quantity, sum(d.value)Value,sum(d.net_weight)Net_Weight from " +
                        " Mas_Product_Detail c inner join Trans_Order_Head h  inner join Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No " +
                        " on  c.Product_Detail_Code=d.Product_Code where h.Sf_code='" + sfcode + "' and  CONVERT(VARCHAR(25), h.Order_Date, 126)  like '" + activitydate + "%' " +
                        " group by Product_Detail_Name order by  Product_Detail_Name ";
            }
            else
            {
                strQry = " select c.Product_Short_Name,sum(d.Quantity)Quantity, " +
                         " sum(d.value)Value,sum(d.net_weight)Net_Weight from   Mas_Product_Detail c inner join Trans_Order_Head h " +
                         " inner join Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No  on " +
                         " c.Product_Detail_Code=d.Product_Code where h.Sf_code='" + sfcode + "' and " +
                         " CONVERT(VARCHAR(25), h.Order_Date, 126)  like '" + activitydate + "%' group by Product_Short_Name order by  Product_Short_Name ";
            }
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
        public DataSet dcr_product_detaill1(string Trans_Sl_no, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code == "11")
            {
                strQry = "SELECT t1.Product_Detail_Name, t2.Quantity FROM( " +
                          " select c.Product_Detail_Name from Mas_Product_Detail c where c.Division_Code='" + div_code + "' and c.Product_Active_Flag=0 " +
                          " ) t1 " +
                          " left JOIN " +
                          " (select c.Product_Detail_Name,d.Quantity from   Mas_Product_Detail c inner join Trans_Order_Head h " +
                          " inner join Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No   on " +
                          " c.Product_Detail_Code=d.Product_Code where h.Trans_Sl_No='" + Trans_Sl_no + "') t2 " +
                          " ON t1.Product_Detail_Name = t2.Product_Detail_Name";
            }
            else
            {
                strQry = "SELECT t1.Product_Short_Name, t2.Quantity FROM( " +
                          " select c.Product_Short_Name from Mas_Product_Detail c where c.Division_Code='" + div_code + "' and c.Product_Active_Flag=0 " +
                          " ) t1 " +
                          " left JOIN " +
                          " (select c.Product_Short_Name,d.Quantity from   Mas_Product_Detail c inner join Trans_Order_Head h " +
                          " inner join Trans_Order_Details d   on h.Trans_Sl_No=d.Trans_Sl_No   on " +
                          " c.Product_Detail_Code=d.Product_Code where h.Trans_Sl_No='" + Trans_Sl_no + "') t2 " +
                          " ON t1.Product_Short_Name = t2.Product_Short_Name";
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
        public DataSet order_img(string date)
        {

            DataSet dsAdmin = null;
            try
            {

                DB_EReporting db_ER = new DB_EReporting();

                strQry = "select Order_Flag from Trans_Order_Head where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet attendance_view(string sf_code, string div_code, string month, string year, string mode, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == "Maximised")
            {
                strQry = "exec [attendancemaximisedcfinal] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
            }
            else if (mode == "Minimised")
            {
                strQry = "exec [attendanceminisedcfinal] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";


            }
            else
            {

                strQry = "exec [attendancemaximisedcfinal_valuewise] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "'";
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
        public DataSet Getroutedeviation_MR(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            /*strQry = "select top 1  CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, " +    
                        "(select Worktype_Name_M from Mas_WorkType_Mgr WT where tb.wtype=WT.WorkType_Code_M and sf_code like'MGR%') as Worktype_Name_B, "+ 
                        "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+      
                        "from TbMyDayPlan TB where Division_Code='"+div_code+"' and sf_code='"+sf_code+"' and CONVERT(char(10), Pln_Date,126)='"+date+"' "+     
                        "order by Pln_Date";*/
            //strQry = "exec [sp_UserList_getMR] '" + div_code + "','" + sf_code + "'";
            strQry = "exec [getHyrSFList] '" + sf_code + "','" + div_code + "'";
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
        public DataSet view_order_detail_view(string div_code, string distributor, string route, int Fmonth, int Fyear, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec ORDER_detail_sample1_prac '" + sf_code + "', '" + div_code + "','" + Fmonth + "','" + Fyear + "' ,'" + distributor + "','" + route + "'";
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
        public DataSet view_Daliy_inv_view(string div_code, string sf_code, string date, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            DataSet dsTerr = null;

            string state_code = string.Empty;

            strQry = "select State_Code from Mas_Salesforce where Sf_Code='" + sf_code + "' and SF_Status=0";
            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    state_code = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            strQry = "exec [view_Daliy_inv_view_sub] '" + sf_code + "','" + div_code + "','" + date + "','" + state_code + "','" + sub_div_code + "'";

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
        public DataSet get_DCRView_Approved_All_Dates_primary(string sf_code, string Fdate, string Tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select * from(select a.sf_Code,a.trans_slno,convert(varchar,a.Submission_Date,103) Submission_Date,  " +
     "day(a.Activity_Date) Activity_Date,c.Trans_Detail_Info_Code,c.Trans_Detail_Slno,c.Trans_Detail_Name,c.instrument_type,c.date_of_instrument,c.POB,c.Activity_Remarks from dcrdetail_csh_Trans c , DCRMain_Trans a" +
   " where a.Trans_SlNo = c.Trans_SlNo and  a.Sf_Code = '" + sf_code + "' and a.confirmed=1 and a.Activity_Date between '" + Fdate + "' and '" + Tdate + "')aa";

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
        public DataSet dcr_Gettransno_primary(string sfcode, string div_code, string trans_sl_no, string retailer_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            if (div_code == "32")
            {
                strQry = "	select pp.Product_code,cast(st.MRP_Price as varchar)Quantity,st.Distributor_Price,(cast(pp.Quantity as float) * cast(st.Distributor_Price as float))val,(cast(pp.Quantity as float) * cast(st.Distributor_Price as float) * cast(st.Distributor_Discount_Price as float)/100)Discount, " +
            "((cast(pp.Quantity as float) * cast(st.Distributor_Price as float))- (cast(pp.Quantity as float) * cast(st.Distributor_Price as float) * cast(st.Distributor_Discount_Price as float)/100))Fdiscountval,st.Product_detail_code  from(select   STUFF(left(gg.Dept, charindex('~', gg.Dept) -0),DATALENGTH(left(gg.Dept, charindex('~', gg.Dept) -0)), 1, '') as Product_code,REPLACE(right(gg.Dept, charindex('~', reverse(gg.Dept))-0),'~','') as Quantity  from (SELECT t.Trans_Detail_Info_Code,t.Trans_SlNo,t.sdp,  f.Val Dept " +
          "FROM [dcrdetail_csh_Trans] t " +
          "CROSS APPLY dbo.ParseValues(t.Additional_Prod_Code,'#')f  where t.Trans_SlNo='" + trans_sl_no + "' and  t.Trans_Detail_Info_Code ='" + retailer_code + "' and t.Division_code='" + div_code + "' and (t.sf_code='" + sfcode + "' or charindex('&&'+'" + sfcode + "'+'&&','&&'+t.Worked_with_Code+'&&')>0  ))gg group by  gg.Dept)pp inner join Mas_Product_State_Rates st on st.product_detail_Code=pp.Product_code group by product_code,Quantity,Distributor_Price,Distributor_Discount_Price,Product_Detail_Code,MRP_Price";
            }
            else
            {
                strQry = "	select pp.Product_code,pp.Quantity,st.Distributor_Price,(cast(pp.Quantity as float) * cast(st.Distributor_Price as float))val,(cast(pp.Quantity as float) * cast(st.Distributor_Price as float) * cast(st.Distributor_Discount_Price as float)/100)Discount, " +
                 "((cast(pp.Quantity as float) * cast(st.Distributor_Price as float))- (cast(pp.Quantity as float) * cast(st.Distributor_Price as float) * cast(st.Distributor_Discount_Price as float)/100))Fdiscountval,st.Product_detail_code  from(select   STUFF(left(gg.Dept, charindex('~', gg.Dept) -0),DATALENGTH(left(gg.Dept, charindex('~', gg.Dept) -0)), 1, '') as Product_code,REPLACE(right(gg.Dept, charindex('~', reverse(gg.Dept))-0),'~','') as Quantity  from (SELECT t.Trans_Detail_Info_Code,t.Trans_SlNo,t.sdp,  f.Val Dept " +
               "FROM [dcrdetail_csh_Trans] t " +
               "CROSS APPLY dbo.ParseValues(t.Additional_Prod_Code,'#')f  where t.Trans_SlNo='" + trans_sl_no + "' and  t.Trans_Detail_Info_Code ='" + retailer_code + "' and t.Division_code='" + div_code + "' and (t.sf_code='" + sfcode + "' or charindex('&&'+'" + sfcode + "'+'&&','&&'+t.Worked_with_Code+'&&')>0  ))gg group by  gg.Dept)pp inner join Mas_Product_State_Rates st on st.product_detail_Code=pp.Product_code group by product_code,Quantity,Distributor_Price,Distributor_Discount_Price,Product_Detail_Code";
            }
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
        public DataSet Get_Close_stock_MGR(string sf_code, string Div, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_Close_stock_MR '" + sf_code + "', '" + Div + "', " + iMonth + ", " + iYear + " ";

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
        public DataTable attendance_view_tb(string sf_code, string div_code, string month, string year, string mode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            if (mode == "Maximised")
            {
                strQry = "exec [attendancemaximisedcfinal] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "'";
            }
            else if (mode == "Minimised")
            {
                strQry = "exec [attendanceminisedcfinal] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "'";

            }
            else
            {
                strQry = "exec attendancemaximisedcfinal_valuewise_day '" + sf_code + "','" + div_code + "','" + month + "','" + year + "'";
            }
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Get_DCR_Count(string Div_Code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            //strQry = "select H.sf_code, count(distinct Trans_Detail_Info_Code) as dcr_count from dcrmain_trans H inner join dcrdetail_lst_trans d on h.trans_slno = d.trans_slno where H.division_code='" + Div_Code + "' AND MONTH(Activity_Date)='" + fmonth + "' AND YEAR(Activity_Date)='" + fyear + "' group by H.sf_code";
            strQry = "select sf_code, sum(dcr_count) dcr_count from (select H.sf_code, count(Trans_Detail_Info_Code) as dcr_count from dcrmain_trans H	 inner join dcrdetail_lst_trans d on h.trans_slno = d.trans_slno 	 where h.Division_Code='" + Div_Code + "' AND MONTH(Activity_Date)='" + fmonth + "' AND YEAR(Activity_Date)='" + fyear + "' group by H.sf_code , cast(convert(varchar,Activity_Date,101)as datetime) )kk group by sf_code";


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
        public DataTable sales_analysis_datatable(string div_code, string sf_code, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [Salesanalysispivot] '" + sf_code + "','" + div_code + "','" + year + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet sales_analysis_data(string div_code, string sf_code, string year, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [tsSalesanalysispivot1] '" + sf_code + "','" + div_code + "','" + year + "','" + subdiv_code + "'";
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
        public DataSet GetTPDayPlan_designationwise(string div_code, string sf_code, string Date, string designation_code, string sub_div_code = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select top 1 CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, "+  
            //            "(select Worktype_Name_B from Mas_WorkType_BaseLevel WT where tb.wtype=WT.WorkType_Code_B and sf_code like'MR%') as Worktype_Name_B, "+  
            //            "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+ 
            //            "from TbMyDayPlan TB where Division_Code='" + div_code + "' and sf_code='" + sf_code + "' and CONVERT(char(10), Pln_Date,126)='" + Date + "' " + 
            //            "order by Pln_Date ";
            strQry = "exec [getMyDayPlanVwSFHry_new_mm_withatt1] '" + sf_code + "','" + div_code + "','" + Date + "','" + designation_code + "','" + sub_div_code + "'";

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

        public DataSet dcr_GetAch(string sfcode, string div_code, string activitydate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int state_code = 0;
            strQry = "select State_Code from Mas_Salesforce where sf_Code='" + sfcode + "'";
            state_code = db_ER.Exec_Scalar(strQry);
            DataSet dsSF = null;
            strQry = "SELECT isnull(sum(Quantity*Convert(Decimal(10,2),MRP_Price)),0)Ach FROM Trans_Order_Head H inner join Trans_Order_Details D " +
                     "on H.Trans_Sl_No=D.Trans_Sl_No inner join Mas_Product_State_Rates r " +
                     "on r.Product_Detail_Code=D.Product_Code and r.State_code='" + state_code + "' where  H.Sf_Code='" + sfcode + "' " +
                     "and convert(varchar,H.Order_Date,103)='" + activitydate + "'";

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
        public DataSet daywisesalessummary(string div_code, string year, string month, string Sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [daywisesales_check1] '" + div_code + "','" + year + "','" + month + "','" + Sf_code + "'";
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

        public DataTable daywisesalessummary_tbl(string div_code, string year, string month, string Sf_code, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;


            strQry = "exec [daywisesales_check1] '" + div_code + "','" + year + "','" + month + "','" + Sf_code + "','" + subdiv_code + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet update_tax_Master(string tax_name, string tax_type, string tax_value, string tax_id, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "update TAX_master set Tax_Name='" + tax_name + "',Tax_Type='" + tax_type + "',Value='" + tax_value + "' where Tax_Id='" + tax_id + "' and Division_code='" + div_code + "'";
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
        public DataSet delete_tax_Master(string tax_id, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "Delete from TAX_master where Tax_Id='" + tax_id + "' and  Division_code='" + div_code + "' ";
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
        public DataSet view_tax_Master(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select *  from Tax_Master where  Tax_Active_Flag=0 and Division_code='" + div_code + "'";
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
        public DataSet attendance_view_status(string sf_code, string div_code, string month, string year, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code == "107") 
            {
                strQry = "exec [attendancemaximisedcfinal_Status_new] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
            }
			else
            {
            strQry = "exec [attendancemaximisedcfinal_Status] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";

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
        public DataSet attendance_view_typewise(string sf_code, string div_code, string month, string year, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [attendance_field_typewise_tri] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";



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
        public DataSet retailer_closing_stock_value(string sf_code, string div_code, string date, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [sp_retailerwiseclosingst_ch] '" + sf_code + "','" + div_code + "','" + date + "','" + todate + "'";
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
        public DataSet retailer_otstanding_value(string sf_code, string div_code, string year, string month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [retaileroutstanding_drm] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "'";
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


        public DataSet salesmandailycallfl(string sf_code, string div_code, string fdate, string todate, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [Salesmandaywisereport] '" + sf_code + "','" + div_code + "','" + fdate + "','" + todate + "','" + subdiv_code + "'";
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
        public DataSet view_total_order_view_in(string sf_code, string date, string cus_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "SELECT d.Trans_sl_no,d.SF_Code,(select Sf_Name from Mas_Salesforce g where Sf_Code=D.Sf_Code) Sf_Name,d.Stockist_Code,D.Cust_Code,(select TP.Stockist_Name from mas_stockist TP where Stockist_Code=D.Stockist_Code) Stockist_Name," +
                         "d.Order_value,d.net_weight_value,d.Order_date,d.Order_Flag," +
                         "(select t.Territory_Name from Mas_Territory_creation t where t.Territory_code=d.route and t.Territory_Active_Flag=0) as routename," +
                         "(select l.ListedDr_Name from Mas_Listeddr l where l.ListedDrCode=d.Cust_Code and l.ListedDr_Active_Flag=0) as retailername " +
                         "FROM Trans_Order_Head d " +
                         "where CAST(CONVERT(VARCHAR, D.Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and D.STOCKIST_CODE='" + sf_code + "' and Cust_Code='" + cus_code + "' " +
                         "group by d.Trans_sl_no,d.SF_Code,d.Stockist_Code,D.Cust_Code,d.Order_value,d.net_weight_value,d.Order_date,d.route,d.Order_Flag " +
                         "order by d.Stockist_Code,D.Cust_Code,d.route,d.Order_date,d.Order_value,d.net_weight_value,d.Order_Flag";
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

        public DataSet view_total_order_view_in_order(string sf_code, string date, string cus_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            //strQry = "select Cust_Code,Product_Code,Product_Name,Quantity,sum(Quantity * net_weight) net_weight,value,a.Order_Flag,a.discount,a.free from  Trans_Order_Details a inner join Trans_Order_Head b on " +
            //         " a.Trans_sl_no =b.Trans_Sl_No where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) " +
            //         " and STOCKIST_CODE='" + sf_code + "' and Cust_Code='" + cus_code + "' group by Cust_Code,Product_Code,Product_Name,Quantity,value,a.Order_Flag,a.discount,a.free";
            strQry = "select Cust_Code,Product_Code,Product_Name,Quantity,sum(Quantity * net_weight) net_weight,R.Retailor_Price,a.discount,a.free,a.Trans_Order_No " +
                     "from  Trans_Order_Details a " +
                     "inner join Trans_Order_Head b on  a.Trans_sl_no =b.Trans_Sl_No " +
                     "inner join mas_salesforce ms on ms.sf_code = b.sf_code " +
                     "inner join Mas_Product_Detail p on a.Product_Code=P.Product_Detail_Code " +
                     "inner join Mas_Product_State_Rates R  on a.Product_Code = R.Product_Detail_Code  and  R.state_code = ms.State_Code " +
                     "where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)  " +
                     "and STOCKIST_CODE='" + sf_code + "' and Cust_Code='" + cus_code + "' group by Cust_Code,Product_Code,Product_Name,Quantity,R.Retailor_Price,a.Order_Flag,a.discount,a.free,a.Trans_Order_No";
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

        public DataSet view_tax_Mas(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            // strQry = "select (cast(Tax_Name as varchar)+'('+cast (Value as varchar)+Tax_Type+')')Tax_Name,Value as Tax_Val from Tax_Master where Division_Code='" + div_code + "'";
            strQry = "select '0' as Tax_Id,'--Select--' as Tax_Name, '0' as Tax_Val union all select Tax_Id, (cast(Tax_Name as varchar)+'('+cast (Value as varchar)+Tax_Type+')')Tax_Name,Value as Tax_Val from Tax_Master where Division_Code='" + div_code + "'";
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

        public DataSet view_Trans_Mas(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Trans_Code,Trans_Name from Mas_Transport_Master where Division_Code='" + div_code + "' and Active_Flag=0";
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
        public DataSet view_Pay_Mas(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

           // strQry = "select Code,Name from Mas_Payment_Type where Division_Code='" + div_code + "' and Active_Flag=0";
		    strQry = "select Code, Name from Mas_Payment_Type where charindex('," + div_code + ",', ',' + cast(Division_Code as varchar) + ',') > 0 and Active_Flag=0";
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
        public DataSet attendance_view_D(string sf_code, string div_code, string month, string year, string mode, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (mode == "Maximised")
            {
                strQry = "exec [attendancemaximisedcfinal_D] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
            }
            else if (mode == "Minimised")
            {
                strQry = "exec [attendanceminisedcfinal_D] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
            }
            else
            {

                strQry = "exec [attendancemaximisedcfinal_valuewise_D] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";
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
        public DataSet attendance_view_status_D(string sf_code, string div_code, string month, string year, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [attendancemaximisedcfinal_Status_D] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";



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
        public DataSet attendance_view_typewise_D(string sf_code, string div_code, string month, string year, string subdiv_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [attendance_field_typewise_tri_D] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "'";



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
        public DataSet view_pri_order_view(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_PRI_VIEW_DISCOUNT_FREEVAL] '" + sf_code + "','" + div_code + "','" + date + "'";
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
        public DataSet view_Primary_order_view(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "SELECT d.Trans_sl_no,d.SF_Code,(select Sf_Name from Mas_Salesforce g where Sf_Code=D.Sf_Code) Sf_Name, " +
                        "d.Stockist_Code,(select TP.Stockist_Name from mas_stockist TP where Stockist_Code=D.Stockist_Code) Stockist_Name, " +
                        "d.Order_Value,d.Collected_Amount,d.Order_Date,d.Order_Flag,d.Pay_Type,d.Order_No FROM Trans_PriOrder_Head d where " +
                        "CAST(CONVERT(VARCHAR, D.Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and " +
                        "D.Stockist_Code='" + sf_code + "' group by d.Trans_Sl_No,d.Sf_Code,d.Stockist_Code,d.Order_value,d.Collected_Amount, " +
                        "d.Order_date,d.Order_Flag,d.Pay_Type,d.Order_No order by d.Stockist_Code,d.Order_date,d.Order_value, " +
                        "d.Collected_Amount,d.Order_Flag,d.Pay_Type,d.Order_No";
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
        public int Primary_order_Confirm(string Flag, string date, string cus, string supp, string supp_code, string order_no)
        {
            int iReturn = 1;


            try
            {

                DB_EReporting db = new DB_EReporting();
                string remark = "";

                strQry = "update Trans_PriOrder_Head set Order_Flag = '" + 1 + "',Sup_No='" + supp + "',Sup_Name='" + supp_code + "' where Trans_Sl_No='" + order_no + "' and  Stockist_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Trans_PriOrder_Details SET Order_Flag='" + 1 + "' FROM Trans_PriOrder_Details AS Trans_PriOrder_Details INNER JOIN Trans_PriOrder_Head AS Trans_PriOrder_Head ON Trans_PriOrder_Details.Trans_Sl_No = Trans_PriOrder_Head.Trans_Sl_No " +
                         "WHERE  Trans_PriOrder_Head.Stockist_Code='" + cus + "' and Trans_PriOrder_Details.Trans_Sl_No='" + order_no + "'";
                iReturn = db.ExecQry(strQry);

                strQry = "insert into UPL_Purchase(Year,Month,Day,DistributorCode,DistributorName,ItemCode,ItemName,Qty,Division_Code,sfcode) " +
                         "select  year(Order_Date)Year,month(Order_Date)Month,day(Order_Date)Day,Stockist_Code DistributorCode, " +
                         "(select st.Stockist_Name from Mas_Stockist st where st.Stockist_Code=a.Stockist_Code)DistributorName,b.Product_Code ItemCode, " +
                         "b.Product_Name ItemName,b.CQty Qty,a.Division_Code Division_Code,a.Sf_Code sfcode " +
                         "from Trans_PriOrder_Head a inner join Trans_PriOrder_Details b on b.Trans_Sl_No=a.Trans_Sl_No where a.Trans_Sl_No='" + order_no + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet view_total_order_viewyy_baidiya(string div_code, string sf_code, string date, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_VIEW_sampletest] '" + sf_code + "','" + div_code + "','" + date + "','" + subdivcode + "'";
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
        public DataSet Getsfcode_all(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            /*strQry = "select top 1  CONVERT(varchar(15),CAST(pln_date AS TIME),100) as Pln_Time,sf_code,convert(char(10),Pln_Date,103) as Pln_Date,ClstrName,remarks, " +    
                        "(select Worktype_Name_M from Mas_WorkType_Mgr WT where tb.wtype=WT.WorkType_Code_M and sf_code like'MGR%') as Worktype_Name_B, "+ 
                        "(select Stockist_Name from Mas_Stockist MS where tb.stockist=MS.Stockist_Code) as dist_name "+      
                        "from TbMyDayPlan TB where Division_Code='"+div_code+"' and sf_code='"+sf_code+"' and CONVERT(char(10), Pln_Date,126)='"+date+"' "+     
                        "order by Pln_Date";*/
            //strQry = "exec [sp_UserList_getMR] '" + div_code + "','" + sf_code + "'";
            strQry = "exec [getHyrSFList] '" + sf_code + "','" + div_code + "'";
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

        public DataSet view_pri_order_view_sub(string div_code, string sf_code, string date, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_PRI_VIEW_DISCOUNT_FREEVAL1] '" + sf_code + "','" + div_code + "','" + date + "','" + subdivcode + "'";
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
        public DataSet view_SS_order_view_sub(string div_code, string sf_code, string date, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_SS_VIEW] '" + sf_code + "','" + div_code + "','" + date + "','" + tdate + "','" + subdivcode + "'";
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

        public DataSet attendance_view_wortypewise(string sf_code, string div_code, string month, string year, string subdiv_code, string statec)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;



            strQry = "exec [attendancemaximisedcfinal_wortypewise] '" + sf_code + "','" + div_code + "','" + month + "','" + year + "','" + subdiv_code + "','" + statec + "'";

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
        public DataSet view_total_Pri_order_view_in(string sf_code, string date, string cus_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "SELECT d.Trans_Sl_No,d.Sf_Code,(select Sf_Name from Mas_Salesforce g where Sf_Code=D.Sf_Code) Sf_Name,d.Stockist_Code,(select TP.Stockist_Name from mas_stockist TP where Stockist_Code=D.Stockist_Code) Stockist_Name," +
                         "d.Order_Value,d.Collected_Amount,d.Order_Date,d.Order_Flag,d.Sup_Name,d.Sup_No  " +
                         "FROM Trans_PriOrder_Head d " +
                         "where CAST(CONVERT(VARCHAR, D.Order_Date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and D.Stockist_Code='" + sf_code + "' and Order_No='" + cus_code + "' " +
                         "group by d.Trans_sl_no,d.SF_Code,d.Stockist_Code,d.Order_value,d.Collected_Amount,d.Order_Date,d.Sup_Name,d.Sup_No,d.Order_Flag " +
                         "order by d.Stockist_Code,D.Sup_Name,d.Sup_No,d.Order_Date,d.Order_value,d.Collected_Amount,d.Order_Flag";
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
        public DataSet view_total_Pri_order_view_in_order(string sf_code, string date, string cus_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            //strQry = "select Cust_Code,Product_Code,Product_Name,Quantity,sum(Quantity * net_weight) net_weight,value,a.Order_Flag,a.discount,a.free from  Trans_Order_Details a inner join Trans_Order_Head b on " +
            //         " a.Trans_sl_no =b.Trans_Sl_No where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) " +
            //         " and STOCKIST_CODE='" + sf_code + "' and Cust_Code='" + cus_code + "' group by Cust_Code,Product_Code,Product_Name,Quantity,value,a.Order_Flag,a.discount,a.free";
            strQry = "select Product_Code,Product_Name,R.Distributor_Price,CQty,PQty,value " +
                     "from  Trans_PriOrder_Details a " +
                     "inner join Trans_PriOrder_Head b on  a.Trans_Sl_No =b.Trans_Sl_No " +
                     "inner join mas_salesforce ms on ms.sf_code = b.Sf_Code " +
                     "inner join Mas_Product_Detail p on a.Product_Code=P.Product_Detail_Code " +
                     "inner join Mas_Product_State_Rates R  on a.Product_Code = R.Product_Detail_Code  and  R.state_code = ms.State_Code " +
                     "where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)  " +
                     "and b.Stockist_Code='" + sf_code + "' and b.Order_No='" + cus_code + "' group by Product_Code,Product_Name,R.Distributor_Price,CQty,PQty,value";
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
        //Giri 06-08-2018
        public DataSet view_Visited_Outlets_view_sub(string div_code, string subdivcode, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null; string sub = "";
            if (subdivcode == "" || subdivcode == null)
            {
                sub = "";
            }
            else { sub = subdivcode.ToString(); }


            //strQry = "exec [GET_Total_OutLetdashboardMGR] '" + div_code + "','" + sub + "','" + sf_code + "','" + Convert.ToDateTime(date) + "'";

            strQry = "exec [GET_Total_OutLetdashboardMGR] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";


            /* strQry = " select A.Sf_Code,A.Sf_Name, rss.Sf_Name rsfname, M.stockist_code,M.stockist_name,A.Plan_No,A.Plan_Name, " +
                    " count(A.Sf_Code)Total_call,sum(isnull(M.Order_Value,0))Order_Value, " +
                    " count( (case when M.POB_Value>0 then 1 end))  as Productive, " +
                    " count( (case when  M.POB_Value=0 then 1 end)) as Nonproductive " +
                    " from " +
                    " [vwActivity_MSL_Details] M inner join " +
                    " [vwActivity_Report] A  on M.Trans_SlNo=A.Trans_SlNo  inner join mas_salesforce ss on ss.sf_code= a.Sf_Code   inner join mas_salesforce rss on rss.sf_code = ss.Reporting_To_SF " +
                    " where  a.Division_Code='" + div_code + "' and " +
                    " cast(CONVERT(varchar, a.Activity_Date,101) as datetime)='" + date + "' " +
                    " group by A.Sf_Code,A.Sf_Name, rss.Sf_Name, " +
                    " A.Sf_Code,A.Sf_Name,M.stockist_code,M.stockist_name,A.Plan_No,A.Plan_Name ";*/

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
        public DataSet zonewiseperformance(string div_code, string month, string year, string desigcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            DataSet sf_codee = null;
            string sf_code = string.Empty;
            strQry = " select top 1 sf_code from  Mas_Salesforce where Designation_code ='" + desigcode + "' and sf_status=0";
            sf_codee = db_ER.Exec_DataSet(strQry);
            sf_code = sf_codee.Tables[0].Rows[0][0].ToString();
            if (sf_code.Contains("MR"))
            {
                strQry = "exec [Zonewise_PerformanceFINAL] '" + div_code + "','" + month + "','" + year + "','" + desigcode + "','admin'";
            }
            else
            {
                strQry = "exec [Zonewise_Performance_finalmgr_test_di] '" + div_code + "','" + month + "','" + year + "','" + desigcode + "','admin'";

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
        public DataSet get_missed_data(string sf_code, string FYear, string FMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select convert(varchar, Dcr_Missed_Date,103) as mDate  from DCR_MissedDates d inner join Mas_Salesforce s on s.Sf_Code = d.sf_code where d.Sf_Code = '" + sf_code + "' and year = '" + FYear + "' and month = '" + FMonth + "' and Status=0 group by convert(varchar, Dcr_Missed_Date,103) order by mDate";
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

        public int Releas_missed_data(string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;



            strQry = "update DCR_MissedDates set Status=1  where Sf_Code = '" + sf_code + "' and cast(convert(varchar,Dcr_Missed_Date,101)as datetime)='" + date + "'";
            try
            {

                iReturn = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet get_trans_demo_head(string sf_code, string Activity_date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select * from Trans_Demo_Head where sf_code='" + sf_code + "' and CONVERT(VARCHAR(10), CAST(current_dateand_time AS DATETIME), 103)='" + Activity_date + "'";

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

        public DataSet vanstockledger(string sf_code, string divcode, string fdate, string tdate, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " exec van_stock_ledger '" + sf_code + "'," + divcode + ",'" + fdate + "','" + tdate + "','" + subdivcode + "'";

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

	  public DataSet getRetClosingQty(string div_code, string sf_code,string subdiv, string Fmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (subdiv == "" || subdiv == null)
            {
                subdiv = "";
            }

            strQry = "exec getRetClosingQty '" + sf_code + "','" + div_code + "','" + subdiv + "','" + Fmonth + "','" + Fyear + "'";
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



       public DataSet getRetailerClosing(string div_code, string sfcode, string subdiv, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (subdiv == "" || subdiv == null)
            {
                subdiv = "";
            }
            strQry = "exec getRetailerClosing '" + sfcode + "','" + div_code + "','" + subdiv + "','" + month + "','" + year + "'";

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
  /*public DataSet getRetClosingQty(string div_code, string sf_code, string Fmonth, string Fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select t.Trans_sl_no,Order_Date,Product_Code,Product_Name,((isnull(Sample_Erp_Code,0)*cast(isnull(CCLStock,0) as int))+ClStock)Closing " +
                     " from(select row_number() over(partition by Cust_Code order by[Order_Date] desc)rw, Cust_Code, Order_Date, Trans_sl_no from trans_order_head " +
                     " where month(Order_Date) = " + Fmonth + " and year(Order_Date) = " + Fyear + " and sf_code = '" + sf_code + "' and Div_ID = " + div_code + ") as t " +
                     " inner join trans_order_details od on od.Trans_sl_no = t.Trans_sl_no  inner join mas_listeddr dr on dr.ListedDrCode = t.Cust_Code " +
                     " inner join mas_product_detail pd on pd.Product_Detail_Code = od.Product_code where rw = 1 order by Order_Date";

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
		
		
		
		public DataSet getRetailerClosing(string div_code, string sfcode, string month, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select dr.ListedDr_Name,Order_Date,Trans_sl_no from (select row_number() over(partition by Cust_Code order by [Order_Date] desc)rw,Cust_Code,Order_Date,oh.Trans_sl_no from trans_order_head oh " +
                     " inner join trans_order_details od on od.Trans_sl_no=oh.Trans_sl_no where month(Order_Date)=" + month + " and year(Order_Date)=" + year + " and sf_code='" + sfcode + "' and oh.Div_ID=" + div_code + ") as t  " +
                     " inner join mas_listeddr dr on dr.ListedDrCode=t.Cust_Code where rw=1 order by Order_Date";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }*/
		public DataSet get_product_order(string sfcode,string Div, string FDT, string TDT)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec get_product_order'" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "'";
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


        public DataSet get_distributor_order(string sfcode, string Div, string FDT, string TDT)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec get_distributor_order '" + sfcode + "','" + Div + "','" + FDT + "','" + TDT + "'";

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
		
		public DataSet getcompetitorlist(string div_code, string sfcode, string pcode, string odate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select Order_Date,Name,Brand_Name,Qty,Rate from trans_compititor c inner join Trans_RCPA_Brand b on b.Sl_No=c.Brand_Code " +
                     " where cat_id = '" + pcode + "' and convert(date, Order_Date)= '" + odate + "' and sf_code = '" + sfcode + "' and c.Division_Code = " + div_code + " ";

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

        public DataSet TODAY_ORDER_VIEWHAP(string div_code, string sf_code, string date, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec TODAY_ORDER_VIEWHAP '" + sf_code + "','" + div_code + "','" + date + "','" + sf_type + "'";
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

        public DataSet TODAY_PRI_ORDER_VIEWstk(string sf_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec TODAY_PRI_ORDER_VIEWstk '" + sf_code + "','" + div_code + "','" + date + "'";
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

        public DataSet getretdetsaa(string sf_code, string div_code, string fdate, string tdate,string subdiv_code="0",string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "exec GET_NEW_Retailer51 '" + div_code + "','" + sf_code + "','" + fdate + "','" + tdate + "','"+subdiv_code+"',"+statecode+"";
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

        public DataSet secneworderview(string div_code, string sfcode, string fdate, string tdate, string subdiv="0",string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " exec TODAY_ORDER_VIEW_sample51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "',"+statecode+"";

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
        public DataSet secordernewqty(string div_code, string sfcode, string fdate, string tdate, string subdiv="0",string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;

            strQry = " exec TODAY_ORDER_VIEW_sample_QTY51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "',"+statecode+"";

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
    public DataSet getgrpname(string div_code, string subdivcode, string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (subdivcode != "0" && subdivcode != null && subdivcode != "")
            {
                strQry = "select * from (select Product_Grp_Code,Product_Grp_Name,(select COUNT(product_detail_code) from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Grp_Code=gp.Product_Grp_Code and CHARINDEX(','+CAST(" + subdivcode + " as varchar)+',',','+subdivision_code+',')>0 and CHARINDEX(','+iif('" + statecode + "'<>'0',CAST(" + statecode + " as varchar),State_Code)+',',','+State_Code+',')>0  and Product_Active_Flag=0)as t from Mas_Product_Group gp where Division_Code='" + div_code + "' and  Product_Grp_Active_Flag=0) as gr where t<>0";
            }
            else
            {
                strQry = "select * from (select Product_Grp_Code,Product_Grp_Name,(select COUNT(product_detail_code) from Mas_Product_Detail where Division_Code='" + div_code + "' and Product_Grp_Code=gp.Product_Grp_Code  and CHARINDEX(','+iif('" + statecode + "'<>'0',CAST(" + statecode + " as varchar),State_Code)+',',','+State_Code+',')>0 and Product_Active_Flag=0)as t from Mas_Product_Group gp where Division_Code='" + div_code + "'  and  Product_Grp_Active_Flag=0) as gr where t<>0";
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
        public DataSet getgrppdname(string div_code,string subdivcode,string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select pd.Product_Detail_Code,case when isnull(pd.UOM_Weight,'')='' or pd.division_Code<>98 then pd.Product_Detail_Name else pd.UOM_Weight End Product_Detail_Name from Mas_Product_Detail pd left join Mas_Product_Group gp on gp.Product_Grp_Code=pd.Product_Grp_Code where pd.Division_Code='" + div_code + "'";
            if (subdivcode != "0" && subdivcode != null && subdivcode != "")
            {
                strQry += " and CHARINDEX(','+CAST(" + subdivcode + " as varchar)+',',','+pd.subdivision_code+',')>0";
            }
            strQry += " and pd.Product_Active_Flag=0 and CHARINDEX(','+iif('"+ statecode + "'<>'0',CAST(" + statecode + " as varchar),pd.State_Code)+',',','+pd.State_Code+',')>0  order by gp.Product_Grp_Code,Prod_Detail_Sl_No";
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

        public DataSet get_invoice_detail(string sf_code, string div_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec get_invoice_detail '" + sf_code + "','" + div_code + "','" + date + "'";
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
        public int delete_Order(string TransslNo, string sfcode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            int dsAdmin = -1;
            strQry = "exec Delete_SecOrder '" + TransslNo + "'";
            try
            {
                dsAdmin = db_ER.Exec_Scalar(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet gen_missed_data(string sf_code,string div_code, string FMonth, string FYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec Missed_Date_Gen_Web '" + sf_code + "'," + div_code + ",'" + FMonth + "','" + FYear + "'";
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
        public DataSet vanordernewqty(string div_code, string sfcode, string fdate, string tdate, string subdiv = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;

            strQry = " exec TODAY_ORDER_VIEW_vantour_QTY51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "'," + statecode + "";

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
		  public DataSet vanorderview(string div_code, string sfcode, string fdate, string tdate, string subdiv = "0", string statecode = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " exec TODAY_ORDER_VIEW_vantour51 '" + sfcode + "','" + div_code + "','" + fdate + "','" + tdate + "','" + subdiv + "'," + statecode + "";

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
        public DataSet getDayplan(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getPerDaySumaary '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataSet getPerDayCalls(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getPerDayCallDetails '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataSet getRetailerCount(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getRoutewiseCnt '" + DivCode.TrimEnd(',') + "'";

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
        public DataSet getLoginTimes(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getDayAttnDets '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataSet getTPDates(string fdt, string tdt)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getTPDates '" + fdt + "','" + tdt + "'";

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
        public DataSet getNRetailerPOB(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getNewRetailersPOB '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
        public DataTable getDataTable(string Qry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            strQry = Qry;

            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public int ApproveDeviation(string Qry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int dsAdmin = -1;
            strQry = Qry;
            try
            {
                dsAdmin = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataTable getDCRCount(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            strQry = "exec getDCRDetailCount '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataTable getRoutewiseDist(string sfcode, string DivCode, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            strQry = "exec getRoutewiseDist '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + subdiv + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataTable getRoutewiseBrandSales(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsAdmin = null;

            strQry = "exec getRoutewiseBrandSales '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

            try
            {
                dsAdmin = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet getDayplanExcel(string sfcode, string DivCode, string fdt, string tdt, string subdiv = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getPerDaySumaary_Excel '" + sfcode + "','" + DivCode.TrimEnd(',') + "','" + fdt + "','" + tdt + "','" + subdiv + "'";

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
    }
}
