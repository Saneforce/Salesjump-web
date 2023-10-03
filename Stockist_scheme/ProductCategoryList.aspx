<%@ Page Title="Product-Category" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master_DIS.master" CodeFile="ProductCategoryList.aspx.cs"
    Inherits="MasterFiles_ProductCategoryList" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-lg-12 sub-header">Product Category List<span style="float: right; display: none"><a href="ProductDetail.aspx" class="btn btn-primary btn-update">Add New</a></span></div>
    </div>

    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" autocomplete="off" style="width: 250px;" />
                <label style="float: right">
                    Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    entries</label>
            </div>
            <table class="table table-hover" id="Product_Cat_List">
                <thead class="text-warning">
                    <tr>
                        <th style="text-align: left">Sl.No</th>
                        <th style="text-align: left">Code</th>
                        <th style="text-align: left">Name</th>
                        <th style="text-align: left">No.of.Products</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination">
                            <li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto;" tabindex="0" aria-hidden="true">
        <div class="modal-dialog" role="document" style="width: 1200px !important">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Product Category List</h5>
                    <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12" style="padding: 15px">
                            <div class="tableBodyScroll">
                                <table class="table table-hover" id="Product_Cat_Details_List">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">Sl.No</th>
                                            <th style="text-align: left">Product_Code</th>
                                            <th style="text-align: left">Product_Name</th>
                                            <th style="text-align: left">Sale_Erp_Code</th>
                                            <th style="text-align: left">Con_Factor</th>
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

    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = []; var Product_details = []; var filter_product = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Cat_SName,Product_Cat_Name,cat_count,";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            }
        );

        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
            //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg = (pgNo > 7) ? (parseInt(pgNo) + 1) - 7 : 1;
            if ((Nxtpg) == pgNo) {
                selpg = (parseInt(TotalPg)) - 7;
                selpg = (selpg > 1) ? selpg : 1;
            }
            spg = '<li class="paginate_button previous" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }

        function ReloadTable() {
            $("#Product_Cat_List TBODY").html("");
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Product_Cat_SName + "</td><td>" + Orders[$i].Product_Cat_Name + "</td><td class='clc'><a id='newsorder' onclick=get_cat(" + Orders[$i].Product_Cat_Code + ")  href='#'>" + Orders[$i].cat_count + "</a></td>");
                    $("#Product_Cat_List TBODY").append(tr);
                    // <td id='cat_code' style='display:none';>" + Orders[$i].Product_Cat_Code + "</td>
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }

        function View_Product_details(filter_product) {

            $("#Product_Cat_Details_List TBODY").html("");
            //  st = PgRecords * (pgNo - 1);
            for ($i = 0; filter_product.length > $i; $i++) {
                if ($i < filter_product.length) {
                    tr = $("<tr></tr>");
                    //$(tr).html("<td>" + ($i + 1) + "</td><td>" + filter_product[$i].Sale_Erp_Code + "</td><td>" + filter_product[$i].Product_Detail_Name + "</td><td>" + filter_product[$i].Sample_Erp_Code + "</td><td>" + filter_product[$i].product_grosswt + "</td><td>" + filter_product[$i].product_netwt + "</td>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + filter_product[$i].Product_Detail_Code + "</td><td>" + filter_product[$i].Product_Detail_Name + "</td><td>" + filter_product[$i].Sale_Erp_Code + "</td><td>" + filter_product[$i].Sample_Erp_Code + "</td>");                    					
                    $("#Product_Cat_Details_List TBODY").append(tr);
                    // <td id='cat_code' style='display:none';>" + Orders[$i].Product_Cat_Code + "</td>
                }
            }
            //  $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            //  loadPgNos();
        }


        $("#tSearchOrd").on("keyup", function () {
            if ($(this).val() != "") {
                shText = $(this).val();
                Orders = AllOrders.filter(function (a) {
                    chk = false;
                    $.each(a, function (key, val) {
                        if (val != null && val.toString().toLowerCase().indexOf(shText.toLowerCase()) > -1 && (',' + searchKeys).indexOf(',' + key + ',') > -1) {
                            chk = true;
                        }
                    })
                    return chk;
                })
            }
            else
                Orders = AllOrders
              pgNo = 1;
            ReloadTable();
        })
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                //   async: false,
                url: "ProductCategoryList.aspx/GetProductCate",
                data: "{'div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }

        function get_cat(cat_no) {

            filter_product = Product_details.filter(function (a) {
                return (a.Product_Cat_Code == cat_no);
            });
            $('#exampleModal').modal('toggle');
            View_Product_details(filter_product);

        }



        $(document).ready(function () {
            loadData();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "ProductCategoryList.aspx/Get_product_details",
                data: "{'div':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    Product_details = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        });




    </script>
</asp:Content>
