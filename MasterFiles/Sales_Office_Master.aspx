<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Sales_Office_Master.aspx.cs" Inherits="MasterFiles_Sales_Office_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
        .popover {
            z-index: 10000000;
        }
    </style>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Sales Office Master<span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a></span>
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
                            <label>SalesOffice Name</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtSOffname" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label>SalesOffice Code</label>
                        </div>
                        <div class="col-sm-10">
                            <input type="text" id="txtSOffcode" class="form-control" autocomplete="off" />
                        </div>
                    </div>
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label style="white-space:nowrap;">Region</label>
                        </div>
                        <div class="col-sm-10" style="display: flex;">
                            <div class="col-xs-6" style="margin-left: -15px;">
                                <select class="reg form-control" id="Region">
                                </select>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row" style="margin-top: 10px;">
                        <div class="col-sm-2">
                            <label style="white-space:nowrap;">Plant</label>
                        </div>
                        <div class="col-sm-10" style="display: flex;">
                            <div class="col-xs-6" style="margin-left: -15px;">
                                <select class="pnt form-control" id="Plant">
                                </select>
                            </div>
                        </div>
                    </div>--%>
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
                        <th style="text-align: left">SalesOffice Code</th>
                        <th style="text-align: left">SalesOffice Name</th>
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
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "sOffName,sOffCode,Status";

        function fillshift(scode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_Office_Master.aspx/getPreSOff",
                data: "{'divcode':" + divcode + ",'scode':" + scode + "}",
                dataType: "json",
                success: function (data) {
                    var dts = JSON.parse(data.d) || [];
                    $('#hscode').val((dts[0].sOffID).toFixed(0));
                    $('#txtSOffname').val(dts[0].sOffName);
                    $('#txtSOffcode').val(dts[0].sOffCode);
                    $('.reg').val(dts[0].RegionId);
                    //$('.pnt').val(dts[0].PlantId);
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
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
            );
        }

        function clear() {
            $('#txtSOffname').val('');
            $('#txtSOffcode').val('');
            $('.reg').val(2);
            $('.pnt').val(2);
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
                    return a.Act_Flg == filtrkey;
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
                    $(tr).html('<td>' + slno + '</td><td>' + Orders[$i].sOffCode + '</td><td>' + Orders[$i].sOffName +
                        '</td><td id=' + Orders[$i].sOffID +
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
                    sf = Orders[$indx].sOffID;
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Sales_Office_Master.aspx/SetNewStatus",
                        data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                        dataType: "json",
                        success: function (data) {
                            Orders[$indx].Act_Flg = stus;
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
        function loadData(divcode, sf) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_Office_Master.aspx/getSalesOffice",
                data: "{'divcode':'" + divcode + "'}",
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
        function fillRegion() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Sales_Office_Master.aspx/GetRegionDetails",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var reg = JSON.parse(data.d) || [];
                    var regid = $("[id*=Region]");
                    regid.empty().append($('<option value="0">---Select---</option>'));
                    for (var i = 0; i < reg.length; i++) {
                        regid.append($('<option value="' + reg[i].Area_code + '">' + reg[i].Area_name + '</option>'));
                    }
                }
            });
        }
        $(document).ready(function () {
            var sf;
            sf = 'admin';
            divcode = Number(<%=Session["div_code"]%>);
            loadData(divcode, sf);
            $(document).on('click', '#newsf', function () {
                var title = 'Sales Office Details';
                var scode1 = '';
                ShowPopup(title);
                fillRegion();
                
            });
            $('#btnsave').on('click', function () {
                var scode = $('#hscode').val();
                var sname = $('#txtSOffname').val();
                if (sname == '' || sname == null) {
                    alert('Enter the Sales Office name');
                    return false;
                }
                var socode = $('#txtSOffcode').val();
                if (socode == '' || socode == null) {
                    alert('Enter the Sales Office Code');
                    return false;
                }
                var sreg = $('#Region :selected').val();
                if (sreg == '' || sreg == '0') {
                    alert('Select the Region ');
                    return false;
                }
                var splant = $('#txtSOffcode').val();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    data: "{'divcode':'<%=Session["div_code"]%>','id':'" + scode + "','sname':'" + sname + "','socode':'" + socode + "','reg':'" + sreg + "','splant':'" + splant + "'}",
                    url: "Sales_Office_Master.aspx/insertdata",
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
                $('#hscode').val('') ;
                
            });
            $(document).on('click', '.sfedit', function () {
                var title = 'Sales Office Details';
                x = this.id;
                ShowPopup(title);
                fillRegion();
                fillshift(x);
            });
        });
        
    </script>
</asp:Content>

