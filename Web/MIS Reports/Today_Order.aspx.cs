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
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using DBase_EReport;
using Newtonsoft.Json;

public partial class MIS_Reports_Today_Order : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    DataSet dsdiv = new DataSet();
    DataSet dsTP = null;
    DataSet dsDivision = null;
    string strMultiDiv = string.Empty;
    public static string div_code = string.Empty;
    public static string sf_code = string.Empty;
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
            }
        }

        else if (Session["sf_type"].ToString() == "2")
        {
            ViewState["sf_type"] = "";
            if (!Page.IsPostBack)
            {
                Filldis();
            }

        }
        else if (Session["sf_type"].ToString() == "3")
        {
            ViewState["sf_type"] = "admin";
            if (!Page.IsPostBack)
            {
                Filldis();

            }

            if (Session["div_code"] != null)
            {
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

        dsGV = dc.view_total_order_view1(div_code, ddl_dis.SelectedItem.Value, Date);

        dsGc1 = dc.view_total_order_view2(div_code, ddl_dis.SelectedItem.Value, Date);

        if (dsGV.Tables[0].Rows.Count > 0)
        {
            Repeater1.DataSource = dsGV;
            Repeater1.DataBind();

        }
        else
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
			ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Order Data...');</script>");
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
            string ordno = (e.Item.FindControl("hforderno") as HiddenField).Value;
            string Flag = (e.Item.FindControl("HiddenField2") as HiddenField).Value;

            if (Flag == "0" || Flag == "")
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                im.Visible = false;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("Image2") as System.Web.UI.WebControls.Image;
                img.Visible = false;
				System.Web.UI.WebControls.Image imgg = e.Item.FindControl("Image3") as System.Web.UI.WebControls.Image;
                imgg.Visible = false;

            }
            else if(Flag == "2")
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image2") as System.Web.UI.WebControls.Image;
                im.Visible = true;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                img.Visible = false;
                System.Web.UI.WebControls.Panel pnl = e.Item.FindControl("hide") as System.Web.UI.WebControls.Panel;
                pnl.Visible = false;
				System.Web.UI.WebControls.Image imgg = e.Item.FindControl("Image3") as System.Web.UI.WebControls.Image;
                imgg.Visible = false;
            }
            else if(Flag == "1")
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                im.Visible = true;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("Image2") as System.Web.UI.WebControls.Image;
                img.Visible = false;
                System.Web.UI.WebControls.Panel pnl = e.Item.FindControl("hide") as System.Web.UI.WebControls.Panel;
                pnl.Visible = false;
				System.Web.UI.WebControls.Image imgg = e.Item.FindControl("Image3") as System.Web.UI.WebControls.Image;
                imgg.Visible = false;
				System.Web.UI.WebControls.Panel imgpanel = e.Item.FindControl("pnlOrders") as System.Web.UI.WebControls.Panel;
                var imageInput = imgpanel.FindControl("btnDelete") as HtmlInputImage;
                imageInput.Visible = false;
            }
			else
            {
                System.Web.UI.WebControls.Image im = e.Item.FindControl("Image1") as System.Web.UI.WebControls.Image;
                im.Visible = false;
                System.Web.UI.WebControls.Image img = e.Item.FindControl("Image2") as System.Web.UI.WebControls.Image;
                img.Visible = false;
                System.Web.UI.WebControls.Panel pnl = e.Item.FindControl("hide") as System.Web.UI.WebControls.Panel;
                pnl.Visible = false;
                System.Web.UI.WebControls.Image imgg = e.Item.FindControl("Image3") as System.Web.UI.WebControls.Image;
                imgg.Visible = true;
            }

            Repeater rptOrders = e.Item.FindControl("rptOrders") as Repeater;

            DataTable ds12 = new DataTable();
            ds12 = GetData(string.Format("select Cust_Code,Product_Code,Product_Name,Quantity,sum(Quantity * net_weight) net_weight,value,a.Order_Flag,a.discount,a.free,b.trans_sl_no from  Trans_Order_Details a inner join Trans_Order_Head b on " +
             "a.Trans_sl_no =b.Trans_Sl_No where CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + Date + "' , 101) AS DATETIME) " +
             "and STOCKIST_CODE='" + ddl_dis.SelectedItem.Value + "' and b.trans_sl_no='"+ ordno + "' and Cust_Code='{0}' group by Cust_Code,Product_Code,Product_Name,Quantity,value,a.Order_Flag,a.discount,a.free,b.trans_sl_no", customerId));

            rptOrders.DataSource = ds12;
            rptOrders.DataBind();
			 for (int i = 0; i < ds12.Rows.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)rptOrders.Items[i].FindControl("chkRow");
                if (Convert.ToString(ds12.Rows[i].ItemArray[6]) == "2" || Convert.ToString(ds12.Rows[i].ItemArray[6])=="1")
                {
                    chk.Enabled = false;
                }
            }

            Session["Date"] = Date;
        }

    }
    [WebMethod]
    public static string savedata(string data, string prod)
    {
        MIS_Reports_Today_Order ms = new MIS_Reports_Today_Order();
        return ms.save(data, prod);
    }
    public class svorders
    {
        public string flag { get; set; }
        public string custid { get; set; }
        public string prods { get; set; }
        public string ordno { get; set; }
    }

        private string save(string data,string prod)
    {
        var items = JsonConvert.DeserializeObject<List<svorders>>(data);

        string[] str = prod.Split(',');
        string pro = "'"; 
        divcode = Convert.ToString(Session["div_code"]);
        string date = Session["Date"].ToString();
        string Flag = Convert.ToString(items[0].flag);
        string cus = Convert.ToString(items[0].custid);
        for(int i=0;i<str.Length;i++)
        {
            pro += str[i] + "','";
        }
        pro += "'";
		string orno = Convert.ToString(items[0].ordno);
        if (div_code != "" && divcode != null)
        {
            //update
            DCR dc = new DCR();

loc dc1 = new loc();
            int iReturn = dc1.order_Confirm(Flag, date, cus, pro,orno);
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

loc dc1 = new loc();
            int iReturn = dc1.order_Confirm(Flag, date, cus, pro, orno);
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
    public static string saveData1(string data,string prod)
    {
        MIS_Reports_Today_Order ms = new MIS_Reports_Today_Order();
        return ms.save1(data,prod);
    }

    private string save1(string data,string prod)
    {
		string[] str1 = prod.Split(',');
        string pro = "'";
		string[] str = data.Split(',');
        divcode = Convert.ToString(Session["div_code"]);
        string date = Session["Date"].ToString();
        string remark = Convert.ToString(str[0]);
        string cus_code = Convert.ToString(str[1]);
        string ord_no= Convert.ToString(str[2]);
		for (int i = 0; i < str1.Length; i++)
        {
            pro += str1[i] + "','";
        }
        pro += "'";

        if (remark != "" && remark != null)
        {
            //update
            DCR dc = new DCR();
            loc dc1 = new loc();

            int iReturn = dc1.order_Cancel(remark, date, cus_code, ord_no,pro);
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
            loc dc1 = new loc();

            int iReturn = dc1.order_Cancel(remark, date, cus_code, ord_no,pro);
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
        MIS_Reports_Today_Order ms = new MIS_Reports_Today_Order();
        return ms.save2(data, orno, dd);
    }

    private string save2(string data, string orno, string dd)
    {

        string date = "";
        int iReturn;
            divcode = Convert.ToString(Session["div_code"]);
            date = Session["Date"].ToString();
            if (div_code != "" && divcode != null)
            {
                //update
                loc dc = new loc();
                iReturn = dc.order_Tranfer(dd, date, data, orno);
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
                loc dc = new loc();
                iReturn = dc.order_Tranfer(dd, date, data, orno);
                if (iReturn > 0)
                {
                    return "Sucess";
                }
                else
                {
                    return "Error";
                }
            }
        return "Sucess";

    }

    //delete
    [WebMethod]
    public static string savedel(string data, string orno, string dd)
    {
        MIS_Reports_Today_Order ms = new MIS_Reports_Today_Order();
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
                loc  dc = new loc ();


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
                loc  dc = new loc ();


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
        MIS_Reports_Today_Order ms = new MIS_Reports_Today_Order();
        result = ms.loaddata();
        return result;

    }

    private List<string> loaddata()
    {

        List<string> result = new List<string>();
        divcode = Convert.ToString(Session["div_code"]);

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


            //DropDownList1.DataTextField = "Stockist_Name";
            //DropDownList1.DataValueField = "Stockist_code";
            //DropDownList1.DataSource = dsSalesForce;
            //DropDownList1.DataBind();

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


            //DropDownList1.DataTextField = "Stockist_Name";
            //DropDownList1.DataValueField = "Stockist_code";
            //DropDownList1.DataSource = dsSalesForce;
            //DropDownList1.DataBind();

            //Select1.DataTextField = "Stockist_Name";
            //Select1.DataValueField = "Stockist_code";
            //Select1.DataSource = dsSalesForce;
            //Select1.DataBind();

        }
    }
	[WebMethod]
    public static string filldropdown()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.GetStockName_Cus1(div_code, sf_code);
        return JsonConvert.SerializeObject(dsSalesForce.Tables[0]);
    }
	
	public class loc
    {
		public int order_Tranfer(string stk, string date, string cus, string ord)
        {
            int iReturn = 1;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "UPDATE Trans_Order_Head SET Stockist_Code='" + stk + "' FROM Trans_Order_Details AS Trans_Order_Details " +
                         "INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE  Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Head.Trans_Sl_No='"+ ord + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
		
       public int order_Delete(string Flag, string date, string cus, string ord)
        {
            int iReturn = 1;//DCR
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "delete Trans_Order_Details FROM Trans_Order_Details AS Trans_Order_Details " +
                         "INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE  Trans_Order_Head.Cust_Code='" + ord + "' and Product_Code='" + Flag + "' and Trans_Order_Details.Trans_sl_no='"+ cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME)";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
        }
        public int order_Cancel(string Flag, string date, string cus,string ordno,string pro)
        {
            int iReturn = 1;
            DataSet ds = null;
            try
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "update Trans_Order_Head set Remarks='" + Flag + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='"+ ordno + "' ";
                iReturn = db.ExecQry(strQry);
                string strQry1 = "UPDATE Trans_Order_Details SET Order_Flag='2' FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Details.Product_Code in (" + pro + ") and Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + ordno + "'";
                iReturn = db.ExecQry(strQry1);
                string strQry2 = "select count(*)cnt FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + ordno + "' and Trans_Order_Details.Order_Flag not in ('2','1') ";
                ds=db.Exec_DataSet(strQry2);
                if(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0])==0 )
                {
                    strQry2 = "select count(distinct Trans_Order_Details.Order_Flag)cnt FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + ordno + "' ";
                    ds = db.Exec_DataSet(strQry2);
                    if(Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]) > 1)
                    {
                        strQry = "update Trans_Order_Head set Order_Flag='3',Remarks='" + Flag + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='" + ordno + "' ";
                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = "update Trans_Order_Head set Order_Flag='2',Remarks='" + Flag + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='" + ordno + "' ";
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
        public int order_Confirm(string Flag, string date, string cus, string pro,string orno)
        {
            int iReturn = 1;
            DataSet ds = null;

            try
            {

                DB_EReporting db = new DB_EReporting();
                string remark = "";

                string strQry = "update Trans_Order_Head set Remarks='" + remark + "'where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='"+ orno + "'";
                iReturn = db.ExecQry(strQry);
                string strQry1 = "UPDATE Trans_Order_Details SET Order_Flag='" + Flag + "' FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Details.Product_Code in (" + pro + ") and Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + orno + "'";
                iReturn = db.ExecQry(strQry1);
                string strQry2 = "select count(*)cnt FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + orno + "' and Trans_Order_Details.Order_Flag not in ('2','1') ";
                ds = db.Exec_DataSet(strQry2);
                if (Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0])==0)
                {
                    strQry2 = "select count(distinct Trans_Order_Details.Order_Flag)cnt FROM Trans_Order_Details AS Trans_Order_Details INNER JOIN Trans_Order_Head AS Trans_Order_Head ON Trans_Order_Details.Trans_sl_no = Trans_Order_Head.Trans_Sl_No " +
                         "WHERE Trans_Order_Head.Cust_Code='" + cus + "' and Trans_Order_Details.Trans_Sl_No='" + orno + "' ";
                    ds = db.Exec_DataSet(strQry2);
                    if (Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]) > 1)
                    {
                        strQry = "update Trans_Order_Head set Order_Flag='3',Remarks='" + remark + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='" + orno + "' ";
                        iReturn = db.ExecQry(strQry);
                    }
                    else
                    {
                        strQry = "update Trans_Order_Head set Order_Flag='1',Remarks='" + remark + "' where  Cust_Code='" + cus + "' and CAST(CONVERT(VARCHAR, Order_date, 101) AS DATETIME) =CAST(CONVERT(VARCHAR, '" + date + "' , 101) AS DATETIME) and Trans_Sl_No='" + orno + "' ";
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
    }


}

