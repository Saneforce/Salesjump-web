<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Expense_Approval_SingleDay.aspx.cs" Inherits="MasterFiles_Expense_Approval_SingleDay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <a href="../Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.js.map"></a>
    <link href="../Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.css" rel="stylesheet" />
    <script src="../Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.js"></script>

    <style>
        .spinner {
            margin: 100px auto;
            width: 50px;
            height: 40px;
            text-align: center;
            font-size: 10px;
        }

            .spinner > div {
                background-color: #333;
                height: 100%;
                width: 6px;
                display: inline-block;
                -webkit-animation: sk-stretchdelay 1.2s infinite ease-in-out;
                animation: sk-stretchdelay 1.2s infinite ease-in-out;
            }

            .spinner .rect2 {
                -webkit-animation-delay: -1.1s;
                animation-delay: -1.1s;
            }

            .spinner .rect3 {
                -webkit-animation-delay: -1.0s;
                animation-delay: -1.0s;
            }

            .spinner .rect4 {
                -webkit-animation-delay: -0.9s;
                animation-delay: -0.9s;
            }

            .spinner .rect5 {
                -webkit-animation-delay: -0.8s;
                animation-delay: -0.8s;
            }

        @-webkit-keyframes sk-stretchdelay {
            0%, 40%, 100% {
                -webkit-transform: scaleY(0.4)
            }

            20% {
                -webkit-transform: scaleY(1.0)
            }
        }

        @keyframes sk-stretchdelay {
            0%, 40%, 100% {
                transform: scaleY(0.4);
                -webkit-transform: scaleY(0.4);
            }

            20% {
                transform: scaleY(1.0);
                -webkit-transform: scaleY(1.0);
            }
        }

        .spinnner_div {
            width: 1200px;
            height: 1000px;
            background: rgba(255, 255, 255, 0.1);
            backdrop-filter: blur(2px);
            position: absolute;
            z-index: 100;
            overflow-y: hidden;
        }


        .export-button {
            width: 35px;
            height: 42px;
            //background-color: #4CAF50; /* Green */
            border: none;
            color: transparent; /* Make the text color transparent */
            padding: 15px 32px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            float: right;
            margin-right: 10px;
            background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAACXBIWXMAAAsTAAALEwEAmpwYAAACcklEQVR4nGNgGAWjYBSMgmEJDOsNpfRqDX11aw0bdGsNN+vWGj5jGIzAvt6eRb9GX1uvxiBOt8Zwol6t0W69WsM3erVG/9HxQLuVQb3Uilev2thGt9YoDeRY3RrDI3o1ht+xOXbAPaBboSsIdmyNUb5ujdEi3Rqjq3o1Rn+JdeyAe0CPAofiwj7rA8jC3uv9jwxpD/isDyA99kY9UDsaA/8HXRJCBjCH4QOjHtBDCr3Obd0ooVO+qgpF3q7d6f+XH1/g8j07+gZXDBjWm/6/9eI23PC7r+7+N6gzgcsvPb4cLnfn5Z3/Rg1mg68YTZmfjhJCuUsKwOIevT7/f/7+CRb79+/f/+R5aYO3Hth7bT/cA5ceXwaLbbmwFS62+cKWwZ2Jvfp8//+AhjYIlKws///3318w+9P3T/8dO10Gtwf0ao3+zzowB27Jx28f4ez2LZ1Doxg1b7b+//LjSxTLrj+78d+gHpGpB20e0Ks1+m/VaofhgWfvn/03a7IaGh5YfXot3OGvP7+Bs2funz34k1Di3BRwUQkC3399/5+3tBBuIago9Z0QOHg9YNJo8f/uq3twC+YcnAcWP3r7GFzs2J0Tg9cDMw/MRil9bNscwOKhUyPgRSkIlK6sGHx5IGhy6P9ff37hbOvsvLwLJV+AMvqg8YBBncn/8w8vwB0IKoFMmyxRHTch4P+fv3/gahYfWzr4kpAehXjUAwMdA4OmItMb9UDAaAwMqSTkvd7/MMOIG9zFNbyuW2tkDJsLGNTD6+RMcOjVGnWAZmMG7QQHCYBRq95QRbfOOFS31rBNr8Zom16N4XNSDBgFo2AUjAKGIQEAkqNB3aFhJ4wAAAAASUVORK5CYII=);
            background-size: contain; /* Scale the icon to fit the button */
            background-repeat: no-repeat; /* Do not repeat the icon */
            background-position: center; /* Center the icon within the button */
            transition: transform 0.2s; /* Add a transition effect */
        }

            .export-button:hover {
                transform: scale(1.2); /* Scale up the button on hover */
            }

        @keyframes shake {
            0% {
                transform: translate(1px, 1px) rotate(0deg);
            }

            10% {
                transform: translate(-1px, -2px) rotate(-1deg);
            }

            20% {
                transform: translate(-3px, 0px) rotate(1deg);
            }

            30% {
                transform: translate(3px, 2px) rotate(0deg);
            }

            40% {
                transform: translate(1px, -1px) rotate(1deg);
            }

            50% {
                transform: translate(-1px, 2px) rotate(-1deg);
            }

            60% {
                transform: translate(-3px, 1px) rotate(0deg);
            }

            70% {
                transform: translate(3px, 1px) rotate(-1deg);
            }

            80% {
                transform: translate(-1px, -1px) rotate(1deg);
            }

            90% {
                transform: translate(1px, 2px) rotate(0deg);
            }

            100% {
                transform: translate(1px, -2px) rotate(-1deg);
            }
        }

        .shaking-image {
            animation: shake 1s cubic-bezier(.36,.07,.19,.97) both;
            transform: translate3d(0, 0, 0);
            backface-visibility: hidden;
            /*perspective: 1000px;*/
        }

            .shaking-image:hover {
                animation: shake 1s cubic-bezier(.36,.07,.19,.97) both;
                transform: translate3d(0, 0, 0);
                backface-visibility: hidden;
            }

        .modal {
            position: fixed;
            top: 97px;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 1;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
            overflow: initial;
            overflow-y: initial;
        }

        .segment1 {
            display: inline-block;
            padding-left: 0;
            margin: -2px 22px;
            border-radius: 4px;
            font-size: 13px;
            font-family: "Poppins";
            /* border: 1px solid  #66a454   #d3252a  #f59303;*/
        }

            .segment1 > li {
                display: inline-block;
                background: #fafafa;
                color: #666;
                margin-left: -4px;
                padding: 5px 10px;
                min-width: 50px;
                border: 1px solid #ddd;
            }

                .segment1 > li:first-child {
                    border-radius: 4px 0px 0px 4px;
                    border-radius: 4px 0px 0px 4px;
                }

                .segment1 > li:last-child {
                    border-radius: 0px 4px 4px 0px;
                }

                .segment1 > li.active {
                    color: #fff;
                    cursor: default;
                    background-color: #428bca;
                    border-color: #428bca;
                }

        .strike {
            text-decoration: line-through;
        }
    </style>
    <script type="text/javascript">
        var today = '', Mnth = '', Yr = '';
        var PgRecords = 10, pgNo = 1, TotalPg = 0;
        var filtrkey = '';
        var AllOrders = [];
        var rejdt = '', SF_Code = '';
        $(document).ready(function () {
            var now = new Date();
            today = now.getFullYear() + '-' + ((now.getMonth() + 1).toString().length < 2 ? '0' + (now.getMonth() + 1) : (now.getMonth() + 1));
            $('#start').attr('value', today);
            Mnth = (now.getMonth() + 1); Yr = now.getFullYear();
            //$("#reportrange").on("domsubtreemodified", function () {
            //    id = $('#orddate').text();
            //    id = id.split('-');
            //    fdt = id[2].trim() + '-' + id[1] + '-' + id[0];
            //    tdt = id[5] + '-' + id[4] + '-' + id[3].trim();
            //    //$('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");
            //    //$('#loadover').show();                   
            //});
            GetExpDet()
            $('#start').on('change', function () {
                var Mnth_yr = $('#start').attr('value').split('-');
                Mnth = Mnth_yr[1]; Yr = Mnth_yr[0];
                GetExpDet()
            })
            $(".segment>li").on("click", function () {
                $(".segment>li").removeClass('active');
                $(this).addClass('active');
                Orders = AllOrders;
                filtrkey = $(this).attr('data-va');
                $("#tSearchOrd").val('');
                ReloadTable();
            });
        })
        function GetExpDet() {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_SingleDay.aspx/GetSingle_Expense",
                data: "{'exp_years':'" + Yr + "','exp_month':'" + Mnth + "'}",
                dataType: "json",
                success: function (data) {
                    //period = '<td>' +  + '</td>';
                    sfexp = JSON.parse(data.d) || [];
                    AllOrders = sfexp;
                    Orders = AllOrders;
                    ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            })
        }
        function ReloadTable() {
            str = '';
            list = '';
            if (filtrkey != "All" && filtrkey != "") {
                Orders = Orders.filter(function (a) {
                    return a.exstatus.indexOf(filtrkey) > -1;
                })
            }
            if (Orders.length > 0) {
                st = PgRecords * (pgNo - 1); slno = 0;
                $("#ExpTbl tbody").html("");
                for (j = st; j < st + PgRecords; j++) {
                    if (j < Orders.length) {
                        slno = j + 1;
                        var dis = Orders[j].Appr_By == '<%=Session["Sf_Code"]%>' && Orders[j].exstatus == 'Need Approval' ? 'onclick="Exp($(this))"' : 'class="strike disabled"';
                        str += "<tr><td style='text-align:left'>" + slno + "</td>\
                                <td style='text-align:left' class='ret' sfcode="+ Orders[j].sf_code + ">" + Orders[j].sfname + "</td>\
                                <td style='text-align:left' ExpDt="+ Orders[j].ExpDt + ">" + Orders[j].Expense_Date + "</td>\
                                <td style = 'text-align:left' > "+ Orders[j].exstatus + "</td >\
                                <td style='text-align:left'>"+ Orders[j].Expense_wtype + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Travel + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Expense_Distance + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Expense_All_Type + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Expense_DA + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Expense_Fare + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Additional + "</td>\
                                <td style='text-align:left'>"+ Orders[j].Daily_Total + "</td>\
                                <td style='text-align:left;cursor:pointer;display:none;' onclick='View($(this))'><a>View</a></td>\
                                <td style='text-align:left;cursor:pointer;' "+ dis + "><a>Approve</a></td>\
                                <td style='text-align:left;cursor:pointer;' "+ dis + "><a>Reject</a></td></tr > ";
                    }
                }
                $("#orders_info").html("Showing " + (st + 1) + " to " + (Number(st) + Number(PgRecords)) + " of " + Orders.length + " entries");
                $("#ExpTbl tbody").append(str);
            }
            else {
                $("#ExpTbl tbody").html("");
                str = "<div class='col-lg-12' style='text-align: center;height: 50px;padding: 15px;left: 520px;color: red;'>No Data Found</div>";
                $("#ExpTbl tbody").append(str);
            }
            loadPgNos();
            $('.spinnner_div').hide();
        }
        function rjalert(titles, contents, types) {
            if (types == 'error') {
                var tp = 'red';
                var icons = 'fa fa-warning';
                var btn = 'btn-red';
            }
            else {
                var tp = 'green';
                var icons = 'fa fa-check fa-2x';
                var btn = 'btn-green';
            }
            $.confirm({
                title: '' + titles + '',
                content: '' + contents + '',
                type: '' + tp + '',
                typeAnimated: true,
                autoClose: 'action|8000',
                icon: '' + icons + '',
                buttons: {
                    tryAgain: {
                        text: 'OK',
                        btnClass: '' + btn + '',
                        action: function () {

                        }
                    }
                }
            });
        }
        function Exp(x) {
            rejdt = $(x).closest('tr').find('td').eq(2).attr('ExpDt');
            SF_Code = $(x).closest('tr').find('td').eq(1).attr('sfcode');
            rejdt = rejdt.split('T');
            if ($(x).text() == 'Approve') {
                Approve(x)
            }
            else if ($(x).text() == 'Reject') {
                Reject(x)
            }
        }
        function View(x) {
            $("#View").modal('toggle');
        }
        function Approve(x) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_SingleDay.aspx/SaveSingle_Expense",
                data: "{'SF':'" + SF_Code + "','exp_dt':'" + rejdt[0] + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d == 'Updated')
                        toastr.success(data.d, 'success');
                    else
                        toastr.success('Not Updated', 'success');
                    setTimeout(function () {
                        GetExpDet();
                    }, 2000);
                },
                error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
            });

        }
        function showERROR(jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            alert(msg);
        }
        $(document).on('click', '.toastr-yes', function () {
            var name = $(".name").val();
            if (name != "") {
                $(".toastr-confirm").hide();
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Approval_SingleDay.aspx/rejectExpense",
                    data: "{'SF':'" + SF_Code + "','rejdt':'" + rejdt[0] + "','Rtype':'" + 0 + "','Remarks':'" + name + "'}",
                    dataType: "json",
                    success: function (data) {
                        toastr.success(data.d, 'success');
                        setTimeout(function () {
                            GetExpDet();
                        }, 2000);
                    },
                    error: function (jqXHR, exception) { showERROR(jqXHR, exception); }
                });
            }
            else {
                toastr.error('Reason is Mandatory', 'Alert');
            }
        });
        function Reject(x) {
            var rtype = 0, remarks = '';

            rtype = 0;
            rejdt = $(x).closest('tr').find('td').eq(2).attr('ExpDt');
            SF_Code = $(x).closest('tr').find('td').eq(1).attr('sfcode');
            rejdt = rejdt.split('T');

            toastr.options = {
                closeButton: true,
                timeOut: 0,
                extendedTimeOut: 0,
                preventDuplicates: true,
                tapToDismiss: false
            };
            toastr.warning('<div class="toastr-confirm">Are you sure you want to continue?<div class="toast-actions"><input type="rtext" placeholder="Reject Reason" class="name form-control" required /><button type="button" class="btn btn-success toastr-yes" style="margin-right:15px">OK</button><button type="button" class="btn btn-danger toastr-no">Cancel</button></div></div>', 'Confirmation');
        }
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
    </script>
    <div class="spinnner_div" style="display: none; width: 100%">
        <div class="spinner" style="position: absolute; left: 525px; top: 133px;">
            <div class="rect1" style="background: #1a60d3;"></div>
            <div class="rect2" style="background: #DB4437;"></div>
            <div class="rect3" style="background: #F4B400;"></div>
            <div class="rect4" style="background: #0F9D58;"></div>
            <div class="rect5" style="background: orangered;"></div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 sub-header">
            Expense Report <span style="position: absolute; right: 45px;">
                <input type="month" id="start" name="start" value="" />
            </span>
        </div>
    </div>
    <div class="row" style="margin-top: 30px;">
        <div class="col-lg-6" style="margin-bottom: 1rem; float: left;">
            <ul class="segment" style="float: right;">
                <li data-va="All" class="active">ALL</li>
                <li data-va="Need Approval">Need Approval</li>
                <li data-va="Approved">Approved</li>
                <li data-va="Pending">Pending</li>
                <li data-va="Not Submit for Approval">Not Sent</li>
                <li data-va="Rejected">Rejected</li>
            </ul>
        </div>
        <div class="col-lg-6" style="display: none;">
            <button class="export-button" onclick="dwngstr_excel()" style="">Export to Excel</button>
        </div>
    </div>
    <div class="card" style="margin-top: 0px;">
        <div class="card-body table-responsive" style="overflow-x: auto;">
            <div>
                Search&nbsp;&nbsp;<input type="text" id="tSearchOrd" style="width: 250px;">
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
        </div>
        <div style="overflow-x: auto; width: 100%; margin-bottom: 8px;">
            <table id="ExpTbl" class="table" style="font-size: 12px">
                <thead>
                    <tr>
                        <th>Sl.No.</th>
                        <th>Sales Force</th>
                        <th>Expense Date</th>
                        <th>Expense Status</th>
                        <th>Work Type</th>
                        <th>Travel Location</th>
                        <th>Distance</th>
                        <th>Expense DA</th>
                        <th>DA Amount</th>
                        <th>Expense Fare</th>
                        <th>Additional Total</th>
                        <th>Total</th>
                        <th style="display: none;">View</th>
                        <th>Approve</th>
                        <th>Reject</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div class="row" style="padding: 5px 0px">
            <div class="col-sm-5">
                <div class="dataTables_info" id="orders_info" role="status" aria-live="polite">Showing 1 to 0 of 0 entries</div>
            </div>
            <div class="col-sm-7">
                <div class="dataTables_paginate paging_simple_numbers" id="example2_paginate">
                    <ul class="pagination" style="float: right; margin: -11px 0px">
                        <li class="paginate_button previous disabled" id="example2_previous"><a href="#" aria-controls="example2" data-dt-idx="1" tabindex="0">First</a></li>
                        <li class="paginate_button next" id="example2_next"><a href="#" aria-controls="example2" data-dt-idx="0" tabindex="0">Last</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="View" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="View" aria-hidden="true">
        <div class="modal-dialog" role="document" style="width: 750px">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="row">
                        <div class="col-sm-7">
                            <h5 class="modal-title" id="exampleModalLabel">Approval</h5>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" style="background-color: rgb(26 115 232 / 1);" class="btn btn-primary" id="svleave">Approve</button>
                    <button type="button" style="background-color: #e01212b5;" class="btn btn-primary" id="rjdt">Reject</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

