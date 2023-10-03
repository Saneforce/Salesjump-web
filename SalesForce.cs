 ﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;
using System.Windows.Forms;

namespace Bus_EReport
{
    public class SalesForce
    {
        private string strQry = string.Empty;
        DataTable dt = new DataTable();
        DataTable dt_recursive_Aud = new DataTable();

        DataRow dr = null;
        string Audit_mgr = string.Empty; // Added by Sri - 29Aug15
        string Audit_mgr_All = string.Empty; // Added by Sri - 29Aug15
        int iReturn_Backup = -1;
        public DataSet getSalesForce_BulkEdit(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            //strQry = " SELECT Sf_Code,Sf_Name,Sf_UserName,Sf_HQ,State_Code,Sf_Password,sf_emp_id,Designation_Code,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date FROM  Mas_Salesforce " +
            //         " WHERE Division_Code= '" + divcode + "' " +
            //         " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " ORDER BY 2";

            strQry = " SELECT Sf_Code,Sf_Name,Sf_UserName,Sf_HQ,State_Code,Sf_Password,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.Designation_Short_Name as Designation_Name,a.Designation_Code " +
                     " FROM  Mas_Salesforce a join Mas_SF_Designation b on a.Designation_Code=b.Designation_Code WHERE " +
                     " (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 AND lower(sf_code) != 'admin'" +
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
        public DataSet getSalesForce_BulkEditFind(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Sf_Code,Sf_Name,Sf_UserName,UsrDfd_UserName,Sf_HQ,State_Code,Sf_Password,sf_emp_id,sf_desgn,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.Designation_Short_Name as Designation_Name,b.Designation_Short_Name,a.Designation_Code FROM Mas_Salesforce a,Mas_SF_Designation b " +

                     " WHERE SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
                      " and a.Designation_Code=b.Designation_Code" +
                     " AND lower(sf_code) != 'admin' " +
                     sFindQry +
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
        public DataSet GetStockist_subdivisionwisee(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
                     " UNION  all " +

                "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag='0'";

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
        public DataSet purchase_view_detailss(string divcode, string date, string stockist_code, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "Declare  @rlquotient int,@rlremainder int " +

                "SELECT  ROUND(((floor(dd.rlquotient)+dd.rlremainder)+ receipt_qty),2) as receipt_qty  from (select SUM(s.Rec_Qty) as receipt_qty,s.Conversion_Qty, " +
                    "rlquotient=((SUM(s.Rec_Pieces))/(nullif(s.Conversion_Qty, 0))) , " +
           "rlremainder=(((cast(SUM(s.Rec_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100)  " +
           " from Trans_Secondary_Sales_Details s  where  CONVERT(VARCHAR(25), date, 126)  like '" + date + "%' and   s.Product_Code='" + product_code + "' and Stockist_Code='" + stockist_code + "' group by s.Conversion_Qty)dd ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet getSalesForce_BulkEdit_Rpt(string divcode, string sReport, string RptMgr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Sf_Code,Sf_Name,Sf_UserName,UsrDfd_UserName,Sf_HQ,State_Code,Sf_Password,sf_emp_id,a.Designation_Code,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date, b.Designation_Short_Name as Designation_Name " +
                     " FROM  Mas_Salesforce a join Mas_SF_Designation b on a.Designation_Code=b.Designation_Code " +
                     " WHERE (a.Division_Code like '" + divcode + ',' + "%'  or " +
                      " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
                     " AND (TP_Reporting_SF = '" + sReport + "'" +
                     " OR sf_Code = '" + sReport + "' " +
                     " OR sf_Code = '" + RptMgr + "' )" +
                     " AND lower(sf_code) != 'admin' " +
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

        public DataSet getSfName(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Name FROM  Mas_Salesforce " +
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

        public int BulkEdit(string str, string SF_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + SF_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Salesforce SET " + str + "  Where SF_Code='" + SF_Code + "'";
                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + SF_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public DataSet getState()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT '' as State_Code, '---Select---' as StateName " +
                     " UNION " +
                     " select State_Code,StateName  from Mas_State " +
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

        public DataSet getState_BulkEdit(string SF_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT State_Code FROM  Mas_Salesforce " +
                     " WHERE SF_Code='" + SF_Code + "' AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') ";
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

        public DataSet getSFType(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT '' as Sf_Code, '---Select---' as sf_name " +
                   " UNION " +
                   "SELECT 'admin' as Sf_Code, 'admin' as sf_name " +
                   " UNION " +
                   "select Sf_Code,sf_name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ from Mas_Salesforce where  (Division_Code like '" + div_code + ',' + "%'  or " +
                   " Division_Code like '%" + ',' + div_code + ',' + "%')  AND sf_type = 2 and sf_TP_Active_Flag !=1 and SF_Status !=2";

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

        public DataSet getSFType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "SELECT '' as Sf_Code, '---Select Manager---' as sf_name " +
                    " UNION " +
                    "SELECT 'admin' as Sf_Code, 'admin' as sf_name " +
                    " UNION " +
                    " select Sf_Code,sf_name from Mas_Salesforce where sf_type = 2"
                    ;

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

        public DataSet getUserList_Reporting(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                      " UNION " +
                      " SELECT 'admin' as sf_code, 'admin' as Sf_Name " +
                      " UNION " +
                      " select Sf_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name  from mas_salesforce " +
                         " where sf_TP_Active_Flag in (0,2) and sf_code != 'admin'  and Sf_Code in " +
                         " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and " +
                         " (Division_Code like '" + div_code + ',' + "%'  or " +
                       " Division_Code like '%" + ',' + div_code + ',' + "%' ))";

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
        public DataSet getUserList_Reporting()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select a.Sf_Code,a.Sf_Name  from mas_salesforce a, Mas_Division b " +
                      " where a.sf_TP_Active_Flag = 0 and" +
                       "(a.Division_Code like b.Division_Code +','+'%' or a.Division_Code like '%'+','+b.Division_Code+','+'%')  " +
                        "and b.Division_Active_Flag = 0 " +
                        " and  a.Sf_Code in (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' ) " +
                        " order by 2 ";


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

        // Re_P_To
        public DataSet getU_L_R(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select Sf_Code,Sf_Name  from mas_salesforce " +
                        " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
                        " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and TP_Reporting_SF = '" + sf_code + "' " +
                       " and (Division_Code like '" + div_code + ',' + "%'  or " +
                       " Division_Code like '%" + ',' + div_code + ',' + "%') ) ";

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
        // Approval Screen Alphabatical Search Option 
        public DataSet getUserList_Reporting(string div_code, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '' as sf_code, '---Select the Manager---' as Sf_Name " +
                     " UNION " +
                     " select Sf_Code,Sf_Name  from mas_salesforce " +
                        " where sf_TP_Active_Flag in (0,2) AND LEFT(Sf_Name,1) = '" + sAlpha + "'  and Sf_Code in " +
                        " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and " +
                        " (Division_Code like '" + div_code + ',' + "%'  or " +
                         " Division_Code like '%" + ',' + div_code + ',' + "%')) ";
            //" (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and Division_Code = '" + div_code + "' ) " +
            //" order by Sf_Name ";

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

        public DataSet SF_Hierarchy(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, sf_Type,Reporting_To_SF,sf_hq,sf_password " +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status != 2 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND Reporting_To_SF =  '" + sf_code + "' " +
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

        public DataSet SalesForceList(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC getHyrSFList '" + sf_code + "','" + divcode + "'";

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
        public DataSet UserList(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList '" + divcode + "', '" + sf_code + "' ";

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


        public DataSet UserList(string divcode, string sf_code, string level)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Mail '" + divcode + "', '" + sf_code + "', '" + level + "' ";

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


        public DataSet getSalesForcelist_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0 and sf_type = 2 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 0 " +
                     " ORDER BY 1";
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

        public DataSet getSalesForcelist_Reporting(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (sf_code != "admin")
            {
                strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code " +
                         " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                         " WHERE SF_Status=0 " +
                         " AND lower(sf_code) != 'admin' " +
                         " AND a.TP_Reporting_SF != '" + sf_code + "' " +
                         " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                         " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                         " ORDER BY 2";
            }
            else
            {
                strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code " +
                        " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                        " WHERE SF_Status=0 " +
                        " AND lower(sf_code) != 'admin' " +
                        " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                        " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                        " ORDER BY 2";
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

        // Reporting_To ( Values in Grid)
        public DataSet getSalesForcelist_Reporting_Approval(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ,a.sf_Designation_Short_Name as Designation_Name," +
                     "(select s.Sf_Name + ' - ' + s.sf_Designation_Short_Name + ' - ' + s.Sf_HQ from mas_salesforce s where sf_code=a.TP_Reporting_SF) as Reporting_To" +
                    " FROM mas_salesforce a" +
                    " WHERE lower(sf_code) != 'admin' " +
                    " AND a.TP_Reporting_SF = '" + sf_code + "' " +
                    " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') and SF_Status !=2 " +
                    " ORDER BY 2";

            //SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ,(select sfc.Sf_Name from mas_salesforce_AM sfc where   sf_code=a.Reporting_To) as Reporting_To,(select c.Sf_Name from Mas_Salesforce_AM c where Sf_Code=a.DCR_AM) as DCR_AM  FROM mas_salesforce_AM a WHERE lower(sf_code) != 'admin'  AND a.Reporting_To = 'MGR0003'  AND a.Division_Code = '9'  ORDER BY 2
            //strQry = " SELECT SF_Code,Sf_Name,Sf_HQ,Reporting_To FROM  Mas_Salesforce_AM " +
            //" WHERE Reporting_To ='" + sf_code + "' AND Division_Code= '" + divcode + "' ";



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


        // Dropdown Reporting 
        public DataSet getView_AM(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ," +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Reporting_To) as Reporting_To, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.DCR_AM) as DCR_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.TP_AM) as TP_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.LstDr_AM) as LstDr_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Leave_AM) as Leave_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.SS_AM) as SS_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Expense_AM) as Expense_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Otr_AM) as Otr_AM " +
            //         " FROM mas_salesforce_AM a" +
            //         " WHERE lower(sf_code) != 'admin' " +
            //         " AND a.Reporting_To = '" + sf_code + "' " +
            //         " and a.Division_Code = '" + div_code + "' " +
            //         " ORDER BY 2";

            strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=a.Reporting_To) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                    " WHERE c.sf_TP_Active_Flag=0 and c.SF_Status=0 " +
                    " AND a.Reporting_To = '" + sf_code + "' " +
                    " and a.Division_Code = '" + div_code + "' " +
                    " ORDER BY 2";


            //strQry = " SELECT SF_Code,Sf_Name, Reporting_To_SF FROM  Mas_Salesforce " +
            //" WHERE Reporting_To_SF ='" + sf_code + "' AND Division_Code= '" + div_code + "' ";
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
        public DataSet getView_AM_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ," +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Reporting_To) as Reporting_To, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.DCR_AM) as DCR_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.TP_AM) as TP_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.LstDr_AM) as LstDr_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Leave_AM) as Leave_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.SS_AM) as SS_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Expense_AM) as Expense_AM, " +
            //         "(select Sf_Name from mas_salesforce_AM where sf_code=a.Otr_AM) as Otr_AM " +
            //         " FROM mas_salesforce_AM a" +
            //         " WHERE lower(sf_code) != 'admin' " +
            //    //  " AND a.Reporting_To = '" + sf_code + "' " +
            //         " and a.Division_Code = '" + div_code + "' " +
            //         " ORDER BY 2";

            //strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ ,c.sf_Designation_Short_Name as Designation_Name," +
            //          "(select Sf_Name  from mas_salesforce_AM where sf_code=a.Reporting_To)+'-'+ " +
            //          "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
            //          "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.DCR_AM) as DCR_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.TP_AM) as TP_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.LstDr_AM) as LstDr_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.Leave_AM) as Leave_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.SS_AM) as SS_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.Expense_AM) as Expense_AM, " +
            //          "(select Sf_Name from mas_salesforce_AM where sf_code=a.Otr_AM) as Otr_AM " +
            //          " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
            //          " WHERE lower(a.sf_code) != 'admin' and c.sf_TP_Active_Flag=0 and c.SF_Status=0" +
            //    //  " AND a.Reporting_To = '" + sf_code + "' " +
            //          " and a.Division_Code = '" + div_code + "' " +
            //          " ORDER BY 2";

            strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ ,c.sf_Designation_Short_Name as Designation_Name," +
                      "(select Sf_Name  from mas_salesforce where sf_code=a.Reporting_To)+'-'+ " +
                      "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
                      "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
                      "(select Sf_Code from Mas_Salesforce where sf_code=a.Reporting_To) Reporting, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                      "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                      " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                      " WHERE c.sf_TP_Active_Flag=0 and c.SF_Status=0" +
                //  " AND a.Reporting_To = '" + sf_code + "' " +
                      " and a.Division_Code = '" + div_code + "' " +
                      " ORDER BY 2";

            //strQry = " SELECT SF_Code,Sf_Name, Reporting_To_SF FROM  Mas_Salesforce " +
            //" WHERE Reporting_To_SF ='" + sf_code + "' AND Division_Code= '" + div_code + "' ";
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

        public DataSet getSalesForcelist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Name,a.State_Code as state_code " +
            //         " FROM mas_salesforce a, mas_state b, Mas_SF_Designation c" +
            //         " WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " AND a.State_Code=b.State_Code " +
            //         " And a.Designation_Code = c.Designation_Code" +
            //         " AND a.Division_Code like '" + divcode + ',' + "%'  or " +
            //         " a.Division_Code like '%" + ',' + divcode + "' or" +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%' "+
            //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
            //         " ORDER BY Sf_Name";

            strQry = "  SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, " +
                     "  (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     "  (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "  a.sf_type as Type, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type," +
                      " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                      " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                      " WHERE (a.Division_Code like '" + divcode + ',' + "%' or  a.Division_Code like '%" + ',' + divcode + ',' + "%') and  " +
                      " SF_Status=0  AND lower(sf_code) != 'admin' AND a.sf_Tp_Active_flag = 0 " +
                      " ORDER BY Sf_Name";
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
        public DataSet getSalesForcelist()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT a.SF_Code, a.Sf_Name, a.Division_Code,  a.Reporting_To_SF" +
                     " FROM mas_salesforce a, mas_state b" +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
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

        public DataSet getDoctorCount_SFWise(string div_code, string mgr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = " select distinct b.state_code, b.statename, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and Division_Code = a.Division_Code and " +
            //" State_Code = a.State_Code) as MR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and Division_Code = a.Division_Code and " +
            //" State_Code = a.State_Code) as MGR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and sf_Tp_Active_flag = 0 and Division_Code = a.Division_Code " +
            //" and State_Code = a.State_Code) as Active_MR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and sf_Tp_Active_flag = 0 and Division_Code = a.Division_Code " +
            //" and State_Code = a.State_Code) as Active_MGR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and sf_Tp_Active_flag = 1 and Division_Code = a.Division_Code " +
            //" and State_Code = a.State_Code) as DeActive_MR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and sf_Tp_Active_flag = 1 and Division_Code = a.Division_Code " +
            //" and State_Code = a.State_Code) as DeActive_MGR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where SF_Status=1 and sf_type=1  and Division_Code = a.Division_Code and " +
            //" State_Code = a.State_Code) as Block_MR_Count, " +
            //" (select COUNT(sf_code) from Mas_Salesforce where SF_Status=1  and sf_type=2 and Division_Code = a.Division_Code and " +
            //" State_Code = a.State_Code) as Block_MGR_Count " +
            //" from Mas_Salesforce a, Mas_State b " +
            //" where a.State_Code = b.State_Code and a.Sf_Code != 'admin' and a.Division_Code = '" + divcode + "' " +
            //" ORDER BY 2";

            strQry = "select sf_code, sf_name " +
                        " from Mas_Salesforce " +
                        " where (Division_Code like '" + div_code + ',' + "%' or Division_Code like '%" + ',' + div_code + ',' + "%')  and " +
                        " TP_Reporting_SF = '" + mgr_code + "' " +
                        " order by sf_name ";

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

        public DataSet getDoctorCount_statewise(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select sf_code, sf_name, sf_username, sf_HQ, sf_Designation_Short_Name, sf_Designation_Short_Name as Designation_Short_Name " +
                     " from Mas_Salesforce " +
                     " where Sf_Code !='admin' and (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and State_Code = '" + state_code + "' and " +
                     " sf_type = 1 order by sf_name ";

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

        public DataSet getSF_Status(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " select distinct b.state_code, b.statename, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and Division_Code = a.Division_Code and SF_Status !=2 and  " +
            " State_Code = a.State_Code) as MR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and Division_Code = a.Division_Code and SF_Status !=2 and " +
            " State_Code = a.State_Code) as MGR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and sf_Tp_Active_flag = 0 and Division_Code = a.Division_Code and SF_Status !=2 " +
            " and State_Code = a.State_Code) as Active_MR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and sf_Tp_Active_flag = 0 and Division_Code = a.Division_Code and SF_Status !=2 " +
            " and State_Code = a.State_Code) as Active_MGR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=1 and sf_Tp_Active_flag = 1 and Division_Code = a.Division_Code and SF_Status !=2 " +
            " and State_Code = a.State_Code) as DeActive_MR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where sf_type=2  and sf_Tp_Active_flag = 1 and Division_Code = a.Division_Code and SF_Status !=2" +
            " and State_Code = a.State_Code) as DeActive_MGR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where SF_Status=1 and sf_type=1  and Division_Code = a.Division_Code and SF_Status !=2 and " +
            " State_Code = a.State_Code) as Block_MR_Count, " +
            " (select COUNT(sf_code) from Mas_Salesforce where SF_Status=1  and sf_type=2 and Division_Code = a.Division_Code and SF_Status !=2 and " +
            " State_Code = a.State_Code) as Block_MGR_Count " +
            " from Mas_Salesforce a, Mas_State b " +
            " where a.State_Code = b.State_Code and a.Sf_Code != 'admin' and (a.Division_Code like '" + divcode + ',' + "%'  or " +
            " a.Division_Code like '%" + ',' + divcode + ',' + "%') ORDER BY 2";

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
        // Approval Managers
        public DataSet getApproval_Managers(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " SELECT sf_code,sf_name,sf_hq,TP_Reporting_SF " +
                     " FROM Mas_Salesforce " +
                     " where (Division_Code like '" + divcode + ',' + "%' or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') AND sf_type = 1 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND TP_Reporting_SF = '" + sf_code + "' " +
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
        public DataSet getSalesForcelist(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
            //         " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
            //         " WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
            //         " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
            //         " ORDER BY Sf_Name";

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, " +
                      " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     "  (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "  Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
                     " ORDER BY Sf_Name";
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

        public DataSet getSalesForceBlklist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_BlkReason,sf_Designation_Short_Name " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code" +
                     " WHERE SF_Status=1 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 1 AND a.sf_Tp_Active_flag = 0 " +
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
        public DataSet getSalesForceVaclist(string divcode, string sftype)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code ,sf_Designation_Short_Name ," +
                    "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                    " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                    " WHERE SF_Status=0 " +
                    " AND lower(sf_code) != 'admin' " +
                    " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                    " AND a.sf_type = '" + sftype + "' " +
                    " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 " +
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
        public DataSet getSalesForceVaclist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code,sf_Designation_Short_Name ," +
                    "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                    " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                    " WHERE a.SF_Status=0 " +
                    " AND lower(a.sf_code) != 'admin' " +
                    " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                    " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 " +
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

        public DataSet FindSalesForcelist(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
            //         " FROM mas_salesforce a, mas_state b,Mas_SF_Designation c " +
            //         " WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " AND a.State_Code=b.State_Code " +
            //         " AND a.Designation_Code = c.Designation_Code" +
            //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
            //         sFindQry +
            //         " ORDER BY Sf_Name";
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "sf_type as Type , case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a, mas_state b,Mas_SF_Designation c " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                     " AND a.Designation_Code = c.Designation_Code" +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     sFindQry +
                     " ORDER BY Sf_Name";

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
        public DataSet getSalesForce_ReportingTo_TourPlan(string div_code, string reporting_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT '' as sf_code, '---Select---' as Sf_Name " +
                     " UNION " +
                     "SELECT sf_code, Sf_Name " +
                     " FROM mas_salesforce " +
                     " WHERE Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
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

        public DataSet SF_ReportingTo_TourPlan(string div_code, string reporting_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT a.sf_code, a.Sf_Name, a.Sf_HQ,a.sf_type,b.Designation_Short_Name" +
                     " FROM mas_salesforce a, Mas_SF_Designation b" +
                     " WHERE a.Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     "and a.Designation_Code=b.Designation_Code " +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
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

        public DataSet getSalesForce_ReportingTo(string div_code, string reporting_to)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT a.sf_code, a.Sf_Name ,a.Sf_UserName,c.Designation_Name,a.Sf_HQ" +
                     " FROM mas_salesforce a,Mas_SF_Designation c" +
                     " WHERE a.Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND a.Designation_Code = c.Designation_Code" +
                     " AND (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') and SF_Status !=2 " +
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

        public DataSet getSalesForce_ReportingTo_MGR(string div_code, string reporting_to, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code, Sf_Name " +
                     " FROM mas_salesforce " +
                     " WHERE Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND sf_code != '" + sf_code + "' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
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

        public DataSet getSFCode_Manager(string div_code, string reporting_to, string emp_id, string sf_name, string user_name, string password)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code" +
                     " FROM mas_salesforce " +
                     " WHERE sf_type=2 and Reporting_To_SF= '" + reporting_to + "' " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_emp_id = '" + emp_id + "' and Sf_Name ='" + sf_name + "' " +
                     " and Sf_UserName = '" + user_name + "' and Sf_Password = '" + password + "' ";
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
        // New Change Reporting
        public DataSet Change_Rep(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_Reporting_To  '" + sf_code + "' ";

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

        public DataSet getSalesForce_ReMap_ReportingTo(string div_code, string sf_code, string reporting_sf)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT a.sf_code, a.Sf_Name ,a.Sf_UserName,c.Designation_Name,a.Sf_HQ" +
                     " FROM mas_salesforce a,Mas_SF_Designation c" +
                     " WHERE a.sf_code IN " +
                     " (SELECT sf_team FROM Mas_Salesforce_Vac_RepTeam WHERE Sf_Code = '" + sf_code + "' and Reporting_To_SF='" + reporting_sf + "' and division_code like '" + div_code + "' )  and SF_Status !=2  " +
                     " AND a.Designation_Code = c.Designation_Code" +
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

        public DataSet getsf(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code, Sf_Name,convert(varchar,Sf_TP_DCR_Active_Dt,103) Sf_TP_DCR_Active_Dt,convert(varchar,Last_DCR_Date,103) Last_DCR_Date" +
                     " FROM mas_salesforce " +
                     " WHERE lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
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
        public DataSet getsf(string div_code, string sReport, string ReportingMGR)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code, Sf_Name,convert(varchar,Sf_TP_DCR_Active_Dt,101) Sf_TP_DCR_Active_Dt,convert(varchar,Last_DCR_Date,101) Last_DCR_Date" +
                     " FROM mas_salesforce " +
                     " WHERE lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
                     " AND (TP_Reporting_SF = '" + sReport + "'" +
                     " OR sf_Code = '" + sReport + "' " +
                     " OR sf_Code = '" + ReportingMGR + "' )" +
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
        public DataSet getsf_tp(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code, Sf_Name,convert(varchar,sf_TP_Active_Dt,103) sf_TP_Active_Dt,convert(varchar,Last_TP_Date,103) Last_TP_Date" +
                     " FROM mas_salesforce " +
                     " WHERE lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                      " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +

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


        public DataSet getReportingTo(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT Reporting_To_SF" +
                     " FROM mas_salesforce " +
                      " WHERE sf_code = '" + sf_code + "' ";
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

        // Datasource Reporting
        public DataSet getR_To(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code,sf_name,Reporting_To_SF " +
                     " FROM Mas_salesforce " +
                     " where Reporting_To_SF = '" + sf_code + "' ";
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
        public DataSet getActiveReportingTo(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code" +
                     " FROM mas_salesforce " +
                      " WHERE Reporting_To_SF = '" + sf_code + "' ";
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


        public DataSet getSalesForce_ReportingTo(string div_code, string reporting_to, int sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT 'admin' as Sf_Code, 'admin' as sf_name " +
                    " UNION " +
                    "SELECT sf_code, Sf_Name " +
                     " FROM mas_salesforce " +
                     " WHERE sf_code != '" + reporting_to + "' " +
                     " AND sf_type = '" + sf_type + "' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') and sf_TP_Active_Flag !=1 and SF_Status !=2" +
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

        public DataSet getSalesForce_ReportingTo_MGR(string div_code, string reporting_to, int sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT 'admin' as Sf_Code, 'admin' as sf_name " +
                     " UNION " +
                     " SELECT sf_code, Sf_Name " +
                     " FROM mas_salesforce " +
                     " WHERE sf_type = '" + sf_type + "' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                //" AND sf_tp_active_flag = 0 " +
                     " and sf_TP_Active_Flag !=1 and SF_Status !=2" +
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

        public DataSet getSalesForce_Active_ReportingTo(string div_code, string reporting_to, int sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT sf_code, Sf_Name FROM mas_salesforce " +
                     " WHERE lower(sf_code) = 'admin' " +
                     " UNION " +
                     " SELECT sf_code, Sf_Name " +
                     " FROM mas_salesforce " +
                     " WHERE sf_type != '" + sf_type + "' " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%') and sf_TP_Active_Flag !=1 and SF_Status !=2" +
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

        public DataSet getSalesForce(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT Sf_Name,Sf_UserName,Sf_Password,convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date, " +
                     " Reporting_To_SF,TP_Reporting_SF,State_Code,convert(varchar,sf_Tp_Active_Dt,103) sf_Tp_Active_Dt, " +
                     " SF_ContactAdd_One,SF_ContactAdd_Two,SF_City_Pincode,SF_Email,SF_Mobile, " +
                     " case when SF_DOB='1900-01-01' then '' else convert(varchar,sf_Dob,103) end as SF_DOB, " +
                     " SF_Per_ContactAdd_One, SF_Per_ContactAdd_Two,SF_Per_City_Pincode,SF_Per_Contact_No,SF_Cat_Code,Sf_HQ, " +
                     " Division_Code,Sf_Code,sf_Tp_Active_flag, " +
                     " (select isnull(convert(varchar,Max(Entry_Date),101),'') from Trans_DCR_Entry_HEad where sf_Code='" + sf_code + "') " +
                     "  as DCr_Last_dt,sf_Tp_Deactive_Dt,sf_emp_id,sf_short_name,Designation_Code,sf_type,subdivision_code,SF_VacantBlock,sf_BlkReason," +
                      " case when SF_DOW='1900-01-01' then '' else convert(varchar,sf_Dow,103) end as SF_DOW,UsrDfd_UserName,fftype,Territory_Code " +
                     " FROM mas_salesforce " +
                     " WHERE Sf_Code= '" + sf_code + "' ";

            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dsDivision;
        }


        public int RecordUpdate(string sf_code, string sf_name, int state, string hq, string sf_design, string desig_short_name)
        {
            int iReturn = -1;
            //if (!RecordExist(div_code, div_sname))
            //{
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Salesforce " +
                         " SET sf_name = '" + sf_name + "', " +
                    //" sf_username = '" + usr_name + "', " +
                         " state_code = " + state + ", " +
                         " sf_hq = '" + hq + "'," +
                         " Designation_Code = '" + sf_design + "'," +
                          " sf_Designation_Short_Name = '" + desig_short_name + "'," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Salesforce_AM " +
                        " SET Sf_Name = '" + sf_name + "' , " +
                        "Sf_HQ = '" + hq + "'  where Sf_Code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //}
            //else
            //{
            //    iReturn = -3;
            //}
            return iReturn;

        }

        public int DeActivate(string sf_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC Sp_DeactSf '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Block(string sf_code, string blkreason, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set sf_status = 1 ," +
                        "  sf_blkreason = '" + blkreason + "'," +
                           "LastUpdt_Date = getdate() " +
                        " WHERE Sf_Code= '" + sf_code + "' and Division_Code='" + div_code + ",' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = " INSERT INTO Mas_SF_Block (sf_code,sf_Block_Reason, sf_Block_Flag, sf_Block_Start_Date, sf_Block_End_Date,   " +
                              " Division_Code) " +
                              " VALUES ('" + sf_code + "','" + blkreason + "' , '0', " +
                              " getdate(), getdate(), '" + div_code + "' ) ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int Activate(string sf_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set sf_status = 0," +
                        " LastUpdt_Date = getdate() ," +
                        " sf_blkreason='' " +
                        " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_SF_Block set sf_Block_Flag = 1," +
                       " sf_Block_End_Date = getdate() " +
                    //" sf_blkreason='' " +
                       " WHERE Sf_Code= '" + sf_code + "' and sf_Block_Flag=0 ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int VacActivate(string sf_code)
        {
            int iReturn = -1;
            DataSet dsVacant;

            string empl_id = string.Empty;
            int sl_num = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set sf_Tp_Active_flag = 0," +
                        " LastUpdt_Date = getdate() " +
                        " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                // Populate the new table "salesforce_vacant" before making the fieldforce as VACANT by Sridevi on 04/21/2015

                //strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM salesforce_vacant ";
                strQry = "SELECT max(Cast(SubString(Employee_Id,2, LEN(Employee_Id)) as int))+1 FROM salesforce_vacant ";
                sl_num = db.Exec_Scalar(strQry);

                empl_id = "E" + sl_num.ToString();

                strQry = " SELECT Sf_Code, Sf_Name, Designation_Code , Reporting_To_SF, Division_Code, " +
                            " (SELECT Sf_Name FROM Mas_Salesforce WHERE Sf_Code = a.Reporting_To_SF) Reporting_To_SF_Name, Created_Date, LastUpdt_Date  " +
                            " FROM Mas_Salesforce a " +
                            " WHERE Sf_Code = '" + sf_code + "' ";

                dsVacant = db.Exec_DataSet(strQry);

                if (dsVacant.Tables[0].Rows.Count > 0)
                {
                    //Populate salesforce_vacant
                    strQry = " INSERT INTO salesforce_vacant (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Division_Code ) " +
                                " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                " getdate(), getdate(), '" + dsVacant.Tables[0].Rows[0][4].ToString() + "' ) ";

                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }



        public DataSet GetUserName(string div_code, string state_code, string designation_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            //strQry = "declare @DivName varchar(5)" +
            //        "declare @ShName varchar(5)" +
            //        "declare @DnName varchar(5)" +
            //        "set @DivName=(select upper(Division_SName) Division_SName from  Mas_Division WHERE Division_Code=" + div_code + ")" + " " +
            //        "set @ShName=(select ShortName from Mas_State where State_Code=" + state_code + ")" + " " +
            //        "set @DnName=(select isnull(Designation_Short_Name,'') from Mas_SF_Designation where Designation_Code=" + designation_code + ")" + " " +
            //        "select @DivName as Division_SName" + " " +
            //        "select @ShName as ShortName" + " " +
            //        "select @DnName as Designation_Short_Name" + " " +
            //        "select @DivName+@ShName+@DnName CompletedName" + " " +
            //        "declare @AutoNum varchar(10)" +
            //        "declare @Incr int " +
            //        "declare @Count int " +
            //        "select @Count=(select count(Sf_UserName) from mas_salesforce where Sf_UserName like '%' +@DivName+@ShName+@DnName+'%')" + " " +
            //        "set @Incr=@Count" + " " +
            //        "set @AutoNum=CAST(@Incr+RIGHT(CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS VARCHAR(3)),0) AS VARCHAR(30))+1" + " " +
            //        "select @AutoNum Number";
            strQry = "declare @DivName varchar(5)" +
                              "declare @ShName varchar(5)" +
                              "declare @DnName varchar(5)" +
                              "set @DivName=(select upper(Division_SName) Division_SName from  Mas_Division WHERE Division_Code=" + div_code + ")" + " " +
                              "set @ShName=(select ShortName from Mas_State where State_Code=" + state_code + ")" + " " +
                              "set @DnName=(select isnull(Designation_Short_Name,'') from Mas_SF_Designation where Designation_Code=" + designation_code + ")" + " " +
                              "select @DivName as Division_SName" + " " +
                              "select @ShName as ShortName" + " " +
                              "select @DnName as Designation_Short_Name" + " " +
                              "select @DivName+@ShName+@DnName CompletedName" + " " +
                              "declare @AutoNum varchar(10)" +
                              "declare @Incr int " +
                              "declare @Count int " +
                              "select @Count=(select isnull(max(convert(int,SubString(Sf_UserName,PATINDEX('%[0-9]%',Sf_UserName),Len(Sf_UserName)))),0) " +
                              "from mas_salesforce where Sf_UserName like '%' +@DivName+@ShName+@DnName+'%')" + " " +
                              "set @Incr=@Count" + " " +
                              "set @AutoNum=CAST(@Incr+RIGHT(CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS VARCHAR(3)),0) AS VARCHAR(30))+1" + " " +
                              "select @AutoNum Number";


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

        //Added by Sri - to check duplicte user name
        public bool CheckDupUserName(string User_Name, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UsrDfd_UserName) FROM Mas_Salesforce WHERE UsrDfd_UserName='" + User_Name + "'  AND sf_code !='" + sf_code + "'";
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

        //Added by Sri - to check duplicte user name
        public bool CheckDupUserName(string User_Name)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(UsrDfd_UserName) FROM Mas_Salesforce WHERE UsrDfd_UserName='" + User_Name + "'";
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
        public string RecordAdd(string sf_name, string user_name, string password, DateTime joining_date, string reporting_to, string state_code, DateTime tp_dcr_start_date, string contact_address1, string contact_address2, string contact_citypin, string email, string mobile, string dob, string dow, string permanent_address1, string permanent_address2, string permanent_citypin, string permanent_contact_no, string sf_hq, string div_code, int sf_type, string emp_id, string short_name, string desgn, string sub_division, string des_sname, string UsrDfd_UserName, string ddl_fftype, string ddlTerritory, string Terr_code)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;
            string sf_code = string.Empty;
            string strjoinDate = joining_date.Month + "-" + joining_date.Day + "-" + joining_date.Year;
            string strtp_dcr_start_date = tp_dcr_start_date.Month + "-" + tp_dcr_start_date.Day + "-" + tp_dcr_start_date.Year;
            string sEmp_ID = string.Empty;

            if (!CheckDupUserName(UsrDfd_UserName))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();


                    int sf_sl_no = -1;
                    DateTime deactDt = DateTime.Now.AddDays(-1);

                    strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM mas_salesforce WHERE Sf_Code !='admin'";
                    sf_sl_no = db.Exec_Scalar(strQry);

                    string sftype = string.Empty;
                    if (sf_type == 1)
                    {
                        sftype = "MR";
                        strQry = "SELECT  isnull(max(Cast(SubString(Sf_Code,3, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                    }
                    else
                    {
                        sftype = "MGR";
                        strQry = "SELECT  isnull(max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int)),0)+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                    }

                    //strQry = "SELECT COUNT(Sf_Code)+1 FROM mas_salesforce where  Sf_Code like '" + sftype + "%' ";
                    //   strQry = "SELECT  max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int))+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                    int iCnt = db.Exec_Scalar(strQry);
                    if (iCnt.ToString().Length == 1)
                    {
                        sf_code = "000" + iCnt.ToString();
                    }
                    else if (iCnt.ToString().Length == 2)
                    {
                        sf_code = "00" + iCnt.ToString();
                    }
                    else if (iCnt.ToString().Length == 3)
                    {
                        sf_code = "0" + iCnt.ToString();
                    }
                    else
                    {
                        sf_code = iCnt.ToString();
                    }
                    sf_code = sftype + sf_code;

                    sEmp_ID = "E" + sf_sl_no.ToString();
                    // Changed on 6th june 15 to include last dcr date - sridevi
                    strQry = " INSERT INTO Mas_Salesforce(Sf_Code,Sf_Name,Sf_UserName,Sf_Password,Sf_Joining_Date, " +
                           "Reporting_To_SF,TP_Reporting_SF,State_Code,sf_Tp_Active_Dt,Sf_TP_DCR_Active_Dt,SF_ContactAdd_One, " +
                           " SF_ContactAdd_Two,SF_City_Pincode,SF_Email,SF_Mobile,SF_DOB,SF_DOW,SF_Per_ContactAdd_One,SF_Per_ContactAdd_Two, " +
                           " SF_Per_City_Pincode,SF_Per_Contact_No,SF_Cat_Code,SF_Status,Sf_HQ,Division_Code,Created_Date,sf_Tp_Active_flag, " +
                           " sf_Tp_Deactive_Dt,sf_Sl_No,sf_type,sf_emp_id,sf_short_name,Designation_Code,LastUpdt_Date,subdivision_code, Employee_Id,Last_DCR_Date,sf_Designation_Short_Name,UsrDfd_UserName,Last_TP_Date,fftype,Territory,Territory_Code) " +
                           " VALUES ( '" + sf_code + "' , '" + sf_name + "', '" + user_name + "', '" + password + "', '" + strjoinDate + "', " +
                           "  '" + reporting_to + "' , '" + reporting_to + "' , '" + state_code + "', '" + strtp_dcr_start_date + "', '" + strtp_dcr_start_date + "' , " +
                           " '" + contact_address1 + "', '" + contact_address2 + "', '" + contact_citypin + "', '" + email + "', '" + mobile + "', " +
                           " '" + dob + "','" + dow + "' , '" + permanent_address1 + "', '" + permanent_address2 + "' , '" + permanent_citypin + "', " +
                           " '" + permanent_contact_no + "', '0' , '0', " +
                           " '" + sf_hq + "' , '" + div_code + "' , getdate(), '0', '" + deactDt.Month + "-" + deactDt.Day + "-" + deactDt.Year + "', '" + sf_sl_no + "', " + sf_type + ", '" + emp_id + "', '" + short_name + "', '" + desgn + "',getdate(),'" + sub_division + "' , '" + sEmp_ID + "' ,'" + strtp_dcr_start_date + "', '" + des_sname + "','" + UsrDfd_UserName + "','" + strjoinDate + "','" + ddl_fftype + "','" + ddlTerritory + "','" + Terr_code + "')";

                    iReturn = db.ExecQry(strQry);
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
                if (iReturn > 0)
                {
                    strSfCode = sf_code; //Pass Sf Code for Salesforce_ApprovalManager table data creation
                    if (sf_type == 1)
                    {
                        AdminSetup adm = new AdminSetup();
                        if (div_code.Contains(","))
                        {
                            div_code = div_code.Remove(div_code.Length - 1);
                        }
                        iReturn = adm.Add_Admin_FieldForce_Setup(strSfCode, div_code, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);
                    }
                }
                return strSfCode;
            }
            else
            {
                return "Dup";
            }

        }
        public int RecordAddApprovalMgr(string sfcode, string sf_name, string sf_hq, string DCR_AM, string TP_AM, string Leave_AM, string LstDr_AM, string SS_AM, string Expense_AM, string Otr_AM, string reporting_to, string Div_Code)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "INSERT INTO Mas_Salesforce_AM(Sf_Code,Sf_Name,Sf_HQ,DCR_AM,TP_AM,LstDr_AM,Leave_AM,SS_AM,Expense_AM,Otr_AM,Reporting_To,Division_Code) " +
                         " VALUES ( '" + sfcode + "' ,'" + sf_name + "','" + sf_hq + "', '" + DCR_AM + "', '" + TP_AM + "', '" + LstDr_AM + "', '" + Leave_AM + "', " +
                         " '" + SS_AM + "' ,'" + Expense_AM + "', '" + Otr_AM + "' , '" + reporting_to + "' ,'" + Div_Code + "') ";



                iReturn = db.ExecQry(strQry);
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
        //Modified by Sri - 29Aug15
        public int RecordUpdate(string sf_code, string sf_name, string user_name, string password, DateTime joining_date, string reporting_to, string state_code, DateTime tp_dcr_start_date, string contact_address1, string contact_address2, string contact_citypin, string email, string mobile, string dob, string dow, string permanent_address1, string permanent_address2, string permanent_citypin, string permanent_contact_no, string sf_hq, string div_code, int sf_type, string emp_id, string short_name, string desgn, string sub_division, string UsrDfd_UserName, string ddl_fftype, string sf_Designation_Short_Name)
        {
            int iReturn = -1;
            string strSfCode = string.Empty;

            string strjoinDate = joining_date.Month + "-" + joining_date.Day + "-" + joining_date.Year;
            string strtp_dcr_start_date = tp_dcr_start_date.Month + "-" + tp_dcr_start_date.Day + "-" + tp_dcr_start_date.Year;

            if (!CheckDupUserName(UsrDfd_UserName, sf_code))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                    iReturn_Backup = db.ExecQry(strQry);

                    strQry = "Update Mas_Salesforce set Sf_Name = '" + sf_name + "', Sf_UserName = '" + user_name + "' , " +
                             " Sf_Password = '" + password + "', Sf_Joining_Date = '" + strjoinDate + "' , " +
                             " Reporting_To_SF=  '" + reporting_to + "', TP_Reporting_SF= '" + reporting_to + "', " +
                             " State_Code= '" + state_code + "', sf_Tp_Active_Dt= '" + strtp_dcr_start_date + "', Sf_TP_DCR_Active_Dt= '" + strtp_dcr_start_date + "' , " +
                             " SF_ContactAdd_One = '" + contact_address1 + "', SF_ContactAdd_Two= '" + contact_address2 + "', " +
                             " SF_City_Pincode= '" + contact_citypin + "',  SF_Email= '" + email + "', SF_Mobile= '" + mobile + "', " +
                             " SF_DOB = '" + dob + "',SF_DOW = '" + dow + "', SF_Per_ContactAdd_One='" + permanent_address1 + "', " +
                             " SF_Per_ContactAdd_Two = '" + permanent_address2 + "',SF_Per_City_Pincode= '" + permanent_citypin + "', " +
                             " SF_Per_Contact_No= '" + permanent_contact_no + "', Sf_HQ='" + sf_hq + "'," +
                             " sf_emp_id = '" + emp_id + "', sf_short_name = '" + short_name + "', Designation_Code = '" + desgn + "', " +
                             " LastUpdt_Date = getdate(), subdivision_code = '" + sub_division + "' ," +
                             " UsrDfd_UserName = '" + UsrDfd_UserName + "',fftype='" + ddl_fftype + "', sf_Designation_Short_Name = '" + sf_Designation_Short_Name + "' " +
                             " WHERE Sf_Code= '" + sf_code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                    iReturn_Backup = db.ExecQry(strQry);

                    strQry = "Update Mas_Salesforce_AM set Sf_Name = '" + sf_name + "' , Sf_HQ ='" + sf_hq + "' ,Reporting_To ='" + reporting_to + "' " +
                           " where Sf_Code = '" + sf_code + "' ";

                    iReturn = db.ExecQry(strQry);
                    //strQry = "update Mas_Stockist set Territory_Code='" + terr_code + "',Territory='" + ddlTerritory + "' where Field_Code='" + sf_code + "'";

                    //iReturn = db.ExecQry(strQry);


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
            else
            {
                return -99;
            }
        }
        public int RecordUpdate(string sf_code)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set sf_Tp_Active_flag = 1 ," +
                         " LastUpdt_Date = getdate() " +
                         " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int RecordUpdate(string sf_code, string reporting)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce " +
                         " set Reporting_To_SF = '" + reporting + "', TP_Reporting_SF = '" + reporting + "' ," +
                          " LastUpdt_Date = getdate() " +
                         " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                RecordUpdate_App(sf_code, reporting, reporting, reporting, reporting, reporting, reporting, reporting);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public int BulkUpdateDCR(string sf_code, DateTime dcr)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                // Modified on 6th june 15 to include last dcr date.
                strQry = "Update Mas_Salesforce " +
                         " set Sf_TP_DCR_Active_Dt =  '" + dcr.Month.ToString() + "-" + dcr.Day + "-" + dcr.Year + "' , " +
                         "  Last_Dcr_Date =  '" + dcr.Month.ToString() + "-" + dcr.Day + "-" + dcr.Year + "' , " +
                          " LastUpdt_Date = getdate() " +
                         " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int BulkUpdateTP(string sf_code, DateTime tp)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce " +
                         " set Sf_TP_Active_Dt = '" + tp.Month.ToString() + "-" + tp.Day + "-" + tp.Year + "' , " +
                         "  Last_TP_Date =  '" + tp.Month.ToString() + "-" + tp.Day + "-" + tp.Year + "' , " +
                          " LastUpdt_Date = getdate() " +
                         " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        // Approval Changes Update
        public int Update_App(string sf_code, string sMgr, string sMode)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_Salesforce_AM ";
                if (sMode == "DCR")
                {
                    strQry = strQry + " set DCR_AM = '" + sMgr + "' ";
                }
                else if (sMode == "TP")
                {
                    strQry = strQry + " set TP_AM = '" + sMgr + "' ";
                }
                else if (sMode == "LstDr")
                {
                    strQry = strQry + " set LstDr_AM = '" + sMgr + "' ";
                }
                else if (sMode == "Leave")
                {
                    strQry = strQry + " set Leave_AM = '" + sMgr + "' ";
                }
                else if (sMode == "SS")
                {
                    strQry = strQry + " set SS_AM = '" + sMgr + "' ";
                }
                else if (sMode == "Expense")
                {
                    strQry = strQry + " set Expense_AM = '" + sMgr + "' ";
                }
                else if (sMode == "Otr")
                {
                    strQry = strQry + " set Otr_AM = '" + sMgr + "' ";
                }

                strQry = strQry + " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public DataSet getUserList_Reporting_All(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '0' as sf_code, '---ALL---' as Sf_Name " +
                     " UNION " +
                     " select Sf_Code,Sf_Name  from mas_salesforce " +
                     " where sf_TP_Active_Flag in (0,2)  and Sf_Code in " +
                     " (select TP_Reporting_SF from Mas_Salesforce where sf_code != 'admin' and (Division_Code like '" + div_code + ',' + "%' or " +
                     " Division_Code like '%" + ',' + div_code + ',' + "%')  ) " +
                     " order by 2";

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

        //16Jul - Sridevi - To check the type of SF(MR or MGR)
        public DataSet CheckSFType(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sf_type from Mas_Salesforce where sf_code = '" + sf_code + "'";


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

        //16Jul - Sridevi - To get the list of MRs under the Manager
        public DataSet UserList_getMR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

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
        //Sridevi- Procedures for Report
        public DataSet UserList_Hierarchy(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Hierarchy '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet UserList_Alpha(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alpha '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet UserList_getMR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

            strQry = "SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
                        " FROM mas_salesforce a " +
                        " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                        " and a.sf_code !='admin' and a.sf_type=1 " +
                        " order by 2 ";
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

        public DataSet UserList_Alphasearch(string divcode, string sf_code, string alpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alphasearch '" + divcode + "', '" + sf_code + "', '" + alpha + "'";

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

        public DataSet UserList_getMR_Alpha(string divcode, string Alpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";
            strQry = "SELECT a.sf_Code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
                        " FROM mas_salesforce a " +
                        " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') and a.sf_code !='admin' and a.sf_type=1 and LEFT(a.sf_Name,1) = '" + Alpha + "' " +
                        " order by 2 ";

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


        public DataSet UserListTP_Hierarchy(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_TP '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet UserListTP_Alpha(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alpha_TP '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet UserListTP_Alphasearch(string divcode, string sf_code, string alpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alphasearch_TP '" + divcode + "', '" + sf_code + "', '" + alpha + "'";

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

        public DataSet UserList_HQ(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_HQ '" + divcode + "', '" + sf_code + "' ";

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
        //Created the below function for MR Status Report by Devi on 07/14/14.
        // This will call the new proc to fetch the required data.
        public DataSet get_MR_Status_Report(string div_code, int state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_mr_status_report  '" + div_code + "', " + state_code;

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

        public DataSet get_Rep_Status_Report(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_rep_status_report  '" + div_code + "', '" + sf_code + "' ";

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



        public DataSet getTerritory_Rep(string sf_code, int type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select Territory_Code, Territory_Name, Territory_SName " +
                       " from Mas_Territory_Creation " +
                       " where sf_code = '" + sf_code + "' and Territory_Active_Flag = " + type +
                       " order by Territory_Name ";

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

        public DataSet getDoctor_Rep(string sf_code, int type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode,a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //             " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e" +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           " and a.sf_code = '" + sf_code + "' and a.ListedDr_Active_Flag = " + type +
            //           " order by ListedDr_Name ";
            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode,a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code, " +
                       " case isnull(a.ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(a.ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e" +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       " and a.sf_code = '" + sf_code + "' and a.ListedDr_Active_Flag = " + type +
                       " order by ListedDr_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getNonDoctor_Rep(string sf_code, int type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            //strQry = " select a.UnListedDrCode, a.UnListedDr_Name, a.UnListedDr_Mobile, a.UnListedDr_Email, " +
            //           " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, f.Territory_Name, a.UnListedDr_DOB,a.UnListedDr_DOW " +
            //           " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Territory_Code = f.Territory_Code and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           " and a.sf_code = '" + sf_code + "' and a.UnListedDr_Active_Flag = " + type +
            //           " order by a.UnListedDr_Name ";
            strQry = " select UnListedDrCode, UnListedDr_Name, UnListedDr_Mobile,UnListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, f.Territory_Name, " +
                       " case isnull(a.UnListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  a.UnListedDr_DOB end UnListedDr_DOB, " +
                       " case  isnull(a.UnListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else a.UnListedDr_DOW end UnListedDr_DOW " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = f.Territory_Code and a.Doc_ClsCode=b.Doc_ClsCode " +
                       " and a.sf_code = '" + sf_code + "' and a.UnListedDr_Active_Flag = " + type +
                       " order by UnListedDr_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getChemists_Rep(string sf_code, int type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            strQry = " select chemists_code,Chemists_Name,Chemists_Contact, " +
                       " Chemists_Phone,b.Territory_Name " +
                       " from Mas_Chemists a, Mas_Territory_Creation b " +
                       " where a.Territory_Code=b.Territory_Code and  a.sf_code = '" + sf_code + "' and a.Chemists_Active_Flag  = " + type +
                       " order by Chemists_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getStockiest_Rep(string sf_code, int type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            strQry = " select Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile " +
                       " from Mas_Stockist " +
                       " where Stockist_Active_Flag  = " + type +
                       " order by Stockist_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet UserList_TP_Status(string divcode, int state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC [sp_UserList_TP_Status] '" + divcode + "', 'admin', " + state_code;

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

        public DataSet UserList_TP_Status_All(string divcode, int state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC [sp_UserList_TP_Status_All] '" + divcode + "', 'admin', " + state_code;

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
        //New Designation 
        public DataSet getDesignation()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,''Designation_Short_Name,''Designation_Name,'---Select---'Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
                     " FROM Mas_SF_Designation  " +
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
        public DataSet getsfDesignation(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT 0 as Designation_Code,'---Select---' as Designation_Short_Name,'' as Designation_Name " +
                           " UNION " +
                       " SELECT Designation_Code,Designation_Short_Name,Designation_Name " +
                       " FROM Mas_SF_Designation where Division_Code ='" + div_code + "' " +
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
        public DataSet getSalesForce_des(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code" +
            //         " FROM  mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
            //         " where lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
            //         " AND a.sf_Tp_Active_flag = 0  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' " +
            //         " or a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //         " ORDER BY 2";

            strQry = " SELECT SF_Code, Sf_Name, UsrDfd_UserName,Sf_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     " Sf_Password, Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code,c.Designation_Short_Name" +
                     " FROM  mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " where lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
                     " AND a.sf_Tp_Active_flag = 0  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' " +
                     " or a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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
        //end
        public DataTable getSalesForcelist_Sort(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                      "sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " ORDER BY Sf_Name";


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
        public DataTable getSalesForcelist_Sort(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsSF = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                     " WHERE a.SF_Status=0 " +
                     " AND lower(a.sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " AND LEFT(SF_Code,1) = '" + sAlpha + "' " +
                     " ORDER BY 2";


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

        public DataSet getSalesForce_st(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,b.StateName,a.State_Code as state_code,c.Designation_Short_Name as Designation_Name" +
            //" FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
            //" join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0 " +
            //" AND lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
            //" AND a.sf_Tp_Active_flag = 0  AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
            //" a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //" ORDER BY 2";

            strQry = " SELECT SF_Code, Sf_Name,Sf_UserName, UsrDfd_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     " Sf_Password,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date, Sf_HQ,sf_type as Type , case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,b.StateName,a.State_Code as state_code,c.Designation_Short_Name as Designation_Name,c.Designation_Short_Name" +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
                     " AND a.sf_Tp_Active_flag = 0  AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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

        public DataSet getSFStateType(string state_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select '' Sf_Code,'---Select---' sf_name" + " " +
                      "union" + " " +
                      "SELECT 'admin' as Sf_Code, 'admin' as sf_name " +
                      " UNION " +
                      "select Sf_Code,sf_name from Mas_Salesforce where sf_type=2 and State_Code='" + state_code + "' and (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                      "union all" + " " +
                      "select Sf_Code,sf_name from Mas_Salesforce where sf_type=2 and State_Code<>'" + state_code + "' and (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') ";


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

        //Changes done by Saravanan
        public DataTable getDTSalesForcelist(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     " Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
                     " ORDER BY Sf_Name";



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

        public DataTable FindDTSalesForcelist(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;
            strQry = " SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "a.Sf_HQ,a.sf_type as Type, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type, " +
                      " b.StateName,c.Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                      " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     sFindQry +
                     " ORDER BY 2";
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
        public DataTable getDTSalesForce_st(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDivision = null;
            //strQry = " SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type, " +
            //         " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
            //         " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
            //         "  WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 " +
            //         " AND a.sf_Tp_Active_flag = 0  AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%' or " +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //          " ORDER BY 2";

            strQry = " SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, a.Sf_HQ, " +
                    " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                    " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                    "a.sf_type as Type, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type, " +
                    " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                    " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                    "  WHERE SF_Status=0 " +
                    " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 " +
                    " AND a.sf_Tp_Active_flag = 0  AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " ORDER BY 2";


            try
            {
                dtDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDivision;
        }

        public DataTable getDTSalesForcelist_Reporting(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDivision = null;
            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
            //         " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
            //         " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
            //         " WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " AND a.TP_Reporting_SF = '" + sf_code + "' " +
            //         " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
            //         " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
            //         " ORDER BY Sf_Name";

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, " +
                    " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                    " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                    " sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                    " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                    " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                    " WHERE SF_Status=0 " +
                    " AND lower(sf_code) != 'admin' " +
                    " AND a.TP_Reporting_SF = '" + sf_code + "' " +
                    " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                    " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                    " ORDER BY Sf_Name";


            try
            {
                dtDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDivision;
        }

        //

        //change done by saravanan on 120914
        public int vbRecordUpdate(string sf_code, string strVacantBlock, string strDCRDate, string strTPDCRStartDate, string Effective_Date)
        {
            int iReturn = -1;
            int count = 0;
            DateTime dtDCRDate = Convert.ToDateTime(strDCRDate.ToString());
            DateTime dtTPDCRStartDate = Convert.ToDateTime(strTPDCRStartDate.ToString());
            DataSet dsVacant;
            DataSet dsMonth;
            DataSet dsdcr;
            string empl_id = string.Empty;
            int sl_num = -1;
            int iretdcr = -1;
            DateTime sfdate;
            DateTime Effe_Date = Convert.ToDateTime(Effective_Date.ToString());

            strDCRDate = dtDCRDate.Month + "-" + dtDCRDate.Day + "-" + dtDCRDate.Year;
            strTPDCRStartDate = dtTPDCRStartDate.Month + "-" + dtTPDCRStartDate.Day + "-" + dtTPDCRStartDate.Year;
            Effective_Date = Effe_Date.Month + "-" + Effe_Date.Day + "-" + Effe_Date.Year;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set sf_Tp_Active_flag = 1 ,SF_VacantBlock='" + strVacantBlock + "',Last_DCR_Date='" + strDCRDate + "'," +
                       "sf_Tp_Active_Dt='" + strTPDCRStartDate + "', LastUpdt_Date = getdate() " +
                       " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                if (iReturn > 0)
                {

                    strQry = "select distinct Tour_Month from trans_tp where convert(char(10),Tour_Date,105)='" + dtTPDCRStartDate.ToString().Substring(0, 10) + "'" +
                             "and SF_Code='" + sf_code + "'";

                    dsMonth = db.Exec_DataSet(strQry);
                    if (dsMonth.Tables[0].Rows.Count > 0)
                    {

                        strQry = "Insert into Trans_TP_one(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                                 "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                                 "Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name) " +
                                 "Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                                 "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,'0' Confirmed, " +
                                 "Confirmed_Date,Rejection_Reason,'1' Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name from Trans_TP " +
                                 "where SF_Code='" + sf_code + "' and MONTH(tour_date)='" + dsMonth.Tables[0].Rows[0][0].ToString() + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "Delete from trans_tp where SF_Code='" + sf_code + "' and MONTH(tour_date)>='" + dsMonth.Tables[0].Rows[0][0].ToString() + "' ";

                        iReturn = db.ExecQry(strQry);
                    }


                    // Populate the new table "salesforce_vacant" before making the fieldforce as VACANT by Sridevi on 04/21/2015

                    //strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM salesforce_vacant ";
                    strQry = "SELECT max(Cast(SubString(Employee_Id,2, LEN(Employee_Id)) as int))+1 FROM salesforce_vacant ";
                    sl_num = db.Exec_Scalar(strQry);

                    empl_id = "E" + sl_num.ToString();

                    strQry = " SELECT Sf_Code, Sf_Name, Designation_Code , Reporting_To_SF, Division_Code, " +
                                " (SELECT Sf_Name FROM Mas_Salesforce WHERE Sf_Code = a.Reporting_To_SF) Reporting_To_SF_Name, Created_Date, LastUpdt_Date  " +
                                " FROM Mas_Salesforce a " +
                                " WHERE Sf_Code = '" + sf_code + "' ";

                    dsVacant = db.Exec_DataSet(strQry);

                    if (dsVacant.Tables[0].Rows.Count > 0)
                    {
                        sfdate = Convert.ToDateTime(dsVacant.Tables[0].Rows[0][6].ToString());
                        strQry = " INSERT INTO salesforce_vacant (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                    " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Vacant_Reason,Division_Code ,Effective_Date) " +
                                    " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                    " '" + sfdate.ToString("MM/dd/yyyy") + "', getdate(),'" + strVacantBlock + "', '" + dsVacant.Tables[0].Rows[0][4].ToString() + "','" + Effective_Date + "' ) ";

                        iReturn = db.ExecQry(strQry);
                    }


                    if (strVacantBlock == "R")
                    {
                        strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM salesforce_vacant_resign ";
                        sl_num = db.Exec_Scalar(strQry);

                        empl_id = "E" + sl_num.ToString();
                        //Populate salesforce_vacant_resign                    
                        sfdate = Convert.ToDateTime(dsVacant.Tables[0].Rows[0][6].ToString());

                        strQry = " INSERT INTO salesforce_vacant_resign (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                    " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Division_Code,Effective_Date ) " +
                                    " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                    " '" + sfdate.ToString("MM/dd/yyyy") + "' , getdate(), '" + dsVacant.Tables[0].Rows[0][4].ToString() + "' ,'" + Effective_Date + "') ";

                        iReturn = db.ExecQry(strQry);
                    }
                    else if (strVacantBlock == "T")
                    {
                        strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM salesforce_vacant_transfer ";
                        sl_num = db.Exec_Scalar(strQry);

                        empl_id = "E" + sl_num.ToString();
                        //Populate salesforce_vacant_transfer
                        sfdate = Convert.ToDateTime(dsVacant.Tables[0].Rows[0][6].ToString());

                        strQry = " INSERT INTO salesforce_vacant_transfer (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                    " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Division_Code,Effective_Date ) " +
                                    " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                    " '" + sfdate.ToString("MM/dd/yyyy") + "', getdate(), '" + dsVacant.Tables[0].Rows[0][4].ToString() + "','" + Effective_Date + "' ) ";

                        iReturn = db.ExecQry(strQry);
                    }
                    else if (strVacantBlock == "P")
                    {
                        strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM SalesForce_Vacant_Promote ";
                        sl_num = db.Exec_Scalar(strQry);

                        empl_id = "E" + sl_num.ToString();
                        //Populate salesforce_vacant_transfer
                        sfdate = Convert.ToDateTime(dsVacant.Tables[0].Rows[0][6].ToString());

                        strQry = " INSERT INTO SalesForce_Vacant_Promote (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                    " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Division_Code,Effective_Date ) " +
                                    " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                    " '" + sfdate.ToString("MM/dd/yyyy") + "', getdate(), '" + dsVacant.Tables[0].Rows[0][4].ToString() + "' ,'" + Effective_Date + "') ";

                        iReturn = db.ExecQry(strQry);
                    }
                    else if (strVacantBlock == "D")
                    {
                        strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM SalesForce_Vacant_DePromote ";
                        sl_num = db.Exec_Scalar(strQry);

                        empl_id = "E" + sl_num.ToString();
                        //Populate salesforce_vacant_transfer
                        sfdate = Convert.ToDateTime(dsVacant.Tables[0].Rows[0][6].ToString());

                        strQry = " INSERT INTO SalesForce_Vacant_DePromote (Employee_Id, SF_Code, SF_Name, SF_Desgn,   " +
                                    " Reporting_To_SF_Code, Reporting_To_SF_Name, Start_Date, End_Date, Division_Code,Effective_Date ) " +
                                    " VALUES ('" + empl_id + "' , '" + dsVacant.Tables[0].Rows[0][0].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][1].ToString() + "', '" + dsVacant.Tables[0].Rows[0][2].ToString() + "', " +
                                    " '" + dsVacant.Tables[0].Rows[0][3].ToString() + "', '" + dsVacant.Tables[0].Rows[0][5].ToString() + "', " +
                                    " '" + sfdate.ToString("MM/dd/yyyy") + "', getdate(), '" + dsVacant.Tables[0].Rows[0][4].ToString() + "','" + Effective_Date + "' ) ";

                        iReturn = db.ExecQry(strQry);
                    }
                }

                // DCR Pending Requests - Auto Approve - Sridevi - 5June15

                DCR dcr = new DCR();
                dsdcr = dcr.get_dcr_pending_approval_TransSl(sf_code);
                if (dsdcr.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drdcr in dsdcr.Tables[0].Rows)
                    {
                        iretdcr = dcr.Create_DCRHead_Trans(sf_code, Convert.ToInt32(drdcr["Trans_Slno"])); // Auto Approve all pending requests.
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataTable getDTSalesForce_des(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtDivision = null;

            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code" +
            //    " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
            //    " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0" +
            //    " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 " +
            //    " AND a.sf_Tp_Active_flag = 0  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' or " +
            //    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //    " ORDER BY 2";

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, " +
                " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                "Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code" +
                " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0" +
                " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 " +
                " AND a.sf_Tp_Active_flag = 0  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                " ORDER BY 2";

            try
            {
                dtDivision = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDivision;
        }
        //

        //Changes done by Priya
        public DataSet getDesignation_BulkEdit(string SF_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Designation_Code FROM  Mas_Salesforce " +
                     " WHERE SF_Code='" + SF_Code + "' AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') ";


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
        //end

        public DataSet dsSF { get; set; }

        public DataSet getSF_Password(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Password  from mas_salesforce where Sf_Code = '" + sf_code + "' ";

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

        public int UpdatePassword(string sf_code, string pwd)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Salesforce " +
                         " SET Sf_Password = '" + pwd + "' " +
                         " WHERE sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }

        public DataSet getFieldForce_MailView(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select sf_code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name , sf_username, sf_HQ " +
                        " from Mas_Salesforce " +
                        " where Sf_Code !='admin' and (Division_Code like '" + div_code + ',' + "%'  or " +
                        " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                        " and sf_type = 1 " +
                        " order by sf_name ";



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
        public DataSet getFieldForce_Birth(string Sf_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //    strQry = " SELECT e.Sf_Code,e.Sf_Name,e.SF_DOB FROM Mas_Salesforce e WHERE MONTH(SF_DOB) = MONTH(GETDATE()) " +
            //" AND DAY(SF_DOB) >= DAY(DATEADD(WK, DATEDIFF(WK, 0, GETDATE()), -1)) " +
            //" AND DAY(SF_DOB) < DAY(DATEADD(WK, DATEDIFF(WK, 0, GETDATE()) + 1, -1)) and e.Sf_Code='" + Sf_Code + "' ";

            //strQry = " SELECT e.Sf_Code,e.Sf_Name,e.SF_DOB FROM Mas_Salesforce e WHERE MONTH(SF_DOB) = MONTH(GETDATE()) " +
            //" and e.Sf_Code='" + Sf_Code + "' ";

            strQry = " Select Sf_Code,Sf_Name, case convert(char(10),SF_DOB,103)" +
                       " when '01/01/1900' then '' " +
                       " else convert(char(10),SF_DOB,103) end SF_DOB from Mas_Salesforce " +
                     " where (month(SF_DOB)=month(getdate())) " +
                      " and (day(SF_DOB) between day(getdate())-3 and day(getdate())+3 ) and Sf_Code='" + Sf_Code + "' and (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%')  and isnull(SF_DOB,'')!=''";


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
        public DataSet getSaleforce_Image(string divcode, string Sf_Code, string ImageId)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDivision = null;
            strQry = " select a.Filename, a.FilePath, a.Subject, b.Sf_Code, b.Sf_Name, b.Sf_HQ, b.Sf_Code " +
                     " From mas_salesforce b, Mas_HomeImage_FieldForce a " +
                     " Where (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " and b.Sf_Code='" + Sf_Code + "' and ImageId='" + ImageId + "'";


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
        public DataSet Sales_Image(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForce_Image '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getSales(string divcode, string Sf_Code, string Image_Id)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            if (Sf_Code != "")
            {
                strQry = "SELECT a.FileName,a.FilePath,a.Subject, b.Sf_Code,b.Sf_Name, b.Sf_HQ  " +
                          " FROM Mas_HomeImage_FieldForce a " +
                          " inner join mas_salesforce b On a.Sf_Code=b.Sf_Code " +
                          " WHERE (b.Division_Code like '" + divcode + ',' + "%'  or " +
                          " b.Division_Code like '%" + ',' + divcode + ',' + "%')" +
                          " and a.Sf_Code='" + Sf_Code + "' or Image_Id= '" + Image_Id + "' ";


            }
            else
            {
                strQry = "SELECT  ''FileName,''FilePath,''Subject, b.Sf_Code,b.Sf_Name, b.Sf_HQ  " +
                         " FROM  mas_salesforce b  " +
                         " WHERE (b.Division_Code like '" + divcode + ',' + "%'  or " +
                         " b.Division_Code like '%" + ',' + divcode + ',' + "%')" +
                         " and b.Sf_Code='" + Sf_Code + "'";


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

        //Changes Done by Sridevi - Starts
        public DataSet UserList_getVacantMR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getVacantMR '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getReporting(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_getReportingTo '" + sf_code + "' ";

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

        public DataSet FillSF_ScreenAccess(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_screen_lock  '" + div_code + "', '" + sf_code + "' ";

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
        //public DataSet MissedCallReport(string sf_code, string div_code, DateTime dtcurrent)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    DataSet dsAdmin = null;

        //    strQry = "EXEC sp_MissedCallReport  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "' ";

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

        public DataSet MissedCallReport(string div_code, string sf_code, int cmonth, int cyear, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_LstDr_Cnt  '" + div_code + "', '" + sf_code + "'," + cmonth + "," + cyear + ", '" + dtcurrent + "' ";

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

        public DataSet DCR_MissedCallReport(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_MissedCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet SDP_LST_Report(string sf_code, string div_code, DateTime dtcurrent)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_SDP_MSD_Report  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "' ";

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

        public DataSet SDP_MSD_Report(string sf_code, string div_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_SDP_MSD_DCR_Report  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + " ";

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

        public DataSet Catg_LST_Report(string sf_code, string div_code, DateTime dtcurrent, int strDay, int strMonth, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_CatgMissedCallReport  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "','" + strDay + "','" + strMonth + "', " + catg_code + " ";

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

        public DataSet Catg_MSD_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_CatgMissedCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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

        public DataSet Spec_LST_Report(string sf_code, string div_code, DateTime dtcurrent, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_SpecMissedCallReport  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "','" + iMonth + "','" + iYear + "', " + catg_code + " ";

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

        public DataSet Spec_MSD_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_SpecMissedCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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

        public DataSet Class_LST_Report(string sf_code, string div_code, DateTime dtcurrent, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_ClassMissedCallReport  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "','" + iMonth + "','" + iYear + "', " + catg_code + " ";

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

        public DataSet Class_MSD_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_ClassMissedCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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


        public string getMonthName(string sMonth)
        {
            string sReturn = string.Empty;

            if (sMonth == "1")
            {
                sReturn = "January";
            }
            else if (sMonth == "2")
            {
                sReturn = "February";
            }
            else if (sMonth == "2")
            {
                sReturn = "February";
            }
            else if (sMonth == "3")
            {
                sReturn = "March";
            }
            else if (sMonth == "4")
            {
                sReturn = "April";
            }
            else if (sMonth == "5")
            {
                sReturn = "May";
            }
            else if (sMonth == "6")
            {
                sReturn = "June";
            }
            else if (sMonth == "7")
            {
                sReturn = "July";
            }
            else if (sMonth == "8")
            {
                sReturn = "August";
            }
            else if (sMonth == "9")
            {
                sReturn = "September";
            }
            else if (sMonth == "10")
            {
                sReturn = "October";
            }
            else if (sMonth == "11")
            {
                sReturn = "November";
            }
            else if (sMonth == "12")
            {
                sReturn = "December";
            }

            return sReturn;
        }

        //Changes Done by Sridevi - Ends

        //Changes Done by Saravana
        public DataSet getFieldForce_Distance(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select sf_code, sf_name+' - '+b.Designation_Short_Name+' - '+ sf_HQ  sf_name, sf_username, sf_HQ " +
                     " from Mas_Salesforce a,Mas_SF_Designation b " +
                     " where Sf_Code !='admin' and (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_type = 1 and sf_status=0 and a.Designation_Code=b.Designation_Code " +
                     " order by sf_name ";

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
        public DataSet getSalesForceVaclist_MR(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name, Sf_UserName, Sf_HQ,case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type, b.StateName,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " WHERE a.SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 and a.sf_Type=1 " +
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

        public DataSet getFieldForce_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select sf_code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name , sf_username, sf_HQ " +
                        " from Mas_Salesforce " +
                        " where Sf_Code !='admin' and (Division_Code like '" + div_code + ',' + "%' or " +
                        " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                        " and sf_type = 1 and sf_status=0 " +
                        " order by sf_name ";

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
        public DataSet getSalesForce_subdiv(string divcode, string subdivision_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Name,a.Designation_Code as Designation_Code,d.subdivision_name" +
                " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code join mas_subdivision d on a.subdivision_code=d.subdivision_code WHERE SF_Status=0" +
                " AND lower(sf_code) != 'admin' and a.SF_Status = 0 " +
                " AND a.sf_Tp_Active_flag = 0  AND a.subdivision_code = '" + subdivision_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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

        public DataSet get_HO_ID_Reporting()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = " select '0' ho_id,'Select' ho_name " +
                        " union all " +
                        " select CONVERT(varchar,ho_id) ho_id, name ho_name from mas_ho_id_creation " +
                         " WHERE HO_Active_flag = 0 ";

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

        //public int HO_ID_RecordAdd(string username, string password, string name, string menu, string div_code, string reporting_to, string reporting_to_name)
        //{
        //    int iReturn = -1;
        //    if (!RecordExistAdd(username))
        //    {
        //        try
        //        {
        //            DB_EReporting db = new DB_EReporting();
        //            strQry = "INSERT INTO mas_ho_id_creation VALUES ( '" + username + "' ,'" + password + "','" + name + "', '" + menu + "', '" + div_code + "', '" + reporting_to + "', '" + reporting_to_name + "',getdate(),getdate(),'0') ";
        //            iReturn = db.ExecQry(strQry);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }

        //    else
        //    {
        //        iReturn = -2;
        //    }
        //    return iReturn;
        //}
        public DataSet getHO()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT HO_ID,Name,User_Name,Password,Reporting_To_Name" +
                     " FROM mas_ho_id_creation " +
                     " WHERE HO_Active_flag = 0  and Name <> 'admin' " +
                     " ORDER BY 1";
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
        public int HO_DeActivate(int ho_id)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Update Mas_HO_ID_Creation set HO_Active_flag = 1," +
                        " LastUpdt_Date = getdate() " +
                        " WHERE HO_ID= '" + ho_id + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public DataSet FindSalesForcelist_Mgr(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code,a.Division_Code  " +
                     " FROM mas_salesforce a, mas_state b" +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " AND a.sf_type = 2 " +
                     sFindQry +
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
        public DataSet FillSalesForcelist_Mgr(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code,a.Division_Code  " +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                      " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     " AND a.sf_type = 2  AND a.sf_code not in(select reporting_to_sf from mas_salesforce where isnull(reporting_to_sf,'')!='' and (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%' ))" +
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
        public DataSet getSalesForce_st_mgr(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,b.StateName,a.State_Code as state_code,c.Designation_Name," +
                           " c.desig_color " +
                  " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                  " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0 " +
                  " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 and a.Designation_code = c.Designation_code " +
                 " AND a.sf_Tp_Active_flag = 0   AND a.sf_type = 2 AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                 " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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
        public DataSet getSalesForce_des_mgr(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Name,a.Designation_Code as Designation_Code" +
                " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code WHERE SF_Status=0" +
                " AND lower(sf_code) != 'admin' AND a.SF_Status = 0 " +
                " AND a.sf_Tp_Active_flag = 0  a.sf_type = 2  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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

        public DataSet getSfDivision(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT Division_code FROM mas_salesforce WHERE Sf_Code = '" + sf_code + "'";

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


        public DataSet getSfDivision_Main(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT Division_code FROM mas_salesforce_AM WHERE Sf_Code = '" + sf_code + "'";

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

        public int UpdateDivisionCode(string sf_code, string division_code, int ismultidivision)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "UPDATE Mas_Salesforce " +
                         " SET division_code = '" + division_code + "',IsMultiDivision = " + ismultidivision + " " +
                         " WHERE sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by Saravana
        public DataSet TPviewGetAlphapet(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_TPViewAlphapetGet '" + divcode + "', '" + sf_code + "' ";

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

        //Added by devi 
        public int UpdateRepMgr(string sf_code, string Mgr_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce set " +
                        "  Reporting_To_SF = '" + Mgr_Code + "'," +
                        "  TP_Reporting_SF = '" + Mgr_Code + "'," +
                           "LastUpdt_Date = getdate() " +
                        " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        public int CreateAuditMgr(string sf_code, string audit_team, string mgrAuditTeam, string division_code) // Modified by Sri - 28Aug15
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max([S No])+1,'1') [S No] from mas_sf_audit_team ";
                int SNo = db.Exec_Scalar(strQry);
                if (!RecordExistAudit(division_code, sf_code))
                {

                    strQry = "INSERT INTO mas_sf_audit_team VALUES ('" + SNo + "' ,'" + sf_code + "' ,'" + audit_team + "','" + division_code + "', getdate(),getdate(),'" + mgrAuditTeam + "') ";
                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = "Update mas_sf_audit_team set " +
                        "  Audit_team = '" + audit_team + "'," +
                           "LastUpdt_Date = getdate() " +
                        " WHERE Sf_Code= '" + sf_code + "' and Division_code like '" + division_code + "'";

                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public bool RecordExistAudit(string division_code, string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Division_code) FROM mas_sf_audit_team WHERE sf_code='" + sf_code + "' and division_code='" + division_code + "'";
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

        public DataSet UserList_Self(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Self '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet UserListTP_AlphasearchMgr(string divcode, string sf_code, string alpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alphasearch_TPMgr '" + divcode + "', '" + sf_code + "', '" + alpha + "'";

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
        public DataSet SalesForceListMgrGet(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForceMgrGet '" + divcode + "', '" + sf_code + "' ";

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
        //end
        public int Vac_Team_RecordAdd(string sf_Code, string sf_team, string division_code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT isnull(max(S_No)+1,'1') S_No from Mas_Salesforce_Vac_RepTeam ";
                int S_No = db.Exec_Scalar(strQry);
                strQry = "INSERT INTO Mas_Salesforce_Vac_RepTeam VALUES ( '" + S_No + "','" + sf_Code + "' ,'" + sf_team + "','" + division_code + "') ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int del_vac_team(string sf_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "delete from Mas_Salesforce_Vac_RepTeam where sf_Code = '" + sf_Code + "'";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        //saravana changes
        public DataSet TPviewGetAlphapetMgr(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_TPViewAlphapetMgrGet '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet UserListMailAddress(string divcode, string sf_code, string level)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_MailAddress '" + divcode + "', '" + sf_code + "', '" + level + "' ";

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
        //Changes done by Sridevi
        public DataSet UserList_AlphaAll(string divcode, string sf_code, string alpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Alphasearch_TP  '" + divcode + "', '" + sf_code + "', '" + alpha + "'";

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
        //Changes done by Priya

        public DataSet getVacantlist_MR(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "EXEC sp_get_VacantMr '" + sf_code + "' ";


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
        public DataSet SalesForce_State_Get(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForce_State '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet UserList_Self_Vacant(string divcode, string sf_code, string strVacant)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Self_vacant '" + divcode + "', '" + sf_code + "','" + strVacant + "' ";

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

        public DataSet getDesignation_MR(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,''Designation_Short_Name,''Designation_Name,'---Select---'Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
                     " FROM Mas_SF_Designation where type=1 and Division_Code = '" + div_code + "'  and Designation_Active_Flag=0 " +
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
        public DataSet getDesignation_Manager(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,''Designation_Short_Name,''Designation_Name,'---Select---'Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
                     " FROM Mas_SF_Designation where type=2 and Division_Code = '" + div_code + "'  and Designation_Active_Flag=0 " +
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
        //Change the Saravanan 230315

        public int ExpSalesforce_RecordAdd(string lblsfcode, string txtHq, string txtEXHQ, string txtOS, string txtHill, string txtFareKm, string txtRangeofFare1, string txtRangeofFare2, string txtEffective, string strParametrValue, string strColumn)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                int iCount = -1;
                DateTime dtEffective = Convert.ToDateTime(txtEffective);
                //strQry = "select count(SF_Code) from Mas_Allowance_Fixation where Sf_Code = '" + lblsfcode + "' ";
                //iCount = db.Exec_Scalar(strQry);

                strQry = "delete from Mas_Allowance_Fixation where SF_Code='" + lblsfcode + "' ";
                iCount = db.Exec_Scalar(strQry);

                //if (iCount == 0)
                //{
                //    strQry =" Insert Into Mas_Allowance_Fixation (SF_Code,HQ_Allowance,EX_HQ_Allowance,OS_Allowance,Hill_Allowance, " +
                //            " FareKm_Allowance,Range_of_Fare1,Range_of_Fare2,Effective_Form)values('" + lblsfcode + "','" + txtEXHQ + "','" + txtEXHQ + "','" + txtOS + "', " +
                //            " '" + txtHill + "','" + txtFareKm + "','" + txtRangeofFare1 + "','" + txtRangeofFare2 + "','"+dtEffective.Month+"-"+dtEffective.Day+"-"+dtEffective.Year +"') ";

                //}
                //else
                //{
                //    strQry = "update Mas_Allowance_Fixation set HQ_Allowance='" + txtHq + "',EX_HQ_Allowance='" + txtEXHQ + "',OS_Allowance='" + txtOS + "'," +
                //             "Hill_Allowance='" + txtHill + "',FareKm_Allowance='" + txtFareKm + "',Range_of_Fare1='" + txtRangeofFare1 + "',Range_of_Fare2='" + txtRangeofFare2 + "' , " +
                //             "Effective_Form='" + dtEffective.Month + "-" + dtEffective.Day + "-" + dtEffective.Year + "'" +
                //             "where Sf_Code='" + lblsfcode + "'";
                //}

                //strQry = " Insert Into Mas_Allowance_Fixation values('" + lblsfcode + "','" + txtEXHQ + "','" + txtEXHQ + "','" + txtOS + "', " +
                //           " '" + txtHill + "','" + txtFareKm + "','" + txtRangeofFare1 + "','" + txtRangeofFare2 + "','" + dtEffective.Month + "-" + dtEffective.Day + "-" + dtEffective.Year + "',"+ strParametrValue+") ";


                if (strColumn == "")
                {
                    strQry = " Insert Into Mas_Allowance_Fixation (SF_Code,HQ_Allowance,EX_HQ_Allowance,OS_Allowance,Hill_Allowance, " +
                            " FareKm_Allowance,Range_of_Fare1,Range_of_Fare2,Effective_Form)values('" + lblsfcode + "','" + txtHq + "','" + txtEXHQ + "','" + txtOS + "', " +
                            " '" + txtHill + "','" + txtFareKm + "','" + txtRangeofFare1 + "','" + txtRangeofFare2 + "','" + dtEffective.Month + "-" + dtEffective.Day + "-" + dtEffective.Year + "') ";
                }
                else
                {
                    strQry = " Insert Into Mas_Allowance_Fixation (SF_Code,HQ_Allowance,EX_HQ_Allowance,OS_Allowance,Hill_Allowance, " +
                                " FareKm_Allowance,Range_of_Fare1,Range_of_Fare2,Effective_Form," + strColumn + ")values('" + lblsfcode + "','" + txtHq + "','" + txtEXHQ + "','" + txtOS + "', " +
                                " '" + txtHill + "','" + txtFareKm + "','" + txtRangeofFare1 + "','" + txtRangeofFare2 + "','" + dtEffective.Month + "-" + dtEffective.Day + "-" + dtEffective.Year + "'," + strParametrValue + ") ";
                }

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet GetExp_AllwanceFixation(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForceExpMgr_Get_New '" + divcode + "', '" + sf_code + "' ";

            //strQry = "select * from Mas_Allowance_Fixation";

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
        //Changes done by Priya

        public DataSet Hq_Camp(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_Camp_HQ '" + divcode + "', '" + sf_code + "' ";

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

        //Changes done by Saravanan
        public DataSet UserList_get_SelfMail(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Self_Mail '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getSfName_HQ(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;

            strQry = " SELECT a.sf_code, a.Sf_Name, a.Sf_HQ,a.sf_type,b.Designation_Short_Name,UsrDfd_UserName" +
                     " FROM mas_salesforce a, Mas_SF_Designation b" +
                     " WHERE a.Sf_Code= '" + sfcode + "' " +
                     "and a.Designation_Code=b.Designation_Code " +
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

        public DataSet Type_LST_Report(string sf_code, string div_code, DateTime dtcurrent, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_TypeMissedCallReport  '" + div_code + "', '" + sf_code + "', '" + dtcurrent + "', " + catg_code + " ";

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

        public DataSet Type_MSD_Report(string sf_code, string div_code, int iMonth, int iYear, int catg_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_DCR_TypeMissedCallReport  '" + div_code + "', '" + sf_code + "', " + iMonth + ", " + iYear + ", " + catg_code + " ";

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

        public int PromoteSf(string sf_code, string sf_Name, string From_Des, string To_Des, string Reporting_To, string div_code, string Effective_Date)
        {
            int iReturn = -1;
            string empl_id = string.Empty;
            int sl_num;
            DateTime Effe_Date = Convert.ToDateTime(Effective_Date.ToString());

            Effective_Date = Effe_Date.Month + "-" + Effe_Date.Day + "-" + Effe_Date.Year;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM Salesforce_Promotion_Dtls ";
                sl_num = db.Exec_Scalar(strQry);

                empl_id = "E" + sl_num.ToString();

                strQry = " INSERT INTO Salesforce_Promotion_Dtls (Employee_Id, SF_Code, SF_Name, Promoted_From_Desg,   " +
                                    " Promoted_To_Desg, Reporting_To_Sf_Code, Effective_Date,Division_Code ) " +
                                    " VALUES ('" + empl_id + "' , '" + sf_code + "', " +
                                    " '" + sf_Name + "', " + From_Des + ", " +
                                    " " + To_Des + ", '" + Reporting_To + "', " +
                                    "  '" + Effective_Date + "','" + div_code + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }


        public int DePromoteSf(string sf_code, string sf_Name, string From_Des, string To_Des, string Reporting_To, string div_code, string Effective_Date)
        {
            int iReturn = -1;
            string empl_id = string.Empty;
            int sl_num;
            DateTime Effe_Date = Convert.ToDateTime(Effective_Date.ToString());

            Effective_Date = Effe_Date.Month + "-" + Effe_Date.Day + "-" + Effe_Date.Year;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT ISNULL(COUNT(Employee_Id),0)+1 FROM Salesforce_DePromotion_Dtls ";
                sl_num = db.Exec_Scalar(strQry);

                empl_id = "E" + sl_num.ToString();

                strQry = " INSERT INTO Salesforce_DePromotion_Dtls (Employee_Id, SF_Code, SF_Name, Promoted_From_Desg,   " +
                                    " Promoted_To_Desg, Reporting_To_Sf_Code, Effective_Date,Division_Code ) " +
                                    " VALUES ('" + empl_id + "' , '" + sf_code + "', " +
                                    " '" + sf_Name + "', " + From_Des + ", " +
                                    " " + To_Des + ", '" + Reporting_To + "', " +
                                    "  '" + Effective_Date + "','" + div_code + "' ) ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        //Changes done by Priya

        public DataSet getFieldForce_Transfer(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = " SELECT '' as sf_code, '---Select the FieldForce---' as sf_name, '' as sf_username,'' as sf_HQ  " +
                   " UNION " +
                     "select sf_code, sf_name+' - '+b.Designation_Short_Name+' - '+ sf_HQ  sf_name, sf_username, sf_HQ " +
                     " from Mas_Salesforce a,Mas_SF_Designation b " +
                     " where Sf_Code !='admin' and (a.Division_Code like '" + div_code + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + div_code + ',' + "%') " +
                     " and sf_type = 1 and a.Designation_Code=b.Designation_Code " +
                     " order by sf_name ";

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
        //Changes done by Saravanan
        public DataSet sp_UserList_NameHierarchy(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_UserList_NameHierarchy '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getMR_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_type = 1 " +
                     " ORDER BY 1";
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
        public DataSet getSfCode(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Code FROM  Mas_Salesforce " +
                     " WHERE UsrDfd_UserName= '" + sfcode + "' ";

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
        public DataSet getSfCodeNew(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT Sf_Code FROM  Mas_Salesforce " +
                     " WHERE UsrDfd_UserName= '" + sfcode + "' ";

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

        public DataSet GetSFCode_Upload(string sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            try
            {


                string sftype = string.Empty;
                string Sf_Code = string.Empty;
                if (sf_type == "1")
                {
                    sftype = "MR";
                    strQry = "SELECT  max(Cast(SubString(Sf_Code,3, LEN(Sf_Code)) as int))+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                }
                else
                {
                    sftype = "MGR";
                    strQry = "SELECT  max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int))+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                }

                //  strQry = "SELECT MAX(sf_code) FROM mas_salesforce where  Sf_Code like '" + sftype + "%' ";
                //  strQry = "SELECT  max(Cast(SubString(Sf_Code,4, LEN(Sf_Code)) as int))+1 FROM mas_salesforce where Sf_Code like '" + sftype + "%'  ";
                int iCnt = db_ER.Exec_Scalar(strQry);
                if (iCnt.ToString().Length == 1)
                {
                    Sf_Code = "000" + iCnt.ToString();
                }
                else if (iCnt.ToString().Length == 2)
                {
                    Sf_Code = "00" + iCnt.ToString();
                }
                else if (iCnt.ToString().Length == 3)
                {
                    Sf_Code = "0" + iCnt.ToString();
                }
                else
                {
                    Sf_Code = "0001";
                    Sf_Code = iCnt.ToString();
                }
                Sf_Code = sftype + Sf_Code;

                // dsSF = Sf_Code.ToString();

                dsSF = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsSF;

        }
        public DataSet getReporting_Name(string Sf_Name, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            if (Sf_Name == "Admin")
            {
                strQry = " SELECT sf_code FROM  Mas_Salesforce " +
                         " WHERE Sf_Name= '" + Sf_Name + "' ";
            }
            else
            {
                strQry = " SELECT sf_code FROM  Mas_Salesforce " +
                        " WHERE Sf_Name= '" + Sf_Name + "' and  Division_code = '" + div_code + ',' + "'  ";
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
        public DataSet getLastDCR(string sfcode)
        {

            //changed by sri - 15july
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            //strQry = " SELECT max (convert(varchar,Activity_Date,103)) as activity_date FROM " +
            //         " ( SELECT activity_date FROM DCRMain_Temp where sf_code='" + sfcode + "' UNION ALL " +
            //         " SELECT activity_date FROM DCRMain_Trans where sf_code='" + sfcode + "') activity ";

            strQry = " SELECT convert(varchar,Last_Dcr_Date,103) as activity_date FROM Mas_Salesforce " +
                      "where sf_code='" + sfcode + "'";

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
        //Changes done by Saravanan

        public DataSet SalesForceList_TP_StartingDt_Get(string divcode, string sf_code, string Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForce_TP_StartingDt_Get '" + divcode + "', '" + sf_code + "' ,'" + Month + "' ";

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

        //Changes done by saravanan
        public DataSet sp_UserMGRLogin(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC [sp_UserMGRLogin] '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet sp_UserMRLogin(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC [sp_UserMRLogin] '" + divcode + "', '" + sf_code + "' ";

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

        // added by sri - 21 July
        public DataSet getSalesForcelist_Alphabet_List(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0  " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 0 " +
                     " ORDER BY 1";
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

        // added by sri - 22 July
        public DataSet UserList_BulkEditStartDate(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_BulkEditStartDate '" + divcode + "', '" + sf_code + "' ";

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
        // added by sri - 22 July
        public DataSet UserList_BulkEditTPStartDate(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_BulkEditTPStartDate '" + divcode + "', '" + sf_code + "' ";

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

        //Added by sri - to show des short name
        public DataSet getDesignation_SN(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,'---Select---'  Designation_Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name AS Designation_Name " +
                     " FROM Mas_SF_Designation where Division_Code = '" + div_code + "' " +
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
        //Changes done by Saravana
        public DataSet getsf_tp(string div_code, string sReport, string ReportingMGR)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT sf_code, Sf_Name,convert(varchar,sf_TP_Active_Dt,103) sf_TP_Active_Dt,convert(varchar,Last_TP_Date,103) Last_TP_Date" +
                     " FROM mas_salesforce " +
                     " WHERE lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + div_code + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + div_code + ',' + "%') " +
                      " AND SF_Status = 0 AND sf_Tp_Active_flag = 0 " +
                       " AND (TP_Reporting_SF = '" + sReport + "'" +
                     " OR sf_Code = '" + sReport + "' " +
                     " OR sf_Code = '" + ReportingMGR + "' )" +
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

        //// To fetch UserList by using DataSet & DataTable by Recursive call - Modified by Sri - 29Aug15
        public DataTable getUserListReportingTo(string div_code, string sf_code, int order_id)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (sf_code != "admin")
                    {
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();

                        dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {

                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                        dr["sf_Type"] = drFF["sf_Type"].ToString();
                        dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                        dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                        dr["sf_hq"] = drFF["sf_hq"].ToString();
                        dr["sf_password"] = drFF["sf_password"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                        dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                        dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                        dt.Rows.Add(dr);

                        dt_recursive = getUserListReportingTo(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        //Added by sri - to check for audit
        public DataSet CheckforAudit(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Sf_Code from Mas_sf_Audit_Team Where" +
                     " (Audit_Team like '" + sf_code + ',' + "%'  or " +
                      " Audit_Team like '%" + ',' + sf_code + ',' + "%' or  " +
                     " Audit_Team like '%" + sf_code + "%' ) " +
                     " and  Division_code = '" + div_code + "'";
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

        //Added by sri - to fetch Audit Mgr Dtls
        public DataSet getAuditMgr(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag ,a.UsrDfd_UserName , '' Lst_drCount, c.StateName,convert(varchar,a.Last_TP_Date,103) Last_TP_Date,convert(varchar,a.Sf_TP_DCR_Active_Dt,103) Sf_TP_DCR_Active_Dt,  " +
                     " convert(varchar,a.Last_DCR_Date,103) Last_DCR_Date,a.sf_emp_id ,a.sf_Type as type , (select UsrDfd_UserName from Mas_Salesforce " +
                     " where sf_code=a.sf_code) +'- '+   (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To,b.Designation_Short_Name as Designation_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ";
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

        //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15
        public DataTable getUserList_Managers(string div_code, string sf_code, int order_id)
        {
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
            }

            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.sf_type = 2";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                        dr["sf_Type"] = drFF["sf_Type"].ToString();
                        dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                        dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                        dr["sf_hq"] = drFF["sf_hq"].ToString();
                        dr["sf_password"] = drFF["sf_password"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                        dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();

                        dt.Rows.Add(dr);

                        dt_recursive = getUserList_Managers(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15
        public DataTable getUserList_Managers_Audit(string div_code, string sf_code, int order_id, string Aud_Mgr)
        {
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
            }

            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code"; // Modified to include MR - 29Aug15
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        if (drFF["sf_Code"].ToString() != Aud_Mgr)
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();

                            dt.Rows.Add(dr);
                        }
                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserList_Managers_Audit(div_code, drFF["sf_code"].ToString(), order_id, Aud_Mgr);//Modified by Sri to include MR - 29Aug15
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // Added by Sri - 29Aug15
        public DataTable getUserList_ForAM(string div_code, string sf_code, int order_id)
        {
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
            }

            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag   " +
                     " FROM mas_salesforce a, Mas_SF_Designation b " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        int i = 0;
                        DataSet dsA = FillSalesForcelist_Mgr(div_code);
                        foreach (DataRow dra in dsA.Tables[0].Rows)
                        {
                            if (dra["SF_Code"].ToString() == drFF["sf_code"].ToString())
                            {
                                i = 1;
                            }
                        }
                        if (i == 0)
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();

                            dt.Rows.Add(dr);
                        }
                        dt_recursive = getUserList_ForAM(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet getAuditTeam(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " Select Audit_team from Mas_Sf_Audit_Team where " +
                     " Division_Code =  + '" + div_code + "'" +
                     " and sf_code = '" + sf_code + "'";
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


        public DataSet getAuditTeam(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " Select sf_code,Audit_team from Mas_Sf_Audit_Team where " +
                     " Division_Code =  + '" + div_code + "'";

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
        // Added Newly
        public DataTable getUserListReportingToAllNew(string div_code, string sf_code, int order_id, string sf_type) // 23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                   " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                   " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                   " when '0' then 'Active'  " +
                   " when '1' then 'Vacant'  " +
                   " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName " +
                   " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                   " WHERE a.SF_Status=0 and " +
                   " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                   " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.State_Code=c.State_Code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (sf_type == "3")
                    {
                        strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                  " FROM mas_ho_id_creation " +
                                   " WHERE HO_Active_flag = 0  and  " +
                                   "(Division_Code like '" + div_code + "%'  or " +
                                    "Division_Code like '%" + ',' + div_code + "%') and " +
                                   "Sub_HO_ID is null";

                        DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsmgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = "admin";
                            dr["sf_Name"] = "admin";
                            dr["Sf_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Type"] = "";
                            dr["Sf_Joining_Date"] = "";
                            dr["Reporting_To_SF"] = "";
                            dr["sf_hq"] = "";
                            dr["sf_password"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Designation_Short_Name"] = "Admin";
                            dr["Desig_Color"] = "33ff96";

                            dr["sf_Tp_Active_flag"] = "";
                            dr["UsrDfd_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Lst_drCount"] = "";
                            dr["StateName"] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {

                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();


                        dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        dt.Rows.Add(dr);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        //Check for Audit
                        // DataSet ds = CheckforAuditteammgr(drFF["sf_code"].ToString(), div_code);
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }
                                            dsA = getAuditMgr(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dt.Rows.Add(dr);
                        }
                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToAllNew(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //// To fetch UserList by using DataSet & DataTable by Recursive call - Sridevi on 07/23/15
        public DataTable getUserListReportingToAll(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                   " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                   " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                   " when '0' then 'Active'  " +
                   " when '1' then 'Vacant'  " +
                   " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                   " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                   " WHERE a.SF_Status=0 and " +
                   " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                   " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    dt.Rows.Add(dr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        if (drFF["sf_code"].ToString() != Audit_mgr_All)
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dt.Rows.Add(dr);
                        }
                        //Check for Audit
                        DataSet ds = CheckforAuditteammgr(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            dsA = getAuditMgr(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                            if (dsA.Tables[0].Rows.Count > 0)
                            {
                                order_id = order_id + 1;
                                dr = dt.NewRow();
                                dr["order_id"] = order_id;
                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                Audit_mgr_All = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToAll(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        //changes done by Reshmi

        public DataSet getSalesForce_React(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "  SELECT a.SF_Code, a.Sf_Name , a.Sf_UserName,sf_Tp_Active_flag, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type," +
                      " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                      " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                      " WHERE (a.Division_Code like '" + divcode + ',' + "%' or  a.Division_Code like '%" + ',' + divcode + ',' + "%') and " +
                      " SF_Status= 2  AND lower(sf_code) != 'admin' " +
                      " ORDER BY Sf_Name";
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
        public int Reactivate_Sales(string Sf_Code)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + Sf_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce " +
                         "SET SF_Status = 0 , " +
                         "LastUpdt_Date = getdate() " +
                         "WHERE Sf_Code ='" + Sf_Code + "'";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + Sf_Code + "'";
                iReturn_Backup = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Changes done by Reshmi


        public DataSet get_Ho_Id(string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSalesForce = null;

            strQry = "Select Name , User_Name ,Password,Menu_type,Division_Code ,Reporting_To,ho_id " +
                     "From Mas_HO_ID_Creation WHERE HO_Active_Flag =0 and HO_ID ='" + HO_ID + "' " +
                     "ORDER BY 2";

            try
            {
                dsSalesForce = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsSalesForce;
        }
        public DataSet Check_box()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSalesForce = null;

            strQry = "Select Name , User_Name ,Password,Menu_type,Division_Code ,Reporting_To_Name,ho_id " +
                     "From Mas_HO_ID_Creation WHERE HO_Active_Flag =0  " +
                     "ORDER BY 2";

            try
            {
                dsSalesForce = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsSalesForce;
        }

        public int Update_HO_Id(int HO_Id, string Name, string User_Name, string Password, string Menu_type, string div_code, string Reporting_To_Name)
        {
            int iReturn = -1;
            if (!RecordExist(User_Name, HO_Id))
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "Update Mas_HO_ID_Creation " +
                             "SET Name = '" + Name + "' ," +
                             "User_Name = '" + User_Name + "' ," +
                             "Password ='" + Password + "' ," +
                             "Menu_type = '" + Menu_type + "' ," +
                             "Division_Code ='" + div_code + "'," +
                             "Reporting_To_Name ='" + Reporting_To_Name + "', " +
                             "LastUpdt_Date = getdate() " +
                             "WHERE HO_ID ='" + HO_Id + "' and HO_Active_Flag =0";

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
            return iReturn;
        }
        public bool RecordExistAdd(string username)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(User_Name) FROM Mas_HO_ID_Creation WHERE User_Name='" + username + "'";
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
        public bool RecordExist(string User_Name, int HO_Id)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(User_Name) FROM Mas_HO_ID_Creation WHERE User_Name='" + User_Name + "'  AND HO_ID !='" + HO_Id + "'";
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

        //Added by sri - to check for audit - 28-Aug-15 sridevi
        public DataSet CheckforAuditteammgr(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT Sf_Code from Mas_sf_Audit_Team Where " +
                       " Mgr_Audit_Team = '" + sf_code + "' " +
                     " and  Division_code = '" + div_code + "'";
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
        //Changes done by Saravanan

        public DataSet getDCRStatus_MR(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;

            strQry = " SELECT a.sf_code, a.Sf_Name, a.Sf_HQ,a.sf_type,b.Designation_Short_Name as sf_Designation_Short_Name" +
                     " FROM mas_salesforce a, Mas_SF_Designation b" +
                     " WHERE a.Sf_Code= '" + sfcode + "' " +
                     "and a.Designation_Code=b.Designation_Code " +
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
        //New Designation 
        public DataSet getDesignation_div(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,''Designation_Short_Name,''Designation_Name,'---Select---'Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name,Designation_Name,Designation_Short_Name + ' / ' + Designation_Name AS Name " +
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

        public DataSet getHO_new(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT HO_ID,Name,User_Name,Password,Reporting_To_Name" +
                     " FROM mas_ho_id_creation " +
                     " WHERE HO_Active_flag = 0  and Name <> 'admin'  and  Division_Code = '" + div_code + "'" +
                     " ORDER BY 1";
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
        // Added Newly
        public DataTable getUserListReportingToNew(string div_code, string sf_code, int order_id, string sf_type)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;

                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (sf_type == "3")
                    {
                        strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                  " FROM mas_ho_id_creation " +
                                   " WHERE HO_Active_flag = 0  and  " +
                                   "(Division_Code like '" + div_code + "%'  or " +
                                    "Division_Code like '%" + ',' + div_code + "%') and " +
                                   " (Sub_HO_ID is null or Sub_HO_ID = '0')";

                        DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsmgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = "admin";
                            dr["sf_Name"] = "admin";
                            dr["Sf_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Type"] = "";
                            dr["Sf_Joining_Date"] = "";
                            dr["Reporting_To_SF"] = "";
                            dr["sf_hq"] = "";
                            dr["sf_password"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Designation_Short_Name"] = "Admin";
                            dr["Desig_Color"] = "33ff96";
                            dr["sf_Tp_Active_flag"] = "";
                            dr["UsrDfd_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Lst_drCount"] = "";
                            dr["StateName"] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        dt.Rows.Add(dr);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag = 1 and sf_type='1') and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }

                                            dsA = getAuditMgr(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dt.Rows.Add(dr);
                        }

                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToNew(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        // Changes done by Priya
        public DataSet SalesForceList_New(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForceGet_MR '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getSalesForcelist_New(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "  SELECT a.SF_Code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name, a.Sf_UserName, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type," +
                      " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code,c.Desig_Color from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                      " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                      " WHERE (a.Division_Code like '" + divcode + ',' + "%' or  a.Division_Code like '%" + ',' + divcode + ',' + "%') and  " +
                      " SF_Status=0  AND lower(sf_code) != 'admin' AND a.sf_Tp_Active_flag = 0 " +
                      " ORDER BY Sf_Name";
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
        public DataSet UserList_getMR_New(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

            strQry = "SELECT a.sf_Code, Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
                        " FROM mas_salesforce a " +
                        " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                        " and a.sf_code !='admin' and a.sf_type=1 " +
                        " order by 2 ";
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
        // Added by Sri - Address Book in Mail - 06-Oct-15
        public DataTable getAddressBookMgr(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));

                DataSet dsmgr = null;


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
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (dsmgr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFF in dsmgr.Tables[0].Rows)
                        {

                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_mail"] = drFF["sf_mail"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                            dr["sf_color"] = drFF["Sf_HQ"].ToString();
                            dr["des_color"] = drFF["des_color"].ToString();
                            dr["sf_type"] = drFF["sf_type"].ToString();
                            dt.Rows.Add(dr);
                            order_id = order_id + 1;
                        }
                    }
                    foreach (DataRow drFF in dsmgr.Tables[0].Rows)
                    {
                        DataSet dsAudit = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (dsAudit.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow drFF1 in dsAudit.Tables[0].Rows)
                            {
                                strQry = " select a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name,a.Reporting_To_SF,a.Sf_HQ,a.sf_type,'-Level1' as sf_color, " +
                                        " b.Desig_Color as des_color ,b.Designation_Code from Mas_Salesforce a,Mas_SF_Designation b " + // SM Level
                                        " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and a.Designation_Code=b.Designation_Code and  " +
                                        " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                        " and a.sf_code = '" + drFF1["sf_code"].ToString() + "'";

                                DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);

                                if (dsMgr1.Tables[0].Rows.Count > 0)
                                {
                                    dr = dt.NewRow();
                                    order_id = order_id + 1;
                                    dr["order_id"] = order_id;
                                    dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    dr["sf_mail"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                    dr["sf_name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                    dr["Designation_Short_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                    dr["Reporting_To_SF"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                    dr["Sf_HQ"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                    dr["sf_type"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                    dr["sf_color"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                    dr["des_color"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                    dr["Designation_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;

            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "'and a.Designation_Code=b.Designation_Code  ORDER BY sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_mail"] = drFF["sf_mail"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                        dr["sf_color"] = drFF["Sf_HQ"].ToString();
                        dr["des_color"] = drFF["des_color"].ToString();
                        dr["sf_type"] = drFF["sf_type"].ToString();
                        dt.Rows.Add(dr);

                        dt_recursive = getAddressBookMgr(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // Added by Sri - Address Book in Mail - 06-Oct-15
        public DataTable getAddressBookDesign(string div_code, string sf_code, int order_id)
        {
            if (div_code.Contains(","))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }

            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));

                DataSet dsmgr = null;


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
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (dsmgr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFF in dsmgr.Tables[0].Rows)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_mail"] = drFF["sf_mail"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                            dr["sf_color"] = drFF["Sf_HQ"].ToString();
                            dr["des_color"] = drFF["des_color"].ToString();
                            dr["sf_type"] = drFF["sf_type"].ToString();
                            dt.Rows.Add(dr);
                            order_id = order_id + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;

            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "'and a.Designation_Code=b.Designation_Code  ORDER BY sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_mail"] = drFF["sf_mail"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                        dr["sf_color"] = drFF["Sf_HQ"].ToString();
                        dr["des_color"] = drFF["des_color"].ToString();
                        dr["sf_type"] = drFF["sf_type"].ToString();
                        dt.Rows.Add(dr);

                        dt_recursive = getAddressBookMgr(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string[] TobeDistinct = { "Designation_Code", "Designation_Short_Name" };
            return GetDistinctRecords(dt, TobeDistinct);
        }

        //Following function will return Distinct records for Name, City and State column.
        public static DataTable GetDistinctRecords(DataTable dt, string[] Columns)
        {
            DataTable dtUniqRecords = new DataTable();
            dtUniqRecords = dt.DefaultView.ToTable(true, Columns);
            return dtUniqRecords;
        }
        public DataSet sp_UserList_Hierarchy_Mail(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Hierarchy_Mail '" + divcode + "', '" + sf_code + "' ";

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

        // Added by Sri - Address Book in Mail - 06-Oct-15
        public DataTable getAddressBookWithoutAdmin(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));

                DataSet dsmgr = null;


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
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (dsmgr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFF in dsmgr.Tables[0].Rows)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_mail"] = drFF["sf_mail"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                            dr["sf_color"] = drFF["Sf_HQ"].ToString();
                            dr["des_color"] = drFF["des_color"].ToString();
                            dr["sf_type"] = drFF["sf_type"].ToString();
                            dt.Rows.Add(dr);
                            order_id = order_id + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;

            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                     " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "'and a.Designation_Code=b.Designation_Code  ORDER BY sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_mail"] = drFF["sf_mail"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                        dr["sf_color"] = drFF["Sf_HQ"].ToString();
                        dr["des_color"] = drFF["des_color"].ToString();
                        dr["sf_type"] = drFF["sf_type"].ToString();
                        dt.Rows.Add(dr);

                        dt_recursive = getAddressBookMgr(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        //done by reshmi

        public DataSet getSubHO_new(string div_code, string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT HO_ID,Name,User_Name,Password " +
                     " FROM mas_ho_id_creation " +
                     " WHERE HO_Active_flag = 0  and Name <> 'admin'  and  " +
                //" (Division_Code like '" + div_code +  "%'  or " +
                //" Division_Code like '%" + ',' + div_code  + "%') and " +
                      "Sub_HO_ID='" + HO_ID + "'" +
                     " ORDER BY 1";
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

        public int Sub_HO_ID_RecordAdd(string username, string password, string name, string menu, string div_code, string reporting_to, string reporting_to_name, string Ho_Id)
        {
            int iReturn = -1;
            if (!RecordExistAddSub(username))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(HO_ID)+1,'1') HO_ID from mas_ho_id_creation ";
                    int HO_IDNew = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO mas_ho_id_creation VALUES ('" + HO_IDNew + "' ,'" + username + "' ,'" + password + "','" + name + "', '" + menu + "', '" + div_code + "', '" + reporting_to + "', '" + reporting_to_name + "',getdate(),getdate(),'0','" + Ho_Id + "') ";
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
            return iReturn;
        }

        public int Update_Sub_HO_Id(int HO_Id, string Name, string User_Name, string Password, string div_code)
        {
            int iReturn = -1;
            if (!RecordExistSub(User_Name, HO_Id))
            {

                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "Update Mas_HO_ID_Creation " +
                             "SET Name = '" + Name + "' ," +
                             "User_Name = '" + User_Name + "' ," +
                             "Password ='" + Password + "' ," +
                             "Division_Code ='" + div_code + "'," +
                             "LastUpdt_Date = getdate() " +
                             "WHERE HO_ID ='" + HO_Id + "' and HO_Active_Flag =0";

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
            return iReturn;
        }

        public DataSet get_Sub_Ho_Id(string HO_ID)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSalesForce = null;

            strQry = "Select Name , User_Name ,Password,Division_Code ,ho_id " +
                     "From Mas_HO_ID_Creation WHERE HO_Active_Flag =0 and HO_ID ='" + HO_ID + "' " +
                     "ORDER BY 2";

            try
            {
                dsSalesForce = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return dsSalesForce;
        }

        public DataSet getHO_new(string div_code, string ho_id)//changes done by reshmi
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT HO_ID,Name,User_Name,Password,Reporting_To_Name" +
                     " FROM mas_ho_id_creation " +
                     " WHERE HO_Active_flag = 0  and Name <> 'admin'  and  Division_Code = '" + div_code + "' and HO_ID='" + ho_id + "'" +
                     " ORDER BY 1";
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
        public int HO_ID_RecordAdd(string username, string password, string name, string menu, string div_code, string reporting_to, string reporting_to_name, string sub_ho)
        {
            int iReturn = -1;
            if (!RecordExistAdd(username))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(HO_ID)+1,'1') HO_ID from mas_ho_id_creation ";
                    int HO_ID = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO mas_ho_id_creation VALUES ('" + HO_ID + "' ,'" + username + "' ,'" + password + "','" + name + "', '" + menu + "', '" + div_code + "', '" + reporting_to + "', '" + reporting_to_name + "',getdate(),getdate(),'0','" + sub_ho + "') ";
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
            return iReturn;
        }
        public bool RecordExistAddSub(string username)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(User_Name) FROM Mas_HO_ID_Creation WHERE User_Name='" + username + "'";
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
        public bool RecordExistSub(string username, int Ho_Id)
        {
            bool bRecordExist = false;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(User_Name) FROM Mas_HO_ID_Creation WHERE User_Name ='" + username + "' AND HO_ID !='" + Ho_Id + "'";
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
        public DataSet sp_UserList_getMR_Doc_List(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getMR_Doc_List '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getDCRStatus_MRStatus(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;

            strQry = " SELECT a.sf_code, a.Sf_Name, a.Sf_HQ,a.sf_type,b.Designation_Short_Name,a.UsrDfd_UserName " +
                     " FROM mas_salesforce a, Mas_SF_Designation b" +
                     " WHERE a.Sf_Code= '" + sfcode + "' " +
                     "and a.Designation_Code=b.Designation_Code " +
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


        // Added by Sri - Joined Work in DCR - 25 Nov 15
        public DataTable getJointWorkDCR(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));


                DataSet dsmgr = null;


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
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (dsmgr.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drFF in dsmgr.Tables[0].Rows)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dt.Rows.Add(dr);
                            order_id = order_id + 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;

            strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                     " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                     " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and Reporting_To_SF = '" + sf_code + "' ORDER BY sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();

                        dt.Rows.Add(dr);

                        dt_recursive = getJointWorkDCR(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet getSalesForcelist_NewUser(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "  SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type," +
                     " b.StateName,a.sf_Designation_Short_Name as Designation_Name, a.State_Code as state_code,a.UsrDfd_UserName,a.Sf_Password, " +
                     " (select s.Sf_Name from mas_salesforce s where sf_code=a.TP_Reporting_SF) as Reporting_To, " +
                     " case when a.Reporting_To_SF='admin' then " +
                     " (select User_Name from Mas_HO_ID_Creation where   (Division_Code like '" + divcode + ',' + "%'  or Division_Code like '%" + ',' + divcode + ',' + "%') and  (Sub_HO_ID is null or Sub_HO_ID = '0')  ) " +
                     " else " +
                     " (select s.UsrDfd_UserName from mas_salesforce s where sf_code=a.TP_Reporting_SF) end as rep_username, " +
                     " case when a.Reporting_To_SF='admin' then " +
                     " (select Password from Mas_HO_ID_Creation where   (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') and (Sub_HO_ID is null or Sub_HO_ID = '0') ) " +
                     " else " +
                     " (select s.Sf_Password from mas_salesforce s where sf_code=a.TP_Reporting_SF) end as rep_password, " +
                     " CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'A'  " +
                     " when '1' then 'V' end sf_Tp_Active_flag  " +
                      "  from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                // " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                      " WHERE (a.Division_Code like '" + divcode + ',' + "%' or  a.Division_Code like '%" + ',' + divcode + ',' + "%') and  " +
                      " lower(sf_code) != 'admin' " +
                      " AND a.sf_Tp_Active_flag = 0 AND SF_Status=0   " +
                      " ORDER BY Sf_Name";
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

        public DataSet FindSalesForcelist_NewUser(string sFindQry, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.sf_Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " ,a.UsrDfd_UserName,a.Sf_Password, (select s.Sf_Name from mas_salesforce s where sf_code=a.TP_Reporting_SF) as Reporting_To,  " +

                     " case when a.Reporting_To_SF='admin' then " +
                     " (select User_Name from Mas_HO_ID_Creation where   (Division_Code like '" + divcode + ',' + "%'  or Division_Code like '%" + ',' + divcode + ',' + "%') and  (Sub_HO_ID is null or Sub_HO_ID = '0')  ) " +
                     " else " +
                     " (select s.UsrDfd_UserName from mas_salesforce s where sf_code=a.TP_Reporting_SF) end as rep_username, " +
                     " case when a.Reporting_To_SF='admin' then " +
                     " (select Password from Mas_HO_ID_Creation where   (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') and (Sub_HO_ID is null or Sub_HO_ID = '0') ) " +
                     " else " +
                     " (select s.Sf_Password from mas_salesforce s where sf_code=a.TP_Reporting_SF) end as rep_password, " +
                     " CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'A'  " +
                     " when '1' then 'V' end sf_Tp_Active_flag  " +
                     " FROM mas_salesforce a, mas_state b " +
                     " WHERE  " +
                     " lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                //   " AND a.Designation_Code = c.Designation_Code" +
                //  " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
                     sFindQry +
                     " ORDER BY Sf_Name";
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
        public DataSet FindSalesForcelistApp(string sFindQry)//done by resh
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
            //         " FROM mas_salesforce a, mas_state b,Mas_SF_Designation c " +
            //         " WHERE SF_Status=0 " +
            //         " AND lower(sf_code) != 'admin' " +
            //         " AND a.State_Code=b.State_Code " +
            //         " AND a.Designation_Code = c.Designation_Code" +
            //         " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 " +
            //         sFindQry +
            //         " ORDER BY Sf_Name";

            strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=a.Reporting_To) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                    " WHERE c.sf_TP_Active_Flag=0 and c.SF_Status=0" +
                    sFindQry +
                    " ORDER BY Sf_Name";

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

        public DataSet getSalesForce_desAp(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            //strQry = "SELECT SF_Code, Sf_Name, Sf_UserName,Sf_Password, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code" +
            //         " FROM  mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
            //         " where lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
            //         " AND a.sf_Tp_Active_flag = 0  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' " +
            //         " or a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
            //         " ORDER BY 2";

            strQry = "SELECT a.SF_Code, a.Sf_Name,a.Sf_HQ ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=a.Reporting_To)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=a.Reporting_To) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=a.Reporting_To) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                    " WHERE  c.sf_TP_Active_Flag=0 and c.SF_Status=0 " +
                    " AND c.Designation_Code = '" + Designation_Code + "' AND a.Division_Code = '" + divcode + "' " +
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

        //done by resh
        public DataSet getDesignation_SNM(string div_code, int sf_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,'---Select---'  Designation_Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name AS Designation_Name " +
                     " FROM Mas_SF_Designation where Division_Code = '" + div_code + "' and Designation_Active_Flag=0 and Type = '" + sf_type + "' " +
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

        public DataSet getSalesForcelist_Rep(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ" +
                     " FROM mas_salesforce   " +
                     " WHERE SF_Status=0 " +
                //" AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                     " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND (sf_type=2 or ISNULL(sf_type,'')='') and   sf_Tp_Active_flag = 0 " +
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

        public int RecordUpdate_App(string sf_code, string DCR, string TP, string Lst_Dr, string leave, string SS_AM, string Expense, string Otr_AM)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = " UPDATE Mas_Salesforce_AM " +
                         " SET DCR_AM = '" + DCR + "', " +
                         " TP_AM = '" + TP + "', " +
                         " LstDr_AM = '" + Lst_Dr + "', " +
                         " Leave_AM = '" + leave + "'," +
                         " SS_AM = '" + SS_AM + "'," +
                         " Expense_AM = '" + Expense + "'," +
                         " Otr_AM = '" + Otr_AM + "'" +
                         " WHERE sf_code = '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }
        // Added by Sridevi - Audit Manager login 
        // Audit Manager Team
        public DataTable getAuditManagerTeam(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Des_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));

                dt_recursive_Aud.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Des_Color", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));


                strQry = " SELECT a.sf_Code, sf_Name + ' - '+ sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name , a.sf_Type, " +
                    " b.Designation_Short_Name, b.Desig_Color,a.Sf_HQ,convert(char(10),a.Sf_Joining_Date,103) as Sf_Joining_Date, b.Desig_Color as Des_Color,a.Sf_UserName," +
                    " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                    " FROM mas_salesforce a, Mas_SF_Designation b " +
                    " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                    " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                    " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";

                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["Des_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                    dt.Rows.Add(dr);
                }

            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {

                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, sf_Name + ' - '+ sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name , a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color,a.Sf_HQ,convert(char(10),a.Sf_Joining_Date,103) as Sf_Joining_Date,b.Desig_Color as Des_Color,a.Sf_UserName, " +
                                " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {

                                    dr = dt.NewRow();
                                    order_id = order_id + 1;

                                    dr["order_id"] = order_id;
                                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                    dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                    dr["Des_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                    dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();

                                    dt.Rows.Add(dr);
                                    dt_recursive_Aud.Rows.Clear();
                                    dt_recursive = getAuditTeamMGRRep(div_code, dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                                    if (dt_recursive != null)
                                    {
                                        if (dt_recursive.Rows.Count > 0)
                                        {
                                            foreach (DataRow dataRow in dt_recursive.Rows)
                                            {
                                                dr = dt.NewRow();
                                                order_id = order_id + 1;
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                dr["Desig_Color"] = dataRow["Desig_Color"].ToString();
                                                dr["Sf_HQ"] = dataRow["Sf_HQ"].ToString();
                                                dr["Sf_Joining_Date"] = dataRow["Sf_Joining_Date"].ToString();
                                                dr["Des_Color"] = dataRow["Des_Color"].ToString();
                                                dr["Sf_UserName"] = dataRow["Sf_UserName"].ToString();
                                                dr["Reporting_To_SF"] = dataRow["Reporting_To_SF"].ToString();

                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return dt;

        }

        // Added by Sridevi - Audit Manager login 

        // Get Audit Team - Mgr - Reporting 
        public DataTable getAuditTeamMGRRep(string div_code, string sf_code, int order_id)
        {

            DataTable dt_recursive1 = new DataTable();

            DataSet dsDivision = null;
            DB_EReporting db_ER = new DB_EReporting();
            strQry = " SELECT a.sf_Code, Sf_Name + ' - '+ sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name , a.sf_Type, " +
          " b.Designation_Short_Name, b.Desig_Color,a.Sf_HQ,convert(char(10),a.Sf_Joining_Date,103) as Sf_Joining_Date,b.desig_Color as Des_Color" +
          " FROM mas_salesforce a, Mas_SF_Designation b " +
          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
          " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
           " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt_recursive_Aud.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["sf_Type"] = drFF["sf_Type"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                        dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                        dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                        dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                        dr["Des_Color"] = drFF["Des_Color"].ToString();
                        dt_recursive_Aud.Rows.Add(dr);

                        dt_recursive1 = getAuditTeamMGRRep(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt_recursive_Aud;
        }

        // Audit Manager Team - MR only
        public DataTable getAuditManagerTeam_GetMR(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_username", typeof(string)));
            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {
                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.Sf_Name + '-'+ a.sf_Designation_Short_Name + '-' + a.Sf_HQ as sf_Name, a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color , a.sf_Designation_Short_Name, a.Sf_HQ, a.sf_username" +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {
                                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                                    {
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["sf_Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_username"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        string mgrcode = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        strQry = "EXEC [sp_get_Rep_audit]  '" + mgrcode + "' , '" + div_code + "'";

                                        dsSF = db_ER.Exec_DataSet(strQry);

                                        if (dsSF != null)
                                        {
                                            if (dsSF.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                                {
                                                    dr = dt.NewRow();
                                                    order_id = order_id + 1;

                                                    dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                    dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                    dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                    dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                    dr["Desig_Color"] = "";

                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            order_id = order_id + 1;
                        }
                    }
                }
            }
            return dt;

        }

        // Audit Manager Team - MR only for DCR Manager 
        public DataTable getAuditManagerTeam_GetMR_DCR(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            DataSet ds = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));

                dr = dt.NewRow();
                dr["order_id"] = order_id;
                dr["sf_Code"] = "0";
                dr["sf_Name"] = "---Select---";
                dr["sf_Type"] = "0";
                dr["Designation_Short_Name"] = "";
                dr["Desig_Color"] = "";

                dt.Rows.Add(dr);
            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {

                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.sf_HQ + '(' + a.Sf_Name + ')' As sf_Name, a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color" +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {

                                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                                    {
                                        dr = dt.NewRow();
                                        order_id = order_id + 1;

                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        string mgrcode = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        strQry = "EXEC sp_get_Rep  '" + mgrcode + "'";

                                        dsSF = db_ER.Exec_DataSet(strQry);

                                        if (dsSF != null)
                                        {
                                            if (dsSF.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                                {
                                                    dr = dt.NewRow();
                                                    order_id = order_id + 1;
                                                    dr["order_id"] = order_id;
                                                    dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                    dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                    dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                    dr["Designation_Short_Name"] = "";
                                                    dr["Desig_Color"] = "";

                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return dt;

        }
        // Audit Manager Team - MGR only
        public DataTable getAuditManagerTeam_GetMGR(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));

                strQry = " SELECT a.sf_Code, sf_Name + ' - '+ sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name , a.sf_Type, " +
                 " b.Designation_Short_Name, b.Desig_Color,Sf_HQ " +
                 " FROM mas_salesforce a, Mas_SF_Designation b " +
                 " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                 " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                 " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";

                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["des_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dt.Rows.Add(dr);
                }

            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {

                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.Sf_Name + '-'+ a.sf_Designation_Short_Name + '-' + a.Sf_HQ as sf_Name, a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color,Sf_HQ" +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {
                                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "2")
                                    {
                                        dr = dt.NewRow();
                                        order_id = order_id + 1;
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["des_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();

                                        dt.Rows.Add(dr);

                                        string mgrcode = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        strQry = "EXEC [sp_UserList_Hierarchy_Aud]  '" + div_code + "','" + mgrcode + "' ";

                                        dsSF = db_ER.Exec_DataSet(strQry);

                                        if (dsSF != null)
                                        {
                                            if (dsSF.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                                {
                                                    dr = dt.NewRow();
                                                    order_id = order_id + 1;

                                                    dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                    dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                    dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                    dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                    dr["Desig_Color"] = dataRow["des_color"].ToString();
                                                    dr["des_color"] = dataRow["des_color"].ToString();
                                                    dr["Sf_HQ"] = dataRow["Sf_HQ"].ToString();
                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }

                        }
                    }
                }
            }
            return dt;

        }


        public DataSet getSalesForcelist_TP_Alphabet(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 0 " +
                     " ORDER BY 1";
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
        public DataSet getDoctorMgr_View(string mgr_code, string type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (mgr_code != "admin"))
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where sf_code in (" + mgr_code + ") ) and a.ListedDr_Active_Flag = 0";
            }
            else if ((type == "0") && (mgr_code == "admin"))
            {
                swhere = swhere + "and a.ListedDr_Active_Flag = 0 ";
            }
            else
            {
                swhere = swhere + "and a.Sf_Code in " +
                                "(select sf_code from Mas_Salesforce " +
                                " where Sf_Code !='admin' and State_Code = '" + mgr_code + "'  and sf_type = 1)";
            }


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,case isnull(ListedDr_DOB,null)" +
                       " when '1900-01-01 00:00:00.000' then null  else  ListedDr_DOB  end ListedDr_DOB," +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW,ListedDr_Mobile,ListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and  a.Doc_ClsCode=b.Doc_ClsCode and a.Division_Code='" + div_code + "'  " +
                       swhere +
                       " order by ListedDr_Name ";

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

        public DataSet getDoctorCategory(string sf_code, string cat_code, string type, string strsf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDocCat = null;
            string swhere = string.Empty;

            if ((type == "0") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Cat_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "1") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_Special_Code = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "2") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_ClsCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }
            else if ((type == "3") && (cat_code != "-1"))
            {
                swhere = " and a.Doc_QuaCode = '" + cat_code + "' and a.Sf_Code in (" + strsf_Code + ") ";
            }

            if (sf_code != "-1")
            {
                swhere = swhere + "and a.Sf_Code = '" + sf_code + "' ";
            }

            //strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
            //           " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code,ListedDr_DOB,ListedDr_DOW, " +
            //           " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name,  " +
            //           " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
            //           " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
            //           " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
            //           " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
            //           " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
            //           " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
            //           " and a.Doc_ClsCode=b.Doc_ClsCode " +
            //           swhere +
            //           " order by ListedDr_Name ";

            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode, a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code," +
                       " case isnull(ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " (select Sf_Name from mas_salesforce where sf_code = a.Sf_code) sf_name, " +
                       " (select sf_Designation_Short_Name from mas_salesforce where sf_code = a.Sf_code) sf_Designation_Short_Name, " +
                       " (select Sf_HQ from mas_salesforce where sf_code = a.Sf_code) Sf_HQ, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       swhere +
                       " order by ListedDr_Name ";
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
        public int getDoctorcount_Total(string sf_code, string cat_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in (" + strSf_Code + ") " +
                              " and Doc_Cat_Code = '" + cat_code + "' and ListedDr_Active_Flag= 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getSpecialcount_Total(string sf_code, string spec_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_Special_Code = '" + spec_code + "' and ListedDr_Active_Flag=0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getClasscount_Total(string sf_code, string Doc_ClsCode, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();
                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                             " where Sf_Code in(" + strSf_Code + ") " +
                             " and Doc_ClsCode = '" + Doc_ClsCode + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public int getQualcount_Total(string sf_code, string qual_code, string strSf_Code)
        {

            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                if (sf_code != "-1")
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code = '" + sf_code + "' " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }
                else
                {
                    strQry = "select COUNT(listeddrcode) from Mas_ListedDr " +
                              " where Sf_Code in(" + strSf_Code + ") " +
                              " and Doc_QuaCode = '" + qual_code + "' and ListedDr_Active_Flag = 0 ";
                }

                iReturn = db.Exec_Scalar(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getTerritory_Rep_Total(string Division_Code, int type, string strSF_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            if (Division_Code.Contains(","))
            {
                Division_Code = Division_Code.Remove(Division_Code.Length - 1);
            }

            strQry = " select Territory_Code, Territory_Name, Territory_SName " +
                       " from Mas_Territory_Creation " +
                       " where Division_Code = '" + Division_Code + "' and sf_code in(" + strSF_code + ") and Territory_Active_Flag = " + type +
                       " order by Territory_Name ";

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

        public DataSet getDoctor_Rep_Total(string Div_Code, int type, string strSF_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            //if (Div_Code.Contains(','))
            //{
            //    Div_Code = Div_Code.Remove(Div_Code.Length - 1);
            //}


            strQry = " select ListedDrCode, ListedDr_Sl_No, ListedDr_Name, a.Doc_QuaCode,ListedDr_Address1,ListedDr_Address2, " +
                       " ListedDr_Address3, ListedDr_PinCode,a.Doc_Special_Code,a.Doc_Cat_Code,a.Doc_ClsCode,a.Territory_Code, " +
                       " case isnull(a.ListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  ListedDr_DOB end ListedDr_DOB, " +
                       " case  isnull(a.ListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else ListedDr_DOW end ListedDr_DOW, " +
                       " ListedDr_Mobile,ListedDr_Email, b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, " +
                       " stuff((select ', '+territory_Name from Mas_Territory_Creation t where a.Territory_Code = cast(t.Territory_Code as varchar) and  charindex(cast(t.Territory_Code as varchar)+',',a.Territory_Code+',')>0 for XML path('')),1,2,'') Territory_Name " +
                       " from Mas_ListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e" +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Doc_ClsCode=b.Doc_ClsCode " +
                       " and a.Division_Code = '" + Div_Code + "' and a.sf_code in(" + strSF_code + ") and a.ListedDr_Active_Flag = " + type +
                       " order by ListedDr_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getNonDoctor_Rep_Total(string Div_Code, int type, string strSF_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            if (Div_Code.Contains(","))
            {
                Div_Code = Div_Code.Remove(Div_Code.Length - 1);
            }


            strQry = " select UnListedDrCode, UnListedDr_Name, UnListedDr_Mobile,UnListedDr_Email, " +
                       " b.Doc_ClsName,c.Doc_QuaName, d.Doc_Cat_Name,e.Doc_Special_Name, f.Territory_Name, " +
                       " case isnull(a.UnListedDr_DOB,null) when '1900-01-01 00:00:00.000' then null else  a.UnListedDr_DOB end UnListedDr_DOB, " +
                       " case  isnull(a.UnListedDr_DOW,null) when '1900-01-01 00:00:00.000'then null else a.UnListedDr_DOW end UnListedDr_DOW " +
                       " from Mas_UnListedDr a, Mas_Doc_Class b, Mas_Doc_Qualification c, Mas_Doctor_Category d, Mas_Doctor_Speciality e, Mas_Territory_Creation f " +
                       " where a.Doc_QuaCode = c.Doc_QuaCode and a.Doc_Special_Code = e.Doc_Special_Code and a.Doc_Cat_Code = d.Doc_Cat_Code " +
                       " and a.Territory_Code = f.Territory_Code and a.Doc_ClsCode=b.Doc_ClsCode " +
                       " and a.Division_Code = '" + Div_Code + "' and a.sf_code in(" + strSF_code + ") and a.UnListedDr_Active_Flag = " + type +
                       " order by UnListedDr_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getChemists_Rep_Total(string Div_Code, int type, string strSF_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            if (Div_Code.Contains(","))
            {
                Div_Code = Div_Code.Remove(Div_Code.Length - 1);
            }


            strQry = " select chemists_code,Chemists_Name,Chemists_Contact, " +
                       " Chemists_Phone,b.Territory_Name " +
                       " from Mas_Chemists a, Mas_Territory_Creation b " +
                       " where a.Territory_Code=b.Territory_Code and  a.Division_Code = '" + Div_Code + "' and a.sf_code in(" + strSF_code + ")  and a.Chemists_Active_Flag  = " + type +
                       " order by Chemists_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet getStockiest_Rep_Total(string Div_Code, int type, string strSF_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDoc = null;

            if (Div_Code.Contains(","))
            {
                Div_Code = Div_Code.Remove(Div_Code.Length - 1);
            }

            strQry = " select Stockist_Code,Stockist_Name,Stockist_ContactPerson,Stockist_Mobile " +
                       " from Mas_Stockist " +
                       " where Division_Code='" + Div_Code + "'  and sf_code in(" + strSF_code + ")  Stockist_Active_Flag  = " + type +
                       " order by Stockist_Name ";

            try
            {
                dsDoc = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsDoc;
        }

        public DataSet sp_UserListMr_Doc_List(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_Rep_audit '" + sf_code + "', '" + divcode + "' ";

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

        // Audit Manager Team - MR only --- only Sfname
        public DataTable getAuditManagerTeam_GetMR_Sfname(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_username", typeof(string)));
            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {
                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.sf_Name, a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color , a.sf_Designation_Short_Name, a.Sf_HQ, a.sf_username" +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {
                                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                                    {
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["sf_Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_username"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        string mgrcode = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        strQry = "EXEC [sp_get_Rep_audit_sfName]  '" + mgrcode + "' , '" + div_code + "'";

                                        dsSF = db_ER.Exec_DataSet(strQry);

                                        if (dsSF != null)
                                        {
                                            if (dsSF.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                                {
                                                    dr = dt.NewRow();
                                                    order_id = order_id + 1;

                                                    dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                    dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                    dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                    dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                    dr["Desig_Color"] = "";
                                                    dr["sf_Designation_Short_Name"] = dataRow["sf_Designation_Short_Name"].ToString();
                                                    dr["Sf_HQ"] = dataRow["Sf_HQ"].ToString();
                                                    dr["sf_username"] = dataRow["sf_username"].ToString();
                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            order_id = order_id + 1;
                        }
                    }
                }
            }
            return dt;

        }


        // Added by Sri - Joined Work in DCR - MR - 20 Dec 15
        public DataTable getMRJointWork(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable DJW = new DataTable();

            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));

                strQry = " Select Sf_Code,'SELF' Sf_Name, 'ZZZZ' Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

                DataSet ds = db_ER.Exec_DataSet(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dt.Rows.Add(dr);
                }
            }
            DataSet dsmgr = null;

            strQry = " Select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

            try
            {
                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "admin")
                    {
                        // Check if the Sf-Code has any Audit Manager.
                        DataSet dsAudit = CheckforAudit(dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                        if (dsAudit.Tables[0].Rows.Count > 0)
                        {
                            strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                             " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                             " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                             " and sf_code = '" + dsAudit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                            DataSet dsMgr2 = db_ER.Exec_DataSet(strQry);
                            if (dsMgr2.Tables[0].Rows.Count > 0)
                            {
                                dr = dt.NewRow();
                                order_id = order_id + 1;
                                dr["order_id"] = order_id;
                                dr["sf_Code"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                dr["sf_Name"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                dt.Rows.Add(dr);
                            }
                        }

                        strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                                 " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                                 " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                 " and sf_code = '" + dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                        DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsMgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            order_id = order_id + 1;
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dt.Rows.Add(dr);

                            DJW = getMRJointWork(div_code, dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        // Added by Sri - Joined Work in DCR - MGR 
        public DataTable getManagerJointWork(string div_code, string sf_code, int order_id, string DCRDate, string MGR_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable DJW = new DataTable();

            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));

                strQry = " Select Sf_Code,'SELF' Sf_Name, 'ZZZZ' Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

                DataSet ds = db_ER.Exec_DataSet(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dt.Rows.Add(dr);
                }

                DataSet ds1 = null;

                strQry = "select distinct a.sf_code,b.Sf_Name + '-'+ b.sf_Designation_Short_Name + '-' + b.Sf_HQ as Sf_Name from DCR_MGR_WorkAreaDtls a,mas_salesforce b Where " +
                    " a.MGR_Code ='" + MGR_Code + "' and  a.DCR_Date ='" + DCRDate + "' and a.sf_code = b.sf_code ";

                ds1 = db_ER.Exec_DataSet(strQry);

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in ds1.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["Sf_Name"].ToString();
                        dt.Rows.Add(dr);

                        strQry = "EXEC [sp_get_Reporting_To_MGR_Upp]  '" + drFF["sf_code"].ToString() + "'";

                        dsSF = db_ER.Exec_DataSet(strQry);

                        if (dsSF != null)
                        {
                            if (dsSF.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                {
                                    if (MGR_Code != dataRow["Sf_Code"].ToString())
                                    {
                                        dr = dt.NewRow();
                                        order_id = order_id + 1;

                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dataRow["Sf_Code"].ToString();
                                        dr["sf_Name"] = dataRow["Sf_Name"].ToString();
                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            DataSet dsmgr = null;

            strQry = " Select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

            try
            {
                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {

                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "admin")
                    {
                        // Check if the Sf-Code has any Audit Manager.
                        DataSet dsAudit = CheckforAudit(dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                        if (dsAudit.Tables[0].Rows.Count > 0)
                        {
                            strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                             " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                             " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                             " and sf_code = '" + dsAudit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                            DataSet dsMgr2 = db_ER.Exec_DataSet(strQry);
                            if (dsMgr2.Tables[0].Rows.Count > 0)
                            {
                                dr = dt.NewRow();
                                order_id = order_id + 1;
                                dr["order_id"] = order_id;
                                dr["sf_Code"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                dr["sf_Name"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                dt.Rows.Add(dr);
                            }
                        }

                        strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF  from Mas_Salesforce " + // SM Level
                                 " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                                 " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                 " and sf_code = '" + dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                        DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsMgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            order_id = order_id + 1;
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dt.Rows.Add(dr);

                            DJW = getManagerJointWork(div_code, dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id, DCRDate, MGR_Code);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        // Audit Manager Team
        public DataTable getAuditManagerTeam_User(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));


                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));

                dt_recursive_Aud.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Desig_Color", typeof(string)));

                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("StateName", typeof(string)));


                strQry = " SELECT a.sf_Code, a.Sf_Name , a.sf_Type, " +

                    " b.Designation_Short_Name, b.Desig_Color, convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     "  CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                    //" FROM mas_salesforce a, Mas_SF_Designation b " +
                    " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                    " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                    " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";

                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();



                    dt.Rows.Add(dr);
                }

            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {

                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.sf_Name , a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color, convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                                " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                                "  CASE a.sf_Tp_Active_flag   " +
                                " when '0' then 'Active'  " +
                                " when '1' then 'Vacant'  " +
                                " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                                " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                                //  " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {

                                    dr = dt.NewRow();
                                    order_id = order_id + 1;

                                    dr["order_id"] = order_id;
                                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();

                                    dt.Rows.Add(dr);
                                    dt_recursive_Aud.Rows.Clear();
                                    dt_recursive = getAuditTeamMGRRep_User(div_code, dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                                    if (dt_recursive != null)
                                    {
                                        if (dt_recursive.Rows.Count > 0)
                                        {
                                            foreach (DataRow dataRow in dt_recursive.Rows)
                                            {
                                                dr = dt.NewRow();
                                                order_id = order_id + 1;
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                dr["Desig_Color"] = dataRow["Desig_Color"].ToString();
                                                dr["Sf_Joining_Date"] = dataRow["Sf_Joining_Date"].ToString();
                                                dr["Reporting_To_SF"] = dataRow["Reporting_To_SF"].ToString();
                                                dr["sf_hq"] = dataRow["sf_hq"].ToString();
                                                dr["sf_password"] = dataRow["sf_password"].ToString();
                                                dr["sf_Tp_Active_flag"] = dataRow["sf_Tp_Active_flag"].ToString();
                                                dr["UsrDfd_UserName"] = dataRow["UsrDfd_UserName"].ToString();
                                                dr["Lst_drCount"] = dataRow["Lst_drCount"].ToString();
                                                dr["StateName"] = dataRow["StateName"].ToString();

                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return dt;

        }

        // Get Audit Team - Mgr - Reporting User
        public DataTable getAuditTeamMGRRep_User(string div_code, string sf_code, int order_id)
        {

            DataTable dt_recursive1 = new DataTable();

            DataSet dsDivision = null;
            DB_EReporting db_ER = new DB_EReporting();
            strQry = " SELECT a.sf_Code, a.Sf_Name , a.sf_Type, " +
          " b.Designation_Short_Name, b.Desig_Color, convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
          " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
          "  CASE a.sf_Tp_Active_flag   " +
          " when '0' then 'Active'  " +
          " when '1' then 'Vacant'  " +
          " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
          " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                //" FROM mas_salesforce a, Mas_SF_Designation b " +
          " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
          " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
           " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt_recursive_Aud.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = drFF["sf_code"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["sf_Type"] = drFF["sf_Type"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Desig_Color"] = drFF["Desig_Color"].ToString();

                        dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                        dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                        dr["sf_hq"] = drFF["sf_hq"].ToString();
                        dr["sf_password"] = drFF["sf_password"].ToString();
                        dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                        dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                        dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                        dr["StateName"] = drFF["StateName"].ToString();
                        dt_recursive_Aud.Rows.Add(dr);

                        dt_recursive1 = getAuditTeamMGRRep_User(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt_recursive_Aud;
        }


        // Audit Manager Team
        public DataTable getAuditManagerTeam_UserAll(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));


                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));

                dt_recursive_Aud.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Desig_Color", typeof(string)));

                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("StateName", typeof(string)));


                strQry = " SELECT a.sf_Code, a.Sf_Name , a.sf_Type, " +

                    " b.Designation_Short_Name, b.Desig_Color, convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     "  CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                    //" FROM mas_salesforce a, Mas_SF_Designation b " +
                    " WHERE a.SF_Status=0 and  " +
                    " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                    " and a.sf_code = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";

                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();



                    dt.Rows.Add(dr);
                }

            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {

                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.sf_Name , a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color, convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                                " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                                "  CASE a.sf_Tp_Active_flag   " +
                                " when '0' then 'Active'  " +
                                " when '1' then 'Vacant'  " +
                                " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName  " +
                                " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                                //  " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status=0 and   " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {

                                    dr = dt.NewRow();
                                    order_id = order_id + 1;

                                    dr["order_id"] = order_id;
                                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();

                                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();

                                    dt.Rows.Add(dr);
                                    dt_recursive_Aud.Rows.Clear();
                                    dt_recursive = getAuditTeamMGRRep_User(div_code, dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                                    if (dt_recursive != null)
                                    {
                                        if (dt_recursive.Rows.Count > 0)
                                        {
                                            foreach (DataRow dataRow in dt_recursive.Rows)
                                            {
                                                dr = dt.NewRow();
                                                order_id = order_id + 1;
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                dr["Desig_Color"] = dataRow["Desig_Color"].ToString();
                                                dr["Sf_Joining_Date"] = dataRow["Sf_Joining_Date"].ToString();
                                                dr["Reporting_To_SF"] = dataRow["Reporting_To_SF"].ToString();
                                                dr["sf_hq"] = dataRow["sf_hq"].ToString();
                                                dr["sf_password"] = dataRow["sf_password"].ToString();
                                                dr["sf_Tp_Active_flag"] = dataRow["sf_Tp_Active_flag"].ToString();
                                                dr["UsrDfd_UserName"] = dataRow["UsrDfd_UserName"].ToString();
                                                dr["Lst_drCount"] = dataRow["Lst_drCount"].ToString();
                                                dr["StateName"] = dataRow["StateName"].ToString();

                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return dt;

        }

        // Added by Ram
        public DataSet getSecSales_MR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC [sp_get_Rep_DivCode] '" + divcode + "', '" + sf_code + "' ";

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

        public DataTable getAuditTeamMGRRep_Mail(string div_code, string sf_code, int order_id)
        {

            DataTable dt_recursive1 = new DataTable();

            DataSet dsDivision = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = " SELECT a.sf_Code,'admin-Level1' as sf_mail,sf_name, " +
                    " b.Designation_Short_Name,a.Reporting_To_SF,a.Sf_HQ, a.sf_Type,'-Level1' as sf_color,b.Desig_Color as des_color,a.Designation_Code" +
                    " FROM mas_salesforce a, Mas_SF_Designation b " +
                    " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                    " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        order_id = order_id + 1;
                        dr = dt_recursive_Aud.NewRow();

                        dr["sf_Code"] = drFF["sf_Code"].ToString();
                        dr["sf_mail"] = drFF["sf_mail"].ToString();
                        dr["sf_Name"] = drFF["sf_Name"].ToString();
                        dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                        dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                        dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                        dr["sf_type"] = drFF["sf_type"].ToString();
                        dr["sf_color"] = drFF["sf_color"].ToString();
                        dr["des_color"] = drFF["des_color"].ToString();
                        dr["sf_type"] = drFF["sf_type"].ToString();
                        dr["Designation_Code"] = drFF["Designation_Code"].ToString();
                        dt_recursive_Aud.Rows.Add(dr);

                        dt_recursive1 = getAuditTeamMGRRep_Mail(div_code, drFF["sf_code"].ToString(), order_id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt_recursive_Aud;
        }

        public DataTable getAuditManagerTeam_mail(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));


                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_type", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Designation_Code", typeof(string)));
                dt_recursive_Aud.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));

                strQry = " SELECT  'admin' Sf_Code,'admin-Level1' as sf_mail,Name as sf_name,'admin' Designation_Short_Name, 'ZZZ' Reporting_To_SF, '' Sf_HQ,'' sf_type,'-Level1' as sf_color, '' des_color ,'' Designation_Code,'' Sf_Joining_Date from Mas_HO_ID_Creation" +
                         " Where (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%' or Division_Code like '" + div_code + "') " +
                         " UNION " +
                         " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color ,b.Designation_Code,a.Sf_Joining_Date" +
                         " from Mas_Salesforce  a, Mas_SF_Designation b    " + // AM Level
                             " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') " +
                             " UNION" +
                            " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code,a.Sf_Joining_Date" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                              " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                             " ( select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) " +
                             " UNION " +
                              " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code,a.Sf_Joining_Date" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                            " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                             " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce  where Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "'))) " +
                             " UNION " +
                              " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code,a.Sf_Joining_Date" +
                      " from Mas_Salesforce  a, Mas_SF_Designation b    " +
                              " where a.Designation_Code=b.Designation_Code and a.Sf_Code !='admin' and a.Sf_Code in " +
                             " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                             " (select Reporting_To_SF  from Mas_Salesforce where Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce where Sf_Code in " +
                             " (select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "') ) ) ) order by Reporting_To_SF Desc ";

                dsmgr = db_ER.Exec_DataSet(strQry);

                dr = dt.NewRow();

                dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                dr["sf_mail"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                dr["sf_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                dr["des_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                dr["Designation_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                dt.Rows.Add(dr);

            }

            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                Audit = AuditMgr.Split(',');
                foreach (string Au_cd in Audit)
                {
                    if (Au_cd.Length > 0)
                    {
                        strQry = " SELECT a.sf_Code,'admin-Level1' as sf_mail,a.Sf_Name , " +
                            " b.Designation_Short_Name,a.Reporting_To_SF,a.Sf_HQ,a.sf_Type,'-Level1' as sf_color,b.Desig_Color as des_color," +
                            " a.Designation_Code,a.Sf_Joining_Date FROM mas_salesforce a, Mas_SF_Designation b " +
                            " WHERE a.SF_Status=0 and a.sf_Tp_Active_flag = 0 and  " +
                            " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                            " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                        try
                        {
                            dsmgr = db_ER.Exec_DataSet(strQry);

                            dr = dt.NewRow();
                            order_id = order_id + 1;

                            //dr["order_id"] = order_id;
                            dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_mail"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                            dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            dr["sf_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                            dr["des_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                            dr["Designation_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                            dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();


                            dt.Rows.Add(dr);

                            dt_recursive = getAuditTeamMGRRep_Mail(div_code, dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), 0);
                            if (dt_recursive != null)
                            {
                                if (dt_recursive.Rows.Count > 0)
                                {
                                    foreach (DataRow dataRow in dt_recursive.Rows)
                                    {
                                        dr = dt.NewRow();
                                        //order_id = order_id + 1;
                                        //dr["order_id"] = order_id;
                                        dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                        dr["sf_mail"] = dataRow["sf_mail"].ToString();
                                        dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                        dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                        dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                        dr["Reporting_To_SF"] = dataRow["Reporting_To_SF"].ToString();
                                        dr["Sf_HQ"] = dataRow["Sf_HQ"].ToString();
                                        dr["sf_color"] = dataRow["sf_color"].ToString();
                                        dr["des_color"] = dataRow["des_color"].ToString();
                                        dr["Designation_Code"] = dataRow["Designation_Code"].ToString();
                                        dr["Sf_Joining_Date"] = dataRow["Sf_Joining_Date"].ToString();

                                        dt.Rows.Add(dr);
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            return dt;
        }

        public DataTable getMail_MRJointWork(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable DJW = new DataTable();

            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("Sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_mail", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_type", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_color", typeof(string)));
                dt.Columns.Add(new DataColumn("des_color", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Code", typeof(string)));

            }
            DataSet dsmgr = null;

            if (order_id == 0)
            {
                strQry = " SELECT  'admin' Sf_Code,'admin-Level1' as sf_mail,Name as sf_name,'admin' Designation_Short_Name, 'ZZZ' Reporting_To_SF, '' Sf_HQ,'' sf_type,'-Level1' as sf_color, '' des_color ,'' Designation_Code from Mas_HO_ID_Creation" +
                         " Where (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%' or Division_Code like '" + div_code + "') ";

                dsmgr = db_ER.Exec_DataSet(strQry);

                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_mail"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["sf_type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["sf_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["des_color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["Designation_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    dt.Rows.Add(dr);
                }

            }

            strQry = " Select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

            try
            {
                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "admin")
                    {
                        // Check if the Sf-Code has any Audit Manager.
                        DataSet dsAudit = CheckforAudit(dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                        if (dsAudit.Tables[0].Rows.Count > 0)
                        {
                            strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                                        " from Mas_Salesforce  a, Mas_SF_Designation b    Where a.Designation_Code=b.Designation_Code and" +
                                        " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                         " and a.sf_code = '" + dsAudit.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";


                            DataSet dsMgr2 = db_ER.Exec_DataSet(strQry);

                            if (dsMgr2.Tables[0].Rows.Count > 0)
                            {
                                dr = dt.NewRow();
                                order_id = order_id + 1;
                                dr["order_id"] = order_id;
                                dr["sf_Code"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                dr["sf_mail"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                dr["sf_Name"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                dr["Designation_Short_Name"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                dr["Reporting_To_SF"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                dr["Sf_HQ"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                dr["sf_type"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                dr["sf_color"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                dr["des_color"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                dr["Designation_Code"] = dsMgr2.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                dt.Rows.Add(dr);
                            }
                        }

                        strQry = " SELECT  a.Sf_Code,a.Sf_Code+'-Level1' as sf_mail,a.sf_name,b.Designation_Short_Name, a.Reporting_To_SF, a.Sf_HQ,a.sf_type,'-Level1' as sf_color, b.Desig_Color as des_color,b.Designation_Code" +
                                        " from Mas_Salesforce  a, Mas_SF_Designation b    Where a.Designation_Code=b.Designation_Code and" +
                                        " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                         " and a.sf_code = '" + dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";



                        DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsMgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            order_id = order_id + 1;
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_mail"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dr["sf_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Designation_Short_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Reporting_To_SF"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            dr["Sf_HQ"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            dr["sf_type"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                            dr["sf_color"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                            dr["des_color"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                            dr["Designation_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                            dt.Rows.Add(dr);

                            DJW = getMail_MRJointWork(div_code, dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        //Siva
        public DataSet UserList_Hierarchy_Managers(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Hierarchy_Managers_NoSpace '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet UserList_getMR_Multiple(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getMR_Multiple '" + divcode + "', '" + sf_code + "' ";

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

        // Added by Sridevi - BulkEdit TP DCR Date
        public bool IsDcrStarted(string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(trans_slno) FROM DCRMain_trans WHERE sf_code='" + sf_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
                else
                {
                    strQry = " SELECT COUNT(trans_slno) FROM DCRMain_temp WHERE sf_code='" + sf_code + "'";
                    int iRecordExist1 = db.Exec_Scalar(strQry);
                    if (iRecordExist1 > 0)
                        bRecordExist = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        // Added by Sridevi - BulkEdit TP Start Date
        public bool IsTpStarted(string sf_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " SELECT COUNT(Tour_Date) FROM Trans_Tp WHERE sf_code='" + sf_code + "'";
                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
                else
                {
                    strQry = " SELECT COUNT(Tour_Date) FROM Trans_Tp_One WHERE sf_code='" + sf_code + "'";
                    int iRecordExist1 = db.Exec_Scalar(strQry);
                    if (iRecordExist1 > 0)
                        bRecordExist = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        // done by reshmi for notification
        public int AddNotification(string div_code, string SF_Code, string SF_Name, string Notification_Message)
        {
            int iReturn = -1;
            int Trans_Sl_No = -1;

            try
            {
                if (div_code.Contains(","))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }

                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT ISNULL(COUNT(Trans_Sl_No),0)+1 FROM Mas_Notification ";
                Trans_Sl_No = db.Exec_Scalar(strQry);

                strQry = "INSERT INTO Mas_Notification (Trans_Sl_No,Division_Code,SF_Code,SF_Name,Notification_Message,Notification_Sent_Date,Notification_From_Date,Notification_To_Date)" +
                         "values('" + Trans_Sl_No + "','" + div_code + "' , '" + SF_Code + "', '" + SF_Name + "' ,'" + Notification_Message + "' , getdate() , getdate() ,getdate())";


                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        // Added by sridevi - To Update Reporting Date
        public int Rep_StDateRecordUpdate(string sf_code, DateTime repdate)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);

                strQry = "Update Mas_Salesforce " +
                         " set Sf_TP_DCR_Active_Dt =  '" + repdate.Month.ToString() + "-" + repdate.Day + "-" + repdate.Year + "' , " +
                         "  Last_Dcr_Date =  '" + repdate.Month.ToString() + "-" + repdate.Day + "-" + repdate.Year + "' , " +
                         "  Last_Tp_Date =  '" + repdate.Month.ToString() + "-" + repdate.Day + "-" + repdate.Year + "' , " +
                          " LastUpdt_Date = getdate() " +
                         " WHERE Sf_Code= '" + sf_code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Insert into Mas_Salesforce_Backup select * from Mas_Salesforce Where SF_Code='" + sf_code + "'";
                iReturn_Backup = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet CheckSFNameVacant(string sf_code, int cmon, int cyr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_get_sf_name_between_dates '" + sf_code + "', " + cmon + ", " + cyr + " ";

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
        public DataSet getmode(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT 'Total Days in Month' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " SELECT 'Working Days (Excl/Holidays & Sundays )' as Doc_Cat_SName,'' as Doc_Cat_Code " +
                     " union all " +
                     " select 'Fieldwork Days' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select  'Leave' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select 'TP Deviation Days' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select 'No of Listed Drs Met' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select 'No of Listed Drs Seen' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select 'Call Average' as Doc_Cat_SName,'' as Doc_Cat_Code union all " +
                     " select  Doc_Cat_SName +' Drs Met', Doc_Cat_Code as Doc_Cat_Code " +
                     " from Mas_Doctor_Category where Division_Code = '" + div_code + "' and Doc_Cat_Active_Flag = 0";


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

        public DataSet getProduct_Exp_count(string sf_code, string div_code, int cmonth, int cyear, int Prod, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = "EXEC sp_Get_LstDr_Prd_Count '" + div_code + "', '" + sf_code + "','" + cmonth + "' ,'" + cyear + "' , '" + cdate + "' ,'" + Prod + "'";




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

        public DataSet getProduct_Exp(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT Product_Detail_Code,Product_Detail_Name  " +
                     " FROM Mas_Product_Detail " +
                     " where Division_Code = '" + Division_Code + "' and Product_Active_Flag=0 ";

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
        public DataSet UserList_Self_Call(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Self_Call '" + divcode + "', '" + sf_code + "' ";

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

        // ---- Changes done by saravanan --------
        public DataSet UserListVacant_getMR(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserListVacant_getMR '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getProduct_Exp_Speciality(string sf_code, string div_code, int cmonth, int cyear, int Prod, string cdate, int Speciality)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_LstDr_Prd_SpeCat '" + div_code + "', '" + sf_code + "','" + cmonth + "' ,'" + cyear + "' , '" + cdate + "' ,'" + Prod + "','" + Speciality + "'";

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
        public DataSet getProduct_Exp_Category(string sf_code, string div_code, int cmonth, int cyear, int Prod, string cdate, int Category)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_LstDr_Prd_Category '" + div_code + "', '" + sf_code + "','" + cmonth + "' ,'" + cyear + "' , '" + cdate + "' ,'" + Prod + "','" + Category + "'";




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

        public DataSet SalesForceListMgrGet_MgrOnly(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_GetMgrTeam_MgrOnly '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet getSfName_Mr(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " SELECT distinct Sf_Name + ' - '+ sf_Designation_Short_Name + ' - ' + Sf_HQ as Sf_Name,sf_code FROM  Mas_Salesforce " +
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

        public DataSet sp_UserListMr_Doc_List_Vacant(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_Rep_audit_with_Vacant '" + sf_code + "', '" + divcode + "' ";

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

        public DataTable getAuditManagerTeam_GetMR_Sfname_With_Vacant(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dt_recursive = new DataTable();
            DataSet dsmgr = null;
            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_username", typeof(string)));
            }
            DataSet dsAtm = getAuditTeam(sf_code, div_code);
            if (dsAtm.Tables[0].Rows.Count > 0)
            {
                string AuditMgr = dsAtm.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                string[] Audit;
                if (AuditMgr.Length > 0)
                {
                    Audit = AuditMgr.Split(',');
                    foreach (string Au_cd in Audit)
                    {
                        if (Au_cd.Length > 0)
                        {
                            strQry = " SELECT a.sf_Code, a.sf_Name, a.sf_Type, " +
                                " b.Designation_Short_Name, b.Desig_Color , a.sf_Designation_Short_Name, a.Sf_HQ, a.sf_username" +
                                " FROM mas_salesforce a, Mas_SF_Designation b " +
                                " WHERE a.SF_Status!=2 and  " +
                                " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                " and a.sf_code = '" + Au_cd + "' and a.Designation_Code=b.Designation_Code ORDER BY a.sf_type";
                            try
                            {
                                dsmgr = db_ER.Exec_DataSet(strQry);
                                if (dsmgr.Tables[0].Rows.Count > 0)
                                {
                                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString() == "1")
                                    {
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["sf_Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_username"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        string mgrcode = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();

                                        strQry = "EXEC [sp_get_Rep_audit_sfName_With_Vacant]  '" + mgrcode + "' , '" + div_code + "'";

                                        dsSF = db_ER.Exec_DataSet(strQry);

                                        if (dsSF != null)
                                        {
                                            if (dsSF.Tables[0].Rows.Count > 0)
                                            {
                                                foreach (DataRow dataRow in dsSF.Tables[0].Rows)
                                                {
                                                    dr = dt.NewRow();
                                                    order_id = order_id + 1;

                                                    dr["sf_Code"] = dataRow["sf_Code"].ToString();
                                                    dr["sf_Name"] = dataRow["sf_Name"].ToString();
                                                    dr["sf_Type"] = dataRow["sf_Type"].ToString();
                                                    dr["Designation_Short_Name"] = dataRow["Designation_Short_Name"].ToString();
                                                    dr["Desig_Color"] = "";
                                                    dr["sf_Designation_Short_Name"] = dataRow["sf_Designation_Short_Name"].ToString();
                                                    dr["Sf_HQ"] = dataRow["Sf_HQ"].ToString();
                                                    dr["sf_username"] = dataRow["sf_username"].ToString();
                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            order_id = order_id + 1;
                        }
                    }
                }
            }
            return dt;

        }

        public DataSet sp_UserMRLogin_With_Vacant(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC [sp_UserMRLogin_With_Vacant] '" + divcode + "', '" + sf_code + "' ";

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

        public DataTable getMRJointWork_camp(string div_code, string sf_code, int order_id)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable DJW = new DataTable();

            if (order_id == 0)
            {
                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Designation_Short_Name", typeof(string)));

                strQry = " Select Sf_Code,'SELF' Sf_Name, 'ZZZZ' Reporting_To_SF, sf_Designation_Short_Name from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

                DataSet ds = db_ER.Exec_DataSet(strQry);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["Reporting_To_SF"] = ds.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["sf_Designation_Short_Name"] = ds.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dt.Rows.Add(dr);
                }
            }
            DataSet dsmgr = null;

            strQry = " Select Reporting_To_SF from Mas_Salesforce where Sf_Code = '" + sf_code + "' ";

            try
            {
                dsmgr = db_ER.Exec_DataSet(strQry);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    if (dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "admin")
                    {
                        strQry = " select Sf_Code,Sf_Name + '-'+ sf_Designation_Short_Name + '-' + Sf_HQ as Sf_Name,Reporting_To_SF,sf_Designation_Short_Name  from Mas_Salesforce " + // SM Level
                                 " WHERE SF_Status=0 and sf_Tp_Active_flag = 0 and  " +
                                 " (Division_Code like  + '" + div_code + ",'+'%' or Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                                 " and sf_code = '" + dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() + "'";

                        DataSet dsMgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsMgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            order_id = order_id + 1;
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["sf_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dr["Reporting_To_SF"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Designation_Short_Name"] = dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dt.Rows.Add(dr);

                            DJW = getMRJointWork_camp(div_code, dsMgr1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), order_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataSet getProduct_Sample_count(string sf_code, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_Product_Count '" + div_code + "', '" + sf_code + "','" + cmonth + "' ,'" + cyear + "' , '" + cdate + "' ";

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

        public DataSet getSalesForceAlpha(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_BlkReason,sf_Designation_Short_Name " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code" +
                     " WHERE SF_Status=1 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 1 AND a.sf_Tp_Active_flag = 0 " +
                     " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
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

        public DataSet getSalesForcelist_Alphabet_ForBlock(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=1  " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 0 " +
                     " ORDER BY 1";
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
        public DataSet getSalesForce_desBlock(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_BlkReason,sf_Designation_Short_Name " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code" +
                     " WHERE SF_Status=1 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%' or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 1 AND a.sf_Tp_Active_flag = 0  and a.Designation_Code = '" + Designation_Code + "'" +
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

        public DataSet FindSalesForceBlock(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,a.State_Code as state_code," +
                     "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_BlkReason,sf_Designation_Short_Name " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code" +
                     " WHERE SF_Status=1 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.SF_Status = 1 AND a.sf_Tp_Active_flag = 0 " +
                     sFindQry +
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

        public DataSet getSalesForce_desVacant(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code,sf_Designation_Short_Name ," +
                   "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                   " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                   " WHERE a.SF_Status=0 " +
                   " AND lower(a.sf_code) != 'admin' " +
                   " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                   " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                   " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 and a.Designation_Code = '" + Designation_Code + "' " +
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

        public DataSet FindSalesForceVacant(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code,sf_Designation_Short_Name ," +
                   "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                   " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                   " WHERE a.SF_Status=0 " +
                   " AND lower(a.sf_code) != 'admin' " +
                   " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 " +
                    sFindQry +
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

        public DataSet getSalesForceAlpha_vacant(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code,sf_Designation_Short_Name ," +
                    "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                    " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                    " WHERE a.SF_Status=0 " +
                    " AND lower(a.sf_code) != 'admin' " +
                    " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                    " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                    " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 " +
                    " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
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
        public DataSet getProd(string state, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = " SELECT '' as Territory_code, '--Select--' as Territory_name " +
                  " UNION  select Territory_code,Territory_name from Mas_Territory t inner join " +
                  " Mas_Zone z on t.Zone_code=z.Zone_code inner join Mas_Area a on a.Area_code=z.Area_code where a.State_Code='" + state + "' and t.Territory_Active_Flag=0 and t.Div_Code='" + div_code + "'  " +
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

        public DataSet getSalesForcelist_Alphabet_ForVacant(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0  " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 1 " +
                     " ORDER BY 1";
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

        public DataSet getDr_Mapp_prd_count(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT (distinct a.Listeddr_Code) from  " +
                       " Map_LstDrs_Product a,Mas_ListedDr b " +
                       " where a.Division_Code='" + div_code + "' and a.Sf_Code='" + sf_code + "'  and b.ListedDr_Active_Flag=0  " +
                        " and b.ListedDrCode =a.Listeddr_Code ";


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

        public DataSet getDr_Dcr_prd_count(string sf_code, string div_code, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select COUNT ( distinct Trans_Detail_Info_Code) from DCRMain_Trans b, " +
                     " DCRDetail_Lst_Trans c ,Mas_ListedDr e,Mas_Product_Detail P,Map_LstDrs_Product m where " +
                     " c.Division_Code ='" + div_code + "' and b.Trans_SlNo = c.Trans_SlNo  and  " +
                     " charindex('#'+cast(Product_Detail_Code as varchar)+'~','#'+ c.Product_Code) > 0 " +
                     " and c.Trans_Detail_Info_Type=1  and c.Trans_Detail_Info_Code =m.Listeddr_Code " +
                     " and month(b.Activity_Date)='" + Month + "' and  YEAR(b.Activity_Date)= '" + Year + "' and  c.sf_code in " +
                     " ('" + sf_code + "') and b.sf_code in ('" + sf_code + "') ";

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

        public DataSet getDrprdMap_Status(string sf_code, string div_code, string ddlmode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (ddlmode == "1")
            {

                strQry = " select COUNT (distinct Listeddr_Code),(select COUNT(c.ListedDrCode) from Mas_ListedDr c " +
                        " where Division_Code='" + div_code + "' and Sf_Code ='" + sf_code + "' and c.ListedDr_Active_Flag=0) from " +
                        " Mas_ListedDr b,Map_LstDrs_Product a where a.Division_Code='" + div_code + "' " +
                        " and b.Sf_Code='" + sf_code + "' and a.Sf_Code=b.Sf_Code and a.Listeddr_Code =b.ListedDrCode and b.ListedDr_Active_Flag =0 ";
            }
            else if (ddlmode == "2")
            {

                strQry = " select COUNT (distinct ListedDrCode) ,(select COUNT(c.ListedDrCode) from Mas_ListedDr c " +
                        " where Division_Code='" + div_code + "' and Sf_Code ='" + sf_code + "' and c.ListedDr_Active_Flag=0) " +
                       " from Mas_ListedDr a,Mas_Doc_SubCategory b where Sf_Code='" + sf_code + "' and a.Division_Code='" + div_code + "' and " +
                       "  charindex(cast(b.Doc_SubCatCode as varchar),a.Doc_SubCatCode ) >0 and a.ListedDr_Active_Flag=0 ";
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


        public DataSet getSalesForce_count(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            div_code = div_code + ',';
            strQry = "select COUNT(Sf_Code) cnt from Mas_Salesforce where Division_Code='" + div_code + "' and SF_Status != 2 ";
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

        public DataSet getSubDiv_Selected(string divcode, string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = " SELECT subdivision_code FROM  Mas_Salesforce " +
                   " WHERE SF_Code='" + SF_Code + "' AND (Division_Code like '" + divcode + ',' + "%'  or " +
                    " Division_Code like '%" + ',' + divcode + ',' + "%' )";
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

        public DataTable getUserListReportingToNew_for_all(string div_code, string sf_code, int order_id, string sf_type)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;

                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Type", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Joining_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To_SF", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_hq", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_password", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Short_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Desig_Color", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_Tp_Active_flag", typeof(string)));
                dt.Columns.Add(new DataColumn("UsrDfd_UserName", typeof(string)));
                dt.Columns.Add(new DataColumn("Lst_drCount", typeof(string)));
                dt.Columns.Add(new DataColumn("StateName", typeof(string)));
                dt.Columns.Add(new DataColumn("Last_TP_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_TP_DCR_Active_Dt", typeof(string)));
                dt.Columns.Add(new DataColumn("Last_DCR_Date", typeof(string)));
                dt.Columns.Add(new DataColumn("sf_emp_id", typeof(string)));
                dt.Columns.Add(new DataColumn("type", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Name", typeof(string)));

                strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
           " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
           " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
           " when '0' then 'Active'  " +
           " when '1' then 'Vacant'  " +
           " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,convert(varchar,a.Last_TP_Date,103) Last_TP_Date,convert(varchar,a.Sf_TP_DCR_Active_Dt,103) Sf_TP_DCR_Active_Dt, " +
           " convert(varchar,a.Last_DCR_Date,103) Last_DCR_Date,a.sf_emp_id ,a.sf_Type as type ,(select UsrDfd_UserName from Mas_Salesforce " +
           " where sf_code=a.sf_code) +'- '+   (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To,b.Designation_Short_Name as Designation_Name " +
           " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
           " WHERE a.SF_Status=0 and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag != 1) and  " +
           " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
           " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);

                    dr = dt.NewRow();
                    dr["order_id"] = order_id;
                    dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                    dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                    dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                    dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    dr["Last_TP_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    dr["Sf_TP_DCR_Active_Dt"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    dr["Last_DCR_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                    dr["sf_emp_id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                    dr["type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                    dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                    dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                    dt.Rows.Add(dr);

                    if (sf_type == "3")
                    {
                        strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                  " FROM mas_ho_id_creation " +
                                   " WHERE HO_Active_flag = 0  and  " +
                                   "(Division_Code like '" + div_code + "%'  or " +
                                    "Division_Code like '%" + ',' + div_code + "%') and " +
                                   " (Sub_HO_ID is null or Sub_HO_ID = '0')";

                        DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                        if (dsmgr1.Tables[0].Rows.Count > 0)
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = "admin";
                            dr["sf_Name"] = "admin";
                            dr["Sf_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["sf_Type"] = "";
                            dr["Sf_Joining_Date"] = "";
                            dr["Reporting_To_SF"] = "";
                            dr["sf_hq"] = "";
                            dr["sf_password"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Designation_Short_Name"] = "Admin";
                            dr["Desig_Color"] = "33ff96";
                            dr["sf_Tp_Active_flag"] = "";
                            dr["UsrDfd_UserName"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Lst_drCount"] = "";
                            dr["StateName"] = "";
                            dr["Last_TP_Date"] = "";
                            dr["Sf_TP_DCR_Active_Dt"] = "";
                            dr["Last_DCR_Date"] = "";
                            dr["sf_emp_id"] = "";
                            dr["type"] = "";
                            dr["Reporting_To"] = "";
                            dr["Designation_Name"] = "";
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        dr["sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        dr["Sf_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["sf_Type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["Sf_Joining_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Reporting_To_SF"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["sf_hq"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["sf_password"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["Designation_Short_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Desig_Color"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["sf_Tp_Active_flag"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["UsrDfd_UserName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dr["Lst_drCount"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        dr["Last_TP_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                        dr["Sf_TP_DCR_Active_Dt"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                        dr["Last_DCR_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                        dr["sf_emp_id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                        dr["type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                        dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                        dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                        dt.Rows.Add(dr);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = " SELECT a.sf_Code, a.sf_Name, a.Sf_UserName, a.sf_Type,convert(char(10),Sf_Joining_Date,105) Sf_Joining_Date, " +
                     " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password, " +
                     " b.Designation_Short_Name, b.Desig_Color, CASE a.sf_Tp_Active_flag   " +
                     " when '0' then 'Active'  " +
                     " when '1' then 'Vacant'  " +
                     " end sf_Tp_Active_flag, a.UsrDfd_UserName, '' Lst_drCount, c.StateName,convert(varchar,a.Last_TP_Date,103) Last_TP_Date,convert(varchar,a.Sf_TP_DCR_Active_Dt,103) Sf_TP_DCR_Active_Dt, " +
                     " convert(varchar,a.Last_DCR_Date,103) Last_DCR_Date,a.sf_emp_id ,a.sf_Type as type ,(select UsrDfd_UserName from Mas_Salesforce " +
                     " where sf_code=a.sf_code) +'- '+   (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To,b.Designation_Short_Name as Designation_Name " +
                     " FROM mas_salesforce a, Mas_SF_Designation b, mas_state c " +
                     " WHERE a.SF_Status=0 and (a.sf_Tp_Active_flag = 0 or a.sf_Tp_Active_flag != 1) and  " +
                     " (a.Division_Code like  + '" + div_code + ",'+'%' or a.Division_Code like '%'+','+ '" + div_code + ",'+'%') " +
                     " and a.Reporting_To_SF = '" + sf_code + "' and a.Designation_Code=b.Designation_Code and a.state_code = c.state_code ORDER BY a.sf_type";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        dr["Last_TP_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                        dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                        dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                        dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                        dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                                        dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                                        dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }

                                            dsA = getAuditMgr(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                dr["Last_TP_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                                dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                                dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["sf_Type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Sf_Joining_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Reporting_To_SF"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["sf_hq"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["sf_password"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["Designation_Short_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Desig_Color"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["sf_Tp_Active_flag"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["UsrDfd_UserName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["Lst_drCount"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        dr["Last_TP_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                        dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                        dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                        dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                        dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                        dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                        dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["sf_Code"] = drFF["sf_code"].ToString();
                            dr["sf_Name"] = drFF["sf_Name"].ToString();
                            dr["Sf_UserName"] = drFF["Sf_UserName"].ToString();
                            dr["sf_Type"] = drFF["sf_Type"].ToString();
                            dr["Sf_Joining_Date"] = drFF["Sf_Joining_Date"].ToString();
                            dr["Reporting_To_SF"] = drFF["Reporting_To_SF"].ToString();
                            dr["sf_hq"] = drFF["sf_hq"].ToString();
                            dr["sf_password"] = drFF["sf_password"].ToString();
                            dr["Designation_Short_Name"] = drFF["Designation_Short_Name"].ToString();
                            dr["Desig_Color"] = drFF["Desig_Color"].ToString();
                            dr["sf_Tp_Active_flag"] = drFF["sf_Tp_Active_flag"].ToString();
                            dr["UsrDfd_UserName"] = drFF["UsrDfd_UserName"].ToString();
                            dr["Lst_drCount"] = drFF["Lst_drCount"].ToString();
                            dr["StateName"] = drFF["StateName"].ToString();
                            dr["Last_TP_Date"] = drFF["Last_TP_Date"].ToString();
                            dr["Sf_TP_DCR_Active_Dt"] = drFF["Sf_TP_DCR_Active_Dt"].ToString();
                            dr["Last_DCR_Date"] = drFF["Last_DCR_Date"].ToString();
                            dr["sf_emp_id"] = drFF["sf_emp_id"].ToString();
                            dr["type"] = drFF["type"].ToString();
                            dr["Reporting_To"] = drFF["Reporting_To"].ToString();
                            dr["Designation_Name"] = drFF["Designation_Name"].ToString();
                            dt.Rows.Add(dr);
                        }

                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToNew_for_all(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataSet sp_UserListMr_Doc_List_View(string sf_code, string divcode, string rdoType)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (rdoType == "0")
            {
                strQry = "EXEC sp_DCR_View '" + sf_code + "', '" + divcode + "' ";
            }
            else if (rdoType == "1")
            {
                strQry = "EXEC sp_Doc_Spec '" + sf_code + "', '" + divcode + "' ";
            }
            else if (rdoType == "2")
            {
                strQry = "EXEC sp_Doc_Class '" + sf_code + "', '" + divcode + "' ";
            }
            else if (rdoType == "3")
            {
                strQry = "EXEC sp_DCR_Quali_Get '" + sf_code + "', '" + divcode + "' ";
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

        public DataSet getprd_Dr_count(string div_code, string sf_code, int Prod_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_Get_MappProduct_Doctor_Count '" + div_code + "', '" + sf_code + "','" + Prod_code + "' ";

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

        public DataSet getVacantManagers(string div_code, string type, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;


            strQry = "SELECT '' as Sf_Code, '---Select---' as sf_name " +
                   " UNION " +
                   "select Sf_Code,sf_name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ from Mas_Salesforce where  (Division_Code like '" + div_code + ',' + "%'  or " +
                   " Division_Code like '%" + ',' + div_code + ',' + "%')  AND sf_type = '" + type + "' and sf_TP_Active_Flag =1 and SF_Status = 0 and Sf_Code !='" + sf_code + "'";


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

        public DataSet getVac_info(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "SELECT case when SF_DOB='1900-01-01' then '' else convert(varchar,sf_Dob,103) end as SF_DOB,case when SF_DOW='1900-01-01' then '' else convert(varchar,sf_Dow,103) end as SF_DOW, " +
                    " SF_Mobile ,SF_Email ,SF_ContactAdd_One,SF_ContactAdd_Two,SF_City_Pincode,SF_Per_ContactAdd_One,SF_Per_ContactAdd_Two,SF_Per_City_Pincode,SF_Per_Contact_No,Sf_Password,Division_Code,Designation_Code,State_Code,Sf_HQ,Reporting_To_SF,subdivision_code " +
                     " FROM mas_salesforce " +
                    " WHERE Sf_Code= '" + sf_code + "' ";

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

        public int RecordUdpate_forUsdefd(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                DataSet DsSal = null;

                strQry = "Select UsrDfd_UserName,sf_emp_id,Sf_Name from Mas_Salesforce where Division_Code='" + div_code + ",' and sf_code='" + sf_code + "'";

                DsSal = db.Exec_DataSet(strQry);

                if (DsSal.Tables[0].Rows.Count > 0)
                {
                    strQry = "UPDATE Mas_Salesforce " +
                             " SET UsrDfd_UserName = '" + DsSal.Tables[0].Rows[0][0].ToString() + "T' , " +
                             " sf_emp_id ='" + DsSal.Tables[0].Rows[0][1].ToString() + "T', " +
                             " Sf_Name ='" + DsSal.Tables[0].Rows[0][2].ToString() + " - Transferred', " +
                             " LastUpdt_Date = getdate() " +
                             " WHERE sf_code = '" + sf_code + "' and Division_Code='" + div_code + ",' ";

                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public int Transfer_RecordInsert(string sfname, string Vac_sfcode, string sfcode, int from_designation, int To_designation, string from_division, string to_division)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();
                strQry = "SELECT isnull(max(S_No)+1,'1') S_No from Salesforce_Transfer_Promote ";
                int S_No = db.Exec_Scalar(strQry);

                if (from_division.Contains(","))
                {
                    from_division = from_division.Remove(from_division.Length - 1);
                }

                strQry = "INSERT INTO Salesforce_Transfer_Promote(S_No,Sf_Name,From_sf_code,To_sf_code,From_Desigantion,To_Designation,From_Division_Code,To_Division_Code,Transfered_Date) " +
                         " VALUES ( '" + S_No + "','" + sfname + "' ,'" + Vac_sfcode + "','" + sfcode + "', '" + from_designation + "', '" + To_designation + "', '" + from_division + "', '" + to_division + "', " +
                         " getdate())";




                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getSalesForce_st_vacant(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, b.StateName,a.State_Code as state_code,sf_Designation_Short_Name ," +
                 "(select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                 " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code " +
                 " WHERE a.SF_Status=0 " +
                 " AND lower(a.sf_code) != 'admin' " +
                 " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                 " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                 " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 1 and a.State_Code = '" + state_code + "' " +
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

        public DataSet UserList_getAll_Multiple(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getALL_Multiple '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getDCR_Delayed_Dates(string sf_code, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select top(1) STUFF((Select ','+ cast(day(a.Delayed_Date) as varchar)  from DCR_Delay_Dtls a " +
                    " where a.Sf_Code = '" + sf_code + "' and MONTH(a.Delayed_Date) = '" + cmonth + "' and YEAR(a.Delayed_Date) = '" + cyear + "' and a.Division_Code='" + div_code + "' " +
                    " FOR XML PATH('')),1,1,'') Delayed_Date from DCR_Delay_Dtls T2 where t2.Sf_Code = '" + sf_code + "' and " +
                    " MONTH(T2.Delayed_Date) = '" + cmonth + "' and YEAR(t2.Delayed_Date) = '" + cyear + "' and t2.Division_Code='" + div_code + "' ";

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

        public DataSet getDCR_Leave_Count(string sf_code, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select COUNT(distinct Activity_Date) from DCRMain_Trans where Sf_Code='" + sf_code + "' and " +
                      " FieldWork_Indicator='L' and MONTH(activity_date)='" + cmonth + "' " +
                      " and YEAR(activity_date)='" + cyear + "' and Division_Code='" + div_code + "' ";

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

        public DataSet getDCR_Leave_Count_ToolTip(string sf_code, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select top(1) STUFF((Select ','+ cast(day(a.Activity_Date) as varchar)  from DCRMain_Trans a " +
                     " where a.Sf_Code = '" + sf_code + "' and MONTH(a.Activity_Date) = '" + cmonth + "' " +
                     " and YEAR(a.Activity_Date) = '" + cyear + "' and a.Division_Code='" + div_code + "' and a.FieldWork_Indicator ='L' " +
                     " FOR XML PATH('')),1,1,'') Delayed_Date from DCRMain_Trans T2 where t2.Sf_Code = '" + sf_code + "' and " +
                     " MONTH(T2.Activity_Date) = '" + cmonth + "' and YEAR(t2.Activity_Date) = '" + cyear + "' and t2.Division_Code='" + div_code + "' " +
                     " and T2.FieldWork_Indicator ='L' ";

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
        public DataSet SalesForceList_camp(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_SalesForceGet_MR '" + divcode + "', '" + sf_code + "' ";

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
        public DataSet UserList_getMR_Multiple_camp(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_getMR_Multiple '" + divcode + "', " + sf_code + " ";

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
        public DataSet get_sf_code(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;


            strQry = "select sf_code from mas_salesforce where sf_code in (" + sf_code + ") ";
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

        public DataSet sp_UserListMr_FieldForce(string sf_code, string divcode, string iMonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            // strQry = "EXEC sp_get_Rep_audit_with_Vacant '" + sf_code + "', '" + divcode + "' ";

            strQry = "EXEC sp_Get_DCRCount_View '" + sf_code + "', '" + divcode + ',' + "','" + iMonth + "','" + iYear + "' ";

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

        public DataSet sp_Get_DCRCount_Current_Pending_date_Count(string sf_code, string divcode, string iMonth, string iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "EXEC sp_Get_DCRCount_Current_Pending_date_Count '" + sf_code + "', '" + divcode + ',' + "','" + iMonth + "','" + iYear + "' ";

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

        public DataSet sp_FieldForce(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_FieldForce_Status '" + sf_code + "', '" + divcode + "' ";

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

        public DataSet sp_UserList_Hierarchy_Vacant(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_UserList_Hierarchy_Vacant '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet getDCR_Leave_Count_Type(string sf_code, string div_code, int cmonth, int cyear, int Leave_type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select count(distinct a.Activity_Date) Leave_Date From DCRMain_Trans a,mas_Leave_Form b " +
                    " where a.FieldWork_Indicator='L' and a.sf_code='" + sf_code + "' " +
                    " and a.Activity_Date between b.From_Date and b.To_Date and a.Sf_Code=b.sf_code " +
                    " and a.Division_Code=b.Division_Code  " +
                    " and MONTH(a.Activity_Date)='" + cmonth + "' and YEAR(a.activity_date)='" + cyear + "' and a.Division_Code='" + div_code + "' and b.Leave_Type='" + Leave_type + "' and b.Leave_Active_Flag=0 ";


            //strQry = "select (select count(distinct a.Activity_Date) Leave_Date From DCRMain_Trans a,mas_Leave_Form b " +
            //        " where a.FieldWork_Indicator='L' and a.sf_code='" + sf_code + "' " +
            //        " and a.Activity_Date between b.From_Date and b.To_Date and a.Sf_Code=b.sf_code " +
            //        " and a.Division_Code=b.Division_Code  " +
            //        " and MONTH(a.Activity_Date)='" + cmonth + "' and YEAR(a.activity_date)='" + cyear + "' and a.Division_Code='" + div_code + "' and b.Leave_Type='" + Leave_type + "') , " +
            //        " (select count(a.Activity_Date) From DCRMain_Trans a " +
            //        " where a.FieldWork_Indicator='L' and a.sf_code='"+sf_code+"' " +
            //        " and month(a.Activity_Date)='"+cmonth+"' and Entry_Mode='Apps') ";

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

        public DataSet getDCR_Leave_Count_ToolTip_Type(string sf_code, string div_code, int cmonth, int cyear, int Leave_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            strQry = " select top (1) STUFF((Select distinct ','+ cast(day(a.Activity_Date) as varchar)  from DCRMain_Trans a,mas_Leave_Form b " +
                     " where a.Sf_Code = '" + sf_code + "' and MONTH(a.Activity_Date) = '" + cmonth + "' " +
                     " and  a.Activity_Date between b.From_Date and b.To_Date and a.Sf_Code=b.sf_code and a.Division_Code=b.Division_Code " +
                     " and YEAR(a.Activity_Date) = '" + cyear + "' and a.Division_Code='" + div_code + "' and b.Leave_Type='" + Leave_Type + "' and b.Leave_Active_Flag=0 and a.FieldWork_Indicator ='L' " +
                     " FOR XML PATH('')),1,1,'') Leave_Date from DCRMain_Trans T2,mas_Leave_Form T3 where t2.Sf_Code = '" + sf_code + "' and " +
                     " T2.Activity_Date between T3.From_Date and T3.To_Date and T2.Sf_Code=T3.sf_code and T2.Division_Code=T3.Division_Code and " +
                     " MONTH(T2.Activity_Date) = '" + cmonth + "' and YEAR(t2.Activity_Date) = '" + cyear + "' and t2.Division_Code='" + div_code + "' and T3.Leave_Type='" + Leave_Type + "' and T3.Leave_Active_Flag=0 " +
                     " and T2.FieldWork_Indicator ='L' ";

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
        public DataSet getSalesForcelist_Alphabet_List_Promo(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' sf_name " +
                     " union " +
                     " select distinct LEFT(sf_name,1) val, LEFT(sf_name,1) sf_name" +
                     " FROM mas_salesforce " +
                     " WHERE SF_Status=0  " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (Division_Code like '" + divcode + ',' + "%'  or " +
                      " Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND sf_Tp_Active_flag = 0 and sf_type = 1 " +
                     " ORDER BY 1";
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
        public DataSet getSalesForcelist_Pro(string divcode, string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, " +
                      " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     "  (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "  Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a join mas_state b on a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 and sf_type = 1 " +
                     " AND LEFT(a.sf_name,1) = '" + sAlpha + "' " +
                     " ORDER BY Sf_Name";
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

        public DataSet getSalesForce_st_promo(string divcode, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = " SELECT SF_Code, Sf_Name,Sf_UserName, UsrDfd_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     " Sf_Password,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date, Sf_HQ,sf_type as Type , case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,b.StateName,a.State_Code as state_code,c.Designation_Short_Name as Designation_Name,c.Designation_Short_Name" +
                     " FROM mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                     " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code  WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
                     " AND a.sf_Tp_Active_flag = 0 and a.sf_type = 1  AND a.State_Code = '" + state_code + "' AND (a.Division_Code like '" + divcode + ',' + "%'  or " +
                     " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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

        public DataSet getSalesForce_des_promo(string divcode, string Designation_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


            strQry = " SELECT SF_Code, Sf_Name, UsrDfd_UserName,Sf_UserName, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     " Sf_Password, Sf_HQ,sf_type as Type, case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,sf_emp_id,convert(varchar,Sf_Joining_Date,103)Sf_Joining_Date,b.StateName,c.Designation_Short_Name as Designation_Name,a.Designation_Code as Designation_Code,c.Designation_Short_Name" +
                     " FROM  mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                     " where lower(sf_code) != 'admin'  AND a.SF_Status = 0 " +
                     " AND a.sf_Tp_Active_flag = 0 and a.sf_type= 1  AND a.Designation_Code = '" + Designation_Code + "' AND (a.Division_Code like '" + divcode + ',' + "%' " +
                     " or a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
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

        public DataSet FindSalesForcelist_Promo(string sFindQry)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "SELECT SF_Code, Sf_Name, Sf_UserName, Sf_HQ, " +
                     " (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     " (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "sf_type as Type , case when sf_Type = 1 THEN 'Medical Rep' ELSE 'Manager' END as sf_Type,b.StateName,c.Designation_Short_Name as Designation_Name,a.State_Code as state_code " +
                     " FROM mas_salesforce a, mas_state b,Mas_SF_Designation c " +
                     " WHERE SF_Status=0 " +
                     " AND lower(sf_code) != 'admin' " +
                     " AND a.State_Code=b.State_Code " +
                     " AND a.Designation_Code = c.Designation_Code" +
                     " AND a.SF_Status = 0 AND a.sf_Tp_Active_flag = 0 and a.sf_type = 1 " +
                     sFindQry +
                     " ORDER BY Sf_Name";

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

        public DataSet getDesignation_SN_prom(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " select ''Designation_Code,'---Select---'  Designation_Name" +
                     " union" +
                     " SELECT Designation_Code,Designation_Short_Name AS Designation_Name " +
                     " FROM Mas_SF_Designation where Division_Code = '" + div_code + "' and type = 1 " +
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

        public DataSet getOnlyBaselevel(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "  SELECT a.SF_Code, a.Sf_Name, a.Sf_UserName, " +
                     "  (select UsrDfd_UserName from Mas_Salesforce where sf_code=a.sf_code) +'- '+ " +
                     "  (select Sf_Name from mas_salesforce where sf_code=a.Reporting_To_SF) Reporting_To ," +
                     "  a.sf_type as Type, a.Sf_HQ, case when a.sf_Type = 1  THEN 'Medical Rep' ELSE 'Manager' END as sf_Type," +
                      " b.StateName,c.Designation_Short_Name as Designation_Name, a.State_Code as state_code from mas_salesforce a join mas_state b  on  a.State_Code=b.State_Code " +
                      " join Mas_SF_Designation c on a.Designation_Code = c.Designation_Code " +
                      " WHERE (a.Division_Code like '" + divcode + ',' + "%' or  a.Division_Code like '%" + ',' + divcode + ',' + "%') and  " +
                      " SF_Status=0  AND lower(sf_code) != 'admin' AND a.sf_Tp_Active_flag = 0 and a.sf_type=1 " +
                      " ORDER BY Sf_Name";
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

        public int RecordUdpate_forBase_Manag(string sf_code, string div_code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                DataSet DsSal = null;

                strQry = "Select UsrDfd_UserName,sf_emp_id,Sf_Name from Mas_Salesforce where Division_Code='" + div_code + ",' and sf_code='" + sf_code + "'";

                DsSal = db.Exec_DataSet(strQry);

                if (DsSal.Tables[0].Rows.Count > 0)
                {
                    strQry = "UPDATE Mas_Salesforce " +
                             " SET UsrDfd_UserName = '" + DsSal.Tables[0].Rows[0][0].ToString() + "P' , " +
                             " sf_emp_id ='" + DsSal.Tables[0].Rows[0][1].ToString() + "P', " +
                             " Sf_Name ='" + DsSal.Tables[0].Rows[0][2].ToString() + " - Promoted', " +
                             " LastUpdt_Date = getdate() " +
                             " WHERE sf_code = '" + sf_code + "' and Division_Code='" + div_code + ",' ";

                    iReturn = db.ExecQry(strQry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet getVacantManagersonly(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;


            strQry = "SELECT '' as Sf_Code, '---Select---' as sf_name " +
                   " UNION " +
                   "select Sf_Code,sf_name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ from Mas_Salesforce where  (Division_Code like '" + div_code + ',' + "%'  or " +
                   " Division_Code like '%" + ',' + div_code + ',' + "%')  AND sf_type = 2 and sf_TP_Active_Flag =1 and SF_Status = 0 ";


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

        public DataTable getUserListReportingToNew_for_all_Approval_Changes(string div_code, string sf_code, int order_id, string sf_type)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            if (order_id == 0)
            {
                DataSet dsmgr = null;

                dt.Columns.Add(new DataColumn("order_id", typeof(int)));
                dt.Columns.Add(new DataColumn("Sf_Code", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Sf_HQ", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation_Name", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting_To", typeof(string)));
                dt.Columns.Add(new DataColumn("Reporting", typeof(string)));
                dt.Columns.Add(new DataColumn("DCR_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("TP_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("LstDr_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("Leave_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("SS_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("Expense_AM", typeof(string)));
                dt.Columns.Add(new DataColumn("Otr_AM", typeof(string)));

                strQry = "SELECT a.Sf_Code, a.Sf_Name,a.Sf_HQ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                    " WHERE  c.sf_TP_Active_Flag=0 and c.SF_Status=0 " +
                    " AND a.Reporting_To = '" + sf_code + "' " +
                    " and a.Division_Code = '" + div_code + "' " +
                    " ORDER BY 2";
                try
                {
                    dsmgr = db_ER.Exec_DataSet(strQry);
                    if (dsmgr.Tables[0].Rows.Count > 0)
                    {

                        dr = dt.NewRow();
                        dr["order_id"] = order_id;
                        dr["Sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                        dr["Sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                        dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                        dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                        dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                        dr["Reporting"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                        dr["DCR_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                        dr["TP_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                        dr["LstDr_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                        dr["Leave_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                        dr["SS_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                        dr["Expense_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                        dr["Otr_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                        //dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                        //dr["Last_TP_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                        //dr["Sf_TP_DCR_Active_Dt"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                        //dr["Last_DCR_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                        //dr["sf_emp_id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                        //dr["type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                        //dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                        //dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                        dt.Rows.Add(dr);

                        if (sf_type == "3")
                        {
                            strQry = "SELECT HO_ID,Name,User_Name,Password " +
                                      " FROM mas_ho_id_creation " +
                                       " WHERE HO_Active_flag = 0  and  " +
                                       "(Division_Code like '" + div_code + "%'  or " +
                                        "Division_Code like '%" + ',' + div_code + "%') and " +
                                       " (Sub_HO_ID is null or Sub_HO_ID = '0')";

                            DataSet dsmgr1 = db_ER.Exec_DataSet(strQry);
                            if (dsmgr1.Tables[0].Rows.Count > 0)
                            {
                                dr = dt.NewRow();
                                dr["order_id"] = order_id;
                                dr["Sf_Code"] = "admin";
                                dr["Sf_Name"] = "admin";
                                dr["Sf_HQ"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                dr["Designation_Name"] = "";
                                dr["Reporting_To"] = "";
                                dr["Reporting"] = "";
                                dr["DCR_AM"] = "";
                                dr["TP_AM"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                dr["LstDr_AM"] = "";
                                dr["Leave_AM"] = "";
                                dr["SS_AM"] = "";
                                dr["Expense_AM"] = dsmgr1.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                dr["Otr_AM"] = "";
                                //dr["StateName"] = "";
                                //dr["Last_TP_Date"] = "";
                                //dr["Sf_TP_DCR_Active_Dt"] = "";
                                //dr["Last_DCR_Date"] = "";
                                //dr["sf_emp_id"] = "";
                                //dr["type"] = "";
                                //dr["Reporting_To"] = "";
                                //dr["Designation_Name"] = "";
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["Sf_Code"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                            dr["Sf_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                            dr["Sf_HQ"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                            dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                            dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                            dr["Reporting"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                            dr["DCR_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                            dr["TP_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                            dr["LstDr_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                            dr["Leave_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                            dr["SS_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                            dr["Expense_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                            dr["Otr_AM"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                            //dr["StateName"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                            //dr["Last_TP_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                            //dr["Sf_TP_DCR_Active_Dt"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                            //dr["Last_DCR_Date"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                            //dr["sf_emp_id"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                            //dr["type"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                            //dr["Reporting_To"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                            //dr["Designation_Name"] = dsmgr.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                            dt.Rows.Add(dr);
                        }

                    }
                    else
                    {

                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

            }


            DataTable dt_recursive = new DataTable();
            DataSet dsDivision = null;
            DataSet dsA = null;

            strQry = "SELECT a.Sf_Code, a.Sf_Name,a.Sf_HQ,c.sf_Designation_Short_Name as Designation_Name," +
                    "(select Sf_Name  from mas_salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                    "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                    "(select Sf_HQ from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting_To, " +
                    "(select Sf_Code from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                    "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                    " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                    " WHERE  (c.sf_Tp_Active_flag = 0 or c.sf_Tp_Active_flag != 1) and c.SF_Status=0 " +
                    " AND a.Reporting_To = '" + sf_code + "' " +
                    " and a.Division_Code = '" + div_code + "' " +
                    " ORDER BY 2";
            try
            {
                dsDivision = db_ER.Exec_DataSet(strQry);

                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drFF in dsDivision.Tables[0].Rows)
                    {
                        DataSet ds = CheckforAudit(drFF["sf_code"].ToString(), div_code);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dsloop in ds.Tables[0].Rows)
                            {
                                int am = 0;
                                foreach (DataRow draud in dt.Rows)
                                {
                                    if (draud["sf_Code"].ToString() == dsloop["Sf_Code"].ToString())
                                    {
                                        am = 1;
                                    }
                                }
                                if (am == 0)
                                {
                                    DataSet ds1 = CheckforAudit(dsloop["Sf_Code"].ToString(), div_code);
                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        int am1 = 0;
                                        foreach (DataRow draud in dt.Rows)
                                        {
                                            if (draud["sf_Code"].ToString() == ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                            {
                                                am1 = 1;
                                            }
                                        }
                                        if (am1 == 0)
                                        {
                                            DataSet ds2 = CheckforAudit(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (ds2.Tables[0].Rows.Count > 0)
                                            {
                                                int am2 = 0;
                                                foreach (DataRow draud in dt.Rows)
                                                {
                                                    if (draud["sf_Code"].ToString() == ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString())
                                                    {
                                                        am2 = 1;
                                                    }
                                                }
                                                if (am2 == 0)
                                                {
                                                    dsA = getAuditMgr_Approval_Changes(ds2.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                                    if (dsA.Tables[0].Rows.Count > 0)
                                                    {
                                                        order_id = order_id + 1;
                                                        dr = dt.NewRow();
                                                        dr["order_id"] = order_id;
                                                        dr["Sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                        dr["Sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                        dr["Sf_HQ"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                        dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                        dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                        dr["Reporting"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                        dr["DCR_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                        dr["TP_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                        dr["LstDr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                        dr["Leave_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                        dr["SS_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                        dr["Expense_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                        dr["Otr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                        //dr["Expense_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                        //dr["Otr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                        //dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                        //dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                        //dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                        //dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                                        //dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                                        //dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                                        dt.Rows.Add(dr);
                                                    }
                                                }
                                            }

                                            dsA = getAuditMgr_Approval_Changes(ds1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString(), div_code);
                                            if (dsA.Tables[0].Rows.Count > 0)
                                            {
                                                order_id = order_id + 1;
                                                dr = dt.NewRow();
                                                dr["order_id"] = order_id;
                                                dr["Sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                                dr["Sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                                dr["Sf_HQ"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                                dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                                dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                                dr["Reporting"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                                dr["DCR_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                                dr["TP_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                                dr["LstDr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                                dr["Leave_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                                dr["SS_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                                dr["Expense_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                                dr["Otr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                                //dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                                //dr["Last_TP_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                                //dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                                //dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                                //dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                                //dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                                //dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                                //dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                                dt.Rows.Add(dr);
                                            }
                                        }
                                    }
                                    dsA = getAuditMgr_Approval_Changes(dsloop["Sf_Code"].ToString(), div_code);
                                    if (dsA.Tables[0].Rows.Count > 0)
                                    {
                                        order_id = order_id + 1;
                                        dr = dt.NewRow();
                                        dr["order_id"] = order_id;
                                        dr["Sf_Code"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        Audit_mgr = dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                                        dr["Sf_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                                        dr["Sf_HQ"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                                        dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                                        dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                                        dr["Reporting"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                                        dr["DCR_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                                        dr["TP_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                                        dr["LstDr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
                                        dr["Leave_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                                        dr["SS_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
                                        dr["Expense_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                                        dr["Otr_AM"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                                        //dr["StateName"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                                        //dr["Last_TP_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                                        //dr["Sf_TP_DCR_Active_Dt"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                                        //dr["Last_DCR_Date"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                                        //dr["sf_emp_id"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                                        //dr["type"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                                        //dr["Reporting_To"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
                                        //dr["Designation_Name"] = dsA.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        dsA = getAuditTeam(drFF["sf_code"].ToString(), div_code);
                        if ((dsA.Tables[0].Rows.Count == 0) || (dsA.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == ""))
                        {
                            order_id = order_id + 1;
                            dr = dt.NewRow();
                            dr["order_id"] = order_id;
                            dr["Sf_Code"] = drFF["Sf_Code"].ToString();
                            dr["Sf_Name"] = drFF["Sf_Name"].ToString();
                            dr["Sf_HQ"] = drFF["Sf_HQ"].ToString();
                            dr["Designation_Name"] = drFF["Designation_Name"].ToString();
                            dr["Reporting_To"] = drFF["Reporting_To"].ToString();
                            dr["Reporting"] = drFF["Reporting"].ToString();
                            dr["DCR_AM"] = drFF["DCR_AM"].ToString();
                            dr["TP_AM"] = drFF["TP_AM"].ToString();
                            dr["LstDr_AM"] = drFF["LstDr_AM"].ToString();
                            dr["Leave_AM"] = drFF["Leave_AM"].ToString();
                            dr["SS_AM"] = drFF["SS_AM"].ToString();
                            dr["Expense_AM"] = drFF["Expense_AM"].ToString();
                            dr["Otr_AM"] = drFF["Otr_AM"].ToString();
                            //dr["StateName"] = drFF["StateName"].ToString();
                            //dr["Last_TP_Date"] = drFF["Last_TP_Date"].ToString();
                            //dr["Sf_TP_DCR_Active_Dt"] = drFF["Sf_TP_DCR_Active_Dt"].ToString();
                            //dr["Last_DCR_Date"] = drFF["Last_DCR_Date"].ToString();
                            //dr["sf_emp_id"] = drFF["sf_emp_id"].ToString();
                            //dr["type"] = drFF["type"].ToString();
                            //dr["Reporting_To"] = drFF["Reporting_To"].ToString();
                            //dr["Designation_Name"] = drFF["Designation_Name"].ToString();
                            dt.Rows.Add(dr);
                        }

                        if (order_id == 0)
                            order_id = order_id + 1;
                        dt_recursive = getUserListReportingToNew_for_all_Approval_Changes(div_code, drFF["sf_code"].ToString(), order_id, dr["sf_Name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataSet getAuditMgr_Approval_Changes(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "SELECT a.Sf_Code, a.Sf_Name,a.Sf_HQ,c.sf_Designation_Short_Name as Designation_Name," +
                     "(select Sf_Name  from mas_salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                     "(select sf_Designation_Short_Name from Mas_Salesforce where sf_code=c.Reporting_To_SF)+'-'+ " +
                     "(select Sf_HQ from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting_To, " +
                     "(select Sf_Code from Mas_Salesforce where sf_code=c.Reporting_To_SF) Reporting, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.DCR_AM) as DCR_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.TP_AM) as TP_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.LstDr_AM) as LstDr_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.Leave_AM) as Leave_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.SS_AM) as SS_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.Expense_AM) as Expense_AM, " +
                     "(select Sf_Name from mas_salesforce where sf_code=a.Otr_AM) as Otr_AM " +
                     " FROM mas_salesforce_AM a join Mas_Salesforce c on a.Sf_Code = c.Sf_Code " +
                     " WHERE  c.sf_TP_Active_Flag=0 and c.SF_Status=0 " +
                     " AND a.Reporting_To = '" + sf_code + "' " +
                     " and a.Division_Code = '" + div_code + "' " +
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


        public DataSet UserList_getMR_Vacant(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = "EXEC sp_UserList_getMR '" + divcode + "', '" + sf_code + "' ";

            strQry = "SELECT a.sf_Code,Sf_Name +' - '+sf_Designation_Short_Name + ' - ' + sf_hq as sf_Name, a.Sf_UserName, a.sf_Type, " +
                        " (select sf_name from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF, a.sf_hq, a.sf_password " +
                        " FROM mas_salesforce a " +
                        " WHERE  a.sf_Tp_Active_flag = 1 and (a.Division_Code like '" + divcode + ',' + "%'  or " +
                        " a.Division_Code like '%" + ',' + divcode + ',' + "%') " +
                        " and a.sf_code !='admin' and a.sf_type=1 " +
                        " order by 2 ";
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
        public DataSet getStatePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Territory_code from Mas_Territory where div_code='" + div_code + "'";
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
        public DataSet getStateProd(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT 0 as state_code,'ALL' as statename,'' as shortname " +
            //         " UNION " +
            //         " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
            //         " ORDER BY 2";
            strQry = " SELECT '' as Territory_code, '--Select--' as Territory_name " +
                      " UNION " +
                      " select Territory_code,Territory_name from Mas_Territory where Div_Code = '" + div_code + "' order by 2";
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

        public DataSet GetBrandName_Customer(string divcode, string catcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Product_Brd_Code,Product_Brd_Name from  Mas_Product_Brand where  Product_Brd_Active_Flag=0  and Division_Code='" + divcode + "' and Product_Cat_Code='" + catcode + "' ";

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
        public DataSet GetcategoryName_Customer(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select  Product_Cat_Code,Product_Cat_Name from  Mas_Product_Category where Product_Cat_Active_Flag=0  and   Division_Code='" + divcode + "' ";

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
        public DataSet Purchase_Category_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";

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
        public DataSet catogory_wise_sale(string productcatname, string divcode, int cmonth, int cyear, string scurrentdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select sum((Rec_Qty )* (Distributer_Rate))" +
                     "from Mas_Product_Category a " +
                    " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                     "left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.Purchase_Date)='" + cmonth + "' and YEAR(x.Purchase_Date)='" + cyear + "'  " +
                    " group by a.Product_Cat_Name,MONTH(Purchase_Date),year(Purchase_Date) ";

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
        public DataSet Purchase_Brand_value(string brandcode, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select sum(Rec_Qty * Distributer_Rate)from Mas_Product_Brand a " +
                    "join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
                   " left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "'" +
                   "group by a.Product_Brd_Name,MONTH(Purchase_Date),year(Purchase_Date)";
            //"select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";

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

        public DataSet purchase_register_productdetail_Stockistwise(string divcode, string stockist, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Prod_Detail_Sl_No,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "'  and YEAR(Purchase_Date)='" + Year + "' and Stockist_code='" + stockist + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_Branddetail_stockistwise(string divcode, string brand, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "'  and YEAR(Purchase_Date)='" + Year + "' and P.Product_Brd_Code='" + brand + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_catogorydetail_stockistwise(string divcode, string brand, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "'  and YEAR(Purchase_Date)='" + Year + "' and P.Product_Cat_Code='" + brand + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_productdetail(string divcode, string stockist, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Prod_Detail_Sl_No,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "' and YEAR(Purchase_Date)='" + Year + "' and Stockist_code='" + stockist + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_brandwisedetail(string divcode, string Brand, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "' and YEAR(Purchase_Date)='" + Year + "' and P.Product_Brd_Code='" + Brand + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_categorywisedetail(string divcode, string Brand, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "'  and YEAR(Purchase_Date)='" + Year + "' and P.Product_Cat_Code='" + Brand + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Territorywise_purchase_value(string territorycode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = "select sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "'" +
               "AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "' ";
            }
            else
            {

                strQry = "select sum((Rec_Qty * Distributer_Rate)+ (Rec_Pieces* DP_BaseRate))as value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "'" +
               "AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "' ";
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
        public DataSet Zonewise_purchase_value(string zonecode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {
                strQry = "select sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and subdivision_code='" + subdivision + "' ";
            }
            else
            {
                strQry = "select sum ((Rec_Qty * Distributer_Rate)+ (Rec_Pieces*DP_BaseRate)) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
" ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and subdivision_code='" + subdivision + "' ";
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
        public DataSet zonewise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "'  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "'" +

          " group by Product_Code,Product_Name,Distributer_Rate   ";

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
        public DataSet zonewise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE   s.Division_Code='" + div_code + "'  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "'" +

          " group by Product_Code,Product_Name,Distributer_Rate   ";

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
        public DataSet Territorywise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string territorycode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "'" +
        " group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet Territorywise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "'" +
        " group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet Statewise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
   "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "' and u.Product_Code='" + product_code + "' and  DATEPART(WEEK, u.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,u.Purchase_Date), 0))+ 1 ='" + week + "'" +
"AND S.State_Code='" + state_code + "'and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";
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
        public DataSet Statewise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
    "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "' and u.Product_Code='" + product_code + "' and  DATEPART(WEEK, u.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,u.Purchase_Date), 0))+ 1 ='" + week + "'" +
 "and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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

        public DataSet Statewise_purchase_value(string statecode, string div_code, int cmonth, int cyear, string cdate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  " +
             "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "'" +
             "AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "'";
            }
            else
            {
                strQry = "select sum((Rec_Qty * Distributer_Rate)+ (Rec_Pieces * DP_BaseRate)) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  " +
           "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "'" +
           "AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "' and subdivision_code='" + subdivision + "'";
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





        public DataSet GetStockName_Customer(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0  order by Stockist_Name asc";

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
        public DataSet GetStockName_Cus(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Division_Code='" + divcode + "'";

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


        public DataSet Purchase_Distributor_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = "select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";

            }
            else
            {
                strQry = "select SUM(((Rec_Qty) * (Distributer_Rate)) +((Rec_Pieces)*(DP_BaseRate)))   from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";
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

        public DataSet purchase_register_categorywisedetail_stockist(string divcode, string cat_code, int Year, int Month, string cdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "'  and YEAR(Purchase_Date)='" + Year + "'   and s.Stockist_code='" + stockistcode + "' and B.Product_Cat_Code='" + cat_code + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet catogory_wise_sale_value_stockist(string productcatname, string divcode, int cmonth, int cyear, string scurrentdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (divcode != "4")
            {

                strQry = " select sum((Rec_Qty )* (Distributer_Rate))" +
                         "from Mas_Product_Category a " +
                        " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                         "left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.Purchase_Date)='" + cmonth + "' and YEAR(x.Purchase_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                        " group by a.Product_Cat_Name,MONTH(Purchase_Date),year(Purchase_Date) ";
            }
            else
            {
                strQry = " select sum((Rec_Qty * Distributer_Rate)+(x. Rec_Pieces* DP_BaseRate)) value " +
                        "from Mas_Product_Category a " +
                       " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                        "left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and z.Product_Active_Flag=0 and MONTH(x.Purchase_Date)='" + cmonth + "' and YEAR(x.Purchase_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                       " group by a.Product_Cat_Name,MONTH(Purchase_Date),year(Purchase_Date)";
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

        public DataSet purchase_register_brandwisedetail_categorywise(string divcode, string Brand, int Year, int Month, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "' and YEAR(Purchase_Date)='" + Year + "' and P.Product_Brd_Code='" + Brand + "' and P.Product_Cat_Code='" + catcode + "'and  s.Stockist_code ='" + stockistcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }


        public DataSet purchase_register_Branddetail_stockistwise_caterywise(string divcode, string brand, int Year, int Month, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "'  and YEAR(Purchase_Date)='" + Year + "' and P.Product_Brd_Code='" + brand + "'and P.Product_Cat_Code='" + catcode + "'and  s.Stockist_code ='" + stockistcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet Purchase_Brand_value_categorywise(string brandcode, string div_code, int cmonth, int cyear, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = " select sum(Rec_Qty * Distributer_Rate)from Mas_Product_Brand a " +
                        "join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
                       " left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'  " +
                       "group by a.Product_Brd_Name,MONTH(Purchase_Date),year(Purchase_Date)";
            }
            else
            {
                strQry = "select sum((Rec_Qty * Distributer_Rate) +( Rec_Pieces* DP_BaseRate)) value from Mas_Product_Brand a " +
                         " join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code " +
                          "left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0 and z.Product_Active_Flag=0 and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'  " +
                          "group by a.Product_Brd_Name,MONTH(Purchase_Date),year(Purchase_Date) ";
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



        public DataSet areawise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "'  and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "' " +
                "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet areawise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE   s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "'  and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.Purchase_Date), 0))+ 1 ='" + week + "' " +
                "group by Product_Code,Product_Name,Distributer_Rate ";

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



        public DataSet lost_Purchase_value_stockist(string stockistcode, string div_code, int cmonth, int cyear, string cdate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT count(Product_Detail_Name)  FROM Mas_Product_Detail  WHERE (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
  " OR " +
  "  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
   " OR " +
    "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
   " subdivision_code =   '" + subdivision + "') and   Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Stock_Updation_Details where Stockist_code='" + stockistcode + "' and MONTH (purchase_date)='" + cmonth + "' and year(purchase_date)='" + cyear + "') " +
    " and Division_Code='" + div_code + "'";

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
        public DataSet lost_Purchase_value_view(string stockistcode, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT Product_Detail_Name,Product_Detail_Code   FROM Mas_Product_Detail WHERE Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Stock_Updation_Details where Stockist_code='" + stockistcode + "' and MONTH (purchase_date)='" + cmonth + "' and year(purchase_date)='" + cyear + "') and Division_Code='" + div_code + "'";

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


        public DataSet Gettop10value_stockist(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   SELECT  SUM((Rec_Qty)*(Distributer_Rate))value,T.Stockist_code,S.Stockist_Name  FROM Trans_Stock_Updation_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where T.Division_Code='" + divcode + "'  and year(T.Purchase_Date)='" + year + "' group by T.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC ";

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

        public DataSet Gettop10value_category(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select  sum((s.Rec_Qty) * (s.Distributer_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
                     " where s.Division_Code='" + divcode + "'  and year(s.Purchase_Date)='" + year + "' group by b.Product_Cat_Code,b.Product_Cat_Name  ORDER BY VALUE DESC";

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
        public DataSet Gettop10value_Brand(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select  sum((s.Rec_Qty) * (s.Distributer_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
            "where s.Division_Code='" + divcode + "'  and year(s.Purchase_Date)='" + year + "'  group by b.Product_Brd_Code,b.Product_Brd_Name  ORDER BY VALUE DESC ";

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
        public DataSet Gettop10value_Product(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   select   sum((s.Rec_Qty) * (s.Distributer_Rate))value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code  " +
 "where s.Division_Code='" + divcode + "' and year(s.Purchase_Date)='" + year + "' group by P.Product_Detail_Name,p.Product_Detail_Code ORDER BY VALUE DESC ";

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
        public DataSet Sales_lost_Purchase_value_stockist(string stockistcode, string div_code, int cmonth, int cyear, string cdate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT count(Product_Detail_Name)  FROM Mas_Product_Detail WHERE (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
" OR " +
"  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
" OR " +
 "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
 " OR " +
" subdivision_code =   '" + subdivision + "') and  Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Secondary_Sales_Details where Stockist_code='" + stockistcode + "' and MONTH (date)='" + cmonth + "' and year(date)='" + cyear + "') and Division_Code='" + div_code + "'";

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
        public DataSet Sales_Trend_analysis_stockist(string stockistcode, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Sale_Qty * Retailor_Rate)as value from Trans_Secondary_Sales_Details " +
 "where date between '" + fromdate + "' and '" + todate + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet Sales_Trend_analysis_Distributor(string stockistcode, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Sale_Qty * Retailor_Rate)as value from Trans_Secondary_Sales_Details " +
 "where date between '" + fromdate + "' and '" + todate + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet Sales_Trend_analysis_currentmonth(string stockistcode, string div_code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(Sale_Qty * Retailor_Rate)value from Trans_Secondary_Sales_Details where year(date)='" + fyear + "' and MONTH(date)='" + fmonth + "'  and  Stockist_code='" + stockistcode + "' ";

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
        public DataSet Sales_Gettop10value_stockist(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (divcode != "4")
            {

                strQry = "   SELECT  SUM((Sale_Qty)*(Distributer_Rate))value,T.Stockist_code,S.Stockist_Name  FROM Trans_Secondary_Sales_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where s.Division_Code='" + divcode + "'  and year(T.date)='" + year + "' group by T.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC ";
            }
            else
            {
                strQry = "   SELECT  sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value,T.Stockist_code,S.Stockist_Name  FROM Trans_Secondary_Sales_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where s.Division_Code='" + divcode + "'  and year(T.date)='" + year + "' group by T.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC ";
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

        public DataSet Sales_Gettop10value_category(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select  sum((s.Sale_Qty) * (s.Retailor_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
                     " where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "' group by b.Product_Cat_Code,b.Product_Cat_Name  ORDER BY VALUE DESC";

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
        public DataSet Sales_Gettop10value_Brand(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select sum((s.Sale_Qty) * (s.Retailor_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
            "where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "'  group by b.Product_Brd_Code,b.Product_Brd_Name  ORDER BY VALUE DESC ";

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
        public DataSet Sales_Gettop10value_Product(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   select  sum((s.Sale_Qty) * (s.Retailor_Rate))value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code  " +
 "where P.Division_Code='" + divcode + "' and year(s.date)='" + year + "' group by P.Product_Detail_Name,p.Product_Detail_Code ORDER BY VALUE DESC ";

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
        public DataSet Secondary_sales_product_valuee(string divcode, int month, int year, int week, string product_code, string distributor, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            if (distributor == "ALL")
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int " +
" set @Remainder=null;" +
" set @Quotient=null;" +
"SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as saleqty,dd.receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, ((ROUND(((Floor(dd.Quotient)+dd.Remainder)+sale),2))*dd.Retailor_Rate)as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty,SUM(s.sale_pieces)as salepieces, " +
" Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ,opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ,clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),s.Retailor_Rate " +
 "from Trans_Secondary_Sales_Details s inner join Mas_stockist st on s.Stockist_Code=st.Stockist_Code where month(s.date)='" + month + "' and year(s.date)='" + year + "'and st.subdivision_code='" + subdivision + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' group by s.Conversion_Qty,s.Retailor_Rate)dd";
            }
            else
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int " +
" set @Remainder=null;" +
" set @Quotient=null;" +
"SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as saleqty,dd.receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, ((ROUND(((Floor(dd.Quotient)+dd.Remainder)+sale),2))*dd.Retailor_Rate)as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty,SUM(s.sale_pieces)as salepieces, " +
" Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ,opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ,clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),s.Retailor_Rate " +
 "from Trans_Secondary_Sales_Details s where month(s.date)='" + month + "' and year(s.date)='" + year + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' and Stockist_Code='" + distributor + "' group by s.Conversion_Qty,s.Retailor_Rate)dd";
            }


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Retail_Gettop10value_category(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select top 10 sum(t.value)value,b.Product_Cat_Code,b.Product_Cat_Name from " +
                     " Mas_Product_Detail P inner join Trans_Order_Details t ON t.Product_Code=P.Product_Detail_Code " +
                     " inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  where P.Division_Code='" + divcode + "' " +
                     " group by b.Product_Cat_Code,b.Product_Cat_Name  ORDER BY VALUE DESC";

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
        public DataSet Retail_Gettop10value_Brand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select top 10 sum(t.value)value,b.Product_Brd_Code,b.Product_Brd_Name from " +
                     "Mas_Product_Detail P inner join Trans_Order_Details t ON t.Product_Code=P.Product_Detail_Code " +
                     "inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code  where P.Division_Code='" + divcode + "' " +
                     "group by b.Product_Brd_Code,b.Product_Brd_Name  ORDER BY VALUE DESC";

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
        public DataSet Retail_Gettop10value_Product(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select top 10 sum(t.value)value,P.Product_Detail_Name,p.Product_Detail_Code from " +
                      " Mas_Product_Detail P inner join Trans_Order_Details t ON t.Product_Code=P.Product_Detail_Code " +
                      " where P.Division_Code='" + divcode + "' group by P.Product_Detail_Name,p.Product_Detail_Code  ORDER BY VALUE DESC";

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

        public DataSet Trend_analysis_stockist(string stockistcode, string div_code, string st)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select ((SUM(Rec_Qty * Distributer_Rate))/6) from Trans_Stock_Updation_Details where Purchase_Date > DATEADD(m, -6, '" + st + "') and Purchase_Date <'" + st + "'  and  Stockist_code='" + stockistcode + "'";

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
        public DataSet Trend_analysis_currentmonth(string stockistcode, string div_code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(Rec_Qty * Distributer_Rate)value from Trans_Stock_Updation_Details where year(Purchase_Date)='" + fyear + "' and MONTH(Purchase_Date)='" + fmonth + "'  and  Stockist_code='" + stockistcode + "' ";

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

        public DataSet sales_register_productdetail(string divcode, int Month, int Year, int week, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,SUM(S.Sale_Qty)Sale_Qty,(SUM(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "' and YEAR(date)='" + Year + "' and Stockist_code='" + distributor + "'  " +
" and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sales_register_productdetail_total(string divcode, int Month, int Year, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,SUM(S.Sale_Qty)Sale_Qty,(SUM(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "' and YEAR(date)='" + Year + "' " +
" and s.Product_Code='" + product_code + "' and  P.Product_Active_Flag=0 and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet Sales_Distributor_value(string stockistcode, int cmonth, int cyear, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (divcode != "4")
            {


                strQry = "select SUM((Sale_Qty) * (Retailor_Rate)) from Trans_Secondary_Sales_Details where month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";

            }
            else
            {

                strQry = "select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value from Trans_Secondary_Sales_Details where month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";
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
        public DataSet sale_register_categorywisedetail_stockist(string divcode, string cat_code, int Year, int Month, string cdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Sale_Qty,((Sale_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "'  and YEAR(date)='" + Year + "'   and s.Stockist_code='" + stockistcode + "' and B.Product_Cat_Code='" + cat_code + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sale_register_categorywisedetail_stockist_total(string divcode, int Year, int Month, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,(sum(S.Sale_Qty)) as Sale_Qty,(sum(Sale_Qty) * (Distributer_Rate))Value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "'  and YEAR(date)='" + Year + "'   and s.Stockist_code='" + stockistcode + "' group by Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,P.Product_Cat_Code,s.Distributer_Rate ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet sale_register_Branddetail_stockistwise_caterywise(string divcode, int Month, int Year, int week, string product_code, string stockistcode, string catcode, string brand)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where p.Division_Code='" + divcode + "'  and YEAR(date)='" + Year + "' and P.Product_Brd_Code='" + brand + "'and P.Product_Cat_Code='" + catcode + "'and  s.Stockist_code ='" + stockistcode + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Brd_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sale_register_Branddetail_stockistwise_caterywise_total(string divcode, int Month, int Year, int week, string product_code, string stockistcode, string catcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where p.Division_Code='" + divcode + "'  and YEAR(date)='" + Year + "' and P.Product_Cat_Code='" + catcode + "'and  s.Stockist_code ='" + stockistcode + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Brd_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet sale_Brand_value_categorywise(string brandcode, string div_code, int cmonth, int cyear, string cdate, string catcode, string stockistcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = " select sum(Sale_Qty * Retailor_Rate)from Mas_Product_Brand a " +
                        "join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
                       " left outer join Trans_Secondary_Sales_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0  and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "' AND  a.Product_Cat_Div_Code='" + subdivision + "' " +
                       "group by a.Product_Brd_Name,MONTH(date),year(date) ";
            }
            else
            {
                strQry = " select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value from Mas_Product_Brand a " +
                                    "join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
                                   " left outer join Trans_Secondary_Sales_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0  and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "' AND  a.Product_Cat_Div_Code='" + subdivision + "' " +
                                   "group by a.Product_Brd_Name,MONTH(date),year(date) ";
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
        public DataSet Sale_Statewise_value(string statecode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
             "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(date)='" + cmonth + "'and year(date)='" + cyear + "'" +
             "AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "' and s.subdivision_code='" + subdivision + "' ";
            }
            else
            {


                strQry = "select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value " +
            " from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
             "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(date)='" + cmonth + "'and year(date)='" + cyear + "'" +
             "AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "' and s.subdivision_code='" + subdivision + "' ";
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
        public DataSet Sale_Statewise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string Product_code, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
      "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(U.date)='" + cmonth + "'and year(U.date)='" + cyear + "'  and U.Product_Code='" + Product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "'" +
    " AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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
        public DataSet Sale_Statewise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string Product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
      "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(U.date)='" + cmonth + "'and year(U.date)='" + cyear + "'  and U.Product_Code='" + Product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "'" +
    " and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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
        public DataSet sale_areawise_purchase_value(string areacode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S" +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and subdivision_code='" + subdivision + "' ";
            }
            else
            {


                strQry = "select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S" +
                      " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and subdivision_code='" + subdivision + "' ";
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

        public DataSet sale_areawise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "'" +

" and U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "' group by Product_Code,Product_Name,Distributer_Rate    ";

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
        public DataSet sale_areawise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE   s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "'" +

" and U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "' group by Product_Code,Product_Name,Distributer_Rate    ";

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
        public DataSet sale_Zonewise_purchase_value(string zonecode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S " +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and subdivision_code='" + subdivision + "'";
            }
            else
            {


                strQry = "select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value " +
      " from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S " +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and subdivision_code='" + subdivision + "'";
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
        public DataSet sale_zonewise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "'  and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "' " +
 "group by Product_Code,Product_Name,Distributer_Rate  ";

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
        public DataSet sale_Territorywise_purchase_value(string territorycode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {
                strQry = "select sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "'" +
               "AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";
            }

            else
            {
                strQry = "select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value " +
   " from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "'" +
           "AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";
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
        public DataSet sale_Territorywise_purchase_productdetail(string div_code, int cmonth, int cyear, int week, string product_code, string territorycode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "'" +
          "group by Product_Code,Product_Name,Distributer_Rate ";

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

        public DataSet sale_catogory_wise_sale_value_stockist(string productcatname, string divcode, int cmonth, int cyear, string scurrentdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (divcode != "4")
            {
                strQry = " select sum((Sale_Qty )* (Retailor_Rate))" +
                         "from Mas_Product_Category a " +
                        " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                         "left outer join Trans_Secondary_Sales_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.date)='" + cmonth + "' and YEAR(x.date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                        " group by a.Product_Cat_Name,MONTH(date),year(date) ";

            }
            else
            {

                strQry = " select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value " +
                                 "from Mas_Product_Category a " +
                                " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                                 "left outer join Trans_Secondary_Sales_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.date)='" + cmonth + "' and YEAR(x.date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                                " group by a.Product_Cat_Name,MONTH(date),year(date) ";
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
        public DataSet retail_areawise_purchase_productdetail(string div_code, string areacode, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No  INNER JOIN Mas_Stockist S ON h.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "' and  s.subdivision_code='" + subdivision + "'" +
         "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
 " OR " +
 " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
 " OR" +
 "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
 " OR " +
 "  p.subdivision_code =   '" + subdivision + "')" +

                "group by Product_Code,Product_Name";

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
        public DataSet retail_areawise_purchase_productdetail_total(string div_code, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No  INNER JOIN Mas_Stockist S ON h.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code  inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code WHERE s.Division_Code='" + div_code + "' and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "'and  s.subdivision_code='" + subdivision + "'  " +
"and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
 " OR " +
 " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
 " OR" +
 "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
 " OR " +
 "  p.subdivision_code =   '" + subdivision + "')" +

  "group by Product_Code,Product_Name";

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
        public DataSet retail_areawise_purchase_value(string areacode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(x.Order_Value) value from Trans_Order_Head x  INNER JOIN Mas_Stockist S ON x.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code  WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(x.Order_Date)='" + cmonth + "' and year(x.Order_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";

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
        public DataSet retail_register_brandwisedetail_categorywise(string divcode, string Brand, int Year, int Month, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select g.Product_Code,g.Product_Name,g.Quantity,g.value,z.Product_Sale_Unit from Mas_Product_Brand a  join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code  left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No where a.Division_Code='" + divcode + "' " +
            "and a.Product_Brd_Active_Flag=0 and  MONTH(Order_Date)='" + Month + "'and year(Order_Date)='" + Year + "' and a.Product_Brd_Code='" + Brand + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'  " +
        "group by a.Product_Brd_Name,Month(Order_Date),year(Order_Date),g.Product_Code ,g.Product_Name,g.Quantity,g.value,Product_Sale_Unit";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_brandwisedetail_categorywise_stockist(string divcode, int Year, int Month, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select g.Product_Code,g.Product_Name,g.Quantity,g.value,z.Product_Sale_Unit from Mas_Product_Brand a  join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code  left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No where a.Division_Code='" + divcode + "' " +
            "and a.Product_Brd_Active_Flag=0  and MONTH(Order_Date)='" + Month + "' and year(Order_Date)='" + Year + "'  AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'  " +
        "group by a.Product_Brd_Name,MONTH(Order_Date),year(Order_Date),g.Product_Code ,g.Product_Name,g.Quantity,g.value,Product_Sale_Unit";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_category_wise_sale_value_stockist(string productcatname, string divcode, int cmonth, int cyear, string scurrentdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select sum(value)" +
                    "from Mas_Product_Category a" +
                    " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
                   "left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No  where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.Order_Date)='" + cmonth + "' and YEAR(x.Order_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                 " group by a.Product_Cat_Name,MONTH(Order_Date),year(Order_Date) ";

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
        public DataSet retail_register_categorywisedetail_stockist(string divcode, string cat_code, int Year, int Month, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select g.Product_Code,g.Product_Name,g.Quantity,g.value,z.Product_Sale_Unit from Mas_Product_Category a join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No  where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + cat_code + "' and MONTH(x.Order_Date)='" + Month + "' and YEAR(x.Order_Date)='" + Year + "' and x.Stockist_code ='" + stockistcode + "' group by a.Product_Cat_Name,MONTH(Order_Date),year(Order_Date),g.Product_Code ,g.Product_Name,g.Quantity,g.value,Product_Sale_Unit ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_categorywisedetail_stockist_total(string divcode, int Year, int Month, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select g.Product_Code,g.Product_Name,g.Quantity,g.value,z.Product_Sale_Unit from Mas_Product_Category a join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No  where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and MONTH(x.Order_Date)='" + Month + "' and YEAR(x.Order_Date)='" + Year + "' and x.Stockist_code ='" + stockistcode + "' group by a.Product_Cat_Name,MONTH(Order_Date),year(Order_Date),g.Product_Code ,g.Product_Name,g.Quantity,g.value,Product_Sale_Unit ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_productdetail(string divcode, string stockist, int Year, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select d.Product_Code,d.Product_Name,sum(d.Quantity) as Quantity,sum(d.value) as value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No and month(h.Order_Date)='" + Month + "' and YEAR(h.Order_Date)='" + Year + "' and h.Stockist_Code='" + stockist + "'  group by d.Product_Code,d.Product_Name  ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_productdetail_total(string divcode, int Year, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select d.Product_Code,d.Product_Name,sum(d.Quantity) as Quantity,sum(d.value) as value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No inner join mas_stockist s on s.Stockist_Code=h.Stockist_Code and s.Division_Code='" + divcode + "'and month(h.Order_Date)='" + Month + "' and YEAR(h.Order_Date)='" + Year + "'   group by d.Product_Code,d.Product_Name ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_Distributor_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "Select SUM (Order_Value) from Trans_Order_Head where Stockist_Code='" + stockistcode + "' and MONTH(Order_Date)='" + cmonth + "' and YEAR(Order_Date)='" + cyear + "'";

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

        public DataSet retail_Brand_value_categorywise(string brandcode, string div_code, int cmonth, int cyear, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            strQry = " select sum(g.value)from Mas_Product_Brand a " +
           " join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
           " left outer join Trans_Order_Details g on z.Product_Detail_Code=g.Product_Code inner join Trans_Order_Head x  on g.Trans_Sl_No=x.Trans_Sl_No where a.Division_Code='1' and a.Product_Brd_Active_Flag=0  and MONTH(Order_Date)='" + cmonth + "' and year(Order_Date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'" +
           "  group by a.Product_Brd_Name,MONTH(Order_Date),year(Order_Date)";
            //"select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";

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
        public DataSet retail_Statewise_purchase_productdetail(string div_code, string statecode, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S  ON h.Stockist_code=S.Stockist_Code  inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code WHERE  MONTH(h.Order_Date)='" + cmonth + "' and  s.subdivision_code='" + subdivision + "' " +
               " and year(h.Order_Date)='" + cyear + "'AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "' and  p.Product_Active_Flag=0 " +
               "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +


     "group by Product_Code,Product_Name";

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
        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution_routee_total(string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No   " +
        "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "'";

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

        public DataSet retail_Statewise_purchase_productdetail_total(string div_code, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S   ON h.Stockist_code=S.Stockist_Code  inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code  WHERE  MONTH(h.Order_Date)='" + cmonth + "' " +
               " and year(h.Order_Date)='" + cyear + "'and s.Division_Code='" + div_code + "' and  s.subdivision_code='" + subdivision + "' " +
    "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +

    "group by Product_Code,Product_Name";

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
        public DataSet retail_Statewise_value(string statecode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum(x.Order_Value) value from  Trans_Order_Head x   INNER JOIN Mas_Stockist S on x.Stockist_Code=s.Stockist_Code WHERE  MONTH(x.Order_Date)='" + cmonth + "'and year(x.Order_Date)='" + cyear + "'  AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "'  and s.subdivision_code='" + subdivision + "'";

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
        public DataSet retail_Territorywise_purchase_productdetail(string div_code, string territorycode, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S  ON h.Stockist_code=S.Stockist_Code inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code  WHERE S. Territory_Code='" + territorycode + "' AND  s.Division_Code='" + div_code + "' and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "' and  s.subdivision_code='" + subdivision + "'" +
           "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +

                "group by Product_Code,Product_Name ";

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
        public DataSet retail_Territorywise_purchase_productdetail_total(string div_code, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S  ON h.Stockist_code=S.Stockist_Code inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code  WHERE   s.Division_Code='" + div_code + "' and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "' and  s.subdivision_code='" + subdivision + "'" +
            "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +

     "group by Product_Code,Product_Name ";

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
        public DataSet retail_Territorywise_purchase_value(string territorycode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum(x.Order_Value) value from Trans_Order_Head x  INNER JOIN Mas_Stockist S  ON x.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "'" +
           "AND  s.Division_Code='" + div_code + "' and MONTH(x.Order_Date)='" + cmonth + "' and year(x.Order_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";

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
        public DataSet retail_Gettop10value_stockist(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT  SUM(x.Order_Value)value,x.Stockist_code,S.Stockist_Name  FROM   Trans_Order_Head x   INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=x.Stockist_code where s.Division_Code='" + divcode + "'  and year(x.Order_Date)='" + year + "' group by x.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC";

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

        public DataSet Retail_Gettop10value_category(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select * from(select top 10 sum(t.value)as value,r.Product_Cat_Code from Trans_Order_Details t   inner join Trans_Order_Head h on h.Trans_Sl_No=t.Trans_Sl_No inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where DATEPART(yyyy,h.Order_Date)='" + year + "' group by r.Product_Cat_Code ) se RIGHT OUTER JOIN (select s.Product_Cat_Code as cat_code,s.Product_Cat_Name  from Mas_Product_Category s " +
                     "where s.Product_Cat_Active_Flag=0 and s.Division_Code='" + divcode + "' ) dd  on se.Product_Cat_Code=dd.cat_code group by se.Product_Cat_Code,se.value,dd.Product_Cat_Name,dd.cat_code  order by value desc";

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
        public DataSet Retail_Gettop10value_Brand(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select * from(select top 10 sum(t.value)as value,r.Product_Brd_Code from Trans_Order_Details t  inner join Trans_Order_Head h on h.Trans_Sl_No=t.Trans_Sl_No  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  DATEPART(yyyy,h.Order_Date)='" + year + "' group by r.Product_Brd_Code ) se  RIGHT OUTER JOIN (select s.Product_Brd_Code as Brand_code,s.Product_Brd_Name  from Mas_Product_Brand s " +
                     "where  s.Product_Brd_Active_Flag=0 and s.Division_Code='" + divcode + "')dd  on se.Product_Brd_Code=dd.Brand_code group by se.Product_Brd_Code,se.value,dd.Product_Brd_Name,dd.Brand_code  order by value desc";

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


        public DataSet Retail_Gettop10value_Product(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select top 10 sum(t.value)as value,r.Product_Detail_Code,r.Product_Detail_Name from Trans_Order_Details t inner join Trans_Order_Head h on h.Trans_Sl_No=t.Trans_Sl_No inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where DATEPART(yyyy,h.Order_Date)='" + year + "' and r.Division_Code='" + divcode + "' group by r.Product_Detail_Code,r.Product_Detail_Name order by value desc";

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
        public DataSet Retail_Trend_analysis_stockist(string stockistcode, string div_code, string st)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select ((SUM(Order_Value))/6) from Trans_Order_Head where Order_Date > DATEADD(m, -6, '" + st + "') and Order_Date <('" + st + "')  and  Stockist_code='" + stockistcode + "'";

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
        public DataSet Retail_Trend_analysis_currentmonth(string stockistcode, string div_code, string fyear, string fmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(Order_Value)value from Trans_Order_Head where year(Order_Date)='" + fyear + "' and MONTH(Order_Date)='" + fmonth + "'  and  Stockist_code='" + stockistcode + "' ";

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
        public DataSet retail_zonewise_purchase_productdetail(string div_code, string zonecode, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S ON h.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code  inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "'  and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "' and  s.subdivision_code='" + subdivision + "' " +

"and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +

                " group by Product_Code,Product_Name  ";

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
        public DataSet retail_zonewise_purchase_productdetail_total(string div_code, int cyear, int cmonth, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(d.Quantity)Quantity,Product_Name,sum(value) value from Trans_Order_Details d inner join Trans_Order_Head h on h.Trans_Sl_No=d.Trans_Sl_No INNER JOIN Mas_Stockist S ON h.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code inner join Mas_Product_Detail p on d.Product_Code=p.Product_Detail_Code WHERE  s.Division_Code='" + div_code + "'  and MONTH(h.Order_Date)='" + cmonth + "' and year(h.Order_Date)='" + cyear + "' and  s.subdivision_code='" + subdivision + "' " +

                "and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
    " OR " +
    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
    " OR" +
    "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
    "  p.subdivision_code =   '" + subdivision + "')" +
                " group by Product_Code,Product_Name  ";

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
        public DataSet retail_Zonewise_purchase_value(string zonecode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum(x.Order_Value) value from Trans_Order_Head x    INNER JOIN Mas_Stockist S  " +
    " ON x.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "' and MONTH(x.Order_Date)='" + cmonth + "' and year(x.Order_Date)='" + cyear + "' and subdivision_code='" + subdivision + "'";

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
        public DataSet getTP_Deviation_Transl_terr(string Trans_SlNo)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " select distinct SDP,SDP_Name from DCRDetail_Lst_Trans where Trans_SlNo ='" + Trans_SlNo + "' ";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getTP_Deviation(string divcode, string sf_code, int Month, int Year, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "EXEC sp_TP_Deviation '" + divcode + "', '" + sf_code + "','" + Month + "' ,'" + Year + "' ,'" + type + "'";

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }
        public DataSet SalesForceList_New_GetMr(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_Rep_access_with_HQ '" + sf_code + "', '" + divcode + "' ";

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
        public DataSet purchase_value_product_weekwise(string divcode, int month, int year, int week, string product_code, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select SUM((Rec_Qty) * (Distributer_Rate)) as value from Trans_Stock_Updation_Details s where month(s.Purchase_Date)='" + month + "' and year(s.Purchase_Date)='" + year + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' and s.Stockist_Code='" + stockistcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet sale_value_product_weekwise(string divcode, int month, int year, int week, string product_code, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select SUM((Sale_Qty) * (Distributer_Rate)) as value from Trans_Secondary_Sales_Details s where month(s.date)='" + month + "' and year(s.date)='" + year + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' and s.Stockist_Code='" + stockistcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet GetProduct_Name(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code='" + divcode + "'";

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


        public DataSet primary_Purchase_Distributor_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate, string productcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (stockistcode == "ALL")
            {
                strQry = "select SUM((t.Rec_Qty) * (t.Distributer_Rate)) from Trans_Stock_Updation_Details t inner join Mas_Stockist s on s.Stockist_Code=t.Stockist_Code where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "'  and  Product_Code='" + productcode + "' and s.subdivision_code='" + subdivision + "'";
            }
            else
            {

                strQry = "select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "' and Product_Code='" + productcode + "'";
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
        public DataSet primary_Purchase_Distributor_value_single_prt(string stockistcode, string div_code, int cmonth, int cyear, int week, string productcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and  DATEPART(WEEK, Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,Purchase_Date), 0))+ 1 ='" + week + "' and Stockist_code='" + stockistcode + "' and Product_Code='" + productcode + "'";

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
        public DataSet secondary_Purchase_Distributor_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate, string productcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (stockistcode == "ALL")
            {
                strQry = "select SUM((t.Sale_Qty) * (t.Retailor_Rate)) from Trans_Secondary_Sales_Details t inner join Mas_Stockist s on s.Stockist_Code=t.Stockist_Code where month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "'  and  Product_Code='" + productcode + "' and s.subdivision_code='" + subdivision + "'";
            }
            else
            {

                strQry =
                      "select SUM((Sale_Qty) * (Retailor_Rate)) from Trans_Secondary_Sales_Details where month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "'  and Stockist_code='" + stockistcode + "' and Product_Code='" + productcode + "'";
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
        public DataSet secondary_Purchase_Distributor_value_single_prt(string stockistcode, string div_code, int cmonth, int cyear, int week, string productcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM((Sale_Qty) * (Retailor_Rate)) from Trans_Secondary_Sales_Details where month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "'  and  DATEPART(WEEK, date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,date), 0))+ 1 ='" + week + "' and Stockist_code='" + stockistcode + "' and Product_Code='" + productcode + "'";

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
        public DataSet Secondary_sales_get_no_of_week(int year, string month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = ";WITH NumWeeks " +
                        " AS " +
                      " (" +
                      "SELECT Number + 1 as mth," +
                  "  DATEDIFF(day,-1,DATEADD(month,(('" + year + "'-1900)*12)+ Number,0))/7 AS fst," +
                   " DATEDIFF(day,-1,DATEADD(month,(('" + year + "'-1900)*12)+ Number,30))/7  AS lst " +
                 "  FROM master..spt_values " +
                  " WHERE Type = 'P' and Number < 12 " +
                 "      ) " +
                " SELECT  " +
                 " lst - fst + 1 AS [NumberOfWeeks]" +
              "      FROM NumWeeks where DateName(mm,DATEADD(mm,mth,-1))='" + month + "';";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Secondary_sales_productdetail(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "SELECT P.Product_Detail_Code, P.Product_Detail_Name,P.product_unit AS Product_Sale_Unit from Mas_Product_Detail P  where p.Division_Code='" + divcode + "' and P.Product_Active_Flag =0  and " +
    "  (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%'" +
     " OR" +
     " subdivision_code LIKE  '" + subdivision + "' + ',%'" +
      "OR" +
   " subdivision_code LIKE '%,' +  '" + subdivision + "' " +
    "  OR " +
    "  subdivision_code =   '" + subdivision + "') ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet Route_Distributor_value(string stockistcode, string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = "select SUM((Rec_Qty) * (Distributer_Rate)),sum(net_weight_value)net_weight_value from Trans_Stock_Updation_Details where month(Purchase_Date)='"+cmonth+"' and YEAR(Purchase_Date)='"+cyear+"' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";
            strQry = "select sum(P.value)value,sum(P.net_weight* P.Quantity)net_weight_value " +
                         " from Trans_Order_Details P inner join Trans_Order_Head x ON x.Trans_Sl_No=P.Trans_Sl_No " +
                         " where  MONTH(x.Order_Date)='" + cmonth + "' and YEAR(x.Order_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
                         " group by MONTH(Order_Date),year(Order_Date) ";
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
        public DataSet Sec_Order_Dis_wise(string divcode, string stockist, int Year, int Month, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select p.Product_Code,P.Product_Name,sum(P.Quantity)Quantity,sum(p.value)value from Trans_Order_Details P inner join Trans_Order_Head S ON s.Trans_Sl_No=P.Trans_Sl_No where  month(s.Order_Date)='" + Month + "' and YEAR(s.Order_Date)='" + Year + "' and s.Stockist_code='" + stockist + "' group by p.Product_Code,p.Product_Name";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet GetRouteName_Customer(string dis_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
             strQry = " SELECT 0 as Territory_Code, '--Select--' as Territory_Name " +
                     " UNION " +
                     "select Territory_Code,Territory_Name from Mas_Territory_Creation where Dist_Name='" + dis_code + "' and Territory_Active_Flag=0 and Division_Code='" + divcode + "'";

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
        public DataSet Route_wise_sale_value_stockist(string productcatname, string divcode, int cmonth, int cyear, string scurrentdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //strQry = " select sum((Rec_Qty )* (Distributer_Rate))" +
            //         "from Mas_Product_Category a " +
            //        " join Mas_Product_Detail z on a.Product_Cat_Code=z.Product_Cat_Code " +
            //         "left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + divcode + "'and a.Product_Cat_Active_Flag=0 and a.Product_Cat_Code='" + productcatname + "' and MONTH(x.Purchase_Date)='" + cmonth + "' and YEAR(x.Purchase_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' " +
            //        " group by a.Product_Cat_Name,MONTH(Purchase_Date),year(Purchase_Date) ";
            strQry = "select sum(P.value)value " +
                   " from Trans_Order_Details P inner join Trans_Order_Head x ON x.Trans_Sl_No=P.Trans_Sl_No  " +
                   " where  MONTH(x.Order_Date)='" + cmonth + "' and YEAR(x.Order_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' and x.Route='" + productcatname + "' " +
                   " group by MONTH(Order_Date),year(Order_Date) ";

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
        public DataSet Secondary_Order_Route_wise(string divcode, string cat_code, int Year, int Month, string cdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            //strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "'  and YEAR(Purchase_Date)='" + Year + "'   and s.Stockist_code='" + stockistcode + "' ";
            strQry = "select P.Product_Code,P.Product_Name,sum(P.Quantity)Quantity,sum(p.value)value from Trans_Order_Details P inner join Trans_Order_Head S ON s.Trans_Sl_No=P.Trans_Sl_No where  month(s.Order_Date)='" + Month + "' and YEAR(s.Order_Date)='" + Year + "' and s.Stockist_code='" + stockistcode + "' and s.Route='" + cat_code + "' group by p.Product_Code,p.Product_Name ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet GetRetailer_Customer(string divcode, string stockist_code, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select ListedDrCode,ListedDr_Name from Mas_ListedDr where Sf_Code='" + stockist_code + "' and Territory_Code='" + cat_code + "' and ListedDr_Active_Flag=0 and Division_Code='" + divcode + "'";

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
        public DataSet Retailer_value_wise(string brandcode, string div_code, int cmonth, int cyear, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            //strQry = " select sum(Rec_Qty * Distributer_Rate)from Mas_Product_Brand a " +
            //        "join Mas_Product_Detail z on a.Product_Brd_Code=z.Product_Brd_Code" +
            //       " left outer join Trans_Stock_Updation_Details x on z.Product_Detail_Code=x.Product_Code where a.Division_Code='" + div_code + "' and a.Product_Brd_Active_Flag=0  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and a.Product_Brd_Code='" + brandcode + "' AND z.Product_Cat_Code='" + catcode + "'and  x.Stockist_code ='" + stockistcode + "'  " +
            //       "group by a.Product_Brd_Name,MONTH(Purchase_Date),year(Purchase_Date)";
            //"select SUM((Rec_Qty) * (Distributer_Rate)) from Trans_Stock_Updation_Details where month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and Division_Code='" + div_code + "' and Stockist_code='" + stockistcode + "'";
            strQry = "select sum(Order_Value)value " +
                 " from Trans_Order_Head x " +
                 " where  MONTH(x.Order_Date)='" + cmonth + "' and YEAR(x.Order_Date)='" + cyear + "' and x.Stockist_code ='" + stockistcode + "' and x.Route='" + catcode + "' and x.Cust_Code='" + brandcode + "' " +
                 " group by MONTH(Order_Date),year(Order_Date) ";
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


        public DataSet areawise_purchase_value(string areacode, string div_code, int cmonth, int cyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {
                strQry = "select sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";

            }
            else
            {

                strQry = "select sum((Rec_Qty * Distributer_Rate)+ (Rec_Pieces*DP_BaseRate)) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
            " ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and s.subdivision_code='" + subdivision + "'";
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
        public DataSet purchase_register_Retailerwisedetail_Routewise(string divcode, string Brand, int Year, int Month, string cdate, string catcode, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Code,P.Product_Name,sum(P.Quantity)Quantity,sum(p.value)value from Trans_Order_Details P inner join Trans_Order_Head S ON s.Trans_Sl_No=P.Trans_Sl_No where  month(s.Order_Date)='" + Month + "' and YEAR(s.Order_Date)='" + Year + "' and s.Cust_Code='" + Brand + "' and s.Route='" + catcode + "' and  s.Stockist_code ='" + stockistcode + "' group by p.Product_Code,p.Product_Name";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Getretail_customer(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '0' as Territory_Code, '---ALL---' as Territory_Name " +
                     " UNION  " +
     " select Territory_Code,Territory_Name from mas_territory_creation where Division_Code='" + divcode + "'";

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
        public DataSet retail_Gettop10value_route(string divcode, string year, string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (route == "0")
            {
                strQry = "SELECT t.ListedDr_Name, SUM(x.Order_Value)value,x.Cust_Code,X.net_weight_value  FROM   Trans_Order_Head x  inner join Mas_ListedDr t on x.Cust_Code=t.ListedDrCode   where  year(x.Order_Date)='" + year + "'  and ListedDr_Active_Flag=0  group by x.Cust_Code,t.ListedDr_Name,X.net_weight_value ORDER BY VALUE DESC";
            }
            else
            {

                strQry = "SELECT t.ListedDr_Name, SUM(x.Order_Value)value,x.Cust_Code,X.net_weight_value   FROM   Trans_Order_Head x  inner join Mas_ListedDr t on x.Cust_Code=t.ListedDrCode   where  year(x.Order_Date)='" + year + "' and x.Route='" + route + "'  and ListedDr_Active_Flag=0 group by x.Cust_Code,t.ListedDr_Name,X.net_weight_value ORDER BY VALUE DESC";
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
        public DataSet Getretailer_distributor(string divcode, string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Territory_Code,Territory_Name from mas_territory_creation where Division_Code='" + divcode + "' and Dist_Name='" + stockist_code + "'";

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
        public DataSet retail_lost_Purchase_value_stockist(string routecode, string div_code, int cmonth, int cyear, string cdate, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " WITH ctee AS " +
               " (" +
"SELECT Product_Detail_Name  FROM Mas_Product_Detail WHERE Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Order_Details d inner join Trans_Order_Head  h on D.Trans_Sl_No=H.Trans_Sl_No where h.Stockist_code='" + stockistcode + "' and h.Route='" + routecode + "' and MONTH (Order_Date)='" + cmonth + "' and year(Order_Date)='" + cyear + "'))" +

   " SELECT Stuff(" +
   " (" +
    "SELECT ', ' + ctee.Product_Detail_Name   FROM ctee " +
    "FOR XML PATH('')" +
  "  ), 1, 2, '') AS  Product_Name";

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
        public DataSet purchase_register_producttotal(string divcode, int Year, int Month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,(Sum(S.Rec_Qty)) as Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + Month + "' and YEAR(Purchase_Date)='" + Year + "' group by P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,s.Distributer_Rate ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_register_categorywisedetail_producttotal(string divcode, string cat_code, int Year, int Month, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,S.Rec_Qty,((Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code  where s.Division_Code='" + divcode + "' and Month(Purchase_Date)='" + Month + "'  and YEAR(Purchase_Date)='" + Year + "' and  s.Stockist_Code='" + stockistcode + "' ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Secondary_sales_product_value(string divcode, int month, int year, int week, string product_code, string distributor, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            if (distributor == "ALL")
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int ,@rlquotient int,@rlremainder int ,@salepiecevalue varchar,@salevalue varchar " +
            "set @Remainder=null;" +
            "set @Quotient=null;" +
         "SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as saleqty, ROUND(((floor(dd.rlquotient)+dd.rlremainder)+ receipt_qty),2) as receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, (dd.salevalue+dd.salepiecevalue) as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty," +
         "salepiecevalue=sum(s.sale_pieces*s.RP_BaseRate),salevalue=sum(s.Sale_Qty*s.Retailor_Rate) " +
        " ,Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
       "opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ," +
        "clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),rlquotient=((SUM(s.Rec_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
    "rlremainder=(((cast(SUM(s.Rec_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100) " +
    "from Trans_Secondary_Sales_Details s inner join Mas_stockist st on  " +
    "s.Stockist_Code=st.Stockist_Code where month(s.date)='" + month + "' and year(s.date)='" + year + "' " +
    "and st.subdivision_code='" + subdivision + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' group by s.Conversion_Qty)dd";
            }
            else
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int ,@rlquotient int,@rlremainder int ,@salepiecevalue varchar,@salevalue varchar " +
            "set @Remainder=null;" +
            "set @Quotient=null;" +
         "SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as saleqty, ROUND(((floor(dd.rlquotient)+dd.rlremainder)+ receipt_qty),2) as receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, (dd.salevalue+dd.salepiecevalue) as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty," +
         "salepiecevalue=sum(s.sale_pieces*s.RP_BaseRate),salevalue=sum(s.Sale_Qty*s.Retailor_Rate) " +
        " ,Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
       "opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ," +
        "clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),rlquotient=((SUM(s.Rec_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
    "rlremainder=(((cast(SUM(s.Rec_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100) " +
    "from Trans_Secondary_Sales_Details s  where month(s.date)='" + month + "' and year(s.date)='" + year + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' and Stockist_Code='" + distributor + "' group by s.Conversion_Qty)dd";
            }


            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet GetStockName_Customer_DIS(string divcode, string DIS)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Stockist_Name='" + DIS + "'and Division_Code='" + divcode + "'";

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
        public DataSet UserList_DSM(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '0' as DSM_code, '---Select---' as DSM_name " +
                     " UNION " +
                     "select [DSM_code],[DSM_name] from Mas_DSM where  Div_code='" + divcode + "'";

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

        public DataSet getUserListDSM(string div_code, string sf_code, int order_id, string sf_type)//23-oct-15
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsDivision = null;
            strQry = "SELECT Town_Name,DSM_name,DSM_sname,Distributor_Name,UserName,Password,DSM_Active_Flag " +
                     "FROM Mas_DSM " +
                     "WHERE Div_code='" + div_code + "' and DSM_code='" + sf_code + "' " +
                     "ORDER BY 1 ";
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

        public DataSet Sales_lost_Purchase_value_view(string stockistcode, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT Product_Detail_Name,Product_Detail_Code FROM Mas_Product_Detail WHERE Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Secondary_Sales_Details where Stockist_code='" + stockistcode + "' and MONTH (date)='" + cmonth + "' and year(date)='" + cyear + "') and Division_Code='" + div_code + "' ";



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
        public DataSet Retail_lost_Purchase_value_view(string territorycode, string stockistcode, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT Product_Detail_Name,Product_Sale_Unit  FROM Mas_Product_Detail WHERE Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Order_Details d inner join Trans_Order_Head  h on D.Trans_Sl_No=H.Trans_Sl_No where h.Stockist_code='" + stockistcode + "' and MONTH (Order_Date)='" + cmonth + "' and year(Order_Date)='" + cyear + "' and Route='" + territorycode + "')and Division_Code='" + div_code + "'";




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
        public DataSet Getsubdivisionwise(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code='" + divcode + "' and SubDivision_Active_Flag=0";

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


 public DataSet Getsubdivisionwise_sfcode(string divcode,string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
           // strQry = "select subdivision_code,subdivision_name from mas_subdivision where Div_Code='" + divcode + "' and sf_code ='" + sf_code +"' and SubDivision_Active_Flag=0";
            strQry = " select a.subdivision_code,a.subdivision_name from mas_subdivision as a inner join Mas_Salesforce as b on a.subdivision_code=  replace(b.subdivision_code ,',','') where a.Div_Code='" + divcode + "' and b.sf_code ='" + sf_code + "' and a.SubDivision_Active_Flag=0";
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

        public DataSet purchase_disributor_view_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and s.stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_disributor_view_weekise_details_total(string divcode, int month, int year, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_categoryview_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "'  and YEAR(Purchase_Date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "' and P.Product_Cat_Code='" + cat_code + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Cat_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_categoryview_weekise_details_total(string divcode, int month, int year, int week, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "'  and YEAR(Purchase_Date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "'  group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Cat_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet purchase_Brandview_weekise_details_total(string divcode, int month, int year, int week, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "'  and P.Product_Cat_Code='" + cat_code + "'and  s.Stockist_code ='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Brd_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_Brandview_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor, string cat_code, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and P.Product_Brd_Code='" + brand_code + "' and P.Product_Cat_Code='" + cat_code + "'and  s.Stockist_code ='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.Purchase_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.Purchase_Date), 0))+ 1 ='" + week + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Brd_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet sale_categoryview_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + month + "'  and YEAR(date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "' and P.Product_Cat_Code='" + cat_code + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Cat_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sale_categoryview_weekise_details_total(string divcode, int month, int year, int week, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + month + "'  and YEAR(date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'  group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Cat_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }


        public DataSet GetDistName(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + divcode + "'";

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
         public DataSet GetPro(string str_code, string divcode,string mon,string year,string Tmon,string Tyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Territory_Code,Territory_Name,DSM_Code,DSM_Name, "+
                     "(select COUNT(ListedDrCode) from Mas_ListedDr where Territory_Code=T.Territory_Code "+
                     ") RetCnt,(select sum(net_weight_value) from  Trans_Order_Head where Route=T.Territory_Code and Stockist_Code='"+str_code+"'  and MONTH(Order_Date) between "+mon+" and "+Tmon+" and year(order_Date) between "+year+" and "+Tyear+")Net  ,(select COUNT( distinct Cust_Code) from vwRoutewsTarOVal " +
                     "where Stockist_Code='" + str_code + "' and MONTH(Order_Date) between "+mon+" and "+Tmon+" and year(order_Date) between "+year+" and "+Tyear+" and Route=T.Territory_Code ) Cnt," +
                      "sum(RouteTar) RouteTar,sum(OVal) OVal from Mas_Territory_Creation T left outer join Mas_DSM D on D.DSM_code=T.SF_Code "+
                      "left outer join (select Order_Date,Route,max(cast(RouteTar as float)) RouteTar,sum(OVal) OVal from vwRoutewsTarOVal "+
                      "where Stockist_Code='" + str_code + "' and MONTH(Order_Date) between "+mon+" and "+Tmon+" and year(order_Date) between "+year+" and "+Tyear+"  group by Order_Date,Route " +
                      ") O on Route=Territory_Code where Dist_Name='" + str_code + "' and MONTH(Order_Date) between "+mon+" and "+Tmon+" and year(order_Date) between "+year+" and "+Tyear+" " + 
                      " group by Territory_Code,Territory_Name,DSM_Code,DSM_Name";

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


        public DataSet retail_routewise_distribution_value(string stockistcode, string div_code, string routecode, string product_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(d.value)Order_Value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "'";

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
        public DataSet retail_routewise_distribution_quantity(string stockistcode, string div_code, string routecode, string product_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(d.Quantity) Quantity from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "'";

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

        public DataSet GetRouteName_Distributorwise(string dis_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select t.Territory_Code,t.Territory_Name from Mas_Territory_Creation t inner join Mas_Stockist  s on  s.Stockist_code= t.Dist_Name where s.Stockist_code='" + dis_code + "'  and Territory_Active_Flag=0 and s.Division_Code='" + divcode + "'";

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
        public DataSet GetProduct_subdivisionwise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Product_Detail_Code,Product_Detail_Name from mas_product_detail  " +
   "WHERE Division_Code='" + divcode + "'and Product_Active_Flag=0 and " +
    "  (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%'" +
     " OR" +
     " subdivision_code LIKE  '" + subdivision + "' + ',%'" +
      "OR" +
   " subdivision_code LIKE '%,' +  '" + subdivision + "' " +
    "  OR " +
    "  subdivision_code =   '" + subdivision + "')";

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
        public DataSet Sales_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = " select SUM(t.Sale_Qty * t.Retailor_Rate)as value from Trans_Secondary_Sales_Details t inner join mas_stockist s  on s.Stockist_Code=t.Stockist_Code where date between '" + fromdate + "' and '" + todate + "'  and s.subdivision_code='" + subdivision + "'";

            }
            else
            {
                strQry = " select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Trans_Secondary_Sales_Details t inner join mas_stockist s  on s.Stockist_Code=t.Stockist_Code where date between '" + fromdate + "' and '" + todate + "'  and s.subdivision_code='" + subdivision + "'";
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
        public DataSet Getcategorysudivision_wise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P  inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  " +
          " where P.Division_Code='" + divcode + "' and  Product_Cat_Active_Flag=0 " +
          "and  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            " OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
            " OR " +
            "  p.subdivision_code =   '" + subdivision + "'" +

        " group by b.Product_Cat_Code,b.Product_Cat_Name";

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
        public DataSet GetBrandsudivision_wise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P  inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
            "where P.Division_Code='" + divcode + "' and " +
            "   p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            " OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
            " OR " +
            "  p.subdivision_code = '" + subdivision + "'" +

        "  group by b.Product_Brd_Code,b.Product_Brd_Name ";

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
        public DataSet Getproductsubdivision_wise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P  " +
            " where P.Division_Code='" + divcode + "' and P.Product_Active_Flag=0 and  " +
            "  ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            " OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
            " OR " +
            "  p.subdivision_code = '" + subdivision + "') " +

        "   group by P.Product_Detail_Name,p.Product_Detail_Code ";

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
        public DataSet Sales_Trend_analysis_categorywise(string category_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  " +
       " where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and s.date between '" + fromdate + "' and '" + todate + "'   group by b.Product_Cat_Code,b.Product_Cat_Name";

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
        public DataSet Sales_Trend_analysis_categorywise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = "  select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
          "where P.Division_Code='" + div_code + "'  and s.date between '" + fromdate + "' and '" + todate + "'   and " +
           " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
             " OR " +
             " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            "  OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
             " OR " +
            "  p.subdivision_code =   '" + subdivision + "')";

            }
            else
            {


                strQry = "  select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
          "where P.Division_Code='" + div_code + "'  and s.date between '" + fromdate + "' and '" + todate + "'   and " +
           " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
             " OR " +
             " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            "  OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
             " OR " +
            "  p.subdivision_code =   '" + subdivision + "')";
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
        public DataSet Sales_Trend_analysis_Brandwise(string brand_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'  and s.date between '" + fromdate + "' and '" + todate + "'   group by b.Product_Brd_Code,b.Product_Brd_Name ";

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
        public DataSet Sales_Trend_analysis_Productwise(string product_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value ,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code " +
                 " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and s.date between '" + fromdate + "' and '" + todate + "'   group by P.Product_Detail_Name,p.Product_Detail_Code ";

            }
            else
            {


                strQry = " select sum((s.Sale_Qty * s.Retailor_Rate)+(s.sale_pieces * s.RP_BaseRate))as value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code " +
                            " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and s.date between '" + fromdate + "' and '" + todate + "'   group by P.Product_Detail_Name,p.Product_Detail_Code ";
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
        public DataSet Sales_Trend_analysis_brandwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
          "where P.Division_Code='" + div_code + "'  and s.date between '" + fromdate + "' and '" + todate + "'   and " +
           " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
             " OR " +
             " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            "  OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
             " OR " +
            "  p.subdivision_code =   '" + subdivision + "')";
            }
            else
            {

                strQry = "select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
                     "where P.Division_Code='" + div_code + "'  and s.date between '" + fromdate + "' and '" + todate + "'   and " +
                      " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
                        " OR " +
                        " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
                       "  OR" +
                       "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
                        " OR " +
                       "  p.subdivision_code =   '" + subdivision + "')";

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

        public DataSet Sales_Trend_analysis_productwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code != "4")
            {

                strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code   " +
      "where P.Division_Code='" + div_code + "' and  Product_Active_Flag=0 and s.date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

            }
            else
            {
                strQry = "select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code   " +
                     "where P.Division_Code='" + div_code + "' and  Product_Active_Flag=0 and s.date between '" + fromdate + "' and '" + todate + "'   and " +
                      " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
                        " OR " +
                        " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
                       "  OR" +
                       "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
                        " OR " +
                       "  p.subdivision_code =   '" + subdivision + "')";
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


        public DataSet purchase_Trend_analysis_stockist(string stockistcode, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Rec_Qty * Distributer_Rate)as value from Trans_Stock_Updation_Details " +
     "where Purchase_Date between '" + fromdate + "' and '" + todate + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet Purchase_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(t.Rec_Qty  * t.Distributer_Rate)as value from Trans_Stock_Updation_Details t inner join mas_stockist s  on s.Stockist_Code=t.Stockist_Code where Purchase_Date between '" + fromdate + "' and '" + todate + "'  and s.subdivision_code='" + subdivision + "'";

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
        public DataSet Purchase_Trend_analysis_brandwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
      "where P.Division_Code='" + div_code + "'  and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet Purchase_Trend_analysis_productwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code   " +
      "where P.Division_Code='" + div_code + "' and  Product_Active_Flag=0 and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet Purchase_Trend_analysis_categorywise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
      "where P.Division_Code='" + div_code + "'  and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet Purchase_Trend_analysis_Brandwise(string brand_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty ) * (s.Distributer_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'  and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   group by b.Product_Brd_Code,b.Product_Brd_Name ";

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
        public DataSet Purchase_Trend_analysis_Productwise(string product_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value ,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code " +
             " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   group by P.Product_Detail_Name,p.Product_Detail_Code ";

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
        public DataSet Purchase_Trend_analysis_categorywise(string category_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  " +
       " where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and s.Purchase_Date between '" + fromdate + "' and '" + todate + "'   group by b.Product_Cat_Code,b.Product_Cat_Name";

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
        public DataSet retail_routewise_distribution_weekwise(string stockistcode, string div_code, string routecode, string product_code, int month, int year, int week)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(d.Quantity) Quantity,SUM(d.value) value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  DATEPART(WEEK, h.Order_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,Order_Date), 0))+ 1 ='" + week + "'";

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
        public DataSet Sales_Trend_analysis_categorywise_monthwise(string product_code, string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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
        public DataSet Sales_Trend_analysis_Brandwise_monthwise(string product_code, string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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
        public DataSet Sales_Trend_analysis_Brandwise_monthwise_total(string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "' ";

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
        public DataSet Sales_Trend_analysis_categorywise_monthwise_total(string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'";

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
        public DataSet Sales_Trend_analysis_stockist_monthwise_distribution(string productcode, string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Sale_Qty * Retailor_Rate)as value from Trans_Secondary_Sales_Details " +
      " where month(date)='" + month + "' and YEAR(date)='" + year + "' and  Stockist_code='" + distributor + "'and Product_Code='" + productcode + "'";

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
        public DataSet Sales_Trend_analysis_stockist_monthwise_distribution_total(string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Sale_Qty * Retailor_Rate)as value from Trans_Secondary_Sales_Details " +
      " where month(date)='" + month + "' and YEAR(date)='" + year + "' and  Stockist_code='" + distributor + "'";

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
        public DataSet Purchase_Trend_analysis_Brandwise_monthwise(string product_code, string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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
        public DataSet Purchase_Trend_analysis_Brandwise_monthwise_total(string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "' ";

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
        public DataSet Purchase_Trend_analysis_categorywise_monthwise_total(string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'";

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
        public DataSet Purchase_Trend_analysis_categorywise_monthwise(string product_code, string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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
        public DataSet Purchase_Trend_analysis_stockist_monthwise_distribution(string productcode, string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Rec_Qty  * Distributer_Rate)as value from Trans_Stock_Updation_Details " +
      " where month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and  Stockist_code='" + distributor + "'and Product_Code='" + productcode + "'";

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
        public DataSet Purchase_Trend_analysis_stockist_monthwise_distribution_total(string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Rec_Qty  * Distributer_Rate)as value from Trans_Stock_Updation_Details " +
      " where month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and  Stockist_code='" + distributor + "'";

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

        public DataSet Retail_Trend_analysis_totalvalue(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(h.Order_Value) from Trans_Order_Head h inner join mas_stockist s  on s.Stockist_Code=h.Stockist_Code where h.Order_Date between '" + fromdate + "' and '" + todate + "'  and s.subdivision_code='" + subdivision + "'";

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

        public DataSet Retail_Trend_analysis_categorywise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "   select sum(s.value)value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  " +
      "where P.Division_Code='" + div_code + "'  and h.Order_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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


        public DataSet Retail_Trend_analysis_brandwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(s.value)value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
      "where P.Division_Code='" + div_code + "'  and h.Order_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet Retail_Trend_analysis_Productwise(string product_code, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(s.value)value  from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No " +
             " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and h.Order_Date between '" + fromdate + "' and '" + todate + "'   group by P.Product_Detail_Name,p.Product_Detail_Code ";

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
        public DataSet Retail_Trend_analysis_Productwise_full_detail(string subdivision, string div_code, string fromdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select  P.Product_Detail_Name,p.Product_Detail_Code,sum(s.value)value,sum(s.net_weight*s.quantity) as net_weight_value  from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No " +
             " where P.Division_Code='" + div_code + "'  and  Product_Active_Flag=0   and " +
                 "  ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            " OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR" +
            "  p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
            " OR " +
            "  p.subdivision_code = '" + subdivision + "') and  " +
           "  h.Order_Date between '" + fromdate + "' and '" + todate + "'   group by P.Product_Detail_Name,p.Product_Detail_Code order by value desc ";

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
        public DataSet Retail_Trend_analysis_productwise_total_value(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum(s.value)value  from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No  " +
      "where P.Division_Code='" + div_code + "' and  Product_Active_Flag=0 and h.Order_Date between '" + fromdate + "' and '" + todate + "'   and " +
       " (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
         " OR " +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
        "  OR" +
        "  p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "')";

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

        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution(string productcode, string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No   " +
        "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "'and d.Product_Code='" + productcode + "'";

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
        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution_total(string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No  " +
        "where MONTH(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "'";

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
        public DataSet Retail_Trend_analysis_categorywise_monthwise(string product_code, string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(s.value)value,sum(s.net_weight * s.quantity) as net_weight_value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(h.Order_Date)='" + fmonth + "' and YEAR(h.Order_Date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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
        public DataSet Retail_Trend_analysis_Brandwise_monthwise(string product_code, string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(s.value)value,sum(s.net_weight * s.quantity) as net_weight_value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(h.Order_Date)='" + fmonth + "' and YEAR(h.Order_Date)='" + fyear + "'  and s.Product_Code='" + product_code + "' ";

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

        public DataSet Retail_Trend_analysis_categorywise_monthwise_total(string div_code, int fmonth, int fyear, string category_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(s.value)value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join  Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code  " +
    "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'";

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
        public DataSet Retail_Trend_analysis_Brandwise_monthwise_total(string div_code, int fmonth, int fyear, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select sum(s.value)value from Mas_Product_Detail P inner join Trans_Order_Details S ON s.Product_Code=P.Product_Detail_Code inner join Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "' ";

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

        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution_routee(string route, string div_code, int month, int year, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value,sum(d.net_weight * d.quantity)as net_weight_value  from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No   " +
        "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "'and h.Route='" + route + "'";

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

        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution_finaltotal(string productcode, string div_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No   " +
        "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and d.Product_Code='" + productcode + "'";

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
        public DataSet Gettop10value_category(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
                     " where P.Division_Code='" + divcode + "' group by b.Product_Cat_Code,b.Product_Cat_Name  ORDER BY VALUE DESC";

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
        public DataSet Gettop10value_Brand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
            "where P.Division_Code='" + divcode + "'  group by b.Product_Brd_Code,b.Product_Brd_Name  ORDER BY VALUE DESC ";

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
        public DataSet Gettop10value_Product(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code  " +
                     " where P.Division_Code='" + divcode + "'group by P.Product_Detail_Name,p.Product_Detail_Code ORDER BY VALUE DESC ";

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
        public DataSet Secondary_sales_product_value_daywisee(string divcode, int month, int year, string day, string product_code, string distributor, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            if (distributor == "ALL")
            {
                strQry = "select SUM((s.Op_Qty +(s.OP_Pieces*1.0/100)))opening,SUM((s.Rec_Qty + (s.Rec_Pieces*1.0/100))) as receipt_qty,sum((s.Cl_Qty+ (s.pieces*1.0/100))) as closing,sum((s.Sale_Qty + (s.sale_pieces*1.0/100))) as sale,(sum((s.Sale_Qty + (s.sale_pieces*1.0/100) )* s.Retailor_Rate)) as value  from Trans_Secondary_Sales_Details s inner join Mas_stockist st on s.Stockist_Code=st.Stockist_Code  where month(s.date)='" + month + "' and year(s.date)='" + year + "' and s.Product_Code='" + product_code + "'  and st.subdivision_code='" + subdivision + "' AND  CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%'";
            }
            else
            {
                strQry = " select SUM((s.Op_Qty + (s.OP_Pieces*1.0/100)))opening,SUM((s.Rec_Qty + (s.Rec_Pieces*1.0/100))) as receipt_qty,sum((s.Cl_Qty+ (s.pieces*1.0/100))) as closing,sum((s.Sale_Qty + (s.sale_pieces*1.0/100))) as sale,(sum((s.Sale_Qty + (s.sale_pieces*1.0/100) )* s.Retailor_Rate)) as value  from Trans_Secondary_Sales_Details s   where month(s.date)='" + month + "' and year(s.date)='" + year + "' and s.Product_Code='" + product_code + "' and Stockist_Code='" + distributor + "'  AND  CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%'";
            }

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Sales_Gettop10value_category(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select top 10 sum((s.Sale_Qty) * (s.Distributer_Rate))value,b.Product_Cat_Code,b.Product_Cat_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
                     " where P.Division_Code='" + divcode + "'group by b.Product_Cat_Code,b.Product_Cat_Name  ORDER BY VALUE DESC ";

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
        public DataSet Sales_Gettop10value_Brand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select top 10 sum((s.Sale_Qty) * (s.Distributer_Rate))value,b.Product_Brd_Code,b.Product_Brd_Name from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
                     " where P.Division_Code='" + divcode + "'group by b.Product_Brd_Code,b.Product_Brd_Name  ORDER BY VALUE DESC ";

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
        public DataSet Sales_Gettop10value_Product(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select  sum((s.Sale_Qty) * (s.Distributer_Rate))value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code  " +
                     " where P.Division_Code='" + divcode + "'group by P.Product_Detail_Name,p.Product_Detail_Code ORDER BY VALUE DESC ";

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

        public DataSet Retail_Trend_analysis_stockist_monthwise_distribution_finaltotal(string div_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value)as value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No  " +
        "where MONTH(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "'";

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
        public DataSet lost_Purchase_date(string productcode, int cmonth, int cyear, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max((purchase_date)) as MaxDate from Trans_Stock_Updation_Details where Product_Code='" + productcode + "' and MONTH(Purchase_Date)< '" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet lost_sale_date(string productcode, int cmonth, int cyear, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max((date)) as MaxDate from Trans_Secondary_Sales_Details where Product_Code='" + productcode + "' and MONTH(date)< '" + cmonth + "' and YEAR(date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet retail_routewise_non_available_retailer_name(string stockistcode, string div_code, string routecode, string product_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  ListedDrCode,ListedDr_Name  from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "')";

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
        public DataSet retail_routewise_available_retailer(string stockistcode, string div_code, string routecode, string product_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select COUNT( distinct h.Cust_Code) as retailer from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "'";

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

        public DataSet retail_routewise_non_available_retailer(string stockistcode, string div_code, string routecode, string product_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  count( distinct ListedDrCode)  from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "')";

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
        public DataSet Gettotal_retailers_per_poute(string divcode, string routecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select   count(distinct ListedDrCode)  from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + divcode + "' and ListedDr_Active_Flag=0  ";

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
        public DataSet Sales_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = "select * from(select sum(t.Sale_Qty * t.Retailor_Rate)as value ,Stockist_Code from Trans_Secondary_Sales_Details  t   where  t.date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code ) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
              "where subdivision_code='" + subdivision + "' ) dd  on se.Stockist_Code=dd.Stockist_Code group by se.Stockist_Code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc ";

            }
            else
            {
                strQry = "select * from(select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value ,Stockist_Code from Trans_Secondary_Sales_Details  t   where  t.date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code ) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
              "where subdivision_code='" + subdivision + "' ) dd  on se.Stockist_Code=dd.Stockist_Code group by se.Stockist_Code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc ";
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
        public DataSet Sales_Trend_analysis_categorywise_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {

                strQry = "select * from(select sum(t.Sale_Qty * t.Retailor_Rate)as value,r.Product_Cat_Code from Trans_Secondary_Sales_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.date between '" + fromdate + "' and '" + todate + "' group by r.Product_Cat_Code ) se RIGHT OUTER JOIN (select s.Product_Cat_Code as cat_code,s.Product_Cat_Name  from Mas_Product_Category s " +
             "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Cat_Code=dd.cat_code group by se.Product_Cat_Code,se.value,dd.Product_Cat_Name,dd.cat_code  order by value desc";

            }
            else
            {

                strQry = "select * from(select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value,r.Product_Cat_Code from Trans_Secondary_Sales_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.date between '" + fromdate + "' and '" + todate + "' group by r.Product_Cat_Code ) se RIGHT OUTER JOIN (select s.Product_Cat_Code as cat_code,s.Product_Cat_Name  from Mas_Product_Category s " +
                        "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Cat_Code=dd.cat_code group by se.Product_Cat_Code,se.value,dd.Product_Cat_Name,dd.cat_code  order by value desc";
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
        public DataSet Sales_Trend_analysis_Brandwise_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {
                strQry = "select * from(select sum(t.Sale_Qty * t.Retailor_Rate)as value,r.Product_Brd_Code from Trans_Secondary_Sales_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.date between '" + fromdate + "' and '" + todate + "' group by r.Product_Brd_Code ) se RIGHT OUTER JOIN (select s.Product_Brd_Code as Brand_code,s.Product_Brd_Name  from Mas_Product_Brand s " +
               "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Brd_Code=dd.Brand_code group by se.Product_Brd_Code,se.value,dd.Product_Brd_Name,dd.Brand_code  order by value desc ";

            }
            else
            {
                strQry = "select * from(select sum((t.Sale_Qty * t.Retailor_Rate)+(t.sale_pieces * t.RP_BaseRate))as value,r.Product_Brd_Code from Trans_Secondary_Sales_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.date between '" + fromdate + "' and '" + todate + "' group by r.Product_Brd_Code ) se RIGHT OUTER JOIN (select s.Product_Brd_Code as Brand_code,s.Product_Brd_Name  from Mas_Product_Brand s " +
               "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Brd_Code=dd.Brand_code group by se.Product_Brd_Code,se.value,dd.Product_Brd_Name,dd.Brand_code  order by value desc ";
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
        public DataSet Sales_Trend_analysis_Productwise_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            if (div_code != "4")
            {


                strQry = " select * from( select P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P where " +
               "(  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
                " OR" +
                " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
               "  OR" +
                " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
                " OR " +
                " p.subdivision_code =   '" + subdivision + "')and  P.Division_Code='" + div_code + "' and  Product_Active_Flag=0  )dd left outer join (select sum((Sale_Qty) * (Retailor_Rate))value,Product_Code from  Trans_Secondary_Sales_Details   where date between '" + fromdate + "' and '" + todate + "'  group by Product_Code) se ON se.Product_Code=dd.Product_Detail_Code " +
                "   group by dd.Product_Detail_Name,dd.Product_Detail_Code,se.Product_Code,se.value  order by value desc";

            }
            else
            {


                strQry = " select * from( select P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P where " +
                   "(  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
                    " OR" +
                    " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
                   "  OR" +
                    " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
                    " OR " +
                    " p.subdivision_code =   '" + subdivision + "')and  P.Division_Code='" + div_code + "' and  Product_Active_Flag=0  )dd left outer join (select sum((Sale_Qty * Retailor_Rate)+(sale_pieces * RP_BaseRate))as value,Product_Code from  Trans_Secondary_Sales_Details   where date between '" + fromdate + "' and '" + todate + "'  group by Product_Code) se ON se.Product_Code=dd.Product_Detail_Code " +
                    "   group by dd.Product_Detail_Name,dd.Product_Detail_Code,se.Product_Code,se.value  order by value desc";
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



        public DataSet Sales_Trend_analysis_Productwise_monthwise(string product_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value ,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code " +
             " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'    group by P.Product_Detail_Name,p.Product_Detail_Code";



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
        public DataSet retaill_Trend_analysis_Productwise_monthwise(string product_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(s.value)value ,sum(s.net_weight * s.quantity) as net_weight_value,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P  inner join  Trans_Order_Details s ON s.Product_Code=P.Product_Detail_Code inner join Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No  " +
             " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'    group by P.Product_Detail_Name,p.Product_Detail_Code";



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
        public DataSet Sales_Trend_analysis_Productwise_monthwise_total(string subdivision, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty) * (s.Retailor_Rate))value  from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code " +
             " where P.Division_Code='" + div_code + "' and   (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
          "OR" +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
         " OR " +
          "p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "') and  Product_Active_Flag=0   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'";



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
        public DataSet retaill_Trend_analysis_Productwise_monthwise_total(string subdivision, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum(s.value)value  from Mas_Product_Detail P inner join  Trans_Order_Details s ON s.Product_Code=P.Product_Detail_Code inner join Trans_Order_Head h on h.Trans_Sl_No=s.Trans_Sl_No  " +
             " where P.Division_Code='" + div_code + "' and   (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
          "OR" +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
         " OR " +
          "p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "') and  Product_Active_Flag=0   and  month(Order_Date)='" + fmonth + "' and YEAR(Order_Date)='" + fyear + "'";



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
        public DataSet Purchase_Trend_analysis_Productwise_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select * from( select P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P where " +
           "(  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            " OR" +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
           "  OR" +
            " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
            " OR " +
            " p.subdivision_code =   '" + subdivision + "')and  P.Division_Code='" + div_code + "' and  Product_Active_Flag=0  )dd left outer join (select sum((Rec_Qty ) * (Distributer_Rate))value,Product_Code from  Trans_Stock_Updation_Details " +
            "where Purchase_Date between '" + fromdate + "' and '" + todate + "'  group by Product_Code) se ON se.Product_Code=dd.Product_Detail_Code " +
            "   group by dd.Product_Detail_Name,dd.Product_Detail_Code,se.Product_Code,se.value  order by value desc";

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
        public DataSet Purchase_Trend_analysis_Brandwise_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.Rec_Qty * t.Distributer_Rate)as value,r.Product_Brd_Code from Trans_Stock_Updation_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' group by r.Product_Brd_Code ) se RIGHT OUTER JOIN (select s.Product_Brd_Code as Brand_code,s.Product_Brd_Name  from Mas_Product_Brand s " +
           "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Brd_Code=dd.Brand_code group by se.Product_Brd_Code,se.value,dd.Product_Brd_Name,dd.Brand_code  order by value desc ";

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
        public DataSet Purchase_Trend_analysis_category_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.Rec_Qty * t.Distributer_Rate)as value,r.Product_Cat_Code from Trans_Stock_Updation_Details  t  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' group by r.Product_Cat_Code ) se RIGHT OUTER JOIN (select s.Product_Cat_Code as cat_code,s.Product_Cat_Name  from Mas_Product_Category s " +
         "where Product_Cat_Div_Code='" + subdivision + "' ) dd  on se.Product_Cat_Code=dd.cat_code group by se.Product_Cat_Code,se.value,dd.Product_Cat_Name,dd.cat_code  order by value desc";

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
        public DataSet Purchase_Trend_analysis_Distributor_descending(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.Rec_Qty * t.Distributer_Rate)as value ,Stockist_Code from Trans_Stock_Updation_Details  t   where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
          "where subdivision_code='" + subdivision + "' ) dd  on se.Stockist_Code=dd.Stockist_Code group by se.Stockist_Code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc ";

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

        public DataSet rretail_Trend_analysis_stockist(string div_code, string fromdate, string todate, string subdivsion)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(Order_Value)as value ,sum(t.net_weight_value) as net_weight_value,Stockist_Code as distributor_code  from Trans_Order_Head  t   where  t.Order_Date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
       "where subdivision_code='" + subdivsion + "' ) dd  on se.distributor_code=dd.Stockist_Code group by se.distributor_code,se.value,se.net_weight_value,dd.Stockist_Name,dd.Stockist_Code  order by value desc";

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
        public DataSet Retail_Trend_analysis_categorywise(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select * from(select sum(t.value)as value,sum(t.net_weight * t.quantity) as net_weight_value,r.Product_Cat_Code from Trans_Order_Details t   inner join Trans_Order_Head h on h.Trans_Sl_No=t.Trans_Sl_No inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  h.Order_Date between '" + fromdate + "' and '" + todate + "' group by r.Product_Cat_Code ) se RIGHT OUTER JOIN (select s.Product_Cat_Code as cat_code,s.Product_Cat_Name  from Mas_Product_Category s " +
       "where Product_Cat_Div_Code='" + subdivision + "' and s.Product_Cat_Active_Flag=0 and s.Division_Code=" + div_code + " ) dd  on se.Product_Cat_Code=dd.cat_code group by se.Product_Cat_Code,se.value,se.net_weight_value,dd.Product_Cat_Name,dd.cat_code  order by value desc";

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
        public DataSet Retail_Trend_analysis_Brandwise(string div_code, string fromdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.value)as value ,sum(t.net_weight * t.quantity) as net_weight_value,r.Product_Brd_Code from Trans_Order_Details t  inner join Trans_Order_Head h on h.Trans_Sl_No=t.Trans_Sl_No  inner join Mas_Product_Detail r on r.Product_Detail_Code=t.Product_Code  where  h.Order_Date between '" + fromdate + "' and '" + todate + "' and r.Product_Active_Flag=0 group by r.Product_Brd_Code ) se RIGHT OUTER JOIN (select s.Product_Brd_Code as Brand_code,s.Product_Brd_Name  from Mas_Product_Brand s  " +
    "where Product_Cat_Div_Code='" + subdivision + "' and  s.Product_Brd_Active_Flag=0 and s.Division_Code='" + div_code + "')dd  on se.Product_Brd_Code=dd.Brand_code group by se.Product_Brd_Code,se.value,se.net_weight_value,dd.Product_Brd_Name,dd.Brand_code  order by value desc";

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
        public DataSet Sale_Trend_analysis_stockisttotal_monthwise_distribution(string distributor, string div_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Sale_Qty  * Retailor_Rate)as value from Trans_Secondary_Sales_Details  " +
       " where month(date)='" + month + "' and YEAR(date)='" + year + "' and  Stockist_code='" + distributor + "'";

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
        public DataSet sale_Trend_analysis_stockisttotal_monthwise_distribution_total(string div_code, int month, int year, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(t.Sale_Qty  * t.Retailor_Rate)as value from Trans_Secondary_Sales_Details t inner join mas_stockist s  on s.Stockist_Code=t.Stockist_Code  " +
      " where month(t.date)='" + month + "' and YEAR(t.date)='" + year + "' and s.subdivision_code='" + subdivision + "'";

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
        public DataSet Sale_Trend_analysis_categorywisevalue_monthwise(string category_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty ) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
       "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "' ";

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
        public DataSet sale_Trend_analysis_categorywisetotal_monthwise_total(string div_code, int fmonth, int fyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Sale_Qty ) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
      "where P.Division_Code='" + div_code + "'and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "' and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            "OR " +
           " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
           " OR " +
           " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
           " OR  " +
           " p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet sale_Trend_analysis_Brandwisevalue_monthwise(string brand_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'";

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
        public DataSet sale_Trend_analysis_Brandwisetotal_monthwise_total(string div_code, int fmonth, int fyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "'   and  month(date)='" + fmonth + "' and YEAR(date)='" + fyear + "'     and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
             "OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR " +
            " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
            " OR  " +
            " p.subdivision_code =   '" + subdivision + "')";
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
        public DataSet Purchase_Trend_analysis_stockisttotal_monthwise_distribution(string distributor, string div_code, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(Rec_Qty  * Distributer_Rate)as value from Trans_Stock_Updation_Details " +
       " where month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and  Stockist_code='" + distributor + "'";

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
        public DataSet Purchase_Trend_analysis_stockisttotal_monthwise_distribution_total(string div_code, int month, int year, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(t.Rec_Qty  * t.Distributer_Rate)as value from Trans_Stock_Updation_Details t inner join mas_stockist s  on s.Stockist_Code=t.Stockist_Code  " +
      " where month(t.Purchase_Date)='" + month + "' and YEAR(t.Purchase_Date)='" + year + "' and s.subdivision_code='" + subdivision + "'";

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
        public DataSet Purchase_Trend_analysis_categorywisevalue_monthwise(string category_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
       "where P.Division_Code='" + div_code + "' and p.Product_Cat_Code='" + category_code + "' and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "' ";

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
        public DataSet Purchase_Trend_analysis_categorywisetotal_monthwise_total(string div_code, int fmonth, int fyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code " +
      "where P.Division_Code='" + div_code + "'and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "' and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
            "OR " +
           " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
           " OR " +
           " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
           " OR  " +
           " p.subdivision_code =   '" + subdivision + "')";

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
        public DataSet Purchase_Trend_analysis_Brandwisevalue_monthwise(string brand_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "' and b.Product_Brd_Code='" + brand_code + "'   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'";

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
        public DataSet Purchase_Trend_analysis_Brandwisetotal_monthwise_total(string div_code, int fmonth, int fyear, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
             " where P.Division_Code='" + div_code + "'   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'     and ( p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
             "OR " +
            " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
            " OR " +
            " p.subdivision_code LIKE '%,' +  '" + subdivision + "' " +
            " OR  " +
            " p.subdivision_code =   '" + subdivision + "')";
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
        public DataSet Purchasee_Trend_analysis_Productwise_monthwise(string product_code, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value ,P.Product_Detail_Name,p.Product_Detail_Code from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code " +
             " where P.Division_Code='" + div_code + "' and   p.Product_Detail_Code='" + product_code + "' and  Product_Active_Flag=0   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "'    group by P.Product_Detail_Name,p.Product_Detail_Code";



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
        public DataSet Purchasee_Trend_analysis_Productwise_monthwise_total(string subdivision, string div_code, int fmonth, int fyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sum((s.Rec_Qty ) * (s.Distributer_Rate))value  from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code " +
             " where P.Division_Code='" + div_code + "' and   (  p.subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
          "OR" +
         " p.subdivision_code LIKE  '" + subdivision + "' + ',%'" +
         " OR " +
          "p.subdivision_code LIKE '%,' +  '" + subdivision + "'" +
         " OR " +
        "  p.subdivision_code =   '" + subdivision + "') and  Product_Active_Flag=0   and  month(Purchase_Date)='" + fmonth + "' and YEAR(Purchase_Date)='" + fyear + "' ";


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
        public DataSet Getorderdetail(string distributor, string route, string div_code, int FMonth, int FYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select distinct order_date, cust_code, listeddr_name, route, territory_name, sum(order_value) order_value,DCR_Code,sum(net_weight_value)net from " +
                      " (select CONVERT(VARCHAR (20),a.Order_Date,105)ORDER_DATE,a.Cust_Code,b.ListedDr_Name,a.Route,c.Territory_Name,a.Order_Value,a.DCR_Code,a.net_weight_value " +
                      " from Trans_Order_Head a join Mas_ListedDr b on a.Cust_Code=b.ListedDrCode join Mas_Territory_Creation c on a.Route=c.Territory_Code " +
                      " where a.Stockist_Code='" + distributor + "' and a.Route='" + route + "'and MONTH(a.Order_Date)='" + FMonth + "' and YEAR(a.Order_Date)='" + FYear + "') main group by ORDER_DATE,Cust_Code,ListedDr_Name,Route,Territory_Name,DCR_Code,net_weight_value";

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
        public DataSet Gettransno(string distributor, string DCR_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select a.Trans_Sl_No from Trans_Order_Head a where a.Stockist_Code='" + distributor + "' and a.DCR_Code='" + DCR_Code + "'";

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
        public DataSet Getorder(string tran_no)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select a.Trans_Sl_No,a.Product_Code,a.Product_Name,a.Quantity,a.value,a.net_weight,(a.net_weight*a.Quantity)netvalue from Trans_Order_Details a join Trans_Order_Head b on a.Trans_Sl_No=b.Trans_Sl_No where a.Trans_Sl_No='" + tran_no + "'";

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
        public DataSet retail_lostretail_non_available_retailer_view(string stockistcode, string div_code, string routecode, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select   distinct ListedDrCode,ListedDr_Name from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "')";

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
        public DataSet retail_lostretail_non_available_retailerlastdate(string stockistcode, string div_code, string routecode, int month, int year, string retailercode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " SELECT max( Order_Date) as MaxDate from Trans_Order_Head where MONTH(Order_Date)< '" + month + "' and YEAR(Order_Date)='" + year + "' and  Stockist_code='" + stockistcode + "' and Route='" + routecode + "' and Cust_Code='" + retailercode + "'";

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
        public DataSet retail_lostretail_non_available_retailer(string stockistcode, string div_code, string routecode, int month, int year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  count( distinct ListedDrCode)  from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and  month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "')";

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
        public DataSet DISGetsubdivisionwise(string sf_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            int sub_code = 0;
            strQry = "select subdivision_code from Mas_Stockist  where Stockist_Code='" + sf_code + "' and Division_Code='" + divcode + "' and Stockist_Active_Flag=0";
            sub_code = db_ER.Exec_Scalar(strQry);
            strQry = "select subdivision_code,subdivision_name from mas_subdivision where subdivision_code=" + sub_code + " and Div_Code='" + divcode + "' and SubDivision_Active_Flag=0";
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

        public DataSet GetStockist_subdivisionwise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = " SELECT 'ALL' as Stockist_code, '---ALL---' as Stockist_Name " +
                     " UNION  all " +

                "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Active_Flag=0 ";

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
        public DataSet DISGetStockist_subdivisionwise(string divcode, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name from Mas_Stockist where Division_Code='" + divcode + "' and Stockist_code='" + subdivision + "'";

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
        public DataSet DISGetStockName_Customer(string divcode, string subdivision, string dis)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Division_Code='" + divcode + "' and subdivision_code='" + subdivision + "' and Stockist_Code='" + dis + "'";

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
        public DataSet Secondary_sales_productdetail(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "SELECT P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit from Mas_Product_Detail P  where p.Division_Code='" + divcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet GetDistNamewise(string divcode, string sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " SELECT '0' as Stockist_code, '---ALL---' as Stockist_Name " +
                    " UNION " +
             " select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + divcode + "' and Field_Code='" + sub + "'";

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
        public DataSet GetDistNamewise1(string divcode, string sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + divcode + "' and Field_Code='" + sub + "'";

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
        public DataSet GetDistNamewise2(string divcode, string sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Division_Code ='" + divcode + "' and subdivision_code='" + sub + "'";

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
        public DataSet Secondary_sales_product_value_daywise(string divcode, int month, int year, string day, string product_code, string distributor, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            if (distributor == "ALL")
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int ,@rlquotient int,@rlremainder int ,@salepiecevalue varchar,@salevalue varchar " +
            "set @Remainder=null;" +
            "set @Quotient=null;" +
         "SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as sale, ROUND(((floor(dd.rlquotient)+dd.rlremainder)+ receipt_qty),2) as receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, (dd.salevalue+dd.salepiecevalue) as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty," +
         "salepiecevalue=sum(s.sale_pieces*s.Retailor_Rate),salevalue=sum(s.Sale_Qty*s.Retailor_Rate) " +
        " ,Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
       "opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ," +
        "clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),rlquotient=((SUM(s.Rec_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
    "rlremainder=(((cast(SUM(s.Rec_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100) " +
    "from Trans_Secondary_Sales_Details s inner join Mas_stockist st on  " +
    "s.Stockist_Code=st.Stockist_Code where month(s.date)='" + month + "' and year(s.date)='" + year + "' " +
    "and st.subdivision_code='" + subdivision + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' and s.Product_Code='" + product_code + "' group by s.Conversion_Qty)dd";
            }
            else
            {
                strQry = "Declare  @Quotient int, @Remainder int ,@opquotient int,@opremainder int,@clquotient int,@clremainder int ,@rlquotient int,@rlremainder int ,@salepiecevalue varchar,@salevalue varchar " +
            "set @Remainder=null;" +
            "set @Quotient=null;" +
         "SELECT  ROUND(((floor(dd.Quotient)+dd.Remainder)+sale),2) as sale, ROUND(((floor(dd.rlquotient)+dd.rlremainder)+ receipt_qty),2) as receipt_qty, ROUND(((floor(dd.opquotient)+dd.opremainder)+ dd.opening),2) as opening,ROUND(((floor(dd.clquotient)+dd.clremainder)+ dd.closing),2)as closing, (dd.salevalue+dd.salepiecevalue) as value  from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty," +
         "salepiecevalue=sum(s.sale_pieces*s.Retailor_Rate),salevalue=sum(s.Sale_Qty*s.Retailor_Rate) " +
        " ,Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),opquotient=((SUM(s.OP_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
       "opremainder=(((cast(SUM(s.OP_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),clquotient=((SUM(s.pieces))/(nullif(s.Conversion_Qty, 0))) ," +
        "clremainder=(((cast(SUM(s.pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),rlquotient=((SUM(s.Rec_Pieces))/(nullif(s.Conversion_Qty, 0))) ," +
    "rlremainder=(((cast(SUM(s.Rec_Pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100) " +
    "from Trans_Secondary_Sales_Details s  where month(s.date)='" + month + "' and year(s.date)='" + year + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' and s.Product_Code='" + product_code + "' and Stockist_Code='" + distributor + "' group by s.Conversion_Qty)dd";
            }

            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet get_nth_week(string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = "SELECT DATEPART( wk,'" + date + "')";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet get_weekdays(int weekno, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "declare	@year	int	= '" + year + "'," +
         "@week 	int 	='" + weekno + "'   " +

          "declare	@dte date  " +

          "select	@dte = dateadd(week, @week -1, dateadd(year, @year - 1900, 0)) " +

           "SELECT count([DATE])" +
           "FROM " +
              "(" +
       "SELECT	[DATE]	= DATEADD(DAY, n, dateadd(day, (datediff(day, '17530107', @dte) / 7) * 7, '17530107')) " +
         "FROM	(" +
           "	VALUES (0), (1), (2), (3), (4), (5), (6)" +
           ") num (n)" +
            ") d  " +
        "WHERE	[DATE]	>= dateadd(year, @year - 1900, 0)" +
        "AND	[DATE]	<= dateadd(year, @year - 1900 + 1,-1) and MONTH(DATE)='" + month + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet get_weekdays_date(int weekno, int year, int month)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "declare	@year	int	= '" + year + "'," +
         "@week 	int 	='" + weekno + "'   " +

          "declare	@dte date  " +

          "select	@dte = dateadd(week, @week -1, dateadd(year, @year - 1900, 0)) " +

           "SELECT [DATE]" +
           "FROM " +
              "(" +
       "SELECT	[DATE]	= DATEADD(DAY, n, dateadd(day, (datediff(day, '17530107', @dte) / 7) * 7, '17530107')) " +
         "FROM	(" +
           "	VALUES (0), (1), (2), (3), (4), (5), (6)" +
           ") num (n)" +
            ") d  " +
        "WHERE	[DATE]	>= dateadd(year, @year - 1900, 0)" +
        "AND	[DATE]	<= dateadd(year, @year - 1900 + 1,-1) and MONTH(DATE)='" + month + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_disributor_view_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and s.stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "'  AND   CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%' group by Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_Brandview_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor, string cat_code, string brand_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and P.Product_Brd_Code='" + brand_code + "' and P.Product_Cat_Code='" + cat_code + "'and  s.Stockist_code ='" + distributor + "' and s.Product_Code='" + product_code + "'  and    CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%' group by s.Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_categoryview_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "'  and YEAR(Purchase_Date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and    CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%' and P.Product_Cat_Code='" + cat_code + "' group by s.Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Territorywise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string territorycode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%'" +
        " group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet areawise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "'  and  U.Product_Code='" + product_code + "' and    CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%'" +
                "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet Statewise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
     "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "' and u.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%'" +
     "AND S.State_Code='" + state_code + "'and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";
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
        public DataSet zonewise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "'  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%'" +

          " group by Product_Code,Product_Name,Distributer_Rate   ";

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
        public DataSet sale_areawise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string areacode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE Z.Area_code='" + areacode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "'" +

       " and U.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' group by Product_Code,Product_Name,Distributer_Rate";

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


        public DataSet Sale_Statewise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string Product_code, string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
       "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(U.date)='" + cmonth + "'and year(U.date)='" + cyear + "'  and U.Product_Code='" + Product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%'" +
       " AND S.State_Code='" + statecode + "'and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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
        public DataSet sale_Territorywise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string day, string product_code, string territorycode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE S. Territory_Code='" + territorycode + "' AND  s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%'" +
          "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet sale_register_Branddetail_stockistwise_caterywise_daywise(string divcode, int Month, int Year, string day, string product_code, string stockistcode, string catcode, string brand)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where p.Division_Code='" + divcode + "'  and YEAR(date)='" + Year + "' and P.Product_Brd_Code='" + brand + "'and P.Product_Cat_Code='" + catcode + "'and  s.Stockist_code ='" + stockistcode + "' and s.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Brd_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sales_register_productdetail_daywise(string divcode, int Month, int Year, string day, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,SUM(S.Sale_Qty)Sale_Qty,(SUM(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "' and YEAR(date)='" + Year + "' and Stockist_code='" + distributor + "'  " +
       " and s.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sale_categoryview_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + month + "'  and YEAR(date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), date, 126) LIKE '" + day + "%' and P.Product_Cat_Code='" + cat_code + "' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Cat_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet getsto(string Mr_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select Stockist_Code from Mas_Stockist where Field_Code='" + Mr_code + "' and Division_Code='" + divcode + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_productdetail_routewise(string divcode, string stockist, int Year, int Month, string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select d.Product_Code,d.Product_Name,sum(d.Quantity) as Quantity ,sum(d.value) as value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No and month(h.Order_Date)='" + Month + "' and YEAR(h.Order_Date)='" + Year + "' and h.Stockist_Code='" + stockist + "' and h.route='" + route + "' group by d.Product_Code,d.Product_Name ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_register_productdetail_routewise_total(string divcode, int Year, int Month, string stockist)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select d.Product_Code,d.Product_Name,sum(d.Quantity) as Quantity,sum(d.value) as value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No and month(h.Order_Date)='" + Month + "' and YEAR(h.Order_Date)='" + Year + "' and h.Stockist_Code='" + stockist + "' group by d.Product_Code,d.Product_Name  ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet GetStockName_Terrritoryy(string divcode, string territory_code, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Division_Code='" + divcode + "'and Territory_Code='" + territory_code + "'  and subdivision_code='" + subdivision + "'";

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
        public DataSet retail_routewise_distributorwise_retailer(string stockistcode, string div_code, string routecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  distinct ListedDrCode,ListedDr_Name,Milk_Potential from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0 ";
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
        public DataSet retail_routewise_distriretailer_quantity(string stockistcode, string div_code, string retailer, string routecode, string product_code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.quantity) from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Cust_Code='" + retailer + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and d.Product_Code='" + product_code + "'and CONVERT(VARCHAR(25), h.Order_Date, 126) LIKE '" + date + "%'";

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
        public DataSet retail_routewise_distriretailer_value(string stockistcode, string div_code, string retailer, string routecode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(d.value) from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Cust_Code='" + retailer + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and CONVERT(VARCHAR(25), h.Order_Date, 126) LIKE '" + date + "%'";

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
        public DataSet purchase_disributor_view_daywise_details_total(string divcode, int month, int year, string day, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "' and s.Product_Code='" + product_code + "' and   CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + day + "%' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_Brandview_daywise_details_total(string divcode, int month, int year, string date, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Brd_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "' and YEAR(Purchase_Date)='" + year + "'  and P.Product_Cat_Code='" + cat_code + "'and  s.Stockist_code ='" + distributor + "' and s.Product_Code='" + product_code + "' and CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + date + "%' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Brd_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet purchase_categoryview_daywise_details_total(string divcode, int month, int year, string date, string product_code, string distributor, string cat_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Rec_Qty)Rec_Qty,(sum(Rec_Qty) * (Distributer_Rate))VALUE from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where s.Division_Code='" + divcode + "' and month(Purchase_Date)='" + month + "'  and YEAR(Purchase_Date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and  CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + date + "%'  group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Distributer_Rate,p.Product_Cat_Code ";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Statewise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S " +
    "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(Purchase_Date)='" + cmonth + "'and year(Purchase_Date)='" + cyear + "' and u.Product_Code='" + product_code + "'   and CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + date + "%'" +
 "and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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
        public DataSet areawise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE   s.Division_Code='" + div_code + "' and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "'  and  U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + date + "%' " +
                "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet zonewise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,Distributer_Rate,sum(Rec_Qty * Distributer_Rate) value from Trans_Stock_Updation_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE   s.Division_Code='" + div_code + "'  and MONTH(Purchase_Date)='" + cmonth + "' and year(Purchase_Date)='" + cyear + "' and  U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), Purchase_Date, 126) LIKE '" + date + "%'" +

          " group by Product_Code,Product_Name,Distributer_Rate   ";

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
        public DataSet sale_Territorywise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE   s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%'" +
          "group by Product_Code,Product_Name,Distributer_Rate ";

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

        public DataSet Territorywise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Rec_Qty)Rec_Qty,Product_Name,sum(Rec_Qty * Distributer_Rate) value from Trans_stock_updation_details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE   s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%'" +
          "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet sales_register_productdetail_total_daywise(string divcode, int Month, int Year, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Detail_Code,P.Product_Detail_Name,P.Product_Sale_Unit,SUM(S.Sale_Qty)Sale_Qty,(SUM(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code where p.Division_Code='" + divcode + "' and month(date)='" + Month + "' and YEAR(date)='" + Year + "' " +
" and s.Product_Code='" + product_code + "' and  P.Product_Active_Flag=0  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%' group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet sale_categoryview_daywise_details_total(string divcode, int month, int year, string date, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = " select P.Product_Cat_Code,P.Product_Detail_Name,P.Product_Sale_Unit,sum(S.Sale_Qty)Sale_Qty,(sum(Sale_Qty) * (Retailor_Rate))VALUE from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code where p.Division_Code='" + divcode + "' and month(date)='" + month + "'  and YEAR(date)='" + year + "'   and s.Stockist_code='" + distributor + "' and s.Product_Code='" + product_code + "' and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%'  group by p.Product_Detail_Code,p.Product_Detail_Name,p.Product_Sale_Unit,s.Retailor_Rate,p.Product_Cat_Code";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet Sale_Statewise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string Product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  " +
      "ON U.Stockist_code=S.Stockist_Code  WHERE  MONTH(U.date)='" + cmonth + "'and year(U.date)='" + cyear + "'  and U.Product_Code='" + Product_code + "'  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%'" +
    " and s.Division_Code='" + div_code + "'  group by Product_Code,Product_Name,Distributer_Rate";

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
        public DataSet sale_areawise_purchase_productdetail_total_daywise(string div_code, int cmonth, int cyear, string date, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code INNER JOIN Mas_Area A ON A.Area_code=Z.Area_code WHERE   s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "'" +

" and U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%' group by Product_Code,Product_Name,Distributer_Rate    ";

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
        public DataSet sale_zonewise_purchase_productdetail_daywise(string div_code, int cmonth, int cyear, string date, string product_code, string zonecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S ON U.Stockist_code=S.Stockist_Code inner join Mas_Territory T on T.Territory_code=S.Territory_Code INNER JOIN Mas_Zone Z ON Z.Zone_code=T.Zone_code WHERE T.Zone_code='" + zonecode + "' AND  s.Division_Code='" + div_code + "'  and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "'  and CONVERT(VARCHAR(25), date, 126) LIKE '" + date + "%' " +
 "group by Product_Code,Product_Name,Distributer_Rate  ";

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
        public DataSet sale_Territorywise_purchase_productdetail_total(string div_code, int cmonth, int cyear, int week, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Product_Code,sum(u.Sale_Qty)Sale_Qty,Product_Name,Distributer_Rate,sum(Sale_Qty * Retailor_Rate) value from Trans_Secondary_Sales_Details  U INNER JOIN Mas_Stockist S  ON U.Stockist_code=S.Stockist_Code  WHERE   s.Division_Code='" + div_code + "' and MONTH(date)='" + cmonth + "' and year(date)='" + cyear + "' and  U.Product_Code='" + product_code + "' and  DATEPART(WEEK, U.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,U.date), 0))+ 1 ='" + week + "'" +
          "group by Product_Code,Product_Name,Distributer_Rate ";

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
        public DataSet GetStockName_Terrritory(string divcode, string territory_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select  Stockist_code,Stockist_Name,Distributor_Code from Mas_Stockist where Division_Code='" + divcode + "'and Territory_Code='" + territory_code + "'";

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
        public DataSet primary_Purchase_Distributor(string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select round(isnull(sum((isnull(Rec_Qty,0) * isnull(Distributer_Rate,0))+(isnull(Rec_Pieces,0)*isnull(DP_BaseRate,0))),0),2) value  from Trans_Stock_Updation_Details where Division_Code='" + div_code + "'and month(Purchase_Date)='" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "'";

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
        public DataSet secondary_Purchase_Distributor(string div_code, int cmonth, int cyear, string cdate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select round(isnull(sum((isnull(Sale_Qty,0) * isnull(Retailor_Rate,0))+(isnull(sale_pieces,0)*isnull(RP_BaseRate,0))),0),2) value from Trans_Secondary_Sales_Details a inner join Mas_Stockist b  on a.Stockist_Code=b.Stockist_Code  where b.Division_Code='" + div_code + "' and month(date)='" + cmonth + "' and YEAR(date)='" + cyear + "'";

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

        public DataSet salesmandaily_call_report(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select A.Sf_Code,A.Sf_Name,a.WorkType_Name,m.Trans_Detail_Name,m.SDP_Name,m.Trans_Detail_Info_Code,a.Activity_Date,m.Activity_Remarks,m.tm,(select g.stockist_name from Mas_stockist g where g.stockist_code=m.stockist_code)as stockist_name,l.Doc_Spec_ShortName,l.ListedDr_Address1,case when isnull( M.POB_Value,0)>0 then'Productive' else 'Visited' end as activity from [vwActivity_MSL_Details] M inner join [vwActivity_Report] A  on M.Trans_SlNo=A.Trans_SlNo and m.Division_Code=a.Division_Code  inner join Mas_ListedDr l on l.ListedDrCode=m.Trans_Detail_Info_Code  where a.sf_code='" + sfcode + "' and a.Division_Code='" + div_code + "'" +
          "and CONVERT(VARCHAR(25), a.Activity_Date, 126) LIKE '" + date + "%' ORDER BY M.Time";

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

        public DataSet salesmandaily_call_report_trans_order(string div_code, string sfcode, string date, string retailer_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "  select  sum(Order_Value)Order_Value,sum(net_weight_value)net_weight_value from Trans_Order_Head   where  Sf_code='" + sfcode + "' and  CONVERT(VARCHAR(25), Order_Date, 126)  like '" + date + "%'  and Cust_Code='" + retailer_code + "'";

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

        public DataSet salesmandaily_call_report_time(string div_code, string sfcode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  m.tm,A.Sf_Code,A.Sf_Name,m.Trans_Detail_Name,a.Activity_Date,case when isnull( M.POB_Value,0)>0 then'Productive' else 'Visited' end as activity from [vwActivity_MSL_Details] M inner join [vwActivity_Report] A  on M.Trans_SlNo=A.Trans_SlNo and m.Division_Code=a.Division_Code  inner join Mas_ListedDr l on l.ListedDrCode=m.Trans_Detail_Info_Code  where a.sf_code='" + sfcode + "' and a.Division_Code='" + div_code + "'" +
          "and CONVERT(VARCHAR(25), a.Activity_Date, 126) LIKE '" + date + "%' ORDER BY M.Time";

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
        public DataSet feildforceelist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select * from Mas_Salesforce  where  ( Division_Code LIKE '%,' + '" + divcode + "' + ',%' " +
      " OR " +
       " Division_Code LIKE  '" + divcode + "' + ',%' " +
       "OR " +
        "Division_Code LIKE '%,' +  '" + divcode + "' " +
        "OR " +
      "Division_Code =   '" + divcode + "')";

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
        public DataSet feildforceelist_SF(string divcode, string sub)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            strQry = "select * from Mas_Salesforce  where  ( Division_Code LIKE '%,' + '" + divcode + "' + ',%' " +
       " OR " +
       " Division_Code LIKE  '" + divcode + "' + ',%' " +
       "OR " +
        "Division_Code LIKE '%,' +  '" + divcode + "' " +
        "OR " +
       "Division_Code =   '" + divcode + "')and sf_TP_Active_Flag=0 and ( subdivision_code LIKE '%,' + '" + sub + "' + ',%' " +
       " OR " +
       " subdivision_code LIKE  '" + sub + "' + ',%' " +
       "OR " +
        "subdivision_code LIKE '%,' +  '" + sub + "' " +
        "OR " +
       "subdivision_code =   '" + sub + "')";

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
        public DataSet daily_Trans_Order_primary_value(string div_code, string product_code, string date, string stockist_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select (((sum(Rec_Qty)* Conversion_Qty))+ sum(pieces))as Rec_QTY from Trans_Stock_Updation_Details  where Stockist_code='" + stockist_code + "' and Product_Code='" + product_code + "' and Division_Code='" + div_code + "' group by Conversion_Qty";

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
        public DataSet daily_Trans_Order_vs_primary(string div_code, string sfcode, string date, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select s.Stockist_Code,s.Stockist_Name,sum(d.Quantity)as Quantity,d.Product_Code,d.Product_Name from Trans_Order_Head h inner join Trans_Order_Details d on d.Trans_Sl_No=h.Trans_Sl_No   inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code " +
                   "where  CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + date + "%' and h.SF_Code='" + sfcode + "' and s.Division_Code='" + div_code + "' and s.subdivision_code='" + subdivision + "' group by s.Stockist_Name,d.Product_Name,d.Product_Code,s.Stockist_Code order by s.Stockist_Name,Product_Name";

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
        public DataSet get_Product_conversion_qty(string div_code, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select Sample_Erp_Code from Mas_Product_Detail where Product_Detail_Code='" + product_code + "'";

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
        public DataSet get_case_qty_order_vs_primary(int qty_value, string con_qty)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT  ROUND((round(dd.Quotient,0)+dd.Remainder),2) as saleqty from (select Quotient=((SUM(" + qty_value + "))/" + con_qty + ") ,Remainder=(((cast(SUM(" + qty_value + ") as int))%cast((" + con_qty + ")as int))*1.0/100) )dd";

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
        public DataSet Getsalesforceli(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select * from Mas_Salesforce  where  ( subdivision_code LIKE '%,' + '" + divcode + "' + ',%' " +
      " OR " +
       " subdivision_code LIKE  '" + divcode + "' + ',%' " +
       "OR " +
        "subdivision_code LIKE '%,' +  '" + divcode + "' " +
        "OR " +
      "subdivision_code =   '" + divcode + "')";

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
        public DataSet GetDistNamewise_MR(string divcode, string sub, string MR_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select Stockist_Code,Stockist_Name from Mas_Stockist where Stockist_Active_Flag=0 and Field_Code='" + MR_code + "' and Division_Code ='" + divcode + "' and subdivision_code='" + sub + "'";

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
        public DataSet Sales_Trend_analysis_Distributor_descending_MR(string div_code, string fromdate, string todate, string subdivision, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.Sale_Qty * t.Retailor_Rate)as value ,Stockist_Code from Trans_Secondary_Sales_Details  t   where  t.date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code ) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
          "where  Field_Code='" + sf_code + "' and Division_Code ='" + div_code + "' and subdivision_code='" + subdivision + "' ) dd  on se.Stockist_Code=dd.Stockist_Code group by se.Stockist_Code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc ";

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
        public DataSet Purchase_Trend_analysis_Distributor_descending_MR(string div_code, string fromdate, string todate, string subdivision, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(t.Rec_Qty * t.Distributer_Rate)as value ,Stockist_Code from Trans_Stock_Updation_Details  t   where  t.Purchase_Date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
          "where  Field_Code='" + sfcode + "' and Division_Code ='" + div_code + "' and subdivision_code='" + subdivision + "' ) dd  on se.Stockist_Code=dd.Stockist_Code group by se.Stockist_Code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc ";

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
        public DataSet Gettop10value_stockist_MR(string divcode, string year, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   SELECT  SUM((Rec_Qty)*(Distributer_Rate))value,T.Stockist_code,S.Stockist_Name  FROM Trans_Stock_Updation_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where s.Field_Code='" + sfcode + "' and s.Division_Code='" + divcode + "'  and year(T.Purchase_Date)='" + year + "' group by T.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC ";

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
        public DataSet Sales_Gettop10value_stockist_MR(string divcode, string year, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "SELECT  SUM((Sale_Qty)*(Distributer_Rate))value,T.Stockist_code,S.Stockist_Name  FROM Trans_Secondary_Sales_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where s.Field_Code='" + sfcode + "' and s.Division_Code='" + divcode + "'  and year(T.date)='" + year + "' group by T.Stockist_code,s.Stockist_Name  ORDER BY VALUE DESC ";

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
        public DataSet rretail_Trend_analysis_stockist_MR(string div_code, string fromdate, string todate, string subdivsion, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select * from(select sum(Order_Value)as value ,Stockist_Code as distributor_code  from Trans_Order_Head  t   where  t.Order_Date between '" + fromdate + "' and '" + todate + "' group by Stockist_Code ) se RIGHT OUTER JOIN (select s.Stockist_Name, s.Stockist_Code  from Mas_Stockist s " +
              "where Field_Code='" + sfcode + "' and Division_Code ='" + div_code + "' and subdivision_code='" + subdivsion + "' ) dd  on se.distributor_code=dd.Stockist_Code group by se.distributor_code,se.value,dd.Stockist_Name,dd.Stockist_Code  order by value desc";

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
        public DataSet lost_Purchase_date_sel_month(string productcode, string cmonth, string cyear, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max((purchase_date)) as MaxDate from Trans_Stock_Updation_Details where Product_Code='" + productcode + "' and MONTH(Purchase_Date)< '" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet lost_Purchase_value_view_sel_month(string stockistcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT Product_Detail_Name,Product_Detail_Code   FROM Mas_Product_Detail WHERE (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
        " OR " +
        "  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
         " OR " +
          "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
          " OR " +
         " subdivision_code =   '" + subdivision + "') and   Product_Active_Flag=0  and  Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Stock_Updation_Details where Stockist_code='" + stockistcode + "' and Purchase_Date >  '" + fdate + "' and Purchase_Date <'" + todate + "') and Division_Code='" + div_code + "'   ";

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
        public DataSet lost_Purchase_value_stockist_purchase(string stockistcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT count(Product_Detail_Name)  FROM Mas_Product_Detail  WHERE (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
  " OR " +
  "  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
   " OR " +
    "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
    " OR " +
   " subdivision_code =   '" + subdivision + "') and   Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Stock_Updation_Details where Stockist_code='" + stockistcode + "' and Purchase_Date > '" + fdate + "' and Purchase_Date <'" + todate + "') " +
    " and Division_Code='" + div_code + "' and Product_Active_Flag=0";

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
        public DataSet Sales_lost_Purchase_value_stockist_sel_month(string stockistcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT count(Product_Detail_Name)  FROM Mas_Product_Detail WHERE Product_Active_Flag=0 and  (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
     " OR " +
     "  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
     " OR " +
     "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
     " OR " +
     " subdivision_code =   '" + subdivision + "') and  Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Secondary_Sales_Details where Stockist_code='" + stockistcode + "' and date > '" + fdate + "' and date <'" + todate + "') and Division_Code='" + div_code + "'";

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
        public DataSet retail_lostretail_non_available_retailer_sel_month(string stockistcode, string div_code, string routecode, string fdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select  count( distinct ListedDrCode)  from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and h.Order_Date > '" + fdate + "' and h.Order_Date <'" + todate + "')";

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
        public DataSet retail_lostretail_non_available_retailer_view_sel_month(string stockistcode, string div_code, string routecode, string fdate, string todate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select   distinct ListedDrCode,ListedDr_Name from Mas_ListedDr where Territory_Code='" + routecode + "' and Division_Code='" + div_code + "' and ListedDr_Active_Flag=0  and ListedDrCode not in (select distinct h.Cust_Code from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and h.Order_Date >  '" + fdate + "' and h.Order_Date <'" + todate + "' )";

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
        public DataSet retail_lostretail_non_available_retailerlastdate_sel_month(string stockistcode, string div_code, string routecode, string month, string year, string retailercode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max( Order_Date) as MaxDate from Trans_Order_Head where MONTH(Order_Date)< '" + month + "' and YEAR(Order_Date)='" + year + "' and  Stockist_code='" + stockistcode + "' and Route='" + routecode + "' and Cust_Code='" + retailercode + "'";

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

        public DataSet Sales_lost_Purchase_value_view_sel_month(string stockistcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT Product_Detail_Name,Product_Detail_Code FROM Mas_Product_Detail WHERE  Product_Active_Flag=0 and (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%' " +
      " OR " +
      "  subdivision_code LIKE  '" + subdivision + "' + ',%'" +
      " OR " +
      "subdivision_code LIKE '%,' +  '" + subdivision + "'" +
      " OR " +
      " subdivision_code =   '" + subdivision + "') and   Product_Detail_Name NOT IN  (SELECT Product_Name  FROM Trans_Secondary_Sales_Details where Stockist_code='" + stockistcode + "' and date > '" + fdate + "' and date <'" + todate + "') and Division_Code='" + div_code + "' ";



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
        public DataSet lost_sale_date_sel_month(string productcode, string cmonth, string cyear, string stockistcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max((date)) as MaxDate from Trans_Secondary_Sales_Details where Product_Code='" + productcode + "' and MONTH(date)< '" + cmonth + "' and YEAR(date)='" + cyear + "' and  Stockist_code='" + stockistcode + "'";

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
        public DataSet retail_Gettop10value_route_total(string divcode, string year, string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            //if (route == "0")
            //{
            //    strQry = "SELECT t.ListedDr_Name, SUM(x.Order_Value)value,x.Cust_Code  FROM   Trans_Order_Head x  inner join Mas_ListedDr t on x.Cust_Code=t.ListedDrCode   where  year(x.Order_Date)='" + year + "' group by x.Cust_Code,t.ListedDr_Name ORDER BY VALUE DESC";
            //}
            //else
            //{

            strQry = "SELECT  SUM(x.Order_Value)value  FROM   Trans_Order_Head x  inner join Mas_ListedDr t on x.Cust_Code=t.ListedDrCode   where  year(x.Order_Date)='" + year + "' and x.Route='" + route + "'";

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
        public DataSet Gettop10value_category_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = " select Top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
                     " where s.Division_Code='" + divcode + "'  and year(s.Purchase_Date)='" + year + "'";

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
        public DataSet Gettop10value_Brand_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "  select Top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
            "where s.Division_Code='" + divcode + "'  and year(s.Purchase_Date)='" + year + "'";

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
        public DataSet Gettop10value_Product_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   select Top 10 sum((s.Rec_Qty) * (s.Distributer_Rate))value from Mas_Product_Detail P inner join Trans_Stock_Updation_Details S ON s.Product_Code=P.Product_Detail_Code  " +
       "where s.Division_Code='" + divcode + "' and year(s.Purchase_Date)='" + year + "'";

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
        public DataSet Gettop10value_stockist_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   SELECT  SUM((Rec_Qty)*(Distributer_Rate))value  FROM Trans_Stock_Updation_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where T.Division_Code='" + divcode + "'  and year(T.Purchase_Date)='" + year + "'";

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
        public DataSet Sales_Gettop10value_stockist_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   SELECT  SUM((Sale_Qty)*(Retailor_Rate))value  FROM Trans_Secondary_Sales_Details T INNER JOIN Mas_Stockist S  ON  S.Stockist_Code=T.Stockist_code where s.Division_Code='" + divcode + "'  and year(T.date)='" + year + "'";

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
        public DataSet Sales_Gettop10value_category_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            if (divcode != "4")
            {
                strQry = " select  sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
           " where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "'";
            }
            else
            {

                strQry = " select  sum((s.Sale_Qty * s.Retailor_Rate)+(s.sale_pieces * s.RP_BaseRate))as value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Category B on  B.Product_Cat_Code=P.Product_Cat_Code" +
                     " where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "'";
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
        public DataSet Sales_Gettop10value_Brand_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;


            if (divcode != "4")
            {

                strQry = "  select  sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
                "where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "'";

            }
            else
            {
                strQry = "  select  sum((s.Sale_Qty * s.Retailor_Rate)+(s.sale_pieces * s.RP_BaseRate))as value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code inner join Mas_Product_Brand B on  B.Product_Brd_Code=P.Product_Brd_Code " +
                "where P.Division_Code='" + divcode + "'  and year(s.date)='" + year + "'";
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
        public DataSet Sales_Gettop10value_Product_total(string divcode, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "   select  sum((s.Sale_Qty) * (s.Retailor_Rate))value from Mas_Product_Detail P inner join Trans_Secondary_Sales_Details S ON s.Product_Code=P.Product_Detail_Code  " +
      "where P.Division_Code='" + divcode + "' and year(s.date)='" + year + "'";

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
        public DataSet GetRetailerName_Distributorwise(string dis_code, string routecode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select ListedDrCode,ListedDr_Name from Mas_ListedDr d inner join Mas_Territory_Creation t on t.Territory_Code=d.Territory_Code " +
            "inner join Mas_Stockist s on s.Stockist_Code=t.Dist_Name where s.Stockist_Code='" + dis_code + "' and d.Territory_Code='" + routecode + "' and s.Division_Code='" + divcode + "'";

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
        public DataSet Retail_Trend_analysis_retail_valuee(string route, string div_code, int month, int year, string distributor, string retailer)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(d.value)as value,sum(d.net_weight * d.quantity) as net_weight_value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No " +
            "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "' and h.Route='" + route + "' and h.Cust_Code='" + retailer + "'";

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
        public DataSet Retail_Trend_analysis_retailer_value_total(string div_code, int month, int year, string distributor, string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select SUM(d.value)as value from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No " +
               "where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and  h.Stockist_code='" + distributor + "' and h.Route='" + route + "'";

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
        public DataSet Secondary_sales_productdetail_singleproduct(string divcode, string subdivision, string product_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "SELECT P.Product_Detail_Code, P.Product_Detail_Name,P.Product_Sale_Unit from Mas_Product_Detail P  where p.Division_Code='" + divcode + "'  and Product_Detail_Code='" + product_code + "' and  " +
      "  (subdivision_code LIKE '%,' + '" + subdivision + "' + ',%'" +
      " OR" +
      " subdivision_code LIKE  '" + subdivision + "' + ',%'" +
      "OR" +
      " subdivision_code LIKE '%,' +  '" + subdivision + "' " +
      "  OR " +
      "  subdivision_code =   '" + subdivision + "')";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet getFW(string sfcode, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = " select FieldWork_Indicator from DCRDetail_Distributors_Hunting a inner join DCRMain_Trans b on a.Trans_SlNo=b.Trans_SlNo " +
                    " where b.Division_Code='" + Div_code + "' and b.Sf_Code='" + sfcode + "'  ";

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
        public DataSet GetTable(string sfcode, string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "  select area,Shop_Name,Contact_Person,Phone_Number,a.Remarks,Convert(varchar(10),CONVERT(date,a.Submission_Date,106),103) as Date from DCRDetail_Distributors_Hunting a inner join DCRMain_Trans b on a.Trans_SlNo=b.Trans_SlNo " +
                     "  where b.Division_Code='" + Div_code + "' and b.Sf_Code='" + sfcode + "' ";

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
        public DataSet CheckSFDis(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Stockist_Code,Stockist_Name,subdivision_code from  Mas_Stockist a  where a.Field_Code='" + sf_code + "' and a.Division_Code='" + div_code + "' and  a.Stockist_Active_Flag=0";
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
        public DataSet Secondary_sales_product_value(string divcode, int month, int year, int week, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "Declare  @Quotient int, @Remainder int " +
  "set @Remainder=null;" +
   "set @Quotient=null;" +
"SELECT  ROUND(((round(dd.Quotient,0)+dd.Remainder)+sale),2) as saleqty,dd.receipt_qty,dd.opening,dd.closing, ((ROUND(((round(dd.Quotient,0)+dd.Remainder)+sale),2))*dd.Retailor_Rate)as value from (select SUM(s.Op_Qty)opening,SUM(s.Rec_Qty) as receipt_qty,sum(s.Cl_Qty) as closing,sum(s.Sale_Qty) as sale,s.Conversion_Qty,SUM(s.sale_pieces)as salepieces,Quotient=((SUM(s.sale_pieces))/(nullif(s.Conversion_Qty, 0))) ,Remainder=(((cast(SUM(s.sale_pieces) as int))%cast((nullif(s.Conversion_Qty, 0))as int))*1.0/100),s.Retailor_Rate from Trans_Secondary_Sales_Details s where month(s.date)='" + month + "' and year(s.date)='" + year + "' and  DATEPART(WEEK, s.date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,s.date), 0))+ 1 ='" + week + "'and s.Product_Code='" + product_code + "' and Stockist_Code='" + distributor + "' group by s.Conversion_Qty,s.Retailor_Rate)dd";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet retail_lostretail_total_retailers(string stockistcode, string div_code, string routecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  count( distinct D.ListedDrCode)  from Mas_ListedDr D INNER JOIN   " +
               "mAS_TERRITORY_CREATION T ON T.Territory_Code=D.Territory_Code " +
            " where D.Territory_Code='" + routecode + "' and D.Division_Code='" + div_code + "' and T.Dist_Name ='" + stockistcode + "' AND D.ListedDr_Active_Flag=0 and T.Territory_Active_Flag=0";

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
        public DataSet retail_routewise_distriretailer_netvalue(string stockistcode, string div_code, string retailer, string routecode, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select SUM(h.net_weight_value) from Trans_Order_Head h inner join Trans_Order_Details d on h.Trans_Sl_No=d.Trans_Sl_No inner join Mas_Stockist s on s.Stockist_Code=h.Stockist_Code where  h.Route='" + routecode + "' and h.Cust_Code='" + retailer + "' and h.Stockist_Code='" + stockistcode + "' and s.Division_Code='" + div_code + "' and CONVERT(VARCHAR(25), h.Order_Date, 126) LIKE '" + date + "%'";

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

        public DataSet lost_Purchasedistributor_date(string stockistcode, int cmonth, int cyear, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "SELECT max(purchase_date) as MaxDate from Trans_Stock_Updation_Details where MONTH(Purchase_Date)< '" + cmonth + "' and YEAR(Purchase_Date)='" + cyear + "' and  Stockist_code='" + stockistcode + "' and sfcode='" + sfcode + "' and (Rec_Pieces!='' or Rec_Qty!='')";

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
        public DataSet lost_distributor_value_view(string sfcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select  DISTINCT Stockist_Code,Stockist_Name  from Mas_stockist  " +
                       " WHERE subdivision_code ='" + subdivision + "' and    Field_code='" + sfcode + "' and   " +
                       " Stockist_Code NOT IN  (SELECT DISTINCT Stockist_Code   " +
                       " FROM Trans_Stock_Updation_Details  " +
                       " where Purchase_Date > '" + fdate + "' and Purchase_Date <'" + todate + "' and sfcode='" + sfcode + "' and (Rec_Pieces!='' or Rec_Qty!='')) " +
                       " and Division_Code='" + div_code + "' and  Stockist_Active_Flag=0";

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
        public DataSet lost_value_stockist_purch(string sfcode, string div_code, string fdate, string todate, string subdivision)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = " select count( DISTINCT Stockist_Code)  from Mas_stockist  " +
                     " WHERE subdivision_code ='" + subdivision + "' and   Field_code='" + sfcode + "' and  " +
                     " Stockist_Code NOT IN  (SELECT DISTINCT  Stockist_Code   " +
                     " FROM Trans_Stock_Updation_Details  " +
                     " where Purchase_Date > '" + fdate + "' and Purchase_Date <'" + todate + "' and sfcode='" + sfcode + "' and (Rec_Pieces!='' or Rec_Qty!=''))   " +
                     " and Division_Code='" + div_code + "' and  Stockist_Active_Flag=0";

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
        public DataSet getAdmin_Password(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Password from Mas_HO_ID_Creation where Division_Code like '" + sf_code + ',' + "%' ";

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

        public int AdminUpdatePassword(string sf_code, string pwd)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();



                strQry = "UPDATE Mas_HO_ID_Creation " +
                         " SET  Password= '" + pwd + "' " +
                         " WHERE Division_Code like '" + sf_code + ',' + "%' ";

                iReturn = db.ExecQry(strQry);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
		  public DataTable getSalesForcelistEX(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dsDivision = null;


            strQry = "SELECT a.Sf_Name as FieldForceName,a.sf_Designation_Short_Name as Designation,b.StateName,a.SF_Mobile as MobileNo  from Mas_Salesforce a join mas_state b  on  a.State_Code=b.State_Code "+ 
                     "WHERE  a.sf_Tp_Active_flag = 0 and a.SF_Status=0 and a.Division_Code like '" + divcode + ",%' " +
                     "or  a.Division_Code like '%," + divcode + ",%' and lower(a.sf_code) != 'admin' " +
                     "group by  a.Sf_Name,a.sf_Designation_Short_Name,b.StateName,a.SF_Mobile";

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
 public DataSet retail_disributor_view_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and h.Stockist_Code='" + distributor + "'  and  d.Product_Code='" + product_code + "' and  DATEPART(WEEK, h.Order_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,h.Order_Date), 0))+ 1 ='" + week + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        
        public DataSet retail_route_view_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor,string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and h.Stockist_Code='" + distributor + "' and h.route='" + route + "'  and  d.Product_Code='" + product_code + "' and  DATEPART(WEEK, h.Order_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,h.Order_Date), 0))+ 1 ='" + week + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
        public DataSet retail_retailer_view_weekise_details(string divcode, int month, int year, int week, string product_code, string distributor, string route_code,string retailer_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where month(h.Order_Date)='" + month + "' and YEAR(h.Order_Date)='" + year + "' and h.Stockist_Code='" + distributor + "' and h.route='" + route_code + "'and h.cust_code='"+ retailer_code+"'  and  d.Product_Code='" + product_code + "' and  DATEPART(WEEK, h.Order_Date) -  DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM,0,h.Order_Date), 0))+ 1 ='" + week + "'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
 public DataSet retail_disributor_view_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where  month(Order_Date)='" + month + "' and YEAR(Order_Date)='" + year + "' and h.stockist_code='" + distributor + "' and d.Product_Code='" + product_code + "'  AND   CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + day + "%'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

        public DataSet retail_routewise_view_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor ,string route)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where  month(Order_Date)='" + month + "' and YEAR(Order_Date)='" + year + "' and h.route='" + route + "' and h.stockist_code='" + distributor + "' and d.Product_Code='" + product_code + "'  AND   CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + day + "%'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }

       public DataSet retail_retailerwise_view_daywise_details(string divcode, int month, int year, string day, string product_code, string distributor, string route_code,string retailer_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            strQry = "select sum(d.Quantity) as Quantity,sum(d.value) as value,sum(d.Quantity*d.net_weight) as net_value from Trans_Order_Details d inner  join Trans_Order_Head  h on d.Trans_Sl_No=h.Trans_Sl_No where  month(Order_Date)='" + month + "' and YEAR(Order_Date)='" + year + "' and h.route='" + route_code + "' and h.cust_code='" + retailer_code + "'and h.stockist_code='" + distributor + "' and d.Product_Code='" + product_code + "'  AND   CONVERT(VARCHAR(25), Order_Date, 126) LIKE '" + day + "%'";
            try
            {
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;

        }
    }
}
