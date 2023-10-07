using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.IO;
using Bus_EReport;
using System.Web.Services;

public partial class MasterFiles_Tabctrl : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsSubDiv = null;
    DataSet accessMas = null;
    int subdivcode = 0;
    int subdivision_code = 0;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    string state_name = string.Empty;
    string Area_name = string.Empty;
    string Area_code = string.Empty;
    DataSet dsState = null;
    DataSet dsDivision = null;
    string state_cd = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string sState = string.Empty;
    string[] statecd;
    int time;
    #endregion
string sf_type = string.Empty;
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
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);


            /*  if (divcode != "" && divcode != null)
              {
                  //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + divcode  + "');</script>");


                  AdminSetup admin = new AdminSetup();
                  accessMas = admin.getapp_Setting(divcode);

                  //if (accessMas.Tables[0].Rows.Count > 0)
                  //{
                  //    rbtnnre.SelectedValue = accessMas.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
                  //}

                  foreach (DataRow row in accessMas.Tables[0].Rows)
                  {
                      rbtnnre.SelectedValue = row["UNLNeed"] == DBNull.Value ? "1" : row["UNLNeed"].ToString();// row["UNLNeed"].ToString(); 
                      rbtnnrc.SelectedValue = row["NwRoute"] == DBNull.Value ? "1" : row["NwRoute"].ToString(); //row["NwRoute"].ToString();
                      rbtnde.SelectedValue = row["StkNeed"] == DBNull.Value ? "1" : row["StkNeed"].ToString();// row["StkNeed"].ToString();
                      rbtnndc.SelectedValue = row["NwDist"] == DBNull.Value ? "1" : row["NwDist"].ToString();// row["NwDist"].ToString();
                      rbtnjcc.SelectedValue = row["jointwork"] == DBNull.Value ? "1" : row["jointwork"].ToString();// row["jointwork"].ToString();
                      rbtnrt.SelectedValue = row["template"] == DBNull.Value ? "1" : row["template"].ToString(); //row["template"].ToString();
                      rbtnwis.SelectedValue = row["CusOrder"] == DBNull.Value ? "0" : row["CusOrder"].ToString();// row["CusOrder"].ToString();
                      rbtnsov.SelectedValue = row["OrderVal"] == DBNull.Value ? "1" : row["OrderVal"].ToString(); //row["OrderVal"].ToString();
                      rbtnsnv.SelectedValue = row["NetweightVal"] == DBNull.Value ? "1" : row["NetweightVal"].ToString(); //row["NetweightVal"].ToString();
                      ddlois.SelectedValue = row["sms"] == DBNull.Value ? "0" : row["sms"].ToString(); //row["sms"].ToString();
                      rbtnspv.SelectedValue = row["DrSmpQ"] == DBNull.Value ? "1" : row["DrSmpQ"].ToString(); //row["DrSmpQ"].ToString();
                      rbtncae.SelectedValue = row["CollectedAmount"] == DBNull.Value ? "1" : row["CollectedAmount"].ToString();// row["CollectedAmount"].ToString();
                      rbtnpse.SelectedValue = row["recv"] == DBNull.Value ? "0" : row["recv"].ToString(); //row["recv"].ToString();
                      rbtncse.SelectedValue = row["closing"] == DBNull.Value ? "1" : row["closing"].ToString();// row["closing"].ToString();
                      rbtnondp.SelectedValue = row["OrdAsPrim"] == DBNull.Value ? "0" : row["OrdAsPrim"].ToString(); //row["OrdAsPrim"].ToString();
                      rbtngf.SelectedValue = row["GEOTagNeed"] == DBNull.Value ? "1" : row["GEOTagNeed"].ToString(); //row["GEOTagNeed"].ToString();
                      txtradius.Text = row["DisRad"] == DBNull.Value ? "0" : row["DisRad"].ToString();// row["DisRad"].ToString();
                      lblradius.Text = row["DisRad"] == DBNull.Value ? "0" : row["DisRad"].ToString();// row["DisRad"].ToString();
                      rbtngt.SelectedValue = row["geoTrack"] == DBNull.Value ? "1" : row["geoTrack"].ToString();
                   
                  }

              }
              else
              {
                  rbtnnre.SelectedValue = "1";
                  rbtnnrc.SelectedValue = "1";
                  rbtnde.SelectedValue = "1";
                  rbtnndc.SelectedValue = "1";
                  rbtnjcc.SelectedValue = "1";
                  rbtnrt.SelectedValue = "1";
                  rbtnwis.SelectedValue = "0";
                  rbtnsov.SelectedValue = "1";
                  rbtnsnv.SelectedValue = "1";
                  ddlois.SelectedValue = "0";
                  rbtnspv.SelectedValue = "1";
                  rbtncae.SelectedValue = "1";
                  rbtnpse.SelectedValue = "0";
                  rbtncse.SelectedValue = "1";
                  rbtnondp.SelectedValue = "0";
                  rbtngf.SelectedValue = "1";
                  txtradius.Text = "0";
                  lblradius.Text = "0";
                  rbtngt.SelectedValue = "1";
              }  */
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
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + serverTimeDiff.Ticks + "');</script>");
        time = serverTimeDiff.Minutes;

    }
    [WebMethod]
    public static List<string> getdata()
    {

        List<string> result = new List<string>();
        MasterFiles_Tabctrl ms = new MasterFiles_Tabctrl();
        result = ms.loaddata();
        return result;

    }

    private List<string> loaddata()
    {

        List<string> result = new List<string>();
        divcode = Convert.ToString(Session["div_code"]);
        if (divcode != "" && divcode != null)
        {
            //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + divcode  + "');</script>");


            AdminSetup admin = new AdminSetup();
            accessMas = admin.getapp_Setting(divcode);

            //if (accessMas.Tables[0].Rows.Count > 0)
            //{
            //    rbtnnre.SelectedValue = accessMas.Tables[0].Rows[0].ItemArray.GetValue(6).ToString();
            //}

            foreach (DataRow row in accessMas.Tables[0].Rows)
            {

                //rbtnnre.SelectedValue = row["UNLNeed"] == DBNull.Value ? "1" : row["UNLNeed"].ToString();// row["UNLNeed"].ToString(); 
                //rbtnnrc.SelectedValue = row["NwRoute"] == DBNull.Value ? "1" : row["NwRoute"].ToString(); //row["NwRoute"].ToString();
                //rbtnde.SelectedValue = row["StkNeed"] == DBNull.Value ? "1" : row["StkNeed"].ToString();// row["StkNeed"].ToString();
                //rbtnndc.SelectedValue = row["NwDist"] == DBNull.Value ? "1" : row["NwDist"].ToString();// row["NwDist"].ToString();
                //rbtnjcc.SelectedValue = row["jointwork"] == DBNull.Value ? "1" : row["jointwork"].ToString();// row["jointwork"].ToString();
                //rbtnrt.SelectedValue = row["template"] == DBNull.Value ? "1" : row["template"].ToString(); //row["template"].ToString();
                //rbtnwis.SelectedValue = row["CusOrder"] == DBNull.Value ? "0" : row["CusOrder"].ToString();// row["CusOrder"].ToString();
                //rbtnsov.SelectedValue = row["OrderVal"] == DBNull.Value ? "1" : row["OrderVal"].ToString(); //row["OrderVal"].ToString();
                //rbtnsnv.SelectedValue = row["NetweightVal"] == DBNull.Value ? "1" : row["NetweightVal"].ToString(); //row["NetweightVal"].ToString();
                //ddlois.SelectedValue = row["sms"] == DBNull.Value ? "0" : row["sms"].ToString(); //row["sms"].ToString();
                //rbtnspv.SelectedValue = row["DrSmpQ"] == DBNull.Value ? "1" : row["DrSmpQ"].ToString(); //row["DrSmpQ"].ToString();
                //rbtncae.SelectedValue = row["CollectedAmount"] == DBNull.Value ? "1" : row["CollectedAmount"].ToString();// row["CollectedAmount"].ToString();
                //rbtnpse.SelectedValue = row["recv"] == DBNull.Value ? "0" : row["recv"].ToString(); //row["recv"].ToString();
                //rbtncse.SelectedValue = row["closing"] == DBNull.Value ? "1" : row["closing"].ToString();// row["closing"].ToString();
                //rbtnondp.SelectedValue = row["OrdAsPrim"] == DBNull.Value ? "0" : row["OrdAsPrim"].ToString(); //row["OrdAsPrim"].ToString();
                //rbtngf.SelectedValue = row["GEOTagNeed"] == DBNull.Value ? "1" : row["GEOTagNeed"].ToString(); //row["GEOTagNeed"].ToString();
                //txtradius.Text = row["DisRad"] == DBNull.Value ? "0" : row["DisRad"].ToString();// row["DisRad"].ToString();
                //lblradius.Text = row["DisRad"] == DBNull.Value ? "0" : row["DisRad"].ToString();// row["DisRad"].ToString();
                //rbtngt.SelectedValue = row["geoTrack"] == DBNull.Value ? "1" : row["geoTrack"].ToString();
                result.Add(String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}|{17}|{18}|{19}|{20}|{21}|{22}|{23}|{24}|{25}|{26}|{27}|{28}|{29}|{30}|{31}|{32}|{33}|{34}|{35}|{36}|{37}|{38}", row["UNLNeed"], row["NwRoute"], row["StkNeed"], row["NwDist"], row["jointwork"], row["template"], row["CusOrder"], row["OrderVal"], row["NetweightVal"], row["sms"], row["DrSmpQ"], row["CollectedAmount"], row["recv"], row["closing"], row["OrdAsPrim"], row["GEOTagNeed"], row["DisRad"], row["geoTrack"], row["RetCBNd"], row["RateEditable"], row["OrdRetNeed"], row["DyInvNeed"], row["EdSubCalls"], row["Needed"], row["Mandatory"], row["DTDNeed"], row["InshopND"], row["DistBased"], row["MsdDate"], row["opbal"], row["clbal"], row["OfferMode"], row["PreCall"], row["PhoneOrderND"], row["BatteryStatus"], row["GeoTagPrimary_Nd"], row["Price_category"], row["Near_Me"], row["exp_key"]));
            }

        }
        return result;

    }
    [WebMethod]
    public static string savedata(string data)
    {
        MasterFiles_Tabctrl ms = new MasterFiles_Tabctrl();
        return ms.save(data);
    }

    private string save(string data)
    {

        string[] str = data.Split(',');
        divcode = Convert.ToString(Session["div_code"]);



        byte UNLNeed = Convert.ToByte(str[0]);
        byte NwRoute = Convert.ToByte(str[1]);
        byte StkNeed = Convert.ToByte(str[2]);
        byte NwDist = Convert.ToByte(str[3]);
        byte jointwork = Convert.ToByte(str[4]);
        byte template = Convert.ToByte(str[5]);
        byte CusOrder = Convert.ToByte(str[6]);
        byte OrderVal = Convert.ToByte(str[7]);
        byte NetweightVal = Convert.ToByte(str[8]);
        Int16 sms = Convert.ToInt16(str[9]);
        byte DrSmpQ = Convert.ToByte(str[10]);
        byte CollectedAmount = Convert.ToByte(str[11]);
        byte recv = Convert.ToByte(str[12]);
        byte closing = Convert.ToByte(str[13]);
        byte OrdAsPrim = Convert.ToByte(str[14]);
        byte GEOTagNeed = Convert.ToByte(str[15]);
        decimal DisRad = Convert.ToDecimal(str[16]);
        byte geoTrack = Convert.ToByte(str[17]);


        byte rcstk = Convert.ToByte(str[18]);
        byte sredit = Convert.ToByte(str[19]);
        byte orentry = Convert.ToByte(str[20]);
        byte dlyinven = Convert.ToByte(str[21]);
        byte esubcall = Convert.ToByte(str[22]);

        string Dispaly = str[23].ToString();
        string mandatory = str[24].ToString();


        byte doorTodoor = Convert.ToByte(str[25]);
        byte inshop = Convert.ToByte(str[26]);
        byte distNeed = Convert.ToByte(str[27]);
        byte misDate = Convert.ToByte(str[28]);

        byte opbal = Convert.ToByte(str[29]);
        byte clobal = Convert.ToByte(str[30]);
        byte freeqty = Convert.ToByte(str[31]);
        byte cllPreView = Convert.ToByte(str[32]);

        byte phoneOrder = Convert.ToByte(str[33]);
        byte battory = Convert.ToByte(str[34]);
        byte geoTrackprimary = Convert.ToByte(str[35]);

        byte Price_category = Convert.ToByte(str[36]);
        byte Explorerneed = Convert.ToByte(str[37]);

        string explorerkey = Convert.ToString(str[38]);

        if (divcode != "" && divcode != null)
        {
            //update
            AdminSetup admin = new AdminSetup();
            //   int iReturn = admin.RecordUpdate_App_Setting(Convert.ToByte(rbtnnre.SelectedValue), Convert.ToByte(rbtnnrc.SelectedValue), Convert.ToByte(rbtnde.SelectedValue), Convert.ToByte(rbtnndc.SelectedValue), Convert.ToByte(rbtnjcc.SelectedValue), Convert.ToByte(rbtnrt.SelectedValue), Convert.ToByte(rbtnwis.SelectedValue), Convert.ToByte(rbtnsov.SelectedValue), Convert.ToByte(rbtnsnv.SelectedValue), Convert.ToInt16(ddlois.SelectedValue), Convert.ToByte(rbtnspv.SelectedValue), Convert.ToByte(rbtncae.SelectedValue), Convert.ToByte(rbtnpse.SelectedValue), Convert.ToByte(rbtncse.SelectedValue), Convert.ToByte(rbtnondp.SelectedValue), Convert.ToByte(rbtngf.SelectedValue), Convert.ToDecimal(txtradius.Text.Trim()), Convert.ToByte(rbtngt.SelectedValue), divcode);
            int iReturn = admin.RecordUpdate_App_Setting(UNLNeed, NwRoute, StkNeed, NwDist, jointwork, template, CusOrder, OrderVal, NetweightVal, sms, DrSmpQ, CollectedAmount, recv, closing, OrdAsPrim, GEOTagNeed, DisRad, geoTrack, divcode, rcstk, sredit, orentry, dlyinven, esubcall, Dispaly.Replace("#", ","), mandatory.Replace("#", ","), doorTodoor, inshop, distNeed, misDate, opbal, clobal, freeqty, cllPreView, phoneOrder, battory, geoTrackprimary, Price_category, Explorerneed, explorerkey);
            if (iReturn > 0)
            {
                //      ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
                return "Sucess";
            }
            else
            {
                return "Error";
            }

        }
        else
        {
            //insert 
            AdminSetup admin = new AdminSetup();
            // int iReturn = admin.RecordUpdate_App_Setting(Convert.ToByte(rbtnnre.SelectedValue), Convert.ToByte(rbtnnrc.SelectedValue), Convert.ToByte(rbtnde.SelectedValue), Convert.ToByte(rbtnndc.SelectedValue), Convert.ToByte(rbtnjcc.SelectedValue), Convert.ToByte(rbtnrt.SelectedValue), Convert.ToByte(rbtnwis.SelectedValue), Convert.ToByte(rbtnsov.SelectedValue), Convert.ToByte(rbtnsnv.SelectedValue), Convert.ToInt16(ddlois.SelectedValue), Convert.ToByte(rbtnspv.SelectedValue), Convert.ToByte(rbtncae.SelectedValue), Convert.ToByte(rbtnpse.SelectedValue), Convert.ToByte(rbtncse.SelectedValue), Convert.ToByte(rbtnondp.SelectedValue), Convert.ToByte(rbtngf.SelectedValue), Convert.ToDecimal(txtradius.Text.Trim()), Convert.ToByte(rbtngt.SelectedValue), divcode);
            int iReturn = admin.RecordUpdate_App_Setting(UNLNeed, NwRoute, StkNeed, NwDist, jointwork, template, CusOrder, OrderVal, NetweightVal, sms, DrSmpQ, CollectedAmount, recv, closing, OrdAsPrim, GEOTagNeed, DisRad, geoTrack, divcode, rcstk, sredit, orentry, dlyinven, esubcall, Dispaly.Replace("#", ","), mandatory.Replace("#", ","), doorTodoor, inshop, distNeed, misDate, opbal, clobal, freeqty, cllPreView, phoneOrder, battory, geoTrackprimary, Price_category, Explorerneed, explorerkey);
            if (iReturn > 0)
            {
                //     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been Added Successfully');</script>");
                return "Sucess";
            }
            else
            {
                return "Error";
            }
        }

    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        /*  if (divcode != "" && divcode != null)
          {
              //update
              AdminSetup admin = new AdminSetup();
              int iReturn = admin.RecordUpdate_App_Setting(Convert.ToByte(rbtnnre.SelectedValue), Convert.ToByte(rbtnnrc.SelectedValue), Convert.ToByte(rbtnde.SelectedValue), Convert.ToByte(rbtnndc.SelectedValue), Convert.ToByte(rbtnjcc.SelectedValue), Convert.ToByte(rbtnrt.SelectedValue), Convert.ToByte(rbtnwis.SelectedValue), Convert.ToByte(rbtnsov.SelectedValue), Convert.ToByte(rbtnsnv.SelectedValue), Convert.ToInt16(ddlois.SelectedValue), Convert.ToByte(rbtnspv.SelectedValue), Convert.ToByte(rbtncae.SelectedValue), Convert.ToByte(rbtnpse.SelectedValue), Convert.ToByte(rbtncse.SelectedValue), Convert.ToByte(rbtnondp.SelectedValue), Convert.ToByte(rbtngf.SelectedValue), Convert.ToDecimal(txtradius.Text.Trim()), Convert.ToByte(rbtngt.SelectedValue), divcode);
              if (iReturn > 0)
              {
                  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been updated Successfully');</script>");
              }

          }
          else
          {
              //insert 
              AdminSetup admin = new AdminSetup();
              int iReturn = admin.RecordUpdate_App_Setting(Convert.ToByte(rbtnnre.SelectedValue), Convert.ToByte(rbtnnrc.SelectedValue), Convert.ToByte(rbtnde.SelectedValue), Convert.ToByte(rbtnndc.SelectedValue), Convert.ToByte(rbtnjcc.SelectedValue), Convert.ToByte(rbtnrt.SelectedValue), Convert.ToByte(rbtnwis.SelectedValue), Convert.ToByte(rbtnsov.SelectedValue), Convert.ToByte(rbtnsnv.SelectedValue), Convert.ToInt16(ddlois.SelectedValue), Convert.ToByte(rbtnspv.SelectedValue), Convert.ToByte(rbtncae.SelectedValue), Convert.ToByte(rbtnpse.SelectedValue), Convert.ToByte(rbtncse.SelectedValue), Convert.ToByte(rbtnondp.SelectedValue), Convert.ToByte(rbtngf.SelectedValue), Convert.ToDecimal(txtradius.Text.Trim()), Convert.ToByte(rbtngt.SelectedValue), divcode);
              if (iReturn > 0)
              {
                  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Setup has been Added Successfully');</script>");
              }
          }*/
    }
}