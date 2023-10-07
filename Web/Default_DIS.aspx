<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default_DIS.aspx.cs" Inherits="Default_DIS" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link type="text/css" rel="stylesheet" href="../css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
    </div>
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>
