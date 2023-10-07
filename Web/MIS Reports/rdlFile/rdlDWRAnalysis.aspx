<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rdlDWRAnalysis.aspx.cs" Inherits="MasterFiles_rdlFile_Default" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR View Report</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style>
        .tr_det_head
        {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
           
          
        }
        .tbldetail_main
        {
            font-family:Verdana;            
            font-size:7.8pt;
            height:17px;            
            border: 1px solid;
            border-color :#999999;            
        }
        .tbldetail_Data
        {
            height: 18px;
        }
        .Holiday
        {
            color: Red;
            font-size: 9pt;
            font-family: Calibri;
        }
        .NoRecord
        {
            font-size: 10pt;
            font-weight: bold;
            color: Red;
            background-color: AliceBlue;
        }
        .BottomTotal
        {
            color:Red;
        }
        
    </style>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    </head>
<body>  
    <form id="form1" runat="server"> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>    
     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="650px" Width="1355px">
     </rsweb:ReportViewer>
    </form>
</body>
</html>
