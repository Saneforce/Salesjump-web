<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    CodeFile="CustomerGeotagging.aspx.cs" Inherits="MasterFiles_CustomerGeotagging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="../JsFiles/amcharts.js" type="text/javascript"></script>
    <script src="../JsFiles/serial.js" type="text/javascript"></script>
    <script src="../JsFiles/light.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }

        function NewWindow() {
            var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (st == "---Select Field Force---") { alert("Select Manager ..!"); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }

            var st = $('#<%=ddlFieldForce.ClientID%> :selected').val();
            var sf_name = $('#<%=ddlFieldForce.ClientID%> :selected').text();

            var str = $('#<%=ddlMR.ClientID%> :selected').text();
            if (str == "---Select Base Level---") { alert("Select Field Force..!"); $('#<%=ddlMR.ClientID%>').focus(); return false; }

            var str = $('#<%=ddlMR.ClientID%> :selected').text();
            if (str == "") { alert("Select Field Force..!"); $('#<%=ddlMR.ClientID%>').focus(); return false; }
            var str = $('#<%=ddlMR.ClientID%> :selected').val();
            var MR_name = $('#<%=ddlMR.ClientID%> :selected').text();


            var subDiv = $('#<%=subdiv.ClientID%>').val();

            var types = $('#<%=DropDownList1.ClientID%>').val();

            if (types == "1") {
                window.open("rptDistributorGeotagging.aspx?SF_Code=" + str + "&SF_Name=" + MR_name
                + "&subDiv=" + subDiv
                , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=750,left=0,top=0');
            }
            else {

                window.open("rptCustomerGeotagging.aspx?SF_Code=" + str + "&SF_Name=" + MR_name
                + "&subDiv=" + subDiv
                , null, 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=750,left=0,top=0');
            }
        }



    </script>
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
        
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
    </style>
    <form id="form1" runat="server">
    <div class="container" style="width: 100%">
        <div class="row">
            <label id="Label1" class="col-md-2 col-md-offset-3 control-label">
                Division</label>
            <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="subdiv" runat="server" CssClass="form-control" Width="120"
                        OnSelectedIndexChanged="subdiv_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblFF" class="col-md-2 col-md-offset-3 control-label">
                Manager</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group" id="kk" runat="server">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlFFType" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="form-control"
                        Visible="false" Width="350">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" CssClass="form-control"
                        Width="70">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                        CssClass="form-control" Width="350">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"
                        CssClass="form-control" Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="lblMR" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Field Force</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="ddlMR" runat="server" Visible="false" CssClass="form-control"
                        Width="350">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <label id="Label2" runat="server" class="col-md-2 col-md-offset-3 control-label">
                Select Type</label>
            <div class="col-md-5 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <asp:DropDownList ID="DropDownList1" runat="server" Visible="true" CssClass="form-control"
                        Width="350">
                        <asp:ListItem Text="Distributor" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Retailer" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-5">
                <a id="btnView" class="btn btn-primary" onclick="NewWindow()" style="vertical-align: middle;
                    width: 100px"><span>View</span></a>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
