<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_retailer_remark_count.aspx.cs" Inherits="MIS_Reports_rpt_retailer_remark_count" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reasonwise Retailer List</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script language="Javascript">
         function RefreshParent() {
             window.close();
         }
    </script>
    <script src="../JsFiles/canvasjs.min.js"></script>
	
    <style type="text/css">
        .rptCellBorder
        {
            border: 1px solid;
            border-color :#999999;
        }
        td{padding:2px 5px;}
		.subTot{Font-size:11pt;font-weight:bold;}
		.GrndTot{Font-size:13pt;font-weight:bold;}
        .remove  
  {
    text-decoration:none;
  }
  	.TopButton{
		border-color: Black;
	    border-width: 1px;
	    border-style: Solid;
	    font-family: Verdana;
	    font-size: 10px;
	    height: 25px;
	    width: 60px;
	}
	.remark{
  /* FF3.5+ */
  -moz-transform: rotate(-90.0deg);
  /* Opera 10.5 */
  -o-transform: rotate(-90.0deg);
  /* Saf3.1+, Chrome */
  -webkit-transform: rotate(-90.0deg);
  /* IE6,IE7 */
  filter: progid: DXImageTransform.Microsoft.BasicImage(rotation=0.083);
  /* IE8 */
  -ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=0.083)";
  /* Standard */
  transform: rotate(-90.0deg);
  text-align: center;
    font-weight: 600;
    font-family: serif !important;
      }
    </style>
 <script type="text/javascript">
     function exportexcel() {
         $("#pnlContents").table2excel({

             filename: "ReasonwiseRetailAnalysis",
             fileext: ".xls"
         });
     }; $(document).ready(function () {
         $('.remark').css('height', $('.remark').width());
     });
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
      <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
             
                    <td width="60%" align="center" >
                    <asp:Label ID="lblHead"   Font-Bold="true" STYLE="COLOR: #3F51B5;PADDING-RIGHT: 217PX;"  Font-Size="Large"  Font-Underline="true"
                runat="server" ></asp:Label>
                    </td>
                     <td width="40%" align="right">
                        <table>
                            <tr>
<td>
                                 <asp:LinkButton ID="btnPrint" runat="Server" style="padding: 0px 20px;" class="btn btnPrint" OnClick="btnPrint_Click"/>
       <asp:LinkButton ID="btnExport"  runat="Server" style="padding: 0px 20px;" class="btn btnPdf" Visible="false"   />
       <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel"
                                        OnClick="btnExcel_Click" />
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
                   </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>

        <asp:Panel id="pnlContents" EnableViewState="false"  runat="server" Width="100%" style="margin-left:35px">
                <table border="0" id='1'style="margin:auto"   width="100%">                 
                            <tr><td>&nbsp;&nbsp;</td></tr>                         
          <tr><td style="font-size: small; font-weight: bold;font-family: Andalus;Padding-left:80px;">
                   <asp:Label ID="Field" align="left" runat="server" Font-Bold="true" style="font-family: Andalus; color:Red;" ></asp:Label></td></tr>
                         
        
      
                   
                    <tr> 
                        <td width="100%">
                            <asp:GridView ID="gvincentive" runat="server"      ShowHeader="false"
                                HorizontalAlign="Center"  Width="50%"  Font-Names="andalus" OnRowCreated = "OnRowCreated"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"   
                                AutoGenerateColumns="true"  class="newStly"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>                                                             
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    </table>
 </asp:Panel>
    </form>
</body>
</html>
