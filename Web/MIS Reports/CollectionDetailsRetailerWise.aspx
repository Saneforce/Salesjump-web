<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CollectionDetailsRetailerWise.aspx.cs"
    Inherits="MIS_Reports_CollectionDetailsRetailerWise" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link href="../css/bootstrap-3.3.2.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(document).on('click', "#btnExcel", function () {
                var printableArea = $('#content').html();
                window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('#content').html()));
                e.preventDefault();
            });
            $(document).on('click', "#btnExcel", function () {
                window.close();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin: 0 auto; width: 90%">
        <br />
        <div class="row" style="width: 100%">
            <div class="col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Collection Details Retailerwise" Style="font-weight: bold;
                    font-size: x-large"></asp:Label>
            </div>
            <div class="col-md-6" style="text-align: right">
                <a name="btnExcel" id="btnExcel" type="button" href="#" class="btn" style="padding: 2px 2px;">
                    <i class="fa fa-file-excel-o" style="font-size: 36px; color: green"></i></a>
                <a name="btnClose" id="btnClose" type="button" href="javascript:window.open('','_self').close();"
                    class="btn" style="padding: 2px 2px;"><i class="fa fa-close" style="font-size: 36px;
                        color: red"></i></a>
            </div>
        </div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <div id="content">
            <asp:GridView ID="GVRetailer" runat="server" Width="100%" HorizontalAlign="Center"
                AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="newStly"
                ShowFooter="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" HeaderStyle-ForeColor="white"
                        ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ListedDr_Name" HeaderStyle-Width="250px" HeaderStyle-ForeColor="white"
                        HeaderText="Retailer Name" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblname" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BillNo" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                        HeaderText="Bill No." ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblbilno" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField SortExpression="CollDt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                        HeaderText="Paid Date" ItemStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lbldate" runat="server" Text='<%# Bind("CollDt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField SortExpression="BillAmt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                        HeaderText="Bill Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblcamount" runat="server" Text='<%# Bind("BillAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CollAmt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                        HeaderText="Paid Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblcamount" runat="server" Text='<%# Bind("CollAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="BalAmt" HeaderStyle-Width="150px" HeaderStyle-ForeColor="white"
                        HeaderText="Bal Amount" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblcamount" runat="server" Text='<%# Bind("BalAmt") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#496a9a" ForeColor="White" Font-Bold="true" />
                <HeaderStyle BackColor="#496a9a" ForeColor="White" Font-Bold="true" />
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
