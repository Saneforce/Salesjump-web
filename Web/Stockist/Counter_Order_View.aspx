<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Counter_Order_View.aspx.cs" Inherits="Stockist_Counter_Order_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">
        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />

        <div>
            <div class="row">

                <div class="col-md-8">
                    <div class="col-sm-6">
                        <label style="margin-bottom: 15px;" class="col-sm-6">Customer Name :</label>
                        <label style="margin-bottom: 15px; font-weight: 100;" id="Con_cust_name" class="col-sm-6"></label>
                    </div>
                    <div class="col-sm-6">
                        <label class="col-sm-6">Counter Sales Date :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="counter_date"></label>
                    </div>
                </div>

                <div class="col-md-8">

                    <div class="col-sm-6">
                        <label style="margin-bottom: 15px;" class="col-sm-6">Customer Address :</label>
                        <label style="font-weight: 100; margin-bottom: 15px;" id="Cust_Add" class="col-sm-6"></label>
                    </div>

                    <div class="col-sm-6">
                        <label class="col-sm-6">Counter Mobile No :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="counter_Mob"></label>
                    </div>

                </div>

                <div class="col-md-8">
                    <div class="col-sm-6">
                        <label class="col-sm-6">Reference No :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="ref_no"></label>
                    </div>
                    <div class="col-sm-6">
                        <label class="col-sm-6">Counter Sales No :</label>
                        <label style="font-weight: 100;" class="col-sm-6" id="counter_orderno"></label>
                    </div>
                </div>

                <div class="col-md-offset-6">
                    <div class="col-md-offset-4">
                        <label>Notes :</label>
                    </div>
                    <div class="col-md-offset-4">
                        <textarea readonly="" rows="4" autocomplete="off" class="form-control" id="txt_no"></textarea>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <center>
            <table id="tbl2">
                <tbody>
                </tbody>
            </table>
        </center>

        <div class="card">
            <div class="card-body table-responsive">
                <br />
                <div style="white-space: nowrap">
                   <%-- Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />--%>
                    <table class="table table-hover" id="OrderList">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left; width: 6%;">Sl No.</th>
                                <th style="text-align: left; width: 13%;">Product Code</th>
                                <th style="text-align: left; width: 30%;">Product Name</th>
                                <th style="text-align: left; width: 10%;">Unit</th>
                                <th style="text-align: left; width: 10%;">Price</th>
								<th style="text-align: left; width: 10%;">Con_fac</th>
                                <th style="text-align: left; width: 10%;">Qty</th>
                                <th style="text-align: left; width: 10%;">Dis</th>
                                <th style="text-align: left; width: 10%;">Free</th>
                                <th style="text-align: left; width: 10%;">Amount</th>
                                <th style="text-align: left; width: 10%;">Tax</th>
                                <th style="text-align: right; width: 10%;">Gross</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
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
                                    <th>Unit</th>
                                    <th style="width: 14%;">Free</th>
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
                    Subtotal :
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

<%--            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 80px;">
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
            </div>--%>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 80px;">
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


            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 120px;">
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


            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 160px;">
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
    <script type="text/javascript">

        $(document).ready(function () {

            var AllOrders = []; var Orders = [];
            searchKeys = "Product_Name,Unit,Product_Code,";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Counter_Order_View.aspx/Get_Counter_Order_Details",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    for (var a = 0; a < AllOrders.length; a++) {
                        $('#Con_cust_name').text(AllOrders[a].Cus_Name);
                        $('#counter_date').text(AllOrders[a].Order_Date1);
                        $('#Cust_Add').text(AllOrders[a].Cust_Address);
                        $('#counter_orderno').text(AllOrders[a].Order_No);
                        $('#sub_tot').val(AllOrders[a].Sub_Total.toFixed(2));
                        $('#Txt_Dis_tot').val(AllOrders[a].Dis_total.toFixed(2));
                        $('#Tax_GST').val(AllOrders[a].Tax_Total.toFixed(2));
                        $('#Tax_CGST').val(AllOrders[a].CGST.toFixed(2));
                        $('#Tax_SGST').val(AllOrders[a].SGST.toFixed(2));
                        $('#in_Tot').val(AllOrders[a].Total.toFixed(2));
                        $('#txt_no').val(AllOrders[a].Remarks);
                        $('#ref_no').text(AllOrders[a].Ref_No);
                        $('#counter_Mob').text(AllOrders[a].Cust_Mobie_No);

                    }
                },
                error: function (result) {
                }
            });


            function ReloadTable() {
                var tot = 0;
                $("#OrderList TBODY").html("");
                var q_tot = 0; var f_tot = 0; var d_tot = 0; var tot = 0; var tax_t = 0; var gross_t = 0;
                for ($i = 0; $i < Orders.length; $i++) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    var gross = parseFloat(Orders[$i].Amount) + parseFloat(Orders[$i].Tax1);

                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Product_Code + "</td>" +
                        "<td> " + Orders[$i].Product_Name + "</td>" +
                        "<td> " + Orders[$i].Unit + "</td>" +
                        "<td> " + Orders[$i].Price.toFixed(2) + "</td>" +
						"<td> " + parseFloat(Orders[$i].Con_Fac || 0) + "</td>" +
                        "<td>" + Orders[$i].Quantity + "</td>" +
                        "<td>" + Orders[$i].Discount + "</td>" +
                        "<td>" + Orders[$i].Free + "</td>" +
                        "<td>" + Orders[$i].Amount.toFixed(2) + "</td>" +
                        "<td>" + Orders[$i].Tax1.toFixed(2) + "</td>" +
                        "<td  style = 'text-align: right;'>" + parseFloat(gross).toFixed(2) + "</td>"
                    );

                    //  tot = parseFloat(tot) + parseFloat(Orders[$i].Amount);
                    $('#ttt').text(tot);
                    $("#OrderList TBODY").append(tr);

                    q_tot = parseFloat(Orders[$i].Quantity || 0) + parseFloat(q_tot);
                    f_tot = parseFloat(Orders[$i].Free || 0) + parseFloat(f_tot);
                    d_tot = parseFloat(Orders[$i].Discount || 0) + parseFloat(d_tot);
                    tot = parseFloat(Orders[$i].Amount || 0) + parseFloat(tot);
                    tax_t = parseFloat(Orders[$i].Tax1 || 0) + parseFloat(tax_t);
                    gross_t = parseFloat(gross || 0) + parseFloat(gross_t);



                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    if (Orders[$i].Free != 0 || Orders[$i].Free != "") {
                        $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].offer_pro_code + "</td>" +
                            "<td> " + Orders[$i].offer_pro_name + "</td>" +
                            "<td align='left'>" + Orders[$i].offer_pro_unit + "</td>" +
                            "<td align='left'>" + Orders[$i].Free + "</td>");
                        $("#free_table TBODY").append(tr);
                    }
                }
                $("#OrderList").append('<tfoot><tr><th colspan="5"></th><th>Total</th><th>' + q_tot + '</th><th>' + d_tot + '</th><th>' + f_tot + '</th><th id="ttt" style="text-align: right">' + tot.toFixed(2) + '</th><th id="tax_tot" style="text-align: right">' + tax_t.toFixed(2) + '</th><th id="gross_tot" style="text-align: right">' + gross_t.toFixed(2) + '</th></tr></tfoot>');

            }

            $("#tSearchOrd").on("keyup", function () {
                if ($(this).val() != "") {
                    shText = $(this).val().toLowerCase();
                    Orders = AllOrders.filter(function (a) {
                        chk = false;
                        $.each(a, function (key, val) {
                            if (val != null && val.toString().toLowerCase().indexOf(shText) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                                chk = true;
                            }
                        })
                        return chk;
                    })
                }
                else
                    Orders = AllOrders
                ReloadTable();
            })

        });

    </script>
</asp:Content>

