using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;
using System.Web.Services;
using Newtonsoft.Json;
using DBase_EReport;

public partial class MasterFiles_SubDivisionCreation : System.Web.UI.Page
{
#region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
#endregion
string sf_type = string.Empty;
public static string hqcode = string.Empty;
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
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "SubDivisionList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];
      hqcode = Request.QueryString["hqcode"];
        if (!Page.IsPostBack)
        {
           
            //menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                SubDivision sd = new SubDivision();
                dsSubDiv = sd.getSubDiv(divcode, Subdivision_Code);                
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();                  
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
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
	[WebMethod]
    public static string edit_subdiv(string divcode, string scode)
    {
        subdiv cp = new subdiv();
        DataSet ds = cp.getsubid(divcode, scode);
        return JsonConvert.SerializeObject(ds.Tables[0]);
    }
     protected void btnSubmit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        subdiv_sname = txtSubDivision_Sname.Text;
        subdiv_name = txtSubDivision_Name.Text;
        if (hqcode == null)
        {
            // Add New Sub Division
            SubDivision dv = new SubDivision();
            int iReturn = dv.RecordAdd(divcode, subdiv_sname, subdiv_name);

             if (iReturn > 0 )
            {           
           
                // menu1.Status = "Sub Division created Successfully ";
               ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");           
               Resetall();
            }
             else if (iReturn == -2)
             {
                
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Division Name Already Exist');</script>");
                 txtSubDivision_Name.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Division Code Already Exist');</script>");
                 txtSubDivision_Sname.Focus();
             }
        }
        else
        {
            // Update Sub Division
            subdiv dv = new subdiv();
            int subdivcode = Convert.ToInt16(hqcode);
            int iReturn = dv.RecordUpdate(subdivcode, subdiv_sname, subdiv_name, divcode);
            if (iReturn > 0 )
            {
               // menu1.Status = "Sub Division Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='SubDivisionList.aspx';</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Division Name Already Exist');</script>");
                txtSubDivision_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Division Code Already Exist');</script>");
                txtSubDivision_Sname.Focus();
            }
        }
    }
     private void Resetall()
     {
         txtSubDivision_Sname.Text = "";
         txtSubDivision_Name.Text = "";
     }   
public class subdiv
    {
		public bool RecordExist(int subdivision_code, string subdiv_sname, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_name ='" + subdiv_sname + "' and Div_Code= '" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public bool sRecordExist(int subdivision_code, string subdiv_name, string divcode)
        {

            bool bRecordExist = false;
            try
            {
                DB_EReporting db = new DB_EReporting();

                string strQry = "SELECT COUNT(subdivision_code) FROM mas_subdivision WHERE subdivision_code != '" + subdivision_code + "' AND subdivision_sname ='" + subdiv_name + "' and Div_Code= '" + divcode + "' ";

                int iRecordExist = db.Exec_Scalar(strQry);

                if (iRecordExist > 0)
                    bRecordExist = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRecordExist;
        }
        public int RecordUpdate(int subdivision_code, string subdiv_sname, string subdiv_name, string divcode)
        {
            int iReturn = -1;
            if (!sRecordExist(subdivision_code, subdiv_sname, divcode))
            {
                if (!RecordExist(subdivision_code, subdiv_name, divcode))
                {
                    try
                    {

                        DB_EReporting db = new DB_EReporting();
                        string strQry = "UPDATE Mas_Product_Category SET Product_Cat_Div_Name = '" + subdiv_name + "' WHERE Product_Cat_Div_Code = '" + subdivision_code + "' ";
                        iReturn = db.ExecQry(strQry);
                        strQry = "UPDATE Mas_Product_Brand SET Product_Cat_Div_Name = '" + subdiv_name + "' WHERE Product_Cat_Div_Code = '" + subdivision_code + "' ";
                        iReturn = db.ExecQry(strQry);

                        strQry = "UPDATE mas_subdivision " +
                                 " SET subdivision_sname = '" + subdiv_sname + "', " +
                                 " subdivision_name = '" + subdiv_name + "' , " +
                                 " LastUpdt_Date = getdate() " +
                                 " WHERE subdivision_code = '" + subdivision_code + "' ";

                        iReturn = db.ExecQry(strQry);

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    iReturn = -2;
                }
            }
            else
            {
                iReturn = -3;
            }
            return iReturn;

        }
        public DataSet getsubid(string divcode, string scode)
        {
            DB_EReporting db = new DB_EReporting();
            DataSet ds = null;
            string strQry = "select subdivision_sname,subdivision_name from  mas_subdivision where subdivision_code='" + scode + "'and Div_Code='" + divcode + "'";
            try
            {
                ds = db.Exec_DataSet(strQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }     
}