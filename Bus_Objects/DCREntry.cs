using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
/// <summary>
/// Summary description for DCREntry
/// </summary>

namespace Bus_Objects
{
    public class DCREntry
    {
        public class SFDetails
        {
            public string SFCode { get; set; }
            public string SFName { get; set; }
            public int SFtype { get; set; }
            public byte State { get; set; }
            public int Div { get; set; }
            public string Divs { get; set; }
            public string ETyp { get; set; }
            public string SysIP { get; set; }
            
        }

        public class Worktypes {
            public int Code { get; set; }
            public string Name { get; set; }
            public string ETabs { get; set; }
            public string FWFlg { get; set; }
        }
        public class Clusters
        {
            public decimal Code { get; set; }
            public string Name { get; set; }
        }
        public class DtDetsDCR
        {
            public DateTime DCR_Date { get; set; }
            public string DTRem { get; set; }
            public byte Type { get; set; }
            public DateTime CurrDt { get; set; }
            public DateTime STime { get; set; }
        }

        public class DCRSetup
        {
            public byte TPBased { get; set; }
            public byte DCRAppr { get; set; }
            public string DlyNeed { get; set; }
            public byte DlyDays { get; set; }
            public string DlyHoli { get; set; }
            public byte HosNeed { get; set; }
            public byte StkNeed { get; set; }
            public byte UdrNeed { get; set; }
            public byte ProdMand { get; set; }
            public byte ProdSel { get; set; }
            public byte PQtyZro { get; set; }
            public string POBtype { get; set; }
            public byte TmNeed { get; set; }
            public byte TmMand { get; set; }
            public byte SesNeed { get; set; }
            public byte SesMand { get; set; }
            public byte NoOfDrs { get; set; }
            public byte NoOfChm { get; set; }
            public byte NoOfStk { get; set; }
            public byte NoOfUdr { get; set; }
            public byte NoOfHos { get; set; }
            public int RemLen { get; set; }
            public string HoliAuto { get; set; }
            public string WkOffAuto { get; set; }
            public byte DrRem { get; set; }
            public byte NChm { get; set; }
            public byte NUdr { get; set; }

            public string SDPCap { get; set; }

            public string SDCRE { get; set; }
            public string SDCRV { get; set; }
            


        }
        public class NewCus
        {
            public string id { get; set; }
            public string name { get; set; }
            public string addr { get; set; }
            public vals twn { get; set; }
            public vals Cat { get; set; }
            public vals Spc { get; set; }
            public vals Cla { get; set; }
            public vals Qua { get; set; }
            public string typ { get; set; }
            public string sf { get; set; }
        }
        public class vals
        {            
            public string val { get; set; }
            public string txt { get; set; }

        }
        public class DetailData
        {
            [JsonProperty("ses")]
            public vals ses { get; set; }
            [JsonProperty("tm")]
            public vals tm { get; set; }
            [JsonProperty("cus")]
            public vals cus { get; set; }
            [JsonProperty("pob")]
            public vals pob { get; set; }
            [JsonProperty("jw")]
            public vals jw { get; set; }
            [JsonProperty("prd")]
            public vals prd { get; set; }
            [JsonProperty("inp")]
            public vals inp { get; set; }
            public string rem { get; set; }
            [JsonProperty("fedbk")]
            public vals fedbk { get; set; }
            [JsonProperty("twn")]
            public vals twn { get; set; }
            [JsonProperty("sf")]
            public vals sf { get; set; }
        }
        public class HeadData {
            public string SFCode { get; set; }
            public string SFTyp { get; set; }
            public DateTime EDate { get; set; }
            public int Wtyp { get; set; }
            public string TwnCd { get; set; }
            public string rem { get; set; }
            public string SysIP { get; set; }
            public DateTime STime { get; set; }
            public DateTime ETime { get; set; }
            public byte Div { get; set; }
            public byte DCRType { get; set; }
            public string ETyp { get; set; }
            public string FWFlg { get; set; }
            public string ETabs { get; set; }
        }
        public class DCRDatas : HeadData
        {
            [JsonProperty("Msl")]
            public List<DetailData> MslData { get; set; }
            [JsonProperty("Chm")]
            public List<DetailData> ChmData { get; set; }
            [JsonProperty("Stk")]
            public List<DetailData> StkData { get; set; }
            [JsonProperty("Hos")]
            public List<DetailData> HosData { get; set; }
            [JsonProperty("Udr")]
            public List<DetailData> UdrData { get; set; }
        }

    }

}