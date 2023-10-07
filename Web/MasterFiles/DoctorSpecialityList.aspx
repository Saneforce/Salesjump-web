<%@ Page Title="Channel List" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="DoctorSpecialityList.aspx.cs" Inherits="MasterFiles_DoctorSpecialityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<title>Division</title>

        <link type="text/css" rel="stylesheet" href="../css/style.css" />
        <style type="text/css">
            .modal {
                position: fixed;
                top: 0;
                left: 0;
                background-color: gray;
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

            .auto-style1 {
                width: 83%;
            }

            .auto-style2 {
                width: 21px;
            }
        </style>
<body>
    <form id="form1" runat="server">
        <div class="row">
        <div class="col-lg-12 sub-header"><span style="float: right"><a href="DoctorSpeciality.aspx" class="btn btn-primary btn-update" id="newsf">Add New</a></span><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> </div>
    </div>
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
                        <table class="table table-hover" id="DocList" style="font-size: 12px">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">Sl.No</th>
                                    <th id="Outlet Type Code" style="text-align: left">Outlet Type Code</th>
                                    <th id="Outlet Type" style="text-align: left">Outlet Type</th>
                                    <th id="No of Customers" style="text-align: left">No of Customers</th>
                                    <th id="Edit" style="text-align: left">Edit</th>
                                    <th id="Deactivate" style="text-align: left">Status</th>
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
</body>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
            <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
            <script language="javascript" type="text/javascript">
                var AllOrders = [];
                var filtrkey = '0';
                var Orders = [], pgNo = 1, PgRecords = 10, TotalPg = 0, searchKeys = "Doc_Special_SName,Doc_Special_Name,";

                $(".data-table-basic_length").on("change",
                    function () {
                        pgNo = 1;
                        PgRecords = $(this).val();
                        ReloadTable();
                    }
                );
                function ReloadTable() {
                    var tr = '';
                    $("#DocList TBODY").html("");
                    if (filtrkey != "All") {
                        Orders = Orders.filter(function (a) {
                            return a.Doc_Special_Active_Flag == filtrkey;
                        });
                    }
                    st = PgRecords * (pgNo - 1); slno = 0;
                    for ($i = st; $i < st + PgRecords; $i++) {
                        if ($i < Orders.length) {
                            tr = $("<tr rname='" + Orders[$i].Doc_Special_SName + "'  rocode='" + Orders[$i].Doc_Special_Code + "'></tr>");
                            slno = $i + 1;
                            $(tr).html('<td>' + slno + "</td><td>" + Orders[$i].Doc_Special_SName + '</td><td>' + Orders[$i].Doc_Special_Name + 
                                 "</td><td class='scount'><input type='hidden' class='pcod_hidd' value='" + Orders[$i].Spec_Count + "'>" + Orders[$i].Spec_Count + "</td><td id=" + Orders[$i].Doc_Special_Code +
                                " class= 'roedit' > <a href='#'>Edit</a></td > <td class='rodeact'><a href='#' style=" + ((parseInt(Orders[$i].Doc_Special_Active_Flag) == 0) ? 'color:green;' : 'color:red;') + " >" + ((parseInt(Orders[$i].Doc_Special_Active_Flag) == 0) ? 'Active' : 'Deactive') + "</a></td>");
                            $("#DocList TBODY").append(tr);
                            hq = [];
                        }
                    }
                    $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");

                    loadPgNos();
                }
                function loadPgNos() {
                    prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
                    Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
                    $(".pagination").html("");
                    TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
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
                    });
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
                function loadData(divcode) {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "DoctorSpecialityList.aspx/GetDetails",
                        data: "{'divcode':'" + divcode + "'}",
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
                    divcode = Number(<%=Session["div_code"]%>);
                    loadData(divcode);
                    $(document).on('click', '.roedit', function () {
                        x = this.id;
                        window.location.href = "DoctorSpeciality.aspx?Doc_Special_Code=" + x;
                    });
                    $(document).on("click", ".rodeact", function () {
                        var row = $(this).closest('tr');
                        var route_C = $(this).closest('tr').attr('rocode');
                        let oindex = Orders.findIndex(x => x.Doc_Special_Code == route_C);
                        let disstat = (parseInt(Orders[oindex]["Doc_Special_Active_Flag"]) == 0) ? 'Deactive' : 'Active';
                        let flg = (parseInt(Orders[oindex]["Doc_Special_Active_Flag"]) == 0) ? 1 : 0;
                        let flag = 0;
                        if ((flg == 1) || (flg == 0)) {
                        if (disstat == "Deactive") {
                                var res = row.find('.pcod_hidd').val();
                                if (res > 0) {
                                    flag = flag + 1;
                                    alert('This Division mapped with Customers! Cannot deactivate!');
                                }
                            }
                            if (flag == 0) {
                            if (confirm("Do you want " + disstat + " the Outlet ?")) {

                                     $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        data: "{'Doc_Special_Code':'" + route_C + "','stat':'" + flg + "'}",
                                        url: "DoctorSpecialityList.aspx/deactivate",
                                        
                                        dataType: "json",
                                        success: function (data) {
                                            if (data.d == 'Success') {
                                                Orders[oindex]["Doc_Special_Active_Flag"] = flg;
                                                ReloadTable();
                                                alert('Status Changed Successfully...');
                                            }
                                            else {
                                                alert("Status Can't be Changed");
                                            }
                                        },
                                        error: function (result) {
                                        }
                                    });
                            }
                            }
                        }
                        else {
                            alert("Status Can't be Changed");
                        }
                    });
                });
            </script>

    </asp:Content>
