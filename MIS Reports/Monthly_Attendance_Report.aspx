<%@ Page Language="C#" MasterPageFile="~/Master.master"  AutoEventWireup="true" CodeFile="Monthly_Attendance_Report.aspx.cs" Inherits="MIS_Reports_Monthly_Attendance_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
        th, td {
            white-space: nowrap;
        }
        th{            
            cursor: pointer;
        }
        #txtfilter_chzn {
            width: 300px !important;
            top: 10px;
        }

        .sk-folding-cube {
            margin: 40px auto;
            width: 40px;
            height: 40px;
            position: relative;
            -webkit-transform: rotateZ(45deg);
            transform: rotateZ(45deg);
        }

            .sk-folding-cube .sk-cube {
                float: left;
                width: 50%;
                height: 50%;
                position: relative;
                -webkit-transform: scale(1.1);
                transform: scale(1.1);
            }

                .sk-folding-cube .sk-cube:before {
                    content: '';
                    position: absolute;
                    top: 0;
                    left: 0;
                    width: 100%;
                    height: 100%;
                    background-color: #3c4b64;
                    -webkit-animation: sk-foldCubeAngle 2.4s infinite linear both;
                    animation: sk-foldCubeAngle 2.4s infinite linear both;
                    -webkit-transform-origin: 100% 100%;
                    transform-origin: 100% 100%;
                }

            .sk-folding-cube .sk-cube2 {
                -webkit-transform: scale(1.1) rotateZ(90deg);
                transform: scale(1.1) rotateZ(90deg);
            }

            .sk-folding-cube .sk-cube3 {
                -webkit-transform: scale(1.1) rotateZ(180deg);
                transform: scale(1.1) rotateZ(180deg);
            }

            .sk-folding-cube .sk-cube4 {
                -webkit-transform: scale(1.1) rotateZ(270deg);
                transform: scale(1.1) rotateZ(270deg);
            }

            .sk-folding-cube .sk-cube2:before {
                -webkit-animation-delay: .3s;
                animation-delay: .3s;
            }

            .sk-folding-cube .sk-cube3:before {
                -webkit-animation-delay: .6s;
                animation-delay: .6s;
            }

            .sk-folding-cube .sk-cube4:before {
                -webkit-animation-delay: .9s;
                animation-delay: .9s;
            }

        @-webkit-keyframes sk-foldCubeAngle {
            0%,10% {
                -webkit-transform: perspective(140px) rotateX(-180deg);
                transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%,75% {
                -webkit-transform: perspective(140px) rotateX(0deg);
                transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%,100% {
                -webkit-transform: perspective(140px) rotateY(180deg);
                transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }

        @keyframes sk-foldCubeAngle {
            0%,10% {
                -webkit-transform: perspective(140px) rotateX(-180deg);
                transform: perspective(140px) rotateX(-180deg);
                opacity: 0;
            }

            25%,75% {
                -webkit-transform: perspective(140px) rotateX(0deg);
                transform: perspective(140px) rotateX(0deg);
                opacity: 1;
            }

            90%,100% {
                -webkit-transform: perspective(140px) rotateY(180deg);
                transform: perspective(140px) rotateY(180deg);
                opacity: 0;
            }
        }
		  #grid {
            border-collapse: collapse;
            }

            #grid th {
                position: sticky;
                top: 0;
                background: #6c7ae0;
                text-align: center;
                font-weight: normal;
                font-size: 15px;
                color: white;
                }

            #grid td, #grid th {
                padding: 5px;
            }
    </style>
    <form runat="server" id="frm1">
        <div class="row">
            <asp:ImageButton ID="ImageButton1" runat="server" align="right" ImageUrl="~/img/Excel-icon.png" OnClientClick="onclck(event)"
                style="height: 40px; width: 40px; border-width: 0px; position: absolute; right: 15px; top: 43px;" OnClick="lnkDownload_Click" />
        </div>
        <div class="row">
			<div class="col-lg-6 sub-header">
                Monthly Attendance Report
			</div>
			<div class="col-lg-6">
				<div class="card" style="margin:0px;">					
					<table class="Numbers" id="grid" cellpadding="0" cellspacing="0" style="float: right;">
					</table>
				</div>
			</div>
        </div>
        <div class="card">
            <div class="card-body table-responsive" style="overflow-x: auto;">
                <div>
                    Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;" />
                    <label style="float: right">
                        Show
                    <select class="data-table-basic_length" aria-controls="data-table-basic">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                        <option value="All">All</option>
                    </select>
                        entries</label>
                </div>
            </div>
            <div style="overflow-x: auto; width: 100%; margin-bottom: 8px;">
                <div class="sk-folding-cube">
                    <div class="sk-cube1 sk-cube"></div>
                    <div class="sk-cube2 sk-cube"></div>
                    <div class="sk-cube4 sk-cube"></div>
                    <div class="sk-cube3 sk-cube"></div>
                </div>
                <table class="table table-hover" id="OrderList" style="font-size: 12px">
                    <thead class="text-warning av">
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
            <div class="row" style="padding: 5px 0px">
                <div class="col-sm-5">
                    <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 0 to 0 of 0 entries</div>
                </div>
                <div class="col-sm-7">
                    <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                        <ul class="pagination" style="float: right; margin: -11px 0px">
                            <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Previous</a></li>
                            <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="7" tabindex="0">Next</a></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </form>
  
    <script type="text/javascript">
        var AllOrders = [], AllMonth = [], Allleave = [], filtmon = [], fillev = [];
        var SFDivisions = [];
        var SFDepts = [];
        var SFHQs = [];
        var sf;
        var sf1;
        var fdt = '';
        var tdt = '';
        var filtrkey = 'All';
        var sortid = '';
        var asc = true;
        var Orders = [], MonOrders = [], LvOrders = [];
        pgNo = 1; PgRecords = 10; TotalPg = 0; searchKeys = "SlNo,Employee_ID,Employee_Name,Designation,HQ,Joining_Date,Reporting_Manger,State,";
        $(".data-table-basic_length").on("change",
        function () {
            pgNo = 1;
            PgRecords = $(this).val();
            if (PgRecords == 'All') {
                PgRecords = AllOrders.length;
            }
            ReloadTable();
        });
        $("#reportrange").on("DOMSubtreeModified", function () {
            id = $('#ordDate').text();
            id = id.split('-');
            fdt = id[2] + '-' + id[1] + '-' + id[0] + ' 00:00:00';
            tdt = id[5] + '-' + id[4] + '-' + id[3] + ' 00:00:00';
            loadData();
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
        function ReloadTable() {
            $("#OrderList TBODY").html("");
            $("#OrderList THEAD").html("");
            st = PgRecords * (pgNo - 1);
            if (Orders.length > 0) {
                var objkeys = Object.keys(Orders[0]);
                var str = '<tr>';
                for ($i = 0; $i < objkeys.length; $i++) {
                    str += '<th onclick="thclick(this)" class="thclick" id="' + objkeys[$i] + '">' + objkeys[$i] + '</th>';
                }
                str += '</tr>';
                $("#OrderList THEAD").append(str);
                var tbtr = '';
                for ($i = st; $i < (st + PgRecords) ; $i++) {
                    if ($i < Orders.length) {
                        tbtr += '<tr>';
                        for ($j = 0; $j < objkeys.length; $j++) {
                            tbtr += '<td>' + ((Orders[$i][objkeys[$j]]) == null ? '' : (Orders[$i][objkeys[$j]])) + '</td>';
                        }
                        tbtr += '</tr>';
                    }
                }
                $("#OrderList TBODY").append(tbtr);
            }
            else {
                $("#OrderList TBODY").append('<tr><td>No Records Found</td></tr>');
            }
            $('.sk-folding-cube').hide();
            $("#orders_info").html("Showing " + (st + 1) + " to " + (((st + PgRecords) < Orders.length) ? (st + PgRecords) : Orders.length) + " of " + Orders.length + " entries")
            loadPgNos();
        }
        function onclck(e) {
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
        });
        function loadData() {
            $("#OrderList TBODY").html("");
            $("#OrderList THEAD").html("");
            $('.sk-folding-cube').show();
                setTimeout(function () {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Monthly_Attendance_Report.aspx/GetDetails",
                        async: true,
                        data: "{'SF':'<%=sfCode%>','Div':'<%=Session["Division_Code"]%>','Mn':<%=mnth%>,'Yr':<%=yr%>,'subdiv':'<%=subdiv_code%>','statecd':'<%=statv%>'}",
                        dataType: "json",
                        success: function (data) {
                            AllOrders = JSON.parse(data.d) || [];
                            Orders = AllOrders;
                            ReloadTable();
                        },
                        error: function (result) {
                            //alert(JSON.stringify(result.responseText));
                        }
                    });
                }, 500);
            
        }
        function thclick($th) {
            //sortid = $th.id;
            //asc = $($th).attr('asc');
            //if (asc == undefined) asc = 'true';
            //Orders.sort(function (a, b) {
            //    if (a[sortid].toLowerCase() < b[sortid].toLowerCase() && asc == 'true') return -1;
            //    if (a[sortid].toLowerCase() > b[sortid].toLowerCase() && asc == 'true') return 1;

            //    if (b[sortid].toLowerCase() < a[sortid].toLowerCase() && asc == 'false') return -1;
            //    if (b[sortid].toLowerCase() > a[sortid].toLowerCase() && asc == 'false') return 1;
            //    return 0;
            //});

            //$($th).attr('asc', ((asc == 'true') ? 'false' : 'true'));
            //ReloadTable();
        }
        $(document).ready(function () {
            $('.sk-folding-cube').hide();
           
            loadData();
			gethint();
        });
		function gethint() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Monthly_Attendance_Report.aspx/gethints",
                data: '{"divcode":"<%=Session["div_code"]%>"}',
                dataType: "json",
                success: function (data) {
                    hint = JSON.parse(data.d) || [];
                    str = '<tr>';
                    for (var i = 0; i < hint.length; i++) {
                        str += "<td style='color:#162dd3;font-weight: 100;'>" + hint[i].WType_SName + "</td><td  style='color:#cb16a4;font-weight: 100;'>" + hint[i].Wtype + "</td>";
                        if (((i + 1) % 3) == 0) {
                            str += "</tr>"
                            $(".Numbers").append(str); str = '<tr>';
                        } else {
                            continue;
                        }
                    }
                }
            });

         }
    </script>
</asp:Content>
