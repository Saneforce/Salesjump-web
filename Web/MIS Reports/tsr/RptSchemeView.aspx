<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="RptSchemeView.aspx.cs" Inherits="MIS_Reports_tsr_RptSchemeView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Scheme Report</title>
        <link type="text/css" rel="Stylesheet" href="../../../css/Report.css" />
        <link href="../../../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
        <link href="../../../css/style.css" rel="stylesheet" />

        <style type="text/css">
            #GridView1 td {
                padding: 5px 5px;
            }
        </style>

        <script type="text/javascript" language="Javascript">
            function RefreshParent() {
                // window.opener.document.getElementById('form1').click();
                window.close();
            }
        </script>
    </head>
    <body>
         <form id="form1" runat="server">
             <div>
                 <center>
                     <asp:Panel ID="pnlbutton" runat="server">
                         <table width="100%">
                             <tr>
                                 <td width="80%"></td>
                                 <td align="right">
                                     <table>
                                         <tr>
                                             <td>
                                                 <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                                      OnClick="btnPrint_Click" />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClick="btnExcel_Click" />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClick="btnPDF_Click" Visible="false" />
                                             </td>
                                             <td>
                                                 <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" 
                                                     OnClientClick="RefreshParent();" />
                                             </td>
                                         </tr>
                                     </table>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>

                     <asp:Panel ID="pnlContents" runat="server" Width="100%">
                         <table border="0" width="90%">
                             <tr align="center">
                                 <td>
                                     <asp:Label ID="lblTitle" runat="server" Font-Size="Small" Font-Bold="True" Font-Underline="true"></asp:Label>
                                     <span style="color: Red"></span>
                                 </td>
                             </tr>
                             <tr>
                                 <td align="center">
                                     <asp:Label ID="lblHead" runat="server" Text="TP My Day Plan Report for " Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                                 </td>
                             </tr>
                             <tr>
                                <%--<td align="center">
                                     <span style="font-family: Verdana">Field Force Name :</span>
                                     <asp:Label ID="lblFieldForceName" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                                 </td>--%>
                             </tr>
                             <tr>
                                 <td>
                                     <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333"  class="newStly"  GridLines="Both"  AutoGenerateColumns="false">
                                         <AlternatingRowStyle BackColor="White" />
                                         <Columns>
                                             <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1" 
                                                 HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Center">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Zone">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblZone_name" runat="server" Text='<%#Eval("Zone_name")%>' Width="100"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Area">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblArea" runat="server" Text='<%#Eval("Area_name")%>' Width="250"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="User">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblUser" runat="server" Text='<%#Eval("Sf_Name")%>' Width="180"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="HQ">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("HeadQuarter")%>' Width="180"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="DB">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblDB" runat="server" Text='<%#Eval("Stockist_Name")%>' Width="250"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Town">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblTown" runat="server" Text='<%#Eval("Town")%>' Width="180"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField> 

                                             <asp:TemplateField HeaderText="Route">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblRouteNm" runat="server" Text='<%#Eval("RouteNm")%>' Width="180"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField> 

                                             <asp:TemplateField HeaderText="Outlet Name">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblOutletName" runat="server" Text='<%#Eval("OutletName")%>' Width="150"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Brand">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Brand")%>' Width="150"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             
                                             <asp:TemplateField HeaderText="SKU">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblSKU" runat="server" Text='<%#Eval("SKU")%>' Width="150"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Quantity">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Qty")%>' Width="150"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                             
                                             <asp:TemplateField HeaderText="Order Value">
                                                 <ItemTemplate>
                                                     <asp:Label ID="lblOrderValue" runat="server" Text='<%#Eval("OrderValue")%>' Width="150"></asp:Label>
                                                 </ItemTemplate>
                                             </asp:TemplateField>
                                         </Columns>
                                         <EditRowStyle BackColor="#2461BF" />
                                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                         <RowStyle BackColor="#EFF3FB" />
                                         <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                         <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                         <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                         <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                         <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                     </asp:GridView>
                                 </td>
                             </tr>
                         </table>
                     </asp:Panel>
                     <br />
                 </center>
             </div>
         </form>
    </body>
</html>

