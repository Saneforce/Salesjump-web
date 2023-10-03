﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControl_TestMenu : System.Web.UI.UserControl
{
    string _isErr;
    string _sURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"];
        if (Session["div_name"] != null)
        {
            //LblDiv.Text = Session["div_name"].ToString();
        }

    }
    string _title;
    public string Title
    {
        get
        {
            return this._title;
        }
        set
        {
            this._title = value;
            lblHeading.Text = value;
        }
    }

    string _status;
    public string Status
    {
        get
        {
            return this._status;
        }
        set
        {
            this._status = value;
            lblStatus.Text = value;
            if (_isErr == "error")
                lblStatus.ForeColor = System.Drawing.Color.Red;
        }
    }


    public string isERR
    {
        get
        {
            return this._isErr;
        }
        set
        {
            this._isErr = value;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["backurl"].ToString());
    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Index.aspx");
    }

}