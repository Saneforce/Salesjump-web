<%@ Page Title="" Language="C#" MasterPageFile="~/Master_SS.master" AutoEventWireup="true" CodeFile="SS_Puchase_Order_View.aspx.cs" Inherits="SuperStockist_Purchase_Order_SS_Puchase_Order_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="frm1" runat="server">
        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />


        <div>

            <div class="row">
                <div class="col-sm-4">
                    <label class="control-label">To :</label>
                </div>
                <div class="col-sm-4">
                    <label class="control-label">Billing Address</label>
                </div>
                <div class="col-sm-4">
                    <label class="control-label">Shipping Address</label>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-4">
                <label style="font-weight: 100;" id="To_Address"></label>
                <label style="font-weight: 100;" id="lbl_To_Address" class="control-label"></label>
            </div>
            <div class="col-sm-4">
                <label style="font-weight: 100;" id="lbl_Bill_Address" class="control-label"></label>
            </div>
            <div class="col-sm-4">
                <label style="font-weight: 100;" id="txt_Ship_add"></label>

            </div>

        </div>

        <div class="row">

            <%--<div class="col-sm-offset-8">
                <input type="checkbox" id="chck" />&nbsp;&nbsp;&nbsp;<span>Same As Billing Address</span>

            </div>--%>
        </div>
        <br />

        <div class="row">
            <div class="form-horizontal">
                <div class="col-sm-4">
                    <label>Purchase Date :</label>
                    <label style="margin-left: 28px; font-weight: 100;" id="pdate"></label>
                </div>
            </div>
            <div class="col-sm-4">
                <label>Expected Date :</label>
                <label style="margin-left: 28px; font-weight: 100;" id="edate"></label>
            </div>
            <div class="col-sm-4">
                <label>Order No :</label>
                 <label style="margin-left: 28px; font-weight: 100;" id="order_id"></label>
           </div>
        </div>

        <br />
        <center>
            <table id="tbl2">
                <tbody>
                </tbody>
            </table>
        </center>

        <div class="card" style="margin-top: 0px;">
            <div class="card-body table-responsive">
                <br />
                <div class="tableBodyScroll">
                    <table class="table table-hover" id="OrderList">
                        <thead class="text-warning">
                            <tr>
                                <th style="text-align: left;">Sl No.</th>
                                <th style="text-align: left;">Product Code</th>
                                <th style="text-align: left;;">Product Name</th>
                                <th style="text-align: right;">Rate</th>
                                <th style="text-align: right;">Unit</th>
                                <th style="text-align: right;">Quantity</th>
                                <th style="text-align: right;">Free</th>
                                <th style="text-align: right;">Dis %</th>
                                 <th style="text-align: right;">Dis_val</th>
                                <th style="text-align: right;display:none;">Amount</th>
                                <th style="text-align: Right; width: 6%;">Tax </th>
                                <th style="text-align: Right; width: 5%;">Amount</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

        <div class="col-sm-7">
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
        /*.tableBodyScroll tbody {
            display: block;
            max-height: 230px;
            overflow-y: scroll;
            width: 102%;
        }

        .tableBodyScroll thead,
        tbody tr {
            display: table;
            width: 100%;
            table-layout: fixed;
        }*/
    </style>

    <script type="text/javascript">
		var total = 0; var qty_total = 0; var free_total = 0; var dis_tot = 0; var dis_val_tot = 0; var tot = 0; var tax_t = 0; var gross_t = 0;
        $(document).ready(function () {
			
            $(function () {
                $('.example-popover').popover({
                    container: 'body'
                })
            })

            var ss = "<%=v%>";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SS_Puchase_Order_View.aspx/GetPriOrderDetails",
                data: "{'Order_No':'" + ss + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    for (var a = 0; a < AllOrders.length; a++) {
                        $('#lbl_Bill_Address').text(AllOrders[a].Billing_Address);
                        $('#txt_Ship_add').text(AllOrders[a].Shipping_Address);
                        $('#lbl_To_Address').text(AllOrders[a].company_add);
                        $('#To_Address').text(AllOrders[a].Div_Name);
 			            $('#sub_tot').val((tot - tax_t).toFixed(2));                        
                        $('#Txt_Dis_tot').val(dis_val_tot);
                        $('#Tax_GST').val(tax_t);
                        $('#in_Tot').val(gross_t);
                        //$('#sub_tot').val(AllOrders[a].sub_tot.toFixed(2));
                        //$('#Txt_Dis_tot').val(AllOrders[a].dis_tot.toFixed(2));
                       // $('#Tax_GST').val(AllOrders[a].tax_tot.toFixed(2));
                       // $('#in_Tot').val(AllOrders[a].Order_Value.toFixed(2));
                        $('#order_id').text(AllOrders[a].Trans_Sl_No);
                        //  var dateAr = AllOrders[a].Order_Date.split('-');
                        //  var newDate = dateAr[2].slice(0, 2) + '-' + dateAr[1] + '-' + dateAr[0];
                        //  console.log(newDate);
                        $("#pdate").text(AllOrders[a].Order_Date);
                        $("#edate").text(AllOrders[a].Expect_Date);
                    }
                },
                error: function (result) {
                }
            });

            function ReloadTable() {
               
                $("#OrderList TBODY").html("");
                $("#free_table TBODY").html("");
                for ($i = 0; $i < Orders.length; $i++) {
                    //var gross = parseFloat(Orders[$i].value) - parseFloat(Orders[$i].discount_price) + parseFloat(Orders[$i].tax);
                    var gross = parseFloat(Orders[$i].value) - parseFloat(Orders[$i].discount_price);
                    //var amt = parseFloat(Orders[$i].value) - parseFloat(Orders[$i].discount_price);
                    var amt = parseFloat(Orders[$i].value) - parseFloat(Orders[$i].discount_price);
                    var amtsub = parseFloat(Orders[$i].value) - parseFloat(Orders[$i].discount_price)-parseFloat(Orders[$i].tax);
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td>" +
                        "<td> " + Orders[$i].Product_Name + "</td>" +
                        "<td align='right'>" + parseFloat(Orders[$i].Rate).toFixed(2) + "</td>" +
                        "<td align='right'>" + Orders[$i].Product_Unit_Name + "</td>" +
                        "<td align='right'>" + Orders[$i].CQty /Orders[$i].con_fact  + "</td>" +
                   //  "<td align='right'>" + Orders[$i].Free + "</td>" +
                        "<td align='right'><a href='#' class='example-popover' data-toggle='popover' data-trigger='hover' data-placement='top' data-content='" + Orders[$i].Offer_ProductCd + " - " + Orders[$i].Free + " " + Orders[$i].Offer_Product_Unit + " '>" + Orders[$i].Free + "</a></td>" +
                        "<td align='right'>" + Orders[$i].discount + "</td>" +
                        "<td align='right'>" + parseFloat(Orders[$i].discount_price).toFixed(2) + "</td>" +
                        //"<td align='right'>" + amtsub.toFixed(2) + "</td>" +
                        "<td align='right'>" + parseFloat(Orders[$i].tax).toFixed(2) + "</td>" +
                        "<td align='right'>" + parseFloat(gross).toFixed(2) + "</td>");

                    qty_total = parseFloat(Orders[$i].CQty || 0) + parseFloat(qty_total);
                    free_total = parseFloat(Orders[$i].Free || 0) + parseFloat(free_total);
                    dis_tot = parseFloat(Orders[$i].discount || 0) + parseFloat(dis_tot);
                    dis_val_tot = parseFloat(Orders[$i].discount_price || 0) + parseFloat(dis_val_tot);
                    tax_t = parseFloat(Orders[$i].tax || 0) + parseFloat(tax_t);
					tot = parseFloat(amt || 0) + parseFloat(tot);
                    
                    gross_t = parseFloat(gross || 0) + parseFloat(gross_t);
                    gross_suv = parseFloat(amtsub || 0) + parseFloat(amtsub);


                    $("#OrderList TBODY").append(tr);

                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    if (Orders[$i].Free != 0 || Orders[$i].Free != "") {
                        $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].Offer_ProductNm + "</td>" +
                            "<td> " + Orders[$i].Offer_ProductCd + "</td>" +
                            "<td align='left'>" + Orders[$i].Offer_Product_Unit + "</td>" +
                            "<td align='left'>" + Orders[$i].Free + "</td>");
                        $("#free_table TBODY").append(tr);
                    }
                }
                $("#OrderList").append('<tfoot><tr><th colspan="4"></th><th style="text-align: right;">Total</th><th style="text-align: right;">' + qty_total + '</th><th style="text-align: right;">' + free_total + '</th><th style="text-align: right;">' + dis_tot + '</th><th style="text-align: right;">' + dis_val_tot.toFixed(2) + '</th><th id="ttt" style="text-align: right;display:none;">' + (tot).toFixed(2) + '</th><th id="tax_tot" style="text-align: right">' + tax_t.toFixed(2) + '</th><th id="gross_tot" style="text-align: right">' + gross_t.toFixed(2) + '</th></tr></tfoot>');
            }
        });

    </script>
</asp:Content>

