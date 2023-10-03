<%@ Page Title="Category List" Language="C#" MasterPageFile="~/Master_DIS.master" AutoEventWireup="true"
    CodeFile="CategoryCreationList.aspx.cs" Inherits="MasterFiles_CategoryCreationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>--%>
    <form id="form1" runat="server">
        <script language="javascript" type="text/javascript">


            var AllOrders = []; Allbrands = [];
            var sortid = '';
            var asc = true;
            var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Doc_Cat_SName,Doc_Cat_Name,Cat_Count";
            $(document).ready(function () {
                loadData();
                $("#SearchOrd").on("keyup", function () {
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
                });
                $(".data-table-basic_length").on("change",
                    function () {
                        pgNo = 1;
                        PgRecords = $(this).val();
                        ReloadTable();
                    }
                );
            });

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

            $('th').on('click', function () {
                sortid = this.id;
                asc = $(this).attr('asc');
                if (asc == undefined) asc = 'true';
                Orders.sort(function (a, b) {
                    if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true')
                        return -1;
                    if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true')
                        return 1;

                    if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false')
                        return -1;
                    if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false')
                        return 1;
                    return 0;
                });

                $(this).attr('asc', ((asc == 'true') ? 'false' : 'true'));
                ReloadTable();
            });

            function ReloadTable() {
                $("#OrderList TBODY").html("");
                st = PgRecords * (pgNo - 1);
                for ($i = st; $i < st + PgRecords; $i++) {
                    if ($i < Orders.length) {
                        tr = $("<tr></tr>");
                        //$(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Doc_Special_Code + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + Orders[$i].Product_Brd_Name + "</td><td>" + Orders[$i].Product_Cat_Name + "</td><td>" + Orders[$i].Product_Sale_Unit + "</td><td>" + Orders[$i].product_unit + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Sample_Erp_Code + "</td><td>" + parseFloat(Orders[$i].RP_Base_Rate).toFixed(2) + "</td><td>" + parseFloat(Orders[$i].RP_Case_Rate).toFixed(2) + "</td>");
                        $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Doc_Cat_SName + "</td><td>" + Orders[$i].Doc_Cat_Name + "</td><td>" + Orders[$i].Cat_Count + "</td>");
                        $("#OrderList TBODY").append(tr);
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                loadPgNos();
            }



            function loadData() {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    //   async: false,
                    url: "CategoryCreationList.aspx/FillDocSpeS",
                    dataType: "json",
                    success: function (data) {
                        AllOrders = JSON.parse(data.d) || [];
                        Orders = AllOrders;
                        ReloadTable();
                    },
                    error: function (result) {
                        //alert(JSON.stringify(result));
                    }
                });
            }



            function ReloadTableForExcel() {
                $("#OrderList TBODY").html("");
                for ($i = 0; $i < Orders.length; $i++) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Sale_Erp_Code + "</td><td>" + Orders[$i].Product_Detail_Name + "</td><td>" + parseFloat(Orders[$i].Target_Price).toFixed(2) + "</td><td>" + Orders[$i].product_unit + "/" + Orders[$i].Product_Sale_Unit + "</td><td>" + Orders[$i].HSN_Code + "</td><td>" + parseFloat(Orders[$i].Retailor_Price).toFixed(2) + "</td><td>" + "</td><td>" + "</td>");
                    $("#OrderList TBODY").append(tr);
                }
            }


            $('#btnExcel').on('click', function (e) {
                ReloadTableForExcel();
                var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#divtable').html());
                var a = document.createElement('a');
                a.href = data_type;

                a.download = 'ProductDetailList.xls';
                a.click();
                e.preventDefault();
                ReloadTable();

            });
        </script>
        <div>
              <h3>Customer / <span style="color: dodgerblue">Category</span></h3>

            <table align="center" style="width: 100%">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">

                            <div class="card">
                                <div class="card-body table-responsive">
                                    <div style="white-space: nowrap">
                                        <label style="float: left">
                                            Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="SearchOrd" style="width: 250px;" /></label>
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
                                    <div id="divtable">
                                        <table class="table table-hover" id="OrderList">
                                            <thead class="text-warning">
                                                <tr>
                                                    <th style="text-align: left">Sl.No</th>
                                                    <th id="Product_Detail_Code" style="text-align: left; cursor: pointer">Category Code</th>
                                                    <th id="Product_Detail_Name" style="text-align: left; cursor: pointer">Category Name</th>
                                                    <th id="Product_Brd_Name1" style="text-align: left; cursor: pointer">No Of Customers</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="row" style="padding: 5px 0px">
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
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</asp:Content>
