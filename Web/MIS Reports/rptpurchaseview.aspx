<%@ Page Language="C#"  EnableEventValidation="false" AutoEventWireup="true" CodeFile="rptpurchaseview.aspx.cs" Inherits="MIS_Reports_rptpurchaseview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase View</title>
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
                    <asp:Label ID="lblHead" SkinID="lblMand" Font-Bold="true"  Font-Names="Nyala" Font-Underline="true"
                runat="server"></asp:Label>
                    </td>
                  <td width="40%" align="right">
                      <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
                                                                        <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"/>
   
								  <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf"  OnClick="btnExport_Click" />
<asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>




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
         <asp:Label ID="lblRegionName" runat="server" Text="Label"></asp:Label> 
       
        
            <br />
  <table align="center" style="padding-left:220px"><tr><td>
            <asp:Label ID="DistributorName" runat="server" Text="Distributor :" 
                Font-Bold="True " Font-Size="14px"></asp:Label></td><td>
                <asp:Label ID="dist" runat="server" Font-Size="12px" Font-Underline="True"></asp:Label></td></tr></table> </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" class="newStly"  GridLines="Both" Width="95%">
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
