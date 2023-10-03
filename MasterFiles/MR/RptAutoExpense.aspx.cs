using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections;
using System.Configuration;
using Bus_EReport;
public partial class MasterFiles_MR_RptAutoExpense : System.Web.UI.Page
{

    bool isSavedPage = false;
    bool isEmpty = true;
    string sfcode = string.Empty;
    string divcode = string.Empty;
    DataSet expenseDataset = null;
    DataSet placeDataset = null;
    double otherExpAmnttot = 0;
    ArrayList pList = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        sfcode = Convert.ToString(Session["Sf_Code"]);
        divcode = Convert.ToString(Session["div_code"]);
        sfCode.Value=sfcode;
        divCode.Value=divcode;

        if (!IsPostBack)
        {
            GetMyMonthList();
            bind_year_ddl();

        }
    }

    public void GetMyMonthList()
    {
        DateTime month = Convert.ToDateTime("1/1/2012");
        for (int i = 0; i < 12; i++)
        {
            DateTime nextMonth = month.AddMonths(i);
            ListItem list = new ListItem();
            list.Text = nextMonth.ToString("MMMM");
            list.Value = nextMonth.Month.ToString();
            monthId.Items.Add(list);
        }
        monthId.Items.Insert(0, new ListItem("  Select Month  ", "0"));
    }

    private void bind_year_ddl()
    {
        int year = (System.DateTime.Now.Year);
        for (int intCount = 2015; intCount <= year + 1; intCount++)
        {
            yearID.Items.Add(intCount.ToString());
        }
        yearID.Items.Insert(0, new ListItem("  Select Year  ", "0"));
    }
    private string getDisplayToPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                string p1 = p.Substring(0, p.IndexOf('('));

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        distinctValues = distinctValues + "," + p1;
                    else
                        distinctValues = distinctValues + "," + p1;
                }
                else
                    distinctValues = p1;


                counter++;

            }
        }
        return distinctValues;
    }
    private string getDisplayPlaceOfWork(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            int counter = 0;
            foreach (string p in distSet)
            {
                int startInx = p.IndexOf('(');
                int endInx = p.LastIndexOf(')') + 1;
                string type = p.Substring(startInx, endInx - startInx);
                string p1 = p.Substring(0, p.IndexOf('(')) + "<span style=\"background-color:yellow\">" + type + "</span>";

                if (distinctValues != "")
                {
                    if ((counter % 2) == 0)
                        distinctValues = distinctValues + p1;
                    else
                        distinctValues = distinctValues + "," + p1;

                }
                else
                    distinctValues = p1;

                counter++;
            }
        }
        return distinctValues;
    }

    private string getDistinctPlaces(string places)
    {
        string distinctValues = "";
        if (null != places && places != "")
        {
            String[] iteams = places.Split(',');
            var distSet = new HashSet<String>(iteams);
            foreach (string p in distSet)
            {
                if (distinctValues != "")
                    distinctValues = distinctValues + "," + p;
                else
                    distinctValues = p;
            }
        }
        return distinctValues;
    }
    private string getOSPlace(string places)
    {
        if (null != places && places != "")
        {

            String[] iteams = places.Split(',');
            foreach (string i in iteams)
            {
                if (i.Contains("(OS") && !i.Contains("(OS-EX"))
                {
                    return "'" + i.Substring(0, i.IndexOf('(')) + "'";
                }
            }
        }
        return "''";
    }
    private string getDistinctPlacesQry(string places)
    {

        HashSet<string> temp = new HashSet<string>();
        String[] iteams = places.Split(',');
        ArrayList list = new ArrayList();

        foreach (string i in iteams)
        {
            if (i.Contains("(OS") && !i.Contains("(OS-EX"))
            {

            }
            else
            {
                list.Add(i);
            }

        }
        //var distSet = new HashSet<String>(iteams);
        string distinctValues = "";
        foreach (string p in list)
        {
            string p1 = p.Substring(0, p.IndexOf('('));
            if (distinctValues != "")
                distinctValues = distinctValues + ",'" + p1 + "'";
            else
                distinctValues = "'" + distinctValues + p1 + "'";
        }

        return distinctValues;
    }
    private string getSingleOsDistinctPlacesQry(string places)
    {

        HashSet<string> temp = new HashSet<string>();
        String[] iteams = places.Split(',');

        var distSet = new HashSet<String>(iteams);
        string distinctValues = "'";
        foreach (string p in distSet)
        {
            string p1 = p.Substring(0, p.IndexOf('('));
            if (distinctValues != "'")
                distinctValues = distinctValues + ",'" + p1 + "'";
            else
                distinctValues = distinctValues + p1 + "'";
        }

        return distinctValues;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        //mainDiv.Visible = false;
        //System.Threading.Thread.Sleep(time);
        //_css = "removeMainDiv";
        Response.Redirect("RptAutoExpense.aspx");
        mainDiv.Style.Remove("background-color");
        //mainDiv.Attributes.Add("class", mainDiv.Attributes["class"].Replace("mainDiv", "removeMainDiv"));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            mainDiv.Style.Value = "background-color:white;padding:0 100px 100px";
            //_css = "mainDiv";
            Distance_calculation Exp = new Distance_calculation();
            DataTable ds = Exp.getFieldForce(divcode, sfcode);
            DataTable owt = Exp.getotherWorkType(divcode);
            // mainDiv.Visible = true; 
            // mainDiv.Attributes.Add("class", mainDiv.Attributes["class"].Replace("removeMainDiv", "mainDiv"));
            menuId.Visible = false;
            heading.Visible = true;
            mnthtxtId.InnerHtml = monthId.SelectedItem.ToString();
            yrtxtId.InnerHtml = yearID.SelectedValue.ToString();
            fieldforceId.InnerHtml = "Fieldforce Name :" + ds.Rows[0]["sf_name"].ToString();
            hqId.InnerHtml = "HQ :" + ds.Rows[0]["sf_hq"].ToString();
            empId.InnerHtml = "Employee Code :" + ds.Rows[0]["sf_code"].ToString();
            double totalAllowance = 0;
            double totalDistance = 0;
            double totalFare = 0;
            double grandTotal = 0;
            otherExpAmnttot = 0;
            DataTable frmPlaceDT = Exp.getFrmandTo(divcode, sfcode);
            DataTable dist = Exp.getDist(divcode, sfcode);

            ListItem[] frmLists = new ListItem[frmPlaceDT.Rows.Count + ds.Rows.Count];
            ListItem[] toLists = new ListItem[frmPlaceDT.Rows.Count];

            if (frmPlaceDT.Rows.Count > 0)
            {
                for (int i = 0; i < frmPlaceDT.Rows.Count; i++)
                {
                    DataRow row = frmPlaceDT.Rows[i];
                    ListItem list = new ListItem();
                    list.Text = row["Territory_Name"].ToString();
                    list.Value = row["territory_code"].ToString() + "~~" + row["Town_Cat"];
                    frmLists[i] = list;
                    toLists[i] = list;
                }
            }
            if (ds.Rows.Count > 0)
            {
                for (int i = frmPlaceDT.Rows.Count; i < (ds.Rows.Count + frmPlaceDT.Rows.Count); i++)
                {
                    int j = i - (frmPlaceDT.Rows.Count);
                    DataRow row = ds.Rows[j];
                    ListItem list = new ListItem();
                    list.Text = row["sf_hq"].ToString();
                    list.Value = row["sf_code"].ToString();
                    frmLists[i] = list;
                }
            }

            String disValue = "";
            foreach (DataRow r in dist.Rows)
            {
                disValue = disValue + r["FrmTown"].ToString().Trim() + "#" + r["ToTown"].ToString().Trim() + "#" + r["Distance"] + "$";

            }
            distString.Value = disValue;

            DataSet dsAllowance = Exp.getAllow(sfcode);
            DataTable allowanceTab = dsAllowance.Tables[0];
            DataTable otherExpTable = Exp.getOthrExp(divcode);

            String ex = "0";
            String os = "0";
            String hq = "0";
            double fare = 0;
            string allowStr = "";
            if (allowanceTab != null)
            {
                foreach (DataRow row in allowanceTab.Rows)
                {
                    ex = row["ex_allowance"].ToString();
                    os = row["os_allowance"].ToString();
                    hq = row["hq_allowance"].ToString();
                    fare = Convert.ToDouble(row["fare"].ToString());

                    allowStr = ex + "@" + os + "@" + hq + "@" + fare;
                }
                allowString.Value = allowStr;
            }
            if (Exp.headRecordExist(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode))
            {
                DataTable t1 = Exp.getSavedRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode,divcode);

                foreach (DataRow row in t1.Rows)
                {
                    totalAllowance = totalAllowance + Convert.ToDouble(row["Allowance"].ToString());
                    totalDistance = totalDistance + Convert.ToDouble(row["Distance"].ToString());
                    totalFare = totalFare + Convert.ToDouble(row["Fare"].ToString());
                    grandTotal = grandTotal + Convert.ToDouble(row["Total"].ToString());
                    String filter = "Work_Type_Name='" + row["Worktype_Name_B"].ToString() + "'";
                    if (!"Field Work".Equals(row["Worktype_Name_B"].ToString()))
                    {
                        /*DataRow[] dr =owt.Select(filter);
                        if (dr.Count() > 0)
                        {
                            row["Territory_Cat"] = dr[0]["Allow_type"];
                        }*/
                    }

                }
                t1.Rows.Add();
                t1.Rows[t1.Rows.Count - 1]["Allowance"] = totalAllowance;
                t1
                    .Rows[t1.Rows.Count - 1]["Distance"] = totalDistance;
                t1.Rows[t1.Rows.Count - 1]["Fare"] = totalFare;
                t1.Rows[t1.Rows.Count - 1]["Total"] = grandTotal;


                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                //if (!isEmpty)
                //{
                generateOtherExpControls(Exp);
                //}
                //else
                //{
                //generateOtherExpListData(otherExpTable);
                //}
                DataTable headR = Exp.getHeadRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
                if (headR.Rows[0]["Status"].ToString() == "1")
                {
                    btnSave.Visible = false;
                    btnDrftSave.Visible = false;
                    messageId.Visible = true;
                    messageId.Text = "Expense yet to proccess";
                }
                else if (headR.Rows[0]["Status"].ToString() == "2")
                {
                    messageId.Visible = true;
                    messageId.Text = "Expense proccessed";
                }
                else
                {
                    messageId.Visible = false;

                }

            }
            else
            {
                DataSet dsExDist = null;
                expenseDataset = Exp.getExpense(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                placeDataset = Exp.getPlace(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());



                // DataTable contacts = expenseDataset.Tables[0];
                //DataTable tb22 = placeDataset.Tables[0];

                DataTable t1 = expenseDataset.Tables[0];
                DataTable t2 = placeDataset.Tables[0];

                foreach (DataRow row in t1.Rows)
                {
                    String filter = "Activity_Date='" + row["Activity_Date"].ToString() + "'";
                    DataRow[] rows = t2.Select(filter);
                    if (!(row["Worktype_Name_B"].Equals("Field Work")))
                    {
                        filter = "Work_Type_Name='" + row["Worktype_Name_B"].ToString() + "'";
                        DataRow[] ss = owt.Select(filter);
                        if (ss.Count() > 0)
                        {
                            row["Territory_Cat"] = ss[0]["Allow_type"];
                        }
                    }

                    foreach (DataRow r in rows)
                    {

                        if (row["Territory_Name"] != null && row["Territory_Name"].ToString() != "")
                        {
                            row["Territory_Name"] = row["Territory_Name"].ToString() + "," + r["Territory_Name"] + "(" + r["Territory_Cat"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }
                        else
                        {
                            row["Territory_Name"] = r["Territory_Name"] + "(" + r["Territory_Cat"].ToString() + "-" + r["cnt"].ToString() + ")";
                        }

                        String old = "";
                        String curr = "";
                        if (row["Territory_Cat"] != null)
                        {
                            old = row["Territory_Cat"].ToString();
                        }
                        if (r["Territory_Cat"] != null)
                        {
                            curr = r["Territory_Cat"].ToString();
                        }
                        if (old == "")
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else if (old.Equals("OS-EX"))
                        {
                        }
                        else if (curr.Equals("OS-EX"))
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else if (curr.Equals("OS") && !old.Equals("OS-EX"))
                        {
                            row["Territory_Cat"] = curr;
                        }
                        else
                        {
                            row["Territory_Cat"] = curr;
                        }
                    }
                }
                t1.Columns.Add("PrevType");
                t1.Columns.Add("NextType");
                t1.Columns.Add("Allowance");
                t1.Columns.Add("Distance");
                t1.Columns.Add("From_place");
                t1.Columns.Add("To_place");
                t1.Columns.Add("Fare");
                t1.Columns.Add("Total");
                t1.Columns.Add("catTemp");
                t1.Rows.Add();
                string toPlace = "";
                double tDistance = 0;
                for (int i = 0; i < t1.Rows.Count; i++)
                {
                    try
                    {

                    int prevIndex = (i == 0 ? i : i - 1);
                    int nextIndex = (i == t1.Rows.Count - 1 ? i : i + 1);
                    DataRow currRow = t1.Rows[i];
                    DataRow prevRow = t1.Rows[prevIndex];
                    DataRow nextRow = t1.Rows[nextIndex];
                    currRow["PrevType"] = prevRow["Territory_Cat"];
                    currRow["NextType"] = nextRow["Territory_Cat"];
                    String prev = currRow["PrevType"].ToString();
                    String next = currRow["PrevType"].ToString();
                    currRow["Allowance"] = (currRow["Territory_Cat"] == null ? "0" : (currRow["Territory_Cat"].ToString() == "HQ" ? hq : currRow["Territory_Cat"].ToString() == "OS" ? os : currRow["Territory_Cat"].ToString() == "EX" ? ex : currRow["Territory_Cat"].ToString() == "OS-EX" ? os : "0"));
                    String currCatType = "";
                    String prevCatType = "";
                    String nextCatType = "";
                    /*if (!(currRow["Worktype_Name_B"].Equals("Field Work")))
                    {
                        String filter = "Work_Type_Name='" + currRow["Worktype_Name_B"].ToString() + "'";
                        DataRow[] ss = owt.Select(filter);
                        if (ss.Count() > 0)
                        {
                            currRow["Territory_Cat"] = ss[0]["Allow_type"];
                        }
                    }*/

                    if (currRow["Territory_Cat"] != null)
                    {
                        currCatType = currRow["Territory_Cat"].ToString();
                    }
                    if (prevRow["prevType"] != null)
                    {
                        prevCatType = currRow["prevType"].ToString();
                    }
                    if (nextRow["nextType"] != null)
                    {
                        nextCatType = currRow["nextType"].ToString();

                    }
                    currRow["catTemp"] = currCatType;
                    //if ((currRow["Worktype_Name_B"].Equals("Field Work")) || currRow["Activity_Date"].ToString() == null || currRow["Activity_Date"].ToString() == "")
                   // {
                        if (currCatType == "HQ")
                        {
                            currRow["From_place"] = "";
                            currRow["To_place"] = "";
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            currRow["Allowance"] = hq;
                            currRow["Total"] = hq;
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            totalAllowance = totalAllowance + allow;
                            grandTotal = grandTotal + allow;
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());

                        }
                        else if (currCatType == "EX")
                        {
                            dsExDist = Exp.getExDistance(divcode, sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
                            DataTable exDistTab = dsExDist.Tables[0];
                            if (currRow["Worktype_Name_B"].Equals("Field Work"))
                            {
                                currRow["From_place"] = currRow["sf_hq"];
                                currRow["To_place"] = getDisplayToPlaces(currRow["Territory_Name"].ToString());
                                currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            }
                            String filter = "adate='" + currRow["Activity_Date"].ToString() + "'";
                            DataRow[] rows = exDistTab.Select(filter);
                            int d = 0;
                            foreach (DataRow r in rows)
                            {
                                d = Convert.ToInt32(r["dist"].ToString());
                                currRow["Distance"] = d;
                            }
                            double totFare = d * fare;
                            currRow["Fare"] = totFare;
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            currRow["Allowance"] = ex;
                            double total = totFare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + d;
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;
                        }
                        else if ((!currRow["Worktype_Name_B"].Equals("Field Work")) && currRow["Activity_Date"].ToString() != null && currRow["Activity_Date"].ToString() != "")
                        {
                            currRow["From_place"] = "";
                            currRow["To_place"] = "";
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            currRow["Allowance"] = currCatType=="HQ"?hq:currCatType=="EX"?ex:currCatType=="OS"?os:"0";
                            currRow["Total"] = currCatType == "HQ" ? hq : currCatType == "EX" ? ex : currCatType == "OS" ? os : "0";
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            totalAllowance = totalAllowance + allow;
                            grandTotal = grandTotal + allow;
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (prevCatType.Equals("HQ") || prevCatType.Equals("EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX")))
                        {
                            currRow["From_place"] = currRow["sf_hq"];
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }
                            toPlace = getDistinctPlaces(toPlace);
                            currRow["To_place"] = getDisplayToPlaces(toPlace);
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());

                            int osDistance;
                            string osFrmCode;
                            /*if (currCatType.Equals("OS-EX"))
                            {

                                getOsDistAndFrmCode(Exp, toPlace, out osDistance, out osFrmCode);
                                osDistance = getOsExDistance(Exp, toPlace, osDistance, osFrmCode);
                            }
                            else
                            {*/
                            getSingleOsDistAndFrmCode(Exp, toPlace, out osDistance, out osFrmCode);
                            //}
                            currRow["Distance"] = osDistance;
                            double totFare = osDistance * fare;
                            currRow["Fare"] = totFare;
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            double total = totFare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;

                            toPlace = "";
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("HQ") || nextCatType.Equals("EX") || nextCatType.Equals("NA")))
                        {
                            // osFlag = false;
                            currRow["From_place"] = currRow["sf_hq"];
                            if (toPlace != "")
                                toPlace = toPlace + "," + currRow["Territory_Name"];
                            else
                            {
                                toPlace = currRow["Territory_Name"].ToString();
                            }
                            pList.Add(currRow["Territory_Name"].ToString());
                            currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                            string places = getDistinctPlaces(toPlace);
                            currRow["To_place"] = getDisplayToPlaces(places);
                            int osDistance;
                            string osFrmCode;
                            getOsDistAndFrmCode(Exp, places, out osDistance, out osFrmCode);
                            osDistance = getOsExDistance(Exp, places, osDistance, osFrmCode);
                            tDistance = tDistance + osDistance;
                            currRow["Distance"] = tDistance;
                            double totFare = osDistance * fare;
                            currRow["Fare"] = totFare;
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            double total = totFare + allow;
                            currRow["Total"] = total;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + osDistance;
                            totalFare = totalFare + totFare;
                            grandTotal = grandTotal + total;

                            toPlace = "";
                            // osCalculated = false;
                            tDistance = 0;

                            pList = new ArrayList();
                        }
                        else if ((currCatType.Equals("OS") || currCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("") ))
                        {
                            //  osFlag = true;
                            if (currRow["Territory_Name"].ToString() != "")
                            {
                                currRow["From_place"] = "";
                                currRow["To_place"] = "";
                                currRow["Distance"] = "0";
                                currRow["Fare"] = 0;
                                if (toPlace != "")
                                {
                                    toPlace = toPlace + "," + currRow["Territory_Name"];
                                }
                                else
                                {
                                    toPlace = currRow["Territory_Name"].ToString();
                                }
                                pList.Add(currRow["Territory_Name"].ToString());
                                currRow["Territory_Name"] = getDisplayPlaceOfWork(currRow["Territory_Name"].ToString());
                                //int osDistance;
                                //string osFrmCode;
                                //getOsDistAndFrmCode(Exp, currRow["Territory_Name"].ToString(), out osDistance, out osFrmCode);
                                //osDistance = getOsExDistance(Exp, currRow["Territory_Name"].ToString(), osDistance, osFrmCode);
                                //tDistance = tDistance + osDistance;
                                //totalDistance = totalDistance + totalDistance;

                                double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                                currRow["Total"] = allow;
                                grandTotal = grandTotal + allow;
                                totalAllowance = totalAllowance + allow;
                            }
                        }
                        //            else if (!(currRow["Worktype_Name_B"].Equals("Field Work")) && (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX")) && (nextCatType.Equals("OS") || nextCatType.Equals("OS-EX") || nextCatType.Equals("")))
                        else if (!(currRow["Worktype_Name_B"].Equals("Field Work")) && (prevCatType.Equals("OS") || prevCatType.Equals("OS-EX") || prevCatType.Equals("")))
                        {
                            currRow["Total"] = "0";
                            currRow["From_place"] = "";
                            currRow["To_place"] = "";
                            currRow["Distance"] = "0";
                            currRow["Fare"] = 0;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_B"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                currRow["catTemp"] = ss[0]["Allow_type"];
                            }
                        }
                        else
                        {
                            double allow = Convert.ToDouble(currRow["Allowance"].ToString());
                            currRow["Total"] = allow;
                            String filter = "Work_Type_Name='" + currRow["Worktype_Name_B"].ToString() + "'";
                            DataRow[] ss = owt.Select(filter);
                            if (ss.Count() > 0)
                            {
                                currRow["Territory_Cat"] = ss[0]["Allow_type"];
                            }
                            if (currRow["Allowance"].ToString().Equals("0"))
                            {
                                currRow["From_place"] = "";
                                currRow["Allowance"] = "";
                                currRow["Distance"] = "0";
                            }
                            else
                            {
                                if (currRow["Worktype_Name_B"].Equals("Field Work"))
                                {
                                    currRow["From_place"] = currRow["sf_hq"];
                                }
                                currRow["Distance"] = "0";
                            }

                            currRow["To_place"] = currRow["Territory_Name"];

                            double totFare = fare;
                            //currRow["Fare"] = totFare;
                            totalAllowance = totalAllowance + allow;
                            totalDistance = totalDistance + 0;
                            //totalFare = totalFare + totFare;
                            grandTotal = grandTotal + allow;

                            toPlace = "";
                        }
                        if (currRow["Activity_Date"].ToString() == null || currRow["Activity_Date"].ToString() == "")
                        {
                            currRow["Allowance"] = totalAllowance;
                            currRow["Distance"] = totalDistance;
                            currRow["Fare"] = totalFare;
                            currRow["Total"] = grandTotal;
                        }
                        if (currRow["Worktype_Name_B"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_B"].ToString().Equals("Holiday"))
                        {
                            currRow["Adate"] = "<span style='background-color:#FFE2D5'>" + currRow["Adate"].ToString() + "</span>";
                            currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
                        }
                     }
                    catch (Exception ex1)
                    {
                        Console.WriteLine("Exception Type: {0}", ex1.GetType());
                        Console.WriteLine("  Message: {0}", ex1.Message);
                    }

                    }
                    //if (currRow["Worktype_Name_B"].ToString().Equals("Weekly Off") || currRow["Worktype_Name_B"].ToString().Equals("Holiday"))
                    //{
                      //  currRow["Adate"] = "<span style='background-color:#FFE2D5'>" + currRow["Adate"].ToString() + "</span>";
                        //currRow["theDayName"] = "<span style='background-color:#FFE2D5'>" + currRow["theDayName"].ToString() + "</span>";
                   // }

                //}
                //var query = from tbl1 in contacts.AsEnumerable()
                //                                       join tbl2 in tb22.AsEnumerable() on tbl1["activity_date"] equals tbl2["activity_date"]
                //        select new { Territory_Name = tbl2["Territory_Name"], cnt = tbl2["cnt"], activity_date = tbl1["activity_date"], Adate = tbl1["Adate"], Territory_code = tbl2["Territory_code"] };
                /*   var table1=contacts.AsEnumerable();
                   var table2 = tb22.AsEnumerable();
                   var query1 = (from tab1 in table1 
                                join tab2 in table2 on tab1["activity_date"] equals tab2["activity_date"] into datas
                                 from tab in datas.DefaultIfEmpty()
                     
                               select new
                               {
                                   Name = (tab == null ? "" : tab["Territory_Name"]),
                                   Territory_Cat = (tab == null ? null : tab["Territory_Cat"]),
                                   sf_hq = (tab == null ? null : tab["sf_hq"]),
                                   activity_date = tab1["activity_date"],
                                   Adate = tab1["Adate"],
                                   theDayName = tab1["theDayName"],
                                   Allowance = (tab == null ? "" : (tab["Territory_Cat"].ToString() == "HQ" ? hq : tab["Territory_Cat"].ToString() == "OS" ? os : tab["Territory_Cat"].ToString() == "EX" ? ex : "")),
                                   Worktype_Name_B = tab1["Worktype_Name_B"],
                               });

                   foreach(var row in  query1)
                   {
                   }

                   var query = from tbl1 in contacts.AsEnumerable()
                               join tbl2 in tb22.AsEnumerable() on tbl1["activity_date"] equals tbl2["activity_date"] into datas
                               from data in datas.DefaultIfEmpty()
                                   where data!=null
                                   select new { 
                                   Territory_Name = (data["Territory_Name"]==null)?null:data["Territory_Name"],
                                   Territory_Cat = (data["Territory_Cat"] == null) ? null : data["Territory_Cat"],
                                   activity_date = (data["activity_date"] == null) ? null : data["activity_date"],
                                   Adate = tbl1["Adate"],theDayName = tbl1["theDayName"],Worktype_Name_B = tbl1["Worktype_Name_B"],
                                                       };
                   */

                //DataView view = query..CopyToDataTable().AsDataView();

                //DataTable contacts11 = query.CopyToDataTable();
                //if (view.Count > 0)
                // {

                misExp.Visible = true;
                grdExpMain.Visible = true;
                grdExpMain.DataSource = t1;
                grdExpMain.DataBind();
                generateOtherExpListData(otherExpTable);

            }

            DataTable customExpTable = new DataTable();
            customExpTable.Columns.Add("Expense_Parameter_Name");
            customExpTable.Columns.Add("amount");
            customExpTable.Columns.Add("Expense_Parameter_Code");

            DataTable expParamsAmnt = Exp.getExpParamAmt(sfcode, divcode);
            double otherExAmnt = 0;
            if (expParamsAmnt.Rows.Count > 0)
            {
                for (int i = 0; i < expParamsAmnt.Rows.Count; i++)
                {
                    string colName = "Fixed_Column" + (i + 1);
                    if (expParamsAmnt.Rows[i][colName].ToString() != "")
                    {
                        otherExAmnt = otherExAmnt + Convert.ToDouble(expParamsAmnt.Rows[i][colName].ToString());
                    }
                    customExpTable.Rows.Add();

                    customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Name"] = expParamsAmnt.Rows[i]["Expense_Parameter_Name"];
                    customExpTable.Rows[customExpTable.Rows.Count - 1]["Expense_Parameter_Code"] = expParamsAmnt.Rows[i]["Expense_Parameter_Code"];
                    customExpTable.Rows[customExpTable.Rows.Count - 1]["amount"] = expParamsAmnt.Rows[i][colName] == "" ? "0" : expParamsAmnt.Rows[i][colName];

                }
                otherExpGrid.DataSource = customExpTable;
                otherExpGrid.DataBind();
            }

            // fixedExpense.InnerHtml = "180";
            Othtotal.Value = "0";
            double tot = otherExAmnt + grandTotal + otherExpAmnttot;
            grandTotalName.InnerHtml = tot.ToString();
            //}
            // else
            // {
            //    grdSalesForce.DataSource = dsExpense;
            //     grdSalesForce.DataBind();
            // }

            foreach (GridViewRow gridRow in grdExpMain.Rows)
            {
                Label lblCat = (Label)gridRow.FindControl("lblCat");
                Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
                HiddenField allowTypeVal = (HiddenField)gridRow.FindControl("allowTypeHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden1");
                // String filter = "Allow_type='" + lblTerrName.Text + "'";
                // string type = "";

                /*   if (!"Field Work".Equals(lblWorkType.Text))
                   {
                       DataRow[] dr = owt.Select(filter);
                       if (dr.Count() > 0)
                       {
                           type = dr[0]["Allow_type"].ToString();
                       }
                   }*/
                Label frmLbl = ((Label)gridRow.FindControl("lblFrom"));
                Label toLbl = ((Label)gridRow.FindControl("lblTo"));
                HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
                HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
                //Label toHidden = (Label)gridRow.FindControl("toHidden");
                DropDownList frmBox = ((DropDownList)gridRow.FindControl("fromPlace"));
                DropDownList toBox = ((DropDownList)gridRow.FindControl("toPlace"));
                DropDownList allowType = ((DropDownList)gridRow.FindControl("AllowType"));
                Label allTypLbl = ((Label)gridRow.FindControl("lblCat"));
                TextBox txtAllow = ((TextBox)gridRow.FindControl("txtAllow"));
                Label allowLbl = ((Label)gridRow.FindControl("lblAllw"));
                TextBox txtFare = ((TextBox)gridRow.FindControl("txtFare"));
                Label fareLbl = ((Label)gridRow.FindControl("lblFare"));

                if ("AA-NF".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("NA-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    allowLbl.Text = "";
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    allowLbl.Text = "0";
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AE-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = true;
                    allowLbl.Visible = false;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AA-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AA-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AS-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = true;
                    allowType.SelectedValue = allowTypeVal.Value;
                    allTypLbl.Visible = false;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AS-FE".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {
                    allowType.Visible = true;
                    allowType.SelectedValue = allowTypeVal.Value;
                    allTypLbl.Visible = false;
                    txtAllow.Visible = false;
                    allowLbl.Visible = true;
                    txtFare.Visible = true;
                    fareLbl.Visible = false;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
                else if ("AE-FA".Equals(allowTypeHidden.Value, StringComparison.OrdinalIgnoreCase))
                {

                    allowType.Visible = false;
                    allTypLbl.Visible = true;
                    txtAllow.Visible = true;
                    allowLbl.Visible = false;
                    txtFare.Visible = false;
                    fareLbl.Visible = true;
                    changeFromToCtrl(frmLists, toLists, frmLbl, toLbl, frmBox, toBox, fromHidden.Value, toHidden.Value);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);
        }
    }

    private void generateOtherExpListData(DataTable otherExpTable)
    {
        if (otherExpTable.Rows.Count > 0)
        {
            foreach (DataRow row in otherExpTable.Rows)
            {
                ListItem list = new ListItem();
                list.Text = row["Expense_Parameter_Name"].ToString();
                list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
                otherExp.Items.Add(list);
            }
            //otherExp.Items.Insert(0, new ListItem("  --Select--  ", "0"));
            //otherExp.DataTextField = "Expense_Parameter_Name";
        }
    }

    private static void changeFromToCtrl(ListItem[] frmLists, ListItem[] toLists, Label frmLbl, Label toLbl, DropDownList frmBox, DropDownList toBox, string from, string to)
    {
        ListItem[] newfrmLists = new ListItem[frmLists.Count()];
        ListItem[] newtoLists = new ListItem[toLists.Count()];
        for (int i = 0; i < frmLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = frmLists[i].Text;
            l.Value = frmLists[i].Value;
            newfrmLists[i] = l;
        }
        for (int i = 0; i < toLists.Count(); i++)
        {
            ListItem l = new ListItem();
            l.Text = toLists[i].Text;
            l.Value = toLists[i].Value;
            newtoLists[i] = l;
        }
        frmBox.Visible = true;
        frmLbl.Visible = false;
        frmBox.Items.AddRange(newfrmLists);
        if (from != null && from != "")
        {
            frmBox.ClearSelection();
            frmBox.Items.FindByText(from).Selected = true;
        }
        toBox.Visible = true;
        toLbl.Visible = false;
        toBox.Items.AddRange(newtoLists);
        if (to != null && to != "")
        {
            toBox.ClearSelection();
            toBox.Items.FindByText(to).Selected = true;
        }
    }

    private int getOsExDistance(Distance_calculation Exp, string places, int osDistance, string osFrmCode)
    {
        string osPlace = getOSPlace(places);
        foreach (string p in pList)
        {
            string qryPlaces = getDistinctPlacesQry(p);

            if (qryPlaces == "")
            {
                qryPlaces = "''";
            }
            DataSet dsOsExDis = Exp.getOsExDistance(sfcode, qryPlaces, osFrmCode);
            if (dsOsExDis.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsOsExDis.Tables[0].Rows)
                {
                    osDistance = osDistance + Convert.ToInt32(row["distance_in_kms"].ToString());
                }
            }
        }
        return osDistance;
    }

    private void getOsDistAndFrmCode(Distance_calculation Exp, string places, out int osDistance, out string osFrmCode)
    {

        string osPlace = getOSPlace(places);

        DataSet dsDis = null;
        string qryPlaces = getDistinctPlacesQry(places);
        dsDis = Exp.getOsDistance(sfcode, osPlace);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }

    }
    private void getSingleOsDistAndFrmCode(Distance_calculation Exp, string places, out int osDistance, out string osFrmCode)
    {
        DataSet dsDis = null;
        string qryPlaces = getSingleOsDistinctPlacesQry(places);
        dsDis = Exp.getSingleOsDistance(sfcode, qryPlaces);
        osDistance = 0;
        osFrmCode = "";

        if (dsDis.Tables[0].Rows.Count > 0)
        {
            osFrmCode = dsDis.Tables[0].Rows[0]["Territory_Code"].ToString();
            osDistance = Convert.ToInt32(dsDis.Tables[0].Rows[0]["distance_in_kms"].ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        saveData("1");
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {

        saveData("0");
    }
    private void saveData(string flag)
    {
        isSavedPage = true;
        Dictionary<String, String> values = new Dictionary<String, String>();
        values["sf_code"] = sfcode;
        values["div_code"] = divcode;
        values["month"] = monthId.SelectedValue.ToString();
        values["year"] = yearID.SelectedValue.ToString();
        values["period"] = "";
        values["flag"] = flag;
        values["s_date"] = "2016-01-03 20:43:47.643";

        int iReturn = -1;
        Distance_calculation dist = new Distance_calculation();
        dist.addHeadRecord(values);

        Dictionary<int, Dictionary<String, String>> valueList = new Dictionary<int, Dictionary<String, String>>();
        int count = 0;
        foreach (GridViewRow gridRow in grdExpMain.Rows)
        {

            values = new Dictionary<String, String>();
            values["sf_code"] = sfcode;
            values["div_code"] = divcode;
            values["month"] = monthId.SelectedValue.ToString();
            values["year"] = yearID.SelectedValue.ToString();

            Label lbl_aDate = (Label)gridRow.FindControl("lbl_ADate");
            string date = lbl_aDate.Text;
            Label lblDayName = (Label)gridRow.FindControl("lblDayName");
            Label lblWorkType = (Label)gridRow.FindControl("lblWorkType");
            Label lblTerrName = (Label)gridRow.FindControl("lblTerrName");
            Label lblCat = (Label)gridRow.FindControl("lblCat");

            HiddenField adateHidden = (HiddenField)gridRow.FindControl("adateHidden");
                HiddenField allowTypeHidden = (HiddenField)gridRow.FindControl("allowTypeHidden");
            HiddenField allowTypeHidden1 = (HiddenField)gridRow.FindControl("allowTypeHidden1");
            Label lblAllw = (Label)gridRow.FindControl("lblAllw");
            HiddenField allowHidden = (HiddenField)gridRow.FindControl("allowHidden");
            Label lblFrom = (Label)gridRow.FindControl("lblFrom");
            HiddenField fromHidden = (HiddenField)gridRow.FindControl("fromHidden");
            Label lblTo = (Label)gridRow.FindControl("lblTo");
            HiddenField toHidden = (HiddenField)gridRow.FindControl("toHidden");
            Label lblDistance = (Label)gridRow.FindControl("lblDistance");
            Label lblFare = (Label)gridRow.FindControl("lblFare");
            HiddenField fareHidden = (HiddenField)gridRow.FindControl("fareHidden");
            Label lblTotal = (Label)gridRow.FindControl("lblTotal");
            HiddenField totHidden = (HiddenField)gridRow.FindControl("totHidden");
            HiddenField distHidden = (HiddenField)gridRow.FindControl("distHidden");
            values["dayName"] = lblDayName.Text;
            values["adate"] = date;
            values["adate1"] = adateHidden.Value;
            values["dayName"] = lblDayName.Text;
            values["workType"] = lblWorkType.Text;
            values["terrName"] = lblTerrName.Text;
            values["cat"] = allowTypeHidden.Value;
            values["allowance"] = allowHidden.Value;
            values["distance"] = distHidden.Value;
            values["fare"] = fareHidden.Value;
            values["total"] = totHidden.Value;
            values["from"] = fromHidden.Value;
            values["to"] = toHidden.Value;
            values["catTemp"] = allowTypeHidden1.Value;
            if (date != "")
            {
                valueList[count] = values;
                count = count + 1;
                //iReturn = dist.addDetailRecord(values);
            }
        }
        iReturn = dist.addAllDetailRecord(valueList);
        dist.deleteFixed(values);

        foreach (GridViewRow gridRow in otherExpGrid.Rows)
        {

            HiddenField Expense_Parameter_Code = (HiddenField)gridRow.FindControl("hdnSexpName");
            Label amnt = (Label)gridRow.FindControl("lblSexpAmnt");
            values["Amt"] = amnt.Text;
            values["Expense_Parameter_Code"] = Expense_Parameter_Code.Value;

            iReturn = dist.addFixedRecord(values);

        }
        HiddenField otherExpValues = (HiddenField)FindControl("otherExpValues");
        dist.deleteOtheExp(sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        string[] splitVal = otherExpValues.Value.Split('~');

        string[] rms = splitVal[0].Split(',');
        string[] amount = splitVal[1].Split(',');
        string[] exp = splitVal[2].Split(',');
        for (int p = 0; p < exp.Length; p++)
        {
            isEmpty = false;
            string[] e = exp[p].Split('=');
            iReturn = dist.addOthExpRecord(e[0], e[1], amount[p], rms[p], sfcode, monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString());
        }

        if (iReturn > 0)
        {
            Response.Redirect("RptAutoExpense.aspx");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");

        }
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        if (isSavedPage)
        {
            Distance_calculation dist = new Distance_calculation();
            if (!isEmpty)
                generateOtherExpControls(dist);
        }
    }

    private void generateOtherExpControls(Distance_calculation dist)
    {
        DataTable t2 = dist.getSavedOtheExpRecord(monthId.SelectedValue.ToString(), yearID.SelectedValue.ToString(), sfcode);
        HtmlTable htmlTable = (HtmlTable)FindControl("tableId");
        DataTable otherExp1 = dist.getOthrExp(divcode);

        for (int p = htmlTable.Rows.Count - 1; p > 0; p--)
        {
            if (t2.Rows.Count > 0)
                htmlTable.Rows.RemoveAt(p);
            else
            {
                generateOtherExpListData(otherExp1);
            }

        }
        for (int i = 0; i < t2.Rows.Count; i++)
        {

            HtmlTableRow r = new HtmlTableRow();
            DropDownList d = new DropDownList();
            d.ID = "otherExp_" + i;
            d.CssClass = "otherExp";


            if (otherExp1.Rows.Count > 0)
            {
                foreach (DataRow row in otherExp1.Rows)
                {
                    ListItem list = new ListItem();
                    list.Text = row["Expense_Parameter_Name"].ToString();
                    list.Value = row["Expense_Parameter_Code"].ToString() + "##" + row["Fixed_Amount"].ToString();
                    d.Items.Add(list);
                }
                d.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            string amnt = t2.Rows[i]["amt"].ToString();
            otherExpAmnttot = otherExpAmnttot + Convert.ToInt32(amnt);
            string rm = t2.Rows[i]["remarks"].ToString();
            d.Text = t2.Rows[i]["Paritulars"].ToString();
            d.Items.FindByText(t2.Rows[i]["Paritulars"].ToString()).Selected = true;
            HtmlTableCell cell1 = new HtmlTableCell();
            cell1.Controls.Add(d);
            r.Cells.Add(cell1);

            HtmlTableCell cell2 = new HtmlTableCell();
            TextBox box = new TextBox();
            Literal lit = new Literal();
            lit.Text = @"<input type='text' class='textbox' name='OthExpVal' id='OthExpVal_" + i + "' runat='server' value='" + amnt + "' size='6' maxlength=4 onkeyup='OthExpCalc(this)'/>";
            HtmlTable table = new HtmlTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder("");
            System.IO.StringWriter tw = new System.IO.StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            box.RenderControl(hw);


            cell2.Controls.Add(lit);
            r.Cells.Add(cell2);
            HtmlTableCell cell3 = new HtmlTableCell();
            TextBox box1 = new TextBox();
            lit = new Literal();
            lit.Text = @"<input type='text' class='textbox' name='OthExpRmk' id='OthExpRmk_" + i + "' runat='server' value='" + rm + "' size='50' onkeyup='OthExpCalc(this)'/>";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            box1.RenderControl(hw);

            cell3.Controls.Add(lit);
            r.Cells.Add(cell3);
            HtmlTableCell cell4 = new HtmlTableCell();
            Button b1 = new Button();
            lit = new Literal();
            lit.Text = @"<input type='button' id='btnadd' value=' + ' class='btnSave' onclick='_AdRowByCurrElem(this)' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b1.RenderControl(hw);
            cell4.Controls.Add(lit);
            r.Cells.Add(cell4);
            HtmlTableCell cell5 = new HtmlTableCell();
            Button b2 = new Button();
            lit = new Literal();
            lit.Text = @"<input type='button' id='btndel' value=' - ' class='btnSave' onclick='DRForOthExp(this,this.parentNode.parentNode,1)' />";
            sb = new System.Text.StringBuilder("");
            tw = new System.IO.StringWriter(sb);
            hw = new HtmlTextWriter(tw);
            b2.RenderControl(hw);

            cell5.Controls.Add(lit);
            r.Cells.Add(cell5);

            htmlTable.Rows.Add(r);
        }
    }


}