<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="rpt_Distribution_Width.aspx.cs" Inherits="MIS_Reports_rpt_Distribution_Width" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Distribution Width</title>


    <script src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>

   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
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
               
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="Distribution Width" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                    <td width="40%" align="right">
                        <table>
                            <tr>
<td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
       <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

       <asp:Panel ID="pnlContents" runat="server" Width="100%">
    <div>
        <div align="center">
          
        </div>
        <div>
                
                <table width="100%" align="center">
                    <tr>
                    <td width="2.5%"></td>
                        <td align="right">
                                       <asp:Label ID="fieldf" Text="Fieldforce Name:" runat="server"  ForeColor="#0099CC"
                                Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                            <asp:Label ID="fldfrce" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label></td>
                       
                        <td align="center">
                            <asp:Label ID="Label1" Text="Distributor Name:" runat="server"  ForeColor="#0099CC"
                                Font-Bold="True" Font-Names="Andalus" Font-Underline="True"></asp:Label>
                            <asp:Label ID="distname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        <td>&nbsp&nbsp</td>
                        <td align="left">
                            <asp:Label ID="prodt" Text="Product Name:" runat="server" 
                                Font-Bold="True" ForeColor="#0099CC" Font-Names="Andalus" 
                                Font-Underline="True"></asp:Label>
                            <asp:Label ID="prdname" runat="server" SkinID="lblMand"  Font-Bold="True"></asp:Label>
                        </td>
                        
                    </tr>
                </table>
           </div>
            <br> </br>
            <%--<div id="chartContainer" style="height: 300px; width: 100%;"></div>--%>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" class="newStly" GridLines="Both" Width="90%">
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
