<%@ Page Title="" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true" CodeFile="CustWise_Bill_Unbill_Report.aspx.cs" Inherits="Stockist_Sales_CustWise_Bill_Unbill_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="container">

        <div class="row">
            <label class="col-md-2  col-md-offset-3  control-label">From Date :</label>
            <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="date" class="form-control" id="from_date" />
                </div>
            </div>

        </div>

        <div class="row">
            <label class="col-md-2  col-md-offset-3  control-label">To Date :</label>
            <div class="col-md-3 inputGroupContainer">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                    <input type="date" class="form-control" id="to_date" />
                </div>
            </div>
        </div>
        <br />
        <center>
            <input type="button" id="btn_view" value="View Details" class="btn btn-info" />
        </center>

        <div class="card" style="max-width: 950px;">
            <div class="card-body table-responsive">
                <table class="table table-hover" id="cust_list">
                    <thead class="text-warning">
                        <tr>
                            <th style='text-align: center;'>Total Customer</th>
                            <th style='text-align: center;'>Billed Customer</th>
                            <th style='text-align: center;'>Un Billed Customer</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                    <tfoot></tfoot>
                </table>
            </div>
        </div>

        <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true" data-keyboard="false">
            <div class="modal-dialog" role="document" style="width: 1200px !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel"></h5>
                        <button type="button" id="btntimesClose" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-12" style="padding: 15px">
                                <table class="table table-hover" id="Cust_Detail_table">
                                    <thead class="text-warning">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Customer Code</th>
                                            <th>Customer Name</th>
                                            <th>Customer Mobile No</th>
                                            <th>Customer Class</th>
                                            <th>Last Order Date</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var Cust_billed_Details = []; var F_Date = ''; var T_Date = ''; var details_Date = [];

        $(document).ready(function () {
            $(".container").parent().css("overflow", "hidden");
            $('#btn_view').click(function () {

                F_Date = $('#from_date').val();
                T_Date = $('#to_date').val();

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "CustWise_Bill_Unbill_Report.aspx/Get_CustWise_billed",
                    data: "{'From_Date':'" + F_Date + "','To_Date':'" + T_Date + "'}",
                    dataType: "json",
                    success: function (data) {
                        Cust_billed_Details = JSON.parse(data.d);
                        $('#cust_list TBODY').html('');
                        for (var g = 0; g < Cust_billed_Details.length; g++) {
                            tr = $("<tr></tr>");
                            $(tr).html("<td style='text-align: center;' >" + Cust_billed_Details[g].Total_Customer + "</td><td style='text-align: center;'><a href='#' fff='Ordered' id='href_list' >" + Cust_billed_Details[g].Order_Customer + "</td><td style='text-align: center;'><a href='#' fff='Un_Order' id='href_list'>" + Cust_billed_Details[g].Un_Ordered_Customer + "</td>");
                            $("#cust_list TBODY").append(tr);
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });

            $(document).on('click', '#href_list', function () {

                var selected_href = $(this).attr('fff');
                $('#exampleModalLabel').text(selected_href + '_Customer_Details');
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "CustWise_Bill_Unbill_Report.aspx/Get_CustWise_billed_Details",
                    data: "{'From_Date':'" + F_Date + "','To_Date':'" + T_Date + "','Type':'" + selected_href + "'}",
                    dataType: "json",
                    success: function (data) {
                        details_Date = JSON.parse(data.d);
                        $('#exampleModal').modal('toggle');
                        $('#Cust_Detail_table TBODY').html('');
                        for (var f = 0; f < details_Date.length; f++) {
                            tr = $("<tr></tr>");
                            $(tr).html("<td>" + (f + 1) + " </td><td>" + details_Date[f].ListedDrCode + "</td><td >" + details_Date[f].ListedDr_Name + "</td><td >" + details_Date[f].Mobile_No + "</td><td >" + details_Date[f].Class + "</td><td >" + details_Date[f].Last_Ordered_Date + "</td>");
                            $("#Cust_Detail_table TBODY").append(tr);
                        }
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });





            });

        });

    </script>

</asp:Content>

