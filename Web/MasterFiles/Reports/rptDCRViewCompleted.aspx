<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptDCRViewCompleted.aspx.cs" Inherits="Reports_rptDCRViewCompleted" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .tblHead
        {
          background-color:#0097AC;
          font-family:Verdana;
          color :White;
          font-size:9pt;
          font-weight:bold;
          font-family:Calibri;
          height:22px;
          border-color:Black;
        }
        .tblRow
        {
          font-size :8pt;
          font-family:Calibri;           
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" width="100%">
            <tr>
                <td align="center"> 
                    <asp:Label ID="lblHead" runat="server" Text="Daily Call Report for "  Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>                    
                </td>                
            </tr>
        </table>
        <asp:Table ID="tbl" runat="server" Width="95%">
        </asp:Table>
    </form>
</body>
</html>
