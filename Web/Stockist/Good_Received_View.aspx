<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="Good_Received_View.aspx.cs" Inherits="Stockist_Good_Received_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">
        <asp:HiddenField ID="hid_order_no" runat="server" />
        <asp:HiddenField ID="hid_Stockist" runat="server" />
        <asp:HiddenField ID="hid_div" runat="server" />

        <div class="row">
            <div class="col-md-8">
                <div class="col-sm-6">
                    <label style="margin-bottom: 15px;" class="col-sm-6">Grn No :</label>
                    <label style="margin-bottom: 15px;font-weight: 100;" id="grn_no" class="col-sm-6"></label>
                </div>

                <div class="col-sm-6">
                    <label class="col-sm-6">Grn Date :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="grn_date"></label>
                </div>
            </div>
            <div class="col-md-8">
                <div class="col-sm-6">
                    <label style="margin-bottom: 15px;" class="col-sm-6">Supplier Name :</label>
                    <label style="margin-bottom: 15px; font-weight: 100"; id="sup_name" class="col-sm-6"></label>
                </div>

                <div class="col-sm-6">
                    <label class="col-sm-6">Dispatch Date :</label>
                    <label style="font-weight: 100"; id="dis_date" class="col-sm-6"></label>
                </div>           
            </div>

            <div class="col-md-8">
                <div class="col-sm-6">
                    <label style="margin-bottom: 15px;" class="col-sm-6">Po No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="po_no"></label>
                </div>

                 <div class="col-sm-6">
                    <label class="col-sm-6">PI No :</label>
                    <label style="font-weight: 100;" class="col-sm-6" id="pi_no"></label>
                </div>

            </div>

            <div class="col-md-8">
                <div class="col-sm-6">
                    <label style="margin-bottom: 15px;" class="col-sm-6">Authorised By:</label>
                    <label style="margin-bottom: 15px; font-weight: 100;" class="col-sm-6" id="aut_by"></label>
                </div>

				<div class="col-sm-6">
                    <label class="col-sm-6">Received By :</label>
                    <label style="font-weight: 100"; id="rec_name" class="col-sm-6"></label>
                </div>  
             </div>


            <div class="col-md-8">
                <div class="col-sm-6">
                    <label style="margin-bottom: 15px;" class="col-sm-6">Entry Date:</label>
                    <label style="margin-bottom: 15px; font-weight: 100;" class="col-sm-6" id="lbl_entry_date"></label>
                     </div>  
             </div>


            <div class="col-md-offset-6">
                	<div class="col-md-offset-4">
                    <label>Remarks :</label>
                </div>				
				<div class="col-md-offset-4">
                    <textarea id="re" rows="4" readonly="readonly" class="form-control"></textarea>
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
                <table class="table table-hover" id="OrderList">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left; width: 5%;">SlNo</th>
                            <th style="text-align: left; width: 13%;">Product Code</th>
                            <th style="text-align: left; width: 30%;">Product Name</th>
                            <th style="text-align: left; width: 10%;">Batch No</th>
                            <th style="text-align: left; width: 10%;">UOM</th>
                            <th style="text-align: left; width: 10%;">Rate</th>
                            <%--<th style="text-align: left; width: 10%;">Quantity</th>--%>
                            <th style="text-align: left; width: 10%;">Quantity</th>
                            <%--  <th style="text-align: left; width: 10%;">Damage</th>--%>
                            <th style="text-align: left; width: 10%;">Net_value</th>
                            <th style="text-align: left; width: 10%;">Tax</th>
                            <th style="text-align: right; width: 10%;">Gross_value</th>

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
                <label class=" col-xs-3 control-label" style="font-size:16px">
                    Sub total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="sub_tot" data-format="0,0.00" class="form-control" readonly="">
                    </div>
                </div>
            </div>

<%--            <div class="col-sm-offset-8 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-3 control-label">
                    Dis Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Txt_Dis_tot" data-format="0,0.00" class="form-control" readonly="">
                    </div>
                </div>
            </div>--%>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 40px;">
                <label class=" col-xs-3 control-label"  style="font-size:16px">
                    Tax Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="Tax_GST" data-format="0,0.00" class="form-control" readonly="">
                    </div>
                </div>
            </div>

            <div class="col-sm-offset-7 form-horizontal" style="margin-top: 80px;">
                <label class=" col-xs-3 control-label"  style="font-size:16px">
                    Total :
                </label>
                <div class="col-sm-9">
                    <div class="input-group">
                        <div class="input-group-addon currency">
                            <i class="fa fa-inr"></i>
                        </div>
                        <input data-cell="G1" id="in_Tot" data-format="0,0.00" class="form-control" readonly="">
                    </div>
                </div>
            </div>
        </div>



    </form>
    <script type="text/javascript">

        $(document).ready(function () {

            var ss = "<%=Order_ID%>";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Good_Received_View.aspx/GetPriOrderDetails",
                data: "{'Order_No':'" + ss + "'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    for (var a = 0; a < AllOrders.length; a++) {
                        $('#grn_no').text(AllOrders[a].GRN_No);
                        $('#grn_date').text(AllOrders[a].GRN_Date1);
                        $('#sup_name').text(AllOrders[a].Supp_Name);
                        $('#po_no').text(AllOrders[a].Po_No);
                        $('#pi_no').text(AllOrders[a].Challan_No);
                        $('#aut_by').text(AllOrders[a].Authorized_By);
                        $('#rec_name').text(AllOrders[a].Received_By);
                        $('#re').text(AllOrders[a].remarks);
                        $('#sub_tot').val(parseFloat(AllOrders[a].Net_Tot_Goods).toFixed(2));
                        $('#Tax_GST').val(parseFloat(AllOrders[a].Net_Tot_Tax).toFixed(2));
                        $('#in_Tot').val(parseFloat(AllOrders[a].Net_Tot_Value).toFixed(2));
                        $('#dis_date').text(AllOrders[a].Dispatch_Date1);
                        $('#lbl_entry_date').text(AllOrders[a].Entry_Date1);



                    }
                },
                error: function (result) {
                }
            });

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                var net_tot = 0; var tax_tot = 0; var gross_tot = 0; var tot_qty = 0;
                for ($i = 0; $i < Orders.length; $i++) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html("<td>" + slno + "</td><td>" + Orders[$i].PCode + "</td>" +
                        "<td> " + Orders[$i].PDetails + "</td>" +
                        "<td> " + Orders[$i].Batch_No + "</td>" +
                        "<td> " + Orders[$i].uom_name + "</td>" +
                        "<td align='left'>" + (parseFloat(Orders[$i].Price) * parseFloat(Orders[$i].Cnv_qty)).toFixed(2) + "</td>" +
                        //"<td align='left'>" + Orders[$i].POQTY + "</td>" +
                        "<td align='left'>" + Orders[$i].POQTY + "</td>" +
                        "<td align='left' style='display:none;'>" + Orders[$i].Damaged + "</td>" +
                        "<td align='left'>" + parseFloat(Orders[$i].Gross_Value).toFixed(2) + "</td>" +
                        "<td align='left'>" + parseFloat(Orders[$i].Tax).toFixed(2) + "</td>" +
                        "<td align='right'>" + parseFloat(Orders[$i].Net_Value).toFixed(2) + "</td>");
                    $("#OrderList TBODY").append(tr);

                    net_tot = parseFloat(Orders[$i].Gross_Value) + net_tot;
                    tax_tot = parseFloat(Orders[$i].Tax) + tax_tot;
                    gross_tot = parseFloat(Orders[$i].Net_Value) + gross_tot;
                    tot_qty = parseFloat(Orders[$i].POQTY) + tot_qty



                    //$('#dv_total').html("<div class='Row'><div class='col col-lg-7'></div><div class='col col-lg-2'><div class='row'><div style='font-weight: bold;font-size: 15px;' class='col col-sm-5' id='lbl'>Total :</div><div style='font-weight: bold;font-size: 15px;text-align:right;padding-right:35px;' readonly class='col col-sm-7 total1'  id='ttt'></div><input type='hidden' readonly class='total1'  id='ttt'/></div></div></div>");
                }
                $("#OrderList").append('<tfoot><tr><th colspan="6">Total</th><th>' + tot_qty + '</th><th style="text-align:left">' + parseFloat(net_tot).toFixed(2) + '</th><th style="text-align:left">' + parseFloat(tax_tot).toFixed(2) + '</th><th style="text-align:right">' + parseFloat(gross_tot).toFixed(2) + '</th></tr></tfoot>');
            }

        });

    </script>
</asp:Content>

