<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Mas_Asset_Request.aspx.cs" Inherits="MasterFiles_Mas_Asset_Request" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link href="../css/jquery.multiselect.css" rel="stylesheet" />
<style type="text/css">
    #ddlsf_chzn     {
        width: 300px !important;
        font-weight: 500;
    }

    #txtfilter_chzn {
        width: 300px !important;
        top: 10px;
    }

    th {
        white-space: nowrap;
        cursor: pointer;
    }
</style>
    <form runat="server">
        <div class="row">
    <div class="col-lg-12 sub-header">
        Asset Master<span style="float: right"><a href="ProductDetail.aspx" class="btn btn-primary btn-update" id="newsf">Add Asset</a></span>
    </div>
</div>
            <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
               <%-- <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;            
                <div class="export btn-group">
                    <button class="btn btn-default dropdown-toggle" aria-label="Export" data-toggle="dropdown" type="button" title="Export data">
                        <i class="fa fa-th-list"></i>
                        <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu" role="menu">
                        <li role="menuitem" class="" onclick="radiochange(this)" value="1">
                            <a href="#">Product Category</a>
                        </li>
                        <li role="menuitem" class="" onclick="radiochange(this)" value="2">
                            <a href="#">Product Brand</a>
                        </li>
                        <li role="menuitem" class="" onclick="radiochange(this)" value="3">
                            <a href="#">Sub Division</a>
                        </li>
                        <li role="menuitem" class="" onclick="radiochange(this)" value="4">
                            <a href="#">State</a>
                        </li>
                    </ul>
                </div>
                    <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline"></select></span>--%>
                <label style="float: right">
                    Show
                <select class="data-table-basic_length" aria-controls="data-table-basic">
                    <option value="10">10</option>
                    <option value="25">25</option>
                    <option value="50">50</option>
                    <option value="100">100</option>
                </select>
                    entries</label><div style="float: right; padding-top: 3px;">
                        <ul class="segment">
                            <li data-va='All'>ALL</li>
                            <li data-va='0' class="active">Active</li>
                        </ul>
                    </div>
            </div>
            <table class="table table-hover" id="OrderList" style="font-size: 12px">
                <thead class="text-warning">
                    <tr>
                        <th style="text-align: left">Sl.No</th>
                        <th style="text-align: left">Asset ID</th>
                        <th style="text-align: left">Asset Code</th>
                        <th style="text-align: left">Asset Name</th>
                        <th style="text-align: left">Category</th>
                        <th style="text-align: left">Location</th>
                        <th style="text-align: left">Brand</th>
                        <th style="text-align: left">Model</th>
                        <th style="text-align: left">SerialNumber</th>
                        <th style="text-align: left">Asset Status</th>
                        <th style="text-align: left">Edit</th>
                        <th style="text-align: left">ChangeStatus</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <div class="row" style="padding: 5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination" style="float: right; margin: -11px 0px">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</form>
     <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
 <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [], sf, filtrkey = '0', Orders = [], pgNo = 1, PgRecords = 10, TotalPg = 0,
        searchKeys = "";
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
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt($(this).attr("data-dt-idx"));
                ReloadTable();
            }
            );
        }
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
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
        });
        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Mas_Asset_Request.aspx/GetDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                },
                error: function (result) {
                }
            });
        }
        $(document).ready(function () {
            
        });
    </script>
</asp:Content>

