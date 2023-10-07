﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MIS_Reports_RetailerAllSal : System.Web.UI.Page
{
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    string sf_code = string.Empty;
    DateTime ServerStartTime;
    DataSet dsTP = null;
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
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillYear();
          //  fillsubdivision();
            FillMRManagers("0");
        }
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());

                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();

            }
        }
        ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    //private void fillsubdivision()
    //{
    //    SalesForce sd = new SalesForce();
    //    dsSalesForce = sd.Getsubdivisionwise(div_code);
    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        subdiv.DataTextField = "subdivision_name";
    //        subdiv.DataValueField = "subdivision_code";
    //        subdiv.DataSource = dsSalesForce;
    //        subdiv.DataBind();
    //        subdiv.Items.Insert(0, new ListItem("--Select--", "0"));

    //    }
    //}
    //protected void subdiv_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (subdiv.SelectedValue.ToString() != "0")
    //    {
    //        FillMRManagers(subdiv.SelectedValue.ToString());
    //    }
    //    else
    //    {
    //        FillMRManagers(subdiv.SelectedValue.ToString());
    //    }
    //}

    private void FillMRManagers(string Sub_Div_Code)
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.SalesForceList(div_code, sf_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select Field Force---", "0"));

        }
    }
}