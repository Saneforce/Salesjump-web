<%@ Page Title="AssetCategory" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="AssetCategoryMaster.aspx.cs" Inherits="MasterFiles_AssetCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link href="../css/jquery.multiselect.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <style>

        #txtfilter_chzn {
            width: 300px !important;
            top: 10px;
        }
        #ddlcattype_chzn{
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
             <div class="col-lg-12 sub-header">Category Master 
            <div class="row">
            <div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> 
               <%-- <div style="float: right;padding-top: 3px;">
                    
            <a href="AssetCategory.aspx" class="btn btn-primary" id="bulkcat">Bulk Upload</a></div>--%>
                <div style="float: right;padding-top: 3px; width: 136px;">
                    <button class="btn btn-primary" id="newcat" data-toggle="modal" data-target="#addcat">Add Category</button>
            
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
                    <span id="filspan" style="margin-left: 10px;">Filter By&nbsp;&nbsp;
                        <select class="form-control" id="txtfilter" name="ddfilter" style="min-width: 315px !important; max-width: 318px !important; display: inline">
                            <option>Select</option>
                        </select></span>&nbsp;&nbsp;&nbsp;&nbsp;
                     <label style="float: right">
                        Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                        entries</label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
    <div class="col-lg-0 dropdown" style="float: right">
        <button type="button" class="btn btn-default btn-sm dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-th-list"></span> 
            <span class="caret"></span></button>
        <ul class="dropdown-menu" id="dmckhbx">
          <li><a href="#" class="small" data-value="option1" tabIndex="-1"><input type="checkbox" value="5"/>&nbsp;Created By</a></li>
          <li><a href="#" class="small" data-value="option2" tabIndex="-1"><input type="checkbox" value="6"/>&nbsp;Created On</a></li>
          <li><a href="#" class="small" data-value="option3" tabIndex="-1"><input type="checkbox" value="7"/>&nbsp;Modified By</a></li>
          <li><a href="#" class="small" data-value="option4" tabIndex="-1"><input type="checkbox" value="8"/>&nbsp;Modified On</a></li>
        </ul>
    </div>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                   
                </div>
                <table class="table table-hover" id="atable" style="font-size: 12px">
                    <thead class="text-warning">
                        <tr>
                            <th style="text-align: left">Sl.No</th>
                            <th style="text-align: left">Category Name</th>
                            <th style="text-align: left">Category Type</th>
                            <th style="text-align: left">No of Assets</th>
                            <th style="text-align: left;" id="Created By">Created By</th>
                            <th style="text-align: left" id="Created On">Created On</th>
                            <th style="text-align: left" id="Modified By">Modified By</th>
                            <th style="text-align: left" id="Modifed On">Modifed On</th>
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
        <div id="addcat" class="modal fade" role="dialog" style="z-index: 10000000; background-color: #00000000">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">
                                        &times;</button>
                                    <h4 class="modal-title">Add Category</h4>
                                </div>
                                <div class="modal-body">
                                    <input type="hidden" id="hgrpcode" />
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <label>Category Name<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                           <div class="col-sm-8">
                                            <input type="text" id="txtcat" class="form-control" autocomplete="off" onkeypress="return (event.charCode > 64 && event.charCode < 91) || (event.charCode > 96 && event.charCode < 123)" />
                                        </div>
                                    </div>
                                     <div class="row" style="margin-top: 10px;">
                                         <div class="col-sm-3">
                                            <label>Category Type<span style="color:red;font:bold;">  *</span></label>
                                        </div>
                                        <div class="col-sm-4">
                                            <select id="ddlcattype" class="form-control">
                                            </select> 
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
        var pgNo = 1; PgRecords = 10; TotalPg = 0, searchKeys = "category_Name,category_type,";
        $(document).ready(function () {
            loadData();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetCategoryMaster.aspx/GetCatType",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    var cattype = JSON.parse(data.d) || [];
                    $('#txtfilter_chzn').remove();
                    $('#txtfilter').removeClass('chzn-done');
                    $('#ddlcattype_chzn').remove();
                    $('#ddlcattype').removeClass('chzn-done');
                    var sfmgs = $("#txtfilter");
                    var ctyp = $("#ddlcattype");
                    sfmgs.empty().append('<option value="0">Select CategoryType</option>');
                    ctyp.empty().append('<option value="0">Select CategoryType</option>');
                    for (var i = 0; i < cattype.length;i++) {
                        sfmgs.append($('<option value=' + cattype[i].cattype_Id + '>' + cattype[i].cattype_Name + '</option>')).trigger('chosen:updated');
                        ctyp.append($('<option value=' + cattype[i].cattype_Id + '>' + cattype[i].cattype_Name + '</option>')).trigger('chosen:updated');
                    }
                    $('#txtfilter').chosen();
                    $('#ddlcattype').chosen();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });

            $('#txtfilter').on('change', function () {
                var sfs = $(this).val();
                var hqn = $('#txtfilter_chosen a').text().trim();
                if (sfs != 0) {
                    Orders = AllOrders; 
                    Orders = Orders.filter(function (a) {  
                        return a.category_type == sfs;
                    });
                }
                else {
                    Orders = AllOrders; 
                }
                ReloadTable();
                showandhideclm();
            });
            $("#dmckhbx :checkbox").each(function () {
                $('#atable th:nth-child(' + $(this).val() + '),#atable td:nth-child(' + $(this).val() + ')').hide();
            });
            $('input[type=checkbox]').click(function () {
                showandhideclm();
            });
        });

        function showandhideclm() {
            //$('input[type=checkbox]').click(function () {
            //    var chkbxval = $(this).val();
            //    var value = '';
                $("#dmckhbx :checkbox").each(function () {
                    value = $(this).val();
                    if ($(this).prop('checked') == true) {
                        $('#atable th:nth-child(' + value + '),#atable td:nth-child(' + value + ')').show();
                    }
                    else {
                        $('#atable th:nth-child(' + value + '),#atable td:nth-child(' + value + ')').hide();
                    }

                });
            //});
        }
        $('#newcat').on('click', function () {
            $("#txtcat").val('');
            $('#ddlcattype').val('0');
        });

        $('#btnsave').on('click', function () {
            var catnm = $("#txtcat").val();
            var catyp = $("#ddlcattype option:selected").val();
            if (catnm == '') {
                alert("Please Enter Category Name.");
                $("#txtcat").focus();
                return false;
            }
            if (catyp == '0') {
                alert("Please select Category Type.");
                $("#ddlcattype").focus();
                return false;
            }
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetCategoryMaster.aspx/Save_category",
                data: "{'catname':'" + catnm + "','cattype':'" + catyp + "'}",
                dataType: "json",
                success: function (data) {
                    alert(data.d);
                    $("#addcat").modal("hide");
                    loadData();
                },
                error: function (result) {
                }
            });
        });
       
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders; //dataobj
            $("#tSearchOrd").val('');
            ReloadTable();
            showandhideclm();
        });
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
                showandhideclm();
            }
        );
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0));  //dataobj
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
                showandhideclm();
            }
            );
        }
        function ReloadTable() {

            var tr = '';
            $("#atable TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) { //dataobj
                    return a.active_flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1);
            for ($i = st; $i < st + Number(PgRecords); $i++) {
                if ($i < Orders.length) { //dataobj

                    tr = $("<tr rname='" + Orders[$i].category_Name + "'  rocode='" + Orders[$i].category_Id + "'></tr>");
                    $(tr).html("<td>" + ($i + 1) + "</td><td>" + Orders[$i].category_Name + "</td><td>" + Orders[$i].category_type + "</td><td  class='rocount'><a href='#'>" + Orders[$i].Assets + "</a></td><td>" + Orders[$i].craeted_by + "</td><td>" + Orders[$i].created_date + "</td><td>" + Orders[$i].Modified_by + "</td><td>" + ((Orders[$i].Lastupdt_date == null) ? '-' : Orders[$i].Lastupdt_date) + "</td><td id='" + Orders[$i].category_Id +"' class='roedit'><a href='#'>Edit</a></td><td><ul class='nav' style='margin: 0px'><li class='dropdown'><a href='#' style='padding: 0px' class='dropdown - toggle' data-toggle='dropdown'>"
                        + '<span><span class="aState" data-val="' + Orders[$i].Status + '">' + Orders[$i].Status + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + ((Orders[$i].Status == "Active") ? '<li><a href="#" v="1">Deactivate</a></li>' : '<li><a href="#" v="0">Active</a></li>') + '</ul></li></ul></td>');

                    $("#atable TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }
        $(document).on('click', '.roedit', function () {
            x = this.id;
            $("#addcat").modal("show");
            fillCategory(x);
        });
        function fillCategory(ccode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetCategoryMaster.aspx/getAssetCat_Id",
                data: "{'ccode':'" + ccode + "'}",
                dataType: "json",
                success: function (data) {
                    var astcat = JSON.parse(data.d) || [];
                    $("#txtcat").val(astcat[0].category_Name);
                    $("#ddlcattype").val(astcat[0].category_type);
                },
                error: function (data) {
                    //alert(JSON.stringify(data));
                }
            });
        }
        $(document).on('click', ".ddlStatus>li>a", function () {
            cStus = $(this).closest("td").find(".aState");
            let catid = $(this).closest("tr").find(".roedit").attr("id");
            stus = $(this).attr("v");
            $indx = Orders.findIndex(x => x.category_Id == catid);
            cStusNm = $(this).text();
            if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                id = Orders[$indx].category_Id;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "AssetCategoryMaster.aspx/SetNewStatus",
                    data: "{'ID':'" + id + "','stus':'" + stus + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders[$indx].active_flag = stus;
                        Orders[$indx].Status = cStusNm;
                        $(cStus).html(cStusNm);

                        ReloadTable();
                        alert('Status Changed Successfully...');
                        showandhideclm();

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
                Orders = AllOrders.filter(function (a) { //dataobj
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
                Orders = AllOrders //dataobj
            ReloadTable();
            showandhideclm();
        });
        function loadData() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "AssetCategoryMaster.aspx/GetDetails",
                data: "{}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || []; //dataobj
                    Orders = AllOrders; //dataobj
                    ReloadTable();
                    showandhideclm();
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
    </script>
</asp:Content>

