<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupScreenAccess.aspx.cs" Inherits="MasterFiles_Options_SetupScreenAccess" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Setup for Screen Access</title>
      <style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
       
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
  
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
         
        <center>
        <br />
        <table id="tblState" width = "20%" align="center">
            <tr >
                <td>
                    <asp:Label ID="lblState" runat="server" Text="State"></asp:Label>
                </td>
                <td > 
                    <asp:DropDownList ID="ddlState" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        onselectedindexchanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <asp:Panel ID="pnlTable" runat="server">
        
        </asp:Panel>
        
<%--        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both" Width="80%">
        </asp:Table>
--%>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Save" Visible="false" 
                onclick="btnSubmit_Click"/>
        &nbsp;&nbsp;
        <asp:Button ID="btnClear" runat="server" Width="60px" Height="25px" Text="Clear" Visible="false"/>
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
