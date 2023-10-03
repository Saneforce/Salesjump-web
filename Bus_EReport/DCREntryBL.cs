using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Bus_Objects;
using DBase_EReport;
using Newtonsoft.Json;

/// <summary>
/// Summary description for DCREntry
/// </summary>

namespace Bus_EReport
{
    public class DCREntryBL 
    {
        public DCREntry.SFDetails get_SFDetails(string SFCode)
        {
            DCREntry.SFDetails SFDets = new DCREntry.SFDetails();
            DCREntryDL DL = new DCREntryDL();
            try{
                SFDets = DL.get_SFDetails(SFCode);  
            }catch{throw;}
            finally { DL = null; }
            return SFDets;
        }
        public DCREntry.DCRSetup getSetups(DCREntry.SFDetails SF)
        {
            DCREntry.DCRSetup Setups = new DCREntry.DCRSetup();
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Setups = DL.getSetups(SF);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;            
        }
        public DCREntry.DtDetsDCR GetDCRDtDet(DCREntry.SFDetails SF,DCREntry.DCRSetup Setups)
        {
            DCREntry.DtDetsDCR DCRDtDets = new DCREntry.DtDetsDCR();
            DCREntryDL DL = new DCREntryDL();
            try
            {
                DCRDtDets = DL.GetDCRDtDet(SF, Setups);
            }
            catch { throw; }
            finally { DL = null; }
            return DCRDtDets;
        }
        public List<DCREntry.SFDetails> get_BaseSFs(string SFCode)
        {
            List<DCREntry.SFDetails> BSFs = new List<DCREntry.SFDetails>();
            DCREntryDL DL = new DCREntryDL();
            try
            {
                BSFs = DL.get_BaseSFs(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return BSFs;
        }
        public List<DCREntry.Worktypes> get_WorkTypes(string SFCode)
        {
            List<DCREntry.Worktypes> WrkTys = new List<DCREntry.Worktypes>();
            DCREntryDL DL = new DCREntryDL();
            try
            {
                WrkTys = DL.get_WorkTypes(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return WrkTys;
        }

        public List<DCREntry.Clusters> getClusters(string SFCode)
        {
            List<DCREntry.Clusters> Twns = new List<DCREntry.Clusters>();
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Twns = DL.getClusters(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Twns;
        }
        public string getClustersJSON(string SFCode)
        {
            string Twns = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Twns = DL.getClustersJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Twns;
        }
        public string getProdsJSON(string SFCode)
        {
            string Prods = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Prods = DL.getProdsJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Prods;
        }
        public string getInputJSON(string SFCode)
        {
            string inputs = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                inputs = DL.getInputJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return inputs;
        }
        public string getFeedBkJSON(int div)
        {
            string inputs = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                inputs = DL.getFeedBkJSON(div);
            }
            catch { throw; }
            finally { DL = null; }
            return inputs;
        }
        public string getCateJSON(int div)
        {
            string sJson = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sJson = DL.getCateJSON(div);
            }
            catch { throw; }
            finally { DL = null; }
            return sJson;
        }
        public string getSpecJSON(int div)
        {
            string sJson = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sJson = DL.getSpecJSON(div);
            }
            catch { throw; }
            finally { DL = null; }
            return sJson;
        }
        public string getClaJSON(int div)
        {
            string sJson = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sJson = DL.getClaJSON(div);
            }
            catch { throw; }
            finally { DL = null; }
            return sJson;
        }
        public string getQualJSON(int div)
        {
            string sJson = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sJson = DL.getQualJSON(div);
            }
            catch { throw; }
            finally { DL = null; }
            return sJson;
        }
        public string getDoctorJSON(string SFCode)
        {
            string sDrs = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sDrs = DL.getDoctorJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return sDrs;
        }
        public string getChemistJSON(string SFCode)
        {
            string sChm = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sChm = DL.getChemistJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return sChm;
        }
        public string getStockistJSON(string SFCode)
        {
            string sStk = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sStk = DL.getStockistJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return sStk;
        }
        public string getHospJSON(string SFCode)
        {
            string sHos = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sHos = DL.getHospJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return sHos;
        }
        public string getUnlistedDrJSON(string SFCode)
        {
            string sUnld = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sUnld = DL.getUnlistedDrJSON(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return sUnld;
        }
        public string getJntWrkJSON(string BSF,string ESF)
        {
            string sJW = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                sJW = DL.getJntWrkJSON(BSF,ESF);
            }
            catch { throw; }
            finally { DL = null; }
            return sJW;
        }

        public Boolean deleteEntryTemp(string SFCode, DateTime Dt)
        {
            Boolean Flag;
            Flag = false;
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Flag = DL.deleteEntryTemp(SFCode, Dt);
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean updateType(string SFCode, DateTime Dt, byte type)
        {
            Boolean Flag;
            Flag = false;
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Flag = DL.updateType(SFCode, Dt, type);
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public string getDCRDetails(string SF, DateTime Dt)
        {
            string DCRJSON = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                DCRJSON = DL.getDCRDetails(SF, Dt);

            }
            catch { throw; }
            finally { DL = null; }
            return DCRJSON;
        }
        public Boolean udpDCRDt(string SFCode, DateTime Dt)
        {
            Boolean Flag;
            Flag = false;
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Flag = DL.udpDCRDt(SFCode, Dt);
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public Boolean ApproveDCR(string SFCode, DateTime Dt)
        {
            Boolean Flag;
            Flag = false;
            DCREntryDL DL = new DCREntryDL();
            try
            {
                Flag = DL.ApproveDCR(SFCode, Dt);
            }
            catch { throw; }
            finally { DL = null; }
            return Flag;
        }
        public string SaveDCRTemp(DCREntry.DCRDatas HeadDta)
        {
            string ARCd = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                ARCd = DL.SaveDCRTemp(HeadDta);

            }
            catch { throw; }
            finally { DL = null; }
            return ARCd;
        }
        public string SaveDCRDetTemp(string ARCd, string SFCode, byte Typ, byte Div, string ETyp,DateTime EDt, DCREntry.DetailData DtaDet,DCREntry.NewCus nCus=null)
        {
            string ARMsl = "";
            DCREntryDL DL = new DCREntryDL();
            try
            {
                ARMsl = DL.SaveDCRDetTemp(ARCd, SFCode, Typ, Div, ETyp, EDt, DtaDet, nCus);

            }
            catch { throw; }
            finally { DL = null; }
            return ARMsl;
        }
    }

}