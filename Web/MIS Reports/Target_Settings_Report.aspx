<%@ Page Title="DSR Monitoring Report" Language="C#" MasterPageFile="~/Master.master"
    AutoEventWireup="true" CodeFile="Target_Settings_Report.aspx.cs" Inherits="MIS_Reports_Target_Settings_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/style1.css" />
    <style type="text/css">
        input[type='text'], select, label
        {
            line-height: 22px;
            padding: 4px 6px;
            font-size: medium;
            border-radius: 7px;
            width: 100%;
            font-weight: normal;
        }
        .button
        {
            display: inline-block;
            border-radius: 4px;
            background-color: #6495ED;
            border: none;
            color: #FFFFFF;
            text-align: center;
            font-bold: true;
            width: 75px;
            height: 29px;
            transition: all 0.5s;
            cursor: pointer;
            margin: 5px;
        }
        
        .button span
        {
            cursor: pointer;
            display: inline-block;
            position: relative;
            transition: 0.5s;
        }
        
        .button span:after
        {
            content: '»';
            position: absolute;
            opacity: 0;
            top: 0;
            right: -20px;
            transition: 0.5s;
        }
        
        .button:hover span
        {
            padding-right: 25px;
        }
        
        .button:hover span:after
        {
            opacity: 1;
            right: 0;
        }
        .col-sm-6
        {
            padding: 0px 3px 6px 4px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
        $('form').live("submit", function () {
            ShowProgress();
        });


        function NewWindow() {
            var stcode = $('#<%=ddlstate.ClientID%>').val();
            var stname = $('#<%=ddlstate.ClientID%> :selected').text();
            if (stcode == 0) {
                alert('Select the State');
                return false;
            }
            if ($('#<%=ddlFieldForce.ClientID%>').val().toString() == "---Select Field Force---") { alert("Select FieldForce."); $('#<%=ddlFieldForce.ClientID%>').focus(); return false; }
            var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFMonth').focus(); return false; }
            var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFYear').focus(); return false; }
            var ddlfo_Code = $('#<%=ddlFieldForce.ClientID%>').val();
            var ddlfo_Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            FMonth = $('#<%=ddlFMonth.ClientID%> :selected').val();
            var month_name = $('#<%=ddlFMonth.ClientID%> :selected').text();
            FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
            var sub_div_code = $('#<%=subdiv.ClientID%>').val();
            window.open("rpt_target_setting.aspx?&FMonth=" + FMonth + "&StateCode=" + stcode + "&StateName=" + stname + "&FYear=" + FYear + "&Month_Name=" + month_name + "&SF_code=" + ddlfo_Code + "&SF_Name=" + ddlfo_Name + "&Sub_Div=" + sub_div_code, 'DSRMonitor', 'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=950,height=650,left=0,top=0');
        }
   
    </script>
    <form id="target_form" runat="server">
    <div class="container" style="width:100%">
        <div class="form-group">
               
            <div class="row">
                <label id="ddlDivision" class="col-md-2  col-md-offset-3  control-label">
                    Division</label>
                <%--<asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: right;
                    padding: 8px 4px;" Text="Select Division" CssClass="col-md-4 control-label"></asp:Label>--%>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="subdiv" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style=" min-width: 150px" Width="120" OnSelectedIndexChanged="subdiv_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
			 <div class="row">
                    <label id="Label5" class="col-md-2  col-md-offset-3  control-label">
                        State</label>
                    <div class="col-md-6 inputGroupContainer">
                        <div class="input-group">
                            <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                            <asp:DropDownList ID="ddlstate" runat="server" SkinID="ddlRequired" CssClass="form-control" OnSelectedIndexChanged="ddlstate_SelectIndexchanged"
                                Style="min-width: 150px" Width="120" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            <div class="row">
                <label for="ddlFF" class="col-md-2 col-md-offset-3 control-label">
                    Field Force</label>
                <%--<asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Field Force" Style="text-align: right;
                    padding: 8px 4px;" CssClass="col-md-4 control-label"></asp:Label>--%>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" 
                            CssClass="form-control"  Width="350">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtMonth" class="col-md-2  col-md-offset-3  control-label">
                    Month</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFMonth" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style=" min-width: 100px" Width="120">
                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <label for="txtYear" class="col-md-2 col-md-offset-3  control-label">
                    Year</label>
                <div class="col-md-6 inputGroupContainer">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <asp:DropDownList ID="ddlFYear" runat="server" SkinID="ddlRequired" CssClass="form-control"
                            Style=" min-width: 100px" Width="120">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6 col-md-offset-5">
                    <a id="btnGo" class="btn btn-primary" onclick="NewWindow().this"
                        style="vertical-align: middle;width: 100px">
                        <span>View</span></a>
                </div>
            </div>
        </div>
    </div>
    </form>
</asp:Content>
