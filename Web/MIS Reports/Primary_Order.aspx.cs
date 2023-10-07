using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.Web.Services;

public partial class MIS_Reports_Primary_Order : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string sub_div = string.Empty;
    string sfCode = string.Empty;
    string sfname = string.Empty;
    string divcode = string.Empty;
    string Date = string.Empty;
    decimal subTotal = 0;
    decimal nettotal = 0;
    decimal nttotal = 0;
    decimal total = 0;
    string StartDate = string.Empty;
    int subTotalRowIndex = 0;
    int currentId = 0;
    DataSet accessMas = null;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            this.MasterPageFile = "~/Master.master";
        }
        else if (sf_type == "2")
        {
            this.MasterPageFile = "~/Master_MGR.master";
        }
        else if (sf_type == "1")
        {
            this.MasterPageFile = "~/Master_MR.master";
        }
        else if (sf_type == "4")
        {
            this.MasterPageFile = "~/Master_DIS.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //Session["Date"] = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");
        if (sf_type == "4")
        {

            try
            {

                div_code = Session["Division_Code"].ToString().Replace(",", "");

                sfCode = Session["Sf_Code"].ToString();

                Date = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");
            }
            catch (Exception)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select Date!!!');</script>");

            }

            //FillSF();


        }
        else
        {
            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
            sf_type = Session["sf_type"].ToString();
            Date = Convert.ToDateTime(Session["Date"]).ToString("dd-MM-yyyy");


        }


        if (Session["sf_type"].ToString() == "1")
        {

            ViewState["sf_type"] = "";
            SalesForce sf = new SalesForce();
            dsSf = sf.getReportingTo(sf_code);
            if (dsSf.Tables[0].Rows.Count > 0)
            {
                sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            }
            if (!Page.IsPostBack)
            {
                //FillMRManagers();
            }
            //ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            //ddlFieldForce.Enabled = false;
            //lblDivision.Visible = false;
            //ddlDivision.Visible = false;
            //chkVacant.Visible = false;
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            ViewState["sf_type"] = "";
            //SalesForce sf = new SalesForce();
            //dsSf = sf.getReportingTo(sf_code);
            //if (dsSf.Tables[0].Rows.Count > 0)
            //{
            //    sf_code = dsSf.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            //}
            if (!Page.IsPostBack)
            {
                Filldis();
            }
            //ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            //ddlFieldForce.Enabled = false;
            //lblDivision.Visible = false;
            //ddlDivision.Visible = false;
            //chkVacant.Visible = false;

        }
        else if (Session["sf_type"].ToString() == "3")
        {
            ViewState["sf_type"] = "admin";
            //Request.Form["txtFrom"] = Session["Date"].ToString();
            //UserControl_MenuUserControl c1 =
            //(UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            //Divid.Controls.Add(c1);
            //c1.FindControl("btnBack").Visible = false;
            //c1.Title = this.Page.Title;
            if (!Page.IsPostBack)
            {
                Filldis();
                Fillsupp();
                //Request.Form["txtFrom"] = Session["Date"].ToString();
                //chkVacant.Visible = true;

            }

            if (Session["div_code"] != null)
            {
                //lblDivision.Visible = false;
                //ddlDivision.Visible = false;
            }
        }



    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Date = Convert.ToDateTime(Request.Form["txtFrom"]).ToString("yyyy-MM-dd");
        FillSF();
    }



    private void FillSF()
    {

        decimal subTotal = 0;
        decimal nettotal = 0;
        decimal nttotal = 0;
        decimal total = 0;

        string sURL = string.Empty;
        string stURL = string.Empty;


        string stCrtDtaPnt = string.Empty;

        DataSet dsGV = new DataSet();
        DataSet dsGc1 = new DataSet();
        DCR dc = new DCR();

        //ClientScript.RegisterStartupScript(GetType(), "TestAlert", "alert(\'hi\');", true);

        dsGV = dc.view_Primary_order_view(div_code, ddl_dis.SelectedItem.Value, Date);

        //dsGc1 = dc.view_total_order_view2(div_code, ddl_dis.SelectedItem.Value, Date);

        if (dsGV.Tables[0].Rows.Count > 0)
        {
            //dsGV.Relations.Add(new DataRelation("nestThem", dsGV.Tables[0].Columns["Trans_sl_no"], dsGV.Tables[0].Columns["SF_Code"]));
            Repeater1.DataSource = dsGV;
            Repeater1.DataBind();

        }
        else
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
        }



    }
    protected void OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        subTotal = 0;
        nettotal = 0;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dt = (e.Row.DataItem as DataRowView).DataView.Table;
            int orderId = Convert.ToInt32(dt.Rows[e.Row.RowIndex]["Stockist_Code"]);
            total += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["Order_value"]);
            nttotal += Convert.ToDecimal(dt.Rows[e.Row.RowIndex]["net_weight_value"]);
            //Response.Write(gvtotalorder.Rows[0].Cells[2].Text);
            if (orderId != currentId)
            {
                if (e.Row.RowIndex > 0)
                {
                    for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                    {


                    }
                    this.AddTotalRow("Sub Total", nettotal.ToString("N2"), subTotal.ToString("N2"));
                    subTotalRowIndex = e.Row.RowIndex;
                }
                currentId = orderId;
            }
        }
    }
    private void AddTotalRow(string labelText, string netvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#dbf7d9");
        row.CssClass = "subTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=4},
                                          new TableCell { Text = netvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //gvtotalorder.Controls[0].Controls.Add(row);
    }
    private void AddTotalRoww(string labelText, string ntvalue, string value)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.BackColor = ColorTranslator.FromHtml("#ecf19f");

        row.CssClass = "GrndTot";
        row.Cells.AddRange(new TableCell[4] { new TableCell (), //Empty Cell
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,ColumnSpan=4},
                                          new TableCell { Text = ntvalue, HorizontalAlign = HorizontalAlign.Right },
                                        new TableCell { Text = value, HorizontalAlign = HorizontalAlign.Right } });

        //gvtotalorder.Controls[0].Controls.Add(row);
    }


    private static DataTable GetData(string query)
    {
        string constr = Globals.ConnString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataSet ds = new DataSet())
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {

            string customerId = (e.Item.FindControl("hfCustomerId") as HiddenField).Value;
            string Flag = (e.Item.FindControl("HiddenField2") as HiddenField).Value;

            //System.Web.UI.WebControls.Image img = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
            if (Flag == "0" || Flag == "")
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                im.Visible = false;

            }
            else
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                im.Visible = true;
                System.Web.UI.WebControls.Panel pnl = e.Item.FindControl("hide") as System.Web.UI.WebControls.Panel;
                pnl.Visible = false;



            }
            Repeater rptOrders = e.Item.FindControl("rptOrders") as Repeater;
            rptOrders.DataSource = GetData(string.Format("select Stockist_Code,Product_Code,Product_Name,CQty,PQty,value,b.Order_Flag,b.Collected_Amount from  Trans_PriOrder_Details a inner join Trans_PriOrder_Head b on " +
             "a.Trans_Sl_No =b.Trans_Sl_No where CAST(CONVERT(VARCHAR, b.Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + Date + "' , 101) AS DATETIME) " +
             "and Stockist_Code='{0}' group by Stockist_Code,Product_Code,Product_Name,CQty,PQty,value,b.Order_Flag,b.Collected_Amount", customerId));
            rptOrders.DataBind();

            Session["Date"] = Date;
        }

    }
    [WebMethod]
    public static string savedata(string data, string dd, string ddN, string order_no)
    {
        MIS_Reports_Primary_Order ms = new MIS_Reports_Primary_Order();
        return ms.save(data, dd, ddN,order_no);
    }

    private string save(string data, string dd, string ddN, string order_no)
    {

        string[] str = data.Split(',');

        divcode = Convert.ToString(Session["div_code"]);
        string date = Session["Date"].ToString();
        string Flag = Convert.ToString(str[0]);
        string cus = Convert.ToString(str[1]);

        string supp = dd.ToString().Trim();
        string supp_name = ddN.ToString().Trim();
        string orde_no = order_no.ToString().Trim();
        //string pro1 = Convert.ToString(str[3]);
        if (div_code != "" && divcode != null)
        {
            //update
            DCR dc = new DCR();


            int iReturn = dc.Primary_order_Confirm(Flag, date, cus, supp, supp_name, orde_no);
            if (iReturn > 0)
            {

                return "Sucess";
            }
            else
            {
                return "Error";
            }

        }
        else
        {
            DCR dc = new DCR();


            int iReturn = dc.Primary_order_Confirm(Flag, date, cus, supp, supp_name, orde_no);
            if (iReturn > 0)
            {

                return "Sucess";

            }
            else
            {
                return "Error";
            }

        }


    }
    [WebMethod]
    public static string saveData1(string data)
    {
        MIS_Reports_Primary_Order ms = new MIS_Reports_Primary_Order();
        return ms.save1(data);
    }

    private string save1(string data)
    {


        string[] str = data.Split(',');
        divcode = Convert.ToString(Session["div_code"]);
        string date = Session["Date"].ToString();
        string remark = Convert.ToString(str[0]);
        string cus_code = Convert.ToString(str[1]);

        if (remark != "" && remark != null)
        {
            //update
            DCR dc = new DCR();


            int iReturn = dc.order_Cancel(remark, date, cus_code);
            if (iReturn > 0)
            {

                return "Sucess";
            }
            else
            {
                return "Error";
            }

        }
        else
        {
            DCR dc = new DCR();


            int iReturn = dc.order_Cancel(remark, date, cus_code);
            if (iReturn > 0)
            {

                return "Sucess";
            }
            else
            {
                return "Error";
            }

        }


    }
    //tranfer
    [WebMethod]
    public static string savedata2(string data, string orno, string dd)
    {
        MIS_Reports_Primary_Order ms = new MIS_Reports_Primary_Order();
        return ms.save2(data, orno, dd);
    }

    private string save2(string data, string orno, string dd)
    {

        string[] str = data.Split(',');
        string[] d = dd.Split(',');
        string date = "";
        string flag = "";
        string pro = "";
        int iReturn;
        foreach (string s in str)
        {
            divcode = Convert.ToString(Session["div_code"]);
            date = Session["Date"].ToString();
            flag = s.ToString().Trim();
            pro = Convert.ToString(d[0].Trim());
            string pro1 = orno.ToString().Trim();

            if (div_code != "" && divcode != null)
            {
                //update
                DCR dc = new DCR();


                iReturn = dc.order_Tranfer(flag, date, pro, pro1);
                if (iReturn > 0)
                {

                    return "Sucess";
                }
                else
                {
                    return "Error";
                }

            }
            else
            {
                DCR dc = new DCR();


                iReturn = dc.order_Tranfer(flag, date, pro, pro1);
                if (iReturn > 0)
                {

                    return "Sucess";

                }
                else
                {
                    return "Error";
                }

            }
        }
        return "Sucess";

    }

    //delete
    [WebMethod]
    public static string savedel(string data, string orno, string dd)
    {
        MIS_Reports_Primary_Order ms = new MIS_Reports_Primary_Order();
        return ms.savedel1(data, orno, dd);
    }

    private string savedel1(string data, string orno, string dd)
    {

        string[] str = data.Split(',');
        string[] d = dd.Split(',');
        string date = "";
        string flag = "";
        string pro = "";
        int iReturn;
        foreach (string s in str)
        {
            divcode = Convert.ToString(Session["div_code"]);
            date = Session["Date"].ToString();
            flag = s.ToString().Trim();
            pro = Convert.ToString(d[0].Trim());
            string pro1 = orno.ToString().Trim();

            if (div_code != "" && divcode != null)
            {
                //update
                DCR dc = new DCR();


                iReturn = dc.order_Delete(flag, date, pro, pro1);
                if (iReturn > 0)
                {

                    return "Sucess";
                }
                else
                {
                    return "Error";
                }

            }
            else
            {
                DCR dc = new DCR();


                iReturn = dc.order_Delete(flag, date, pro, pro1);
                if (iReturn > 0)
                {

                    return "Sucess";

                }
                else
                {
                    return "Error";
                }

            }
        }
        return "Sucess";

    }

    [WebMethod]
    public static List<string> getdata()
    {
        List<string> result = new List<string>();
        MIS_Reports_Primary_Order ms = new MIS_Reports_Primary_Order();
        result = ms.loaddata();
        return result;

    }

    private List<string> loaddata()
    {

        List<string> result = new List<string>();
        divcode = Convert.ToString(Session["div_code"]);
        //string date = Session["Date"].ToString();

        if (divcode != "" && divcode != null)
        {



            DCR dc = new DCR();
            accessMas = dc.order_img(divcode);


            foreach (DataRow row in accessMas.Tables[0].Rows)
            {
                result.Add(String.Format("{0}", row["Order_Flag"]));
            }

        }
        return result;

    }

    protected void ddl_dis_SelectedIndexChanged(object sender, EventArgs e)
    {

        Filldis();


    }

    private void Filldis_mgr()
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.GetStockist_subdivisionwise(div_code, sub_div, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddl_dis.DataTextField = "Stockist_Name";
            ddl_dis.DataValueField = "Stockist_code";
            ddl_dis.DataSource = dsSalesForce;
            ddl_dis.DataBind();


            DropDownList1.DataTextField = "Stockist_Name";
            DropDownList1.DataValueField = "Stockist_code";
            DropDownList1.DataSource = dsSalesForce;
            DropDownList1.DataBind();
            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

        }
    }

    private void Fillsupp()
    {

        SalesForce sf = new SalesForce();

        dsSalesForce = sf.GetSupplier(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {


            Select1.DataTextField = "S_Name";
            Select1.DataValueField = "S_No";
            Select1.DataSource = dsSalesForce;
            Select1.DataBind();
            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

        }
 
    }

    private void Filldis()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.GetStockName_Cus1(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddl_dis.DataTextField = "Stockist_Name";
            ddl_dis.DataValueField = "Stockist_code";
            ddl_dis.DataSource = dsSalesForce;
            ddl_dis.DataBind();


            DropDownList1.DataTextField = "Stockist_Name";
            DropDownList1.DataValueField = "Stockist_code";
            DropDownList1.DataSource = dsSalesForce;
            DropDownList1.DataBind();

            //Select1.DataTextField = "Stockist_Name";
            //Select1.DataValueField = "Stockist_code";
            //Select1.DataSource = dsSalesForce;
            //Select1.DataBind();
            //ddlSF.DataTextField = "des_color";
            //ddlSF.DataValueField = "sf_code";
            //ddlSF.DataSource = dsSalesForce;
            //ddlSF.DataBind();

        }
    }


}

