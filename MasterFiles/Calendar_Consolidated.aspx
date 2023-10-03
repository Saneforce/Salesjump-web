<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar_Consolidated.aspx.cs" Inherits="MasterFiles_Calendar_Consolidated" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consolidated View</title>
    <style type="text/css">
    .aclass{Border: 1px solid lighgray;}
    .aclass{width:50%}
.aclass tr td {background:White; font-weight:bold; color: Black; border: 1px solid black;
    border-collapse: collapse;}
 .aclass th {
    border: 1px solid black;
    border-collapse: collapse;
    background:LightBlue;
 
    
    
 
}
   .lbl {color: Red;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <div id="Divid" runat="server"></div>
     <br />
    </div>
    <center>
  <asp:Label ID="lblyear" runat="server" Text="Year" SkinID="lblMand" ></asp:Label>
  <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" AutoPostBack="true"
            onselectedindexchanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>

  </center>
  <br />
    <asp:Panel ID="pnl" runat="server" align="Center" >
  
    </asp:Panel>
    </form>
</body>
</html>
