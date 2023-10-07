<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rpt_Catagory_Wise_Order.aspx.cs" Inherits="MIS_Reports_rpt_Catagory_Wise_Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>SKU Details</title>
<link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
           
            $(document).on('click', '#btnExcel', function (e) {
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html());
                var a = document.createElement('a');
                a.href = data_type;
                a.download = 'Category_wise_Order_Detail xls';
                a.click();
                e.preventDefault();

            });
        });
            </script>

      

</head>
<body>
    <form id="form1" runat="server">
        <div>
      <asp:Panel ID="pnlbutton" runat="server">
         
            <div class="col-sm-4" style="text-align: right;width: 100%" >	
                            <asp:LinkButton ID="btnExcel" runat="Server" style="padding: 0px 20px;" class="btn btnExcel" />
                                 
       	<a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
              </div>
          
         </asp:Panel>
    </div>
      <asp:Panel id="pnlContents"  runat="server" Width="100%">
                <table border="0" id='1'   width="90%" style="margin:auto;">                 
                          
            <tr align="center"><td style="font-size:x-large;  font-weight: bold;font-family: Andalus;"><asp:Label ID="lblHead"  ForeColor="Red"  Font-Bold="true"  runat="server"></asp:Label> </td></tr>
         <tr><td>
              <asp:GridView ID="GridViewcat" runat="server" Width="100%" CssClass="newStly"  OnDataBound="catOnDataBound"
                                HorizontalAlign="Center"
                                BorderWidth="1px" CellPadding="2" EmptyDataText="No Data found for View" 
                                AutoGenerateColumns="false"  HeaderStyle-BackColor="#33CCCC" 
                                HeaderStyle-HorizontalAlign="Center" BorderColor="Black" BorderStyle="Solid">                               
                                <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="8pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo1" runat="server" Font-Size="9pt" Text='<%#((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Order Taken By" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="SF_Name" runat="server" Font-Size="9pt" Text='<%#Eval("SF_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Product Category" ItemStyle-Width="50" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="product_Detail_Name" runat="server" Font-Size="9pt" Text='<%#Eval("product_Detail_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Net weight" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="net_weight" runat="server" Font-Size="9pt" Text='<%#Eval("net_weight")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="quantity" runat="server" Font-Size="9pt" Text='<%#Eval("quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                   
                                   
                                    <asp:TemplateField HeaderText="Value" ItemStyle-Width="70" HeaderStyle-BorderWidth="1"
                                        HeaderStyle-Font-Size="10pt" ItemStyle-BorderWidth="1" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="value" runat="server" Font-Size="9pt" Text='<%#Eval("value")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>  
                                   
                                </Columns>
                               

<EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
              </td></tr></table>
        </asp:Panel>
       
    </form>
</body>
</html>
