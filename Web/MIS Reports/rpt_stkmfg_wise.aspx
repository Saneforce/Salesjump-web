<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_stkmfg_wise.aspx.cs" Inherits="MIS_Reports_rpt_stkmfg_wise" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Closing Stock View</title>


    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" /> 
       
   <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        
        .remove  
  {
    text-decoration:none;
  }
  
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="30%"></td>
                    <td width="70%" align="Center" >
<br></br>

                    </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnExcel_Click" />
                                </td>
                                  <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                         OnClientClick="javascript:window.open('','_self').close();" />
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>
<table>
<tr><td></td><td></td><td></td><td align="right">
                    <asp:Label ID="lblHead"  SkinID="lblMand" Font-Bold="true" width="290px"  Font-Underline="true"
                runat="server"></asp:Label></td></tr></table>
        </asp:Panel>
    </div>
       <asp:Panel ID="pnlContents" runat="server" Width="80%">
    <table width="100%" align="center">
<tr><td>
        <asp:Label id="lbl" runat="server" Text="Closing Stock View" Font-Size="Large" Font-Bold="true" ></asp:Label>
</td></tr></table>
                <table width="100%" align="center"><tr><td><asp:Label id="lbl1" runat="server" Font-Size="Large" Font-Bold="true"></asp:Label></td></tr>  </table>        
                <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>
                        </td>

                    </tr>
                    <tr><td>
                        &nbsp;</td></tr>
                </tbody>
            </table>    
          
        </div>
           </asp:Panel>
    </form>
</body>
</html>
