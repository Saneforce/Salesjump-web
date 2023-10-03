<%@ Page Title="Designation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="Designation.aspx.cs"
    Inherits="MasterFiles_Designation"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "https://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

    <title>Designation</title>

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

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript">
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
        </script>
    <form id="frm1" runat="server">
    <div class="row">
        <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 10000;">
    <div class="modal-dialog"  style="width: 80%;text-align: center;margin: auto;line-height: 0px;">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <input type="hidden" id="descode" data-dismiss="modal" value="des_code"/>
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                <h4 class="modal-title" style="font-weight: bold;
    color: #ee3939;">
                </h4>
            </div>
             <div id="d1" style="overflow: auto;height: 558px;font-size: inherit;">
                </div>
            <div class="modal-footer">
                <input type="button" id="btnsave" value="Submit" class="btn btn-success" onclick="savedata()"/>
                <input type="button" id="btnClosePopup" value="Close" class="btn btn-danger" data-dismiss="modal" />
            </div>
        </div>
    </div>
</div>
        <div class="row">
        <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 55px;" OnClick="ExportToExcel" />
        <div class="col-lg-12 sub-header">Designation<span style="float: right"><a href="DesignationCreation.aspx" class="btn btn-primary btn-update" id="newsf">Add New</a></span><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
               </div> </div>
            </div>
    </div>

    <div class="modal fade" id="FieldModal" style="z-index: 10000000; background: transparent; overflow-y: scroll;" tabindex="0" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 80% !important">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="FieldModalLabel"></h5>
                    </div>
                    <div class="modal-body" style="padding-top: 10px">
                        <div class="row">
                            <div class="col-sm-12">
                                <table id="fielddets" style="width: 100%; font-size: 12px;">
                                    <thead class="text-warning">
                                        <tr>
                                            <th style="text-align: left">S.NO</th>
                                            <th style="text-align: left">FieldForce Code</th>
                                            <th style="text-align: left">FieldForce Name</th>
                                            <th style="text-align: left">Mobile</th>
                                            <th style="text-align: left">HQ</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
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
                        <table class="table table-hover" id="DegList" style="font-size: 12px">
                            <thead class="text-warning">
                                <tr>
                                    <th style="text-align: left">Sl.No</th>
                                    <th id="ShortName" style="text-align: left">Short </th>
                                    <th id="Designation" style="text-align: left">Designation</th>
                                    <th id="FieldforcewiseCount" style="text-align: left">FieldforcewiseCount</th>
                                    <th id="Edit" style="text-align: left">Edit</th>
                                    <th id="Deactivate" style="text-align: left">Status</th>
                                    <th id="Rights" style="text-align:left" class="hrts">Rights</th>
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
                var menurights = '';
                /*var menurights = '', rsfc = '';*/
                var AllOrders = [];
                var filtrkey = '0';
                var Orders = [], pgNo = 1, PgRecords = 10, TotalPg = 0, searchKeys = "Designation_Code,Designation_Name,Designation_Short_Name,";
                

                $(".data-table-basic_length").on("change",
                    function () {
                        pgNo = 1;
                        PgRecords = $(this).val();
                        ReloadTable();
                    }
                );
                function ReloadTable() {
                    var tr = '';
                    $("#DegList TBODY").html("");
                    if (filtrkey != "All") {
                        Orders = Orders.filter(function (a) {
                            return a.Designation_Active_Flag == filtrkey;
                        });
                    }
                    st = PgRecords * (pgNo - 1); slno = 0;
                    for ($i = st; $i < st + PgRecords; $i++) {
                        if ($i < Orders.length) {
                            tr = $("<tr rname='" + Orders[$i].Designation_Name + "'  rocode='" + Orders[$i].Designation_Code + "'></tr>");
                            slno = $i + 1;
                            $(tr).html('<td>' + slno + "</td><td>" + Orders[$i].Designation_Short_Name + '</td><td class="des" id=' + Orders[$i].Designation_Code +'>' + Orders[$i].Designation_Name +
                                "</td><td input type='hidden' class='sfcount'><a href='#'>" + Orders[$i].sf_count + "</a></td><td id=" + Orders[$i].Designation_Code +
                                " class= 'roedit' > <a href='#'>Edit</a></td > <td class='rodeact'><a href='#' style=" + ((parseInt(Orders[$i].Designation_Active_Flag) == 0) ? 'color:green;' : 'color:red;') + " >" + ((parseInt(Orders[$i].Designation_Active_Flag) == 0) ? 'Active' : 'Deactive') +
                                "</a></td><td  menuids=" + Orders[$i].Menuid +" id= " + Orders[$i].Designation_Code + " class= 'rts' > <a href='#'>Rights</a></td >");
                            /*"</a></td><td input type='hidden' class='des' menuids=\"' + Orders[$i].Menuid '\"+ id=" + Orders[$i].Designation_Code + "'><a href='#' class='rts'>Rights</a></td >");*/
                            $("#DegList TBODY").append(tr);
                            hq = [];
                        }
                    }
                    $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries");
                    
                    loadPgNos();
                }
                function getdata() {

                    $('#d1').html('<div style="padding-top: 55px;">Loading...</div>');

                    $.ajax({
                        type: "Post",
                        contentType: "application/json; charset=utf-8",
                        url: "Designation.aspx/display",
                        dataType: "json",
                        success: function (data) {
                            var MainMnu = data.d.filter(function (a) {
                                return a.Menu_Type == '0';
                            });
                            ss = data;
                            //var spmenurights = menurights.split(',');
                            var str = "<table style='border-collapse: separate;border-spacing: 16px; width:100%;'><tr>";
                            for (var i = 0; i < MainMnu.length; i++) {
                                var mchecked = ((menurights.indexOf(',' + MainMnu[i].Menu_ID + ',')) > -1 ? 'checked' : '');
                                str += "<td class='tdclass' id=" + MainMnu[i].Menu_ID + " style='background: #FFFFFF;border: 1px solid #e6e6e6; width:30%;padding: 0px;box-shadow: 0px 2px 1px #0000001f;text-align: left;' valign='top'><div class='divclass1' id=" + MainMnu[i].Menu_ID + " style='background: #19a4c6; padding: 5px; box-shadow: 0px 3px 9px -2px #0000007a;color:#fff;'><label><input type='checkbox'  class='sss' name='check2' value=" + MainMnu[i].Menu_ID + " id=" + MainMnu[i].Menu_ID + " " + mchecked + "/> " + MainMnu[i].Menu_Name + "</label></div><div name='mas'  value=" + MainMnu[i].Menu_ID + " class='" + MainMnu[i].Menu_ID + "' id=" + MainMnu[i].Menu_ID + " style='padding:10px 0px;'>";
                                var s = data.d.filter(function (a) {
                                    return (a.Parent_Menu == MainMnu[i].Menu_ID)
                                });
                                for (var j = 0; j < s.length; j++) {
                                    var schecked = ((menurights.indexOf(',' + s[j].Menu_ID + ',')) > -1 ? 'checked' : '');

                                    if (s[j].Menu_Type == 1) {
                                        str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><label><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + " " + schecked + " /> " + s[j].Menu_Name + "</label></div>";

                                        var h = data.d.filter(function (a) {
                                            return (a.Parent_Menu == s[j].Menu_ID)
                                            // return (a.Parent_Menu == MainMnu[i].Menu_ID)
                                        });

                                        for (var r = 0; r < h.length; r++) {
                                            var hchecked = ((menurights.indexOf(',' + h[r].Menu_ID + ',')) > -1 ? 'checked' : '');

                                            str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (h[r].lvl - 1))) + "px'><label  style='font-weight:normal;'><input type='checkbox' id= " + h[r].Menu_ID + "  value=" + h[r].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check3' title=" + MainMnu[i].Menu_ID + "  " + hchecked + " /> " + h[r].Menu_Name + "</label></div>";

                                        }
                                    }
                                    else {

                                        str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><label style='font-weight:normal;'><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + "  /> " + s[j].Menu_Name + "</label></div>";
                                    }



                                    //str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + "  />" + s[j].Menu_Name + "</div>";
                                }
                                str += "</div></td>";
                                if (((i + 1) % 3) == 0) {
                                    str += "</tr></tr>";
                                }
                            }
                            $('#d1').html(str);
                            $('.sss').click(function () {

                                var b = $(this).attr('value');
                                var c = $(this).attr('checked') ? true : false;
                                $('.cchk' + b).each(function () {
                                    $(this).prop("checked", c);
                                });
                            });
                            $('input[name=check2]').click(function () {

                                var m = $(this).attr('id');
                                var u = $(this).attr('title');

                                var c = $(this).attr('checked') ? true : false;
                                $('input[value="' + m + '"][name=check3]').each(function () {
                                    $(this).prop("checked", c);
                                });
                                $('input[value="' + u + '"].sss').prop("checked", "checked");
                            });
                            $('input[name=check3]').click(function () {

                                var m = $(this).attr('value');
                                var c = $(this).attr('checked') ? true : false;
                                var u = $(this).attr('title');
                                $('input[value="' + u + '"].sss').prop("checked", "checked");
                                $('input[id="' + m + '"][name=check2]').each(function () {
                                    $(this).prop("checked", "checked");
                                });
                            });
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                };
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

                function fillFields(divcode) {
                    $('#fielddets tbody').html('');
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Designation.aspx/Field_Count",
                        data: "{'divcode':'<%=Session["div_code"]%>','dsgcode':'" + divcode + "'}",
                        dataType: "json",
                        success: function (data) {
                            var AllOrders2 = JSON.parse(data.d) || [];
                            $('#fielddets TBODY').html("");
                            var slno = 0;
                            for ($i = 0; $i < AllOrders2.length; $i++) {
                                if (AllOrders2.length > 0) {
                                    slno += 1;
                                    tr = $("<tr></tr>");
                                    $(tr).html("<td>" + slno + "</td><td>" + AllOrders2[$i].Sf_Code + "</td><td>" + AllOrders2[$i].Sf_Name + "</td><td>" + AllOrders2[$i].SF_Mobile + "</td><td>" + AllOrders2[$i].Sf_HQ + "</td>");
                                    $("#fielddets TBODY").append(tr);
                                }
                            }
                        }
                    });
                }
                function loadData(div_code) {

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Designation.aspx/GetList",
                        data: "{'div_code':'" + div_code + "'}",
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
                    div_code = Number(<%=Session["div_code"]%>);
                    loadData(div_code);
                    $(document).on('click', '.roedit', function () {
                        x = this.id;
                        window.location.href = "DesignationCreation.aspx?Designation_Code=" + x + "&Division_Code=" + div_code;
                        /*window.location.href = "DesignationCreation.aspx?Designation_Code={0}&Division_Code={1}", x, div_code;*/
                    });
                    $(document).on('click', '.sfcount', function () {
                        $('#FieldModal').modal('toggle');
                        var route_C = $(this).closest('tr').attr('rocode');
                        var route_N = $(this).closest('tr').attr('rname');
                        $('#FieldModalLabel').text("FieldForce for " + route_N);
                        fillFields(route_C);
                    });
                    $(document).on("click", ".rodeact", function () {
                        var row = $(this).closest('tr');
                        var route_C = $(this).closest('tr').attr('rocode');
                        let oindex = Orders.findIndex(x => x.Designation_Code == route_C);
                        let disstat = (parseInt(Orders[oindex]["Designation_Active_Flag"]) == 0) ? 'Deactive' : 'Active';
                        let flg = (parseInt(Orders[oindex]["Designation_Active_Flag"]) == 0) ? 1 : 0;
                        let flag = 0;
                        if ((flg == 1) || (flg == 0)) {

                            if (flag == 0) {
                                if (confirm("Do you want " + disstat + " the Designation ?")) {

                                    $.ajax({
                                        type: "POST",
                                        contentType: "application/json; charset=utf-8",
                                        async: false,
                                        url: "Designation.aspx/deactivate",
                                        data: "{'dsgcode':'" + route_C + "','stat':'" + flg + "','div_code':'" + div_code + "'}",
                                        dataType: "json",
                                        success: function (data) {
                                            if (data.d == 'Success') {
                                                Orders[oindex]["Designation_Active_Flag"] = flg;
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

                    });
                    $(document).on('click', '.rts', function () {
                        var title = "Menu Rights";
                        var rsfc = $(this).closest('td').closest('tr').find('.des').attr('id');//$(this).closest('td').closest('tr').find('.des').val()
                        //rsfc = $(this).closest('td').closest('tr').find('.sfedit').attr('id');
                        $('#descode').val(rsfc);
                        menurightss = $(this).closest('td').attr('menuids');
                        menurights = ',' + menurightss + ',';
                        getdata();
                        $("#MyPopup .modal-title").html(title);
                        //$("#MyPopup .modal-body").html(body);
                        $("#MyPopup").modal("show");

                    });
                    $("#btnClosePopup").click(function () {
                        $("#MyPopup").modal("hide");
                    });
                    $('#btnsave').click(function () {
                        var des_code = $('#descode').val();
                        var valuesArray2 = $('input[name=check2]:checked').map(function () {
                            return $(this).attr('id');
                        }).get().join(',');

                        var valuesArray3 = $('input[name=check3]:checked').map(function () {
                            return $(this).attr('id');
                        }).get().join();

                        var result = valuesArray2.concat(",");
                        var f = result.concat(valuesArray3);
                        if (f == '' || f == ',') {
                            alert("Please Select Menu");
                            return false;
                        }
                        $.ajax({
                            type: "Post",
                            contentType: "application/json; charset=utf-8",
                            data: "{'des_codes':'" + des_code + "','arr':'" + f + "'}",
                            url: "Designation.aspx/savedata",
                            dataType: "json",
                            success: function () {
                                alert("Menu List has been Added successfully");
                                $('input[name=check1]:checked').removeAttr('checked');
                                $('input[name=check2]:checked').removeAttr('checked');
                                $('input[name=check3]:checked').removeAttr('checked');

                                $('.sss').removeAttr('checked');

                            },
                            error: function (result) {
                                alert(JSON.stringify(result));
                            }
                        });
                    });
                

                });
            </script>
</asp:Content>
