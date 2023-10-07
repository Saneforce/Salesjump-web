using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DBase_EReport;
using System.Configuration;

namespace Bus_EReport
{
    public class TourPlan
    {

        private string strQry = string.Empty;

        public DataSet getEmptyTourPlan(DateTime dtCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dtDiff = dtCurrentDate;
            DataSet dsListedDR = null;
            DataSet dsDiff = null;
            int iDiff = -1;


            strQry = "Select dateadd(day, 0 - day(dateadd(month, 1 , '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "' )), " +
                                                  "dateadd(month, 1 , '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "')) ";

            dsDiff = db_ER.Exec_DataSet(strQry);

            if (dsDiff.Tables[0].Rows.Count > 0)
            {
                dtDiff = Convert.ToDateTime(dsDiff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            }

            strQry = "Select datediff(day, '" + dtCurrentDate.Month + "-" + dtCurrentDate.Day + "-" + dtCurrentDate.Year + "' ," +
                                           "'" + dtDiff.Month + "-" + dtDiff.Day + "-" + dtDiff.Year + "'  ) ";

            dsDiff = db_ER.Exec_DataSet(strQry);
            if (dsDiff.Tables[0].Rows.Count > 0)
            {
                iDiff = Convert.ToInt32(dsDiff.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                iDiff = iDiff + 1;
            }

            strQry = " SELECT TOP " + iDiff + " '' TP_Date,'' TP_Day " +
                     " FROM  sys.tables ";
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

        public DataSet Find_TP_MMYYYY(string sf_code, int iMonth, int iYear)
        {
            DataSet iReturn = null;
            DB_EReporting db = new DB_EReporting();

            strQry = "select tour_date from Trans_TP_One " +
                     " where SF_Code='" + sf_code + "' and Tour_Month=" + iMonth + " and " +
                     " Tour_Year=" + iYear + " and (Change_Status=1 or Change_Status=2) and isnull(confirmed,'')=0 " +
                      "union all " +
                     " select tour_date from Trans_TP " +
                     " where SF_Code='" + sf_code + "' and Tour_Month=" + iMonth + " and " +
                     " Tour_Year=" + iYear + " and (Change_Status=1 or Change_Status=2) and isnull(confirmed,'')=0";

            iReturn = db.Exec_DataSet(strQry);

            return iReturn;
        }


        public int RecordAdd(string TP_Date, string TP_Day, string TP_Terr, string TP_Terr1, string TP_Terr2, string worktype_Code, string worktype_Name, string TP_Objective, bool TP_Submit, string sf_code, string Territory_Code1, string Territory_Code2, string Territory_Code3, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                //int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);


                if (TP_Submit == false)
                {
                    // Change_Status - 0 : Not Completed
                    // WorkType_Code - 0 : MR

                    strQry = " select Objective from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                             " union all " +
                             " select Objective from Trans_TP " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "'";

                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 0, getdate(),'" + Territory_Code1 + "','" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);

                        strQry = "update Mas_Salesforce set Last_TP_Date='" + strTPdt + "' where Sf_Code='" + sf_code + "'";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=0,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "', Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "', " +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = " update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=0,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "'" +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }

                }
                else if (TP_Submit == true)
                {
                    // Change_Status - 1 : Completed
                    // WorkType_Code - 0 : MR

                    strQry = " select Objective from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                             " union all " +
                             " select Objective from Trans_TP " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "'";

                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                 " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                 " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                 " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                 " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 1, getdate(),'" + Territory_Code1 + "'," +
                                 "'" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);
                        strQry = "update Mas_Salesforce set Last_TP_Date='" + strTPdt + "' where Sf_Code='" + sf_code + "'";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = " update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1,submission_date=getdate(), " +
                                " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "'  " +
                                " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        // Added by Sridevi - 13-Nov-15

        public int RecordAdd_New(string TP_Date, string TP_Day, string TP_Terr, string TP_Terr1, string TP_Terr2, string worktype_Code, string worktype_Name, string TP_Objective, bool TP_Submit, string sf_code, string Territory_Code1, string Territory_Code2, string Territory_Code3, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
              
                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);


                if (TP_Submit == false)
                {
                    strQry = " select Objective from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                            

                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 0, getdate(),'" + Territory_Code1 + "','" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);

                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=0,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "', Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "', " +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                        strQry = " update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=0,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "'" +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);
                    }

                }
                else if (TP_Submit == true)
                {

                    strQry = " select Objective from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                         
                    dsListedDR = db.Exec_DataSet(strQry);

                    if (dsListedDR.Tables[0].Rows.Count == 0)
                    {
                        strQry = " insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2, " +
                                 " Tour_Schedule3,Objective, Worked_With_SF_Code,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Territory_Code2,Territory_Code3,Confirmed,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name) " +
                                 " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + strTPdt + "', " +
                                 " '" + TP_Terr + "','" + TP_Terr1 + "','" + TP_Terr2 + "','" + TP_Objective + "', 'Tour Schedule', " +
                                 " '" + worktype_Code + "','" + worktype_Name + "','" + Division_Code + "', 1, getdate(),'" + Territory_Code1 + "'," +
                                 "'" + Territory_Code2 + "','" + Territory_Code3 + "',0,'" + ddlValueWT1 + "','" + ddlTextWT1 + "','" + ddlValueWT2 + "','" + ddlTextWT2 + "','" + SF_Name + "') ";

                        iReturn = db.ExecQry(strQry);
                       
                        strQry = "update Mas_Salesforce set Last_TP_Date='" + strTPdt + "' where Sf_Code='" + sf_code + "'";

                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = " update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='Tour Schedule',Change_Status=1,submission_date=getdate(), " +
                                 " Tour_Schedule1='" + TP_Terr + "',Tour_Schedule2='" + TP_Terr1 + "',Tour_Schedule3='" + TP_Terr2 + "', " +
                                 " WorkType_Code_B = '" + worktype_Code + "',Worktype_Name_B='" + worktype_Name + "',Territory_Code1='" + Territory_Code1 + "',Territory_Code2='" + Territory_Code2 + "',Territory_Code3='" + Territory_Code3 + "', " +
                                 " WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "',WorkType_Code_B2='" + ddlValueWT2 + "'," +
                                 " Worktype_Name_B2='" + ddlTextWT2 + "',TP_Sf_Name='" + SF_Name + "' " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + strTPdt + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                        iReturn = db.ExecQry(strQry);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }
        public DataSet GetTPConfirmed_Date(string Tour_Month, string Tour_Year, string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;
            //if (!RecordExist(div_sname))
            //{
            try
            {
                strQry = "select count(Confirmed_Date) Confirmed_Date from Trans_TP where Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' and SF_Code='" + sf_code + "' and confirmed=0 and change_status=1";

                dsTP = db.Exec_DataSet(strQry);

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
            return dsTP;
        }
        public int RecordAddMGR(string TP_Date, string TP_Day, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code)
        {
            int iReturn = -1;

            //if (!RecordExist(div_sname))
            //{
            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();

                int Division_Code = -1;
                int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                //strQry = "SELECT ISNULL(MAX(Territory_Code),0)+1 FROM Mas_Territory_Creation WHERE Division_Code = '" + Division_Code + "' ";
                //Territory_Code = db.Exec_Scalar(strQry);

                if (TP_Submit == false)
                {
                    // Change_Status - 0 : Not Completed
                    // WorkType_Code - 0 : MR

                    strQry = "select COUNT(Objective) from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    iCount = db.Exec_Scalar(strQry);

                    if (iCount <= 0)
                    {
                        strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule,Objective, " +
                             " Worked_With_SF_Code,WorkType,Division_Code,Change_Status,WorkType_Code,submission_date) " +
                             " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan + "',  '" + TP_WW + "', " +
                             " '" + TP_Objective + "', '" + TP_Terr + "','" + worktype + "', '" + Division_Code + "', 0, 0,getdate()) ";
                    }
                    else
                    {
                        strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "'," +
                                 " WorkType = '" + worktype + "' , " +
                                 " Tour_Schedule='" + TP_WW + "', submission_date=getdate() " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    }

                }
                else if (TP_Submit == true)
                {
                    // Change_Status - 1 : Completed
                    // WorkType_Code - 0 : MR

                    strQry = "select COUNT(Objective) from Trans_TP_One " +
                             " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    iCount = db.Exec_Scalar(strQry);

                    if (iCount <= 0)
                    {
                        strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule,Objective, " +
                             " Worked_With_SF_Code,WorkType,Division_Code,Change_Status,WorkType_Code,submission_date) " +
                             " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan + "',  '" + TP_WW + "', " +
                             " '" + TP_Objective + "', '" + TP_Terr + "',  '" + worktype + "','" + Division_Code + "', 1, 0,getdate()) ";
                    }
                    else
                    {
                        strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "'," +
                             " WorkType = '" + worktype + "' , Tour_Schedule='" + TP_WW + "',Change_Status=1,submission_date=getdate() " +
                                 " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                                 " Tour_Date =  '" + dt_TourPlan + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";
                    }
                }

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

        public int RecordAddMGRApproval_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();
                DataSet dsWT = new DataSet();

                int Division_Code = -1;
                //int iCount = -1;

                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr where WorkType_Code_M='" + TP_WW + "' and Division_Code='" + Div_Code + "'";
                dsWT = db.Exec_DataSet(strQry);
                TP_WW = dsWT.Tables[0].Rows[0][0].ToString();

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                // Change_Status - 1 : Completed
                // WorkType_Code - 0 : MR

                strQry = "select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    //strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //        " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                    //        " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                    //        "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    //iReturn = db.ExecQry(strQry);

                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                             "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                             "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                             "Division_Code,GETDATE(),'C'Mode from Trans_TP where SF_Code='" + sf_code + "' " +
                             "and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                           "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                           "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                           "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + sf_code + "' " +
                           "and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    iReturn = db.ExecQry(strQry);
                }

                strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                         "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                         "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                         "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                         "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                         "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                iReturn = db.ExecQry(strQry);



            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }



        public int RecordEditMGR_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                //int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = " select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worked_With_SF_Name='" + TP_WWName + "' and Objective='" + TP_Objective + "' " +
                            " and Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and Worktype_Name_B='" + worktype + "' " +
                            " and Tour_Schedule1='" + WorkType_Name + "' and Worked_With_SF_Name='" + TP_WWName + "' and Objective='" + TP_Objective + "' " +
                            " and Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }
                //else
                //{                   

                //    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                //             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                //             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                //             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                //    iReturn = db.ExecQry(strQry);

                //    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                //             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                //             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                //             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                //    iReturn = db.ExecQry(strQry);
                //}



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

        public int RecordAddMGR_TP(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName, string Div_Code)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();
                DataSet dsWT = new DataSet();

                int Division_Code = -1;
                //int iCount = -1;

                strQry = "select WorkType_Code_M from Mas_WorkType_Mgr where WorkType_Code_M='" + TP_WW + "' and Division_Code='" + Div_Code + "'";
                dsWT = db.Exec_DataSet(strQry);
                TP_WW = dsWT.Tables[0].Rows[0][0].ToString();

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                            " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                            "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    //strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //         "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                    //         "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                    //         "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + sf_code + "' and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    //iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }



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

        public int Approve(string MR_Code, string MR_Month, string MR_Year, string sf_code, string Division_Code, string ddlWT, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string TP_Date, string TP_Terr_Value, string TP_Terr1_Value, string TP_Terr2_Value, string ddlWT_Value, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;
            //int iCount = -1;

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
            string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
            DataSet dsListedDR = new DataSet();

            try
            {
                DB_EReporting db = new DB_EReporting();

                 strQry = " Select SF_Code from Trans_TP_One " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month=" + MR_Month + " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' " +
                         " and Tour_Schedule3='" + TP_Terr2_Name + "' and Worktype_Name_B ='" + ddlWT + "' and Tour_Year=" + MR_Year + " " +
                         " and Division_Code= '" + Division_Code + "'and Tour_Date='" + strTPdt + "' " +
                         " union all " +
                         " Select SF_Code from Trans_TP " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month=" + MR_Month + " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' " +
                         " and Tour_Schedule3='" + TP_Terr2_Name + "' and Worktype_Name_B ='" + ddlWT + "' and Tour_Year=" + MR_Year+ " " +
                         " and Division_Code= '" + Division_Code + "'and Tour_Date='" + strTPdt + "'";


                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                             "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                             "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                             "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + MR_Code + "' and Tour_Date='" + strTPdt + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    //strQry = " Update Trans_TP set confirmed=1, confirmed_date=getdate(),Change_Status =1" +
                    //         " where SF_Code='" + MR_Code + "' and Tour_Date='" + strTPdt + "'";

                    //iReturn = db.ExecQry(strQry);
                }

                strQry = " Update Trans_TP set confirmed=1, confirmed_date=getdate(),Change_Status =2,Tour_Schedule1='" + TP_Terr_Name + "'," +
                         " Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "',Territory_Code1='" + TP_Terr_Value + "', " +
                         " Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "',WorkType_Code_B='" + ddlWT_Value + "', " +
                         " Worktype_Name_B ='" + ddlWT + "',WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "', " +
                         " WorkType_Code_B2='" + ddlValueWT2 + "',Worktype_Name_B2='" + ddlTextWT2 + "',TP_Approval_MGR='" + SF_Name + "' where SF_Code='" + MR_Code + "'  " +
                         " and Tour_Date='" + strTPdt + "'";

                iReturn = db.ExecQry(strQry);

                strQry = " Update Trans_TP_One set confirmed=1, confirmed_date=getdate(),Change_Status =2,Tour_Schedule1='" + TP_Terr_Name + "'," +
                         " Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "',Territory_Code1='" + TP_Terr_Value + "', " +
                         " Territory_Code2='" + TP_Terr1_Value + "',Territory_Code3='" + TP_Terr2_Value + "',WorkType_Code_B='" + ddlWT_Value + "', " +
                         " Worktype_Name_B ='" + ddlWT + "',WorkType_Code_B1='" + ddlValueWT1 + "',Worktype_Name_B1='" + ddlTextWT1 + "', " +
                         " WorkType_Code_B2='" + ddlValueWT2 + "',Worktype_Name_B2='" + ddlTextWT2 + "',TP_Approval_MGR='" + SF_Name + "' where SF_Code='" + MR_Code + "'  " +
                         " and Tour_Date='" + strTPdt + "'";

                iReturn = db.ExecQry(strQry);

                strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name," +
                         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                         " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                         " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                         " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Date='" + strTPdt + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }


        public int Approve_New(string MR_Code, string MR_Month, string MR_Year, string sf_code, string Division_Code, string ddlWT, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string TP_Date, string TP_Terr_Value, string TP_Terr1_Value, string TP_Terr2_Value, string ddlWT_Value, string ddlValueWT1, string ddlTextWT1, string ddlValueWT2, string ddlTextWT2, string SF_Name)
        {
            int iReturn = -1;
            //int iCount = -1;

            DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
            string strTPdt = dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year;
            DataSet dsListedDR = new DataSet();

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = " Select SF_Code from Trans_TP_One " +
                        " where SF_Code='" + MR_Code + "' and Tour_Month=" + MR_Month + " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' " +
                        " and Tour_Schedule3='" + TP_Terr2_Name + "' and Worktype_Name_B ='" + ddlWT + "' and Tour_Year=" + MR_Year + " " +
                        " and Division_Code= '" + Division_Code + "'and Tour_Date='" + strTPdt + "' ";
                      
                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    //strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //         "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                    //         "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                    //         "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + MR_Code + "' and Tour_Date='" + strTPdt + "'";

                    //iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name," +
                             " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR) " +
                             " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name, " +
                             " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                             " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Date='" + strTPdt + "' ";

                    iReturn = db.ExecQry(strQry);

                }

             
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int TP_Delete(string MR_Code, string Tour_Month, string Tour_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Delete from Trans_TP_One where SF_Code='" + MR_Code + "' and Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Reject(string sf_code, string RejectReason, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string lblDate, string txtReason, string strMonth, string Sf_Name)
        {
            int iReturn = -1;
            int iCount = -1;
            DateTime dt = Convert.ToDateTime(lblDate.ToString());

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='0',confirmed=0,TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + strMonth + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Trans_TP set Change_Status='0',confirmed=0,TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + strMonth + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + txtReason + "'";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                             " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + sf_code + "' " +
                             " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                             " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                    iReturn = db.ExecQry(strQry);

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                            " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP where SF_Code='" + sf_code + "' " +
                            " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                            " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                    iReturn = db.ExecQry(strQry);

                }

                //else
                //{
                //    strQry = " Update TP_Reject_B_Mgr set Tour_Schedule1='" + TP_Terr_Name + "'," +
                //           "  Tour_Schedule2='" + TP_Terr1_Name + "',Tour_Schedule3='" + TP_Terr2_Name + "' where SF_Code='" + sf_code + "'  ";

                //    iReturn = db.ExecQry(strQry);
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int Reject_New(string sf_code, string RejectReason, string TP_Terr_Name, string TP_Terr1_Name, string TP_Terr2_Name, string lblDate, string txtReason, string strMonth, string Sf_Name)
        {
            int iReturn = -1;
            int iCount = -1;
            DateTime dt = Convert.ToDateTime(lblDate.ToString());

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='0',confirmed=0,TP_Approval_MGR='" + Sf_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + sf_code + "' and Tour_Month='" + strMonth + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + txtReason + "'";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                             " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + sf_code + "' " +
                             " and Tour_Schedule1='" + TP_Terr_Name + "' and Tour_Schedule2='" + TP_Terr1_Name + "' and Tour_Schedule3='" + TP_Terr2_Name + "' " +
                             " and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "'";

                    iReturn = db.ExecQry(strQry);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ReadyforCalanderEdit(string MR_Code, string MR_Month, string MR_Year, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=1,Confirmed=0,Confirmed_Date=GETDATE(),TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int ReadyforCalanderApproval(string MR_Code, string MR_Month, string MR_Year, string sDate, string SF_Name)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select COUNT(*) from Trans_TP where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' ";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {
                    strQry = " update Trans_TP_one set change_status=1,TP_Approval_MGR='" + SF_Name + "',Confirmed=1,Confirmed_Date=GETDATE()" +
                     " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                     " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                    iReturn = db.ExecQry(strQry);

                    strQry = " Insert into Trans_TP(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name" +
                             " ,TP_Sf_Name,TP_Approval_MGR) " +
                             " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                             " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                             " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name,TP_Sf_Name,TP_Approval_MGR from Trans_TP_One " +
                             " where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "'";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = " update Trans_TP set change_status=1,Confirmed=1,Confirmed_Date=GETDATE()" +
                             " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                             " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int ReadyforReject(string MR_Code, string MR_Month, string MR_Year, string RejectReason, DateTime sDate, string div_code, string SF_Name)
        {

            int iReturn = -1;
            int iCount = -1;
            DateTime dt = Convert.ToDateTime(sDate.ToString());

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "Update Trans_TP_One set Change_Status='0',confirmed=0,TP_Approval_MGR='" + SF_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "Update Trans_TP set Change_Status='0',confirmed=0,TP_Approval_MGR='" + SF_Name + "',Rejection_Reason='" + RejectReason + "' where SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iReturn = db.ExecQry(strQry);

                strQry = "select COUNT(*) from TP_Reject_B_Mgr where Rejection_Reason='" + RejectReason + "' and SF_Code='" + MR_Code + "' and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' ";

                iCount = db.Exec_Scalar(strQry);

                if (iCount <= 0)
                {
                    //strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) " +
                    //         " Select SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,GETDATE() from Trans_TP_One where SF_Code='" + MR_Code + "' " +
                    //         " and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' and Tour_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "' ";

                    strQry = " Insert into TP_Reject_B_Mgr(SF_Code,Tour_Month,Tour_Year,Rejection_Reason,Division_Code,Reject_Date) values " +
                             " ('" + MR_Code + "','" + MR_Month + "','" + MR_Year + "','" + RejectReason + "','" + "9" + "',GETDATE())";


                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    strQry = " Update TP_Reject_B_Mgr set Rejection_Reason='" + RejectReason + "',Reject_Date=GETDATE() where SF_Code='" + MR_Code + "' " +
                               " and Tour_Month='" + MR_Month + "' and Tour_Year='" + MR_Year + "' and Reject_Date='" + dt.Month + "-" + dt.Day + "-" + dt.Year + "' ";

                    iReturn = db.ExecQry(strQry);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ReadyforApproval(string MR_Code, string MR_Month, string MR_Year, string SF_Name)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=1,Confirmed=0,TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

                strQry = "update Trans_TP set change_status=1,Confirmed=0,TP_Sf_Name='" + SF_Name + "'" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public int ReadyforCalanderDraft(string MR_Code, string MR_Month, string MR_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "update Trans_TP_One set change_status=0" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }



        public DataSet getHolidays(string state_code, string divcode, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);

            DataSet dsHoliday = null;
            // Modified by sridevi - 8-July-15  replaced '=' by 'like' as statecode is multiple
            strQry = "SELECT convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name" +
                     " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
                     " WHERE a.Division_Code = '" + divcode + "' and (a.state_code like '" + state_code + ',' + "%'  or " +
                     " a.state_code like '%" + ',' + state_code + "' or" +
                     " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "')" +
                     " and a.Holiday_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' and convert(varchar,b.state_code)='" + state_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }



        public DataSet getManagerList(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select Sf_Code,Sf_Name from Mas_Salesforce" +
                     " WHERE sf_type=2 and Reporting_To_SF = '" + sf_code + "' and sf_TP_Active_Flag=0  " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getWorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select WorkType_Code_M,Worktype_Name_M from Mas_WorkType_Mgr " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet getManagerHQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = "select distinct Sf_HQ from Mas_Salesforce" +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " ORDER BY 1";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public int getDivision(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            strQry = "select division_code from Mas_Salesforce" +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " ORDER BY 1";
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

        public int get_TP_Count(string sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            int iReturn = -1;
            strQry = "select COUNT(SF_Code) from Trans_TP_One WHERE sf_code = '" + sf_code + "' and Tour_Date='" + sDate + "' ";

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

        public DataSet get_TP_Count_one(string sf_code, string sDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select COUNT(SF_Code) from Trans_TP_One WHERE sf_code = '" + sf_code + "' and Tour_Date='" + sDate + "' " +
                     " union all " +
                     "select COUNT(SF_Code) from Trans_TP WHERE sf_code = '" + sf_code + "' and Tour_Date='" + sDate + "'";

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

        public DataSet get_TP_HQ(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            string divcode = getDivision(sf_code).ToString();

            DataSet dsSF = null;
            strQry = "EXEC sp_get_TP_HQ '" + divcode + "', '" + sf_code + "' ";

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

        public DataSet get_TP_FieldForce(string sf_HQ)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sf_code,sf_name from Mas_Salesforce " +
                      " where Sf_Code != 'admin' and Sf_HQ='" + sf_HQ + "' and sf_TP_Active_Flag=0 ";

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

        public DataSet get_MR_Reporting(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;

            //int iCount = -1;



            strQry = "select '0' sf_code,'' sf_name " +
                     " union " +
                     " select  sf_code,sf_name+' - '+Sf_HQ+' - '+sf_Designation_Short_Name from Mas_Salesforce " +
                      " where Sf_Code != 'admin' and TP_Reporting_SF ='" + sf_code + "' and sf_TP_Active_Flag=0 " +
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

        public DataSet get_sf_name_schedule(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select sf_name from Mas_Salesforce " +
                      " where Sf_Code = '" + sf_code + "' ";

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

        public DataSet get_TP_Territory(string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsSF = null;
            strQry = "select Territory_Code,Territory_Name  from Mas_Territory_Creation  " +
                      " where Sf_code ='" + sf_Code + "' and Territory_Active_Flag=0 ";

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

        public DataSet get_TP_EntryforMGR(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select convert(char(10),tour_Date,103) tour_date, Worked_With_SF_Name,worktype_name_b,worktype_name_b1,worktype_name_b2, Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worked_With_SF_Name,objective" +
            //            " from Trans_TP_One " +
            //            " where  sf_code = '" + sf_code + "' " +
            //            " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
            //            " Union All " +
            //            "select convert(char(10),tour_Date,103) tour_date,Worked_With_SF_Name,worktype_name_b,worktype_name_b1,worktype_name_b2, Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worked_With_SF_Name,objective" +
            //            " from Trans_TP " +
            //            " where sf_code = '" + sf_code + "' " +
            //            " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " order by 1";

            //strQry = "select convert(char(10),tour_Date,103) tour_date,Worked_With_SF_Name, " +
            //                   "case worktype_name_b when '' then 'Weekly Off' else worktype_name_b end worktype_name_b,worktype_name_b1,worktype_name_b2," +
            //                   "case Tour_Schedule1 when convert(varchar(50),0) then '' else Territory_Code1 end Tour_Schedule1," +
            //                   "case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
            //                   "case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3," +
            //                   " Worked_With_SF_Name,objective" +
            //                   " from Trans_TP " +
            //                   " where sf_code = '" + sf_code + "' " +
            //                   " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + "  "+
            //                   " Union " +
            //                   " select convert(char(10),tour_Date,103) tour_date,Worked_With_SF_Name, " +
            //                   " case worktype_name_b when '' then 'Weekly Off' else worktype_name_b end worktype_name_b,worktype_name_b1,worktype_name_b2," +
            //                   " case Tour_Schedule1 when convert(varchar(50),0) then '' else Territory_Code1 end Tour_Schedule1," +
            //                   " case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
            //                   " case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3," +
            //                   " Worked_With_SF_Name,objective" +
            //                   " from Trans_TP_one " +
            //                   " where sf_code = '" + sf_code + "' " +
            //                   " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + "  order by 1";
            strQry = "exec getNewTPPlan '" + sf_code + "'," + iMonth + "," + iYear + "";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Entry(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "(select tour_date, objective, Worked_With_SF_Code,'Approved' Change_Status from Trans_TP " +
                     " where sf_code = '" + sf_code + "') union all (select tour_date, objective, Worked_With_SF_Code, " +
                     " case when Change_Status = 0 then 'Saved. But not yet submitted'  " +
                     " case when Change_Status = 1 then 'Submitted. But not yet approved' else 'Approved' end Change_Status from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Status(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select distinct submission_date,Confirmed_Date," +
                     "case Confirmed " +
                     " when 1  then 'Approved'  " +
                     " when 1 & Change_Status then 'Saved. But not yet submitted'else 'Submitted. But not yet approved' " +
                     " end as Change_Status " +
                     " from Trans_TP_One where sf_code = '" + sf_code + "' " +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Entry(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "(select CAST(tour_date as Date) tour_date, objective,Worktype_Name_B, Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,";
            strQry += " Tour_Schedule3,'Approved' as Change_Status from Trans_TP ";
            strQry += " where sf_code = '" + sf_code + "'  ";
            strQry += " and month(tour_date) = CAST(" + iMonth + " AS VarChar) and year(tour_date) = CAST(" + iYear + " AS VarChar))  ";
            strQry += " union all (select CAST(tour_date as Date) tour_date, objective,Worktype_Name_B, Worked_With_SF_Code, ";
            strQry += " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3, ";
            strQry += " case Change_Status  ";
            strQry += " when 0 then 'Saved. But not yet submitted'  ";
            strQry += " else 'Submitted. But not yet approved' end as Change_Status from Trans_TP_One  ";
            strQry += " where sf_code = '" + sf_code + "'  ";
            strQry += " and month(tour_date) = CAST(" + iMonth + " AS VarChar) and year(tour_date) = CAST(" + iYear + " AS VarChar)) order by 1 ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Entry(string sf_code, int iMonth, int iYear, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "declare @Desingation_Code varchar(5) " +
                     "set @Desingation_Code=(select Designation_Code from Mas_Salesforce where Sf_Code='" + sf_code + "') " +
                     "select CONVERT(char(10),a.Tour_Date,103) Tour_Date,a.objective,a.Worked_With_SF_Name,a.Worktype_Name_B,a.Worktype_Name_B1,a.Worktype_Name_B2,a.Worked_With_SF_Code,Territory_Code1 Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                     "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                     "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                     "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                     "from Trans_TP_One a,Mas_Salesforce b " +
                     "where a.sf_code = '" + sf_code + "' and a.sf_code=b.sf_code " +
                     "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " and Confirmed=0 " +
                     "union all " +
                     "select CONVERT(char(10),a.Tour_Date,103) Tour_Date, a.objective,a.Worked_With_SF_Name,Worktype_Name_B,Worktype_Name_B1,Worktype_Name_B2,a.Worked_With_SF_Code,Territory_Code1 Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                    "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                    "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                     "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                     "from Trans_TP a,Mas_Salesforce b " +
                     "where a.sf_code = '" + sf_code + "' and a.sf_code=b.sf_code " +
                     "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " and Confirmed=1 ";


            // strQry = "declare @Desingation_Code varchar(5) " +
                    // "set @Desingation_Code=(select Designation_Code from Mas_Salesforce where Sf_Code='" + sf_code + "') " +
                    // "select vwt.StateName,vwt.Zone_name,vwt.Area_Name,vwt.SF_Name,vwt.Territory_name as HQ_Name, CONVERT(char(10),a.Tour_Date,103) Tour_Date,a.objective,a.Worked_With_SF_Name,sl.Territory,a.Worktype_Name_B,a.Worktype_Name_B1,a.Worktype_Name_B2,a.Worked_With_SF_Code,Territory_Code1 Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                    // "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                    // "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                    // "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                    // "from Trans_TP_One a,Mas_Salesforce b ,vw_TSR_Direct_Markecting vwt,Mas_Stockist sl " +
                    // "where a.sf_code=b.sf_code  and a.SF_Code = vwt.Sf_Code and sl.Stockist_Code = a.Worked_With_SF_Code and a.sf_code = '" + sf_code + "' " +
                    // "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " and Confirmed=0 " +
                    // "union all " +
                    // "select vwt.StateName,vwt.Zone_name,vwt.Area_Name,vwt.SF_Name, vwt.Territory_name as HQ_Name,CONVERT(char(10),a.Tour_Date,103) Tour_Date, a.objective,a.Worked_With_SF_Name,sl.Territory,Worktype_Name_B,Worktype_Name_B1,Worktype_Name_B2,a.Worked_With_SF_Code,Territory_Code1 Tour_Schedule1,a.Tour_Schedule2,a.Tour_Schedule3, " +
                    // "a.Submission_date,a.Confirmed_Date,case a.Change_Status when 0 then 'Saved. But not yet submitted' " +
                    // "when 1 & a.Confirmed then 'Approved' else 'Submitted. But not yet approved' end as Change_Status,b.Sf_HQ,b.sf_desgn, " +
                    // "(select Designation_Short_Name from Mas_SF_Designation where Designation_Code= @Desingation_Code) Designation_Short_Name " +
                    // "from Trans_TP a,Mas_Salesforce b ,vw_TSR_Direct_Markecting vwt,Mas_Stockist sl " +
                    // "where a.sf_code=b.sf_code  and a.SF_Code = vwt.Sf_Code and sl.Stockist_Code = a.Worked_With_SF_Code  And a.sf_code = '" + sf_code + "' " +
                    // "and month(a.tour_date) = " + iMonth + " and year(a.tour_date) = " + iYear + " and Confirmed=1 ";



            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Draft(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dt = Convert.ToDateTime(sCurrentDate);
            string strCurrentDate = dt.Month + "-" + dt.Day + "-" + dt.Year;
            DataSet dsTP = null;

            //strQry = "select objective,Worked_With_SF_Code,Tour_Schedule,SDP1,SDP2,WorkType from Trans_TP_One " +
            //         " where WorkType_Code=0 and Change_Status =0 and sf_code = '" + sf_code + "' " +
            //         " and Tour_Date = '" + strCurrentDate + "'  ";

            strQry = " select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0 " +
                     " union all " +
                     " select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        // Added by Sridevi - 20Nov
        public DataSet get_TP_Draft_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DateTime dt = Convert.ToDateTime(sCurrentDate);
            string strCurrentDate = dt.Month + "-" + dt.Day + "-" + dt.Year;
            DataSet dsTP = null;

            strQry = " select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,WorkType_Code_B,Rejection_Reason,Worktype_Name_B " +
                     " ,WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "'and Tour_Date = '" + strCurrentDate + "' and Change_Status =0";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_TP_Details(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP_One " +
                     " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' " +
                     " union all  " +
                     " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP " +
                     " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        // Added by Sridevi - 20 Nov
        public DataSet get_TP_Details_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Rejection_Reason,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP_One " +
                     " where (Change_Status =1 or Change_Status=2) and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        // Added by Sridevi - 20 Nov
        public DataSet get_TP_Details_Approve_New(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            DateTime dtsCurrentDate = Convert.ToDateTime(sCurrentDate);


            strQry = " Select objective,Worked_With_SF_Code,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Worktype_code_B,Rejection_Reason,Worktype_Name_B, " +
                     " WorkType_Code_B1,Worktype_Name_B1,WorkType_Code_B2,Worktype_Name_B2,Territory_Code1,Territory_Code2,Territory_Code3 from Trans_TP_One " +
                     " where Change_Status =1 and sf_code = '" + sf_code + "' and Confirmed=0 " +
                     " and Tour_Date = '" + dtsCurrentDate.Month + "-" + dtsCurrentDate.Day + "-" + dtsCurrentDate.Year + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public DataSet get_TourPlan_MGRDetails(string sf_code, string sCurrentDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select Tour_Schedule1,objective,Worked_With_SF_Code,WorkType_Code_B from Trans_TP " +
                     " where sf_code = '" + sf_code + "' " +
                     " and Tour_Date = '" + sCurrentDate + "' " +
                     " union all " +
                     " select Tour_Schedule1,objective,Worked_With_SF_Code,WorkType_Code_B from Trans_TP_One " +
                     " where sf_code = '" + sf_code + "' " +
                     " and Tour_Date = '" + sCurrentDate + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }



        public DataSet get_WeekOff()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%W%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_FieldWork(string strlike)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where Worktype_Name_B LIKE '" + strlike + "%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_Holiday()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%H%'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TP_Calander_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select distinct(b.sf_TP_Active_Dt)sf_TP_Active_Dt " +
                     " from Trans_TP_One a,Mas_Salesforce b where a.SF_Code = '" + sf_code + "' and a.SF_Code=b.Sf_Code " +
                     " and a.Change_Status=1 and a.confirmed=0";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }


        public DataSet get_TP_Active_Edit(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //int iCount = -1;
            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2)";
                   

            dsListedDR = db_ER.Exec_DataSet(strQry);
           
            return dsListedDR;
        }

        public DataSet get_TP_Active_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //int iCount = -1;

            //strQry = "select top 1'01-' + RTRIM(cast((Tour_Month + 1) as CHAR(2)))+'-' + cast(Tour_Year as CHAR(4))+' 00:00:00.000'  from Trans_TP_One" +
            //         " where SF_Code = '" + sf_code + "' and Change_Status=1 and confirmed=1 "+
            //         "order by Tour_Date desc";           


            strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
                     "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

            dsListedDR = db_ER.Exec_DataSet(strQry);
            if (dsListedDR.Tables[0].Rows.Count == 0)
            {
                strQry = "select top 1'01-' + substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                         "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000' " +
                         " from Trans_TP  where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2) and confirmed=1 " +
                         "order by Tour_Date desc";

                dsListedDR = db_ER.Exec_DataSet(strQry);
            }


            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }


        public DataSet get_TPCalender_Active_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            //int iCount = -1;

            //strQry = "select top 1'01-' + RTRIM(cast((Tour_Month + 1) as CHAR(2)))+'-' + cast(Tour_Year as CHAR(4))+' 00:00:00.000'  from Trans_TP_One" +
            //         " where SF_Code = '" + sf_code + "' and Change_Status=1 and confirmed=1 "+
            //         "order by Tour_Date desc";           

            //strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP_One where " +
            //         "SF_Code = '" + sf_code + "' and confirmed=0 " +
            //         " union all " +
            //         "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
            //         "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

            strQry = "select top 1 Tour_Date " +
                    " from Trans_TP_one  where SF_Code = '" + sf_code + "' and confirmed=0 and Change_Status=1 " +
                    "order by Tour_Date desc";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count == 0)
            {

                strQry = "select top(1)(Tour_Date) as Tour_Month from Trans_TP where " +
                         "SF_Code = '" + sf_code + "' and confirmed=0 order by Tour_Month asc ";

                dsListedDR = db_ER.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "select isnull(max('01-' +  substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                             "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000'),'') as Tour_Date " +
                             "from Trans_TP where SF_Code=SF_Code and SF_Code = '" + sf_code + "' " +
                             "and Change_Status=1 and confirmed=1 " +
                             "union all " +
                             "select isnull(max('01-' +  substring(convert(char(7),dateadd(mm,1,Tour_Date),120),6,2)+'-'+ " +
                             "substring(convert(char(7),dateadd(mm,1,Tour_Date),120),0,5)+' 00:00:00.000'),'') as Tour_Date " +
                             "from Trans_TP_One  where SF_Code=SF_Code and SF_Code = '" + sf_code + "' " +
                             "and Change_Status=1 and confirmed=1";

                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }



                //if (dsListedDR.Tables[0].Rows[0][0] != "")
                //{
                //    //Do Nothing
                //}

                else
                {
                    strQry = " SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

                    try
                    {
                        dsListedDR = db_ER.Exec_DataSet(strQry);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return dsListedDR;
        }
        public DataSet get_TP_Submission_Date(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            try
            {

                strQry = "Declare @SF_Code varchar(100)" +
                         "set @SF_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code='" + sf_code + "')" +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP  " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "union all  " +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP_One " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "select Sf_Name from Mas_Salesforce where Sf_Code=@SF_Code";

                dsListedDR = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsListedDR;
        }
        //Added by Sridevi 
        public DataSet get_TP_Submission_Date_New(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;
            try
            {

                strQry = "Declare @SF_Code varchar(100)" +
                         "set @SF_Code=(select TP_Reporting_SF from Mas_Salesforce where Sf_Code='" + sf_code + "')" +
                         "Select convert(char(3),Tour_Date,107)+' '+ convert(char(4),Tour_Date,111) Tour_Date from Trans_TP_One " +
                         "where SF_Code='" + sf_code + "'and isnull(Confirmed,'')='0'  and (Change_Status=1 or Change_Status=2) " +
                         "select Sf_Name from Mas_Salesforce where Sf_Code=@SF_Code";

                dsListedDR = db_ER.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsListedDR;
        }

        public DataSet Get_TP_ApprovalTitle(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "Select Sf_Name,convert(char(3),sf_TP_Active_Dt,107)+' '+ convert(char(4),sf_TP_Active_Dt,111) " +
                     " Sf_Joining_Date,Sf_HQ, convert(char(10),sf_TP_Active_Dt,103) Date from Mas_Salesforce where Sf_Code='" + sfcode + "' ";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            return dsListedDR;
        }

        public DataSet TPCalander_checkmonth(string sfcode, string tourmonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select top 1 Tour_Month  from Trans_TP" +
                     " where SF_Code = '" + sfcode + "' and tour_month='" + tourmonth + "' and  Change_Status=1 " +
                     " order by Tour_Date desc ";


            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            return dsListedDR;
        }

        public DataSet checkmonth(string sfcode, string tourmonth)
        {

            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select top 1 Tour_Month as Tour_Date from Trans_TP_One where SF_Code='" + sfcode + "' and tour_month='" + tourmonth + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) " +
                     "union all " +
                     "select top 1 Tour_Month as Tour_Date from Trans_TP where SF_Code='" + sfcode + "'  and tour_month='" + tourmonth + "' and Confirmed=0 and (Change_Status=1 or Change_Status=2) order by Tour_Date desc";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count > 0)
            {
                //Do Nothing
            }

            return dsListedDR;
        }
        public DataSet get_TP_Calendar(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;
            DateTime dtTP;
            int iMonth;
            int iYear;

            string sDate = string.Empty;
            //int iDays;
            //int iCount;

            strQry = "SELECT sf_TP_Active_Dt from mas_salesforce where sf_code = '" + sf_code + "' ";

            dsTP = db_ER.Exec_DataSet(strQry);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                dtTP = Convert.ToDateTime(dsTP.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                iMonth = dtTP.Month;
                iYear = dtTP.Year;
                sDate = getActualTPDate(iMonth, iYear);
                string[] TP_Month_Year;
                TP_Month_Year = sDate.Split('~');
                iMonth = Convert.ToInt32(TP_Month_Year[0]);
                iYear = Convert.ToInt32(TP_Month_Year[1]);
                string curDate = iMonth.ToString() + "-01-" + iYear.ToString();

                strQry = "select '" + curDate + "' ";

                dsTP = db_ER.Exec_DataSet(strQry);
            }

            return dsTP;
        }

        public DataSet get_TourPlan_Details(string sf_code, string sTP)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;
            DateTime dtTP = Convert.ToDateTime(sTP);
            string sReturn = string.Empty;

            strQry = " select Tour_Schedule1,Worked_With_SF_Code,objective,WorkType_Code_B from Trans_TP_One  " +
                     " where sf_code = '" + sf_code + "' and  Tour_Month =  " + dtTP.Month + " and Tour_Year = " + dtTP.Year + " and DAY(tour_date) = " + dtTP.Day + " " +
                     " union all " +
                     " select Tour_Schedule1,Worked_With_SF_Code,objective,WorkType_Code_B from Trans_TP " +
                     " where sf_code = '" + sf_code + "' and  Tour_Month = " + dtTP.Month + " and Tour_Year = " + dtTP.Year + " and DAY(tour_date) = " + dtTP.Day + " ";

            dsTP = db_ER.Exec_DataSet(strQry);

            return dsTP;
        }


        public string getActualTPDate(int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();
            int iDays;
            int iCount;
            string sReturn = string.Empty;

            iDays = getLastDay(iMonth, iYear);

            strQry = "select count(Tour_Month) from Trans_TP where Tour_Month = " + iMonth + " and Tour_Year = " + iYear;
            iCount = db_ER.Exec_Scalar(strQry);

            if (iDays == iCount)
            {
                //Next Month
                if (iMonth == 12)
                {
                    iMonth = 1;
                    iYear = iYear + 1;
                }
                else
                {
                    iMonth = iMonth + 1;
                }
                sReturn = getActualTPDate(iMonth, iYear);
            }
            else
            {
                sReturn = iMonth.ToString() + "~" + iYear.ToString();
            }
            return sReturn;
        }

        public int getLastDay(int iMonth, int iYear)
        {
            int iDay = -1;

            if (iMonth == 1)
            {
                iDay = 31;
            }
            else if (iMonth == 2)
            {
                iDay = 28;
                //if ((iYear % 4)==0)
                //{
                //    iDay = 29;
                //}
                //else
                //{
                //    iDay = 28;
                //}
            }
            else if (iMonth == 3)
            {
                iDay = 31;
            }
            else if (iMonth == 4)
            {
                iDay = 30;
            }
            else if (iMonth == 5)
            {
                iDay = 31;
            }
            else if (iMonth == 6)
            {
                iDay = 30;
            }
            else if (iMonth == 7)
            {
                iDay = 31;
            }
            else if (iMonth == 8)
            {
                iDay = 31;
            }
            else if (iMonth == 9)
            {
                iDay = 30;
            }
            else if (iMonth == 10)
            {
                iDay = 31;
            }
            else if (iMonth == 11)
            {
                iDay = 30;
            }
            else if (iMonth == 12)
            {
                iDay = 31;
            }
            return iDay;
        }

        public DataSet get_TP_Active_Date(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2)" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     " union all " +
                     " select tour_date as tour_date from Trans_TP" +
                     " where SF_Code = '" + sf_code + "' and (Change_Status=1 or Change_Status=2)" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     "  order by tour_date";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            if (dsListedDR.Tables[0].Rows.Count != 0)
            {
                //Do Nothing
            }

            else
            {
                strQry = "SELECT sf_TP_Active_Dt as tour_date from mas_salesforce where sf_code = '" + sf_code + "' ";

                try
                {
                    dsListedDR = db_ER.Exec_DataSet(strQry);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return dsListedDR;
        }

        // Added by Sridevi
        public DataSet get_TP_Approval(string sf_code, int iMonth, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = " select tour_date as tour_date from Trans_TP_One" +
                     " where SF_Code = '" + sf_code + "' and  Change_Status=1" +
                     " and month(tour_date) = " + iMonth + " and year(tour_date) = " + iYear + " " +
                     " order by tour_date";

            dsListedDR = db_ER.Exec_DataSet(strQry);

            return dsListedDR;
        }

        public DataSet get_TP_Pending_Approval(string sf_code, string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            if (Div_Code.Contains(","))
            {
                Div_Code = Div_Code.Remove(Div_Code.Length - 1);
            }

            if (sf_code != "admin")
            {

                strQry = "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                         " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                         "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
                         " from Mas_Salesforce a, Trans_TP b " +
                         " where a.sf_code = b.sf_code and a.Reporting_To_SF  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
                         " and b.Change_Status=1 and b.confirmed=0" +
                         " union all " +
                         "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                         " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                         "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
                         " from Mas_Salesforce a, Trans_TP_One b " +
                         " where a.sf_code = b.sf_code and a.Reporting_To_SF  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
                         " and b.Change_Status=1 and b.confirmed=1";

            }
            else
            {
                strQry = "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                        " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                        "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
                        " from Mas_Salesforce a, Trans_TP b " +
                        " where a.sf_code = b.sf_code and a.Reporting_To_SF  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
                        " and b.Change_Status=1 and b.confirmed=0" +
                        " union all " +
                        "select distinct a.sf_code, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                        " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                        "'Click here to Approve '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month]" +
                        " from Mas_Salesforce a, Trans_TP_One b " +
                        " where a.sf_code = b.sf_code and a.Reporting_To_SF  = '" + sf_code + "' and b.Division_Code='" + Div_Code + "'" +
                        " and b.Change_Status=1 and b.confirmed=0";
            }

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet get_WeekOff(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            //strQry = " select WeekOff from Mas_Division a, Mas_Salesforce_AM b Where a.Division_Code = b.Division_Code and b.Sf_Code = '" + sf_code + "' ";
            strQry = " select WeekOff from mas_state a, Mas_Salesforce b Where a.state_code = b.state_code and b.Sf_Code = '" + sf_code + "' ";

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


        public DataSet FetchTerritory(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //int iCount;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                    " UNION " +
                    " SELECT Territory_Code,Territory_Name FROM  Mas_Territory_Creation " +
                    " where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
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

        public DataSet GetTPOptionDate(string sf_code, string ddlMonth, string ddlYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select Selected_Date from Options_TP_Edit where SF_Code='" + sf_code + "' " +
                     " and Tour_Month='" + ddlMonth + "' and Tour_Year='" + ddlYear + "' ";

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

        public DataSet GetTPEdit(string sf_code, string ddlMonth, string ddlYear, string lblDate)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            string strDate = lblDate.Substring(3, 2) + "-" + lblDate.Substring(0, 2) + "-" + lblDate.Substring(6, 4);

            strQry = " select Selected_Date from Options_TP_Edit where SF_Code='" + sf_code + "' " +
                     " and Tour_Month='" + ddlMonth + "' and Tour_Year='" + ddlYear + "' and (Selected_Date like '%" + strDate + "%' or Selected_Date like '%ALL%') order by created_dtm desc ";

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


        public DataSet FetchWorkType()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as WorkType_Code_B,'---Select---' as WorkType_Name_B " +
                         " UNION" +
                     " SELECT WorkType_Code_B,WorkType_Name_B FROM Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%T%'";
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


        public DataSet FetchTerritory(string sf_code, string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
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

        public DataSet FetchTerritory_MGR(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code in (" + terr_code + ")";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
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

        public string FetchTerritoryCode(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT Territory_Code " +
                     " FROM  Mas_Territory_Creation where Territory_Name='" + terr_code + "' " +
                     " AND territory_active_flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }

        public string FetchTerritoryName(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Name='" + terr_code + "' " +
                     " AND territory_active_flag=0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }

        public string FetchSFCode(string SF_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;
            string sReturn = string.Empty;

            strQry = " SELECT SF_Code FROM  Mas_SalesForce where SF_Name='" + SF_Name + "' AND sf_TP_Active_Flag =0 ";

            try
            {
                dsTerr = db_ER.Exec_DataSet(strQry);
                if (dsTerr.Tables[0].Rows.Count > 0)
                {
                    sReturn = dsTerr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return sReturn;
        }



        public DataSet FetchSFName(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT SF_Name " +
                     " FROM  Mas_SalesForce where Sf_Code='" + terr_code + "' " +
                     " AND sf_TP_Active_Flag =0 ";

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

        public DataSet getTerritoryName(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;


            strQry = " SELECT Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Code='" + terr_code + "' " +
                     " AND territory_active_flag=0 ";

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

        public DataSet getHolidays_TP(string sf_code, int iMonth, int iDay, int iYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsHoliday = null;
            //convert(varchar,Sf_Joining_Date,103) Sf_Joining_Date
            //strQry = "SELECT a.sl_no,a.Academic_Year, a.state_code, convert(varchar,a.Holiday_Date,101) Holiday_Date, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
            //          " FROM Mas_Statewise_Holiday_Fixation a, mas_state b " +
            //          " WHERE   a.Division_Code = '" + divcode + "' AND a.state_code=b.state_code " +
            //          " ORDER BY 1";

            strQry = "select Holiday_Name from Mas_Statewise_Holiday_Fixation a, mas_state b, Mas_Salesforce c " +
                      " where DAY(Holiday_Date)=" + iDay + " and MONTH(Holiday_Date)=" + iMonth + " and YEAR(Holiday_Date)=" + iYear + " " +
                //" AND a.state_code=b.state_code " +
                      " and b.State_Code = c.State_Code and c.Sf_Code = '" + sf_code + "' ";

            try
            {
                dsHoliday = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsHoliday;
        }

        public DataSet TPFetchWorktype(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " select Tour_Schedule1,Tour_Schedule2,Tour_Schedule3 from Trans_TP_One where Tour_Schedule1='" + terr_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
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

        public DataSet FetchWorkType(string terr_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Worktype_Name_B " +
                     " FROM  Mas_WorkType_BaseLevel where WorkType_Code_B='" + terr_code + "' ";

            /* " UNION " +
             " SELECT WorkType_Code_B as Territory_Code,Worktype_Name_B as Territory_Name " +
             " FROM Mas_WorkType_BaseLevel where active_flag=0 ";*/
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
        //16Jul - Sridevi - New function for Calender - TP
        public DataSet FetchTerritory_Chkbox(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Sf_Code = '" + sf_code + "' AND territory_active_flag=0 ";
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

        public DataSet FetchTerritory_TourSchedule(string Territory_Name)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT Territory_Code,Territory_Name " +
                     " FROM  Mas_Territory_Creation where Territory_Name = '" + Territory_Name + "' AND territory_active_flag=0 ";
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

        //16Jul - ClearTPCalender
        public int ClearTPCalender(string MR_Code, string MR_Month, string MR_Year)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "DELETE FROM Trans_TP_One" +
                            " where SF_Code='" + MR_Code + "' and Tour_Month=" + Convert.ToInt32(MR_Month) + " and " +
                            " Tour_Year=" + Convert.ToInt32(MR_Year) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //16 July
        public DataSet get_TourPlan_MGRWorkType(string WT_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select Worktype_Name_M from Mas_WorkType_Mgr Where WorkType_Code_M = '" + WT_Code + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_TourPlan_MGRSF_Name(string WT_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select Sf_Name from  Mas_Salesforce where Sf_Code = '" + WT_Code + "' ";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_UserList_TP_Report(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_UserList_TP_Report '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_UserList_TP_Report_Level(string div_code, string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_UserList_TP_Report_Level '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_TP_Detail(string sf_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "EXEC sp_get_tp_detail '" + sf_code + "', " + cmonth + ", " + cyear + " ";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }

        public DataSet get_TP_Entry_Confirm(string sf_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsRate = null;
            strQry = "select isnull(convert(char(10),submission_date,103),'0') submission_date,isnull(CONVERT(char(10),Confirmed_Date,103),'0') Confirmed_Date from Trans_TP " +
                      " where tour_month=" + cmonth + " and tour_year = " + cyear +
                      " and Change_Status=1 and Confirmed=1 and SF_Code='" + sf_code + "'" +
                      " union all " +
                      "select isnull(convert(char(10),submission_date,103),'0') submission_date,isnull(CONVERT(char(10),Confirmed_Date,103),'0') Confirmed_Date from Trans_TP_One  " +
                      "where tour_month=" + cmonth + " and tour_year =" + cyear +
                      "and Change_Status=1 and Confirmed=0 and SF_Code='" + sf_code + "'";

            try
            {
                dsRate = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRate;
        }


        public DataSet FillTourPlan(string sf_code, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_tp  '" + div_code + "', '" + sf_code + "' ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        //public int get_TP_Count_FieldForce(string sf_code, string sMonth, string sYear, string ddlDay)
        //{
        //    DB_EReporting db_ER = new DB_EReporting();

        //    int iReturn = -1;
        //    strQry = "select COUNT(tour_month) from Trans_TP " +
        //             " WHERE sf_code = '" + sf_code + "' " +
        //             " and confirmed = 1 " +
        //             " and tour_month = '" + sMonth + "'  " +
        //             " and tour_year = '" + sYear + "' and Tour_Date='" + ddlDay + "' ";

        //    try
        //    {
        //        iReturn = db_ER.Exec_Scalar(strQry);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iReturn;
        //}

        public DataSet FillFutureTourPlan(string sf_code, string sMonth, string sYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            DateTime dtTP;
            string sTP = sMonth + "-01-" + sYear;
            dtTP = Convert.ToDateTime(sTP);

            strQry = "select distinct Tour_Month, Tour_Year from Trans_TP_One " +
                     " WHERE sf_code = '" + sf_code + "' " +
                     " and confirmed = 1 " +
                     " and tour_date >= '" + dtTP + "'  ";

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int DeleteTP(string SF_Code, string sMonth, string sYear)
        {
            int iReturn = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "delete from Trans_TP_One " +
                            " where SF_Code='" + SF_Code + "' and Tour_Month=" + Convert.ToInt32(sMonth) + " and " +
                            " Tour_Year=" + Convert.ToInt32(sYear) + " ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        //Change done by saravanan 23/10/14
        public DataSet Get_TP_Edit_Year(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        //Change done by saravanan 23/10/14
        public DataSet Get_TP_Edit_Month(string div_code, string strMonth)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select distinct top(3)Tour_Month from Trans_TP WHERE sf_code = '" + strMonth + "' and confirmed = 1 order by Tour_Month desc";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Get_TP_Entry_Edit_Year(string Division_Code, string SF_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select distinct Tour_Month,Tour_Year from Trans_TP where isnull(Confirmed,'')=0 " +
                     "and Division_Code='" + Division_Code + "' and SF_Code='" + SF_Code + "' " +
                     "union all " +
                     "select distinct Tour_Month,Tour_Year from Trans_TP_One where isnull(Confirmed,'')=0 " +
                     "and Division_Code='" + Division_Code + "' and SF_Code='" + SF_Code + "' order by Tour_Month asc ";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public int TP_Edit_RecordAdd(string sf_code, string smonth, string syear, string sdate)
        {
            int iReturn = -1;
            int iCount = -1;

            try
            {
                DB_EReporting db = new DB_EReporting();

                strQry = "select count(*) from Options_TP_Edit where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "' ";

                iCount = db.Exec_Scalar(strQry);

                //strQry = " Insert into Trans_TP_One(SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name) " +
                //         " Select SF_Code,Tour_Month,Tour_Year,Submission_date,Tour_Date,WorkType_Code_B,Worktype_Name_B, " +
                //         " Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Objective,Worked_With_SF_Code,Division_Code,Confirmed, " +
                //         " Confirmed_Date,Rejection_Reason,Change_Status,Territory_Code1,Territory_Code2,Territory_Code3,Worked_With_SF_Name from trans_tp " +
                //         " where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "'";

                //iReturn = db.ExecQry(strQry);

                if (iCount <= 0)
                {
                    strQry = "insert into Options_TP_Edit " +
                          " VALUES('" + sf_code + "', '" + smonth + "', '" + syear + "', '" + sdate + "',getdate()) ";
                }
                else
                {
                    strQry = " update Options_TP_Edit set Selected_Date='" + sdate + "',created_dtm=getdate() " +
                             " where SF_Code='" + sf_code + "' and Tour_Month ='" + smonth + "'  " +
                             " and Tour_Year='" + syear + "' ";
                }

                iReturn = db.ExecQry(strQry);

                strQry = "update Trans_TP set Confirmed='0',Change_Status=1 where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "'";

                iReturn = db.ExecQry(strQry);


                strQry = " Insert into Trans_TP_one " +
                            " Select * from Trans_TP where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "'";

                iReturn = db.ExecQry(strQry);

                strQry = "Delete from Trans_TP where SF_Code='" + sf_code + "' and Tour_Month='" + smonth + "' and Tour_Year='" + syear + "' ";

                iReturn = db.ExecQry(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }

        public DataSet FillTourPlan_Delete(string sf_code, string div_code, int cmonth, int cyear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "EXEC sp_tp_delete  '" + div_code + "', '" + sf_code + "', " + cmonth + ", " + cyear;

            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }

        public DataSet get_TP_Count_FieldForce(string sf_code, string sMonth, string sYear, string Day)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;
            //int iReturn = -1;

            if (Day != "All")
            {

                strQry = "select COUNT(tour_month) from Trans_TP " +
                         " WHERE sf_code = '" + sf_code + "' " +
                         " and confirmed = 1 " +
                         " and tour_month = '" + sMonth + "'  " +
                         " and tour_year = '" + sYear + "' and DAY(Tour_Date)='" + Day + "' ";
                
            }
            else
            {
                strQry = "select COUNT(tour_month) from Trans_TP " +
                        " WHERE sf_code = '" + sf_code + "' " +
                        " and confirmed = 1 " +
                        " and tour_month = '" + sMonth + "'  " +
                        " and tour_year = '" + sYear + "' ";               
            }

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Saravanan Changes
        public DataSet GetTPEditConfirmed_Date(string Tour_Month, string Tour_Year, string Sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;
            //if (!RecordExist(div_sname))
            //{
            try
            {
                strQry = "select count(Confirmed_Date) Confirmed_Date from Trans_TP where Tour_Month='" + Tour_Month + "' and Tour_Year='" + Tour_Year + "' and sf_code='" + Sf_code + "'";

                dsTP = db.Exec_DataSet(strQry);

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
            return dsTP;
        }

        public DataSet GetTPWorkTypePlaceInvolved(string WorkType, string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Place_Involved from Mas_WorkType_BaseLevel where Place_Involved='N' and Worktype_Name_B='" + WorkType + "' and Division_Code='" + Div_Code + "' ";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet GetTPWorkTypeFieldWork(string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                         " select distinct a.Territory_Code,a.Territory_Name from Mas_Territory_Creation a,Mas_ListedDr b where " +
                         " cast(a.Territory_Code as varchar)=b.Territory_Code and a.SF_Code='" + sf_code + "' and a.territory_active_flag=0 ";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        //Changes done by Saravana
        public DataSet GetExpenseMGRWorkType(string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Worktype_Name_M Worktype_Name_B,Expense_Type from Mas_WorkType_Mgr where Division_Code='" + Div_Code + "'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet GetExpenseBaseWorkType(string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Worktype_Name_B,Expense_Type from Mas_WorkType_BaseLevel where ExpNeed=1 and division_Code='" + Div_Code + "'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public DataSet GetExp_MGR(string Div_Code, string sf_code,string created_By)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Parameter_Name,case cal_type when '1' then '(-) ' else '' end + Amount as Amount,Cal_Type from Trans_Additional_Exp where Division_code='" + Div_Code + "'and Created_By='" + sf_code + "'or Created_By='"+created_By+"'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        //Giri 08.08.2016
        public DataSet GetExp(string Div_Code,string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "select Parameter_Name,case cal_type when '1' then '(-) ' else '' end + Amount as Amount,Cal_Type,Created_By from Trans_Additional_Exp where Division_code='" + Div_Code + "'and Sf_Code='" + sf_code + "'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public int Expense_Update(string Parameter_Name, string Amount, string div_code,string sf_code,string Cal_Type,string sf_Name,string month,string Year,string con)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select Parameter_Name from Trans_Additional_Exp where Parameter_Name='" + Parameter_Name + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_Additional_Exp set Amount='" + Amount + "',cal_Type='" + Cal_Type + "',Sf_Code='" + sf_code + "',Created_By='" + sf_Name + "',Month='" + month + "',Year='" + Year + "',LastUpdt_Date=getdate(),Confirmed='"+con+"'where Parameter_Name='" + Parameter_Name + "' and division_Code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = "Insert into Trans_Additional_Exp(Parameter_Name,Amount,Division_code,Cal_Type,Sf_Code,Created_By,Month,Year,Created_Date,LastUpdt_Date,Confirmed)values('" + Parameter_Name + "','" + Amount + "','" + div_code + "','" + Cal_Type + "','" + sf_code + "','" + sf_Name + "','" + month + "','" + Year + "',getdate(),getdate(),'"+con+"')";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }
        public int Expense_App_Admin(string sfCode, string Month, string Year, string flag, string div_code, string sf_name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select Sf_Code from Trans_FM_Expense_Head where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_FM_Expense_Head set Month='" + Month + "',Year='" + Year + "',sndhqfl='" + flag + "',Admin_Appr=getdate() where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    //strQry = "Insert into Trans_FM_Expense_Head(Sf_Code,Month,Year,sndhqfl,Division_code,snd_dt,SF_Name)values('" + sfCode + "','" + Month + "','" + Year + "','" + flag + "','" + div_code + "',getdate(),'" + sf_name + "')";

                    //iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }
        //Giri 11.08.2016
        public int Expense_App_MGR(string sfCode, string Month, string Year, string flag, string div_code, string sf_name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select Sf_Code from Trans_FM_Expense_Head where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_FM_Expense_Head set Month='" + Month + "',Year='" + Year + "',sndhqfl='" + flag + "',MGR_Appr=getdate() where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    //strQry = "Insert into Trans_FM_Expense_Head(Sf_Code,Month,Year,sndhqfl,Division_code,snd_dt,SF_Name)values('" + sfCode + "','" + Month + "','" + Year + "','" + flag + "','" + div_code + "',getdate(),'" + sf_name + "')";

                    //iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }
        public int Expense_App(string sfCode, string Month, string Year, string flag, string div_code, string sf_name)
        {
            int iReturn = -1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                DataSet dsData = new DataSet();
                int Date;
                strQry = "select Sf_Code from Trans_FM_Expense_Head where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                dsData = db.Exec_DataSet(strQry);
                Date = dsData.Tables[0].Rows.Count;


                if (Date > 0)
                {
                    strQry = "Update Trans_FM_Expense_Head set Month='" + Month + "',Year='" + Year + "',sndhqfl='" + flag + "',snd_dt=getdate() where Sf_Code='" + sfCode + "' and Division_code='" + div_code + "'";

                    iReturn = db.ExecQry(strQry);

                }
                else
                {
                    strQry = "Insert into Trans_FM_Expense_Head(Sf_Code,Month,Year,sndhqfl,Division_code,snd_dt,SF_Name)values('" + sfCode + "','" + Month + "','" + Year + "','" + flag + "','" + div_code + "',getdate(),'" + sf_name + "')";

                    iReturn = db.ExecQry(strQry);
                }



            }
            catch (Exception)
            {

            }
            return iReturn;

        }

        //Giri 04.08.16 
        public DataSet GetBaseWorkType(string SFCode,string Div_Code,string Month,string Year)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;
          
            try
            {
                strQry = " select distinct convert(varchar(10),p.Activity_Date,105)as Date, " +
                         " p.WorkType_Name,p.Plan_Name,p.Sf_Name,isnull(Expense_Allowance,0) Expense_Allowance,isnull(Expense_Distance,0) Expense_Distance, " +
                         " isnull(Expense_Fare,0)Expense_Fare,isnull(Expense_Total,0) Expense_Total from vwActivity_Report p left outer join Trans_FM_Expense_Detail r " +
                         " on P.SF_Code=r.SF_Code and convert(varchar(10),p.Activity_Date,105)=r.DCR_Date where r.SF_Code='" + SFCode + "' and p.Division_code= '" + Div_Code + "'and MONTH(p.Activity_Date)='" + Month + "' and YEAR(p.Activity_Date)='" + Year + "' ";

                dsTP = db.Exec_DataSet(strQry);
               

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public DataSet GetRouType(string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "  SELECT 0 as Plan_No,'---Select---' as Plan_Name "+
                         "  UNION "+ 
                         "  SELECT Plan_No,Plan_Name FROM vwActivity_Report "+
                         "  where division_Code = '"+Div_Code+"' AND FWFlg='F'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public DataSet Getsf(string Div_Code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = "  SELECT '0' as Sf_Code,'---Select---' as Sf_Name " +
                         "  UNION " +
                         "  SELECT Sf_Code,Sf_Name FROM vwActivity_Report " +
                         "  where division_Code = '" + Div_Code + "' AND FWFlg='F'";

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        //Changes done by Saravanan
        public DataSet getWorkType_Select(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' WorkType_Code_M,'---Select---' Worktype_Name_M,'' WType_SName " +
                     " union " +
                     " select WorkType_Code_M,Worktype_Name_M,WType_SName from Mas_WorkType_Mgr where Division_Code='" + Div_Code + "' " +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Giri 10.08.2016
        public DataSet Get_FFN(string sf_code,string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;
            strQry = " select '' Sf_Code,'---Select---' Sf_Name " +
                     " union " +
                     " select Sf_Code,Sf_Name from Mas_Salesforce where Reporting_To_SF='"+sf_code+"'" +
                     " ORDER BY 2";
            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        public int RecordAddMGR_TPWeekOff(string TP_Date, string WorkType_Name, string TP_Terr, string TP_WW, string worktype, string TP_Objective, bool TP_Submit, string sf_code, string TP_Terr_Code, string TP_WWName)
        {
            int iReturn = -1;

            try
            {
                DateTime dt_TourPlan = Convert.ToDateTime(TP_Date);
                DB_EReporting db = new DB_EReporting();
                DataSet dsListedDR = new DataSet();

                int Division_Code = -1;
                //int iCount = -1;

                strQry = "select division_code from Mas_Salesforce_AM where Sf_Code = '" + sf_code + "' ";
                Division_Code = db.Exec_Scalar(strQry);

                strQry = "select * from Trans_TP " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' " +
                            " union all " +
                            " select * from Trans_TP_One " +
                            " where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " + " " +
                            " Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                dsListedDR = db.Exec_DataSet(strQry);

                if (dsListedDR.Tables[0].Rows.Count == 0)
                {
                    strQry = "insert into Trans_TP_One (SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                            " Worked_With_SF_Code,Objective,Tour_Schedule1,WorkType_Code_B,Worktype_Name_B,Division_Code,Change_Status,submission_date,Territory_Code1,Worked_With_SF_Name) " +
                            " VALUES('" + sf_code + "', " + dt_TourPlan.Month + ", " + dt_TourPlan.Year + ", '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "', " +
                            "'" + TP_Terr + "','" + TP_Objective + "','" + WorkType_Name + "','" + TP_WW + "',  '" + worktype + "', '" + Division_Code + "', 0,getdate(),'" + TP_Terr_Code + "','" + TP_WWName + "') ";

                    iReturn = db.ExecQry(strQry);
                }
                else
                {
                    //strQry = "Insert into tp_change_Date(SF_Code,Tour_Month,Tour_Year,Tour_Date, " +
                    //         "Tour_Schedule1,Tour_Schedule2,Tour_Schedule3,Division_Code,change_dt,Mode) " +
                    //         "Select SF_Code,Tour_Month,Tour_Year,Tour_Date,Tour_Schedule1,Tour_Schedule2,Tour_Schedule3," +
                    //         "Division_Code,GETDATE(),'C'Mode from Trans_TP_One where SF_Code='" + sf_code + "' and Tour_Date='" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' ";

                    //iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP_One set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);

                    strQry = "update Trans_TP set Objective='" + TP_Objective + "', Worked_With_SF_Code='" + TP_Terr + "',Worktype_Name_B='" + worktype + "'," +
                             "WorkType_Code_B = '" + TP_WW + "', Tour_Schedule1='" + WorkType_Name + "',Change_Status=0,submission_date=getdate(),Territory_Code1='" + TP_Terr_Code + "',Worked_With_SF_Name='" + TP_WWName + "' " +
                             "where SF_Code='" + sf_code + "' and Tour_Month=" + dt_TourPlan.Month + " and " +
                             "Tour_Date =  '" + dt_TourPlan.Month + "-" + dt_TourPlan.Day + "-" + dt_TourPlan.Year + "' and Tour_Year=" + dt_TourPlan.Year + " and Division_Code= '" + Division_Code + "' ";

                    iReturn = db.ExecQry(strQry);
                }



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

        //Changes done by Saravanan
        public DataSet get_TP_ApprovalStatus(string sf_code, string Month, string Year)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsTP = null;

            //strQry = " select distinct TP.sf_code,case TP.Change_Status "+ 
            //         " when '1' then 'Prepared & Not Completed' "+
            //         " else 'Not Prepared'  end as [Status]  from Trans_TP_One TP,Mas_Salesforce S "+
            //         " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and TP.Tour_Month=month(" + Month + ") and TP.Tour_Year=year('" + Year + "') and s.sf_TP_Active_Flag=0 " +
            //         " union all "+
            //         " select distinct TP.sf_code,case TP.Change_Status "+
            //         " when '1' then 'Prepared & Not Completed' "+
            //         " else 'Not Prepared' end as [Status]  from Trans_TP TP,Mas_Salesforce S " +
            //         " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and TP.Tour_Month=month(" + Month + ") and TP.Tour_Year=year('" + Year + "') and s.sf_TP_Active_Flag=0 ";


            strQry = " select distinct TP.sf_code,case TP.Change_Status " +
                     " when '1' then 'Prepared & Not Completed' " +
                     " when '2' then 'Completed'" +
                     "  end as [Status]  from Trans_TP_One TP,Mas_Salesforce S " +
                     " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0  and Tour_Month='" + Month + "' " +
                     " union all " +
                     " select distinct TP.sf_code,case TP.Change_Status " +
                     " when '1' then 'Prepared & Not Completed' " +
                     " when '2' then 'Completed'" +
                     "  end as [Status]  from Trans_TP TP,Mas_Salesforce S " +
                     " where TP.SF_Code='" + sf_code + "' AND TP.SF_Code=S.Sf_Code and s.sf_TP_Active_Flag=0 and Tour_Month='" + Month + "' ";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        //Changes done by Saravanan
        public DataSet Get_TP_Start_Title(string sfcode)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsListedDR = null;

            strQry = "select SF_Code from Trans_TP where SF_Code='" + sfcode + "' ";

            dsListedDR = db_ER.Exec_DataSet(strQry);



            return dsListedDR;
        }

        //Changes done by Saravanan
        public DataSet get_TP_MR(string sf_code, string Worktype, string Date)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select " +
                     " case Tour_Schedule1 when convert(varchar(50),0) then '' else Tour_Schedule1 end Tour_Schedule1," +
                     " case Tour_Schedule2 when convert(varchar(50),0)then '' else Tour_Schedule2 end Tour_Schedule2," +
                     " case Tour_Schedule3 when convert(varchar(50),0) then '' else Tour_Schedule3 end Tour_Schedule3" +
                     " from Trans_TP where Worktype_Name_B='" + Worktype + "' and SF_Code='" + sf_code + "' and convert(char(10),Tour_Date,103)='" + Date + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }
        //Changes done by Saravanan

        public DataSet get_TP_Rejected_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            //strQry = "select distinct a.sf_code,REPLACE(b.Rejection_Reason,'asdf','''') as Rejection_Reason, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
            //           " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //           "'Send to Manager Approval '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month],b.TP_Approval_MGR" +
            //           " from Mas_Salesforce a, Trans_TP b " +
            //           " where a.sf_code = b.sf_code and a.Sf_Code  = '" + sf_code + "' and b.Change_Status=0 and b.confirmed=0 and isnull(b.Rejection_Reason,'')!=''" +
            //           " union all " +
            //           "select distinct a.sf_code,b.Rejection_Reason, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
            //           " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
            //           "'Send to Manager Approval '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month],b.TP_Approval_MGR" +
            //           " from Mas_Salesforce a, Trans_TP_One b " +
            //           " where a.sf_code = b.sf_code and a.Sf_Code  = '" + sf_code + "' and b.Change_Status=0 and b.confirmed=0 and isnull(b.Rejection_Reason,'')!=''";

            strQry = "select distinct a.sf_code,REPLACE(b.Rejection_Reason,'asdf','''') as Rejection_Reason, a.sf_name,a.Sf_HQ, b.Tour_Month , b.Tour_Year, a.sf_code,a.sf_Designation_Short_Name ,  " +
                       " a.sf_code + '-' + cast(b.Tour_Month as varchar)+ '-' + cast(b.Tour_Year as varchar) as key_field, " +
                       "'Click Here to Re enter the TP '+convert(char(3),b.Tour_Date,107)+' '+convert(char(4),b.Tour_Date,111) [Month],b.TP_Approval_MGR" +
                       ",(select sf_Designation_Short_Name+' - '+Sf_HQ from mas_salesforce where sf_code=a.Reporting_To_SF) as Reporting_To_SF " +
                       " from Mas_Salesforce a, Trans_TP_One b " +
                       " where a.sf_code = b.sf_code and a.Sf_Code  = '" + sf_code + "' and b.Change_Status=2 and isnull(b.Rejection_Reason,'')!=''";





            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }
        public DataSet get_TP_Edit_MonthDDL(string sf_code, string strYear)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = " select Tour_Month from Trans_TP_One where SF_Code='" + sf_code + "' and Tour_Year='" + strYear + "' " +
                     " union " +
                     " select Tour_Month from Trans_TP where SF_Code='" + sf_code + "' and Tour_Year='" + strYear + "'";


            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        //Changes done by Saravanan
        public DataSet Get_HOID_TP_Edit_Year(string sf_Type, string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;


            if (sf_Type == "3" && div_code.Contains(","))
            {
                div_code = div_code.Remove(div_code.Length - 1);
                strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code in(" + div_code + ")";
            }
            else
            {
                strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
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
        public DataSet FetchWorkType_New(string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = " SELECT 0 as WorkType_Code_B,'---Select---' as WorkType_Name_B " +
                         " UNION" +
                     " SELECT WorkType_Code_B,WorkType_Name_B FROM Mas_WorkType_BaseLevel where active_flag=0 and TP_DCR like '%T%' and Division_Code='" + Division_Code + "'";
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

        public DataSet FetchWorkCode_New(string WorkType_Code, string Division_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            strQry = "select Place_Involved from Mas_WorkType_Mgr where WorkType_Code_M='" + WorkType_Code + "' and Division_Code='" + Division_Code + "'";
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
        
        public DataSet get_Holiday_DivCode(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%H%' and Division_Code='" + Div_Code + "' ";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet get_WeekOff_Divcode(string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTP = null;

            strQry = "select WorkType_Code_B from Mas_WorkType_BaseLevel where TP_Flag LIKE '%W%' and Division_Code='" + Div_Code + "'";

            try
            {
                dsTP = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsTP;
        }

        public DataSet GetTPWorkTypeFieldWork_Approval(string sf_code)
        {

            DB_EReporting db = new DB_EReporting();
            DataSet dsTP = null;

            try
            {
                strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                         " UNION " +
                         " select distinct a.Territory_Code,a.Territory_Name from Mas_Territory_Creation a,Mas_ListedDr b where " +
                         " cast(a.Territory_Code as varchar)=b.Territory_Code and a.SF_Code='" + sf_code + "' and Territory_Active_Flag=0 " +
                         " UNION "+
                         " select distinct a.Territory_Code,a.Territory_Name from Mas_Territory_Creation a where " +
                         " a.SF_Code='" + sf_code + "' and a.Territory_Active_Flag=1 ";
                         

                dsTP = db.Exec_DataSet(strQry);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsTP;
        }

        public DataSet FetchTerritory_Approval(string sf_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //int iCount;

            strQry = " SELECT 0 as Territory_Code,'---Select---' as Territory_Name " +
                    " UNION " +
                    " SELECT Territory_Code,Territory_Name FROM  Mas_Territory_Creation " +
                    " where Sf_Code = '" + sf_code + "'  ";
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

        public DataSet FetchTerritory_Active_Flag(string sf_code,string Territory_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //int iCount;

            strQry = " select Territory_Active_Flag from Mas_Territory_Creation where SF_Code='" + sf_code + "' "+
                     " and Territory_Code='" + Territory_Code + "' ";
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

        public DataSet FetchTerritory_Rejected_Status(string sf_code, string Tour_Month,string Div_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsTerr = null;

            //int iCount;

            strQry = " select SF_Code from Trans_TP_one where Division_Code='"+ Div_Code +"' and "+
                     " Tour_Month='"+ Tour_Month +"' and SF_Code='"+ sf_code +"' and  Confirmed='0' and Change_Status='0'";
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
        public DataSet Get_TP_Edit_Year_New(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;
            if (div_code.Contains(","))
            {
                div_code = div_code.Remove(div_code.Length - 1);
            }
            strQry = "select max([Year]-1) as Year from Mas_Division where Division_Code='" + div_code + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet Get_TP_Edit_Year_div()
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select max([Year]-1) as Year from Mas_Division ";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet GetFlag(string sf_code,string div_code,string mon, string year)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsAdmin = null;

            strQry = "select sndhqfl from Trans_FM_Expense_Head where SF_Code='" + sf_code + "' and Division_Code='" + div_code + "' and Month='" + mon + "' and Year='" + year + "'";
            try
            {
                dsAdmin = db_ER.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsAdmin;
        }
        public DataSet get_TPPlanApprove(string sf_Code, string Divcode, string Mn, string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "exec getApproveTPPlan '" + sf_Code + "'," + Divcode + "," + Mn + "," + Yr + "";
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
        public DataSet get_TPPlanDist(string sf_Code, string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "select Stockist_Code,Stockist_Name from Mas_Stockist where Division_Code=" + Divcode + " and Stockist_Active_Flag=0 and CHARINDEX(','+'" + sf_Code + "'+',',','+Field_Code+',')>0";
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
        public DataSet get_TPPlanRoute(string sf_Code, string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "select Territory_Code,Territory_Name from Mas_Territory_Creation where Division_Code=" + Divcode + " and Territory_Active_Flag=0 and CHARINDEX(','+'" + sf_Code + "'+',',','+SF_Code+',')>0 order by 2";
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
        public DataSet get_TPPlanRetailers(string sf_Code, string Divcode, string routecode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "select ListedDrCode,ListedDr_Name from Mas_ListedDr where Division_Code=" + Divcode + " and ListedDr_Active_Flag=0 and CHARINDEX(','+'" + sf_Code + "'+',',','+Sf_Code+',')>0 and CHARINDEX(','+'" + routecode + "'+',',','+Territory_Code+',')>0 order by 2";
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
        public DataSet get_TPPlanWorktypes(string sf_Code, string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "exec getTPWOrktype '" + sf_Code + "','" + Divcode + "'";
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
        public string updateTPPlan(string sf_Code, string Divcode, string TpDate, string TMn, string TYr, string Distc, string Distname, string Retc, string Retn, string Routec, string wtypc, string wtypn, string Remarks, string POB, string SOB, string Styp)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            string msg = string.Empty;
            strQry = "exec up_SFTPWeb '" + sf_Code + "','" + Divcode + "','" + TpDate + "'," + TMn + "," + TYr + ",'" + Distc + "','" + Distname + "','" + Retc + "','" + Retn + "','" + Routec + "'," + wtypc + ",'" + wtypn + "','" + Remarks + "','" + POB + "','" + SOB + "','" + Styp + "'";
            try
            {
                dsSF = db_ER.Exec_DataSet(strQry);
                msg = "TP Entry Updated";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                throw ex;
            }
            return msg;
        }
        public DataSet get_DayPlanWorkedWith(string sf_Code)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "exec getJointWork_App '" + sf_Code + "','" + sf_Code + "'";
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
        public DataSet get_SFDayPlan(string sf_Code, string Divcode, string Dt)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "exec getSFDayplanWeb '" + sf_Code + "','" + Divcode + "','" + Dt + "'";
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
        public DataSet getDepot(string Divcode)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsSF = null;
            strQry = "exec getDepot '" + Divcode + "'";
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
        public DataTable getTpDetails(string sfcode,string Mn,string Yr)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataTable dsSF = null;
            strQry = "exec getExpTP '" + sfcode + "','"+ Mn + "','"+ Yr + "'";
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
    }
}

    
