using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;

namespace Bus_EReport
{
    public class UserLogin
    {
        private string strQry = string.Empty;

        public DataSet Process_Login(string usr_id, string pwd)
        {
            if (usr_id == "admin")
            {
                strQry = "select Sf_Code, sf_name," +
                        " case when sf_code='admin' then '' else  left(ms.Division_Code,len(ms.Division_Code))  end as Division_code, " +
                       " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name, Sf_HQ ,sf_status , '' standby,isnull(Exp_Web_Auto,0) ExpType " +
                       "  from Mas_Salesforce ms " +
                       " left join Access_Master ac on replace(ms.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar) " +
                       " Where Sf_UserName= '" + usr_id + "' and Sf_Password= '" + pwd + "'  " +
                       " and sf_status=0 and sf_TP_Active_Flag=0";
            }
            else
            {
                strQry = " select a.Sf_Code, a.sf_name, case when a.sf_code='admin' then '' else  left(a.Division_Code,len(b.Division_Code)) "+
                         "end as Division_code,sf_TP_Active_Flag,sf_type,a.sf_Designation_Short_Name, a.Sf_HQ,a.sf_status,b.Division_Name,b.Standby,a.subdivision_code,isnull(Exp_Web_Auto,0) ExpType " +
                         "from "+
                         "Mas_Salesforce a inner join Mas_Division b on charindex(','+cast(b.Division_Code as varchar)+',',','+a.Division_Code +',')>0 " +
                         " inner join Access_Master ac on replace(a.Division_Code,',','')=cast(replace(ac.division_code,',','') as varchar)" +
                         "where (a.UsrDfd_UserName = '" + usr_id + "') and a.Sf_Password= '" + pwd + "' ";
            }

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

          
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

        public DataSet HO_Login(string usr_id, string pwd, string shrtname, string Menu_type)
        {
            if (Menu_type == "h")
            {
                strQry = " declare @SubDivicode as varchar(100) " +
        " declare @standby as varchar(100) " +
        " set @SubDivicode=(select Division_Name from Mas_Division where Division_Code = " +
        " (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
        " Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  " +
        " set @standby=(select standby from Mas_Division where Division_Code = (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
        " Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag =0))  " +
        " select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,isnull(sub_div_code,'0')sub_div_code from Mas_HO_ID_Creation " +
        "  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag=0  ";
            }
            else if (shrtname == "sanfmcg")
            {
                strQry = " declare @SubDivicode as varchar(100) " +
                       " declare @standby as varchar(100) " +
                       " set @SubDivicode=(select Division_Name from Mas_Division where Division_Code = " +
                       " (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
                       " Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  " +
                       " set @standby=(select standby from Mas_Division where Division_Code = (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
                       " Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag =0))  " +
                       " select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,isnull(sub_div_code,'0')sub_div_code from Mas_HO_ID_Creation " +
                       "  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag=0  ";
            }
            else
            {
                strQry = " declare @SubDivicode as varchar(100) " +
            " declare @standby as varchar(100) " +
            "declare @urlshrtname as varchar(80)  " +
            " set @SubDivicode=(select Division_Name from Mas_Division where Division_Code = " +
            " (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
            " Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  " +
            " set @urlshrtname=(select Url_Short_Name from Mas_Division  where Div_Sl_No=( select replace(Division_Code,',','') from Mas_HO_ID_Creation  Where User_Name= '" + usr_id + "' and Password = '" + pwd + "' and HO_Active_Flag =0))  " +
            " set @standby=(select standby from Mas_Division where Division_Code = (select reverse(stuff(reverse(Division_Code), 1, 10, '')) from Mas_HO_ID_Creation " +
            " Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and HO_Active_Flag =0))  " +
            " select HO_ID, User_Name,Division_Code,Password,@SubDivicode as name,Name as Corporate,@standby as standby,isnull(sub_div_code,'0')sub_div_code from Mas_HO_ID_Creation " +
            //"  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "' and @urlshrtname='" + shrtname + "' and HO_Active_Flag=0  ";
             "  Where User_Name= '" + usr_id + "' and Password =  '" + pwd + "'  and HO_Active_Flag=0  ";
            }
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


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

        public DataSet Check_Mail(string sf_code, string div_code)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsMail = null;

            strQry = "EXEC CheckMail '" + sf_code + "', '" +  Convert.ToInt32(div_code) + "' ";

            try
            {
                dsMail = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsMail;
        }
        public DataSet Process_LoginMr(string Sf_Code, string Sf_Password)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name," +
                    " case when sf_code='admin' then '' else  left(Division_Code,len(Division_Code))  end as Division_code, " +
                   " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ " +
                   "  from Mas_Salesforce " +
                   " Where Sf_Code='" + Sf_Code + "' and sf_status=0  " +
                    " (select Password from Mas_HO_ID_Creation where Password='" + Sf_Password + "')";
               

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
        public DataSet ProcessMgr_LoginMr(string Sf_Code, string Sf_Password, string smgr)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select Sf_Code, sf_name,sf_type,sf_Designation_Short_Name as Designation_Short_Name,Sf_HQ," +
                     " Division_code " +                   
                     " from Mas_Salesforce " +
                     " Where sf_code='" + Sf_Code + "' " +
                     "(select Sf_Password from Mas_Salesforce where sf_code = '" + smgr + "' and Sf_Password='" + Sf_Password + "')";


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
	public DataSet DSM_Login(string usr_id, string pwd,string shrtname)
        {

                 
   //            strQry = "declare @Divicode as varchar(100)"+
   //"set @Divicode=( select Division_Code from Mas_Division where Url_Short_Name='" + shrtname + "') " +
   //          "SELECT a.Sf_Code, a.sf_name,a.Division_Code,a.sf_TP_Active_Flag,a.sf_type,a.sf_status from vwUserDetails a WHERE  a.UsrDfd_UserName ='"+ usr_id+"' and a.Sf_Password='"+pwd+"' and a.sf_TP_Active_Flag=0";
            strQry = "SELECT a.Sf_Code, a.sf_name,a.Division_Code,a.sf_TP_Active_Flag,a.sf_type,a.sf_status,State_Code,a.SF_UserName from vwUserDetails a WHERE a.UsrDfd_UserName ='" + usr_id + "' and a.Sf_Password='" + pwd + "'";
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;


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
    public DataSet Process_Menu(string usr_id,string id)
    {

        strQry = "select Reporting_To from Mas_HO_ID_Creation  Where Division_Code = '" + usr_id + "'and Menu_type='" + "h" + "'and HO_ID='"+id+"'";


        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;


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
    public DataSet Process_Type(string usr_id, string pwd)
    {
        if (usr_id == "admin")
        {
            strQry = "select Sf_Code, sf_name," +
                    " case when sf_code='admin' then '' else  left(Division_Code,len(Division_Code))  end as Division_code, " +
                   " sf_TP_Active_Flag,sf_type,sf_Designation_Short_Name, Sf_HQ ,sf_status , '' standby" +
                   "  from Mas_Salesforce " +
                   " Where Sf_UserName= '" + usr_id + "' and Sf_Password= '" + pwd + "'  " +
                   " and sf_status=0 and sf_TP_Active_Flag=0";
        }
        else
        {


            strQry = " select Menu_type from Mas_HO_ID_Creation a  Where (a.User_Name = '" + usr_id + "') and a.Password= '" + pwd + "'and HO_Active_Flag=0 ";
        }

        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsDivision = null;


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

   }
}
