﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnListedDr_View.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnListedDr_View" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
 <%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnListed Customer View</title>
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
           .marRight
        {
            margin-right:35px;
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
    <div id="Divid" runat="server">
        </div>
         <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" CssClass="marRight">
          <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
        </asp:Panel>
          <table id="Table1" runat="server" width="90%">
       
            <tr>
                 <td align="right" width="30%">
                   <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                </td>
            </tr>
              <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnBack" CssClass="BUTTON" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>
            </tr>
            </table>  
            <br />     
        <center>
        <table border="1" width="40%" style="border: thin solid #800000">
            <tr>
                <td align="left">
                    <asp:Label ID="lblDRName" runat="server" Text="UnListed Customer Name" SkinID="lblMand" ></asp:Label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtName" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'" Width="225px">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblAddress1" SkinID="lblMand" runat="server" Text="Address "></asp:Label>    
                </td>
                <td align="left">
                    <asp:TextBox ID="txtAddress1" runat="server" SkinID="MandTxtBox" 
                        onfocus="this.style.backgroundColor='#E0EE9D'" 
                        onblur="this.style.backgroundColor='White'" Width="250px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblCatg" runat="server" SkinID="lblMand" Text="Category: "></asp:Label>    
                </td>
              <td align="left">
                    <asp:DropDownList ID="ddlCatg" runat="server" SkinID="ddlRequired" ></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Label ID="lblSpec" runat="server" SkinID="lblMand" Text="Channel: "></asp:Label>    
                </td>
                  <td align="left">
                    <asp:DropDownList ID="ddlSpec" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                </td>
            </tr>
            <%--<tr>
                 <td align="left">
                    <asp:Label ID="lblQual" runat="server" SkinID="lblMand" Text="Qualification: "></asp:Label>    
                </td>
                 <td align="left">
                    <asp:DropDownList ID="ddlQual" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                 <td align="left">
                    <asp:Label ID="lblClass" runat="server" SkinID="lblMand" Text="Class: "></asp:Label>    
                </td>
                 <td align="left">
                    <asp:DropDownList ID="ddlClass" runat="server" SkinID="ddlRequired" Width="225px"></asp:DropDownList> 
                </td>
            </tr>
            <tr>
                 <td align="left">
                    <asp:Label ID="lblTerritory" runat="server" SkinID="lblMand" Text="Territory: "></asp:Label>    
                </td>
                 <td align="left">
                    <asp:DropDownList ID="ddlTerritory" runat="server" SkinID="ddlRequired"></asp:DropDownList> 
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Width="60px" Height="25px" Text="Save" onclick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>

        </center>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
