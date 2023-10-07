using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;


namespace Bus_EReport
{
    public class Notice
    {
        private string strQry = string.Empty;
		
		 public DataSet getDesignationByUser(string div_code, string sf_type, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT DISTINCT sf.Designation_Code ,sd.Designation_Short_Name,sd.Designation_Name, ";
            strQry += " sd.Designation_Short_Name + ' / ' + sd.Designation_Name AS [Name]   FROM Mas_Salesforce  sf ";
            strQry += " Inner Join Mas_SF_Designation sd With(Nolock) On sf.Designation_Code = sd.Designation_Code ";
            strQry += "  WHERE sd.Division_Code = '" + div_code + "'  and sd.Designation_Active_Flag = 0 ";
            strQry += "  AND sf.sf_type = " + Convert.ToInt32(sf_type) + "  and sf.Sf_Code = '" + Sf_Code + "' ";
            
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
		
		
		
		
		public DataSet getSalesForceByRMFare(string div_code, string sf_type, string Sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT s.sf_code, s.sf_name sf_name,isnull(convert(varchar,Sf_Joining_Date,103),'')JDT,isnull(sf_emp_id,'')sf_emp_id,d.designation_name ,s.sf_HQ ,d.Designation_Code" +
                     " FROM mas_salesforce s inner join Mas_SF_Designation d on d.Designation_code=s.designation_code " +
                     " where s.Division_Code like '%" + div_code + "%' and s.Reporting_To_SF = '" + Sf_Code + "'  order by 2";
            //" where s.Division_Code like '%" + div_code + "%' and s.Reporting_To_SF = '" + Sf_Code + "' s.sf_type = " + Convert.ToInt32(sf_type) + " and s.sf_status=0 order by 2";


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
		
		
		
		
        public DataSet Notification_view(string div_code)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "SELECT 'Created ' " +
    "+ CAST(DATEDIFF(day,Created_Date, getdate()) AS NVARCHAR(50)) + ' days ago, ' " +
    "+ CAST(DATEDIFF(second,Created_Date, GETDATE()) / 60 / 60 % 24  AS NVARCHAR(50)) + ' hours, ' " +
    "+ CAST(DATEDIFF(second,Created_Date, GETDATE()) / 60 % 60 AS NVARCHAR(50)) + ' min ago ' as timee,Comment,Comment_Type,Created_Date from Mas_Notice_Board where Division_Code='" + div_code + "'";
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
        public DataSet retailercount_mr(string div_code, string feildcode)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
			strQry = "EXEC dasbord_customer_count '"+ div_code + "','"+ feildcode + "'";
            //strQry = "select count(ListedDrCode) as retailercount from Mas_ListedDr  D inner join Mas_Territory_Creation T on D.Territory_Code=cast(T.Territory_code as varchar)  where T.Dist_Name='" + feildcode + "' and d.ListedDr_Active_Flag='0'";
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
		
		
		
		public DataSet Total_VacantUserdashboard_country(string div_code,string cntry)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select count(sf_code) cnt from mas_salesforce u inner join mas_State ms on u.state_Code=ms.state_code  where CHARINDEX(','+'" + div_code + "'+',',','+Division_Code+',')>0 and SF_Status='1' and ms.Country_code='" + cntry + "'";
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
		 public DataSet Total_Userdashboard_country(string div_code, string cntry, string date)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select count(u.Sf_Code) as Tot_User,(select count(TP.sf_code)  from  TbMyDayPlan TP " +
                       " inner join Mas_Salesforce U on  u.Sf_Code=TP.sf_code where " +
                       " cast(CONVERT(varchar, TP.Pln_Date,101) as datetime)='" + date + "' and TP.FWFlg='L' " +
                       " and u.Division_Code='" + div_code + ",' and u.SF_Status='0') as Leave,(select count(u.Sf_Code) as Tot_User from Mas_Salesforce u where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='2')INactive " +
                       " from Mas_Salesforce u  inner join mas_State ms on u.state_Code=ms.state_code and ms.Country_code='" + cntry + "'  where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='0'";
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
        public string Notice_Comment_Add(string div_code, string comment, string type, string date)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {


                strQry = " INSERT INTO [Mas_Notice_Board]([Division_Code],[Comment],[Comment_Type],[Created_Date]) " +
                       " VALUES ( '" + div_code + "' , '" + comment + "', '" + type + "', '" + date + "') ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public DataSet countindashboard(string div_code,string sub_divc="0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();

            //strQry = "select count(ListedDrCode) as retailercount from Mas_ListedDr where Division_Code='" + div_code + "' and ListedDr_Active_Flag='0'";
			strQry = "exec getRetailerCountDashboard '" + div_code + "','" + sub_divc + "'";
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
        public DataSet countinpriorder(string div_code, string date,string subdiv="0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(*)cou from Trans_PriOrder_Head where Order_Value!=0 and CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + date + "%' and Division_Code='" + div_code + "' and Order_Flag='0'";
			strQry = "exec getPrimarOrderCnt '" + div_code + "','" + date + "','" + subdiv + "'";
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
        public DataSet orderdashboard(string div_code, string date)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + date + "%'  and m.Division_Code=" + div_code + "";
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
        public DataSet get_order_orderwithsale(string divcode, string sfcode, string month, string year, string productcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select cast((sum(d.Quantity)/max(p.Sample_Erp_Code)) as varchar)+'.'+cast((sum(d.Quantity)-(max(p.Sample_Erp_Code)*(sum(d.Quantity)/max(p.Sample_Erp_Code)))) as varchar),sum(d.Quantity),max(p.Sample_Erp_Code) from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No" +
                    " inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code"+
                    " where d.Product_Code='" + productcode + "' and h.Sf_Code='" + sfcode + "' and MONTH(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "'";

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

        public DataSet get_sale_orderwithsale(string divcode, string sfcode, string month, string year, string productcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sum(Sale_Qty)as gg from Trans_Secondary_Sales_Details " +
         " where Product_Code='" + productcode + "' and SfCode='" + sfcode + "' and MONTH(date)='" + month + "' and YEAR(date)='" + year + "'";

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
        public DataSet GetStockist_divisionwise(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "'  and Stockist_Active_Flag='0'";

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

        public DataSet get_call_count(string divcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select   'Productive' as call_type,  " +
" count( (case when M.POB_Value>0 then 1 end))  as tot_cals " +
"from   [vwProductive_Details] m  where m.Division_Code='" + divcode + "' and CONVERT(VARCHAR(25),m.Activity_Date, 126) LIKE '" + date + "%' " +
"union all " +
"select  'Nonproductive' as call_type, " +
"count( (case when  M.POB_Value=0 then 1 end)) as tot_cals  " +
" from  [vwProductive_Details] m  where m.Division_Code='" + divcode + "'  and CONVERT(VARCHAR(25),m.Activity_Date, 126) LIKE '" + date + "%'";

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
        public DataSet get_call_count_distributor_rotewise(string divcode, string distri_code, string route_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select   'Productive' as call_type,  " +
   " count( (case when M.POB_Value>0 then 1 end))  as tot_cals " +
   "from   [vwProductive_Details] m  where m.Division_Code='" + divcode + "' and m.Stockist_Code='" + distri_code + "' and m.sdp='" + route_code + "' and CONVERT(VARCHAR(25),m.Activity_Date, 126) LIKE '" + date + "%' " +
    "union all " +
    "select  'Nonproductive' as call_type, " +
    "count( (case when  M.POB_Value=0 then 1 end)) as tot_cals  " +
   " from  [vwProductive_Details] m  where m.Division_Code='" + divcode + "' and m.Stockist_Code='" + distri_code + "' and m.sdp='" + route_code + "' and CONVERT(VARCHAR(25),m.Activity_Date, 126) LIKE '" + date + "%'";

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
        public DataSet view_stockist_Distributorwise(string div_code, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet feidforce = null;
            string sff_code = string.Empty;
            DataSet dsAdmin = null;
            strQry = "SELECT stuff((    " +
         " SELECT ', ' + cast(QUOTENAME(d.sf_code,'''') as varchar(max)) " +
       " FROM Mas_salesforce d where d.Reporting_to_sf='" + sfcode + "' " +
          " FOR XML PATH('') " +
          "  ), 1, 2, '')  ";
            feidforce = db_ER.Exec_DataSet(strQry);
            if (feidforce.Tables[0].Rows.Count > 0)
            {
                sff_code = feidforce.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                if (sff_code == "")
                {
                    strQry = "select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + div_code + "' and Field_Code in ('" + sfcode + "') ";
                }
                else
                {
                    strQry = "select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + div_code + "' and  Field_Code in (" + sff_code + ") ";
                }
            }
            else
            {
                strQry = "select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + div_code + "'  and Field_Code in ('" + sfcode + "') ";
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
        public DataSet GetDate_daywise(string fmonth, string fyear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            DataSet fromday = null;
            string sff_code = string.Empty;
            strQry = "select From_Day from MAS_DIVISION where Division_code='" + div_code + "'";
            fromday = db_ER.Exec_DataSet(strQry);
            if (fromday.Tables[0].Rows.Count > 0)
            {
                sff_code = fromday.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            strQry = "DECLARE @dt1 Datetime='" + fyear + "-" + fmonth + "-" + sff_code + "' " +
            "DECLARE @dt2 Datetime=DATEADD(mm, 1, DATEADD(dd, -1, '" + fyear + "-" + fmonth + "-" + sff_code + "'))" +
             ";WITH ctedaterange " +
             " AS (SELECT [Dates]=@dt1 " +
              "  UNION ALL " +
              "  SELECT [dates] + 1 " +
              "  FROM   ctedaterange " +
               " WHERE  [dates] + 1<= @dt2) " +
               " SELECT DATEPART(day,[dates]) 'DayPart',DATEPART(month,[dates]) 'monthe' " +
               "  FROM   ctedaterange " +
               "OPTION (maxrecursion 0)";

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

        public DataSet Getvalue_daywise(string sfcode, string day_of_date, string fyear, string fmonth, string div_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;



            strQry = "SELECT d.pob_value,sum(d.total_cals) as TC,sum(d.productive_calls) as EC,D.Sf_Code,AA.WType_SName," +
            "CASE " +
            " WHEN d.total_cals ='0'  THEN  CAST(AA.WType_SName AS Varchar(20)) " +
           "  ELSE  CAST(d.total_cals AS Varchar(20)) " +
             " END as datevalue," +
         "CASE  WHEN d.total_cals ='0'  THEN  CAST(AA.WType_SName AS Varchar(20))   ELSE  CAST(d.productive_calls AS Varchar(20))  END as dateprodvalue " +

            "FROM    dcr_summary d  INNER join vwactivity_report v on D.transslno=v.[Trans_SlNo] INNER JOIN  vwmas_workType_all AA ON AA.tYPE_CODE=V.wORK_TYPE where d.sf_code='" + sfcode + "' and DATEPART(day,d.submission_date)='" + day_of_date + "'  and DATEPART(MONTH,d.submission_date)='" + fmonth + "' and aa.sftyp='" + sf_type + "'    group by d.pob_value,D.SF_CODE,total_cals,productive_calls,AA.WType_SName " +
           " OPTION (maxrecursion 0) ";

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


        public DataSet Getvalue_daywise_maximised(string sfcode, string day_of_date, string fyear, string fmonth, string div_code, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT d.pob_value,sum(d.total_cals) as TC,sum(productive_calls) as EC,D.Sf_Code,AA.WType_SName," +
            "CASE " +
            " WHEN d.total_cals ='0'  THEN  CAST(AA.WType_SName AS Varchar(20)) " +
           "  ELSE  CAST(d.total_cals AS Varchar(20)) " +
             " END as datevalue," +
         "CASE  WHEN d.total_cals ='0'  THEN  CAST(AA.WType_SName AS Varchar(20))   ELSE  CAST(d.productive_calls AS Varchar(20))  END as dateprodvalue, " +

        " CASE  WHEN d.pob_value =null  THEN  CAST(AA.WType_SName AS Varchar(20))  ELSE  CAST(d.pob_value AS Varchar(20))END as pobvalue," +
         " isnull((SELECT  sum(net_weight_value) FROM  vwactivity_msl_details where trans_slno=v.Trans_SlNo),0)as netvalue " +
            "FROM  dcr_summary d   INNER join vwactivity_report v on D.transslno=v.[Trans_SlNo] INNER JOIN  vwmas_workType_all AA ON AA.tYPE_CODE=V.wORK_TYPE  where d.sf_code='" + sfcode + "' and DATEPART(day,d.submission_date)='" + day_of_date + "'  and DATEPART(MONTH,d.submission_date)='" + fmonth + "' and aa.sftyp='" + sf_type + "'    group by d.pob_value,D.SF_CODE,total_cals,productive_calls,AA.WType_SName,d.pob_value,v.Trans_SlNo  " +
           " OPTION (maxrecursion 0) ";

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
        public DataSet countindashboard_MGR(string div_code, string sf_code)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(ListedDr_Name) as retailercount from Mas_ListedDr where Division_Code='"+div_code+"' and ListedDr_Active_Flag='0'";
            strQry = "exec [GET_Retailer_Count] '" + div_code + "','" + sub + "','" + sf_code + "'";
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

        public DataSet countinDist(string div_code,string subdiv="0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(Stockist_Code) as Distcount from Mas_Stockist where Division_Code='" + div_code + "' and Stockist_Active_Flag='0'";
			strQry = "exec getDashboardStkCount '" + div_code + "','" + subdiv + "'";
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
        public DataSet countinDist_MGR(string div_code, string sf_code)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select count(ListedDr_Name) as retailercount from Mas_ListedDr where Division_Code='"+div_code+"' and ListedDr_Active_Flag='0'";
            strQry = "exec [GET_DISTRIBUTOR_Cou] '" + div_code + "','" + sub + "','" + sf_code + "'";
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
        public DataSet view_total_order_view(string div_code, string sf_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "exec [TODAY_ORDER_VIEW_COU] '" + sf_code + "','" + div_code + "','" + date + "'";
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
        public DataSet orderdashboard(string sf_code, string div_code, string date)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [TODAY_ORDER_VIEW_List_cou] '" + sf_code + "','" + div_code + "','" + date + "'";
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


        public DataSet Getproductstatewise(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Mas_PRoduct_detail  where CHARINDEX( '," + state_code + ",', ',' + STATE_CODE + ',' ) > 0 and pRODUCT_aCTIVE_FLAG=0 AND dIVISION_CODE='" + div_code + "'; ";


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
        //public DataSet getSalesForce_Fare(string div_code, string sf_code, string Expense_Mode, string exp_year, string exp_month)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsDivision = null;
        //    strQry = " SELECT * from Trans_Expense_Head1 h inner  join mas_salesforce ms on ms.sf_code=h.sf_code " +
        //             " where H.Division_Code='" + div_code + "' and (Expense_Mode='" + Expense_Mode + "' or Reporting_To_SF='" + sf_code + "') and Expense_Year='" + exp_year + "' and Expense_Month='" + exp_month + "' ";
        //    try
        //    {
        //        dsDivision = db_ER.Exec_DataSet(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return dsDivision;
        //}
        public DataSet getSalesForce_Fare(string div_code, string sf_code, string Expense_Mode, string exp_year, string exp_month, string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpenseFare '" + sf_code + "'," + exp_month + "," + exp_year + ",'" + div_code + "'";

            //if (sf_type == "3")
            //{
            //    strQry = " SELECT * from Trans_Expense_Head1 h inner  join mas_salesforce ms on ms.sf_code=h.sf_code " +
            //         " where H.Division_Code='" + div_code + "' and ( Expense_Mode='" + Expense_Mode + "' or Expense_Mode='3'  ) and Expense_Year='" + exp_year + "' and Expense_Month='" + exp_month + "' ";
            //}
            //else
            //{
            //  //  strQry = " SELECT * from mas_salesforce " +
            //  //          " where charindex(','+'" + div_code + "'+',',','+Division_Code+',')>0 and SF_Status=0 and Reporting_To_SF='" + sf_code + "'";


            //    strQry = " SELECT * from mas_salesforce ms  left outer join Trans_Expense_Head1 h on ms.sf_code=h.sf_code  and  ( Expense_Mode='"+Expense_Mode+"' or Expense_Mode='2'  )  and Expense_Year='"+exp_year+"' and Expense_Month='"+exp_month+"'"+
            //              " where charindex(','+'"+div_code+"'+',',','+ms.Division_Code+',')>0 and SF_Status=0 and Reporting_To_SF='"+sf_code+"'";

            //}
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
        public DataSet getSalesForce_Fare(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT s.sf_code, s.sf_name sf_name,isnull(convert(varchar,Sf_Joining_Date,103),'')JDT,isnull(sf_emp_id,'')sf_emp_id,d.designation_name ,s.sf_HQ ,d.Designation_Code" +
                     " FROM mas_salesforce s inner join Mas_SF_Designation d on d.Designation_code=s.designation_code " +

                     " where s.Division_Code like '" + div_code + ",' and s.sf_status=0 order by 2";


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
        public DataSet getDesignation_div(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
                     " FROM Mas_SF_Designation where Division_Code = '" + div_code + "'  and Designation_Active_Flag=0 " +
                     " ORDER BY 2";
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



        public string Add_Allowance_Type(string div_code, string alwname, string alwsname, string type)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {

                strQry = " INSERT INTO [Mas_Allowance_Type]([Division_Code],[Allowance_Name],[Short_Name],[Type],[Active_Flag],[Created_Date]) " +
                    " VALUES ( '" + div_code + "' , '" + alwname + "', '" + alwsname + "', '" + type + "','0',getdate()) ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
		
		
        public string Add_Allowance_Type(string div_code, string alwname, string alwsname, string type, string uentr, string alw_code, string maxall, string attach, string multiv, string refv, string glcodewith, string glcodewithout,string expFor,string eligibility,string AllTmode,string gradeid, string arrv)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                if (alw_code != string.Empty)
                {
                    strQry = "exec updateAllowanceTypes '" + div_code + "' , '" + alwname + "','" + alw_code + "','" + alwsname + "', '" + type + "','" + uentr + "','" + maxall + "','" + attach + "','" + multiv + "','" + refv + "','" + glcodewith + "','" + glcodewithout + "','"+ expFor + "','"+ eligibility + "','"+ AllTmode + "','"+ gradeid + "','" + arrv + "'";
                }
                else
                {
                    strQry = "exec insertAllowanceTypes '" + div_code + "' , '" + alwname + "', '" + alwsname + "', '" + type + "','" + uentr + "','" + maxall + "','" + attach + "','" + multiv + "','" + refv + "','" + glcodewith + "','" + glcodewithout + "','"+ expFor + "','"+ eligibility + "','"+ AllTmode + "','"+ gradeid + "','" + arrv + "'";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }

        public DataSet get_Allowance_Type(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Mas_Allowance_Type " +
                     " where Division_Code ='" + div_code + "' and Active_Flag=0 order by  Type,Allowance_Name";
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
        public DataSet get_Allowance_Type_nonenter(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select * from Mas_Allowance_Type " +
                     " where Division_Code ='" + div_code + "' and Active_Flag=0 and  isnull(user_enter,0) <>  '1'  order by  Type,Allowance_Name";
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

        public DataSet Total_VacantUserdashboard(string div_code, string subdivc = "0",string sf_Code="")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
			strQry = "exec getVacantUsers '"+ sf_Code + "','" + div_code + "','" + subdivc + "'";
           // strQry = "select count(sf_code) cnt from mas_salesforce where CHARINDEX(','+'" + div_code + "'+',',','+Division_Code+',')>0 and SF_Status='1'  and CHARINDEX(','+iif('" + subdivc + "'='0',subdivision_code,'" + subdivc + "')+',',','+subdivision_code+',')>0";
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

        public DataSet Total_Userdashboard(string div_code, string date, string subdivc = "0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            /*strQry = "select count(u.Sf_Code) as Tot_User,(select count(TP.sf_code)  from  TbMyDayPlan TP " +
                       " inner join Mas_Salesforce U on  u.Sf_Code=TP.sf_code where " +
                       " cast(CONVERT(varchar, TP.Pln_Date,101) as datetime)='" + date + "' and TP.FWFlg='L' " +
                       " and u.Division_Code='" + div_code + ",' and u.SF_Status='0') as Leave,(select count(u.Sf_Code) as Tot_User from Mas_Salesforce u where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='2')INactive " +
                       " from Mas_Salesforce u  where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='0'";*/
            strQry = "exec getTotal_Userdashboard '" + div_code + "','" + date + "','" + subdivc + "'";
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
        public DataSet Total_OutLetdashboard(string div_code, string date, string subdiv = "0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            /*strQry = "select count(ListedDrCode) as retailercount,(select count(A.Sf_Code)Total_Cal from [vwActivity_MSL_Details] M " +
                        "inner join [vwActivity_Report] A  on M.Trans_SlNo=A.Trans_SlNo and m.Division_Code=a.Division_Code  inner join " +
                        "Mas_ListedDr l on l.ListedDrCode=m.Trans_Detail_Info_Code  where  a.Division_Code='" + div_code + "'and " +
                        "cast(CONVERT(varchar, a.Activity_Date,101) as datetime)='" + date + "')Total_call " +
                        "from Mas_ListedDr " +
                        "where Division_Code='" + div_code + "' and ListedDr_Active_Flag='0'";*/
            strQry = "exec getDashboardRetVstCnt '" + div_code + "','" + date + "','" + subdiv + "'";
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

        public DataSet Total_Prodashboard(string div_code, string date, string subdiv = "0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            /*strQry = "select " +
                      "count( (case when M.POB_Value>0 then 1 end))  as Productive, count( (case when  M.POB_Value=0 then 1 end)) as Nonproductive " +
                      "from [vwActivity_MSL_Details] m  where m.Division_Code='" + div_code + "' and cast(CONVERT(varchar, m.Activity_Date,101) as datetime)='" + date + "'";*/
            strQry = "exec getDashboardRetTCPCCnt '" + div_code + "','" + date + "','" + subdiv + "'";
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

        public DataSet GET_Total_Userdashboard(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_Total_Userdashboard] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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
        public DataSet GET_Total_OutLetdashboard(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_Total_OutLetdashboard] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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

        public DataSet GET_Total_Prodashboard(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_Total_Prodashboard] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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

        public DataSet GET_Total_NewRetailerdashboard(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_Total_NewRetailerdashboard] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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



        public DataSet GET_In_Time_Statistics(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_In_Time_Statistics] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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
        public DataSet Total_Userdashboard_Att(string div_code, string date, string SubDiv = "0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();

            //strQry = "select Typ,sum(Cnt) Cnt from ( " +
            //         "select  (Case when FWFlg='F' or FWFlg='L' then FWFlg when FWFlg is null then 'iNA' else 'Oth' End) Typ " +
            //         ",count(U.SF_Code) Cnt " +
            //         "from Mas_Salesforce U    LEFT outer JOIN  " +
            //         "( " +
            //         "select row_number() over(partition by SF_Code order by Pln_Date) rwno, SF_Code,Pln_Date,cluster,wtype,FWFlg from TbMyDayPlan TP where cast(CONVERT(varchar, TP.Pln_Date,101) as datetime)='" + date + "' and " +
            //         "Division_Code='" + div_code + "' " +
            //         ")as TP " +
            //         "on  " +
            //         "TP.Sf_Code =u.sf_code  and rwno=1 " +
            //         "where u.Division_Code='" + div_code + ",' and u.SF_Status='0' " +
            //         "group by (Case when FWFlg='F' or FWFlg='L' then FWFlg when FWFlg is null then 'iNA' else 'Oth' End) " +
            //        "union " +
            //        "select 'F',0 union " +
            //        "select 'L',0 union " +
            //        "select 'iNA',0 union " +
            //        "select 'Oth',0 ) as t group by typ";
            strQry = "exec getTotal_Userdashboard_Att '" + div_code + "','" + date + "','" + SubDiv + "'";
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

        public DataSet GET_Total_Userdashboard_Att(string sf_code, string div_code, string date)
        {
            string sub = "";
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            //strQry = "select COUNT(Cust_Code) from Trans_Order_Head d inner join Mas_Stockist m ON m.Stockist_Code=d.Stockist_Code where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '"+date+"%'  and m.Division_Code="+div_code+"";
            strQry = "exec [GET_Total_Userdashboard_Att] '" + div_code + "','" + sub + "','" + sf_code + "','" + date + "'";
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
        public string deactive_Allowance_Type(string div_code, string alw_code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                strQry = "update Mas_Allowance_Type set Active_Flag='1', Delete_Date=getdate() where Division_Code='" + div_code + "' and ID='" + alw_code + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public string Add_Allowance_Type(string div_code, string alwname, string alwsname, string type, string uentr, string alw_code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();
            try
            {
                if (alw_code != string.Empty)
                {
                    strQry = "update Mas_Allowance_Type set Allowance_Name='" + alwname + "', Short_Name='" + alwsname + "',Type='" + type + "',user_enter='" + uentr + "' where Division_Code='" + div_code + "' and ID='" + alw_code + "'";
                }
                else
                {
                    strQry = " INSERT INTO [Mas_Allowance_Type]([Division_Code],[Allowance_Name],[Short_Name],[Type],[Active_Flag],[Created_Date],user_enter) " +
                     " VALUES ( '" + div_code + "' , '" + alwname + "', '" + alwsname + "', '" + type + "','0',getdate(),'" + uentr + "') ";
                }
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
        public DataSet Getretaileraccount_statement(string cust_code, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT  Edate,cust_code,EntryFrom, " +
             " (CASE WHEN etype = 'D' THEN( value) ELSE 0 END) as Debit,(CASE WHEN eType = 'C' THEN (Value) ELSE 0 END) as Credit" +

             "   FROM [customer_ledger_details] where cust_code='" + cust_code + "'   and CAST(CONVERT(VARCHAR, Edate, 101) AS datetime)  between '" + fdate + "' and '" + tdate + "' Order by Edate";

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
   public DataSet notice_board(string div_code)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec notice_board '"+ div_code + "'";
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
        public int deletenotice(string slno, string divCode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec deletenotice '" + divCode + "','" + slno + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet Getretaileraccount_statement_outstanding_value(string cust_code, string fdate, string tdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " 	select(SELECT SUM(Value) FROM Customer_Ledger_DETAILS c  INNER JOIN Mas_Listeddr d on d.ListedDrCode=c.Cust_Code  where  eType='D' and  CUST_CODE='" + cust_code + "' and CAST(CONVERT(VARCHAR, eDate, 101) AS DATETIME)<'" + fdate + "' )-(SELECT ISNULL(SUM(Value),0) FROM Customer_Ledger_DETAILS c INNER JOIN Mas_Listeddr d on d.ListedDrCode=c.Cust_Code  where eType='C'  and CUST_CODE='" + cust_code + "'  and  CAST(CONVERT(VARCHAR, eDate, 101) AS DATETIME)<'" + fdate + "')as outstanding_value ";

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
        public DataSet getretailerofftake(string div_code, string retailer_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Product_name,Product_code,sum(op)op,sum(cl)cl,sum(op)-sum(cl) sale from(SELECT Product_name,Product_code,isnull([1],0)cl,isnull([2],0)op  from " +
         " (select rw,Order_Date,Product_name,Product_code, " +
         "  case when rw=1 then clstock else quantity+clstock end  qty  from (select row_number() over( partition by Product_name order by Order_Date desc,Product_name )rw,Order_Date,Product_name,Quantity,isnull(clstock,0)clstock,Product_code from Trans_ORDER_DETAILS d  INNER JOIN  tRANS_oRDER_HEAD h on h.Trans_Sl_No=d.Trans_Sl_No where h.cust_Code='" + retailer_Code + "') " +
          "ff where rw in (1,2))tt	  " +

           " PIVOT(MAX(qty)  FOR rw IN ([1],[2])) AS PivotTable )hh " +

             " group by  Product_name,Product_code";


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
        public DataSet getSalesForce_Fare_With_Fare(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT distinct s.sf_code, s.sf_name ,d.designation_name ,s.sf_HQ ,d.Designation_Code,f.fare,s.division_code,Effective_Date  " +
           " FROM mas_salesforce s left join Mas_SF_Designation d on d.Designation_code=s.designation_code left  join Mas_Salesforcefare_KM f on f.sf_code=s.sf_code  " +
         " where  s.Division_Code = '" + div_code + ",' and s.sf_status=0 ";


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
        public DataSet Getproductstatewise1(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select * from Mas_PRoduct_detail  p  left  outer join Mas_StateProduct_TaxDetails t on t.Product_Code=P.Product_detail_Code and t.State_code='" + state_code + "'  where CHARINDEX( '," + state_code + ",', ',' +p.STATE_CODE + ',' ) > 0 and  p.pRODUCT_aCTIVE_FLAG=0  and  p.dIVISION_CODE='" + div_code + "'   ";





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
        public DataSet Gettaxdetails(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;



            strQry = "SELECT 0 as Tax_Id,'---Select---' as Tax_Name union all select Tax_Id,Tax_Name from Tax_Master where Tax_Active_Flag=0 AND dIVISION_CODE='" + div_code + "' ";


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
        public DataSet Getstockistaccount_statement(string cust_code, string fdate, string tdate, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "   SELECT  Ledg_Date,Dist_Code, EntryBy, " +
         "(CASE WHEN CalType = '0' THEN(   GStock) ELSE 0 END) as Debit,(CASE WHEN CalType = '1' THEN (  GStock) ELSE 0 END) as Credit " +

        " FROM [trans_stock_ledger] where Dist_Code='" + cust_code + "'   and CAST(CONVERT(VARCHAR, Ledg_Date, 101) AS datetime)  between '" + fdate + " ' and '" + tdate + "' and Prod_Code='" + product_code + "' Order by Ledg_Date,Ledger_ID";

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
        public DataSet Getstockistaccount_statement_outstanding_value(string cust_code, string fdate, string tdate, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select(SELECT SUM(GStock) FROM trans_stock_ledger c  INNER JOIN Mas_stockist d on d.stockist_code=c.Dist_Code  where  CalType='1' and  Prod_Code='" + product_code + "'  and  c.Dist_Code='" + cust_code + "' and CAST(CONVERT(VARCHAR, Ledg_Date, 101) AS DATETIME)<'" + fdate + "' )-(SELECT ISNULL(SUM(GStock),0) FROM trans_stock_ledger c INNER JOIN Mas_stockist d on d.stockist_code=c.Dist_Code  where CalType='0'  and  Prod_Code='" + product_code + "' and c.Dist_Code='" + cust_code + "'  and  CAST(CONVERT(VARCHAR, Ledg_Date, 101) AS DATETIME)<'" + fdate + "')as outstanding_value ";

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
        public DataSet Getvan_statement_outstanding_value(string sf_code, string div_code, string fdate, string tdate, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

          //  strQry = "    select(SELECT SUM(GQty) FROM Trans_VanStock_Ledger c  " +
          // "where  CalcTyp='1' and   PCode ='" + product_code + "' and ( c.from_loc='" + sf_code + "' OR c.To_Loc='" + sf_code + "')" +
          //"and CAST(CONVERT(VARCHAR, eDate, 101) AS DATETIME)<'" + fdate + "' )-(SELECT ISNULL(SUM(GQty),0) FROM Trans_VanStock_Ledger c " +
          //"  where CalcTyp='0' and   PCode ='" + product_code + "'and  ( c.from_loc='" + sf_code + "' OR c.To_Loc='" + sf_code + "')  and  CAST(CONVERT(VARCHAR, eDate, 101) AS DATETIME)<'" + fdate + "')as outstanding_value ";
            strQry = " exec trans_van_stock_bal '" + sf_code + "','" + div_code + "','" + fdate + "','" + tdate + "','" + product_code + "'";

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
        public DataSet Getvanaccount_statement(string sf_code, string div_code, string fdate, string tdate, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

         //   strQry = " SELECT  eDate,from_loc,To_Loc, EntryBy,  " +
         //"(CASE WHEN CalcTyp = '0' THEN(   GQty) ELSE 0 END) as Debit,(CASE WHEN CalcTyp = '1' THEN (  GQty) ELSE 0 END) as Credit  " +

         //"FROM Trans_VanStock_Ledger where ( from_loc='" + sf_code + "' OR To_Loc='" + sf_code + "' )  and CAST(CONVERT(VARCHAR, eDate, 101) AS datetime)  between '" + fdate + " ' and '" + tdate + "' and   PCode ='" + product_code + "' Order by eDate,Legr_ID";

            strQry = "exec trans_van_stock '" + sf_code + "','" + div_code + "', '" + fdate + "','" + tdate + "', '" + product_code + "'";
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
        public DataSet Getproduct_schememaster(string div_code, string state_code, string effective_date, string distributor, string effective_todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (distributor == "all")
            {
                strQry = "select p.product_detail_name,p.product_detail_code,'' scheme,''free,''discount,'N'package from  mas_product_detail p " +
" where p.division_code='" + div_code + "'and CHARINDEX( '," + state_code + ",', ',' +p.STATE_CODE + ',' ) > 0  and  p.pRODUCT_aCTIVE_FLAG=0 ";
            }
            else
            {

                strQry = "select p.product_detail_name,p.product_detail_code,at.* from  mas_product_detail p " +
        "left  join (select * from  mas_scheme s where s.state_code='" + state_code + "' and s.stockist_code='" + distributor + "' and CAST(CONVERT(VARCHAR, s.Effective_From, 101) AS DATETIME)='" + effective_date + "' and CAST(CONVERT(VARCHAR, s.Effective_To, 101) AS DATETIME)='" + effective_todate + "' and s.division_code='" + div_code + "'  )at on at.Product_Code=p.Product_Detail_Code where p.division_code='" + div_code + "'and CHARINDEX( '," + state_code + ",', ',' +p.STATE_CODE + ',' ) > 0  and  p.pRODUCT_aCTIVE_FLAG=0 ";
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
        public DataSet getretailerofftake_productwise(string div_code, string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "EXEC  retailer_offtake_productwise '" + sf_Code + "','" + div_code + "'";


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
  public string Notice_Comment_update(string div_code, string comment, string type, string date,string sl_no)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            DB_EReporting db = new DB_EReporting();

            try
            {
                strQry ="exec  Notice_Comment_update '"+ div_code + "','"+ comment + "','"+ type + "','"+ date + "','"+ sl_no + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (iReturn > 0)
            {
                return iReturn.ToString();
            }
            else
            {
                return "0";
            }
        }
  public DataSet getNoticeboardedit(string div, string sl_no)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC getNoticeboardedit '" + div + "', " + sl_no + " ";

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
 public DataSet noticeboard_list(string div_code)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "exec noticeboard_list '" + div_code + "'";
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
public int deletenoticeboard(string slno, string divCode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "exec deletenoticeboard '" + divCode + "','" + slno + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet countinRetailersToday(string div_code, string FToday, string subdiv = "0")
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();

            //strQry = "select count(distinct ListedDrCode) lrdr_count from Mas_ListedDr Ld where ld.division_code = '" + div_code + "' and cast(CONVERT(varchar, ListedDr_Created_Date, 101) as datetime) = '" + FToday + "'";
            strQry = "exec getDashboardNewRetCnt '" + div_code + "','" + FToday + "','" + subdiv + "'";
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
		 public DataSet Total_Userdashboard_country_st(string div_code, string cntry,string stat, string date)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select count(u.Sf_Code) as Tot_User,(select count(TP.sf_code)  from  TbMyDayPlan TP " +
                       " inner join Mas_Salesforce U on  u.Sf_Code=TP.sf_code where " +
                       " cast(CONVERT(varchar, TP.Pln_Date,101) as datetime)='" + date + "' and TP.FWFlg='L' " +
                       " and u.Division_Code='" + div_code + ",' and u.SF_Status='0') as Leave,(select count(u.Sf_Code) as Tot_User from Mas_Salesforce u where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='2')INactive " +
                       " from Mas_Salesforce u  inner join mas_State ms on u.state_Code=ms.state_code and ms.Country_code='" + cntry + "'  where " +
                       " u.Division_Code='" + div_code + ",' and u.SF_Status='0' and u.state_Code='" + stat + "'";
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
		 public DataSet Total_VacantUserdashboard_country_st(string div_code, string cntry,string stat)
        {

            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select count(sf_code) cnt from mas_salesforce u inner join mas_State ms on u.state_Code=ms.state_code  where CHARINDEX(','+'" + div_code + "'+',',','+Division_Code+',')>0 and SF_Status='1' and ms.Country_code='" + cntry + "' and  u.state_Code='" + stat + "'";
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

    }


}

