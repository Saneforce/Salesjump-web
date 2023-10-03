<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Credit_Note_View.aspx.cs" Inherits="Stockist_Credit_Note_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="frm1" runat="server">
        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />


        <div style="padding-top: 10px;">
            <div class="row">
                <div class="col-sm-4">
                    <label style="margin-bottom: 15px;" class="control-label">Customer Name :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <label style="margin-bottom: 15px;" id="Con_cust_name" class="control-label"></label>
                </div>

                <div class="col-sm-4">
                    <label class="col-sm-6">Credit Note Date :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="credit_date"></label>
                </div>
                <div class="col-sm-4">
                    <label class="col-sm-6">Notes :</label>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-4">
                    <label style="margin-bottom: 15px;" class="control-label">Customer Address :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <label style="font-weight: 100; margin-bottom: 15px;" id="Cust_Add" class="control-label"></label>
                </div>

                <div class="col-sm-4">
                    <label class="col-sm-6">Credit Note No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="counter_orderno"></label>
                    <label class="col-sm-6" style="padding-top: 5px;">Reference No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="inv_no"></label>
                </div>
                <div class="col-sm-4">
                    <textarea readonly="" rows="3" autocomplete="off" class="form-control" id="txt_no"></textarea>
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
                    Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                    <table class="table table-hover" id="OrderList">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left; width: 6%;">Sl No.</th>
                                <th style="text-align: left; width: 13%;">Product Code</th>
                                <th style="text-align: left; width: 30%;">Product Name</th>
                                <th style="text-align: left; width: 10%;">Unit</th>
                                <th style="text-align: left; width: 10%;">Price</th>
                                <th style="text-align: left; width: 10%;">Qty(In p)</th>
                                <th style="text-align: right; width: 10%;">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">

        $(document).ready(function () {

            var Order_no = "<%=Order_No%>";

            // var st = '<%=stat%>';

            var AllOrders = []; var Orders = [];
            searchKeys = "Sale_Erp_Code,Product_Name,";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Credit_Note_View.aspx/Get_Credit_OrderDetails",
                data: "{'Order_No':'" + Order_no + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    $('#Con_cust_name').text(AllOrders[0].Cus_Name);
                    $('#credit_date').text(AllOrders[0].cr_da);
                    $('#counter_orderno').text(AllOrders[0].Credit_note_no);
                    $('#Cust_Add').text(AllOrders[0].ListedDr_Address);
                    $('#inv_no').text(AllOrders[0].Inv_No);
                    $('#txt_no').text(AllOrders[0].Remarks);
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
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Product_Code + "</td>" +
                        "<td> " + Orders[$i].Product_Name + "</td>" +
                        "<td> " + Orders[$i].Unit + "</td>" +
                        "<td align='left'>" + Orders[$i].Price.toFixed(2) + "</td>" +
                        "<td align='left'>" + Orders[$i].Quantity + "</td>" +
                        "<td align='right'>" + Orders[$i].amt.toFixed(2) + "</td>");
                    $("#OrderList TBODY").append(tr);


                    q_tot = parseFloat(Orders[$i].Quantity || 0) + parseFloat(q_tot);
                 //   f_tot = parseFloat(Orders[$i].free || 0) + parseFloat(f_tot);
                   // d_tot = parseFloat(Orders[$i].discount || 0) + parseFloat(d_tot);
                    tot = parseFloat(Orders[$i].amt || 0) + parseFloat(tot);
                   // tax_t = parseFloat(Orders[$i].Tax_value || 0) + parseFloat(tax_t);
                   // gross_t = parseFloat(gross || 0) + parseFloat(gross_t);


                }
                $("#OrderList").append('<tfoot><tr><th colspan="4"></th><th>Total</th><th>' + q_tot + '</th><th id="ttt" style="text-align: right">' + tot.toFixed(2) + '</th></tr></tfoot>');
            }
            //<th style="text-align: right">' + f_tot + '</th><th style="text-align: right">' + d_tot + '</th><th id="tax_tot" style="text-align: right">' + tax_t.toFixed(2) + '</th><th id="gross_tot" style="text-align: right">' + gross_t.toFixed(2) + '</th>

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

