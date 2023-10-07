<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_detailviewchgd.aspx.cs" Inherits="MIS_Reports_order_detailviewchgd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Order Detail View</title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
     <script language="Javascript">
         function RefreshParent() {
             //window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(Brandcode, Brandname, cyear, cmonth, sCurrentDate, type) {
            popUpObj = window.open("rptPurchas_Register_Brandwise_view.aspx?Brand_Code=" + Brandcode + "&Brand_name=" + Brandname + " &Year=" + cyear + "&Month=" + cmonth + "&sCurrentDate=" + sCurrentDate + "&Type=" + type,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=700," +
    "height=500," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
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
                <td width="20%"></td>
                    <td width="80%" align="center" >
                    <asp:Label ID="lblHead" Text="Order Detail View" SkinID="lblMand" Font-Bold="true"  Font-Underline="true"
                runat="server" Font-Size="Medium" ForeColor="#0066CC"></asp:Label>
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
                                    <asp:Button ID="btnExport" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                        BorderWidth="1" Height="25px" Width="60px" BorderColor="Black" BorderStyle="Solid"
                                        OnClick="btnExport_Click" />
                                </td>
                               
                                <td>
                                    <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
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
        </div>
       <div>
                <table width="90%" align="center">
                <tr>
                      
                    <td width="2.5%">
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="Lbl_Ex_Name" runat="server" Font-Bold="true"  Font-Underline="true" SkinID="lblMand" 
                            Text=""></asp:Label>
                        <br />
                        <asp:Label ID="Lbl_Retailer" runat="server" Font-Bold="true" SkinID="lblMand"  Font-Underline="true"
                            Text=""></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="Lbl_Fo" runat="server" Font-Bold="true" SkinID="lblMand"   Font-Underline="true"
                            Text=""></asp:Label>
                        <br />
                        <asp:Label ID="lbl_Year" runat="server" Font-Bold="true" SkinID="lblMand" 
                            Text=""></asp:Label>
                    </td>
</tr>
                </table>
            </div>
            <br />
<div align="center">

            <asp:GridView ID="gvorderview" runat="server" Width="85%"  
            OnRowCreated="OnRowCreated"   ShowHeader="false" 
                                HorizontalAlign="Center" 
                                BorderWidth="1px" CellPadding="1" 
            EmptyDataText="No Data found for View"   OnRowDataBound="MainGridView_RowDataBound"
                                AutoGenerateColumns="true"  HeaderStyle-BackColor="#ce93d8"   

                                HeaderStyle-HorizontalAlign="Center" 
            BorderColor="Black" BorderStyle="Solid" Font-Size="13px" 
            Font-Names="Times New Roman">                               
                                <Columns>
                                


                                   <%-- <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                             <asp:Label ID="lblSNo" runat="server" Font-Size="9pt" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distributor" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="stockist" style="white-space:nowrap" runat="server" Font-Size="9pt" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Taken By" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_name" runat="server" Font-Size="9pt" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

 <asp:TemplateField HeaderText="Route" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Route_name" runat="server" Font-Size="9pt" Text='<%#Eval("routename")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Retailer" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="Retailer_name" runat="server" Font-Size="9pt" Text='<%#Eval("retailername")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="netval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "net_weight_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Value" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="toval" runat="server" Font-Size="9pt" Text='<%#Eval("Totalvalue")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Discount Price" ItemStyle-Width="270" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="discountpri" runat="server" Font-Size="9pt" Text='<%#Eval("discount_price")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                
                                     <asp:TemplateField HeaderText="Order Value" ItemStyle-Width="120" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="orderval" runat="server" Font-Size="9pt" Text='<%#DataBinder.Eval(Container.DataItem, "Order_value", "{0:N2}")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                   
                                   
                                </Columns>
                                <AlternatingRowStyle Font-Names="Andalus" />
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                                <HeaderStyle BackColor="#33CCCC" HorizontalAlign="Center" />
                            </asp:GridView>


</div>



            <table width="100%" align="center"> 
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:Table ID="tbl"  runat="server" Style="border-collapse: collapse;  border: solid 1px Black;
                                 font-family: Calibri" Font-Size="8pt" GridLines="Both" Width="95%">
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