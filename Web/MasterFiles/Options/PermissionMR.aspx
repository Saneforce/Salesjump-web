﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PermissionMR.aspx.cs" Inherits="MasterFiles_Options_PermissionMR" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Permission for Vacant MR Login</title>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
 
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <ucl:Menu ID="menu1" runat="server" /> 
        <center>
        <br />
        <table id="tblDocRpt" cellpadding="3" cellspacing="3" width = "60%">
            <tr >
                <td>
                    <asp:Label ID="lblFF" runat="server" Text="Field Force"></asp:Label>
                </td>
                <td > 
                    <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                        <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                        onselectedindexchanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                    </asp:DropDownList>

                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack = "true" SkinID="ddlRequired"></asp:DropDownList>
                    <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" 
            onclick="btnSubmit_Click" />
        <br />
        <br />
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
        </asp:Table>
        <br />
        <asp:Button ID="btnSave" runat="server" Width="70px" Height="25px" Text="Save" onclick="btnSave_Click" 
             />

    </center>          
       <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>
