<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Product_Group_Master.aspx.cs" Inherits="MasterFiles_Product_Group_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
        <head>
            <title></title>
        </head>
        <body>
            <form id="pgmform1" runat="server">
                <div class="card">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-lg-12 sub-header">Product Group List<span style="float:right;"><a href="#" class="btn btn-primary btn-update" id="nwgrp">New Group</a></span></div>
                        </div>
                    </div>
                    
                    <div class="card-body table-responsive">
                        <div style="white-space:nowrap">Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width:250px;" />
                            <label style="white-space:nowrap;margin-left: 57px;display:none;">Filter By&nbsp;&nbsp;<select id="txtfilter" name="ddfilter" style="width:250px;display:none;"></select></label>
                            <label style="float:right">Shows <select class="data-table-basic_length" aria-controls="data-table-basic"><option value="10">10</option><option value="25">25</option><option value="50">50</option><option value="100">100</option></select> entries</label>
                            <div style="float:right"><ul class="segment"><li data-va='All'>ALL</li><li data-va='0' class="active">Active</li></ul></div>
                        </div>
                        <table class="table table-hover" id="OrderList">
                            <thead class="text-warning">
                                <tr>                           
                                    <th style="text-align:left">Sl.No</th>
                                    <th  style='display:none;text-align:left'>Code</th>
                                    <th style="text-align:left">Name</th>
                                    <th style="text-align:left">No.of.Products</th>
                                    <th style="text-align:left">Edit</th>
                                    <th style="text-align:left">Status</th>
                                </tr>
                            </thead>
                            <tbody>

                            </tbody>
                        </table>
                        <div class="row" style="padding:5px 0px">
                            <div class="col-sm-5">
                                <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                            </div>
                            <div class="col-sm-7">
                                <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                                    <ul class="pagination" style="float:right;margin:-11px 0px">
                                        <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                        <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                        <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>            
                    </div>

                    <div class="modal fade" id="exampleModal" style="z-index: 10000000;overflow-y:auto;background-color: #0000000f;" tabindex="0" aria-hidden="true">
                        <div class="modal-dialog" role="document" style="width:623px !important">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Add New Group</h5><button type="button" class="close" style="margin-top:-20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="modal-body">
                                    <div class="container">
                                        <input type="hidden" id="hscode" />
                                        <div class="row">
                                            <div class="col-sm-2">
                                                <label style="padding-top: 4px;">Group Name</label>
                                            </div>
                                            <div class="col-sm-4" style="margin-left: -61px;">
                                                <input type="text" id="txtgrname" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="row" style="margin-top: 10px;">
                                            <div class="col-sm-2">
                                                <label style="white-space:nowrap;padding-top: 4px;">Group Short Name</label>
                                            </div>
                                            <div class="col-sm-4" style="margin-left: -61px;">
                                                <input type="text" id="txtsname" class="form-control"  />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                    <button type="button" class="btn btn-primary" id="sgrp">Save</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal fade" id="leaveModal" style="z-index: 10000000; overflow-y: scroll;" tabindex="0" aria-hidden="true">
                        <div class="modal-dialog" role="document" style="width: 98% !important">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="leaveModalLabel"></h5>
                                    <button type="button" class="close" style="margin-top: -20px;" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                </div>
                                <div class="card">
                                    <table id="leavedets" style="width: 100%; font-size: 12px;">
                                        <thead class="text-warning">
                                            <tr>
                                                <th style="text-align: left">SlNO</th>
                                                <th style="text-align: left">Product Code</th>
                                                <th style="text-align: left">Product Name</th>
                                                <th style="text-align: left">Product Description</th>
                                                <th style="text-align: left">UOM</th>
                                                <th style="text-align: left">Product Category</th>
                                                <th style="text-align: left">Product Brand</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                        </tbody>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal" data-toggle="modal" data-target="#summaryModal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>

            <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
            <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
            <script language="javascript" type="text/javascript">
                var AllOrders = [];
                var filtrkey = '0';
                optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Deactivate</a></li>"
                var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Product_Grp_Name,Product_Grp_SName,grp_count,";
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
                        /* $(".paginate_button").removeClass("active");
                         $(this).closest(".paginate_button").addClass("active");*/
                    }
                    );
                }

                function ReloadTable() {
                    $("#OrderList TBODY").html("");
                    if (filtrkey != "All") {
                        Orders = Orders.filter(function (a) {
                            return a.Product_Grp_Active_Flag == filtrkey;
                        })
                    }

                    st = PgRecords * (pgNo - 1); slno = 0;
                    for ($i = st; $i < st + PgRecords; $i++) {
                        //var filtered = Orders.filter(function (x) {
                        //    return x.Sf_Code != 'admin';
                        //})
                        if ($i < Orders.length) {
                            tr = $("<tr></tr>");
                            //var hq = filtered[$i].Sf_Name.split('-');
                            slno = $i + 1;
                            $(tr).html('<td>' + slno + '</td><td style="display:none">' + Orders[$i].Product_Grp_SName + '</td><td>' + Orders[$i].Product_Grp_Name + '</td><td cd=' + Orders[$i].Product_Grp_Code + ' nm=' + Orders[$i].Product_Grp_Name + ' class="deptsurs">' + Orders[$i].grp_count +
                                '</td><td id=' + Orders[$i].Product_Grp_Code +
                                ' class="sfedit"><a href="#">Edit</a></td>' +
                                '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                                + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                                '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td>');

                            $("#OrderList TBODY").append(tr);
                        }
                    }
                    $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
                    loadPgNos();
                }

                $(".ddlStatus>li>a").on("click", function () {
                    cStus = $(this).closest("td").find(".aState");
                    stus = $(this).attr("v");
                    $indx = $(this).closest("tr").index();
                    cStusNm = $(this).text();
                    if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                        sf = Orders[$indx].DSM_code;
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            async: false,
                            url: "Product_Group_Master.aspx/SetNewStatus",
                            data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                            dataType: "json",
                            success: function (data) {
                                Orders[$indx].Product_Grp_Active_Flag = stus;
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
                        shText = $(this).val();
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
                        url: "Product_Group_Master.aspx/GetProductCate",
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

                function clear() {
                    $('#txtgrname').val('');
                    $('#txtsname').val('');
                    hscode.value = '';
                }

                function savegrp() {
                    var grname = $('#txtgrname').val();
                    var sgrname = $('#txtsname').val();
                    var grcode = hscode.value;
                    if (grname == '' || grname == null) {
                        alert('Enter a Group Name');
                        return false;
                    }
                    if (sgrname == '' || sgrname == null) {
                        alert('Enter  Group Short Name');
                        return false;
                    }
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Product_Group_Master.aspx/saveprogrp",
                        data: "{'div':'<%=Session["div_code"]%>','grpname':'" + grname + "','grpsname':'" + sgrname + "','grpcode':'" + grcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            alert(data.d);
                            clear(); loadData();
                            $('#exampleModal').modal('hide');
                        },
                        error: function (result) {
                            //alert(JSON.stringify(result));
                        }
                    });
                }

                function editgrp(x) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Product_Group_Master.aspx/editprogrp",
                        data: "{'div':'<%=Session["div_code"]%>','grpcode':'" + x + "'}",
                        dataType: "json",
                        success: function (data) {
                            var getegr = JSON.parse(data.d) || [];
                            $('#txtgrname').val(getegr[0].Product_Grp_Name);
                            $('#txtsname').val(getegr[0].Product_Grp_SName);
                        },
                        error: function (result) {
                            //alert(JSON.stringify(result));
                        }
                    });
                }

                $(document).ready(function () {
                    loadData();
                    $(document).on('click', '.sfedit', function () {
                        $('#exampleModal').modal('toggle');
                        var x = this.id;
                        hscode.value = this.id;
                        editgrp(x);
                    });
                    $(document).on('click', '#sgrp', function () {
                        savegrp();
                    });
                    $(document).on('click', '#nwgrp', function () {
                        clear();
                        $('#exampleModal').modal('toggle');
                    });
                    $(document).on("click", ".deptsurs", function () {
                        let prodc = $(this).attr("cd");
                        let prodn = $(this).attr("nm");

                        $('#leaveModal').modal('toggle');
                        $('#leavedets TBODY').html("<tr><td colspan='4'>Loading please wait...</td></tr>");
                        $("#leaveModalLabel").html(`${prodn} Details`)

                        setTimeout(function () {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: false,
                                url: "Product_Group_Master.aspx/getdataliprod",
                                data: "{'div':'<%=Session["div_code"]%>','prodcode':'" + prodc + "'}",
                                dataType: "json",
                                success: function (data) {
                                    let AllOrders = JSON.parse(data.d) || [];
                                    $('#leavedets TBODY').html("");
                                    var slno = 0;
                                    for ($i = 0; $i < AllOrders.length; $i++) {
                                        if (AllOrders.length > 0) {
                                            slno += 1;
                                            tr = $("<tr></tr>");
                                            $(tr).html("<td>" + slno + "</td><td>" + AllOrders[$i].Product_Detail_Code + "</td><td>" + AllOrders[$i].Product_Detail_Name + "</td><td>" + AllOrders[$i].Product_Description + "</td><td>" + AllOrders[$i].Product_Sale_Unit + "</td><td>" + AllOrders[$i].Product_Cat_Name + "</td><td>" + AllOrders[$i].Product_Brd_Name + "</td>");
                                            $("#leavedets TBODY").append(tr);
                                        }
                                    }
                                },
                                error: function (resp) {
                                    $('#leavedets TBODY').html("<tr><td colspan='4'>Something went wrong. Try again.</td></tr>");
                                }
                            });
                        }, 500);

                    });
                });

                $("input[type='text']").on("keypress", function (e) {
                    var val = $(this).val();
                    var regex = /(<([^>]+)>)/ig;
                    var result = val.replace(regex, "");
                    $(this).val(result);
                });
            </script>
        </body>
    </html>
</asp:Content>