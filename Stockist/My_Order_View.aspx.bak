<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="My_Order_View.aspx.cs" Inherits="Stockist_My_Order_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="frm1" runat="server">

        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />
        <asp:HiddenField ID="hid_status" runat="server" />

        <div>
            <div class="row">

                <div class="col-sm-4">
                    <label style="margin-bottom: 15px; display: block" class="control-label">Customer :</label>
                </div>

                <div class="col-sm-3">
                    <label class="col-sm-6">Order No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="lbl_Order_no"></label>
                </div>
                <div class="col-sm-3">
                    <label class="col-sm-6">Notes:</label>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-4">
                    <label style="font-weight: 100; margin-bottom: 15px; display: block;" id="lbl_Cust_Name" class="control-label"></label>
                    <label style="font-weight: 100; margin-bottom: 15px; display: block;" id="lbl_Cust_Address" class="control-label"></label>
                </div>
                <div class="col-sm-3">
                    <label class="col-sm-6">Order Date :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="lbl_orderdate"></label>
                    <div style="padding: 32px 0px 0px 2px;">
                        <label class="col-sm-6">Status :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="lbl_status"></label>
                    </div>
                </div>
                <div class="col-sm-4">
                    <textarea readonly="" rows="4" autocomplete="off" class="form-control" id="txt_no"></textarea>
                </div>
            </div>
        </div>

        <%--       <div class="row">
            <div class="col-sm-8" style="margin-right: -23px;">
            </div>
            <div class="col-sm-3">
                <label class="col-sm-6">Status :</label>
                <label style="font-weight: 100;" class="col-sm-6" id="lbl_status"></label>
            </div>
        </div>--%>

        <div class="card">
            <div class="card-body table-responsive">
                <br />
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left; width: 5%;">Sl No.</th>
                            <th style="text-align: left; width: 15%;">Product Code</th>
                            <th style="text-align: left; width: 30%;">Product Name</th>
                            <th style="text-align: left; width: 5%;">Unit</th>
                            <th style="text-align: Right; width: 7%;">Rate</th>
                            <th style="text-align: Right; width: 8%;">Quantity</th>
                            <th style="text-align: Right; width: 6%;">Free</th>
                            <th style="text-align: Right; width: 8%;">Discount</th>
                            <th style="text-align: Right; width: 8%;">Amount</th>
                            <th style="text-align: Right; width: 6%;">Tax </th>
                            <th style="text-align: Right; width: 5%;">Gross</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>

        <div class="col-sm-7" style="padding: 0px 0px 0px 0px;">
            <div class="card freecard">
                <div class="card-body table-responsive">
                    <div style="white-space: nowrap; padding: 0px 0px 7px 7px; font-weight: 900;">View Offer Products</div>
                    <div class="tddd">
                        <table id="free_table" class="table table-hover">
                            <thead class="text-warning">
                                <tr>
                                    <th style="width: 14%;">Sl No.</th>
                                    <th style="width: 22%;">Product Code </th>
                                    <th style="width: 37%;">Product Name</th>
                                    <th style="width: 14%;">Free</th>
                                    <th>Unit</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" style="margin-top: 27px;">
            <div class="col-sm-offset-7 form-horizontal">
                <label class=" col-xs-3 control-label">
                    Sub total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-3 control-label">
                    Dis Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 80px;">
                <label class=" col-xs-3 control-label">
                    Tax Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 120px;">
                <label class=" col-xs-3 control-label">
                    Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="in_Tot" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>
        </div>

    </form>

    <style type="text/css">
        .green {
            color: #28a745;
        }

        .red {
            color: #f10b0b;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            var ss = "<%=v%>";
            var stat = "<%=status%>";

            
          //  window.history.go(1);

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "My_Order_View.aspx/GetSecOrderDetails",
                data: "{'Order_No':'" + ss + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();

                    $('#lbl_Cust_Name').text(AllOrders[0].ListedDr_Name);
                    $('#lbl_Cust_Address').text(AllOrders[0].ListedDr_Address1);
                    $('#lbl_Order_no').text(AllOrders[0].Trans_Sl_No);
                    $('#lbl_orderdate').text(AllOrders[0].Order_Date);
                    //   $('#ttt').text(parseFloat(AllOrders[0].Sub_Total).toFixed(2));
                    $('#sub_tot').val(parseFloat(AllOrders[0].Sub_Total).toFixed(2));
                    $('#Txt_Dis_tot').val(parseFloat(AllOrders[0].Dis_Total||0).toFixed(2));
                    $('#Tax_GST').val(parseFloat(AllOrders[0].Tax_Total).toFixed(2));
                    $('#in_Tot').val(parseFloat(AllOrders[0].Order_Value).toFixed(2));
                    $('#lbl_status').text(stat);
                    $('#txt_no').val(AllOrders[0].Remarks);

                    if (stat == 'Pending') {
                        $('#lbl_status').css('color', '#f10b0b')
                    }
                    else if (stat == 'Invoiced') {
                        $('#lbl_status').css('color', '#28a745')
                    }
                },
                error: function (result) {
                }
            });

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                var q_tot = 0; var f_tot = 0; var d_tot = 0; var tot = 0; var tax_t = 0; var gross_t = 0;
                for ($i = 0; $i < Orders.length; $i++) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;

                    var gross = parseFloat(Orders[$i].value);

                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td>" +
                        "<td> " + Orders[$i].Product_Name + "</td>" +
                        "<td> " + Orders[$i].Unit_Name + "</td>" +
                        "<td align='right'>" + Orders[$i].Rate.toFixed(2) + "</td>" +
                        "<td align='right'>" + Orders[$i].qty + "</td><td align='right'>" + Orders[$i].free + "</td><td align='right'>" + Orders[$i].discount + "</td>" +
                        "<td align='right'>" + Orders[$i].value.toFixed(2) + "</td><td align='right'>" + Orders[$i].Tax_value.toFixed(2) + "</td><td align='right'>" + parseFloat(gross).toFixed(2) + "</td>");
                    $("#OrderList TBODY").append(tr);

                    q_tot = parseFloat(Orders[$i].qty || 0) + parseFloat(q_tot||0);
                    f_tot = parseFloat(Orders[$i].free || 0) + parseFloat(f_tot||0);
                    d_tot = parseFloat(Orders[$i].discount || 0) + parseFloat(d_tot||0);
                    tot = parseFloat(Orders[$i].value || 0) + parseFloat(tot||0);
                    tax_t = parseFloat(Orders[$i].Tax_value || 0) + parseFloat(tax_t||0);
                    gross_t = parseFloat(gross || 0) + parseFloat(gross_t||0);

                }
                $("#OrderList").append('<tfoot><tr><th colspan="4"></th><th style="text-align: right">Total</th><th style="text-align: right">' + q_tot + '</th><th style="text-align: right">' + f_tot + '</th><th style="text-align: right">' + d_tot + '</th><th id="ttt" style="text-align: right">' + tot.toFixed(2) + '</th><th id="tax_tot" style="text-align: right">' + tax_t.toFixed(2) + '</th><th id="gross_tot" style="text-align: right">' + gross_t.toFixed(2) + '</th></tr></tfoot>');

            }

        });
    </script>

</asp:Content>

