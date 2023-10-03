<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vacant_MR_Access.aspx.cs"
    Inherits="MasterFiles_Options_Vacant_MR_Access" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login To Vacant ID's</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <style type="text/css">
        table.gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        table.gridtable th
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: 1px #666666;
            background-color: #A6A6D2;
        }
        table.gridtable td
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: #666666;
            background-color: #ffffff;
        }
    </style>
    <!-- Table goes in the document BODY -->
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
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <center>
   <asp:Panel ID="pnlaccess" runat="server" Width="80%">
            <table id="tblgrid" width="80%" border="1" class="gridtable">
                <thead>
                    <tr>
                        <th style="color: White; border-color: Black">
                            Field Force Name
                        </th>
                        <th style="color: White; border-color: Black">
                            Login
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style="height: 400px; width: 30%;" valign="top" align="center">
                            <asp:Label ID="lblmr" runat="server" Font-Size="Small" ForeColor="BlueViolet">Vacant ID's</asp:Label><br />
                            <asp:ListBox ID="lstVacant" Font-Size="Small" Font-Names="Verdana" Width="100%"  Height="400px" runat="server"
                                Style="border: solid 1px black; border-collapse: collapse;" AutoPostBack="true" OnSelectedIndexChanged="lstVacant_SelectedIndexChanged">
                            </asp:ListBox>
                        </td>
                        <td align="center">
                            <br />
                            <br />
                            <br />
                                 <asp:Panel ID="pnlSf" runat="server" Visible="false">
                            <table align="center" border="1" width="60%" style="border-collapse:collapse">
                                <tr>
                                    <th align="center" colspan="2" style="color: White; border:1px solid; border-color:Black">
                                        Login
                                    </th>
                                </tr>
                                <tr >
                                    <td valign="middle" style="width:20%">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password" SkinID="lblMand"></asp:Label>

                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="15" TextMode="Password" SkinID="TxtBoxStyle"></asp:TextBox>
<asp:Label ID="lblPassword1" runat="server" Text="(Type your own Password)" ForeColor="Red" SkinID="lblMand"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblLoginto" runat="server" Text="Login To:" SkinID="lblMand"></asp:Label>
                                    </td>
                                    <td align="left">
                                    <asp:Label ID="lblLogin" ForeColor="Blue" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnLogin" runat="server" CssClass="BUTTON" Text="Login" OnClick="btnLogin_Click" />
                                    </td>
                                </tr>
                            </table>
                                 </asp:Panel>
                        </td>
                    </tr>
                </tbody>
            </table>
            </asp:Panel>
            <center>
            <asp:Label ID="lblrecord" ForeColor="Black" Width="80%" BackColor="AliceBlue" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" runat="server" Text="No Records Found" Visible="false"></asp:Label>
            </center>
    
        </center>
      <%--  <center>
        <asp:Label ID="lblnorecord" Width="90%" runat="server" Visible="false" ForeColor="Black" Text="No Records Found" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></asp:Label>
        </center>--%>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
    </div>
    </div>
    </form>
</body>
</html>
