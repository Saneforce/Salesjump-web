using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Bus_EReport;

public partial class MasterFiles_DCR_ShowMap : System.Web.UI.Page
{
    DataSet dsdoc = new DataSet();
    string sf_code = string.Empty;
    string strDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DCR dc = new DCR();

        sf_code = Request.QueryString["sf_Code"].ToString();
        strDate = Request.QueryString["strDate"].ToString();

        dsdoc = dc.get_dcr_details_Maps(sf_code, strDate, 1);
        if (dsdoc.Tables[0].Rows.Count > 0)
        {
            GVMap.DataSource = dsdoc;
            GVMap.DataBind();



           
        }

    }

    protected void GVMap_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lbllati = (TextBox)e.Row.FindControl("lati");
            TextBox lbllong = (TextBox)e.Row.FindControl("long");
            TextBox lblGeoAddrs = (TextBox)e.Row.FindControl("GeoAddrs");

            string strlongname = "";
            if (lblGeoAddrs.Text.Trim() == "NA" && lbllati.Text != "")
            {

                int i = 0;
                XmlDocument doc = new XmlDocument();
                doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + lbllati.Text + "," + lbllong.Text + "&sensor=false");
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
                    lblGeoAddrs.Text = strlongname;
                }
            }
           

            

        }
    }
   
}