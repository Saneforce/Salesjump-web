using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

public partial class MIS_Reports_Product_Tax_Allocation : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    Notice Addcomment = new Notice();
    DataSet dsState = new DataSet();
    DataSet dsState1 = new DataSet();
    DataSet stnam = new DataSet();
    string state_code = string.Empty;
    string sub_code = string.Empty;
    string sState = string.Empty;
    string sState1 = string.Empty;
    string statelist = string.Empty;
    DataSet dsDivision = null;
    DataSet dsDivision1 = null;
    string[] statecd;
    string[] statecd1;
    string state_cd = string.Empty;
    DataSet dsSub = null;
    int iIndex = -1;

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
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
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            submit1.Visible = false;
            Button1.Visible = false;
            BindGridd();
            submit1.Visible = true;
            Button1.Visible = true;
        }
    }

    protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public class  GInsVal
    {
       public string PCode{get;set;}
       public string taxcode{get;set;}
       public string stnm{get;set;}       
    }

    protected void BindGridd()
    {

        ntc gg = new ntc();
        DataSet ff = new DataSet();

        ff = gg.Getproductstatewise1(div_code);
        if (ff.Tables.Count > 0)
        {
            if (ff.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ff;
                GridView1.DataBind();
            }
        }
    }

    //    [WebMethod]
    //public static string getstatenm(string prodname)
    //{
    //    ntc gg = new ntc();
    //    DataSet ff1 = new DataSet();
    //    ff1 = gg.getstatenam(prodname);
    //    return JsonConvert.SerializeObject(ff1.Tables[0]);
    //}
    [WebMethod]
    public static string deletedata(string prdouct_cd)
    {
        string msg = "";
        string divcode = null;

        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                divcode = HttpContext.Current.Session["div_code"].ToString();

            }
        }
        if (prdouct_cd != "")
        {

            SqlConnection con = new SqlConnection(Globals.ConnString);
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from Mas_StateProduct_TaxDetails  where Division_Code='" + divcode + "' and Product_Code='" + prdouct_cd + "' ", con);

            int i = cmd.ExecuteNonQuery();
            if (i > 1)
            {
                msg = "true";
            }
            else
            {
                msg = "false";
            }
        }
        return msg;
    }
    [WebMethod]
    public static string insertdata(string Data)
    {

        string msg = "";
        //string Product_codee = Product_code.Trim();
        //string Tax_codee = Tax_code.Trim();
        //string State_Codee = State_Code.Trim();
        string divcode = null;

        DB_EReporting db_ER = new DB_EReporting();

        if (HttpContext.Current.Session["div_code"] != null)
        {
            if (HttpContext.Current.Session["div_code"].ToString() != "")
            {
                divcode = HttpContext.Current.Session["div_code"].ToString();

            }
        }

        var items = JsonConvert.DeserializeObject<List<GInsVal>>(Data);
        string sxml = "<ROOT>";
        for (int i = 0; i < items.Count; i++)
        {
            string[] stateList = items[i].stnm.Split(',');
            for (int j=0;j < stateList.Length && stateList[j]!=""; j++)
            {
                sxml += "<TXSV PCode=\"" + items[i].PCode + "\" TCode=\"" + items[i].taxcode + "\" SName=\"" + stateList[j] + "\" />";
            }
        }
        sxml += "</ROOT>";
        string strQry = "exec sp_save_tax '" + sxml + "','"+ divcode+"'";
        int result = 0;

        result = db_ER.ExecQry(strQry);
        return "Success";
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        ntc gg = new ntc();
        DataSet ff1 = new DataSet();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string taxid = "";
            string prdcd = "";
            string stcd = "";
            ff1 = gg.Gettaxdetails(div_code);
            if (ff1.Tables.Count > 0)
            {
                if (ff1.Tables[0].Rows.Count > 0)
                {

                    DropDownList DropDownList1 =(DropDownList)e.Row.FindControl("drptax_detail");
                    DropDownList1.DataSource = ff1;
                    DropDownList1.DataTextField = "Tax_Name";
                    DropDownList1.DataValueField = "Tax_Id";
                    HiddenField hdn = (HiddenField)e.Row.FindControl("HiddenFieYY1");
                    taxid = hdn.Value;
                    if (hdn.Value == "")
                    {
                        DropDownList1.SelectedValue = "0";
                    }
                    else
                    {
                        DropDownList1.SelectedValue = hdn.Value;

                    }
                    DropDownList1.DataBind();

                }
            }
            Division dv = new Division();
            dsDivision1 = dv.getStatePerDivision(div_code);
            if (dsDivision1.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                state_cd = string.Empty;
                sState1 = dsDivision1.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                statecd1 = sState1.Split(',');
                foreach (string st_cd in statecd1)
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
                dsState1 = st.getState_new(state_cd);
                CheckBoxList ListItem1 = (CheckBoxList)e.Row.FindControl("ddlState1");
                ListItem1.DataTextField = "statename";
                ListItem1.DataValueField = "state_code";
                HiddenField hdn1 = (HiddenField)e.Row.FindControl("HiddenField1");
                prdcd = hdn1.Value;
                ListItem1.DataSource = dsState1;
                HiddenField hdn2 = (HiddenField)e.Row.FindControl("HiddenFieYY1");
                taxid = hdn2.Value;
                stnam = gg.getstatenam(prdcd, taxid,div_code);
                ListItem1.DataBind();
                TextBox myTextBox = (TextBox)e.Row.FindControl("TextBox1");
                myTextBox.Text = "";
                for (int j = 0; j < stnam.Tables[0].Rows.Count; j++)
                {
                    ListItem1.Items.FindByText(Convert.ToString(stnam.Tables[0].Rows[j].ItemArray[4])).Selected = true;

                    myTextBox.Text += Convert.ToString(stnam.Tables[0].Rows[j].ItemArray[4]);
                    myTextBox.Text += ",";

                }
            }
        }

    }
    public class Employee
    {
        public string name { get; set; }
        public string code { get; set; }
    }

    protected void gridView_PreRender(object sender, EventArgs e)
    {
        //GridDecorator.MergeRows(GridView1);
    }

    public class ntc
    {
        public DataSet Getproductstatewise1(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "select Product_Detail_Code,Product_Detail_Name,p.Division_Code,Product_Active_Flag,p.State_Code,subdivision_code,Product_Code,tm.Tax_Id from Mas_PRoduct_detail  p  left  outer join Mas_StateProduct_TaxDetails t on t.Product_Code=P.Product_detail_Code left join Tax_Master tm on tm.Tax_Id=t.Tax_Id and tm.Division_Code='" + div_code + "' where p.pRODUCT_aCTIVE_FLAG=0  and  p.dIVISION_CODE='" + div_code + "' group by Product_Detail_Name,Product_Detail_Code,p.Division_Code,Product_Active_Flag,p.State_Code,subdivision_code,Product_Code,tm.Tax_Id  ";

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
        public DataSet getstatenam(string prodnm, string taxid,string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;
            string strQry = "select product_detail_code,product_detail_name,st.state_code,tax_id,StateName from mas_product_detail pd inner join mas_stateproduct_taxdetails st on product_code=product_detail_code inner join mas_state ms on ms.state_code = st.state_code inner join mas_division md on charindex(','+st.state_code+',',','+md.State_Code+',')>0 and md.Division_Code='"+div_code+"' where product_detail_code = '" + prodnm + "' and tax_id='" + taxid + "' group by product_detail_code,product_detail_name,st.state_code,tax_id,StateName ";
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
        public DataSet Gettaxdetails(string div_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "SELECT 0 as Tax_Id,'---Select---' as Tax_Name union all select Tax_Id,Tax_Name+'@'+convert(varchar,Value) Tax_Name  " +
                "from Tax_Master where Tax_Active_Flag=0 AND dIVISION_CODE='" + div_code + "' ";


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
        public DataSet Getproductstatewise1(string div_code, string state_code)
        {
            DB_EReporting db_ER = new DB_EReporting();

            DataSet dsDivision = null;

            string strQry = "select Product_Detail_Code,Product_Detail_Name,p.Division_Code,Product_Active_Flag,p.State_Code,subdivision_code,Product_Code,Tax_Id from Mas_PRoduct_detail  p  left  outer join Mas_StateProduct_TaxDetails t on t.Product_Code=P.Product_detail_Code and t.State_code='" + state_code + "'  where CHARINDEX( '," + state_code + ",', ',' +p.STATE_CODE + ',' ) > 0 and  p.pRODUCT_aCTIVE_FLAG=0  and  p.dIVISION_CODE='" + div_code + "'   ";

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

    public class GridDecorator
    {
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }
}