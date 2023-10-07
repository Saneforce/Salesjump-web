using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Bus_EReport
{
    public class DSM
    {
        private string strQry = string.Empty;
        public DataSet ZonegetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //strQry = "SELECT Zone_code,a.Zone_sname,a.Zone_name,Area,a.UserName,a.Password," +
            //         "(select count(d.subdivision_code) from Mas_Product_Detail d where charindex(cast(a.Zone_code as varchar),d.subdivision_code )> 0 and d.Division_Code ='" + divcode + "') Sub_Coun" +
            //         " FROM Mas_Zone a WHERE a.Zone_Active_Flag=0 and a.Div_Code= '" + divcode + "'" +
            //         "ORDER BY 2";
            strQry = " select a.DSM_code,a.DSM_sname,a.DSM_name,a.Distributor_Name,a.UserName,a.Password, " +
                     " COUNT(z.Zone) as 'Sub_Coun' from Mas_Territory z full join " +
                     " Mas_DSM a on a.DSM_name=z.Zone and z.Territory_Active_Flag=0 WHERE a.DSM_Active_Flag=0 and a.Div_Code= '" + divcode + "'group by a.DSM_name,a.DSM_code,a.DSM_sname,a.Distributor_Name,a.UserName,a.Password ";
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
        //Giri 23.01.19
        public DataSet SuppliergetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = " select a.S_No,a.S_Name,a.Contact_Person,a.Mobile,a.Erp_Code from" +
                     " supplier_master a WHERE a.Act_flg=0 and a.Division_Code= '" + divcode + "'group by a.S_No,a.S_Name,a.Contact_Person,a.Mobile,a.Erp_Code order by a.S_Name";
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
        //Giri 28.01.19
        public DataSet LeavegetSubDiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            //     strQry = " select a.Leave_code,a.Leave_SName,a.Leave_Name from" +
            //            " mas_Leave_Type a WHERE a.Active_Flag=0 and a.Division_Code= '" + divcode + "'group by a.Leave_code,a.Leave_SName,a.Leave_Name ";


            strQry = "  select a.Leave_code,a.Leave_SName,a.Leave_Name, case  when   sum(LeaveValue) is not null then sum(LeaveValue) else 0 end as LeaveTaken from " +
                " mas_Leave_Type a  left outer join MasEntitlement t on t.LeaveCode=a.Leave_code WHERE a.Active_Flag=0 and a.Division_Code= '" + divcode + "' group by a.Leave_code,a.Leave_SName,a.Leave_Name ";


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
        public DataSet getStatePerDivision(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            strQry = "select Division_Code from Mas_Stockist";
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
        public DataSet getStateProd(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "SELECT 0 as Distributor_Code,'--Select--' as Stockist_Name " +
                         " UNION " +
                         " SELECT Distributor_Code,Stockist_Name " +
                         " FROM Mas_Stockist " +
                         " WHERE Division_Code='" + state_code + "' and Stockist_Active_Flag=0 " +
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
        public DataSet getZone(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT DSM_sname,DSM_name,Distributor_Name,UserName,Password,Town_Name,FO_Name " +
                     " FROM Mas_DSM WHERE DSM_Active_Flag=0 And DSM_code= '" + subdivcode + "' AND Div_Code= '" + divcode + "'" +
                     " ORDER BY 2";
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
        public DataSet getSupplier(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT S_Name,Contact_Person,Mobile,Erp_Code,sf_Code,sf_name,State_Code,UsrDfd_UserName,sf_password,subdivision_code" +
                     " FROM Supplier_Master WHERE Act_flg=0 And S_No= '" + subdivcode + "' AND Division_Code= '" + divcode + "'" +
                     " ORDER BY 2";
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

        public DataSet getLeaveType(string divcode, string subdivcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSubDiv = null;

            strQry = "SELECT Leave_SName,Leave_Name " +
                     " FROM mas_Leave_Type WHERE Active_Flag=0 And Leave_code= '" + subdivcode + "' AND Division_Code= '" + divcode + "'" +
                     " ORDER BY 2";
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
        public bool sZoneRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(DSM_code) FROM Mas_DSM WHERE DSM_sname='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";
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
        public bool sLeaveRecordExist(string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Leave_code) FROM mas_Leave_Type WHERE Leave_SName='" + subdiv_sname + "' and Division_Code= '" + divcode + "' ";
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
        public bool ZoneRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(DSM_code) FROM Mas_DSM WHERE DSM_name='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";
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

        public bool LeaveRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Leave_code) FROM mas_Leave_Type WHERE Leave_Name='" + subdiv_name + "' and Division_Code= '" + divcode + "' ";
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

        public bool SupplierRecordExist(string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(S_No) FROM supplier_master WHERE S_Name='" + subdiv_name + "' and Division_Code= '" + divcode + "' ";
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

        public int ZoneRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Area_name, string Area_cd, string Username, string Password, string Town_Name, string Town_code, string Fo_code, string FO_name)
        {
            int iReturn = -1;
            if (!sZoneRecordExist(subdiv_sname, divcode))
            {
                if (!ZoneRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max( CAST(replace(DSM_code,'DSM','')AS NUMERIC))+1,'1') DSM_code from Mas_DSM";
                        int DSM_Id = db.Exec_Scalar(strQry);
                        string dsm_code = "DSM" + DSM_Id;
                        strQry = "INSERT INTO Mas_DSM(DSM_code,Div_Code,DSM_sname,DSM_name,Distributor_Name,Distributor_Code,Created_Date,LastUpdt_Date,DSM_Active_Flag,UserName,Password,Town_Name,Town_Code,FO_Code,FO_Name)" +
                                 "values('" + dsm_code + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + Area_name + "','" + Area_cd + "',getdate(),getdate(),0,'" + Username + "','" + Password + "','" + Town_Name + "','" + Town_code + "','" + Fo_code + "','" + FO_name + "')";
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

        //Giri Supplier 23-1-2019
       /* public int supplierRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Username, string Password,string sf_code,string sf_name,string HqterrCode)
        {
            int iReturn = -1;

            if (!SupplierRecordExist(subdiv_sname, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "SELECT isnull(max(S_No),0) + 1 from supplier_master";
                    int DSM_Id = db.Exec_Scalar(strQry);

                    strQry = "INSERT INTO supplier_master(S_No,Division_Code,S_Name,Contact_Person,Act_flg,Mobile,Erp_Code,Sub_Division_Code,sf_code,sf_name,TerrHQCode)" +
                             "values('" + DSM_Id + "','" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "',0,'" + Username + "','" + Password + "','" + divcode + "','" + sf_code + "','" + sf_name + "','" + HqterrCode + "')";
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
        }*/


		public int supplierRecordAdd(string divcode, string subdiv_sname, string subdiv_name, string Username, string Password, string sf_code, string sf_name, string HqterrCode, string usrname, string pwd, string addr)
        {
            int iReturn = -1;

            if (!SupplierRecordExist(subdiv_sname, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();
                    strQry = "exec insertsuperstockist '" + divcode + "','" + subdiv_sname + "','" + subdiv_name + "','" + Username + "','" + Password + "','" + sf_code + "','" + sf_name + "','" + HqterrCode + "','" + usrname + "','" + pwd + "','" + addr + "'";
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



        //Giri Leave Type 28.1.19
        public int LeaveRecordAdd(string divcode, string subdiv_sname, string subdiv_name)
        {
            int iReturn = -1;
            if (!sLeaveRecordExist(subdiv_sname, divcode))
            {
                if (!LeaveRecordExist(subdiv_name, divcode))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();
                        strQry = "SELECT isnull(max(Leave_code),0) + 1 from mas_Leave_Type";
                        int DSM_Id = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO mas_Leave_Type(Leave_code,Division_Code,Created_Date,Active_Flag,Leave_SName,Leave_Name)" +
                                 "values('" + DSM_Id + "','" + divcode + "',getdate(),0,'" + subdiv_sname + "','" + subdiv_name + "')";
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

        public bool sZoneRecordExist1(string subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(DSM_code) FROM Mas_DSM WHERE DSM_code != '" + subdivision_code + "' AND DSM_sname ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

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
        public bool sLeaveRecordExist1(string subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Leave_code) FROM mas_Leave_Type WHERE Leave_code != '" + subdivision_code + "' AND Leave_SName ='" + subdiv_sname + "' and Division_Code= '" + divcode + "' ";

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
        public bool ZoneRecordExist1(string subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(DSM_code) FROM Mas_DSM WHERE DSM_code != '" + subdivision_code + "' AND DSM_name ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

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

        public bool LeaveRecordExist1(string subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(Leave_code) FROM mas_Leave_Type WHERE Leave_code != '" + subdivision_code + "' AND Leave_Name ='" + subdiv_name + "' and Division_Code= '" + divcode + "' ";

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

        public bool SupplierRecordExist1(string subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(S_No) FROM supplier_master WHERE S_No != '" + subdivision_code + "' AND S_Name ='" + subdiv_name + "' and Division_Code= '" + divcode + "' ";

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
        public int ZoneRecordUpdate(string subdiv_sname, string subdiv_name, string Area_name, string divcode, string Area_code, string Username, string Password, string Town_code, string Town_Name, string Fo_code, string Fo_Name, string subdivision_code)
        {
            int iReturn = -1;
            if (!sZoneRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!ZoneRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_DSM " +
                                " SET DSM_sname = '" + subdiv_sname + "', " +
                                " DSM_name = '" + subdiv_name + "' ,  " +
                                " Distributor_Name= '" + Area_name + "', " +
                                " Distributor_Code= '" + Area_code + "', " +
                                " UserName= '" + Username + "', " +
                                " Password= '" + Password + "', " +
                                " Town_Name= '" + Town_Name + "', " +
                                " Town_Code= '" + Town_code + "', " +
                                " FO_Code= '" + Fo_code + "', " +
                                " FO_Name= '" + Fo_Name + "', " +
                                " LastUpdt_Date = getdate() " +
                                " WHERE DSM_code = '" + subdivision_code + "' ";

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

        //Giri 23.1.19
        public int SupplierRecordUpdate(string subdiv_sname, string subdiv_name, string divcode, string Username, string Password, string subdivision_code, string sf_code, string sf_name, string terrHQcode, string usrname, string pwd, string addr)
        {
            int iReturn = -1;

            if (!SupplierRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                try
                {
                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE supplier_master " +
                            " SET S_Name = '" + subdiv_sname + "', " +
                            " Contact_Person = '" + subdiv_name + "' ,  " +
                            " Mobile= '" + Username + "', " +
                            " sf_code= '" + sf_code + "', " +
                            " sf_name= '" + sf_name + "', " +
                            " State_Code= '" + terrHQcode + "', " +
                            " Erp_Code= '" + Password + "', " +
                            " UsrDfd_UserName= '" + usrname + "', " +
                            " sf_password= '" + pwd + "'," +
                            " subdivision_code= '" + addr + "'" +
                            " WHERE S_No = '" + subdivision_code + "' ";

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
        //Leave Update

        public int LeaveRecordUpdate(string div_code, string subdiv_sname, string subdiv_name, string subdivision_code)
        {
            int iReturn = -1;
            if (!sLeaveRecordExist1(subdivision_code, subdiv_sname, div_code))
            {
                if (!LeaveRecordExist1(subdivision_code, subdiv_name, div_code))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE mas_Leave_Type " +
                                " SET Leave_SName = '" + subdiv_sname + "', " +
                                " Leave_Name = '" + subdiv_name + "' " +
                                " WHERE Leave_code = '" + subdivision_code + "' ";

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

        public int RecordUpdate(string subdivision_code, string subdiv_sname, string subdiv_name, string Area_name, string divcode, string Area_code)
        {
            int iReturn = -1;
            if (!sZoneRecordExist1(subdivision_code, subdiv_sname, divcode))
            {
                if (!ZoneRecordExist1(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();

                        strQry = "UPDATE Mas_DSM " +
                                 " SET DSM_sname = '" + subdiv_sname + "', " +
                                 " DSM_name = '" + subdiv_name + "' ,  " +
                                 " Distributor_Name= '" + Area_name + "', " +
                                 " Distributor_Code= '" + Area_code + "', " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE DSM_code = '" + subdivision_code + "' ";

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
        public DataSet area_code(string Area_name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select Distributor_Code from Mas_Stockist where Stockist_Name='" + Area_name + "' ";
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
        public DataSet DSM_Count(string count)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            //strQry = "select state_code from mas_division where division_code='" + div_code + "'";
            strQry = "select DSM_name from Mas_DSM where Distributor_Code='" + count + "' and DSM_Active_Flag=0 ";
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
        public DataSet getPool_Name(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsStockist = null;
            strQry = " SELECT '' as Town_code, '--Select--' as Town_name " +
                     " UNION " +
                     " select Town_code,Town_name from Mas_Town where Div_Code = '" + div_code + "'";

            try
            {
                dsStockist = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsStockist;
        }
        public int DSMDeActivate(string subdivision_code,string divCode)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();


                strQry = "UPDATE mas_Leave_Type" +
                            " SET Active_Flag=1 , " +
                            " LastUpdt_Date = getdate() " +
                            " WHERE Leave_code = '" + subdivision_code + "' and Division_Code='" + divCode + "'";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;

        }

        public DataSet savcusmap(string data, string divcode, string supcde, string supname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            strQry = "exec insertSSCustomers '" + divcode + "','" + supcde + "','" + supname + "','" + data + "'";

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

        public string insertDSM(SaveDSM sd)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dss = new DataSet();

            //strQry = "exec insertDSM '" + sd.divcode + "','" + sd.dname + "','" + sd.dtype + "','" + sd.status + "','" + sd.stype + "','" + sd.usrname + "','" + sd.pwd + "','" + sd.dist + "','" + sd.distname + "','" + sd.mobile + "','" + sd.email + "'";
            //try
            // {
            //   dss = db_ER.Exec_DataSet(strQry);
            // }
            //catch (Exception ex)
            //{
            //   throw ex;
            //}


            string msg = string.Empty;
            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString()))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.CommandText = "insertDSM";
                    SqlParameter[] parameters = new SqlParameter[]
                            {
                                        new SqlParameter("@Div", sd.divcode),
                                        new SqlParameter("@dname", sd.dname),
                                        new SqlParameter("@dtype", sd.dtype),
                                        new SqlParameter("@status", sd.status),
                                        new SqlParameter("@stype", sd.stype),
                                        new SqlParameter("@usrname", sd.usrname),
                                        new SqlParameter("@pwd",sd.pwd),
                                        new SqlParameter("@stkcode", sd.dist),
                                        new SqlParameter("@stkname",sd.distname),
                                        new SqlParameter("@mobile",sd.mobile),
                                        new SqlParameter("email",sd.email)

                            };
                    cmd.Parameters.AddRange(parameters);
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        cmd.ExecuteNonQuery();
                        msg = "Success";
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                    }
                }
            }
            return msg;
        }


        public string updateDSM(SaveDSM sd, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dss = new DataSet();
            string msg = string.Empty;
            strQry = "exec updateDSM '" + sd.divcode + "','" + sf_code + "','" + sd.dname + "','" + sd.dtype + "','" + sd.status + "','" + sd.stype + "','" + sd.usrname + "','" + sd.pwd + "','" + sd.dist + "','" + sd.distname + "','" + sd.mobile + "','" + sd.email + "'";
            try
            {
                dss = db_ER.Exec_DataSet(strQry);
                msg = "success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public class SaveDSM
        {
            [JsonProperty("DivCode")]
            public object divcode { get; set; }

            [JsonProperty("DSMName")]
            public object dname { get; set; }

            [JsonProperty("DType")]
            public object dtype { get; set; }

            [JsonProperty("Status")]
            public object status { get; set; }

            [JsonProperty("Salestype")]
            public object stype { get; set; }

            [JsonProperty("UsrName")]
            public object usrname { get; set; }

            [JsonProperty("PWD")]
            public object pwd { get; set; }

            [JsonProperty("Dist")]
            public object dist { get; set; }

            [JsonProperty("Distname")]
            public object distname { get; set; }

            [JsonProperty("Email")]
            public object email { get; set; }

            [JsonProperty("Mobile")]
            public object mobile { get; set; }
        }

        public DataSet getSalesman(string divcode, string dsmcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dss = new DataSet();

            strQry = "select DSM_code,DSM_name,Desig_type,DSM_Active_Flag,Distributor_Code,Distributor_Name,Salestype,UserName,Password,DSM_Email,DSM_Phone_no from Mas_DSM where Div_code='" + divcode + "' and DSM_code='" + dsmcode + "'";

            try
            {
                dss = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dss;
        }

        public DataSet getSalesmanList(string divcode, string SF)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dss = new DataSet();
            /*if (SF == null)
            {
                strQry = "select DSM_code,DSM_name,Desig_type,Distributor_Name,DSM_Active_Flag,Salestype,UserName from Mas_DSM where Div_code='" + divcode + "' and DSM_Active_Flag<>2";
            }
            else
            {
                strQry = "select DSM_code,DSM_name,Desig_type,Distributor_Name,DSM_Active_Flag,Salestype,UserName from Mas_DSM where Div_code='" + divcode + "' and Distributor_Code='" + SF + "' and DSM_Active_Flag<>2";
            }*/
            strQry = "exec get_DSMDetails '" + SF + "','" + divcode + "'";
            try
            {
                dss = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dss;
        }

        public int DeActivate(string sfcode, string stus)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update Mas_DSM set DSM_Active_Flag='" + stus + "' where dsm_code='" + sfcode + "'";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		
	public DataSet Get_Pri_Order_Count(string sf_code, string todate1,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            strQry = "select count(*) from trans_priorder_head where Stockist_Code='" + sf_code + "' and division_code='" + div_code + "' and convert(date,order_date)='" + todate1 + "'";

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

    }
}
