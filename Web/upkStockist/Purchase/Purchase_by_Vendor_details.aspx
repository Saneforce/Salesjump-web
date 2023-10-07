<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Purchase_by_Vendor_details.aspx.cs" Inherits="Stockist_Purchase_Purchase_by_Vendor_details" %>

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
                    <a href="Purchase_by_Vendor.aspx" style="font-size: 17px;">Purchase by Vendor</a>
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
                    <table class="table table-hover" id="Purchase_Vendor_Details">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left">Date</th>                                                                
                                <th style="text-align: left">Purchase Order No</th>
                                <th style="text-align: left">Supplier Name</th>
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
            url: "Purchase_by_Vendor_details.aspx/Get_Purchase_details",
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

            var amt = 0; 

            $("#Purchase_Vendor_Details tbody").html('');
            for ($i = 0; Item_details.length > $i; $i++) {

                if ($i < Item_details.length) {

                    tr = $("<tr></tr>");
                    $(tr).html("<td style='display:none';>" + Item_details[$i].Sup_No + "</td><td>" + Item_details[$i].dat + "</td><td class='clck'><a id='clc' href='../../../../Stockist/Puchase_Order_View.aspx?Order_No=" + Item_details[$i].Order_No + "&Stockist_Code=" + stockist_Code + "&Div_Code=" + Div_Code + "'>" + Item_details[$i].Order_No + "</td><td>" + Item_details[$i].Sup_Name + "</td><td align='right'>" + Item_details[$i].Order_Value.toFixed(2) + "</td>");
                    $("#Purchase_Vendor_Details TBODY").append(tr);                     

                     amt = parseFloat(Item_details[$i].Order_Value) + parseFloat(amt);

                }
                $("#Purchase_Vendor_Details TFOOT").html("<tr><td style='font-weight: bold;'>Total :</td><td colspan='2'></td><td style='font-weight: bold;text-align: right;'>" + amt.toFixed(2) + "</td></tr>");
            }
        }
        
    </script>
    </html>

</asp:Content>

