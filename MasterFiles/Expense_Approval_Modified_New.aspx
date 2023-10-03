<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Expense_Approval_Modified_New.aspx.cs" Inherits="MasterFiles_Expense_Approval_Modified_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        #FieldForce {
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
            }

        .strike {
            text-decoration: line-through;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <a href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.js.map"></a>
    <link href="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.css" rel="stylesheet" />
    <script src="../alertstyle/Highly-Customizable-jQuery-Toast-Message-Plugin-Toastr/build/toastr.min.js"></script>
    
    <script type="text/javascript">
        var dt = new Date();
        var pgYR = dt.getFullYear();
        var pgMNTH = dt.getMonth() + 1;
        var sfexp = [];
        var Orders = [];
        var filsfexp = [];
        var AllOrders = [];
        var sfhyr = [];
        var period = '';
        var sfchng = '';
        var ddlCustomers = '';
        var FilterOrders = []; var loc = [];
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
                url: "Expense_Approval_Modified_New.aspx/Get_FieldForce",
                data: "{'exp_years':'" + Yr + "','exp_month':'" + Mnth + "','sfcode':'" + sfcode + "'}",
                dataType: "json",
                success: function (data) {
                    period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
                    sfexp = JSON.parse(data.d) || [];
                    Orders = sfexp;
                    ReloadTable(Orders);
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });

        }
		 function ProcessExp(sfcode) {
            toastr.options = {
                closeButton: true,
                timeOut: 0,
                extendedTimeOut: 0,
                preventDuplicates: true
            };

            toastr.info('<div class="toastr-info">Are you sure. do you want to process ? <div class="toast-actions" style="margin: 10px;"><button type="button" class="btn btn-success toastr-yes" style="margin-right:15px">Yes</button><button type="button" class="btn btn-danger toastr-no">No</button></div></div>', 'Confirmation');
            $(document).on('click', '.toastr-yes', function () {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    url: "Expense_Approval_Modified_New.aspx/ProcessExp",
                    data: "{'exp_years':'" + pgYR + "','exp_month':'" + pgMNTH + "','sfcode':'" + sfcode + "'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d != "") {
                            toastr.success('Success', data.d, 'success');
                            window.location.href = 'Expense_Approval_Modified_New.aspx';
                        }
                        else
                            toastr.error('Alert!', 'Expense Cannot be process!!!', 'error');
                    },
                    error: function (result) {
                        alert(JSON.stringify(result));
                    }
                });
            });

            $(document).on('click', '.toastr-no', function () {

                toastr.warning('Expense process canceled!');
                toastr.clear();

            });
        }
        function Get_HyrCHng(sf) {
            sfchng = sf;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New.aspx/HyrCHng",
                data: "{'sfcode':'" + sf + "'}",
                dataType: "json",
                success: function (data) {
                    sfhyr = JSON.parse(data.d) || [];
                    $("#apprby").empty();
                    ddlCustomers = ('<option selected="selected" value="0">--- Select ---</option>');
                    $.each(sfhyr, function (key, val) {
                        ddlCustomers += "<option value='" + val.Appr_SF + "'>" + val.Appr_SF_Name + "</option>";
                    });
                    $("#apprby").append(ddlCustomers);
                    $('#exampleModal').modal('toggle');
                    //Hyr(name, sf);                
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
        }
        function Hyr(ApprSF) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New.aspx/Hyr",
                data: "{'ApprSF':'" + ApprSF + "','exp_years':'" + pgYR + "','exp_month':'" + pgMNTH + "','sfcode':'" + sfchng + "'}",
                dataType: "json",
                success: function (data) {
                    if (data.d != "")
                        toastr.success('Success', data.d, 'success');
                    else
                        toastr.error('Alert!', 'Hry Not Change!!!', 'error');
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
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
        function ReloadTable(Orders) {
            $('#FieldForce tbody tr').remove();
            if (Orders.length > 0) {
                for (var i = 0; i < Orders.length; i++) {
                    var cls = '', getproc = '', clss = '', sClr = "#05cc05";
                    if (Orders[i].exstatus == 'Approved') sClr = "#05cc05";
                    if (Orders[i].exstatus == 'Approved') sClr = "#05cc05";
                    if (Orders[i].exstatus == 'Approved') sClr = "#05cc05";
					getproc = ('<%=Session["sf_code"]%>' == 'admin' && '<%=Session["div_code"]%>' == '109' && (Orders[i].exstatus == 'Approved' || Orders[i].exstatus == 'Need To Process') && Orders[i].exp_proc == 0) ? "<td class='Process'><a onclick=\"ProcessExp(\'" + Orders[i].sf_code + "\')\" style='background-color: transparent;' >Process</a></td>" : (Orders[i].exstatus == 'Approved')? "<td class='Process'><a style='background-color: transparent;text-decoration: line-through;color:#05cc05'>Processed</a></td>":"<td class='Process'><a style='background-color: transparent;text-decoration: line-through;color:#a3a3a3'>Can't Process</a></td>";
                    cls = '<%=Session["sf_code"]%>' != 'admin' ? '' : Orders[i].exstatus == 'Expense not submitted' ? "" : Orders[i].exstatus == 'Not submit for approval' ? "" : Orders[i].exstatus == 'Approved' ? "" : Orders[i].exstatus == 'Rejected' ? "" : "<i class='fa fa-pencil' onclick=\"Get_HyrCHng(\'" + Orders[i].sf_code + "\')\" style='padding: 10px; '></i>";
					clss = cls != '' ? "<div class='col-lg-6'><a>" + cls + "</a></div>" : '';
                    var sty = '';
                    sty = '<td>' + Orders[i].exstatus + '</td>';
                    var strFF = "<td ><input type='hidden' name='sf_Code' value='" + Orders[i].sf_code + "'/>" + Orders[i].sfname + "</td><td>" + Orders[i].EmpID + "</td><td>" + Orders[i].StateName + "</td>" + period + sty + "<td><div class='row'><div class='col-lg-6'><a style='display:none' class='btn btn-info' href='Expense_Entry.aspx?Mode=1&SF_Code=" + Orders[i].sf_code + "&SF_EmpID=" + Orders[i].EmpID + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "'>Edit</a>";
                    strFF += " <a class='btn btn-info AAA' href='rpt_Expense_Entry_Modified.aspx?SF_Code=" + Orders[i].sf_code + "&SF_name=" + Orders[i].sfname + "&SF_EmpID=" + Orders[i].EmpID + "&SF_DesgID=" + Orders[i].Designation_Code + "&FYear=" + pgYR + "&FMonth=" + pgMNTH + "&Exp_Proc=" + ((Orders[i].exstatus.slice(0,7) == 'Pending') ? 1 : Orders[i].exp_proc) + "'>View</a></div>"+ clss +"</div>" + getproc + "";
                    strFF += "</td>";
                    $("#FieldForce tbody").append("<tr class='gvRow'>" + strFF + "</tr>");
					 if ('<%=Session["sf_code"]%>' != 'admin')
                        $(".Process").hide();
                }
            }
            else {
                $("#FieldForce tbody").append("<tr class='gvRow'><td colspan='3'>No Record Found.!.</td></tr>");
            }
        }
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                url: "Expense_Approval_Modified_New.aspx/Get_Year",
                dataType: "json",
                success: function (data) {
                    var ddlCustomers = $("#ddl_year");
                    ddlCustomers.empty().append('<option selected="selected" value="0">--- Select ---</option>');
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
                url: "Expense_Approval_Modified_New.aspx/FillMRManagers",
                dataType: "json",
                success: function (data) {
                    var sfmgr = JSON.parse(data.d);
                    var ddlCustomers = $("#ddlmgrs");
                    ddlCustomers.empty().append('<option value="0">Select Manager</option>')
                    for (var i = 0; i < sfmgr.length; i++) {
                        ddlCustomers.append('<option value="' + sfmgr[i].Sf_Code + '">' + sfmgr[i].Sf_Name + '</option>')
                    }
                },
                error: function (result) {
                    alert(JSON.stringify(result));
                }
            });
            $("#btnapprby").on('click', function () {
                //$(this).attr('value');
                var appr = $("#apprby").val();
                if (appr != "0") {
                    Hyr(appr);
                    $("#exampleModal").hide();
                    window.location.href = 'Expense_Approval_Modified_New.aspx';
                }
                else {
                    toastr.error('Alert!', 'Select ApproveBy!!!', 'error');
                }
            })
            //$("#ddl_year").val(pgYR);
            //$("#dll_month").val(pgMNTH);
            //getApprovalDets(pgYR, pgMNTH);
            if (localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, ''))) {
                loc = JSON.parse(localStorage.getItem(window.location.pathname.replace(/^.*[\\\/]/, '')));
                if (loc.length > 0) {
                    $("#ddl_year").val(loc[0].year);
                    $("#dll_month").val(loc[0].month);
                    $('#ddlmgrs > option[value="' + loc[0].sfcode + '"]').prop('selected', true);
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        async: false,
                        url: "Expense_Approval_Modified_New.aspx/Get_FieldForce",
                        data: "{'exp_years':'" + loc[0].year + "','exp_month':'" + loc[0].month + "','sfcode':''}",
                        dataType: "json",
                        success: function (data) {
                            period = '<td>' + ($('#dll_month :selected').attr('label')) + ',' + ($('#ddl_year :selected').text()) + '</td>';
                            sfexp = JSON.parse(data.d) || [];
                            if (sfexp.length == '1') {
                                $('#ddlmgrs > option[value="' + sfexp[0].Reporting_To_SF + '"]').prop('selected', true);
                            }
                            pgMNTH = loc[0].month;
                            pgYR = loc[0].year;
                            Orders = sfexp;
                            ReloadTable(Orders);
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
                getApprovalDets(pgYR, pgMNTH);
            }
            $(document).on('click', '#btnGo', function () {
                $('#ddlmgrs').val('0');
                $('#ddlsts').val('0');
                var selyear = $("#ddl_year").val();
                if (selyear == 0) { $("#container").hide(); alert("Please Select Yaer"); $("#ddl_year").focus(); return false; }
                var selmonth = $("#dll_month").val();
                if (selmonth == 0) { $("#container").hide(); alert("Please Select Month"); $("#dll_month").focus(); return false; }
                if (selyear <= 2022 && selmonth <= 11 && '<%=Session["div_code"]%>' == '100') {
                    toastr.error('Expense Can\'t be process', 'Alert!!!');
                }
                else if (selyear <= 2023 && ((selyear <= 2022) ? selmonth = selmonth : selmonth <= 4) && '<%=Session["div_code"]%>' == '109') {
                    location.href = "Expense_Approval.aspx";
                    //toastr.error('Expense Can\'t be process','Alert!!!');
                }
                else {
                    getApprovalDets(selyear, selmonth);
                }
            });


            $(document).on('click', '.AAA', function (event) {
                event.preventDefault();
                event.stopPropagation();
                var url = $(this).attr('href');
                window.open(url, 'poprpExpense1', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=1000,height=600,left = 0,top = 0');

            });
            $("#ddlmgrs").on('change', function () {
                $('#FieldForce tbody tr').remove();
                if ($("#ddlmgrs").val() != 'admin') {
                    //Get_FieldForce($("#ddlmgrs").val(), pgYR, pgMNTH);
                    FilterOrders = sfexp.filter(function (a) {
                        return (a.Reporting_To_SF == $("#ddlmgrs").val() || a.sf_code == $("#ddlmgrs").val() || a.Appr_By == $("#ddlmgrs").val());
                    });
                    ReloadTable(FilterOrders);
                }
                else if ($("#ddlmgrs").val() != '') {
                    Orders = sfexp;
                    ReloadTable(Orders);
                }
                else {
                    $("#FieldForce tbody").append("<tr class='gvRow'><td colspan='3'>No Record Found.!.</td></tr>");
                }
                $('#ddlsts').val('0');
            });
            $("#ddlsts").on('change', function () {
                if ($("#ddlmgrs").val() != '0' && $("#ddlmgrs").val() != 'admin') {
                    shText = $(this).val().toLowerCase();
                    FiltrOrd = FilterOrders.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
                    ReloadTable(FiltrOrd);
                }
                else if ($(this).val() != "" && $(this).val() != "0") {
                    shText = $(this).val().toLowerCase();
                    Orders = sfexp.filter(function (a) {
                        return (a.exstatus.toLowerCase().indexOf(shText) > -1)
                    });
                    ReloadTable(Orders);
                }
                else {
                    Orders = sfexp
                    ReloadTable(Orders);
                }

            });
        });
    </script>
    <div class="modal fade" id="exampleModal" tabindex="-1" style="z-index: 1000000; background-color: transparent;" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <%--<button type="button" id="btnpls" class="btn btn-info btn-circle btn-lg" style="float: right">+</button>--%>
                    <h5 class="modal-title" id="exampleModalLabel">Approval Hyr</h5>
                </div>
                <div class="modal-body">
                    <div class="panel panel-default">
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-sm-6">ApproveBy</div>
                                <div class="col-sm-6">
                                    <select id="apprby"></select>
                                </div>
                            </div>
                        </div>

                        <!-- /.panel-body -->
                    </div>
                </div>
                <div class="modal-footer">
                    <input type="button" id="btnapprby" value="SUBMIT" class="btn btn-primary" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <form id="Allowancefrm" runat="server">
        <div class="container" style="max-width: 100%; width: 55%">
            <div class="row">
                <asp:Label ID="lblFMonth" runat="server" Style="text-align: left; padding: 8px 4px;"
                    SkinID="lblMand" Text="Year" CssClass="col-md-1"></asp:Label>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="ddl_year" name="txt_year" class="form-control">
                            <option>--- Select ---</option>
                        </select>
                    </div>
                </div>
                <asp:Label ID="Label3" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                    Text="Month" CssClass="col-md-1"></asp:Label>
                <div class="col-md-4">
                    <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                        <select id="dll_month" name="dll_month" class="form-control">
                            <option value="0" label="--- Select---"></option>
                            <option value="1" label="JAN"></option>
                            <option value="2" label="FEB"></option>
                            <option value="3" label="MAR"></option>
                            <option value="4" label="APR"></option>
                            <option value="5" label="MAY"></option>
                            <option value="6" label="JUN"></option>
                            <option value="7" label="JUL"></option>
                            <option value="8" label="AUG"></option>
                            <option value="9" label="SEP"></option>
                            <option value="10" label="OCT"></option>
                            <option value="11" label="NOV"></option>
                            <option value="12" label="DEC"></option>
                        </select>
                    </div>
                </div>
                <a id="btnGo" class="btn btn-primary" style="vertical-align: middle; font-size: 17px;">
                    <span>Go</span></a>
            </div>
            <br />
            <div class="row" style="text-align: center">
                <div class="col-sm-10 inputGroupContainer">
                </div>
            </div>
        </div>
        <br />
        <div class="row col-md-10 col-md-offset-1" style="margin-top: 8px;">
            <asp:Label ID="Label2" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                Text="Filter By Manager" CssClass="col-md-2"></asp:Label>
            <div class="col-md-10">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <select class="form-control" id="ddlmgrs"></select>
                </div>
            </div>
        </div>
        <br />
        <div class="row col-md-10 col-md-offset-1" style="margin-top: 8px;">
            <asp:Label ID="Label1" runat="server" SkinID="lblMand" Style="text-align: left; padding: 8px 4px;"
                Text="Filter By status" CssClass="col-md-2"></asp:Label>
            <div class="col-md-10">
                <div class="input-group">
                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                    <select class="form-control" id="ddlsts">
                        <option value="0">Select Status</option>
                        <option value="">All</option>
                        <option value="Approved">Approved</option>
                        <option value="Need approval">Need approval</option>
			<option value="Need To Process">Need To Process</option>
                        <option value="Expense not submitted">Not submitted</option>
                        <option value="Not submit for approval">Not submit for approval</option>
                        <option value="Pending">Pending</option>
                        <option value="Rejected">Rejected</option>
                    </select>

                </div>
            </div>
        </div>
        <div class="container" style="max-width: 90%; width: 90%">
            <div class="row">
                <div class="col-sm-12 inputGroupContainer">
                    <table id="FieldForce" class="gvHeader card" style="display: table; box-shadow: 0px 5px 20px 4px grey;">
                        <thead>
                            <tr>
                                <th class="col-xs-8" style="text-align:left">Name</th>
                                <th class="col-xs-2" style="text-align:left">Emp ID</th>
                                <th class="col-xs-4" style="text-align:left">State</th>
                                <th style="text-align:left">Period</th>
                                <th style="text-align:left">Status</th>
                                <th style="text-align:left">View</th>
								<th class="Process" style="text-align:left">Process</th>
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
