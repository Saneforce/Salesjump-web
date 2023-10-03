using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Bus_EReport
{
    public class CallPlan
    {
        private string strQry = string.Empty;

        public DataSet FetchTerritoryName(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT a.Territory_Code Territory_Code, " +
                     " (a.Territory_Name +  ' (' + CAST((select COUNT(distinct b.ListedDrCode) from Call_Plan b " +
                     " where a.Territory_Code=b.Territory_Code and b.ListedDr_Active_Flag=0 and sf_code = '" + sf_code + "' ) as CHAR(3)) " +
                     " + ') ' ) Territory_Name " +
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
        public DataSet GetTerritoryName(string sf_code,string terr_code)
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

        public DataTable get_ListedDoctor_Territory(string sfcode, string TerrCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtListedDR = null;

            strQry = "SELECT DISTINCT d.ListedDrCode,d.ListedDr_Name, t.territory_name, c.Territory_Code, c.Plan_No, '' color FROM " +
                        "Mas_ListedDr d, Mas_Territory_Creation t, Call_Plan c " +
                        "WHERE c.Sf_Code =  '" + sfcode + "' and " +
                        " (c.Territory_Code like '" + TerrCode + ',' + "%'  or " +
                     " c.Territory_Code like '%" + ',' + TerrCode + ',' + "%') and " +
                        " c.ListedDrCode = d.ListedDrCode and " +
                        " c.Territory_Code  = t.Territory_Code and " +
                        " c.ListedDr_Active_Flag = 0 " +
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

        public int Std_WorkPlan(string Territory_Code, string Doc_Code, string sf_code, int Plan_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Call_Plan " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Plan_No = " + Plan_No + " ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int Update_claim(string sxml, string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec updateclaim '" + sxml + "','" + sf_code + "'," + divcode + "";
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
        public int Remove_CallPlan(string Territory_Code, string Doc_Code, string sf_code, int Plan_No)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Delete from Call_Plan " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' AND Territory_Code = '" + Territory_Code + "'  ";

                iReturn = db.ExecQry(strQry);

                //strQry = "Update Mas_ListedDr " +
                //         " Set Territory_Code = 0 " +
                //         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                //iReturn = db.ExecQry(strQry);
            
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

                strQry = "UPDATE Call_Plan " +
                         " SET Territory_Code = '" + Territory_Code + "' " +
                         " WHERE ListedDrCode='" + Doc_Code + "' AND sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

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

  public DataSet EventClosing(string divcode, string sf, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " Exec EventClosing '" + sf + "','" + divcode + "','" + date + "'";
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
        public DataTable EventClosingxl(string divcode, string sf, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = " Exec EventClosing '" + sf + "','" + divcode + "','" + date + "'";
            try
            {
                dsSF = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;
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

                strQry = "select division_code from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";
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
 public DataSet claimdes(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " Exec claimdes '" + divcode + "'";
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
        public DataSet getShiftID(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getShiftID '" + divcode + "'";
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

        public string saveShiftTime(SaveShift ss)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string msg = string.Empty;
            strQry = "exec insertShift '" + ss.divcode + "','" + ss.scode + "','" + ss.sname + "','" + ss.stime + "','" + ss.etime + "','" + ss.hqcode + "','" + ss.shtcode + "','" + ss.duration + "','" + ss.etype + "','" + ss.ehours + "','" + ss.dept + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public DataSet getShift(string scode, string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "select Sft_ID,Sft_Name,Sft_STime,sft_ETime,Sft_Code,HQ_Code,ACtOffTyp,ACtOffHrs,isnull(dept_code,'')dept_code from Mas_Shift_Timings where Sft_ID='" + scode + "' and Division_Code='" + divcode + "'";
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
        public DataSet getAllShift(string divcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec getShift_Details '" + divcode + "'";
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

        public class SaveShift
        {
            [JsonProperty("Divcode")]
            public object divcode { get; set; }

            [JsonProperty("SName")]
            public object sname { get; set; }

            [JsonProperty("SCode")]
            public object scode { get; set; }

            [JsonProperty("STime")]
            public object stime { get; set; }

            [JsonProperty("ETime")]
            public object etime { get; set; }

            [JsonProperty("HQCode")]
            public object hqcode { get; set; }

            [JsonProperty("ShtCode")]
            public object shtcode { get; set; }

            [JsonProperty("Duration")]
            public object duration { get; set; }

            [JsonProperty("Endtype")]
            public object etype { get; set; }

            [JsonProperty("Endhour")]
            public object ehours { get; set; }

            [JsonProperty("Depart")]
            public object dept { get; set; }
        }

        public int DeActivate(string plcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_Shift_Timings set ActiveFlag='" + stus + "' where Sft_ID='" + plcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public DataSet getGiftAdApproval(string sf_code, string Fromdate, string Todate, string div, string gifttype, string ssf)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec getGiftApprovalwebnew '" + sf_code + "','" + Fromdate + "','" + Todate + "','" + div + "','" + gifttype + "','" + ssf + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
        public DataSet getGiftEventIMG(string sf_code, string ddldes, string desc, string div, string gifttype)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec getGiftevent '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
    public int Getclaim_upload(string claimid, string Action, string adminFlag, string sucess, string nsmFlag, string Div)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db_ER = new DB_EReporting();
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = null;
                DataSet dschk = new DataSet();


                strQry = "select * from Trans_Gift_Claim where Sl_No='" + claimid + "'  and Division_Code='" + Div + "' and isnull(Aproval_Flag,0)<>0";
                dschk = db.Exec_DataSet(strQry);

                if (dschk.Tables[0].Rows.Count > 0)
                {
                    strQry = "exec upload_save '"+ claimid + "','"+ Action + "','"+ adminFlag + "','"+ sucess + "','"+ nsmFlag + "','"+ Div + "'";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    iReturn = -2;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
    public DataTable getGiftApprovalxl(string sf_code, string Fromdate, string Todate, string div, string gifttype, string sfCode,string Ftype)
  {

      DataTable dsSF = null;
      DB_EReporting db = new DB_EReporting();
      strQry = "exec getGiftApprovalweb '" + sf_code + "','" + Fromdate + "','" + Todate + "','" + div + "','" + gifttype + "','" + sfCode + "','" + Ftype + "'";
      try
      {
          dsSF = db.Exec_DataTable(strQry);
      }
      catch (Exception ex)
      {
          throw ex;
      }
      return dsSF;

  }
 public int Update_claimNSM(string sxml,string sf)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iReturn = -1;
            strQry = "exec updateclaimNSM '" + sxml + "','"+sf+"'";
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
 public DataSet setapprovel(string sf_code, string div)
 {

     DataSet dsSF = null;
     DB_EReporting db = new DB_EReporting();
     strQry = "exec setapprovel '" + sf_code + "','" + div + "'";
     try
     {
         dsSF = db.Exec_DataSet(strQry);
     }
     catch (Exception ex)
     {
         throw ex;
     }
     return dsSF;

 }
 public DataSet getGiftApprovalNSM(string sf_code, string ddldes, string desc, string div, string gifttype, string sfcode)
 {

     DataSet dsSF = null;
     DB_EReporting db = new DB_EReporting();
     strQry = "exec getGiftApprovalwebnsm '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "','" + sfcode + "'";
     try
     {
         dsSF = db.Exec_DataSet(strQry);
     }
     catch (Exception ex)
     {
         throw ex;
     }
     return dsSF;

 }
        public DataSet getGiftEventNSM(string sf_code, string ddldes, string desc, string div, string gifttype)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec getGifteventNSM '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "'";
            try
            {
                dsSF = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
       
        public DataTable getGiftApprovalNSMxl(string sf_code, string ddldes, string desc, string div, string gifttype, string sfCode)
        {

            DataTable dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec getGiftApprovalwebnsm '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "','" + sfCode + "'";
            try
            {
                dsSF = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
       
            public DataSet getclaimstatussummryWebsf(string sf_code, string ddldes, string desc, string div, string gifttype)
            {

                DataSet dsSF = null;
                DB_EReporting db = new DB_EReporting();
                strQry = "exec getclaimstatussummryWebsf '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "'";
                try
                {
                    dsSF = db.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsSF;

            }
            public DataSet getclaimstatussummryWeb(string sf_code, string ddldes, string desc, string div, string gifttype)
            {

                DataSet dsSF = null;
                DB_EReporting db = new DB_EReporting();
                strQry = "exec getclaimstatussummryWeb '" + sf_code + "','" + div + "','" + ddldes + "','" + desc + "','" + gifttype + "'";
                try
                {
                    dsSF = db.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return dsSF;

            }
        public DataSet getAttendShift(string divcode, string hqcode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Mas_Shift_Timings where CHARINDEX(','+'" + hqcode + "'+',',','+HQ_Code+',')>0 ";
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
        public DataTable getGiftApprovalOrders(string sf_code, string Fromdate, string Todate, string div, string gifttype, string sfCode, string Ftype)
        {

            DataTable dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec getClaimOrders '" + sf_code + "','" + Todate + "','" + div + "'";
            try
            {
                dsSF = db.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSF;

        }
    }
}
