<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload_Category.aspx.cs" Inherits="MasterFiles_Options_FileUpload_Category" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category</title>
      <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" />
        <br /> 
    </div>
    </form>
</body>
</html>
