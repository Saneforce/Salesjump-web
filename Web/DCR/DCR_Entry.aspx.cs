using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Text;
using Newtonsoft.Json;
using Bus_Objects;
using Bus_EReport;

public partial class DCR_DCR_Entry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            string SFCode = Session["sf_code"].ToString();
            DCREntryBL BL=new DCREntryBL();
            DCREntry.SFDetails SFDet = BL.get_SFDetails(SFCode);
            hSF_Code.Value = SFDet.SFCode;
            hSFTyp.Value = SFDet.SFtype.ToString();
            hDiv.Value = SFDet.Div.ToString();
            SFInf.InnerHtml = SFDet.SFName;

            SFDet.SysIP = Request.ServerVariables["REMOTE_ADDR"];
            if (Request.Browser.IsMobileDevice) SFDet.ETyp = "Mobile"; else SFDet.ETyp = "Desktop";

            DCREntry.DCRSetup Setup = BL.getSetups(SFDet);
            
            var DtDet = BL.GetDCRDtDet(SFDet,Setup);
            Path.GetFileName(Request.Url.AbsolutePath);
            DtInf.InnerHtml = DtDet.DCR_Date.ToString("dd/MM/yyyy") + " - " + DtDet.DCR_Date.DayOfWeek.ToString() + " " + ((DtDet.DTRem != "") ? " - <span class='stat"+DtDet.Type+"'>" + DtDet.DTRem+"</span>" : "");

            string scrpt = "clearMasterData();setData('SFDet'," + JsonConvert.SerializeObject(SFDet) + ");setData('Setup'," + JsonConvert.SerializeObject(Setup) + ");";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);
            
            hDCRDt.Value = DtDet.DCR_Date.ToString("MM/dd/yyyy");
            hDtTyp.Value = DtDet.Type.ToString();
            hSTime.Value = DtDet.STime.ToString("yyyy-MM-dd HH:mm:ss");
            hCurrDt.Value = DtDet.CurrDt.ToString("MM/dd/yyyy");
            Plchold_HQ.Visible = false;
            if (SFDet.SFtype != 1)
            {
                Plchold_HQ.Visible = true;
                aHome.HRef = "../MGR_Home.aspx";
            }
            else
                aHome.HRef = "../default_MR.aspx";

            List<DCREntry.Worktypes> WTs = BL.get_WorkTypes(SFDet.SFCode);
            ddl_WorkType.DataSource = WTs;
            ddl_WorkType.DataValueField = "Code";
            ddl_WorkType.DataTextField = "Name";
            ddl_WorkType.DataBind();

            for (int il = 0; il < ddl_WorkType.Items.Count; il++)
            {
                ListItem item = ddl_WorkType.Items[il];
                item.Attributes["data-eTabs"] = WTs[il].ETabs; 
                item.Attributes["data-FWFlg"] = WTs[il].FWFlg;
            }

            ListItem DfItm = new ListItem("-- Select The Work Type --", "");
            ddl_WorkType.Items.Insert(0, DfItm);
            string ScriptValues = "";
            if (DtDet.Type > 0 && DtDet.Type < 3) {
                string DCRJSON = BL.getDCRDetails(SFDet.SFCode, DtDet.DCR_Date);
                ScriptValues += "$(window).bind('load', function () {setExistData('" + DCRJSON + "');});";
            }
            if (SFDet.SFtype == 1)
            {
                ddl_HQ.Items.Clear();
                DfItm = new ListItem("-- Select The Headquater --", "");
                ddl_HQ.Items.Insert(0, DfItm);
                DfItm = new ListItem(SFDet.SFName, SFDet.SFCode);
                DfItm.Attributes["data-sfdiv"] = SFDet.Div.ToString();
                ddl_HQ.Items.Insert(0, DfItm);
                ddl_HQ.SelectedValue = SFDet.SFCode;

                List<DCREntry.Clusters> Twns = BL.getClusters(SFDet.SFCode);
                ddl_SDP.DataSource = Twns;
                ddl_SDP.DataValueField = "Code";
                ddl_SDP.DataTextField = "Name";
                ddl_SDP.DataBind();
                string sTwns = JsonConvert.SerializeObject(Twns);
                ScriptValues += "d_twns=" + sTwns.Replace("\"Code\"", "\"id\"").Replace("\"Name\"", "\"name\"") + ";setData('d_twn_" + SFCode + "',d_twns);";
            }
            else
            {
                List<DCREntry.SFDetails> BSFs = BL.get_BaseSFs(SFDet.SFCode);
                ddl_HQ.DataSource = BSFs;
                ddl_HQ.DataValueField = "SFCode";
                ddl_HQ.DataTextField = "SFName";
                ddl_HQ.DataBind();
                for (int il = 0; il < ddl_HQ.Items.Count; il++)
                {
                    ListItem item = ddl_HQ.Items[il];
                    item.Attributes["data-sfdiv"] = BSFs[il].Div.ToString();
                }
                DfItm = new ListItem("-- Select The Headquater --", "");
                ddl_HQ.Items.Insert(0, DfItm);
                
            }

            DfItm = new ListItem("-- Select The " + Setup.SDPCap + " Name --", "");
            ddl_SDP.Items.Insert(0, DfItm);
            lbSDP.InnerHtml = Setup.SDPCap + " Name";
            string prodJSON = BL.getProdsJSON(SFDet.SFCode);
            string inputJSON = BL.getInputJSON(SFDet.SFCode);
            string FedBkJSON = BL.getFeedBkJSON(SFDet.Div);
            ScriptValues += "d_Prod = " + prodJSON + ";d_Input = " + inputJSON + ";d_fedbk=" + FedBkJSON + ";setData('d_Prod',d_Prod);setData('d_Input',d_Input);setData('d_fedbk',d_fedbk);";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DatasLoad", ScriptValues, true);
        }
    }
    [WebMethod]
    public static string GetClusterList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string TwnJSON = BL.getClustersJSON(SF);
        return TwnJSON;
    }
    [WebMethod]
    public static string GetDoctorList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string DocJSON = BL.getDoctorJSON(SF);
        return  DocJSON; 
    }
    [WebMethod]
    public static string GetChemistList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string ChmJSON = BL.getChemistJSON(SF);
        return ChmJSON;
    }
    [WebMethod]
    public static string GetStockistList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string StkJSON = BL.getStockistJSON(SF);
        return StkJSON;
    }
    [WebMethod]
    public static string GetUnlistedDrList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string StkJSON = BL.getUnlistedDrJSON(SF);
        return StkJSON;
    }
    [WebMethod]
    public static string GetJntWrkList(string BSF,string ESF)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getJntWrkJSON(BSF,ESF);
        return JWJSON;
    }
    [WebMethod]
    public static string GetHospitalList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getHospJSON(SF);
        return JWJSON;
    }


    [WebMethod]
    public static string GetCateList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getCateJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetSpecList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getSpecJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetClaList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getClaJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetQualList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getQualJSON(Div);
        return JWJSON;
    }
    
    [WebMethod]
    public static string SaveDCR(string SFData, string DCRData, string sSetup, IList<DCREntry.NewCus> nData)
    {
        
        DCREntryBL BL = new DCREntryBL();
        DCREntry.DCRDatas Data = JsonConvert.DeserializeObject<DCREntry.DCRDatas>(DCRData);
        DCREntry.DCRSetup Setup = JsonConvert.DeserializeObject<DCREntry.DCRSetup>(sSetup);
        DCREntry.SFDetails SFDet = JsonConvert.DeserializeObject<DCREntry.SFDetails>(SFData);
        IList<DCREntry.NewCus> nDet = nData;
        Data.Div = (byte)SFDet.Div;
        try
        {
           
            BL.deleteEntryTemp(Data.SFCode, Data.EDate);
            string Detstr = "[";
            string ARCd = BL.SaveDCRTemp(Data);
            if (Data.FWFlg == "F")
            {
                if (Data.ETabs.IndexOf("D") > -1)
                {
                    for (int il = 0; il < Data.MslData.Count; il++)
                    {
                        DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.MslData[il].cus.val);
                        if (Detstr != "[") Detstr += ",";
                        Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 1, Data.Div, Data.ETyp, Data.EDate, Data.MslData[il], nDta);
                    }
                }
                if (Data.ETabs.IndexOf("C") > -1)
                {
                    for (int il = 0; il < Data.ChmData.Count; il++)
                    {
                        DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.ChmData[il].cus.val);
                        if (Detstr != "[") Detstr += ",";
                        Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 2, Data.Div, Data.ETyp, Data.EDate, Data.ChmData[il], nDta);

                    }
                }
                if (Data.ETabs.IndexOf("S") > -1)
                {
                    for (int il = 0; il < Data.StkData.Count; il++)
                    {
                        if (Detstr != "[") Detstr += ",";
                        Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 3, Data.Div, Data.ETyp, Data.EDate, Data.StkData[il]);
                    }
                }
                if (Data.ETabs.IndexOf("U") > -1)
                {
                    for (int il = 0; il < Data.UdrData.Count; il++)
                    {
                        DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.UdrData[il].cus.val);
                        if (Detstr != "[") Detstr += ",";
                        Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 4, Data.Div, Data.ETyp, Data.EDate, Data.UdrData[il], nDta);
                    }
                }
                if (Data.ETabs.IndexOf("H") > -1)
                {
                    for (int il = 0; il < Data.HosData.Count; il++)
                    {
                        DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.HosData[il].cus.val);
                        if (Detstr != "[") Detstr += ",";
                        Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 5, Data.Div, Data.ETyp, Data.EDate, Data.HosData[il],nDta);
                    }
                }
            }
            Detstr += "]";
            if (Data.DCRType == 0)
            {
                BL.udpDCRDt(Data.SFCode, Data.EDate.AddDays(1));
            }
            else
                BL.updateType(Data.SFCode, Data.EDate, Data.DCRType);

            if (Setup.DCRAppr == 0)
            {
                BL.ApproveDCR(Data.SFCode, Data.EDate);
            }
            var DtDet = BL.GetDCRDtDet(SFDet, Setup);
        
            string JWJSON = "{\"success\":true,";
            JWJSON = JWJSON + "\"NextDat\":" + JsonConvert.SerializeObject(DtDet) + ",";
            JWJSON = JWJSON + "\"newCus\":" + Detstr + "}"; //BL.getJntWrkJSON(BSF, ESF);            
            return JWJSON;
            
        }
        catch(Exception ex) {
            var errmsg = "SF :" + SFDet.SFCode + " Call Date : " + Data.EDate + "\r\nmsg:" + ex.Message + "\r\nData:" + DCRData;
            Log(errmsg);
            throw ex; }
        finally { BL = null; }
    }

    public static void Log(string logMessage)
    {
        using (StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath("/DCR")+"/log/log" + DateTime.Today.ToString("yyy_MM_dd") + ".txt"))
        {
            w.WriteLine("Log Entry :{0} {1} {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), logMessage);
            w.WriteLine("-------------------------------");
        }
    }
}