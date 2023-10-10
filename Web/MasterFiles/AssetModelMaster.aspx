<%@ Page Title="Asset Model" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AssetModelMaster.aspx.cs" Inherits="MasterFiles_AssetModelMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>
        #ddlcat_chzn {
    width: 300px !important;
    top: 10px;
}

th {
    white-space: nowrap;
    cursor: pointer;
}
    </style>
       <form runat="server">
        <div>
             <div class="col-lg-12 sub-header">Model Master
            <div class="row">
            <div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> 
                <%--<div style="float: right;padding-top: 3px;">
                    
            <a href="AssetCategory.aspx" class="btn btn-primary" id="bulkcat">Bulk Upload</a></div>--%>
                <div style="float: right;padding-top: 3px; width: 136px;">
                    <button class="btn btn-primary" id="newmodel" data-toggle="modal" data-target="#addmod">Add Model</button>
            </div>
        </div>
            </div>
        </div>
       <br />
        <br />
        <br />
           <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
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
                <table class="table table-hover" id="amtable" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">Model Name</th>
                            <th style="text-align: left">Category</th>
                            <th style="text-align: left">Model No</th>
                            <th style="text-align: left;">No.of Assets</th>
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
                            <ul class="pagination" style="float: right; margin: -11px 0px">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">First</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Last</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
               <div id="addmod" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Add Model</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hgrpcode" />
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <label>Model Name<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                           <div class="col-sm-8">
                                            <input type="text" id="txtmod" class="form-control" autocomplete="off" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)" />
                                        </div>
                                    </div>
                                     <div class="row" style="margin-top: 10px;">
                                         <div class="col-sm-3">
                                            <label>Category<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-4">
                                            <select id="ddlcat" class="form-control">
                                            </select> 
                                        </div>
                                    </div>
                                    <div class="row" style="margin-top: 10px;">
                                         <div class="col-sm-3">
                                            <label>Model No</label>
                                        </div>
                                           <div class="col-sm-8">
                                            <input type="text" id="txtmodno" class="form-control" autocomplete="off" />
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
           </div>
           </form>
    <script type="text/javascript" src="../js/plugins/timepicker/bootstrap-clockpicker.min.js"></script>
<script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
<script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
<script lang="javascript" type="text/javascript">
    var AllOrders = [], Orders = [];
    var filtrkey = '0';
    var pgNo = 1; PgRecords = 10; TotalPg = 0, searchKeys = "Model_Name,Model_No,category_Id,";
    $(document).ready(function () {
        loadData();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetModelMaster.aspx/GetCatType",
            data: "{}",
            dataType: "json",
            success: function (data) {
                var cattype = JSON.parse(data.d) || [];
                var ctyp = $("#ddlcat");
                ctyp.empty().append('<option value="0">Select Category</option>');
                for (var i = 0; i < cattype.length; i++) {
                    ctyp.append($('<option value=' + cattype[i].cattype_Id + '>' + cattype[i].cattype_Name + '</option>'));
                }
            },
            error: function (result) {
            }
        });
    });
    $(".segment>li").on("click", function () {
        $(".segment>li").removeClass('active');
        $(this).addClass('active');
        filtrkey = $(this).attr('data-va');
        Orders = AllOrders; 
        $("#tSearchOrd").val('');
        ReloadTable();
    });
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
        TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); 
        selpg = 1;
        if (isNaN(prepg)) prepg = 0;
        if (isNaN(Nxtpg)) Nxtpg = 2;
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
    function ReloadTable() {

        var tr = '';
        $("#amtable TBODY").html("");
        if (filtrkey != "All") {
            Orders = Orders.filter(function (a) { 
                return a.Model_Active_Flag == filtrkey;
            })
        }
        st = PgRecords * (pgNo - 1);
        for ($i = st; $i < st + Number(PgRecords); $i++) {
            if ($i < Orders.length) {

                tr = $("<tr rname='" + Orders[$i].Model_Name + "'  rocode='" + Orders[$i].Model_Id + "'></tr>");
                $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].Model_Name + "</td><td>" + Orders[$i].category_Id + "</td><td>" + Orders[$i].Model_No + "</td><td  class='rocount'><a href='#'>" + Orders[$i].Assets + "</a></td><td id='" + Orders[$i].Model_Id + "' class='roedit'><a href='#'>Edit</a></td><td><ul class='nav' style='margin: 0px'><li class='dropdown'><a href='#' style='padding: 0px' class='dropdown - toggle' data-toggle='dropdown'>"
                    + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                    '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v="1">Deactivate</a></li>' : '<li><a href="#" v="0">Active</a></li>') + '</ul></li></ul></td>');

                $("#amtable TBODY").append(tr);
            }
        }
        $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
        loadPgNos();
    }
    $('#btnsave').on('click', function () {
        var modnam = $("#txtmod").val();
        var modcat = $("#ddlcat option:selected").val();
        var modno = $("#txtmodno").val(); 
        if (modnam == '') {
            alert("Please Enter Model Name.");
            $("#txtmod").focus();
            return false;
        }
        if (modcat == '') {
            alert("Please Select Category.");
            $("#ddlcat").focus();
            return false;
        }
        if (modno == '') {
            alert("Please Enter ModelNumber.");
            $("#txtmodno").focus();
            return false;
        }
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetModelMaster.aspx/Save_Model",
            data: "{'modname':'" + modnam + "','modno':'" + modno + "','modcat':'" + modcat + "'}",
            dataType: "json",
            success: function (data) {
                alert(data.d);
                $("#addmod").modal("hide");
                loadData();
            },
            error: function (result) {
            }
        });
    });
    $('#newmodel').on('click', function () {
        $("#txtmod").val('');
        $('#ddlcat').val('0');
        $("#txtmodno").val('');
    });
    $(document).on('click', '.roedit', function () {
        x = this.id;
        $("#addmod").modal("show");
        fillModel(x);
    });
    function fillModel(mcode) {
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetModelMaster.aspx/getAssetMod_Id",
            data: "{'mcode':'" + mcode + "'}",
            dataType: "json",
            success: function (data) {
                var astmod = JSON.parse(data.d) || [];
                $("#txtmod").val(astmod[0].Model_Name);
                $("#ddlcat").val(astmod[0].category_Id);
                $("#txtmodno").val(astmod[0].Model_No);
            },
            error: function (data) {
                //alert(JSON.stringify(data));
            }
        });
    }
    $(document).on('click', ".ddlStatus>li>a", function () {
        cStus = $(this).closest("td").find(".aState");
        let modid = $(this).closest("tr").find(".roedit").attr("id");
        stus = $(this).attr("v");
        $indx = Orders.findIndex(x => x.Model_Id == modid);
        cStusNm = $(this).text();
        if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
            id = Orders[$indx].Model_Id;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetModelMaster.aspx/SetNewStatus",
                data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                dataType: "json",
                success: function (data) {
                    Orders[$indx].Model_Active_Flag = stus;
                    Orders[$indx].Status = cStusNm;
                    $(cStus).html(cStusNm);
                    ReloadTable();
                    alert('Status Changed Successfully...');

                },
                error: function (result) {
                }
            });
        }
        loadPgNos();
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
            Orders = AllOrders;
        ReloadTable();
    });
    function loadData() {

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            url: "AssetModelMaster.aspx/GetDetails",
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
</script>
</asp:Content>

