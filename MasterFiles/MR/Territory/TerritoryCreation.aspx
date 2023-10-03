<%@ Page Title="Quick Bulk Route Add" Language="C#" AutoEventWireup="true" CodeFile="TerritoryCreation.aspx.cs" MasterPageFile="~/Master.master"
    Inherits="MasterFiles_TerritoryCreation" %>
     <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Territory Creation</title>    
        <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    
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
    </style>
    <%-- <script type="text/javascript">
      function s() {
          var t = document.getElementById("<%=grdTerr.ClientID%>");
          var t2 = t.cloneNode(true)
          for (i = t2.rows.length - 1; i > 0; i--)
              t2.deleteRow(i)
          t.deleteRow(0)
          a.appendChild(t2)
      }
      window.onload = s
    </script>--%>
    <style type="text/css">
        body
        {
            margin-left: 0px;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
        }
        a:link
        {
            color: #0000FF;
        }
        a:visited
        {
            color: #0000FF;
        }
        a:hover
        {
            color: #0000FF;
            text-decoration: none;
        }
        a:active
        {
            color: #0000FF;
        }
        .basix
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
        }
        .header1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            font-weight: bold;
            color: #006699;
            width: 120px;
        }
        .lgHeader1
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 18px;
            font-weight: bold;
            color: #0066CC;
            background-color: #CEE9FF;
        }
        .clr
        {
        }
        
        .clr th
        {
            background-color: #7AA3CC;
            padding: 4px 2px; /*background:url(menubg.gif) center center repeat-x;*/
            border: solid 1px Black;
            border-left: solid 1px Black;
            font-weight: normal;
            font-size: 11px;
            font-family: Verdana;
            color: #ffffff;
        }
        
        .clr tr:nth-child(even)
        {
            background-color: #DCEDFF;
        }
        
        .clr tr:nth-child(odd)
        {
            background-color: white;
        }
        
        .clr td
        {
            padding: 2px;
              border: solid 1px Black;
            border-left: solid 1px Black;
        }
        .marRight
        {
            margin-right:35px;
        }
    </style>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var grid = document.getElementById('<%= grdTerritory.ClientID %>');
            if (grid != null) {

                var isEmpty = false;
                var Inputs = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                for (i = 2; i < Inputs.length; i++) {
                    if (Inputs[i].type == 'text') {
                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }

                        var Name = document.getElementById('grdTerritory_ctl' + index + '_Territory_Name');
                        var Type = document.getElementById('grdTerritory_ctl' + index + '_Territory_Type');
                        if (Name.value != '' || Type.value != '0') {
                            isEmpty = true;
                        }
                        if (Name.value != '') {
                            if (Type.value == '0') {
                                alert('Select Type');
                                Type.focus();
                                return false;
                            }

                        }
                        if (Type.value != '0') {
                            if (Name.value == '') {
                                alert('Enter Name')
                                Name.focus();
                                return false;
                            }

                        }
                    }

                }
            }
        }
    </script>
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
</head>
<body>
    <form id="form1" runat="server">
  
    <div id="Divid" runat="server">
        </div>
       <asp:Panel ID="pnlsf" runat="server" Visible="false" HorizontalAlign="Left" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="false" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" CssClass="marRight">
         <asp:Label ID="Lab_DSM" runat="server"  Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
        
          
        
            <div>
            <table  id="tblTerritory" width="95%">
            <tr>
                <td valign="top" style="width: 60%;">
                    <table align="right" style="margin-right:120px">
                        <tr>
                            <td style="" >
                                <asp:GridView ID="grdTerritory" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10" GridLines="None"
                                    BorderStyle="Solid" OnRowDataBound="getTerritory_RowDataBound" CssClass="mGridImg"
                                    PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Short Name" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_SName" MaxLength="3" SkinID="TxtBxAllowSymb" runat="server"
                                                    Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event)" Text='<%#Eval("Territory_SName")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                         <asp:TemplateField HeaderText="Route Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_Code" runat="server" SkinID="TxtBxAllowSymb" MaxLength="10"
                                                    Width="100px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Text='<%#Eval("Route_Code")%>'></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_Name" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                                    Width="150px" onkeypress="AlphaNumeric_NoSpecialChars(event);" Text='<%#Eval("Territory_Name")%>'></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Dist Name" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                             <asp:DropDownList ID="Territory_DistName" runat="server" SkinID="ddlRequired">
                                                   <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:TextBox ID="Territory_Name3" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                                    Width="150px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Territory_Cat")%>'></asp:TextBox>--%>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DSM Name" ItemStyle-HorizontalAlign="Left" Visible="true" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                             <asp:DropDownList ID="Territory_DisName" runat="server" SkinID="ddlRequired">
                                                   <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <%--<asp:TextBox ID="Territory_Name3" runat="server" SkinID="TxtBxAllowSymb" MaxLength="50"
                                                    Width="150px" onkeypress="AlphaNumeric_NoSpecialChars_New(event)" Text='<%#Eval("Territory_Cat")%>'></asp:TextBox>--%>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Route Target" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_Target" runat="server" SkinID="TxtBxAllowSymb" MaxLength="5"
                                                    Width="80px" onkeypress="CheckNumeric(event);" Text='<%#Eval("Target")%>'></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Min Prod%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_MinProd" runat="server" SkinID="TxtBxAllowSymb" MaxLength="5"
                                                    Width="50px" onkeypress="CheckNumeric(event);" Text='<%#Eval("Min_Prod")%>'></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route Population" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="Territory_Cenus" runat="server" SkinID="TxtBxAllowSymb" MaxLength="5"
                                                    Width="50px" onkeypress="CheckNumeric(event);" Text='<%#Eval("Population")%>'></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="rfvterr_name" runat="server" ControlToValidate="Territory_Name" ErrorMessage="Required"></asp:RequiredFieldValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSave" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Save" OnClick="btnSave_Click"
                                    OnClientClick="return ValidateEmptyValue()" />
                                <asp:Button ID="btnClear" CssClass="BUTTON" runat="server" Width="60px" Height="25px" Text="Clear" OnClick="btnClear_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="20%" style="vertical-align:top" align="left">
                
                    <asp:GridView ID="grdTerr" runat="server" HorizontalAlign="Center" AutoGenerateColumns="false"
                        CellPadding="4" Width="100%" CssClass="clr" AlternatingRowStyle-CssClass="alt"
                        AllowSorting="True" OnSorting="grdTerr_Sorting">
                        <HeaderStyle BackColor="#EDEDED" Height="26px" Font-Names="Calibri" Font-Size="XX-Small"/>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Left">
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" ItemStyle-Font-Size="X-Small" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Route_Code" HeaderText="Route Code" ItemStyle-Font-Size="X-Small" ItemStyle-Width="50px"/>
                            <asp:BoundField DataField="Territory_Name" ItemStyle-Font-Size="XX-Small" ItemStyle-Width="50px" ItemStyle-Font-Names="Calibri" HeaderText="Name"
                                HeaderStyle-ForeColor="White" />
                            <asp:BoundField DataField="Dist_Name" ItemStyle-Font-Size="X-Small" HeaderText="Distributor Name" Visible="false" ItemStyle-Width="100px" />
                            <asp:BoundField DataField="Target" ItemStyle-Font-Size="X-Small" HeaderText="Target" ItemStyle-Width="50px" />
                             <asp:BoundField DataField="Min_Prod" ItemStyle-Font-Size="X-Small" HeaderText="Min_Prod %" ItemStyle-Width="50px" />
                            <asp:BoundField DataField="Population" ItemStyle-Font-Size="X-Small" HeaderText="Route Population" ItemStyle-Width="50px" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
</asp:Content>