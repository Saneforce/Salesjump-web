using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bus_Objects;
using System.Data.SqlClient;
using System.Data;

namespace DBase_EReport
{
    public class ChartDAL
    {
        //1
        public ChatBO.HeadDatas get_SFDetails(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();

                SFDets.caption = ("Three Years Product Contribution (CASES) in Total Sale YTD");
                SFDets.subcaption = ("");
              //  SFDets.yaxisname = ("Count");
                SFDets.numvisibleplot = ("12");
               // SFDets.labeldisplay = ("auto");
                SFDets.palettecolors = ("#428bca,f2726f,#5cb85c");
                SFDets.theme = ("fusion");
                SFDets.formatnumberscale = ("0");
                  SFDets.showValues = ("1");  
                SFDets.rotateValues = ("90");               
                 
               
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> getSetups(string Div,string SFCode,string tYear)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@fyear", SFCode),
                    new SqlParameter("@tyear", tYear)
                };

                //DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetChart", CommandType.StoredProcedure, parameters);

                //var SFs = (from w in SFDet.AsEnumerable() select w);

                //foreach (var SF in SFs)
                //{
                //    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                //    nSF.label = SF.ItemArray.GetValue(1).ToString();
                //    nSF.value = SF.ItemArray.GetValue(2).ToString();                    
                //    nSF.displayValue = SF.ItemArray.GetValue(2).ToString() + " ( " + SF.ItemArray.GetValue(3).ToString() + " ) ";
                //    //nSF.link = "newchart-xml-2010Quarters";
                //    //nSF.color = "008ee4";
                //    Setup.Add(nSF);
                //}
              

            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public ChatBO.HeadDatas get_Pur_Brands(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();
                //DataTable SFDet = DL.Exec_DataTableWithParam(" ", CommandType.StoredProcedure, parameters);

                //SFDets.caption = ("Annual Purchase Summary ("+SFCode+")");
                //SFDets.subcaption = ("Purchase Top10 Brands");
                //SFDets.numberprefix = (" ");
                //SFDets.showvalues = ("0");
                //SFDets.bgcolor = ("FFFFFF");
                //SFDets.xaxisname = ("Year -"+SFCode.ToString());
                //SFDets.plotgradientcolor = (" ");
                //SFDets.showalternatehgridcolor = ("0");
                //SFDets.showplotborder = ("0");
                //SFDets.divlinecolor = ("CCCCCC");
                //SFDets.canvasborderalpha = ("0");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                   new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetChart1", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    nSF.displayValue = SF.ItemArray.GetValue(2).ToString() + " ( " + SF.ItemArray.GetValue(3).ToString() + " ) ";
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }
              

            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public ChatBO.HeadDatas get_Pur_Prod(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();
                //DataTable SFDet = DL.Exec_DataTableWithParam(" ", CommandType.StoredProcedure, parameters);

                //SFDets.caption = ("Annual Purchase Summary (" + SFCode + ")");
                //SFDets.subcaption = ("Purchase Top10 Products");
                //SFDets.numberprefix = (" ");
                //SFDets.showvalues = ("0");
                //SFDets.bgcolor = ("FFFFFF");
                //SFDets.xaxisname = ("Year -"+SFCode.ToString());
                //SFDets.plotgradientcolor = (" ");
                //SFDets.showalternatehgridcolor = ("0");
                //SFDets.showplotborder = ("0");
                //SFDets.divlinecolor = ("CCCCCC");
                //SFDets.canvasborderalpha = ("0");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetChart2", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetChart3", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    ////nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }

        //2

        public ChatBO.HeadDatas get_Sale_Cat(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();
                //DataTable SFDet = DL.Exec_DataTableWithParam(" ", CommandType.StoredProcedure, parameters);

                //SFDets.caption = ("Annual Sale Summary (" + SFCode + ")");
                //SFDets.subcaption = ("Sale Top10 Categorys");
                //SFDets.numberprefix = (" ");
                //SFDets.showvalues = ("0");
                //SFDets.bgcolor = ("FFFFFF");
                //SFDets.xaxisname = ("Year -"+SFCode.ToString());
                //SFDets.plotgradientcolor = (" ");
                //SFDets.showalternatehgridcolor = ("0");
                //SFDets.showplotborder = ("0");
                //SFDets.divlinecolor = ("CCCCCC");
                //SFDets.canvasborderalpha = ("0");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_sale_Cat_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetSaleChart1", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(2).ToString();
                    nSF.value = SF.ItemArray.GetValue(0).ToString();
                    //nSF.link = "newchart-xml-2010Quarters";
                    //nSF.color = "6baa01";
                    Setup.Add(nSF);
                }
                DataTable DSDT = new DataTable();
                string sJsonE = "", sJsonV = "";
                var DSRows = (from w in DSDT.AsEnumerable() select w);
                foreach (var rw in DSRows)
                {
                    if (sJsonE != "") { sJsonE += ","; sJsonV += ","; }
                    sJsonE += rw.Field<string>("ES");
                    sJsonV += rw.Field<string>("VS");
                }

            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public ChatBO.HeadDatas get_Sale_Brands(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();
                //DataTable SFDet = DL.Exec_DataTableWithParam(" ", CommandType.StoredProcedure, parameters);

                //SFDets.caption = ("Annual Sale Summary (" + SFCode + ")");
                //SFDets.subcaption = ("Sale Top10 Brands");
                //SFDets.numberprefix = (" ");
                //SFDets.showvalues = ("0");
                //SFDets.bgcolor = ("FFFFFF");
                //SFDets.xaxisname = ("Year -"+SFCode.ToString());
                //SFDets.plotgradientcolor = (" ");
                //SFDets.showalternatehgridcolor = ("0");
                //SFDets.showplotborder = ("0");
                //SFDets.divlinecolor = ("CCCCCC");
                //SFDets.canvasborderalpha = ("0");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Sale_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                   new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetSaleChart2", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(2).ToString();
                    nSF.value = SF.ItemArray.GetValue(0).ToString();
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "6baa01";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public ChatBO.HeadDatas get_Sale_Prod(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();
                //DataTable SFDet = DL.Exec_DataTableWithParam(" ", CommandType.StoredProcedure, parameters);

                //SFDets.caption = ("Annual Sale Summary (" + SFCode + ")");
                //SFDets.subcaption = ("Sale Top10 Products");
                //SFDets.numberprefix = (" ");
                //SFDets.showvalues = ("0");
                //SFDets.bgcolor = ("FFFFFF");
                //SFDets.xaxisname = ("Year -"+SFCode.ToString());
                //SFDets.plotgradientcolor = (" ");
                //SFDets.showalternatehgridcolor = ("0");
                //SFDets.showplotborder = ("0");
                //SFDets.divlinecolor = ("CCCCCC");
                //SFDets.canvasborderalpha = ("0");

            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Sale_Prod_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetSaleChart3", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(0).ToString();
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "6baa01";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public List<ChatBO.MainDatas> get_sale_Cat_item_MGR(string Div, string Year, string Sfcode)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {             
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", Year),
                     new SqlParameter("@SF", Sfcode)

                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetSaleChart1_MGR", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(2).ToString();
                    nSF.value = SF.ItemArray.GetValue(0).ToString();
                    //nSF.link = "newchart-xml-2010Quarters";
                    //nSF.color = "6baa01";
                    Setup.Add(nSF);
                }
                DataTable DSDT = new DataTable();
                string sJsonE = "", sJsonV = "";
                var DSRows = (from w in DSDT.AsEnumerable() select w);
                foreach (var rw in DSRows)
                {
                    if (sJsonE != "") { sJsonE += ","; sJsonV += ","; }
                    sJsonE += rw.Field<string>("ES");
                    sJsonV += rw.Field<string>("VS");
                }

            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        //new chart
        public ChatBO.HeadDatas get_SFDetails1(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();

                SFDets.caption = ("State wise Achievement For Last 3 Years –" + SFCode + "");
                SFDets.subcaption = ("");
            //    SFDets.yaxisname = ("Count");
                SFDets.numvisibleplot = ("12");
               // SFDets.labeldisplay = ("auto");
                SFDets.palettecolors = ("#428bca,f2726f,#5cb85c");
                SFDets.theme = ("fusion");
                SFDets.formatnumberscale = ("0");
                SFDets.showValues = ("1");
                SFDets.rotateValues = ("90");
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<ChatBO.MainDatas> getSetups1(string Div, string SFCode,string tyear, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@tyear", tyear),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetStateChart", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    //nSF.link = "newchart-xml-2010Quarters";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item1(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                   new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetStateChart1", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item1(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetStateChart2", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    ////nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX1(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetStateChart3", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    ////nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        //new Zone
        public ChatBO.HeadDatas get_SFDetails2(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@SF", SFCode)
                };
                DataTable SFDet = new DataTable();

                SFDets.caption = ("Zone wise Achievement For Last 3 Years –" + SFCode + "");
                SFDets.subcaption = ("");
               // SFDets.yaxisname = ("Count");
                SFDets.numvisibleplot = ("12");
               // SFDets.labeldisplay = ("auto");
                SFDets.palettecolors = ("#428bca,f2726f,#5cb85c");
                SFDets.theme = ("fusion");
                SFDets.formatnumberscale = ("0");
                SFDets.showValues = ("1");
                SFDets.rotateValues = ("90");
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<ChatBO.MainDatas> getSetups2(string Div, string SFCode,string tYear, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@tyear", tYear),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetZoneChart", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    //nSF.link = "newchart-xml-2010Quarters";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item2(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                   new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetZoneChart1", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    //nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item2(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetZoneChart2", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    ////nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX2(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setup = new List<ChatBO.MainDatas>();
            //ChatBO.MainDatas Setup = new ChatBO.MainDatas();
            DB_EReporting DL = new DB_EReporting();
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@div_code", Div),
                    new SqlParameter("@year", SFCode),
                    new SqlParameter("@State", state_code)
                };

                DataTable SFDet = DL.Exec_DataTableWithParam("SP_GetZoneChart3", CommandType.StoredProcedure, parameters);

                var SFs = (from w in SFDet.AsEnumerable() select w);

                foreach (var SF in SFs)
                {
                    ChatBO.MainDatas nSF = new ChatBO.MainDatas();

                    nSF.label = SF.ItemArray.GetValue(1).ToString();
                    nSF.value = SF.ItemArray.GetValue(2).ToString();
                    ////nSF.link = "newchart-xml-2010Q1";
                    //nSF.color = "008ee4";
                    Setup.Add(nSF);
                }


            }
            catch { throw; }
            finally { DL = null; }
            return Setup;
        }
    }
}
