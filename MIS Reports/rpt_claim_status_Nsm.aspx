<%@ Page Title="" Language="C#" MasterPageFile="~/Master_MGR.master" AutoEventWireup="true" CodeFile="rpt_claim_status_Nsm.aspx.cs" Inherits="MIS_Reports_rpt_claim_status_Nsm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="frm1" runat="server">
        <div id="container">
            <div class="row">
                <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                    Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 55px;" OnClick="ExportToExcel" />
            </div>

            <div class="row">
                <div class="col-lg-12 sub-header">Claim Status Report <span style="float: right"><%--<a href="#" class="btn btn-primary btn-update" id="newsf">Add New</a>--%></span></div>
            </div>

            <div class="row">
                <div class="card col-sm-8" style="margin: 0px 0px 0px 0px; width: 450px;">
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
                        <asp:HiddenField ID="hdnfield" runat="server" />
                    </div>

                </div>
                <div>
                    <span style="float: right">

                        <ul class="segment">
                            <li data-va='All' class="active">ALL</li>
                            <li data-va='Pending'>Pending</li>
                            <li data-va='Approved'>Approved</li>
                            <li data-va='Rejected'>Rejected</li>
                        </ul>
                    </span>
                </div>
            </div>


            <div class="card">
                <div class="card-body table-responsive" style="overflow-y: scroll;">
                    <div style="white-space: nowrap">
                        Search&nbsp;&nbsp;<input type="text" autocomplete="off" id="tSearchOrd" style="width: 250px;" />


                    </div>
                    <table class="table table-hover" id="claimstatus">
                        <thead class="text-warning" style="white-space: nowrap;">
                            <tr style="font-size: 10px">
                                <th>Sl No</th>
                                <th>
                                    <input type="checkbox" id="chkParent" /></th>
                                <th>Claim ID</th>
                                <th>Approvel<select class="form-control" id="parentaction" disabled><option value="0">Select</option>
                                    <option value="1">Approved</option>
                                    <option value="2">Rejected</option>
                                </select></th>
                                <th>Reject Reason</th>
                                <th>Scheme Period</th>
                                <th>Responsible Person</th>
                                <th>Raised Date</th>
                                <th>Customer</th>
                                <th>Approved By</th>
                                <th>Geography</th>
                                <th>Supplier</th>
                                <th>Gift</th>
                                <th>Gift Description</th>
                                <th>Gift Product</th>
                                <th>Qty</th>
                                <th>Gift Type</th>
                                <th>Slab Name</th>
                                <th>InvoiceNo</th>
                                <th>InvoiceAmt</th>
                                <th>InvoiceDate</th>
                                <th>Picture</th>
                            </tr>
                        </thead>
                        <tbody style="white-space: nowrap; font-size: 8pt;">
                        </tbody>
                    </table>
                </div>
                <center>
            <button id="btnSave" type="button" class="btn btn-primary btn-md  save" style="height:30px;width:50px;">Save</button>
             <asp:Button ID="btnclose" runat="server" CssClass="btn btn-primary btn-md" PostBackUrl="rpt_Claim_main_sl.aspx" Text="close" style="height:30px;width:50px;" />                     
                  </center>
            </div>
            <div class="modal fade" id="exampleModal" style="z-index: 10000000; overflow-y: auto; background-color: rgba(0, 0, 0, 0.06);" tabindex="0" aria-hidden="true">
                <div class="modal-dialog" role="document" style="width: 618px !important">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">SR Claim Summary</h5>
                            <button type="button" class="close" onclick="clearOrder()" style="margin-top: -20px;" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-sm-12" style="padding: 15px">
                                    <table class="table table-hover orden" id="OrderEntry">
                                        <thead class="text-warning">
                                            <tr>
                                                <th style="text-align: left">Sl No</th>
                                                <th style="text-align: left">Field Force</th>
                                                <th style="text-align: left">Approved</th>
                                                <th style="text-align: left">Reject</th>
                                                <th style="text-align: left">Pending</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>
                                    </table>
                                </div>

                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" onclick="clearOrder()" data-dismiss="modal">Close</button>

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
        var Allorders1 = [];
        var AllOrders2 = [];
        var AllOrders3 = [];
        var giftInv = [], giftSlb = [], giftRt = [], giftImgs = [];
        var filtrkey = 'All';
        var Orders1 = []; var Orders2 = [];
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Status,SchemePeriod,ResponsiblePerson,Customer,Geography,GeoType,Supplier,";

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
        function getGiftData() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rpt_claim_status_Nsm.aspx/getGiftData",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders;
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function getGiftDataInv() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rpt_claim_status_Nsm.aspx/getGiftINV",
                dataType: "json",
                success: function (data) {
                    giftInv = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function getGiftDataRt() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rpt_claim_status_Nsm.aspx/getGiftDataRT",
                dataType: "json",
                success: function (data) {
                    giftRt = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function getGiftDataGf() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rpt_claim_status_Nsm.aspx/getGiftDataGF",
                dataType: "json",
                success: function (data) {
                    giftSlb = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function getGiftDataImg() {
            return $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "rpt_claim_status_Nsm.aspx/getGiftDataIMG",
                dataType: "json",
                success: function (data) {
                    giftImgs = JSON.parse(data.d) || [];
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $("#claimstatus TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Status == filtrkey;
                });
            }
            st = PgRecords * (pgNo - 1); slno = 0;
            for ($i = 0; $i < Orders.length; $i++) {
                if ($i < Orders.length) {
                    tr = $("<tr class='" + Orders[$i].sfc + "'></tr>");
                    slno = $i + 1;
                    var Imgarr = [];
                    var img1 = '';
                    Imgarr = giftImgs.filter(function (a) {
                        return a.sl_no == Orders[$i].ClaimId;
                    });
                    if (Imgarr.length > 0) {
                        for (var k = 0; k < Imgarr.length; k++) {
                            img1 += '<img class="picc"  src = "http://fmcg.sanfmcg.com/photos/' + Imgarr[k].imgurl.replace('/storage/emulated/0/Pictures/FMCG/Images/', '').replace('.jpg.jpg', '.jpg') + '" width="25" height="25"  />';
                        }
                    }
                    let GiftName = giftSlb.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.GiftName
                    }).join(',');
                    let GiftDesc = giftSlb.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.GiftDesc
                    }).join(',');
                    let SlabName = giftRt.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.SlabName
                    }).join(',');
                    let InvoiceNo = giftInv.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.Invoice_number
                    }).join(',');
                    let InvoiceDate = giftInv.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.Invoice_date
                    }).join(',');
                    let InvoiceAmt = giftInv.filter(function (a) {
                        return a.Sl_No == Orders[$i].ClaimId
                    }).map(function (el) {
                        return el.Amount
                    }).join(',');
                    if (Orders[$i].Status == 'Pending')
                        $(tr).html('<td>' + slno + '</td><td><input type="checkbox" class="chkchild" /></td><td class="claimid">' + Orders[$i].ClaimId + '</td><td><input type="hidden" value="' + Orders[$i].Status + '"  class="hdnaction" ><select class="ChildAction" onchange="childaction(this)" ><option value="0">Pending</option><option value="1">Approved</option><option value="2">Rejected</option></select></td><td><input type="text" reason="' + Orders[$i].RejectReason + '" class="adminreason" text="' + Orders[$i].RejectReason + '"   style="width: 73px;"  /></td><td>' + Orders[$i].SchemePeriod + '</td><td>' + Orders[$i].ResponsiblePerson + '</td><td>' + Orders[$i].RaisedDate + '</td><td>' + Orders[$i].Customer + '</td><td>' + Orders[$i].Approvedby + '</td><td>' + Orders[$i].Geography + '</td><td>' + Orders[$i].Supplier + '</td><td>' + GiftName + '</td><td>' + GiftDesc + '</td><td>' + Orders[$i].Products + '</td><td>' + Orders[$i].Qty + '</td><td>' + Orders[$i].GiftType + '</td><td>' + SlabName + '</td><td>' + InvoiceNo + '</td><td>' + InvoiceAmt + '</td><td>' + InvoiceDate + '</td><td>' + img1 + '</td>');
                    else
                        $(tr).html('<td>' + slno + '</td><td></td><td class="claimid">' + Orders[$i].ClaimId + '</td><td><input type="hidden" value="' + Orders[$i].Status + '"  class="hdnaction" >' + Orders[$i].Status + '</td><td>' + ((Orders[$i].Status == "Rejected") ? Orders[$i].RejectReason : "") + '</td><td>' + Orders[$i].SchemePeriod + '</td><td>' + Orders[$i].ResponsiblePerson + '</td><td>' + Orders[$i].RaisedDate + '</td><td>' + Orders[$i].Customer + '</td><td>' + Orders[$i].Approvedby + '</td><td>' + Orders[$i].Geography + '</td><td>' + Orders[$i].Supplier + '</td><td>' + GiftName + '</td><td>' + GiftDesc + '</td><td>' + Orders[$i].Products + '</td><td>' + Orders[$i].Qty + '</td><td>' + Orders[$i].GiftType + '</td><td>' + SlabName + '</td><td>' + InvoiceNo + '</td><td>' + InvoiceAmt + '</td><td>' + InvoiceDate + '</td><td>' + img1 + '</td>');
                    $("#claimstatus TBODY").append(tr);
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");
        }
        function loadData() {
            let str = "<tr><td colspan='10' style='font-size: 12pt;'>Loading...</td></tr>";
            $("#claimstatus TBODY").append(str);
            $.when(getGiftData(), getGiftDataGf(), getGiftDataImg(), getGiftDataInv(), getGiftDataRt()).then(function () {
                ReloadTable(); loadData2(); loadData4();
            });
        }
        function loadData1() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "rpt_claim_status_Nsm.aspx/getGiftEvent",
                dataType: "json",
                success: function (data) {
                    AllOrders1 = JSON.parse(data.d) || [];
                    Orders1 = AllOrders1;
                },
                error: function (result) {
                    // alert(JSON.stringify(result));
                }
            });
        }
        function loadData2() {
            if (Orders.length > 0) {
                var approved = [];
                approved = Orders.filter(function (a) {
                    return a.Status == 'Approved';
                });
                var reject = [];
                reject = Orders.filter(function (a) {
                    return a.Status == 'Rejected';
                });
                var pending = [];
                pending = Orders.filter(function (a) {
                    return a.Status == 'Pending';
                });
                $('#totalsum').html(Orders.length);
                $('#totalapp').html(approved.length);
                $('#totalRej').html(reject.length);
                $('#totalPend').html(pending.length);
            }
        }
        function loadData4() {
            $("#OrderEntry > TBODY").html('');
            if (Orders.length > 0) {
                var sfcode = [];
                var arrsfname = [];
                var sfname = '';
                sfcode = Orders.filter(function (a) {
                    if (("," + sfname + ",").indexOf("," + a.ResponsiblePerson + ",") < 0) {
                        sfname += (a.ResponsiblePerson + ',')
                        return true
                    }
                });
                arrsfname = sfname.split(',');
                for (var $k = 0; $k < arrsfname.length - 1; $k++) {
                    x = $k + 1;
                    var approvedsf = [];
                    approvedsf = Orders.filter(function (a) {
                        return ((a.Status == 'Approved') && (arrsfname[$k] == a.ResponsiblePerson));
                    });
                    var rejectsf = [];
                    rejectsf = Orders.filter(function (a) {
                        return ((a.Status == 'Rejected') && (arrsfname[$k] == a.ResponsiblePerson));
                    });
                    var pendingsf = [];
                    pendingsf = Orders.filter(function (a) {
                        return ((a.Status == 'Pending') && (arrsfname[$k] == a.ResponsiblePerson));
                    });
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + x + "</td><td>" + arrsfname[$k] + "</td><td>" + approvedsf.length + "</td><td>" + rejectsf.length + "</td><td>" + pendingsf.length + "</td>");
                    $("#OrderEntry > TBODY").append(tr);
                }
            }
        }
        function childaction(k) {
            var status = $(k).children("option:selected").text();
            if (status == "Rejected") {
                $(k).closest('tr').find('.adminreason').show();
            }
            else {
                $(k).closest('tr').find('.adminreason').hide();
            }
            if (status == "Approved") {
                $(k).closest('tr').find('.adminreason').hide();
            }
            if (status == "Pending") {
                $(k).closest('tr').find('.adminreason').hide();
            }
        }
        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
        });
        clearOrder = function () {
            $("#OrderEntry > TBODY").html("");
        }
        $(document).on('click', '.picc', function () {
            var photo = $(this).attr("src");
            $('#photo1').attr("src", $(this).attr("src"));
            $('#cphoto1').css("display", 'block');
        });
        function ExportToExcel() {
            var data_type = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#container').html());
            var a = document.createElement('a');
            a.href = data_type;
            a.download = 'Claim Status xls';
            a.click();
        }
        $(document).ready(function () {
            dv = $('<div style="position:fixed;left:50%;top:50%;width:50%;height:50%;transform: translate(-50%, -50%);border-radius: 15px;display:none" id="cphoto1"></div>');
            $(dv).html('<span style="position: absolute;padding: 5px;cursor: default;background: #dcd6d652;border-radius: 50%;width: 20px;height: 20px;line-height: 6px;text-align: center;border: solid 1px gray;top: 6px;right: 6px;" onclick="closew()">x</span><img style="width:100%;height:100%;border-radius: 15px;" id="photo1" />')
            $("body").append(dv);
            //loadData1();
            loadData();
            $("#claimstatus  tr:has(td)").each(function () {
                $(this).find('.adminreason').hide();
                var action = $(this).find(".hdnaction").val();
                if (action == "Rejected") {
                    $(this).find(".adminreason").show();
                }
                else
                    $(this).find(".adminreason").hide();
            });
            $("[id*=chkParent]").on("click", function () {
                var isChecked = $(this).prop("checked");
                $('#claimstatus tr:has(td)').find('input[type="checkbox"]').prop('checked', isChecked);
                if (isChecked == true)
                    $("[id*=parentaction]").prop("disabled", false);
                    //$("[id*=btnSave]").prop(sh)
                else
                    $("[id*=parentaction]").prop("disabled", true);
                $("#claimstatus  tr:has(td)").each(function () {
                    if (status == "Rejected") {
                        $(this).find(".adminreason").show();
                    }
                });
            });
            $("[id*=parentaction]").on("change", function () {
                var ddlHeader = $(this).children("option:selected").val();
                var grid = $(this).closest("table");
                var action = $(this).children("option:selected").text();
                $(".ChildAction", grid).each(function () {
                    // var status = $(this).closest('tr').find("[id*=status]").text();
                    var action1 = $(this).closest('tr').find("[id*=parentaction]").children("option:selected").text();
                    var chkbx = $(this).closest('tr').find("[Type=checkbox]")[0];
                    if ($(chkbx).is(":checked")) {
                        $(this).val(ddlHeader);
                    } else {
                        $(this).val("");
                    }
                    if ((ddlHeader == "2")) {
                        $(this).closest('tr').find(".adminreason").show();
                    }
                    else {
                        $(this).closest('tr').find(".adminreason").hide();
                    }
                });
            });
            $(document).on('click', '.save', function () {
                var row = $("#claimstatus  tr:has(td)")
                var arr = [];
                if (row.length > 0) {
                    $("#claimstatus  tr:has(td)").each(function () {
                        //    claimid = $(this).find("[id*=claimid]").text();
                        var ddlrow = $(this).find(".ChildAction");
                        var chkbx = $(this).closest('tr').find("[Type=checkbox]")[0];
                        if ($(chkbx).is(":checked") && (Number($(ddlrow).children("option:selected").val()) > 0)) {
                            arr.push({
                                claimid: $(this).find(".claimid").text(),
                                flag: $(ddlrow).children("option:selected").val(),
                                adminreject: $(this).find(".adminreason").val(),
                                sf: $(this).attr('class')
                            });
                        }
                    });
                    if (arr.length > 0) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "rpt_claim_status_Nsm.aspx/savedata",
                            data: "{'data':'" + JSON.stringify(arr) + "'}",
                            dataType: "json",
                            success: function (data) {
                                alert("Action Updated...");
                                arr = [];
                                loadData();
                            },
                            error: function (data) {
                                alert(JSON.stringify(data));
                            }
                        });
                    }
                    else {
                        alert("No Row Enter Values..!");
                    }
                }
            });
        });
        $(".qqq").on("click", function () {
            $('#exampleModal').modal('toggle');
            loadData4();
        });
        function closew() {
            $('#cphoto1').css("display", 'none');
        }
    </script>
</asp:Content>
