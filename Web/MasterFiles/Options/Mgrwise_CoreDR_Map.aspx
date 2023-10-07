<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgrwise_CoreDR_Map.aspx.cs" Inherits="MasterFiles_Options_Mgrwise_CoreDR_Map" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Managerwise Core Customer Map</title>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
        <center>
        <br />
        <table width="90%" border="1" bgcolor="#D4D4D4" style="border-style: solid; border-width: thin">
            <tr>
                <td align="left" width="40%">
                    <asp:Button ID="btnSave" runat="server" Width="60px" Height="25px" Text="Save" onclick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnClear" runat="server" Text = "Clear" />
                </td>
                <td>
                    <asp:Label ID="lblHead" runat="server" Text="Core Customer Map" Font-Bold="True" Font-Names="Arial" Font-Size="11px"></asp:Label>
                </td>
                <td align="right">
                    <asp:Label ID="lblMR" runat="server" Text="MR : "></asp:Label>
                    <asp:DropDownList ID="ddlMR" runat="server" AutoPostBack="true"
                        onselectedindexchanged="ddlMR_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="90%">
        </asp:Table>

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
