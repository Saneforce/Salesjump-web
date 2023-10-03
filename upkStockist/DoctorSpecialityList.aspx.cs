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

public partial class MasterFiles_DoctorSpecialityList : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsDocSpe = null;
    int DocSpeCode = 0;
    static string divcode = string.Empty;
    string DocSpe_SName = string.Empty;
    string DocSpeName = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    static string sf_type = string.Empty;
    static string sf_code = string.Empty;
    int time;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sf_type = Convert.ToString(Session["sf_type"]);
        sf_code = Convert.ToString(Session["sf_code"]);

        if (!Page.IsPostBack)
        {
           
            //btnNew.Focus();
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
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
    [WebMethod]
    public static string FillDocSpeS()
    {

        Doctor dv = new Doctor();
        DataTable dt = new DataTable();
        dt = dv.getDocSpe(divcode.TrimEnd(','), sf_type, sf_code).Tables[0];
        return JsonConvert.SerializeObject(dt);
        
    }
    
    // Sorting 
    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    private DataTable BindGridView()
    {
        DataTable dtGrid = new DataTable();
        Doctor dv = new Doctor();
        dtGrid = dv.getDocSpecialitylist_DataTable(divcode.TrimEnd(','));
        return dtGrid;
    }
  
    protected void btnNew_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("/Stockist/DoctorSpeciality.aspx");
    }
   

   
    private void Update(int eIndex)
    {
        //Label lblDocSpeCode = (Label)grdDocSpe.Rows[eIndex].Cells[1].FindControl("lblDocSpeCode");
        //DocSpeCode = Convert.ToInt16(lblDocSpeCode.Text);
        //TextBox txtDocSpe_SName = (TextBox)grdDocSpe.Rows[eIndex].Cells[2].FindControl("txtDoc_Spe_SName");
        //DocSpe_SName = txtDocSpe_SName.Text;
        //TextBox txtDocSpeName = (TextBox)grdDocSpe.Rows[eIndex].Cells[3].FindControl("txtDocSpeName");
        //DocSpeName = txtDocSpeName.Text;

        // Update Doctor Speciality
        Doctor dv = new Doctor();
        int iReturn = dv.RecordUpdateDocSpl(DocSpeCode, DocSpe_SName, DocSpeName, divcode.TrimEnd(','));
         if (iReturn > 0 )
        {
            //menu1.Status = "Doctor Speciality Updated Successfully ";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }
         else if (iReturn == -2)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Name Already Exist');</script>");
         }
         else if (iReturn == -3)
         {
             ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Short Name Already Exist');</script>");
         }
    }
    protected void btnBulkEdit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("BulkEditDocSpec.aspx");
    }
    protected void btnSlNo_Gen_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("DocSpec_SlNo_Gen.aspx");
    }

    protected void btnTransfer_Spec_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Spec_Trans.aspx");
    }
    protected void btnReactivate_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        Response.Redirect("Doc_Spec_React.aspx");
    }
}