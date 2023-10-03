<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Sales_By_Item_Detail.aspx.cs" Inherits="Stockist_Sales_Rpt_Sales_By_Item_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <html>
    <title>Sales By Customer Details</title>
    <head>
    </head>
    <body>
        <form id="form1" runat="server">

            <div class="row">
                <div class="col-md-4">
                    <span class="glyphicon glyphicon-chevron-left" style="color: #5a89e0;"></span>
                    <a href="Rpt_Sale_By_Item.aspx" style="font-size: 17px;">Sale by Item</a>
                </div>

                <div class="col-md-4 sub-header">
                    <asp:Label Style="font-size: 14px;" ID="Tit" runat="server"></asp:Label>
                </div>

                <div class="col-md-4">
                    <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Style="height: 40px; width: 40px; border-width: 0px; right: 15px; top: 43px;" OnClick="ImageButton1_Click" />
                </div>

            </div>

            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <%--  <h5 style="padding: 0px 0px 0px 23px;" id="date_details"></h5>--%>
                    <asp:Label ID="date_details" runat="server"></asp:Label>
                </div>
            </div>

            <div class="card">
                <div class="card-body table-responsive">
                    <table class="table table-hover" id="Item_Details">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">Customer Name</th>
                                <th style="text-align: left">Quantity</th>
                                <th style="text-align: right">Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                        <tfoot>
                        </tfoot>
                    </table>
                </div>
            </div>

        </form>
    </body>
    <script type="text/javascript">

        var AllOrders = []; var Orders = []; var Item_details = [];

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "Rpt_Sales_By_Item_Detail.aspx/Get_salebyitem_details",
            dataType: "json",
            success: function (data) {
                Item_details = JSON.parse(data.d);
                Bind_Details();
            },
            error: function (result) {
                alert(JSON.stringify(result));
            }
        });


        function Bind_Details() {
            var qty1 = 0; var amt1 = 0; var due_amt = 0;

            $("#Item_Details tbody").html('');
            for ($i = 0; Item_details.length > $i; $i++) {

                if ($i < Item_details.length) {

                    tr = $("<tr></tr>");
                    $(tr).html("<td style='display:none';>" + Item_details[$i].Cus_Code + "</td><td>" + Item_details[$i].Cus_Name + "</td><td text-align:'right'>" + /*Item_details[$i].Qty*/ Item_details[$i].Base_Qty + "</td><td style='text-align: right;'>" + Item_details[$i].Amt.toFixed(2) + "</td>");
                    $("#Item_Details TBODY").append(tr);
                    // <td><a id='clc' href='#' onclick= \" inv_count_click(" + Item_details[$i].no + ") \" >" + Item_details[$i].no + "</td>mm

                    qty1 = parseFloat(Item_details[$i].Base_Qty || 0) + parseFloat(qty1);
                    amt1 = parseFloat(Item_details[$i].Amt || 0) + parseFloat(amt1);
                    // due_amt = parseFloat(Invoice_details[$i].due) + parseFloat(due_amt);

                }
                $("#Item_Details TFOOT").html("<tr><td style='font-weight: bold;'>Total</td><td style='font-weight: bold;'>" + qty1.toFixed(2) + "</td><td style='font-weight: bold;text-align: right;'>" + amt1.toFixed(2) + "</td><td></td></tr>");
            }
        }


    </script>
    </html>



</asp:Content>

