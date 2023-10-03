using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
using System.Web.Services;

public partial class MasterFiles_DashBoard_Doctor_Details : System.Web.UI.Page
{
    DataSet dsListedDR = null;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataSet dsSalesForce = new DataSet();
    public static DataSet dsDoctor = new DataSet();
    string strdiv = null;
    string div_code = string.Empty;
    string strMultiDiv = string.Empty;
    string request_doctor = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    public static int iddvalue;
    public static int iDRCatg = -1;
    public static string imrvalue = string.Empty;
    int iTotal_FF = 0;
    int[] iTotal_Catg = new int[20];
    int tot_catg = 0;
    int i = -1;
    string sDRCatg_Count = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        if (sf_type == "3")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
   
        if (!Page.IsPostBack)
        {
            Filldiv();
          //  ddlDivision.SelectedIndex = 1;
            ddlFieldForce.SelectedIndex = 1;
          rdoType.SelectedIndex = 1;
            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
            btnGo.Focus();
           FillMRManagers();
           FillManagers();
          btnGo_Click(sender, e);
          pnlchart.Visible = true;
          pnlchart.Visible = false;
          
        } 
        FillColor();
       
    }
 
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }

            }

        }
        else
        {
            DataSet dsDivision = new DataSet();

            if (strMultiDiv != "")
            {
                dsDivision = dv.getMultiDivision(strMultiDiv);
            }
            else
            {
                dsDivision = dv.getDivision_Name();
            }
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }
        }

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMRManagers();
    }
    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }

        FillColor();
    }
    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j = j + 1;

        }
    }
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblFF.Text = "Field Force";
        FillManagers();
        FillColor();
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        if (ddlFFType.SelectedValue.ToString() == "1")
        {
            ddlAlpha.Visible = false;
            dsSalesForce = sf.UserList_Hierarchy(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "0")
        {
            FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(ddlDivision.SelectedValue.ToString(), "admin");
        }
        else if (ddlFFType.SelectedValue.ToString() == "2")
        {
            dsSalesForce = sf.UserList_HQ(ddlDivision.SelectedValue.ToString(), "admin");
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
    }

    private void FillSF_Alpha()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForcelist_Alphabet(ddlDivision.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlAlpha.DataTextField = "sf_name";
            ddlAlpha.DataValueField = "val";
            ddlAlpha.DataSource = dsSalesForce;
            ddlAlpha.DataBind();
            ddlAlpha.SelectedIndex = 0;
        }
    }

    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(ddlDivision.SelectedValue.ToString(), "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }

    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_getMR(ddlDivision.SelectedValue.ToString(), ddlFieldForce.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lblMR.Visible = true;
            ddlMR.Visible = true;
            ddlMR.DataTextField = "sf_name";
            ddlMR.DataValueField = "sf_code";
            ddlMR.DataSource = dsSalesForce;
            ddlMR.DataBind();
            ddlMR.Items.Insert(0, new ListItem("---Select---", "0"));
        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
       
        int tot_rows = 0;
        int tot_cols = 0;

        // Fetch the total columns for the table
        pnlchart.Visible = true;
        Doctor dr = new Doctor();
        if (rdoType.SelectedValue == "0")
        {
            dsDoctor = dr.getDocCat(ddlDivision.SelectedValue.ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                tot_cols = dsDoctor.Tables[0].Rows.Count;
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (rdoType.SelectedValue == "1")
        {
            dsDoctor = dr.getDocSpec(ddlDivision.SelectedValue.ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (rdoType.SelectedValue == "2")
        {
            dsDoctor = dr.getDocClass(ddlDivision.SelectedValue.ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        else if (rdoType.SelectedValue == "3")
        {
            dsDoctor = dr.getDocQual(ddlDivision.SelectedValue.ToString());
            if (dsDoctor.Tables[0].Rows.Count > 0)
            {
                ViewState["dsDoctor"] = dsDoctor;
            }
        }
        CreateDynamicTable();
     //   GetData();
    }


    [WebMethod]
    public static List<Data> GetData()
    {
       
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
     
            TableCell tc_catg_det_name = new TableCell();
            Literal lit_catg_det_name = new Literal();

            Doctor dr_cat = new Doctor();

            //ds.Merge(dsDoctor);
          //  dt = ds.Tables[0];
            List<Data> dataList = new List<Data>();
            string cat = "";
            int val = 0;
            foreach (DataRow dr in dsDoctor.Tables[0].Rows)
            {
                if (iddvalue == 0)
                {
                    iDRCatg = dr_cat.getDoctorcount(imrvalue, dr["Doc_Cat_Code"].ToString());
                    
                }
                else if (iddvalue == 1)
                {
                    iDRCatg = dr_cat.getSpecialcount(imrvalue, dr["Doc_Cat_Code"].ToString());
                   
                }
                else if (iddvalue == 2)
                {
                    iDRCatg = dr_cat.getClasscount(imrvalue, dr["Doc_Cat_Code"].ToString());
                    
                }
                else if (iddvalue == 3)
                {
                    iDRCatg = dr_cat.getQualcount(imrvalue, dr["Doc_Cat_Code"].ToString());
                    
                }

                //foreach (DataRow dr in dt.Rows)
                //{
                    cat = dr[2].ToString();
                    val = Convert.ToInt32(iDRCatg);

                    dataList.Add(new Data(cat, val));
                //}
            }

            return dataList;
       // }
    }

  
    public class Data
    {
        public string ColumnName = "";
        public int Value = 0;

        public Data(string columnName, int value)
        {
            
            ColumnName = columnName;
            Value = value;
        }
    }    
    private void CreateDynamicTable()
    {
        dsDoctor = (DataSet)ViewState["dsDoctor"];
        TableRow tr_catg = new TableRow();

        

        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        {

            TableCell tc_catg_name = new TableCell();
            tc_catg_name.BorderStyle = BorderStyle.Solid;
            tc_catg_name.BorderWidth = 1;
            Literal lit_catg_name = new Literal();
            lit_catg_name.Text = "<center><b>" + dataRow["Doc_Cat_Name"].ToString() + "</b></center>";
            tc_catg_name.Controls.Add(lit_catg_name);
            tr_catg.Cells.Add(tc_catg_name);            
        }

        tbl.Rows.Add(tr_catg);

        TableRow tr_det = new TableRow();
        iTotal_FF = 0;
        i = 0;
        iddvalue =Convert.ToInt16(rdoType.SelectedValue);
        imrvalue = ddlMR.SelectedValue;
        foreach (DataRow dataRow in dsDoctor.Tables[0].Rows)
        {
            TableCell tc_catg_det_name = new TableCell();
            Literal lit_catg_det_name = new Literal();

            Doctor dr_cat = new Doctor();
            if (rdoType.SelectedValue == "0")
            {
                iDRCatg = dr_cat.getDoctorcount(ddlMR.SelectedValue, dataRow["Doc_Cat_Code"].ToString());
                lblCatg.Text = "Listed Customer Count - Categorywise";
            }
            else if (rdoType.SelectedValue == "1")
            {
                iDRCatg = dr_cat.getSpecialcount(ddlMR.SelectedValue, dataRow["Doc_Cat_Code"].ToString());
                lblCatg.Text = "Listed Customer Count - Specialitywise";
            }
            else if (rdoType.SelectedValue == "2")
            {
                iDRCatg = dr_cat.getClasscount(sf_code, dataRow["Doc_Cat_Code"].ToString());
                lblCatg.Text = "Listed Customer Count - Classwise";
            }
            else if (rdoType.SelectedValue == "3")
            {
                iDRCatg = dr_cat.getQualcount(ddlMR.SelectedValue, dataRow["Doc_Cat_Code"].ToString());
                lblCatg.Text = "Listed Customer Count - Qualificationwise";
            }

            if (iDRCatg == 0)
            {
                sDRCatg_Count = " - ";
            }
            else
            {
                sDRCatg_Count = iDRCatg.ToString();
            }

            lit_catg_det_name.Text = "<center>" + sDRCatg_Count + "</center>";

            tc_catg_det_name.BorderStyle = BorderStyle.Solid;
            tc_catg_det_name.VerticalAlign = VerticalAlign.Middle;
            tc_catg_det_name.BorderWidth = 1;
            tc_catg_det_name.Controls.Add(lit_catg_det_name);
            tr_det.Cells.Add(tc_catg_det_name);          
            tbl.Rows.Add(tr_det);
           
            
        }
    }

   
}