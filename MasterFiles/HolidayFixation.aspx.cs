using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Bus_EReport;
using DBase_EReport;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
public partial class MasterFiles_HolidayFixation : System.Web.UI.Page
{
    private string strQry = string.Empty;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DataSet dshol = null;
    DataSet dsTP = null;
    string sState = string.Empty;
    string div_code = string.Empty;
    string slno;
    string str = string.Empty;
    string state_code = string.Empty;
    string[] statecd;
    string strState;
    string state_cd = string.Empty;
    string Holiday_Name = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    protected string Values;
    int time;
    string strCase = string.Empty;
    Repeater rptAmounts;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        Session["backurl"] = "HolidayList.aspx";
        slno = Request.QueryString["Sl_No"];   
        if (!Page.IsPostBack)
        {

            //bindHoliday();
            bindState();
            bindStatenew();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);
            if (dsTP.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
           // TextBox txtDate;
            if (slno != "" && slno != null)
            {
                Holiday dv = new Holiday();
                dshol = dv.getHoli(div_code, slno);

                if (dshol.Tables[0].Rows.Count > 0)
                {
                    // txtDate.Text = dshol.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                }
            }
        }
    }


    protected override void OnLoadComplete(EventArgs e)
    {

        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;

    } 
    public void bindHoliday()
    {
        SqlConnection con = new SqlConnection(Globals.ConnString);
        con.Open();
        //SqlCommand cmd = new SqlCommand(" SELECT Holiday_Id,Month,H.Holiday_Name,H.Fixed_date,H.Multiple_Date, convert(varchar,ISNULL(Holiday_Date,H.Fixed_date),105) Holiday_Date  from Holidaylist H left outer join  Mas_Statewise_Holiday_Fixation S on H.Holiday_Id=S.Holiday_Name_Sl_No where H.Holiday_Name='DIWALI' order by H.Month", con);
        //SqlCommand cmd = new SqlCommand("SELECT Holiday_Id,Month,H.Holiday_Name,H.Fixed_date,H.Multiple_Date, convert(varchar,ISNULL(Holiday_Date,H.Fixed_date),105) Holiday_Date  from Holidaylist H left outer join  Mas_Statewise_Holiday_Fixation S on H.Holiday_Id=S.Holiday_Name_Sl_No  order by H.Month", con);
        //SqlCommand cmd = new SqlCommand("SELECT  count(Holiday_Id) as Holiday_Id,H.Holiday_Name,H.Month from Holidaylist H left outer join  Mas_Statewise_Holiday_Fixation S on H.Holiday_Id=S.Holiday_Name_Sl_No group by H.Holiday_Name,H.Month order by H.Month", con);
        SqlCommand cmd = new SqlCommand( "CREATE TABLE #TestTable (Holiday_Id int,Month int,Holiday_Name VARCHAR(100), Multiple_Date int, " +
                                         "Holiday_Date VARCHAR(100), Holiday_SlNo int) " +
                                         "insert into #TestTable SELECT Holiday_Id,Month,H.Holiday_Name,H.Multiple_Date, " +
                                         "convert(varchar,ISNULL(h.Fixed_date,Holiday_Date),105)as Holiday_Date, H.Holiday_SlNo   " +
                                         "from Holidaylist H left outer join  Mas_Statewise_Holiday_Fixation S on " +
                                         "H.Holiday_Id=S.Holiday_Name_Sl_No where H.Division_Code='" + div_code + "' and Holiday_Active_Flag = 0  " +
                                         "SELECT  Holiday_Id,Month,Holiday_Name,Multiple_Date," +
                                         "STUFF((SELECT ',' + Holiday_Date FROM #TestTable " +
                                         "WHERE (Holiday_Id = StudentCourses.Holiday_Id) FOR XML PATH ('')),1,1,'') AS Holiday_Date " +
                                         "FROM #TestTable StudentCourses " +
                                         "GROUP BY Holiday_Id,Month,Holiday_SlNo,Holiday_Name,Multiple_Date order by Holiday_SlNo", con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
       
        da.Fill(ds);
        rptName.DataSource = ds;
        rptName.DataBind();
    }
    public DataSet getState(string state_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsState = null;
        //strQry = "SELECT 0 as state_code,'---Select State---' as statename,'' as shortname " +
        //         " UNION " +
        //         " SELECT state_code,statename,shortname " +
        //         " FROM mas_state " +
        //         " WHERE state_code in (" + state_code + ") " +
        //         " ORDER BY 2";
        
        strQry = " SELECT state_code,left(statename,12) as  statename,shortname " +
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
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    
    public void bindState()
    {
        DB_EReporting db_ER = new DB_EReporting();
        strQry = " SELECT state_code,statename,shortname " +
                          " FROM mas_state " +
                          " WHERE state_code in (" + state_code + ") " +
                          " ORDER BY 2";
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;

                    }
                    state_code = state_cd; 
                }
                i++;
            }

            dsState = getState(state_cd);
            rptYearHeader.DataSource = dsState;
            rptYearHeader.DataBind();
        }
    }
    protected void rptName_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        string squery = "";
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HtmlTableCell Holiday = (HtmlTableCell)e.Item.FindControl("TDHoliday"); 
            HiddenField hdnHolidayId = (HiddenField)e.Item.FindControl("hdnHolidayId");
            
            Repeater rptCal = (Repeater)e.Item.FindControl("rptAmounts");
            Label lblMulti = (Label)e.Item.FindControl("lblMulti");
            Literal litName = (Literal)e.Item.FindControl("litName");
            string Color = ds.Tables[0].Rows[e.Item.ItemIndex]["Month"].ToString();

            string Clr = GetCaseColor(Color);
            
            // Holiday.Attributes.Add("style", "background-color:" + Clr + ";");

            //litName.Text = "<span style='background : " + Clr + ";'>" + litName.Text + "</span>";
           //string squery = "SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted  FROM mas_state h left outer join  Mas_Statewise_Holiday_Fixation S   on h.state_code=s.state_code and holiday_name_sl_no=" + hdnHolidayId.Value;
           //string squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where s.state_code=h.state_code and  holiday_name_sl_no=" + hdnHolidayId.Value + " ) as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(strState.Remove(strState.Length - 1)) + ") order by statename";
            if (lblMulti.Text != "0")
            {
                //squery = " select h.state_code ,h.statename,case when  s.sl_no IS NULL then 0 else 1 end as HolidaySeleted from Mas_Statewise_Holiday_Fixation s,mas_state h where h.state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString()) + ") and s.state_code like '%'+ CAST(h.state_code as varchar) +'%' and s.holiday_name_sl_no=" + hdnHolidayId.Value + "";
                squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and state_code like '%'+ CAST(h.state_code  as varchar) +'%') as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
                DB_EReporting db_ER = new DB_EReporting();
                dsState = db_ER.Exec_DataSet(squery);

                rptCal.DataSource = dsState;
                rptCal.DataBind();
            }
            else
            {
                //squery = " select h.state_code ,h.statename,case when  s.sl_no IS NULL then 0 else 1 end as HolidaySeleted from Mas_Statewise_Holiday_Fixation s,mas_state h where h.state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString()) + ") and s.state_code like '%'+ CAST(h.state_code as varchar) +'%' and s.holiday_name_sl_no=" + hdnHolidayId.Value + "";
                //squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and state_code like '%'+ CAST(h.state_code  as varchar) +'%') as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
                squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and state_code =CAST(h.state_code  as varchar)) as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
                DB_EReporting db_ER = new DB_EReporting();
                dsState = db_ER.Exec_DataSet(squery);

                rptCal.DataSource = dsState;
                rptCal.DataBind();
            }
            //string squery = "select state_code ,statename, (SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  Mas_Statewise_Holiday_Fixation S where holiday_name_sl_no=" + hdnHolidayId.Value + " and state_code like '%'+ CAST(h.state_code  as varchar) +'%') as HolidaySeleted   from mas_state h where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");
           
        }
    }
    protected void rptCal_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            HiddenField hdnHolidaySeleted = (HiddenField)e.Item.FindControl("hdnseleted");
            HiddenField hdnStateName = (HiddenField)e.Item.FindControl("hdnStateName");
            CheckBox cb = (CheckBox)e.Item.FindControl("cbHolidaySelection");

            cb.ToolTip = hdnStateName.Value;
            if (hdnHolidaySeleted.Value == "1")
            {
                cb.Checked = true;
            }

        }

    }

   
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Repeater rptAmounts;
            Literal litHolidayname;
            TextBox txtDate;
            TextBox txtAdd;
            TextBox txtAddDate;
            CheckBox cbHolidaySelection;
            CheckBox chksta=new CheckBox();
            CheckBox chkstate2=new CheckBox();
            HiddenField hdnHolidayID;
            HiddenField hdnState_code;
            int iReturn = 0;
            string strDateValue = string.Empty;
            string existingHoliday = "";
            string existingholidayList = "";
            foreach (RepeaterItem item in rptName.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    rptAmounts = (Repeater)item.FindControl("rptAmounts");
                    litHolidayname = (Literal)item.FindControl("litName");
                    txtDate = (TextBox)item.FindControl("txtDate");
                    txtAdd = (TextBox)item.FindControl("txtAdd");
                    txtAddDate = (TextBox)item.FindControl("txtAdd1");
                    hdnHolidayID = (HiddenField)item.FindControl("hdnHolidayID");


                   
                    string strState = "";
                    string strState1 = "";
                    string strState2 = "";
                   
                    foreach (RepeaterItem checkItem in rptAmounts.Items)
                    {
                        string strStateCode = null;
                        
                        hdnState_code = (HiddenField)checkItem.FindControl("hdnState_code");
                        cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                        Label lblMulti = (Label)item.FindControl("lblMulti");

                        if (lblMulti.Text == "0")
                        {
                            chksta = (CheckBox)checkItem.FindControl("chkstate1");
                            chkstate2 = (CheckBox)checkItem.FindControl("chkstate2");
                        }
                        else
                        {
                            chksta = (CheckBox)checkItem.FindControl("chkstate1");
                            chkstate2 = (CheckBox)checkItem.FindControl("chkstate2");
                        }
                        
                        str = "";
                        strDateValue = "";
                       
                        if (cbHolidaySelection.Checked == true || chksta.Checked == true || chkstate2.Checked == true)
                        {                          
                            
                            if (txtDate.Text != "" || txtAdd.Text != "" || txtAddDate.Text != "")
                            {
                                statecd = lblValues.Text.Split(',');
                                strStateCode = hdnState_code.Value;
                                str += strStateCode + (',');
                                strDateValue += txtDate.Text + "," + txtAdd.Text + "," + txtAddDate.Text;
                                
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('The Holiday Date should not be empty');</script>");
                                txtDate.Focus();
                                //txtDate.BackColor = System.Drawing.Color.Yellow;
                            }                           
                            
                        }
                        else
                        {
                            if (txtDate.Text != "" || txtAdd.Text != "" || txtAddDate.Text != "")
                            {
                                statecd = lblValues.Text.Split(',');
                                strStateCode = hdnState_code.Value;
                                str += strStateCode + (',');
                                strDateValue = txtDate.Text + "," + txtAdd.Text + "," + txtAddDate.Text + (',');                                
                            }
                            
                        }

                        if (str != "" || txtDate.Text != "")
                        {                            
                            
                            string strDate = txtDate.Text.Substring(6, 4);

                            string[] strArry;
                            strArry = strDateValue.Split(',');
                            foreach (string streach in strArry)
                            {
                                if (streach == txtDate.Text)
                                {
                                    Holiday hol = new Holiday();
                                    existingholidayList = "";
                                    existingHoliday = hol.getHolidayState(div_code, hdnHolidayID.Value,txtDate.Text);
                                    string[] arrState = existingHoliday.Split(',');

                                    foreach (ListItem checkValue in Chkstate.Items)
                                    {
                                        if (!checkValue.Selected)
                                        {
                                            foreach (string stateStr in arrState)
                                            {
                                                if (stateStr == checkValue.Value)
                                                {
                                                    existingholidayList += stateStr + ",";
                                                }
                                            }
                                        }
                                    }

                                    strState = "";
                                    if (cbHolidaySelection.Checked == true && txtDate.Text != "")
                                    {
                                        strState += str;
                                        iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingHoliday);
                                    }
                                    else if (cbHolidaySelection.Checked == false && txtDate.Text != "")
                                    {
                                        existingholidayList = "";
                                        string strChkValue = str;
                                        strChkValue = strChkValue.Remove(strChkValue.Length - 1);
                                        string[] strExistingHoliday = existingHoliday.Split(',');
                                        foreach (string strExist in strExistingHoliday)
                                        {
                                            string[] arr = { strExist };
                                            arr = arr.Where(s => s != strChkValue).ToArray();
                                            if (arr.Length != 0)
                                            {
                                                string st = arr[0];
                                                existingholidayList += st + ",";

                                            }
                                        }

                                        existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
                                        iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingholidayList);
                                    }
                                }

                                if (streach == txtAdd.Text)
                                {
                                    Holiday hol = new Holiday();
                                    existingholidayList = "";
                                    existingHoliday = hol.getHolidayState(div_code, hdnHolidayID.Value,txtAdd.Text);
                                    string[] arrState = existingHoliday.Split(',');

                                    foreach (ListItem checkValue in Chkstate.Items)
                                    {
                                        if (!checkValue.Selected)
                                        {
                                            foreach (string stateStr in arrState)
                                            {
                                                if (stateStr == checkValue.Value)
                                                {
                                                   
                                                        existingholidayList += stateStr + ",";
                                                    
                                                }
                                            }
                                        }
                                    }
                                    

                                    
                                    strState1 = "";
                                    if (chksta.Checked == true && txtAdd.Text != "")
                                    {
                                        strState1 += str;
                                        //existingholidayList = "";
                                        //string strChkValue = str;
                                        //strChkValue = strChkValue.Remove(strChkValue.Length - 1);
                                        //string[] strExistingHoliday = existingHoliday.Split(',');
                                        //foreach (string strExist in strExistingHoliday)
                                        //{
                                        //    //string[] arr = { strExist };
                                        //    //arr = arr.Where(s => s != strChkValue).ToArray();
                                        //    //if (arr.Length != 0)
                                        //    //{
                                        //    //    string st = arr[0];
                                        //    existingholidayList += strChkValue + ",";

                                        //   // }
                                        //}
                                        ////existingholidayList += str + ",";
                                        //existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
                                        iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState1, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingHoliday);
                                    }
                                    else if (chksta.Checked == false && txtAdd.Text != "")
                                    {
                                        existingholidayList = "";
                                        string strChkValue = str;
                                        strChkValue = strChkValue.Remove(strChkValue.Length - 1);
                                        string[] strExistingHoliday = existingHoliday.Split(',');
                                        foreach (string strExist in strExistingHoliday)
                                        {
                                            string[] arr = { strExist };
                                            arr = arr.Where(s => s != strChkValue).ToArray();
                                            if (arr.Length != 0)
                                            {
                                                string st = arr[0];
                                                existingholidayList += st + ",";
                                                
                                            }
                                        }

                                        existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
                                       //existingHoliday = Convert.ToString(arr);

                                       iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState1, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingholidayList);
                                    }
                                }

                                if (streach == txtAddDate.Text)
                                {
                                    Holiday hol = new Holiday();
                                    existingholidayList = "";
                                    existingHoliday = hol.getHolidayState(div_code, hdnHolidayID.Value,txtAddDate.Text);
                                    string[] arrState = existingHoliday.Split(',');

                                    foreach (ListItem checkValue in Chkstate.Items)
                                    {
                                        if (!checkValue.Selected)
                                        {
                                            foreach (string stateStr in arrState)
                                            {
                                                if (stateStr == checkValue.Value)
                                                {
                                                    existingholidayList += stateStr;
                                                }

                                            }
                                        }
                                    }

                                    strState2 = "";
                                    if (chkstate2.Checked == true && txtAddDate.Text != "")
                                    {
                                        strState2 += str;
                                        //existingholidayList = "";
                                        //string strChkValue = str;
                                        //strChkValue = strChkValue.Remove(strChkValue.Length - 1);
                                        //string[] strExistingHoliday = existingHoliday.Split(',');
                                        //foreach (string strExist in strExistingHoliday)
                                        //{
                                        //    //string[] arr = { strExist };
                                        //    //arr = arr.Where(s => s != strChkValue).ToArray();
                                        //    //if (arr.Length != 0)
                                        //    //{
                                        //    //    string st = arr[0];
                                        //    existingholidayList += strExist + ",";

                                        //    //}

                                        //}
                                        //existingholidayList += strChkValue + ",";
                                       // existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
                                        iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState2, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingHoliday);
                                    }
                                    else if (chkstate2.Checked==false && txtAddDate.Text != "")
                                    {
                                        existingholidayList = "";
                                        string strChkValue = str;
                                        strChkValue = strChkValue.Remove(strChkValue.Length - 1);
                                        string[] strExistingHoliday = existingHoliday.Split(',');
                                        foreach (string strExist in strExistingHoliday)
                                        {
                                            string[] arr = { strExist };
                                            arr = arr.Where(s => s != strChkValue).ToArray();
                                            if (arr.Length != 0)
                                            {
                                                string st = arr[0];
                                                existingholidayList += st + ",";
                                                
                                            }

                                       }
                                        existingholidayList = existingholidayList.Remove(existingholidayList.Length - 1);
                                       iReturn = hol.RecordAdd(Convert.ToInt32(strDate), strState2, streach, litHolidayname.Text, div_code, Convert.ToInt32(hdnHolidayID.Value), lblMulti.Text, existingholidayList);
                                    }                                   
                                }
                            }
                            if (iReturn > 0)
                            {
                                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Holiday Created Successfully')</script>");

                            }
                        }
                    }

                }
            }
            }

        
        catch (Exception ex)
        {
            //Response.Redirect(ex.Message);
        }
    }
    public void bindStatenew()
    {

        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getSt(state_cd);
            Chkstate.DataTextField = "statename";
            Chkstate.DataValueField = "state_code";
            Chkstate.DataSource = dsState;
            Chkstate.DataBind();

        }
    }

    public DataSet getHoldayDate(string state_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsHolidayState = null;

        strQry = " select Sl_No,convert(char(10),Holiday_Date,103) Holiday_Date,Holiday_Name,State_Code from Mas_Statewise_Holiday_Fixation " +
                 " where State_Code in (" + state_code + ") " +
                 " ORDER BY 2";
        //strQry = "SELECT a.sl_no,convert(varchar,a.Holiday_Date,105), Holiday_Date, a.Academic_Year, a.state_code, a.Holiday_Name_Sl_No, a.Holiday_Name, a.Created_Date, a.Division_Code, b.StateName as State_Name" +
        //             " FROM mas_state b join Mas_Statewise_Holiday_Fixation a " +
        //             " on a.state_code like '" + state_code + ',' + "%'  or " +
        //             " a.state_code like '%" + ',' + state_code + "' or" +
        //             " a.state_code like '%" + ',' + state_code + ',' + "%' or a.state_code like '" + state_code + "'" +
        //             " WHERE convert(varchar,b.state_code)='" + state_code + "' ";

        try
        {
            dsHolidayState = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsHolidayState;
    }

    protected void rptYearHeader_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {

           
            //lblValues.Text = string.Empty;
            //foreach (ListItem li in Chkstate.Items)
            //{
              
            //    if (li.Selected)
            //    {
            //        strState += li.Value + ",";
            //        lblValues.Text += li.Value + ",";
            //        DataSet dsHoliday = new DataSet();
            //        dsHoliday = getHoldayDate(strState.Remove(strState.Length - 1));

            //        //rptYearHeader.DataSource = dsState;
            //        //rptYearHeader.DataBind();

            //        //bindHoliday();
            //        CheckBox cbHolidaySelection;
            //        Repeater rptAmounts;                    
                    
            //        foreach (RepeaterItem ri in rptName.Items)
            //        {
                        
                            
            //              rptAmounts = (Repeater)ri.FindControl("rptAmounts");
            //                foreach (RepeaterItem checkItem in rptAmounts.Items)
            //                {
            //                    Literal quantityLabel = (Literal)ri.FindControl("litName");
            //                    TextBox partNumberLabel = (TextBox)ri.FindControl("txtDate");
            //                    cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                                
            //                    string quantityText = quantityLabel.Text;
            //                    //string partNumberText = partNumberLabel.Text;
            //                    if (quantityText == dsHoliday.Tables[0].Rows[0]["Holiday_Name"].ToString())
            //                    {
            //                        partNumberLabel.Text = "25/12/2014";// dsHoliday.Tables[0].Rows[0]["Holiday_Date"].ToString();
            //                        cbHolidaySelection.Checked = true;
            //                    }
            //                }
                        
            //        }
            //    }
            //}
        
    }

    protected void btngo_onclick(object sender, EventArgs e)
    {
        string squery = "";
         System.Threading.Thread.Sleep(time);
         lblValues.Text = string.Empty;
      
         foreach (ListItem li in Chkstate.Items)
         {
            
             if (li.Selected)
             {
                 strState += li.Value + ",";
                 lblValues.Text += li.Value + ",";
                 dsState = getState(strState.Remove(strState.Length - 1));
                 rptYearHeader.DataSource = dsState;
                 rptYearHeader.DataBind();
                // bindHoliday();
             }
         }
         if (strState != "")
         {
             strState = strState.TrimEnd(',');
             ViewState["hidSelectedState"] = strState;
         }
   
        bindHoliday();
        pnlholiday.Visible = true;
        foreach (RepeaterItem ri in rptName.Items)
        {
            rptAmounts = (Repeater)ri.FindControl("rptAmounts");          
            
                foreach (RepeaterItem checkItem in rptAmounts.Items)
                {
                    Literal quantityLabel = (Literal)ri.FindControl("litName");
                    TextBox partNumberLabel = (TextBox)ri.FindControl("txtDate");
                    TextBox partNumberLabel1 = (TextBox)ri.FindControl("txtAdd");
                    TextBox partNumberLabel2 = (TextBox)ri.FindControl("txtAdd1");
                    CheckBox cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                    CheckBox ChkState = (CheckBox)checkItem.FindControl("chkstate1");
                    CheckBox ChkState1 = (CheckBox)checkItem.FindControl("chkstate2");
                    Label lblMulti = (Label)ri.FindControl("lblMulti");
                     //Label lblDate = (Label)ri.FindControl("Holiday_Date");
                    //cbHolidaySelection = (CheckBox)checkItem.FindControl("cbHolidaySelection");
                    string quantityText = quantityLabel.Text;                  

                    if (lblMulti.Text == "0")
                    {
                        partNumberLabel1.Visible = true;
                        partNumberLabel2.Visible = true;
                        ChkState.Visible = true;
                        ChkState1.Visible = true;

                        string[] str = partNumberLabel.Text.Split(',');
                        if (checkItem.ItemIndex == 0)
                        {
                          str.Length.ToString();                          
                          
                          for (int i = 0; i < str.Length; i++)
                          {
                              if (i==0)
                              {
                                  partNumberLabel.Text = str[0];
                                 
                              }
                              if (i == 1)
                              {
                                  partNumberLabel1.Text = str[1];
                              }
                              if (i == 2)
                              {
                                  partNumberLabel2.Text = str[2];
                              }
                          }

                          //if (String.IsNullOrEmpty(str[2]))
                          //{
                          //    partNumberLabel1.Text = str[1];                              
                          //}
                          //if (str.Length == 3 && str.Length == 2)
                          //{
                          //    partNumberLabel1.Text = str[2];
                          //}
                          //rptAmounts.DataSource = dsState;
                          //rptAmounts.DataBind();
                          //partNumberLabel1.Text = str[1];
                          //partNumberLabel2.Text = str[2];   
                         
                        }
                        ChkState.Controls.Add(new LiteralControl("<br/>"));
                        ChkState1.Controls.Add(new LiteralControl("<br/>"));                        
                    }

                    squery = "select state_code ,statename, " +
                               "(SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as HolidaySeleted, " +
                                "(SELECT s.holiday_name from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as holiday_name, " +
                                " (SELECT convert(char(10),holiday_date,105) from " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') " +
                                " as HolidayDate from mas_state h  where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");

                    DB_EReporting db_ER = new DB_EReporting();
                    dsState = db_ER.Exec_DataSet(squery);
                    cbHolidaySelection.Checked = false;
                    if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                        {
                            string str1 = dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString();
                            if (partNumberLabel.Text.Trim() == str1)
                            {
                                cbHolidaySelection.Checked = true;
                            }
                        }
                    }

                    squery = "select state_code ,statename, " +
                               "(SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel1.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as HolidaySeleted, " +
                                "(SELECT s.holiday_name from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel1.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as holiday_name, " +
                                " (SELECT convert(char(10),holiday_date,105) from " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel1.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') " +
                                " as HolidayDate from mas_state h  where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");

                  
                    dsState = db_ER.Exec_DataSet(squery);
                    ChkState.Checked = false;
                    if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                        {
                            string str1 = dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString();
                            if (partNumberLabel1.Text.Trim() == str1)
                            {
                                ChkState.Checked = true;
                            }
                        }
                    }

                    squery = "select state_code ,statename, " +
                              "(SELECT  case when  sl_no IS NULL  then 0 else 1 end as HolidaySeleted from  " +
                               "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel2.Text.Trim() + "'" +
                               "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as HolidaySeleted, " +
                               "(SELECT s.holiday_name from  " +
                                "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel2.Text.Trim() + "'" +
                                "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') as holiday_name, " +
                               " (SELECT convert(char(10),holiday_date,105) from " +
                               "Mas_Statewise_Holiday_Fixation S where convert(varchar(10),s.holiday_date,105)='" + partNumberLabel2.Text.Trim() + "'" +
                               "and (s.state_code like CAST(h.state_code  as varchar) +',%' or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)  or " +
                                " s.state_code like '%,' + CAST(h.state_code  as varchar)+ ',%') and s.Division_Code='" + div_code + "') " +
                               " as HolidayDate from mas_state h  where  state_code in (" + Convert.ToString(ViewState["hidSelectedState"].ToString() + ") order by statename");

                    
                    dsState = db_ER.Exec_DataSet(squery);
                    ChkState1.Checked = false;
                    if (dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString() != "")
                    {
                        if (dsState.Tables[0].Rows[checkItem.ItemIndex]["holiday_name"].ToString() == quantityLabel.Text)
                        {
                        if (partNumberLabel2.Text.Trim() == dsState.Tables[0].Rows[checkItem.ItemIndex]["HolidayDate"].ToString())
                        {
                            ChkState1.Checked = true;
                        }
                        }
                    }
                }
            
        }
    }
    protected void ChkAll_CheckedChanged(object sender, EventArgs e)
    {
        Chkstate.Attributes.Add("onclick", "checkAll(this);");
    }
    private string GetCaseColor(string caseSwitch)
    {
        switch (caseSwitch)
        {
            case "1":
                strCase = "#FFFFCC";
                break;
            case "2":
                strCase = "#CCCC66";
                break;
            case "3":
                strCase = "#6699FF";
                break;
            case "4":
                strCase = "#CCFFFF";
                break;
            case "5":
                strCase = "#CCFFCC";
                break;
            case "6":
                strCase = "#F7819F";
                break;
            case "7":
                strCase = "#0B610B";
                break;
            case "8":
                strCase = "#FF4000";
                break;
            case "9":
                strCase = "#FE2E64";
                break;
            case "10":
                strCase = "#9AFE2E";
                break;
            case "11":
                strCase = "#F2F5A9";
                break;
            case "12":
                strCase = "#F5A9D0";
                break;
            default:
                strCase = "#F2F5A9";
                break;
        }
        return strCase;
    }
}