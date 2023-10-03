using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class StockistMaster
    {
        private string strQry = string.Empty;
        DataTable dt = new DataTable();
        DataTable dt_recursive_Aud = new DataTable();

        DataRow dr = null;
        string Audit_mgr = string.Empty; // Added by Sri - 29Aug15
        string Audit_mgr_All = string.Empty; // Added by Sri - 29Aug15
        int iReturn_Backup = -1;
        public int AddStockistFiedlforce(string sf_name, string user_name, string password, DateTime joining_date, string reporting_to, string state_code, DateTime tp_dcr_start_date, string contact_address1, string contact_address2, string contact_citypin, string email, string mobile, string dob, string dow, string permanent_address1, string permanent_address2, string permanent_citypin, string permanent_contact_no, string sf_hq, string div_code, int sf_type, string emp_id, string short_name, string desgn, string sub_division, string des_sname, string UsrDfd_UserName, string ddl_fftype, string ddlTerritory, string Terr_code, string ffcnfdate, string Stockist_Code)
        {
            int i = 0;
            string strSfCode = string.Empty;
            string sf_code = string.Empty;

            string strjoinDate = joining_date.Month + "-" + joining_date.Day + "-" + joining_date.Year;
            string strtp_dcr_start_date = tp_dcr_start_date.Month + "-" + tp_dcr_start_date.Day + "-" + tp_dcr_start_date.Year;
            if (!CheckDupUserName(UsrDfd_UserName))
            {
                try
                {
                    int sf_sl_no = -1;
                    DateTime deactDt = DateTime.Now.AddDays(-1);
                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM Mas_Stockist_FieldForce WHERE Sf_Code !='admin'";
                    sf_sl_no = db.Exec_Scalar(strQry);

                    string sftype = string.Empty;

                    sftype = "DMS";
                    strQry = "SELECT ISNULL(MAX(sf_Sl_No),0)+1 FROM Mas_Stockist_FieldForce WHERE Sf_Code !='admin'";
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

                    string sEmp_ID = string.Empty;
                    sEmp_ID = "E" + sf_sl_no.ToString();

                    strQry = " INSERT INTO Mas_Stockist_FieldForce(Sf_Code,Sf_Name,Sf_UserName,Sf_Password,Sf_Joining_Date, " +
                              "Reporting_To_SF,TP_Reporting_SF,State_Code,sf_Tp_Active_Dt,Sf_TP_DCR_Active_Dt,SF_ContactAdd_One, " +
                              " SF_ContactAdd_Two,SF_City_Pincode,SF_Email,SF_Mobile,SF_DOB,SF_DOW,SF_Per_ContactAdd_One,SF_Per_ContactAdd_Two, " +
                              " SF_Per_City_Pincode,SF_Per_Contact_No,SF_Cat_Code,SF_Status,Sf_HQ,Division_Code,Created_Date,sf_Tp_Active_flag, " +
                              " sf_Tp_Deactive_Dt,sf_Sl_No,sf_type,sf_emp_id,sf_short_name,Designation_Code,LastUpdt_Date,subdivision_code, Employee_Id,Last_DCR_Date,sf_Designation_Short_Name,UsrDfd_UserName,Last_TP_Date,fftype,Territory,Territory_Code,FFT_CNF_Date,Stockist_Id) " +
                              " VALUES ( '" + sf_code + "' , '" + sf_name + "', '" + user_name + "', '" + password + "', '" + strjoinDate + "', " +
                              "  '" + reporting_to + "' , '" + reporting_to + "' , '" + state_code + "', '" + strtp_dcr_start_date + "', '" + strtp_dcr_start_date + "' , " +
                              " '" + contact_address1 + "', '" + contact_address2 + "', '" + contact_citypin + "', '" + email + "', '" + mobile + "', " +
                              " '" + dob + "','" + dow + "' , '" + permanent_address1 + "', '" + permanent_address2 + "' , '" + permanent_citypin + "', " +
                              " '" + permanent_contact_no + "', '0' , '0', " +
                              " '" + sf_hq + "' , '" + div_code + "' , getdate(), '0', '" + deactDt.Month + "-" + deactDt.Day + "-" + deactDt.Year + "', '" + sf_sl_no + "', " + sf_type + ", '" + emp_id + "', '" + short_name + "', '" + desgn + "',getdate(),'" + sub_division + "' , '" + sEmp_ID + "' ,'" + strtp_dcr_start_date + "', '" + des_sname + "','" + UsrDfd_UserName + "','" + strjoinDate + "','" + ddl_fftype + "','" + ddlTerritory + "','" + Terr_code + "','" + ffcnfdate + "','" + Stockist_Code + "')";

                    i = db.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return i;
        }

public DataSet getsf(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from Mas_Salesforce where Division_Code like '%" + ',' + divcode + ',' + "%'  and sf_status = '0' and sf_type='1' ";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
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


        public DataSet Stockist_getProCat(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name,c.Product_Cat_Div_Name, " +
                     " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
                     " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY c.ProdCat_SNo";
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
		
	  public DataSet Stockist_getProCat(string divcode, string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            //strQry = " SELECT c.Product_Cat_Code,c.Product_Cat_SName,c.Product_Cat_Name,c.Product_Cat_Div_Name, " +
            //         " (select COUNT(p.Product_Cat_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Cat_Code = c.Product_Cat_Code ) as cat_count   FROM  Mas_Product_Category c" +
            //         " WHERE c.Product_Cat_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY c.ProdCat_SNo";


            strQry = "EXEC get_category_details '" + divcode + "','" + sfcode + "'";

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

        public bool Stockist_CategoryList_RecordExist(int Product_Cat_Code, string Product_Cat_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Stockist_Product_Category WHERE Product_Cat_SName='" + Product_Cat_SName + "' and Division_Code = '" + div_code + "' ";
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
        public bool Stoskit_CategoryList_sRecordExist(int Product_Cat_Code, string Product_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Stockist_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' and Division_Code = '" + divcode + "'";

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

        public int Stockist_RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name, string divcode, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!Stockist_CategoryList_RecordExist(Product_Cat_Code, Product_Cat_SName, divcode))
            {
                if (!Stoskit_CategoryList_sRecordExist(Product_Cat_Code, Product_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Stockist_Product_Category " +
                                 " SET Product_Cat_SName = '" + Product_Cat_SName + "', " +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' and Product_Cat_Active_Flag = 0 ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Stockist_Product_Brand SET Product_Cat_Name = '" + Product_Cat_Name + "' WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";
                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Stockist_Product_Brand SET Product_Cat_Div_Name = '" + Pro_Div_name + "' WHERE Product_Cat_Div_Code = '" + Pro_Div_code + "' ";
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

        public DataSet Stockist_getProCate(string divcode, string ProdCatcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT Product_Cat_SName,Product_Cat_Name,Product_Cat_Div_Name,Product_Cat_Div_Code FROM  Mas_Stockist_Product_Category " +
                     " WHERE Product_Cat_Code= '" + ProdCatcode + "'AND Division_Code= '" + divcode + "' " +
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

        public int Stockist_RecordAdd(string divcode, string Product_Cat_SName, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name, string Stockist_Code)
        {
            int iReturn = -1;
            if (!Stockist_RecordExist(Product_Cat_SName, divcode))
            {
                if (!Stockist_sRecordExist(Product_Cat_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Cat_Code)+1,'1') Product_Cat_Code from Mas_Stockist_Product_Category ";
                        int Product_Cat_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Stockist_Product_Category(Product_Cat_Code,Division_Code,Product_Cat_SName,Product_Cat_Name,Product_Cat_Div_Code,Product_Cat_Div_Name,Product_Cat_Active_Flag,Created_Date,LastUpdt_Date)" +
                                 "values('" + Product_Cat_Code + "','" + divcode + "','" + Product_Cat_SName + "', '" + Product_Cat_Name + "','" + Pro_Div_code + "','" + Pro_Div_name + "',0,getdate(),getdate(),'" + Stockist_Code + "')";


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

        public bool Stockist_RecordExist(string Product_Cat_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Stockist_Product_Category WHERE Product_Cat_SName='" + Product_Cat_SName + "' and Division_Code = '" + div_code + "' ";
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

        public bool Stockist_sRecordExist(string Product_Cat_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Stockist_Product_Category WHERE Product_Cat_Name='" + Product_Cat_Name + "' and Division_Code = '" + div_code + "' ";
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

        public int Stockist_RecordAdd_RecordUpdate(int Product_Cat_Code, string Product_Cat_SName, string Product_Cat_Name, string divcode, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!Stockist_RecordExist(Product_Cat_Code, Product_Cat_SName, divcode))
            {
                if (!Stockist_sRecordExist(Product_Cat_Code, Product_Cat_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Stockist_Product_Category " +
                                 " SET Product_Cat_SName = '" + Product_Cat_SName + "', " +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE Product_Cat_Code = '" + Product_Cat_Code + "' and Product_Cat_Active_Flag = 0 ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Stockist_Product_Brand SET Product_Cat_Name = '" + Product_Cat_Name + "' WHERE Product_Cat_Code = '" + Product_Cat_Code + "' ";
                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE Mas_Stockist_Product_Brand SET Product_Cat_Div_Name = '" + Pro_Div_name + "' WHERE Product_Cat_Div_Code = '" + Pro_Div_code + "' ";
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

        public bool Stockist_RecordExist(int Product_Cat_Code, string Product_Cat_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_SName) FROM Mas_Stockist_Product_Category WHERE Product_Cat_SName = '" + Product_Cat_SName + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' AND Division_Code= '" + divcode + "' ";

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

        public bool Stockist_sRecordExist(int Product_Cat_Code, string Product_Cat_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Cat_Name) FROM Mas_Stockist_Product_Category WHERE Product_Cat_Name = '" + Product_Cat_Name + "' AND Product_Cat_Code!='" + Product_Cat_Code + "' and Division_Code = '" + divcode + "'";

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

        public DataSet Stockist_getProdCate(string Div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT 0 as Product_Cat_Code,'--Select--' as Product_Cat_Name " +
                     " UNION " +
                     " SELECT Product_Cat_Code,Product_Cat_Name " +
                     " FROM Mas_Stockist_Product_Category " +
                     " WHERE Division_Code='" + Div_code + "' and Product_Cat_Active_Flag=0 ";
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

        public DataSet Stockist_getProdBrd(string divcode, string ProdBrdCode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT Product_Brd_SName,Product_Brd_Name,Product_Cat_Name,Product_Cat_Div_Name,Product_Cat_Div_Code FROM  Mas_Stockist_Product_Brand " +
                     " WHERE Product_Brd_Code= '" + ProdBrdCode + "'AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public int Brd_Stockist_RecordAdd(string divcode, string Product_Brd_SName, string Product_Brd_Name, int Product_Cat_Code, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name, string Stockist_Code)
        {
            int iReturn = -1;
            if (!Stockist_RecordExist_Brd(Product_Brd_SName, divcode))
            {
                if (!Stockist_sRecordExist_Brd(Product_Brd_Name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Product_Brd_Code)+1,'1') Product_Brd_Code from Mas_Stockist_Product_Brand ";
                        int Product_Brd_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO Mas_Stockist_Product_Brand(Product_Brd_Code,Division_Code,Product_Brd_SName,Product_Brd_Name,Product_Brd_Active_Flag,Created_Date,LastUpdt_Date,Product_Cat_Code,Product_Cat_Name,Product_Cat_Div_Code,Product_Cat_Div_Name)" +
                                 "values('" + Product_Brd_Code + "','" + divcode + "','" + Product_Brd_SName + "', '" + Product_Brd_Name + "',0,getdate(),getdate()," + Product_Cat_Code + ",'" + Product_Cat_Name + "','" + Pro_Div_code + "','" + Pro_Div_name + "','" + Stockist_Code + "')";


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

        public bool Stockist_RecordExist_Brd(string Product_Brd_SName, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_SName='" + Product_Brd_SName + "' and Division_Code = '" + div_code + "' ";
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

        public bool Stockist_sRecordExist_Brd(string Product_Brd_Name, string div_code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_Name='" + Product_Brd_Name + "' and Division_Code = '" + div_code + "' ";
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

        public int Stockist_Brd_RecordUpdate(int ProBrdCode, string Product_Brd_SName, string Product_Brd_Name, string divcode, int Product_Cat_Code, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!Stockist_RecordExistbrd(ProBrdCode, Product_Brd_SName, divcode))
            {
                if (!Stockist_nRecordExist(ProBrdCode, Product_Brd_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Stockist_Product_Brand " +
                                 " SET Product_Brd_SName = '" + Product_Brd_SName + "', " +
                                 " Product_Brd_Name = '" + Product_Brd_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() ," +
                                 " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' " +
                                 " WHERE Product_Brd_Code = '" + ProBrdCode + "' and Product_Brd_Active_Flag = 0 ";

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
        public bool Stockist_RecordExistbrd(int ProBrdCode, string Product_Brd_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_SName = '" + Product_Brd_SName + "' AND Product_Brd_Code!='" + ProBrdCode + "' AND Division_Code= '" + divcode + "' ";

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


        public bool Stockist_nRecordExist(int ProBrdCode, string Product_Brd_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_Name = '" + Product_Brd_Name + "' AND Product_Brd_Code!='" + ProBrdCode + "' and Division_Code = '" + divcode + "'";

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

        public DataSet Stockist_getProdDiv(string ddlCategore, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " SELECT Product_Cat_Div_Code,Product_Cat_Div_Name " +
                     " FROM Mas_Stockist_Product_Category " +
                     " WHERE Product_Cat_Code='" + ddlCategore + "' AND Division_Code='" + divcode + "' and Product_Cat_Active_Flag=0 ";
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

        public DataSet Stockist_getProBrd(string divcode, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            //strQry = " SELECT b.Product_Brd_Code,b.Product_Brd_SName,b.Product_Brd_Name,b.Product_Cat_Name,b.Product_Cat_Div_Code,b.Product_Cat_Div_Name," +
            //         " (select COUNT(p.Product_Brd_Code) from Mas_Product_Detail p where p.Product_Active_Flag=0 and p.Product_Brd_Code = b.Product_Brd_Code ) as brd_count   FROM  Mas_Product_Brand b" +
            //         " WHERE b.Product_Brd_Active_Flag=0 AND Division_Code= '" + divcode + "' " +
            //         " ORDER BY b.Product_Brd_SNO";

            strQry = "EXEC get_brand_details '" + divcode + "','" + sf_code + "'";
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

        public int Stockist_Brd_RecordUpdate1(int ProBrdCode, string Product_Brd_SName, string Product_Brd_Name, string divcode, int Product_Cat_Code, string Product_Cat_Name, string Pro_Div_code, string Pro_Div_name)
        {
            int iReturn = -1;
            if (!Stockist_RecordExistbrd1(ProBrdCode, Product_Brd_SName, divcode))
            {
                if (!Stockist_nRecordExist1(ProBrdCode, Product_Brd_Name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_Stockist_Product_Brand " +
                                 " SET Product_Brd_SName = '" + Product_Brd_SName + "', " +
                                 " Product_Brd_Name = '" + Product_Brd_Name + "' ," +
                                 " Product_Cat_Div_Name = '" + Pro_Div_name + "' ," +
                                 " Product_Cat_Div_Code = '" + Pro_Div_code + "' ," +
                                 " LastUpdt_Date = getdate() ," +
                                 " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                 " Product_Cat_Name = '" + Product_Cat_Name + "' " +
                                 " WHERE Product_Brd_Code = '" + ProBrdCode + "' and Product_Brd_Active_Flag = 0 ";

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

        public bool Stockist_RecordExistbrd1(int ProBrdCode, string Product_Brd_SName, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_SName) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_SName = '" + Product_Brd_SName + "' AND Product_Brd_Code!='" + ProBrdCode + "' AND Division_Code= '" + divcode + "' ";

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

        public bool Stockist_nRecordExist1(int ProBrdCode, string Product_Brd_Name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Brd_Name) FROM Mas_Stockist_Product_Brand WHERE Product_Brd_Name = '" + Product_Brd_Name + "' AND Product_Brd_Code!='" + ProBrdCode + "' and Division_Code = '" + divcode + "'";

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
        public DataSet getProductCategory1(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProCat = null;

            strQry = " SELECT '' as Product_Cat_Code, '---Select---' as Product_Cat_Name " +
                     " UNION " +
                     " SELECT Product_Cat_Code,Product_Cat_Name FROM  Mas_Product_Category " +
                     " WHERE Product_Cat_Active_Flag=0 AND Product_Cat_Code> 0 AND Division_Code= '" + divcode + "' " +
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

        public DataSet Stockist_getProductBrand(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                     " UNION " +
                     " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Stockist_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public int Stocikist_RecordAdd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, int Base_unit_code, string uom, int uom_code, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, int Division_Code, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code, string txtPacksize, string txtGrosswt, string txtNetwt, int target, string Product_Short_Name, string HSN_Code, string Netwt, int Txtprovalid, string Stockist_Code)
        {
            int iReturn = -1;
            int iSlNo = -1;
            int icodeSlNo = -1;
            if (!Stockist_RecordExistdet(Product_Detail_Code, Division_Code))
            {
                if (!Stockist_sRecordExistdet(Product_Detail_Name, Division_Code))
                {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "SELECT ISNULL(MAX(Prod_Detail_Sl_No),0)+1 FROM Mas_StockistProduct_Detail";
                    iSlNo = db.Exec_Scalar(strQry);


                    strQry = "SELECT ISNULL(MAX(product_code_slno),0)+1 FROM Mas_StockistProduct_Detail";
                    icodeSlNo = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO Mas_StockistProduct_Detail(Product_Detail_Code,Product_Detail_Name,Product_Sale_Unit,Base_Unit_code,product_unit,Unit_code, " +
                                " Product_Cat_Code,Product_Type_Code,Product_Description, " +
                                " Division_Code,Created_Date,Product_Active_Flag,Prod_Detail_Sl_No,Product_Grp_Code,LastUpdt_Date,state_code,subdivision_code,Product_Mode,Sample_Erp_Code,Sale_Erp_Code,Product_Brd_Code,product_code_slno,product_packsize,product_grosswt,product_netwt,target,Product_Short_Name,HSN_Code,UOM_Weight,Product_Validity) " +
                                " VALUES('" + Product_Detail_Code + "', '" + Product_Detail_Name + "', '" + Product_Sale_Unit + "','" + Base_unit_code + "','" + uom + "','" + uom_code + "','" + Product_Cat_Code + "', " +
                                " '" + Product_Type_Code + "', '" + Product_Description + "', " + Division_Code + ", getdate(), 0, " + iSlNo + ", " + Product_Grp_Code + ",getdate(),'" + state + "','" + sub_division + "','" + mode + "','" + sample + "','" + sale + "','" + Product_Brd_Code + "', " + icodeSlNo + ",'" + txtPacksize + "','" + txtGrosswt + "','" + txtNetwt + "','" + target + "','" + Product_Short_Name + "','" + HSN_Code + "','" + Netwt + "','" + Txtprovalid + "','" + Stockist_Code + "')";


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

        public bool Stockist_RecordExistdet(string Product_Detail_Code, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Code) FROM Mas_StockistProduct_Detail WHERE Product_Detail_Code='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "' ";
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

        public bool Stockist_sRecordExistdet(string Product_Detail_Name, int divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_StockistProduct_Detail WHERE Product_Detail_Name='" + Product_Detail_Name + "' AND Division_Code= '" + divcode + "' ";
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

        public int Stockist_RecordUpdateProd(string Product_Detail_Code, string Product_Detail_Name, string Product_Sale_Unit, int Base_unit_code, string uom, int uom_code, int Product_Cat_Code, int Product_Grp_Code, string Product_Type_Code, string Product_Description, string divcode, string state, string sub_division, string mode, string sample, string sale, int Product_Brd_Code, string txtPacksize, string txtGrosswt, string txtNetwt, int target, string Product_Short_Name, string Hsn_Code, string Netwt, int Txtprovalid)
        {
            int iReturn = -1;
            if (!Stockist_sRecordExistDetail(Product_Detail_Name, Product_Detail_Code, divcode))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE Mas_StockistProduct_Detail " +
                             " SET Product_Detail_Name = '" + Product_Detail_Name + "', " +
                             " Product_Sale_Unit = '" + Product_Sale_Unit + "', " +
                              " Base_Unit_code = '" + Base_unit_code + "', " +
                               " product_unit = '" + uom + "', " +
                               " Unit_code = '" + uom_code + "', " +

                                     " Product_Cat_Code = " + Product_Cat_Code + " ," +
                                    " Product_Grp_Code = " + Product_Grp_Code + "," +

                                 " Product_Type_Code = '" + Product_Type_Code + "', " +
                                 " Product_Description = '" + Product_Description + "'," +
                                    " Product_Short_Name ='" + Product_Short_Name + "'," +
                                 " LastUpdt_Date = getdate() , " +
                                 " State_Code = '" + state + "',subdivision_code = '" + sub_division + "', Product_Mode ='" + mode + "', Sample_Erp_Code = '" + sample + "', Sale_Erp_Code ='" + sale + "' ,Product_Brd_Code ='" + Product_Brd_Code + "',product_packsize='" + txtPacksize + "',product_grosswt='" + txtGrosswt + "',product_netwt='" + txtNetwt + "', target ='" + target + "', HSN_Code ='" + Hsn_Code + "', UOM_Weight ='" + Netwt + "',Product_Validity = '" + Txtprovalid + "'  " +
                             " WHERE  Division_Code = " + divcode + " AND Product_Detail_Code = '" + Product_Detail_Code + "' ";

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

        public bool Stockist_sRecordExistDetail(string Product_Detail_Name, string Product_Detail_Code, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Product_Detail_Name) FROM Mas_StockistProduct_Detail WHERE Product_Detail_Name = '" + Product_Detail_Name + "' AND Product_Detail_Code!='" + Product_Detail_Code + "' AND Division_Code= '" + divcode + "'and Product_Active_Flag=0";

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

        public DataSet Stockist_get_cat_base_ProductBrand(string Cat_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT '' as Product_Brd_Code, '---Select---' as Product_Brd_Name " +
                     " UNION " +
                     " SELECT Product_Brd_Code,Product_Brd_Name FROM  Mas_Stockist_Product_Brand " +
                     " WHERE Product_Brd_Active_Flag=0 AND Product_Brd_Code> 0 AND Product_Cat_Code='" + Cat_code + "' AND Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public DataSet getCompanyName(string div_code)
        {
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select HO_ID,Name from Mas_HO_ID_Creation where HO_ID='" + div_code + "'";
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
		public DataSet getstockistdetails(string div_code, string Sf_Code, string Sf_type)
        {
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC sp_get_stk_sstk_details '" + div_code + "','" + Sf_Code + "','" + Sf_type + "'";
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

		public DataSet getcompanydetails(string div_code,string stk_code)
        {
            DataSet dsSF = null;
            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC get_com_sup_stk_Details '"+ div_code + "','"+ stk_code + "'";
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

        public DataSet getallproductdetails(string div_code, string sfcode)
        {
            DataSet ds = null;
            DB_EReporting db = new DB_EReporting();

            strQry = "EXEC get_purchafe_product_details '" + div_code + "','" + sfcode + "'";
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


        public DataSet getallfirstletterproductdetails(string div_code,string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "EXEC Get_pro_first_letter '"+ div_code + "','"+ Stockist_Code + "'";
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

        public DataSet getallfirstletterproduct(string div_code,string letter)
        {
            DataSet ds = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select distinct(sr.Product_Detail_Code),pd.Product_Detail_Name,sr.MRP_Price from Mas_Product_State_Rates sr join mas_product_detail pd on"+
                     " pd.Product_Detail_Code = sr.Product_Detail_Code where pd.Division_Code ='"+ div_code + "' and Product_Detail_Name like '%" +letter+ "%'";
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



        public DataSet Stockist_getProdforbrd(string divcode, string val)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;

            strQry = " SELECT a.Product_Detail_Code,a.Product_Detail_Name,a.Product_Sale_Unit,a.Product_Description, " +
                     " b.Product_Cat_Name,d.Product_Brd_Name,a.Product_Mode,a.Sale_Erp_Code,a.Sample_Erp_Code" +
                     " FROM  Mas_StockistProduct_Detail a,Mas_Stockist_Product_Category b,Mas_Stockist_Product_Brand d" +
                     " WHERE a.product_cat_code = b.product_cat_code AND " +
                     " a.Product_Brd_Code = d.Product_Brd_Code AND " +
                     " a.Product_Brd_Code = '" + val + "' AND " +
                     " a.Product_Active_Flag=0 AND a.Division_Code= '" + divcode + "' " +
                     " ORDER BY 2";
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

        public int Stockist_InsertProductRate(string div_code, string prod_code, string stockist_code, string effective_from, string prod_name, int Op_Qty, int Rec_Qty, decimal dist_amt, int Sale_Qty, decimal mrp_amt, int Retailor_Rate, decimal nsr_amt, int sale_pieces, decimal ret_amt, int RP_BaseRate, int OP_Pieces, int RwFlg, int Rec_Pieces)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "EXEC [GetProductRate] '" + stockist_code + "','" + div_code + "','" + effective_from + "','" + prod_code + "','" + prod_name + "','" + Op_Qty + "','" + Rec_Qty + "','" + dist_amt + "','" + Sale_Qty + "','" + nsr_amt + "','" + sale_pieces + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


		public int insertpurchaseOrder(string Stockist_Code, string div_code, string sxml, string sxml1, string Sup_Code, string Sup_name, string sf_code)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();

            try
            {
                strQry = "EXEC [svPrimaryOrder]  '" + Stockist_Code + "','" + div_code + "','" + sxml + "', '" + sxml1 + "','" + Sup_Code + "','" + Sup_name + "','" + sf_code + "'";
                i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }


        public DataSet GET_Trans_Sl_No(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsProBrd = null;
            try
            {
               

                strQry = "EXEC [GET_Trans_Sl_No] '" + div_code + "'";
                dsProBrd = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsProBrd;
        }

        public DataSet getretailerdetailsmyorder(string scode, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                //strQry = "select ListedDr_Name,ListedDrCode from mas_listeddr where charindex(','+cast('" + scode + "' as varchar)+',',','+Dist_Name+',')>0 and ListedDr_Active_Flag='0' order by ListedDr_Name";
                //  strQry = "select ListedDr_Name,ListedDrCode,mc.Territory_Code,mc.Territory_Name,ISNULL(ListedDr_Name,'') +' '+ '('+  ISNULL(mc.Territory_Name,11) + ')' as ListedDr_Name1  from mas_listeddr dr inner join Mas_Territory_Creation mc on dr.Territory_Code = mc.Territory_Code where charindex(','+cast('" + scode + "' as varchar)+',',','+dr.Dist_Name+',')>0  and ListedDr_Active_Flag = '0' and Territory_Active_Flag = '0' order by ListedDr_Name";
                strQry = "EXEC spGetRetailerName '"+ scode + "','"+ divcode + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet getretailerdetails(string scode, string divcode, string Sf_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                // strQry = "select msd.ListedDr_Name,msd.ListedDrCode from Trans_Order_Head toh join mas_listeddr msd on toh.Cust_Code = msd.ListedDrCode where toh.Stockist_Code = '" + scode + "' and toh.Order_Flag = '0' and toh.Div_ID = '" + divcode + "' and Order_Value > 0  group by ListedDr_Name,ListedDrCode order by ListedDr_Name";
                strQry = "EXEC Bind_sales_details '" + Sf_Type + "','" + divcode + "','" + scode + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet Getpendingorder(string Retailer_ID, string Stockist_Code, string Div_code, string Sf_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            //  strQry = "select Trans_Sl_No,Trans_Sl_No+ '_' +convert(varchar(10),Order_Date,103) as Order_Date from trans_order_head where Order_Flag = '0' and Stockist_Code = '" + Stockist_Code + "' and Cust_Code = '" + Retailer_ID + "' and Order_Value > 0";
            strQry = "EXEC bind_pending_order '" + Sf_Type + "','" + Div_code + "','" + Stockist_Code + "','" + Retailer_ID + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getproductDetails(string no, string Div_code, string Sf_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            // strQry = "select tod.*,toh.Trans_Sl_No,CONVERT(VARCHAR(10), Order_Date, 101) as Order_Date,mtd.Tax_Id,Tax_Name,tm.Value as Tax_Val from Trans_Order_Details tod join trans_order_head toh  on tod.Trans_Sl_No = toh.Trans_Sl_No left join Mas_StateProduct_TaxDetails mtd on mtd.Product_Code = tod.Product_Code left join tax_master tm on tm.Tax_Id = mtd.Tax_Id  where toh.Div_ID = '" + Div_code + "' and toh.Trans_Sl_No = '" + no + "'";
            strQry = "EXEC bind_invoice_product '" + Sf_Type + "','" + Div_code + "','" + no + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getallorderdetails(string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select Order_No,company_add,Billing_Address,CONVERT(varchar, Order_Date, 103) as Order_Date,CONVERT(varchar, Expect_Date, 103) as Expect_Date,Order_Value,count(*) total from Trans_PriOrder_Details tpd join Trans_PriOrder_Head tph  on tph.Trans_Sl_No = tpd.Trans_Sl_No  where Stockist_Code = '" + Stockist_Code + "' group by Order_No,company_add,Billing_Address,Order_Date,Expect_Date,Order_Value";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }



        public DataSet getinvoicedetails(string Stockist_Code, string FDT, string TDT)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "EXEC Bind_Invoice_List '" + Stockist_Code + "','" + FDT + "','" + TDT + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet GETPOORDERS(string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select Trans_Sl_No from Trans_PriOrder_Head where Stockist_Code='" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		 public DataSet GetPOoRDERS(string Stockist_Code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            //strQry = "select Trans_Sl_No, Order_Date from Trans_PriOrder_Head where Stockist_Code =  '" + Stockist_Code + "' and Order_Value> 0 and Division_Code = '" + Div_Code + "' and Order_Flag = '0' order by Order_Date  ";
			strQry = "select Trans_Sl_No, convert(varchar(10),Order_Date,120)as Order_Date from Trans_PriOrder_Head where Stockist_Code =  '" + Stockist_Code + "' and Order_Value> 0 and Division_Code = '" + Div_Code + "' and Order_Flag = '0' order by Order_Date  ";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet GetproDet(string orderid)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Trans_PriOrder_Details where Trans_Sl_No='"+ orderid + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet getsuppilerbyorder(string orderid,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select distinct(HO_ID),Name from Mas_HO_ID_Creation mhic join Trans_PriOrder_Details tpd on mhic.HO_ID = tpd.Division_Code where tpd.Trans_Sl_No = '"+ orderid + "'";

            //strQry = "select distinct(HO_ID),Name from Mas_HO_ID_Creation mhic join Trans_PriOrder_Details tpd on mhic.HO_ID = tpd.Division_Code where Trans_Sl_No = '"+ orderid + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet getproductname(string Pcode,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select distinct(tpod.Product_Code),MPD.Product_Short_Name,MPD.Product_Detail_Name,MPD.Unit_code,MUE.Move_MailFolder_Name,MPD.Sample_Erp_Code,mr.Distributor_Price,tpod.CQty " +
                     " from Mas_Product_Detail MPD INNER JOIN Trans_PriOrder_Details tpod on MPD.Product_Detail_Code=tpod.Product_Code join Mas_Multi_Unit_Entry MUE ON MPD.Unit_code = MUE.Move_MailFolder_Id join Mas_Product_State_Rates mr on mr.Product_Detail_Code=tpod.Product_Code where  " +
                     " MPD.Division_Code = '"+ div_code + "' and Product_Active_Flag = '0' and tpod.Trans_Sl_No = '" + Pcode + "'";

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

        public DataSet getSupplier(string Stockist_Code,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select HO_ID as id,Name as name,1 as type from mas_ho_Id_Creation where HO_ID = '" + div_code + "' union all select S_No as id ,S_Name as name,2 as type from[dbo].[Supplier_Master] where division_code = '" + div_code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getsupllierbytype1(string supcode, string div_code,string type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

                strQry = "select * from mas_division where Division_Code='"+ div_code + "'";
           
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getsupllierbytype2(string supcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from [Supplier_Master] where S_No='" + supcode + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getbyletter(string letter,string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select Product_Detail_Code,Product_Detail_Name from Mas_Product_Detail where Division_Code = '" + divcode + "'  and Product_Active_Flag = '0' and Product_Detail_Name like '%"+ letter + "%' ";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getprodcut(string letter, string divcode,string pname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from Mas_Product_Detail where Division_Code = '" + divcode + "'  and Product_Active_Flag = '0' and Product_Detail_Name = '" + pname + "' ";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getbatch(string pname,string scode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
          
            strQry = "select distinct(PCode) from [dbo].[Trans_GRN_Details] where PDetails='"+ pname + "' ";

            string code= db_ER.Exec_Scalar_s(strQry);


            // strQry = "select tcb.BatchNo from Mas_Product_Detail mpd join Trans_CurrStock_Batchwise tcb on mpd.Product_Detail_Code=tcb.Prod_Code where Product_Detail_Name='" + pname + "'  group by Product_Detail_Code,BatchNo ";
            strQry = " select Batch_No from Trans_GRN_Details tgd join Trans_GRN_Head tgh on tgd.Trans_Sl_No=tgh.Trans_Sl_No where tgh.SF_Code= '" + scode + "' and tgd.PCode='"+ code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getheaddetails(string divcode, string value1, string value2,string type,string stockist)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;


            strQry = "select tih.Trans_Inv_Slno,tih.Dis_Code,tih.Dis_Name,tih.Cus_Code,tih.Cus_Name,tih.Order_No,convert(varchar,tih.Invoice_Date,111) as Invoice_Date,convert(varchar,tih.Order_Date,111) as Order_Date,convert(varchar,tih.Delivery_Date,111) as Delivery_Date,ms.Stockist_Address,Isnull(ListedDr_Address1,'')+','+Isnull(ListedDr_Address2,'')+','+Isnull(ListedDr_Address3,'')+','+Isnull(ListedDr_PinCode,'')" +
                    " +','+Isnull(ListedDr_Mobile,'') as Address from Trans_Invoice_Head tih join mas_stockist ms on " +
                    "tih.Dis_Code=ms.Stockist_Code join Mas_ListedDr mld on tih.Cus_Code=mld.ListedDrCode where tih.Trans_Inv_Slno between '"+ value1 + "' and '"+ value2 + "' and tih.Dis_Code='" + stockist + "' and tih.Division_Code='"+ divcode + "'";

            //strQry = "select * from Trans_Invoice_Head where Trans_Inv_Slno in ('"+ value1 + "','" + value2 + "') and  Dis_Code='"+ stockist + "' and Division_Code='"+divcode+"'";

            //strQry = " select tih.Dis_Code,tih.Dis_Name,tih.Invoice_Date,ms.Stockist_Address from Trans_Invoice_Head tih join mas_stockist ms on tih.Dis_Code = ms.Stockist_Code where tih.Trans_Inv_Slno in(157, 158) and tih.Dis_Code = 14910";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getproddetailsforslip(string divcode, string value1, string value2, string type, string stockist)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from Trans_Invoice_details where Trans_Inv_Slno between '"+ value1 + "' and '"+ value2 + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getCustomer(string Stockist_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select r.* from mas_stockist ms join mas_territory_creation mtc on charindex(','+ms.Stockist_Code+',',','+mtc.Dist_Name+',')>0 join "+
                     "mas_listeddr r on r.Territory_Code = mtc.Territory_Code where ms.Stockist_Code = '"+ Stockist_Code + "' and ms.Division_Code = '"+ div_code + "' and Stockist_Active_Flag = 0";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getCustomeraddress(string Customer_Code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = " select* from[Mas_ListedDr] where ListedDrCode = '"+ Customer_Code + "' and  Division_Code='"+ div_code + "' ";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetCustDetailsByMobNo(string Mobile_no)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            // strQry = " select ListedDrCode,ListedDr_Name,Isnull(ListedDr_Address1,'')+','+Isnull(ListedDr_Address2,'')+','+Isnull(ListedDr_Address3,'')+','+Isnull(ListedDr_PinCode,'') +','+Isnull(ListedDr_Mobile,'') as Address from  Mas_ListedDr where ListedDr_Mobile = '" + Mobile_no + "' ";
            strQry = "EXEC get_cust_by_mblno '"+ Mobile_no + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getprodDet(string Div_Code, string Product_Name, string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "exec sp_GetProductTaxDetails '" + Product_Name + "','" + Div_Code + "','" + Stockist_Code + "'";

            //strQry = " select mpd.Product_Detail_Code,mpd.Product_Detail_Name,mpd.Division_Code,mpd.State_Code,mpsr.MRP_Price,mpsr.Distributor_Price from Mas_Product_Detail mpd join Mas_Product_State_Rates mpsr on "+
            //         " mpd.Product_Detail_Code = mpsr.Product_Detail_Code where mpd.Product_Detail_Name = '" + Product_Name + "' and mpd.Division_Code = '" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDesignName(string prefix)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from Mas_Dsm_Designation where Desig_Name like '%" + prefix + "%'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDesignAllName(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select Desig_id,Desig_Name from Mas_Dsm_Designation";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getallorderdetails(string Stockist_Code, string Div_Code, string fdt, string tdt)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "exec sp_getpriorderlist  '" + Stockist_Code + "','" + Div_Code + "','" +fdt+ "','" +tdt+ "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getallorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "sp_get_orderby_ordno '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }


        public DataSet getallCounterSalesDetails(string Stockist_Code, string fdt, string tdt)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select tch.Order_No,tch.Dis_Code,tch.Dis_Name,tch.Cus_Code,tch.Cus_Name,tch.Cust_Mobie_No,CONVERT(varchar, tch.Order_Date, 103) as Order_Date,tch.Pay_Term,tch.Sub_Total,tch.Dis_total,tcd.Discount,tch.Total,tcd.Price,tcd.Product_Name,tcd.Amount,count(*) detailtotal from Trans_CounterSales_Details tcd join Trans_CounterSales_Head tch on tch.Trans_Count_Slno = tcd.Trans_Count_Slno where Dis_Code = '" + Stockist_Code + "' and convert(date,tch.Order_Date) between '" + fdt + "' and '" + tdt + "' group by Order_No,Dis_Code,Dis_Name,Cus_Code,Cus_Name,Cust_Mobie_No,Order_Date,Pay_Term,Sub_Total,Dis_total,Total,Price,total,Discount,Product_Name,Amount";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getallSecorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "sp_get_Sec_orderby_ordno '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getSSDist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "Select Stockist_Code,Stockist_Name from mas_stockist where stockist_active_flag=0 and division_code='" + divcode + "' ";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getmpdist(string divcode, string supcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select * from Mas_SSCustomers where sup_code='" + supcode + "' and division_code='" + divcode + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetNewQuan(string Stockist_Code, string Div_Code, string date)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = " exec get_new_qnty '" + Stockist_Code+ "','" + date + "','"+ Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getSlatTimes(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = " exec getSlatTimes '" + Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getSlotOrders(string SlatID, string Div_Code, string fDt,string tDt)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = " exec getPOrdersBySlot '" + SlatID + "','" + fDt + "','" + tDt + "','" + Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet getallinvoiceorderbystockist(string Stockist_Code, string Div_Code, string Order_No)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "EXEC sp_get_InvoiceOrders '" + Order_No + "','" + Stockist_Code + "','" + Div_Code + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public int insertinvoice(string sxmlGen_detils, string sxmlpro_detils, string sxmltax_details)
        {

            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();

            try
            {
                strQry = "EXEC [Sp_Invoice_Order] '" + sxmlGen_detils + "','" + sxmlpro_detils + "','" + sxmltax_details + "'";
                i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;

        }

        public DataSet PrintInvoiceDetails(string Order_ID, string dis_code, string Div_Code, string Cust_Code)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "EXEC sp_Invoice_Print '" + Order_ID + "','" + Cust_Code + "','" + dis_code + "','" + Div_Code + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
        public DataSet getdist(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "select * from Mas_Stockist where Division_Code = '" + divcode + "'  and Stockist_Active_Flag = '0'  ";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
	  public DataSet gets_Product_unit_details(string Div_Code,string stk_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec get_pro_unit '" + Div_Code + "','"+ stk_code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
       public DataSet gets_cat_details(string Div_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select* from Mas_Product_Category where Division_Code = '" + Div_code + "' and Product_Cat_Active_Flag = '0'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet getsdistdetails(string div_code, string sscode)
        {
            DataSet ds = null;
            DB_EReporting db = new DB_EReporting();
            strQry = "select Stockist_Code HO_ID,Stockist_Name,Stockist_Address Division_Add1 from Mas_Stockist where Division_Code='" + div_code + "' and Stockist_Code in (select SUBSTRING(Customer_code, CHARINDEX(','+Stockist_Code+',',','+Customer_code+','), LEN(Stockist_Code)) from Mas_SSCustomers where Division_Code='" + div_code + "' and Sup_Code='" + sscode + "')";
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
		
		 public DataSet get_rate_new_bystk(string Div_Code,string Stockist_Code,string State)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec getrate_new '" + Div_Code + "','"+ Stockist_Code + "','" + State + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet getschemebystk(string Stockist_Code, string Div_Code, string date)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "EXEC get_scheme_by_stk '"+ Stockist_Code + "','" + Div_Code + "','" + date + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
		
		
	   public DataSet Get_Sec_scheme(string Stockist_Code, string Div_Code, string date)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                // strQry = "select ms.*,mp.Sale_Erp_Code,Sample_Erp_Code from mas_scheme ms left join mas_product_detail mp on ms.Product_Code=mp.Product_Detail_Code where ms.Division_Code='" + Div_Code + "' and  CHARINDEX(','+cast('" + Stockist_Code + "' as varchar)+',',','+Stockist_Code+',')>0 and cast(convert(varchar, Effective_To, 101) as datetime) >= cast(convert(varchar,'" + date + "' ,101) as datetime)   and cast(convert(varchar, Effective_From, 101) as datetime) <= cast(convert(varchar, '" + date + "', 101) as datetime) order by Product_Code,cast(scheme as int) desc ";
                strQry = "EXEC get_secondary_scheme '"+ Stockist_Code + "','" + Div_Code + "','" + date + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
		
	   public DataSet Get_Adv_Credit_amt(string Stockist_Code, string Div_Code, string Retailer_ID)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec Sp_Get_Adv_Credit_amt '" + Stockist_Code + "','" + Div_Code + "','" + Retailer_ID + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		        public DataSet getStockBydid(string Div_Code,string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec Sp_getStockBydid '" + Div_Code + "','" + Stockist_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		        public DataSet Getpendingorder_details(string Retailer_ID, string Stockist_Code, string Div_Code, string Sf_Type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            // strQry = "select tod.*,toh.Trans_Sl_No,CONVERT(VARCHAR(10), Order_Date, 101) as Order_Date,mtd.Tax_Id,Tax_Name,tm.Value as Tax_Val from Trans_Order_Details tod join trans_order_head toh  on tod.Trans_Sl_No = toh.Trans_Sl_No left join Mas_StateProduct_TaxDetails mtd on mtd.Product_Code = tod.Product_Code left join tax_master tm on tm.Tax_Id = mtd.Tax_Id  where toh.Div_ID = '" + Div_code + "' and toh.Trans_Sl_No = '" + no + "'";
            strQry = "EXEC bind_invoice_product_1 '" + Sf_Type + "','" + Div_Code + "','" + Retailer_ID + "','" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet get_dis_details(string stk_Code, string div_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "select * from mas_stockist where stockist_code='"+ stk_Code + "' and Division_Code='"+ div_code + "' and Stockist_Active_Flag='0'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Get_invoiced_retailer(string Stockist_Code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
           // strQry = "select msd.ListedDr_Name as Name,msd.ListedDrCode as Code from Trans_Order_Head toh join mas_listeddr msd on toh.Cust_Code = msd.ListedDrCode where toh.Stockist_Code ='" + Stockist_Code + "' and toh.Order_Flag = '1' and toh.Div_ID = '" + Div_Code + "' and Order_Value > 0 group by ListedDr_Name,ListedDrCode order by ListedDr_Name";
		      strQry = "EXEC Sp_get_invoice_pending_cust_Name '"+ Div_Code + "','"+ Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Get_invoiced_retailer_order(string Stockist_Code, string Div_Code, string Customer_Code, string FromYear, string To_Year,string From_Month, string To_Month, string type)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "Exec get_pending_payment_details '" + Stockist_Code + "','" + Div_Code + "', '" + Customer_Code + "', '" + FromYear + "','" + To_Year + "','" + From_Month + "','" + To_Month + "','" + type + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		        public DataSet Get_custWise_credit_note_details(string Stockist_Code, string Div_Code, string Customer_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec sp_Get_custWise_credit_note_details '" + Stockist_Code + "','" + Div_Code + "','" + Customer_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		        public DataSet Get_Dsm_Details_bystockist(string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            //strQry = "select stockist_code as Code, stockist_name as Name from mas_stockist where stockist_code = '" + Stockist_Code + "' and Stockist_Active_Flag = '0'" +
            //         "union all " +
            //         "select DSM_code as Code, dsm_name as Name from mas_dsm where Distributor_Code = '" + Stockist_Code + "' and DSM_Active_Flag = '0' order by Name";


            strQry = "EXEC sp_get_pay_dsm '"+ Stockist_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		     public DataSet Save_adv_amt_for_retailer(string c_id,string adv_amt,string div)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec sp_Save_adv_for_retailer '" + c_id + "','" + adv_amt + "','" + div + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Get_Retailer_Advance(string Customer_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "Exec sp_Ret_adv '" + Customer_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet get_priorder_No(string stk_Code , string div_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec get_primary_order_ids '" + div_code + "','" + stk_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet get_paymt_det(string stockist_code, string FDT, string TDT)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();
            // strQry = "select *,convert(varchar,Pay_Date,103) as Pay_Date1 from trans_payment_detail where Sf_Code='" + stockist_code + "' and type='payment' and CAST(CONVERT(VARCHAR, Pay_Date,101) AS DATETIME)>= '" + FDT + "' and CAST(CONVERT(VARCHAR, Pay_Date, 101) AS DATETIME) <= '" + TDT + "'";

            strQry = "EXEC get_payment_list '"+ stockist_code + "','"+ FDT + "','"+ TDT + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;

        }
		
		       public DataSet Bind_Invoice_Cust(string Div_code, string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();
            strQry = "EXEC Bind_Inv_cust '" + Div_code + "','" + Stockist_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Bind_Cust_Inv_no(string Div_code, string Stockist_Code, string Retailer_ID)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_Cust_Invoice_no '" + Div_code + "','" + Stockist_Code + "','" + Retailer_ID + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet getprodDet(string Div_Code, string Stockist_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "exec sp_GetProductTaxDetails '" + Div_Code + "','" + Stockist_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Get_inv_Pro_details(string Div_code, string Stockist_Code, string inv_no)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_Inv_Pro_details '" + Div_code + "','" + Stockist_Code + "','" + inv_no + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		
        public DataSet Get_SalesMan_details(string Div_code, string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_Sales_Man_details '" + Div_code + "','" + Stockist_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet Get_Pre_credit_details(string Cust_ID , string Div_Code, string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec Get_Pre_credit_details '" + Cust_ID   + "','" + Stockist_Code  + "','" + Div_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public DataSet View_credit_details(string Credit_no, string Div_Code, string Stockist_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec Sp_View_credit_details '" + Credit_no + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		public DataSet getallCounterSalesDetails(string Stockist_Code, string fdt, string tdt, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            //   strQry = "select tch.*,CONVERT(varchar, tch.Order_Date, 103) as Order_Date1,ROUND(Total, 2) AS Total1 from Trans_CounterSales_Head tch where Dis_Code = '" + Stockist_Code + "' and Division_Code = '" + Div_Code + "' and convert(date, tch.Order_Date) between '" + fdt + "' and '" + tdt + "' order by tch.Order_Date desc";
            strQry = "EXEC get_counter_list '"+ Stockist_Code + "','" + fdt + "','" + tdt + "','" + Div_Code + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		public DataSet Check_stockist_credit_details(string stk_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_Check_stockist_credit_details '" + stk_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet get_access_details(string stk_Code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC sp_get_extra_tax_Details '" + stk_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		 public DataSet get_Credit_note_details(string Div_code, string Stockist_Code, string FDt, string TDt)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "EXEC Bind_Credit_note_details '" + Div_code + "','" + Stockist_Code + "','" + FDt + "','" + TDt + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		        public string Update_Pending_bills(string sxml, string Stockist_Code, string Cust_ID,string Total_amount,string Advance_pay,string Type)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();
            string msg = string.Empty;
            strQry = "exec Update_Pending_bill '" + sxml + "','" + Stockist_Code + "','" + Cust_ID + "','"+ Total_amount + "','"+ Advance_pay + "','"+ Type + "'";
            try
            {
                ds = db_ER.Exec_DataSet(strQry);
                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
		
	 public int insert_Credit(string sxmlGen_detils, string sxmlPro_detils)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();

            try
            {
                strQry = "EXEC [Sp_Save_Credit_Notes] '" + sxmlGen_detils + "','" + sxmlPro_detils + "'";
                i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
		
	 public DataSet get_Product_stock(string Date, string div_code,string stk_Code)
       {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec get_stock_details '" + Date + "','" + div_code + "','" + stk_Code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		 public DataSet get_access_master_details(string div_code)
        {
            DataSet ds = null;
            DB_EReporting db_ER = new DB_EReporting();

            strQry = "Exec sp_get_access_master_details '" + div_code + "'";

            try
            {
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		
		 public DataSet getallgrnorderdetails(string Stockist_Code, string Div_Code, string FDT, string TDT)
        {

            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "exec sp_Get_GRN_details '" + Stockist_Code + "','" + Div_Code + "','" + FDT + "','" + TDT + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

		
        

    }

}


