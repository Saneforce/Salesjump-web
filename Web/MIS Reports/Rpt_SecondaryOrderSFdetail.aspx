<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_SecondaryOrderSFdetail.aspx.cs" Inherits="MIS_Reports_Rpt_SecondaryOrderSFdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Primary Order VS Sale</title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript">
        $(document).on('click', '#btnExcel', function (e) {
            var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divexcel').html());
            var a = document.createElement('a');
            a.href = data_type;
            a.download = 'PrimaryOrderReport xls';
            a.click();
            e.preventDefault();

        });
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Rpt_SecondaryOrderSFdetail.aspx/getorderdetSF",
                data: "{'Div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    SFdet = JSON.parse(data.d) || [];
                    if (SFdet[0].Order_Flag == "2") {
                        RejectTable();
                    }
                    else {
                        saleTable();
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }

            });

        });
        function saleTable() {

            $('#ordertable thead').html('');
            $('#ordertable tbody').html('');
            var hstr = '<tr><th>Sl.No</th><th>Field force Name</th><th>Order Date</th><th>Distributor Name</th><th>Order No</th><th>Retailer Name</th><th>Order Value</th></tr > ';  

            $('#ordertable thead').append(hstr);
            var bstr = '';
            var Slno = 0;
            for (var i = 0; i < SFdet.length; i++) {
                Slno += 1;
               bstr += '<tr><td>' + Slno + '</td><td>' + SFdet[i].SF_Name + '</td><td>' + SFdet[i].Order_Date + '</td><td>' + SFdet[i].Stockist_Name + '</td><td>' + SFdet[i].Trans_Sl_No + '</td><td>' + SFdet[i].Retailer_Name + '</td><td>' + SFdet[i].Order_value + '</td></tr>';
            }
            $('#ordertable tbody').append(bstr);
        }
        function RejectTable() {

            $('#ordertable thead').html('');
            $('#ordertable tbody').html('');
            var hstr = '<tr><th>Sl.No</th><th>Field force Name</th><th>Order Date</th><th>Distributor Name</th><th>Order No</th><th>Retailer Name</th><th>Order Value</th><th>Remarks</th></tr > ';
            $('#ordertable thead').append(hstr);
            var bstr = '';
            var Slno = 0;
            for (var i = 0; i < SFdet.length; i++) {
                Slno += 1;
                bstr += '<tr><td>' + Slno + '</td><td>' + SFdet[i].SF_Name + '</td><td>' + SFdet[i].Order_Date + '</td><td>' + SFdet[i].Stockist_Name + '</td><td>' + SFdet[i].Trans_Sl_No + '</td><td>' + SFdet[i].Retailer_Name + '</td><td>' + SFdet[i].Order_value + '</td><td>' + SFdet[i].rejRemarks + '</td></tr>';
            }
            $('#ordertable tbody').append(bstr);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div> <asp:Label ID="lblHead" Text="Report"  Font-Names="Andalus" Font-Bold="true"     margin-left="71px;" Font-Underline="true"
                runat="server" Font-Size="Large"></asp:Label></div>
            <div style="text-align: right">

                <a name="btnExcel" id="btnExcel" type="button" style="padding: 0px 20px;" href="#" class="btn btnExcel"></a>
                <a name="btnClose" id="btnClose" type="button" style="padding: 0px 20px;" href="javascript:window.open('','_self').close();" class="btn btnClose"></a>
            </div>

            <div id="divexcel">
                <table id="ordertable" border="1" class="newStly" style="border-collapse: collapse;">
                    <thead>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <%-- <div >
                <asp:Label ID="sfname" Text="Field Force" runat="server"></asp:Label>
                <asp:TextBox ID ="txtsfname" runat="server"></asp:TextBox>
                  
                   
                <asp:Label ID="stockist" Text="Distributer" runat="server"></asp:Label>
                <asp:TextBox ID ="txtstockist" runat="server"></asp:TextBox>
                    </div>
            <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="false" class="newStly"  >
                <Columns>
                     <asp:TemplateField HeaderText="Sl No">
                        <ItemTemplate>
                           <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name" >
                        <ItemTemplate>
                                 <%#Eval("Product_Name")%>
                            </ItemTemplate>
                    </asp:TemplateField>              
                         <asp:TemplateField HeaderText="Distributer">
                             <ItemTemplate>
                                 <%#Eval("Stockist_Name")%>
                             </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retailer">
                        <ItemTemplate>
                            <%#Eval("ListedDr_Name") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Order No" >
                        <ItemTemplate>
                            <%#Eval("Trans_Sl_No") %>
                        </ItemTemplate>
                    </asp:TemplateField> 
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <%#Eval("Quantitycnf") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Free">
                        <ItemTemplate>
                            <%#Eval("freecnf") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Discount Price">
                        <ItemTemplate>
                            <%#Eval("discount_price") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Discount %">
                        <ItemTemplate>
                            <%#Eval("discountcnf") %>
                        </ItemTemplate>
                    </asp:TemplateField>                      
                     <asp:TemplateField HeaderText="Net Weight">
                        <ItemTemplate>
                            <%#Eval("net_weight") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Rate">
                        <ItemTemplate>
                            <%#Eval("Rate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Order Value">
                        <ItemTemplate>
                            <%#Eval("value") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="OrderVsSale">
                        <ItemTemplate>
                            <%#Eval("order_flag") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>              
            </asp:GridView>--%>
            </div>
        </div>
    </form>
</body>
</html>
