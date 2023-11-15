<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="SalesForce_List.aspx.cs" Inherits="MasterFiles_SalesForce_List" %>

<asp:Content ID="Content1" class=".content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />																							   
<style>
    #txtfilter_chzn
    {
        width:328px !important;
        position:absolute;
    }
    #txtfilter{
        width:250px !important;
        position:absolute;
    }
    .bootstrap-select{
        width:100% !important;
    }
	li
    {
        font-weight:100;
    }

        .sub-header {
            margin-top: 54px;
        }

        .bs-example {
            margin: 20px;
        }
        #draggablePanelList .panel-heading {
            cursor: move;
        }
</style>
    <form id="frm1" runat="server">
        <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000;background-color:transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document" style="width: 750px">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="row">
                            <div class="col-sm-7">
                                <h5 class="modal-title" id="exampleModalLabel">Approval Hierarchy</h5>
                            </div>
                            <div class="col-sm-1" style="text-align: right; padding-top: 3px;">
                                <span>Type</span>
                            </div>
                            <div class="col-sm-4" style="display: inline-flex;">
                                <select id="apptyp" class="form-control">
                                    <option value="">Select</option>
                                    <option value="1">Expense</option>
									 <option value="2">TP</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-sm-6">
                                <select id="selmgr" data-dropupAuto="true"></select>
                            </div>
                            <div class="col-sm-2">
                                <button type="button" style="background-color: #1a73e8; text-decoration-color: white;" id="addLvl" class="btn btn-primary">+</button>
                            </div>
                        </div>
                        <ul id="draggablePanelList" class="list-unstyled">
                        </ul>
                        <%-- <div class="panel panel-default">
                            <div class="panel-heading" style="margin: 5px 0px 0px 0px;">
                                <div class="row">
                                </div>
                            </div>
                            <!-- /.panel-heading -->
                            <div class="panel-body">
                            </div>
                            <!-- /.panel-body -->
                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        <button type="button" style="background-color: rgb(26 115 232 / 1);" class="btn btn-primary" id="svleave">Save</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Popup -->
        <div id="MyPopup" class="modal fade" role="dialog" style="z-index: 100000;">
            <div class="modal-dialog" style="width: 80%; text-align: center; margin: auto; line-height: 0px;">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="hidden" id="sfcode" data-dismiss="modal" value="sf_code" />
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title" style="font-weight: bold; color: #ee3939;"></h4>
                    </div>
                    <div id="d1" style="overflow: auto; height: 558px; font-size: inherit;">
                    </div>
                    <div class="modal-footer">
                        <input type="button" id="btnsave" value="Submit" class="btn btn-success" onclick="savedata();" />
                        <input type="button" id="btnClosePopup" value="Close" class="btn btn-danger" data-dismiss="modal" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png"
                Style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 55px;" OnClick="ExportToExcel" />
        </div>
        <div class="row">
            <div class="col-lg-12 sub-header">FieldForce<span style="float: right">
                <a href="#" class="btn btn-primary btn-update" id="newsf">New ID Creation</a></span><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
                </div></div>
        </div>
        <%--<div class="row">
            <div class="col-lg-12 sub-header">FieldForce<span style="float: right"><a href="#" class="btn btn-primary btn-update" id="newsf">New ID Creation</a></span><div style="float: right;padding-top: 3px;">
                    <ul class="segment">
                        <li data-va='All'>ALL</li>
                        <li data-va='0' class="active">Active</li>
                    </ul>
                </div></div>
        </div>--%>


        <div class="card">
            <div class="card-body table-responsive">
                <div style="white-space: nowrap">
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="white-space: nowrap; margin-left: 57px;">Filter By&nbsp;&nbsp;</label>
                    <select id="txtfilter" name="ddfilter" style="width: 100px;"></select>
                    <label style="float: right">Show
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
                            <th style="text-align:left">SlNo</th>
                            <th style="text-align:left">Emp.Code</th>
                            <th style="text-align:left">Emp.Name</th>
                            <th style="text-align:left">Designation</th>
                            <th style="text-align:left">HQ</th>
                            <th style="text-align:left">State</th>
                            <th style="text-align:left">Mobile.No</th>
                            <th style="text-align:left">Manager Name</th>
                            <th style="text-align:left">DOJ</th>
							<th style="text-align:left">App Version</th>
                            <th style="text-align:left">Edit</th>
                            <th style="text-align:left">Status</th>
                            <th style="text-align:left;display:none;" class="hrts">Rights</th>
                        </tr>
                    </thead>
                    <tbody>
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
    </form>
	<script src="<%=Page.ResolveUrl("~/js/jquery.min.js")%>" type="text/javascript"></script>
    <script type="text/javascript" src="../js/plugins/datatables/jquery.dataTables.js"></script>
    <script type="text/javascript" src="../js/plugins/datatables/dataTables.bootstrap.js"></script>
	<script type="text/javascript" src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script language="javascript" type="text/javascript">
        var menurights = '',rsfc='';
        var AllOrders = [];
        var filtrkey = '0';
        if ('<%=Session["div_code"]%>' == '4' || '<%=Session["div_code"]%>' == '100' || '<%=Session["div_code"]%>' == '109')
            optrights = "<li><a href='#' r='1'>Menu Rights</a></li>";
        else
            optrights = "<li><a href='#' r='0'>Approval Hierarchy</a><a href='#' r='1'>Menu Rights</a></li>";
        optStatus = "<li><a href='#' v='0'>Active</a><a href='#' v='1'>Vacant / Block</a><a href='#' v='2'>Deactivate</a></li>"
        var Orders = []; pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "Sf_Code,Employee_Id,Sf_Name,StateName,SF_Mobile,DeptName,SF_Email";
        $(".data-table-basic_length").on("change",
            function () {
                pgNo = 1;
                PgRecords = $(this).val();
                ReloadTable();
            if('<%=Session["div_code"]%>'=='4'||'<%=Session["div_code"].ToString()%>' == '106'||'<%=Session["div_code"].ToString()%>' == '107'||'<%=Session["div_code"].ToString()%>' == '109'||'<%=Session["div_code"].ToString()%>' == '150'||'<%=Session["div_code"].ToString()%>' == '29')
            {
                $('.hrts').show();
                $('#OrderList>tbody>tr>.rts').show();
            }
            }
        );
		
		$(document).keypress(
            function (event) {
                if (event.which == '13') {
                    event.preventDefault();
                }
            });


        function sfdeact(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SalesForce_List.aspx/sfDeact",
                data: "{'sfcode':'" + x + "'}",
                dataType: "json",
                success: function (data) {
                    var i = data.d;
                    if (i > 1) {
                        alert('Deactivated Successfully');
                    }
                    else {
                        alert('Deactivation Failed');
                    }
                },
                error: function (rs) {

                }
            });
        }
        function loadPgNos() {
            prepg = parseInt($(".paginate_button.previous>a").attr("data-dt-idx"));
            Nxtpg = parseInt($(".paginate_button.next>a").attr("data-dt-idx"));
            $(".pagination").html("");
            TotalPg = parseInt(parseInt(Orders.length / PgRecords) + ((Orders.length % PgRecords) ? 1 : 0)); selpg = 1;
            if (isNaN(prepg)) prepg = 0;
            if (isNaN(Nxtpg)) Nxtpg = 2;
          //  if ((prepg + 1) == pgNo && pgNo > 1) selpg = (parseInt(pgNo) - 1);
            selpg =(pgNo > 7)? (parseInt(pgNo) + 1) - 7:1;
            if ((Nxtpg) == pgNo){
                 selpg = (parseInt(TotalPg)) - 7;
                 selpg =(selpg>1)? selpg:1;
            }
            spg = '<li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>';
            for (il = selpg - 1; il < selpg + 7; il++) {
                if (il < TotalPg)
                    spg += '<li class="paginate_button' + ((pgNo == (il + 1)) ? " active" : "") + '"><a href="#" aria-controls="example2" data-dt-idx="' + (il + 1) + '" tabindex="0">' + (il + 1) + '</a></li>';
            }
            spg += '<li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="' + TotalPg + '" tabindex="0">Last</a></li>';
            $(".pagination").html(spg);

            $(".paginate_button > a").on("click", function () {
                pgNo = parseInt( $(this).attr("data-dt-idx")); ReloadTable();
            if('<%=Session["div_code"]%>'=='4'||'<%=Session["div_code"].ToString()%>' == '106'||'<%=Session["div_code"].ToString()%>' == '107'||'<%=Session["div_code"].ToString()%>' == '109'||'<%=Session["div_code"].ToString()%>' == '150' ||'<%=Session["div_code"].ToString()%>' == '29')
            {
                $('.hrts').show();
                $('#OrderList>tbody>tr>.rts').show();
            }
                /* $(".paginate_button").removeClass("active");
                 $(this).closest(".paginate_button").addClass("active");*/
            }
           );
        }
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            if (filtrkey != "All") {
                Orders = Orders.filter(function (a) {
                    return a.Active_Flag == filtrkey;
                })
            }
            st = PgRecords * (pgNo - 1); slno = 0;
                var filtered = Orders.filter(function (x) {
                    return x.Sf_Code != 'admin';
                });
            for ($i = st; $i < st + PgRecords ; $i++) {
                if ($i < filtered.length) {
                    tr = $("<tr></tr>");
                    //var hq = filtered[$i].Sf_Name.split('-');
                    slno = $i + 1;
					$(tr).html('<td>' + slno + '</td><td>' + filtered[$i].sf_emp_id + '</td><td>' + filtered[$i].SFNA + '</td><td>' + filtered[$i].sf_Designation_Short_Name +
                    '</td><td>' + filtered[$i].sf_hq + '</td><td>' + filtered[$i].StateName + '</td><td>' + filtered[$i].SF_Mobile + '</td><td>' + filtered[$i].Reporting_To + '</td><td>' + filtered[$i].Joining_Date + '</td><td>' + filtered[$i].Appversion + '</td><td id=' + filtered[$i].Sf_Code + ' class="sfedit" align="center"><a href="#">Edit</a></td>' +
                    '<td><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span class="aState" data-val="' + filtered[$i].Status + '">' + filtered[$i].Status + '</span></span></a>' +
                       '</td>' + ((filtered[$i].Status)=="Active"? '<td menuids=\"' + filtered[$i].Menuid + '\" id=' + filtered[$i].Sf_Code + ' class="rts" style="text-align:left;display:none;" align="center"><ul class="nav" style="margin:0px"><li class="dropdown"><a href="#" style="padding:0px" class="dropdown-toggle" data-toggle="dropdown">'
                        + '<span><span data-val="Rights">Rights</span><i class="caret" style="float:right;margin-top:8px;margin-right:0px"></i></span></a>' +
                        '<ul class="dropdown-menu dropdown-custom dropdown-menu-right ddlrights" style="right:0;left:auto;">' + optrights + '</ul></li></ul></td>':'<td class="rts" style="text-align:left;display:none;">Rights</td>'));

                    $("#OrderList TBODY").append(tr);
                    hq = [];
                }
            }
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < filtered.length) ? (st + PgRecords) : filtered.length) + " of " + filtered.length + " entries")
            $(".ddlrights>li>a").on("click", function () {
                if ($(this).attr("r") == "0") {
                    var title = "Approval Hierarchy";
                    rsfc = $(this).closest('td').closest('tr').find('.sfedit').attr('id');
                    $('#exampleModal').modal('toggle');
                    $('#draggablePanelList').html('');
                    $('#selmgr').val('');
                    $('#apptyp').val('');
                    getworkedwith(rsfc);
                }
                if ($(this).attr("r") == "1") {
                    var title = "Menu Rights";
                    rsfc = $(this).closest('td').closest('tr').find('.sfedit').attr('id');
                    $('#sfcode').val(rsfc);
                    menurights = $(this).closest('td').attr('Menuids');
                    getdata();
                    $("#MyPopup .modal-title").html(title);
                    //$("#MyPopup .modal-body").html(body);
                    $("#MyPopup").modal("show");
                }
            });   
        $(".ddlStatus>li>a").on("click", function () {
            cStus = $(this).closest("td").find(".aState");
            stus = $(this).attr("v");
            $indx = $(this).closest("tr").index();
            cStusNm = $(this).text();
            if (confirm("Do you want change status " + $(cStus).text() + " to " + cStusNm + " ?")) {
                sf = Orders[$indx].ID;
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "SalesForce_List.aspx/sfDeact",
                    data: "{'SF':'" + sf + "','stus':'" + stus + "'}",
                    dataType: "json",
                    success: function (data) {
                        Orders[$indx].Active_Flag = stus;
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
            if('<%=Session["div_code"]%>'=='4'||'<%=Session["div_code"].ToString()%>' == '106'||'<%=Session["div_code"].ToString()%>' == '107'||'<%=Session["div_code"].ToString()%>' == '109'||'<%=Session["div_code"].ToString()%>' == '150' || '<%=Session["div_code"].ToString()%>' == '100' ||'<%=Session["div_code"].ToString()%>' == '29')
            {
                $('.hrts').show();
                $('#OrderList>tbody>tr>.rts').show();
            }
        })

        $(".segment>li").on("click", function () {
            $(".segment>li").removeClass('active');
            $(this).addClass('active');
            filtrkey = $(this).attr('data-va');
            Orders = AllOrders;
            $("#tSearchOrd").val('');
            ReloadTable();
            if('<%=Session["div_code"]%>'=='4'||'<%=Session["div_code"].ToString()%>' == '106'||'<%=Session["div_code"].ToString()%>' == '107'||'<%=Session["div_code"].ToString()%>' == '109'||'<%=Session["div_code"].ToString()%>' == '150' || '<%=Session["div_code"].ToString()%>' == '100' ||'<%=Session["div_code"].ToString()%>' == '29')
            {
                $('.hrts').show();
                $('#OrderList>tbody>tr>.rts').show();
            }
        });
        function loadData(sfcode) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SalesForce_List.aspx/GetList",
                data: "{'divcode':'<%=Session["div_code"]%>','sfcode':'"+sfcode+"'}",
                dataType: "json",
                success: function (data) {
                    AllOrders = JSON.parse(data.d) || [];
                    Orders = AllOrders; ReloadTable();
                    /*for ($i = 0; $i < ordList.length; $i++) {
                    tr = $("<tr></tr>");
                    $(tr).html("<td>" + ordList[$i].OrderNo + "</td><td>" + ordList[$i].Order_Date_Disp + "</td><td>" + ordList[$i].Retail + "</td><td>" + ordList[$i].Order_Value.toFixed(2) + "</td><td>" + ordList[$i].Sf_Name + "</td><td>" + ordList[$i].Status + "</td>");
                    $("TBODY").append(tr);
                    }*/
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
        }
		
        function getdata() {

            $('#d1').html('<div style="padding-top: 55px;">Loading...</div>');

            $.ajax({
                type: "Post",
                contentType: "application/json; charset=utf-8",
                url: "SalesForce_List.aspx/display",
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
                        str += "<td class='tdclass' id=" + MainMnu[i].Menu_ID + " style='background: #FFFFFF;border: 1px solid #e6e6e6; width:30%;padding: 0px;box-shadow: 0px 2px 1px #0000001f;text-align: left;' valign='top'><div class='divclass1' id=" + MainMnu[i].Menu_ID + " style='background: #19a4c6; padding: 5px; box-shadow: 0px 3px 9px -2px #0000007a;color:#fff;'><label><input type='checkbox'  class='sss' name='check2' value=" + MainMnu[i].Menu_ID + " id=" + MainMnu[i].Menu_ID + " " + mchecked + " /> " + MainMnu[i].Menu_Name + "</label></div><div name='mas'  value=" + MainMnu[i].Menu_ID + " class='" + MainMnu[i].Menu_ID + "' id=" + MainMnu[i].Menu_ID + " style='padding:10px 0px;'>";
                        var s = data.d.filter(function (a) {
                            return (a.Parent_Menu == MainMnu[i].Menu_ID)
                        });

                        console.log(s);

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

                                    str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (h[r].lvl - 1))) + "px'><label  style='font-weight:normal;'><input type='checkbox' id= " + h[r].Menu_ID + "  value=" + h[r].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check3' title=" + MainMnu[i].Menu_ID + " " + hchecked + " /> " + h[r].Menu_Name + "</label></div>";

                                }
                            }
                              if (s[j].Menu_Type == 2) {
                                str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (s[j].lvl - 1))) + "px'><label><input type='checkbox' id= " + s[j].Menu_ID + "  value=" + s[j].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check2' title=" + MainMnu[i].Menu_ID + " " + schecked + " /> " + s[j].Menu_Name + "</label></div>";

                                var n = data.d.filter(function (a) {
                                    return (a.Parent_Menu == s[j].Menu_ID)
                                    // return (a.Parent_Menu == MainMnu[i].Menu_ID)
                                });

                                for (var r = 0; r < n.length; r++) {
                                    var hchecked = ((menurights.indexOf(',' + n[r].Menu_ID + ',')) > -1 ? 'checked' : '');

                                    str += "<div class=" + MainMnu[i].Menu_ID + " style='padding-left:" + (5 + (25 * (h[r].lvl - 1))) + "px'><label  style='font-weight:normal;'><input type='checkbox' id= " + n[r].Menu_ID + "  value=" + n[r].Parent_Menu + " class='cchk" + MainMnu[i].Menu_ID + "' name='check3' title=" + MainMnu[i].Menu_ID + " " + hchecked + " /> " + n[r].Menu_Name + "</label></div>";

                                }
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
        function getworkedwith($sffc) {
             $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SalesForce_List.aspx/getSFWorkedWith",
                data: "{'sfcode':'" + $sffc + "'}",
                dataType: "json",
                success: function (data) {
                    $('#selmgr').empty().append('<option value="">Select</option>');
                    $('#selmgr').append('<option value="admin">admin-Admin</option>');
                    var filtmgr = JSON.parse(data.d);
                    for ($i = 0; $i < filtmgr.length; $i++) {
                        $('#selmgr').append('<option value="' + filtmgr[$i].id + '">' + filtmgr[$i].name + '</option>');
                    }
                    $('#selmgr').selectpicker({
                        liveSearch: true
                    });
                    $('#selmgr').selectpicker('refresh');
                }
            });
        }
        function getApprSFDets($appTyp) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SalesForce_List.aspx/getApprSFDets",
                data: "{'SF':'" + rsfc + "','div':'<%=Session["div_code"]%>','apprTyp':'" + $appTyp + "'}",
                dataType: "json",
                success: function (data) {
                    var sfApprDets = JSON.parse(data.d);
                    if (sfApprDets.length > 0) {
                        for ($i = 0; $i < sfApprDets.length; $i++) {
                            $('#draggablePanelList').append(`<li class='panel panel-info' style='margin-bottom: 1rem;'><div class='panel-heading' tsf='${sfApprDets[$i].SF}'><span class="apsf">${sfApprDets[$i].SFNM}</span><i class="fa fa-trash-o" style="float: right;line-height: 20px;margin-left: 2rem;cursor:pointer !important"></i>&nbsp;<span class="aplvl" style='float:right;'>${sfApprDets[$i].Lvl}</span></div></li>`);
                            $('#selmgr > option').each(function () {
                                if ($(this).val() == sfApprDets[$i].SF) {
                                    $(this).remove();
                                    $('#selmgr').selectpicker('refresh');
                                }
                            });
                        }
                    }
                }
            });
        }
        function fillfilter() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "SalesForce_List.aspx/getMGR",
                data: "{'divcode':'<%=Session["div_code"]%>'}",
                dataType: "json",
                success: function (data) {
                    var sf = $("[id*=txtfilter]");
                    sf.empty().append('<option selected="selected" value="0">Select Filter</option>');
                    for (var i = 0; i < data.d.length; i++) {
                        sf.append($('<option value="' + data.d[i].sfcode + '">' + data.d[i].sfname + '</option>')).trigger('chosen:updated').css("width", "100%");;;
                    };
                }
            });
            $('[id*=txtfilter]').chosen();
        }

        $(document).ready(function () {
            var sf = 'admin';
            loadData(sf);
            fillfilter();
            $('#addLvl').on('click', function () {
                var hrsf = $('#selmgr').val();
                var hrsfname = $('#selmgr :selected').text();
                if ($('#apptyp').val() == '') {
                    alert('Select a Type');
                    return false;
                }
                if (hrsf == '') {
                    alert('Select a Manager');
                    return false;
                }
                $('#draggablePanelList').append(`<li class='panel panel-info' style='margin-bottom: 1rem;'><div class='panel-heading' tsf='${hrsf}'><span class="apsf">${hrsfname}</span><i class="fa fa-trash-o" style="float: right;line-height: 20px;margin-left: 2rem;cursor:pointer !important"></i>&nbsp;<span class="aplvl" style='float:right;'>Level-${($('#draggablePanelList>li').length + 1)}</span></div></li>`);
                $('#selmgr :selected').remove();
                $('#selmgr').selectpicker('refresh');
            });
            $(document).on('click', '.panel-heading>i', function () {
                var addrsf = $(this).closest('.panel-heading').attr('tsf');
                var addrsfname = $(this).closest('.panel-heading').find('.apsf').text();
                $('#selmgr').append('<option value="' + addrsf + '">' + addrsfname + '</option>');
                $('#selmgr').selectpicker('refresh');
                $(this).parent().parent().remove();
                var panelList = $('#draggablePanelList');
                $('.panel', panelList).each(function (index, elem) {
                    var $listItem = $(elem),
                        newIndex = $listItem.index();
                    $(elem).find('div>.aplvl').html("Level - " + ($listItem.index() + 1))
                    // Persist the new indices.
                });
            }); 
            if('<%=Session["div_code"]%>'=='4'||'<%=Session["div_code"].ToString()%>' == '106'||'<%=Session["div_code"].ToString()%>' == '107'||'<%=Session["div_code"].ToString()%>' == '109'||'<%=Session["div_code"].ToString()%>' == '150' || '<%=Session["div_code"].ToString()%>' == '100' ||'<%=Session["div_code"].ToString()%>' == '29')
            {
                $('.hrts').show();
                $('#OrderList>tbody>tr>.rts').show();
            }
            $(document).on('click', '#newsf', function () {
                if (Number(JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto)>0 || '<%=Session["div_code"]%>' == '4' || '<%=Session["div_code"]%>' == '29' || '<%=Session["div_code"]%>' == '109' || '<%=Session["div_code"]%>' == '100' || '<%=Session["div_code"]%>' == '162' || '<%=Session["div_code"]%>' == '98') {
                    window.location.href = "SalesForce_ApprvHyr.aspx";
                }
                else {
                    window.location.href = "SalesForce_New.aspx";
                }
            });

            $(document).on('click', '.sfedit', function () {
                x = this.id;
                
                if (Number(JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto)>0 || '<%=Session["div_code"]%>' == '4'  || '<%=Session["div_code"]%>' == '29' || '<%=Session["div_code"]%>' == '109' || '<%=Session["div_code"]%>' == '100'|| '<%=Session["div_code"]%>' == '162' || '<%=Session["div_code"]%>' == '98') {
                    window.location.href = "SalesForce_ApprvHyr.aspx?sfcode=" + x;
                }
                else {
                    window.location.href = "SalesForce_New.aspx?sfcode=" + x;
                }
            });

            $('select[name="ddfilter"]').change(function () {
                var r = $(this).val();
                loadData(r);
            });
            $('#apptyp').on('change', function () {
                var appTypVal = $(this).val();
                if (appTypVal == "") {
                    alert("Select a Type");
                    return false;
                }
                getApprSFDets(appTypVal);
            });
            $('#svleave').on('click', function () {
                var appHyrTyp = $('#apptyp').val();
                var appHyrNm = $('#apptyp :selected').text();
                var appFSFC = rsfc;
                var appSfHyr = [];
                if (appHyrTyp == "") {
                    alert("Select Type");
                    return false;
                }
                if ($('#draggablePanelList').find('li').length < 1) {
                    alert('User Must Have Atleast One Approval Manager');
                    return false;
                }
                else {
                    $('#draggablePanelList').find('li').each(function () {
                        appHyr = {};
                        appHyr.SF = $(this).find('div').attr('tsf');
                        appHyr.SFN = $(this).find('.apsf').text();
                        appHyr.LvlTxt = $(this).find('.aplvl').text();
                        appHyr.Lvl = parseInt($(this).find('.aplvl').text().length-1);
                        appSfHyr.push(appHyr);
                    });
                }
                var sXMl = '<ROOT>';
                for ($i = 0; $i < appSfHyr.length; $i++) {
                    sXMl += "<ASSD ALvl=\"" + appSfHyr[$i].Lvl + "\" ASF=\"" + appSfHyr[$i].SF + "\" ALvlTxt=\"" + appSfHyr[$i].LvlTxt + "\" ASFN=\"" + appSfHyr[$i].SFN + "\" />"
                }
                sXMl += "</ROOT>";
                console.log(sXMl);
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: "{'sf':'" + appFSFC + "','Div':'<%=Session["div_code"].ToString().TrimEnd(',')%>','Appr_Type':'" + appHyrTyp + "','Appr_Name':'" + appHyrNm + "','cusxml':'" + sXMl + "'}",
                    url: "SalesForce_List.aspx/svApprHierarchy",
                    dataType: "json",
                    success: function (data) {
                        var rmsg = data.d;
                        alert(rmsg);
                        $('#exampleModal').modal('toggle');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });
            $('#btnsave').click(function () {
                var sf_code = $('#sfcode').val();
                var valuesArray2 = $('input[name=check2]:checked').map(function () {
                    return $(this).attr('id');
                }).get().join(',');

                var valuesArray3 = $('input[name=check3]:checked').map(function () {
                    return $(this).attr('id');
                }).get().join();

                var result = valuesArray2.concat(",");
                var f = result.concat(valuesArray3);
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    data: "{'sf_codes':'" + sf_code + "','arr':'" + f + "'}",
                    url: "SalesForce_List.aspx/savedata",
                    dataType: "json",
                    success: function () {
                        alert("Menu List has been Added successfully");
                        $('input[name=check1]:checked').removeAttr('checked');
                        $('input[name=check2]:checked').removeAttr('checked');
                        $('input[name=check3]:checked').removeAttr('checked');

                        $('.sss').removeAttr('checked');
                        location.reload();
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });
        $("#btnClosePopup").click(function () {
            $("#MyPopup").modal("hide");
        });
	if(Number(JSON.parse(localStorage.getItem('Access_Details'))[0].Exp_Web_Auto) > 0){
	 $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: true,
                url: "SalesForce_List.aspx/GetHyrUpdt",                
                dataType: "json",
                success: function (data) {
                    
                },
                error: function (result) {
                    //alert(JSON.stringify(result));
                }
            });
	}
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var panelList = $('#draggablePanelList');
            new Sortable(draggablePanelList, {
                animation: 150,
                ghostClass: 'blue-background-class',
                handle: '.panel-heading',
                onUpdate: function () {
                            $('.panel', panelList).each(function (index, elem) {
                                var $listItem = $(elem),
                                    newIndex = $listItem.index();
                                $(elem).find('div>.aplvl').html("Level-" + ($listItem.index() + 1))
                            });
                        }
            }); 
		});
    </script>
</asp:Content>