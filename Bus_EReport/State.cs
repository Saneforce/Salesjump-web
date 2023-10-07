using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBase_EReport;

namespace Bus_EReport
{
    public class State
    {
        private string strQry = string.Empty;

        public DataSet getState()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " where State_Active_Flag=0 " +
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
        // Sorting
        public DataTable getStateLocationlist_DataTable()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtState = null;

            //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
            //       " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name,  sf_count" +
            //       " from Mas_State b where State_Active_Flag=0 " +
            //       "  order by b.StateName";
            strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where Division_Active_Flag = 0 and charindex(','+cast(st.State_Code as varchar)+',',','+a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                " from Mas_State ST  where State_Active_Flag=0  " +
                " order by ST.StateName";

            try
            {
                dtState = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtState;
        }
        public DataTable getStateLocationlist_DataTable(string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataTable dtState = null;

            strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(st.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                " from Mas_State ST  where State_Active_Flag=0 and LEFT(ST.StateName,1) = '" + sAlpha + "' " +
                " order by ST.StateName";

            try
            {
                dtState = db_ER.Exec_DataTable(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtState;
        }
        public DataSet getStateEd(string statecode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT shortname, statename" +
                     " FROM mas_state WHERE State_Active_Flag=0 And state_code= '" + statecode + "'" +
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
        //Changes done by priya
        public bool RecordExist(string shortname)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(state_code) FROM mas_state WHERE shortname='" + shortname + "' ";
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

        public bool sRecordExist(string statename, int statecode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(state_code) FROM mas_state WHERE statename='" + statename + "'AND State_Code!='" + statecode + "' ";
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

        public bool sRecordExist(int statecode, string statename)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(state_code) FROM mas_state WHERE state_code != '" + statecode + "' AND statename='" + statename + "' ";

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

        public int RecordAdd(string shortname, string statename)
        {
            int iReturn = -1;
            if (!RecordExistAdd(shortname))
            {
                if (!sRecordExistAdd(statename))
                {
                    try
                    {
                        DB_EReporting db = new DB_EReporting();

                        strQry = "SELECT isnull(max(State_Code)+1,'1') State_Code from mas_state ";
                        int State_Code = db.Exec_Scalar(strQry);

                        strQry = "INSERT INTO mas_state(State_Code,shortname,statename,Created_Date,LastUpdt_Date,State_Active_Flag)" +
                                 "values('" + State_Code + "','" + shortname + "', '" + statename + "',getdate(),getdate(),0) ";

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
        public int RecordUpdate(int statecode, string statename)
        {
            int iReturn = -1;

            if (!sRecordExist(statename, statecode))
            {
                try
                {

                    DB_EReporting db = new DB_EReporting();

                    strQry = "UPDATE mas_state " +
                             " SET statename = '" + statename + "' ," +
                             " LastUpdt_Date = getdate() " +
                             " WHERE state_code = '" + statecode + "' and State_Active_Flag=0 ";

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

        //end
        public DataSet getStateProd(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
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
        public DataSet getState(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT 0 as state_code,'---Select---' as statename,'' as shortname " +
                     " UNION " +
                     " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
                     " ORDER BY 2";
            //strQry = " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") " +
            //         " ORDER BY 2";

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

        /* new  */


        /*  */
        public DataSet getStateChkBox(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
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
        public DataSet get_unitconv(string procode, string div_code, string UOT)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "exec get_unitconv'" + procode + "','" + div_code + "' ,'" + UOT + "' ";

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
        public DataSet getUOMChkBox(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "SELECT 0 as Move_MailFolder_Id,'---Select---' as Move_MailFolder_Name " +
                     " UNION " +
                     " SELECT Move_MailFolder_Id,Move_MailFolder_Name " +
                     " FROM Mas_Multi_Unit_Entry " +
                     " WHERE Division_Code in  (" + div_code + ") and Folder_Act_flag=0 ";


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

        public DataSet getStateAddChkBox(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
                     " union select '' state_code,'ALL' statename,'ALL' shortname ORDER BY 2";
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



        // alphabetical
        public DataSet getState_Alphabet()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            strQry = "select '1' val,'All' StateName " +
                     " union " +
                     " select distinct LEFT(StateName,1) val, LEFT(StateName,1) StateName" +
                     " FROM mas_State where State_Active_Flag=0 " +
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
        public DataSet getState_Report(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT 0 as state_code,'---All State---' as statename " +
                     " UNION " +
                     " SELECT state_code,statename " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
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


        public DataSet getState_Alphabet(string sAlpha)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT State_Code,ShortName,StateName " +
            //         " FROM mas_State " +
            //         " where LEFT(StateName,1) = '" + sAlpha + "' and State_Active_Flag=0 " +
            //         " ORDER BY 2";
            //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
            //       " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name, sf_count" +
            //       " from Mas_State b where State_Active_Flag=0 and LEFT(b.StateName,1) = '" + sAlpha + "'" +
            //       "  order by b.StateName";
            strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(st.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                   " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                   " from Mas_State ST  where State_Active_Flag=0 and LEFT(ST.StateName,1) = '" + sAlpha + "' " +
                   " order by ST.StateName";
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

        //

        public int RecordDelete(int statecode)
        {
            int iReturn = -1;
            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM  mas_state WHERE state_code = '" + statecode + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;

        }
        public DataSet getSt(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") and State_Active_Flag=0" +
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
        public bool RecordExist(int State_Code)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                //strQry = "SELECT COUNT(state_code) FROM mas_division WHERE state_code = '" + State_Code + "'  ";
                strQry = " SELECT COUNT(state_code) FROM mas_division WHERE state_code like  '%" + State_Code + "%'";


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

        public int DeActivateNew(int State_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE mas_state " +
                             " SET State_active_flag=1 , " +
                             " LastUpdt_Date = getdate(), State_Deactivate_Date = getdate()" +
                             " WHERE State_Code = '" + State_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet getStateName(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") " +
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
        public DataSet getState_Code(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "SELECT state_code " +
                     " FROM mas_state " +
                     " WHERE state_code in (" + state_code + ") ";

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

        // Changes done by Priya
        public DataSet getState_Division()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            //strQry = " select b.State_Code,b.StateName,b.ShortName, " +
            //         " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name" +                       
            //         " from Mas_State b where State_Active_Flag=0 " +
            //         "  order by b.StateName";

            strQry = " select State_Code,StateName,ShortName, stuff((select ', '+Alias_Name from Mas_Division a where Division_Active_Flag = 0 and charindex(','+cast(st.State_Code as varchar)+',',','+a.State_Code+',')>0 for XML path('')),1,2,'') Division_Name, " +
                     " stuff(isnull((select ', '+sf_Designation_Short_Name+' ( '+CAST(Cnt as varchar)+' )' from  vwSFDesgCnt where State_code=ST.State_code for xml path('')),''),1,2,'') sf_count " +
                     " from Mas_State ST where State_Active_Flag = 0 " +
                     "  group by State_Code,StateName,ShortName";
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
        public DataSet getState_Reactivate()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select b.State_Code,b.StateName,b.ShortName, " +
                     " stuff((select ', '+Alias_Name from Mas_Division a where charindex(cast(b.State_Code as varchar)+',',a.State_Code+',')>0 for XML path('')),1,2,'') division_name" +
                     " from Mas_State b where State_Active_Flag=1 " +
                     "  order by b.StateName";

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
        public int ReActivate_State(int State_Code)
        {
            int iReturn = -1;

            try
            {

                DB_EReporting db = new DB_EReporting();

                strQry = "UPDATE Mas_State " +
                            " SET State_Active_Flag=0 ," +
                            " LastUpdt_Date = getdate(), State_Reactivate_Date= getdate() " +
                            " WHERE State_Code = '" + State_Code + "' ";

                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        //changes by Reshmi
        public bool RecordExistAdd(string shortname)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(state_code) FROM mas_state WHERE shortname='" + shortname + "' ";
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

        public bool sRecordExistAdd(string statename)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "SELECT COUNT(state_code) FROM mas_state WHERE statename='" + statename + "' ";
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
        public DataSet getState_new(string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT 0 as state_code,'---Select State---' as statename,'' as shortname " +
            //         " UNION " +
            //         " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") " +
            //         " ORDER BY 2";
            strQry = " SELECT state_code,statename,shortname " +
                     " FROM mas_state " +
                    " WHERE state_code in (" + state_code + ") and State_Active_Flag=0 " +
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

        public DataSet get_area_count(string state_code, string div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

//            strQry = "  select distinct(select COUNT(State) from Mas_Area where State_Code='" + state_code + "' and Div_Code='" + div_Code + "' and Area_Active_Flag=0 ) areacount," +
//"(select COUNT(Zone_name) from Mas_Zone z inner join Mas_Area a  on a.Area_code=z.Area_code and z.Div_Code=a.Div_Code where a.State_Code='" + state_code + "' and  z.Div_Code='" + div_Code + "' and z.Zone_Active_Flag=0 and a.Area_Active_Flag=0  )zonecount," +
//"(select COUNT(Territory_name) from Mas_Territory t inner join Mas_Zone z on t.Zone_code=z.Zone_code and t.Div_Code=z.Div_Code inner join Mas_Area a on a.Area_code=z.Area_code and a.Div_Code=z.Div_Code where a.State_Code='" + state_code + "' and t.Div_Code='" + div_Code + "'and z.Zone_Active_Flag=0 and a.Area_Active_Flag=0 and t.Territory_Active_Flag=0)territory ," +
//"(select count(Stockist_Name)  from Mas_Stockist s  inner join Mas_Territory t on t.Territory_code=s.Territory_Code and t.Territory_code=s.Territory_Code inner join  Mas_Zone z on z.Zone_code=t.Zone_code and z.Div_Code=t.Div_Code inner join Mas_Area a on a.Area_code=z.Area_code and a.Div_Code=z.Div_Code where a.State_Code='" + state_code + "' and s.Division_Code='" + div_Code + "' and z.Zone_Active_Flag=0 and a.Area_Active_Flag=0 and t.Territory_Active_Flag=0 and Stockist_Active_Flag=0) distributor  from Mas_Zone z, Mas_Area a,Mas_Territory t,Mas_Stockist s";
            strQry="select distinct(0) areacount,(0)zonecount,(0)territory ,(0) distributor  from Mas_Zone z, Mas_Area a,Mas_Territory t,Mas_Stockist s";
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

        public DataSet get_area_Details(string state_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select Area_Name,Zone_name ,Territory_name ,Stockist_name  from Mas_Area a  left outer join Mas_Zone z on a.Area_code=z.Area_code  and z.Div_Code=a.Div_Code left outer join Mas_Territory t on  t.Zone_code=z.Zone_code and z.Div_Code=t.Div_Code   left outer join Mas_Stockist s on t.Territory_code=s.Territory_Code and s.Division_Code=t.Div_Code where a.State_Code='" + state_code + "' and a.Div_Code='" + Div_Code + "'  and a.Area_Active_Flag=0 and z.Zone_Active_Flag=0 and s.Stockist_Active_Flag=0";

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
        public DataSet get_zone_Details(string state_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            //strQry = "SELECT 0 as state_code,'---Select State---' as statename,'' as shortname " +
            //         " UNION " +
            //         " SELECT state_code,statename,shortname " +
            //         " FROM mas_state " +
            //         " WHERE state_code in (" + state_code + ") " +
            //         " ORDER BY 2";
            strQry = "select Zone_name ,Territory_name ,Stockist_name  from Mas_Area a left outer join Mas_Zone z on a.Area_code=z.Area_code and z.Div_Code=a.Div_Code  left outer join Mas_Territory t on  t.Zone_code=z.Zone_code and t.Div_Code=z.Div_Code  left outer join Mas_Stockist s on t.Territory_code=s.Territory_Code and t.Div_Code=s.Division_Code where a.State_Code='" + state_code + "' and  z.Div_Code='" + Div_Code + "' and  z.Zone_Active_Flag=0  and t.Territory_Active_Flag=0 and s.Stockist_Active_Flag=0 ";

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
        public DataSet get_territory_Details(string state_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select Territory_name ,Stockist_name  from Mas_Area a left outer join Mas_Zone z on a.Area_code=z.Area_code and z.Div_Code=a.Div_Code  left outer join Mas_Territory t on  t.Zone_code=z.Zone_code and t.Div_Code=z.Div_Code left outer join Mas_Stockist s on t.Territory_code=s.Territory_Code and t.Div_Code=s.Division_Code where a.State_Code='" + state_code + "' and  t.Div_Code='" + Div_Code + "'    and  t.Territory_Active_Flag=0 and s.Stockist_Active_Flag=0 ";

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
        public DataSet get_area_Details_Territorywise(string Zone_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "select a.Territory_code,a.Territory_Name from Mas_Territory a where a.Zone_code='" + Zone_code + "' and a.Div_Code='" + divcode + "'  ";

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

        public DataSet get_area_Details_Zonewise(string area_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = " select a.Zone_code,a.Zone_Name from Mas_Zone a where a.Area_code='" + area_code + "' and a.Div_Code='" + divcode + "'   ";

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

        public DataSet get_area_Details_Statewise(string state_code, string divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;

            strQry = "  select a.Area_code,a.Area_Name from Mas_Area a where a.State_Code='" + state_code + "' and a.Div_Code='" + divcode + "'";

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
        public DataSet Get_State_Division_Wise(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsState = null;

            strQry = " Exec GET_State_DivisionWise '" + Div_Code + "'";
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

   public DataSet getcountry(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsState = null;
            strQry = "select mc.Country_code,Country_name from mas_division md inner join mas_country mc on charindex(mc.Country_code+',',md.Country_code+',')>0 where md.Division_Code ='" + div_code + "'";
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
    }
}
