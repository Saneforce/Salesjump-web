using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Bus_EReport
{
    public class AdminSetup
    {
        private string strQry = string.Empty;

        public DataSet getAdminSetup()
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
            " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,Division_Code  from Admin_Setups";



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
        public DataSet getAdminSetup_MGR()
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
              "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
              "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
              " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand,Remarks_length_Allowed,TpBased,No_of_TP_View " +
              " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
              " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,Division_Code  from Admin_Setups_Mgr";

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

        public DataSet getAdminSetup(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
                     "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
                     "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
                     " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand, " +
                     " Remarks_length_Allowed,TpBased,No_of_TP_View, " +
                     " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
                     " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr, Doc_App_Needed, wrk_area_Name, Doc_Deact_Needed, Add_Deact_Needed  " +
                     " from Admin_Setups where Division_Code = '" + divcode + "'";



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
        public DataSet getAdminSetup_MGR(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed,No_Of_Sl_StockistAllowed,SingleDr_WithMultiplePlan_Required," +
            "DCRTime_Entry_Permission,DCRSess_Entry_Permission,No_Of_DCR_Chem_Count,No_Of_DCR_Drs_Count,No_Of_DCR_Ndr_Count," +
            "No_Of_DCR_Stockist_Count,No_of_dcr_hosp, Doctor_disp_in_Dcr,NonDrNeeded,  " +
            " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, pobtype,DCRSess_Mand,DCRTime_Mand,DCRProd_Mand,Remarks_length_Allowed,TpBased,No_of_TP_View, " +
            " DelayedSystem_Required_Status , Delay_Holiday_Status , No_Of_Days_Delay, " +
            " AutoPost_Holiday_Status, AutoPost_Sunday_Status,Approval_System,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr from Admin_Setups_MGR where Division_Code = '" + divcode + "'";


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


        public DataSet getMail(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Move_MailFolder_Id,Move_MailFolder_Name from Mas_Mail_Folder_Name where Division_Code = '" + Division_Code + "'";
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

        public DataSet ViewMail(int mailid)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select a.mail_sf_from, a.To_SFName, a.Mail_Subject, a.Mail_Content ,a.Mail_Sent_Time,a.Mail_CC,a.Mail_Content, " +
                      " a.Mail_SF_Name,a.CC_SfName,a.Bcc_SfName,Mail_CC,Mail_BCC,Mail_SF_To,CC_SfName,Bcc_SfName " +
                      " from Trans_Mail_Head a " +
                      " where Trans_Sl_No = " + mailid;

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

        public DataSet FillSalesForce(string des_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                     " a.Designation_Code,'' sf_color,'' sf_Type from Mas_Salesforce a,Mas_SF_Designation b " +
                     " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code " +
                     " order by a.Sf_Name";

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

        public DataSet FillDesignation(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (Div_Code != "")
            {
                if (Div_Code.Contains(','))
                {
                    Div_Code = Div_Code.Remove(Div_Code.Length - 1);
                }
                strQry = "select Designation_Code, Designation_Name, Designation_Short_Name  from Mas_sf_Designation where Division_Code='" + Div_Code + "' ";
            }
            else
            {
                strQry = "select Designation_Code, Designation_Name, Designation_Short_Name  from Mas_sf_Designation ";
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

        public DataSet getMailInbox(string sf_code, string div_code, string type, string folder, string month, string year, string sort, string sortexp, string StrSearch)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //if (div_code == "")
            //{
            //    if (div_code.Contains(','))
            //    {
            //        div_code = div_code.Remove(div_code.Length - 1);
            //    }
            //    strQry = "EXEC MailInbox  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + sort + "', '" + sortexp + "','" + StrSearch + "' ";
            //}
            ////else
            ////{

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "EXEC MailInbox_DivCode  '" + sf_code + "', '" + div_code + "' , '" + type + "' , '" + folder + "', '" + year + "', '" + month + "' , '" + sort + "', '" + sortexp + "','" + StrSearch + "' ";
            //}

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

        public DataSet getMailAttach(string Trans_Sl_No)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select isnull(Mail_Attachement,'') Mail_Attachement from trans_mail_head where Trans_Sl_No='" + Trans_Sl_No + "'";

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
        public DataSet ComposeMail(string sf_code, string to_sf_code, string subject, string msg, string sAttach, string cc_sf_code, string bcc_sf_code, string div_code, string sRemote, string sToAddr, string sCCAddr, string sBCCAddr, string mail_to_sf_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            subject = subject.Replace("'", "asdf");
            msg = msg.Replace("'", "asdf");

            strQry = "EXEC MailInsert  '" + sf_code + "', '" + to_sf_code + "' , '" + subject + "' , '" + msg + "', '" + sAttach + "', '" + cc_sf_code + "' , '" + bcc_sf_code + "', '" + div_code + "' , '" + sRemote + "', '" + sToAddr + "', '" + sCCAddr + "', '" + sBCCAddr + "','" + mail_to_sf_Name + "'  ";

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
        //public DataSet ComposeMail(string sf_code, string to_sf_code, string subject, string msg, string sAttach, string cc_sf_code, string bcc_sf_code, string div_code, string sRemote, string sToAddr, string sCCAddr, string sBCCAddr, string mail_to_sf_Name)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = "EXEC MailInsert  '" + sf_code + "', '" + to_sf_code + "' , '" + subject + "' , '" + msg + "', '" + sAttach + "', '" + cc_sf_code + "' , '" + bcc_sf_code + "', '" + div_code + "' , '" + sRemote + "', '" + sToAddr + "', '" + sCCAddr + "', '" + sBCCAddr + "','" + mail_to_sf_Name + "'  ";

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
        public int ChangeMailStatus(string sf_code, int mail_id, int status, string sf_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            //strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

            strQry = "update Trans_Mail_detail set Mail_Read_Date=GETDATE(), open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id + " and open_mail_id = '" + sf_code + "'";

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
        public bool sRecordExist(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_Setups where Division_Code = '" + Division_Code + "'";
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

        public bool sRecordExistMGR(string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_Setups_MGR where Division_Code = '" + Division_Code + "'";
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
        public int RecordUpdate(string Doc_MulPlan, string strWorkAra, string strNoofTPView, int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR, int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd, int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand, int DCRProd_Mand, string wrk_area_SName, int iDelayedSystem, int iHolidayCalc, int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code, string strDCRTP, int remarkslength, int iDrRem, int inewchem, int inewudr, string ListedDr_Allowed, string Chemist_Allowed, int iDocApp, int iDeactApp, int iAddDeact, string Stockist_Allowed)
        {
            int iReturn = -1;
            int Count = 0;
            DataSet dsadmin = null;
            bool isdcrsetupupdt = false;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
                bool value = sRecordExist(Division_Code);

                if (value == false)
                {

                    strQry = "Insert into Admin_Setups (SingleDr_WithMultiplePlan_Required,Wrk_Area_Name,No_of_TP_View,No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
                         + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp, Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
                         + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand, wrk_area_SName, "
                         + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System, Division_Code, TpBased,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr,No_Of_Sl_DoctorsAllowed,No_Of_Sl_ChemistsAllowed, Doc_App_Needed, Doc_Deact_Needed, Add_Deact_Needed,No_Of_Sl_StockistAllowed) values "
                         + " ('" + Doc_MulPlan + "','" + strWorkAra + "','" + strNoofTPView + "','" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
                         + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
                         + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "','" + wrk_area_SName + "', "
                         + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "', '" + strDCRTP + "' ,'" + remarkslength + "' ,'" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "','" + ListedDr_Allowed + "' ,'" + Chemist_Allowed + "', '" + iDocApp + "', '" + iDeactApp + "', '" + iAddDeact + "','" + Stockist_Allowed + "')";
                }
                else
                {
                    dsadmin = getAdminSetup(Division_Code);

                    if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
                        isdcrsetupupdt = true;
                    if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                        isdcrsetupupdt = true;
                    if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
                        isdcrsetupupdt = true;
                    if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
                        isdcrsetupupdt = true;
                    if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
                        isdcrsetupupdt = true;
                    if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
                        isdcrsetupupdt = true;
                    if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
                        isdcrsetupupdt = true;
                    if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
                        isdcrsetupupdt = true;
                    if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
                        isdcrsetupupdt = true;
                    if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
                        isdcrsetupupdt = true;
                    if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
                        isdcrsetupupdt = true;
                    if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
                        isdcrsetupupdt = true;
                    if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                        isdcrsetupupdt = true;
                    if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
                        isdcrsetupupdt = true;

                    strQry = "UPDATE Admin_Setups " +
                               " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "', "
                                + " No_of_TP_View = '" + strNoofTPView + "',"
                                + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
                                + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
                                + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
                                + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
                                + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
                                + " Doctor_disp_in_Dcr = '" + doc_disp + "', "
                                + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
                                + " NonDrNeeded = '" + UnLstDr_reqd + "', "
                                + " DCRTime_Entry_Permission = '" + time_dcr + "', "
                                + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
                                + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
                                + " POBType = '" + pob + "', "
                                + " DCRSess_Mand = '" + DCRSess_Mand + "', "
                                + " DCRTime_Mand = '" + DCRTime_Mand + "', "
                                + " DCRProd_Mand = '" + DCRProd_Mand + "', "
                                + " wrk_area_SName='" + wrk_area_SName + "', "
                                + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
                                + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
                                + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
                                + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
                                + " Approval_System = " + iApprovalSystem + ", "
                                + " TpBased='" + strDCRTP + "',"
                                + " Remarks_length_Allowed='" + remarkslength + "',"
                                + " AutoPost_Sunday_Status ='" + iSundayStatus + "', "
                                + " DCRLDR_Remarks ='" + iDrRem + "', "
                                + " DCRNew_Chem ='" + inewchem + "', "
                                + " DCRNew_ULDr ='" + inewudr + "',"
                                + " No_Of_Sl_DoctorsAllowed='" + ListedDr_Allowed + "',"
                                + " No_Of_Sl_ChemistsAllowed='" + Chemist_Allowed + "', Doc_App_Needed='" + iDocApp + "', Doc_Deact_Needed = '" + iDeactApp + "', Add_Deact_Needed = '" + iAddDeact + "', No_Of_Sl_StockistAllowed='" + Stockist_Allowed + "' where Division_Code = '" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);

                if (isdcrsetupupdt == true)
                {
                    strQry = "UPDATE Admin_Setups set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
                }

                iReturn = db.ExecQry(strQry);




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }

        public int RecordUpdate_MGR(int Doc_Count_DCR, int Chem_Count_DCR, int Stk_Count_DCR, int UnLstDr_Count_DCR, int Hos_Count_DCR, int doc_disp, int sess_dcr, int time_dcr, int UnLstDr_reqd, int prod_Qty_zero, int prod_selection, int pob, int DCRSess_Mand, int DCRTime_Mand, int DCRProd_Mand, int iDelayedSystem, int iHolidayCalc, int iDelayAllowDays, int iHolidayStatus, int iSundayStatus, int iApprovalSystem, string Division_Code, int remarkslength, int iDrRem, int inewchem, int inewudr)
        {
            int iReturn = -1;
            int Count = 0;
            DataSet dsadmin = null;
            bool isdcrsetupupdt = false;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";
                bool value = sRecordExistMGR(Division_Code);

                if (value == false)
                {

                    strQry = "Insert into Admin_Setups_MGR (No_Of_DCR_Drs_Count,No_Of_DCR_Chem_Count,No_Of_DCR_Stockist_Count,"
                         + " No_Of_DCR_Ndr_Count, No_of_dcr_Hosp,Doctor_disp_in_Dcr, DCRSess_Entry_Permission, NonDrNeeded, DCRTime_Entry_Permission ,"
                         + " SampleProQtyDefaZero, No_Of_Product_selection_Allowed_in_Dcr, POBType, DCRSess_Mand, DCRTime_Mand, DCRProd_Mand ,"
                         + " DelayedSystem_Required_Status,Delay_Holiday_Status,No_Of_Days_Delay,AutoPost_Holiday_Status,AutoPost_Sunday_Status, Approval_System, Division_Code ,Remarks_length_Allowed,DCRLDR_Remarks,DCRNew_Chem,DCRNew_ULDr) values "
                         + " ('" + Doc_Count_DCR + "' ,'" + Chem_Count_DCR + "','" + Stk_Count_DCR + "', "
                         + " '" + UnLstDr_Count_DCR + "', '" + Hos_Count_DCR + "','" + doc_disp + "', '" + sess_dcr + "','" + UnLstDr_reqd + "', '" + time_dcr + "',"
                         + " '" + prod_Qty_zero + "','" + prod_selection + "','" + pob + "','" + DCRSess_Mand + "','" + DCRTime_Mand + "','" + DCRProd_Mand + "' , "
                         + " '" + iDelayedSystem + "', '" + iHolidayCalc + "', '" + iDelayAllowDays + "','" + iHolidayStatus + "','" + iSundayStatus + "', '" + iApprovalSystem + "', '" + Division_Code + "','" + remarkslength + "','" + iDrRem + "' ,'" + inewchem + "' ,'" + inewudr + "' )";
                }
                else
                {
                    dsadmin = getAdminSetup_MGR(Division_Code);

                    if (sess_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(5).ToString()))
                        isdcrsetupupdt = true;
                    if (time_dcr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(4).ToString()))
                        isdcrsetupupdt = true;
                    if (Doc_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(7).ToString()))
                        isdcrsetupupdt = true;
                    if (Chem_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(6).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(8).ToString()))
                        isdcrsetupupdt = true;
                    if (Stk_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(9).ToString()))
                        isdcrsetupupdt = true;
                    if (Hos_Count_DCR != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(10).ToString()))
                        isdcrsetupupdt = true;
                    if (doc_disp != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(11).ToString()))
                        isdcrsetupupdt = true;
                    if (UnLstDr_reqd != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(12).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_Qty_zero != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(13).ToString()))
                        isdcrsetupupdt = true;
                    if (prod_selection != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(14).ToString()))
                        isdcrsetupupdt = true;
                    if (pob != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(15).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRSess_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(16).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRTime_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(17).ToString()))
                        isdcrsetupupdt = true;
                    if (DCRProd_Mand != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(18).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayedSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(22).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayCalc != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(23).ToString()))
                        isdcrsetupupdt = true;
                    if (iDelayAllowDays != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(24).ToString()))
                        isdcrsetupupdt = true;
                    if (iHolidayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(25).ToString()))
                        isdcrsetupupdt = true;
                    if (iSundayStatus != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(26).ToString()))
                        isdcrsetupupdt = true;
                    if (iApprovalSystem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(27).ToString()))
                        isdcrsetupupdt = true;
                    if (remarkslength != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(19).ToString()))
                        isdcrsetupupdt = true;
                    if (iDrRem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(28).ToString()))
                        isdcrsetupupdt = true;
                    if (inewchem != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(29).ToString()))
                        isdcrsetupupdt = true;
                    if (inewudr != Convert.ToInt16(dsadmin.Tables[0].Rows[0].ItemArray.GetValue(30).ToString()))
                        isdcrsetupupdt = true;

                    strQry = "UPDATE Admin_Setups_MGR " +
                               " SET Doctor_disp_in_Dcr = '" + doc_disp + "', "
                                + " No_Of_DCR_Drs_Count ='" + Doc_Count_DCR + "' ,"
                                + " No_Of_DCR_Chem_Count ='" + Chem_Count_DCR + "', "
                                + " No_Of_DCR_Stockist_Count ='" + Stk_Count_DCR + "', "
                                + " No_Of_DCR_Ndr_Count ='" + UnLstDr_Count_DCR + "', "
                                + " No_of_dcr_Hosp ='" + Hos_Count_DCR + "', "
                                + " DCRSess_Entry_Permission = '" + sess_dcr + "', "
                                + " NonDrNeeded = '" + UnLstDr_reqd + "', "
                                + " DCRTime_Entry_Permission = '" + time_dcr + "', "
                                + " SampleProQtyDefaZero = '" + prod_Qty_zero + "', "
                                + " No_Of_Product_selection_Allowed_in_Dcr = '" + prod_selection + "', "
                                + " POBType = '" + pob + "', "
                                + " DCRSess_Mand = '" + DCRSess_Mand + "', "
                                + " DCRTime_Mand = '" + DCRTime_Mand + "', "
                                + " DCRProd_Mand = '" + DCRProd_Mand + "', "
                                + " DelayedSystem_Required_Status ='" + iDelayedSystem + "', "
                                + " Delay_Holiday_Status ='" + iHolidayCalc + "', "
                                + " No_Of_Days_Delay ='" + iDelayAllowDays + "', "
                                + " AutoPost_Holiday_Status ='" + iHolidayStatus + "', "
                                + " Approval_System = " + iApprovalSystem + ", "
                                + " Remarks_length_Allowed='" + remarkslength + "',"
                                + " AutoPost_Sunday_Status ='" + iSundayStatus + "',"
                                + " DCRLDR_Remarks ='" + iDrRem + "', "
                                + " DCRNew_Chem ='" + inewchem + "', "
                                + " DCRNew_ULDr ='" + inewudr + "' where Division_Code = '" + Division_Code + "' ";
                }


                iReturn = db.ExecQry(strQry);
                // strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strTPApprovalSystem + "' where Designation_Short_Name='DM'";

                if (isdcrsetupupdt == true)
                {
                    strQry = "UPDATE Admin_Setups_MGR set LastUpdt_DCRStp = getdate() where Division_Code = '" + Division_Code + "' ";
                }

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int FlashRecordAdd(string cont1, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Flash_News";
                sl_no = db.Exec_Scalar(strQry);
                if (Flash_RecordExist(div_code))
                {
                    strQry = "update Mas_Flash_News set FN_Cont1 ='" + cont1 + "',FNHome_Page_Flag='" + chkback + "' " +
                      " where FN_Active_Flag=0 and Division_Code='" + div_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Flash_News(Sl_No,FN_Cont1,Division_Code,Created_Date,FN_Active_Flag,FNHome_Page_Flag) " +
                             " VALUES ( " + sl_no + " , '" + cont1 + "' , '" + div_code + "', getdate(),0,'" + chkback + "') ";
                }
                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet Get_Flash_News(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where Division_Code='" + div_code + "' and FN_Active_Flag=0 ";

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
        public DataSet Get_Flash_News_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where Division_Code='" + div_code + "' and FN_Active_Flag=0 and FNHome_Page_Flag=1  ";

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

        public DataSet Get_NB_Record(string div_code, string Start_Date, string End_Date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            strQry = "select Sl_No, NB_Cont1, NB_Cont2, NB_Cont3" +
                " from Mas_Notice_Board " +
                " where Division_Code ='" + div_code + "' and  Start_Date='" + Start_Date.Substring(6, 4) + "-" + Start_Date.Substring(3, 2) + "-" + Start_Date.Substring(0, 2) + "' and  End_Date='" + End_Date.Substring(6, 4) + "-" + End_Date.Substring(3, 2) + "-" + End_Date.Substring(0, 2) + "'";
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
        public DataSet Get_Notice(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            if (div_code != "")
            {
                strQry = "  declare @Date as datetime " +
                      " set @Date=(select max(End_Date)as End_Date  from Mas_Notice_Board where End_Date>=GETDATE() and Division_Code ='" + div_code + "') " +
                      " select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date  from Mas_Notice_Board  where  Division_Code ='" + div_code + "' and " +
                      " NB_Active_Flag=0 and (Start_Date <= GETDATE() and End_Date=@Date) ";
            }
            else
            {
                strQry = "  declare @Date as datetime " +
                       " set @Date=(select max(End_Date)as End_Date  from Mas_Notice_Board where End_Date>=GETDATE() and Division_Code ='" + div_code + "')" +
                       " select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date  from Mas_Notice_Board  where " +
                       " NB_Active_Flag=0 and (Start_Date <= GETDATE() and End_Date=@Date) ";
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

        public int NB_RecordAdd(string cont1, string cont2, string cont3, string start_date, string end_date, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Notice_Board";
                sl_no = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Mas_Notice_Board(Sl_No, NB_Cont1, NB_Cont2, NB_Cont3, Start_Date, End_Date, Division_Code, Created_Date,NB_Active_Flag,NBHome_Page_Flag) " +
                         " VALUES ( " + sl_no + " , '" + cont1 + "', '" + cont2 + "', '" + cont3 + "', '" + start_date.Substring(6, 4) + "-" + start_date.Substring(3, 2) + "-" + start_date.Substring(0, 2) + "', '" + end_date.Substring(6, 4) + "-" + end_date.Substring(3, 2) + "-" + end_date.Substring(0, 2) + "', '" + div_code + "',  getdate(),0,'" + chkback + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //  public int Add_Admin_FieldForce_Setup(string sf_code, string div_code, int iDoctorAdd, int iDoctorEdit, int iDoctorDel, int iDoctorDeAct, int iNewDoctorAdd, 
        //    int iNewDoctorDeAct, int iChemAdd, int iChemEdit, int iChemDeAct, int iTerrAdd, int iTerrEdit, int iTerrDel, int iTerrDeAct)
        //{
        //    int iReturn = -1;

        //    try
        //    {

        //        DB_EReporting db = new DB_EReporting();

        //        int sl_no = -1;
        //        DateTime deactDt = DateTime.Now.AddDays(-1);

        //        strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Admin_FieldForce_Setup";
        //        sl_no = db.Exec_Scalar(strQry);

        //        strQry = " INSERT INTO Admin_FieldForce_Setup(Sl_No, Sf_Code, Division_Code, ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Delete_Option, " +
        //                    " ListedDr_Deactivate_Option, NewDoctor_Entry_OptioninDCR, Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option , " +
        //                    " Territory_Add_Option, Territory_Edit_Option, Territory_Deactivate_Option, Territory_Delete_Option, " +
        //                    " ListedDr_View_Option,Chemist_Delete_Option,Chemist_View_Option, Stockist_Add_Option,Stockist_Deactivate_Option, " +
        //                    " Stockist_Delete_Option,Stockist_Edit_Option,Stockist_View_Option,Territory_View_Option ) " +
        //                 " VALUES ( " + sl_no + " , '" + sf_code + "', '" + div_code + "',  " + iDoctorAdd + ",  " + iDoctorEdit + ",  " + iDoctorDel + ", " +
        //                 " " + iDoctorDeAct + ", " + iNewDoctorAdd + ", " + iChemAdd + ", " + iChemEdit + ", " + iChemDeAct + ", " + iTerrAdd + ", " +
        //                 " " + iTerrEdit + ", " + iTerrDeAct + ", " + iTerrDel + ", 0, 0, 0, 0, 0, 0, 0, 0, 0) ";

        //        iReturn = db.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return iReturn;
        //}
        public int Add_Admin_FieldForce_Setup(string sf_code, string div_code,
            int iDoctorAdd, int iDoctorEdit, int iDoctorDeAct, int iDoctorView,
            int iNewDoctorAdd, int iNewDoctorEdit, int iNewDoctorDeAct, int iNewDoctorView,
            int iChemAdd, int iChemEdit, int iChemDeAct, int iChemView,
            int iTerrAdd, int iTerrEdit, int iTerrDeAct, int iTerrView,
            int iClassAdd, int iClassEdit, int iClassDeAct, int iClassView,
            int iDoctorReAct, int iNewDoctorReAct, int iChemReAct, int iClassReAct, int iDocName)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                int sl_no = -1;
                DateTime deactDt = DateTime.Now.AddDays(-1);

                if (Admin_FieldForce_Setup_RecordExist(sf_code))
                {
                    //Admin_FieldForce_Setup_RecordExist(sf_code);

                    strQry = "SELECT Sl_No FROM Admin_FieldForce_Setup where sf_code = '" + sf_code + "' ";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " UPDATE Admin_FieldForce_Setup " +
                                " SET ListedDr_Add_Option = '" + iDoctorAdd + "', ListedDr_Edit_Option = '" + iDoctorEdit + "' , " +
                                " ListedDr_Deactivate_Option = '" + iDoctorDeAct + "', ListedDr_View_Option = '" + iDoctorView + "', " +
                                " NewDoctor_Entry_Option = '" + iNewDoctorAdd + "', NewDoctor_Edit_Option = '" + iNewDoctorEdit + "', " +
                                " NewDoctor_DeAct_Option = '" + iNewDoctorDeAct + "', NewDoctor_View_Option = '" + iNewDoctorView + "', " +
                                " Chemist_Add_Option = '" + iChemAdd + "', Chemist_Edit_Option = '" + iChemEdit + "' , " +
                                " Chemist_Deactivate_Option = '" + iChemDeAct + "', Chemist_View_Option = '" + iChemView + "', " +
                                " Territory_Add_Option = '" + iTerrAdd + "', Territory_Edit_Option = '" + iTerrEdit + "', " +
                                " Territory_Deactivate_Option = '" + iTerrDeAct + "', Territory_View_Option = '" + iTerrView + "', " +
                                " Class_Add_Option = '" + iClassAdd + "', Class_Edit_Option = '" + iClassEdit + "', " +
                                " Class_Deactivate_Option = '" + iClassDeAct + "', Class_View_Option = '" + iClassView + "', " +
                                " ListedDr_Reactivate_Option ='" + iDoctorReAct + "', " +
                                " NewDoctor_ReAct_Option ='" + iNewDoctorReAct + "'," +
                                " Chemist_Reactivate_Option ='" + iChemReAct + "', " +
                                " Class_Reactivate_Option = '" + iClassReAct + "', Doc_Name_Chg = '" + iDocName + "' " +
                                " WHERE Sl_No = " + sl_no + " and sf_code = '" + sf_code + "' ";
                }
                else
                {
                    strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Admin_FieldForce_Setup";
                    sl_no = db.Exec_Scalar(strQry);

                    strQry = " INSERT INTO Admin_FieldForce_Setup(Sl_No, Sf_Code, Division_Code, " +
                                " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                                " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_DeAct_Option, NewDoctor_View_Option, " +
                                " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                                " Territory_Add_Option, Territory_Edit_Option, Territory_Deactivate_Option, Territory_View_Option, " +
                                " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                                " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg) " +
                             " VALUES ( " + sl_no + " , '" + sf_code + "', '" + div_code + "',  " +
                             iDoctorAdd + ",  " + iDoctorEdit + ",  " + iDoctorDeAct + ", " + iDoctorView + ", " +
                             iNewDoctorAdd + ", " + iNewDoctorEdit + ", " + iNewDoctorDeAct + ", " + iNewDoctorView + ", " +
                             iChemAdd + ", " + iChemEdit + ", " + iChemDeAct + ", " + iChemView + ", " +
                             iTerrAdd + ", " + iTerrEdit + ", " + iTerrDeAct + ", " + iTerrView + ", " +
                             iClassAdd + ", " + iClassEdit + ", " + iClassDeAct + ", " + iClassView + ", " +
                             iDoctorReAct + ", " + iNewDoctorReAct + ", " + iChemReAct + ", " + iClassReAct + ", '" + iDocName + "' ) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getMR_MGR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sf_Code, sf_name, sf_Designation_Short_Name,sf_hq " +
                        "  from Mas_Salesforce " +
                        "  where sf_TP_Active_Flag=0 and sf_type=1 and TP_Reporting_SF = '" + sf_code + "' ";

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

        public DataSet Get_Admin_FieldForce_Setup(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sf_Code, Division_Code , " +
                        " ListedDr_Add_Option, ListedDr_Edit_Option, ListedDr_Deactivate_Option, ListedDr_View_Option, " +
                        " NewDoctor_Entry_Option, NewDoctor_Edit_Option, NewDoctor_Deact_Option, NewDoctor_View_Option, " +
                        " Chemist_Add_Option, Chemist_Edit_Option, Chemist_Deactivate_Option, Chemist_View_Option, " +
                        " Territory_Add_Option,Territory_Edit_Option,Territory_Deactivate_Option,Territory_View_Option, " +
                        " Class_Add_Option, Class_Edit_Option, Class_Deactivate_Option, Class_View_Option, " +
                        " ListedDr_Reactivate_Option, NewDoctor_ReAct_Option, Chemist_Reactivate_Option, Class_Reactivate_Option, Doc_Name_Chg " +
                        "  from dbo.Admin_FieldForce_Setup " +
                        "  where Sf_Code = '" + sf_code + "' and Division_Code='" + div_code + "' ";

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
        public bool Admin_FieldForce_Setup_RecordExist(string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Admin_FieldForce_Setup " +
                         " where sf_code = '" + sf_code + "' ";

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
        public int QuoteAdd(string Quote_Text, string div_code, string chkback)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from Trans_Quote ";
                int Sl_No = db.Exec_Scalar(strQry);
                if (Quote_RecordExist(div_code))
                {
                    strQry = "update Trans_Quote set Quote_Text ='" + Quote_Text + "', Division_Code='" + div_code + "', Home_Page_Flag='" + chkback + "' " +
                        " where Quote_Active_Flag=0";
                }
                else
                {

                    strQry = " INSERT INTO Trans_Quote(Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag) " +
                             " VALUES ('" + Sl_No + "' ,'" + Quote_Text + "' , '" + div_code + "', getdate(),0,'" + chkback + "') ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet Get_Quote(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag " +
                " from Trans_Quote where Quote_Active_Flag=0 and Division_Code='" + div_code + "' ";

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
        public bool Quote_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Trans_Quote " +
                         " where Division_Code = '" + div_code + "' and Quote_Active_Flag=0 ";

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
        public int Delete_Quote(string Quote_Text, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                strQry = "update Trans_Quote set Quote_Active_Flag=1 " +
                 " where Quote_Active_Flag=0 and Quote_Text ='" + Quote_Text + "' and Division_Code = '" + div_code + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return iReturn;
        }
        public DataSet Get_Quote_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,Quote_Text,Division_Code,Created_Date,Quote_Active_Flag,Home_Page_Flag " +
                " from Trans_Quote where Quote_Active_Flag=0 and Division_Code='" + div_code + "' and Home_Page_Flag=1 ";

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

        //Changes Done by Sridevi - Starts
        public DataSet Get_FileUpload(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm " +
            //            "  from file_info " +
            //            "  where div_Code = '" + div_code + "' ";
            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                     "  from file_info " +
                     "  where div_Code = '" + div_code + "'";

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

        public int FileUpload_Add(string div_code, string FileName, string FileSubject)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " INSERT INTO file_info(FileName,FileSubject,Div_Code,Update_dtm) " +
                         " VALUES ( '" + FileName + "' , '" + FileSubject + "', '" + div_code + "',  getdate()) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Core_Doctor_Map_Add(string sf_code, string div_code, string Mgr_Code, string DR_Code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " INSERT INTO Core_Doctor_Map(sf_code,Division_Code,Mgr_Code,DR_Code) " +
                         " VALUES ( '" + sf_code + "' , '" + div_code + "', '" + Mgr_Code + "', '" + DR_Code + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Core_Doctor_Map_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Core_Doctor_Map " +
                         " WHERE sf_code= '" + sf_code + "' and Division_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool Core_Doctor_Map_RecordExist(string Mgr_Code, string sf_code, string div_code, string doc_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(DR_Code) from Core_Doctor_Map " +
                         " where Mgr_Code = '" + Mgr_Code + "' and " +
                         " sf_code = '" + sf_code + "' and " +
                         " Division_code = '" + div_code + "' and " +
                         " DR_Code = '" + doc_code + "' ";

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


        public int Screen_Lock_Add(string mgr_code, string sf_code, string div_code, int DCR_Lock, int TP_Lock, int SDP_Lock, int Camp_Lock, int DR_Lock)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max([S.No])+1,'1') [S.No] from Screen_Lock ";
                int SNo = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Screen_Lock([S.No],Mgr_Code, SF_Code,Div_Code,DCR_Lock,TP_Lock,SDP_Lock,Camp_Lock,DR_Lock,Update_dtm) " +
                         " VALUES ( '" + SNo + "','" + mgr_code + "' , '" + sf_code + "' , '" + div_code + "', " + DCR_Lock + " , " + TP_Lock + ", " + SDP_Lock + ", " + Camp_Lock + ", " + DR_Lock + ", getdate() ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Screen_Lock_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Screen_Lock " +
                         " WHERE Mgr_Code= '" + sf_code + "' and Div_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int Permission_MR_Add(string mgr_code, string sf_code, string div_code, int Level1, int Level2, int Level3)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(pid)+1,'1') pid from Permission_MR ";
                int pid = db.Exec_Scalar(strQry);

                strQry = " INSERT INTO Permission_MR(pid,Mgr_Code, SF_Code,Div_Code,Level1,Level2,Level3,Update_dtm) " +
                         " VALUES ('" + pid + "' ,'" + mgr_code + "' , '" + sf_code + "' , '" + div_code + "', " + Level1 + " , " + Level2 + ", " + Level3 + ", getdate() ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }


        public int Permission_MR_Delete(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " DELETE FROM Permission_MR " +
                         " WHERE Mgr_Code= '" + sf_code + "' and Div_code= '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //Changes Done by Sridevi - Ends

        //changes done by Priya
        public DataSet getNBDate(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT 'Select' NB_Start_End UNION SELECT DISTINCT (convert(varchar(12),Start_Date,103)+' To '+convert(varchar(12),End_Date,103)) NB_Start_End FROM Mas_Notice_Board " +
                       " WHERE NB_Active_Flag=0 AND division_code=  '" + divcode + "' ORDER BY 1 DESC";

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
        public int Delete_Flash(string Flash_Text, string div_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();

            try
            {
                strQry = "update Mas_Flash_News set FN_Active_Flag=1 " +
                 " where FN_Active_Flag=0 and FN_Cont1 ='" + Flash_Text + "' and  Division_Code = '" + div_code + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return iReturn;
        }
        public bool Flash_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Flash_News " +
                         " where Division_Code = '" + div_code + "' and FN_Active_Flag=0 ";

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
        public DataSet Get_Notice_Home(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date,NB_Active_Flag,NBHome_Page_Flag " +
            //  " from Mas_Notice_Board " +
            //  " where Division_Code ='" + div_code + "' and NBHome_Page_Flag=1 and NB_Active_Flag=0 ";

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No, NB_Cont1,NB_Cont2, NB_Cont3,Start_Date,End_Date,NB_Active_Flag,NBHome_Page_Flag " +
             " from Mas_Notice_Board " +
             " where Division_Code ='" + div_code + "' and (Start_Date <= GETDATE() AND End_Date >=GETDATE()) " +
             " and NBHome_Page_Flag=1 and NB_Active_Flag=0 ";

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

        //Included this function to update the listed doctor plan from multi to single thru set up
        //by Sridevi on 12/08/14
        public int RecordUpdate_ListedDR(string terr_code, string doc_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_listeddr " +
                         " SET Territory_Code = '" + terr_code + "' " +
                         " WHERE ListedDrCode = '" + doc_code + "' and Division_code = '" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet get_listed_doctor(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select ListedDrCode , Territory_Code " +
                        "  from mas_listeddr " +
                        "  where Territory_Code is not null and Territory_Code <> '' and Division_code = '" + div_code + "'";

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
        //Changes done by Saravnan
        public DataSet getMoved(string Division_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "select '0' as Move_MailFolder_Id,'' as Move_MailFolder_Name " +
                     "Union ALL " +
                     "select Move_MailFolder_Id,Move_MailFolder_Name from Mas_Mail_Folder_Name where Division_Code = '" + Division_Code + "'";
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

        public DataSet GetDivision(string Division_Code) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT a.sf_code+'-Level1' as sf_mail, a.Sf_Name, a.Sf_UserName, a.sf_Type,'' sf_color," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF,b.Designation_Short_Name," +
                     "a.sf_hq,a.sf_desgn, a.sf_password FROM mas_salesforce a,Mas_SF_Designation b " +
                     "WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 " +
                     "and a.Designation_Code=b.Designation_Code " +
                     "and a.Division_Code = '" + Division_Code + "'  ";

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

        public int ChangeMailDeleteStatus(string sf_code, int mail_id, int status) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' , Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

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

        public int ChangeMailFolder(string sf_code, int mail_id, string ddlMovedFolder, int status) // move
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "update Trans_Mail_detail set open_mail_id = '" + sf_code + "' ,Mail_Moved_to='" + ddlMovedFolder + "', Mail_Active_Flag = " + status + " where Trans_Sl_No = " + mail_id;

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
        //Changes done by Saravanan
        public int get_MailCount(string division_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (division_code.Contains(","))
            {
                division_code = division_code.Remove(division_code.Length - 1);
            }

            int iReturn = -1;
            //strQry = " select count(*) from trans_mail_head a,Trans_Mail_Detail b where a.Trans_Sl_No=b.Trans_Sl_No and " +
            //         " month(a.mail_sent_time)=month(getdate())and year(a.mail_sent_time)=year(getdate()) " +
            //         " and  b.Mail_Active_Flag=0 and a.Mail_SF_To like '%admin%'";
            strQry = " select count(*) from Trans_Mail_Detail b where b.Mail_Active_Flag=0 and Open_Mail_Id like '%admin%' and b.Division_code='" + division_code + "'";

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

        public DataSet FillSalesForce_Level(string des_code, string div_code, string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "")
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                         " a.Designation_Code,b.Desig_Color des_color,'' sf_Type,'' sf_color from Mas_Salesforce a,Mas_SF_Designation b " +
                         " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code  " +
                         " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + div_code + ',' + "%') and Sf_Name !='admin'" +
                         " group by a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                         " a.Designation_Code,b.Desig_Color,sf_code " +
                         " union all" +
                         " select 'admin'+'-Level1' as sf_mail,[USER_NAME] as sf_name,'' as sf_desgn,'' as Sf_HQ,'' as Designation_Short_Name," +
                         " '' as sf_username,'' as Designation_Code,'' as des_color,'' sf_Type,'' sf_color from Mas_HO_ID_Creation where division_code like '%" + div_code + "%' " +
                         " order by b.Designation_Short_Name desc";
            }
            else
            {
                strQry = "select a.sf_code+'-Level1' as sf_mail, a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                        " a.Designation_Code,b.Desig_Color des_color,'' sf_Type,'' sf_color from Mas_Salesforce a,Mas_SF_Designation b " +
                        " where a.Designation_Code in (" + des_code + ")  and a.sf_TP_Active_Flag=0 and a.Designation_Code=b.Designation_Code  " +
                        " group by a.sf_name,a.sf_desgn,a.Sf_HQ,b.Designation_Short_Name,a.sf_username, " +
                        " a.Designation_Code,b.Desig_Color,sf_code " +
                        " union all " +
                        " select 'admin'+'-Level1' as sf_mail,[USER_NAME] as sf_name,'' as sf_desgn,'' as Sf_HQ,'' as Designation_Short_Name," +
                        " '' as sf_username,'' as Designation_Code,'' as des_color,'' sf_Type,'' sf_color from Mas_HO_ID_Creation " +
                        " order by b.Designation_Short_Name desc";
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
        //Changes done by Priya
        public DataSet FillLeave_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "SELECT 0 as Leave_code,'--Select--' as Leave_SName,'' as Leave_Name " +
                       " UNION " +
                       " SELECT Leave_code,Leave_SName,Leave_Name " +
                       " FROM mas_Leave_Type where Active_Flag = 0 and Division_Code='" + div_code + "' ";



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
        public int Insert_Leave(string Leave_Type, DateTime From_Date, DateTime To_Date, string Reason, string Address, string No_of_Days, string Inform_by, string Valid_Reason, string sf_code, string Division_Code, string Informed_Ho)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Leave_Id)+1,'1') Leave_Id from mas_Leave_Form ";
                int Leave_Id = db.Exec_Scalar(strQry);

                string leave_From_Date = From_Date.Month.ToString() + "-" + From_Date.Day + "-" + From_Date.Year;
                string leave_To_Date = To_Date.Month.ToString() + "-" + To_Date.Day + "-" + To_Date.Year;

                strQry = " INSERT INTO mas_Leave_Form(Leave_Id,Leave_Type, From_Date,To_Date,Reason,Address,No_of_Days,Inform_by,Valid_Reason,Leave_Active_Flag,Created_Date,sf_code,Division_Code,Informed_Ho) " +
                         " VALUES ('" + Leave_Id + "', '" + Leave_Type + "' , '" + leave_From_Date + "' , '" + leave_To_Date + "', '" + Reason + "' , '" + Address + "', " +
                         " '" + No_of_Days + "','" + Inform_by + "','" + Valid_Reason + "',2, getdate(),'" + sf_code + "','" + Division_Code + "','" + Informed_Ho + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getLeave_approve(string sfcode, int iVal, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,a.sf_Designation_Short_Name as Designation_Short_Name,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id " +
                     " from Mas_Salesforce a,  mas_Leave_Form c , Mas_Salesforce_AM d" +
                     " where d.Leave_AM = '" + sfcode + "' and a.Sf_Code = d.Sf_Code and  a.Sf_Code = c.Sf_Code  and c.Leave_Active_Flag = '" + iVal + "' and c.Division_code in('" + div_code + "')";
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

        public DataSet getLeave_Cancel(string sfcode, string FYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getLeaveCancel '" + sfcode + "','" + FYear + "'";

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



        public DataSet getLeaveApproval(string sf_code,string DivCode)
        {

            DataSet dsAdmin = null;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "select * from vwLeave where Reporting_To_SF='" + sf_code + "' AND Division_Code='" + DivCode + "'";
                dsAdmin = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet getLeave(string sf_code, string Leave_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select a.Leave_Id,a.Leave_Type, convert(varchar(12), a.From_Date,103) From_Date, convert(varchar(12),a.To_Date,103) To_Date,a.Reason,a.Address,a.No_of_Days,a.Inform_by,a.Valid_Reason,a.Informed_Ho " +
                //  " b.Designation_Name,c.sf_name+' - '+b.Designation_Short_Name+' - '+ sf_HQ sf_name,c.sf_emp_id " +
                     " from  mas_Leave_Form a " +
                     " where a.sf_code = '" + sf_code + "' and a.Leave_Id = '" + Leave_Id + "' ";
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

        public DataSet GetStatus(string sf_code, string FromMonth, string FromYear, string ToMonth, string ToYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select a.Leave_Id,a.Leave_Type, Convert(varchar(12), a.From_Date,103) From_Date, Convert(varchar(12),a.To_Date,103) To_Date , a.Address, a.Reason,a.No_of_Days,c.Designation_Short_Name, " +
                     " a.Valid_Reason,b.sf_code, b.Sf_Name,b.sf_HQ,b.sf_emp_id,  " +
                     " (select sf_name from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_To_SF," +
                     " CASE a.Leave_Active_Flag when '0' then 'Approved' when '2' then 'Pending' when '4' then 'Reject' end Leave_Active_Flag " +
                     " from mas_Leave_Form a, mas_salesforce b, Mas_SF_Designation c " +
                     " where a.sf_code = '" + sf_code + "' and b.Designation_Code=c.Designation_Code " +
                     " and a.sf_code = b.Sf_Code and " +
                     " (year(a.From_Date) >= '" + FromYear + "' and year(a.To_Date) <= '" + ToYear + "' ) " +
                     " and (MONTH(a.From_Date) >='" + FromMonth + "' and MONTH(a.To_Date) <= '" + ToMonth + "') ";

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

        public int Leave_Appprove(string sf_code, string Leave_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DCR dc = new DCR();
            int iReturn = -1;

            strQry = " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate()" +
                     "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);
                // Added  by Sridevi - To Update DCR - for Leave
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db_ER.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            int Trans_SlNo = db_ER.Exec_Scalar(strQry);
                            if (Trans_SlNo > 0)
                            {
                                strQry = "UPDATE DCRMain_Temp  SET Confirmed = 1 ,ReasonforRejection = '' " +
                                    " WHERE Sf_Code = '" + sf_code + "' and Trans_SlNo =" + Trans_SlNo + "";

                                iReturn = db_ER.ExecQry(strQry);

                                int iretmain = dc.Create_DCRHead_Trans(sf_code, Trans_SlNo);
                            }
                            Leave_From = Leave_From.AddDays(1);
                        }
                    }
                }
                // Code Changes Ends - To Update DCR - for Leave
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //public int Leave_Appprove(string sf_code, string Leave_Id)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;

        //    strQry = " update mas_Leave_Form set Leave_Active_Flag = 0 ,LastUpdt_Date= getdate()" +
        //             "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2  and Leave_Id = '" + Leave_Id + "'";

        //    try
        //    {
        //        iReturn = db_ER.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}
        //Changes done by Priya

        public DataSet FillWorkArea()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry =
                       " SELECT WorkArea_Code,wrk_area_Name,wrk_area_SName " +
                       " FROM Mas_WorkArea_Type " +
                       " ORDER BY 2";


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

        //Changes done by saravanan
        public int RecordUpdate_AdminSetUp_MGR(string strGMValue, string strSMValue, string strDMValue, string strSZValue, string strZSMValue,
                                               string strRSMValue, string strASMValue, string strDGMValue)
        {
            int iReturn = -1;
            int Count = 0;

            try
            {

                DB_EReporting db = new DB_EReporting();

                //strQry = "UPDATE Admin_Setups " +
                //            " SET SingleDr_WithMultiplePlan_Required= '" + Doc_MulPlan + "',Wrk_Area_Name='" + strWorkAra + "',No_of_TP_View='" + strNoofTPView + "'";

                //                bool value = sRecordExistMGR();

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strGMValue + "' where Designation_Short_Name='GM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strSMValue + "' where Designation_Short_Name='SM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strDMValue + "' where Designation_Short_Name='DM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strSZValue + "' where Designation_Short_Name='SZ'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strZSMValue + "' where Designation_Short_Name='ZSM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strRSMValue + "' where Designation_Short_Name='RSM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strASMValue + "' where Designation_Short_Name='ASM'";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + strDGMValue + "' where Designation_Short_Name='DGM'";

                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Get_Flash_News_adm(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,FN_Cont1,Division_Code,created_Date,FN_Active_Flag,FNHome_Page_Flag " +
                " from Mas_Flash_News where FN_Active_Flag=0 and Division_Code = '" + div_code + "'";

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
        //Added by sridevi
        public DataSet getMR_MGR_New(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select Sf_Code, sf_name, sf_Designation_Short_Name,sf_hq " +
            //            "  from Mas_Salesforce " +
            //            "  where sf_TP_Active_Flag=0 and sf_type=1 and TP_Reporting_SF = '" + sf_code + "' ";

            strQry = "EXEC sp_get_Rep_access  '" + sf_code + "', '" + div_code + "' ";
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
        //Changes done by Saravanan
        public int RecordUpdate_DesigMR(int StrId, string strDesignation)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_SF_Designation set TP_Approval_Sys='" + StrId + "' where Designation_Short_Name='" + strDesignation + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //public DataSet getLeave_Reject(string sfcode, int iVal)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id,c.Reason, c.Approved_to, c.Leave_App_Mgr   " +
        //             " from Mas_Salesforce a, mas_Leave_Form c " +
        //             " where a.Sf_Code = '" + sfcode + "' and  a.Sf_Code = c.Sf_Code and c.Leave_Active_Flag = '" + iVal + "'";
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
        public DataSet getLeave_Reject(string sfcode, int iVal)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select distinct a.Sf_Code,a.Sf_Name,a.Sf_HQ,a.sf_emp_id,c.No_of_Days,convert(varchar(10),c.From_Date,103) From_Date,convert(varchar(10),c.To_Date,103) To_Date, c.Leave_Id,c.Reason, c.Approved_to, c.Leave_App_Mgr, c.Rejected_Reason  " +
                     " from Mas_Salesforce a, mas_Leave_Form c " +
                     " where a.Sf_Code = '" + sfcode + "' and  a.Sf_Code = c.Sf_Code and c.Leave_Active_Flag = '" + iVal + "'";
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
        public int Leave_Reject_Mgr(string sf_code, string sf_name, string Leave_Id, string reject)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadmn;
            DateTime Leave_From = DateTime.Now;
            DateTime Leave_To = DateTime.Now;
            DCR_New dc = new DCR_New();
            int iReturn = -1;

            strQry = " update mas_Leave_Form set Leave_Active_Flag = 1 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + sf_name + "', Rejected_Reason = '" + reject + "'" +
                     "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2 and Leave_Id='" + Leave_Id + "'";

            try
            {
                iReturn = db_ER.ExecQry(strQry);

                // Added  by Sridevi - To Update DCR - for Leave
                if (iReturn > 0)
                {
                    strQry = "select From_Date,TO_Date from mas_Leave_Form where sf_code = '" + sf_code + "' and Leave_Id = '" + Leave_Id + "'";

                    dsadmn = db_ER.Exec_DataSet(strQry);

                    if (dsadmn.Tables[0].Rows.Count > 0)
                    {
                        Leave_From = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        Leave_To = Convert.ToDateTime(dsadmn.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        while (Leave_From <= Leave_To)
                        {
                            DateTime dcrdate = Leave_From;
                            string Leave_Date = dcrdate.ToString("MM/dd/yyyy");
                            strQry = "SELECT Trans_SlNo FROM DCRMain_Temp WHERE   Sf_Code = '" + sf_code + "' and Activity_Date ='" + Leave_Date + "' ";
                            int Trans_SlNo = db_ER.Exec_Scalar(strQry);
                            if (Trans_SlNo > 0)
                            {
                                int iretmain = dc.Reject_DCR(sf_code, Trans_SlNo, reject);
                            }

                            Leave_From = Leave_From.AddDays(1);
                        }
                    }
                }
                // Code Changes Ends - To Update DCR - for Leave
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //public int Leave_Reject_Mgr(string sf_code, string sf_name, string Leave_Id)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;

        //    strQry = " update mas_Leave_Form set Leave_Active_Flag = 1 ,LastUpdt_Date= getdate(), Leave_App_Mgr ='" + sf_name + "'" +
        //             "  where sf_code = '" + sf_code + "' and Leave_Active_Flag=2 and Leave_Id='" + Leave_Id + "'";

        //    try
        //    {
        //        iReturn = db_ER.ExecQry(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}



        public int get_Mail_MR_MGR_Count(string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            //strQry = "select count(*) from trans_mail_head a,Trans_Mail_Detail b where a.Trans_Sl_No=b.Trans_Sl_No and " +
            //         " month(a.mail_sent_time)=month(getdate())and year(a.mail_sent_time)=year(getdate()) " +
            //         " and  b.Mail_Active_Flag=0 and a.Mail_SF_To like'%" + sf_Code + "%'";
            strQry = " select count(*) from Trans_Mail_Detail b where b.Mail_Active_Flag=0 and Open_Mail_Id like '%" + sf_Code + "%'";
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

        //Changes done by Priya
        public int AddQuery(string Querytype, string Query, string div_code, string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Query_Id)+1,'1') Query_Id from Mas_Query_Box ";
                int Query_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Query_Box(Query_Id,Query_Type,Query_Text,Created_Date,Division_Code,sf_code,Query_Active_Flag)" +
                         "values('" + Query_Id + "','" + Querytype + "', '" + Query + "',getdate(),'" + div_code + "','" + sf_code + "',0) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iReturn;
        }
        public DataSet getQuery_List(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date, a.Completed_Date  " +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.Division_Code = '" + div_code + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code and a.Query_Active_Flag = 0" +
                        "  order by Created_Date desc ";

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

        public DataSet getQuery_Reportingto(string sf_code, string Query_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date,  " +
                     " (select sf_name from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_To_SF," +
                       " (select Sf_UserName from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_User," +
                         " (select Sf_Password from mas_salesforce where sf_code=b.Reporting_To_SF) as Reporting_Pass" +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.sf_code = '" + sf_code + "' and a.Query_Id = '" + Query_Id + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code ";

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
        //done by reshmi
        public DataSet getMailFolderName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT m.Move_MailFolder_Id,m.Move_MailFolder_Name ," +
                     " (select count(d.Mail_Moved_to) from Trans_Mail_Detail d where  d.Mail_Moved_to = m.Move_MailFolder_Name and d.Division_code=m.Division_Code) as mail_count" +
                     " From Mas_Mail_Folder_Name m " +
                     "WHERE m.Division_Code='" + div_code + "' and Folder_Act_flag=0 " +
                     "ORDER BY Move_MailFolder_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public DataSet getTypeName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT m.Move_MailFolder_Id,m.Move_MailFolder_Name ," +
                     " (select count(d.Mail_Moved_to) from Trans_Mail_Detail d where  d.Mail_Moved_to = m.Move_MailFolder_Name and d.Division_code=m.Division_Code) as mail_count" +
                     " From Mas_Multi_Unit_Entry m " +
                     "WHERE m.Division_Code='" + div_code + "' and Folder_Act_flag=0 " +
                     "ORDER BY Move_MailFolder_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int MailUpdate(int MailFolder_Id, string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                          " SET Move_MailFolder_Name = '" + MailFolder_Name + "'" +
                          " WHERE Move_MailFolder_Id = '" + MailFolder_Id + "' and Division_Code='" + div_code + "' and Folder_Act_flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public int TypeUpdate(int MailFolder_Id, string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_Multi_Unit_Entry " +
                          " SET Move_MailFolder_Name = '" + MailFolder_Name + "'" +
                          " WHERE Move_MailFolder_Id = '" + MailFolder_Id + "' and Division_Code='" + div_code + "' and Folder_Act_flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RecordAdd(string MailName, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Mail_Folder_Name ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Mail_Folder_Name (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                        "values('" + Move_MailFolder_Id + "','" + MailName + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int RecordAdded(string MailName, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Multi_Unit_Entry ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Multi_Unit_Entry (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                        "values('" + Move_MailFolder_Id + "','" + MailName + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeleteMail_Id(int Mail_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();


                strQry = "DELETE FROM Mas_Mail_Folder_Name Where Move_MailFolder_Id='" + Mail_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        //Changes done by Priya

        public int Query_Com(int Query_Id, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Query_Box " +
                             " SET Query_Active_Flag=1 , " +
                             " LastUpdt_Date = getdate(), Completed_Date = getdate()" +
                             " WHERE Query_Id = '" + Query_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getQuery_List_com(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Query_Id, a.Query_Type, a.Query_Text, a.Division_Code, a.sf_code,b.sf_name,b.Sf_UserName,b.Sf_Password,c.Division_Name,a.created_date,a.Completed_Date  " +
                      " from Mas_Query_Box a, mas_salesforce b, mas_division c " +
                      " where a.Division_Code = '" + div_code + "' and a.sf_code = b.sf_code and a.Division_Code = c.Division_Code and a.Query_Active_Flag =1";

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

        public DataSet Get_talktous(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "select Sl_No,TalktoUs_Text,Division_Code,Created_Date " +
                " from mas_Talk_to_Us where Talk_Active_Flag=0 and Division_Code='" + div_code + "' ";

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

        public int talkAdd(string talk_Text, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(Sl_No)+1,'1') Sl_No from mas_Talk_to_Us ";
                int Sl_No = db.Exec_Scalar(strQry);
                if (talk_RecordExist(div_code))
                {
                    strQry = "update mas_Talk_to_Us set TalktoUs_Text ='" + talk_Text + "' " +
                        " where Talk_Active_Flag=0 and Division_Code='" + div_code + "'";
                }
                else
                {

                    strQry = " INSERT INTO mas_Talk_to_Us(Sl_No,TalktoUs_Text,Division_Code,Created_Date,Talk_Active_Flag) " +
                             " VALUES ( '" + Sl_No + "','" + talk_Text + "' , '" + div_code + "', getdate(),0) ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public bool talk_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from mas_Talk_to_Us " +
                         " where Division_Code = '" + div_code + "' and Talk_Active_Flag=0 ";

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
        public DataSet getHomePage_Restrict(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " Select DCR_Home,TP_Home,Leave_Home, Expense_Home, Listeddr_Add_Home, Listeddr_Deact_Home, Listeddr_Add_Deact_Home, SS_Entry_Home, Doctor_Ser_Home " +
                     " from Mas_Home_Page_Restrict where Division_Code = '" + divcode + "'";

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

        public int Home_Restrict(int DCR_Home, int TP_Home, int Leave_Home, int Expense_Home, int Listeddr_Add_Home, int Listeddr_Deact_Home, int Listeddr_Add_Deact_Home, int SS_Entry_Home, int Doctor_Ser_Home, string divcode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                // int sl_no = -1;
                // DateTime deactDt = DateTime.Now.AddDays(-1);

                //strQry = "SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_Notice_Board";
                //sl_no = db.Exec_Scalar(strQry);
                if (HomePageRes_RecordExist(divcode))
                {
                    strQry = " update Mas_Home_Page_Restrict set DCR_Home ='" + DCR_Home + "', TP_Home = '" + TP_Home + "', Leave_Home = '" + Leave_Home + "', Expense_Home = '" + Expense_Home + "'," +
                             " Listeddr_Add_Home = '" + Listeddr_Add_Home + "', Listeddr_Deact_Home ='" + Listeddr_Deact_Home + "', Listeddr_Add_Deact_Home = '" + Listeddr_Add_Deact_Home + "', SS_Entry_Home = '" + SS_Entry_Home + "', Doctor_Ser_Home = '" + Doctor_Ser_Home + "'" +
                             " where  Division_Code='" + divcode + "'";
                }
                else
                {

                    strQry = " INSERT INTO Mas_Home_Page_Restrict(DCR_Home, TP_Home, Leave_Home, Expense_Home, Listeddr_Add_Home,Listeddr_Deact_Home, Listeddr_Add_Deact_Home,SS_Entry_Home, Doctor_Ser_Home,Division_Code, Created_Date) " +
                             " VALUES ( '" + DCR_Home + "' , '" + TP_Home + "', '" + Leave_Home + "', '" + Expense_Home + "', '" + Listeddr_Add_Home + "', '" + Listeddr_Deact_Home + "', '" + Listeddr_Add_Deact_Home + "','" + SS_Entry_Home + "','" + Doctor_Ser_Home + "', '" + divcode + "',  getdate()) ";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public bool HomePageRes_RecordExist(string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Mas_Home_Page_Restrict " +
                         " where Division_Code = '" + div_code + "'  ";

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

        //done by resh for App
        //start
        public DataSet getFeedback(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT Feedback_Id,Feedback_Content From Mas_App_CallFeedback " +
                     "WHERE Division_Code='" + div_code + "' and Act_Flag=0 " +
                     " ORDER BY Feedback_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public int FeedbackAdd(string Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "INSERT INTO Mas_App_CallFeedback (Feedback_Content,Division_Code,Act_Flag)" +
                         "values('" + Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int FeedbackUpdate(int Feedback_Id, string Feedback_Content, string div_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_App_CallFeedback " +
                          " SET Feedback_Content = '" + Feedback_Content + "'" +
                          " WHERE Feedback_Id = '" + Feedback_Id + "' and Division_Code='" + div_code + "' and Act_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int FeedbackDelete(string Feedback_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Delete From Mas_App_CallFeedback " +
                          " WHERE Feedback_Id = '" + Feedback_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getRemarks(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdm = null;

            if (div_code.Contains(','))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            strQry = "SELECT Remarks_Id,Remarks_Content,type From Mas_App_CallRemarks " +
                     "WHERE Division_Code='" + div_code + "' and Act_Flag=0 " +
                     " ORDER BY Remarks_Id ";

            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int RemarksAdd(string Name, string div_code, string type)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "INSERT INTO Mas_App_CallRemarks (Remarks_Content,Division_Code,Act_Flag,type)" +
                         "values('" + Name + "' , '" + div_code + "',0,'" + type + "')";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int RemarksUpdate(int Remarks_Id, string Remarks_Content, string div_code, string type)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                strQry = "UPDATE Mas_App_CallRemarks " +
                          " SET Remarks_Content = '" + Remarks_Content + "', type='" + type + "'" +
                          " WHERE Remarks_Id = '" + Remarks_Id + "' and Division_Code='" + div_code + "' and Act_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public int RemarksDelete(string Remarks_Id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Delete From Mas_App_CallRemarks " +
                          " WHERE Remarks_Id = '" + Remarks_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int DeActMail_Id(int Mail_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                         " SET Folder_Act_flag=1 " +
                         " WHERE Move_MailFolder_Id = '" + Mail_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int DeActtype_Id(int Mail_Id)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_Multi_Unit_Entry " +
                         " SET Folder_Act_flag=1 " +
                         " WHERE Move_MailFolder_Id = '" + Mail_Id + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int MailAdd(string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Mail_Folder_Name ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Mail_Folder_Name (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                         "values('" + Move_MailFolder_Id + "','" + MailFolder_Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int TypeAdd(string MailFolder_Name, string div_code)
        {
            int iReturn = -1;

            try
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(Move_MailFolder_Id)+1,'1') Move_MailFolder_Id from Mas_Multi_Unit_Entry ";
                int Move_MailFolder_Id = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Multi_Unit_Entry (Move_MailFolder_Id,Move_MailFolder_Name,Division_Code,Folder_Act_flag)" +
                         "values('" + Move_MailFolder_Id + "','" + MailFolder_Name + "' , '" + div_code + "',0)";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        //start for mail remaining
        public DataSet getMail_TransFrom(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = "SELECT 0 as Move_MailFolder_Id,'--Select--' as Move_MailFolder_Name " +
                     " UNION " +
                     " SELECT Move_MailFolder_Id,Move_MailFolder_Name FROM  Mas_Mail_Folder_Name " +
                     " WHERE Folder_Act_flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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
        public DataSet getMail_TransTo(string divcode, string MailFolder_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "SELECT 0 as Move_MailFolder_Id,'--Select--' as Move_MailFolder_Name " +
                     " UNION " +
                     " SELECT Move_MailFolder_Id,Move_MailFolder_Name FROM  Mas_Mail_Folder_Name " +
                     " WHERE Folder_Act_flag=0 AND Division_Code=  '" + divcode + "' and Move_MailFolder_Name!='" + MailFolder_Name + "'  " +
                     " ORDER BY 2";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public DataSet getMailcount(string Move_MailFolder_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCls = null;

            strQry = "select COUNT(d.Mail_Moved_to) as Move_MailFolder_Id from Trans_Mail_Detail d,Mas_Mail_Folder_Name m " +
                      "where d.Mail_Moved_to = m.Move_MailFolder_Name and d.Division_code=m.Division_Code  and m.Move_MailFolder_Id = '" + Move_MailFolder_Id + "' and m.Folder_Act_flag=0";
            try
            {
                dsDocCls = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDocCls;
        }
        public int Updatmail(string Trans_From, string Trans_To, string Trans_FromName, string Trans_ToName, string chkdel, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Trans_Mail_Detail set Mail_Moved_to= '" + Trans_ToName + "' where Mail_Moved_to= '" + Trans_FromName + "' and Division_Code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Mail_Folder_Name " +
                       " SET Folder_Act_flag = '" + chkdel + "' " +
                       " WHERE Move_MailFolder_Id = '" + Trans_From + "' and Folder_Act_flag=0 and Division_Code='" + div_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet Get_Block_Reason(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sf_blkreason " +
                " from Mas_Salesforce where SF_Status=1 and Division_Code = '" + div_code + "' and sf_code = '" + sf_code + "'";

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

        //Done by Reshmi-- start
        public DataSet getWorkTye(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            if (ddltype == 1)
            {

                strQry = "Select WorkType_Code_B,Worktype_Name_B,WType_SName,WorkType_Orderly,TP_Flag,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                          " From Mas_WorkType_BaseLevel where Division_Code='" + div_code + "' and active_flag=0 " +
                          "ORDER BY WorkType_Orderly ";
            }
            else if (ddltype == 2)
            {
                strQry = "Select WorkType_Code_M WorkType_Code_B,Worktype_Name_M Worktype_Name_B,WType_SName,WorkType_Orderly,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Designation_Code" +
                        " FROM Mas_WorkType_Mgr where Division_Code='" + div_code + "' and active_flag=0 " +
                        "ORDER BY WorkType_Orderly ";
            }

            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }

        public int Wrk_RecordUpdate(int WorkType_Code, string WType_SName, string WorkType_Orderly, string TP_Flag, string TP_DCR, string Place_Involved, string Button_Access, string FieldWork_Indicator, string divcode, int ddltype, string Designation_short_name, string Designation_Code)
        {
            int iReturn = -1;
            //if (!RecordExistWork_Slno(WorkType_Code, WorkType_Orderly, divcode,ddltype))
            //{
            if (!RecordExistWrk_S(WorkType_Code, WType_SName, divcode, ddltype))
            {

                try
                {
                    if (ddltype == 1)
                    {


                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_WorkType_BaseLevel " +
                                 " SET WType_SName = '" + WType_SName + "' ," +
                                 " WorkType_Orderly = '" + WorkType_Orderly + "' ," +
                                 " TP_Flag = '" + TP_Flag + "' ," +
                                 " TP_DCR = '" + TP_DCR + "' ," +
                                 " Place_Involved = '" + Place_Involved + "' ," +
                                 " Button_Access = '" + Button_Access + "' ," +
                                 " FieldWork_Indicator = '" + FieldWork_Indicator + "' ," +
                                 " Designation_Short_Name = '" + Designation_short_name + "' ," +
                                 " Designation_Code ='" + Designation_Code + "' " +
                                 " WHERE WorkType_Code_B = '" + WorkType_Code + "' and Division_Code='" + divcode + "' and active_flag = 0 ";

                        iReturn = db.ExecQry(strQry);
                    }
                    else if (ddltype == 2)
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_WorkType_Mgr " +
                                 " SET WType_SName ='" + WType_SName + "' ," +
                                 " WorkType_Orderly = '" + WorkType_Orderly + "', " +
                            //" TP_Flag = '" + TP_Flag + "' ," +
                                 " TP_DCR = '" + TP_DCR + "' ," +
                                 " Place_Involved = '" + Place_Involved + "' ," +
                                 " Button_Access = '" + Button_Access + "' ," +
                                 " FieldWork_Indicator = '" + FieldWork_Indicator + "' ," +
                                 " Designation_Short_Name = '" + Designation_short_name + "' ," +
                                 " Designation_Code ='" + Designation_Code + "' " +
                                 " WHERE WorkType_Code_M = '" + WorkType_Code + "' and Division_Code ='" + divcode + "' and active_flag = 0";

                        iReturn = db.ExecQry(strQry);
                    }
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
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }

        public bool RecordExistWrk_S(int WorkType_Code, string WType_SName, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_BaseLevel WHERE WType_SName = '" + WType_SName + "' AND WorkType_Code_B!='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_Mgr WHERE WType_SName ='" + WType_SName + "' AND WorkType_Code_M !='" + WorkType_Code + "' AND Division_Code = '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool RecordExistWork_Slno(int WorkType_Code, string WorkType_Orderly, string divcode, int ddltype)
        {
            bool bRecordExist = false;

            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WorkType_Orderly) FROM Mas_WorkType_BaseLevel Where WorkType_Orderly ='" + WorkType_Orderly + "'AND WorkType_Code_B!='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WorkType_Orderly) FROM Mas_WorkType_Mgr where WorkType_Orderly ='" + WorkType_Orderly + "' AND WorkType_Code_M !='" + WorkType_Code + "' AND Division_Code ='" + divcode + "' ";
                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public DataSet getTp_Flag(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            strQry = "Select distinct TP_Flag from Mas_WorkType_BaseLevel where Division_Code= '" + div_code + "'";
            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }
        public DataSet get_WorkType_Code(string WorkType_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            strQry = "Select Worktype_Name_B,WType_SName ,WorkType_Orderly,TP_Flag,TP_DCR ,Place_Involved,Button_Access,FieldWork_Indicator,WorkType_Code_B " +
                     "From Mas_WorkType_BaseLevel WHERE active_flag =0 and WorkType_Code_B ='" + WorkType_Code + "' " +
                     "ORDER BY 2";

            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsadm;
        }
        public DataSet getFieldWork_Indicator(string div_code, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsadm = null;

            if (ddltype == 1)
            {

                strQry = "Select distinct FieldWork_Indicator from Mas_WorkType_BaseLevel where Division_Code='" + div_code + "'";
            }
            else if (ddltype == 2)
            {
                strQry = "Select distinct FieldWork_Indicator from Mas_WorkType_Mgr where Division_Code='" + div_code + "'";
            }
            try
            {
                dsadm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsadm;
        }

        public DataSet getWrkTP_Indicator(string WorkType_Code, string divcode, int ddltype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            if (ddltype == 1)
            {

                strQry = " SELECT TP_Flag,TP_DCR,Place_Involved,FieldWork_Indicator,Button_Access,Designation_Short_Name FROM  Mas_WorkType_BaseLevel " +
                         " WHERE WorkType_Code_B='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
            }
            else if (ddltype == 2)
            {

                strQry = " SELECT '',TP_DCR,Place_Involved,FieldWork_Indicator,Button_Access,Designation_Short_Name FROM  Mas_WorkType_Mgr " +
                         " WHERE WorkType_Code_M ='" + WorkType_Code + "' AND Division_Code= '" + divcode + "' ";
            }
            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public int Addwrktype(string name, string short_name, string order, string TpFlag, string TpDcr, string place_involved, string butt, string Indicator, string div_code, int ddltype, string designation_code, string desig_short_name)
        {

            int iReturn = -1;


            if (!RecordExistWrk_Name(name, div_code, ddltype))
            {

                if (!RecordExistWrk_Short(short_name, div_code, ddltype))
                {
                    try
                    {
                        //if (div_code.Contains(','))
                        //{ 
                        //    div_code = div_code.Remove(div_code.Length - 1);
                        //}


                        if (ddltype == 1)
                        {

                            DB_EReporting db = new DB_EReporting();

                            strQry = "SELECT isnull(max(WorkType_Code_B)+1,'1') WorkType_Code_B from Mas_WorkType_BaseLevel ";
                            int WorkType_Code_B = db.Exec_Scalar(strQry);

                            strQry = "INSERT INTO Mas_WorkType_BaseLevel (WorkType_Code_B,Worktype_Name_B,WType_SName,WorkType_Orderly,TP_Flag,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Division_Code,active_flag,Designation_Code,Designation_Short_Name)" +
                                     "values('" + WorkType_Code_B + "','" + name + "' ,'" + short_name + "','" + order + "','" + TpFlag + "','" + TpDcr + "','" + place_involved + "','" + butt + "','" + Indicator + "', '" + div_code + "',0,'" + designation_code + "','" + desig_short_name + "')";


                            iReturn = db.ExecQry(strQry);
                        }
                        else if (ddltype == 2)
                        {
                            DB_EReporting db = new DB_EReporting();

                            strQry = "SELECT isnull(max(WorkType_Code_M)+1,'1') WorkType_Code_M from Mas_WorkType_Mgr ";
                            int WorkType_Code_M = db.Exec_Scalar(strQry);

                            strQry = "INSERT INTO Mas_WorkType_Mgr (WorkType_Code_M,WorkType_Name_M,WType_SName,WorkType_Orderly,TP_DCR,Place_Involved,Button_Access,FieldWork_Indicator,Division_Code,active_flag,Designation_Code,Designation_Short_Name)" +
                                     "values('" + WorkType_Code_M + "','" + name + "' ,'" + short_name + "','" + order + "','" + TpDcr + "','" + place_involved + "','" + butt + "','" + Indicator + "', '" + div_code + "',0,'" + designation_code + "','" + desig_short_name + "')";


                            iReturn = db.ExecQry(strQry);
                        }
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

        public bool RecordExistWrk_Short(string short_name, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_BaseLevel WHERE WType_SName = '" + short_name + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(WType_SName) FROM Mas_WorkType_Mgr WHERE WType_SName ='" + short_name + "' AND Division_Code = '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public bool RecordExistWrk_Name(string name, string divcode, int ddltype)
        {

            bool bRecordExist = false;
            try
            {
                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(Worktype_Name_B) FROM Mas_WorkType_BaseLevel WHERE Worktype_Name_B = '" + name + "' AND Division_Code= '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT COUNT(Worktype_Name_M) FROM Mas_WorkType_Mgr WHERE Worktype_Name_M ='" + name + "' AND Division_Code = '" + divcode + "' ";

                    int iRecordExist = db.Exec_Scalar(strQry);

                    if (iRecordExist > 0)
                        bRecordExist = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }

        public int DeactivateWorktype(string worktype_Code, int ddltype)
        {
            int iReturn = -1;

            try
            {

                if (ddltype == 1)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_BaseLevel " +
                                " SET active_flag=1  " +
                                " WHERE WorkType_Code_B = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                else if (ddltype == 2)
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_WorkType_Mgr " +
                               " SET active_flag=1  " +
                               " WHERE WorkType_Code_M = '" + worktype_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Get_UserManual(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm " +
            //            "  from file_info " +
            //            "  where div_Code = '" + div_code + "' ";
            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType " +
                     "  from usermanual " +
                     "  where div_Code = '" + div_code + "'";

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
        //end

        public DataSet GetStatus_MGR(string div_code, string sf_code, string FromMonth, string FromYear, string ToMonth, string ToYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " Exec Leave_Status '" + div_code + "','" + sf_code + "', '" + FromMonth + "', '" + FromYear + "', '" + ToMonth + "', '" + ToYear + "' ";

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
        public DataSet getMobApp_Setting(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select GeoChk,GEOTagNeed,DisRad,DrCap,ChmCap,StkCap, NLCap,DPNeed,DrRxQCap,DrSmpQCap,DINeed,ChmNeed,CPNeed, " +
                     " ChmQCap,CINeed,StkNeed,SPNeed,StkQCap,SINeed,UNLNeed,NPNeed,NLRxQCap,NLSmpQCap,NINeed from Access_Master " +
                     " where Division_Code = '" + divcode + "'";



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
        public DataSet getMobApp_Setting_halfday(string divcode, string WorkType_Code_B)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "  select Hlfdy_flag,WorkType_Code_B from Mas_WorkType_BaseLevel where Division_Code='" + divcode + "' and WorkType_Code_B='" + WorkType_Code_B + "' ";

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

        public DataSet getMobApp_geo(string sf_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select GeoNeed,sf_code from Access_Table where sf_code='" + sf_code + "' ";

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

        public DataSet gethalf_Daywrk(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            strQry = " select WorkType_Code_B,Worktype_Name_B from Mas_WorkType_BaseLevel " +
                    " where Division_Code='" + div_code + "' and active_flag=0 ";
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
        public int RecordUpdate_MobApp(int GeoChk, int GEOTagNeed, float DisRad, string DrCap, string ChmCap, string StkCap, string NLCap, int DPNeed, string DrRxQCap, string DrSmpQCap, int DINeed, int ChmNeed, int CPNeed, string ChmQCap, int CINeed, int StkNeed, int SPNeed, string StkQCap, int SINeed, int UNLNeed, int NPNeed, string NLRxQCap, string NLSmpQCap, int NINeed, string Division_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE Access_Master " +
                              " SET GeoChk= '" + GeoChk + "',GEOTagNeed='" + GEOTagNeed + "', "
                               + " DisRad = '" + DisRad + "',"
                               + " DrCap ='" + DrCap + "' ,"
                               + " ChmCap ='" + ChmCap + "', "
                               + " StkCap ='" + StkCap + "', "
                               + " NLCap ='" + NLCap + "', "
                               + " DPNeed ='" + DPNeed + "', "
                               + " DrRxQCap = '" + DrRxQCap + "', "
                               + " DrSmpQCap = '" + DrSmpQCap + "', "
                               + " DINeed = '" + DINeed + "', "
                               + " ChmNeed = '" + ChmNeed + "', "
                               + " CPNeed = '" + CPNeed + "', "
                               + " ChmQCap = '" + ChmQCap + "', "
                               + " CINeed = '" + CINeed + "', "
                               + " StkNeed = '" + StkNeed + "', "
                               + " SPNeed = '" + SPNeed + "', "
                               + " StkQCap = '" + StkQCap + "', "
                               + " SINeed='" + SINeed + "', "
                               + " UNLNeed ='" + UNLNeed + "', "
                               + " NPNeed ='" + NPNeed + "', "
                               + " NLRxQCap ='" + NLRxQCap + "', "
                               + " NLSmpQCap ='" + NLSmpQCap + "', "
                               + " NINeed =' " + NINeed + "' where Division_Code = '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;


        }

        public int RecordUpdate_Forhalfday(string WorkType_Code_B, string div_code, int Hlfdy_flag)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_WorkType_BaseLevel set Hlfdy_flag='" + Hlfdy_flag + "' " +
                         " where WorkType_Code_B='" + WorkType_Code_B + "' and Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_geosf_code(string sf_code, int GeoNeed)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Access_Table set GeoNeed='" + GeoNeed + "' " +
                         " where sf_code='" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int RecordUpdate_baselevel_tp(string strDesignation, int tp_start_date, int tp_end_date, string div_code)
        {
            int iReturn = -1;

            //string start_date = tp_start_date.Month + "-" + tp_start_date.Day + "-" + tp_start_date.Date.Year;
            //string end_date = tp_end_date.Month + "-" + tp_end_date.Day + "-" + tp_end_date.Date.Year;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_SF_Designation set Tp_Start_Date='" + tp_start_date + "', Tp_End_Date='" + tp_end_date + "' where Designation_Code='" + strDesignation + "' and Division_Code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_LockSystemMGR(string div_code, int lock_sysyem)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Admin_Setups_MGR set LockSystem_Needed='" + lock_sysyem + "' " +
                         " where Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int RecordUpdate_LockSystemMR(string div_code, int lock_sysyem)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Admin_Setups set LockSystem_Needed='" + lock_sysyem + "' " +
                         " where Division_code='" + div_code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getLockSystem_AdmMR(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select LockSystem_Needed from Admin_Setups where Division_Code='" + div_code + "' ";

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

        public DataSet getLockSystem_AdmMGR(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select LockSystem_Needed from Admin_Setups_MGR where Division_Code='" + div_code + "' ";

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

        public DataSet chk_tpbasedsystem_MR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select TpBased from Admin_Setups where Division_Code='" + divcode + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public DataSet chkRange_tpbased(string divcode, int Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select Tp_Start_Date,Tp_End_Date from Mas_SF_Designation where Division_Code='" + divcode + "' and Designation_Code='" + Designation_Code + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }

        public DataSet chk_tpbasedsystem_MGR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdm = null;

            strQry = " select TpBased from Admin_Setups_MGR where Division_Code='" + divcode + "' ";


            try
            {
                dsAdm = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdm;
        }
        public DataSet Get_UserManual_MR(string div_code, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select ID, FileName, FileSubject, Div_Code, Update_dtm,Data,ContentType,Designation_Code,Designation_Short_Name " +
                     "  from File_Info " +
                     "  where div_Code = '" + div_code + "' and Designation_Code like '%" + Designation_Code + "%'";

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
        public DataSet getSetup_forTargetFix(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select Target_Yearbasedon,Targer_Cal_Based  from Setup_Others where Division_Code='" + divcode + "' ";



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

        public int TargetFix_Setup(string div_code, string Target_Yearbasedon, string Targer_Cal_Based)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                bool count = sRecordExist_Target(div_code);

                if (count == false)
                {
                    strQry = "Insert into Setup_Others(Division_Code,Target_Yearbasedon,Targer_Cal_Based) values " +
                             " ('" + div_code + "', '" + Target_Yearbasedon + "','" + Targer_Cal_Based + "') ";
                }

                else
                {

                    strQry = "Update Setup_Others set Target_Yearbasedon='" + Target_Yearbasedon + "', " +
                             " Targer_Cal_Based ='" + Targer_Cal_Based + "' " +
                             " where Division_Code='" + div_code + "'";
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public bool sRecordExist_Target(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Setup_Others where Division_Code = '" + Division_Code + "'";
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

        public DataSet getOtherSetupfor_Targetyear(string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select Target_Yearbasedon from Setup_Others where Division_Code='" + div_code + "' ";

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
        public DataSet get_day(int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;



            strQry = "EXEC Month_date  " + month + " ";
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

        public DataSet getSetup_Expense(string divcode)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select MgrAppr_Remark,MgrAppr_Row_Wise,ExpSub_Basedon,BasedOn_Peri_Date,Last_Os_Wrkconsider,Exp_Subm_Range_Start,Exp_Subm_Range_End ," +
                      " Fieldforce_HQ_Ex_Max,ExCalls_Minimum from Expense_Setup where Division_Code='" + divcode + "' ";

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

        public int Expense_Setup(string div_code, string MgrAppr_Remark, string MgrAppr_Row_Wise, string ExpSub_Basedon, string BasedOn_Peri_Date, string Last_Os_Wrkconsider, string Exp_Subm_Range_Start, string Exp_Subm_Range_End, string Fieldforce_HQ_Ex_Max, string ExCalls_Minimum)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                bool count = sRecordExist_Expense(div_code);

                if (count == false)
                {
                    strQry = "Insert into Expense_Setup(Division_Code,MgrAppr_Remark,MgrAppr_Row_Wise,ExpSub_Basedon,BasedOn_Peri_Date,Last_Os_Wrkconsider,Exp_Subm_Range_Start,Exp_Subm_Range_End,Fieldforce_HQ_Ex_Max,ExCalls_Minimum) values " +
                             " ('" + div_code + "', '" + MgrAppr_Remark + "','" + MgrAppr_Row_Wise + "','" + ExpSub_Basedon + "','" + BasedOn_Peri_Date + "','" + Last_Os_Wrkconsider + "','" + Exp_Subm_Range_Start + "','" + Exp_Subm_Range_End + "','" + Fieldforce_HQ_Ex_Max + "','" + ExCalls_Minimum + "') ";
                }

                else
                {

                    strQry = "Update Expense_Setup set MgrAppr_Remark='" + MgrAppr_Remark + "', " +
                             " MgrAppr_Row_Wise ='" + MgrAppr_Row_Wise + "', " +
                             " ExpSub_Basedon ='" + ExpSub_Basedon + "', " +
                             " BasedOn_Peri_Date ='" + BasedOn_Peri_Date + "', " +
                             " Last_Os_Wrkconsider ='" + Last_Os_Wrkconsider + "', " +
                             " Exp_Subm_Range_Start ='" + Exp_Subm_Range_Start + "', " +
                             " Exp_Subm_Range_End ='" + Exp_Subm_Range_End + "', " +
                             " Fieldforce_HQ_Ex_Max ='" + Fieldforce_HQ_Ex_Max + "', " +
                             " ExCalls_Minimum ='" + ExCalls_Minimum + "' " +
                             " where Division_Code='" + div_code + "'";
                }

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public bool sRecordExist_Expense(string Division_Code)
        {
            if (Division_Code.Contains(','))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }
            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Expense_Setup where Division_Code = '" + Division_Code + "'";
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
        public DataSet getapp_Setting(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = "select * from Access_Master " +
                     " where Division_Code = '" + divcode + "'";
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
        //  int GeoChk, int GEOTagNeed, float DisRad, string DrCap, string ChmCap, string StkCap, string NLCap, int DPNeed, string DrRxQCap, string DrSmpQCap, int DINeed, int ChmNeed, int CPNeed, string ChmQCap, int CINeed, int StkNeed, int SPNeed, string StkQCap, int SINeed, int UNLNeed, int NPNeed, string NLRxQCap, string NLSmpQCap, int NINeed, string Division_Code)
        public int RecordUpdate_App_Setting(byte UNLNeed, byte NwRoute, byte StkNeed, byte NwDist, byte jointwork, byte template, byte CusOrder, byte OrderVal, byte NetweightVal, Int16 sms, byte DrSmpQ, byte CollectedAmount, byte recv, byte closing, byte OrdAsPrim, byte GEOTagNeed, decimal DisRad, byte geoTrack, string Division_Code, byte rcstk, byte sredit, byte orentry, byte dlyinven, byte esubcall, string display, string mandatory, byte doorTodoor, byte inshop, byte distNeed, byte misDate, byte opbal, byte clobal, byte freeqty, byte cllPreView, byte phoneOrder, byte battory, byte geoTrackprimary, byte Price_category, byte Explorerneed, string explorerkey)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                bool value = sRecordExistaccess_master(Division_Code);

                if (value == false)
                {
                    strQry = "INSERT INTO Access_Master(company_code, mobile_access,computer_access ,TBase,GeoChk,UNLNeed,DrCap,ChmCap,StkCap,NLCap,ChmNeed ,StkNeed ,DPNeed ,DINeed ,CPNeed,CINeed,SPNeed,SINeed"
                            + ",NPNeed ,NINeed,division_code,DrRxQCap,DrSmpQCap,ChmQCap,StkQCap,NLRxQCap,NLSmpQCap,GEOTagNeed,DisRad,CusOrder,closing,recv,DrSmpQ,sms,CollectedAmount,jointwork,OrdAsPrim"
                            + ",OrderVal,NetweightVal,VisitDist,template,NwRoute,NwDist,GeoTrack,DyInvNeed,OrdRetNeed,RetCBNd,RateEditable,EdSubCalls,Needed,Mandatory,DTDNeed , InshopND,DistBased,MsdDate,opbal,clbal,OfferMode,PreCall,PhoneOrderND,BatteryStatus,AgnBillClct,GeoTagPrimary_Nd,Price_category,Near_Me,exp_key"
                    + ") VALUES('1', '1','1' ,'0','0','" + UNLNeed + "','Retailer','Chemist','Distributors','New Retailer','1' ,'" + StkNeed + "' ,'0' ,'1' ,'1','1','1','1'"
                            + ",'1' ,'1','" + Division_Code + "','O.Qty','Value','Qty','Qty','Rx Qty','Sample Qty','" + GEOTagNeed + "','" + DisRad + "','" + CusOrder + "','" + closing + "','" + recv + "','" + DrSmpQ + "','" + sms + "','" + CollectedAmount + "','" + jointwork + "','" + OrdAsPrim + "'"
                            + ",'" + OrderVal + "','" + NetweightVal + "','0','" + template + "','" + NwRoute + "','" + NwDist + "','" + geoTrack + "','" + dlyinven + "','" + orentry + "','" + rcstk + "','" + sredit + "','" + esubcall + "','"
                            + display + "','" + mandatory + "','" + doorTodoor + "','" + inshop + "','" + distNeed + "','" + misDate + "','" + opbal + "','" + clobal + "','" + freeqty + "','" + cllPreView + "','" + phoneOrder + "','" + battory + "','" + CollectedAmount + "','" + geoTrackprimary + "','" + Price_category + "','" + Explorerneed + "','" + explorerkey + "')";
                }
                else
                {
                    strQry = "UPDATE Access_Master " +
                                   " SET UNLNeed = '" + UNLNeed + "',"
                                    + " NwRoute ='" + NwRoute + "', "
                                    + " StkNeed = '" + StkNeed + "',"
                                    + " NwDist ='" + NwDist + "' ,"
                                    + " jointwork ='" + jointwork + "', "
                                    + " template ='" + template + "', "
                                    + " CusOrder ='" + CusOrder + "', "
                                    + " OrderVal ='" + OrderVal + "', "
                                    + " NetweightVal = '" + NetweightVal + "', "
                                    + " sms = '" + sms + "', "
                                    + " DrSmpQ = '" + DrSmpQ + "', "
                                    + " CollectedAmount = '" + CollectedAmount + "', "
                                    + " recv = '" + recv + "', "
                                    + " closing = '" + closing + "', "
                                    + " OrdAsPrim = '" + OrdAsPrim + "', "
                                    + " GEOTagNeed = '" + GEOTagNeed + "', "
                                    + " DisRad = '" + DisRad + "', "
                                    + " DyInvNeed = '" + dlyinven + "', "
                                    + " OrdRetNeed = '" + orentry + "', "
                                    + " RetCBNd = '" + rcstk + "', "
                                    + " RateEditable = '" + sredit + "', "
                                    + " EdSubCalls = '" + esubcall + "', "
                                    + " geoTrack='" + geoTrack + "',"
                                    + " Needed='" + display + "',"

                                    + " DTDNeed='" + doorTodoor + "',"
                                    + " InshopND='" + inshop + "',"
                                    + " DistBased='" + distNeed + "',"
                                    + " MsdDate='" + misDate + "',"
                                    + " opbal='" + opbal + "',"
                                    + " clbal='" + clobal + "',"
                                    + " OfferMode='" + freeqty + "',"
                                    + " PreCall='" + cllPreView + "',"
                                    + " PhoneOrderND='" + phoneOrder + "',"
                                    + " BatteryStatus='" + battory + "',"
                                    + " AgnBillClct='" + CollectedAmount + "',"
                                      + " GeoTagPrimary_Nd='" + geoTrackprimary + "',"
                                      + " Price_category='" + Price_category + "',"
                                        + " Near_Me='" + Explorerneed + "',"
                                      + " exp_key='" + explorerkey + "',"
                                    + " Mandatory='" + mandatory + "' where Division_Code = '" + Division_Code + "' ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public bool sRecordExistaccess_master(string Division_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Select count(*) from Access_Master where Division_Code = '" + Division_Code + "'";
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

        public DataSet GetEmployees_sp(string div_code, string sf_code, string Sub_Div_Code= "0",string Alpha="1",string statecode="0")
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [getHyrSFList] '" + sf_code + "','" + div_code + "','" + Sub_Div_Code + "','" + Alpha + "','" + statecode + "'";
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

        public DataSet GetAccessmas_sp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec [getAccess_mas]   '" + div_code + "'";
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

        public int insert_acmas(string sf_code, string sf_name, string srf_code, byte geoneed, byte geotrack, byte geofencing, byte eddysumry,byte vansl, byte chnen, byte fcs,  string div_code, Int16 sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "exec [Insert_acctable] '" + sf_code + "','" + sf_name + "','" + geoneed + "','" + geotrack + "','" + geofencing + "','" + eddysumry + "','" + vansl + "','" + chnen + "','" + fcs + "','" + div_code + "','" + sf_type + "'";
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
        public DataSet Get_Target_Names(string Div_Code)
        {
            DB_EReporting Db_Er = new DB_EReporting();
            DataSet dsTarget = null;
            strQry = "Select * from mas_target_settings where division_code = '" + Div_Code + "'";
            try
            {
                dsTarget = Db_Er.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTarget;
        }


        public int insert_target_setting(string Div_Code, string code, string tvalues, string months, string years, string state)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;

            strQry = "exec Insert_Target_Setting '" + Div_Code + "','" + code + "','" + tvalues + "','" + months + "','" + years + "','" + state + "'";
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
		
        public DataSet Get_target_setting_Values(string Div_Code, string year, string state, string months = "0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTarget = null;
            strQry = "select t.id,code,TVALUES,months,years from Trans_Target_Settings T inner join Mas_Target_Settings M on m.id=t.id where T.DIVISION_CODE = '" + Div_Code + "'  and years='" + year + "' and ('" + months + "'='0' or months='" + months + "') and State_Code='" + state + "'";
            try
            {
                dsTarget = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTarget;
        }
		
        public DataSet getLeave_Details(string sfcode, string FYear, string TYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "exec getLeaveDetails '" + sfcode + "' ,'" + FYear + "/04/01' , '" + TYear + "/03/31'";

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

        public DataSet view_product(string taxcode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            string strQry = "select State_Code,Product_Code,Tax_Id,Division_Code from Mas_stateproduct_taxdetails where Tax_Id = '" + taxcode + "' and Division_Code = '" + divcode + "' ";
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

             string strQry = "select Tax_Id,Tax_Name,Tax_Type,Value,Created_Date,Deactivated_Date,Tax_Active_Flag,case when Tax_Active_Flag=0 then 'Deactivate' else 'Activate'" +
                 " end as status from Tax_Master where Division_code='" + div_code + "'";
            
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
        public int atcdec_tax_Master(string tax_id, string stat)
        {
            int iReturn = -1;
            string strQry = "";

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "exec TaxMas_DeActivate '" + tax_id + "'," + stat + " ";
                //strQry=update Tax_Master set Tax_Active_Flag = @stat, Deactivated_Date = GETDATE() where Tax_Id = @sdcode

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet gettaxmaster(string Tax_Name,string Tax_Type, string Value, string div_code)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Tax_Id,Tax_Name,Tax_Type,Value,Division_Code from  Tax_Master where Tax_Name='"+ Tax_Name + "'and Value='" + Value + "'and Tax_Type='" + Tax_Type + "'and Division_Code='" + div_code + "'";
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
        public DataSet gettaxid(string divcode, string scode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select Tax_Id,Tax_Name,Tax_Type,Value from  Tax_Master where Tax_Id='" + scode + "'and Division_Code='" + divcode + "'";
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
        public int saveTax(string Tax_Name, string Tax_Type,string Value,string div_code)
        {
            DB_EReporting db = new DB_EReporting();
            int ds = -1;
            string strQry = " exec Save_Tax'" + Tax_Name + "','" + Tax_Type + "','"+ Value+"','"+ div_code+"' ";
            try
            {
                ds = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        
        public DataSet CustomMasterTableVal(string DivCode)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = " Exec [Get_CustomMaster_TableList] '" + DivCode + "'";
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

        public DataSet CustomGetTableColumns(string TableName)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = " Exec Get_TableColumns '" + TableName + "'";
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

               
        public DataSet getCustomTempVal(string Div_Code, string cuttemp)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Template_Text from Mas_Custom_Temp where Division_Code='" + Div_Code + "' and Template_Name='" + cuttemp + "'";

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

        public DataSet GetCustomFormsFieldsData(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomForms_Fields] '" + divcode + "' ,'" + ModeleId + "' ";

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

        public DataSet GetCustomFormsFieldsDataById(string divcode, string FieldId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomFormsFields_ById] '" + divcode + "' ,'" + FieldId + "' ";

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

        public DataSet getCustomFields_Edit(string divcode, string FldID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomFormsFields_ById] '" + divcode + "' ,'" + FldID + "' ";

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

          public DataSet GetAdditionalRoute(string divcode, string ModeleId)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT  *FROM  Trans_Route_Custom_Field";

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
        
         public DataSet GetCustomFormsFieldsColumns(string divcode, string ModeleId, string sf)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC [Get_CustomFormsFieldsColumns] '" + divcode + "' ,'" + ModeleId + "','" + sf + "' ";

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