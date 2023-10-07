<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Rpt_Sales_By_Salesman_Details.aspx.cs" Inherits="Stockist_Sales_Rpt_Sales_By_Salesman_Details" %>

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
                    <a href="Rpt_Sales_By_Salesman.aspx" style="font-size: 17px;">Sale by Sales Person</a>
                </div>

                <div class="col-md-4 sub-header">
                    <asp:Label Style="font-size: 14px;" ID="Tit" runat="server"></asp:Label>
                </div>

                <div class="col-md-4">
                    <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                        Style="height: 40px; width: 40px; border-width: 0px; right: 15px; top: 43px;" />
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
                    <table class="table table-hover" id="sales_man_Details">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">Date</th>
                                <th style="text-align: left">Type</th>
                                <th style="text-align: left">Status</th>
                                <th style="text-align: right">Number</th>
                                <th style="text-align: right">Cust Name</th>
                                <th style="text-align: right">Sales</th>
                                <th style="text-align: right">Sales With Tax</th>
                                <th style="text-align: right">Balance Due</th>
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


    <style type="text/css">
        .green {
            color: #28a745;
        }

        .yellow {
            color: #e8a11e;
        }
    </style>


    <script type="text/javascript">

        var AllOrders = []; var Orders = []; var Item_details = [];
        
            var stockist_Code = ("<%=Session["Sf_Code"].ToString()%>");
            var Div_Code = ("<%=Session["div_code"].ToString()%>");

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "Rpt_Sales_By_Salesman_Details.aspx/Get_saleman_details",
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

            var sales = 0; var sales_tax = 0; var due_amt = 0; var sales1 = 0; var sales_tax1 = 0; var due_amt1 = 0; var tot_sales = 0; var tot_sales_tax = 0; var tot_due = 0;

            $("#Item_Details tbody").html('');
            for ($i = 0; Item_details.length > $i; $i++) {

                if ($i < Item_details.length) {

                    if (Item_details[$i].Type == "Invoice") {

                        sales = parseFloat(Item_details[$i].total_sale_without_tax || 0) + parseFloat(sales);
                        sales_tax = parseFloat(Item_details[$i].total_sale_with_tax || 0) + parseFloat(sales_tax);
                        due_amt = parseFloat(Item_details[$i].due) + parseFloat(due_amt);
                        var mius = Item_details[$i].total_sale_without_tax.toFixed(2);
                        var mius1 = Item_details[$i].total_sale_with_tax.toFixed(2);

                    }
                    else {

                        var mius = '-' + Item_details[$i].total_sale_with_tax.toFixed(2);
                        var mius1 = '-' + Item_details[$i].total_sale_without_tax.toFixed(2);
                        sales1 = parseFloat(Item_details[$i].total_sale_without_tax || 0) + parseFloat(sales1);
                        sales_tax1 = parseFloat(Item_details[$i].total_sale_with_tax || 0) + parseFloat(sales_tax1);
                        due_amt1 = parseFloat(Item_details[$i].due) + parseFloat(due_amt1);
                    }

                    tr = $("<tr></tr>");
                    $(tr).html("<td style='display:none';>" + Item_details[$i].Cus_Code + "</td><td>" + Item_details[$i].Date1 + "</td><td id='tye' text-align:'right'>" + Item_details[$i].Type + "</td><td id='stst'>" + Item_details[$i].Status + "</td><td style='text-align:right;' class='clck'><a class='aclck' href='#'</a>" + Item_details[$i].no + "</td><td align='right'>" + Item_details[$i].Cust_Name + "</td><td align='right'>" + mius + "</td><td align='right'>" + mius1 + "</td><td align='right'>" + Item_details[$i].due.toFixed(2) + "</td>");
                    $("#sales_man_Details TBODY").append(tr);
                    // <td><a id='clc' href='#' onclick= \" inv_count_click(" + Item_details[$i].no + ") \" >" + Item_details[$i].no + "</td>mm

                    if (Item_details[$i].Status == 'Paid') {
                        $("td:contains('Paid')").addClass('green');
                    }
                    else if (Item_details[$i].Status == 'Partially Paid') {
                        $("td:contains('Partially Paid')").addClass('yellow');
                    }

                    tot_sales = sales - sales1;
                    tot_sales_tax = sales_tax - sales_tax1;
                    tot_due = due_amt - due_amt1;

                }
                $("#sales_man_Details TFOOT").html("<tr><td colspan='4'></td><td style='font-weight: bold;text-align:right;'>Total :</td></td><td style='font-weight: bold;text-align: right;'>" + tot_sales.toFixed(2) + "</td><td style='font-weight: bold;text-align: right;'>" + tot_sales_tax.toFixed(2) + "</td><td style='text-align: right;font-weight: bold;'>" + tot_due.toFixed(2) + "</td></tr>");
            }
        }


        $(".clck").on("click", function () {

            var row = $(this).closest('tr');
            var type = row.find('#tye').text();
            var order_no = $(this).text();
            var st = row.find('#stst').text();

            if (type == 'Invoice') {
                window.location.href = '../../../../Stockist/Invoice_Order_View.aspx?Order_No=' + order_no + '&Stockist_Code=' + stockist_Code + '&Div_Code=' + Div_Code + '&Status=' + st + '';
            }
            else if (type == 'Credit Note') {
                window.location.href = '../../../../Stockist/Credit_Note_View.aspx?Order_No=' + order_no + '&Stockist_Code=' + stockist_Code + '&Div_Code=' + Div_Code + '';
            }

        });

    </script>
    </html>

</asp:Content>

