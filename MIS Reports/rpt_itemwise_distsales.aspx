<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_itemwise_distsales.aspx.cs" Inherits="MIS_Reports_rpt_itemwise_distsales" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Itemwise Distrbutor sales</title>
    <script src='https://cdn.rawgit.com/simonbengtsson/jsPDF/requirejs-fix-dist/dist/jspdf.debug.js'></script>
<script src='https://unpkg.com/jspdf-autotable@2.3.2'></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
  <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
     <script language="Javascript">
         function RefreshParent() {
             window.close();
         }
    </script>
     <script type="text/javascript">
         $(document).on('click', ".btnExcel", function (e) {

             var a = document.createElement('a');
             var blob = new Blob([$('div[id$=pnlContents]').html()], {
                 type: "application/csv;charset=utf-8;"
             });
             document.body.appendChild(a);
             a.href = URL.createObjectURL(blob);
             a.download = 'ItemwiseSales.xls';
             a.click();
             e.preventDefault();
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%" >
                <tr>
                    <td width="70%" align="left" ></td>
                    <td align="right">
                   <asp:LinkButton ID="btnExcel" runat="Server" Style="padding: 0px 20px;" class="btn btnExcel" /> 
                   <asp:LinkButton ID="LinkButton1" runat="Server" style="padding: 0px 20px;" class="btn btnClose"   OnClientClick="javascript:window.open('','_self').close();"/>

                   </td></tr>
            </table>
        </asp:Panel>
        </div>
        <asp:Panel id="pnlContents"  runat="server" Width="100%">
             <table border="0" id='1'  width="90%" style="margin:auto;">  
			<tr>
                    <td width="70%" align="left" >
                    <asp:Label ID="lblHead" Text="Retail Lost  Details" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium"></asp:Label>
                    </td>
					</tr>
          <tr align="right">
		  <td align="left" style="font-size: small; font-weight: bold;font-family: Andalus;">Distributor Name:
                   <asp:Label ID="Feild" runat="server" Text="" ForeColor="Red"></asp:Label>
				   </td>
				   </tr>
                    <tr> 
                        <td width="100%" colspan="2">
                    <asp:HiddenField ID="HiddenField1" runat="server"  />
                            <asp:GridView ID="gvtotalorder" runat="server" Width="80%"  class="newStly"  GridLines="Both"

 ShowHeader="true" Font-Size=8pt  HorizontalAlign="Center" 
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="false" BackColor="#ffffe0" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                    <asp:TemplateField HeaderText="SI.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" ItemStyle-Font-Names="Rockwell" HeaderStyle-Font-Size="12pt"
                                ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Style="white-space: nowrap" runat="server" Font-Size="10pt"
                                        Text='<%#(Container.DataItemIndex)+1%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ProductName" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" ItemStyle-Font-Names="Rockwell" HeaderStyle-Font-Size="12pt"
                                ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="prodname" Style="white-space: nowrap" runat="server" Font-Size="10pt"
                                        Text='<%#Eval("Product_Name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRP Rate" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" ItemStyle-Font-Names="Rockwell" HeaderStyle-Font-Size="12pt"
                                ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="rate" Style="white-space: nowrap" runat="server" Font-Size="10pt"
                                        Text='<%#Eval("Rate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                ItemStyle-Font-Bold="true" ItemStyle-Font-Names="Rockwell" HeaderStyle-Font-Size="12pt"
                                ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="qty" Style="white-space: nowrap" runat="server" Font-Size="10pt"
                                        Text='<%#Eval("Quantity")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                HeaderStyle-Font-Size="12pt" ItemStyle-BorderWidth="1" ItemStyle-Font-Bold="true"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="sale" runat="server" Font-Size="10pt" Text='<%#Eval("value")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
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
