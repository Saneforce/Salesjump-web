using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bus_Objects;
using DBase_EReport;
using Newtonsoft.Json;

namespace Bus_EReport
{ 
    //1
    public class ChartBAL
    {
        public ChatBO.HeadDatas get_SFDetails(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_SFDetails(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> getSetups(string Div, string SFCode, string tYear)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.getSetups(Div, SFCode, tYear);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public ChatBO.HeadDatas get_Pur_Brands(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_Pur_Brands(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Bra_item(Div,SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public ChatBO.HeadDatas get_Pur_Prod(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_Pur_Prod(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_item(Div,SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_itemX(Div, SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        //2
        public ChatBO.HeadDatas get_Sale_Cat(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_Sale_Cat(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_sale_Cat_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_sale_Cat_item(Div,SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public ChatBO.HeadDatas get_Sale_Brands(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_Sale_Brands(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Pur_Sale_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Sale_item(Div,SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public ChatBO.HeadDatas get_Sale_Prod(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_Sale_Prod(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }
        public List<ChatBO.MainDatas> get_Sale_Prod_item(string Div, string SFCode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Sale_Prod_item(Div,SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public List<ChatBO.MainDatas> get_sale_Cat_item_MGR(string Div, string Year, string Sfcode)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_sale_Cat_item_MGR(Div, Year, Sfcode);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        //giri
        public ChatBO.HeadDatas get_SFDetails1(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_SFDetails1(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<ChatBO.MainDatas> getSetups1(string Div, string SFCode,string tyear, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.getSetups1(Div, SFCode,tyear, state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item1(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Bra_item1(Div, SFCode, state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item1(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_item1(Div, SFCode, state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX1(string Div, string SFCode, string State_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_itemX1(Div, SFCode, State_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }

        //Zone
        public ChatBO.HeadDatas get_SFDetails2(string SFCode)
        {
            ChatBO.HeadDatas SFDets = new ChatBO.HeadDatas();
            ChartDAL DL = new ChartDAL();
            try
            {
                SFDets = DL.get_SFDetails2(SFCode);
            }
            catch { throw; }
            finally { DL = null; }
            return SFDets;
        }

        public List<ChatBO.MainDatas> getSetups2(string Div, string SFCode,string tYear, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.getSetups2(Div, SFCode, tYear,state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public List<ChatBO.MainDatas> get_Pur_Bra_item2(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Bra_item2(Div, SFCode, state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
        public List<ChatBO.MainDatas> get_Pur_Prod_item2(string Div, string SFCode, string state_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_item2(Div, SFCode, state_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }

        public List<ChatBO.MainDatas> get_Pur_Prod_itemX2(string Div, string SFCode, string State_code)
        {
            List<ChatBO.MainDatas> Setups = new List<ChatBO.MainDatas>();
            ChartDAL DL = new ChartDAL();
            try
            {
                Setups = DL.get_Pur_Prod_itemX2(Div, SFCode, State_code);
            }
            catch { throw; }
            finally { DL = null; }
            return Setups;
        }
    
    }
}
