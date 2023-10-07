using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
public partial class MasterFiles_HolidayList_SlNo : System.Web.UI.Page
{
    DataSet dsDivision = null;
    string div_code = string.Empty;
    string txtNewSlNo = string.Empty;
    string txtSlNo = string.Empty;
    string Division_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet dsHoliday = null;
    DataSet dsdiv = null;

    string Holiday_Name = string.Empty;
    string sf_type = string.Empty;
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
           Session["backurl"] = "Holiday_List.aspx";
            menu1.Title = this.Page.Title;
           // menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Filldiv();
         
            FillHoliday();
         
        

        }
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
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
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
    private void FillHoliday()
    {

        Holiday holi = new Holiday();
        dsHoliday = holi.get_Holidays(ddlDivision.SelectedValue.ToString());
        if (dsHoliday.Tables[0].Rows.Count > 0)
        {
            grdHoliday.Visible = true;
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }
        else
        {
            grdHoliday.DataSource = dsHoliday;
            grdHoliday.DataBind();
        }

    }
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHoliday();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool isError = false;
        if (btnSubmit.Text == "Generate - Sl No")
        {
            System.Threading.Thread.Sleep(time);
            int i = 1;
            int iVal = 1;
            string sVal = string.Empty;
            sVal = ",";

            foreach (GridViewRow gridRow in grdHoliday.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                if (txtSNo.Text.Length > 0)
                {
                    if (grdHoliday.Rows.Count >= Convert.ToInt16(txtSNo.Text.Trim()))
                    {
                        sVal = sVal + txtSNo.Text + ',';
                    }
                    else
                    {
                        isError = true;
                        break;
                    }
                }
                i++;
            }
            if (isError == false)
            {

                if (sVal == "")
                {
                    foreach (GridViewRow gridRow in grdHoliday.Rows)
                    {
                        Label lblSNo = (Label)gridRow.Cells[1].FindControl("lblSNo");
                        TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                        txtSNo.Text = lblSNo.Text;
                    }
                }
                else
                {
                    iVal = 1;
                    System.Threading.Thread.Sleep(time);
                    foreach (GridViewRow gridRow in grdHoliday.Rows)
                    {
                        TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                        if (txtSNo.Text.Length <= 0)
                        {
                            for (iVal = 1; iVal <= i; iVal++)
                            {
                                string schk = ',' + iVal.ToString() + ',';
                                if (sVal.IndexOf(schk) != -1)
                                {
                                    //Do Nothing
                                }
                                else
                                {
                                    sVal = sVal + iVal.ToString() + ',';
                                    break;
                                }
                            }

                            txtSNo.Text = iVal.ToString();
                        }

                    }
                }
                btnSubmit.Focus();
                btnSubmit.Text = "Save";
            }
            else if (isError == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter Valid Number');</script>");
            }
        }
        else
        {
            System.Threading.Thread.Sleep(time);
            // Save
            foreach (GridViewRow gridRow in grdHoliday.Rows)
            {
                TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
                txtSlNo = txtSNo.Text;
                Label lblHoliday_Id = (Label)gridRow.Cells[2].FindControl("lblHoliday_Id");
               
                Holiday dv = new Holiday();
                int iReturn = dv.Update_HolidaySlNO(txtSlNo,ddlDivision.SelectedValue, lblHoliday_Id.Text);
                if (iReturn > 0)
                {
                    // menu1.Status = "Sl No Updated Successfully ";
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sl No Updated Successfully');window.location='Holiday_List.aspx';</script>");
                }
              
            }

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gridRow in grdHoliday.Rows)
        {
            TextBox txtSNo = (TextBox)gridRow.Cells[1].FindControl("txtSlNo");
            txtSNo.Text = "";
        }
    }
}