<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Payment_View.aspx.cs" Inherits="Stockist_Payment_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="frm1" runat="server">
        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />


        <div style="padding-top: 20px;">
            <div class="row">
                <div class="col-sm-8">
                    <label style="margin-bottom: 15px;" class="control-label">Customer Name :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <label style="margin-bottom: 15px;" id="Con_cust_name" class="control-label"></label>
                </div>

                <div class="col-sm-4">
                    <label class="col-sm-6">Payment Date :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="credit_date"></label>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-8">
                    <label style="margin-bottom: 15px;" class="control-label">Customer Address :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <label style="font-weight: 100; margin-bottom: 15px;" id="Cust_Add" class="control-label"></label>
                </div>

                <div class="col-sm-4">
                    <label class="col-sm-6">Payment No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="counter_orderno"></label>
                </div>
            </div>
			<div class="row">
                <div class="col-sm-8">
                     <label style="margin-bottom: 15px;" class="control-label">Reference No :</label>&nbsp;&nbsp;&nbsp;&nbsp;
                     <label style="font-weight: 100; margin-bottom: 15px;" id="ref_no" class="control-label"></label>
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
                    <table class="table table-hover" id="paymentlist">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left;">Sl No.</th>
                                <th style="text-align: left;">Bill No</th>
                                <th style="text-align: left;">Date</th>
                                <th style="text-align: left;">Bill Amount</th>
								<th style="text-align: left;">Pending Amount</th>
                                <th style="text-align: left;">Paid Amount</th>
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


            var AllOrders = []; var Orders = [];
            searchKeys = "ListedDr_Name,Sl_No,bill_no,bill_amt,Pen_amt,paid_amt,";

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "Payment_View.aspx/Get_Payment_details_view",
                dataType: "json",
                async: false,
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    $('#Con_cust_name').text(AllOrders[0].ListedDr_Name);
                    $('#credit_date').text(AllOrders[0].dat);
                    $('#counter_orderno').text(AllOrders[0].Sl_No);
                    $('#Cust_Add').text(AllOrders[0].ListedDr_Address);
					$('#ref_no').text(AllOrders[0].Pay_Ref_No);
                },
                error: function (result) {
                }
            });

            function ReloadTable() {
                $("#paymentlist TBODY").html("");
                for ($i = 0; $i < Orders.length; $i++) {
                    var pend = parseFloat(Orders[$i].Pen_amt || 0).toFixed(2) == parseFloat(Orders[$i].paid_amt || 0).toFixed(2) ? 0.00 : parseFloat(Orders[$i].Pen_amt || 0).toFixed(2);
                    tr = $("<tr></tr>");
                    slno = $i + 1;
					$(tr).html("<td>" + slno + "</td><td>" + Orders[$i].bill_no + "</td>" +
                        "<td> " + Orders[$i].dat + "</td>" +
                        "<td> " + parseFloat(Orders[$i].bill_amt).toFixed(2) + "</td>" +
                        "<td align='left'>" +  pend  + "</td>" +
                        "<td align='left'>" + parseFloat(Orders[$i].paid_amt).toFixed(2)+ "</td>");
                    $("#paymentlist TBODY").append(tr)

                }
                // $("#OrderList").append('<tfoot><tr><th colspan="4">Total</th><th colspan="4" id="ttt" style="text-align: right"></th></tr></tfoot>');
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

