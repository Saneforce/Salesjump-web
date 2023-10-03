<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_stock_second_sales.aspx.cs" Inherits="MIS_Reports_rpt_stock_second_sales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distributor Stock And Sale Analysis</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>

   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script language="Javascript">
         function RefreshParent() {
             // window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
<script type="text/javascript">
        function exportTabletoPdf() {

        };

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
             
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead" Text="Distributor Stock and Sales Analysis"   Font-Bold="true" STYLE="COLOR: #3F51B5;PADDING-RIGHT: 217PX;"  Font-Size="Large"  Font-Underline="true"
                runat="server" ></asp:Label>
                    </td>
                     <td width="40%" align="right">
                        <table>
                            <tr>
<td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf" Visible="false"   />
       <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" OnClick="btnExcel_Click"    />
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        </div>
       <asp:Panel id="pnlContents"  EnableViewState="false" runat="server" Width="100%" style="margin-left:35px">
                <table border="0" id='1'style="margin:auto"   width="100%">                 
                            <tr><td>&nbsp;&nbsp;</td></tr>                         
          <tr align="left"><td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;Padding-left:180px;">
                   <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" style="font-family: Andalus; color:Blue;" ></asp:Label></td></tr>
                         
        
      
                   
                    <tr> 
                        <td width="100%">
                            <asp:Table ID="tbl" runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="100%">
                            </asp:Table>
                        </td>
                    </tr>
                    </table>
 </asp:Panel>
    </form>
</body>
</html>
