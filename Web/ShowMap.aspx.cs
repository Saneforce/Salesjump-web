using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Bus_EReport;

public partial class MasterFiles_ShowMap : System.Web.UI.Page
{
    string sf_code = string.Empty;
    string strDate = string.Empty;
    string day = string.Empty;
    string type = string.Empty;
    string comment = string.Empty;
    string lati = string.Empty;
    string longi = string.Empty;
    string geoadd = "";
    string strlongname = "";
    string Locations = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Request.QueryString["sf_Code"].ToString();
        strDate = Request.QueryString["strDate"].ToString();
		
        string sArrStr = getData(sf_code, strDate,0);

        string newWin = "var markers=" + sArrStr;
        ClientScript.RegisterStartupScript(this.GetType(), "mapdata", newWin, true);
    }
	
	public static string getData(string sf_code, string strDate,int fl)
	{
	
	    DataSet dsdoc = new DataSet();
        DCR dc = new DCR();
		dsdoc = dc.get_details_Maps(sf_code, strDate, fl);

		string sArrStr = "";
        if (dsdoc.Tables[0].Rows.Count > 0)
        {
			sArrStr = "[";
			for (int il = 0; il < dsdoc.Tables[0].Rows.Count; il++)
			{
				string Locations =  dsdoc.Tables[0].Rows[il].ItemArray[2].ToString() + "," + dsdoc.Tables[0].Rows[il].ItemArray[3].ToString();
				string getAdd = getadd(Locations);
				sArrStr += "{\"title\": \"" + dsdoc.Tables[0].Rows[il].ItemArray[5].ToString() + "\",";
				sArrStr += "\"lat\": \"" + dsdoc.Tables[0].Rows[il].ItemArray[2].ToString() + "\",";
				sArrStr += "\"lng\": \"" + dsdoc.Tables[0].Rows[il].ItemArray[3].ToString() + "\",";
				sArrStr += "\"dteT\": \"" + dsdoc.Tables[0].Rows[il].ItemArray[1].ToString() + "\",";
				sArrStr += "\"POB\": \"" + dsdoc.Tables[0].Rows[il].ItemArray[6].ToString() + "\",";
				sArrStr += "\"icon\": \"img/" + ((dsdoc.Tables[0].Rows[il].ItemArray[5].ToString() != "") ? "shop.png" : (il==0)?"Man40.png": (il==(dsdoc.Tables[0].Rows.Count - 1))?"Man49.png":"bluedot.gif") + "\",";
				sArrStr += "\"description\": \"" + getAdd + "\"}";
				
				if (il < dsdoc.Tables[0].Rows.Count - 1) sArrStr += ",";
			}
			sArrStr += "]";
		}else{
			sArrStr="[]";
		}
        return sArrStr;
	}

	[WebMethod]
    public static string GetMapData(string sf, string sDt)
    {
        //string sf = Request.QueryString["pSFCode"].ToString();
        //string sDt = Request.QueryString["pRDate"].ToString();
        string JWJSON = getData(sf, sDt,1);
        return JWJSON;
    }
	
    protected void Item_Bound(Object sender, DataListItemEventArgs e)
    {
        foreach (DataListItem item in DataList1.Items)
        {


            day = (item.FindControl("daytime") as Label).Text;
            type = (item.FindControl("cmttype") as Label).Text;
            comment = (item.FindControl("lblInput") as Label).Text;
            lati = (item.FindControl("Lab_lati") as Label).Text;
            longi = (item.FindControl("Lab_long") as Label).Text;
            //geoadd = (item.FindControl("GeoAddrs") as Label).Text;
            if (type == "")
            {
                item.BackColor = System.Drawing.Color.LightYellow;
            }
            if (comment == "")
            {
                item.BackColor = System.Drawing.Color.AliceBlue;
            }
            //string strlongname = "";
            if (type.Trim() == "NA" && lati != "")
            {

                int i = 0;
                XmlDocument doc = new XmlDocument();
                doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lati + "," + longi + "&sensor=false");
                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                foreach (XmlNode xn in xnList)
                {
                    i += 1;
                    if (i < 8)
                    {
                        strlongname += xn["long_name"].InnerText + ",";
                    }

                }

                if (strlongname != "")
                {
                    strlongname = strlongname.Remove(strlongname.Length - 1);
                    Label lblId = (Label)item.FindControl("cmttype");
                    lblId.Text = strlongname;
                }
            }


        }

    }

    public static string getadd(string loc)
    {

        try
        {
            string strlongname = "";


            int i = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + loc + "&sensor=false");
            XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
            XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
            foreach (XmlNode xn in xnList)
            {
                i += 1;
                if (i < 8)
                {
                    strlongname += xn["long_name"].InnerText + ",";
                }

            }

            if (strlongname != "")
            {
               string getAdd = strlongname.Remove(strlongname.Length - 1);
				return getAdd;
            }

        }
        catch (Exception e) { }
		return "";

    }
       
   
}