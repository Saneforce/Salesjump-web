<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Invoice_Order_View.aspx.cs" Inherits="Stockist_Invoice_Order_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">

        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />


        <div style="padding-top: 10px;">
            <div class="row">
                <div class="col-sm-4">
                    <label style="margin-bottom: 15px;" class="control-label">Customer :</label>
                    <label style="font-weight: 100; margin-bottom: 15px;" id="lbl_Cust_Name" class="control-label"></label>
                </div>

                <div class="col-sm-3">
                    <label class="col-sm-6">Order No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="lbl_Order_no"></label>
                </div>

                <div class="col-sm-3">
                    <label class="col-sm-6">Notes :</label>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-4">
                    <label style="margin-bottom: 15px;" class="control-label">Customer Address :</label>
                    <label style="font-weight: 100; margin-bottom: 15px; display: block;" id="lbl_Cust_Address" class="control-label"></label>
                </div>

                <div class="col-sm-3">
                    <label class="col-sm-6">Invoice Date :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="lbl_Invoice_date"></label>
                    <div style="padding: 32px 0px 0px 2px;">
                        <label class="col-sm-6">Status :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="lbl_status"></label>
                    </div>
                </div>

                <div class="col-sm-4">
                    <textarea readonly="" rows="3" autocomplete="off" class="form-control" id="txt_no"></textarea>
                </div>

            </div>

            <div class="row">

                <div class="col-sm-4">
                    <label style="margin-bottom: 15px;" class="control-label">Invoice No :</label>
                    <label style="font-weight: 100; margin-bottom: 15px;" id="lbl_invoice_no" class="control-label"></label>
                </div>

            </div>


        </div>

        <div class="card">
            <div class="card-body table-responsive">
                <br />
                <div style="white-space: nowrap">
                    <table class="table table-hover" id="OrderList">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left; width: 5%;">Sl No.</th>
                                <th style="text-align: left; width: 13%;">Product Code</th>
                                <th style="text-align: left; width: 30%;">Product Name</th>
                                <th style="text-align: left; width: 6%;">Unit</th>
                                <th style="text-align: left; width: 7%;">Price</th>
                                <th style="text-align: left; width: 7%;">Con Fac</th>
                                <th style="text-align: left; width: 7%;">Quantity</th>
                                <th style="text-align: left; width: 5%;">Free</th>
                                <th style="text-align: left; width: 5%;">Gross</th>
                                <th style="text-align: left; width: 6%;">Discount</th>
                                <th style="text-align: center; width: 8%;">Net</th>
                                <th style="text-align: right; width: 6%;">Tax</th>
                                <th style="text-align: right; width: 5%;">Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="col-sm-8" style="padding: 0px 0px 0px 0px;">
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
            <div class="col-sm-offset-8 form-horizontal">
                <label class=" col-xs-3 control-label">
                    Gross :
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


            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-3 control-label">
                    Discount:
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

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
                <label class=" col-xs-3 control-label">
                    Net :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Txt_net" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>


            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px; display: none;">
                <label class=" col-xs-3 control-label">
                    GST :
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

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 120px;">
                <label class=" col-xs-3 control-label">
                    CGST :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_CGST" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>


            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 160px;">
                <label class=" col-xs-3 control-label">
                    SGST :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_SGST" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>


            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 200px;">
                <label class=" col-xs-3 control-label">
                    IGST :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_IGST" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>

           <%-- <div class="col-sm-offset-8 form-horizontal" style="margin-top: 240px;">
                <label class=" col-xs-3 control-label">
                    TCS :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="txt_tcs" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>--%>


<%--            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 280px;">
                <label class=" col-xs-3 control-label">
                    TDS :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="txt_tds" data-format="0,0.00" class="form-control" readonly />
                    </div>
                </div>
            </div>--%>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 239px;">
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

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 240px; display: none;">
                <label class=" col-xs-3 control-label">
                    Amt Paid:  
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="K1" id="Ad_Paid" autocomplete="off" data-format="0,0.00" readonly class="form-control validate" data-validation="required"
                            name="amountpaid" />
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 282px; display: none;">
                <label class=" col-xs-3 control-label">
                    Amt Due: 
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="V1" id="Amt_Tot" data-format="0,0.00" class="form-control" readonly
                            name="amountdue" />
                    </div>
                </div>
            </div>
            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 323px; display: none">
                <label class=" col-xs-3 control-label">
                    Adv Amt: 
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="V1" id="adv_amt" data-format="0,0.00" class="form-control" readonly
                            name="amountdue" />
                    </div>
                </div>
            </div>

        </div>



        <style type="text/css">
            .green {
                color: #28a745;
            }

            .yellow {
                color: #e8a11e;
            }
        </style>


        <script type="text/javascript">

            $(document).ready(function () {

                var Order_no = "<%=Order_No%>";
                var st = '<%=stat%>';
                var tcs = ''; var tds = '';
                var AllOrders = []; var Orders = [];

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Invoice_Order_View.aspx/GetInvoiceOrderDetails",
                    data: "{'Order_No':'" + Order_no + "'}",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders; ReloadTable();

                        $('#lbl_Cust_Name').text(AllOrders[0].ListedDr_Name);
                        $('#lbl_Cust_Address').text(AllOrders[0].ListedDr_Address1);
                        $('#lbl_Order_no').text(AllOrders[0].Order_No);
                        $('#lbl_Invoice_date').text(AllOrders[0].Invoice_Date1);
                        $('#lbl_invoice_no').text(AllOrders[0].Trans_Inv_Slno);
                        //   $('#sub_tot').val(parseFloat(AllOrders[a].Sub_Total).toFixed(2));
                        //  $('#Txt_Dis_tot').val(parseFloat(AllOrders[a].Dis_total).toFixed(2));
                        //$('#Tax_GST').val(parseFloat(AllOrders[a].Tax_Total).toFixed(2));
                        //$('#Tax_CGST').val(parseFloat(AllOrders[a].Head_cgst).toFixed(2));
                        //$('#Tax_SGST').val(parseFloat(AllOrders[a].Head_sgst).toFixed(2));
                        //$('#Tax_IGST').val(parseFloat(AllOrders[a].Head_igst).toFixed(2));
                        //   $('#in_Tot').val(parseFloat(AllOrders[a].Total).toFixed(2));
                       // $('#txt_tcs').val(AllOrders[0].tcs.toFixed(2));
                        //$('#txt_tds').val(AllOrders[0].tds.toFixed(2));
                        $('#Ad_Paid').val(parseFloat(AllOrders[0].Adv_Paid).toFixed(2));
                        $('#Amt_Tot').val(AllOrders[0].Amt_Due.toFixed(2));
                        $('#lbl_status').text(st);
                        $('#txt_no').val(AllOrders[0].Remarks);
                        $('#adv_amt').val(parseFloat(AllOrders[0].adv_amt).toFixed(2));
                        tcs = parseFloat(AllOrders[0].tcs); tds = parseFloat(AllOrders[0].tds);

                        if (st == 'Paid') {
                            $('#lbl_status').css('color', '#28a745')
                        }
                        else if (st == 'Partially Paid') {
                            $('#lbl_status').css('color', '#e8a11e')
                        }
                        else if (st == 'Pending') {
                            $('#lbl_status').css('color', '#ff8080')
                        }

                    },
                    error: function (result) {
                    }
                });

                function ReloadTable() {
                    $("#OrderList TBODY").html("");
                    var q_tot = 0; var f_tot = 0; var dis_val_tot = 0; var tot = 0; var tax_t = 0; var gross_total = 0; var Net_total = 0; var Invoice_Total = 0;
                    var total_cgst = 0; var total_sgst = 0; var total_igst = 0;
                    for ($i = 0; $i < Orders.length; $i++) {
                        tr = $("<tr></tr>");
                        slno = $i + 1;

                        var Gross =(parseFloat(Orders[$i].Con_Fac)*parseFloat(Orders[$i].qty)) * parseFloat(Orders[$i].Price);
                        var amt = (parseFloat(Orders[$i].Con_Fac)*parseFloat(Orders[$i].qty)) * parseFloat(Orders[$i].Price) - parseFloat(Orders[$i].Discount);
                        var gross = (parseFloat(Orders[$i].Con_Fac)*parseFloat(Orders[$i].qty)) * parseFloat(Orders[$i].Price) - parseFloat(Orders[$i].Discount) + parseFloat(Orders[$i].Tax);

                        $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td>" +
                            "<td> " + Orders[$i].Product_Name + "</td>" +
                            "<td> " + Orders[$i].Unit + "</td>" +
                            "<td align='left'>" + Orders[$i].Price.toFixed(2) + "</td>" +
                            "<td align='left'> " + Orders[$i].Con_Fac + "</td>" +
                            "<td align='left'>" + Orders[$i].qty + "</td>" +
                            "<td align='left'>" + Orders[$i].Free + "</td>" +
                            "<td align='left'>" + parseFloat(Gross).toFixed(2) + "</td>" +
                            "<td align='left'>" + Orders[$i].Discount + "</td>" +
                            "<td align='left'>" + parseFloat(amt).toFixed(2) + "</td>" +
                            "<td align='right'>" + Orders[$i].Tax.toFixed(2) + "</td><td align='right'>" + parseFloat(gross).toFixed(2) + "</td>");
                        $("#OrderList TBODY").append(tr);

                        total_cgst = parseFloat(Orders[$i].Detail_CGST || 0) + parseFloat(total_cgst);
                        total_sgst = parseFloat(Orders[$i].Detail_SGST || 0) + parseFloat(total_sgst);
                        total_igst = parseFloat(Orders[$i].Detail_IGST || 0) + parseFloat(total_igst);
                        q_tot = parseFloat(Orders[$i].qty || 0) + parseFloat(q_tot);
                        f_tot = parseFloat(Orders[$i].Free || 0) + parseFloat(f_tot);
                        gross_total = parseFloat(Gross || 0) + parseFloat(gross_total);
                        dis_val_tot = parseFloat(Orders[$i].Discount || 0) + parseFloat(dis_val_tot);
                        Net_total = parseFloat(amt || 0) + parseFloat(Net_total);
                        Invoice_Total = parseFloat(gross || 0) + parseFloat(Invoice_Total);
                        tax_t = parseFloat(Orders[$i].Tax || 0) + parseFloat(tax_t);

                        $('#sub_tot').val(gross_total.toFixed(2));
                        $('#Txt_Dis_tot').val(dis_val_tot.toFixed(2));
                        $('#Txt_net').val(Net_total.toFixed(2));
                        $('#Tax_GST').val(tax_t.toFixed(2));
                        $('#Tax_CGST').val(total_cgst.toFixed(2));
                        $('#Tax_SGST').val(total_sgst.toFixed(2));
                        $('#Tax_IGST').val(total_igst.toFixed(2));

                        tr = $("<tr></tr>");
                        slno = $i + 1;
                        if (Orders[$i].Free != 0 || Orders[$i].Free != "") {
                            $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Off_Pro_Code + "</td>" +
                                "<td> " + Orders[$i].Off_Pro_Name + "</td>" +
                                "<td align='center'>" + Orders[$i].Free + "</td>" +
                                "<td align='center'>" + Orders[$i].Off_Pro_Unit + "</td>");
                            $("#free_table TBODY").append(tr);
                        }
                    }
                  

                    if (tcs == 'null' || typeof tcs == "undefined" || tcs == "" || isNaN(tcs)) { tcs = 0; }
                    if (tds == 'null' || typeof tds == "undefined" || tds == "" || isNaN(tds)) { tds = 0; }

                    Invoice_Total = Invoice_Total + tcs + tds;
                    $('#in_Tot').val(Math.round(Invoice_Total).toFixed(2));

                    $("#OrderList").append('<tfoot><tr><th colspan="5"></th><th>Total</th><th>' + q_tot + '</th><th>' + f_tot + '</th><th>' + parseFloat(gross_total).toFixed(2) + '</th><th>' + dis_val_tot.toFixed(2) + '</th><th id="ttt">' + Net_total.toFixed(2) + '</th><th id="tax_tot" style="text-align: right">' + tax_t.toFixed(2) + '</th><th id="gross_tot" style="text-align: right">' + Invoice_Total.toFixed(2) + '</th></tr></tfoot>');
                }
            });

        </script>
    </form>
</asp:Content>

