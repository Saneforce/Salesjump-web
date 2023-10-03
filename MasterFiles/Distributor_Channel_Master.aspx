<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Distributor_Channel_Master.aspx.cs" Inherits="MasterFiles_Distributor_Channel_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .popover {
            z-index: 10000000;
        }
    </style>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Distributor Channel Master<span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span>
            <div style="float: right">
                <ul class="segment">
                    <li data-va='All'>ALL</li>
                    <li data-va='0' class="active">Active</li>
                </ul>
            </div>
        </div>
    </div>
    <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #0000000f">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hscode" />
                    <div class="row">
                        <div class="col-sm-2">
                            <label>	Channel Name</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtChnlname" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label>Channel Code</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtChnlcode" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    
                    
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsave" class="btn btn-success">Save</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="card">
        <div class="card-body table-responsive">
            <div style="white-space: nowrap">
                Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />
                <label style="white-space: nowrap; margin-left: 57px; display: none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width: 250px; display: none;"></select></label>
                <label style="float: right">
                    Shows
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                    entries</label>
            </div>
            <table class="table table-hover" id="OrderList">
                <thead class="text-warning">
                    <tr>
                        <th style="text-align: left">SlNO</th>
                        <th style="text-align: left">Channel Code</th>
                        <th style="text-align: left">Channel Name</th>
                        <th style="text-align: left">Edit</th>
                        <th style="text-align: left">Status</th>
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
                        <ul class="pagination">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [];
        var divcode; filtrkey = '0';
        optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "CateNm,CateCode,Status";

        function fillshift(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Channel_Master.aspx/getPreDist",
                data: "{'scode':" + scode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val((dts[0].CateId).toFixed(0));
                    $('#txtChnlname').val(dts[0].CateNm);
                    $('#txtChnlcode').val(dts[0].CateCode);
                    
                },
                error: function (data) {
                    alert(JSON.stringify(data));
                }
            });
        }

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
                pgNo = parseInt($(this).attr("data-dt-idx")); ReloadTable();
                
            }
            );
        }

        function clear() {
            $('#txtChnlname').val('');
            $('#txtChnlcode').val('');
            
        }
        function ShowPopup(title) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup").modal("show");
            clear();
        }

        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.flg == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr></tr>");
                    slno = $i + 1;
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].CateCode + '</td><td>' + Orders[$i].CateNm +
                        '</td><td id=' + Orders[$i].CateId +
                        ' class="sfedit"><a href="#">Edit</a></td>' +
                        '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td>');

                    $("#OrderList TBODY").append(tr);
                    hq = [];
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                $indx = $(this).closest("tr").index();
                cStusNm = $(this).text();
                if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                    sf = Orders[$indx].CateId;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Distributor_Channel_Master.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {
                            Orders[$indx].flg = stus;
                            Orders[$indx].Status = cStusNm;
                            $(cStus).html(cStusNm);

                            ReloadTable();
                            alert('Status Changed Successfully...');

                        },
                        error: function (result) {
                        }
                    });
                }
            });
            loadPgNos();
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
        })

        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Distributor_Channel_Master.aspx/getAlldata",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                }
            });
        }

        $(document).ready(function () {
            var sf;
            sf = 'admin';
            loadData();
            $(document).on('click', '#newsf', function () {
                var title = 'Distributor Channel Details';
                var scode1 = '';
                ShowPopup(title);
              
            });
            $('#btnsave').on('click', function () {
                var scode = $('#hscode').val();
                var dname = $('#txtChnlname').val();
                if (dname == '' || dname == null) {
                    alert('Enter the Distributor Channel name');
                    return false;
                }
                var dcode = $('#txtChnlcode').val();
                if (dcode == '' || dcode == null) {
                    alert('Enter the Distributor Channel Code');
                    return false;
                }
                
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'id':'" + scode + "','dcname':'" + dname + "','dccode':'" + dcode + "'}",
                    url: "Distributor_Channel_Master.aspx/insertdata",
                    dataType: "json",
                    success: function (data) {
                        var obj = data.d;
                        if (obj == 'save') {
                            alert("Saved Successfully");
                            return false;
                        }
                        else {
                            alert("Failed");
                            return false;
                        }
                    },
                    error: function (result) {
                    }
                });
                $('#hscode').val('');

            });
            $(document).on('click', '.sfedit', function () {
                var title = 'Distributor Channel Details';
                x = this.id;
                ShowPopup(title);
                fillshift(x);
            });
        });
    </script>
</asp:Content>


 

