using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Configuration;
namespace Bus_EReport
{
    public class Distance_calculation
    {
        public Distance_calculation()
        {
        }
        private string strQry = string.Empty;
        public DataSet getFieldForce(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;
            strQry = "SELECT sf_code,sf_name,sf_hq " +
                     " FROM mas_salesforce WHERE sf_status=0 and designation_code=1 And Division_Code like '%" + divcode + "%'" +
                     " ORDER BY sf_name";
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
        public DataSet getDistance(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select From_Code,To_Code,Sf_HQ,Territory_Name,Distance_In_Kms, "+
            " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Town_Cat " +
            " from Mas_Distance_Fixation D inner join Mas_Salesforce S on D.SF_Code=S.Sf_Code " +
        "inner join Mas_Territory_Creation T on T.Territory_Code=D.To_Code where S.Sf_Code='" + sf_code + "' AND T.Division_Code like '" + divcode + "'";
                    
           
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
        public DataSet getPlace(string divcode, string sf_code,string mnth,string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsPlace = null;
            strQry = "select T.Territory_Code,Territory_Name,sf_hq, "+
             " case when T.Territory_Cat=1 then 'HQ' " +
                       " else case when T.Territory_Cat=2 then 'EX' " +
                       " else case when T.Territory_Cat=3 then 'OS' " +
                       " else 'OS-EX' " +
                       " end end end as Territory_Cat, " +
            "Activity_Date,COUNT(distinct Trans_Detail_Info_Code)cnt from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo inner join Mas_ListedDr L on L.ListedDr_Sl_No=D.Trans_Detail_Info_Code inner join Mas_Territory_Creation T on T.Territory_Code=L.Territory_Code inner join  mas_salesforce sf on sf.sf_code=T.sf_code " +
        "where H.Sf_Code='" + sf_code + "' and D.Division_Code='" + divcode + "' and MONTH(Activity_Date)='" + mnth + "' and YEAR(Activity_Date)='" + yr + "' group by T.Territory_Code,Territory_Name,Territory_Cat,sf_hq,Activity_Date order by Activity_Date";


            try
            {
                dsPlace = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsPlace;
        }
        public DataSet getExpense(string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "exec getExpense '" + sf_code + "', '" + mnth + "','" + yr + "'";
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
        public DataSet getAllow(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select HQ_Allowance hq_allowance,EX_HQ_Allowance ex_allowance,OS_Allowance os_allowance,FareKm_Allowance fare from Mas_Allowance_Fixation where SF_Code='" + sf_code + "' ";
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
        public DataTable getOthrExp(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select Expense_Parameter_Code,Expense_Parameter_Name,Fixed_Amount from Fixed_Variable_Expense_Setup where Division_code='" + divcode + "' and Param_type<>'F' ";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }

        public DataTable getExpParam(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select M.Expense_Parameter_Code,M.Expense_Parameter_Name from Mas_Expense_Parameter M inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "'  order by M.Expense_Parameter_Name";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataTable getExpParamAmt(string sf_code, String divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "select sf_code,M.expense_parameter_code,M.Expense_Parameter_Name ,amount from Trans_Fixed_Expense_Detail D inner join Trans_Fixed_Expense_Head H on D.sl_no=H.sl_no left outer join Mas_Expense_Parameter M on M.Expense_Parameter_Code=H.expense_parameter_code inner join Fixed_Variable_Expense_Setup F  on M.Expense_Parameter_Code=F.Expense_Parameter_Code where sf_code='" + sf_code + "' and F.param_type='F' and F.desig_code='MR' and F.division_code='" + divcode + "' ";
            try
            {
                dsDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDivision;
        }
        public DataSet getOsDistance(string sf_code,string places)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select top 1 Territory_Code,Territory_Name,distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code where D.sf_code='" + sf_code + "' and Territory_Cat='3'  and Territory_name in(" + places + ") order by distance_in_kms desc";
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

        public DataSet getOsExDistance(string sf_code, string places,string frCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select isnull(sum(distance_in_kms),0) distance_in_kms from Mas_Distance_Fixation D inner join mas_territory_creation T on D.to_code=T.Territory_Code and From_Code='" + frCode + "' where D.sf_code='" + sf_code + "' and Territory_Cat='4' and Territory_name in(" + places + ")";
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
        public DataSet getExDistance(string divcode, string sf_code, string mnth, string yr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDivision = null;
            strQry = "select max(Distance_in_kms*2)dist,activity_date adate from DCRDetail_Lst_Trans D inner join DCRMain_Trans H on D.Trans_SlNo=H.Trans_SlNo "+
            "inner join Mas_ListedDr L on L.ListedDr_Sl_No=D.Trans_Detail_Info_Code "+
           " inner join Mas_Territory_Creation T on T.Territory_Code=L.Territory_Code "+
            "inner join  mas_salesforce sf on sf.sf_code=T.sf_code "+
            "inner join Mas_Distance_Fixation DS on DS.To_Code=T.Territory_Code "+
        "where H.Sf_Code='" + sf_code + "' and D.Division_Code='" + divcode + "' and MONTH(Activity_Date)='" + mnth + "' " +
       " and YEAR(Activity_Date)='" + yr + "' " +
       " group by activity_date order by Activity_Date";
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
        public bool RecordExist(string frm_code,string to_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(To_Code) FROM mas_Distance_Fixation WHERE From_Code='" + frm_code + "' and To_Code='" + to_code + "' and sf_code = '" + sf_code + "'";
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

        public bool headRecordExist(string month, string year, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(sl_No) FROM Trans_Expense_Head WHERE SF_Code='"+sf_code+"' and Expense_Month="+month+" and Expense_Year=" + year;
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
        public int deleteSfDistance(string sf_code)
        {
            int iReturn = -1;
            DB_EReporting db = new DB_EReporting();
            strQry = "delete from mas_Distance_Fixation where Sf_Code = '" + sf_code + "'";
            iReturn = db.ExecQry(strQry);
            return iReturn;
        }
        public int addOrUpdate(string sf_code, string distance, string Terr_To, string Terr_From, string Terr_cat)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                int Division_Code = -1;
                
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);
                  {

                      strQry = "insert into mas_Distance_Fixation (SF_Code,From_Code,To_Code,Town_Cat,Distance_In_Kms,division_code, " +
                         " Flag,Created_Date) " +
                         " VALUES('" + sf_code + "', '" + Terr_From + "', '" + Terr_To + "', '" + Terr_cat + "', '" + distance + "','" + Division_Code + "',  " +
                         " 0, getdate())";
                      iReturn = db.ExecQry(strQry);
            

                  }
            }
                  catch (Exception ex)
            {
                throw ex;
            }
          
            return iReturn;
        }

        public int addHeadRecord(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                if (headRecordExist(values["month"], values["year"], values["sf_code"]))
                {
                    strQry = "delete from Trans_Expense_detail " +
                      "where sl_no in(SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + ") ";
                    iReturn = db.ExecQry(strQry);
                    strQry = "delete from Trans_Expense_Head WHERE SF_Code='" + values["sf_code"] + "' and Expense_Month=" + values["month"] + " and Expense_Year=" + values["year"] + "";
                    iReturn = db.ExecQry(strQry);

                    iReturn = insertHeadRecord(values, iReturn, db);
                }
                else 
                {
                    iReturn = insertHeadRecord(values, iReturn, db);

                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        private int insertHeadRecord(Dictionary<String, String> values, int iReturn, DB_EReporting db)
        {
            strQry = "INSERT INTO Trans_Expense_Head " +
                "(Sl_No,SF_Code,Expense_Month,Expense_Year,Division_Code,sndhqfl,submission_date,Expense_Period)" +
                "VALUES ((select isnull(max(Sl_No)+1,1) id from Trans_Expense_Head),'" +
                values["sf_code"] + "'," + values["month"] + "," + values["year"] + "," + values["div_code"] + "," + values["flag"] + ",'" + values["s_date"] + "',"+values["flag"]+")";
            iReturn = db.ExecQry(strQry);
            return iReturn;
        }
        public int addDetailRecord(Dictionary<String, String> values)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                if (headRecordExist(values["month"],values["year"],values["sf_code"]))
                {
                    strQry = "INSERT INTO Trans_Expense_Detail " +
                        "(Sl_No,Expense_Date,Expense_Day,Expense_wtype,Place_of_Work,Expense_All_Type,Expense_Allowance,Expense_Distance,Expense_Fare,Expense_Total,Division_Code)" +
                        "VALUES ((SELECT sl_No FROM Trans_Expense_Head WHERE SF_Code='"+values["sf_code"]+"' and Expense_Month="+values["month"]+" and Expense_Year=" + values["year"]+"),'" +
                        values["adate"]+ "','" + values["dayName"] + "','" + values["workType"] + "','" + values["terrName"] + "','" + values["cat"]
                        + "','" + values["allowance"]+"','" + values["distance"]+"','" + values["fare"]+"','" + values["total"]+"','" + values["div_code"]+"')";
                    iReturn = db.ExecQry(strQry);

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        
    }
}
