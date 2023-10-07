﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Retailer_Competitor_View.aspx.cs" Inherits="MIS_Reports_rpt_Retailer_Competitor_View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Retailer Competitor View</title>
     <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
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
    </style>
 <script type="text/javascript">
     function exportexcel() {
         $("#pnlContents").table2excel({

             filename: "Retailer Competitor View",
             fileext: ".xls"
         });
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
          <tr align="left"><td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;Padding-left:180px;">
                   <asp:Label ID="Feild" runat="server" Text="" Font-Bold="true" style="font-family: Andalus; color:Blue;" ></asp:Label></td></tr>
                         
        
      
                   
                    <tr> 
                        <td width="100%">
                            <asp:GridView ID="gdcompetitor" runat="server"      ShowHeader="false"
                                HorizontalAlign="Center"  Width="100%"  Font-Names="andalus" OnRowCreated="OnRowCreated"

                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View"   
                                AutoGenerateColumns="true"  class="newStly"
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                
                                    <%--<asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Listeddr_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product" ItemStyle-Width="70" HeaderStyle-BorderWidth="1" ItemStyle-Font-Bold="true"  
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="prtname" style="white-space:nowrap" runat="server" Font-Size="10pt" Text='<%#Eval("Product_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Stock" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1"  ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="10pt" Text='<%#Eval("ClStock")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                                   
                                      
                                   
                                   
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
