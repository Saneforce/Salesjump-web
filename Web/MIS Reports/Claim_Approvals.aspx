<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Claim_Approvals.aspx.cs" Inherits="MIS_Reports_Claim_Approvals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        [data-val='Rejected'] {
            color: #dc3545;
        }

        [data-val='Approved'] {
            color: #28a745;
        }

        [data-val='Reject'] {
            color: #dc3545;
        }

        [data-val='Approve'] {
            color: #28a745;
        }

        [data-val='Pending'] {
            color: #ffc107;
        }
    </style>
    <form runat="server">
        <asp:hiddenfield id="hffilter" runat="server" />
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 55px;" OnClick="ExportToExcel" />
        </div>

        <div class="row">
            <div class="col-lg-12 sub-header">
                Claim Status
                    
            </div>
        </div>
        <div class="row">
            <div class="card col-sm-8" style="margin: 5px 0px 0px 0px; width: 450px;">
                <div class="card-body table-responsive">

                    <table>
                        <tr style="background: #4697cf; border-top-left-radius: 5px; border-top-right-radius: 5px; color: #fff; font-size: 14px; font-weight: bold;">
                            <%-- <td style="background: #4697cf;border-top-left-radius: 5px;border-top-right-radius: 5px;color: #fff;font-size: 14px;font-weight: bold;">Summary</td>--%>
                        </tr>
                        <tr>
                            <td><span style="font-weight: bold;">Claim Receieved :</span> </td>
                            <td id="totalsum"></td>
                            <span class="glyphicon glyphicon-eye-open qqq" style="float: right;"></span>
                            <td><span style="font-weight: bold;">Approved : </span></td>
                            <td id="totalapp"></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td><span style="font-weight: bold;">Rejected :</span> </td>
                            <td id="totalRej"></td>
                            <td><span style="font-weight: bold;">Pending : </span></td>
                            <td id="totalPend"></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>

            </div>
            <div style="float: right; padding-top: 4px;">
                <ul class="segment">
                    <li data-va='All' class="active">ALL</li>
                    <li data-va='Pending'>Pending</li>
                    <li data-va="Approved">Approved</li>
                    <li data-va="Rejected">Rejected</li>
                </ul>
            </div>
        </div>
        <div class="card" style="overflow: auto;">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    <input type="text" id="tSearchOrd" style="width: 250px;" placeholder="Search" />
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
                <table class="table table-hover" id="OrderList" style="font-size: 12px;">
                    <thead class="text-warning">
                        <tr style="white-space: nowrap;">
                            <th style="text-align: left">SlNo</th>
                            <th style="text-align: left">
                                <input type="checkbox" id="sall" onclick="Achkall()" />
                                Select</th>
                            <th style="text-align: left">
                                <ul class="nav" style="margin: 0px">
                                    <li class="dropdown"><a href="#" style="padding: 0px" class="dropdown-toggle" data-toggle="dropdown"><span><span class="aState" data-val="isApproved">isApproved</span><i class="caret" style="float: right; margin-top: 8px; margin-right: 0px"></i></span></a><ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus1" style="left: 50px; right: auto;">
                                        <li><a href="#" v="2">Approve</a><a href="#" v="-1">Reject</a></li>
                                    </ul>
                                    </li>
                                </ul>
                            </th>
                            <th>Claim ID</th>
                            <th>Remarks</th>
                            <th>Pending Status</th>
                            <th>Scheme Period</th>
                            <th>Raised Date</th>
                            <th>Responsible Person</th>
                            <th>Customer ID</th>
                            <th>Customer</th>
                            <th>Geography</th>
                            <th>Supplier ID</th>
                            <th>Supplier</th>
                            <th>Order Amt</th>
                            <th>InvoiceNo</th>
                            <th>InvoiceAmt</th>
                            <th>InvoiceDate</th>
                            <th>Gift</th>
                            <th>Gift Description</th>
                            <th>Gift Products</th>
                            <th>Quantity</th>
                            <th>Gift Type</th>
                            <th>Slab Name</th>
                            <th>Picture</th>
                        </tr>
                    </thead>
                    <tbody id="OrderListtb" style="font-size: 7pt;">
                    </tbody>
                </table>
                <div class="row">
                    <div class="col-sm-5">
                        <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                    </div>
                    <div class="col-sm-7">
                        <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                            <ul class="pagination">
                                <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                                <li class="paginate_button active"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">1</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="2" tabindex="0">2</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="3" tabindex="0">3</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="4" tabindex="0">4</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="5" tabindex="0">5</a></li>
                                <li class="paginate_button "><a href="#" aria-controls="example2" data-dt-idx="6" tabindex="0">6</a></li>
                                <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <center>
            <button type="button" class="btn btn-primary" style="background-color: rgb(26 115 232 / 1);" onclick="svClaim()()">Save</button>
            <a href="rpt_claim_staus_main.aspx" class="btn btn-danger">Close</a>
        </center>
    </form>
    <script type="text/javascript" src="../../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../../js/plugins/datatables/dataTables.bootstrap.js"></script>
    <script language="javascript" type="text/javascript">
        var AllOrders = [], AllTP = [], CTP = [], CusOrders = [];
        var sf = '', sfty = '';
        var claimID = '';
        var filtrkey = 'All';
        var mdsf = '';
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth() + 2;
        var trsfcl = '';
        optStatus = "<li><a href='#' v='2'>Approve</a><a href='#' v='-1'>Reject</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Amount,ClaimID,Customer,Geography,Gift,GiftDescription,GiftType,Invoice_date,Invoice_number,Pending_Status,Products,Quantity,RaisedDate,Rejectreason,ResponsiblePerson,SchemePeriod,SlabName,Supplier,isApproved,";
        $(".data-table-basic_length").on("change", function () {
            pgNo = 1;
            PgRecords = $(this).val();
            ReloadTable();
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
        function Achkall() {
            if ($('#sall').is(":checked")) {
                Orders = Orders.map(function (o) {
                    if (sfty == '3' && o.Aproval_Flag == 3) {
                        o.isApproved = "Approve";
                    }
                    return o;
                });
            }
            else {
                Orders = Orders.map(function (o) {
                    if (sfty == '3' && o.Aproval_Flag == 3) {
                        o.isApproved = "Pending";
                    }
                    return o;
                });
            }
            ReloadTable();
        }
        function Atdclick($Ack) {
            Chkclick(($($Ack).find('.Approve')));
        }
        function Chkclick($Chk) {
            var trsf = parseInt($($Chk).closest("tr").find('td').eq(0).text()) - 1;
            if (!$($Chk).is(":checked")) {
                $($Chk).prop('checked', true);
                $($Chk).closest('tr').find('.aState').text('Approve');
                $($Chk).closest('tr').find('.aState').attr('data-val', 'Approve');
                if (sfty == '3' && Orders[trsf].Aproval_Flag == 3) {
                    Orders[trsf].isApproved = "Approve";
                }
            }
            else {
                $($Chk).prop('checked', false);
                $($Chk).closest('tr').find('.aState').text('Pending');
                $($Chk).closest('tr').find('.aState').attr('data-val', 'Pending');
                if (sfty == '3' && Orders[trsf].Aproval_Flag == 3) {
                    Orders[trsf].isApproved = "Pending";
                }
            }
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return (claimID.indexOf(a.ClaimID) >= 0);
                });
            }
            var approved = [];
            approved = Orders.filter(function (a) {
                return a.isApproved == 'Approved';
            });
            var reject = [];
            reject = Orders.filter(function (a) {
                return a.isApproved == 'Rejected';
            });
            var pending = [];
            pending = Orders.filter(function (a) {
                return a.isApproved == 'Pending';
            });
            var totsumarr = AllOrders.filter(function (a) {
                return a.Aproval_Flag != null;
            })
            $('#totalsum').html(totsumarr.length);
            $('#totalapp').html(approved.length);
            $('#totalRej').html(reject.length);
            $('#totalPend').html(pending.length);
            st = PgRecords * (pgNo - 1); slno = 0;
            tr = '';
            for ($i = st; $i < st + PgRecords; $i++) {
                if ($i < Orders.length) {
                    imgurl = Orders[$i].imgurl;
                    slno = $i + 1;
                    tr = $("<tr trsl=\"" + slno + "\"></tr>"); $timg = '<table><tr>';
                    if (imgurl != "") {
                        imgurl = (imgurl).split(',');
                        for (var j = 0; j < imgurl.length; j++) {
                            $timg += ((imgurl[j] != "") ? '<td><img class="picc"  src=\"' + imgurl[j] + '\" style="width: 200px;height: 25px;" /></td>' : '<td></td>');
                        }
                    }
                    $timg += '</tr></table>';
                    var filtr = CusOrders.filter(function (a) {
                        return a.ListedDrCode == Orders[$i].Customer_Code
                    });
                    if (sfty == '3' && Orders[$i].Pending_Status == "Pending with Admin") {
                        $isapp = (Orders[$i].isApproved != "Pending") ? 'checked' : "";
                        tr.html('<td>' + slno + '</td><td class="Approvetd" onclick="Atdclick(this)" ><input type="checkbox" class="Approve" onclick="Chkclick(this)"  ' + $isapp + ' /></td><td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                            + '<span><span class="aState" data-val="' + Orders[$i].isApproved + '">' + Orders[$i].isApproved + '</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                            '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlStatus" style="right:0;left:auto;">' + optStatus + '</ul></li></ul></td><td>' + Orders[$i].ClaimID + '</td><td><input type="text" class="txtremark" onkeyup="txtremarkkeyup(this)"  value=\"' + Orders[$i].Rejectreason + '\"</td><td>' + Orders[$i].Pending_Status + '</td><td>' + Orders[$i].SchemePeriod + '</td><td>' + Orders[$i].RaisedDate + '</td><td>' + Orders[$i].ResponsiblePerson + '</td><td>' + Orders[$i].Customer_Code + '</td><td>' + Orders[$i].Customer + '</td><td>' + Orders[$i].Geography + '</td><td>' + Orders[$i].Supplier_Code + '</td><td>' + Orders[$i].Supplier + '</td><td>' + ((filtr.length > 0) ? filtr[0].value : 0) + '</td><td>' + Orders[$i].Invoice_number + '</td><td>' + Orders[$i].Amount + '</td><td>' + Orders[$i].Invoice_date + '</td><td>' + Orders[$i].Gift + '</td><td>' + Orders[$i].GiftDescription + '</td><td>' + Orders[$i].Products + '</td><td>' + Orders[$i].Quantity + '</td><td>' + Orders[$i].GiftType + '</td><td>' + Orders[$i].SlabName + '</td><td>' + ($timg) + '</td>');
                    }
                    else {
                        tr.html('<td>' + slno + '</td><td></td><td data-val="' + Orders[$i].isApproved + '">' + Orders[$i].isApproved + '</td><td>' + Orders[$i].ClaimID + '</td><td>' + Orders[$i].Rejectreason + '</td><td>' + Orders[$i].Pending_Status + '</td><td>' + Orders[$i].SchemePeriod + '</td><td>' + Orders[$i].RaisedDate + '</td><td>' + Orders[$i].ResponsiblePerson + '</td><td>' + Orders[$i].Customer_Code + '</td><td>' + Orders[$i].Customer + '</td><td>' + Orders[$i].Geography + '</td><td>' + Orders[$i].Supplier_Code + '</td><td>' + Orders[$i].Supplier + '</td><td>' + ((filtr.length > 0) ? filtr[0].value : 0) + '</td><td>' + Orders[$i].Invoice_number + '</td><td>' + Orders[$i].Amount + '</td><td>' + Orders[$i].Invoice_date + '</td><td>' + Orders[$i].Gift + '</td><td>' + Orders[$i].GiftDescription + '</td><td>' + Orders[$i].Products + '</td><td>' + Orders[$i].Quantity + '</td><td>' + Orders[$i].GiftType + '</td><td>' + Orders[$i].SlabName + '</td><td>' + ($timg) + '</td>');
                    }
                }
                $("#OrderListtb").append(tr);
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")

            $(".ddlStatus>li>a").on("click", function () {
                cStus = $(this).closest("td").find(".aState");
                stus = $(this).attr("v");
                $indx = parseInt($(this).closest("tr").find('td').eq(0).text()) - 1;
                if (!$(this).closest("tr").find('.Approve').is(':checked')) {
                    $(this).closest("tr").find('.Approve').prop('checked', true);
                }
                cStusNm = $(this).text();
                //Orders[$indx].Stat = parseInt(stus);
                if (sfty == '3' && Orders[$indx].Aproval_Flag == 3) {
                    Orders[$indx].isApproved = cStusNm;
                }
                $(cStus).html(cStusNm);
                ReloadTable();
            });
            loadPgNos();
        }
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
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            $("#<%=hffilter.ClientID%>").val(filtrkey);
            claimID = ''
            Orders = AllOrders.filter(filterByID);
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        function filterByID(item) {
            if (filtrkey == "Pending") {
                if (item.isApproved === "Pending") {
                    claimID += item.ClaimID + ',';
                    return true
                }
            }
            if (filtrkey == "Approved") {
                if (item.isApproved === "Approved") {
                    claimID += item.ClaimID + ',';
                    return true
                }
            }
            if (filtrkey == "Rejected") {
                if (item.isApproved === "Rejected") {
                    claimID += item.ClaimID + ',';
                    return true
                }
            }
            return false;
        }
        function loadOrders() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Claim_Approvals.aspx/GetOrders",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'','Mn':'','Yr':''}",
                dataType: "json",
                success: function (data) {
                    CusOrders = JSON.parse(data.d) || [];
                },
                error: function (result) {
                }
            });
        }
        function loadData() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Claim_Approvals.aspx/GetClaim",
                data: "{'divcode':'<%=Session["div_code"]%>','SF':'','Mn':'','Yr':''}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;loadOrders(); ReloadTable();
                },
                error: function (result) {
                }
            });
        }
        $(document).on('click', '.picc',function () {
            var photo = $(this).attr("src");
            $('#photo1').attr("src", $(this).attr("src"));
            $('#cphoto1').css("display", 'block');
        });
        $(document).ready(function () {
            sf = '<%=Session["sf_code"]%>';
            sfty = '<%=Session["sf_type"]%>';
            loadData();
            
            dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
            $(".ddlStatus1>li>a").on("click", function () {
                stus = $(this).text();
                if (!$(this).closest("tr").find('#sall').is(':checked')) {
                    $(this).closest("tr").find('#sall').prop('checked', true);
                }
                cStusNm = $(this).text();
                if ($('#sall').is(":checked")) {
                    Orders = Orders.map(function (o) {
                        if (sfty == '3' && o.Aproval_Flag == 3) {
                            o.isApproved = stus;
                        }
                        return o;
                    });
                }
                else {
                    Orders = Orders.map(function (o) {
                        if (sfty == '3' && o.Aproval_Flag == 3) {
                            o.isApproved = stus;
                        }
                        return o;
                    });
                }
                ReloadTable();
            });
        });
        function closew() {
            $('#cphoto1').css("display", 'none');
        } 
        function txtremarkkeyup($tr) {
            var $idx = $($tr).closest('tr').attr('trsl');
            Orders[($idx-1)]["Rejectreason"] = $($tr).val();
        } 
        function svClaim() {
            var filttp = [];
            var sXMl = '';
            if (sf == 'admin' && sfty == '3') {
                filttp = Orders.filter(function (a) {
                    return a.Pending_Status == 'Pending with Admin';
                });
            }
            for (var i = 0; i < filttp.length; i++) {
                sXMl += "<Claim sfc=\"" + filttp[i].ClaimID + "\" ApStatus=\"" + filttp[i].isApproved + "\"  ClaimRemark=\"" + filttp[i].Rejectreason + "\"  />"
            }
            if (filttp.length > 0) {
                console.log(sXMl);
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Claim_Approvals.aspx/svTpApprove",
                    data: "{'sxml':'" + sXMl + "'}",
                    dataType: "json",
                    success: function (data) {
                        alert(data.d);
                        loadData();
                    },
                    error: function (result) {
                        alert(result.responseText);
                    }
                });
            }
        }
    </script>
</asp:Content>

