using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Data.SqlClient;
using System.Configuration;

namespace Bus_EReport
{
    public class MenuCreation
    {
        private string strQry = string.Empty;

        DataTable dat = null;
        public DataSet menutype()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "select Menu_ID,Menu_Name from Menu_Creation where Menu_Type=0 or Menu_Type=1";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public int AddRecord(string Menu_Name, string Menu_type, string parentmenuvlue, string Link_Url, string icon,string menu_id)
        {
            int i = 0;
            if (menu_id == "")
            {
                if (!RecordExist(Menu_Name))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "select isnull(max(Menu_ID)+1,'1')  Menu_ID from Menu_Creation";
                        int getid = db.Exec_Scalar(strQry);

                        strQry = "insert into Menu_Creation(Menu_ID,Menu_Name,Menu_Type,Parent_Menu,Link_Url,Menu_icon,Active_flag,Dfault_Screen,order_id,Tbar_id)" +
                            "values('" + getid + "','" + Menu_Name + "','" + Menu_type + "','" + parentmenuvlue + "','" + Link_Url + "','" + icon + "',0,0,'" + getid + "',0)";

                        i = db.ExecQry(strQry);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            else
            {
                DB_EReporting db = new DB_EReporting();
                strQry = "update menu_creation set Menu_Name='" + Menu_Name + "',Menu_Type='" + Menu_type + "',Parent_Menu='" + parentmenuvlue + "',Link_Url='" + Link_Url + "',Menu_icon='" + icon + "' where Menu_ID='" + menu_id + "'";
                i = db.ExecQry(strQry);

            }
            return i;
        }


        public bool RecordExist(string Menu_Name)
        {

            bool recordexists = false;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Menu_ID) from Menu_Creation where Menu_Name='" + Menu_Name + "'";
                int a = db.Exec_Scalar(strQry);

                if (a > 0)
                {
                    recordexists = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recordexists;
        }

        public DataSet getComName()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "select Division_Code,Division_Name from Mas_Division where Division_Active_Flag=0 ";
                ds = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public DataSet getadminName_mashoid(string comnm)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "select HO_ID,User_Name from mas_ho_id_creation where charindex(','+'" + comnm + "'+',',','+Division_Code +',')>0 and HO_Active_Flag=0";
                ds = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public DataTable getMenuByUser(string div, string Sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "exec [dbo].[get_MenuByUser] '" + div + "','" + Sf_code + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }

        public DataSet admingetselids(string adname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "select Comp_Name,(','+Menu_IDs+',') Menu_IDs from Mas_MenuPermissions_Head where Comp_ID='" + adname + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
		  public DataSet admingethoselids(string adname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "select HO_ID,(','+Menu_IDs+',') Menu_IDs from Mas_ho_id_creation where HO_ID='"+ adname + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        } 
		public DataSet getComNamebydiv(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "select Division_Code,Division_Name from Mas_Division where Division_Active_Flag=0 and Division_Code='" + divcode + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }
		public int AddMenuPermissionValues_mashoid(int adid, string adname, string result)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();


            if (RecordExistmas(adid, adname))
            {
                strQry = "update mas_ho_id_creation set Menu_IDs='" + result + "' where HO_ID='" + adid + "'";
                i = db_ER.ExecQry(strQry);
            }
            else
            {

                try
                {
                    


                    strQry = "insert into mas_ho_id_creation(HO_ID,User_Name,Menu_IDs)" +
                   "values('" + adid + "','" + adname + "','" + result + "')";

                    i = db_ER.ExecQry(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return i;

        }
		 public bool RecordExistmas(int adid, string adname)
        {

            bool recordexists = false;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(HO_ID) from mas_ho_id_creation where HO_ID='" + adid + "'";
                int a = db.Exec_Scalar(strQry);

                if (a > 0)
                {
                    recordexists = true;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recordexists;
        }
		   public DataTable selgetMenuBycompany(string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {

                strQry = "exec sel_get_companymenus " + divcode + "";
                ds = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }
        public DataSet getdesignation(string value)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "select Designation_Code,Designation_Name from Mas_SF_Designation where Division_Code='" + value + "' union all select 0 as Designation_Code,'admin' as Designation_Name";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getselids(string comname)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "select Comp_Name,(','+Menu_IDs+',') Menu_IDs from Mas_MenuPermissions_Head where Comp_ID='" + comname + "' ";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        public DataSet getdatatocheck()
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                strQry = "select Menu_ID,Menu_Name from Menu_Creation where Menu_Type='0' ";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataTable getMenuByParent(string PMnuId, int lvl = 0, string MMnu = "")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                lvl = lvl + 1;
                if (MMnu == "") MMnu = PMnuId;
                strQry = "exec GetFMCGMenusList '" + PMnuId + "'," + lvl + ",'" + MMnu + "'";
                ds = db_ER.Exec_DataSet(strQry);
                if (dat == null) dat = ds.Tables[0].Clone();

                for (int il = 0; il < ds.Tables[0].Rows.Count; il++)
                {

                    dat.Rows.Add(ds.Tables[0].Rows[il].ItemArray);
                    getMenuByParent(ds.Tables[0].Rows[il]["Menu_ID"].ToString(), lvl, MMnu);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dat;
        }
        public DataTable getMenuBycompany(string div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {
                
                strQry = "exec get_companymenus '" + div + "'";
                ds = db_ER.Exec_DataSet(strQry);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds.Tables[0];
        }

        public DataSet getallmenu(string menutype)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;

            try
            {

                strQry = "exec GetFMCGMenusList '" + menutype + "'";
                // strQry = "select * from Menu_Creation  where Menu_Name='"+menuname+"' or Parent_Menu='0'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }



        public bool RecordExist(int cid, string did)
        {

            bool recordexists = false;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Menu_Slno) from Mas_MenuPermissions_Head where Comp_ID='" + cid + "'";
                int a = db.Exec_Scalar(strQry);

                if (a > 0)
                {
                    recordexists = true;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recordexists;
        }



        public int AddMenuPermissionValues(int cid, string cname, string result)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();


            if (RecordExist(cid, cname))
            {
                strQry = "update Mas_MenuPermissions_Head set Menu_IDs='"+ result + "' where Comp_ID='"+cid+ "'";
                i = db_ER.ExecQry(strQry);
            }
            else
            {

                try
                {
                    strQry = "select isnull(max(Menu_Slno)+1,'1')  Menu_Slno from Mas_MenuPermissions_Head";
                    int getid = db_ER.Exec_Scalar(strQry);


                    strQry = "insert into Mas_MenuPermissions_Head(Menu_Slno,Comp_ID,Comp_Name,Desig_Name,Menu_IDs)" +
                   "values('" + getid + "','" + cid + "','" + cname + "','','" + result + "')";

                    i = db_ER.ExecQry(strQry);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return i;

        }
        public int newAddMenuPermissionValues(string sf_codes, string arr)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();
               try
                {

                   strQry = "update mas_salesforce set SF_Per_ContactAdd_One='" + arr + "' where Sf_Code='" + sf_codes + "'";
                    i = db_ER.ExecQry(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            
            return i;
        }
        public int DesMenuPermissionValues(string des_codes, string arr)
        {
            int i = 0;
            DB_EReporting db_ER = new DB_EReporting();
            try
            {

                // strQry = "update Mas_SF_Designation set Des_Rights='" + arr + "' where Designation_Code='" + des_codes + "'";
                strQry = "exec Des_MenuRights '" + des_codes + "','" + arr + "'";
                 i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }
        public DataSet GetDataToView()
        {
            int i = 0;
            DataSet ds = null;
            try
            {
                DB_EReporting db_ER = new DB_EReporting();

                strQry = "select * from Menu_Creation";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }

        public DataSet GetPermissionValue(int companyid, int desgid)
        {

            int i = 0;
            DataSet ds = null;
            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                strQry = "with menuidvalues " +
                  " AS (select Desig_Name, Comp_ID, Split.a.value('.', 'VARCHAR(100)') AS Menu_IDs FROM(SELECT Menu_Slno, Desig_Name, Comp_ID, CAST('<a>' + REPLACE(Menu_IDs, ',', '</a><a>') + '</a>' AS XML) AS id" +
                  " FROM  Mas_MenuPermissions_Head) AS A CROSS APPLY id.nodes('/a') AS Split(a)" +
                  " )select distinct m.Menu_ID,m.Menu_Name,m.Menu_Type,m.Parent_Menu from menuidvalues mm join Menu_Creation m on mm.Menu_IDs = m.Menu_ID where mm.Desig_Name='" + desgid + "' and mm.Comp_ID = '" + companyid + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;

        }


        public int UpdateDataToUser(int cid, string did, string menuid)
        {
            int i = 0;

            try
            {
                DB_EReporting db_ER = new DB_EReporting();
                strQry = "update Mas_MenuPermissions_Head set User_Menu_IDs='" + menuid + "' where Comp_ID='" + cid + "' and Desig_Name='" + did + "'";
                i = db_ER.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return i;
        }



        public bool RecordExistInPermission(int cid, string did)
        {

            bool recordexists = false;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(Menu_Slno) from Mas_MenuPermissions_Head where Comp_ID='" + cid + "' and  Desig_Name ='" + did + "'";
                int a = db.Exec_Scalar(strQry);

                if (a > 0)
                {
                    recordexists = true;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recordexists;
        }


        public DataSet GetDesingIDValues(int cid, string did)
        {
            DataSet ds = new DataSet();
            DB_EReporting db_ER = new DB_EReporting();

            if (RecordExistInPermission(cid, did))
            {
                strQry = "with menuidvalues " +
                 " AS (select Desig_Name, Comp_ID, Split.a.value('.', 'VARCHAR(100)') AS Menu_IDs FROM(SELECT Menu_Slno, Desig_Name, Comp_ID, CAST('<a>' + REPLACE(Menu_IDs, ',', '</a><a>') + '</a>' AS XML) AS id" +
                 " FROM  Mas_MenuPermissions_Head) AS A CROSS APPLY id.nodes('/a') AS Split(a)" +
                 " )select distinct m.Menu_ID,m.Menu_Name,m.Menu_Type,m.Parent_Menu from menuidvalues mm join Menu_Creation m on mm.Menu_IDs = m.Menu_ID where mm.Desig_Name ='" + did + "' and mm.Comp_ID = '" + cid + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            else
            {

            }
      
            return ds;
        }
        public DataSet MenuByPermission(string desgntype, string div)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "exec getmenubypermission '" + desgntype + "','" + div + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet MenuByPermission(string SF)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "exec getUserMenus '" + SF + "'";
                ds = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataTable getMenuCompany(string CmpID="0")
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable ds = null;

            try
            {
                strQry = "exec getCompanywiseMenus " + CmpID + "";
                ds = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
		public DataSet getMenuByParenthoid(string SF,string div_code,string mentyp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet ds = null;
            try
            {
                strQry = "exec getUserMenushoid '" + SF + "','" + div_code + "','" + mentyp + "'";
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
