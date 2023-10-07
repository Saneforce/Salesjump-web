<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Expense_Approval_Modified_New_Aachi.aspx.cs" Inherits="MasterFiles_Expense_Approval_Modified_New_Aachi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        /*#FieldForce {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #FieldForce td {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #FieldForce tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #FieldForce tr:hover {
                background-color: #ddd;
            }

            #FieldForce th {
                padding-top: 12px;
                padding-bottom: 12px;
                padding-left: 5px;
                padding-right: 5px;
                text-align: center;
                background-color: #496a9a;
                color: white;
            }*/
        #loader {
            position: absolute;
            left: 50%;
            top: 50%;
            z-index: 1;
            width: 120px;
            height: 120px;
            margin: -76px 0 0 -76px;
            border: 16px solid #f3f3f3;
            border-radius: 50%;
            border-top: 16px solid #3498db;
            -webkit-animation: spin 2s linear infinite;
            animation: spin 2s linear infinite;
        }

        .overlay {
            background-color: #EFEFEF;
            position: fixed;
            width: 100%;
            height: 100%;
            z-index: 1000;
            top: 0px;
            left: 0px;
            opacity: .5; /* in FireFox */
            filter: alpha(opacity=50); /* in IE */
        }

        .tableFixHead {
            overflow: auto;
            height: 600px;
        }

        .blinking:hover {
            /*animation: opacity 0.8s ease-in-out infinite;
            opacity: 0.5;*/
            color: darkred;
        }

        .tableFixHead thead th {
            position: sticky;
            top: 0;
            z-index: 1;
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            padding: 8px 16px;
        }

        fieldset {
            padding: 0.35em 0.625em 0.75em !important;
            margin: 0 2px !important;
            border: 1px solid #c0c0c0 !important;
        }

        legend {
            display: block;
            width: 70px;
            padding: 0;
            margin-bottom: 0px;
            font-size: 21px;
            line-height: inherit;
            color: #333;
            border: 0;
            /* border-bottom: 1px solid #e5e5e5; */
            padding-left: 6px;
        }


        label {
            margin-bottom: 0px;
        }

        @keyframes opacity {
            0% {
                opacity: 1;
            }

            25% {
                opacity: 0.75;
            }

            50% {
                opacity: 0.5;
            }

            75% {
                opacity: 0.25;
            }

            100% {
                opacity: 0;
            }
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth() + 1;
        var sfexp = [];
        var Orders = [];
        var filsfexp = [];
        var AllOrders = [];
        var period = '';
        var Periodic = []; var datesArr = []; PriOrd = []; var startDt = '', endDt = '', selPri = '', selPriNm = '';
        searchKeys = "exstatus";
        function getApprovalDets(Yr, Mnth) {
            pgYR = Yr; pgMNTH = Mnth;
            if (Mnth == "0") {
                alert("Please Select Month");
            }
            else {
                Get_FieldForce('', Yr, Mnth);
            }
        }
        function Get_FieldForce(sfcode, Yr, Mnth) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New_Aachi.aspx/Get_FieldForce",
                data: "{'exp_years':'" + Yr + "','exp_month':'" + Mnth + "','sfcode':'" + sfcode + "','PriID':" + selPri + "}",
                dataType: "json",
                success: function (data) {
                    //period = '<td>' +  + '</td>';
                    sfexp = JSON.parse(data.d) || [];
                    Orders = sfexp;
                    ReloadTable();
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function ReloadTable() {
            $('#FieldForce tbody tr').remove();
            if (Orders.length > 0) {
                for (var i = 0; i < Orders.length; i++) {
                    //if(Orders[i].Reporting_To_SF==selMgr && Orders[i].sts==selSts){}
                    var sty = '';
                    //" +( ((i % 2) == 0) ? "class='div'" : "" )+ "
                    var strFF = "<td style='padding:10px;height:50px;'  ><input type='hidden' name='sf_Code' value='" + Orders[i].sf_code + "'/>" + Orders[i].sfname + "<div colspan='3' style='position:absolute;font-size:11px;' ><lable style='font-weight: bold;'>Status : </lable>\
                                 <lable style='font-weight: bold;'>" + $("#ddlPeriodic option:selected").text() + " : " + "</lable>" + ($('#dll_month :selected').text()) + ',' + ($('#ddl_year :selected').text()) + "</div></td><td>" + Orders[i].exstatus + "</td><td style='text-align: center;'>\
                                 <a style='display:none' class='btn btn-info' href='Expense_Entry.aspx?Mode=1&SF_Code=" + Orders[i].sf_code + "&SF_EmpID=" + Orders[i].EmpID + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>Edit</a>";
                    strFF += " <div style='margin:10px;padding:10px;' class='AAA' href='rpt_Expense_Entry_Modified_Aachi.aspx?SF_Code=" + Orders[i].sf_code + "&SF_name=" + Orders[i].sfname + "&SF_EmpID=" + Orders[i].EmpID + "&SF_DesgID=" + Orders[i].Designation_Code + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "&Fdt=" + startDt + "&Tdt=" + endDt + "&PID=" + selPri + "&PNm=" + selPriNm + "&statecode="+ Orders[i].State_Code +"'><i class='fa fa-eye'></i></div>";
                    strFF += "</td>";
                    $("#FieldForce tbody").append("<tr class='blinking' >" + strFF + "</tr>");
                }
            }
            else {
                $("#FieldForce tbody").append("<tr><td colspan='3' style='text-align: center;font-size: large;color: red;'>No Record Found.!.</td></tr>");
            }
        }
	/*onclick='windowopn(" + Orders[i].sf_code + "," + Orders[i].sfname + "," + Orders[i].EmpID + "," + Orders[i].Designation_Code + "," + pgYR + "," + pgMNTH + "," + startDt + "," + endDt + "," + selPri + "," + selPriNm + ")'
	function windowopn(sf_code, sfname, EmpID, Designation_Code, pgYR, pgMNTH, startDt, endDt, selPri, selPriNm) {
            var href = "rpt_Expense_Entry_Modified_Aachi.aspx?SF_Code=" + sf_code.toString() + "&SF_name=" + sfname + "&SF_EmpID=" + EmpID + "&SF_DesgID=" + Designation_Code + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "&Fdt=" + startDt + "&Tdt=" + endDt + "&PID=" + selPri + "&PNm=" + selPriNm + "";
            window.open(href, "Expense", 'resizable=yes,toolbar=no,scrollbars=1,menubar=no,status=no,width=1100,height=700,left=0,top=0');
        }*/
        $(document).ready(function () {
            $("#ddlsts").selectpicker('reload');

            $("#tSearchOrd").attr('action', 'javascript:void(0);')
            $('#reportrange').css("pointer-events", "none");
            //$("#reportrange").on("DOMSubtreeModified", function () {
            //    id = $('#ordDate').text();
            //    id = id.split('-');
            //    FDT = id[2].trim() + '-' + id[1] + '-' + id[0];
            //    TDT = id[5] + '-' + id[4] + '-' + id[3].trim();
            //    //$('#ddlsf').val(0).trigger('chosen:updated').css("width", "100%");
            //    //$('#loadover').show();                   
            //});
            FillPri(pgMNTH, pgYR);
            datechng();

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New_Aachi.aspx/Get_Year",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddl_year");
                    ddlCustomers.empty().append('<option selected="selected" value="0">Nothing Seleted</option>');
                    $.each(data.d, function () {
                        ddlCustomers.append($("<option></option>").val(this['years']).html(this['years']));
                    });
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New_Aachi.aspx/FillMRManagers",
                dataType: "json",
                success: function (data) {
                    var sfmgr = JSON.parse(data.d);
                    var ddlCustomers = $("#ddlmgrs");
                    ddlCustomers.empty().append('<option value="0">Select Manager</option>')
                    for (var i = 0; i < sfmgr.length; i++) {
                        ddlCustomers.append('<option value="' + sfmgr[i].sf_code + '">' + sfmgr[i].Sf_Name + '</option>')
                    }
                    $("#ddlmgrs").selectpicker('reload');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

            //$("#ddl_year").val(pgYR);
            //$("#dll_month").val(pgMNTH);
            //getApprovalDets(pgYR, pgMNTH);
            if (localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, ''))) {
                var loc = JSON.parse(localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, '')));
                if (loc.length > 0) {
                    $("#ddl_year").val(loc[0].year);
                    $("#dll_month").val(loc[0].month);
                    $("#ddlPeriodic").val(loc[0].PeriodID);
                    $('#ddlmgrs > option[value="' + loc[0].sfcode + '"]').prop('selected', true);
                    $("#ddlPeriodic").selectpicker('reload');
                    $("#ddl_year").selectpicker('reload');
                    $("#dll_month").selectpicker('reload');

                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Expense_Approval_Modified_New_Aachi.aspx/Get_FieldForce",
                        data: "{'exp_years':'" + loc[0].year + "','exp_month':'" + loc[0].month + "','sfcode':'" + '' + "','PriID':" + loc[0].PeriodID + "}", //,'Manager':'" + selMgr + "','Status':'" + selSts + "'}",
                        dataType: "json",
                        success: function (data) {
                            // period = '<td>' + ($('#dll_month :selected').text()) + ',' + ($('#ddl_year :selected').text()) + '</td>';
                            sfexp = JSON.parse(data.d) || [];
                            if (sfexp.length == '1') {
                                $('#ddlmgrs > option[value="' + sfexp[0].Reporting_To_SF + '"]').prop('selected', true);
                            }
                            Orders = sfexp;
                            pgMNTH = loc[0].month;
                            pgYR = loc[0].year;
                            Pri();
                        },
                        error: function (result) {
                            alert(JSON.stringify(result));
                        }
                    });
                }
            }
            else {
                $("#ddl_year").val(pgYR);
                $("#dll_month").val(pgMNTH);
                $('#ddlmgrs > option[value="0"]').prop('selected', true);

                $("#ddl_year").selectpicker('reload');
                $("#dll_month").selectpicker('reload');
                //getApprovalDets(pgYR, pgMNTH);
            }
            $(document).on('click', '#btnGo', function () {
                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }

                selMgr = $("#ddlmgrs").val()
                //if (selMgr == 0) { $("#container").hide(); alert("Please Select Manager"); $("#ddlmgrs").focus(); return false; }
                selSts = $("#ddlsts").val()
                //if (selSts == 0) { $("#container").hide(); alert("Please Select Status"); $("#ddlsts").focus(); return false; }

                $('#ddlsts > option[value="0"]').prop('selected', true);
                $('#ddlmgrs > option[value="0"]').prop('selected', true);
                pgYR = selyear;
                pgMNTH = selmonth;
                datechng();
                selPri = $("#ddlPeriodic").val();
                selPriNm = $("#ddlPeriodic option:selected").text();
                if (selPri == 0) { $("#container").hide(); alert("Please Select Periodic"); $("#ddlPeriodic").focus(); return false; }
                Pri();
                if ('<%=Session["div_code"]%>' != '100') {
                    getApprovalDets(selyear, selmonth);
                }
                else if ((selyear > 2022 || selmonth > 11) && '<%=Session["div_code"]%>' == '100') {
                    //getApprovalDets(selyear, selmonth);
                    alert('Expense Can\'t be process');
                }
                else {
                    //alert('Expense Can\'t be process');
                    getApprovalDets(selyear, selmonth);
                }

                $(".fltr").show();
            });



            $(document).on('click', '.AAA', function (event) {
                event.preventDefault();
                event.stopPropagation();
                var url = $(this).attr('href');
                window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

            });
            $("#ddlPeriodic").on('change', function () {
                Pri()
            });
            $("#ddl_year").on('change', function () {
                pgYR = $("#ddl_year").val();
                FillPri(pgMNTH, pgYR);
            });
            $("#dll_month").on('change', function () {
                pgMNTH = $("#dll_month").val();
                FillPri(pgMNTH, pgYR);
            });
            $("#ddlmgrs").on('change', function () {
                $('#FieldForce tbody tr').remove();
                if ($("#ddlmgrs").val() != 'admin') {
                    //Get_FieldForce($("#ddlmgrs").val(), pgYR, pgMNTH);
                    Orders = sfexp.filter(function (a) {
                        return (a.Reporting_To_SF == $("#ddlmgrs").val()) || (a.sf_code == $("#ddlmgrs").val());
                    });
                    ReloadTable();
                }
                else if ($("#ddlmgrs").val() != '') {
                    Orders = sfexp;
                    ReloadTable();
                }
                else {
                    $("#FieldForce tbody").append("<tr ><td colspan='3'>No Record Found.!.</td></tr>");
                }
            });
            $("#ddlsts").on('change', function () {
                if ($(this).val() != "" && $(this).val() != "0") {
                    shText = $(this).val().toLowerCase();
                    Orders = sfexp.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
                }
                else
                    Orders = sfexp
                ReloadTable();
            });

            $("#tSearchOrd").on('keydown', function (event) {
                if (event.key === 'Enter') {
                    event.preventDefault();
                }
            });
            $("#tSearchOrd").on("keyup", function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                }
                var value = $(this).val().toLowerCase();
                $("#FieldForce tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });

            });

        });
        function Pri() {
            selPri = $("#ddlPeriodic").val();
            selPriNm = $("#ddlPeriodic option:selected").text();
            if (selPri == 0) { $("#container").hide(); alert("Please Select Periodic"); $("#ddlPeriodic").focus(); return false; }
            PriOrd = Periodic.filter(function (a) {
                return (a.Period_Id == $("#ddlPeriodic ").val())
            });
            if (PriOrd.length != 0) {
                var frmnth = (pgMNTH.length == 2) ? pgMNTH : '0' + pgMNTH;
                const startDate = frmnth + '-' + PriOrd[0].From_Date + '-' + pgYR;
                const endDate = PriOrd[0].To_Date <= '31' ? frmnth + '-' + PriOrd[0].To_Date + '-' + pgYR : datesArr[datesArr.length - 1].format('MM-DD-YYYY');

                startDt = pgYR + '-' + frmnth + '-' + PriOrd[0].From_Date;
                endDt = PriOrd[0].To_Date <= '31' ? pgYR + '-' + frmnth + '-' + PriOrd[0].To_Date : datesArr[datesArr.length - 1].format('YYYY-MM-DD');
                // Set the date range picker value and options
                $('#reportrange span').html(startDate + ' - ' + endDate);
                $('#reportrange').daterangepicker({
                    startDate: startDate,
                    endDate: endDate,
                });
                getApprovalDets(pgYR, pgMNTH);
                ReloadTable();
            }
            else {
                datechng();
            }
        }
        function FillPri(Mn, Yr) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                data: "{'Mn':'" + Mn + "','Yr':'" + Yr + "'}",
                url: "Expense_Approval_Modified_New_Aachi.aspx/FillPeriodic",
                dataType: "json",
                success: function (data) {
                    Periodic = JSON.parse(data.d);
                    $('#ddlPeriodic').selectpicker('destroy').empty().append('<option value="0">Select Period</option>');
                    for (var i = 0; i < Periodic.length; i++) {
                        //str = i == 0 ? "selected" : "";
                        $('#ddlPeriodic').append('<option value="' + Periodic[i].Period_Id + '">' + Periodic[i].Period_Name + '</option>');
                    }
                    $("#ddlPeriodic").selectpicker('reload');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            datechng();
        }
        function datechng() {
            // Set the month and year you want to get the dates for
            const year = pgYR;
            const month = pgMNTH - 1; // January is 0, so February is 1, and so on...

            // Create a Moment object for the first day of the month
            const firstDayOfMonth = moment({ year, month, day: 1 });

            // Get the number of days in the month
            const daysInMonth = firstDayOfMonth.daysInMonth();

            // Create an array of dates for the month
            const datesArray = [];

            for (let day = 1; day <= daysInMonth; day++) {
                const date = moment({ year, month, day });
                datesArray.push(date);
            }
            datesArr = datesArray;
            // Set the start and end dates for the date range picker

            const startDate = datesArray[0].format('MM-DD-YYYY');
            const endDate = datesArray[datesArray.length - 1].format('MM-DD-YYYY');
            startDt = datesArray[0].format('YYYY-MM-DD');
            endDt = datesArray[datesArray.length - 1].format('YYYY-MM-DD');
            // Set the date range picker value and options
            $('#reportrange span').html(startDate + ' - ' + endDate);
            $('#reportrange').daterangepicker({
                startDate: startDate,
                endDate: endDate,
            });
            ReloadTable();
        }
    </script>
    <link href="../css/SalesForce_New/bootstrap-select.min.css" rel='stylesheet' type='text/css' />
    <form id="Allowancefrm" runat="server">
        <div class="col-lg-12 sub-header">
            Expense Approval<span style="float: right; margin-right: 15px;">
                <div>
                    <div id="reportrange" class="form-control mt-5 mb-5" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc;">
                        <i class="fa fa-calendar"></i>&nbsp;               
                        <span id="ordDate"></span>
                    </div>

                </div>
            </span>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <fieldset>
                    <legend>Filters</legend>
                    <div class="col-lg-2">
                        <label id="lblFMonth" style="text-align: right; padding: 8px 4px;" text="Year">
                            Year</label>
                        <div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <select id="ddl_year" name="txt_year" class="form-control">
                                    <option>Nothing Selected</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <label id="Label3" runat="server" skinid="lblMand" style="text-align: right; padding: 8px 4px;">
                            Month</label>
                        <div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <select id="dll_month" name="dll_month" class="form-control">
                                    <option value="0" label="" selected>Nothing Selected</option>
                                    <option value="1" label="">JAN</option>
                                    <option value="2" label="">FEB</option>
                                    <option value="3" label="">MAR</option>
                                    <option value="4" label="">APR</option>
                                    <option value="5" label="">MAY</option>
                                    <option value="6" label="">JUN</option>
                                    <option value="7" label="">JUL</option>
                                    <option value="8" label="">AUG</option>
                                    <option value="9" label="">SEP</option>
                                    <option value="10" label="">OCT</option>
                                    <option value="11" label="">NOV</option>
                                    <option value="12" label="">DEC</option>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-2">
                        <label id="lblpri" style="text-align: right; padding: 8px 4px;">Periodic</label>
                        <div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <select class="form-control" id="ddlPeriodic">
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3">
                        <label id="Label2" style="text-align: left; padding: 8px 4px;">Filter By Manager</label>
                        <div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <select class="form-control" id="ddlmgrs"></select>
                            </div>
                        </div>
                    </div>


                    <div class="col-lg-3">
                        <label id="Label1" style="text-align: left; padding: 8px 4px;">Filter By status</label>
                        <div>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                 <select class="form-control" id="ddlsts">
									<option value="0">Select Status</option>
									<option value="">All</option>
									<option value="Approved">Approved</option>
									<option value="Need approval">Need approval</option>
									<option value="Expense not submitted">Not submitted</option>
									<option value="Not submit for approval">Not submit for approval</option>
									<option  value="Pending">Pending</option>
									<option  value="Rejected">Rejected</option>
								</select>
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>
            <div class="col-lg-12" style="text-align: end; margin: 15px; padding-right: 55px;">
                <input id="btnGo" type="button" class="btn btn-primary" style="width: 50px;display:none;" value="Go" />
            </div>
            <div class="col-lg-12">

                <div class="card table table-responsive" style="margin-top: 13px; max-height: 600px; overflow: scroll; box-shadow: 0px 2px 2px 0px rgb(0 192 239 / 39%);">
                    <div style="white-space: nowrap; padding: 10px; position: sticky; top: 0px; z-index: 1; background: white;">
                        <div style="width: 30%;">
                            <label>Search</label>
                            <input type="text" id="tSearchOrd" class="form-control">
                        </div>
                    </div>
                    <table id="FieldForce" style="display: table;">
                        <thead style="position: sticky; top: 71px; z-index: 5; background: white; text-align: center;">
                            <tr>
                                <th style="text-align: left;" class="col-xs-8">Name</th>
                                <th style="text-align: left;">Status</th>
                                <th>
                                    <img style="width: 30px;" src='data:image/png;base64,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' />
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
