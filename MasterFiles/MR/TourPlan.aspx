<%@ Page Title="TP Entry" Language="C#" MasterPageFile="~/Master_MR.master" AutoEventWireup="true" CodeFile="TourPlan.aspx.cs" Inherits="MasterFiles_MR_TourPlan" %>

<%--<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu1" TagPrefix="m1" %>
<%@ Register Src="~/UserControl/MR_TP_Menu.ascx" TagName="Menu2" TagPrefix="m2" %>
<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu3" TagPrefix="m3" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>TP Entry</title>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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
        
        .c1
        {
            width: 280px;
            height: 240px;
        }
        
        .modalDialog
        {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(0, 0, 0, 0.8);
            z-index: 99999;
            opacity: 0;
            -webkit-transition: opacity 400ms ease-in;
            -moz-transition: opacity 400ms ease-in;
            transition: opacity 400ms ease-in;
            pointer-events: none;
        }
        .modalDialog:target
        {
            opacity: 1;
            pointer-events: auto;
        }
        .modalDialog > div
        {
            width: 400px;
            position: relative;
            margin: 10% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
            background: #fff;
            background: -moz-linear-gradient(#fff, #999);
            background: -webkit-linear-gradient(#fff, #999);
            background: -o-linear-gradient(#fff, #999);
        }
        .close
        {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }
        .close:hover
        {
            background: #00d9ff;
        }
        .p
        {
            font-family: Calibri;
            font-size: 14px;
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
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Submit ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                            var ddldis = document.getElementById('grdTP_ctl' + index + '_ddldis');
                            var ddlRou = document.getElementById('grdTP_ctl' + index + '_ddlRou');
                            var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                            if (ddlWT.value == '0') {
                                //isEmpty = true;
                                alert('Select Work Type')
                                ddlWT.focus();
                                return false;
                            }
                            if (drpDownListValue == 'Field Work') {
                                if (ddldis.value == '0') {
                                    alert('Select Distributor')
                                    ddldis.focus();
                                    return false;
                                }
                            }
                            if (drpDownListValue == 'Field Work') {
                                if (ddlRou.value == '0') {
                                    alert('Select Route')
                                    ddlRou.focus();
                                    return false;
                                }
                            }

                        }
                    }
                }

                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do You want to Submit?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                    return false;
                }
                document.forms[0].appendChild(confirm_value);

            }
        }

        function ValidateEmptyValueApprove() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';

                for (i = 2; i < Incre; i++) {
                    if (Inputs[i].type != '') {

                        if (Inputs[i].type == 'text') {
                            if (i.toString().length == 1) {
                                index = cnt.toString() + i.toString();
                            }
                            else {
                                index = i.toString();
                            }

                            var ddlWT = document.getElementById('grdTP_ctl' + index + '_ddlWT');
                            var ddldis = document.getElementById('grdTP_ctl' + index + '_ddldis');
                            var ddlRou = document.getElementById('grdTP_ctl' + index + '_ddlRou');
                            var drpDownListValue = ddlWT.options[ddlWT.selectedIndex].innerHTML;

                            if (ddlWT.value == '0') {
                                //isEmpty = true;
                                alert('Select Work Type')
                                ddlWT.focus();
                                return false;
                            }
                            if (drpDownListValue == 'Field Work') {
                                if (ddldis.value == '0') {
                                    alert('Select Distributor')
                                    ddldis.focus();
                                    return false;
                                }
                            }
                            if (drpDownListValue == 'Field Work') {
                                if (ddlRou.value == '0') {
                                    alert('Select Route')
                                    ddlRou.focus();
                                    return false;
                                }
                            }

                        }
                    }
                }

                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to Approve?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);

            }
        }
    </script>
    <script type="text/javascript">
        function ClearValidate() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Clear?")) {
                confirm_value.value = "Yes";
            }
            else {
                confirm_value.value = "No";
                return false;
            }
            document.forms[0].appendChild(confirm_value);
        }
        function ToggleOnOff(ddonoff) {
            var grid = document.getElementById('#<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr

                                .find('select[id*="ddldis"]')//select by id

                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr

                                .find('select[id*="ddlRou"]')//select by id

                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlDis"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlRou"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', true)  //disable-enable  
                                .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlDis"]'));
                            }
                        }
                    }
                }
            }

        }
        function ToggleOnOff1(ddonoff) {
            var grid = document.getElementById('#<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', false)  //disable-enable  
                                .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr1"]'));
                            }
                        }
                    }
                }
            }

        }
        function ToggleOnOff2(ddonoff) {
            var grid = document.getElementById('#<%=grdWorkType.ClientID%>');
            if (grid != null) {
                if (grid.rows.length > 0) {
                    for (i = 2; i < grid.rows.length + 1; i++) {
                        var cnt = 0;
                        var index = '';

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var code = document.getElementById('grdWorkType_ctl' + index + '_lblCode');
                        var place = document.getElementById('grdWorkType_ctl' + index + '_lblplace');
                        var fwd = document.getElementById('grdWorkType_ctl' + index + '_lblFWInd');
                        if ($(ddonoff).val() == code.innerHTML) {
                            if (place.innerHTML == "N") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'LightGray')
                                .val('0')
                                .attr('title', "Disabled!!")
                                .attr('disabled', true); //disable-enable
                                break;
                            }
                            else if (fwd.innerHTML != "F") {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', false);  //disable-enable     
                            }
                            else {
                                $(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]')//select by id
                                .css('backgroundColor', 'white')
                                .attr('title', "--Select--")
                                .attr('disabled', false)  //disable-enable  
                                .empty();
                                $('#ddlAllTerr option').clone().appendTo($(ddonoff)
                                .parent()//td
                                .parent()//tr
                                .find('select[id*="ddlTerr2"]'));
                            }
                        }
                    }
                }
            }

        }
    </script>
    <script type="text/javascript">
        function DraftValidateEmptyValue() {
            var grid = document.getElementById('<%= grdTP.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var Incre = Inputs.length;
                var cnt = 0;
                var index = '';


                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to Save as a Draft ?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                    return false;

                }
                document.forms[0].appendChild(confirm_value);

            }
        }
    </script>
    <form id="form1" runat="server">
    <div>
        <%--<m1:Menu1 ID="menu1" runat="server" />
        <m2:Menu2 ID="menu2" runat="server" />
        <m3:Menu3 ID="menu3" runat="server" />--%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <center>
            <table id="tblMargin" runat="server" width="100%">
                <tr>
                    <td>
                        <a href="#openModal" style="color: Red; font-size: 12px" shape="circle">
                            <img src="../../Images/help_animated.gif" alt="" /></a>
                        <div id="openModal" class="modalDialog">
                            <div>
                                <a href="#close" title="Close" class="close">X</a>
                                <h2 style="color: Red; font-weight: bold">
                                    TP - Entry / Edit</h2>
                                <p class="p">
                                    1. Fill Your "TP" for all days and Press "Send to Manager Approval" Button for Manager
                                    Approval.</p>
                                <p class="p">
                                    2. After Approval From Your Manager, then next Month "TP" will open.</p>
                                <p class="p">
                                    3. After Selecting the "Field Work" , the Area will appear for Selection for the
                                    Particular Day. Whichever having the Customers, those area only will appear.
                                </p>
                                <p class="p">
                                    4. &quot;Without Customer Areas" - will not reflect in your TP- Entry.
                                </p>
                                <p class="p">
                                    5. For Other Worktypes, not Possible to Select the Areas. The "Selection box" will
                                    be in "Disable" Mode.</p>
                                <p class="p">
                                    6. Before Approval from your Manager, You can Edit your TP for the Particular Month.</p>
                                <p class="p">
                                    7. After Approval from your Manager, the Fieldforce cannot Edit their TP. Get the
                                    Permission from "Admin", then the Fiedlforce can Edit their "TP" for the required
                                    month.</p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr valign="bottom">
                    <td colspan="5" align="center">
                        <asp:Label ID="lblHead" runat="server" Text="Tour Plan for the Month of " Font-Size="Medium"
                            Font-Names="Verdana"></asp:Label>
                        <asp:Label ID="lblmon" runat="server" Font-Size="Medium" Font-Names="Verdana" ForeColor="Green"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblLink" runat="server" Font-Size="Small" Font-Names="Verdana" ForeColor="Black"></asp:Label>
                        <asp:HyperLink ID="hylEdit" runat="server" NavigateUrl="~/MasterFiles/MR/TourPlan.aspx?Edit=E"
                            Font-Size="Small" Font-Names="Verdana" ForeColor="Blue"></asp:HyperLink>
                    </td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:Label ID="lblDeactivate" Font-Bold="true" ForeColor="Red" Font-Names="Verdana"
                            Visible="false" Font-Size="8" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table>
                <asp:Panel ID="Panel1" runat="server" Style="text-align: left;">
                    <asp:Label ID="lblReason" runat="server" Style="text-align: left" Font-Size="Small"
                        Font-Names="Verdana" Visible="false"></asp:Label>
                </asp:Panel>
                
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStatingDate" Visible="false" Font-Names="Verdana" runat="server"></asp:Label>
                        &nbsp;&nbsp
                        <asp:Label ID="lblNote" runat="server" Style="text-decoration: underline;" ForeColor="Red"
                            Font-Size="Small" Font-Names="Verdana" Text="Rejection Reason" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="lblFieldForce" SkinID="lblMand" runat="server" Font-Size="Small" Font-Names="Verdana"
                            Font-Bold="true"></asp:Label>
                        <asp:DropDownList ID="ddlAllTerr" runat="server" Style="display: none;">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdTP" runat="server" Width="85%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                    GridLines="None" CssClass="mGridImg" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("TP_Date") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDay" runat="server" Text='<%#  Eval("TP_Day") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff(this)"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Distributor Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddldis" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillDSM() %>"
                                                    DataTextField="Stockist_Name" DataValueField="Distributor_Code" onchange="ToggleOnOff(this)"
                                                    AutoPostBack="true" OnSelectedIndexChanged="select_indexchange">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlRou" Width="230" runat="server" SkinID="ddlRequired" DataTextField="Territory_Name"
                                                    DataValueField="Territory_Code" Enabled="true" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Route" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="true">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT1" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff1(this)">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Distributor Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddldis1" runat="server" SkinID="ddlRequired" Width="150px"
                                                    DataSource="<%# FillDSM() %>" DataTextField="Stockist_Name" DataValueField="Distributor_Code"
                                                    onchange="ToggleOnOff(this)">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Route Plan 2" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr1" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="false">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Work Type" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlWT2" runat="server" SkinID="ddlRequired" Width="150px" DataSource="<%# FillWorkType() %>"
                                                    DataTextField="WorkType_Name_B" DataValueField="WorkType_Code_B" onchange="ToggleOnOff2(this)">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Distributor Name" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddldis2" runat="server" SkinID="ddlRequired" Width="150px"
                                                    DataSource="<%# FillDSM() %>" DataTextField="Stockist_Name" DataValueField="Distributor_Code"
                                                    onchange="ToggleOnOff(this)">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderText="Route Plan 3" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr2" Width="230" runat="server" SkinID="ddlRequired" DataSource="<%# FillTerritory() %>"
                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" Enabled="false">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Objective" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtObjective" runat="server" SkinID="MandTxtBox" Width="250">                                           
                                                </asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnClear" CssClass="BUTTON" Width="85px" Height="26px" runat="server"
                            Text="Clear" OnClick="btnClear_Click" OnClientClick="return ClearValidate()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="85px" Height="26px"
                            Text="Draft Save" OnClick="btnSave_Click" OnClientClick="return DraftValidateEmptyValue()" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" CssClass="BUTTON" Width="175px" Height="26px" runat="server"
                            Text="Send to Manager Approval" OnClick="btnSubmit_Click" OnClientClick="return ValidateEmptyValue()" />
                    </td>
                </tr>
            </table>
        </center>
        <div style="margin-left: 40%">
            <asp:Button ID="btnApprove" CssClass="BUTTON" runat="server" Visible="false" Height="26px"
                Width="90px" Text="Approve TP" OnClick="btnApprove_Click" OnClientClick="return ValidateEmptyValueApprove()" />
            &nbsp
            <asp:Button ID="btnReject" CssClass="BUTTON" runat="server" Visible="false" Text="Reject TP"
                Height="26px" Width="90px" OnClick="btnReject_Click" />
            &nbsp
            <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand"
                runat="server"></asp:Label>
            &nbsp
            <asp:TextBox ID="txtReason" Width="400" Height="45" Visible="false" TextMode="MultiLine"
                runat="server"></asp:TextBox>
            &nbsp
            <asp:Button ID="btnSendBack" CssClass="BUTTON" Height="26px" Width="140px" runat="server"
                Visible="false" Text="Send for ReEntry" OnClick="btnSendBack_Click" />
        </div>
        <asp:GridView ID="grdWorkType" runat="server" Width="100%" HorizontalAlign="Center"
            AlternatingRowStyle-CssClass="alt" GridLines="None" CssClass="mGridDCR" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField HeaderText="WorkType_Code" HeaderStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"WorkType_Code") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="But_Access" HeaderStyle-Width="200px">
                    <ItemTemplate>
                        <asp:Label ID="lblplace" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"Place_Involved") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="fw " HeaderStyle-Width="50px">
                    <ItemTemplate>
                        <asp:Label ID="lblFWInd" runat="server" Text='<%#DataBinder.Eval(Container. DataItem,"FieldWork_Indicator") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>