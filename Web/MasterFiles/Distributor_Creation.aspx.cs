using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using DBase_EReport;


public partial class MasterFiles_Distributor_Creation : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsStockist = null;
    DataSet dsSubDivision = null;
    DataSet dsState = null;
    DataSet dsCat = null;
    string subdivision_code = string.Empty;
    string sub_division = string.Empty;
    string sChkLocation = string.Empty;
    DataSet dsReport = null;
    public static string stockist_code = string.Empty;
    string Sale_Entry = string.Empty;
    int iReturn_FM = -1;
    string divcode = string.Empty;
    string SF_Name = string.Empty;
    string gstnNo = string.Empty;
    string dis_cat_code = string.Empty;
    string stockist_name = string.Empty;
    string stockist_Address = string.Empty;
    string stockist_ContactPerson = string.Empty;
    string stockist_Designation = string.Empty;
    string stockist_mobilno = string.Empty;
    string scheck = string.Empty;
    string Territory_Name = string.Empty;
    string PoolStatus = string.Empty;
    string sf_code = string.Empty;
    public string sf_type = string.Empty;
    string ERP_Code = string.Empty;
    string headquarters = string.Empty;
    string stkemail = string.Empty;
    string Town_Name = string.Empty;
    string Town_code = string.Empty;
    string Username = string.Empty;
    string Password = string.Empty;
    string Territor_Code = string.Empty;
    string sChkSalesforce = string.Empty;
    string Field_Name = string.Empty;
    string Field_Code = string.Empty;
    string dis_type = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
	 string Taluk_Name = string.Empty;
    string Taluk_code = string.Empty;
    string[] statecd;
    string state_code = string.Empty;
    int Norm_val = 0;
    //int sChkSalesforce = -1;
    string ReportingMGR = string.Empty;
    int iIndex = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    public static bool transfer = false;
    public static bool remove = false;	
    string remsf = string.Empty;
	string Rate = string.Empty;
	string Stock_check = string.Empty;
    #endregion

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
        Session["backurl"] = "StockistList.aspx";
        divcode = Convert.ToString(Session["div_code"]);
        stockist_code = Request.QueryString["stockist_code"];


        if (!Page.IsPostBack)
        {


            GetTownName();
			GettalukName();
            GetTerritoryName();
            FillCatagry();
			 GetRate();
            //  GetFFNAME();
            // menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Num();
            DDL_FO.Enabled = false;
            if (stockist_code != "" && stockist_code != null)
            {
                Stockist sk = new Stockist();
                //dsStockist = sk.getStockist_Create(divcode, stockist_code);
				 dsStockist = getStockist_Create(divcode, stockist_code);

                if (dsStockist.Tables[0].Rows.Count > 0)
                {
                    Txt_Password.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();



                    Txt_Stockist_Code.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
                    txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
                    txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
                    txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
                    Txt_ERP_Code.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
                    ddlDist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
					 txttaluk.SelectedItem.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
                    Txtheadquarters.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
                    ddltype.SelectedItem.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
                    txtemail.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
                    Txt_Norm.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(13).ToString();
                    TextBox1.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(14).ToString();
                    // DDL_FO.SelectedValue = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    // DDL_FO.Enabled = false;
                    Txt_User_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
                    Txt_Password.Attributes.Add("value", dsStockist.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
                    Txt_Password.TextMode = TextBoxMode.Password;
                    ddlTerritoryName.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
                    subdivision_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(12).ToString();
                    sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                    txtgstn.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(18).ToString();
                    DDL_category.SelectedValue = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(19).ToString();
					ddlRate.SelectedValue = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(21).ToString();
					
                    string[] str_FO = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(15).ToString().Split(',');
					remsf = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(15).ToString();
                    loadff();
                    for (int i = 0; i < DDL_FO.Items.Count; i++)
                    {
                        for (int k = 0; k < str_FO.Length; k++)
                        {
                            if (DDL_FO.Items[i].Value.ToString() == str_FO[k])
                            {
                                DDL_FO.Items[i].Selected = true;
                            }
                        }
                    }


                    HidStockistCode.Value = stockist_code;
                }


            }
            //Num();

            FillCheckBoxList1();
			
		//GetRate();

        }
    }
	
	public DataSet getStockist_Create(string divcode, string stockist_code)
    {
        DB_EReporting db_ER = new DB_EReporting();

        DataSet dsStockist = null;
        string strQry = string.Empty;
        strQry = "SELECT User_Entry_Code,Stockist_Name,Stockist_Address,Stockist_ContactPerson,Stockist_Designation,Stockist_Mobile,SF_Code,ERP_Code,Dist_Code,Username,Password,Territory_Code,subdivision_code,Norm_Val,(select sf_name +',' from Mas_Salesforce s where charindex(','+ s.sf_code +',' ,','+a.field_code +',')>0  for xml path(''))  Field_Name,Field_Code,Head_Quaters,Type,gstn,Dis_Cat_Code,isnull(Stockist_Address1,'')Stockist_Email,Price_list_Name,stock_maintain" +
                 " FROM mas_stockist a" +
                 " WHERE stockist_active_flag=0 " +
                 " AND Division_Code= '" + divcode + "' " +
                 " AND stockist_code = '" + stockist_code + "'  ";

        try
        {
            dsStockist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsStockist;
    }
	
	 private void GetRate()
    {
        Stockist sk = new Stockist();
        //dsStockist = sk.getRates(divcode);             
        dsStockist = getRates(divcode);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlRate.DataTextField = "Price_list_Name";
            ddlRate.DataValueField = "Price_list_Sl_No";
            ddlRate.DataSource = dsStockist;
            ddlRate.DataBind();
            ddlRate.Items.Insert(0, "Select");
        }
    }

    public DataSet getRates(string div_code)
    {
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;
        DataSet dsStockist = null;        
        strQry = "select Price_list_Sl_No, Price_list_Name from Mas_Product_Wise_Bulk_rate_head where Division_Code = '"+div_code+"' and Price_Active_flag='0' order by Price_list_Name";
        //strQry = "SELECT '' as Price_list_Sl_No, '--Select--' as Price_list_Name " +
        //             " UNION " +
        //             " select Price_list_Sl_No, Price_list_Name from Mas_Product_Wise_Bulk_rate_head where Division_Code = '"+div_code+"' order by Price_list_Name";
        try
        {
            dsStockist = db_ER.Exec_DataSet(strQry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dsStockist;
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
    private void Num()
    {
        int num = 0;
        Stockist sk = new Stockist();
        dsStockist = sk.getCheck(divcode);
        num = Convert.ToInt32(dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

        Txt_Stockist_Code.Text = (num + 1).ToString();


    }

    private void FillCheckBoxList1()
    {

        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubDiv(divcode);
		dsSubDivision.Tables[0].DefaultView.RowFilter = "SubDivision_Active_Flag = 0";
        DataTable dt = (dsSubDivision.Tables[0].DefaultView).ToTable();
        chkboxLocation.DataTextField = "subdivision_name";
        chkboxLocation.DataSource = dt;
        chkboxLocation.DataBind();
        string[] subdiv;
        if (subdivision_code != "")
        {
            iIndex = -1;
            subdiv = subdivision_code.Split(',');
            foreach (string st in subdiv)
            {
                for (iIndex = 0; iIndex < chkboxLocation.Items.Count; iIndex++)
                {
                    if (st == chkboxLocation.Items[iIndex].Value)
                    {
                        chkboxLocation.Items[iIndex].Selected = true;
                        chkboxLocation.Items[iIndex].Attributes.Add("style", "Color: #8A2BE2;font-weight:Bold");
                    }
                }
            }
        }
    }




    private void FillStockist_Reporting()
    {


        Stockist sk = new Stockist();
        dsStockist = sk.getStockist_Create(divcode, stockist_code);

        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            Txt_Stockist_Code.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            txtStockist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            txtStockist_Address.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(2).ToString();
            txtStockist_ContactPerson.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(3).ToString();
            txtStockist_Desingation.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(4).ToString();
            txtStockist_Mobile.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(5).ToString();
            Txt_ERP_Code.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(7).ToString();
            Txtheadquarters.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(16).ToString();
            txtemail.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(20).ToString();
            ddltype.SelectedItem.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(17).ToString();
            ddlDist_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(8).ToString();
            Txt_User_Name.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(9).ToString();
            //Txt_Password.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(10).ToString();
            Txt_Password.Attributes.Add("value", dsStockist.Tables[0].Rows[0].ItemArray.GetValue(10).ToString());
            Txt_Password.TextMode = TextBoxMode.Password;
            ddlTerritoryName.Text = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(11).ToString();
            sf_code = dsStockist.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();

            HidStockistCode.Value = stockist_code;
        }



    }




    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {



            for (int i = 0; i < DDL_FO.Items.Count; i++)
            {
                if (DDL_FO.Items[i].Selected)
                {
                    Field_Code += DDL_FO.Items[i].Value + ",";
                    Field_Name += DDL_FO.Items[i].Text + ",";
                }
            }


            System.Threading.Thread.Sleep(time);
             string Stock_code = Txt_Stockist_Code.Text.Trim();
            stockist_name = txtStockist_Name.Text.Trim();
            stockist_Address = txtStockist_Address.Text.Trim();
            stockist_ContactPerson = txtStockist_ContactPerson.Text.Trim();
            stockist_Designation = txtStockist_Desingation.Text.Trim();
            stockist_mobilno = txtStockist_Mobile.Text.Trim();
            ERP_Code = Txt_ERP_Code.Text.Trim();
            headquarters = Txtheadquarters.Text.Trim();
            stkemail = txtemail.Text.Trim();
            Town_Name = ddlDist_Name.SelectedItem.Text.Trim();
            Town_code = ddlDist_Name.SelectedValue;
			Taluk_Name = txttaluk.SelectedItem.Text.Trim();
            Taluk_code = txttaluk.SelectedValue;
            Territory_Name = ddlTerritoryName.SelectedItem.Text.Trim();
            Territor_Code = ddlTerritoryName.SelectedValue;
            Username = Txt_User_Name.Text.Trim();
            Password = Txt_Password.Text.Trim();
            gstnNo = txtgstn.Text.Trim();
			Rate = ddlRate.SelectedItem.Value;
            dis_cat_code = DDL_category.SelectedValue;
            Txt_Norm.Text = (Txt_Norm.Text == "") ? "0" : Txt_Norm.Text;
            Norm_val = Convert.ToInt32(Txt_Norm.Text);
			Stock_check = ((chckboxstock.Checked.ToString() == "True") ? "1" : "0");
			
            for (int i = 0; i < chkboxLocation.Items.Count; i++)
            {
                if (chkboxLocation.Items[i].Selected)
                {
                    sub_division = sub_division + chkboxLocation.Items[i].Value + ",";
                }
            }

            //sub_division = chkboxLocation.SelectedValue;
            dis_type = ddltype.SelectedItem.Text;
            // Field_Name = DDL_FO.SelectedItem.Text;
            // Field_Code = DDL_FO.SelectedValue;
            //Stockist St = new Stockist();
            //DataSet dsStockist_Admin = St.getStockist_Allow_Admin(divcode);
            //DataSet dsStockist_Count = St.getStockist_Count(divcode);

            //if (dsStockist_Count.Tables[0].Rows[0][0].ToString() == dsStockist_Admin.Tables[0].Rows[0][0].ToString())
            //{
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", " alert('Cannot enter more than " + dsStockist_Admin.Tables[0].Rows[0][0].ToString() + " Stockist');", true);
            //}


            //else
            //{
				 if(ERP_Code!="" && stockist_name!="" && stockist_Address != "") {
            if (stockist_code == null)
            {
                // Add new Stockist Details 
                Stockist sk = new Stockist();
                //int iReturn = sk.RecordAdd(divcode, Stock_code, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, ERP_Code, Town_Name, Town_code, Territor_Code, Territory_Name, Username, Password, sub_division, Norm_val, Field_Name, Field_Code, headquarters, dis_type, gstnNo, dis_cat_code,stkemail, Taluk_Name, Taluk_code);
				string stkname = "";
                if (Username != "")
                { 
                   stkname= chkRecord(divcode, Stock_code, Username, stockist_code,"create");
                }
                int iReturn = -1;
                if (stkname=="")
                {
                 iReturn = RecordAdd(divcode, Stock_code, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, ERP_Code, Town_Name, Town_code, Territor_Code, Territory_Name, Username, Password, sub_division, Norm_val, Field_Name, Field_Code, headquarters, dis_type, gstnNo, dis_cat_code, stkemail, Taluk_Name, Taluk_code, Rate,Stock_check,"0");
				}
				else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Username Exist');</script>");
                     
                }

                if (iReturn > 0)
                {


                     stockist_code = Txt_Stockist_Code.Text.Trim();
                    stockist_name = txtStockist_Name.Text.Trim();
                    stockist_Address = txtStockist_Address.Text.Trim();
                    stockist_ContactPerson = txtStockist_ContactPerson.Text.Trim();
                    stockist_Designation = txtStockist_Desingation.Text.Trim();
                    stockist_mobilno = txtStockist_Mobile.Text.Trim();

                    //iReturn_FM = sk.RecordAdd_FM(divcode, iReturn, stockist_name, Stock_code, Sale_Entry);


                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');window.location='Distributor_Creation.aspx';</script>");


                    Resetall();
                    Num();

                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "Distributor already Exist!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                }
            }
            else
            {
                // Update Stockist Details
                Stockist sk = new Stockist();
                //int iReturn = sk.RecordUpdate(divcode, stockist_code, Stock_code, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, ERP_Code, Town_Name, Town_code, Territor_Code, Territory_Name, Username, Password, Norm_val, Field_Name, Field_Code, sub_division, headquarters, dis_type, gstnNo, dis_cat_code,stkemail, Taluk_Name, Taluk_code);
                string stkname = "";
                if (Username != "")
                {
                    stkname = chkRecord(divcode, Stock_code, Username,stockist_code, "edit");
                }
                int iReturn = -1;
                if (stkname == "")
                {
				 iReturn = RecordUpdate(divcode, stockist_code, Stock_code, stockist_name, stockist_Address, stockist_ContactPerson, stockist_Designation, stockist_mobilno, ERP_Code, Town_Name, Town_code, Territor_Code, Territory_Name, Username, Password, Norm_val, Field_Name, Field_Code, sub_division, headquarters, dis_type, gstnNo, dis_cat_code, stkemail, Taluk_Name, Taluk_code, Rate,Stock_check,"");				
				}
				 else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Username Exist');</script>");
                }
				if (divcode == "52" || divcode=="100")
                {
                    if (transfer == true)
                    {
                        string[] myarr = Field_Code.TrimEnd(',').Split(',');
                        for (int i = 0; i < myarr.Length; i++)
                        {
                            int iret = sk.updateMappedDistSF(Territor_Code, myarr[i], stockist_code);
                        }
                    }
                }
                if (iReturn > 0)
                {
                    //menu1.Status = "Stockist updated Successfully";
					if(divcode=="32" || divcode=="43" ||divcode=="48")
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Distributor_Creation.aspx';</script>");
                    else
						ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='Distributor_Master.aspx';</script>");
                }
                else if (iReturn == -2)
                {
                    //menu1.Status = "Stockist exist with the same stockist name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist with the Same Distributor Name');</script>");
                }
                else if (iReturn == -3)
                {
                    //menu1.Status = "Stockist exist with the same stockist name!!";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                }
            }
            }
			else
            {
                if (ERP_Code == "")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter ERP code');</script>");
                    Txt_ERP_Code.Focus();
                }
                else if (stockist_name=="")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Distributor Name');</script>");
                    txtStockist_Name.Focus();
                }
                else if (stockist_Address=="")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Stockist Address');</script>");
                    txtStockist_Address.Focus();
                }
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + ex.Message + "');</script>");
        }
    }
	
	public int RecordAdd(string divcode, string SF_Name, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code, string Territory_Name, string Username, string Password, string sub_division, int norm, string Fo_Name, string Fo_Code, string head_quaters, string type, string gstnNo, string dis_cat_code, string stkemail, string Taluk_Name, string Taluk_code, string Rate,string stock,string Fssai_no)
    {
        int iReturn = -1;
        //if (!NRecordExist(Territor_Code, stockist_name, divcode))
        //{
            try
            {
                int stockist_code = 0;
                string STATE_CD = "";
                DataSet dsstcode = null;
                DB_EReporting db = new DB_EReporting();
                string strQry = string.Empty;

                strQry = "SELECT CASE WHEN COUNT(Distributor_Code)>0 THEN MAX(Distributor_Code) ELSE 0 END FROM mas_stockist";
                stockist_code = db.Exec_Scalar(strQry);
                stockist_code += 1;
                // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
                strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
                // STATE_CD = db.Exec_Scalar(strQry).ToString();

                dsstcode = db.Exec_DataSet(strQry);
				if (dsstcode.Tables[0].Rows.Count > 1)
				{
                STATE_CD = dsstcode.Tables[0].Rows[0]["State_code"].ToString();
				}
				else
				{
                STATE_CD = "";
				}
                // if (dsstcode.Tables[0].Rows.Count > 1)
                // {
                    // return iReturn;
                // }
                // else
                // {
                    // foreach (DataRow row in dsstcode.Tables[0].Rows)
                    // {
                        // STATE_CD += row["State_code"].ToString() + ",";
                    // }
                    // STATE_CD = STATE_CD.TrimEnd(',');
                // }



                strQry = " INSERT INTO mas_stockist(Division_Code,Stockist_Code, SF_Code, Stockist_Name, Stockist_Address, Stockist_ContactPerson, Stockist_Designation, Stockist_Active_Flag, Stockist_Mobile, Territory, Created_Date,ERP_Code,Dist_Name,Dist_Code,Username,Password,Territory_Code,Distributor_Code,subdivision_code,Norm_Val,Field_Name,Field_Code,State_Code,User_Entry_Code,Head_Quaters,Type,gstn,Dis_Cat_Code,Stockist_Address1,Taluk_code,Taluk_Name,Price_List_Name,stock_maintain,Fssai_No) " +
                         " values('" + divcode + "', '" + stockist_code + "', '" + stockist_code + "','" + stockist_name + "', '" + stockist_Address + "', '" + stockist_ContactPerson + "', '" + stockist_Designation + "', '0' ,'" + stockist_mobilno + "','" + Territory_Name + "',getdate(),'" + ERP_Code + "','" + Town_Name + "','" + Town_code + "','" + Username + "','" + Password + "','" + Territor_Code + "','" + stockist_code + "','" + sub_division + "','" + norm + "','" + Fo_Name + "','" + Fo_Code + "','" + STATE_CD + "','" + SF_Name + "','" + head_quaters + "','" + type + "','" + gstnNo + "','" + dis_cat_code + "','" + stkemail + "','" + Taluk_code + "','" + Taluk_Name + "','" + Rate + "','" + stock + "','" + Fssai_no + "')";

                iReturn = db.ExecQry(strQry);
                if (iReturn > 0)
                {
                    iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
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
	 public string chkRecord(string divcode, string SF_Name,   string Username ,string stockistcode,string type)
    {
        string  iReturn ="";
        //if (!NRecordExist(Territor_Code, stockist_name, divcode))
        //{
        try
        {
			if(Username == ""){ return iReturn;}
            string stockist_code = "";
            DB_EReporting db = new DB_EReporting();
            string strQry = string.Empty;
            DataTable dt = new DataTable();
            strQry = "select Stockist_Code from mas_stockist where Division_Code='" + divcode + "'  and Username ='" + Username + "'";
			if(type=="edit"){
				strQry+=" and Stockist_Code<>'"+stockistcode+"'";	
			}			
            dt = db.Exec_DataTable(strQry);
            if (dt.Rows.Count > 0)
                stockist_code = db.Exec_DataTable(strQry).Rows[0][0].ToString();
            else
                stockist_code = "";

            
            if (stockist_code != "")
            {
                iReturn = stockist_code; //Inorder to maintain the same sl_no on detail table
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
       
        return iReturn;

    }
	
	 public int RecordUpdate(string divcode, string stockist_code, string Stock_code, string stockist_name, string stockist_Address, string stockist_ContactPerson, string stockist_Designation, string stockist_mobilno, string ERP_Code, string Town_Name, string Town_code, string Territor_Code, string Territory_Name, string Username, string Password, int norm, string Fo_Name, string Fo_Code, string sub_division, string headquarters, string type, string gstnNo, string Dis_Cat_Code, string stkemail, string Taluk_Name, string Taluk_code,string Rate,string Stock_check,string fsai_no)
    {
        int iReturn = -1;        
            try
            {
                string STATE_CD = "";
                DataSet dsstcode = null;
                DB_EReporting db = new DB_EReporting();
                string strQry = string.Empty;

                // strQry = "select State_code from mas_Salesforce where sf_Code='" + Fo_Code + "'";
                strQry = "select State_code from mas_Salesforce where charindex( ',' + cast(sf_Code as varchar) + ',','," + Fo_Code + "') > 0 group by State_code";
                // STATE_CD = db.Exec_Scalar(strQry).ToString();

                dsstcode = db.Exec_DataSet(strQry);
                if (dsstcode.Tables[0].Rows.Count >0)
                {
                    DataRow row = dsstcode.Tables[0].Rows[0];
                    STATE_CD = row["State_code"].ToString() + ",";
                    /* foreach (DataRow row in dsstcode.Tables[0].Rows){STATE_CD += row["State_code"].ToString() + ",";}*/
                    STATE_CD = STATE_CD.TrimEnd(',');
                }
                else
                {
                    return iReturn;
                }

                strQry = "UPDATE mas_stockist " +
                         " SET stockist_name = '" + stockist_name + "' , " +
                         " User_Entry_Code = '" + Stock_code + "' , " +
                         " stockist_Address = '" + stockist_Address + "', " +
                         " stockist_ContactPerson = '" + stockist_ContactPerson + "' , " +
                         " stockist_Designation = '" + stockist_Designation + "' , " +
                         " stockist_mobile = '" + stockist_mobilno + "' , " +
                         " Head_Quaters = '" + headquarters + "'  ,   Type = '" + type + "' ," +
                         " Territory = '" + Territory_Name + "', ERP_Code = '" + ERP_Code + "', " +
                         " Dist_Name = '" + Town_Name + "' ,Dist_Code = '" + Town_code + "', " +
                         " Username = '" + Username + "' ,Password = '" + Password + "', " +
                         " Norm_Val ='" + norm + "', " +
                         " Field_Name = '" + Fo_Name + "' ,Field_Code = '" + Fo_Code + "', " +
                         " Territory_Code = '" + Territor_Code + "' , " +
                          " subdivision_code ='" + sub_division + "'," +
                          " State_Code='" + STATE_CD + "'," +
                          " gstn='" + gstnNo + "',Stockist_Address1='" + stkemail + "'," +
                          " Dis_Cat_Code='" + Dis_Cat_Code + "'," +
                          " Taluk_code='" + Taluk_code + "'," +
                          " Taluk_Name='" + Taluk_Name + "'," +
                          " Price_list_Name='" + Rate + "'," +
						  " stock_maintain = '"+Stock_check+"'," +
                         " LastUpdt_Date = getdate()," + 
						 " Fssai_No ='"+ fsai_no +"'" + 
                         " WHERE stockist_code = '" + stockist_code + "' AND Division_Code = '" + divcode + "' ";
                iReturn = db.ExecQry(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }                 
        return iReturn;
    }

    private void GetTownName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getPool_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlDist_Name.DataTextField = "Dist_name";
            ddlDist_Name.DataValueField = "Dist_code";
            ddlDist_Name.DataSource = dsStockist;
            ddlDist_Name.DataBind();
        }
    }
	    private void GettalukName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.TowngetSubDiv(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            txttaluk.DataTextField = "Town_name";
            txttaluk.DataValueField = "Town_code";
            txttaluk.DataSource = dsStockist;
            txttaluk.DataBind();
        }
    }
    private void GetTerritoryName()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getTer_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            ddlTerritoryName.DataTextField = "Territory_name";
            ddlTerritoryName.DataValueField = "Territory_code";
            ddlTerritoryName.DataSource = dsStockist;
            ddlTerritoryName.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        btnSubmit_Click(sender, e);
    }
    private void Resetall()
    {
        Txt_Stockist_Code.Text = "";
        txtStockist_Name.Text = "";
        txtStockist_Address.Text = "";
        txtStockist_ContactPerson.Text = "";
        txtStockist_Desingation.Text = "";
        txtStockist_Mobile.Text = "";
        Txt_ERP_Code.Text = "";
        Txtheadquarters.Text = "";
        Txt_User_Name.Text = "";
        Txt_Password.Text = "";
        ddlDist_Name.SelectedIndex = -1;
		txttaluk.SelectedIndex = -1;
        ddlTerritoryName.SelectedIndex = -1;
        ddltype.SelectedIndex = -1;
        chkboxLocation.SelectedIndex = 0;
        Txt_Norm.Text = "";
        DDL_FO.SelectedIndex = 0;
        txtgstn.Text = "";
    }




    protected void ddlTerritoryName_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadff();
        //Stockist sk = new Stockist();

        //var terr = ddlTerritoryName.SelectedValue;
        //if (ddlTerritoryName.SelectedValue == "0")
        //{
        //    DDL_FO.SelectedIndex = 0;
        //}
        //else
        //{
        //    dsStockist = sk.FOName(terr,divcode);
        //    if (dsStockist.Tables[0].Rows.Count > 0)
        //    {
        //        DDL_FO.DataTextField = "Sf_Name";
        //        DDL_FO.DataValueField = "Sf_Code";
        //        DDL_FO.DataSource = dsStockist;
        //        DDL_FO.DataBind();
        //        DDL_FO.Enabled = true;
        //    }
        //    else
        //    {
        //         DDL_FO.Enabled = false;
        //    }

        //} 


    }


    private void loadff()
    {
        Stockist sk = new Stockist();
        var terr = ddlTerritoryName.SelectedValue;
        if (ddlTerritoryName.SelectedValue == "0")
        {
            DDL_FO.SelectedIndex = 0;
            // lstProducts.SelectedIndex=0;
        }
        else
        {
            dsStockist = sk.FOName(terr, divcode);
            if (dsStockist.Tables[0].Rows.Count > 0)
            {
                DDL_FO.DataTextField = "Sf_Name";
                DDL_FO.DataValueField = "Sf_Code";
                DDL_FO.DataSource = dsStockist;
                DDL_FO.DataBind();
                DDL_FO.Enabled = true;



                // lstProducts.DataTextField = "Sf_Name";
                // lstProducts.DataValueField = "Sf_Code";
                // lstProducts.DataSource = dsStockist;
                //  lstProducts.DataBind();
                //  lstProducts.Enabled = true;
            }
            else
            {
                DDL_FO.Enabled = false;
                //  lstProducts.Enabled = false;
            }

        }

    }





    private void GetFFNAME()
    {
        Stockist sk = new Stockist();
        dsStockist = sk.getffO_Name(divcode);
        if (dsStockist.Tables[0].Rows.Count > 0)
        {
            DDL_FO.DataTextField = "Sf_Name";
            DDL_FO.DataValueField = "Sf_Code";
            DDL_FO.DataSource = dsStockist;
            DDL_FO.DataBind();
        }
    }

    protected void chkboxLocation_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string getMappedSF()
    {
        stkist sk = new stkist();
        if (stockist_code != null)
        {
            DataSet dsStockist = sk.getMappedDistSF(stockist_code);
            return JsonConvert.SerializeObject(dsStockist.Tables[0]);
        }
        else
        {
			DataTable mappsf =new DataTable();
            return JsonConvert.SerializeObject(mappsf);
        }
    }
    [WebMethod]
    public static string removeRoute()
    {
        remove = true;
        return "Success";
    }
    private void FillCatagry()
    {
        ListedDR lstDR = new ListedDR();
        dsCat = lstDR.FetchCatagory(divcode);
        DDL_category.DataTextField = "Doc_Cat_Name";
        DDL_category.DataValueField = "Doc_Cat_Code";
        DDL_category.DataSource = dsCat;
        DDL_category.DataBind();
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        try
        {
            Server.Transfer("DistributorList.aspx");
        }
        catch (Exception ex)
        {

        }
    }
		
    [WebMethod]
    public static string transferRoute()
    {
        transfer = true;
        return "Success";
    }
    public class stkist
    {
        public DataSet getMappedDistSF(string stkcode)
        {
            DataSet iReturn = new DataSet();
            DB_EReporting db = new DB_EReporting();
            try
            {
                string strQry = "select ((select Sf_Name+',' from Mas_Salesforce ms where SF_Status=0 and CHARINDEX(','+ms.Sf_Code+',',','+Field_Code+',')>0 for xml path('')))sf_name,stock_maintain from Mas_Stockist where Stockist_Code='" + stkcode + " '";
                iReturn = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iReturn;
		}
	}
}