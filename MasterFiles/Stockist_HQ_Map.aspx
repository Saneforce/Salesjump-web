<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_HQ_Map.aspx.cs" Inherits="MasterFiles_Stockist_HQ_Map" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HQ - Stockist Map</title>
       <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
  <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    </div>
    <br /> 
    </form>
</body>
</html>
